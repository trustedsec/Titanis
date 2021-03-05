using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using Titanis.Cli;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Kerberos;
using Titanis.Security.Ntlm;
using Titanis.Security.Spnego;

namespace Titanis.Smb2.Cli
{
	[Description("Performs operations on an SMB2 server.")]
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_common_details))]
	[Subcommand("ls", typeof(Smb2LsCommand))]
	[Subcommand("get", typeof(Smb2GetCommand))]
	[Subcommand("put", typeof(Smb2PutCommand))]
	[Subcommand("watch", typeof(Smb2WatchCommand))]
	[Subcommand("enumnics", typeof(Smb2EnumNicsCommand))]
	[Subcommand("mklink", typeof(Smb2MklinkCommand))]
	[Subcommand("mount", typeof(Smb2MountCommand))]
	[Subcommand("umount", typeof(Smb2UmountCommand))]
	[Subcommand("mkdir", typeof(Smb2MkdirCommand))]
	[Subcommand("rmdir", typeof(Smb2RmdirCommand))]
	[Subcommand("rm", typeof(Smb2RmCommand))]
	[Subcommand("enumshares", typeof(Smb2EnumSharesCommand))]
	[Subcommand("enumopenfiles", typeof(Smb2EnumOpenFilesCommand))]
	[Subcommand("enumsessions", typeof(Smb2EnumSessionsCommand))]
	[Subcommand("enumsnapshots", typeof(Smb2EnumSnapshotsCommand))]
	[Subcommand("enumstreams", typeof(Smb2EnumStreamsCommand))]
	internal class Program : MultiCommand
	{
		static int Main(string[] args)
			=> RunProgramAsync<Program>(args);
	}

	/// <summary>
	/// Base class for SMB2 commands.
	/// </summary>
	/// <remarks>
	/// This class declares the common parameters for SMB2 commands.
	/// When invoked, the implementation connects to the SMB2 server and
	/// authenticates.
	/// <para>
	/// To create a new SMB2 command, derive from this class and implement.
	/// </para>
	/// </remarks>
	[DetailedHelpResource(typeof(Messages), nameof(Messages.Smb2Client_Detailed))]
	public abstract partial class Smb2CommandBase : Command
	{
		internal readonly static char[] WildcardChars = new char[] { '*', '?' };
		protected const int UncParamPos = 0;

		#region Connection information
		[Parameter(UncParamPos)]
		[Mandatory]
		[Placeholder("UNC path")]
		[Description("The UNC path of the target")]
		[Category(ParameterCategories.Connection)]
		public string? UncPath { get; set; }

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public AuthenticationParameters AuthenticationParameters { get; set; }

		private NetworkParameters? _netParameters;
		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public NetworkParameters? NetParameters
		{
			get => _netParameters;
			set
			{
				_netParameters = value;
				if (value != null) value.Log = this.Log;
			}
		}

		/// <summary>
		/// Gets the share name to use when the user doesn't provide one.
		/// </summary>
		protected virtual string? DefaultShareName => null;

		/// <summary>
		/// Gets the parsed <see cref="UncPath"/> provided on the command line.
		/// </summary>
		protected UncPath UncPathInfo { get; private set; }
		protected string? ServerName => this.UncPathInfo?.ServerName;
		protected string? ShareName => this.UncPathInfo?.ShareName;
		protected string? ShareRelativePath => this.UncPathInfo?.ShareRelativePath;
		protected int RemotePort => this.UncPathInfo?.Port ?? Smb2Client.TcpPort;
		#endregion

		[ParameterGroup(ParameterGroupOptions.AlwaysInstantiate)]
		public SmbParameters? SmbParameters { get; set; }


		/// <summary>
		/// Executes the command with a configured <see cref="Smb2Client"/>.
		/// </summary>
		/// <param name="client">Client configured for operation</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Command result</returns>
		protected abstract Task<int> RunAsync(Smb2Client client, CancellationToken cancellationToken);

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			this.NetParameters.ValidateParameters(context);

			// Parse the UNC path into components
			this.UncPathInfo = Titanis.UncPath.Parse(this.UncPath);

			// Use default share if appropriate
			if (string.IsNullOrEmpty(this.UncPathInfo.ShareName) && !string.IsNullOrEmpty(this.DefaultShareName))
			{
				this.UncPath = Path.Combine(this.UncPath, this.DefaultShareName);
				this.UncPathInfo = Titanis.UncPath.Parse(this.UncPath);
			}

			this.AuthenticationParameters.Validate(true, context, this.Log);
			this.SmbParameters.Validate(context, this.AuthenticationParameters);
		}

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			await using (Smb2Client client = CreateClient())
			{
				return await this.RunAsync(client, cancellationToken);
			}
		}

		private Smb2Client CreateClient()
		{
			var client = new Smb2Client(this.AuthenticationParameters.GetCredentialServiceFor(new ServicePrincipalName(ServiceClassNames.Cifs, this.ServerName), SecurityCapabilities.Integrity, this.Log), nameResolver: this.NetParameters, traceCallback: new Smb2Logger(this.Log));
			this.SmbParameters?.ConfigureClient(client);
			return client;
		}
	}
}
