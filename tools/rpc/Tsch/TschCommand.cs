using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.TschTool
{
	public abstract class TschCommand : RpcCommand<TschClient>
	{
	}
}
