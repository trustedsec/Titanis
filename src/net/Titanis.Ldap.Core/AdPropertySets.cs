using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titanis.Winterop.Security
{
	public record struct AdPropertySet(Guid Guid, string Description)
	{
	}

	// [MS-ADTS] § 3.1.1.2.3.3 Property Set
	public static class AdPropertySets
	{
		public static bool TryGetPropertySet(Guid key, out AdPropertySet propertySet) => _propertySetsById.TryGetValue(key, out propertySet);

		// TODO: Yeah, they can change it
		public static AdPropertySet[] GetAllPropertySets() => _propertySets;

		private static readonly AdPropertySet[] _propertySets = new AdPropertySet[]
		{
			new AdPropertySet(new Guid("C7407360-20BF-11D0-A768-00AA006E0529"), "Domain Password & Lockout Policies"),
			new AdPropertySet(new Guid("59BA2F42-79A2-11D0-9020-00C04FC2D3CF"), "General Information"),
			new AdPropertySet(new Guid("4C164200-20C0-11D0-A768-00AA006E0529"), "Account Restrictions"),
			new AdPropertySet(new Guid("5F202010-79A5-11D0-9020-00C04FC2D4CF"), "Logon Information"),
			new AdPropertySet(new Guid("BC0AC240-79A9-11D0-9020-00C04FC2D4CF"), "Group Membership"),
			new AdPropertySet(new Guid("E45795B2-9455-11D1-AEBD-0000F80367C1"), "Phone and Mail Options"),
			new AdPropertySet(new Guid("77B5B886-944A-11D1-AEBD-0000F80367C1"), "Personal Information"),
			new AdPropertySet(new Guid("E45795B3-9455-11D1-AEBD-0000F80367C1"), "Web Information"),
			new AdPropertySet(new Guid("E48D0154-BCF8-11D1-8702-00C04FB96050"), "Public Information"),
			new AdPropertySet(new Guid("037088F8-0AE1-11D2-B422-00A0C968F939"), "Remote Access Information"),
			new AdPropertySet(new Guid("B8119FD0-04F6-4762-AB7A-4986C76B3F9A"), "Other Domain Parameters (for use by SAM)"),
			new AdPropertySet(new Guid("72E39547-7B18-11D1-ADEF-00C04FD8D5CD"), "DNS Host Name Attributes"),
			new AdPropertySet(new Guid("FFA6F046-CA4B-4FEB-B40D-04DFEE722543"), "MS-TS-GatewayAccess (*)"),
			new AdPropertySet(new Guid("91E647DE-D96F-4B70-9557-D63FF4F3CCD8"), "Private Information (*)"),
			new AdPropertySet(new Guid("5805BC62-BDC9-4428-A5E2-856A0F4C185E"), "Terminal Server License Server (*)"),
		};

		private static readonly Dictionary<Guid, AdPropertySet> _propertySetsById = _propertySets.ToDictionary(r => r.Guid);
	}
}
