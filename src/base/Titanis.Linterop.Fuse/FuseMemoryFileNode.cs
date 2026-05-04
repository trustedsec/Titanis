using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	public interface IFuseOpenFileOwner : IFuseNode
	{
		Task Commit(CancellationToken cancellationToken);
		void GrowFileTo(int minSize);

		public byte[] Contents { get; }
	}

	public class FuseMemoryFileNode : IFuseNode, IFuseOpenFileOwner
	{
		public FuseMemoryFileNode(
			string name,
			uint uid,
			uint gid,
			byte[] contents,
			PosixFileMode mode = PosixFileMode.ModeReadAll
			)
		{
			ArgumentException.ThrowIfNullOrEmpty(name);
			ArgumentNullException.ThrowIfNull(contents);

			this.Name = name;
			this.Mode = mode;
			this.Uid = uid;
			this.Gid = gid;
			this.Contents = contents;
		}

		public string Name { get; }
		public PosixFileMode Mode { get; }
		public LinuxFileType FileType => LinuxFileType.RegularFile;

		public uint Uid { get; }
		public uint Gid { get; }

		private byte[] _contents;
		public byte[] Contents { get => _contents; set => _contents = value; }
		public void GrowFileTo(int minSize)
		{
			Array.Resize(ref this._contents, minSize);
		}

		public long FileSize
		{
			get => this.Contents.Length;
			set
			{
				Array.Resize(ref this._contents, checked((int)value));
			}
		}

		public long BlockSize => 512;

		public long BlockCount => this.FileSize / this.BlockSize;

		public virtual DateTime? LastAccessTime { get; set; }

		public virtual DateTime? LastWriteTime { get; set; }

		public virtual DateTime? LastChangeTime { get; set; }

		public Task<IFuseNode> CreateDirectory(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IFuseOpenFile> CreateFile(string name, FuseOpenFlags flags, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task DeleteDirectory(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task DeleteFile(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public ValueTask<XAttrData> GetXAttribute(string name, int bufferSize, CancellationToken cancellationToken)
		{
			return ValueTask.FromResult(XAttrData.NotPresent);
		}

		public string[]? GetXAttributeNames() => null;

		public Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IFuseOpenDirectory> OpenDirectory(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IFuseOpenFile> OpenFile(FuseOpenFlags openFlags, CancellationToken cancellationToken)
		{
			return Task.FromResult<IFuseOpenFile>(new FuseMemoryFile(this));
		}

		Task IFuseOpenFileOwner.Commit(CancellationToken cancellationToken) => this.Commit(cancellationToken);

		protected virtual async Task Commit(CancellationToken cancellationToken)
		{

		}
	}
}
