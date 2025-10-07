using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Metadata;
using Titanis.Asn1.Serialization;
using Titanis.Crypto;
using Titanis.IO;
using Titanis.Net;
using Titanis.Security.Kerberos.Asn1.KerberosV5Spec2;
using static Titanis.Security.Kerberos.KerberosClient;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	public class TicketParameters
	{
		public KdcOptions Options { get; set; }
		public DateTime? StartTime { get; set; }
		// [RFC4120] § 5.4.1
		public DateTime? EndTime { get; set; }
		public static DateTime DefaultEndTime => new DateTime(1970, 1, 1, 0, 0, 0);
		public DateTime? RenewTill { get; set; }
	}
	/// <summary>
	/// Implements a Kerberos client.
	/// </summary>
	public partial class KerberosClient : IDisposable
	{
		public const string Krb5CacheVariableName = "KRB5CCNAME";

		/// <summary>
		/// Initializes a new <see cref="KerberosClient"/>.
		/// </summary>
		/// <param name="locator">KDC locator</param>
		/// <param name="socketService"><see cref="ISocketService"/> implementation for network communication</param>
		/// <remarks>
		/// If <paramref name="locator"/> is <see langword="null"/>, this client instance can only provide tickets from the cache.
		/// </remarks>
		public KerberosClient(
			IKdcLocator? locator,
			ISocketService? socketService = null,
			IKerberosCallback? callback = null
			)
		{
			this._locator = locator;
			this._callback = callback;
			if (locator != null)
			{
				// Only required for KDC locator
				// TODO: Log
				socketService ??= new PlatformSocketService(null, null);
				this._socketService = socketService;
			}
		}

		/// <summary>
		/// TCP port used by Kerberos servers.
		/// </summary>
		public const int KdcTcpPort = 88;

		private readonly IKdcLocator? _locator;
		private readonly IKerberosCallback? _callback;
		private readonly ISocketService? _socketService;

		private TicketCache? _ticketCache;
		public TicketCache TicketCache
		{
			get => this._ticketCache ??= new TicketCache();
			set
			{
				ArgumentNullException.ThrowIfNull(value);
				this._ticketCache = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the workstation.
		/// </summary>
		/// <remarks>
		/// If provided, this is included with <c>ASREQ</c> messages.
		/// </remarks>
		public string? Workstation { get; set; }

		#region Network I/O
		// TODO: What is the max buffer size?

		private IKdcLocator VerifyKdcLocator()
		{
			var locator = this._locator;
			if (locator == null)
				throw new NotImplementedException("This Kerberos client is not configured with a KDC locator and cannot request new tickets.");

			return locator;
		}

		private async Task<Kdc_rep_choice> TransceiveKdcAsync(
			string realm,
			LocateKdcOptions options,
			Memory<byte> memory,
			CancellationToken cancellationToken)
		{
			Debug.Assert(!string.IsNullOrEmpty(realm));

			EndPoint kdcEP = this.VerifyKdcLocator().LocateKdc(realm, options);
			if (kdcEP == null)
				throw new NotSupportedException(string.Format(Messages.Krb5_NoKdc, realm));

			using (var s = this._socketService!.CreateTcpSocket(kdcEP.AddressFamilyOrDefault(AddressFamily.InterNetwork)))
			{
				await s.ConnectAsync(kdcEP, cancellationToken).ConfigureAwait(false);

				var stream = s.GetStream(false);
				await stream.WriteAsync(memory, cancellationToken).ConfigureAwait(false);

				byte[]? buf = null;
				const int BufferSize = 64 * 1024;
				try
				{
					buf = ArrayPool<byte>.Shared.Rent(BufferSize);

					int cbTotalRecv = await stream.ReadAtLeastAsync(buf, 0, buf.Length, 4, cancellationToken).ConfigureAwait(false);

					int cbPdu = BinaryPrimitives.ReadInt32BigEndian(buf.SliceReadOnly(0, 4));
					await stream.ReadAllAsync(buf, cbTotalRecv, (cbPdu + 4 - cbTotalRecv), cancellationToken).ConfigureAwait(false);

					s.Shutdown(SocketShutdown.Both);

					var rep = ParseReplyPdu(buf);
					return rep;
				}
				finally
				{
					if (buf != null)
						ArrayPool<byte>.Shared.Return(buf);
				}
			}
		}

		#endregion

		private List<EncProfile> _encProfiles = new List<EncProfile>()
		{
			Singleton.SingleInstance<EncProfile_Aes256CtsHmacSha1_96>(),
			Singleton.SingleInstance<EncProfile_Aes128CtsHmacSha1_96>(),
			Singleton.SingleInstance<Rc4Hmac>(),
			Singleton.SingleInstance<Rc4HmacExp>(),
		};

		/// <summary>
		/// Gets an array of encryption types supported by a credential.
		/// </summary>
		/// <param name="credential">Kerberos credential</param>
		/// <returns>An array of <see cref="int"/> corresponding to <see cref="EType"/> values</returns>
		/// <remarks>
		/// The list is returned as an array of <see cref="int"/> rather than <see cref="EType"/>
		/// since it is packaged into Kerberos structure requiring an array of <see cref="int"/>.
		/// </remarks>
		private int[] GetETypes(KerberosCredential credential)
		{
			List<int> etypes = new List<int>(this._encProfiles.Count);
			for (int i = 0; i < _encProfiles.Count; i++)
			{
				var prof = _encProfiles[i];
				if (credential.SupportsProfile(prof))
					etypes.Add((int)_encProfiles[i].EType);
			}
			return etypes.ToArray();
		}
		private int[] GetAllETypes()
		{
			return this._encProfiles.ConvertAll(r => (int)r.EType).ToArray();
		}
		/// <summary>
		/// Attempts to get an <see cref="EncProfile"/> from the list of profiles.
		/// </summary>
		/// <param name="etype"><see cref="EType"/> value specifying encryption type</param>
		/// <returns>An instance of <see cref="EncProfile"/>, if found; otherwise, <see langword="null"/>.</returns>
		public EncProfile? TryGetEncProfile(EType etype)
		{
			foreach (var encProfile in _encProfiles)
			{
				if (encProfile.EType == etype)
					return encProfile;
			}
			return null;
		}
		/// <summary>
		/// Gets an <see cref="EncProfile"/> from the list of profiles.
		/// </summary>
		/// <exception cref="NotSupportedException">No profile exists for <paramref name="etype"/>.</exception>
		public EncProfile GetEncProfile(EType etype)
		{
			var encProfile = this.TryGetEncProfile(etype);
			if (encProfile == null)
				throw new NotSupportedException("The requested encryption type is not supported.");
			return encProfile;
		}

		/// <summary>
		/// Requests a ticket-granting ticket for the specified realm.
		/// </summary>
		/// <param name="targetRealm">Realm for TGT</param>
		/// <param name="credential">User credential</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>The retrieved TGT.</returns>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="SecurityException"></exception>
		/// <remarks>
		/// This method bypasses the cache.  This means that this call will always result in a request
		/// sent to the KDC.  Any returned ticket is not stored in the cache.
		/// </remarks>
		public Task<TicketInfo> RequestTgt(
			string targetRealm,
			KerberosCredential credential,
			CancellationToken cancellationToken)
			=> this.RequestTgt(targetRealm, credential, null, GetDefaultTgtOptions(), null, cancellationToken);
		/// <summary>
		/// Requests a ticket-granting ticket for the specified realm.
		/// </summary>
		/// <param name="targetRealm">Realm for TGT</param>
		/// <param name="credential">User credential</param>
		/// <param name="targetSpn">SPN to request ticket for</param>
		/// <param name="ticketParameters">Ticket parameters</param>
		/// <param name="encTypes">Encryption types to to support in response</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>The retrieved TGT.</returns>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="SecurityException"></exception>
		/// <remarks>
		/// This method bypasses the cache.  This means that this call will always result in a request
		/// sent to the KDC.  Any returned ticket is not stored in the cache.
		/// </remarks>
		public async Task<TicketInfo> RequestTgt(
			string targetRealm,
			KerberosCredential credential,
			SecurityPrincipalName? targetSpn,
			TicketParameters? ticketParameters,
			EType[]? encTypes,
			CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(targetRealm);
			ArgumentNullException.ThrowIfNull(credential);
			targetSpn ??= new ServicePrincipalName(ServiceClassNames.Krbtgt, targetRealm);

			if (ticketParameters == null)
				ticketParameters = GetDefaultTgtOptions();

			if (!ticketParameters.EndTime.HasValue)
				ticketParameters.EndTime = GetDefaultEndTime();

			int[] encTypeValues = (encTypes != null)
				? Array.ConvertAll(encTypes, r => (int)r)
				: GetETypes(credential);
			if (encTypes == null)
				encTypeValues = GetETypes(credential);

			PreauthInfo paInfo = new PreauthInfo(this, credential, this._callback)
			{
				_requestPac = true
			};
			TicketRequestContext context = new TicketRequestContext(credential)
			{
				preauth = paInfo
			};

			var asreq = this.CreateASReq(
				context,
				paInfo,
				Structs.KdcReqBody(
					ticketParameters,
					Structs.PrincipalName(PrincipalNameType.Principal, credential.UserName),
					credential.Realm,
					Structs.PrincipalName(targetSpn),
					context.nonce,
					encTypeValues,
					this.MakeHostAddress()
				));

			this._callback?.OnRequestingTgt(targetRealm, credential, asreq.asreq.req_body.nonce);

			Memory<byte> pduBytes = BuildPdu(asreq);
			var sendTime = DateTime.UtcNow;
			var rep = await this.TransceiveKdcAsync(targetRealm, LocateKdcOptions.Home, pduBytes, cancellationToken).ConfigureAwait(false);
			var recvTime = DateTime.UtcNow;
			// TODO: Add a max loop count to avoid getting stuck.
			while (rep.err != null)
			{
				if ((KerberosErrorCode)rep.err.error_code is KerberosErrorCode.KDC_ERR_PREAUTH_REQUIRED)
				{
					paInfo.Skew = new KerberosTime(rep.err.stime, rep.err.susec).AsDateTime() - sendTime;
					var paList = Asn1DerDecoder.Decode<Asn1SequenceOf<PA_DATA>>(rep.err.e_data).Values;
					this._callback?.OnReceiveAsrepPadataList(paList);
					bool supportedPreauth = paInfo.ProcessPadata(paList);
					if (supportedPreauth)
					{
						asreq.asreq.padata = paInfo.BuildPadataList();

						pduBytes = BuildPdu(asreq);
						sendTime = DateTime.UtcNow;
						rep = await this.TransceiveKdcAsync(targetRealm, LocateKdcOptions.Home, pduBytes, cancellationToken).ConfigureAwait(false);
						recvTime = DateTime.UtcNow;
						continue;
					}

					throw new InvalidOperationException(Messages.Krb5_NoSupportedPreauths);
				}
				else
				{
					throw rep.err.GetException();
				}
			}

			if (rep.asrep != null)
				return ProcessASRep(rep.asrep, context, Midpoint(sendTime, recvTime));
			else
				throw new SecurityException(Messages.Krb5_NoASRep);
		}

		public async Task<KdcInfo> GetASInfo(
			string targetRealm,
			string userName,
			CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(targetRealm);
			ArgumentException.ThrowIfNullOrEmpty(userName);

			PreauthInfo paInfo = new PreauthInfo(this, null, this._callback)
			{
				_requestPac = true
			};
			TicketRequestContext context = new TicketRequestContext(null)
			{
				preauth = paInfo
			};

			var asreq = this.CreateASReq(
				context,
				paInfo,
				Structs.KdcReqBody(
					GetDefaultTgtOptions(),
					Structs.PrincipalName(PrincipalNameType.Principal, userName),
					targetRealm,
					Structs.PrincipalName(PrincipalNameType.ServiceInstance, ServiceClassNames.Krbtgt, targetRealm),
					context.nonce,
					this.GetAllETypes(),
					this.MakeHostAddress()
				));

			Memory<byte> pduBytes = BuildPdu(asreq);
			var sendTime = DateTime.UtcNow;
			var rep = await this.TransceiveKdcAsync(targetRealm, LocateKdcOptions.Home, pduBytes, cancellationToken).ConfigureAwait(false);
			if (rep.err != null)
			{
				if ((KerberosErrorCode)rep.err.error_code is KerberosErrorCode.KDC_ERR_PREAUTH_REQUIRED)
				{
					var paList = Asn1DerDecoder.Decode<Asn1SequenceOf<PA_DATA>>(rep.err.e_data).Values;
					paInfo.ProcessPadata(paList);
					return new KdcInfo(
						new KerberosTime(rep.err.stime, rep.err.susec).AsDateTime(),
						(IList<KdcEncryptionTypeInfo>?)paInfo.etypesFromKdc ?? Array.Empty<KdcEncryptionTypeInfo>()
					);
				}
				else
				{
					throw rep.err.GetException();
				}
			}
			throw new InvalidOperationException($"KDC did not require preauthentication for user {userName}@{targetRealm}.");
		}

		private static DateTime Midpoint(DateTime start, DateTime end)
		{
			return start + (end - start) / 2;
		}

		internal static TPadata ExtractPAData<TPadata>(KDC_REQ req, PadataType patype)
			where TPadata : IAsn1DerEncodableTlv, new()
		{
			foreach (var padata in req.padata)
			{
				if (padata.padata_type == (int)patype)
				{
					return Asn1DerDecoder.Decode<TPadata>(padata.padata_value);
				}
			}

			throw new KeyNotFoundException();
		}

		public SessionKey CreateSessionKeyFor(EncryptionKey encKey)
		{
			ArgumentNullException.ThrowIfNull(encKey);
			return CreateSessionKeyFor((EType)encKey.keytype, encKey.keyvalue);
		}

		public SessionKey CreateSessionKeyFor(EType etype, ReadOnlySpan<byte> keyBytes)
		{
			var encProfile = this.TryGetEncProfile(etype);
			if (encProfile == null)
				throw new NotSupportedException($"The encryption key uses an unsupported encryption profile {etype}.");

			return encProfile.CreateSessionKey(Structs.EncryptionKey(etype, keyBytes.ToArray()));
		}

		public TicketAuthorizationData GetTicketAuthorizationData(TicketInfo ticket, byte[] keyBytes)
		{
			var key = this.CreateSessionKeyFor(Structs.EncryptionKey(ticket.TicketEncryptionType, keyBytes));
			var decrypted = key.Decrypt(KeyUsage.Asrep_Tgsrep_Ticket, ticket.ticket.enc_part);
			var encPart = Asn1DerDecoder.Decode<Ticket_EncPart>(decrypted);

			TicketAuthorizationData ad = new TicketAuthorizationData(this, key);
			ad.Process(encPart);
			throw new NotImplementedException();
		}

		internal TicketInfo ProcessASRep(
			KDC_REP asrep,
			TicketRequestContext context,
			DateTime midpoint)
		{
			var encPart = this.ExtractASRepEncPart(asrep, context.credential, out var encProfile);
			if (encPart.nonce != context.nonce)
				throw new SecurityException("The nonce in the AS-REP does not match the nonce sent in the AS-REQ.");

			// UNDONE: This situation can occur if the user supplies the NetBIOS name instead of the FQDN
			// See #405
			//if (!this.CheckSName(encPart.sname, context.targetService, context.target))
			//	throw new SecurityException("The returned ticket does not match the requested target service.");

			var dt = (encPart.authtime.value - midpoint).TotalMinutes;

			// TODO: Check encPart flags and retain other fields

			TicketInfo tgtInfo = new TicketInfo(asrep.ticket, this.CreateSessionKeyFor(encPart.key), encPart, asrep.cname.name_string[0].value, asrep.crealm.value);
			this._callback?.OnReceivedTgt(tgtInfo);

			this.TicketCache.AddTicket(tgtInfo);
			return tgtInfo;
		}

		private EncKDCRepPart ExtractASRepEncPart(
			KDC_REP asrep,
			KerberosCredential credential,
			out EncProfile? encProfile)
		{
			encProfile = null;
			var padata = asrep.padata;
			byte[]? salt = null;
			if (padata != null)
			{
				PreauthInfo paInfo = new PreauthInfo(this, credential, this._callback);
				paInfo.ProcessPadata(asrep.padata);
				var encType = paInfo.TryGetSupportedEncProfile();
				encProfile = encType.encProfile;
				salt = encType.Salt;
			}
			if (encProfile == null)
				encProfile = this.GetEncProfile((EType)asrep.enc_part.etype);

			var protoKey = credential.DeriveProtocolKeyFor(encProfile, salt);
			var encPart = Asn1DerDecoder.Decode<EncKDCRepPart_Outer>(
				protoKey.Decrypt(KeyUsage.AsrepEncPart, asrep.enc_part)
				).Value;
			return encPart;
		}

		private ConcurrentDictionary<string, string> _realmMapping = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		private async Task<TicketInfo> GetTgt(
			string realm,
			KerberosCredential? credential,
			CancellationToken cancellationToken)
		{
			var spn = new ServicePrincipalName(ServiceClassNames.Krbtgt, realm);
			var ticket = this.TicketCache.GetTicketFromCache(spn);
			if (ticket == null)
			{
				if (this._realmMapping.TryGetValue(realm, out string mapped))
					realm = mapped;
				ticket = this.TicketCache.GetTicketFromCache(spn);
			}

			if (ticket != null)
			{
				return ticket;
			}
			else
			{
				if (credential == null)
					throw new InvalidOperationException($"Cannot request a TGT for realm {realm} because there are no credentials to present, and no TGT is present in the cache.  Either provide credentials or import a TGT.");
				ticket = await this.RequestTgt(realm, credential, cancellationToken).ConfigureAwait(false);
				this.TicketCache.AddTicket(ticket);
				if (!string.Equals(ticket.TicketRealm, realm))
				{
					// Realm may have been normalized; set mapping
					this._realmMapping[realm] = ticket.TicketRealm;
				}
				return ticket;
			}
		}

		internal async Task<TicketInfo> GetTicketAsync(
			ServicePrincipalName targetSpn,
			string realm,
			KerberosCredential credential,
			TicketParameters ticketParameters,
			CancellationToken cancellationToken
			)
		{
			ArgumentNullException.ThrowIfNull(targetSpn);

			var ticket = this.TicketCache.GetTicketFromCache(targetSpn);
			if (ticket != null)
				return ticket;


			var tgt = await this.GetTgt(realm, credential, cancellationToken).ConfigureAwait(false);
			ticketParameters ??= GetDefaultTicketOptions(tgt);
			ticket = await this.RequestTicket(
				tgt,
				targetSpn,
				tgt.TicketRealm,
				null,
				ticketParameters,
				cancellationToken).ConfigureAwait(false);
			this.TicketCache.AddTicket(ticket);

			return ticket;
		}

		public async Task<TicketInfo> RequestTicket(
			TicketInfo tgt,
			SecurityPrincipalName spn,
			string realm,
			EType[]? encTypes,
			TicketParameters? ticketParameters,
			CancellationToken cancellationToken)
		{
			var ticket = await RequestTicketCore(tgt, spn, realm, encTypes, ticketParameters, cancellationToken).ConfigureAwait(false);
			if (ticket.IsTgt)
			{
				HashSet<string> referralNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				referralNames.Add(realm);
				while (referralNames.Add(ticket.ServiceInstance) && ticket.IsTgt)
				{
					this._callback?.OnReferralReceived(spn, ticket);
					this.TicketCache?.AddTicket(ticket);

					var nextTicket = await RequestTicketCore(ticket, spn, ticket.ServiceInstance, encTypes, ticketParameters, cancellationToken).ConfigureAwait(false);
					ticket = nextTicket;
				}
			}
			this.TicketCache?.AddTicket(ticket);
			return ticket;
		}
		public async Task<TicketInfo> RequestTicketCore(
			TicketInfo tgt,
			SecurityPrincipalName spn,
			string realm,
			EType[]? encTypes,
			TicketParameters? ticketParameters,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(tgt);
			ArgumentNullException.ThrowIfNull(spn);
			ArgumentException.ThrowIfNullOrEmpty(realm);

			if (ticketParameters == null)
				ticketParameters = this.GetDefaultTicketOptions(tgt);

			if (!ticketParameters.EndTime.HasValue)
				ticketParameters.EndTime = tgt.EndTime ?? TicketParameters.DefaultEndTime;

			TicketRequestContext context = new TicketRequestContext(null);

			var tgsreq = this.CreateTgsReq(spn, tgt, encTypes, ticketParameters, context);

			this._callback?.OnRequestingTicket(spn, tgt, (KdcOptions)tgsreq.tgsreq.req_body.kdc_options.ToUInt32());

			Memory<byte> pduBytes = BuildPdu(tgsreq);
			var rep = await this.TransceiveKdcAsync(realm, string.Equals(realm, tgt.UserRealm) ? LocateKdcOptions.Home : LocateKdcOptions.None, pduBytes, cancellationToken).ConfigureAwait(false);

			if (rep.tgsrep != null)
				return ProcessTgsRep(rep.tgsrep, tgt.SessionKey, context);
			else if (rep.err != null)
				throw rep.err.GetException();
			else
				throw new SecurityException(Messages.Krb5_NoTGSRep);
		}

		internal TicketInfo ProcessTgsRep(
			KDC_REP rep,
			SessionKey tgtSessionKey,
			TicketRequestContext context)
		{
			var encPart = this.ExtractTgsEncPart(rep, tgtSessionKey).Value;
			if (encPart.nonce != context.nonce)
				throw new SecurityException("The nonce in the TGS-REP does not match the nonce sent in the TGS-REQ.");

			TicketInfo ticketInfo = new TicketInfo(rep.ticket, this.CreateSessionKeyFor(encPart.key), encPart, rep.cname.name_string[0].value, rep.crealm.value);

			this._callback?.OnReceivedTicket(ticketInfo);

			return ticketInfo;
		}

		private Asn1Explicit<EncKDCRepPart> ExtractTgsEncPart(
			KDC_REP rep,
			SessionKey tgtSessionKey
			)
		{
			var reader = new ByteMemoryReader(tgtSessionKey.Decrypt(KeyUsage.TgsrepEncPart_SessionKey, rep.enc_part));
			// TODO: Replace with symbolic tag
			var encPart = new Asn1Explicit<EncKDCRepPart>(new Asn1Tag(0x7A));
			encPart.DecodeTlv(Asn1DerEncoding.CreateDerDecoder(reader));
			var kdcOptions = (KdcOptions)encPart.Value.flags.ToUInt32();

			return encPart;
		}

		/// <summary>
		/// Builds a PDU from a <see cref="Kdc_req_choice"/>.
		/// </summary>
		/// <param name="obj">Protocol object</param>
		/// <returns>A buffer containing the PDU suitable for transmission within the application protocol</returns>
		private static Memory<byte> BuildPdu(Kdc_req_choice obj)
		{
			Asn1DerEncoder encoder = Asn1DerEncoding.CreateDerEncoder();
			encoder.EncodeObjTlv(obj);
			var writer = encoder.GetWriter();
			int cbPdu = writer.Position;
			writer.WriteInt32BE(cbPdu);
			Memory<byte> pduBytes = writer.GetData();
			return pduBytes;
		}

		internal static Kdc_req_choice ParseRequestPdu(ReadOnlyMemory<byte> pduBytes)
		{
			return Asn1DerDecoder.Decode<Kdc_req_choice>(pduBytes.Slice(4));
		}

		internal static Kdc_rep_choice ParseReplyPdu(ReadOnlyMemory<byte> pduBytes)
		{
			return Asn1DerDecoder.Decode<Kdc_rep_choice>(pduBytes.Slice(4));
		}

		internal class TicketRequestContext
		{
			internal TicketRequestContext(
				KerberosCredential? credential
				)
			{
				this.nonce = GenerateNonce();
				this.credential = credential;
			}

			internal readonly KerberosCredential? credential;

			internal uint nonce;
			internal PreauthInfo preauth;
		}

		public const KdcOptions DefaultTgtOptions = 0
			| KdcOptions.Forwardable
			| KdcOptions.Renewable
			| KdcOptions.Canonicalize
			| KdcOptions.RenewableOK
			;

		public const KdcOptions DefaultTicketOptions = 0
			| KdcOptions.Forwardable
			| KdcOptions.Renewable
			| KdcOptions.Canonicalize
			;

		public TicketParameters GetDefaultTgtOptions()
		{
			DateTime till = GetDefaultEndTime();
			return new TicketParameters()
			{
				Options = DefaultTgtOptions,
				EndTime = till,
				RenewTill = till
			};
		}

		private static DateTime GetDefaultEndTime()
		{
			return DateTime.UtcNow + TimeSpan.FromHours(10);
		}

		public TicketParameters GetDefaultTicketOptions(TicketInfo tgt)
		{
			return new TicketParameters()
			{
				EndTime = tgt.EndTime,
				Options = DefaultTicketOptions,
			};
		}

		private Kdc_req_choice CreateASReq(
			TicketRequestContext context,
			PreauthInfo preauth,
			KDC_REQ_BODY reqBody
			)
		{
			var credential = context.credential;
			Kdc_req_choice req = new Kdc_req_choice
			{
				asreq = Structs.ASReq(
					preauth.BuildPadataList(),
					reqBody
					)
			};

			return req;
		}

		private HostAddress[]? MakeHostAddress()
		{
			HostAddress[]? hostAddresses;
			var netbiosName = this.Workstation;
			if (netbiosName != null)
			{
				netbiosName = netbiosName.ToUpper().PadRight(15, ' ');
				netbiosName += '\x20';

				hostAddresses = new HostAddress[]
				{
					Structs.HostAddress(AddressType.Netbios, netbiosName)
				};
			}
			else
			{
				hostAddresses = null;
			}

			return hostAddresses;
		}

		private static Checksum ComputeChecksum(ReadOnlySpan<byte> message)
		{
			var cksum = SlimHashAlgorithm.ComputeHash<Md5Context>(message);
			return Structs.Checksum(EncChecksumType.RsaMd5, cksum);
		}

		private Kdc_req_choice CreateTgsReq(
			SecurityPrincipalName spn,
			TicketInfo tgt,
			EType[]? etypes,
			TicketParameters ticketParameters,
			TicketRequestContext context
			)
		{
			ArgumentNullException.ThrowIfNull(ticketParameters);

			Debug.Assert(tgt.IsTgt);

			var cname = Structs.PrincipalName(PrincipalNameType.Principal, tgt.UserName);

			uint seqnbr = (uint)GenerateNonce();

			KDC_REQ_BODY reqBody = Structs.KdcReqBody(
				ticketParameters,
				cname,
				tgt.ServiceInstance,
				Structs.PrincipalName(spn),
				context.nonce,
				(etypes == null) ? this.GetAllETypes() : Array.ConvertAll(etypes, r => (int)r),
				null
				);

			AP_REQ apreq = Structs.APReq(
				0,
				tgt.ticket,
				tgt.SessionKey.EncryptAndWrap(
					KeyUsage.TgsreqPatgsreqPadataApreqAuthChecksum_TgsSessionKey_IncludesAuthSubkey,
					Asn1DerEncoder.EncodeTlv(Structs.Authenticator(
						cname,
						tgt.UserRealm,
						ComputeChecksum(Asn1DerEncoder.EncodeTlv(reqBody).Span),
						seqnbr,
						null
						)).Span)
				);
			PA_DATA[] padatas = new PA_DATA[]
			{
				Structs.PAData_APRep(apreq),
				Kerberos.Structs.PAData_PacOptions(PacOptions.BranchAware)
			};

			Kdc_req_choice req = new Kdc_req_choice
			{
				tgsreq = Structs.TgsReq(padatas, reqBody)
			};

			return req;
		}

		internal AP_REQ CreateAPReq(
			TicketInfo ticket,
			EncryptionKey subkey,
			uint initialSeqNbr,
			APOptions options,
			SecurityCapabilities caps
			)
		{
			// Use from ticket instead of credentials
			var cname = Structs.PrincipalName(PrincipalNameType.Principal, ticket.UserName);
			string crealm = ticket.UserRealm;

			//var reqBodyBytes = Asn1DerEncoder.EncodeTlv(reqBody);
			var authenticator = Structs.Authenticator(
				cname,
				crealm,
				new Checksum
				{
					// [RFC 4121] § 4.1.1 - Authenticator Checksum
					cksumtype = AuthChecksumToken.ChecksumType,
					checksum = new AuthChecksumToken()
					{
						bindLength = 0x10,
						capabilities = caps
					}.AsSpan().ToArray()
				},
				initialSeqNbr,
				subkey
				);
			var enc_authenticator = ticket.SessionKey.EncryptTlv(
				KeyUsage.ApreqAuth_AppSessionKey_IncludesAuthSubkey,
				authenticator);

			AP_REQ apreq = Structs.APReq(
				options,
				ticket.ticket,
				enc_authenticator
				);

			return apreq;
		}

		/// <summary>
		/// Generates a 32-bit nonce.
		/// </summary>
		/// <returns>A nonce</returns>
		internal static uint GenerateNonce()
		{
			// TODO: Ensure uniqueness

			Span<uint> nonce = stackalloc uint[1];
			EncProfile.GetRandomBytes(MemoryMarshal.AsBytes(nonce));
			return nonce[0];
		}

		#region Dispose pattern
		private bool _isDisposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_isDisposed = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~KerberosClient()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		#endregion

		#region Test
		internal void TestAsRep(
			byte[] buf,
			KerberosCredential credential
			)
		{
			var asrep = ParseReplyPdu(buf);
			var encPart = ExtractASRepEncPart(asrep.asrep, credential, out var encProfile);
		}
		#endregion

		internal KerbTrace Trace(
			KerberosCredential credential,
			byte[] asreqBytes,
			byte[] asrepBytes,
			byte[] tgsreqBytes,
			byte[] tgsrepBytes,
			byte[] apreqBytes,
			byte[] aprepBytes,
			byte[] aprep2Bytes,
			byte[] mechList,
			byte[] initiatorMechListMic,
			byte[] acceptorMechListMic,
			byte[] req0,
			byte[] rep0
			)
		{
			KerbTrace trace = new KerbTrace(this, credential, this._callback);

			trace.TraceAsreq(asreqBytes);
			trace.TraceAsrep(asrepBytes);
			trace.TraceTgsreq(tgsreqBytes);
			trace.TraceTgsrep(tgsrepBytes);
			trace.TraceApreq(apreqBytes);
			trace.TraceAprep(aprepBytes, mechList, acceptorMechListMic);
			trace.TraceAprep2(aprep2Bytes, mechList, initiatorMechListMic);

			trace.TraceReq(req0);

			return trace;
		}

		internal KerbTrace Trace(
			KerberosCredential credential,
			SessionKey ticketSessionKey,
			byte[] apreqBytes,
			byte[] aprepBytes,
			byte[] aprep2Bytes,
			byte[] mechList,
			byte[] initiatorMechListMic,
			byte[] acceptorMechListMic,
			byte[] req0,
			byte[] rep0
			)
		{
			KerbTrace trace = new KerbTrace(this, credential, this._callback);

			trace.TicketSessionKey = ticketSessionKey;
			trace.TraceApreq(apreqBytes);
			trace.TraceAprep(aprepBytes, mechList, acceptorMechListMic);
			trace.TraceAprep2(aprep2Bytes, mechList, initiatorMechListMic);

			trace.TraceReq(req0);

			return trace;
		}
		public void ImportTickets(IEnumerable<TicketInfo> tickets)
		{
			ArgumentNullException.ThrowIfNull(tickets);
			foreach (var ticket in tickets)
			{
				this.TicketCache.AddTicket(ticket);
			}
		}
		public void ImportTicket(TicketInfo ticket)
		{
			ArgumentNullException.ThrowIfNull(ticket);
			this.TicketCache.AddTicket(ticket);
		}
		public byte[] ExportTickets(IList<TicketInfo> tickets, KerberosFileFormat format)
		{
			ArgumentNullException.ThrowIfNull(tickets);

			return format switch
			{
				KerberosFileFormat.Kirbi => ExportKirbi(tickets),
				KerberosFileFormat.Ccache => ExportCcacheBytes(tickets),
			};
		}

		internal static byte[] ExportKirbi(IList<TicketInfo> tickets)
		{
			Ticket_Ticket[] asnTickets = new Ticket_Ticket[tickets.Count];
			KrbCredInfo[] encParts = new KrbCredInfo[tickets.Count];

			for (int i = 0; i < tickets.Count; i++)
			{
				var ticket = tickets[i];
				asnTickets[i] = ticket.ticket;
				encParts[i] = new KrbCredInfo()
				{
					key = ticket.SessionKey.key,
					prealm = new GeneralString(ticket.UserRealm),
					pname = new Asn1.KerberosV5Spec2.PrincipalName
					{
						name_type = (int)PrincipalNameType.Principal,
						name_string = new GeneralString[]
						{
							new GeneralString(ticket.UserName)
						}
					},
					flags = new Asn1BitString(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((int)ticket.KdcOptions)), 0),
					starttime = ticket.StartTime,
					endtime = ticket.EndTime,
					renew_till = ticket.RenewTill,
					srealm = new GeneralString(ticket.ServiceRealm),
					sname = Structs.PrincipalName(ticket.TargetSpn),
					// TODO caddr
					authtime = null
				};
			}

			EncKrbCredPart_Outer encPart = new()
			{
				Value = new EncKrbCredPart_Unnamed_11
				{
					ticket_info = encParts,
				}
			};
			var encPartBytes = Asn1DerEncoder.EncodeTlv<EncKrbCredPart_Outer>(encPart).ToArray();

			var krbcred = new KrbCred()
			{
				Value = new KRB_CRED_Unnamed_10()
				{
					pvno = 5,
					msg_type = (int)KrbMessageType.Cred,
					tickets = asnTickets,
					enc_part = new EncryptedData
					{
						cipher = encPartBytes
					}
				}
			};
			var krbcredBytes = Asn1DerEncoder.EncodeTlv<KrbCred>(krbcred).ToArray();
			return krbcredBytes;
		}

		private byte[] ExportCcacheBytes(IList<TicketInfo> tickets)
		{
			CCache ccache = new CCache
			{
				format = 5,
				version = 4,

				header = new CCacheHeader()
				{
					headerSize = 0x0C,
					headerData = new byte[] { 0, 1, 0, 8, 0xFF, 0xFF, 0xFF, 0xFF, 0, 0, 0, 0 },
				},
				defaultPrincipal = CCachePrincipal.FromTicketClient(tickets[0]),
				credList = new CCacheCredentialList
				{
					credentials = tickets.Select(ToCCacheCred).ToArray()
				},
			};

			ByteWriter writer = new ByteWriter();
			writer.WritePduStruct(ccache);
			return writer.GetData().ToArray();
		}

		private CCacheCredential ToCCacheCred(TicketInfo ticket)
		{
			CCacheCredential cred = new CCacheCredential
			{
				version = 4,
				client = CCachePrincipal.FromTicketClient(ticket),
				server = CCachePrincipal.FromSpn(ticket.TargetSpn, ticket.ServiceRealm),
				key = new CCacheKeyBlock
				{
					encType = ticket.SessionKey.EType,
					keyData = new CCacheData(ticket.SessionKey.KeyBytes)
				},
				authTime = ticket.StartTime?.ToCCacheDateTime() ?? 0,
				startTime = ticket.StartTime?.ToCCacheDateTime() ?? 0,
				endTime = ticket.EndTime?.ToCCacheDateTime() ?? 0,
				renewTill = ticket.RenewTill?.ToCCacheDateTime() ?? 0,
				isSKey = 0,
				ticketFlags = ticket.KdcOptions,
				addressCount = 0,
				addresses = Array.Empty<CCacheAddress>(),
				authDataCount = 0,
				authData = Array.Empty<CCacheAuthData>(),
				ticket = new CCacheData(Asn1DerEncoder.EncodeTlv(new CCacheTicketWrapper(ticket.ticket)).ToArray()),
				ticket2 = new CCacheData(Array.Empty<byte>())
			};
			return cred;
		}

		public TicketInfo[] LoadTicketsFromFile(byte[] bytes, out KerberosFileFormat format)
		{
			ArgumentNullException.ThrowIfNull(bytes);
			if (bytes.Length == 0)
				throw new ArgumentException("The byte array is empty.", nameof(bytes));

			if (bytes[0] == 0x76)
			{
				var tickets = this.LoadTicketsFromKirbiFile(bytes);
				format = KerberosFileFormat.Kirbi;
				return tickets;
			}
			else if (bytes[0] == 0x05)
			{
				var tickets = this.LoadTicketsFromCcacheFile(bytes);
				format = KerberosFileFormat.Ccache;
				return tickets.ToArray();
			}
			else
				throw new ArgumentException("The file format is not supported.");
		}
		private TicketInfo[] LoadTicketsFromKirbiFile(byte[] kirbiBytes)
		{
			var krbcred = Asn1DerDecoder.Decode<KrbCred>(kirbiBytes);
			var encPart = Asn1DerDecoder.Decode<EncKrbCredPart_Outer>(krbcred.Value.enc_part.cipher);

			var ticketCount = krbcred.Value.tickets.Length;
			TicketInfo[] tickets = new TicketInfo[ticketCount];
			for (int i = 0; i < tickets.Length; i++)
			{
				var ticket = krbcred.Value.tickets[i];
				var encPartInfo = encPart.Value.ticket_info[i];

				TicketInfo ticketInfo = new TicketInfo(
					ticket,
					this.CreateSessionKeyFor(encPartInfo.key),
					encPartInfo
					);

				tickets[i] = ticketInfo;
			}

			return tickets;
		}
		private IList<TicketInfo> LoadTicketsFromCcacheFile(byte[] ccacheBytes)
		{
			if (ccacheBytes.Length < 2)
				throw new InvalidDataException("The file is not a valid .ccache file.");

			var version = ccacheBytes[1];
			if ((uint)(version - 1) >= (uint)4)
				throw new NotSupportedException($"The .ccache file appears to be a version not supported by this implementation.");

			ByteMemoryReader reader = new ByteMemoryReader(ccacheBytes);
			var ccache = reader.ReadPduStruct<CCache>();

			List<TicketInfo> tickets = new List<TicketInfo>(ccache.credList.credentials.Length);
			foreach (var cred in ccache.credList.credentials)
			{
				var key = this.CreateSessionKeyFor(cred.key.encType, cred.key.keyData.bytes);
				TicketInfo info = new TicketInfo(key, cred);
				tickets.Add(info);
			}

			return tickets;
		}


		public const string CCacheExtension = ".ccache";

		public static KerberosFileFormat GetFormatFromFileName(string fileName)
		{
			ArgumentException.ThrowIfNullOrEmpty(fileName);

			if (fileName.EndsWith(CCacheExtension, StringComparison.OrdinalIgnoreCase))
				return KerberosFileFormat.Ccache;
			else
				return KerberosFileFormat.Kirbi;
		}
	}

	internal class KerbTrace
	{
		private readonly KerberosClient kerb;
		private readonly KerberosCredential credential;
		private readonly IKerberosCallback? callback;
		private KdcOptions tgsKdcOptions;

		internal KerbTrace(KerberosClient kerb, KerberosCredential credential, IKerberosCallback? callback = null)
		{
			this.kerb = kerb;
			this.credential = credential;
			this.callback = callback;
		}

		#region AS-REQ
		public KDC_REQ? Asreq { get; private set; }
		public string? AuthService { get; private set; }
		public string? AuthRealm { get; private set; }
		public int AuthNonce { get; private set; }
		#endregion
		#region AS-REP
		public KDC_REP? Asrep { get; private set; }
		public TicketInfo? Tgt { get; private set; }
		public SessionKey? TgtSessionKey { get; private set; }
		#endregion
		#region TGS-REQ
		public KDC_REQ Tgsreq { get; private set; }
		public int TgsNonce { get; private set; }
		public SecurityPrincipalName TargetSpn { get; private set; }
		#endregion
		#region TGS-REP
		public KDC_REP Tgsrep { get; private set; }
		public AP_REQ Tgsreq_apreq { get; private set; }
		public Authenticator_Outer Tgsreq_auth { get; private set; }
		public TicketInfo Ticket { get; private set; }
		public SessionKey TicketSessionKey { get; internal set; }
		#endregion
		#region AP-REQ
		public AP_REQ_Unnamed_3 Apreq { get; private set; }
		public Authenticator_Outer Apreq_auth { get; private set; }
		#endregion
		#region AP-REP
		public AP_REP_Unnamed_5 Aprep { get; private set; }
		public EncPart_APRep Aprep_auth { get; private set; }
		public SessionKey AcceptorSubkey { get; private set; }
		public uint RecvSeqNbr { get; private set; }
		#endregion
		#region AP-REP2
		public AP_REP_Unnamed_5 Aprep2 { get; private set; }
		public EncPart_APRep Aprep2_auth { get; private set; }
		public uint SendSeqNbr { get; private set; }
		#endregion

		internal void TraceAsreq(byte[] asreqBytes)
		{
			var asreq = Asn1DerDecoder.Decode<KDC_REQ>(asreqBytes);
			this.Asreq = asreq;
			var authTarget = asreq.req_body.sname.name_string;
			this.AuthService = authTarget[0].value;
			this.AuthRealm = authTarget[1].value;
			this.AuthNonce = asreq.req_body.nonce;

			this.callback?.OnRequestingTgt(this.AuthRealm, this.credential, this.AuthNonce);
		}
		internal void TraceAsrep(byte[] asrepBytes)
			=> this.TraceAsrep(asrepBytes, this.AuthService, this.AuthRealm, this.AuthNonce);
		internal void TraceAsrep(
			byte[] asrepBytes,
			string authService,
			string authRealm,
			int authNonce)
		{
			var asrep = Asn1DerDecoder.Decode<KDC_REP>(asrepBytes);
			this.Asrep = asrep;
			var tgt = this.kerb.ProcessASRep(asrep, new TicketRequestContext(credential)
			{ nonce = (uint)authNonce }, DateTime.Now);
			this.Tgt = tgt;
			this.TgtSessionKey = tgt.SessionKey;
		}

		internal void TraceTgsreq(byte[] tgsreqBytes)
			=> this.TraceTgsreq(tgsreqBytes, this.TgtSessionKey);
		internal void TraceTgsreq(byte[] tgsreqBytes, SessionKey tgtSessionKey)
		{
			var tgsreq = Asn1DerDecoder.Decode<Asn1.KerberosV5Spec2.KDC_REQ>(tgsreqBytes);
			this.Tgsreq = tgsreq;
			this.TgsNonce = tgsreq.req_body.nonce;

			this.TargetSpn = tgsreq.req_body.sname.ToSecurityPrincipalName();

			var tgsreq_options = (KdcOptions)tgsreq.req_body.kdc_options.ToUInt32();
			AP_REQ? tgsreq_apreq = null;
			Authenticator_Outer? tgsreq_auth = null;
			foreach (var padata in tgsreq.padata)
			{
				switch ((PadataType)padata.padata_type)
				{
					case PadataType.TgsReq:
						tgsreq_apreq = Asn1DerDecoder.Decode<AP_REQ>(padata.padata_value);
						tgsreq_auth = tgtSessionKey.DecryptTlv<Authenticator_Outer>(
							KeyUsage.TgsreqPatgsreqPadataApreqAuthChecksum_TgsSessionKey_IncludesAuthSubkey,
							tgsreq_apreq.Value.authenticator);
						break;
				}
			}

			this.callback?.OnRequestingTicket(this.TargetSpn, this.Tgt, tgsreq_options);
		}

		internal void TraceTgsrep(byte[] tgsrepBytes)
			=> TraceTgsrep(tgsrepBytes, this.TargetSpn, this.TgsNonce, this.TgtSessionKey);
		internal void TraceTgsrep(byte[] tgsrepBytes, SecurityPrincipalName spn, int tgsNonce, SessionKey tgtSessionKey)
		{
			var tgsrep = Asn1DerDecoder.Decode<Asn1.KerberosV5Spec2.KDC_REP>(tgsrepBytes);
			var ticket = this.kerb.ProcessTgsRep(tgsrep, tgtSessionKey, new TicketRequestContext(credential)
			{ nonce = (uint)TgsNonce });
			this.Ticket = ticket;
			this.TicketSessionKey = ticket.SessionKey;
		}

		internal void TraceApreq(byte[] apreqBytes)
			=> this.TraceApreq(apreqBytes, this.TicketSessionKey);
		internal void TraceApreq(byte[] apreqBytes, SessionKey ticketSessionKey)
		{
			this.TicketSessionKey = ticketSessionKey;
			var apreq_ = Asn1DerDecoder.Decode<Asn1.KerberosV5Spec2.AP_REQ_Unnamed_3>(apreqBytes);
			var apreq_auth = Asn1DerDecoder.Decode<Asn1.KerberosV5Spec2.Authenticator_Outer>(ticketSessionKey.Decrypt(KeyUsage.ApreqAuth_AppSessionKey_IncludesAuthSubkey, apreq_.authenticator));

			this.SendSeqNbr = apreq_auth.Value.seq_number.Value;
			if (apreq_auth.Value.authorization_data != null)
			{
				foreach (var authData in apreq_auth.Value.authorization_data)
				{
					switch (authData.ad_type)
					{
						case 1:
							{
								var subauth = Asn1DerDecoder.Decode<Asn1Explicit<Unnamed_0>>(authData.ad_data);
							}
							break;
						default:
							break;
					}
				}
			}

			SecurityCapabilities gssFlags = SecurityCapabilities.None;
			if (
				apreq_auth.Value.cksum.cksumtype == AuthChecksumToken.ChecksumType
				&& apreq_auth.Value.cksum.checksum.Length >= AuthChecksumToken.StructSize
				)
			{
				ref var token = ref MemoryMarshal.AsRef<AuthChecksumToken>(apreq_auth.Value.cksum.checksum);
				gssFlags = token.capabilities;
			}

			this.callback?.OnSendingApreq(null, this.TargetSpn, null, this.credential, gssFlags, ticketSessionKey, this.SendSeqNbr);
		}

		internal void TraceAprep(byte[] aprepBytes,
			byte[]? mechList, byte[]? acceptorMechListMic
			)
			=> this.TraceAprep(aprepBytes, this.TicketSessionKey, mechList, acceptorMechListMic);
		internal void TraceAprep(byte[] aprepBytes, SessionKey ticketSessionKey,
			byte[]? mechList, byte[]? acceptorMechListMic
			)
		{
			var aprep_ = Asn1DerDecoder.Decode<Asn1.KerberosV5Spec2.AP_REP_Unnamed_5>(aprepBytes);
			var aprep_auth = Asn1DerDecoder.Decode<Asn1.KerberosV5Spec2.EncPart_APRep>(ticketSessionKey.Decrypt(KeyUsage.APRep_EncPart, aprep_.enc_part));
			var acceptorSubkey = this.kerb.CreateSessionKeyFor(aprep_auth.Value.subkey);
			this.AcceptorSubkey = acceptorSubkey;
			this.RecvSeqNbr = aprep_auth.Value.seq_number ?? 0;

			this.callback?.OnReceivedAprep(null, this.RecvSeqNbr, acceptorSubkey);

			if (mechList != null && acceptorMechListMic != null)
			{
				acceptorSubkey.VerifySignature(
					KeyUsage.AcceptorSign,
					aprep_auth.Value.seq_number.Value,
					WrapFlags.AcceptorSubkey,
					new MessageVerifyParams(acceptorMechListMic, SecBufferList.Create(SecBuffer.Integrity(mechList)))
					);
			}
		}

		internal void TraceAprep2(byte[] aprep2Bytes, byte[] mechList, byte[] initiatorMechListMic)
			=> this.TraceAprep2(aprep2Bytes, this.TicketSessionKey, this.AcceptorSubkey, this.SendSeqNbr, mechList, initiatorMechListMic);
		internal void TraceAprep2(byte[] aprep2Bytes, SessionKey ticketSessionKey, SessionKey acceptorSubkey, uint sendSeqNbr, byte[] mechList, byte[] initiatorMechListMic)
		{
			var aprep2_ = Asn1DerDecoder.Decode<Asn1.KerberosV5Spec2.AP_REP_Unnamed_5>(aprep2Bytes);
			var aprep2_auth = Asn1DerDecoder.Decode<Asn1.KerberosV5Spec2.EncPart_APRep>(ticketSessionKey.Decrypt(KeyUsage.APRep_EncPart, aprep2_.enc_part));
			acceptorSubkey.VerifySignature(
				KeyUsage.InitiatorSign,
				(uint)sendSeqNbr,
				WrapFlags.AcceptorSubkey,
				new MessageVerifyParams(initiatorMechListMic, SecBufferList.Create(SecBuffer.Integrity(mechList)))
				);
		}

		internal void TraceReq(byte[] req0)
			=> this.TraceReq(req0, this.AcceptorSubkey, this.SendSeqNbr);
		internal void TraceReq(byte[] req0, SessionKey acceptorSubkey, uint sendSeqNbr)
		{
			var buffer = req0;

			const int RpcHeaderSize = 0x18;
			const int AuthHeaderSize = 8;
			int cbFrag = BinaryPrimitives.ReadUInt16LittleEndian(req0.Slice(8, 2));
			int authLength = BinaryPrimitives.ReadUInt16LittleEndian(req0.Slice(10, 2));
			int cbBody = cbFrag - authLength - AuthHeaderSize - RpcHeaderSize;
			Span<byte> rpcHeader = buffer.Slice(0, RpcHeaderSize);
			Span<byte> stubData = buffer.Slice(RpcHeaderSize, cbBody);
			Span<byte> authTrailer = buffer.Slice(RpcHeaderSize + cbBody, AuthHeaderSize);
			Span<byte> sealTrailer = buffer.Slice(cbFrag - authLength, authLength);
			acceptorSubkey.UnsealMessage(
				KeyUsage.InitiatorSeal,
				(uint)(sendSeqNbr + 1),
				WrapFlags.AcceptorSubkey | WrapFlags.Sealed,
				new MessageSealParams(
					default,
					SecBufferList.Create(
						SecBuffer.Integrity(rpcHeader),
						SecBuffer.PrivacyWithIntegrity(stubData),
						SecBuffer.Integrity(authTrailer)
					),
					sealTrailer
				));
		}
	}

	public enum KerberosFileFormat
	{
		Unknown = 0,
		Kirbi,
		Ccache,
	}

	public static class ServiceExtensions
	{
		public static KerberosClient CreateKerberosClient(this IServiceProvider services, IKdcLocator? locator)
		{
			var callback = services.GetService<IKerberosCallback>();
			if (callback == null)
			{
				var log = services.GetService<ILog>();
				if (log != null)
					callback = new KerberosDiagnosticLogger(log);
			}
			return new KerberosClient(locator, services.GetService<ISocketService>(), callback);
		}
	}
}
