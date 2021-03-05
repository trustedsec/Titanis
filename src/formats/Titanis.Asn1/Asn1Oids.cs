using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1
{
	public static class Asn1Oids
	{
		public static Asn1Oid Asn1Modules = new Asn1Oid(
			new Asn1OidPart(2, "joint-iso-itu-t"),
			new Asn1OidPart(1, "asn1"),
			new Asn1OidPart[]
			{
				// joint-iso-itu-t asn1(1) specification(0) modules(0)
				new Asn1OidPart(0, "specification"),
				new Asn1OidPart(0, "modules")
			});
		public static Asn1Oid DefinedTypes = new Asn1Oid(
			new Asn1OidPart(2, "joint-iso-itu-t"),
			new Asn1OidPart(1, "asn1"),
			new Asn1OidPart[]
			{
				// joint-iso-itu-t asn1(1) specification(0) modules(0)
				new Asn1OidPart(0, "specification"),
				new Asn1OidPart(0, "modules"),
				new Asn1OidPart(3, "defined-types-module"),
			});
	}
}
