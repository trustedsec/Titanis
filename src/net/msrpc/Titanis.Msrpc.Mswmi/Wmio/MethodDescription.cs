using System;
using System.Collections.Generic;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	struct MethodSignature
	{

	}
	// [MS-WMIO] § 2.2.41 - MethodDescription
	[PduStruct]
	partial struct MethodDescription
	{
		// [MS-WMIO] § 2.2.42 MethodName
		internal HeapStringRef nameRef;
		// [MS-WMIO] § 2.2.43 - MethodFlags
		internal WmiQualifierFlavor flags;
		// [MS-WMIO] § 2.2.44 - MethodPadding
		private byte pad1, pad2, pad3;
		// [MS-WMIO] § 2.2.45 - MethodOrigin
		internal uint methodOriginIndex;
		// [MS-WMIO] § 2.2.46 - MethodQualifiers
		internal HeapRef qualHeapRef;
		// [MS-WMIO] § 2.2.48 - InputSignature
		internal HeapRef inputSig;
		// [MS-WMIO] § 2.2.49 - OutputSignature
		internal HeapRef outputSig;

		public MethodDescription(HeapStringRef name, WmiQualifierFlavor flags, uint methodOriginIndex, HeapRef qualHeapRef, HeapRef inputSig, HeapRef outputSig)
		{
			this.nameRef = name;
			this.flags = flags;
			this.methodOriginIndex = methodOriginIndex;
			this.qualHeapRef = qualHeapRef;
			this.inputSig = inputSig;
			this.outputSig = outputSig;
		}

		internal WmiMethod Resolve(Heap methodHeap, string className, IList<string> derivationList)
		{
			var name = methodHeap.ResolveString(this.nameRef);
			var originClassName =
				this.methodOriginIndex < derivationList.Count ? derivationList[(int)this.methodOriginIndex]
				: className;

			WmiQualifier[] qualifiers;
			if (!this.qualHeapRef.IsNull)
			{
				var quals = methodHeap.Resolve(this.qualHeapRef, HeapRangeType.QualifierSet, WmiReader.ReadQualifierSet);
				qualifiers = quals.ConvertAll(r => r.Resolve(methodHeap));
			}
			else
			{
				qualifiers = Array.Empty<WmiQualifier>();
			}

			// InputSignature
			var inputSig = methodHeap.Resolve(this.inputSig, HeapRangeType.InputSignature, WmiReader.ReadMethodSignatureBlock);

			// OutputSignature
			var outputSig = methodHeap.Resolve(this.outputSig, HeapRangeType.OutputSignature, WmiReader.ReadMethodSignatureBlock);

			return new WmiMethod(
				name,
				originClassName,
				this.flags,
				qualifiers,
				inputSig,
				outputSig
				);
		}
	}
}