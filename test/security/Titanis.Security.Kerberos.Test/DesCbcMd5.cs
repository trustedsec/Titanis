using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Crypto;

namespace Titanis.Security.Kerberos.Test;
[TestClass]
public class DesCbcMd5Test
{
	[TestMethod]
	[DataRow("ATHENA.MIT.EDUraeburn", "password", "cbc22fae235298e3")]
	[DataRow("WHITEHOUSE.GOVdanny", "potatoe", "df3d32a74fd92a01")]
	[DataRow("EXAMPLE.COMpianist", "\ud834\udd1e", "4ffb26bab0cd9413")]
	[DataRow("ATHENA.MIT.EDUJuri\u0161i\u0107", "\u00DF", "62c81a5232b5e69d")]
	[DataRow("AAAAAAAA", "11119999", "984054d0f1a73e31")]
	[DataRow("FFFFAAAA", "NNNN6666", "c4bf6b25adf7a4f8")]
	public void TestStringToKey(string salt, string password, string expectedKey)
	{
		var prof = new EncProfile_DesCbcMd5();
		var key = prof.StringToKey(password, salt);

		Assert.AreEqual(expectedKey, BinaryHelper.ToHexString(key.KeyBytes));
	}

	[TestMethod]
	public void DecryptMilchick()
	{
		var des = new EncProfile_DesCbcMd5();
		var key = des.StringToKey("Br3@kr00m!", "LUMON.INDmilchick");
		var decrypted = key.Decrypt(KeyUsage.AsreqPaEncTimestamp, Structs.EncryptedData(EType.DesCbcMd5, milchickTimestamp));
	}

	[TestMethod]
	[DataRow("a", DisplayName = "1 byte")]
	[DataRow("12345678", DisplayName = "1 block")]
	[DataRow("1234567812345678", DisplayName = "2 blocks")]
	[DataRow("123456789", DisplayName = "1 block + 1 byte")]
	public void Roundtrip(string text)
	{
		var des = new EncProfile_DesCbcMd5();
		var key = des.StringToKey("Br3@kr00m!", "LUMON.INDmilchick");

		var encrypted = key.EncryptAndWrap(KeyUsage.AsrepEncPart, Encoding.UTF8.GetBytes(text));
		var decrypted = key.Decrypt(KeyUsage.AsrepEncPart, encrypted);
	}

	private static byte[] milchickTimestamp = BinaryHelper.ParseHexString("b54e776d1ef994f9d4221672c6e6c01e8470baf139ffdc9bab641e2d5ce00a274911589b78005c8d4b57f9aefcc88029562f9f2301fa24e6");



	[TestMethod]
	public void PermuteSTables()
	{
		var S = DesPrimitives.S;
		byte[] newS = new byte[64 * 8];
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < 8; i++)
		{
			sb.AppendLine($"// S{i + 1}");

			var tableBase = i * 64;
			for (int j = 0; j < 64; j++)
			{
				var c = j % 16;
				var r = j / 16;

				var newIndex = (c << 1) | (r & 1) | ((r & 2) << 4);

				newS[tableBase + newIndex] = S[tableBase + j];
			}

			for (int j = 0; j < 4; j++)
			{
				sb.Append(string.Join(", ", newS.Slice(tableBase + j * 16, 16).ToArray().Select(r => r.ToString().PadLeft(2))));
				sb.AppendLine(",");
			}
		}

		var tables = sb.ToString();
		//var rev = Array.ConvertAll(DesPrimitives.PC2, r => 56 - r);
		//string s = string.Join(", ", rev);
	}
}
