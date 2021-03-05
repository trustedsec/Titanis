namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.1.1 - SMB2 Packet Header - ASYNC
	enum Smb2Command : ushort
	{
		Negotiate = 0,
		SessionSetup = 1,
		Logoff = 2,
		TreeConnect = 3,
		TreeDisconnect = 4,
		Create = 5,
		Close = 6,
		Flush = 7,
		Read = 8,
		Write = 9,
		Lock = 0xA,
		Ioctl = 0x0B,
		Cancel = 0x0C,
		Echo = 0x0D,
		QueryDirectory = 0x0E,
		ChangeNotify = 0x0F,
		QueryInfo = 0x10,
		SetInfo = 0x11,
		OplockBreak = 0x12,
	}
}
