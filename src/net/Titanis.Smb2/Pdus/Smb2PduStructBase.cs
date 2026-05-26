using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	abstract class Smb2PduStructBase<TStruct> : Smb2Pdu<TStruct>
		where TStruct : struct, IPduStruct, ISmb2PduStruct2
	{
		/// <inheritdoc/>
		internal sealed override Smb2Command Command => TStruct.Command;
		/// <inheritdoc/>
		protected sealed override ushort ValidBodySize => TStruct.ValidSmbSize;

		/// <inheritdoc/>
		internal sealed override void ReadFrom(ByteMemoryReader reader, ref readonly Smb2PduSyncHeader pduHdr)
		{
			this.body = reader.ReadPduStruct<TStruct>();
		}

		/// <inheritdoc/>
		internal sealed override void WriteTo(ByteWriter writer, ref TStruct body)
		{
			writer.WritePduStruct(body);
		}
	}
}
