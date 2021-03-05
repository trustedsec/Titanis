using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Smb2.Cli
{
	/// <task category="Enumeration">Enumerate the sessions of users connected to an SMB server</task>
	[Command]
	[Description("Lists active sessions on the server.")]
	[OutputRecordType(typeof(SessionInfo))]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_enumsessions_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	internal sealed class Smb2EnumSessionsCommand : ServerServiceRpcCommand
	{
		[Parameter]
		[Description("Which level of detail to query")]
		public SessionInfoLevel Level { get; set; } = (SessionInfoLevel)(-1);

		[Parameter]
		[Description("Select sessions belonging to this user")]
		public string? ClientComputer { get; set; }

		[Parameter]
		[Description("Select sessions connected to by this computer")]
		public string? ClientUserName { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (this.ClientUserName != null && this.ClientUserName.IndexOfAny(new char[] { '\\', '@' }) >= 0)
				this.WriteWarning("The -ClientUserName user should not specify a domain name.  This will likely return no results.");

			if (this.Level == (SessionInfoLevel)(-1))
				this.Level = SessionInfoLevel.Level502;
			if (!string.IsNullOrEmpty(this.ClientComputer))
			{
				if (!this.ClientComputer.StartsWith(@"\\"))
					this.ClientComputer = @"\\" + this.ClientComputer;
			}
		}

		protected sealed override async Task<int> RunAsync(ServerServiceClient srvs, CancellationToken cancellationToken)
		{
			var shares = await srvs.GetSessions(
				@"\\" + this.ServerName,
				this.ClientComputer,
				this.ClientUserName,
				this.Level,
				this.BufferSize,
				cancellationToken);

			this.WriteRecords(shares);

			return 0;
		}
	}
}
