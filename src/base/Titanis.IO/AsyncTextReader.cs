using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.IO
{
	/// <summary>
	/// Provides additional methods to read text asynchronously.
	/// </summary>
	public abstract class AsyncTextReader : TextReader
	{
		/// <summary>
		/// Value signifying no value available to peek.
		/// </summary>
		/// <seealso cref="PeekAsync"/>
		public const int NoPeekValue = -1;

		public sealed override async Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			return await this.ReadAsync(buffer, index, count, CancellationToken.None);
		}

		public ValueTask<int> ReadAsync()
		{
			return this.ReadAsync(CancellationToken.None);
		}
		public abstract ValueTask<int> ReadAsync(CancellationToken cancellationToken);

		public abstract ValueTask<int> ReadAsync(char[] buffer, int index, int count, CancellationToken cancellationToken);
		public abstract ValueTask<string?> ReadLineAsync(CancellationToken cancellationToken);

		public abstract ValueTask<int> PeekAsync(CancellationToken cancellationToken);
	}
}
