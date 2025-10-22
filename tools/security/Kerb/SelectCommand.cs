using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
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
	[Example("Print only tickets #1, 3-5, 7+", @"{0} -From milchick*.kirbi -SeqNbr 1, 3-5, 7-*")]
	internal class SelectCommand : Command
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
		[Parameter(0)]
		[Mandatory]
		[Description("File names or patterns")]
		public string[] From { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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

		[Parameter]
		[Description("Seq. nbr. or range")]
		public NumberOrRange[]? SeqNbr { get; set; }

		[Parameter]
		[Description("Invert match; select whatever doesn't match")]
		public SwitchParam InvertMatch { get; set; }

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
				&& MatchesPattern(ticket.TargetSpn.ToString(), this._spnPatterns)
				&& ((this.MatchingEncType == null) || this.MatchingEncType.Contains(ticket.EType))
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

			byte[]? keyBytes = this.TicketKey?.Bytes;

			foreach (var item in this.From)
			{
				var pattern = this.ResolveFsPath(item);
				var dir = Path.GetDirectoryName(pattern);
				if (string.IsNullOrEmpty(dir))
					dir = ".";
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
								var authzData = krb.GetTicketAuthorizationData(ticket, keyBytes);
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
				var bytes = krb.ExportTickets(allTickets, KerberosClient.GetFormatFromFileName(outFileName));

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

	[TypeConverter(typeof(NumberOrRangeConverter))]
	struct NumberOrRange
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

		private static int? ParseBound(string minText)
		{
			return (minText == "*") ? default(int?) : int.Parse(minText);
		}
	}
}