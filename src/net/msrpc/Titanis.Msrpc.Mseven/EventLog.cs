using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Mseven
{
	public partial class EventLog
	{
		internal EventLog(string name, RpcContextHandle handle, EventLogClient eventLogClient)
		{
			this.Name = name;
			this._handle = handle;
			this._eventLogClient = eventLogClient;
		}

		public string Name { get; }

		private readonly RpcContextHandle _handle;
		private readonly EventLogClient _eventLogClient;
	}
	partial class EventLog : IDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~EventLog()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
