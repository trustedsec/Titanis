using KerberosV5Spec2;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Ldap;

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
			int seqnbr,
			Ticket_Tagged1 ticket,
			SessionKey sessionKey
			)
		{
			this.SeqNbr = seqnbr;
			this.ticket = ticket;
			this.SessionKey = sessionKey;

			var names = ticket.sname.name_string;
			this.TargetSpn = ticket.sname.ToSecurityPrincipalName();
		}
		/// <remarks>
		/// Called from <see cref="KerberosClient.LoadTicketsFromKirbiFile(byte[])"/>.
		/// </remarks>
		internal TicketInfo(
			int seqnbr,
			Ticket_Tagged1 ticket,
			SessionKey sessionKey,
			KrbCredInfo credInfo
			)
			: this(seqnbr, ticket, sessionKey)
		{
			this.EndTime = credInfo.endtime?.Value;
			this.StartTime = credInfo.starttime?.Value;
			this.KdcOptions = (KdcOptions)(credInfo.flags?.ToUInt32() ?? 0);
			if (credInfo.pname != null)
			{
				this.ClientName = credInfo.pname.name_string[0].Value;
			}
			this.ClientRealm = credInfo.prealm?.Value;
			this.RenewTill = credInfo.renew_till?.Value;

			this.TargetSpn = credInfo.sname.ToSecurityPrincipalName();
			this.ServiceRealm = credInfo.srealm?.Value;

			// TODO: How should PADATA be encoded?
		}
		/// <remarks>
		/// Called from <see cref="KerberosClient.ProcessASRep"/> and <see cref="KerberosClient.ProcessTgsRep"/>.
		/// </remarks>
		internal TicketInfo(
			int seqnbr,
			Ticket_Tagged1 ticket,
			SessionKey sessionKey,
			EncKDCRepPart encPart,
			string clientName,
			string clientRealm,
			SessionKey? asrepKey,
			SessionKey? ticketKey
			)
			: this(seqnbr, ticket, sessionKey)
		{
			this.EndTime = encPart.endtime.Value;
			this.StartTime = encPart.starttime?.Value;
			this.KdcOptions = (KdcOptions)encPart.flags.ToUInt32();

			this.ClientName = clientName;
			this.ClientRealm = clientRealm;
			this.RenewTill = encPart.renew_till?.Value;

			this.TargetSpn = encPart.sname.ToSecurityPrincipalName();
			this.ServiceRealm = encPart.srealm.Value;

			this.Padata = encPart.padata;
			this.AsrepKey = asrepKey;
			this.TicketKey = ticketKey;

		}
		/// <remarks>
		/// Called from <see cref="KerberosClient.LoadTicketsFromCcacheFile(byte[], string?)"/>.
		/// </remarks>
		internal TicketInfo(string sourceFileName, int seqnbr, SessionKey key, CCacheCredential cred, KerberosClient krb)
		{
			this.SourceFileName = sourceFileName;
			this.SeqNbr = seqnbr;
			this.SessionKey = key;
			this.ticket = Asn1DerDecoder.DecodeTlv<Ticket>(cred.ticket.bytes).Value;

			this.ClientName = cred.client.components[0].str;
			this.ClientRealm = cred.client.realm.str;
			this.TargetSpn = SecurityPrincipalName.Create(cred.server.nameType, Array.ConvertAll(cred.server.components, r => r.str));
			this.ServiceRealm = cred.server.realm.str;
			this.KdcOptions = cred.ticketFlags;

			this.StartTime = FromCcacheTime(cred.startTime);
			this.EndTime = FromCcacheTime(cred.endTime);
			this.RenewTill = FromCcacheTime(cred.renewTill);

			var padataList = cred.authData?.Select(r => new PA_DATA((int)r.authType, r.authData.bytes))?.ToList();
			var padataExt = padataList?.FirstOrDefault(IsSuppPadata);

			if (padataExt != null)
			{
				Debug.Assert(padataList != null);
				padataList.Remove(padataExt);

				try
				{
					var suppList = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<PA_DATA>>(padataExt.padata_value.AsMemory(4)).Values;
					foreach (var suppItem in suppList)
					{
						switch ((SupplementalPadataType)suppItem.padata_type)
						{
							case SupplementalPadataType.AsrepKey:
								this.AsrepKey = krb.CreateSessionKeyFor(Asn1DerDecoder.DecodeTlv<EncryptionKey>(suppItem.padata_value));
								break;
							case SupplementalPadataType.TicketKey:
								this.TicketKey = krb.CreateSessionKeyFor(Asn1DerDecoder.DecodeTlv<EncryptionKey>(suppItem.padata_value));
								break;
							case SupplementalPadataType.TicketComment:
								this.Comment = Encoding.UTF8.GetString(suppItem.padata_value);
								break;
						}
					}
				}
				catch (Exception ex)
				{
					// TODO: Log?  
				}
			}

			this.Padata = padataList?.ToArray();
		}

		private List<CCacheCredential>? _configEntries;
		internal CCacheCredential[]? GetConfigEntries() => this._configEntries?.ToArray();
		internal void AddConfigEntry(CCacheCredential cred)
		{
			(this._configEntries ??= new List<CCacheCredential>()).Add(cred);
		}

		public sealed override string ToString() => $"{this.ClientName}@{this.ClientRealm} => {this.TargetSpn}";

		private static bool IsSuppPadata(PA_DATA padata)
		{
			return
				(padata != null)
				&& ((PadataType)padata.padata_type == PadataType.PasswordSalt)
				&& (padata.padata_value?.Length > 4)
				&& (BinaryPrimitives.ReadUInt32LittleEndian(padata.padata_value) == SupplementalPadata.Signature)
				;
		}

		public TicketInfo(int seqnbr, string? userName, string? userRealm, string? ticketRealm, SecurityPrincipalName spn, string? serviceRealm, KdcOptions kdcOptions, DateTime? endTime, DateTime? startTime, DateTime? renewTill, SessionKey sessionKey, byte[] encodedTicket)
		{
			this.SeqNbr = seqnbr;
			this.ClientName = userName;
			this.ClientRealm = userRealm;
			this.TargetSpn = spn;
			this.ServiceRealm = serviceRealm;
			this.KdcOptions = kdcOptions;
			this.EndTime = endTime;
			this.StartTime = startTime;
			this.RenewTill = renewTill;
			this.SessionKey = sessionKey;

			this.ticket = Asn1DerDecoder.DecodeTlv<Ticket>(encodedTicket).Value;
		}

		private static DateTime FromCcacheTime(int time)
		{
			return TicketParameters.DefaultEndTime + TimeSpan.FromSeconds(time);
		}

		public string SourceFileName { get; }
		public int SeqNbr { get; }
		internal readonly Ticket_Tagged1 ticket;

		public string? Comment { get; set; }

		// keys returned via KERB-KEY-LIST-REP ([MS-KILE] § 2.2.12), if any
		public (int EType, byte[] Key)[]? KeyListKeys { get; internal set; }

		[DisplayName("Client name")]
		public string? ClientName { get; }

		[DisplayName("Client realm")]
		public string? ClientRealm { get; }

		/// <summary>
		/// Gets the realm of the target the ticket is valid in.
		/// </summary>
		[DisplayName("Ticket realm")]
		public string TicketRealm => this.ticket.realm.Value;

		/// <summary>
		/// Gets the target service.
		/// </summary>
		public SecurityPrincipalName TargetSpn { get; private set; }
		/// <summary>
		/// Gets the name of the target service.
		/// </summary>
		public string? ServiceClass => (this.TargetSpn as ServicePrincipalName)?.ServiceClass;
		/// <summary>
		/// Gets a value indicating whether this is a ticket-granting ticket.
		/// </summary>
		[Browsable(false)]
		public bool IsTgt => string.Equals(this.ServiceClass, ServiceClassNames.Krbtgt, StringComparison.OrdinalIgnoreCase);
		/// <summary>
		/// Gets the name of the target host (or realm for TGT).
		/// </summary>
		public string? ServiceInstance => (this.TargetSpn as ServicePrincipalName)?.ServiceInstance;

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

		[Browsable(false)]
		public PA_DATA[]? Padata { get; }

		[Browsable(false)]
		public SessionKey? AsrepKey { get; private set; }
		public string? AsrepKeyText => this.AsrepKey?.KeyBytes?.ToHexString();

		[Browsable(false)]
		public SessionKey? TicketKey { get; set; }
		public string? TicketKeyText => this.TicketKey?.KeyBytes?.ToHexString();

		internal PA_DATA? TryGetPadata(PadataType type)
		{
			return this.Padata?.FirstOrDefault(r => (PadataType)r.padata_type == type);
		}

		public SupportedEncryptionTypes? SupportedEncryptionTypes
		{
			get
			{
				ReadOnlySpan<byte> data = this.TryGetPadata(PadataType.SupportedEncTypes)?.padata_value;
				return PreauthContext.ExtractSupportedEncTypes(data);
			}
		}


		/// <summary>
		/// Gets the session key.
		/// </summary>
		[Browsable(false)]
		public SessionKey SessionKey { get; }
		[DisplayName("Session etype")]
		public EType SessionEType => this.SessionKey?.EType ?? 0;
		[DisplayName("Session key")]
		public string SessionKeyText => this.SessionKey?.KeyBytes?.ToHexString() ?? string.Empty;
		[DisplayName("Ticket etype")]
		public EType TicketEType => (EType)this.ticket.enc_part.etype;

		public int? TgsrepHashcatMethod => this.TicketEType switch
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
			var krb = new KerberosClient();
			EType etype = this.TicketEType;
			var encProf = krb.GetEncProfile(etype);

			switch (etype)
			{
				case EType.Rc4Hmac or EType.Rc4HmacExp:
					sb.Append($"$krb5tgs${(int)etype}$*{this.TargetSpn.ToString().Replace(':', '~')}*${bytes.AsSpan(0, 16).ToHexString()}${bytes.AsSpan(16).ToHexString()}");
					break;
				default:
					{
						var cbChecksum = encProf.ChecksumSizeBytes;
						sb.Append($"$krb5tgs${(int)etype}${this.ClientName}${this.TicketRealm}$*{this.TargetSpn.ToString().Replace(':', '~')}*${bytes.AsSpan(bytes.Length - cbChecksum).ToHexString()}${bytes.AsSpan(0, bytes.Length - cbChecksum).ToHexString()}");
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


		#region Authorization data
		private TicketAuthorizationData? _cachedAuthData;
		private TicketAuthorizationData? CachedAuthData => (this._cachedAuthData ??= this.TryDecryptAuthData());

		private TicketAuthorizationData? TryDecryptAuthData()
		{
			return (this.TicketKey != null) ? this.DecryptAuthorizationData(null, null) : null;
		}

		public SidWithAttributes[]? SecurityGroups => this.CachedAuthData?.GetSecurityGroups()?.ToArray();

		public string? NtlmHashText => this.CachedAuthData?.NtlmHash?.ToHexString();

		public TicketAuthorizationData DecryptAuthorizationData(SessionKey? ticketKey, SessionKey? asrepKey)
		{
			EnsureAuthDataDecrypted(ticketKey, asrepKey);

			return this._cachedAuthData;
		}

		private void EnsureAuthDataDecrypted(SessionKey? ticketKey, SessionKey? asrepKey)
		{
			if (this._cachedAuthData is null)
			{
				ticketKey ??= this.TicketKey;
				asrepKey ??= this.AsrepKey;
				ArgumentNullException.ThrowIfNull(ticketKey);

				var encTicketPart = Asn1DerDecoder.DecodeTlv<EncTicketPart>(ticketKey.Decrypt(KeyUsage.Asrep_Tgsrep_Ticket, this.ticket.enc_part.cipher.ToArray()));

				var authz = new TicketAuthorizationData();
				authz.Process(encTicketPart?.Value?.authorization_data, ticketKey, asrepKey);
				this._cachedAuthData = authz;

				this.TicketKey = ticketKey;
				this.AsrepKey = asrepKey;
			}
		}

		internal void ChangeSpn(ServicePrincipalName targetSpn)
		{
			this.TargetSpn = targetSpn;
			this.ticket.sname = Structs.PrincipalName(targetSpn);
		}
		#endregion
	}
}
