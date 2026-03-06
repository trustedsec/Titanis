using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos.Test;
[TestClass]
public class SaslUnsealTest
{
	[TestMethod]
	public void TestUnseal()
	{
		var keyBytes = BinaryHelper.ParseHexString("c2f8a57293b4b772800eae4531523f93e8967485ccdddcd11ff8170e3d18b552");
		var seqnbr = 198082504U;
		var message = BinaryHelper.ParseHexString("050407ff0000001c000000000bce7fc8e96c7a01961d079d790c738653ad11ed2e836513547affadc5721a926f2edce942c9d21fca8176bbf54e55b49ff8ad04a82a0dcbcd77ce4ac4bb3ea465cd4c8d735a3e5c97c8e57250c20883ba984b95afeb5b480af62dd2bd02714f250b4c047c7752f71f4f6688d3ed0af5da3d227c6fc2bf352e064d8942a7bd8197233067fdd761620fbb010045d97c1f31febfaa87a4257283af3254b559e5382529858db69daa4571495ffc81a26ff86d3e2c11ee80");

		EncProfile_Aes256CtsHmacSha1_96 encProf = new EncProfile_Aes256CtsHmacSha1_96();
		var key = encProf.CreateSessionKey(keyBytes);

		encProf.GetWrapBufferSizes(WrapOptions.Confidentiality, out var headerSize, out var trailerSize);
		var cbToken = headerSize + trailerSize;
		key.UnsealMessage(KeyUsage.AcceptorSeal, seqnbr, WrapFlags.Sealed | WrapFlags.AcceptorSubkey | WrapFlags.SentByAcceptor, new MessageSealParams(message.AsSpan(0, cbToken), SecBufferList.Create(SecBuffer.PrivacyWithIntegrity(message.AsSpan(cbToken))), default));
	}
}
