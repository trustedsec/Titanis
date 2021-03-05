using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.35 SMB2 CHANGE_NOTIFY Request
	sealed class Smb2ChangeNotifyRequest : Smb2Pdu<Smb2ChangeNotifyRequestHeader>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.ChangeNotify;
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.ChangeNotify;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 32;

		/// <inheritdoc/>
		internal sealed override int ResponsePayloadSize => this.body.outputBufferLength;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadChangeNotifyReqHdr();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2ChangeNotifyRequestHeader hdr)
		{
			writer.WriteChangeNotifyReqHdr(this.body);
		}
	}

	[Flags]
	public enum Smb2ChangeNotifyOptions : ushort
	{
		None = 0,

		WatchTree = 1,
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2ChangeNotifyRequestHeader : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2ChangeNotifyRequestHeader);
		public ushort StructureSize { get => this.headerSize; set => this.headerSize = value; }

		internal ushort headerSize;
		internal Smb2ChangeNotifyOptions flags;
		internal int outputBufferLength;
		internal Smb2FileHandle handle;
		internal Smb2ChangeFilter filter;
		internal uint reserved;
	}
}
