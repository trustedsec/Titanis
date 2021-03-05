using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2.Pdus
{
	[Flags]
	enum Smb2TransformFlags
	{
		Encrypted = 1,
	}
	// [MS-SMB2] § 2.2.41
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2TransformHeader
	{
		internal static unsafe int StructSize => sizeof(Smb2TransformHeader);
		public const int Smb2TransformSignature = 0x424D53FD;

		internal uint protocolId;
		internal Guid signature;
		internal Guid nonce;
		internal int originalMessageSize;
		internal ushort reserved;
		internal ushort flags_encAlgo;
		internal ulong sessionId;

		internal Span<byte> SignatureBytes
			=> MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this.signature, 1));
		internal Span<byte> NonceBytes
			=> MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref this.nonce, 1));
		internal Cipher Cipher => (Cipher)this.flags_encAlgo;
		internal Smb2TransformFlags Flags => (Smb2TransformFlags)this.flags_encAlgo;

	}
}
