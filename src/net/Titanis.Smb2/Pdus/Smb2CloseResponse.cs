using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.16 SMB2 CLOSE Response
	sealed class Smb2CloseResponse : Smb2PduStructBase<Smb2CloseResponseBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.Close;
	}

	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct Smb2CloseResponseBody: ISmb2PduStruct2
	{
		public unsafe static int StructSize => sizeof(Smb2CloseResponseBody);
		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		/// <inheritdoc/>
		public static Smb2Command Command => Smb2Command.Close;
		/// <inheritdoc/>
		public static ushort ValidSmbSize => 60;

		internal ushort structureSize;
		internal Smb2CloseOptions flags;
		internal uint reserved;

		internal Smb2OpenFileAttributes attrs;
	}
}
