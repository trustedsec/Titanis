using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security.Kerberos
{
	public class AuthzData
	{
		public void Process(Asn1.KerberosV5Spec2.Unnamed_0[] addata)
		{
			if (addata != null)
			{
				foreach (var aditem in addata)
				{
					this.Process(aditem);
				}
			}
		}

		private void Process(Asn1.KerberosV5Spec2.Unnamed_0 aditem)
		{
			if (aditem is null)
				throw new ArgumentNullException(nameof(aditem));

			switch ((AuthzDataType)aditem.ad_type)
			{
				case AuthzDataType.IfRelevant:
					this.ProcessIfRelevant(aditem.ad_data);
					break;
				default:
					break;
			}
		}

		private void ProcessIfRelevant(byte[] ad_data)
		{
			throw new NotImplementedException();
		}
	}
}
