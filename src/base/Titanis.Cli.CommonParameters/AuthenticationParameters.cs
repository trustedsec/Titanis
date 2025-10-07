using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Kerberos;
using Titanis.Security.Ntlm;
using Titanis.Security.Spnego;

namespace Titanis.Cli
{
	/// <summary>
	/// Defines parameters for authentication.
	/// </summary>
	public class AuthenticationParameters : ParameterGroupBase
	{
		[Parameter]
		[Category(ParameterCategories.Authentication)]
		[Description("Uses anonymous login")]
		public SwitchParam Anonymous { get; set; }

		[Parameter]
		[Alias("u")]
		[Description("User name to authenticate with, not including the domain")]
		[Category(ParameterCategories.Authentication)]
		public string? UserName { get; set; }

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

		[Parameter]
		[Description("KDC endpoint")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[DefaultPort(KerberosClient.KdcTcpPort)]
		[Placeholder("host-or-ip:port")]
		[TypeConverter(typeof(EndPointConverter))]
		public EndPoint? Kdc { get; set; }

		/// <summary>
		/// Gets a value indicating whether the user provided Kerberos parameters.
		/// </summary>
		public bool HasKerberosInfo { get; private set; }
		/// <summary>
		/// Gets a value indicating whether the user provided NTLM parameters.
		/// </summary>
		public bool HasNtlmInfo { get; private set; }
		/// <summary>
		/// Gets a value indicating whether the user provided authentication parameters.
		/// </summary>
		public bool HasAuthInfo => this.HasKerberosInfo | this.HasNtlmInfo;

		/// <summary>
		/// Validates authentication parameters.
		/// </summary>
		/// <param name="isRequired"><see langword="true"/> if authentication is required</param>
		/// <param name="context">Validation context</param>
		public void Validate(bool isRequired, ParameterValidationContext context)
		{
			var log = this.Services?.GetService<ILog>();

			if (string.IsNullOrEmpty(this.UserDomain) && !string.IsNullOrEmpty(this.UserName))
			{
				int isep = this.UserName.IndexOfAny(new char[] { '\\', '@' });
				if (isep >= 0)
				{
					var sep = this.UserName[isep];
					if (isep == 0)
					{
						if (sep == '@')
							context.LogError(nameof(UserName), "The user name component before the @ is missing.");
						else if (sep == '\\')
							context.LogError(nameof(UserName), "The domain component before the \\ is missing.");
					}

					var part1 = this.UserName.Substring(0, isep);
					var part2 = this.UserName.Substring(isep + 1);
					switch (sep)
					{
						case '@':
							this.UserName = part1;
							this.UserDomain = part2;
							break;
						case '\\':
							this.UserDomain = part1;
							this.UserName = part2;
							break;
					}
				}
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
					!string.IsNullOrEmpty(this.UserName)
					&& !string.IsNullOrEmpty(this.UserDomain)
					&& (
						(this.Password != null))
						|| (this.NtlmHash != null)
						|| (this.AesKey != null)
				);
			this.HasKerberosInfo = hasKerbCred;
			if (!hasKerbCred && this.Kdc is not null)
				log?.WriteWarning($"-Kdc option specified but not enough options specified for Kerberos; Kerberos will not be used.");


			// Check for NTLM
			bool hasNtlm = false;
			if (!string.IsNullOrEmpty(this.UserName))
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

			if (isRequired && !hasKerbCred && !hasNtlm)
			{
				context.LogError(nameof(Anonymous), "No authentication specified.  Either provide a user name with -UserName, or specify -Anonymous to authenticate as anonymous.");
			}
		}

		public NtlmClientContext? TryCreateNtlmContext(ServicePrincipalName? targetSpn)
		{
			if (string.IsNullOrEmpty(this.UserName) && !this.Anonymous.IsSet)
				return null;

			var domain = this.UserDomain;

			NtlmCredential? ntlmCred;
			if (this.Password != null)
			{
				ntlmCred = new NtlmPasswordCredential(this.UserName, domain, this.Password);
			}
			else if (this.NtlmHash != null)
			{
				ntlmCred = new NtlmHashCredential(this.UserName, domain, new Buffer128(), new Buffer128(this.NtlmHash.Bytes));
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
					ClientChannelBindingsUnhashed = new byte[16]
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
			// TODO: There is no guarantee that the parameters are valid.  Sure the CLI will validate them, but there is no guarantee that this invocation is from a CLI program
			bool canCreateKerberos = spn != null && !this.Anonymous.IsSet;
			KerberosClientContext? krbContext = canCreateKerberos ? this.TryCreateKerberosContext(spn) : null;
			if (krbContext != null)
			{
				krbContext.RequiredCapabilities |= requiredCaps;
			}

			// Create NTLM context based on parameters
			var ntlmContext = this.TryCreateNtlmContext(spn);
			if (ntlmContext != null)
			{
				ntlmContext.RequiredCapabilities |= requiredCaps;
			}

			// Create SPNEGO context if appropriate
			if ((krbContext != null && ntlmContext != null) || (0 != (options & AuthOptions.PreferSpnego)))
			{
				var authContext = new SpnegoClientContext()
				{
					TargetSpn = spn
				};
				if (krbContext != null)
					authContext.Contexts.Add(krbContext);
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

		private KerberosClient? _kerberosClient;

		/// <summary>
		/// Creates a <see cref="KerberosClientContext"/>.
		/// </summary>
		/// <param name="targetSpn">Target SPN</param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public KerberosClientContext? TryCreateKerberosContext(ServicePrincipalName targetSpn)
		{
			ArgumentNullException.ThrowIfNull(targetSpn);
			// TODO: There is no guarantee that the parameters are valid.  Sure the CLI will validate them, but there is no guarantee that this invocation is from a CLI program

			KerberosClientContext? krbContext = null;
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
				krb.Workstation = this.Workstation;
				this._kerberosClient = krb;

				if (!string.IsNullOrEmpty(this.TicketCache))
				{
					// TODO: ResolveFsPath

					var cacheFileName = this.TicketCache;
					log?.WriteDiagnostic($"Loading ticket cache from {cacheFileName}.");
					// TODO: This doesn't match the search below, which checks user name.  Document the semantics of the ticket cache
					var ticketCache = new TicketCacheFile(cacheFileName, krb);
					krb.TicketCache = ticketCache;
			}
			}

			// Now start processing credentials

			bool foundMatchingTicket = false;

			// Start with cache
			{
				var ticket = this._kerberosClient.TicketCache.GetTicketFromCache(targetSpn);
				foundMatchingTicket = ticket != null;
			}

			// Check tickets
			if (!foundMatchingTicket && this.Tickets != null)
			{
				foreach (var ticketFileName in this.Tickets)
				{
					// TODO: Resolve file name
					log?.WriteVerbose($"Loading tickets from {ticketFileName}");
					var fileCache = new TicketCacheFile(ticketFileName, krb);
					log?.WriteVerbose($"Loaded {fileCache.TicketCount} from {ticketFileName}");

					string? userName = this.UserName;
					string? userDomain = this.UserDomain;
					var fileTickets = fileCache.GetAllTickets();
					foreach (var ticket in fileTickets)
					{
						if (!foundMatchingTicket)
						{
							foundMatchingTicket = FindMatchingTicket(targetSpn, log, ticket, ref userName, ref userDomain);
						}

						// TODO: This will effectively import the ticket into the KRB5CCNAME file, which is not desirable
						this._kerberosClient.ImportTicket(ticket);
					}

					// No primary match, check alternate service classes
					if (!foundMatchingTicket)
					{
						ServicePrincipalName? matchingSpn = null;
						var altNames = new string[] { ServiceClassNames.RestrictedKrbHost, ServiceClassNames.HostU };
						foreach (var altClass in altNames)
						{
							var altSpn = targetSpn.WithServiceClass(altClass);
							foreach (var ticket in fileTickets)
							{
								foundMatchingTicket = FindMatchingTicket(altSpn, log, ticket, ref userName, ref userDomain);
								if (foundMatchingTicket)
								{
									matchingSpn = altSpn;
									// TODO: Nothing was done with the found ticket
									break;
								}
							}

							if (matchingSpn != null)
								break;
						}

						if (matchingSpn != null)
							targetSpn = matchingSpn;
					}

					this.UserName = userName;
					this.UserDomain = userDomain;
				}
			}


			// Now process TGTs
			var tgtFileName = this.Tgt;
			bool foundTgt = false;
			if (krb.TicketCache.HomeTgt is not null)
			{
				foundTgt = true;
				this.UserName ??= krb.TicketCache.HomeTgt.UserName;
				this.UserDomain ??= krb.TicketCache.HomeTgt.ServiceRealm;
			}
			if (!foundMatchingTicket && !foundTgt && !string.IsNullOrEmpty(tgtFileName))
			{
				log?.WriteVerbose($"Loading ticket(s) from {tgtFileName}");
				var tgtCache = new TicketCacheFile(tgtFileName, krb);
				var tickets = tgtCache.GetAllTickets();
				foreach (var ticket in tickets)
				{
					log?.WriteVerbose($"Importing ticket for user {ticket.UserName}@{ticket.UserRealm} for {ticket.TargetSpn}");

					if (ticket.IsTgt)
					{
						if (!ticket.IsCurrent)
						{
							log?.WriteVerbose($"Skipping ticket because it is outside its validity dates.");
							continue;
						}

						if (
							(this.UserName == null || string.Equals(this.UserName, ticket.UserName, StringComparison.OrdinalIgnoreCase))
							&& (this.UserDomain == null || string.Equals(this.UserDomain, ticket.UserRealm, StringComparison.OrdinalIgnoreCase))
							)
						{
							if (this.UserName == null || this.UserDomain == null)
							{
								// Adopt user info from ticket
								log?.WriteVerbose($"Using UPN from TGT: {ticket.UserName}@{ticket.UserRealm}");
								this.UserName ??= ticket.UserName;
								this.UserDomain ??= ticket.UserRealm;
							}
							foundTgt = true;
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
			if (!string.IsNullOrEmpty(this.UserDomain))
			{
				if (this.Password != null)
					cred = new KerberosPasswordCredential(this.UserName, this.UserDomain, this.Password);
				else if (this.NtlmHash != null)
					cred = new KerberosKeyCredential(this.UserName, this.UserDomain, EType.Rc4Hmac, this.NtlmHash.Bytes);
				else if (this.AesKey != null)
					cred = new KerberosKeyCredential(this.UserName, this.UserDomain, this.AesKey.Bytes.Length switch
					{
						(128 / 8) => EType.Aes128CtsHmacSha1_96,
						(256 / 8) => EType.Aes256CtsHmacSha1_96,
						_ => throw new ArgumentException("The AES key is not the correct size for AES 128 or AES 256.")
					}, this.AesKey.Bytes);
			}
			if (cred == null)
			{
				if (foundMatchingTicket || (foundTgt && !string.IsNullOrEmpty(this.UserDomain)))
					cred = new KerberosNullCredential(this.UserName, this.UserDomain);
				else
					cred = null;
			}


			if (cred != null && ((this.Kdc is not null) || foundMatchingTicket))
			{
				var logger = this.Services.GetService<IKerberosCallback>();
				krbContext = new KerberosClientContext(
					cred,
					this._kerberosClient,
					targetSpn,
					this.UserDomain,
					callback: logger
					)
				{
					RequiredCapabilities = 0
						| SecurityCapabilities.MutualAuthentication | SecurityCapabilities.Integrity | SecurityCapabilities.Confidentiality | SecurityCapabilities.SequenceDetection | SecurityCapabilities.ReplayDetection
				};
			}
			else
			{
				//if (!string.IsNullOrEmpty(this.Kdc))
				//	throw new InvalidOperationException("KDC option specified, but no suitable credentials were provided.");
			}

			return krbContext;
		}

		private static bool FindMatchingTicket(ServicePrincipalName targetSpn, ILog? log, TicketInfo ticket,
			ref string? userName,
			ref string? userRealm)
		{
			var matchesSpn = ticket.TargetSpn.Equals(targetSpn);
			if (matchesSpn)
			{
				if (
					(userName == null || string.Equals(userName, ticket.UserName, StringComparison.OrdinalIgnoreCase))
					&& (userRealm == null || string.Equals(userRealm, ticket.UserRealm, StringComparison.OrdinalIgnoreCase))
					)
				{
					if (userName == null || userRealm == null)
					{
						// Adopt user info from ticket
						log?.WriteVerbose($"Using UPN from ticket: {ticket.UserName}@{ticket.UserRealm}");
						userName ??= ticket.UserName;
						userRealm ??= ticket.UserRealm;
					}
					log?.WriteDiagnostic($"Selected ticket with UPN '{ticket.UserName}@{ticket.UserRealm}' and SPN '{ticket.TargetSpn}'.");
					return true;
				}
				else
				{
					log?.WriteDiagnostic($"Skipping ticket because UPN '{ticket.UserName}@{ticket.UserRealm}' doesn't match application-specified UPN of '{userName}@{userRealm}'.");
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
				var authContext = this.authParams.CreateAuthContext
					(spn,
					requiredCaps
,
					options);
				return authContext;
			}
		}
	}
}
