using ms_dcom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Msrpc.Msdcom
{
	internal static class DcomReader
	{
		// TODO: Migrate to [PduStruct]
		internal static STDOBJREF ReadStdObjref(this IByteSource reader)
		{
			return new STDOBJREF
			{
				flags = reader.ReadUInt32LE(),
				cPublicRefs = (uint)reader.ReadInt32LE(),
				oxid = reader.ReadUInt64LE(),
				oid = reader.ReadUInt64LE(),
				ipid = reader.ReadGuid(),
			};
		}
		// TODO: Migrate to [PduStruct]
		internal static DualStringArray ReadDualStringArray(this ByteMemoryReader reader)
		{
			var elemCount = reader.ReadUInt16LE();
			DUALSTRINGARRAY idl = new DUALSTRINGARRAY
			{
				wNumEntries = elemCount,
				wSecurityOffset = reader.ReadUInt16LE(),
				aStringArray = reader.ReadCountedUshortArray(elemCount)
			};
			return DualStringArray.FromIdl(idl);
		}

		internal static ushort[] ReadCountedUshortArray(this ByteMemoryReader reader, int count)
		{
			return MemoryMarshal.Cast<byte, ushort>(reader.Consume(count * 2)).ToArray();
		}
	}
}
