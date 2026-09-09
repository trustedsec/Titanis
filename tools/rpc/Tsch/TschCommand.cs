using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Msrpc.Mstsch;

namespace Titanis.Cli.Tsch;

public abstract class TschCommand : RpcCommand<TaskSchedulerClient>
{
}
