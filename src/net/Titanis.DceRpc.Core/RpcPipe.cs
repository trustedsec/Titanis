using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Represents an RPC pipe.
	/// </summary>
	/// <seealso cref="RpcPipe{T}"/>
	public class RpcPipe
	{
	}
	/// <summary>
	/// Represents an RPC pipe.
	/// </summary>
	public class RpcPipe<T> : RpcPipe
	{
		private readonly Func<T[]> _sourceFunc;

		public RpcPipe()
		{
		}

		public RpcPipe(Func<T[]> sourceFunc)
		{
			if (sourceFunc is null) throw new ArgumentNullException(nameof(sourceFunc));
			this._sourceFunc = sourceFunc;
		}

		public T[]? Chunk { get; private set; }
		public bool ReadNextChunk()
		{
			var chunk = this._sourceFunc();
			this.Chunk = chunk;
			return !chunk.IsNullOrEmpty();
		}
	}
}
