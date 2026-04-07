using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap;

namespace Titanis.Security.Kerberos
{
	public class KdcRepInfo
	{
		private readonly TicketRequestContext context;
		private readonly KDC_REP asrep;
		private EncKDCRepPart encPart;

		internal KdcRepInfo(TicketRequestContext context, KDC_REP kdcrep, EncKDCRepPart encPart, SessionKey? asrepKey, SessionKey sessionKey)
		{
			this.AsrepKey = asrepKey;
			this.context = context;
			this.asrep = kdcrep;
			this.encPart = encPart;
			this.SessionKey = sessionKey;
		}

		public SessionKey? AsrepKey { get; }

		public string? Salt => context.preauth.passwordSalt.ToHexString();
		public string TicketRealm => this.asrep.ticket.realm.Value;

		public SecurityPrincipalName ClientName => this.asrep.cname.ToSecurityPrincipalName();
		public string ClientRealm => this.asrep.crealm.Value;

		public SecurityPrincipalName Spn => this.asrep.ticket.sname.ToSecurityPrincipalName();
		public string ServiceRealm => this.encPart.srealm.Value;
		public KdcOptions TicketFlags => (KdcOptions)this.encPart.flags.ToUInt32();
		public SessionKey SessionKey { get; }

		public SupportedEncryptionTypes? SupportedETypes => context.preauth.SupportedEncryptionTypes;
	}
}
