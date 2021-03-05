using System;
using System.Runtime.Serialization;
using Titanis.DceRpc.WireProtocol;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Thrown when an RPC call fails.
	/// </summary>
	[Serializable]
	public class RpcFaultException : Exception, IHaveErrorCode
	{
		private const int RPC_NT_PROTOCOL_ERROR = unchecked((int)0xC002001D);

		public RpcFaultCode Status { get; }

		int IHaveErrorCode.ErrorCode => (int)this.Status;

		private static int RpcFaultCodeMask = 0x1C00_0000;
		internal static bool IsRpcFaultCode(int code)
			=> ((code & RpcFaultCodeMask) == RpcFaultCodeMask);

		public RpcFaultException(RpcFaultCode status)
				: base($"The RPC operation resulted in a fault: {status}")
		{
			this.HResult = RPC_NT_PROTOCOL_ERROR;
			this.Status = status;
		}

		public RpcFaultException(string message) : base(message) { }
		public RpcFaultException(string message, Exception inner) : base(message, inner) { }
		protected RpcFaultException(
		  SerializationInfo info,
		  StreamingContext context) : base(info, context)
		{
			this.Status = (RpcFaultCode)info.GetInt32(nameof(Status));
		}
	}
}