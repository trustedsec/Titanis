using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Msrpc.Mswmi.Wmio
{
#if DEBUG
	class Box<T> where T : struct
	{
		internal T boxed;
	}
#endif

	// [MS-WMIO] § 2.2.11 - ClassType
	internal partial struct ClassType : IPduStruct
	{
		// [MS-WMIO] § 2.2.12 - ParentClass
		internal WmiClassObject parentClass;
		// [MS-WMIO] § 2.2.13 - CurrentClass
		internal ClassAndMethodsPart currentClass;
		private WmiClassObject _cls;

		public void ReadFrom(IByteSource reader)
		{
			// [MS-WMIO] § 2.2.12 - ParentClass
			ClassAndMethodsPart parentClassStruc = reader.ReadPduStruct<ClassAndMethodsPart>();
			WmiClassObject parentClass = parentClassStruc.CreateClass(null);
			// [MS-WMIO] § 2.2.13 - CurrentClass
			ClassAndMethodsPart currentClass = reader.ReadPduStruct<ClassAndMethodsPart>();

#if DEBUG
			var box = new Box<ClassAndMethodsPart>() { boxed = currentClass };
#endif

			var cls = currentClass.CreateClass(parentClass);
			this._cls = cls;
		}

		public void ReadFrom(IByteSource reader, PduByteOrder byteOrder)
			=> this.ReadFrom(reader);

		public void WriteTo(ByteWriter writer)
		{
			throw new NotImplementedException();
		}

		public void WriteTo(ByteWriter writer, PduByteOrder byteOrder)
		{
			throw new NotImplementedException();
		}

		internal WmiClassObject CreateClass()
		{
			return this._cls;
		}
	}
}
