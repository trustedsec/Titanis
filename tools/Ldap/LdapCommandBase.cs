using System.ComponentModel;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Titanis.Ldap;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.Cli.LdapTool
{
	/// <summary>
	/// Base class for commands using <see cref="LdapClient"/>.
	/// </summary>
	[OutputRecordType(typeof(LdapEntry), DefaultFields = new string[] { nameof(LdapEntry.EntryName) })]
	public abstract class LdapCommandBase : Command, IHaveServerName, ISupportTreeOutput
	{
		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public AuthenticationParameters AuthenticationParams { get; set; }

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public NetworkParameters NetworkParams { get; set; }

		[Parameter(0)]
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
		[Advanced]
		[Description("Name of PEM or PFX certificate file")]
		public FileSpec? SslCert { get; set; }

		[Parameter]
		[Advanced]
		[Description("Name of PFX file for SSL authentication")]
		public FileSpec? SslKeyFile { get; set; }

		[Parameter]
		[Advanced]
		[Description("Password for -SslCert or -SslKeyFile")]
		public string? SslKeyPassword { get; set; }

		protected abstract Task<int> RunAsync(LdapClient ldap, CancellationToken cancellationToken);

		private X509Certificate2Collection? _sslCerts;
		private X509Certificate2? _sslCert;
		private UserPrincipalName? _sslUpn;

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			this.AuthenticationParams?.Validate(false, context);

			if (this.Ssl.IsSet)
			{
				if (this.SslCert != null)
				{
					AuthenticationParameters.LoadCertificateAndKey(
						this.FileAccessService,
						this.SslCert,
						this.SslKeyFile,
						SslKeyPassword,
						this.Log,
						context,
						out this._sslCert,
						out this._sslCerts,
						out this._sslUpn
						);
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

		TreeHandler ISupportTreeOutput.CreateTreeHandler()
		{
			return new TreeHandler<LdapDistinguishedName, LdapEntry>(r => r.EntryName, r => r.EntryName.GetParentName(), null, Comparer<LdapDistinguishedName>.Default);
		}
	}
}
