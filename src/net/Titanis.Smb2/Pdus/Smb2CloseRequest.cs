using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.15 SMB2 CLOSE Request
	sealed class Smb2CloseRequest : Smb2PduStructBase<Smb2CloseRequestBody>
	{

		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.Close;
	}

	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct Smb2CloseRequestBody: ISmb2PduStruct2
	{
		public unsafe static int StructSize => sizeof(Smb2CloseRequestBody);
		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		/// <inheritdoc/>
		public static Smb2Command Command => Smb2Command.Close;
		/// <inheritdoc/>
		public static ushort ValidSmbSize => 24;

		internal ushort structureSize;
		internal Smb2CloseOptions flags;
		internal uint reserved;
		internal Smb2FileHandle handle;
	}
}
