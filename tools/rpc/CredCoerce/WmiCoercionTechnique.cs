using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.CredCoerce;
internal class WmiCoercionTechnique : CoercionTechnique
{
	public sealed override async Task Execute(CoercionContext context, CancellationToken cancellationToken)
	{
		var efs = context.TryGetService<EfsClient>();
		if (efs is null)
		{
			efs = new EfsClient();
			var epm = context.EpmClient;
			try
			{
				var efsEP = await epm.TryMapTcp(efs.AbstractSyntaxId, IPAddress.Any, cancellationToken);
			}
			catch (TimeoutException ex)
			{
				// This usually happens when the service is already running
				context.Log.WriteVerbose("EPM lookup for EFS time out; this usually indicates the service is already running on the target");
			}

			ServicePrincipalName targetSpn = new(context.ServerName, ServiceClassNames.Host);

			var rpcClient = context.RpcClient;
			var smbClient = context.SmbClient;

			// Connect to EFS
			efs = new EfsClient();
			await rpcClient.ConnectPipe(efs, smbClient, $@"\\{context.ServerName}\IPC$\{EfsClient.EfsPipeName}", cancellationToken, authContext: context.CredService.GetAuthContextForService(targetSpn));

			context.AddRpcService(efs);
		}

		await this.Execute(efs, context, cancellationToken);
	}
}
