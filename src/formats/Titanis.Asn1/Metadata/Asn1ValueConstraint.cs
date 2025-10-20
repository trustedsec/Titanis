using System;

namespace Titanis.Asn1.Metadata
{
	public class Asn1ValueConstraint : Asn1ValueSetConstraint
	{
		public Asn1ValueConstraint(object? value)
		{
			this.Value = value;
		}

		public object? Value { get; }

		public override Asn1Int64Range? TryGetInt64Range()
		{
			var value = this.Value;
			var tc = Convert.GetTypeCode(value);
			return tc switch
			{
				TypeCode.Byte
				or TypeCode.UInt16
				or TypeCode.UInt32
				or TypeCode.UInt64 => TryFromValue(Convert.ToUInt64(value)),

				TypeCode.SByte
				or TypeCode.Int16
				or TypeCode.Int32
				or TypeCode.Int64 => new Asn1Int64Range(Convert.ToInt64(value)),

				_ => null
			};
		}

		private Asn1Int64Range? TryFromValue(ulong v)
			=> (v <= long.MaxValue) ? new Asn1Int64Range((long)v) : null;

		public override Asn1UInt64Range? TryGetUInt64Range()
		{
			var value = this.Value;
			var tc = Convert.GetTypeCode(value);
			return tc switch
			{
				TypeCode.Byte
				or TypeCode.UInt16
				or TypeCode.UInt32
				or TypeCode.UInt64 => new Asn1UInt64Range(Convert.ToUInt64(value)),

				TypeCode.SByte
				or TypeCode.Int16
				or TypeCode.Int32
				or TypeCode.Int64 => TryFromValue(Convert.ToInt64(value)),

				_ => null
			};
		}

		private Asn1UInt64Range? TryFromValue(long v)
			=> v >= 0 ? new Asn1UInt64Range((ulong)v) : null;
	}
}