using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.7 SMB2 LOGOFF Request
	sealed class Smb2LogoffRequest : Smb2PduStructBase<Smb2LogoffRequestBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.Logoff;
	}

	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct Smb2LogoffRequestBody : ISmb2PduStruct2
	{
		public unsafe static int StructSize => sizeof(Smb2LogoffRequestBody);
		/// <inheritdoc/>
		public static Smb2Command Command => Smb2Command.Logoff;
		/// <inheritdoc/>
		public static ushort ValidSmbSize => 4;

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;
		internal ushort reserved;
	}
}
