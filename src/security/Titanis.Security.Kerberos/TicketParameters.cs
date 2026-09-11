using System;
using System.Security.Cryptography.X509Certificates;

namespace Titanis.Security.Kerberos
{
	public class TicketParameters
	{
		public TicketParameters()
		{
			this.CorrelationId = Guid.NewGuid();
		}

		public Guid CorrelationId { get; set; }
		public KdcOptions Options { get; set; }
		public DateTime? StartTime { get; set; }
		// [RFC4120] § 5.4.1
		public DateTime? EndTime { get; set; }
		public static DateTime DefaultEndTime => new DateTime(1970, 1, 1, 0, 0, 0);
		public DateTime? RenewTill { get; set; }
		public UserPrincipalName? S4UserName { get; set; }
		public X509Certificate? S4UserCertificate { get; set; }
		public bool IndicatesS4User => this.S4UserName is not null || this.S4UserCertificate is not null;
		public SecurityPrincipalName? S4ProxyService { get; set; }

		// KERB-KEY-LIST-REQ support ([MS-KILE] § 2.2.11)
		public EType[]? KeyListEtypes { get; set; }

		private TicketInfo? _additionalTicket;
		public TicketInfo? AdditionalTicket
		{
			get => _additionalTicket;
			set
			{
				_additionalTicket = value;
				this.addlTicketStruc = value?.ticket;
			}
		}

		public TicketInfo? ArmorTicket { get; set; }

		internal KerberosV5Spec2.Ticket_Tagged1? addlTicketStruc;

		public string? TicketComment { get; internal set; }
		public PacOptions? PacRequestOptions { get; set; }
		public byte[]? AuthorizationData { get; set; }
	}
}
