using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	[Flags]
	public enum AuthOptions
	{
		None = 0,

		PreferSpnego = 1,
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

		[Parameter]
		[Description("NTLM version number (a.b.c.d)")]
		[Category(ParameterCategories.AuthenticationNtlm)]
		public Version? NtlmVersion { get; set; }

		[Parameter]
		[Description("KDC address")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		public string? Kdc { get; set; }

		[Parameter]
		[Description("KDC port")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[DefaultValue(KerberosClient.KdcTcpPort)]
		public int KdcPort { get; set; }

		public void Validate(bool isRequired, ParameterValidationContext context, ILog log)
		{
			ArgumentNullException.ThrowIfNull(log);

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
				(this.Tickets != null)
				|| (
					// A TGT With KDC
					(this.Tgt != null)
					&& !string.IsNullOrEmpty(this.Kdc)
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
			if (!hasKerbCred && !string.IsNullOrEmpty(this.Kdc))
				log.WriteWarning($"\"KDC option specified but not enough options specified for Kerberos; Kerberos will not be used.\"");

			if ((uint)(this.KdcPort - 1) >= 65535)
				context.LogError(nameof(KdcPort), "Port must be [1-65535] inclusive.");


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

			if (isRequired && !hasKerbCred && !hasNtlm)
			{
				context.LogError(nameof(Anonymous), "No authentication specified.  Either provide a user name with -UserName, or specify -Anonymous to authenticate as anonymous.");
			}
		}

		public NtlmClientContext? TryCreateNtlmContext(
			ServicePrincipalName? targetSpn,
			ILog? log)
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
				var ntlmContext = new NtlmClientContext(ntlmCred, true, callback: (log != null) ? new NtlmDiagnosticLogger(log) : null)
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

		public SpnegoClientContext? CreateSpnegoAuthContext(
			ServicePrincipalName spn,
			SecurityCapabilities requiredCaps,
			AuthOptions options,
			ILog? logContext)
			=> (SpnegoClientContext?)this.CreateAuthContext(spn, options | AuthOptions.PreferSpnego, requiredCaps, logContext);

		/// <summary>
		/// Creates an <see cref="AuthClientContext"/> using the configured parameters.
		/// </summary>
		/// <param name="spn">SPN of service</param>
		/// <param name="options"><see cref="AuthOptions"/> affection creation of context</param>
		/// <param name="log"><see cref="ILog"/> for reporting problems</param>
		/// <returns>The <see cref="AuthClientContext"/> configured with the parameters.</returns>
		/// <remarks>
		/// This method attempts to create both an NTLM and a Kerberos authentication context, if configured correctly.  If both contexts are available, they are wrapped in a <see cref="SpnegoClientContext"/>.  If only one context is created, it is returned directly, unless <paramref name="options"/> specifies <see cref="AuthOptions.PreferSpnego"/>, in which case it is wrapped.  Some protocols (such as SMB2) require SP-NEGO.
		/// <para>
		/// If <paramref name="serverName"/> or <paramref name="serviceName"/> are missing, no Kerberos context is created.  <paramref name="serverName"/> should be a host name or FQDN, not an IP address.  If it is, a ticket is requested from the KDC.  This generally fails unless the domain is specifically configured with the IP address is mapped to a service account.
		/// </para>
		/// </remarks>
		public AuthClientContext? CreateAuthContext(
			ServicePrincipalName? spn,
			AuthOptions options,
			SecurityCapabilities requiredCaps,
			ILog? log
			)
		{
			// TODO: There is no guarantee that the parameters are valid.  Sure the CLI will validate them, but there is no guarantee that this invocation is from a CLI program
			bool canCreateKerberos = spn != null && !this.Anonymous.IsSet;
			KerberosClientContext? krbContext = canCreateKerberos ? this.TryCreateKerberosContext(spn, log) : null;
			if (krbContext != null)
			{
				krbContext.RequiredCapabilities |= requiredCaps;
			}

			// Create NTLM context based on parameters
			var ntlmContext = this.TryCreateNtlmContext(spn, log);
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
		/// <param name="log"><see cref="ILog"/> for reporting problems</param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public KerberosClientContext? TryCreateKerberosContext(ServicePrincipalName targetSpn, ILog? log)
		{
			ArgumentNullException.ThrowIfNull(targetSpn);
			// TODO: There is no guarantee that the parameters are valid.  Sure the CLI will validate them, but there is no guarantee that this invocation is from a CLI program

			KerberosClientContext? krbContext = null;
			var logger = (log != null) ? new KerberosDiagnosticLogger(log) : null;

			// Configure the Kerberos client
			var krb = this._kerberosClient;
			if (krb == null)
			{

				SimpleKdcLocator? kdcLocator = null;
				if (!string.IsNullOrEmpty(this.Kdc))
				{
					var port = this.KdcPort;

					if (IPAddress.TryParse(targetSpn.ServiceInstance, out var _))
						log?.WriteWarning("The server name within the UNC path is an IP address.  This will probably result in Kerberos authentication failing.");

					EndPoint kdcEP;
					if (IPAddress.TryParse(this.Kdc, out var kdcAddr))
						kdcEP = new IPEndPoint(kdcAddr, port);
					else
						kdcEP = new DnsEndPoint(this.Kdc, port);

					kdcLocator = new(kdcEP);
				}



				krb = new KerberosClient(kdcLocator, callback: logger);
				krb.Workstation = this.Workstation;
				this._kerberosClient = krb;
			}

			// Now start processing credentials

			// Start with tickets
			bool ticketsImported = false;
			bool foundMatchingTicket = false;
			if (this.Tickets != null)
			{
				foreach (var ticketFileName in this.Tickets)
				{
					log?.WriteVerbose($"Loading tickets from {ticketFileName}");
					var bytes = File.ReadAllBytes(ticketFileName);
					var fileTickets = this._kerberosClient.LoadTicketsFromFile(bytes, out _);
					log?.WriteVerbose($"Loaded {fileTickets.Length} from {ticketFileName}");

					string? userName = this.UserName;
					string? userDomain = this.UserDomain;
					foreach (var ticket in fileTickets)
					{
						if (!foundMatchingTicket)
						{
							foundMatchingTicket = FindMatchingTicket(targetSpn, log, ticket, ref userName, ref userDomain);
						}

						this._kerberosClient.ImportTicket(ticket);
					}

					// No primary match, check alternate service classes
					if (!foundMatchingTicket)
					{
						ServicePrincipalName? matchingSpn = null;
						var altNames = new string[] { ServiceClassNames.RestrictedKrbHost, ServiceClassNames.Host };
						foreach (var altClass in altNames)
						{
							var altSpn = targetSpn.WithServiceClass(altClass);
							foreach (var ticket in fileTickets)
							{
								foundMatchingTicket = FindMatchingTicket(altSpn, log, ticket, ref userName, ref userDomain);
								if (foundMatchingTicket)
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

					this.UserName = userName;
					this.UserDomain = userDomain;
				}
			}


			// Now process TGTs
			var tgtFileName = this.Tgt;
			bool foundTgt = false;
			if (!string.IsNullOrEmpty(tgtFileName))
			{
				log?.WriteVerbose($"Loading ticket(s) from {tgtFileName}");
				var tgtBytes = File.ReadAllBytes(tgtFileName);
				var tickets = krb.LoadTicketsFromFile(tgtBytes, out _);
				foreach (var ticket in tickets)
				{
					log?.WriteVerbose($"Importing ticket for user {ticket.UserName}@{ticket.UserRealm} for {ticket.ServiceClass}/{ticket.Host}");

					bool isTgt = ticket.ServiceClass.Equals(KerberosClient.TgsServiceClass, StringComparison.OrdinalIgnoreCase);
					if (isTgt)
					{
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
						}
					}
					else
					{
						log?.WriteWarning($"The TGT file contained a ticket that doesn't look like a TGT: {ticket.ServiceClass}/{ticket.Host}.");
					}

					if (ticket.StartTime > DateTime.UtcNow)
						log?.WriteWarning($"The ticket isn't valid until {ticket.StartTime} (UTC).");
					if (ticket.EndTime < DateTime.UtcNow)
						log?.WriteWarning($"The ticket expired at {ticket.StartTime} (UTC) and is no longer valid.");

					krb.ImportTicket(ticket);
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


			if (cred != null && (!string.IsNullOrEmpty(this.Kdc) || foundMatchingTicket))
			{
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
			var matchesSpn = (ticket.Spn == targetSpn);
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
					log.WriteDiagnostic($"Selected ticket with UPN '{ticket.UserName}@{ticket.UserRealm}' and SPN '{ticket.Spn}'.");
					return true;
				}
				else
				{
					log.WriteDiagnostic($"Skipping ticket because UPN '{ticket.UserName}@{ticket.UserRealm}' doesn't match application-specified UPN of '{userName}@{userRealm}'.");
				}
			}
			else
			{
				log.WriteDiagnostic($"Skipping ticket because ticket SPN '{ticket.Spn}' doesn't match application-specified SPN of '{targetSpn}'.");
			}

			return false;
		}

		public IClientCredentialService GetCredentialServiceFor(
			ServicePrincipalName targetSpn,
			SecurityCapabilities requiredCaps,
			ILog? log)
		{
			ArgumentNullException.ThrowIfNull(targetSpn);
			return new CredentialService(this, targetSpn, log, requiredCaps);
		}

		class CredentialService : IClientCredentialService
		{
			private readonly AuthenticationParameters authParams;
			private readonly ServicePrincipalName spn;
			private readonly ILog log;
			private readonly SecurityCapabilities requiredCaps;

			internal CredentialService(AuthenticationParameters authParams, ServicePrincipalName spn, ILog? log, SecurityCapabilities requiredCaps)
			{
				this.authParams = authParams;
				this.spn = spn;
				this.log = log;
				this.requiredCaps = requiredCaps;
			}

			AuthClientContext? IClientCredentialService.GetAuthContextForResource(string resourceType, object resourceKey)
			{
				var authContext = this.authParams.CreateAuthContext
					(this.spn,
					AuthOptions.None,
					this.requiredCaps,
					this.log
					);
				return authContext;
			}
		}
	}
}
