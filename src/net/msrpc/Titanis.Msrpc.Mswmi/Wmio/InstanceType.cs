using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.53 - InstanceType
	[PduStruct]
	internal partial struct InstanceType
	{
		[PduField(ReadMethod = nameof(ReadClassPart), WriteMethod = nameof(WriteClassPart))]
		internal ClassPart classPart;
		[PduIgnore]
		internal byte[]? classPartBytes;
		private ClassPart ReadClassPart(IByteSource source, PduByteOrder byteOrder)
		{
			return source.ReadPduStruct<ClassPart>();
		}
		private void WriteClassPart(ByteWriter writer, ClassPart value, PduByteOrder byteOrder)
		{
			if (this.classPartBytes != null)
				writer.WriteBytes(this.classPartBytes);
			else
				writer.WritePduStruct(value);
		}

		[PduPosition]
		internal long position;

		internal int encodingLength;
		// [MS-WMIO] § 2.2.54 - InstanceFlags
		internal byte instanceFlags;
		// [MS-WMIO] § 2.2.55 - InstanceClassName
		internal HeapStringRef classNameRef;

		[PduField(ReadMethod = nameof(ReadNdTable), WriteMethod = nameof(WriteNdTable))]
		internal NdTable ndTable;
		private NdTable ReadNdTable(IByteSource source, PduByteOrder byteOrder)
		{
			return source.ReadNdTable(this.classPart.propertyLookup.properties.Length);
		}
		private void WriteNdTable(ByteWriter byteWriter, NdTable value, PduByteOrder byteOrder)
		{
			byteWriter.WriteBytes(value.Bytes);
		}

		private int InstanceDataSize => (int)(this.classPart.header.ndValueTableLength - this.ndTable.Bytes.Length);

		// [MS-WMIO] § 2.2.56 - InstanceData
		[PduArraySize(nameof(InstanceDataSize))]
		internal byte[] instanceData;

		// [MS-WMIO] § 2.2.57 - InstanceQualifierSet
		internal QualifierSet instanceQualifierSet;
		// [MS-WMIO] § 2.2.65 - InstancePropQualifierSet
		internal InstancePropQualsetFlag qualsetFlag;

		private int PropQualsetCount => (this.qualsetFlag == InstancePropQualsetFlag.HasProps)
			? this.classPart.propertyLookup.properties.Length
			: 0;
		// [MS-WMIO] § 2.2.65 - InstancePropQualifierSet
		[PduArraySize(nameof(PropQualsetCount))]
		internal QualifierSet[] propQualsets;

		// [MS-WMIO] § 2.2.58 - InstanceHeap
		internal Heap instanceHeap;

		partial void OnAfterReadPdu(IByteSource source)
		{
			// Even though the spec requires 0, some objects have 1
			Debug.Assert(this.instanceFlags is 0 or 1);

			var offInstance = this.position;
			var cb = this.encodingLength;
			var offInstanceEnd = offInstance + cb;
			Debug.Assert(source.Position <= offInstanceEnd);
			source.Position = (int)offInstanceEnd;
		}

		partial void OnAfterWritePdu(ByteWriter writer)
		{
			var offEnd = writer.Position;
			this.encodingLength = (int)(offEnd - this.position);
			writer.SetPosition((int)this.position);
			writer.WriteInt32LE(this.encodingLength);
			writer.SetPosition(offEnd);
		}

		public WmiInstanceObject CreateInstance(WmiDecoration? decoration)
		{
			var curClass = this.classPart.CreateClass(null);
			var instanceHeap = this.instanceHeap;
			var className = instanceHeap.ResolveString(this.classNameRef);
			var instanceQualifiers = this.instanceQualifierSet.qualifiers.ConvertAll(r => r.Resolve(instanceHeap));

			var propQualsets = this.propQualsets;
			//if (instanceType.qualsetFlag == InstancePropQualsetFlag.HasProps)
			//{
			//	// 2 indicates QualifierSets are present
			//	var propCount = curClass.Properties.Length;
			//	propQualsets = new Qualifier[propCount][];
			//	for (int i = 0; i < propCount; i++)
			//	{
			//		var qualset = reader.ReadQualifierSet();
			//		propQualsets[i] = qualset;
			//	}
			//}
			//else if (instanceType.qualsetFlag != InstancePropQualsetFlag.NoProps)
			//	throw new InvalidDataException($"Invalid InsatncePropQUalifierSetFlag encountered ({(byte)instanceType.qualsetFlag}).");

			var instanceData = new Heap(this.instanceData);

			var props = curClass.Properties;
			WmiInstanceProperty[] instanceProps = new WmiInstanceProperty[props.Length];
			for (int i = 0; i < instanceProps.Length; i++)
			{
				var prop = props[i];

				// Qualifiers
				WmiQualifier[]? propQualifiers = null;
				if (propQualsets.Length > 0)
				{
					var propQualset = propQualsets[i];
					propQualifiers = propQualset.qualifiers.ConvertAll(r => r.Resolve(instanceHeap));
				}

				// Value
				var ndFlags = this.ndTable.GetFlags(i);
				object? propValue;
				if (ndFlags == 0)
				{
					var encodedValue = instanceData.Resolve(
						new HeapRef(prop.ValueTableOffset),
						HeapRangeType.EncodedValue,
						valueReader => valueReader.ReadEncodedValue(prop.PropertyType)
						);
					propValue = encodedValue.Resolve(instanceHeap, prop);
				}
				else
				{
					propValue = null;
				}

				var instanceProp = new WmiInstanceProperty(prop, propQualifiers, ndFlags, propValue);
				instanceProps[i] = instanceProp;
			}


			var instance = new WmiInstanceObject(
				curClass,
				instanceQualifiers,
				instanceProps);
			return instance;
		}
	}
}
