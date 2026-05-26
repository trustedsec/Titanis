using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.30 SMB2 CANCEL Request
	sealed class Smb2CancelRequest : Smb2PduStructBase<Smb2CancelRequestBody>
	{

		/// <inheritdoc/>
		// TODO: What is the actual priority?
		internal sealed override Smb2Priority Priority => Smb2Priority.Unknown;

		public ulong AsyncId
		{
			get => this.pduhdrbuf.async.asyncId;
			internal set => this.pduhdrbuf.async.asyncId = value;
		}
		public ulong OriginalMessageId
		{
			get => this.pduhdr.messageId;
			internal set => this.pduhdr.messageId = value;
		}
	}

	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct Smb2CancelRequestBody : ISmb2PduStruct2
	{
		public unsafe static int StructSize => sizeof(Smb2CancelRequestBody);
		/// <inheritdoc/>
		public static Smb2Command Command => Smb2Command.Cancel;
		/// <inheritdoc/>
		public static ushort ValidSmbSize => 4;

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }

		internal ushort structureSize;
		internal ushort reserved;
	}
}
