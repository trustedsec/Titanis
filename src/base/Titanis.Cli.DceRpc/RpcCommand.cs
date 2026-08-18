using System.ComponentModel;
using System.Security.Cryptography;
using Titanis.DceRpc.Client;
using Titanis.Smb2;

namespace Titanis.Cli
{
	/// <summary>
	/// Base class for commands that use RPC.
	/// </summary>
	/// <remarks>
	/// Implementors should use <see cref="RpcCommand{TClient}"/>.
	/// </remarks>
	public abstract class RpcCommand : Command
	{

		[ParameterGroup(ParameterGroupOptions.Required)]
		public RpcParameterGroup RpcParameters { get; set; }

		private string _serverName;
		[Parameter(0)]
		[Mandatory]
		[Description("RPC server to interact with")]
		public string ServerName { get => _serverName; set => _serverName = value; }


		private RpcServiceClient _svcClient;
		protected override void ValidateParameters(ParameterValidationContext context)
		{
			var svcClient = this.CreateServiceClient();
			this._svcClient = svcClient;

			base.ValidateParameters(context);
			this.RpcParameters.ValidateParameters(context, svcClient, ref this._serverName);
		}

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			var svcClient = this._svcClient;
			var bindInfo = await RpcParameters.BindServiceClient(
				svcClient,
				ServerName,
				cancellationToken
				).ConfigureAwait(false);

			using (bindInfo.SmbClient)
			{
				return await RunAsync(svcClient, cancellationToken).ConfigureAwait(false);
			}
		}

		protected abstract RpcServiceClient CreateServiceClient();
		protected abstract Task<int> RunAsync(RpcServiceClient client, CancellationToken cancellationToken);
	}

	public abstract class RpcCommand<TClient> : RpcCommand
		where TClient : RpcServiceClient, new()
	{
		protected abstract Task<int> RunAsync(TClient client, CancellationToken cancellationToken);
		protected sealed override Task<int> RunAsync(RpcServiceClient client, CancellationToken cancellationToken)
			=> this.RunAsync((TClient)client, cancellationToken);

		protected sealed override RpcServiceClient CreateServiceClient() => new TClient();
	}
}
