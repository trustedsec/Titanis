using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.IO
{
	/// <summary>
	/// Provides additional methods to read a byte from the stream asynchronously.
	/// </summary>
	public abstract class AsyncStream : Stream
	{
		/// <summary>
		/// Reads a byte from the stream asynchronously.
		/// </summary>
		public virtual ValueTask<int> ReadByteAsync()
		{
			return this.ReadByteAsync(CancellationToken.None);
		}

		/// <summary>
		/// Reads a byte from the stream asynchronously.
		/// </summary>
		public abstract ValueTask<int> ReadByteAsync(CancellationToken cancellationToken);
	}
}
