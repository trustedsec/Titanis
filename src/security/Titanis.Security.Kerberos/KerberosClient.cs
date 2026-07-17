using KerberosPreauthFramework;
using KerberosV5Spec2;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Crypto;
using Titanis.IO;
using Titanis.Net;
using static Titanis.Security.Kerberos.KerberosClient;

[assembly: InternalsVisibleTo("Titanis.Security.Kerberos.Test")]

namespace Titanis.Security.Kerberos
{
	/// <summary>
	/// Implements a Kerberos client.
	/// </summary>
	public partial class KerberosClient : IDisposable
	{
		public const string Krb5CacheVariableName = "KRB5CCNAME";

		public static readonly ServicePrincipalName ChangePwSpn = new ServicePrincipalName(PrincipalNameType.ServiceInstance, "kadmin", "changepw");

		/// <summary>
		/// Initializes a new <see cref="KerberosClient"/> for offline use.
		/// </summary>
		public KerberosClient()
		{

		}

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
				if (socketService != null)
					this._transport = new KerberosSocketTransport(socketService);
			}
		}

		/// <summary>
		/// Initializes a new <see cref="KerberosClient"/>.
		/// </summary>
		/// <param name="locator">KDC locator</param>
		/// <param name="transport"><see cref="IKerberosTransport"/> implementation for network communication</param>
		/// <remarks>
		/// If <paramref name="locator"/> is <see langword="null"/>, this client instance can only provide tickets from the cache.
		/// </remarks>
		internal KerberosClient(
			IKdcLocator? locator,
			IKerberosTransport? transport = null,
			IKerberosCallback? callback = null
			)
		{
			this._locator = locator;
			this._callback = callback;
			this._transport = transport;
		}

		/// <summary>
		/// TCP port used by Kerberos servers.
		/// </summary>
		public const int KdcTcpPort = 88;

		private readonly IKdcLocator? _locator;
		private readonly IKerberosCallback? _callback;
		private readonly IKerberosTransport? _transport;

		private ITicketCache? _ticketCache;
		public ITicketCache TicketCache
		{
			get => this._ticketCache ??= new TicketCache();
			set
			{
				ArgumentNullException.ThrowIfNull(value);
				this._ticketCache = value;
			}
		}

		private IKerberosTransport EnsureTransport()
		{
			if (this._transport is null)
				throw new InvalidOperationException("The Kerberos client is not configured for network configuration.");

			return this._transport;
		}

		/// <summary>
		/// Gets or sets the name of the workstation.
		/// </summary>
		/// <remarks>
		/// If provided, this is included with <c>ASREQ</c> messages.
		/// </remarks>
		public HostAddress? Workstation { get; set; }

		#region Network I/O
		// TODO: What is the max buffer size?

		private IKdcLocator VerifyKdcLocator()
		{
			var locator = this._locator;
			if (locator == null)
				throw new NotImplementedException("This Kerberos client is not configured with a KDC locator and cannot request new tickets.");

			return locator;
		}

		private async Task<KDC_REP_CHOICE> TransceiveKdcAsync(
			string realm,
			LocateKdcOptions options,
			KDC_REQ_CHOICE kdcreq,
			CancellationToken cancellationToken)
		{
			EndPoint kdcEP = this.VerifyKdcLocator().LocateKdc(realm, options);
			if (kdcEP == null)
				throw new NotSupportedException(string.Format(Messages.Krb5_NoKdc, realm));

			return await EnsureTransport().TransceiveKdcAsync(realm, kdcEP, kdcreq, cancellationToken).ConfigureAwait(false);
		}

		#endregion

		private List<EncProfile> _encProfiles = new List<EncProfile>()
		{
			Singleton.SingleInstance<EncProfile_Aes256CtsHmacSha1_96>(),
			Singleton.SingleInstance<EncProfile_Aes128CtsHmacSha1_96>(),
			Singleton.SingleInstance<Rc4Hmac>(),
			Singleton.SingleInstance<Rc4HmacExp>(),
			Singleton.SingleInstance<EncProfile_DesCbcMd5>(),
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
				if (credential.SupportsProfile(prof.EType))
					etypes.Add((int)prof.EType);
			}
			return etypes.ToArray();
		}
		private int[] GetAllETypes()
		{
			return Array.ConvertAll(this.DefaultETypes, r => (int)r);
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

		// [RFC 6113] § 5.1 - Combining Keys
		internal static SessionKey KrbFxCf2(
			EncProfile encProf,
			SessionKey key1,
			SessionKey key2,
			ReadOnlySpan<byte> pepper1,
			ReadOnlySpan<byte> pepper2
			)
		{
			var cbSeed = encProf.KeyGenerationSeedSizeBytes;

			Span<byte> buf1 = stackalloc byte[cbSeed];
			PrfPlus(key1, pepper1, buf1);
			Span<byte> buf2 = stackalloc byte[cbSeed];
			PrfPlus(key2, pepper2, buf2);

			for (int i = 0; i < buf1.Length; i++)
			{
				buf1[i] ^= buf2[i];
			}
			return encProf.RandomToKey(buf1);
		}
		// [RFC 6113] § 5.1 - Combining Keys
		private static void PrfPlus(
			SessionKey key,
			ReadOnlySpan<byte> shared,
			Span<byte> output)
		{
			int offset = 0;
			int count = 0;
			Span<byte> shared_1 = stackalloc byte[1 + shared.Length];
			shared.CopyTo(shared_1.Slice(1));
			var encProf = key.EncryptionProfile;
			do
			{
				count++;
				shared_1[0] = (byte)count;

				Span<byte> prfbuf = stackalloc byte[encProf.PrfSizeBytes];
				encProf.PseudoRandom(key.KeyBytes, shared_1, prfbuf);

				prfbuf.Slice(0, Math.Min(prfbuf.Length, output.Length - offset)).CopyTo(output.Slice(offset));
				// output.Slice(offset, encProf.PrfSizeBytes)
				offset += prfbuf.Length;
			} while (offset < output.Length);
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
			=> this.RequestInitialTicket(targetRealm, credential, null, GetDefaultTgtParameters(), null, cancellationToken);
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
		public async Task<TicketInfo> RequestInitialTicket(
			string targetRealm,
			KerberosCredential credential,
			SecurityPrincipalName? targetSpn,
			TicketParameters? ticketParameters,
			EType[]? encTypes,
			CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(targetRealm);
			ArgumentNullException.ThrowIfNull(credential);
			targetSpn ??= new ServicePrincipalName(PrincipalNameType.ServiceInstance, ServiceClassNames.Krbtgt, targetRealm);

			if (ticketParameters == null)
				ticketParameters = GetDefaultTgtParameters();

			if (!ticketParameters.EndTime.HasValue)
				ticketParameters.EndTime = GetDefaultEndTime();

			int[] encTypeValues = (encTypes != null)
				? Array.ConvertAll(encTypes, r => (int)r)
				: GetETypes(credential);
			if (encTypes == null)
				encTypeValues = GetETypes(credential);

			PreauthContext paContext = credential.CreatePreauthContext(this, this._callback);
			paContext._requestPac = true;
			paContext.PacRequestOptions = ticketParameters.PacRequestOptions;
			TicketRequestContext context = new TicketRequestContext(ticketParameters, credential, paContext, null, false)
			{
				ArmorSubkey = (ticketParameters.ArmorTicket?.GenerateSessionKey() ?? null)
			};

			do
			{
			var asreq = this.CreateASReq(
				context,
				paContext,
				Structs.KdcReqBody(
					ticketParameters,
					Structs.PrincipalName(credential.UserName),
					credential.Realm,
					Structs.PrincipalName(targetSpn),
					context.nonce,
					encTypeValues,
					this.MakeHostAddress(),
					null
				));

			this._callback?.OnRequestingTgt(targetRealm, credential, ticketParameters, asreq.Asreq.req_body.nonce);

			var sendTime = DateTime.UtcNow;
			var rep = await this.TransceiveKdcAsync(targetRealm, LocateKdcOptions.Home, asreq, cancellationToken).ConfigureAwait(false);
			var recvTime = DateTime.UtcNow;
			// TODO: Add a max loop count to avoid getting stuck.
				if (rep.SelectedChoice == KDC_REP_CHOICE.ChoiceIndex.Error)
			{
				var err = rep.Error;
				if ((KerberosErrorCode)err.error_code is KerberosErrorCode.KDC_ERR_PREAUTH_REQUIRED && !err.e_data.IsNullOrEmpty())
				{
					paContext.Skew = new KerberosTime(err.stime, err.susec).AsDateTime() - sendTime;

					var paList = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<PA_DATA>>(err.e_data).Values;
					this._callback?.OnReceivedAsrepPreauthRequired(ticketParameters.CorrelationId, paList);
						if (!paContext.TryProcessPadata(ticketParameters.CorrelationId, paList))
							throw new InvalidOperationException(Messages.Krb5_NoSupportedPreauths);

						continue;

				}
				else
				{
					var ex = err.GetException();
					this._callback?.OnReceivedAsrepError(ticketParameters.CorrelationId, ex);
					throw ex;
				}
			}
				else if (rep.SelectedChoice == KDC_REP_CHOICE.ChoiceIndex.Asrep)
				return ProcessASRep(ticketParameters.CorrelationId, rep.Asrep, context, Midpoint(sendTime, recvTime));
			else
				throw new SecurityException(Messages.Krb5_NoASRep);
			} while (true);
		}

		public async Task<KdcInfo> GetASInfo(
			string targetRealm,
			string userName,
			EType[]? etypes,
			CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(targetRealm);
			ArgumentException.ThrowIfNullOrEmpty(userName);

			Guid correlationId = Guid.NewGuid();

			PreauthContext preauth = new PreauthKeyContext(this, null, this._callback)
			{
				_requestPac = true
			};
			TicketRequestContext context = new TicketRequestContext(null, null, preauth, null, false);

			var asreq = this.CreateASReq(
				context,
				preauth,
				Structs.KdcReqBody(
					GetDefaultTgtParameters(),
					Structs.PrincipalName(PrincipalNameType.Principal, userName),
					targetRealm,
					Structs.PrincipalName(PrincipalNameType.ServiceInstance, ServiceClassNames.Krbtgt, targetRealm),
					context.nonce,
					(etypes != null) ? (Array.ConvertAll(etypes, r => (int)r)) : this.GetAllETypes(),
					this.MakeHostAddress(),
					null
				));

			var sendTime = DateTime.UtcNow;
			var rep = await this.TransceiveKdcAsync(targetRealm, LocateKdcOptions.Home, asreq, cancellationToken).ConfigureAwait(false);
			if (rep.SelectedChoice == KDC_REP_CHOICE.ChoiceIndex.Error)
			{
				var err = rep.Error;
				if ((KerberosErrorCode)err.error_code is KerberosErrorCode.KDC_ERR_PREAUTH_REQUIRED)
				{
					var paList = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<PA_DATA>>(err.e_data).Values;
					preauth.TryProcessPadata(correlationId, paList);
					return new KdcInfo(
						new KerberosTime(err.stime, err.susec).AsDateTime(),
						(IList<KdcEncryptionTypeInfo>?)preauth.etypesFromKdc ?? Array.Empty<KdcEncryptionTypeInfo>()
					);
				}
				else
				{
					throw err.GetException();
				}
			}
			throw new InvalidOperationException($"KDC did not require preauthentication for user {userName}@{targetRealm}.");
		}

		private static DateTime Midpoint(DateTime start, DateTime end)
		{
			return start + (end - start) / 2;
		}

		internal static TPadata ExtractPAData<TPadata>(KDC_REQ req, PadataType patype)
			where TPadata : IAsn1DerDecodableTlv<TPadata>
		{
			foreach (var padata in req.padata)
			{
				if (padata.padata_type == (int)patype)
				{
					return Asn1DerDecoder.DecodeTlv<TPadata>(padata.padata_value);
				}
			}

			throw new KeyNotFoundException();
		}

		public SessionKey CreateSessionKeyFor(EncryptionKey encKey)
		{
			ArgumentNullException.ThrowIfNull(encKey);
			return CreateSessionKeyFor((EType)encKey.keytype, encKey.keyvalue);
		}

		public SessionKey CreateSessionKeyFor(EType etype, ReadOnlySpan<byte> keyBytes, bool allowInvalidEType = false)
		{
			var encProfile = this.TryGetEncProfile(etype);
			if (encProfile == null)
			{
				if (allowInvalidEType)
					encProfile = new DummyEncProfile(etype);
				else
					throw new NotSupportedException($"The encryption key uses an unsupported encryption profile {etype}.");
			}

			return encProfile.CreateSessionKey(keyBytes.ToArray());
		}

		private int _lastTicketSeqnbr;
		private int GetNextTicketSeqnbr()
		{
			return Interlocked.Increment(ref this._lastTicketSeqnbr);
		}

		internal TicketInfo ProcessASRep(
			Guid correlationId,
			KDC_REP asrep,
			TicketRequestContext context,
			DateTime midpoint)
		{
			context.preauth.TryProcessPadata(correlationId, asrep.padata);

			var encPart = this.ExtractASRepEncPart(asrep, context.preauth, out var asrepKey);
			// TODO: Log decryption failure

			SessionKey sessionKey = this.CreateSessionKeyFor(encPart.key);

			if (encPart.padata != null)
				context.preauth.TryProcessPadata(correlationId, encPart.padata);

			this._callback?.OnReceivedAsrep(correlationId, new KdcRepInfo(context, asrep, encPart, asrepKey, sessionKey));

			if (encPart.nonce != context.nonce)
				throw new SecurityException("The nonce in the AS-REP does not match the nonce sent in the AS-REQ.");


			// UNDONE: This situation can occur if the user supplies the NetBIOS name instead of the FQDN
			// See #405
			//if (!this.CheckSName(encPart.sname, context.targetService, context.target))
			//	throw new SecurityException("The returned ticket does not match the requested target service.");

			var dt = (encPart.authtime.Value - midpoint).TotalMinutes;

			// TODO: Check encPart flags and retain other fields

			TicketInfo tgtInfo = new TicketInfo(GetNextTicketSeqnbr(), asrep.ticket, sessionKey, encPart, asrep.cname.name_string[0].Value, asrep.crealm.Value, asrepKey, null);
			tgtInfo.Comment = context.ticketParameters?.TicketComment;

			this._callback?.OnReceivedTgt(tgtInfo);

			bool cacheEligible = context.ticketParameters.AdditionalTicket == null;
			this.TicketCache.AddTicket(tgtInfo);
			return tgtInfo;
		}

		private EncKDCRepPart ExtractASRepEncPart(
			KDC_REP asrep,
			PreauthContext paContext,
			out SessionKey asrepKey
			)
		{
			var padata = asrep.padata;
			if (padata != null)
			{
			}

			var encProfile = this.GetEncProfile((EType)asrep.enc_part.etype);
			var replyKey = paContext.DeriveProtocolKey(encProfile);
			SessionKey? armorStrengthenKey = paContext.ArmorStrengthenKey;
			if (armorStrengthenKey != null)
			{
				replyKey = KrbFxCf2(armorStrengthenKey.EncryptionProfile, armorStrengthenKey, replyKey, StrengthenKeyPepper, ReplyKeyPepper);
			}
			var encPart = Asn1DerDecoder.DecodeTlv<EncASRepPart>(
				replyKey.Decrypt(KeyUsage.AsrepEncPart, asrep.enc_part)
				).Value;

			asrepKey = replyKey;
			return encPart;
		}

		private ConcurrentDictionary<string, string> _realmMapping = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		private async Task<TicketInfo> GetTgt(
			string realm,
			KerberosCredential? credential,
			CancellationToken cancellationToken)
		{
			var spn = new ServicePrincipalName(PrincipalNameType.ServiceInstance, ServiceClassNames.Krbtgt, realm);
			var ticket = this.TicketCache.GetTicketFromCache(spn, credential?.UserName.UserName);
			if (ticket == null)
			{
				if (this._realmMapping.TryGetValue(realm, out string? mapped))
				{
					spn = new ServicePrincipalName(PrincipalNameType.ServiceInstance, ServiceClassNames.Krbtgt, mapped);
					ticket = this.TicketCache.GetTicketFromCache(spn, credential?.UserName.UserName);
				}
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
				if (!string.Equals(ticket.TicketRealm, realm))
				{
					// Realm may have been canonicalized; set mapping
					this._realmMapping[realm] = ticket.TicketRealm;
				}
				return ticket;
			}
		}

		public async Task<TicketInfo> GetTicketAsync(
			SecurityPrincipalName targetSpn,
			string realm,
			KerberosCredential credential,
			TicketParameters? ticketParameters,
			CancellationToken cancellationToken
			)
		{
			ArgumentNullException.ThrowIfNull(targetSpn);
			ArgumentException.ThrowIfNullOrEmpty(realm);
			ArgumentNullException.ThrowIfNull(credential);

			bool cacheEligible = (ticketParameters.AdditionalTicket is null);
			var ticket = !cacheEligible ? null : this.TicketCache.GetTicketFromCache(targetSpn, credential.UserName.UserName);
			if (ticket != null)
				return ticket;


			var tgt = await this.GetTgt(realm, credential, cancellationToken).ConfigureAwait(false);
			if (ticketParameters != null)
				ticketParameters.EndTime = tgt.EndTime;
			else
				ticketParameters ??= GetDefaultTicketOptions(tgt);

			ticket = await this.RequestTicket(
				tgt,
				targetSpn,
				tgt.TicketRealm,
				null,
				ticketParameters,
				cancellationToken).ConfigureAwait(false);

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
			if (ticketParameters != null && ticketParameters.S4ProxyService != null)
			{
				// This is a S4U2proxy request

				if (ticketParameters.S4ProxyService != spn)
				{
					if (ticketParameters.addlTicketStruc == null)
					{
						// The caller did not provide a ticket.
						// Request a ticket for the user to the proxy service using S4U2self

						var proxyTicket = await RequestTicket(
							tgt,
							ticketParameters.S4ProxyService,
							realm,
							encTypes,
							ticketParameters,
							cancellationToken
							).ConfigureAwait(false);
						ticketParameters.AdditionalTicket = proxyTicket;
					}
					ticketParameters.Options |= KdcOptions.CNameInAddlTicket;
				}
				else
				{
					// This is a request for the S4U2self ticket prior to the S4U2proxy request
				}
			}

			var ticket = await RequestTicketCore(tgt, spn, realm, encTypes, ticketParameters, cancellationToken).ConfigureAwait(false);
			if (ticket.IsTgt)
			{
				HashSet<string> referralNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				referralNames.Add(realm);
				// If the request is itself for a TGT, add the target domain
				if (spn.NamePartCount == 2 && ServiceClassNames.Krbtgt.Equals(spn.GetNamePart(0), StringComparison.OrdinalIgnoreCase))
					referralNames.Add(spn.GetNamePart(1));

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
		private async Task<TicketInfo> RequestTicketCore(
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

			bool usingSubkey = tgt.SessionKey.EType != EType.Rc4Hmac;
			var sessionKey = usingSubkey ? tgt.GenerateSessionKey() : tgt.SessionKey;

			KerberosNullCredential cred = new KerberosNullCredential(new UserPrincipalName(tgt.ClientName, tgt.TicketRealm));
			TicketRequestContext context = new TicketRequestContext(ticketParameters, null, cred.CreatePreauthContext(this, this._callback), sessionKey, usingSubkey)
			{
				Tgt = tgt,
				ArmorSubkey = (ticketParameters.ArmorTicket?.GenerateSessionKey() ?? null)
			};

			var tgsreq = this.CreateTgsReq(spn, tgt, realm, encTypes, context);

			this._callback?.OnRequestingTicket(spn, tgt, ticketParameters);

			var rep = await this.TransceiveKdcAsync(realm, string.Equals(realm, tgt.ClientRealm) ? LocateKdcOptions.Home : LocateKdcOptions.None, tgsreq, cancellationToken).ConfigureAwait(false);

			if (rep.SelectedChoice == KDC_REP_CHOICE.ChoiceIndex.Tgsrep)
				return ProcessTgsRep(rep.Tgsrep, ticketParameters, context);
			else if (rep.SelectedChoice == KDC_REP_CHOICE.ChoiceIndex.Error)
				throw rep.Error.GetException();
			else
				throw new SecurityException(Messages.Krb5_NoTGSRep);
		}

		internal TicketInfo ProcessTgsRep(
			KDC_REP rep,
			TicketParameters ticketParams,
			TicketRequestContext context)
		{
			context.preauth.ArmorKey = context.ArmorKey;
			context.preauth.TryProcessPadata(ticketParams.CorrelationId, rep.padata);

			var replyKey = context.SessionKey;
			SessionKey? armorStrengthenKey = context.preauth.ArmorStrengthenKey;
			if (armorStrengthenKey != null)
			{
				replyKey = KrbFxCf2(armorStrengthenKey.EncryptionProfile, armorStrengthenKey, replyKey, StrengthenKeyPepper, ReplyKeyPepper);
			}

			var encPart = this.ExtractTgsEncPart(rep, replyKey, context.usingSubkey).Value;
			if (encPart.nonce != context.nonce)
				throw new SecurityException("The nonce in the TGS-REP does not match the nonce sent in the TGS-REQ.");

			if (encPart.padata != null)
				context.preauth.TryProcessPadata(ticketParams.CorrelationId, encPart.padata);

			SessionKey? ticketKey = (0 != (ticketParams.Options & KdcOptions.EncTicketInSKey)) ? ticketParams.AdditionalTicket?.SessionKey : null;
			TicketInfo ticketInfo = new TicketInfo(GetNextTicketSeqnbr(), rep.ticket, this.CreateSessionKeyFor(encPart.key), encPart, rep.cname.name_string[0].Value, rep.crealm.Value, context.Tgt?.AsrepKey, ticketKey);
			ticketInfo.Comment = context.ticketParameters?.TicketComment;

			this._callback?.OnReceivedTicket(ticketParams.CorrelationId, new KdcRepInfo(context, rep, encPart, null, ticketInfo.SessionKey));

			return ticketInfo;
		}

		private Asn1Explicit<EncKDCRepPart> ExtractTgsEncPart(
			KDC_REP rep,
			SessionKey tgtSessionKey,
			bool usingSubkey)
		{
			var encPart = Asn1DerDecoder.DecodeTlv<EncTGSRepPart>(tgtSessionKey.Decrypt(usingSubkey ? KeyUsage.TgsrepEncPart_AuthSubkeyKey : KeyUsage.TgsrepEncPart_SessionKey, rep.enc_part));
			var kdcOptions = (KdcOptions)encPart.Value.flags.ToUInt32();

			return encPart;
		}

		internal static KDC_REQ_CHOICE ParseRequestPdu(ReadOnlyMemory<byte> pduBytes)
		{
			return Asn1DerDecoder.DecodeTlv<KDC_REQ_CHOICE>(pduBytes.Slice(4));
		}

		internal static KDC_REP_CHOICE ParseReplyPdu(ReadOnlyMemory<byte> pduBytes)
		{
			return Asn1DerDecoder.DecodeTlv<KDC_REP_CHOICE>(pduBytes.Slice(4));
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

		public const KdcOptions DefaultU2uTicketOptions = 0
			| KdcOptions.Forwardable
			| KdcOptions.Renewable
			| KdcOptions.Canonicalize
			| KdcOptions.EncTicketInSKey
			;

		// Matches Windows 11
		private static readonly EType[] defaultEtypes = new EType[]
		{
			EType.Aes256CtsHmacSha1_96,
			EType.Aes128CtsHmacSha1_96,
			EType.Rc4Hmac,
			EType.DesCbcMd5
		};

		public EType[] DefaultETypes { get; set; } = defaultEtypes;

		public TicketParameters GetDefaultTgtParameters()
		{
			DateTime till = GetDefaultEndTime();
			return new TicketParameters()
			{
				Options = DefaultTgtOptions,
				EndTime = till,
				RenewTill = till
			};
		}

		public static DateTime GetDefaultEndTime()
		{
			return DateTime.UtcNow + TimeSpan.FromHours(12);
		}

		public TicketParameters GetDefaultTicketOptions(TicketInfo? tgt)
		{
			return new TicketParameters()
			{
				EndTime = tgt?.EndTime,
				Options = DefaultTicketOptions,
			};
		}

		private KDC_REQ_CHOICE CreateASReq(
			TicketRequestContext context,
			PreauthContext preauth,
			KDC_REQ_BODY reqBody
			)
		{
			var credential = context.credential;

			var padataList = preauth.BuildPadataList(reqBody);
			padataList.Add(Structs.PAData_PacOptions(PacOptions.Claims));

			if (context.ticketParameters?.ArmorTicket != null)
			{
				var armorSubkey = context.ArmorSubkey;
				var ticketParameters = context.ticketParameters;

				var armorKey = KrbFxCf2(armorSubkey.EncryptionProfile, armorSubkey, ticketParameters.ArmorTicket.SessionKey, SubkeyArmorPepper, TicketArmorPepper);
				context.ArmorKey = armorKey;
				context.preauth.ArmorKey = armorKey;

				var armorTicket = context.ticketParameters.ArmorTicket;
				var reqBodyBytes = Asn1DerEncoder.EncodeTlv(reqBody);
				padataList = [Structs.PAData_FastReq(new PA_FX_FAST_REQUEST() {
					Armored_data =new KrbFastArmoredReq(
						armorKey.Checksum(KeyUsage.FastReqChecksum, reqBodyBytes.Span),
						armorKey.EncryptAndWrap(KeyUsage.FastEnc, Asn1DerEncoder.EncodeTlv(new KrbFastReq(new Asn1BitString(0U), padataList.ToArray(), reqBody)).Span),
						new KrbFastArmor(1, Asn1DerEncoder.EncodeTlv(Structs.APReq(
								(0 != (ticketParameters.Options & KdcOptions.EncTicketInSKey)) ? APOptions.UseSessionKey : APOptions.None,
								armorTicket.ticket,
								armorTicket.SessionKey.EncryptAndWrap(
									KeyUsage.ApreqAuth_AppSessionKey_IncludesAuthSubkey,
									Asn1DerEncoder.EncodeTlv(Structs.Authenticator(
										Structs.PrincipalName(PrincipalNameType.Principal, armorTicket.ClientName),
										armorTicket.ClientRealm,
										null,
										context.now,
										0,
										armorSubkey?.key
										)).Span)
							)).ToArray()))
					})];
			}

			KDC_REQ_CHOICE req = new KDC_REQ_CHOICE
			{
				Asreq = Structs.ASReq(
					padataList.ToArray(),
					reqBody
					)
			};

			return req;
		}

		private KerberosV5Spec2.HostAddress[]? MakeHostAddress() => (this.Workstation != null) ? [this.Workstation.ToKrb5HostAddress()] : null;

		private static Checksum ComputeChecksum(ReadOnlySpan<byte> message)
		{
			var cksum = SlimHashAlgorithm.ComputeHash<Md5Context>(message);
			return Structs.Checksum(EncChecksumType.RsaMd5, cksum);
		}

		private KDC_REQ_CHOICE CreateTgsReq(
			SecurityPrincipalName spn,
			TicketInfo ticket,
			string realm,
			EType[]? etypes,
			TicketRequestContext context
			)
		{
			var ticketParameters = context.ticketParameters;
			bool useArmor = ticketParameters.ArmorTicket != null;

			var cname = Structs.PrincipalName(PrincipalNameType.Principal, ticket.ClientName);

			// Windows uses the same value for nonce and seqnbr
			int seqnbr = context.nonce;// GenerateNonce();
			var encAuthzData = (ticketParameters.AuthorizationData != null)
				? context.SessionKey.EncryptAndWrap(context.usingSubkey ? KeyUsage.TgsReq_KdcReqBody_AuthData_AuthSubkey : KeyUsage.TgsReq_KdcReqBody_AuthData_SessionKey, ticketParameters.AuthorizationData)
				: null;

			KDC_REQ_BODY reqBody = Structs.KdcReqBody(
				ticketParameters,
				null, //(ticketParameters.S4UserName is null) ? null : ticketParameters.S4UserName.PrincipalName(),// null, //cname,
				realm,
				Structs.PrincipalName(spn),
				context.nonce,
				(etypes == null) ? this.GetAllETypes() : Array.ConvertAll(etypes, r => (int)r),
				null,
				encAuthzData
				);

			var pacOptions = PacOptions.BranchAware;

			List<PA_DATA> padatas = new(3);

			if (ticketParameters.IndicatesS4User)
			{
				if (ticketParameters.addlTicketStruc == null)
				{
					string s4uRealm = ticketParameters.S4UserName?.Realm ?? ticket.ClientRealm;

					// [MS-SFU] § 2.2.1

					pacOptions |= PacOptions.Claims;

					const string KerberosPackageName = "Kerberos";

					// Compose s4uByteArray
					const PrincipalNameType nameType = PrincipalNameType.Principal;
					string s4uString = ticketParameters.S4UserName + s4uRealm + KerberosPackageName;
					var byteCount = 4 + Encoding.UTF8.GetByteCount(s4uString);
					byte[] s4uByteArray = new byte[byteCount];
					BinaryPrimitives.WriteInt32LittleEndian(s4uByteArray, (int)nameType);
					Encoding.UTF8.GetBytes(s4uString, s4uByteArray.Slice(4));

					// [MS-SFU] § 2.2.2 - PA_S4U_X509_USER
					// [MS-SFU] <4> - According to the spec, the X509 structure is included even if no X509 certificate is provided
					// TODO: Verify the above with a PCAP
					// NOTE: Although the spec says the session key of the TGT is used, this is ONLY true if there is no subkey
					S4UUserID userId = new(
						context.nonce,
						s4uRealm,
						(ticketParameters.S4UserName is null) ? null : Structs.PrincipalName(PrincipalNameType.Principal, ticketParameters.S4UserName?.UserName),
						ticketParameters.S4UserCertificate?.GetRawCertData(),
						null);
					var userIdBytes = Asn1DerEncoder.EncodeTlv(userId).ToArray();

					Checksum cksum;
					if (context.SessionKey.EType is EType.Rc4Hmac or EType.Rc4HmacExp)
					{
						Md4Context md4 = new Md4Context();
						md4.Initialize();
						md4.HashData(userIdBytes);

						var hash = new byte[md4.DigestSizeBytes];
						md4.HashFinal(hash);
						cksum = new Checksum((int)EncChecksumType.RsaMd4, hash);
					}
					else
					{
						cksum = context.SessionKey.Checksum(KeyUsage.X509Checksum, userIdBytes);
					}

					var s4uCert = new PA_S4U_X509_USER(userId, cksum);
					padatas.Add(Structs.PAData(PadataType.S4u2Self_X509User, Asn1DerEncoder.EncodeTlv(s4uCert).ToArray()));
					if (ticketParameters.S4UserCertificate is null)
					{
						var cksumBytes = Rc4Hmac.Hash(context.SessionKey.KeyBytes, (int)KrbMessageType.PaForUser, s4uByteArray);
						var s4u = new PA_FOR_USER(Structs.PrincipalName(nameType, ticketParameters.S4UserName.UserName), s4uRealm, Structs.Checksum(EncChecksumType.HmacMd5String, cksumBytes), "Kerberos");
						padatas.Add(Structs.PAData(PadataType.S4u2Self_PaForUser, Asn1DerEncoder.EncodeTlv(s4u).ToArray()));
					}

					// [MS-SFU] § 2.2.5
					pacOptions |= PacOptions.ResourceBasedConstrainedDelegation;
				}
				else
				{
				}
			}

			if (!useArmor)
				padatas.Add(Kerberos.Structs.PAData_PacOptions(pacOptions));

			AP_REQ apreqOuter = Structs.APReq(
				(0 != (ticketParameters.Options & KdcOptions.EncTicketInSKey)) ? APOptions.UseSessionKey : APOptions.None,
				ticket.ticket,
				ticket.SessionKey.EncryptAndWrap(
					KeyUsage.TgsreqPatgsreqPadataApreqAuthChecksum_TgsSessionKey_IncludesAuthSubkey,
					Asn1DerEncoder.EncodeTlv(Structs.Authenticator(
						cname,
						ticket.ClientRealm,
						ComputeChecksum(Asn1DerEncoder.EncodeTlv(reqBody).Span),
						context.now,
						seqnbr,
						context.usingSubkey ? context.SessionKey.key : null
						)).Span)
				);
			PA_DATA padataApreq = Structs.PAData_APReq(apreqOuter);
			padatas.Add(padataApreq);

			// [RFC 6113] § 5.4.2. FAST Request
			if (useArmor)
			{
				var armorSubkey = context.ArmorSubkey;
				var armorTicket = ticketParameters.ArmorTicket;

				// New APREQ
				context.now = new KerberosTime(context.now.AsDateTime() + TimeSpan.FromMicroseconds(1));

				// [MS-KILE] § 3.3.5.7.4 Compound Identity
				var armorKey = KrbFxCf2(armorSubkey.EncryptionProfile, armorSubkey, ticketParameters.ArmorTicket.SessionKey, SubkeyArmorPepper, TicketArmorPepper);
				armorKey = KrbFxCf2(armorSubkey.EncryptionProfile, armorKey, context.SessionKey, ExplicitArmorPepper, TgsArmorPepper);
				context.ArmorKey = armorKey;

				var armoredPadatas = new List<PA_DATA>();
				armoredPadatas.Add(Structs.PAData_PacOptions(pacOptions | PacOptions.Claims));
				PA_FX_FAST_REQUEST padata_fastreq = new PA_FX_FAST_REQUEST()
				{
					Armored_data = new KrbFastArmoredReq(
						armorKey.Checksum(KeyUsage.FastReqChecksum, padataApreq.padata_value),
						armorKey.EncryptAndWrap(KeyUsage.FastEnc, Asn1DerEncoder.EncodeTlv(new KrbFastReq(new Asn1BitString(0U), armoredPadatas.ToArray(), reqBody)).Span),
						new KrbFastArmor(1, Asn1DerEncoder.EncodeTlv(Structs.APReq(
							(0 != (ticketParameters.Options & KdcOptions.EncTicketInSKey)) ? APOptions.UseSessionKey : APOptions.None,
							armorTicket.ticket,
							armorTicket.SessionKey.EncryptAndWrap(
								KeyUsage.ApreqAuth_AppSessionKey_IncludesAuthSubkey,
								Asn1DerEncoder.EncodeTlv(Structs.Authenticator(
									Structs.PrincipalName(PrincipalNameType.Principal, armorTicket.ClientName),
									armorTicket.ClientRealm,
									null,
									context.now,
									seqnbr,
									armorSubkey?.key
									)).Span)
							)).ToArray()))
				};
				padatas.Add(Structs.PAData_FastReq(padata_fastreq));
			}

			// padatas.Add(Structs.PAData_KerbKeyListReq([EType.Rc4Hmac]));




			KDC_REQ_CHOICE req = new KDC_REQ_CHOICE
			{
				Tgsreq = Structs.TgsReq(padatas.ToArray(), reqBody)
			};

			return req;
		}

		private readonly static byte[] SubkeyArmorPepper = Encoding.UTF8.GetBytes("subkeyarmor");
		private readonly static byte[] TicketArmorPepper = Encoding.UTF8.GetBytes("ticketarmor");
		private readonly static byte[] ExplicitArmorPepper = Encoding.UTF8.GetBytes("explicitarmor");
		private readonly static byte[] TgsArmorPepper = Encoding.UTF8.GetBytes("tgsarmor");
		private readonly static byte[] StrengthenKeyPepper = Encoding.UTF8.GetBytes("strengthenkey");
		private readonly static byte[] ReplyKeyPepper = Encoding.UTF8.GetBytes("replykey");

		internal static AP_REQ CreateAPReq(
			TicketInfo ticket,
			SecurityPrincipalName spn,
			EncryptionKey subkey,
			KerberosTime now,
			int initialSeqNbr,
			APOptions options,
			SecurityCapabilities caps,
			ChannelBinding? channelBinding,
			DelegationToken? delegationToken
			)
		{
			// Use from ticket instead of credentials
			var cname = Structs.PrincipalName(PrincipalNameType.Principal, ticket.ClientName);
			string crealm = ticket.ClientRealm;

			Guid channelBind = new Guid();
			if (channelBinding != null)
			{
				var bytes = channelBinding.GetBytes();
				var hash = Md5.ComputeHash<Md5Context>(bytes);
				channelBind = new Guid(hash);
			}

			//var reqBodyBytes = Asn1DerEncoder.EncodeTlv(reqBody);
			var authenticator = Structs.Authenticator(
				cname,
				crealm,
				new Checksum(
					// [RFC 4121] § 4.1.1 - Authenticator Checksum
					AuthChecksumToken.ChecksumType,
					new AuthChecksumToken()
					{
						bindLength = 0x10,
						channelBind = channelBind,
						capabilities = caps,
						DelegationToken = delegationToken
					}.ToBytes()
				),
				now,
				initialSeqNbr,
				subkey
				);
			var enc_authenticator = ticket.SessionKey.EncryptTlv(
				KeyUsage.ApreqAuth_AppSessionKey_IncludesAuthSubkey,
				authenticator);

			var ticketStruc = ticket.ticket;
			if (spn != null && spn != ticket.TargetSpn)
				ticketStruc = new Ticket_Tagged1(ticketStruc.tkt_vno, ticketStruc.realm, Structs.PrincipalName(spn), ticketStruc.enc_part);

			AP_REQ apreq = Structs.APReq(
				options,
				ticketStruc,
				enc_authenticator
				);

			return apreq;
		}

		#region Changepw stuff
#if DEBUG
		internal void TestChangepw_dbg(
			KerberosCredential cred,
			byte[] asrepBytes,
			byte[] msgBytes)
		{
			var asrep = Asn1DerDecoder.DecodeTlv<AS_REP>(asrepBytes);

			var encProfile = this.GetEncProfile((EType)asrep.Value.enc_part.etype);
			var ltk = cred.DeriveProtocolKeyFor(encProfile, Encoding.UTF8.GetBytes("LUMON.INDmilchick"));
			var asrepEncPart = ltk.DecryptTlv<EncASRepPart>(KeyUsage.AsrepEncPart, asrep.Value.enc_part);

			var sessionKey = encProfile.CreateSessionKey(asrepEncPart.Value.key);

			var request = new ByteMemoryReader(msgBytes).ReadPduStruct<ChangepwMessage>();
			var apreq = Asn1DerDecoder.DecodeTlv<AP_REQ>(request.Apreqdata).Value;
			var auth = sessionKey.DecryptTlv<Authenticator>(KeyUsage.ApreqAuth_AppSessionKey_IncludesAuthSubkey, apreq.authenticator);
			KRB_PRIV_Tagged21 priv = Asn1DerDecoder.DecodeTlv<KRB_PRIV>(request.PrivMessage).Value;
			var subkey = encProfile.CreateSessionKey(auth.Value.subkey);
			var privContents = subkey.DecryptTlv<EncKrbPrivPart>(KeyUsage.Priv, priv.enc_part);

			Debug.Assert(privContents.Value.seq_number == auth.Value.seq_number);
		}
#endif

		public async Task ChangePassword(
			EndPoint kdcEP,
			TicketInfo ticket,
			KerberosCredential credential,
			string newPassword,
			HostAddress hostAddress,
			CancellationToken cancellationToken
			)
		{
			ArgumentNullException.ThrowIfNull(kdcEP);
			ArgumentNullException.ThrowIfNull(ticket);
			ArgumentNullException.ThrowIfNull(credential);
			ArgumentNullException.ThrowIfNull(newPassword);
			ArgumentNullException.ThrowIfNull(hostAddress);

			byte[] privData = Encoding.UTF8.GetBytes(newPassword);

			await this.EnsureTransport().SendChangepwRequest(
				this,
				kdcEP,
				privData,
				ChangepwMessage.ChangepwVersionNumber,
				new ChangepwRequest(credential, ticket),
				cancellationToken).ConfigureAwait(false);
		}

		public async Task SetPassword(
			EndPoint kdcEP,
			TicketInfo ticket,
			KerberosCredential credential,
			string newPassword,
			// NOTE: [RFC 3244] declares targname and targrealm as optional, although in practice this fails
			SecurityPrincipalName? targetAccount,
			string? targetRealm,
			HostAddress hostAddress,
			CancellationToken cancellationToken
			)
		{
			ArgumentNullException.ThrowIfNull(kdcEP);
			ArgumentNullException.ThrowIfNull(ticket);
			ArgumentNullException.ThrowIfNull(credential);
			ArgumentNullException.ThrowIfNull(newPassword);
			ArgumentNullException.ThrowIfNull(hostAddress);

			byte[] privData = Asn1DerEncoder.EncodeTlv(new ChangePasswdData(
				Encoding.UTF8.GetBytes(newPassword),
				targetAccount?.PrincipalName(),
				(targetRealm is null) ? default(GeneralString?) : targetRealm)).ToArray();

			await this.EnsureTransport().SendChangepwRequest(
				this,
				kdcEP,
				privData,
				ChangepwMessage.Win2kResetPasswordVersionNumber,
				new ChangepwRequest(credential, ticket),
				cancellationToken).ConfigureAwait(false);
		}

		#endregion

		/// <summary>
		/// Generates a 32-bit nonce.
		/// </summary>
		/// <returns>A nonce</returns>
		internal static int GenerateNonce()
		{
			// TODO: Ensure uniqueness

			Span<int> nonce = stackalloc int[1];
			EncProfile.GetRandomBytes(MemoryMarshal.AsBytes(nonce));
			var n = nonce[0];
			n |= (1 << 31);
			return n;
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
			var encPart = ExtractASRepEncPart(asrep.Asrep, credential.CreatePreauthContext(this, null), out var asrepKey);
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
		public byte[] ExportTickets(IReadOnlyList<TicketInfo> tickets, KerberosFileFormat format)
		{
			ArgumentNullException.ThrowIfNull(tickets);

			return format switch
			{
				KerberosFileFormat.Kirbi => ExportKirbi(tickets),
				KerberosFileFormat.Ccache => ExportCcacheBytes(tickets),
			};
		}

		internal static byte[] ExportKirbi(IReadOnlyList<TicketInfo> tickets)
		{
			Ticket_Tagged1[] asnTickets = new Ticket_Tagged1[tickets.Count];
			KrbCredInfo[] encParts = new KrbCredInfo[tickets.Count];

			for (int i = 0; i < tickets.Count; i++)
			{
				var ticket = tickets[i];
				asnTickets[i] = ticket.ticket;
				encParts[i] = new KrbCredInfo(
					ticket.SessionKey.key,
					new GeneralString(ticket.ClientRealm),
					Structs.PrincipalName(
						PrincipalNameType.Principal,
						new GeneralString(ticket.ClientName)
					),
					new Asn1BitString(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((int)ticket.KdcOptions)), 0),
					null,
					ticket.StartTime,
					ticket.EndTime,
					ticket.RenewTill,
					new GeneralString(ticket.ServiceRealm),
					Structs.PrincipalName(ticket.TargetSpn)
				// TODO caddr
				);
			}

			EncKrbCredPart encPart = new EncKrbCredPart(new EncKrbCredPart_Tagged29(
				encParts
				));
			var encPartBytes = Asn1DerEncoder.EncodeTlv(encPart).ToArray();
			var krbcred = new KRB_CRED(new KRB_CRED_Tagged22(
				5,
				(int)KrbMessageType.Cred,
				asnTickets,
				new EncryptedData(0, encPartBytes)
			));
			var krbcredBytes = Asn1DerEncoder.EncodeTlv(krbcred).ToArray();
			return krbcredBytes;
		}

		private byte[] ExportCcacheBytes(IReadOnlyList<TicketInfo> tickets)
		{
			List<CCacheCredential> creds = new List<CCacheCredential>(tickets.Count);
			foreach (var ticket in tickets)
			{
				ToCCacheCred(ticket, creds);
			}

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
					credentials = creds.ToArray()
				},
			};

			ByteWriter writer = new ByteWriter();
			writer.WritePduStruct(ccache);
			return writer.GetData().ToArray();
		}

		private void ToCCacheCred(TicketInfo ticket, List<CCacheCredential> credList)
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
				authData = GetCcacheAuthData(ticket),
				ticket = new CCacheData(Asn1DerEncoder.EncodeTlv(new Ticket(ticket.ticket)).ToArray()),
				ticket2 = new CCacheData(Array.Empty<byte>())
			};
			credList.Add(cred);
			var configs = ticket.GetConfigEntries();
			if (configs != null)
			{
				credList.AddRange(configs);
			}
		}

		private static CCacheAuthData[] GetCcacheAuthData(TicketInfo ticket)
		{
			var authData = ticket.Padata?.Select(r => new CCacheAuthData((PadataType)r.padata_type, r.padata_value))?.ToList() ?? [];

			{
				List<PA_DATA> suppItems = new List<PA_DATA>();

				if (ticket.AsrepKey != null)
					suppItems.Add(new PA_DATA((int)SupplementalPadataType.AsrepKey, Asn1DerEncoder.EncodeTlv(ticket.AsrepKey.key).ToArray()));
				if (ticket.TicketKey != null)
					suppItems.Add(new PA_DATA((int)SupplementalPadataType.TicketKey, Asn1DerEncoder.EncodeTlv(ticket.TicketKey.key).ToArray()));
				if (ticket.Comment != null)
					suppItems.Add(new PA_DATA((int)SupplementalPadataType.TicketComment, Encoding.UTF8.GetBytes(ticket.Comment)));

				if (suppItems.Count > 0)
				{
					var suppBytes = Asn1DerEncoder.EncodeTlv(new Asn1SequenceOf<PA_DATA>(suppItems.ToArray()));
					byte[] suppPadataBytes = new byte[4 + suppBytes.Length];
					BinaryPrimitives.WriteUInt32LittleEndian(suppPadataBytes, SupplementalPadata.Signature);
					suppBytes.Span.CopyTo(suppPadataBytes.AsSpan(4));

					authData.Add(new CCacheAuthData(PadataType.PasswordSalt, suppPadataBytes));
				}
			}

			return authData.ToArray();
		}

		[Obsolete("Use IFileAccess to read the file.", true)]
		public TicketInfo[] LoadTicketsFromFile(string sourceFileName, out KerberosFileFormat format)
		{
			ArgumentException.ThrowIfNullOrEmpty(sourceFileName);
			return this.LoadTicketsFromFile(File.ReadAllBytes(sourceFileName), sourceFileName, out format);
		}
		/// <summary>
		/// Loads tickets from file data.
		/// </summary>
		/// <param name="fileBytes">File bytes</param>
		/// <param name="sourceFileName">Name of source file</param>
		/// <param name="format">Format of the file</param>
		/// <returns>An array of <see cref="TicketInfo"/> loaded from <paramref name="fileBytes"/>.</returns>
		/// <exception cref="ArgumentException"><paramref name="fileBytes"/> is empty</exception>
		/// <remarks>
		/// This method does not interact directly with the file system.  <paramref name="sourceFileName"/> is only used to populate <see cref="TicketInfo.SourceFileName"/>.
		/// </remarks>
		public TicketInfo[] LoadTicketsFromFile(byte[] fileBytes, string? sourceFileName, out KerberosFileFormat format)
		{
			ArgumentNullException.ThrowIfNull(fileBytes);
			if (fileBytes.Length == 0)
				throw new ArgumentException("The byte array is empty.", nameof(fileBytes));

			if (fileBytes[0] == 0x76)
			{
				var tickets = this.LoadTicketsFromKirbiFile(fileBytes);
				format = KerberosFileFormat.Kirbi;
				return tickets;
			}
			else if (fileBytes[0] == 0x05)
			{
				var tickets = this.LoadTicketsFromCcacheFile(fileBytes, sourceFileName);
				format = KerberosFileFormat.Ccache;
				return tickets.ToArray();
			}
			else
				throw new ArgumentException("The file format is not supported.");
		}
		private TicketInfo[] LoadTicketsFromKirbiFile(byte[] kirbiBytes)
		{
			var krbcred = Asn1DerDecoder.DecodeTlv<KRB_CRED>(kirbiBytes);
			var encPart = Asn1DerDecoder.DecodeTlv<EncKrbCredPart>(krbcred.Value.enc_part.cipher);

			var ticketCount = krbcred.Value.tickets.Length;
			TicketInfo[] tickets = new TicketInfo[ticketCount];
			for (int i = 0; i < tickets.Length; i++)
			{
				var ticket = krbcred.Value.tickets[i];
				var encPartInfo = encPart.Value.ticket_info[i];

				TicketInfo ticketInfo = new TicketInfo(
					GetNextTicketSeqnbr(),
					ticket,
					this.CreateSessionKeyFor(encPartInfo.key),
					encPartInfo
					);

				tickets[i] = ticketInfo;
			}

			return tickets;
		}
		private IList<TicketInfo> LoadTicketsFromCcacheFile(byte[] ccacheBytes, string? sourceFileName)
		{
			if (ccacheBytes.Length < 2)
				throw new InvalidDataException("The file is not a valid .ccache file.");

			var version = ccacheBytes[1];
			if ((uint)(version - 1) >= (uint)4)
				throw new NotSupportedException($"The .ccache file appears to be a version not supported by this implementation.");

			ByteMemoryReader reader = new ByteMemoryReader(ccacheBytes);
			var ccache = reader.ReadPduStruct<CCache>();

			if (ccache.credList.credentials == null)
				return [];

			List<TicketInfo> tickets = new List<TicketInfo>(ccache.credList.credentials.Length);

			// Configuration entries are attached to the preceeding tickt
			// This isn't strictly correct, as the position of a configuration
			// entry isn't specified, but has been observed in the limited cases of
			// a ccache containing configuration entries and enables this implementation to rewrite a ccache file without losing configuration entries
			TicketInfo? lastTicket = null;
			foreach (var cred in ccache.credList.credentials)
			{
				if (cred.IsConfigurationEntry)
				{
					if (lastTicket != null)
						lastTicket.AddConfigEntry(cred);
				}
				else
				{
					var key = this.CreateSessionKeyFor(cred.key.encType, cred.key.keyData.bytes, true);

					TicketInfo info = new TicketInfo(sourceFileName, GetNextTicketSeqnbr(), key, cred, this);
					lastTicket = info;
					tickets.Add(info);
				}
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

		private TicketInfo BuildTicket(
			KdcOptions options,
			SecurityPrincipalName clientName,
			string clientRealm,
			string ticketRealm,
			SecurityPrincipalName targetSpn,
			string targetRealm,
			DateTime authTime,
			DateTime endTime,
			DateTime? startTime,
			DateTime? renewTill,
			SessionKey sessionKey,
			SessionKey ticketKey,
			AuthorizationData_Element[] adElems
			)
		{
			EncTicketPart encTicketPart = new EncTicketPart(new EncTicketPart_Tagged3(
				new Asn1BitString((uint)options),
				sessionKey.key,
				clientRealm,
				Structs.PrincipalName(clientName),
				new TransitedEncoding(0, []),
				authTime,
				endTime,
				startTime,
				renewTill,
				null,
				adElems
				));
			byte[] encData = Asn1DerEncoder.EncodeTlv(encTicketPart).ToArray();
			var encTicketBytes = ticketKey.EncryptAndWrap(KeyUsage.Asrep_Tgsrep_Ticket, encData);

			EncKDCRepPart encRepPart = new EncKDCRepPart(
				sessionKey.key,
				[],
				0,
				new Asn1BitString((uint)options),
				authTime,
				endTime,
				targetRealm,
				Structs.PrincipalName(targetSpn),
				null,
				startTime,
				renewTill,
				[],
				[]
				);
			TicketInfo ticket = new TicketInfo(0, new Ticket_Tagged1(5, ticketRealm, Structs.PrincipalName(targetSpn), encTicketBytes), sessionKey, encRepPart, clientName.GetNamePart(0), clientRealm, null, ticketKey);

#if DEBUG
			//ticket.DecryptAuthorizationData(ticketKey, null);
#endif
			return ticket;
		}
		public TicketInfo ForgeTicket(
			KdcOptions options,
			SecurityPrincipalName clientName,
			string clientRealm,
			string ticketRealm,
			SecurityPrincipalName targetSpn,
			string targetRealm,
			SessionKey sessionKey,
			SessionKey serverKey,
			DateTime authTime,
			DateTime endTime,
			DateTime? startTime,
			DateTime? renewTill,
			LogonInfo? logonInfo,
			UpnDnsInfo? upnDnsInfo,
			SessionKey? kdcKey
			)
		{
			byte[]? ticketChecksum;
			if (kdcKey != null)
			{
				var adelem = new AuthorizationData_Element((int)AdType.IfRelevant, Asn1DerEncoder.EncodeTlv(new Asn1SequenceOf<AuthorizationData_Element>([new AuthorizationData_Element((int)AdType.Pac, [0])])).ToArray());

				var tempTicket = BuildTicket(
					options,
					clientName,
					clientRealm,
					ticketRealm,
					targetSpn,
					targetRealm,
					authTime,
					endTime,
					startTime,
					renewTill,
					sessionKey,
					serverKey,
					[adelem]
					);
				var tempTicketBytes = Asn1DerEncoder.EncodeTlv(tempTicket.ticket);
				ticketChecksum = kdcKey.Checksum(KeyUsage.NonKerbChecksumSalt, tempTicketBytes.Span).checksum;
			}
			else
				ticketChecksum = null;

			{
				kdcKey ??= serverKey;
				var pacBytes = TicketAuthorizationData.BuildPac(
					authTime,
					logonInfo,
					upnDnsInfo,
					serverKey,
					kdcKey,
					kdcKey.EncryptionProfile.ChecksumType,
					ticketChecksum
					);
				var adelem = new AuthorizationData_Element((int)AdType.IfRelevant, Asn1DerEncoder.EncodeTlv(new Asn1SequenceOf<AuthorizationData_Element>([new AuthorizationData_Element((int)AdType.Pac, pacBytes)])).ToArray());
#if DEBUG
				var newPac = new TicketAuthorizationData();
				newPac.Process(adelem, serverKey, null, false);
#endif

				var ticket = BuildTicket(
					options,
					clientName,
					clientRealm,
					ticketRealm,
					targetSpn,
					targetRealm,
					authTime,
					endTime,
					startTime,
					renewTill,
					sessionKey,
					serverKey,
					[adelem]
					);

				return ticket;
			}
		}
	}

	internal class KerbTrace
	{
		private readonly KerberosClient krb;
		private readonly KerberosCredential credential;
		private readonly IKerberosCallback? callback;
		private KdcOptions tgsKdcOptions;

		internal KerbTrace(KerberosClient kerb, KerberosCredential credential, IKerberosCallback? callback = null)
		{
			this.krb = kerb;
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
		public Authenticator Tgsreq_auth { get; private set; }
		public TicketInfo Ticket { get; private set; }
		public SessionKey TicketSessionKey { get; internal set; }
		#endregion
		#region AP-REQ
		public AP_REQ Apreq { get; private set; }
		public Authenticator Apreq_auth { get; private set; }
		#endregion
		#region AP-REP
		public AP_REP Aprep { get; private set; }
		public EncAPRepPart Aprep_auth { get; private set; }
		public SessionKey AcceptorSubkey { get; private set; }
		public uint RecvSeqNbr { get; private set; }
		#endregion
		#region AP-REP2
		public AP_REP Aprep2 { get; private set; }
		public EncAPRepPart Aprep2_auth { get; private set; }
		public int SendSeqNbr { get; private set; }
		#endregion

		internal void TraceAsreq(byte[] asreqBytes)
		{
			var asreq = Asn1DerDecoder.DecodeTlv<KDC_REQ>(asreqBytes);
			this.Asreq = asreq;
			var authTarget = asreq.req_body.sname.name_string;
			this.AuthService = authTarget[0].Value;
			this.AuthRealm = authTarget[1].Value;
			this.AuthNonce = asreq.req_body.nonce;

			this.callback?.OnRequestingTgt(this.AuthRealm, this.credential, new TicketParameters(), this.AuthNonce);
		}
		internal void TraceAsrep(byte[] asrepBytes)
			=> this.TraceAsrep(asrepBytes, this.AuthService, this.AuthRealm, this.AuthNonce);
		internal void TraceAsrep(
			byte[] asrepBytes,
			string authService,
			string authRealm,
			int authNonce)
		{
			var asrep = Asn1DerDecoder.DecodeTlv<KDC_REP>(asrepBytes);
			this.Asrep = asrep;
			var tgt = this.krb.ProcessASRep(Guid.Empty, asrep, new TicketRequestContext(null, credential, credential.CreatePreauthContext(this.krb, this.callback), null, false)
			{ nonce = authNonce }, DateTime.Now);
			this.Tgt = tgt;
			this.TgtSessionKey = tgt.SessionKey;
		}

		internal void TraceTgsreq(byte[] tgsreqBytes)
			=> this.TraceTgsreq(tgsreqBytes, this.TgtSessionKey);
		internal void TraceTgsreq(byte[] tgsreqBytes, SessionKey tgtSessionKey)
		{
			var tgsreq = Asn1DerDecoder.DecodeTlv<KDC_REQ>(tgsreqBytes);
			this.Tgsreq = tgsreq;
			this.TgsNonce = tgsreq.req_body.nonce;

			this.TargetSpn = tgsreq.req_body.sname.ToSecurityPrincipalName();

			var tgsreq_options = (KdcOptions)tgsreq.req_body.kdc_options.ToUInt32();
			AP_REQ? tgsreq_apreq = null;
			Authenticator? tgsreq_auth = null;
			foreach (var padata in tgsreq.padata)
			{
				switch ((PadataType)padata.padata_type)
				{
					case PadataType.TgsReq:
						tgsreq_apreq = Asn1DerDecoder.DecodeTlv<AP_REQ>(padata.padata_value);
						tgsreq_auth = tgtSessionKey.DecryptTlv<Authenticator>(
							KeyUsage.TgsreqPatgsreqPadataApreqAuthChecksum_TgsSessionKey_IncludesAuthSubkey,
							tgsreq_apreq.Value.authenticator);
						break;
				}
			}

			this.callback?.OnRequestingTicket(this.TargetSpn, this.Tgt, new TicketParameters() { Options = tgsreq_options });
		}

		internal void TraceTgsrep(byte[] tgsrepBytes)
			=> TraceTgsrep(tgsrepBytes, this.TargetSpn, this.TgsNonce, this.TgtSessionKey);
		internal void TraceTgsrep(byte[] tgsrepBytes, SecurityPrincipalName spn, int tgsNonce, SessionKey tgtSessionKey)
		{
			var tgsrep = Asn1DerDecoder.DecodeTlv<KDC_REP>(tgsrepBytes);
			var ticket = this.krb.ProcessTgsRep(tgsrep, new TicketParameters(), new TicketRequestContext(null, credential, credential.CreatePreauthContext(this.krb, this.callback), tgtSessionKey, false)
			{ nonce = TgsNonce });
			this.Ticket = ticket;
			this.TicketSessionKey = ticket.SessionKey;
		}

		internal void TraceApreq(byte[] apreqBytes)
			=> this.TraceApreq(apreqBytes, this.TicketSessionKey);
		internal void TraceApreq(byte[] apreqBytes, SessionKey ticketSessionKey)
		{
			this.TicketSessionKey = ticketSessionKey;
			var apreq_ = Asn1DerDecoder.DecodeTlv<AP_REQ>(apreqBytes);
			var apreq_auth = Asn1DerDecoder.DecodeTlv<Authenticator>(ticketSessionKey.Decrypt(KeyUsage.ApreqAuth_AppSessionKey_IncludesAuthSubkey, apreq_.Value.authenticator));

			this.SendSeqNbr = apreq_auth.Value.seq_number.Value;
			if (apreq_auth.Value.authorization_data != null)
			{
				foreach (var authData in apreq_auth.Value.authorization_data)
				{
					switch (authData.ad_type)
					{
						case 1:
							{
								//var subauth = Asn1DerDecoder.DecodeTlv<Asn1Explicit<Unnamed_0>>(authData.ad_data);
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
				)
			{
				ByteMemoryReader reader = new ByteMemoryReader(apreq_auth.Value.cksum.checksum);
				var token = reader.ReadPduStruct<AuthChecksumToken>();
				gssFlags = token.capabilities;
			}

			this.callback?.OnSendingApreq(Guid.Empty, null, this.TargetSpn, null, this.credential, gssFlags, ticketSessionKey, this.SendSeqNbr);
		}

		internal void TraceAprep(byte[] aprepBytes,
			byte[]? mechList, byte[]? acceptorMechListMic
			)
			=> this.TraceAprep(aprepBytes, this.TicketSessionKey, mechList, acceptorMechListMic);
		internal void TraceAprep(byte[] aprepBytes, SessionKey ticketSessionKey,
			byte[]? mechList, byte[]? acceptorMechListMic
			)
		{
			var aprep_ = Asn1DerDecoder.DecodeTlv<AP_REP>(aprepBytes);
			var aprep_auth = Asn1DerDecoder.DecodeTlv<EncAPRepPart>(ticketSessionKey.Decrypt(KeyUsage.APRep_EncPart, aprep_.Value.enc_part));
			var acceptorSubkey = this.krb.CreateSessionKeyFor(aprep_auth.Value.subkey);
			this.AcceptorSubkey = acceptorSubkey;
			this.RecvSeqNbr = aprep_auth.Value.seq_number ?? 0;

			this.callback?.OnReceivedAprep(Guid.Empty, null, this.RecvSeqNbr, acceptorSubkey);

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
		internal void TraceAprep2(byte[] aprep2Bytes, SessionKey ticketSessionKey, SessionKey acceptorSubkey, int sendSeqNbr, byte[] mechList, byte[] initiatorMechListMic)
		{
			var aprep2_ = Asn1DerDecoder.DecodeTlv<AP_REP>(aprep2Bytes);
			var aprep2_auth = Asn1DerDecoder.DecodeTlv<EncAPRepPart>(ticketSessionKey.Decrypt(KeyUsage.APRep_EncPart, aprep2_.Value.enc_part));
			acceptorSubkey.VerifySignature(
				KeyUsage.InitiatorSign,
				(uint)sendSeqNbr,
				WrapFlags.AcceptorSubkey,
				new MessageVerifyParams(initiatorMechListMic, SecBufferList.Create(SecBuffer.Integrity(mechList)))
				);
		}

		internal void TraceReq(byte[] req0)
			=> this.TraceReq(req0, this.AcceptorSubkey, this.SendSeqNbr);
		internal void TraceReq(byte[] req0, SessionKey acceptorSubkey, int sendSeqNbr)
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
			Span<byte> wrapToken = buffer.Slice(cbFrag - authLength, authLength);
			acceptorSubkey.UnsealMessage(
				KeyUsage.InitiatorSeal,
				(uint)(sendSeqNbr + 1),
				WrapFlags.AcceptorSubkey | WrapFlags.Sealed,
				new MessageSealParams(
					wrapToken,
					SecBufferList.Create(
						SecBuffer.Integrity(rpcHeader),
						SecBuffer.PrivacyWithIntegrity(stubData),
						SecBuffer.Integrity(authTrailer)
					),
					default
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
		public static KerberosClient CreateKerberosClient(this IServiceProvider services, IKdcLocator? locator = null)
		{
			var callback = services.GetService<IKerberosCallback>();
			if (callback == null)
			{
				var log = services.GetService<ILog>();
				if (log != null)
					callback = new KerberosDiagnosticLogger(log);
			}
			if (locator is null)
				locator = services.GetService<IKdcLocator>();

			var transport = services.GetService<IKerberosTransport>();
			if (transport is null)
			{
				ISocketService? socketService = services.GetService<ISocketService>();
				if (socketService != null)
					transport = new KerberosSocketTransport(socketService);
			}

			return new KerberosClient(locator, transport, callback);
		}
	}

	internal class TicketRequestContext
	{
		internal TicketRequestContext(
			TicketParameters? ticketParameters,
			KerberosCredential? credential,
			PreauthContext preauth,
			SessionKey sessionKey,
			bool usingSubkey
			)
		{
			this.nonce = GenerateNonce();
			this.ticketParameters = ticketParameters;
			this.credential = credential;
			this.preauth = preauth;
			this.SessionKey = sessionKey;
			this.usingSubkey = usingSubkey;
			this.now = KerberosTime.Now();
		}

		internal readonly TicketParameters? ticketParameters;
		internal readonly KerberosCredential? credential;
		internal readonly bool usingSubkey;

		internal SessionKey SessionKey { get; }
		internal SessionKey? ArmorSubkey { get; set; }

		internal int nonce;
		internal readonly PreauthContext preauth;
		internal KerberosTime now;

		public TicketInfo? Tgt { get; internal set; }
		public SessionKey ArmorKey { get; internal set; }
	}
}
