using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.PduStruct;


namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.16 - ClassHeader
	[PduStruct]
	partial struct ClassHeader
	{
		public ClassHeader(HeapStringRef classNameRef)
		{
			this.classNameRef = classNameRef;
		}

		// [MS-WMIO] § 2.2.73 - EncodingLength
		internal int encodingLength;

		// [MS-WMIO] § 2.2.76 - ReservedOctet
		internal byte reserved;

		internal HeapStringRef classNameRef;

		// [MS-WMIO] § 2.2.28 - NdTableValueTableLength
		internal uint ndValueTableLength;

		partial void OnAfterReadPdu(IByteSource source)
		{
			// UNDONE: Yeah the spec says it should be zero, but it usually isn't
			//Debug.Assert(this.reserved == 0);
		}
	}

	// [MS-WMIO] § 2.2.15 - ClassPart
	[PduStruct]
	partial struct ClassPart
	{
		public ClassPart(ClassHeader header)
		{
			this.header = header;
		}

		[PduPosition]
		internal long position;

		// [MS-WMIO] § 2.2.16 - ClassHeader
		internal ClassHeader header;

		// [MS-WMIO] § 2.2.17 - DerivationList
		internal DerivationList derivationList;

		// [MS-WMIO] § 2.2.20 - ClassQualifierSet
		internal QualifierSet qualifierSet;

		// [MS-WMIO] § 2.2.21 - PropertyLookupTable
		internal PropertyLookupTable propertyLookup;

		// [MS-WMIO] § 2.2.26 - NdTable
		[PduField(ReadMethod = nameof(ReadNdTable), WriteMethod = nameof(WriteNdTable))]
		internal NdTable ndTable;
		private NdTable ReadNdTable(IByteSource source, PduByteOrder byteOrder)
		{
			var propCount = this.propertyLookup.properties.Length;
			if (propCount > 0 && this.header.ndValueTableLength > 0)
			{
				ndTable = source.ReadNdTable(propCount);
				return ndTable;
			}
			else
			{
				return default;
			}
		}
		private void WriteNdTable(ByteWriter writer, NdTable value, PduByteOrder byteOrder)
		{
			writer.WriteBytes(this.ndTable.Bytes);
		}

		private int ValueTableLength =>
			(this.propertyLookup.properties.IsNullOrEmpty())
			? 0
			: Math.Max(0, ((int)this.header.ndValueTableLength - this.ndTable.ByteLength));

		// [MS-WMIO] § 2.2.29 - ValueTable
		[PduArraySize(nameof(ValueTableLength))]
		internal byte[] valueTable;

		internal Heap classHeap;

		[PduIgnore]
		internal byte[] bytes;

		partial void OnAfterReadPdu(IByteSource source)
		{
			source.Position = this.position;
			var cbClass = this.header.encodingLength;
			this.bytes = source.ReadBytes(this.header.encodingLength);

			var offClassEnd = this.position + cbClass;
			Debug.Assert(source.Position == offClassEnd);
			// HACK: Skip unused heap bytes
			source.Position = offClassEnd;
		}

		partial void OnAfterWritePdu(ByteWriter writer)
		{
			var offEnd = writer.Position;
			writer.SetPosition((int)this.position);
			writer.WriteUInt32LE((uint)(offEnd - writer.Position));
			writer.SetPosition(offEnd);
		}

		private WmiClassObject CreateClassWithMethods(
			string className,
			Heap classHeap,
			WmiClassObject? baseClass,
			WmiMethod[]? methods,
			byte[] methodsPartBytes)
		{
			// [MS-WMIO] § 2.2.21 - PropertyLookupTable
			var propTable = this.propertyLookup.properties;

			var props = new WmiProperty[propTable.Length];
			var valueTableReader = new ByteMemoryReader(this.valueTable);
			for (int i = 0; i < props.Length; i++)
			{
				var lookup = propTable[i];
				var prop = lookup.Resolve(
					classHeap,
					className,
					this.derivationList.classNames,
					valueTableReader,
					this.ndTable);
				props[i] = prop;
			}
			Array.Sort(props, (x, y) => (x.DeclarationOrder - y.DeclarationOrder));

			return new WmiClassObject(
				this.bytes,
				methodsPartBytes,
				className,
				baseClass,
				this.derivationList.classNames,
				this.qualifierSet.qualifiers.ConvertAll(q => q.Resolve(classHeap)),
				props,
				methods,
				(int)this.header.ndValueTableLength
				);
		}
		internal WmiClassObject CreateClass(WmiClassObject? baseClass)
		{
			// [MS-WMIO] § 2.2.37 - ClassHeap
			var classHeap = this.classHeap;
			string? className = classHeap.ResolveString(this.header.classNameRef);
			return this.CreateClassWithMethods(className, classHeap, baseClass, null, null);
		}

		internal WmiClassObject CreateClassWithMethods(WmiClassObject? baseClass, ref readonly MethodsPart methodsPart)
		{
			// [MS-WMIO] § 2.2.37 - ClassHeap
			var classHeap = this.classHeap;
			string? className = classHeap.ResolveString(this.header.classNameRef, "ClassHeader.Name");
			var methods = methodsPart.CreateMethods(className, ref this);
			return this.CreateClassWithMethods(className, classHeap, baseClass, methods, methodsPart.bytes);
		}
	}

	// [MS-WMIO] § 2.2.14 - ClassAndMethodsPart
	[PduStruct]
	partial struct ClassAndMethodsPart
	{
		internal ClassPart classPart;
		internal MethodsPart methodsPart;

		internal WmiClassObject CreateClass(WmiClassObject? baseClass)
		{
			return this.classPart.CreateClassWithMethods(baseClass, ref this.methodsPart);
		}
	}
}
