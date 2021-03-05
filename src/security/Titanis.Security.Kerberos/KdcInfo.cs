using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Information retrieved from a KDC.
	/// </summary>
	/// <seealso cref="KerberosClient.GetASInfo(string, string, System.Threading.CancellationToken)"/>
	public class KdcInfo
	{
		public KdcInfo(DateTime kdcTime, IList<KdcEncryptionTypeInfo> supportedEncryptionTypes)
		{
			KdcTime = kdcTime;
			SupportedEncryptionTypes = supportedEncryptionTypes;
		}

		public DateTime KdcTime { get; }
		public IList<KdcEncryptionTypeInfo> SupportedEncryptionTypes { get; }
	}
}
