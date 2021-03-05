using ms_dtyp;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.IO;
using Titanis.Winterop.Security;

namespace Titanis
{
	public static class SidExtensions
	{

		public static RPC_SID ToRpcSid(this SecurityIdentifier sid)
		{
			var bytes = sid.GetBytes();

			ByteMemoryReader reader = new ByteMemoryReader(bytes);
			byte subauthCount;
			return new RPC_SID
			{
				Revision = reader.ReadByte(),
				SubAuthorityCount = subauthCount = reader.ReadByte(),
				IdentifierAuthority = new RPC_SID_IDENTIFIER_AUTHORITY
				{
					Value = reader.Consume(6).ToArray()
				},
				SubAuthority = _ReadInt32Array(reader, subauthCount)
			};
		}

		public static SecurityIdentifier? ToSid(this RpcPointer<RPC_SID>? sid)
			=> (sid != null) ? sid.value.ToSid() : null;
		public static SecurityIdentifier ToSid(this RPC_SID sid)
		{
			if (sid.Revision != SecurityIdentifier.RevisionValue)
				throw new ArgumentException($"The provided SID does not have a valid revision value.", nameof(sid));

			var idauth = sid.IdentifierAuthority.Value;
			if (idauth == null || idauth.Length != 6)
				throw new ArgumentException($"The provided SID does not have a valid identifier authority.", nameof(sid));

			var subauths = sid.SubAuthority;
			if (subauths == null || subauths.Length != sid.SubAuthorityCount)
				throw new ArgumentException($"The provided SID does not have a valid list of subauthorities.", nameof(sid));

			byte[] bytes = new byte[2 + 6 + 4 * sid.SubAuthorityCount];
			bytes[0] = SecurityIdentifier.RevisionValue;
			bytes[1] = (byte)sid.SubAuthorityCount;
			sid.IdentifierAuthority.Value.CopyTo(bytes.AsSpan().Slice(2, 6));
			for (int i = 0; i < subauths.Length; i++)
			{
				uint subauth = subauths[i];
				BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan().Slice(8 + i * 4, 4), subauth);
			}

			return new SecurityIdentifier(bytes);
		}

		private static uint[] _ReadInt32Array(IByteSource reader, byte subauthCount)
		{
			uint[] arr = new uint[subauthCount];
			for (int i = 0; i < subauthCount; i++)
			{
				arr[i] = reader.ReadUInt32();
			}
			return arr;
		}
	}
}
