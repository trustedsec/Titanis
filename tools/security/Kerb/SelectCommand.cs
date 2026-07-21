using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Text.RegularExpressions;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb
{
	/// <task category="Kerberos">Describe a Kerberos ticket (offline)</task>
	/// <task category="Kerberos">Print the contents of a .kirbi file (offline)</task>
	/// <task category="Kerberos">Print the contents of a .ccache file (offline)</task>
	/// <task category="Kerberos">Convert between a .ccache file and a .kirbi file (offline)</task>
	/// <task category="Kerberos">Query tickets within a .ccache file or .kirbi file (offline)</task>
	/// <task category="Kerberos">Decrypt tickets (offline)</task>
	/// <task category="Kerberos">Print ticket authorization data (offline)</task>
	[Command]
	[OutputRecordType(typeof(TicketInfo), DefaultOutputStyle = OutputStyle.Table, DefaultFields = new string[]
	{
		nameof(TicketInfo.SeqNbr), nameof(TicketInfo.ClientName), nameof(TicketInfo.ClientRealm), nameof(TicketInfo.TargetSpn), nameof(TicketInfo.EndTime), nameof(TicketInfo.KdcOptions), nameof(TicketInfo.Comment)
	})]
	[Description("Selects and displays tickets from a file.")]
	[DetailedHelpText(@"This command reads tickets from one or more files (.kirbi or .ccache), optionally filters them, and optionally writes the results to another file (either .kirbi or .ccache).  It can be used to inspect files, convert files, combine files, or remove tickets from files.

The command accepts both -TicketCache and -From to specify one or more files to read tickets from.  If -From is specified, -TicketCache is ignored.  This is to facilitate the use of $KRB5CCNAME.  If this environment variable is set, you don't need to specify -From.  If you specify -From, this expresses your desire to ignore the ticket cache.

Specify the source files using -From.  You may specify multiple files and multiple wildcard patterns.  {0} reads all files from the tickets and applies any filters specified before printing the tickets to the screen.  If you specify -Into, the results are written to the file you specify.  Use -Overwrite to overwrite the outptu file if it already exists.")]
	[Example("Print tickets from all milchick*.ccache files", @"{0} -From milchick*.ccache", Tag = "AllMilchickCcache")]
	[Example("Combine tickets from all milchick*.kirbi files", @"{0} -From milchick*.ccache -Into all-milchick.ccache", Tag = "CombineMilchickCache")]
	[Example("Print only current tickets from all mlichick*.kirbi files", @"{0} -From milchick*.kirbi -Current")]
	[Example("Print only TGTs", @"{0} -From milchick*.kirbi -MatchingSpn krbtgt/.*")]
	[Example("Print only tickets for CIFS", @"{0} -From milchick*.kirbi -MatchingSpn cifs/.*")]
	[Example("Print only tickets targeting LUMON-FS1", @"{0} -From milchick*.kirbi -MatchingSpn .*/LUMON-FS1")]
	[Example("Print only tickets #1, 3-5, 7+", @"{0} -From milchick*.kirbi -SeqNbr 1, 3-5, 7-*")]
	public class SelectCommand : Command
	{
		private const string TicketSourceCategory = "Ticket Source";
		private const string TicketFilterCategory = "Ticket Filter";

		#region Source
		[Parameter(0)]
		[Description("File names or patterns")]
		[Category(TicketSourceCategory)]
		[KerberosTicketFileSpec(true)]
		public FileSpec[]? From { get; set; }

		[Parameter(EnvironmentVariable = KerberosClient.Krb5CacheVariableName)]
		[Category(TicketSourceCategory)]
		[Description("Name of ticket cache file")]
		[KerberosTicketFileSpec(true)]
		public FileSpec? TicketCache { get; set; }
		#endregion

		#region Output
		[Parameter]
		[Description("Overwrites target file if it exists")]
		[Category(ParameterCategories.Output)]
		public SwitchParam Overwrite { get; set; }

		[Parameter]
		[Description("Target file name")]
		[Category(ParameterCategories.Output)]
		[KerberosTicketFileSpec(false)]
		public FileSpec? Into { get; set; }
		#endregion

		[Parameter]
		[Description("Key to decrypt the ticket")]
		[Category("Ticket Decryption")]
		public HexString[]? TicketKey { get; set; }

		[Parameter]
		[Description("Password for service account")]
		[Category("Ticket Decryption")]
		public string[]? ServicePassword { get; set; }

		[Parameter]
		[Description("Salt for service account")]
		[Category("Ticket Decryption")]
		public string[]? ServiceSalt { get; set; }

		#region Filters
		[Parameter]
		[Description("Only select tickets currently valid")]
		[Category(TicketFilterCategory)]
		public SwitchParam Current { get; set; }

		[Parameter]
		[Description("Regex of client name to match")]
		[Category(TicketFilterCategory)]
		public string[]? MatchingClientName { get; set; }
		private Regex[]? _clientNamePatterns;

		[Parameter]
		[Description("Regex of SPN to match")]
		[Category(TicketFilterCategory)]
		public string[]? MatchingSpn { get; set; }
		private Regex[]? _spnPatterns;

		[Parameter]
		[Description("Filter for ticket encryption type")]
		[Category(TicketFilterCategory)]
		public EType[]? MatchingTicketEType { get; set; }

		[Parameter]
		[Description("Filter for session key encryption type")]
		[Category(TicketFilterCategory)]
		public EType[]? MatchingSessionEType { get; set; }

		[Parameter]
		[Description("Seq. nbr. or range")]
		[Category(TicketFilterCategory)]
		public NumberOrRange[]? SeqNbr { get; set; }

		[Parameter]
		[Description("Invert match; select whatever doesn't match")]
		[Category(TicketFilterCategory)]
		public SwitchParam InvertMatch { get; set; }
		#endregion

		[Parameter]
		[Category(ParameterCategories.Output)]
		[Description("Prints ticket authorization data (if decrypted)")]
		public SwitchParam PrintAuthData { get; set; }

		private Regex BuildRegexFor(string pattern)
		{
			bool hasLookbehind = pattern.Contains(@"(?<=") || pattern.Contains("(?<!");
			if (!hasLookbehind)
				pattern = "^" + pattern;

			bool hasLookahead = pattern.Contains(@"(?=") || pattern.Contains("(?!");
			if (!hasLookahead)
				pattern += "$";

			return new Regex(pattern, RegexOptions.IgnoreCase);
		}
		private Regex[]? ToRegex(string[]? patterns)
		{
			return (patterns == null) ? null : Array.ConvertAll(patterns, BuildRegexFor);
		}

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			this._clientNamePatterns = this.ToRegex(this.MatchingClientName);
			this._spnPatterns = this.ToRegex(this.MatchingSpn);

			if (this.TicketCache == null && this.From.IsNullOrEmpty())
			{
				context.LogError(nameof(From), $"You must specify either -{nameof(this.From)} or -{nameof(this.TicketCache)}");
			}
		}

		private bool MatchesPattern(string test, Regex[]? patterns)
		{
			return ((patterns.IsNullOrEmpty()) || (patterns.Any(r => r.Match(test).Success)));
		}

		private bool Matches(TicketInfo ticket)
		{
			bool matches =
				(!this.Current.IsSpecified || (ticket.IsCurrent == this.Current.IsSet))
				&& MatchesPattern(ticket.ClientName, this._clientNamePatterns)
				&& MatchesPattern(ticket.TargetSpn.ToString(), this._spnPatterns)
				&& ((this.MatchingTicketEType == null) || this.MatchingTicketEType.Contains(ticket.TicketEType))
				&& ((this.MatchingSessionEType == null) || this.MatchingSessionEType.Contains(ticket.SessionEType))
				&& MatchesRange(ticket.SeqNbr)
				;
			if (this.InvertMatch.IsSet)
				matches = !matches;
			return matches;
		}

		private bool MatchesRange(int seqNbr)
		{
			return this.SeqNbr == null || this.SeqNbr.Any(r => r.Contains(seqNbr));
		}

		protected sealed override Task<int> RunAsync(CancellationToken cancellationToken)
		{
			List<TicketInfo> allTickets = new List<TicketInfo>();

			KerberosClient krb = this.CreateKerberosClient(null);

			FileSpec[] sourceFileNames;
			if (this.From != null)
				sourceFileNames = this.From;
			else if (this.TicketCache != null)
				sourceFileNames = [this.TicketCache];
			else
				// This condition should be caught by parameter validation
				throw new InvalidOperationException("The command is missing a ticket source.");

			foreach (var item in sourceFileNames)
			{
				var pattern = this.ResolveFsPath(item);
				var dir = Path.GetDirectoryName(pattern);
				if (string.IsNullOrEmpty(dir))
					dir = ".";
				pattern = Path.GetFileName(pattern);
				var fileNames = this.FileAccessService.GetFiles(dir, pattern);
				if (fileNames.Length == 0)
				{
					this.WriteError($"No files found matching '{pattern}'.");
				}
				else
				{
					foreach (var fileName in fileNames)
					{
						var filePath = fileName;
						this.WriteVerbose($"Reading file {filePath}");

						var tickets = krb.LoadTicketsFromFile(this.FileAccessService.ReadAllBytesFrom(new FileSpec(filePath, true)), filePath, out _);

						var selected = tickets.Where(this.Matches).ToList();

						if (this.TicketKey != null || this.ServicePassword != null)
						{
							bool decrypted = false;
							foreach (var ticket in selected)
							{
								if (ticket.TicketKey != null)
									// Ticket already has a key
									continue;

								if (this.TicketKey != null)
								{
									foreach (var ticketKeyBytes in this.TicketKey)
									{
										try
										{
											var ticketKey = krb.CreateSessionKeyFor(ticket.TicketEType, ticketKeyBytes.Bytes);
											var authzData = ticket.DecryptAuthorizationData(ticketKey, null);
											ticket.TicketKey = ticketKey;
											decrypted = true;
											break;
										}
										catch (Exception ex)
										{
											// Decryption failed
										}
									}
								}

								if (this.ServicePassword != null)
								{
									string[] salts;
									if (this.ServiceSalt != null)
										salts = this.ServiceSalt;
									else
									{
										// Try to guess the salt
										string accountName;
										if (ticket.TargetSpn.NamePartCount == 1)
											accountName = ticket.TargetSpn.GetNamePart(0);
										else if (ticket.TargetSpn.NamePartCount >= 2)
										{
											accountName = ticket.TargetSpn.GetNamePart(1);
											var isep = accountName.IndexOf('.');
											if (isep > 0)
												accountName = accountName.Substring(0, isep);
										}
										else
											accountName = ticket.TargetSpn.ToString();

										string saltAsComputer = $"{ticket.ServiceRealm.ToUpper()}host{accountName.TrimEnd('$').ToLower()}.{ticket.ServiceRealm.ToLower()}";
										string saltAsUser = ticket.ServiceRealm.ToUpper() + accountName.ToLower();

										salts = [saltAsComputer, saltAsUser];
									}

									foreach (var servicePassword in this.ServicePassword)
									{
										var encProfile = krb.TryGetEncProfile(ticket.TicketEType);

										foreach (var salt in salts)
										{
											try
											{
												this.WriteDiagnostic($"Attempting to decrypt with password '{servicePassword}' and salt '{salt}'.");
												var ticketKey = encProfile.StringToKey(servicePassword, salt);
												var authzData = ticket.DecryptAuthorizationData(ticketKey, null);
												this.WriteVerbose($"Decrypted ticket #{ticket.SeqNbr} using password '{servicePassword}' and salt '{salt}'.");
												ticket.TicketKey = ticketKey;
												decrypted = true;
												break;
											}
											catch { }
										}

										if (decrypted)
											break;
									}
								}
							}

							if (!decrypted)
								this.WriteWarning($"The ticket key did not decrypt any tickets.");
						}

						if (this.PrintAuthData.IsSet)
						{
							foreach (var ticket in selected)
							{
								if (ticket.TicketKey != null)
									Program.TryPrintAuthorizationData(ticket, $"Ticket #{ticket.SeqNbr}:", this.Log);
							}
						}

						allTickets.AddRange(selected);

						this.WriteRecords(selected);
					}
				}
			}

			if (this.Into != null)
			{
				var outFileName = this.Into;
				this.WriteMessage($"Writing tickets to {outFileName}");
				var bytes = krb.ExportTickets(allTickets, KerberosClient.GetFormatFromFileName(outFileName.FileName));

				if (this.FileAccessService.FileExists(outFileName) && !this.Overwrite.IsSet)
				{
					this.WriteError("Output file exists but -Overwrite not specified.");
					return Task.FromResult(1);
				}

				this.FileAccessService.WriteAllBytesTo(outFileName, bytes);
			}


			return Task.FromResult(0);
		}
	}

	[TypeConverter(typeof(NumberOrRangeConverter))]
	public struct NumberOrRange
	{
		public NumberOrRange(int value)
		{
			MinValue = value;
			MaxValue = value;
		}
		public NumberOrRange(int? min, int? max)
		{
			MinValue = min;
			MaxValue = max;
		}

		public int? MinValue { get; }
		public int? MaxValue { get; }

		public bool Contains(int value) =>
			(!this.MinValue.HasValue || value >= this.MinValue.Value)
			&& (!this.MaxValue.HasValue || value <= this.MaxValue.Value);
	}

	class NumberOrRangeConverter : TypeConverter
	{
		public sealed override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			else
				return base.CanConvertFrom(context, sourceType);
		}

		private static readonly Regex rgxRange = new Regex(@"^(?<a>(\d+|\*))?-(?<b>(\d+)|\*)?$");
		public sealed override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				if (int.TryParse(str, out var n))
					return new NumberOrRange(n);
				else
				{
					var m = rgxRange.Match(str);
					if (m.Success)
					{
						var minText = m.Groups["a"].Value;
						var maxText = m.Groups["b"].Value;

						var range = new NumberOrRange(ParseBound(minText), ParseBound(maxText));
						return range;
					}
					else
					{
						throw new FormatException($"The range was not in the correct format of <number>-<number>");
					}
				}
			}

			return base.ConvertFrom(context, culture, value);
		}

		private static int? ParseBound(string text)
		{
			return (string.IsNullOrEmpty(text) || text == "*") ? default(int?) : int.Parse(text);
		}
	}
}