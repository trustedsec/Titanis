using System;
using System.Collections.Generic;
using System.Diagnostics;
using Titanis.IO;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	[Flags]
	public enum NdFlags : byte
	{
		None = 0,

		IsNull = 1,
		DefaultInherited = 2,
	}

	// [MS-WMIO] § 2.2.23 - PropertyLookup
	[PduStruct]
	partial struct PropertyLookup
	{
		public PropertyLookup(
			HeapStringRef name,
			HeapRef info
			)
		{
			this.Name = name;
			this.Info = info;
		}
		public override string ToString()
			=> $"Name={this.Name.heapRef.Value}, Info={this.Info.Value}";

		// [MS-WMIO] § 2.2.24 - PropertyNameRef
		[PduField]
		public HeapStringRef Name { get; private set; }
		// [MS-WMIO] § 2.2.25 - PropertyInfoRef
		[PduField]
		public HeapRef Info { get; private set; }

		internal WmiProperty Resolve(
			Heap classHeap,
			string className,
			IList<string> derivationList,
			IByteSource valueTable,
			NdTable ndTable
			)
		{
			var name = classHeap.ResolveString(this.Name, "PropertyLookup.Name");

			// [MS-WMIO] § 2.2.30 - PropertyInfo
			Debug.Assert(this.Info.Value >= 0);
			var prop = classHeap.Resolve(new HeapRef((uint)this.Info.Value), HeapRangeType.PropertyInfo, $"PropertyLookup[n].PropertyInfo", reader =>
			{
				var propInfo = reader.ReadPduStruct<PropertyInfo>();
				var isInherited = (propInfo.propertyType & CimType.Inherited) != 0;
				if (isInherited)
					propInfo.propertyType &= ~CimType.Inherited;

				//Debug.Assert(isInherited == (0 != (ndFlags & NdFlags.DefaultInherited)));

				// TODO: Range check
				var classOfOrigin = propInfo.classOfOrigin < derivationList.Count
					? derivationList[propInfo.classOfOrigin]
					: className;

				var ndFlags = ndTable.GetFlags(propInfo.declarationOrder);

				var prop = new WmiProperty(
					name,
					propInfo.propertyType,
					propInfo.classOfOrigin,
					classOfOrigin,
					propInfo.declarationOrder,
					propInfo.qualifierSet.qualifiers?.ConvertAll(r => r.Resolve(classHeap)),
					propInfo.valueTableOffset,
					null,
					ndFlags
					);

				if (0 == ndFlags && (valueTable.Length > propInfo.valueTableOffset))
				{
					valueTable.Position = (int)propInfo.valueTableOffset;
					var encodedValue = valueTable.ReadEncodedValue(propInfo.propertyType);
					object value = encodedValue.Resolve(classHeap, prop);

					prop.DefaultValue = value;
				}

				return prop;
			});
			return prop;
		}
	}
}