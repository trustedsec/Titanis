using System.ComponentModel;
using Titanis.DceRpc.Client;

namespace Titanis.Cli
{
	/// <summary>
	/// Base class for commands that use RPC.
	/// </summary>
	/// <remarks>
	/// Implementors should use <see cref="RpcCommand{TClient}"/>.
	/// </remarks>
	public abstract class RpcCommand : Command, IHaveServerName
	{

		[ParameterGroup(ParameterGroupOptions.Required)]
		public RpcParameterGroup RpcParameters { get; set; }

		private ServerSpec[] _serverName;
		[Parameter(0)]
		[Mandatory]
		[Description("RPC server to interact with")]
		public ServerSpec[] ServerName { get => _serverName; set => _serverName = value; }

		public string? CurrentServerName { get; set; }
		string? IHaveServerName.ServerName => this.CurrentServerName;

		[Parameter]
		[Category(ParameterCategories.ErrorHandling)]
		[Description("Continues executing even if an error occurs")]
		public virtual SwitchParam ContinueOnError { get; set; }

		protected override void OnWritingRecord(object? record, RecordInfo? info = null)
		{
			base.OnWritingRecord(record, info);
			if (record is IWantServerName wantsServer && wantsServer.ServerName is null)
				wantsServer.ServerName = this.CurrentServerName;
		}

		private RpcServiceClient _svcClient;
		private bool _hasMultiServers;
		protected override void ValidateParameters(ParameterValidationContext context)
		{
			var svcClient = this.CreateServiceClient();
			this._svcClient = svcClient;

			base.ValidateParameters(context);
			this.RpcParameters.ValidateParameters(context, svcClient);

			this._hasMultiServers = (this.ServerName.Length > 1) || this.ServerName[0].HasMultiple;
		}

		protected override OutputField[] FilterOutputFields(OutputField[] fields)
		{
			fields = base.FilterOutputFields(fields);
			if (!this.OutputFieldsSpecified && (this.ServerName?.Length ?? 0) <= 1)
			{
				fields = Array.FindAll(fields, r => r.Name != nameof(IWantServerName.ServerName));
			}
			return fields;
		}

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			int lastError = 0;
			int lastExitCode = 0;
			bool multiServer = this._hasMultiServers;
			foreach (var serverSpec in this.ServerName)
			{
				foreach (var serverName_ in serverSpec.GetTargets())
				{
					string serverName = serverName_;
					bool isSmb = serverName.StartsWith("//") || serverName.StartsWith(@"\\");
					if (isSmb)
						serverName = serverName.Substring(2);

					if (multiServer)
						this.WriteMessage($"Running on server {serverName}.");

					try
					{
						this.CurrentServerName = serverName;

						var svcClient = this._svcClient;
						var bindInfo = await RpcParameters.BindServiceClient(
							svcClient,
							serverName,
							isSmb,
							cancellationToken
							).ConfigureAwait(false);

						using (bindInfo.SmbClient)
						{
							lastExitCode = await RunAsync(svcClient, cancellationToken).ConfigureAwait(false);
						}
					}
					catch (Exception ex)
					{
						if (this.ContinueOnError.IsSet)
							this.WriteError($"Error occurred with server {serverName}: {ex.Message}");
						else
							throw;

						while (ex is AggregateException agg)
							ex = agg.InnerException;

						if (ex is IHaveErrorCode err)
							lastError = err.ErrorCode;
						else
							lastError = ex.HResult;
					}

					this.Context.FlushOutput();
				}
			}

			return (lastError != 0) ? lastError : lastExitCode;
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
