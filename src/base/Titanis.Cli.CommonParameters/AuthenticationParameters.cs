using PKIX1Implicit88;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Titanis.AuthProxy;
using Titanis.Certificates;
using Titanis.Cli;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Kerberos;
using Titanis.Security.Ntlm;
using Titanis.Security.Spnego;

namespace Titanis.Cli
{
	[TypeConverter(typeof(SpnMappingConverter))]
	public class SpnMapping
	{
		public SpnMapping(string? matchServiceClass, string? matchServiceInstance, string? replaceServiceClass, string? replaceServiceInstance)
		{
			MatchServiceClass = matchServiceClass;
			MatchServiceInstance = matchServiceInstance;
			ReplaceServiceClass = replaceServiceClass;
			ReplaceServiceInstance = replaceServiceInstance;
		}

		public string? MatchServiceClass { get; }
		public string? MatchServiceInstance { get; }
		public string? ReplaceServiceClass { get; }
		public string? ReplaceServiceInstance { get; }

		public override string ToString()
		{
			return $"{this.MatchServiceInstance ?? "*"}/{this.MatchServiceInstance ?? "*"} => {this.ReplaceServiceClass ?? "*"}/{this.ReplaceServiceInstance ?? "*"}";
		}

		public bool Matches(ServicePrincipalName spn)
		{
			ArgumentNullException.ThrowIfNull(spn);

			bool matches =
				(this.MatchServiceClass?.Equals(spn.ServiceClass, StringComparison.OrdinalIgnoreCase) ?? true)
				&& (this.MatchServiceInstance?.Equals(spn.ServiceInstance, StringComparison.OrdinalIgnoreCase) ?? true)
				;
			return matches;
		}
		public ServicePrincipalName Map(ServicePrincipalName spn)
		{
			string sc = this.ReplaceServiceClass ?? spn.ServiceClass;
			string si = this.ReplaceServiceInstance ?? spn.ServiceInstance;
			return new ServicePrincipalName(sc, si);
		}
	}
	class SpnMappingConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
			=> (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
			{
				var match = rgxMapping.Match(str);
				if (!match.Success)
					throw new ArgumentException($"The SPN mapping must be of the format <serviceClass>/<serviceInstance>=<serviceClass>/<serviceInstance>");

				var sc = match.Groups["sc"].Value;
				var si = match.Groups["si"].Value;
				var sc2 = match.Groups["sc2"].Value;
				var si2 = match.Groups["si2"].Value;

				return new SpnMapping(
					(sc == "*") ? null : sc, (si == "*") ? null : si,
					(sc2 == "*") ? null : sc2, (si2 == "*") ? null : si2
					);
			}
			return base.ConvertFrom(context, culture, value);
		}

		private static readonly Regex rgxMapping = new Regex(@"^(?<sc>[^/]*)/(?<si>.*)=(?<sc2>[^/]*)/(?<si2>.*)$");
	}

	/// <summary>
	/// Defines parameters for authentication.
	/// </summary>
	public class AuthenticationParameters : ParameterGroupBase
	{
		[Parameter]
		[Category(ParameterCategories.Authentication)]
		[Description("Uses anonymous login")]
		public SwitchParam Anonymous { get; set; }

		private UserPrincipalName? _userName;
		[Parameter]
		[Alias("u")]
		[Description("User name to authenticate with, not including the domain")]
		[Category(ParameterCategories.Authentication)]
		public UserPrincipalName? UserName { get => _userName; set => _userName = value; }

		[Parameter]
		[Alias("ud")]
		[Description("Domain of user to authenticate with")]
		[Category(ParameterCategories.Authentication)]
		public string? UserDomain { get; set; }

		[Parameter]
		[Alias("p", "pwd")]
		[Description("Password to authenticate with")]
		[Category(ParameterCategories.Authentication)]
		public string? Password { get; set; }

		[Parameter]
		[Description("NTLM hash for NTLM authentication")]
		[Category(ParameterCategories.Authentication)]
		[Placeholder("hexadecimal hash")]
		public HexString? NtlmHash { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("AES key (128 or 256)")]
		public HexString? AesKey { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("DES key")]
		public HexString? DesKey { get; set; }

		[Parameter]
		[Description("Name of workstation to send with NTLM authentication")]
		[Alias("w")]
		[Category(ParameterCategories.AuthenticationNtlm)]
		public string? Workstation { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of file containing a ticket-granting ticket (.kirbi or ccache)")]
		public string? Tgt { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of file containing service tickets (.kirbi or ccache)")]
		public string[]? Tickets { get; set; }

		[Parameter(EnvironmentVariable = KerberosClient.Krb5CacheVariableName)]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Name of ticket cache file")]
		public string? TicketCache { get; set; }

		[Parameter]
		[Description("NTLM version number (a.b.c.d)")]
		[Category(ParameterCategories.AuthenticationNtlm)]
		public Version? NtlmVersion { get; set; }

		[ParameterGroup]
		public UserCertificateParameterGroup? CertificateParameters { get; set; }

		[Parameter]
		[Description("KDC endpoint")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[DefaultPort(KerberosClient.KdcTcpPort)]
		[Placeholder("host-or-ip:port")]
		[TypeConverter(typeof(EndPointConverter))]
		public EndPoint? Kdc { get; set; }

		[Parameter]
		[Description("Name of user to impersonate with S4U")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		public UserPrincipalName? S4UserName { get; set; }

		[Parameter]
		[Description("User name to request TGT for U2U")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		public SecurityPrincipalName? U2UserName { get; set; }

		[Parameter]
		[Description("Name of file containing a certificate of a user to impersonate with S4U")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		public string? S4UserCert { get; set; }

		[Parameter]
		[Description("Name of service to proxy through")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		public SecurityPrincipalName? S4ProxyService { get; set; }

		[Parameter]
		[Description("Specifies an SPN override")]
		public SpnMapping[]? SpnOverride { get; set; }

		#region AuthProxy
		[Parameter]
		[Description("Endpoint of auth proxy")]
		public EndPoint AuthProxy { get; set; }
		#endregion

		/// <summary>
		/// Gets a value indicating whether the user provided Kerberos parameters.
		/// </summary>
		public bool HasKerberosInfo { get; private set; }
		/// <summary>
		/// Gets a value indicating whether the user provided NTLM parameters.
		/// </summary>
		public bool HasNtlmInfo { get; private set; }
		public bool HasAuthProxy => this.AuthProxy != null;
		/// <summary>
		/// Gets a value indicating whether the user provided authentication parameters.
		/// </summary>
		public bool HasAuthInfo => this.HasKerberosInfo | this.HasNtlmInfo | this.HasAuthProxy;

		private bool _validated;

		/// <summary>
		/// Validates authentication parameters.
		/// </summary>
		/// <param name="isRequired"><see langword="true"/> if authentication is required</param>
		/// <param name="context">Validation context</param>
		public void Validate(bool isRequired, ParameterValidationContext context, bool requiresKerberos = false)
		{
			var log = this.Services?.GetService<ILog>();

			this._userCert = this.CertificateParameters?.Validate(context, log, ref this._userName);

			if (string.IsNullOrEmpty(this.UserDomain) && this.UserName != null)
			{
				if (!string.IsNullOrEmpty(this.UserName.Realm))
					this.UserDomain = this.UserName.Realm;
			}

			// Check for Kerberos credentials
			bool hasKerbCred =
				// A ticket
				(this.Tickets != null || this.TicketCache != null)
				|| (
					// A TGT With KDC
					(this.Tgt != null || this.TicketCache != null)
					&& (this.Kdc is not null)
					)
				|| (
					// Username and credential
					(this.UserName is not null)
					&& !string.IsNullOrEmpty(this.UserDomain)
					&& (
						(this.Password != null)
						|| (this.NtlmHash != null)
						|| (this.AesKey != null)
						|| (this.DesKey != null)
						|| (this._userCert != null)
						)
				);
			this.HasKerberosInfo = hasKerbCred;
			if (!hasKerbCred && this.Kdc is not null)
				log?.WriteWarning($"-Kdc option specified but not enough options specified for Kerberos; Kerberos will not be used.");


			// For methods that require Kerberos, ensure  -Kdc is present
			if (this.S4UserName is not null || this.S4UserCert is not null || (this._userCert != null))
			{
				if (this.Kdc is null)
				{
					if (this.S4UserName is not null)
						context.LogError(new ParameterValidationError(nameof(S4UserName), $"-{nameof(S4UserName)} requires -{nameof(Kdc)}"));
					if (this.S4UserCert is not null)
						context.LogError(new ParameterValidationError(nameof(S4UserCert), $"-{nameof(S4UserCert)} requires -{nameof(Kdc)}"));
					if (this._userCert != null)
						context.LogError(new ParameterValidationError(nameof(UserCertificateParameterGroup.UserCert), $"-{nameof(UserCertificateParameterGroup.UserCert)} requires -{nameof(Kdc)}"));
				}

				if (!string.IsNullOrEmpty(this.S4UserCert))
				{
					log?.WriteDiagnostic($"Loading user certificate from {this.S4UserCert}...");
					var certBytes = File.ReadAllBytes(this.S4UserCert);
					_s4UserCert = new X509Certificate2(certBytes);
					log?.WriteVerbose($"Loaded certificate for {_s4UserCert.Subject}");
				}
			}

			// Check for NTLM
			bool hasNtlm = false;
			if (this.UserName is not null)
			{
				hasNtlm = (this.Password is not null) || (this.NtlmHash is not null);

				// UNDONE: Not required
				//if (string.IsNullOrEmpty(this.UserDomain))
				//	context.LogError(nameof(UserDomain), "No user domain specified.  Specify a domain either with -UserDomain or as part of the user name.");
			}
			else if (this.Anonymous.IsSet)
			{
				hasNtlm = true;
			}
			this.HasNtlmInfo = hasNtlm;

			if (isRequired && !hasKerbCred && !hasNtlm && this.AuthProxy == null)
			{
				context.LogError(nameof(Anonymous), "No authentication specified.  Either provide a user name with -UserName, or specify -Anonymous to authenticate as anonymous.");
			}

			if (!hasKerbCred && requiresKerberos)
			{
				context.LogError("The command requires a Kerberos security context, but not enough information is available to build a Kerberos context.");
			}

			this._validated = true;
		}

		public static bool LoadCertificateAndKey(
			ICommandContext commandContext,
			string certFileName,
			string? keyFile,
			string? keyPassphrase,
			ILog? log,
			ParameterValidationContext validationContext,
			[NotNullWhen(true)] out X509Certificate2? cert,
			[NotNullWhen(true)] out X509Certificate2Collection? store,
			out UserPrincipalName? upn,
			[CallerArgumentExpression(nameof(keyFile))] string? keyFileParamName = null,
			[CallerArgumentExpression(nameof(keyPassphrase))] string? keyPassphraseName = null)
		{
			certFileName = commandContext.ResolveFsPath(certFileName);
			keyFile = string.IsNullOrEmpty(keyFile) ? null : commandContext.ResolveFsPath(keyFile);

			log?.WriteDiagnostic($"Opening certificate file {certFileName}");
			cert = null;
			store = null;
			upn = null;
			try
			{
				store = CertificateHelper.LoadFrom(certFileName, keyFile, keyPassphrase, true);

				var certsWithPrivateKey = store.Where(r => r.HasPrivateKey).ToList();
				if (certsWithPrivateKey.Count == 1)
				{
					cert = certsWithPrivateKey[0];
				}
				else
				{
					var certsWithClientAuth = certsWithPrivateKey.Where(r => r.HasEku(ExtendedKeyUsages.ClientAuthentication)).ToList();
					cert = certsWithClientAuth.Count >= 1 ? certsWithClientAuth[0] : null;
				}

				if (cert != null)
				{
					log?.WriteVerbose($"Selected certificate {cert.Subject}");

					foreach (var ext in cert.Extensions)
					{
						byte[]? subjectKeyId = null;
						if (ext is X509SubjectKeyIdentifierExtension keyIdExt)
							subjectKeyId = keyIdExt.SubjectKeyIdentifierBytes.ToArray();
						else if (ext is X509SubjectAlternativeNameExtension altName)
						{
							var decoded = SubjectAltName.TryReadFrom(altName.RawData);
							if (decoded != null)
							{
								upn = UserPrincipalName.Parse(decoded);
								break;
							}
						}
					}

					return true;
				}
				else
				{
					log?.WriteError($"None of the provided certificates have a private key and the Client Authentication ({ExtendedKeyUsages.ClientAuthentication}) EKU.  If the key is contained in a separate file, specify it with -{keyFileParamName}");
				}

				return false;
			}
			catch (CryptographicException ex) when (keyPassphrase is null)
			{
				validationContext.LogError($"Certificate file {certFileName} is encrypted.  Use -{keyPassphraseName} to specify the password to use to decrypt this file.");
				return false;
			}
		}

		public NtlmClientContext? TryCreateNtlmContext(ServicePrincipalName? targetSpn)
		{
			Debug.Assert(this._validated);

			if ((this.UserName is null) && !this.Anonymous.IsSet)
				return null;

			// Don't use NTLM in S4U or PKINIT scenarios
			if (this.S4UserName != null || this.S4UserCert != null || this.S4ProxyService != null || (this._userCert != null))
				return null;

			var domain = this.UserDomain;

			NtlmCredential? ntlmCred;
			if (this.Password != null)
			{
				ntlmCred = new NtlmPasswordCredential(this.UserName.UserName, this.UserDomain, this.Password);
			}
			else if (this.NtlmHash != null)
			{
				ntlmCred = new NtlmHashCredential(this.UserName.UserName, this.UserDomain, new Buffer128(), new Buffer128(this.NtlmHash.Bytes));
			}
			else if (this.Anonymous.IsSet)
				ntlmCred = NtlmCredential.Anonymous;
			else
				ntlmCred = null;

			if (ntlmCred != null)
			{
				var log = this.Services.GetService<ILog>();
				var ntlmContext = new NtlmClientContext(ntlmCred, true, callback: (log != null) ? new NtlmDiagnosticLogger(log, this.Owner?.GetCallback<INtlmClientCallback>()) : null)
				{
					Workstation = this.Workstation,
					WorkstationDomain = domain,
					TargetSpn = targetSpn,
				};

				return ntlmContext;
			}

			return null;
		}


		/// <summary>
		/// Creates an <see cref="AuthClientContext"/> using the configured parameters.
		/// </summary>
		/// <param name="spn">SPN of service</param>
		/// <param name="options"><see cref="AuthOptions"/> affection creation of context</param>
		/// <returns>The <see cref="AuthClientContext"/> configured with the parameters.</returns>
		/// <remarks>
		/// This method attempts to create both an NTLM and a Kerberos authentication context, if configured correctly.  If both contexts are available, they are wrapped in a <see cref="SpnegoClientContext"/>.  If only one context is created, it is returned directly, unless <paramref name="options"/> specifies <see cref="AuthOptions.PreferSpnego"/>, in which case it is wrapped.  Some protocols (such as SMB2) require SP-NEGO.
		/// <para>
		/// If <paramref name="spn"/> is missing, no Kerberos context is created.
		/// </para>
		/// </remarks>
		private AuthClientContext? CreateAuthContext(
			ServicePrincipalName? spn,
			SecurityCapabilities requiredCaps,
			AuthOptions options)
		{
			if (spn != null)
				spn = TryMapSpn(spn);

			if (this.AuthProxy != null)
			{
				var cancellationToken = CancellationToken.None;

				var sockService = this.Services.RequireService<ISocketService>();
				var socket = sockService.ConnectTcp(this.AuthProxy, cancellationToken).Result;

				var proxyContext = new AuthProxyClientContext(this.UserName?.ToString(), socket)
				{
					RequiredCapabilities = requiredCaps,
					TargetSpn = spn
				};
				return proxyContext;
			}

			int count = 0;

			// TODO: There is no guarantee that the parameters are valid.  Sure the CLI will validate them, but there is no guarantee that this invocation is from a CLI program
			bool canCreateKerberos = spn != null && !this.Anonymous.IsSet;
			KerberosClientContext? extraKerbContext = null;
			KerberosClientContextBase? krbContext = canCreateKerberos ? this.TryCreateKerberosContext(spn, requiredCaps, true, out extraKerbContext) : null;
			if (krbContext != null)
			{
				count = 2;
				Debug.Assert(extraKerbContext != null);

				krbContext.RequiredCapabilities |= requiredCaps;
				extraKerbContext.RequiredCapabilities |= requiredCaps;
			}

			// Create NTLM context based on parameters
			var ntlmContext = this.TryCreateNtlmContext(spn);
			if (ntlmContext != null)
			{
				count++;
				ntlmContext.RequiredCapabilities |= requiredCaps;
			}

			// Create SPNEGO context if appropriate
			if ((count > 1) || (0 != (options & AuthOptions.PreferSpnego)))
			{
				var authContext = new SpnegoClientContext()
				{
					TargetSpn = spn
				};
				if (krbContext != null)
					authContext.Contexts.Add(krbContext);
				if (extraKerbContext != null)
					authContext.Contexts.Add(extraKerbContext);
				if (ntlmContext != null)
					authContext.Contexts.Add(ntlmContext);

				return (authContext.Contexts.Count > 0) ? authContext : null;
			}
			else if (krbContext != null)
				return krbContext;
			else if (ntlmContext != null)
				return ntlmContext;
			else
				return null;
		}

		private ServicePrincipalName TryMapSpn(ServicePrincipalName spn)
		{
			if (this.SpnOverride != null)
			{
				foreach (var spnMapping in this.SpnOverride)
				{
					if (spnMapping.Matches(spn))
					{
						var log = this.Services.GetService<ILog>();
						var mappedSpn = spnMapping.Map(spn);
						log?.WriteVerbose($"Overriding SPN: {spn} => {mappedSpn}");
						return mappedSpn;
					}
				}
			}

			return spn;
		}

		private KerberosClient? _kerberosClient;
		private X509Certificate2 _s4UserCert;
		private X509Certificate2? _userCert;

		/// <summary>
		/// Creates a <see cref="KerberosClientContextBase"/>.
		/// </summary>
		/// <param name="targetSpn">Target SPN</param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public MskileClientContext? TryCreateKerberosContext(ServicePrincipalName targetSpn, SecurityCapabilities requiredCaps, bool wantExtra, out KerberosClientContext? extraContext)
		{
			ArgumentNullException.ThrowIfNull(targetSpn);
			// TODO: There is no guarantee that the parameters are valid.  Sure the CLI will validate them, but there is no guarantee that this invocation is from a CLI program

			var log = this.Services.GetService<ILog>();

			// Configure the Kerberos client
			var krb = this._kerberosClient;
			if (krb == null)
			{
				SimpleKdcLocator? kdcLocator = null;
				if (this.Kdc != null)
				{
					if (IPAddress.TryParse(targetSpn.ServiceInstance, out var _))
						log?.WriteWarning("The server name within the UNC path is an IP address.  This will probably result in Kerberos authentication failing.");

					kdcLocator = new(this.Kdc);
				}

				krb = this.Services.CreateKerberosClient(kdcLocator);
				if (!string.IsNullOrEmpty(this.Workstation))
					krb.Workstation = HostAddress.FromNetbiosName(this.Workstation);
				this._kerberosClient = krb;

				if (!string.IsNullOrEmpty(this.TicketCache))
				{
					// TODO: ResolveFsPath

					var cacheFileName = this.Owner.Context.ResolveFsPath(this.TicketCache);
					log?.WriteDiagnostic($"Loading ticket cache from {cacheFileName}.");
					// TODO: This doesn't match the search below, which checks user name.  Document the semantics of the ticket cache
					var ticketCache = new TicketCacheFile(cacheFileName, krb);
					krb.TicketCache = ticketCache;
			}
			}

			// Now start processing credentials
			TicketInfo? serviceTicket = null;

			// Name and realm of the user authenticating
			var authUser = this.UserName?.UserName;
			var authRealm = this.UserDomain;

			// Client name on service ticket (reflects impersonation/delegation)
			var effectiveUserName = this.S4UserName?.UserName ?? authUser;
			var effectiveUserRealm = this.S4UserName?.Realm ?? this.UserDomain;

			// Search the cache for a service ticket
			serviceTicket = krb.TicketCache.GetTicketFromCache(targetSpn, effectiveUserName);

			// Check for a matching ticket matching the target SPN and user name (if specified)
			if ((serviceTicket is null) && this.Tickets != null)
			{
				foreach (var ticketFileName_ in this.Tickets)
				{
					string ticketFileName = this.Owner.Context.ResolveFsPath(ticketFileName_);
					// TODO: Resolve file name
					log?.WriteVerbose($"Loading tickets from {ticketFileName}");
					var fileCache = new TicketCacheFile(ticketFileName, krb);
					log?.WriteVerbose($"Loaded {fileCache.TicketCount} tickets from {ticketFileName}");

					string? userDomain = this.UserDomain;
					var fileTickets = fileCache.GetAllTickets();
					foreach (var ticket in fileTickets)
					{
						if (serviceTicket is null)
						{
							if (CheckMatchingTicket(targetSpn, log, ticket, ref effectiveUserName, ref userDomain))
								serviceTicket = ticket;
						}

						// TODO: This will effectively import the ticket into the KRB5CCNAME file, which is not desirable
						krb.ImportTicket(ticket);
					}

					// No primary match, check alternate service classes
					if (serviceTicket is null)
					{
						ServicePrincipalName? matchingSpn = null;
						var altNames = new string[] { ServiceClassNames.RestrictedKrbHost, ServiceClassNames.HostU };
						foreach (var altClass in altNames)
						{
							var altSpn = targetSpn.WithServiceClass(altClass);
							foreach (var ticket in fileTickets)
							{
								if (CheckMatchingTicket(altSpn, log, ticket, ref effectiveUserName, ref userDomain))
									serviceTicket = ticket;

								if (serviceTicket is not null)
								{
									matchingSpn = altSpn;
									break;
								}
							}

							if (matchingSpn != null)
								break;
						}

						if (matchingSpn != null)
							targetSpn = matchingSpn;
					}
				}

				if (serviceTicket is not null)
				{
					effectiveUserName ??= serviceTicket.ClientName;
					effectiveUserName ??= serviceTicket.ClientRealm;
				}
			}

			// Note that effectiveUser is set iff a matching service ticket is found

			// Now process TGTs
			var tgtFileName = this.Tgt;
			TicketInfo? tgt = null;

			// TODO: This should really only be used if no auth user is specified
			if (krb.TicketCache.HomeTgt is not null)
			{
				tgt = krb.TicketCache.HomeTgt;
				authUser ??= krb.TicketCache.HomeTgt.ClientName;
				authRealm ??= krb.TicketCache.HomeTgt.ClientRealm;
			}
			// Check the -Tgt file
			if ((serviceTicket is null) && (tgt is null) && !string.IsNullOrEmpty(tgtFileName))
			{
				tgtFileName = this.Owner.Context.ResolveFsPath(tgtFileName);
				log?.WriteVerbose($"Loading ticket(s) from {tgtFileName}");
				var tgtCache = new TicketCacheFile(tgtFileName, krb);
				var tickets = tgtCache.GetAllTickets();
				foreach (var ticket in tickets)
				{
					log?.WriteVerbose($"Importing ticket for user {ticket.ClientName}@{ticket.ClientRealm} for {ticket.TargetSpn}");

					if (ticket.IsTgt)
					{
						if (!ticket.IsCurrent)
						{
							log?.WriteVerbose($"Skipping ticket because it is outside its validity dates.");
							continue;
						}

						if (
							(authUser == null || string.Equals(authUser, ticket.ClientName, StringComparison.OrdinalIgnoreCase))
							&& (authRealm == null || string.Equals(authRealm, ticket.ClientRealm, StringComparison.OrdinalIgnoreCase))
							)
						{
							if (authUser == null || authRealm == null)
							{
								// Adopt user info from ticket
								log?.WriteVerbose($"Using client name from TGT: {ticket.ClientName}@{ticket.ClientRealm}");
								authUser ??= ticket.ClientName;
								authRealm ??= ticket.ClientRealm;
							}
							tgt = ticket;
							krb.ImportTicket(ticket);
						}
					}
					else
					{
						log?.WriteWarning($"The TGT file contained a ticket that doesn't look like a TGT: {ticket.TargetSpn}.");
					}

				}
			}

			KerberosCredential? cred = null;
			if (!string.IsNullOrEmpty(authRealm) && !string.IsNullOrEmpty(authUser))
			{
				if (this.Password != null)
					cred = new KerberosPasswordCredential(authUser, authRealm, this.Password);
				else if (this.NtlmHash != null)
					cred = new KerberosKeyCredential(authUser, authRealm, EType.Rc4Hmac, this.NtlmHash.Bytes);
				else if (this.AesKey != null)
					cred = new KerberosKeyCredential(authUser, authRealm, this.AesKey.Bytes.Length switch
					{
						(128 / 8) => EType.Aes128CtsHmacSha1_96,
						(256 / 8) => EType.Aes256CtsHmacSha1_96,
						_ => throw new ArgumentException("The AES key is not the correct size for AES 128 or AES 256.")
					}, this.AesKey.Bytes);
				else if (this.DesKey != null)
					cred = new KerberosKeyCredential(authUser, authRealm, EType.DesCbcMd5, this.DesKey.Bytes);
				else if (this._userCert != null)
				{
					var upn = this.UserName;
					cred = new KerberosPkinitCredential(upn, this.UserDomain, this._userCert);
				}
			}

			// A credential is required regardless of whether it is used for authentication
			if (cred == null)
			{
				if ((serviceTicket is not null) || ((tgt is not null) && !string.IsNullOrEmpty(authRealm)))
					// Create a placeholder credential for the context
					cred = new KerberosNullCredential(authUser, authRealm ?? serviceTicket.ServiceRealm);
				else
					cred = null;
			}

			// A credential now exists iff the context has enough information to create a context

			if (serviceTicket is null && this.U2UserName is null)
			{
				// Get a ticket
				if (cred != null && (this.Kdc is not null))
				{

					// TODO: This should be truly asynchronous.
					try
					{
						var ticketParams = krb.GetDefaultTicketOptions(null);
						if (this.S4UserName != null || this._s4UserCert != null)
						{
							ticketParams.S4UserName = this.S4UserName;
							ticketParams.S4UserCertificate = this._s4UserCert;
							ticketParams.S4ProxyService = this.S4ProxyService;
						}

						serviceTicket = Task.Factory.StartNew(() => krb.GetTicketAsync(
							targetSpn,
							cred.Realm,
							cred,
							ticketParams,
							CancellationToken.None), TaskCreationOptions.LongRunning).Unwrap().Result;
					}
					catch (Exception ex)
					{
						log?.WriteWarning($"Unable to get Kerberos ticket for {targetSpn}: {ex.Message}");
					}
				}
				else
				{
					//if (!string.IsNullOrEmpty(this.Kdc))
					//	throw new InvalidOperationException("KDC option specified, but no suitable credentials were provided.");
				}
			}

			KerberosClientCred? clientCred;
			if (serviceTicket is not null)
				clientCred = serviceTicket;
			else if (this.U2UserName is not null)
				clientCred = this.U2UserName;
			else
				clientCred = null;

			if (clientCred is not null)
			{
				var logger = this.Services.GetService<IKerberosCallback>();
				var krbContext = new MskileClientContext(
					cred,
					this._kerberosClient,
					targetSpn,
					clientCred,
					callback: logger
					)
				{
					RequiredCapabilities = 0
						| SecurityCapabilities.MutualAuthentication
						| SecurityCapabilities.SequenceDetection
						| SecurityCapabilities.ReplayDetection
						| requiredCaps
				};
				extraContext = wantExtra ? new KerberosClientContext(
					cred,
					this._kerberosClient,
					targetSpn,
					clientCred,
					callback: logger
					)
				{
					RequiredCapabilities = 0
						| SecurityCapabilities.MutualAuthentication
						| SecurityCapabilities.SequenceDetection
						| SecurityCapabilities.ReplayDetection
						| requiredCaps
				} : null;
				return krbContext;
			}

			extraContext = null;
			return null;
		}

		private static bool CheckMatchingTicket(ServicePrincipalName targetSpn, ILog? log, TicketInfo ticket,
			ref string? userName,
			ref string? userRealm)
		{
			var matchesSpn = ticket.TargetSpn.Equals(targetSpn);
			if (matchesSpn)
			{
				if (
					(userName == null || string.Equals(userName, ticket.ClientName, StringComparison.OrdinalIgnoreCase))
					&& (userRealm == null || string.Equals(userRealm, ticket.ClientRealm, StringComparison.OrdinalIgnoreCase))
					)
				{
					if (userName == null || userRealm == null)
					{
						// Adopt user info from ticket
						log?.WriteVerbose($"Using UPN from ticket: {ticket.ClientName}@{ticket.ClientRealm}");
						userName ??= ticket.ClientName;
						userRealm ??= ticket.ClientRealm;
					}
					log?.WriteDiagnostic($"Selected ticket with UPN '{ticket.ClientName}@{ticket.ClientRealm}' and SPN '{ticket.TargetSpn}'.");
					return true;
				}
				else
				{
					log?.WriteDiagnostic($"Skipping ticket because UPN '{ticket.ClientName}@{ticket.ClientRealm}' doesn't match application-specified UPN of '{userName}@{userRealm}'.");
				}
			}
			else
			{
				log?.WriteDiagnostic($"Skipping ticket because ticket SPN '{ticket.TargetSpn}' doesn't match application-specified SPN of '{targetSpn}'.");
			}

			return false;
		}

		protected override void Initialize(Command owner, IServiceContainer services)
		{
			base.Initialize(owner, services);
			services.AddService(typeof(IClientCredentialService), this.CreateCredService);
			services.AddService(typeof(IKerberosCallback), this.CreateKerberosCallback);
		}

		private CredentialService? CreateCredService(IServiceContainer container, Type serviceType)
		{
			return new CredentialService(this);
		}

		private IKerberosCallback? CreateKerberosCallback(IServiceContainer container, Type serviceType)
		{
			var log = this.Services.GetService<ILog>();
			var logger = (log != null) ? new KerberosDiagnosticLogger(log, this.Owner?.GetCallback<IKerberosCallback>()) : null;
			return logger;
		}

		class CredentialService : ClientCredentialServiceBase
		{
			private readonly AuthenticationParameters authParams;

			internal CredentialService(AuthenticationParameters authParams)
			{
				this.authParams = authParams;
			}

			/// <inheritdoc/>
			public sealed override AuthClientContext? GetAuthContextForService(ServicePrincipalName spn, SecurityCapabilities requiredCaps, AuthOptions options)
			{
				ArgumentNullException.ThrowIfNull(spn);
				var authContext = this.authParams.CreateAuthContext(
					spn,
					requiredCaps,
					options);
				return authContext;
			}
		}
	}
}
