using ms_wmi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Msrpc.Msdcom;

namespace Titanis.Msrpc.Mswmi
{
	public sealed class WmiObjref : Objref_Custom
	{
		internal WmiObjref(WmiObject wmiObject)
		{
			this.WmiObject = wmiObject;
		}

		internal const uint EncodingUnitSignature = 0x12345678;

		public sealed override Guid Iid => typeof(IWbemClassObject).GUID;

		public sealed override Guid MarshalClsid => WbemClassObjectUnmarshaler.Clsid;
		public WmiObject WmiObject { get; }

		public sealed override object GetObject() => this.WmiObject;

		protected sealed override void WriteObjectData(ByteWriter writer)
		{
			writer.WriteEncodingUnit(this.WmiObject);
		}
	}
}
