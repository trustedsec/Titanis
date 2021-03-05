using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mswmi
{
	internal class QualifierNames
	{
		public const string Abstract = "abstract";
		public const string Subtype = "SUBTYPE";
		public const string CimType = "CIMTYPE";
		public const string Out = "out";
		public const string Id = "ID";
		public const string Dynamic = "dynamic";
		public const string Provider = "provider";
		public const string Read = "read";
		public const string Static = "Static";
		public const string In = "In";
		public const string Implemented = "Implemented";
		public const string Key = "key";
	}

	public static class QualifierFactory
	{
		public static WmiQualifier Abstract() => new WmiQualifier(QualifierNames.Abstract, WmiQualifierFlavor.None, CimType.Boolean, true);
		public static WmiQualifier CIMTYPE(string typeName) => new WmiQualifier(QualifierNames.CimType, WmiQualifierFlavor.None, CimType.String, typeName);
		public static WmiQualifier Id(int id) => new WmiQualifier(QualifierNames.Id, WmiQualifierFlavor.None, CimType.SInt32, id);
		public static WmiQualifier Out() => new WmiQualifier(QualifierNames.Out, WmiQualifierFlavor.None, CimType.Boolean, true);
		public static WmiQualifier In() => new WmiQualifier(QualifierNames.In, WmiQualifierFlavor.None, CimType.Boolean, true);
		public static WmiQualifier Key() => new WmiQualifier(QualifierNames.Key, WmiQualifierFlavor.None, CimType.Boolean, true);
		public static WmiQualifier Read() => new WmiQualifier(QualifierNames.Read, WmiQualifierFlavor.None, CimType.Boolean, true);
		public static WmiQualifier Static() => new WmiQualifier(QualifierNames.Static, WmiQualifierFlavor.None, CimType.Boolean, true);
		public static WmiQualifier Implemented() => new WmiQualifier(QualifierNames.Implemented, WmiQualifierFlavor.None, CimType.Boolean, true);
		public static WmiQualifier Dynamic() => new WmiQualifier(QualifierNames.Dynamic, WmiQualifierFlavor.PropagateToInstance, CimType.Boolean, true);
		public static WmiQualifier Provider(string providerName) => new WmiQualifier(QualifierNames.Provider, WmiQualifierFlavor.None, CimType.String, providerName);
	}
}
