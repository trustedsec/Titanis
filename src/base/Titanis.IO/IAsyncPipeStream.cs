using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.IO
{
	/// <summary>
	/// Exposes functionality of an asynchronous pipe.
	/// </summary>
	public interface IAsyncPipeStream
	{
		/// <summary>
		/// Sends a request and receives the response.
		/// </summary>
		/// <param name="bytesToSend">Request bytes to send</param>
		/// <param name="responseBuffer">Buffer to receive response</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation.</param>
		/// <returns>Number of bytes received into <paramref name="responseBuffer"/></returns>
		Task<int> Transceive(ReadOnlyMemory<byte> bytesToSend, Memory<byte> responseBuffer, CancellationToken cancellationToken);
	}
}
