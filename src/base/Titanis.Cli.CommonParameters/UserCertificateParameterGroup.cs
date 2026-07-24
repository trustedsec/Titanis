using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Titanis.Security;

namespace Titanis.Cli
{
	public class UserCertificateParameterGroup : ParameterGroupBase
	{
		[Parameter]
		[Advanced]
		[Description("Name of file containing user's certificate (for PKINIT)")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		public FileSpec? UserCert { get; set; }

		[Parameter]
		[Advanced]
		[Description("Name of file containing user's key (for PKINIT)")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		public FileSpec? UserKey { get; set; }

		[Parameter]
		[Advanced]
		[Description("Password to decrypt file containing user's key (for PKINIT)")]
		[Category(ParameterCategories.AuthenticationKerberos)]
		public string? UserKeyPassword { get; set; }

		public X509Certificate2 Certificate { get => this._userCert; set => this._userCert = value; }

		private X509Certificate2? _userCert;
		private X509Certificate2Collection? _userCertCollection;

		public X509Certificate2? Validate(ParameterValidationContext context, ref UserPrincipalName? userName)
		{
			var log = this.Log;
			// Try loading the certificate
			// This will populate or validate UserName and UserDomain

			if (this.UserCert != null)
			{
				AuthenticationParameters.LoadCertificateAndKey(
					this.RequireFileAccess(),
					this.UserCert,
					this.UserKey,
					this.UserKeyPassword,
					log,
					context,
					out this._userCert,
					out this._userCertCollection,
					out var upn
					);

				if (userName == null)
				{
					if (upn is null)
						context.LogError($"The certificate does not specify a user name.  Specify one with -{nameof(AuthenticationParameters.UserName)}.");
					userName = upn;
				}
				else
				{
					if (upn is not null)
					{
						if (!userName.Equals(upn))
							log?.WriteWarning($"The certificate specifies a user name '{upn}' that differs from the user name provided on the command line.  Using the user name from the command line.");
					}
				}
			}
			else
			{
				if (this.UserKey != null)
					context.LogError(new ParameterValidationError(nameof(UserKeyPassword), $"-{nameof(UserKey)} is only valid with -{nameof(UserCert)} or -{nameof(UserKey)}"));
				if (!string.IsNullOrEmpty(this.UserKeyPassword) && (this.UserCert is null))
					context.LogError(new ParameterValidationError(nameof(UserKeyPassword), $"-{nameof(UserKeyPassword)} is only valid with -{nameof(UserCert)}"));
			}

			return this._userCert;
		}
	}
}
