using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Certificates
{
	public static class ExtendedKeyUsages
	{
		/// <summary>
		/// Client Authentication (1.3.6.1.5.5.7.3.2)
		/// </summary>
		public static readonly Oid ClientAuthentication = new Oid("1.3.6.1.5.5.7.3.2");
	}
}
