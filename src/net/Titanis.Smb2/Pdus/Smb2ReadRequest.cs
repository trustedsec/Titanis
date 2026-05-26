using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.19 SMB2 READ Request
	sealed class Smb2ReadRequest : Smb2PduStructBase<Smb2ReadRequestBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.Read;
		/// <inheritdoc/>
		internal sealed override int ResponsePayloadSize => this.body.length;

		internal Memory<byte> receiveBuffer;
	}

	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct Smb2ReadRequestBody : ISmb2PduStruct2
	{
		/// <inheritdoc/>
		public static Smb2Command Command => Smb2Command.Read;
		/// <inheritdoc/>
		public static ushort ValidSmbSize => 49;

		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }
		internal ushort structureSize;

		internal byte padding;
		internal Smb2ReadOptions options;
		internal int length;
		internal long offset;
		internal Smb2FileHandle handle;
		internal int minCount;
		internal uint channel;
		internal int remainingBytes;
		internal ushort readChannelInfoOffset;
		internal ushort readChannelInfoLength;
		// HACK: Sent by Windows, without it STATUS_INVALID_PARAMETER
		private byte dummy;
	}
}
