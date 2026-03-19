using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mswkst;
using Titanis.Winterop;

namespace Titanis.Smb2.Cli
{
	/// <task category="Enumeration">Enumerate the shares of an SMB server</task>
	[Command]
	[OutputRecordType(typeof(ShareInfo))]
	[Description("Lists shares on the server")]
	[DetailedHelpText(@"The Server service returns different levels of share information.  Anything about Level1 requires administrator access.  By default, {0} attempts to query for Level502; if this fails, it falls back to Level1, returning a subset of the available fields.  You may specify one or more levels to override this default, or specify the desired fields.  In the latter case, {0} determines which levels to query to populate the requested fields.")]
	[Example("List basic share info", "{0} LUMON-FS1 -UserName marks@LUMON -Password She's@live!! -Kdc LUMON-DC1", Tag = "marks_basic")]
	[Example("List Level2", "{0} LUMON-FS1 -UserName milchick@LUMON -Password Br3@kr00m! -Kdc LUMON-DC1 -OutputFields ShareName, Type, Path", "Since Path requires Level2, the command attempts to query Level2, falling back to Level1 if necessary", Tag = "milchick_Level2")]
	[Example("List security descriptors", "{0} LUMON-FS1 -UserName milchick@LUMON -Password Br3@kr00m! -Kdc LUMON-DC1 -OutputFields ShareName, Type, Path, SecurityDescriptor", Tag = "milchick_SecDesc")]
	public sealed class Smb2EnumSharesCommand : ServerServiceRpcCommand
	{
		[Parameter]
		[Description("Which level(s) of detail to query")]
		public ShareInfoLevel[]? Level { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);
		}

		protected sealed override async Task<int> RunAsync(ServerServiceClient srvs, CancellationToken cancellationToken)
		{
			List<ShareInfo>? shares = await this.DoMultilevelQuery<ShareInfoLevel, ShareInfo, string>(
				this.Level,
				ShareInfoLevel.Level502,
				ShareInfoLevel.Level1,
				_infoLevelMap,
				(ShareInfo r) => r.ShareName,
				(level, cx) =>
				{
					return srvs.GetShares(@"\\" + this.ServerName, level, this.BufferSize, cancellationToken);
				},
				cancellationToken);

			this.WriteRecords(shares);

			return 0;
		}

		private async Task<List<TResult>?> DoMultilevelQuery<TLevel, TResult, TKey>(
			TLevel[]? userLevels,
			TLevel defaultLevel,
			TLevel? fallbackLevel,
			Dictionary<TLevel, string[]> fieldMap,
			Func<TResult, TKey> keySelector,
			Func<TLevel, CancellationToken, Task<IList<TResult>>> queryFunc, CancellationToken cancellationToken)
			where TLevel : struct
		{
			// Determine which levels are required
			IList<TLevel> levels;
			if (this.OutputFields is null)
			{
				levels = userLevels.IsNullOrEmpty() ? [defaultLevel] : userLevels!;

				// List all fields for this level
				this.OutputFields = fieldMap.Where(r => levels.Contains(r.Key)).SelectMany(r => r.Value).Distinct().ToArray();
			}
			else if (userLevels.IsNullOrEmpty())
			{
				// No levels specified

				// Determine the lowest level required for each field
				var fieldLookup = fieldMap.SelectMany(r => r.Value, (r, s) => new { Level = r.Key, Field = s }).ToLookup(r => r.Field, r => r.Level, StringComparer.OrdinalIgnoreCase);

				HashSet<TLevel> reqLevels = new HashSet<TLevel>();
				foreach (var field in this.OutputFields)
				{
					var fieldLevels = fieldLookup[field];
					if (fieldLevels.Count() == 0)
						// Unknown field; skip it
						;
					else
						reqLevels.Add(fieldLevels.Min());
				}

				// Now loop through through the levels until all fields are accounted for
				var reqFields = new HashSet<string>(this.OutputFields, StringComparer.OrdinalIgnoreCase);
				List<TLevel> actualLevels = new List<TLevel>();
				foreach (var level in reqLevels.OrderDescending())
				{
					if (fieldMap.TryGetValue(level, out var levelFields))
					{
						foreach (var levelField in levelFields)
						{
							if (reqFields.Remove(levelField))
							{
								actualLevels.Add(level);
								break;
							}
						}
					}
				}
				levels = actualLevels;
			}
			else
			{
				// Build field list for levels
				HashSet<string> fields = new HashSet<string>();
				foreach (var level in userLevels)
				{
					if (fieldMap.TryGetValue(level, out var levelFields))
					{
						foreach (var field in levelFields)
						{
							fields.Add(field);
						}
					}
				}

				levels = userLevels;
				this.OutputFields = fields.ToArray();
			}

			var log = this.Log;

			// Get info for each level
			List<TResult>? shares = null;
			bool fallback = false;
			for (int i = 0; i < levels.Count; i++)
			{
				TLevel level = fallback ? fallbackLevel.Value : levels[i];

				try
				{
					var levelShares = await queryFunc(level, cancellationToken);
					if (shares == null)
						shares = [.. levelShares];
					else if (fieldMap.TryGetValue(level, out var levelFields))
					{
						foreach (var share in levelShares)
						{
							var key = keySelector(share);
							var existingShare = shares.FirstOrDefault(r => EqualityComparer<TKey>.Default.Equals(keySelector(r), key));
							if (existingShare is null)
								shares.Add(existingShare);
							else
							{
								foreach (var field in levelFields)
								{
									try
									{
										object fieldValue = share.GetType().InvokeMember(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.GetProperty, null, share, null);
										existingShare.GetType().InvokeMember(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.SetProperty, null, existingShare, null);
									}
									catch (Exception ex)
									{
										log?.WriteWarning($"Failed to copy field {field} on object with key {key}.");
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.Log.WriteWarning($"Field retrieving info for level {level}: {ex.Message}");
					if (fallbackLevel.HasValue && !fallback)
					{
						// Try the fallback level
						fallback = true;
						i = -1;
						continue;
					}
					else if (fallback)
						// The fallback failed; bail
						break;
				}
			}

			return shares;
		}

		private Dictionary<ShareInfoLevel, string[]> _infoLevelMap = new()
		{
			[ShareInfoLevel.Level0] = [nameof(ShareInfo.ShareName)],
			[ShareInfoLevel.Level1] = [nameof(ShareInfo.ShareName), nameof(ShareInfo.Remark)],
			[ShareInfoLevel.Level2] = [nameof(ShareInfo.ShareName), nameof(ShareInfo.ShareType), nameof(ShareInfo.Remark), nameof(ShareInfo.Permissions), nameof(ShareInfo.MaxUses), nameof(ShareInfo.CurrentUses), nameof(ShareInfo.Path), nameof(ShareInfo.Password)],
			[ShareInfoLevel.Level501] = [nameof(ShareInfo.ShareName), nameof(ShareInfo.Remark), nameof(ShareInfo.Flags)],
			[ShareInfoLevel.Level502] = [nameof(ShareInfo.ShareName), nameof(ShareInfo.ShareType), nameof(ShareInfo.Remark), nameof(ShareInfo.Permissions), nameof(ShareInfo.MaxUses), nameof(ShareInfo.CurrentUses), nameof(ShareInfo.Path), nameof(ShareInfo.Password), nameof(ShareInfo.SecurityDescriptor)],
		};
	}
}
