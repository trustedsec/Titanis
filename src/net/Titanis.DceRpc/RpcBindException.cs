using System;
using System.Runtime.Serialization;
using Titanis.DceRpc.WireProtocol;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Thrown when an RPC bind has failed.
	/// </summary>
	[Serializable]
	internal class RpcBindException : Exception, IHaveErrorCode
	{
		private const int RPC_NT_INVALID_BINDING = unchecked((int)0xC0020003);

		/// <summary>
		/// Gets the reason for the rejection.
		/// </summary>
		public BindRejectReason RejectReason { get; }
		/// <summary>
		/// Gets the call ID for the bind operation.
		/// </summary>
		public int CallId { get; }

		/// <inheritdoc/>
		int IHaveErrorCode.ErrorCode => (int)this.RejectReason;

		/// <summary>
		/// Initializes a new <see cref="RpcBindException"/>.
		/// </summary>
		public RpcBindException(BindRejectReason reason, int callId)
			: base($"The bind request was rejected by the server: {reason}")
		{
			this.HResult = RPC_NT_INVALID_BINDING;
			this.RejectReason = reason;
			this.CallId = callId;
		}

		/// <summary>
		/// Initializes a new <see cref="RpcBindException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected RpcBindException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			this.RejectReason = (BindRejectReason)info.GetInt32(nameof(RejectReason));
			this.CallId = info.GetInt32(nameof(CallId));
		}

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(this.RejectReason), (int)this.RejectReason);
			info.AddValue(nameof(this.CallId), this.CallId);
			base.GetObjectData(info, context);
		}
	}
}