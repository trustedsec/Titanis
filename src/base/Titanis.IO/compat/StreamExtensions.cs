using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.IO
{
	public static class CompatStreamExtensions
	{
		public static int Read(this Stream stream, Span<byte> data)
		{
			byte[] array = data.ToArray();
			return stream.Read(array, 0, data.Length);
		}
		public static async Task<int> ReadAsync(this Stream stream, Memory<byte> data)
		{
			byte[] array = data.ToArray();
			return await stream.ReadAsync(array, 0, data.Length);
		}
		public static void Write(this Stream stream, ReadOnlySpan<byte> buffer)
		{
			stream.Write(buffer.ToArray(), 0, buffer.Length);
		}
		public static async ValueTask WriteAsync(this Stream stream, ReadOnlyMemory<byte> buffer)
		{
			await stream.WriteAsync(buffer.ToArray(), 0, buffer.Length);
		}
		public static Task<int> ReadAsync(this Stream stream, byte[] buffer)
		{
			return stream.ReadAsync(buffer, 0, buffer.Length);
		}
	}
}
