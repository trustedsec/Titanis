using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Certificates;
using Titanis.Cli;
using Titanis.Ldap;
using Titanis.Net;

namespace Ldap
{
	/// <summary>
	/// Base class for commands using <see cref="LdapClient"/>.
	/// </summary>
	[OutputRecordType(typeof(LdapEntry), DefaultFields = new string[] { nameof(LdapEntry.EntryName) })]
	internal abstract class LdapCommandBase : Command
	{
		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public AuthenticationParameters AuthenticationParams { get; set; }

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public NetworkParameters NetworkParams { get; set; }

		[Parameter(10)]
		[Mandatory]
		[Description("Name of LDAP server")]
		public string ServerName { get; set; }

		[Parameter]
		[Description("Global Catalog server")]
		public SwitchParam Gc { get; set; }

		[Parameter]
		[Description("Use SSL")]
		public SwitchParam Ssl { get; set; }

		[Parameter]
		[Description("Name of PEM or PFX certificate file")]
		public string? SslCert { get; set; }

		[Parameter]
		[Description("Name of PFX file for SSL authentication")]
		public string? SslKeyFile { get; set; }

		[Parameter]
		[Description("Password for -SslCert or -SslKeyFile")]
		public string? SslKeyPassword { get; set; }

		protected abstract Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken);

		private X509Certificate2Collection? _sslCerts;
		private X509Certificate2? _sslCert;
		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			this.AuthenticationParams?.Validate(false, context);

			if (this.Ssl.IsSet)
			{
				if (this.SslCert != null)
				{
					this.Log.WriteDiagnostic($"Opening SSL certificate file {this.SslCert}");
					var certFileName = this.ResolveFsPath(this.SslCert);
					try
					{
						X509Certificate2Collection store = CertificateHelper.LoadFrom(this.SslCert, this.SslKeyFile, this.SslKeyPassword, true);

						var certsWithPrivateKey = store.Where(r => r.HasPrivateKey).ToList();
						if (certsWithPrivateKey.Count == 1)
						{
							this._sslCert = certsWithPrivateKey[0];
						}
						else
						{
							var certsWithClientAuth = certsWithPrivateKey.Where(r => r.HasEku(ExtendedKeyUsages.ClientAuthentication)).ToList();
							if (certsWithClientAuth.Count >= 1)
								this._sslCert = certsWithClientAuth[0];
						}

						if (this._sslCert != null)
						{
							this.WriteVerbose($"Selected certificate {this._sslCert.Subject}");
						}
						else
						{
							this.WriteError($"None of the provided certificates have a private key and the Client Authentication ({ExtendedKeyUsages.ClientAuthentication}) EKU");
						}

					}
					catch (CryptographicException ex) when (this.SslKeyPassword is null)
					{
						context.LogError($"Certificate file {certFileName} is encrypted.  Use -{nameof(SslKeyPassword)} to specify the password to use to decrypt this file.");
					}
				}
			}
			else
			{
				if (this.SslCert != null)
					context.LogError($"-{nameof(SslCert)} requires -{nameof(Ssl)}");
			}

			if (this.SslKeyPassword != null && this.SslCert is null)
				context.LogError($"-{nameof(SslKeyPassword)} may only be used with -{nameof(SslCert)}.");
		}

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			string serverName = this.ServerName;

			LdapClient ldap = await ConnectLdap(serverName, cancellationToken);
			return await RunAsync(ldap, cancellationToken);
		}

		protected async Task<LdapClient> ConnectLdap(string serverName, CancellationToken cancellationToken)
		{
			var port =
				this.Gc.IsSet ? (
					this.Ssl.IsSet ? LdapClient.GcLdapsPort
					: LdapClient.GcLdapPort
				) : (this.Ssl.IsSet ? LdapClient.LdapsPort
					: LdapClient.LdapPort)
				;

			SslClientAuthenticationOptions? options =
				this.Ssl.IsSet ? new SslClientAuthenticationOptions
				{
					TargetHost = serverName,
					RemoteCertificateValidationCallback = ValidateSsl,
					ClientCertificates = this._sslCerts,
					LocalCertificateSelectionCallback = ClientCertCallback
				} : null;
			var ldap = await LdapClient.Connect(new DnsEndPoint(serverName, port), options, this.RequireService<ISocketService>(), this.Services.RequireService<IClientCredentialService>(), cancellationToken);
			return ldap;
		}

		private X509Certificate ClientCertCallback(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate? remoteCertificate, string[] acceptableIssuers)
		{
			return this._sslCert;
		}

		private bool ValidateSsl(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
	}
}
