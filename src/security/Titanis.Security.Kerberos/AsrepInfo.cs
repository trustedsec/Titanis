using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{
	public class AsrepInfo
	{
		private readonly EncKDCRepPart kdcrep;

		internal AsrepInfo(EncKDCRepPart kdcrep, SessionKey asrepKey)
		{
			this.kdcrep = kdcrep;
			AsrepKey = asrepKey;
		}

		public SessionKey AsrepKey { get; }
	}
}
