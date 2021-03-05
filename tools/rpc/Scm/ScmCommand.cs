using ms_scmr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Msscmr;

namespace Titanis.Cli.ScmTool
{
	internal abstract class ScmCommand : RpcCommand<ScmClient>
	{
		/// <inheritdoc/>
		protected sealed override Type InterfaceType => typeof(svcctl);

		/// <inheritdoc/>
		protected sealed override bool SupportsDynamicTcp => true;

		/// <summary>
		/// Gets the access rights required to run the command.
		/// </summary>
		protected abstract ScmAccess RequiredScmAccess { get; }

		/// <inheritdoc/>
		protected sealed override bool RequiresEncryption => true;

		/// <inheritdoc/>
		protected sealed override async Task<int> RunAsync(ScmClient client, CancellationToken cancellationToken)
		{
			await using (var scm = await client.OpenScm(RequiredScmAccess, cancellationToken))
			{
				return await this.RunAsync(scm, cancellationToken);
			}
		}

		protected abstract Task<int> RunAsync(Scm scm, CancellationToken cancellationToken);
	}
}
