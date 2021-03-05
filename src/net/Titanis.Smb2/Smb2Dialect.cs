using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Smb2
{
	// [MS-SMB2] § 1.7 Versioning and Capability Negotiation
	public enum Smb2Dialect : short
	{
		Smb2_0_2 = 0x0202,
		Smb2_1 = 0x0210,
		Smb3_0 = 0x0300,
		Smb3_0_2 = 0x0302,
		Smb3_1_1 = 0x0311,
	}
}
