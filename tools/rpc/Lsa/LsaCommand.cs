using ms_lsar;
using Titanis.Msrpc.Mslsar;

namespace Titanis.Cli.LsaTool;

/// <summary>
/// Base class for LSA commands.
/// </summary>
public abstract class LsaCommand : RpcCommand<LsaClient>
{
}
