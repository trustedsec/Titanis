using ms_lsar;
using Titanis.Msrpc.Mslsar;

namespace Titanis.Cli.LsaTool;

/// <summary>
/// Base class for LSA commands
/// </summary>
internal abstract class LsaCommand : RpcCommand<LsaClient>
{
	/// <inheritdoc/>
	protected sealed override Type InterfaceType => typeof(lsarpc);
}
