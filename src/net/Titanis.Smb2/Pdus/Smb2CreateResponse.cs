using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.14 SMB2 CREATE Response
	sealed class Smb2CreateResponse : Smb2Pdu<Smb2CreateResponseBody>
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.Create;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 89;

		public bool HasDurableHandle { get; set; }
		public Smb2FileAccessRights MaximalAccessAllowed { get; set; }
		public ulong FileId { get; set; }
		public ulong VolumeId { get; set; }

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			// TODO: Validate header values

			this.body = reader.ReadCreateRespHdr();

			bool hasDurableHandle = false;
			Smb2FileAccessRights maxAccess = 0;
			ulong fileId = 0;
			ulong volumeId = 0;

			if (this.body.createContextsLength > 0)
			{
				var next = this.body.createContextsOffset;
				var offCtx = offPdu;
				while (next != 0)
				{
					offCtx += next;
					reader.Position = offCtx;
					ref readonly var ctxhdr = ref reader.ReadCreateContextHdr();
					next = ctxhdr.next;

					reader.Position = offCtx + ctxhdr.nameOffset;
					Smb2CreateContextId id =
						(ctxhdr.nameLength == 4) ? (Smb2CreateContextId)reader.ReadUInt32LE()
						: 0;
					string name = (ctxhdr.nameLength != 4) ? reader.ReadStringUtf8(ctxhdr.nameLength) : null;

					if (ctxhdr.dataLength > 0)
					{
						reader.Position = offCtx + ctxhdr.dataOffset;

						switch (id)
						{
							case Smb2CreateContextId.ExtendedAttributes:
							case Smb2CreateContextId.SecurityDescriptor:
								// No response
								break;

							case Smb2CreateContextId.DurableHandleRequest:
								hasDurableHandle = true;
								break;

							case Smb2CreateContextId.QueryMaximalAccessRequest:
								if (ctxhdr.dataLength >= 0)
								{
									var status = reader.ReadUInt32LE();
									if (status == 0)
									{
										maxAccess = (Smb2FileAccessRights)reader.ReadUInt32LE();
									}
								}
								break;
							case Smb2CreateContextId.QueryDiskId:
								if (ctxhdr.dataLength > 16)
								{
									fileId = reader.ReadUInt64LE();
									volumeId = reader.ReadUInt64LE();
								}
								break;

								// TODO: Implement additional create context responses
								//case Smb2CreateContextId.DurableHandleReconnect:
								//	break;
								//case Smb2CreateContextId.AllocationSize:
								//	break;
								//case Smb2CreateContextId.TimewarpToken:
								//	break;
								//case Smb2CreateContextId.RequestLease:
								//	break;
								//case Smb2CreateContextId.DurableHandleRequestV2:
								//	break;
								//case Smb2CreateContextId.DurableHandleReconnectV2:
								//	break;
								//default:
								//	break;
						}
					}
				}
			}

			this.HasDurableHandle = hasDurableHandle;
			this.MaximalAccessAllowed = maxAccess;
			this.FileId = fileId;
			this.VolumeId = volumeId;
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2CreateResponseBody body)
		{
			writer.WriteCreateRespHdr(this.body);
		}
	}

	[Flags]
	public enum Smb2CreateResponseFlags : byte
	{
		None = 0,
	}

	public enum Smb2CreateAction : uint
	{
		Superseded = 0,
		Opened = 1,
		Created = 2,
		Overwritten = 3,

		// Titanis-specific, set by PutFile if the destination was a directory and the file name was appended.
		IsDirectory = 0x8000_0000
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2FileOpenInfo
	{
		internal Smb2OplockLevel oplockLevel;
		internal Smb2CreateResponseFlags flags;
		internal Smb2CreateAction createAction;

		internal Smb2OpenFileAttributes attrs;

		internal uint reserved2;
		internal Smb2FileHandle fileId;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2CreateResponseBody : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2CreateResponseBody);
		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }

		internal ushort structureSize;
		internal Smb2FileOpenInfo fileInfo;

		internal int createContextsOffset;
		internal int createContextsLength;
	}
}
