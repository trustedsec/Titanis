using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.17 SMB2 FLUSH Request
	sealed class Smb2FlushRequest : Smb2PduStructBase<Smb2FlushRequestBody>
	{
	}

	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct Smb2FlushRequestBody : ISmb2PduStruct2
	{
		public unsafe static int StructSize => sizeof(Smb2FlushRequestBody);

		public static Smb2Command Command => Smb2Command.Flush;
		public static ushort ValidSmbSize => 24;

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		private ushort reserved;
		private uint reserved2;
		internal Smb2FileHandle handle;
	}
}
