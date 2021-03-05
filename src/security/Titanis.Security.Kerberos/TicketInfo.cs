using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Describes a Kerberos ticket.
	/// </summary>
	/// <remarks>
	/// This encapsulates the ticket itself along with the additional information included
	/// in the response.
	/// </remarks>
	public class TicketInfo
	{
		private TicketInfo(
			Asn1.KerberosV5Spec2.Ticket_Ticket ticket,
			SessionKey sessionKey
			)
		{
			this.ticket = ticket;
			this.SessionKey = sessionKey;

			var names = ticket.sname.name_string;
			this.Spn = ticket.sname.ToServicePrincipalName();
		}
		internal TicketInfo(
			Asn1.KerberosV5Spec2.Ticket_Ticket ticket,
			SessionKey sessionKey,
			KrbCredInfo credInfo
			)
			: this(ticket, sessionKey)
		{
			this.EndTime = credInfo.endtime?.value;
			this.StartTime = credInfo.starttime?.value;
			this.KdcOptions = (KdcOptions)(credInfo.flags?.ToUInt32() ?? 0);
			if (credInfo.pname != null)
			{
				this.UserName = credInfo.pname.name_string[0].value;
			}
			this.UserRealm = credInfo.prealm?.value;
			this.RenewTill = credInfo.renew_till?.value;

			this.Spn = credInfo.sname.ToServicePrincipalName();
			this.ServiceRealm = credInfo.srealm?.value;
		}
		internal TicketInfo(
			Asn1.KerberosV5Spec2.Ticket_Ticket ticket,
			SessionKey sessionKey,
			EncKDCRepPart encPart,
			string userName,
			string userRealm)
			: this(ticket, sessionKey)
		{
			this.EndTime = encPart.endtime.value;
			this.StartTime = encPart.starttime?.value;
			this.KdcOptions = (KdcOptions)encPart.flags.ToUInt32();

			this.UserName = userName;
			this.UserRealm = userRealm;
			this.RenewTill = encPart.renew_till?.value;

			this.Spn = encPart.sname.ToServicePrincipalName();
			this.ServiceRealm = encPart.srealm.value;
		}

		internal TicketInfo(SessionKey key, CCacheCredential cred)
		{
			this.SessionKey = key;
			this.ticket = Asn1DerDecoder.Decode<CCacheTicketWrapper>(cred.ticket.bytes).Value;

			this.UserName = cred.client.components[0].str;
			this.UserRealm = cred.client.realm.str;
			this.Spn = new ServicePrincipalName(cred.server.components[0].str, cred.server.components[1].str);
			this.ServiceRealm = cred.server.realm.str;
			this.KdcOptions = cred.ticketFlags;

			this.StartTime = FromCcacheTime(cred.startTime);
			this.EndTime = FromCcacheTime(cred.endTime);
			this.RenewTill = FromCcacheTime(cred.renewTill);
		}

		public TicketInfo(string? userName, string? userRealm, string? ticketRealm, ServicePrincipalName spn, string? serviceRealm, KdcOptions kdcOptions, DateTime? endTime, DateTime? startTime, DateTime? renewTill, SessionKey sessionKey, byte[] encodedTicket)
		{
			this.UserName = userName;
			this.UserRealm = userRealm;
			this.Spn = spn;
			this.ServiceRealm = serviceRealm;
			this.KdcOptions = kdcOptions;
			this.EndTime = endTime;
			this.StartTime = startTime;
			this.RenewTill = renewTill;
			this.SessionKey = sessionKey;

			this.ticket = Asn1DerDecoder.Decode<TicketWrapper>(encodedTicket).Value;
		}

		private static DateTime FromCcacheTime(int time)
		{
			return TicketParameters.DefaultEndTime + TimeSpan.FromSeconds(time);
		}

		internal readonly Asn1.KerberosV5Spec2.Ticket_Ticket ticket;

		[DisplayName("User name")]
		public string? UserName { get; }
		[DisplayName("User realm")]
		public string? UserRealm { get; }

		/// <summary>
		/// Gets the realm of the target the ticket is valid in.
		/// </summary>
		[DisplayName("Ticket realm")]
		public string? TicketRealm => this.ticket.realm.value;

		/// <summary>
		/// Gets the target service.
		/// </summary>
		public ServicePrincipalName Spn { get; }
		/// <summary>
		/// Gets the name of the target service.
		/// </summary>
		public string ServiceClass => this.Spn.ServiceClass;
		/// <summary>
		/// Gets the name of the target host (or realm for TGT).
		/// </summary>
		public string Host => this.Spn.ServiceInstance;

		[DisplayName("Service realm")]
		public string? ServiceRealm { get; }

		[DisplayName("Options")]
		public KdcOptions KdcOptions { get; }


		[DisplayName("End time")]
		public DateTime? EndTime { get; }
		[DisplayName("Start time")]
		public DateTime? StartTime { get; }
		[DisplayName("Renew till")]
		public DateTime? RenewTill { get; }

		/// <summary>
		/// Gets the session key.
		/// </summary>
		[Browsable(false)]
		public SessionKey SessionKey { get; }
		[DisplayName("Enc. type")]
		public EType EType => this.SessionKey.EType;
		[DisplayName("Session key")]
		public string SessionKeyText => this.SessionKey.KeyBytes.ToHexString();
		[DisplayName("Ticket enc. type")]
		public EType TicketEncryptionType => (EType)this.ticket.enc_part.etype;

		public int? TgsrepHashcatMethod => this.TicketEncryptionType switch
		{
			EType.Rc4Hmac => 13100,
			EType.Aes128CtsHmacSha1_96 => 19600,
			EType.Aes256CtsHmacSha1_96 => 19700,
			_ => null,
		};

		/*
		Preauth = (17)19800
		Preauth = (18)19900
		 */

		private string? _ticketHash;
		public string TicketHash => (this._ticketHash ??= this.GetTicketHash());
		public string GetTicketHash()
		{
			var bytes = this.ticket.enc_part.cipher;
			StringBuilder sb = new StringBuilder();
			var krb = new KerberosClient(null);
			EType etype = this.TicketEncryptionType;
			var encProf = krb.GetEncProfile(etype);

			switch (etype)
			{
				case EType.Rc4Hmac or EType.Rc4HmacExp:
					sb.Append($"$krb5tgs${(int)etype}$*{this.Spn.ToString().Replace(':', '~')}*${bytes.AsSpan(0, 16).ToHexString()}${bytes.AsSpan(16).ToHexString()}");
					break;
				default:
					{
						var cbChecksum = encProf.ChecksumSizeBytes;
						sb.Append($"$krb5tgs${(int)etype}${this.UserName}${this.TicketRealm}$*{this.Spn.ToString().Replace(':', '~')}*${bytes.AsSpan(bytes.Length - cbChecksum).ToHexString()}${bytes.AsSpan(0, bytes.Length - cbChecksum).ToHexString()}");
					}
					break;
			}
			return sb.ToString();
		}


		public bool IsCurrent
		{
			get
			{
				bool isCurrent =
					(this.StartTime <= DateTime.UtcNow)
					&& (this.EndTime >= DateTime.UtcNow)
					;
				return isCurrent;
			}
		}

		internal SessionKey GenerateSessionKey()
		{
			var encProfile = this.SessionKey.EncryptionProfile;
			return encProfile.GenerateSubkey();
		}
	}
}
