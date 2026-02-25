using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.IO.Test;

[TestClass]
public class ByteSourceTests
{
	delegate void WriterAction<T>(Span<byte> bytes, T value);
	private static void TestRead<T>(T value, int size, WriterAction<T> writer, Func<IByteSource, T> readFunc)
	{
		var bytes = new byte[size];
		writer(bytes, value);

		ByteMemoryReader reader = new ByteMemoryReader(bytes);
		var actual = readFunc(reader);
		Assert.AreEqual(value, actual);
	}

	[TestMethod]
	public void TestReadDouble()
	{
		TestRead(double.E, 8, BinaryPrimitives.WriteDoubleLittleEndian, ByteSource.ReadDoubleLE);
		TestRead(double.E, 8, BinaryPrimitives.WriteDoubleBigEndian, ByteSource.ReadDoubleBE);
	}
}
