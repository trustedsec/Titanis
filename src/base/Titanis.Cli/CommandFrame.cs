using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Titanis.Cli
{
	internal class CommandFrame
	{
		public CommandFrame(CommandFrame? parent)
		{
			this.CancellationSource = new CancellationTokenSource();
			this.Parent = parent;
		}

		public CancellationTokenSource CancellationSource { get; }
		public CommandFrame? Parent { get; }
	}
}
