using System;

namespace Titanis.Msrpc.Mslsar
{
	[Flags]
	public enum SystemAccessRights
	{
		None = 0,

		SeInteractiveLogonRight = 1,
		SeNetworkLogonRight = 2,
		SeBatchLogonRight = 4,
		SeServiceLogonRight = 0x10,
		SeDenyInteractiveLogonRight = 0x40,
		SeDenyNetworkLogonRight = 0x80,
		SeDenyBatchLogonRight = 0x100,
		SeDenyServiceLogonRight = 0x200,
		SeRemoteInteractiveLogonRight = 0x400,
		SeDenyRemoteInteractiveLogonRight = 0x800
	}
}
