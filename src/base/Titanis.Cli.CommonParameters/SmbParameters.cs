using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Smb2;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies parameters for SMB.
	/// </summary>
	public class SmbParameters : ParameterGroupBase
	{
		[Parameter]
		[Description("List of SMB2 dialects to negotiate")]
		[Category(ParameterCategories.Connection)]
		public Smb2Dialect[]? Dialects { get; set; }

		[Parameter]
		[Alias("signreq")]
		[Description("Requires packets to be signed")]
		[Category(ParameterCategories.Connection)]
		public SwitchParam RequireSigning { get; set; }

		[Parameter]
		[Category(ParameterCategories.Connection)]
		[Description("Requires the client to authenticate the negotiation")]
		public SwitchParam RequireSecureNegotiate { get; set; }

		[Parameter]
		[Description("Requires an encrypted connection")]
		[Category(ParameterCategories.Connection)]
		public SwitchParam EncryptSmb { get; set; }

		#region Features
		[Parameter]
		[Description("Checks for and follows DFS referrals (default=true)")]
		[Category(ParameterCategories.ClientBehavior)]
		public SwitchParam FollowDfs { get; set; }

		[Parameter]
		[Description("Specifies the size for the DFS referral buffer (default=4096)")]
		[Category(ParameterCategories.ClientBehavior)]
		public int DfsReferralBufferSize { get; set; } = 4096;
		#endregion

		public void Validate(ParameterValidationContext context, AuthenticationParameters? securityParameters)
		{
			// Check SMB features
			if (Dialects != null)
			{
				var newestDialect = Dialects.Max();
				if (newestDialect < Smb2Dialect.Smb3_0 && EncryptSmb.IsSet)
					context.LogError(nameof(EncryptSmb), @"Encryption requested, but the SMB dialect(s) specified does not support encryption.");
			}

			if (securityParameters.Anonymous.IsSet)
			{
				if (RequireSigning.IsSet)
					context.LogError(nameof(RequireSigning), "Signing is not supported on anonymous sessions.");
				if (EncryptSmb.IsSet)
					context.LogError(nameof(EncryptSmb), "Encryption is not supported on anonymous sessions.");
			}
		}

		protected sealed override void Initialize(Command owner, IServiceContainer services)
		{
			base.Initialize(owner, services);
			services.AddService(typeof(ISmb2TraceCallback), this.CreateTraceLogger);
		}

		private ISmb2TraceCallback CreateTraceLogger(IServiceContainer container, Type serviceType)
		{
			return new Smb2Logger(this.Services.RequireService<ILog>(), this.Owner?.GetCallback<ISmb2TraceCallback>());
		}

		public Smb2Client CreateClient()
		{
			var client = this.Services.CreateSmb2Client();
			client.FollowDfs = FollowDfs.GetValue(true);
			client.DfsReferralBufferSize = DfsReferralBufferSize;
			//UseMultiChannel = true

			Smb2ConnectionOptions connectionOptions = new Smb2ConnectionOptions()
			{
				RequiresSecureNegotiate = RequireSecureNegotiate.IsSet
			};
			client.DefaultConnectionOptions = connectionOptions;

			// Configure the client according to user-provided parameters
			if (!Dialects.IsNullOrEmpty())
				connectionOptions.SupportedDialects = Dialects!;
			if (RequireSigning.IsSet)
				connectionOptions.SecurityMode |= Smb2SecurityMode.SigningRequired;

			var sessionOptions = new Smb2SessionOptions(EncryptSmb.IsSet);
			client.DefaultSessionOptions = sessionOptions;

			return client;
		}
	}
}
