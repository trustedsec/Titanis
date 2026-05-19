using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.IO
{
	public interface IPduStruct
	{
		void ReadFrom<TSource>(TSource reader) where TSource : class, IByteSource;
		void WriteTo(ByteWriter writer);
	}
	public interface IPduStruct<T>
	{
		void ReadFrom<TSource>(TSource reader, T arg) where TSource : class, IByteSource;
		void WriteTo(ByteWriter writer, T arg);
	}
	public interface IPduStruct<T1, T2>
	{
		void ReadFrom<TSource>(TSource reader, T1 arg1, T2 arg2) where TSource : class, IByteSource;
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
