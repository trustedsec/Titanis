using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	/// <summary>
	/// Exposes functionality for retrieving a node by name and argument.
	/// </summary>
	/// <remarks>
	/// <see cref="FuseNodeCache{TNode, TArg}"/> uses this interface to retrieve nodes
	/// when it receives a request for a node that is not in the cache.
	/// </remarks>
	public interface IFuseNodeSource<TNode, TArg>
	{
		/// <summary>
		/// Gets a node with a name and optional argument.
		/// </summary>
		/// <param name="name">Name of node</param>
		/// <param name="arg">Argument provided by caller</param>
		/// <returns>A <see cref="IFuseNode"/> corresponding to <paramref name="name"/></returns>
		TNode GetNode(string name, TArg arg);
	}
	/// <summary>
	/// Implements a cacde for inodes.
	/// </summary>
	/// <typeparam name="TNode">Node type</typeparam>
	/// <typeparam name="TArg">Factory argument</typeparam>
	/// <remarks>
	/// This implementation allocates a new entry for each unique name.
	/// </remarks>
	public class FuseNodeCache<TNode, TArg>
	{
		public FuseNodeCache(IFuseNodeSource<TNode, TArg> source, StringComparer? comparer = null)
		{
			comparer ??= StringComparer.Ordinal;
			this._nodesByName = new ConcurrentDictionary<string, TNode>(comparer);
			this._source = source;
		}

		private ConcurrentDictionary<string, TNode> _nodesByName;
		private readonly IFuseNodeSource<TNode, TArg> _source;

		public TNode GetNode(string name, TArg arg)
		{
			return this._nodesByName.GetOrAdd(name, s => _source.GetNode(name, arg));
		}

		public TNode? TryGetNode(string name)
		{
			this._nodesByName.TryGetValue(name, out var node);
			return node;
		}
	}
}
