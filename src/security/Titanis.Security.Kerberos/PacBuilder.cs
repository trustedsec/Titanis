using ms_pac;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.IO;

namespace Titanis.Security.Kerberos
{
	public class PacBuilder
	{
		public PacBuilder(int bufferCount)
		{
			this.BufferCount = bufferCount;

			// Reserve header + PAC_INFO_BUFFER
			var offset = 8 + (16 * bufferCount);
			this._offset = offset;
			this._writer.SetPosition(offset);
		}

		public int BufferCount { get; }
		private ByteWriter _writer = new ByteWriter();

		private int _bufferIndex;
		private int _offset;

		private void AllocBuffer(PacBufferType bufferType)
		{
			var index = this._bufferIndex++;

			var startOffset = this._offset;
			// Compute length BEFORE alignment
			var length = this._writer.Position - this._offset;
			this._writer.Align(8);
			this._offset = this._writer.Position;

			ref var buf = ref MemoryMarshal.AsRef<PAC_INFO_BUFFER>(_writer.GetBuffer().Slice(8 + 16 * index, 16));
			buf = new PAC_INFO_BUFFER { ulType = bufferType, cbBufferSize = length, Offset = (uint)startOffset };

			BinaryPrimitives.WriteInt32LittleEndian(this._writer.GetBuffer().Slice(0, 4), this._bufferIndex);

		}

		public void WriteLogonInfo(LogonInfo logonInfo)
		{
			ArgumentNullException.ThrowIfNull(logonInfo);

			var encoder = MsrpcNdrEncoding.MsrpcNdr.CreateEncoder(this._writer, new RpcCallContext(null));
			encoder.SerializeType1(enc =>
			{
				enc.WriteValue(0x00020000);
				logonInfo.info.Encode(enc);
				logonInfo.info.EncodeDeferrals(enc);
			});

			this.AllocBuffer(PacBufferType.LogonInfo);
		}

		internal int WriteServerChecksum(PacBufferType bufferType, SessionKey? key)
		{
			var offset = this._offset;
			this._writer.WritePduStruct(new PAC_SIGNATURE_DATA
			{
				SignatureType = key.EncryptionProfile.ChecksumType,
			});
			this._writer.Advance(key.EncryptionProfile.ChecksumSizeBytes);

			this.AllocBuffer(bufferType);

			return offset + 4;
		}

		internal void WriteKdcChecksum(EncChecksumType checksumType, byte[] bytes)
		{
			this._writer.WritePduStruct(new PAC_SIGNATURE_DATA
			{
				SignatureType = checksumType,
				Signature = bytes
			});
			this.AllocBuffer(PacBufferType.KdcChecksum);
		}

		internal void WriteClientInfo(DateTime authTime, string effectiveName)
		{
			ArgumentNullException.ThrowIfNull(effectiveName);
			var fileTime = (ulong)authTime.ToFileTimeUtc();

			this._writer.WritePduStruct(new PAC_CLIENT_INFO
			{
				clientId_low = (int)fileTime,
				clientId_hi = (int)(fileTime >> 32),
				Name = effectiveName,
				NameLength = (ushort)Encoding.Unicode.GetByteCount(effectiveName)
			});

			this.AllocBuffer(PacBufferType.ClientNameInfo);
		}

		internal void WriteUpnDnsInfo(UpnDnsInfo upnDnsInfo)
		{
			this._writer.WritePduStruct(upnDnsInfo.dnsInfo);
			this.AllocBuffer(PacBufferType.UserPrincipalName);
		}

		public byte[] GetBytes()
		{
			return this._writer.GetData().ToArray();
		}

		internal void WriteClientClaims()
		{
			this.AllocBuffer(PacBufferType.ClientClaims);
		}

		internal void WriteTicketChecksum(EncChecksumType cktype, byte[] ticketChecksum)
		{
			var offset = this._offset;
			this._writer.WritePduStruct(new PAC_SIGNATURE_DATA
			{
				SignatureType = cktype,
				Signature = ticketChecksum
			});

			this.AllocBuffer(PacBufferType.TicketChecksum);
		}
	}
}
