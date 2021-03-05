using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Provides functionality for marshaling object references.
	/// </summary>
	public interface IObjrefMarshal
	{
		/// <summary>
		/// Decodes an object reference.
		/// </summary>
		/// <typeparam name="T">Interface type</typeparam>
		/// <param name="decoder"><see cref="IRpcDecoder"/> containing data</param>
		byte[] DecodeObjref<T>(IRpcDecoder decoder)
			where T : class, IRpcObject;
		/// <summary>
		/// Encodes an object reference.
		/// </summary>
		/// <typeparam name="T">Interface type</typeparam>
		/// <param name="ncoder"><see cref="IRpcEncoder"/> to contain data</param>
		/// <param name="objref"><see cref="TypedObjref{T}"/> to encode</param>
		void EncodeObjref<T>(IRpcEncoder ncoder, TypedObjref<T>? objref)
			where T : class, IRpcObject;

		/// <summary>
		/// Queries an RPC object for an interface.
		/// </summary>
		/// <typeparam name="TTarget">Interface type to query</typeparam>
		/// <param name="proxy">Object to query</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A proxy for <typeparamref name="TTarget"/></returns>
		Task<TTarget> QueryInterface<TTarget>(
			IRpcObject proxy,
			CancellationToken cancellationToken
			)
			where TTarget : class, IRpcObject;

		/// <summary>
		/// Unwraps an object reference.
		/// </summary>
		/// <typeparam name="T">Desired interface type</typeparam>
		/// <param name="marshalData">Marshaled object reference data</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>An object implementing <typeparamref name="T"/></returns>
		Task<T> Unwrap<T>(byte[] marshalData, CancellationToken cancellationToken)
			where T : class, IRpcObject;
	}
}
