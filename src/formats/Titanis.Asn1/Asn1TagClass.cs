using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1
{
	public enum Asn1TagClass : byte
	{
		Universal = 0,
		Application = 0x40,
		Context = 0x80,
		Private = 0xC0,

		Mask = 0xC0,
	}
	[Flags]
	public enum Asn1TagFlags : byte
	{
		Universal = 0,
		Constructed = 0x20,
		Application = 0x40,
		Context = 0x80,
		Private = 0xC0,

		Mask = 0xE0,
		ClassMask = 0xC0,
	}
}
