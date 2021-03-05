using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	[PduStruct]
	internal partial struct PropertyInfo
	{
		// [MS-WMIO] § 2.2.31 - PropertyType
		internal CimType propertyType;
		// [MS-WMIO] § 2.2.33 - DeclarationOrder
		internal ushort declarationOrder;
		// [MS-WMIO] § 2.2.34 - ValueTableOffset
		internal uint valueTableOffset;
		// [MS-WMIO] § 2.2.35 - ClassOfOrigin
		internal int classOfOrigin;
		// [MS-WMIO] § 2.2.36 - PropertyQualifierSet
		internal QualifierSet qualifierSet;
	}
}
