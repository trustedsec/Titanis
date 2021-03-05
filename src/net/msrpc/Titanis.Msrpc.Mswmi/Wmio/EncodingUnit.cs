using System;
using System.Diagnostics;
using Titanis.IO;
using Titanis.PduStruct;


namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.1 - EncodingUnit
	[PduStruct]
	partial struct EncodingUnit
	{
		// [MS-WMIO] § 2.2.77 - Signature
		private EncodingUnitSignature sig;

		// [MS-WMIO] § 2.2.4 - ObjectEncodingLength
		private uint objectLength;

		[PduPosition]
		private long position;

		// [MS-WMIO] § 2.2.5 - ObjectBlock
		internal ObjectBlock objectBlock;

		partial void OnAfterReadPdu(IByteSource source)
		{
			if (this.objectLength > int.MaxValue)
				throw new NotSupportedException("The object is too large and cannot be read by this implementation.");

			long offObjEnd = this.position + this.objectLength;
			Debug.Assert(source.Position <= offObjEnd);
			source.Position = offObjEnd;
		}
		partial void OnAfterWritePdu(ByteWriter writer)
		{
			var endPos = writer.Position - this.position;
			this.objectLength = (uint)(endPos - this.position);
			writer.SetPosition((int)(this.position - 4));
			writer.WriteUInt32LE(this.objectLength);
			writer.SetPosition((int)endPos);
		}
	}
}
