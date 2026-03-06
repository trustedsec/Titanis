using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Certificates
{
	public abstract class SignatureAlgorithm
	{
		public abstract bool IsSupported { get; }
	}

	public sealed class OtherSignatureAlgorithm : SignatureAlgorithm
	{
		internal OtherSignatureAlgorithm() { }

		public sealed override bool IsSupported => false;
	}
}
