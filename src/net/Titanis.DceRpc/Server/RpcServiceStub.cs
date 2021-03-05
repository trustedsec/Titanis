using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Communication;
using Titanis.DceRpc.WireProtocol;
using Titanis.IO;

namespace Titanis.DceRpc.Server
{
	/// <summary>
	/// Base class for RPC service interface implementations.
	/// </summary>
	/// <remarks>
	/// 
	/// </remarks>
	public abstract class RpcServiceStub
	{
		/// <summary>
		/// Gets the interface UUID.
		/// </summary>
		public abstract Guid InterfaceUuid { get; }
		/// <summary>
		/// Gets the interface version.
		/// </summary>
		public abstract RpcVersion InterfaceVersion { get; }
		/// <summary>
		/// Gets an array of delegates implementing interface operations.
		/// </summary>
		public abstract OperationImplFunc[] DispatchTable { get; }

		/// <summary>
		/// Gets an object marshaler implementation.
		/// </summary>
		/// <remarks>
		/// This property is implemented by <see cref="IRpcObjectProxy"/>
		/// </remarks>
		private protected virtual IObjrefMarshal? DcomClient => null;

		internal async Task DispatchAsync(
			RequestPdu request,
			RpcBindContext bindContext,
			CancellationToken cancellationToken
			)
		{
			RpcCallContext callContext = new RpcCallContext(this.DcomClient);

			var responseWriter = new ByteWriter();
			responseWriter.Advance(PduHeader.PduStructSize + ResponsePduHeader.StructSize);
			var responseEncoder = bindContext.encoding.CreateEncoder(responseWriter, callContext);

			await this.DispatchAsync(
				request.header.opnum,
				request.ObjectId,
				bindContext.encoding.CreateDecoder(new ByteMemoryReader(request.StubData), callContext),
				responseEncoder,
				cancellationToken
				).ConfigureAwait(false);
		}

		// TODO: Change access to protected
		public async virtual Task DispatchAsync(
			ushort opnum,
			Guid objectId,
			RpcDecoder stubData,
			RpcEncoder responseData,
			CancellationToken cancellationToken
			)
		{
			OperationImplFunc[] dispatchtable = this.DispatchTable;
			var opMethod = dispatchtable[opnum];
			await opMethod(stubData, responseData, cancellationToken).ConfigureAwait(false);
			// TODO: Send response
			throw new NotImplementedException();
		}
	}

	struct RpcInterfaceKey : IEquatable<RpcInterfaceKey>
	{
		internal readonly SyntaxId syntaxId;

		public RpcInterfaceKey(SyntaxId syntaxId)
		{
			this.syntaxId = syntaxId;
		}

		public override bool Equals(object obj)
		{
			return obj is RpcInterfaceKey key && this.Equals(key);
		}

		public bool Equals(RpcInterfaceKey other)
		{
			return this.syntaxId.if_uuid.Equals(other.syntaxId.if_uuid) &&
				   this.syntaxId.if_version.Equals(other.syntaxId.if_version);
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(this.syntaxId.if_uuid, this.syntaxId.if_version);
		}

		public static bool operator ==(RpcInterfaceKey left, RpcInterfaceKey right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(RpcInterfaceKey left, RpcInterfaceKey right)
		{
			return !(left == right);
		}
	}
}
