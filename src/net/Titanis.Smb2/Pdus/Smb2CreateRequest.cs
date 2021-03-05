using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.13 - SMB2 CREATE Request
	sealed class Smb2CreateRequest : Smb2Pdu
	{
		internal Smb2CreateRequest(string path, Smb2CreateInfo createInfo)
		{
			this.path = path;
			this.createInfo = createInfo;
		}
		internal Smb2CreateRequest()
		{
		}

		internal sealed override Smb2Command Command => Smb2Command.Create;
		internal sealed override Smb2Priority Priority => this.createInfo.Priority;

		internal ref Smb2CreateRequestBody body => ref this.createInfo.body;
		internal string path;
		internal Smb2CreateInfo createInfo;

		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader hdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			// TODO: Validate header values

			ref readonly Smb2CreateRequestBody body = ref reader.ReadCreateReqHdr();
			this.createInfo = new Smb2CreateInfo(body);
			if (body.pathLength > 0)
			{
				reader.Position = offPdu + body.pathOffset;
				this.path = Encoding.Unicode.GetString(reader.ReadBytes(body.pathLength));
			}
		}

		struct ContextInfo
		{
			internal ByteWriter writer;
			internal int lastContextOffset;
			internal int contextCount;
			internal int bias;
		}

		private static void WriteContextHeader(ref ContextInfo info, int nameLength, int dataLength)
		{
			var writer = info.writer;
			writer.Align(8, info.bias);
			int offset = writer.Position;
			if (info.lastContextOffset != 0)
			{
				writer.SetPosition(info.lastContextOffset);
				writer.WriteInt32LE(offset - info.lastContextOffset);
				writer.SetPosition(offset);
			}

			info.lastContextOffset = offset;

			var pHdr = MemoryMarshal.Cast<byte, Smb2CreateContextHeader>(writer.Consume(Smb2CreateContextHeader.StructSize));
			ref Smb2CreateContextHeader hdr = ref pHdr[0];
			hdr.next = 0;
			hdr.nameOffset = (ushort)Smb2CreateContextHeader.StructSize;
			hdr.nameLength = (ushort)nameLength;
			hdr.reserved = 0;
			hdr.dataOffset = (ushort)BinaryHelper.Align(nameLength + Smb2CreateContextHeader.StructSize, 8);
			hdr.dataLength = (uint)dataLength;

			info.contextCount++;
		}

		private static void WriteContextHeader(ref ContextInfo info, Smb2CreateContextId contextId, int dataLength)
		{
			WriteContextHeader(ref info, 4, dataLength);

			var writer = info.writer;
			writer.WriteInt32LE((int)contextId);
			writer.Align(8, info.bias);
		}

		private static void WriteContextHeader(ref ContextInfo info, Smb2CreateContextId contextId, ReadOnlySpan<byte> data)
		{
			WriteContextHeader(ref info, contextId, data.Length);
			var writer = info.writer;
			writer.WriteBytes(data);
		}

		private static void WriteContextHeader(ref ContextInfo info, Smb2CreateContextId contextId, int dummy, long value)
		{
			WriteContextHeader(ref info, contextId, 8);
			var writer = info.writer;
			writer.WriteInt64LE(value);
		}

		internal override void WriteTo(ByteWriter writer)
		{
			this.body.structSize = 57;

			int offPdu = writer.Position - Smb2PduSyncHeader.StructSize;

			if (this.path != null)
			{
				body.pathOffset = (ushort)(Smb2PduSyncHeader.StructSize + Smb2CreateRequestBody.StructSize);
				body.pathLength = (ushort)Encoding.Unicode.GetByteCount(this.path);
			}
			else
			{
				body.pathOffset = 0;
				body.pathLength = 0;
			}

			int offHdr = writer.Position;
			writer.WriteCreateReqHdr(this.body);
			if (this.path != null)
				writer.WriteStringUni(this.path);

			ContextInfo ctxInfo = new ContextInfo()
			{
				writer = writer,
				bias = offPdu
			};

			int offContexts = BinaryHelper.Align(writer.Position, 8, offHdr);

			// TODO: Figure out the order Windows uses
			if (this.createInfo.ExtendedAttributes != null)
				WriteContextHeader(ref ctxInfo, Smb2CreateContextId.ExtendedAttributes, this.createInfo.ExtendedAttributes);
			if (this.createInfo.SecurityDescriptor != null)
				WriteContextHeader(ref ctxInfo, Smb2CreateContextId.SecurityDescriptor, this.createInfo.SecurityDescriptor);
			if (this.createInfo.RequestDurableHandle)
				WriteContextHeader(ref ctxInfo, Smb2CreateContextId.DurableHandleRequest, Smb2FileHandle.StructSize);
			if (this.createInfo.ReconnectDurableHandle.HasValue)
				WriteContextHeader(ref ctxInfo, Smb2CreateContextId.DurableHandleRequest, this.createInfo.ReconnectDurableHandle.Value.AsSpan());
			if (this.createInfo.RequestMaximalAccess)
				WriteContextHeader(ref ctxInfo, Smb2CreateContextId.QueryMaximalAccessRequest, 0);
			if (this.createInfo.AllocationSize.HasValue)
				WriteContextHeader(ref ctxInfo, Smb2CreateContextId.AllocationSize, 0, this.createInfo.AllocationSize.Value);
			if (this.createInfo.TimeWarpToken.HasValue)
				WriteContextHeader(ref ctxInfo, Smb2CreateContextId.TimewarpToken, 0, this.createInfo.TimeWarpToken.Value.ToFileTimeUtc());
			if (this.createInfo.QueryOnDiskId)
				WriteContextHeader(ref ctxInfo, Smb2CreateContextId.QueryDiskId, 0);
			if (this.createInfo.LeaseInfo != null)
			{
				var leaseInfo = this.createInfo.LeaseInfo;
				if (leaseInfo.UseV2Struct)
				{
					WriteContextHeader(ref ctxInfo, Smb2CreateContextId.RequestLease, Smb2CreateRequestLeaseV2.StructSize);

					var leaseBytes = writer.Consume(Smb2CreateRequestLeaseV2.StructSize);
					ref var leaseContext = ref MemoryMarshal.AsRef<Smb2CreateRequestLeaseV2>(leaseBytes);
					leaseContext = new Smb2CreateRequestLeaseV2
					{
						leaseKey = leaseInfo.LeaseKey,
						leaseState = leaseInfo.LeaseState,
						leaseFlags = (leaseInfo.ParentLeaseKey != Guid.Empty) ? Smb2LeaseFlags.ParentLeaseKeySet : Smb2LeaseFlags.None,
						parentLeaseKey = leaseInfo.ParentLeaseKey,
					};
				}
				else
				{
					WriteContextHeader(ref ctxInfo, Smb2CreateContextId.RequestLease, Smb2CreateRequestLease.StructSize);

					var leaseBytes = writer.Consume(Smb2CreateRequestLease.StructSize);
					ref var leaseContext = ref MemoryMarshal.AsRef<Smb2CreateRequestLease>(leaseBytes);
					leaseContext = new Smb2CreateRequestLease
					{
						leaseKey = leaseInfo.LeaseKey,
						leaseState = leaseInfo.LeaseState
					};
				}
			}

			// TODO: Lease
			//TODO: Lease V2
			//TODO: Durable Handle Request V2
			//TODO: Durable Handle Reconnect V2

			if (writer.Position > offContexts)
			{
				ref Smb2CreateRequestBody hdrBuf = ref MemoryMarshal.AsRef<Smb2CreateRequestBody>(writer.GetBuffer().AsSpan().Slice(offHdr));
				// TODO: If there are no headers, is the offset 0 or the next available slot?
				hdrBuf.contextsOffset = (ushort)(offContexts - offPdu);
				hdrBuf.contextsLength = (ushort)(writer.Position - offContexts);
			}
		}
	}

	// [MS-SMB2] § 2.2.13 - SMB2 CREATE Request
	[Flags]
	enum Smb2CreateFlags : ulong
	{
		None = 0,
	}

	// [MS-SMB2] § 2.2.13 - SMB2 CREATE Request
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2CreateRequestBody
	{
		public unsafe static int StructSize => sizeof(Smb2CreateRequestBody);

		internal ushort structSize;
		internal byte securityFlags;
		internal Smb2OplockLevel oplockLevel;
		internal Smb2ImpersonationLevel impLevel;
		internal Smb2CreateFlags createFlags;
		internal ulong reserved;
		internal uint desiredAccess;
		internal Winterop.FileAttributes fileAttributes;
		internal Smb2ShareAccess shareAccess;
		internal Smb2CreateDisposition createDisp;
		internal Smb2FileCreateOptions createOptions;

		internal ushort pathOffset;
		internal ushort pathLength;

		internal uint contextsOffset;
		internal uint contextsLength;
	}

	// [MS-SMB2] § 2.2.13.2 - SMB2_CREATE_CONTEXT Request Values
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2CreateContextHeader
	{
		internal static unsafe int StructSize => sizeof(Smb2CreateContextHeader);

		internal int next;
		internal ushort nameOffset;
		internal ushort nameLength;
		internal ushort reserved;
		internal ushort dataOffset;
		internal uint dataLength;
	}

	// [MS-SMB2] § 2.2.13.2 - SMB2_CREATE_CONTEXT Request Values
	enum Smb2CreateContextId : uint
	{
		ExtendedAttributes = 0x41747845,
		SecurityDescriptor = 0x44636553,
		DurableHandleRequest = 0x516e4844,
		DurableHandleReconnect = 0x436e4844,
		AllocationSize = 0x69536c41,
		QueryMaximalAccessRequest = 0x6341784d,
		TimewarpToken = 0x70725754,
		QueryDiskId = 0x64694651,
		RequestLease = 0x734c7152,
		DurableHandleRequestV2 = 0x734c7152,
		DurableHandleReconnectV2 = 0x51324844,
	}

	// [MS-SMB2] § 2.2.13.2.8
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2CreateRequestLease
	{
		public static unsafe int StructSize => sizeof(Smb2CreateRequestLease);

		internal Guid leaseKey;
		internal Smb2LeaseState leaseState;
		internal int leaseFlags;
		internal long leaseDuration;
	}

	enum Smb2LeaseFlags : int
	{
		None = 0,
		ParentLeaseKeySet = 4,
	}

	// [MS-SMB2] § 2.2.13.2.8
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2CreateRequestLeaseV2
	{
		public static unsafe int StructSize => sizeof(Smb2CreateRequestLeaseV2);

		internal Guid leaseKey;
		internal Smb2LeaseState leaseState;
		internal Smb2LeaseFlags leaseFlags;
		internal long leaseDuration;
		internal Guid parentLeaseKey;
		internal ushort epoch;
		internal ushort reserved;
	}
}
