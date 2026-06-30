using System.Xml.Linq;
using Titanis.Cli;
using Titanis.Linterop.Fuse;
using Titanis.Security.Kerberos;
using Titanis.Winterop;
using Titanis.Winterop.Security;
using static Titanis.Smb2.Smb2Directory;

namespace Titanis.Smb2.Cli
{
	abstract class Traversal<T>
	{
		internal Traversal(int maxDepth)
		{
			this.MaxDepth = maxDepth;
		}

		public int MaxDepth { get; }

		protected abstract bool IsBranchNode(T node);

		record struct NodeInfo(T Item, int Depth);
		private LinkedList<NodeInfo> _nodes = new LinkedList<NodeInfo>();

		private int _depthRemaining;

		public void AddRoot(T node)
		{
			this._nodes.AddLast(new NodeInfo(node, this.MaxDepth));
		}

		private LinkedListNode<NodeInfo>? _nextNode;
		protected void OnYield(T node)
		{
			if (this._depthRemaining >= 0 && this.IsBranchNode(node))
			{
				if (this._nextNode != null)
					this._nextNode = this._nodes.AddAfter(this._nextNode, new NodeInfo(node, this._depthRemaining));
				else
					this._nextNode = this._nodes.AddFirst(new NodeInfo(node, this._depthRemaining));
			}
		}
		public async Task Traverse(CancellationToken cancellationToken)
		{
			var listItem = this._nodes.First;
			while (this._nodes.Count > 0)
			{
				var head = this._nodes.First!.Value;
				this._nodes.RemoveFirst();

				this._depthRemaining = head.Depth - 1;
				this._nextNode = null;

				try
				{
					await this.TraverseBranch(head.Item, head.Depth, cancellationToken);
				}
				catch (Exception ex)
				{
					this.OnError(head.Item, ex);
				}
			}
		}

		protected virtual void OnError(T? item, Exception ex)
		{
		}

		protected abstract Task TraverseBranch(T node, int depthRemaining, CancellationToken cancellationToken);
	}

	record struct Smb2DirEntryNode(UncPath? TraversalRootPath, Smb2DirEntry Entry);
	record struct QueryDirCallbackArg(UncPath TraversalRoot, string RelativePrefix, bool ShouldPrint, bool ShouldYield);
	class Smb2Traversal : Traversal<Smb2DirEntryNode>, ISmb2QueryDirCallback<QueryDirCallbackArg>
	{
		private readonly ICommandContext _context;
		private readonly Smb2Client _client;
		private readonly Smb2DirQueryOptions _queryOptions;
		private readonly SecurityInfo _secInfo;
		private readonly int _queryBufferSize;
		private readonly Smb2FileCreateOptions _extraOptions;
		private readonly DateTime? _timeWarpToken;
		private readonly string _searchPattern;

		internal Smb2Traversal(
			int maxDepth,
			ICommandContext context,
			Smb2Client client,
			string searchPattern,
			Smb2DirQueryOptions queryOptions,
			SecurityInfo secInfo,
			int queryBufferSize,
			Smb2FileCreateOptions extraOptions,
			DateTime? timeWarpToken
			)
			: base(maxDepth)
		{
			this._context = context;
			this._client = client;
			this._searchPattern = searchPattern;
			this._queryOptions = queryOptions;
			this._secInfo = secInfo;
			this._queryBufferSize = queryBufferSize;
			this._extraOptions = extraOptions;
			this._timeWarpToken = timeWarpToken;
		}

		public int Directories { get; set; }
		public int Files { get; set; }

		protected override bool IsBranchNode(Smb2DirEntryNode node) => node.Entry.IsDirectory && !node.Entry.IsReparsePoint;

		protected override async Task TraverseBranch(Smb2DirEntryNode node, int depthRemaining, CancellationToken cancellationToken)
		{
			this.Directories++;

			var dirPath = node.TraversalRootPath.Append(node.Entry.RelativePath);
			this._context.Log.WriteVerbose($"Traversing {dirPath}; found {this.Files} files in {this.Directories} directories so far");

			// Open directory
			await using (var dir = (Smb2Directory)await _client.CreateFileAsync(dirPath, new Smb2CreateInfo()
			{
				CreateDisposition = Smb2CreateDisposition.OpenExisting,
				Priority = Smb2Priority.OpenDir,
				DesiredAccess = (uint)Smb2FileAccessRights.DefaultOpenDirAccess,
				ShareAccess = Smb2ShareAccess.DefaultDirShare,
				FileAttributes = Winterop.FileAttributes.None,
				CreateOptions = Smb2FileCreateOptions.Directory | Smb2FileCreateOptions.SynchronousIoNonalert | this._extraOptions,
				ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
				RequestMaximalAccess = true,
				QueryOnDiskId = true,
				TimeWarpToken = this._timeWarpToken
				//OplockLevel = lease ? Smb2OplockLevel.Lease : Smb2OplockLevel.None,
				//LeaseInfo = lease
				//	? new Smb2LeaseInfo()
				//	{
				//		LeaseState = Smb2LeaseState.ReadCaching | Smb2LeaseState.HandleCaching,
				//		UseV2Struct = this.Session.Connection.Dialect >= Smb2Dialect.Smb3_0
				//	}
				//	: null
			}, FileAccess.Read, cancellationToken))
			{
				string? prefix = node.Entry.RelativePath;

				bool requireDirPass = (depthRemaining > 0 && this._searchPattern != "*");

				// Enumerate files in directory using mask
				try
				{
					await dir.QueryDirAsync(
						this._searchPattern,
						this._queryOptions,
						this._secInfo,
						this._queryBufferSize,
						this,
						new QueryDirCallbackArg(node.TraversalRootPath, prefix, true, !requireDirPass),
						cancellationToken);
				}
				catch (NtstatusException ex) when (ex.StatusCode is Ntstatus.STATUS_NO_SUCH_FILE)
				{
					// Ignore
				}

				if (requireDirPass)
				{
					try
					{
						await dir.QueryDirAsync(
							"*", 
							this._queryOptions,
							this._secInfo,
							this._queryBufferSize,
							this,
							new QueryDirCallbackArg(node.TraversalRootPath, prefix, false, true),
							cancellationToken);
					}
					catch (NtstatusException ex) when (ex.StatusCode is Ntstatus.STATUS_NO_SUCH_FILE)
					{
						// Ignore
					}
				}
			}
		}

		protected override void OnError(Smb2DirEntryNode node, Exception ex)
		{
			bool isFailure = !(ex is NtstatusException { StatusCode: Ntstatus.STATUS_NO_SUCH_FILE });
			if (isFailure)
			{
				var dirPath = node.TraversalRootPath.Append(node.Entry.RelativePath);
				this._context.Log.WriteError($"Error traversing {dirPath}: {ex.Message}");
			}
		}

		public bool OnDirEntry(Smb2DirEntry entry, QueryDirCallbackArg arg)
		{
			if (entry.FileName == "." || entry.FileName == "..")
				// Ignore
				return true;

			if (!string.IsNullOrEmpty(arg.RelativePrefix))
				entry.RelativePath = $"{arg.RelativePrefix}\\{entry.FileName}";

			if (arg.ShouldPrint)
			{
				this.Files++;
				this._context.WriteRecord(entry);
			}
			if (arg.ShouldYield)
			{
				this.OnYield(new Smb2DirEntryNode(arg.TraversalRoot, entry));
			}


			return true;
		}
	}
}
