using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos.Test;

[TestClass]
public class Rc4HmacTests
{

	private static readonly byte[] Key =
	{
			0xac, 0x8e, 0x65, 0x7f, 0x83, 0xdf, 0x82, 0xbe,
			0xea, 0x5d, 0x43, 0xbd, 0xaf, 0x78, 0x00, 0xcc
		};

	[TestMethod]
	public void StringToKeyTest()
	{
		// [RFC 4757] § 2
		var rc4 = new Rc4Hmac();
		var actual = rc4.StringToKey("foo", Array.Empty<byte>());
		CollectionAssert.AreEqual(Key, actual.KeyBytes);
	}

	[TestMethod]
	public void DecryptAsrepTest()
	{
		KerberosClient kerb = new KerberosClient(new SimpleKdcLocator(new IPEndPoint(IPAddress.Any, 88)));

		byte[] repbuf = BinaryHelper.ParseHexString("000006006b8205fc308205f8a003020105a10302010ba30b1b094c554d4f4e2e494e44a4153013a003020101a10c300a1b086d696c636869636ba58204a26182049e3082049aa003020105a10b1b094c554d4f4e2e494e44a21e301ca003020102a11530131b066b72627467741b094c554d4f4e2e494e44a382046430820460a003020112a103020102a28204520482044ecc41cd5a12839bbf8f9694f86c3b2c04adc8ddbc44f55bd4c3a36b80fbc21c4f498bdc4d671214ac02bf273b15a77126c0f1fb05ffd693af37cd25be704a3b5fddfe9d821c2a7c2d5c71c2b5a84c9a0acab01d2b626be7e0cedea48a5893ac3057761ac684ec2d3746f29f75d00490634d7fea0330791207df6f7f4a18c04655287783e702cfdfb3ef09d93a5a8b4caf8b0dbcad3b4a8326aecf8366d7eada2c30bb4453b7cf5f3b56f9bd47086664172c1804041b05dcf683812f14a585664fb5994ebc91b236ccec093622dbafd2f9dccec1ca59b65ac18f62a8c2eef541bd52e6c710e3fc403ede2e85eb4d2b15a4d00e60b0b9062b28234bce2292192278bbeb61bc919abadcaf1eb7f6565f8edbe2a07a24e17b6bcfc02911486974a5d0bf712ff99ebbe9dbd652af86e58fc12733b857c15270dea5c54ef46472096b034bbdfa0f33304ae5b6dbea525117739c619c2708b5a39b2df44eeaaea770cd5232334c7b7ee915a81fd20ef5693e014f4639c3b8de1030784d397d498024f736c4d32f83340f299e775ab9af0487d92d9852edabfdf70caf4b33d2ae4f5adb957267fb762b17bce437682fca12622fe90d34d13560c1a772ec529026e25c884fa786ed9342920664b6c4ff35ded3ad81db7f1b1dad48468e6eeaeb37b4f183c843ea73984ede2b3034c9463c1636ec0a04b381be9b0e6bc3201c282c25b628aea45d71e247f0aae5394831b7e38e7831dc6af40d089319ab330e212145f29bbb1f256696ffc9e920dcc9ee397ea7d23590e96653331165b1a85d2ba2e00c8d037dcd3ca48842e17c84343140fdbf5ae812bd90c3e340db816f1a262a47825f8351ca65e03730e7e1749456f7dbf20cc66f9c658b2dd9c94e2223f34030c8d67069f48606af0a5ffbba77244d5ee45f73f1846b1b7ca2602fe864998f14f5a5b01463cbcdda6b2a9d670c5cee5e7e59c60eba26101f8945e4b08b9d882595f2eb07170d071c2078dccb6eed16e9d5224990e60a62f6deac36a74e7351924d5d185bca525cbd7c0b1c71777752affa516f1aed32eac2128a15745d1175c8e58e08fde214ef9161664e95dafd2d55eb57cbb73e5e6cf31286d17ab0edf92fc7325b2e9981b9575bd9aa4d3a6d77e1c24ff61d902d17a68ff16c7e2a1e75e0933df3a22bc1b084af3ac1a8c517ed78833bd38437e1205922088fee0f2989ab744c905264163c5ab049d65da76107e7244e0449e61b9a93edcbc94cb881c4f0879af76b2a684b21eb350693361377e4b169c093a16c3c21364a4c91c7beffb8c39d2cf8b2c5f70e3718065bdbf5e2b7fb919a06e7aa5cb3aa6ec632d28b9d0085e1e57143544afc433e04c6751643a5ce0358060f7b006bbb1f4e20763187eafeed3bcffd8df3397340087331a1b20c9eb3f7883e360714bfa53d5e2b2352ff19f4dab1081e6e5c36af674d39d9dab873a8090e2d12900d0db62349e9e4ad4e281981d2b8b34f8a467624915d1d27dd2dedf307781c6e30fd21640c82f56ff92cfd677558e2fecf398bbc91d33532d13ea68201203082011ca003020117a103020102a282010e0482010ae099eb8382ff54943379750bafef3b374e21ac178fecfd641db8fc3943ff98807aeeb18cbe9a3ddfa772d448657bfae35bde4c2c7b70085b2427ccf9cafe37f8f8bb21518eb7038311fd31a2f0bfbcc908cd18e64b0e245c3b49285913c0ec192e975f639ff0cf23b216886afcfab02e1f7f7fa8b3c3288d0a78e0541e1dcbff56290497a3f378bb183801a9780b81620ba6890ad10db3ff751c2d2d154d7e68cc32cc5b1d7bb1fbd989d3b7984cb5c8401cf19ceeff4aa19aa80a7b6f71d7275033a6242c78c86736077157aa5b0186b0c19b06314c6f99defd71d848208b07a7b08503828d5f610a0fba5764f426de8d4a138511105ea8b028f5d779fd849baaaf9ac404a60c52071a");
		var ntlmHash = new byte[]
		{
			0xB4, 0x06, 0xA0, 0x17, 0x72, 0xD0, 0xAD, 0x22,
			0x5D, 0x7B, 0x1C, 0x67, 0xDD, 0x81, 0x49, 0x6F,
		};
		KerberosKeyCredential cred = new KerberosKeyCredential("milchick", "LUMON.IND", EType.Rc4Hmac, ntlmHash);

		kerb.TestAsRep(repbuf, cred);
	}

	[TestMethod]
	public void EncryptRoundtripTest()
	{
		// This test uses the decrypted output from the Lumon_Rc4Hmac tests
		byte[] message = new byte[1];
		byte[] key = BinaryHelper.ParseHexString("b406a01772d0ad225d7b1c67dd81496f");
		byte[] buf = BinaryHelper.ParseHexString(
			"29d8a7e08b8b79e29246987f499b8968"
			+ "5ef460e9d8e67bef"
			+ "301aa011180f32303235303831323137343330395aa10502030d95f4");
		//byte[] checksum = BinaryHelper.ParseHexString("29d8a7e08b8b79e29246987f499b8968");

		Rc4Hmac encProfile = new Rc4Hmac();
		encProfile.Encrypt(key, KeyUsage.AsreqPaEncTimestamp, buf.Slice(0, 8 + 16), SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(buf.Slice(8 + 16))), default);

		var decrypted = encProfile.Decrypt(key, KeyUsage.AsreqPaEncTimestamp, buf);
	}
}
