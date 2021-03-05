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
	/// <task category="Enumeration">Enumerate the open files on an SMB server</task>
	[Command]
	[Description("Lists files open on the server.")]
	[OutputRecordType(typeof(OpenFileInfo))]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_enumopenfiles_Detailed), Placement = DocumentationPlacement.BeforeBase)]
	internal sealed class Smb2EnumOpenFilesCommand : ServerServiceRpcCommand
	{
		[Parameter]
		[Description("Which level of detail to query")]
		public OpenFileInfoLevel Level { get; set; }

		[Parameter]
		[Description("Select files open by this user")]
		public string? OpenBy { get; set; }

		[Parameter]
		[Description("Select files starting with this path")]
		public string? BasePath { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (this.Level == 0)
				this.Level = OpenFileInfoLevel.Level3;

			if (this.OpenBy != null && this.OpenBy.IndexOfAny(new char[] { '\\', '@' }) >= 0)
				this.WriteWarning("The -OpenBy user should not specify a domain name.  This will likely return no results.");
			if (this.BasePath != null)
			{
				if (!(
					this.BasePath.StartsWith(@"\\")
					|| (this.BasePath.Length >= 2 && this.BasePath[1] == ':'))
					)
					this.WriteWarning("The -BasePath should begin with \\ to filter results to open pipes, or with X: (where X is a drive letter) to filter the results to open files on a drive.");
				if (WildcardPattern.ContainsWildcardCharacter(this.BasePath))
					this.WriteWarning("-BasePath does not support wildcards.");
			}
		}

		protected sealed override async Task<int> RunAsync(ServerServiceClient client, CancellationToken cancellationToken)
		{
			var shares = await client.GetOpenFiles(@"\\" + this.ServerName, this.BasePath, this.OpenBy, this.Level, this.BufferSize, cancellationToken);

			this.WriteRecords(shares);

			return 0;
		}
	}
}
