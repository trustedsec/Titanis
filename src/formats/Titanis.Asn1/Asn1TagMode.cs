using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1
{
	public enum Asn1TagMode
	{
		// § 13.2 - EXPLICIT is the default
		Explicit = 0,
		Implicit,
		Automatic
	}
}
