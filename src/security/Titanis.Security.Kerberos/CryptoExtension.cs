using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Security.Kerberos
{
	internal static class CryptoExtension
	{
		public static int TransformBlock(
			this ICryptoTransform transform,
			Span<byte> block
			)
		{
			byte[] arr = block.ToArray();
			int cbTransform = transform.TransformBlock(
				arr, 0, block.Length,
				arr, 0);
			arr.CopyTo(block);
			return cbTransform;
		}
		public static int TransformBlock(
			this ICryptoTransform transform,
			Span<byte> input,
			Span<byte> output
			)
		{
			byte[] arrInput = input.ToArray();
			byte[] arrOutput = new byte[output.Length];
			int cbTransform = transform.TransformBlock(
				arrInput, 0, arrInput.Length,
				arrOutput, 0);
			arrOutput.CopyTo(output);
			return cbTransform;
		}
	}
}
