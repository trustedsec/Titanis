using System.Text;
using Titanis.Linterop.Fuse;

namespace Titanis.Msrpc.Mswmi.Fusion
{
	class WmiMountInfo
	{
		internal WmiClient wmiClient;
		internal string locale;
		internal uint uid;
		internal uint gid;
	}

	public abstract class WmiNodeBase : IFuseNode
	{
		private protected WmiNodeBase(WmiMountInfo mountInfo, string name)
		{
			this._mountInfo = mountInfo;
			this.Name = name;
		}

		internal readonly WmiMountInfo _mountInfo;
		internal WmiClient wmiClient => this._mountInfo.wmiClient;

		public string Name { get; }

		public PosixFileMode Mode => PosixFileMode.ModeReadAll;

		public abstract LinuxFileType FileType { get; }

		public uint Uid => this._mountInfo.uid;

		public uint Gid => this._mountInfo.gid;

		public abstract long FileSize { get; }
		long IFuseNode.FileSize { get => this.FileSize; set { } }

		public long BlockSize => 512;

		public long BlockCount => this.FileSize / BlockSize;

		DateTime? IFuseNode.LastAccessTime { get => null; set { } }

		DateTime? IFuseNode.LastWriteTime { get => null; set { } }

		DateTime? IFuseNode.LastChangeTime => null;

		public virtual Task<IFuseNode> CreateDirectory(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual Task<IFuseOpenFile> CreateFile(string name, FuseOpenFlags flags, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual Task DeleteDirectory(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual Task DeleteFile(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual ValueTask<XAttrData> GetXAttribute(string name, int bufferSize, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual string[]? GetXAttributeNames() => null;

		public virtual Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual Task<IFuseOpenDirectory> OpenDirectory(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public virtual Task<IFuseOpenFile> OpenFile(FuseOpenFlags openFlags, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}

	public abstract class WmiContainerNode : WmiNodeBase
	{
		private protected WmiContainerNode(WmiMountInfo mountInfo, string name) : base(mountInfo, name)
		{
		}

		public override LinuxFileType FileType => LinuxFileType.Directory;

		public override long FileSize => 0x1000;

	}

	#region Namespace
	public sealed class WmiNamespaceNode : WmiContainerNode
	{
		internal WmiNamespaceNode(WmiMountInfo mountInfo, string path, string name, WmiScope scope)
			: base(mountInfo, name)
		{
			this.WmiPath = path;
			Scope = scope;
		}

		public string WmiPath { get; }
		public WmiScope Scope { get; }
		public string[] NamespaceList { get; internal set; }
		public Dictionary<string, WmiClassObject> ClassList { get; internal set; }

		public override Task<IFuseOpenDirectory> OpenDirectory(CancellationToken cancellationToken)
		{
			return Task.FromResult<IFuseOpenDirectory>(new WmiScopeOpenDir(this));
		}

		public override async Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken)
		{
			if (this.NamespaceList?.Contains(name) ?? false)
				return await TryLookupNamespace(name, cancellationToken).ConfigureAwait(false);
			else if (this.ClassList?.TryGetValue(name, out var klass) ?? false)
			{
				return new WmiClassNode(this._mountInfo, this.Scope, (WmiClassObject)klass);
			}
			else if (this.NamespaceList is null || this.ClassList is null)
			{
				var node = await TryLookupNamespace(name, cancellationToken).ConfigureAwait(false);
				if (node == null)
					node = await TryLookupClass(name, cancellationToken).ConfigureAwait(false);

				return node;
			}
			else
				return null;
		}

		private async Task<WmiClassNode?> TryLookupClass(string name, CancellationToken cancellationToken)
		{
			WmiClassNode? node;
			var obj = await Scope.GetObjectAsync(name, cancellationToken).ConfigureAwait(false);
			if (obj != null)
			{
				return new WmiClassNode(this._mountInfo, this.Scope, (WmiClassObject)obj);
			}
			else
				return null;
		}

		private async Task<IFuseNode?> TryLookupNamespace(string name, CancellationToken cancellationToken)
		{
			WmiObject? obj;
			try
			{
				obj = await Scope.GetObjectAsync($"__NAMESPACE.Name=\"{name}\"", cancellationToken).ConfigureAwait(false);
			}
			catch (WmiException ex) { obj = null; }

			if (obj != null)
			{
				string path = $"{this.WmiPath}\\{name}";
				var child = await wmiClient.OpenNamespace(path, _mountInfo.locale, cancellationToken).ConfigureAwait(false);
				return new WmiNamespaceNode(this._mountInfo, path, name, child);
			}
			else
				return null;
		}
	}

	class WmiScopeOpenDir : OpenDirBase
	{
		internal WmiScopeOpenDir(WmiNamespaceNode node)
		{
			this._node = node;
		}

		private WmiNamespaceNode _node;
		public override IFuseNode Node => this._node;

		private string[]? _nameList;

		protected override async Task<IFuseNode?> ReadNextAsync(int index, CancellationToken cancellationToken)
		{
			if (this._nameList == null)
			{
				var nsList = new List<string>();

				var reader = await this._node.Scope.ExecuteWqlQueryAsync($"SELECT * FROM __namespace", 20, cancellationToken).ConfigureAwait(false);
				while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
				{
					var nsobj = (WmiInstanceObject)reader.Current;
					var ns = (string)nsobj["Name"];
					nsList.Add(ns);
				}
				this._node.NamespaceList = nsList.ToArray();

				reader = await this._node.Scope.ExecuteWqlQueryAsync($"SELECT * FROM meta_class", 20, cancellationToken).ConfigureAwait(false);
				var classList = new Dictionary<string, WmiClassObject>();
				while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
				{
					var clsobj = (WmiClassObject)reader.Current;
					var name = clsobj.Name;
					classList.Add(name, clsobj);
					nsList.Add(name);
				}
				this._node.ClassList = classList;

				this._nameList = nsList.ToArray();
			}

			if (index < this._nameList.Length)
			{
				var name = this._nameList[index];
				var node = await this._node.Lookup(name, cancellationToken).ConfigureAwait(false);
				return node;
			}
			else
				return null;
		}
	}
	#endregion
	#region Classes
	public sealed class WmiClassNode : WmiContainerNode
	{
		internal WmiClassNode(WmiMountInfo mountInfo, WmiScope scope, WmiClassObject klass)
			: base(mountInfo, klass.Name)
		{
			this.Class = klass;
			this.Scope = scope;
		}

		private const string MofName = "mof";

		public WmiClassObject Class { get; }
		public WmiScope Scope { get; }


		internal Dictionary<string, WmiInstanceNode> instanceList;

		public override Task<IFuseOpenDirectory> OpenDirectory(CancellationToken cancellationToken)
		{
			return Task.FromResult<IFuseOpenDirectory>(new WmiClassOpenDir(this));
		}

		public override async Task DeleteFile(string name, CancellationToken cancellationToken)
		{
			var node = await this.Lookup(name, cancellationToken).ConfigureAwait(false) as WmiInstanceNode;
			if (node != null)
				await Scope.DeleteInstance(node.Instance.RelativePath, cancellationToken).ConfigureAwait(false);
		}

		public override async Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken)
		{
			if (name == MofName)
			{
				return this.CreateMofNode();
			}
			else if (this.instanceList != null && this.instanceList.TryGetValue(name, out var node))
				return node;
			else if (this.Class.IsSingleton && name == SingletonName)
			{
				var obj = (WmiInstanceObject)await Scope.ExecuteWqlQuerySingleAsync($"SELECT * FROM {Class.Name}", cancellationToken).ConfigureAwait(false);
				return new WmiInstanceNode(this._mountInfo, this.Scope, SingletonName, (WmiInstanceObject)obj);
			}
			else if (this.Class.KeyProperty != null)
			{
				WmiProperty keyProp = this.Class.KeyProperty;
				var obj = (WmiInstanceObject?)await Scope.GetObjectAsync(keyProp.PropertyType.IsStringLike() ? $"{this.Class}.{keyProp.Name}=\"{name}\"" : $"{this.Class}.{keyProp.Name}={name}", cancellationToken).ConfigureAwait(false);
				if (obj != null)
					return CreateInstanceNode(name, obj);

				return null;
			}
			else
				return null;
		}

		internal WmiInstanceNode CreateInstanceNode(string name, WmiInstanceObject obj)
		{
			return new WmiInstanceNode(this._mountInfo, this.Scope, name, obj);
		}

		internal IFuseNode? CreateMofNode()
		{
			return new WmiInstanceNode(this._mountInfo, this.Scope, MofName, this.Class);
		}

		public const string SingletonName = "instance";
	}

	class WmiClassOpenDir : OpenDirBase
	{
		internal WmiClassOpenDir(WmiClassNode node)
		{
			this._node = node;
		}

		private WmiClassNode _node;
		public override IFuseNode Node => this._node;

		private string[]? _keyList;

		protected override async Task<IFuseNode?> ReadNextAsync(int index, CancellationToken cancellationToken)
		{
			if (this._keyList == null)
			{
				var keyList = new List<string>();

				var reader = await this._node.Scope.ExecuteWqlQueryAsync($"SELECT * FROM {this._node.Name}", 20, cancellationToken).ConfigureAwait(false);
				var instanceList = new Dictionary<string, WmiInstanceNode>();
				while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
				{
					var obj = (WmiInstanceObject)reader.Current;
					var key = this._node.Class.IsSingleton ? WmiClassNode.SingletonName : (obj.Key?.ToString() ?? "<unnamed>");
					if (!string.IsNullOrEmpty(key))
					{
						keyList.Add(key);
						instanceList.Add(key, this._node.CreateInstanceNode(key, obj));
					}
				}
				this._node.instanceList = instanceList;

				this._keyList = keyList.ToArray();
			}

			if (index == 0)
			{
				return this._node.CreateMofNode();
			}
			else
			{
				index--;
				if (index < this._keyList.Length)
				{
					var name = this._keyList[index];
					var scope = await this._node.Lookup(name, cancellationToken).ConfigureAwait(false);
					return scope;
				}
				else
					return null;
			}
		}
	}
	#endregion

	public class WmiInstanceNode : WmiNodeBase, IFuseOpenFileOwner
	{
		internal WmiInstanceNode(WmiMountInfo mountInfo, WmiScope scope, string name, WmiObject instance)
			: base(mountInfo, name)
		{
			this._scope = scope;
			this.Instance = instance;

			this._bytes = Encoding.UTF8.GetBytes(instance.ToMof() ?? string.Empty);
		}

		private byte[] _bytes;
		private readonly WmiScope _scope;

		public WmiObject Instance { get; }

		public override LinuxFileType FileType => LinuxFileType.RegularFile;

		public override long FileSize => this._bytes.Length;

		public override Task<IFuseOpenFile> OpenFile(FuseOpenFlags openFlags, CancellationToken cancellationToken)
		{
			return Task.FromResult<IFuseOpenFile>(new WmiObjectFile(this));
		}

		public byte[] Contents => this._bytes;

		public Task Commit(CancellationToken cancellationToken) => Task.CompletedTask;

		public void GrowFileTo(int minSize)
		{
		}
	}

	class WmiObjectFile : FuseMemoryFile
	{
		public WmiObjectFile(IFuseOpenFileOwner owner) : base(owner)
		{
		}

		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		public override Task FsyncAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
