using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb.Test;

[TestClass]
public class S2kTests : CliCommandTest<S2kCommand>
{
	private const string MilchickAes256Key = "26d2823ca837dd162f9a41bf45d4e2c4c77bee0266d283c77a0b7f89c28a253e";
	private const string MilchickAes128Key = "f80bdb4b074f17a0c4b2d745c20b02f4";
	private const string MilchickRc4Hmac = "327e931f48594f4bbd9f10fef8b2841c";
	private const string MilchickDesCbcMd5 = "bf46087954f75e98";

	private const string AllentownAes256Key = "41d93c5f36f2e78d18150b5ca24f1802fbc6b14e8996281c6ae4442ade8e9eef";
	private const string AllentownAes128Key = "fd04dbd2968a5aedd62b3a66d48c3aca";
	private const string AllentownRc4Hmac = "8a9d093f14f8701df17732b2bb182c74";
	private const string AllentownDesCbcMd5 = "707a5d8cfd7f5210";

	[TestMethod]
	[CliTest("AllKeys")]
	public async Task S2kTest(Token[] args)
	{
		var results = await base.TestCommand(args);
		Assert.HasCount(4, results);

		foreach (SessionKey rec in results)
		{
			switch (rec.EType)
			{
				case EType.Aes128CtsHmacSha1_96:
					Assert.AreEqual(MilchickAes128Key, rec.KeyText);
					break;
				case EType.Aes256CtsHmacSha1_96:
					Assert.AreEqual(MilchickAes256Key, rec.KeyText);
					break;
				case EType.Rc4Hmac:
					Assert.AreEqual(MilchickRc4Hmac, rec.KeyText);
					break;
				case EType.DesCbcMd5:
					Assert.AreEqual(MilchickDesCbcMd5, rec.KeyText);
					break;
				default:
					Assert.Fail($"Unexpected EType {rec.EType}");
					break;
			}
		}
	}

	[TestMethod]
	[CliTest("AesKeys")]
	public async Task AesKeyTest(Token[] args)
	{
		var results = await base.TestCommand(args);
		Assert.HasCount(2, results);

		foreach (SessionKey rec in results)
		{
			switch (rec.EType)
			{
				case EType.Aes128CtsHmacSha1_96:
					Assert.AreEqual(MilchickAes128Key, rec.KeyText);
					break;
				case EType.Aes256CtsHmacSha1_96:
					Assert.AreEqual(MilchickAes256Key, rec.KeyText);
					break;
				default:
					Assert.Fail($"Unexpected EType {rec.EType}");
					break;
			}
		}
	}

	[TestMethod]
	[CliTest("AllAllentown")]
	public async Task AllentownTest(Token[] args)
	{
		var results = await base.TestCommand(args);
		Assert.HasCount(4, results);

		foreach (SessionKey rec in results)
		{
			switch (rec.EType)
			{
				case EType.Aes128CtsHmacSha1_96:
					Assert.AreEqual(AllentownAes128Key, rec.KeyText);
					break;
				case EType.Aes256CtsHmacSha1_96:
					Assert.AreEqual(AllentownAes256Key, rec.KeyText);
					break;
				case EType.Rc4Hmac:
					Assert.AreEqual(AllentownRc4Hmac, rec.KeyText);
					break;
				case EType.DesCbcMd5:
					Assert.AreEqual(AllentownDesCbcMd5, rec.KeyText);
					break;
				default:
					Assert.Fail($"Unexpected EType {rec.EType}");
					break;
			}
		}
	}
}
