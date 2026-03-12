using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Crypto.Test
{
	[TestClass]
	public class KdfTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			byte[] kdk = BinaryHelper.ParseHexString("419FDDF34C1E001909D362AE7FB6AF79");
			byte[] context = BinaryHelper.ParseHexString("B23F3CBFD69487D9832B79B1594A367CDD950909B774C3A4C412B4FCEA9EDDDBA7DB256BA2EA30E977F11F9B113247578E0E915C6D2A513B8F2FCA5707DC8770");
			byte[] label = Encoding.UTF8.GetBytes("SMBSigningKey\0");

			byte[] expected = BinaryHelper.ParseHexString("8765949DFEAEE105CE9118B45BE988F0");

			byte[] dk = Sp800_108.KdfCtr(label, context, 128, new HMACSHA256(kdk));
		}
	}
}
