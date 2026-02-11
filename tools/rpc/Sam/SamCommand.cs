using ms_samr;
using Titanis.Msrpc.Mssamr;

namespace Titanis.Cli.SamTool
{
	internal abstract class SamCommand : RpcCommand<SamClient>
	{
		/// <inheritdoc/>
		protected sealed override Type InterfaceType => typeof(samr);

		/// <summary>
		/// Gets the access rights required to run the command.
		/// </summary>
		protected abstract SamServerAccess RequiredSamAccess { get; }

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
