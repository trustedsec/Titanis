using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Mssamr;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Cli.SamTool;

/// <task category="SAM;Enumeration">Get the members of an alias (group)</task>
[Command]
[Description("Gets the members of an alias")]
[DetailedHelpText("You may specify an alias either as a name, decimal RID, or hex RID prefixed with 0x.  You may specify multiple aliases.")]
[Example("Look up administrators", "LUMON-FS1 -UserName LUMON\\milchick -Password Br3@kr00m! -EncryptRpc 544", Tag = "milchickNtlm_544")]
[Example("Look up multiple aliases", "LUMON-FS1 -UserName LUMON\\milchick -Password Br3@kr00m! -EncryptRpc Administrators, \"Backup Operators\"", Tag = "milchickNtlm_multi")]
[Example("Look up bad alias", "LUMON-FS1 -UserName LUMON\\milchick -Password Br3@kr00m! -EncryptRpc Administrators, \"Backup Operators\"", Tag = "milchickNtlm_BadAlias")]
[OutputRecordType(typeof(SamMembership))]
public class AliasMembersCommand : SamDomainEnumCommand
{
	protected override SamDomainAccessRights RequiredDomainAccess => SamDomainAccessRights.Lookup
	;

	[Parameter(After = nameof(ServerName))]
	[Description("Name or RID of alias")]
	public string[] AliasRidOrName { get; set; }

	private HashSet<string> _allNames = new HashSet<string>();
	private HashSet<string> _resolvedNames = new HashSet<string>();

	protected override async Task RunAsync(SamDomain domain, SamEntry domainInfo, Sam sam, CancellationToken cancellationToken)
	{
		List<uint> rids = new List<uint>();
		List<string> names = new List<string>();
		foreach (var aliasRidOrName in this.AliasRidOrName)
		{
			if ((aliasRidOrName.StartsWith("0x") && uint.TryParse(aliasRidOrName, System.Globalization.NumberStyles.HexNumber, null, out var aliasRid))
				|| uint.TryParse(aliasRidOrName, out aliasRid)
				)
			{
				rids.Add(aliasRid);
			}
			else
			{
				names.Add(aliasRidOrName);
				this._allNames.Add(aliasRidOrName);
			}
		}

		if (names.Count > 0)
		{
			SamEntry[]? rids2;
			try
			{
				rids2 = await domain.LookupNamesAsync(names.ToArray(), cancellationToken);
			}
			catch (NtstatusException ex) when (ex.StatusCode == Ntstatus.STATUS_NONE_MAPPED)
			{
				// UNDONE: Checked after all domains enumerated
				//this.WriteError($"None of the names could be resolved");
				rids2 = [];
			}
			catch (Exception ex)
			{
				this.WriteWarning($"Failed to resolve alias names: {ex.Message}");
				rids2 = [];
			}

			foreach (var entry in rids2)
			{
				rids.Add(entry.Id);
				if (entry.EntryType != SamEntryType.Unknown)
					this._resolvedNames.Add(entry.Name);
			}
		}


		foreach (var rid in rids)
		{
			try
			{
				var alias = await domain.OpenAliasAsync(rid, SamAliasAccessRights.ListMembers, cancellationToken);
				var members = await alias.GetMembersAsync(cancellationToken);
				this.WriteRecords(members);
			}
			catch
			{

			}
		}
	}

	protected override ValueTask OnAfterDomains(Sam sam, CancellationToken cancellationToken)
	{
		foreach (var name in this._allNames)
		{
			if (!this._resolvedNames.Contains(name))
				this.WriteError($"Could not resolve name '{name}'.");
		}
		return base.OnAfterDomains(sam, cancellationToken);
	}
}
