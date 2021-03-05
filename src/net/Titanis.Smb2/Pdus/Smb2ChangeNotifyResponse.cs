using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;
using Titanis.Winterop;

namespace Titanis.Smb2.Pdus
{
	// [MS-SMB2] § 2.2.36 SMB2 CHANGE_NOTIFY Response
	sealed class Smb2ChangeNotifyResponse : Smb2Pdu<Smb2ChangeNotifyResponseHeader>
	{
		internal List<FileChangeNotification>? infos;

		/// <inheritdoc/>
		internal sealed override Smb2Command Command => Smb2Command.ChangeNotify;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => 9;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			int offPdu = reader.Position - Smb2PduSyncHeader.StructSize;

			ref readonly Smb2ChangeNotifyResponseHeader body = ref reader.ReadChangeNotifyRespHdr();
			this.body = body;

			if (body.outputBufferLength > 0)
			{
				List<FileChangeNotification> infos = new List<FileChangeNotification>();
				int nextOffset = offPdu + body.outputBufferOffset;
				string? oldName = null;
				while (nextOffset >= reader.Position)
				{
					reader.Position = nextOffset;

					ref readonly FileNotifyInfoHeader notifyHdr = ref reader.ReadFileNotifyInfoHeader();
					string fileName = reader.ReadStringUni(notifyHdr.fileNameLength);
					if (notifyHdr.action == FileNotifyAction.RenamedOldName)
						oldName = fileName;
					else
					{
						FileChangeNotification info = new FileChangeNotification(
							notifyHdr.action,
							fileName,
							notifyHdr.action == FileNotifyAction.RenamedNewName ? oldName : null);
						oldName = null;
						infos.Add(info);
					}

					if (notifyHdr.nextEntryOffset == 0)
						break;

					nextOffset += notifyHdr.nextEntryOffset;
				}

				this.infos = infos;
			}
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref Smb2ChangeNotifyResponseHeader body)
		{
			ref Smb2ChangeNotifyResponseHeader pHdr = ref writer.WriteChangeNotifyRespHdr(body);

			if (this.infos != null)
			{
				int cbBuf = 0;
				int index = 0;
				foreach (var info in this.infos)
				{
					index++;
					bool fMore = (index < this.infos.Count);

					FileNotifyInfoHeader hdr = new FileNotifyInfoHeader();

					int cbFileName = hdr.fileNameLength = Encoding.Unicode.GetByteCount(info.FileName);
					int cbStruct =
						Smb2ChangeNotifyResponseHeader.StructSize
						+ cbFileName
						;
					cbStruct = BinaryHelper.Align(cbStruct, 4);
					cbBuf += cbStruct;

					if (fMore)
						hdr.nextEntryOffset = (writer.Position + cbStruct);
					else
						hdr.nextEntryOffset = 0;

					writer.WriteFileNotifyInfoReqHdr(hdr);
					writer.WriteStringUni(info.FileName);
					writer.Align(4);
				}

				pHdr.outputBufferOffset = (ushort)Smb2ChangeNotifyResponseHeader.StructSize;
				pHdr.outputBufferLength = (ushort)cbBuf;
			}
			else
			{
				pHdr.outputBufferOffset = 0;
				pHdr.outputBufferLength = 0;
			}
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct Smb2ChangeNotifyResponseHeader : ISmb2PduStruct
	{
		public unsafe static int StructSize => sizeof(Smb2ChangeNotifyResponseHeader);
		public ushort StructureSize { get => this.structureSize; set => this.structureSize = value; }

		internal ushort structureSize;
		internal ushort outputBufferOffset;
		internal uint outputBufferLength;
	}
}
