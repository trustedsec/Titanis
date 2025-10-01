using ms_lsar;
using Titanis.Cli;
using Titanis.Msrpc.Mslsar;

namespace Lsa;

/// <summary>
/// Base class for LSA commands
/// </summary>
internal abstract class LsaCommand : RpcCommand<LsaClient>
{
	/// <inheritdoc/>
	protected sealed override Type InterfaceType => typeof(lsarpc);
}
