using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;
using Titanis.Winterop.Security;

namespace Titanis.Cli.SddlTool;


/// <task category="Security;SDDL">Look up a well-known SID (offline)</task>
[Command]
[Description("Looks up a well-known SID")]
[OutputRecordType(typeof(LookupWksResult))]
[Example("Looks up a SID and WKS", "{0} DA, S-1-18-1")]
[Example("Looks up a domain placeholder SID", "{0} S-1-5-21-<domain>-512")]
internal class LookupWksCommand : Command
{
	[Parameter(0)]
	[Mandatory]
	[Description("SID or WKS of interest")]
	public SecurityIdentifier[] SidOrWks { get; set; }

	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		foreach (var sid in this.SidOrWks)
		{
			this.WriteRecord(new LookupWksResult(sid.AsWellKnownSid(), sid));
		}

		return Task.FromResult(0);
	}

	record class LookupWksResult(WellKnownSid Wks, SecurityIdentifier Sid)
	{
	}
}
