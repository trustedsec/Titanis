using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	struct NdTable
	{
		public NdTable(
			byte[] bytes,
			int propertyCount)
		{
			this.Bytes = bytes;
			this.PropertyCount = propertyCount;
		}
		public NdTable(int propertyCount)
		{
			this.Bytes = new byte[ComputeNdTableLength(propertyCount)];
			// TODO: Excess bits are set to 1; check that this is alright
			//Array.Fill<byte>(this.Bytes, 0xFF);
			this.PropertyCount = propertyCount;
		}

		public byte[] Bytes { get; }
		public int PropertyCount { get; }

		public int ByteLength => this.Bytes?.Length ?? 0;

		public static int ComputeNdTableLength(int propertyCount) => (propertyCount - 1) / 4 + 1;

		public NdFlags GetFlags(int propertyIndex)
		{
			if (CheckPropertyIndex(propertyIndex))
			{
				NdFlags ndFlags = NdFlags.None;
				if (this.Bytes != null)
				{
					var byteIndex = propertyIndex / 4;
					var b = this.Bytes[byteIndex];
					var bitIndex = propertyIndex * 2 % 8;
					bool defaultNull = 0 != (b & 1 << bitIndex);
					if (defaultNull)
						ndFlags |= NdFlags.IsNull;
					bool defaultInherited = 0 != (b & 1 << bitIndex + 1);
					if (defaultInherited)
						ndFlags |= NdFlags.DefaultInherited;
				}

				return ndFlags;
			}
			else
			{
				return NdFlags.None;
			}
		}

		private readonly bool CheckPropertyIndex(int propertyIndex)
		{
			return ((uint)propertyIndex < (uint)this.PropertyCount);
		}

		public void SetFlags(int propertyIndex, NdFlags flags)
		{
			if (this.CheckPropertyIndex(propertyIndex))
			{
				var byteIndex = propertyIndex / 4;
				int b = this.Bytes[byteIndex];
				var bitIndex = propertyIndex * 2 % 8;

				b = b & ~(3 << bitIndex);
				if (0 != (flags & NdFlags.IsNull))
					b |= 1 << bitIndex;
				if (0 != (flags & NdFlags.DefaultInherited))
					b |= 1 << bitIndex + 1;

				this.Bytes[byteIndex] = (byte)b;
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(propertyIndex));
			}
		}
	}
}
