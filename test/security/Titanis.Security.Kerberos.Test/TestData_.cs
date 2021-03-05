using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Titanis;
using Titanis.Asn1.Serialization;
using Titanis.Crypto;

namespace Titanis.Security.Kerberos
{
	static class TestData_
	{
		internal const string Realm = "testdom.local";

		internal static KerberosCredential Cred_NoPreauth = new KerberosPasswordCredential(
			"NoPreauthTestUser",
			Realm,
			"password");
		internal static KerberosCredential Cred_TestUser = new KerberosPasswordCredential(
			"TestUser",
			Realm,
			"password");
		internal static SimpleKdcLocator locator = new SimpleKdcLocator(new IPEndPoint(IPAddress.Parse("192.168.66.15"), 88));

		internal static KerberosKeyCredential Cred_Krbtgt = new KerberosKeyCredential(
			"krbtgt",
			Realm,
			EType.Aes256CtsHmacSha1_96,
			BinaryHelper.ParseHexString("2646be878329d7a49f075333606d56aa4cd563403770e8ab6ba80523c13860e2".ToCharArray())
			);
		internal static KerberosKeyCredential Cred_DC1 = new KerberosKeyCredential(
			"DC1$",
			Realm,
			EType.Aes256CtsHmacSha1_96,
			BinaryHelper.ParseHexString("4d23124a6add6efe0778b7350077a6b62757c5197c317c1df7a96377f28f3e0f".ToCharArray())
			);
		//internal static KerberosPasswordCredential Cred_Workstation = new KerberosPasswordCredential(
		//	"DESKTOP-BQQ7ARR",
		//	Realm,
		//	";MvvNBz=bx/8C=(MgaSQn1ohBx$KDlF4R:&%A:.,x\"?muOVz7Z$]O*_iG#lM3>HYmJETFXA@%fUP=_$L[_H5`@ylOcSqF#;.Y Lv<@2)e>mb?H6+Gjtc#r)?"
		//	);
		internal static KerberosCredential Cred_Workstation = new KerberosKeyCredential(
			"DESKTOP-BQQ7ARR",
			Realm,
			EType.Aes256CtsHmacSha1_96,
			BinaryHelper.ParseHexString("4d479f8fc55a8786c8af81a4604d808d5873697788394e60f27b3a234d4dcdc1".ToCharArray())
			);
	}
}
