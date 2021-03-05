using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Security.Spnego.Test
{
	[Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
	public class CertTests
	{
		[TestMethod]
		public void TestCert()
		{
			X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
			store.Open(OpenFlags.ReadOnly);
			var cert = store.Certificates[0];
			var parameters = cert.GetRSAPublicKey().ExportParameters(false);
			BigInteger n = new BigInteger(parameters.Modulus, true, true);
			long size = n.GetByteCount();
			var cosize = size << 24;

			int taskCount = Environment.ProcessorCount;
			Task<BigInteger>[] tasks = new Task<BigInteger>[taskCount];
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = Task.Factory.StartNew(() =>
				{
					byte[] newNumBytes = new byte[(int.MaxValue - 0x100) >> 8];

					BigInteger gcd;
					int attempt = 1;
					do
					{
						GenNumber(newNumBytes);
						BigInteger newNum = new BigInteger(newNumBytes, true);
						gcd = Gcd(newNum, n);
						attempt++;

						Debug.WriteLine($"Attempt: {attempt}");
					} while (gcd.IsOne);
					return gcd;
				}, TaskCreationOptions.LongRunning);
			}

			var result = Task.WhenAny(tasks).Result.Result;
		}

		private void GenNumber(byte[] newNumBytes)
		{
			RandomNumberGenerator.Fill(newNumBytes);
			//int partCount = Environment.ProcessorCount;
			//int partSize = newNumBytes.Length / partCount;
			//Thread[] threads = new Thread[partCount];

			//for (int i = 0; i < threads.Length; i++)
			//{
			//	threads[i] = CreateRandThread(i, partSize, newNumBytes);
			//	threads[i].Start();
			//}
			//foreach (var thread in threads)
			//{
			//	thread.Join();
			//}
		}

		private Thread CreateRandThread(int i, int partSize, byte[] newNumBytes)
		{
			return new Thread(() =>
			{
				RandomNumberGenerator.Fill(
					new Span<byte>(newNumBytes, i * partSize, partSize)
					);
			});
		}

		private BigInteger Gcd(BigInteger newNum, BigInteger n)
		{
			while (!n.IsZero)
			{
				var r = newNum % n;
				newNum = n;
				n = r;
			}

			return newNum;
		}
	}
}
