using ms_dtyp;
using System;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Winterop;

namespace Titanis.Msrpc.Mseven
{
	public class EventLogClient : RpcServiceClient<ms_even.eventlogClientProxy>
	{
		private const int MajorVersion = 1;
		private const int MinorVersion = 1;

		public async Task<EventLog> OpenLog(string logName, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(logName)) throw new System.ArgumentException($"'{nameof(logName)}' cannot be null or empty.", nameof(logName));

			RpcPointer<RpcContextHandle> logHandle = new();
			var res = (Ntstatus)await this._proxy.ElfrOpenELW(
				new RpcPointer<char>('\0'),
				logName.ToRpcUnicodeString(),
				string.Empty.ToRpcUnicodeString(),
				MajorVersion,
				MinorVersion,
				logHandle,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new EventLog(logName, logHandle.value, this);
		}

		public Task ClearLog(string? backupFileName)
		{
			throw new NotImplementedException();
		}
	}
}
