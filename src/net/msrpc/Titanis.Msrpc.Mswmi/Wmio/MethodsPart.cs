using System.Diagnostics;
using Titanis.IO;
using Titanis.PduStruct;


namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.38 - MethodsPart
	[PduStruct]
	partial struct MethodsPart
	{
		[PduPosition]
		internal long position;

		// [MS-WMIO] § 2.2.73 - EncodingLength
		internal int encodingLength;

		// [MS-WMIO] § 2.2.39 - MethodCount
		internal ushort methodCount;

		// [MS-WMIO] § 2.2.40 - MethodCountPadding
		private ushort methodCountPadding;

		// [MS-WMIO] § 2.2.52 - MethodHeap
		[PduArraySize(nameof(methodCount))]
		internal MethodDescription[] methods;

		internal Heap methodHeap;

		[PduIgnore]
		internal byte[] bytes;

		partial void OnAfterReadPdu(IByteSource source)
		{
			var offMethodsPartEnd = (int)this.position + this.encodingLength;
			Debug.Assert(source.Position <= offMethodsPartEnd);

			// END MethodPart
			source.Position = this.position;
			this.bytes = source.ReadBytes(this.encodingLength);
		}

		partial void OnAfterWritePdu(ByteWriter writer)
		{
			var offEnd = writer.Position;
			this.encodingLength = (int)(offEnd - this.position);
			writer.SetPosition((int)this.position);
			writer.WriteInt32LE(this.encodingLength);
			writer.SetPosition(offEnd);
		}

		internal WmiMethod[] CreateMethods(
			string className,
			ref readonly ClassPart classPart)
		{
			var methodHeap = this.methodHeap;
			var derList = classPart.derivationList.classNames;
			var methods = this.methods.ConvertAll(r => r.Resolve(methodHeap, className, derList));
			return methods;
		}
	}
}
