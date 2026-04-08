using ms_samr;
using Titanis.Msrpc.Mssamr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.SamTool
{
	public abstract class SamCommand : RpcCommand<SamClient>
	{
		/// <summary>
		/// Gets the access rights required to run the command.
		/// </summary>
		protected abstract SamServerAccessRights RequiredSamAccess { get; }

		/// <inheritdoc/>
		protected sealed override async Task<int> RunAsync(SamClient client, CancellationToken cancellationToken)
		{
			using (var sam = await client.Connect(this.RequiredSamAccess, this.ServerName, cancellationToken))
			{
				return await this.RunAsync(sam, cancellationToken);
			}
		}

		protected abstract Task<int> RunAsync(Sam sam, CancellationToken cancellationToken);
	}
}
