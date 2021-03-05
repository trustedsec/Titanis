using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.IO
{
	public interface IPduStruct
	{
		void ReadFrom(IByteSource reader);
		void ReadFrom(IByteSource reader, PduByteOrder byteOrder);
		void WriteTo(ByteWriter writer);
		void WriteTo(ByteWriter writer, PduByteOrder byteOrder);
	}
	public interface IPduStruct<T>
	{
		void ReadFrom(IByteSource reader, T arg);
		void ReadFrom(IByteSource reader, PduByteOrder byteOrder, T arg);
		void WriteTo(ByteWriter writer, T arg);
		void WriteTo(ByteWriter writer, PduByteOrder byteOrder, T arg);
	}
	public interface IPduStruct<T1, T2>
	{
		void ReadFrom(IByteSource reader, T1 arg1, T2 arg2);
		void ReadFrom(IByteSource reader, PduByteOrder byteOrder, T1 arg1, T2 arg2);
		void WriteTo(ByteWriter writer, T1 arg1, T2 arg2);
		void WriteTo(ByteWriter writer, PduByteOrder byteOrder, T1 arg1, T2 arg2);
	}
}
