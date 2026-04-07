using System;
using System.Linq;
using Titanis.Security.Kerberos;

namespace KerberosV5Spec2
{
	[Serializable]
	internal class KerberosPadataException : KerberosException
	{
		public KerberosPadataException(KerberosErrorCode errorCode, PA_DATA[] supportedPadataTypes)
			: base(errorCode, null, "Supported PA_DATA types: " + string.Join(", ", supportedPadataTypes.Select(r => (PadataType)r.padata_type)))
		{
			SupportedPadataTypes = supportedPadataTypes;
		}

		public PA_DATA[] SupportedPadataTypes { get; }
	}
}