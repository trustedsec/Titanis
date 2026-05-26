using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.35 SMB2 CHANGE_NOTIFY Request
	sealed class Smb2ChangeNotifyRequest : Smb2PduStructBase<Smb2ChangeNotifyRequestHeader>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Priority Priority => Smb2Priority.ChangeNotify;

		/// <inheritdoc/>
		internal sealed override int ResponsePayloadSize => this.body.outputBufferLength;
	}

	[Flags]
	public enum Smb2ChangeNotifyOptions : ushort
	{
		None = 0,

		WatchTree = 1,
	}

	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	partial struct Smb2ChangeNotifyRequestHeader : ISmb2PduStruct2
	{
		public unsafe static int StructSize => sizeof(Smb2ChangeNotifyRequestHeader);
		public ushort StructureSize { get => this.headerSize; set => this.headerSize = value; }

		/// <inheritdoc/>
		public static Smb2Command Command => Smb2Command.ChangeNotify;
		/// <inheritdoc/>
		public static ushort ValidSmbSize => 32;

		internal ushort headerSize;
		internal Smb2ChangeNotifyOptions flags;
		internal int outputBufferLength;
		internal Smb2FileHandle handle;
		internal Smb2ChangeFilter filter;
		internal uint reserved;
	}
}
