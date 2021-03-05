using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Titanis;
using Titanis.Cli;
using Titanis.Security.Kerberos;

namespace Kerb
{
	/// <task category="Kerberos">Describe a Kerberos ticket</task>
	/// <task category="Kerberos">Print the contents of a .kirbi file</task>
	/// <task category="Kerberos">Print the contents of a .ccache file</task>
	/// <task category="Kerberos">Convert between a .ccache file and a .kirbi file</task>
	/// <task category="Kerberos">Query tickets within a .ccache file or .kirbi file</task>
	[Command]
	[OutputRecordType(typeof(TicketInfo), DefaultOutputStyle = OutputStyle.List)]
	[Description("Selects and displays tickets from a file.")]
	[DetailedHelpText(@"This command reads tickets from one or more files (.kirbi or .ccache), optionally filters them, and optionally writes the results to another file (either .kirbi or .ccache).  It can be used to inspect files, convert files, combine files, or remove tickets from files.

Specify the source files using -From.  You may specify multiple files and multiple wildcard patterns.  {0} reads all files from the tickets and applies any filters specified before printing the tickets to the screen.  If you specify -Into, the results are written to the file you specify.  Use -Overwrite to overwrite the outptu file if it already exists.")]
	[Example("Print tickets from all mlichick*.kirbi files", @"{0} -From milchick*.kirbi")]
	[Example("Combine tickets from all mlichick*.kirbi files", @"{0} -From milchick*.kirbi -Into all-milchick.kirbi")]
	[Example("Print only current tickets from all mlichick*.kirbi files", @"{0} -From milchick*.kirbi -Current")]
	[Example("Print only TGTs", @"{0} -From milchick*.kirbi -MatchingSpn krbtgt/.*")]
	[Example("Print only tickets for CIFS", @"{0} -From milchick*.kirbi -MatchingSpn cifs/.*")]
	[Example("Print only tickets targeting LUMON-FS1", @"{0} -From milchick*.kirbi -MatchingSpn .*/LUMON-FS1")]
	internal class SelectCommand : Command
	{
		[Parameter(0)]
		[Mandatory]
		[Description("File names or patterns")]
		public string[] From { get; set; }

		[Parameter]
		[Description("Target file name")]
		public string? Into { get; set; }

		[Parameter]
		[Description("Only select tickets currently valid")]
		public SwitchParam Current { get; set; }

		[Parameter]
		[Description("Regex of user name to match")]
		public string[]? MatchingUserName { get; set; }
		private Regex[]? _userNamePatterns;

		[Parameter]
		[Description("Regex of SPN to match")]
		public string[]? MatchingSpn { get; set; }
		private Regex[]? _spnPatterns;

		[Parameter]
		[Description("Filter for encryption type")]
		public EType[]? MatchingEncType { get; set; }

		[Parameter]
		[Description("Overwrites target file if it exists")]
		public SwitchParam Overwrite { get; set; }

		[Parameter]
		[Description("Key used to decrypt the ticket")]
		public HexString? TicketKey { get; set; }

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

			this._userNamePatterns = this.ToRegex(this.MatchingUserName);
			this._spnPatterns = this.ToRegex(this.MatchingSpn);
		}

		private bool MatchesPattern(string test, Regex[]? patterns)
		{
			return ((patterns.IsNullOrEmpty()) || (patterns.Any(r => r.Match(test).Success)));
		}

		private bool Matches(TicketInfo ticket)
		{
			bool matches =
				(!this.Current.IsSpecified || (ticket.IsCurrent == this.Current.IsSet))
				&& MatchesPattern(ticket.UserName, this._userNamePatterns)
				&& MatchesPattern(ticket.Spn.ToString(), this._spnPatterns)
				&& ((this.MatchingEncType == null) || this.MatchingEncType.Contains(ticket.EType))
				;
			return matches;
		}

		protected sealed override Task<int> RunAsync(CancellationToken cancellationToken)
		{
			List<TicketInfo> allTickets = new List<TicketInfo>();

			KerberosClient krb = new KerberosClient(null, callback: new KerberosDiagnosticLogger(this.Log));

			byte[]? keyBytes = this.TicketKey?.Bytes;

			foreach (var item in this.From)
			{
				var pattern = this.ResolveFsPath(item);
				var dir = Path.GetDirectoryName(pattern);
				pattern = Path.GetFileName(pattern);
				var fileNames = Directory.GetFiles(dir, pattern);
				if (fileNames.Length == 0)
				{
					this.WriteError($"No files found matching '{pattern}'.");
				}
				else
				{
					foreach (var fileName in fileNames)
					{
						byte[] ticketBytes = File.ReadAllBytes(fileName);

						var tickets = krb.LoadTicketsFromFile(ticketBytes, out _);

						var selected = tickets.Where(this.Matches).ToList();
						if (keyBytes != null)
						{
							foreach (var ticket in selected)
							{
								var authzData= krb.GetTicketAuthorizationData(ticket, keyBytes);
							}
						}
						allTickets.AddRange(selected);

						this.WriteRecords(selected);
					}
				}
			}

			if (this.Into != null)
			{
				var outFileName = this.ResolveFsPath(this.Into);
				var bytes = krb.ExportTickets(allTickets, AsreqCommand.FormatFromFileName(outFileName));

				if (File.Exists(outFileName) && !this.Overwrite.IsSet)
				{
					this.WriteError("Output file exists but -Overwrite not specified.");
					return Task.FromResult(1);
				}

				File.WriteAllBytes(outFileName, bytes);
			}


			return Task.FromResult(0);
		}
	}
}