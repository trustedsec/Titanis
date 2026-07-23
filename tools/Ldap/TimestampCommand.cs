using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;

namespace Titanis.Cli.LdapTool;

[Command]
[Description("Converts between the Active Directory timestamp value and a UTC date/time")]
[Example("Convert a timestamp from AD", "134,182,427,222,091,265", "The value is converted to 2026-03-17T17:38:42.2091265Z")]
[Example("", "\"3/17/2026 5:38:42 PM\"", "The value is converted to 134,182,427,220,000,000.  Dates of this format lack the precision of an Active Directory timestamp so the last several digits are 0.")]
[Example("", "2026-03-17T17:38:42.2091265Z", "The value is converted to 134,182,427,222,091,265")]
[Example("Multiple values", "2026-03-17T17:38:42.2091265Z 134,182,427,220,000,000", "Prints two records corresponding to the two inputs")]
[DetailedHelpText(@"If the input is a number, it is converted to a date.  The number may include the thousands separator, since the Active Directory is likely printed this way.  The result is of the form 2026-03-17T17:38:42.2091265Z, which preserves the precision of the timestamp value.

If the input is a date, it is converted to a timestamp value, with the thousands separator.  The date may be in any parsable format, such as 2026-03-17T17:38:42.2091265Z or 3/17/2026 5:38:42 PM, depending on your local culture settings.  Note that some formats preserve more precision that others, and the resulting timestamp value reflects this.

You may specify multiple values.  Each output record prints both the numeric value and the date/time so you know which record matches which input.")]
public class TimestampCommand : Command
{
	[Parameter(0)]
	[Description("Timestamp or date to convert")]
	public string[] TimestampOrDate { get; set; }

	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		foreach (var timestamp in this.TimestampOrDate)
		{
			try
			{
				var ts = AdTimestamp.Parse(timestamp);
				this.WriteRecord(ts);
			}
			catch (Exception ex)
			{
				this.WriteError($"Error converting '{timestamp}': {ex.Message}");
			}
		}
		return Task.FromResult(0);
	}
}
