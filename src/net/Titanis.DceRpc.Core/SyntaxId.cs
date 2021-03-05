using System;
using System.Runtime.InteropServices;

namespace Titanis.DceRpc
{
	[PduStruct]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public partial struct SyntaxId : IEquatable<SyntaxId>
	{
		public unsafe static int StructSize => sizeof(SyntaxId);

		public Guid if_uuid;
		public RpcVersion if_version;

		/// <inheritdoc/>
		public override string ToString()
			=>$"{this.if_uuid} v{this.if_version}";

		public SyntaxId(Guid uuid, RpcVersion version)
		{
			this.if_uuid = uuid;
			this.if_version = version;
		}

		public override bool Equals(object? obj)
		{
			return obj is SyntaxId id && this.Equals(id);
		}

		public bool Equals(SyntaxId other)
		{
			return this.if_uuid.Equals(other.if_uuid) &&
				   this.if_version.Equals(other.if_version);
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(this.if_uuid, this.if_version);
		}

		public static bool operator ==(SyntaxId left, SyntaxId right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(SyntaxId left, SyntaxId right)
		{
			return !(left == right);
		}
	}
}
