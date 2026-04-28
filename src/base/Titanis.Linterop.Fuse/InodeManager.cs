using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	/// <summary>
	/// Manages dynamic innodes.
	/// </summary>
	class InodeManager
	{
		internal InodeManager(ulong rootId, IFuseNode rootNode, ILog? log)
		{
			this._rootId = rootId;
			this._lastInode = (long)rootId;

			this._rootNode = rootNode;
			this._log = log;
			this._rootInode = new InodeInfo(rootId, RootPath, rootNode);
		}

		private const string RootPath = "/";

		private readonly ulong _rootId;
		private readonly IFuseNode _rootNode;
		private readonly ILog? _log;
		private readonly InodeInfo _rootInode;

		/// <summary>
		/// Last allocated inode ID
		/// </summary>
		private long _lastInode;

		/// <summary>
		/// Gets the next available inode ID.
		/// </summary>
		/// <returns>ID value</returns>
		private fuse_ino_t GetNextInode()
		{
			if (this._reusableInodes.TryDequeue(out var value))
				return value;
			else
				return (ulong)Interlocked.Increment(ref this._lastInode);
		}
		/// <summary>
		/// Tracks inode IDs that were released and may be reused.
		/// </summary>
		private readonly ConcurrentQueue<fuse_ino_t> _reusableInodes = new ConcurrentQueue<fuse_ino_t>();
		/// <summary>
		/// Inodes indexed by ID.
		/// </summary>
		private ConcurrentDictionary<fuse_ino_t, InodeInfo> _inodes = new();
		/// <summary>
		/// Inodes indexed by path.
		/// </summary>
		private ConcurrentDictionary<string, InodeInfo> _inodesByPath = new ConcurrentDictionary<string, InodeInfo>(StringComparer.Ordinal);
		/// <summary>
		/// Gets an inode by its ID.
		/// </summary>
		/// <param name="id">ID</param>
		/// <returns>An <see cref="InodeInfo"/> corresponding to <paramref name="id"/>, if it exists; otherwise, <see langword="null"/></returns>
		private InodeInfo? TryGetInode(fuse_ino_t id)
		{
			if (id.value == this._rootId)
				return this._rootInode;

			if (this._inodes.TryGetValue(id, out var info))
				return info;
			else
				return null;
		}
		/// <summary>
		/// Allocates an inode for a node or increases the reference count fon an existing inode.
		/// </summary>
		/// <param name="path">Path to node</param>
		/// <param name="node">Node</param>
		/// <returns>An <see cref="InodeInfo"/> representing the allocation</returns>
		/// <remarks>
		/// If an inode already exists for <paramref name="path"/>, the reference count is increased and <paramref name="node"/> is ignored.
		/// </remarks>
		internal InodeInfo AllocOrRefInodeFor(string path, IFuseNode node)
		{
			Debug.Assert(!string.IsNullOrEmpty(path));
			Debug.Assert(node != null);

			var entry = this._inodesByPath.AddOrUpdate(
				path,
				static (s, args) =>
				{
					var inode = new InodeInfo(args.owner.GetNextInode(), s, args.node);
					args.log?.WriteFuseAllocInodeMessage(inode.id.value, s);
					Debug.Assert(args.owner._inodes.TryAdd(inode.id, inode));
					return inode;
				},
				static (s, inode, args) =>
				{
					inode.refCount++;
					args.log?.WriteFuseIncRefInodeMessage(inode.id.value, s, inode.refCount);
					return inode;
				},
				(owner: this, node, log: this._log)
				);
			return entry;
		}
		internal InodeInfo GetInode(fuse_ino_t id)
		{
			var node = this.TryGetInode(id);
			if (node is null)
				throw new LinuxException(LinuxErrorCode.ENOENT);

			return node;
		}

		internal void ForgetInode(fuse_ino_t id, ulong nlookup)
		{
			var inode = this.TryGetInode(id);
			if (inode != null)
				this.ForgetInode(inode, nlookup);
		}
		internal void ForgetInode(InodeInfo node, ulong nlookup)
		{
			Debug.Assert(node != null);
			this._log?.WriteFuseDecRefInodeMessage(node.id.value, node.path, nlookup, (long)(node.refCount - nlookup));
			Debug.Assert(node.refCount > 0);

			ulong refcount;
			if (nlookup == 1)
				refcount = Interlocked.Decrement(ref node.refCount);
			else
			{
				refcount = node.refCount;
				while (Interlocked.CompareExchange(ref node.refCount, refcount - nlookup, refcount) != refcount)
				{
					refcount = node.refCount;
				}
				refcount -= nlookup;
			}

			if (refcount == 0)
			{
				var removedEntry = this._inodesByPath.TryRemove(new KeyValuePair<string, InodeInfo>(node.path, node));
				Debug.Assert(removedEntry);

#if DEBUG
#else
				this._inodes.Remove(node.id, out var removedNode);
				this._reusableInodes.Enqueue(node.id.value);
#endif
			}
			else
			{
				// Still has references
			}
		}
	}

	/// <summary>
	/// Represents an inode.
	/// </summary>
	class InodeInfo
	{
		public InodeInfo(fuse_ino_t id, string path, IFuseNode node)
		{
			this.id = id;
			this.path = path;
			this.node = node;
			this.refCount = 1;
		}

		internal readonly fuse_ino_t id;
		internal readonly string path;
		internal readonly IFuseNode node;
		internal ulong refCount;
	}
}
