using ms_drsr;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Titanis.Msrpc.Msdrsr
{
	[TypeConverter(typeof(UsnVectorConverter))]
	public struct UsnVector
	{
		public UsnVector(ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length != 24)
				throw new ArgumentException($"The USN vector must be specified by exactly 24 bytes.", nameof(bytes));
			this.vec = MemoryMarshal.Read<USN_VECTOR>(bytes);
		}
		internal UsnVector(in USN_VECTOR vec)
		{
			this.vec = vec;
		}

		internal readonly USN_VECTOR vec;

		public ReadOnlySpan<byte> ToBytes()
		{
			return MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(in this.vec, 1));
		}

		public override string ToString()
		{
			return this.ToBytes().ToHexString();
		}
	}

	public class UsnVectorConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		}

		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
				return new UsnVector(BinaryHelper.ParseHexString(str));
			else
				return base.ConvertFrom(context, culture, value);
		}
	}
}
