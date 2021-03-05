using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Security.Spnego.Asn1.GSS_API
{

	public class InitialContextToken : Asn1Implicit<InitialContextToken_Unnamed_0>
	{
		public InitialContextToken()
			: base(new Asn1Tag(0x60))
		{

		}
	}
}
