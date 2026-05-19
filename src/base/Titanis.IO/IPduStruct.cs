using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.IO
{
	public interface IPduStruct
	{
		void ReadFrom(IByteSource reader);
		void WriteTo(ByteWriter writer);
	}
	public interface IPduStruct<T>
	{
		void ReadFrom(IByteSource reader, T arg);
		void WriteTo(ByteWriter writer, T arg);
	}
	public interface IPduStruct<T1, T2>
	{
		void ReadFrom(IByteSource reader, T1 arg1, T2 arg2);
		void WriteTo(ByteWriter writer, T1 arg1, T2 arg2);
	}

	public static class PduStructExtensions
	{
		public static Memory<byte> ToBytes<TStruct>(this TStruct struc, ByteWriter writer)
			where TStruct : struct, IPduStruct
		{
			writer.Reset();
			writer.WritePduStruct(struc);
			return writer.GetData();
		}
	}
}
