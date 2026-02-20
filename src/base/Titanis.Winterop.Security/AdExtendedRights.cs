using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titanis.Winterop.Security
{
	public record struct AdExtendedRight(Guid Guid, string Description)
	{
	}

	// [MS-ADTS] § 5.1.3.2.1 Control Access Rights
	public static class AdExtendedRights
	{
		public static bool TryGetExtendedRight(Guid key, out AdExtendedRight right) => _rightsById.TryGetValue(key, out right);

		// TODO: Yeah, they can change it
		public static AdExtendedRight[] GetAllRights() => ExtendedRights;

		private static readonly AdExtendedRight[] ExtendedRights = new AdExtendedRight[]
		{
			new AdExtendedRight(new Guid("ee914b82-0a98-11d1-adbb-00c04fd8d5cd"), "Abandon-Replication"),
			new AdExtendedRight(new Guid("440820ad-65b4-11d1-a3da-0000f875ae0d"), "Add-GUID"),
			new AdExtendedRight(new Guid("1abd7cf8-0a99-11d1-adbb-00c04fd8d5cd"), "Allocate-Rids"),
			new AdExtendedRight(new Guid("68b1d179-0d15-4d4f-ab71-46152e79a7bc"), "Allowed-To-Authenticate"),
			new AdExtendedRight(new Guid("edacfd8f-ffb3-11d1-b41d-00a0c968f939"), "Apply-Group-Policy"),
			new AdExtendedRight(new Guid("0e10c968-78fb-11d2-90d4-00c04f79dc55"), "Certificate-Enrollment"),
			new AdExtendedRight(new Guid("a05b8cc2-17bc-4802-a710-e7c15ab866a2"), "Certificate-AutoEnrollment"),
			new AdExtendedRight(new Guid("014bf69c-7b3b-11d1-85f6-08002be74fab"), "Change-Domain-Master"),
			new AdExtendedRight(new Guid("cc17b1fb-33d9-11d2-97d4-00c04fd8d5cd"), "Change-Infrastructure-Master"),
			new AdExtendedRight(new Guid("bae50096-4752-11d1-9052-00c04fc2d4cf"), "Change-PDC"),
			new AdExtendedRight(new Guid("d58d5f36-0a98-11d1-adbb-00c04fd8d5cd"), "Change-Rid-Master"),
			new AdExtendedRight(new Guid("e12b56b6-0a95-11d1-adbb-00c04fd8d5cd"), "Change-Schema-Master"),
			new AdExtendedRight(new Guid("e2a36dc9-ae17-47c3-b58b-be34c55ba633"), "Create-Inbound-Forest-Trust"),
			new AdExtendedRight(new Guid("fec364e0-0a98-11d1-adbb-00c04fd8d5cd"), "Do-Garbage-Collection"),
			new AdExtendedRight(new Guid("ab721a52-1e2f-11d0-9819-00aa0040529b"), "Domain-Administer-Server"),
			new AdExtendedRight(new Guid("69ae6200-7f46-11d2-b9ad-00c04f79f805"), "DS-Check-Stale-Phantoms"),
			new AdExtendedRight(new Guid("2f16c4a5-b98e-432c-952a-cb388ba33f2e"), "DS-Execute-Intentions-Script"),
			new AdExtendedRight(new Guid("9923a32a-3607-11d2-b9be-0000f87a36b2"), "DS-Install-Replica"),
			new AdExtendedRight(new Guid("4ecc03fe-ffc0-4947-b630-eb672a8a9dbc"), "DS-Query-Self-Quota"),
			new AdExtendedRight(new Guid("1131f6aa-9c07-11d1-f79f-00c04fc2dcd2"), "DS-Replication-Get-Changes"),
			new AdExtendedRight(new Guid("1131f6ad-9c07-11d1-f79f-00c04fc2dcd2"), "DS-Replication-Get-Changes-All"),
			new AdExtendedRight(new Guid("89e95b76-444d-4c62-991a-0facbeda640c"), "DS-Replication-Get-Changes-In-Filtered-Set"),
			new AdExtendedRight(new Guid("1131f6ac-9c07-11d1-f79f-00c04fc2dcd2"), "DS-Replication-Manage-Topology"),
			new AdExtendedRight(new Guid("f98340fb-7c5b-4cdb-a00b-2ebdfa115a96"), "DS-Replication-Monitor-Topology"),
			new AdExtendedRight(new Guid("1131f6ab-9c07-11d1-f79f-00c04fc2dcd2"), "DS-Replication-Synchronize"),
			new AdExtendedRight(new Guid("05c74c5e-4deb-43b4-bd9f-86664c2a7fd5"), "Enable-Per-User-Reversibly-Encrypted-Password"),
			new AdExtendedRight(new Guid("b7b1b3de-ab09-4242-9e30-9980e5d322f7"), "Generate-RSoP-Logging"),
			new AdExtendedRight(new Guid("b7b1b3dd-ab09-4242-9e30-9980e5d322f7"), "Generate-RSoP-Planning"),
			new AdExtendedRight(new Guid("7c0e2a7c-a419-48e4-a995-10180aad54dd"), "Manage-Optional-Features"),
			new AdExtendedRight(new Guid("ba33815a-4f93-4c76-87f3-57574bff8109"), "Migrate-SID-History"),
			new AdExtendedRight(new Guid("b4e60130-df3f-11d1-9c86-006008764d0e"), "msmq-Open-Connector"),
			new AdExtendedRight(new Guid("06bd3201-df3e-11d1-9c86-006008764d0e"), "msmq-Peek"),
			new AdExtendedRight(new Guid("4b6e08c3-df3c-11d1-9c86-006008764d0e"), "msmq-Peek-computer-Journal"),
			new AdExtendedRight(new Guid("4b6e08c1-df3c-11d1-9c86-006008764d0e"), "msmq-Peek-Dead-Letter"),
			new AdExtendedRight(new Guid("06bd3200-df3e-11d1-9c86-006008764d0e"), "msmq-Receive"),
			new AdExtendedRight(new Guid("4b6e08c2-df3c-11d1-9c86-006008764d0e"), "msmq-Receive-computer-Journal"),
			new AdExtendedRight(new Guid("4b6e08c0-df3c-11d1-9c86-006008764d0e"), "msmq-Receive-Dead-Letter"),
			new AdExtendedRight(new Guid("06bd3203-df3e-11d1-9c86-006008764d0e"), "msmq-Receive-journal"),
			new AdExtendedRight(new Guid("06bd3202-df3e-11d1-9c86-006008764d0e"), "msmq-Send"),
			new AdExtendedRight(new Guid("a1990816-4298-11d1-ade2-00c04fd8d5cd"), "Open-Address-Book"),
			new AdExtendedRight(new Guid("1131f6ae-9c07-11d1-f79f-00c04fc2dcd2"), "Read-Only-Replication-Secret-Synchronization"),
			new AdExtendedRight(new Guid("45ec5156-db7e-47bb-b53f-dbeb2d03c40f"), "Reanimate-Tombstones"),
			new AdExtendedRight(new Guid("0bc1554e-0a99-11d1-adbb-00c04fd8d5cd"), "Recalculate-Hierarchy"),
			new AdExtendedRight(new Guid("62dd28a8-7f46-11d2-b9ad-00c04f79f805"), "Recalculate-Security-Inheritance"),
			new AdExtendedRight(new Guid("ab721a56-1e2f-11d0-9819-00aa0040529b"), "Receive-As"),
			new AdExtendedRight(new Guid("9432c620-033c-4db7-8b58-14ef6d0bf477"), "Refresh-Group-Cache"),
			new AdExtendedRight(new Guid("1a60ea8d-58a6-4b20-bcdc-fb71eb8a9ff8"), "Reload-SSL-Certificate"),
			new AdExtendedRight(new Guid("7726b9d5-a4b4-4288-a6b2-dce952e80a7f"), "Run-Protect_Admin_Groups-Task"),
			new AdExtendedRight(new Guid("91d67418-0135-4acc-8d79-c08e857cfbec"), "SAM-Enumerate-Entire-Domain"),
			new AdExtendedRight(new Guid("ab721a54-1e2f-11d0-9819-00aa0040529b"), "Send-As"),
			new AdExtendedRight(new Guid("ab721a55-1e2f-11d0-9819-00aa0040529b"), "Send-To"),
			new AdExtendedRight(new Guid("ccc2dc7d-a6ad-4a7a-8846-c04e3cc53501"), "Unexpire-Password"),
			new AdExtendedRight(new Guid("280f369c-67c7-438e-ae98-1d46f3c6f541"), "Update-Password-Not-Required-Bit"),
			new AdExtendedRight(new Guid("be2bb760-7f46-11d2-b9ad-00c04f79f805"), "Update-Schema-Cache"),
			new AdExtendedRight(new Guid("ab721a53-1e2f-11d0-9819-00aa0040529b"), "User-Change-Password"),
			new AdExtendedRight(new Guid("00299570-246d-11d0-a768-00aa006e0529"), "User-Force-Change-Password"),
			new AdExtendedRight(new Guid("3e0f7e18-2c7a-4c10-ba82-4d926db99a3e"), "DS-Clone-Domain-Controller"),
			new AdExtendedRight(new Guid("084c93a2-620d-4879-a836-f0ae47de0e89"), "DS-Read-Partition-Secrets"),
			new AdExtendedRight(new Guid("94825a8d-b171-4116-8146-1e34d8f54401"), "DS-Write-Partition-Secrets"),
			new AdExtendedRight(new Guid("4125c71f-7fac-4ff0-bcb7-f09a41325286"), "DS-Set-Owner"),
			new AdExtendedRight(new Guid("88a9933e-e5c8-4f2a-9dd7-2527416b8092"), "DS-Bypass-Quota"),
			new AdExtendedRight(new Guid("9b026da6-0d3c-465c-8bee-5199d7165cba"), "DS-Validated-Write-Computer"),
		};

		private static readonly Dictionary<Guid, AdExtendedRight> _rightsById = ExtendedRights.ToDictionary(r => r.Guid);
	}
}
