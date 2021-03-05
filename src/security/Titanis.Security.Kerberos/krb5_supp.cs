using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Titanis.Asn1;

namespace Titanis.Security.Kerberos.Asn1.KerberosV5Spec2
{
	partial class TicketWrapper : Asn1Explicit<Ticket_Ticket>
	{
		public TicketWrapper() : base(new Asn1Tag(0x61)) { }
	}

	partial class PrincipalName
	{
		internal ServicePrincipalName ToServicePrincipalName()
		{
			var name = this;

			Debug.Assert((NameType)name.name_type == NameType.ServiceInstance);
			Debug.Assert(name.name_string.Length == 2);

			return new ServicePrincipalName(
				name.name_string[0].value,
				name.name_string[1].value
				);
		}

	}
	public partial class KRB_ERROR_Err
	{
		internal Exception GetException()
		{
			return new KerberosException((KerberosErrorCode)this.error_code);
		}
	}

	class AP_REQ : Asn1Explicit<AP_REQ_Unnamed_3>
	{
		public AP_REQ()
			: base(new Asn1Tag((int)KrbMessageType.Apreq, Asn1TagFlags.Application | Asn1TagFlags.Constructed))
		{

		}
	}

	class AP_REP : Asn1Explicit<AP_REP_Unnamed_5>
	{
		public AP_REP()
			: base(new Asn1Tag((int)KrbMessageType.Aprep, Asn1TagFlags.Application | Asn1TagFlags.Constructed))
		{

		}
	}

	class EncPart_APRep : Asn1Explicit<EncAPRepPart_Unnamed_6>
	{
		public EncPart_APRep()
			: base(new Asn1Tag(27, Asn1TagFlags.Application | Asn1TagFlags.Constructed))
		{

		}
	}

	class EncKDCRepPart_Outer : Asn1Explicit<EncKDCRepPart>
	{
		public EncKDCRepPart_Outer()
			// TODO: Replace with symbolic tag
			: base(new Asn1Tag(0x19, Asn1TagFlags.Application | Asn1TagFlags.Constructed))
		{

		}
	}

	class KrbCred : Asn1Explicit<KRB_CRED_Unnamed_10>
	{
		public KrbCred()
		{
			this.Tag = new Asn1Tag((int)KrbMessageType.Cred, Asn1TagFlags.Constructed | Asn1TagFlags.Application);
		}
	}

	class EncKrbCredPart_Outer : Asn1Explicit<EncKrbCredPart_Unnamed_11>
	{
		public EncKrbCredPart_Outer()
			// TODO: Replace with symbolic tag
			: base(new Asn1Tag(0x1D, Asn1TagFlags.Application | Asn1TagFlags.Constructed))
		{

		}
	}

	class Authenticator_Outer : Asn1Explicit<Authenticator_Unnamed_4>
	{
		public Authenticator_Outer()
			// TODO: Replace with symbolic tag
			: base(new Asn1Tag(2, Asn1TagFlags.Application | Asn1TagFlags.Constructed))
		{

		}
	}

	class Ticket_EncPart : Asn1Explicit<EncTicketPart_Unnamed_1>
	{
		public Ticket_EncPart()
			: base(new Asn1Tag(0x63))
		{

		}
	}

}