using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;
using Titanis.Winterop.Security;

namespace Titanis.Cli.SddlTool;


/// <task category="Security;SDDL">Look up a property by GUID (offline)</task>
/// <task category="Security;SDDL">Look up a property set by GUID (offline)</task>
/// <task category="Security;SDDL">Look up an extended right by GUID (offline)</task>
[Command]
[Description("Looks up a GUID representing an AD extended right, property, or property set")]
[OutputRecordType(typeof(GuidLookupResult))]
[Example("Look up Logon Information and Account Restrictions property sets", "{0} 5f202010-79a5-11d0-9020-00c04fc2d4cf, 4c164200-20c0-11d0-a768-00aa006e0529")]
internal class LookupGuidCommand : Command
{
	[Parameter(0)]
	[Mandatory]
	[Description("GUID of interest")]
	public Guid[] Guid { get; set; }

	protected override Task<int> RunAsync(CancellationToken cancellationToken)
	{
		foreach (var guid in this.Guid)
		{
			if (AdExtendedRights.TryGetExtendedRight(guid, out var right))
				this.WriteRecord(new GuidLookupResult(guid, GuidKind.Right, right.Description));
			else if (AdPropertySets.TryGetPropertySet(guid, out var propset))
				this.WriteRecord(new GuidLookupResult(guid, GuidKind.PropertySet, propset.Description));
			else if (AdProperties.TryGetProperty(guid, out var prop))
				this.WriteRecord(new GuidLookupResult(guid, GuidKind.Property, prop.Name));
			else
				this.WriteRecord(new GuidLookupResult(guid, GuidKind.Unknown, null));
		}

		return Task.FromResult(0);
	}

	public enum GuidKind
	{
		Unknown = 0,
		Right,
		PropertySet,
		Property
	}
	record class GuidLookupResult(Guid Guid, GuidKind Kind, string? Name)
	{
	}
}
