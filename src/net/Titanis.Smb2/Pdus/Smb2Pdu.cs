using System;
using System.Collections.Generic;
using System.Text;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	/// <summary>
	/// Represents a PDU within SMB2.
	/// </summary>
	abstract class Smb2Pdu
	{
		internal Smb2PduHeaderBuffer pduhdrbuf;

		internal Smb2Pdu? request;

		internal ref Smb2PduSyncHeader pduhdr => ref this.pduhdrbuf.sync;
		public ulong SessionId
		{
			get => this.pduhdr.sessionId;
			set => this.pduhdr.sessionId = value;
		}
		internal bool IsSigned => 0 != (this.pduhdr.flags & Smb2PduFlags.Signed);

		internal abstract Smb2Command Command { get; }
		internal virtual Smb2Priority Priority => 0;
		internal virtual int SendPayloadSize => 0;
		internal virtual int ResponsePayloadSize => 0;

		internal const int CreditChunkSize = 65536;
		internal ushort CreditCharge => (ushort)((Math.Max(this.SendPayloadSize, this.ResponsePayloadSize) - 1) / CreditChunkSize + 1);

		internal virtual bool CanReducePayload => false;
		internal virtual void AdjustPayload(int excessSize)
			=> throw new NotSupportedException();

		internal abstract void WriteTo(ByteWriter writer);
		internal abstract void ReadFrom(
			ByteMemoryReader reader,
			ref readonly Smb2PduSyncHeader pduHdr
			);

		internal virtual void OnResponse(Smb2Message msg)
		{
		}

		internal virtual void OnSending(Span<byte> pduBytes)
		{
		}
	}

	/// <summary>
	/// Marks a structure that forms the body of a PDU.
	/// </summary>
	internal interface ISmb2PduStruct
	{
		/// <summary>
		/// Gets or sets the structure size.
		/// </summary>
		/// <remarks>
		/// Note that this is not always the actual size of the structure, as bit 0
		/// indicates whether the PDU has a dynamic size.
		/// </remarks>
		ushort StructureSize { get; set; }
	}
	abstract class Smb2Pdu<TBody> : Smb2Pdu
		where TBody : struct, ISmb2PduStruct
	{
		protected Smb2Pdu()
		{
		}

		/// <summary>
		/// Gets the valid size for <see cref="ISmb2PduStruct.StructureSize"/> of <see cref="body"/>.
		/// </summary>
		protected abstract ushort ValidBodySize { get; }

		/// <summary>
		/// Structure forming the fixed body of the PDU.
		/// </summary>
		internal TBody body;

		/// <summary>
		/// Writes the PDU.
		/// </summary>
		/// <param name="writer">Writer</param>
		/// <param name="body">Body structure</param>
		internal abstract void WriteTo(ByteWriter writer, ref TBody body);
		/// <summary>
		/// Writes the PDU.
		/// </summary>
		/// <param name="writer">Writer</param>
		/// <remarks>
		/// The implementation sets <see cref="ISmb2PduStruct.StructureSize"/> of the body to <see cref="ValidBodySize"/>.
		/// This was refactored 
		/// </remarks>
		internal sealed override void WriteTo(ByteWriter writer)
		{
			this.body.StructureSize = this.ValidBodySize;
			this.WriteTo(writer, ref this.body);
		}
	}
}
