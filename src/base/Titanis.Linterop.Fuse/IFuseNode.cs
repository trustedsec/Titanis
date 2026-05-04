using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	/// <summary>
	/// Represents a node within a libfuse-mounted file system.
	/// </summary>
	public interface IFuseNode
	{
		/// <summary>
		/// Name of the node
		/// </summary>
		string Name { get; }
		/// <summary>
		/// Gets the POSIX file mode.
		/// </summary>
		PosixFileMode Mode { get; }
		/// <summary>
		/// Gets the Linux device type.
		/// </summary>
		LinuxFileType FileType { get; }
		/// <summary>
		/// Gets the user ID.
		/// </summary>
		uint Uid { get; }
		/// <summary>
		/// Gets the group ID.
		/// </summary>
		uint Gid { get; }
		/// <summary>
		/// Gets the size of the file
		/// </summary>
		long FileSize { get; set; }
		/// <summary>
		/// Gets the block size.
		/// </summary>
		long BlockSize { get; }
		/// <summary>
		/// Gets the number of blocks.
		/// </summary>
		long BlockCount { get; }
		/// <summary>
		/// Gets the time of the last access to the file.
		/// </summary>
		DateTime? LastAccessTime { get; set; }
		/// <summary>
		/// Gets the time of the last write to the file.
		/// </summary>
		DateTime? LastWriteTime { get; set; }
		/// <summary>
		/// Gets the time of the last change to the file.
		/// </summary>
		DateTime? LastChangeTime { get; }

		string[]? GetXAttributeNames();

		/// <summary>
		/// Gets extended attribute data.
		/// </summary>
		/// <param name="name">Attribute name</param>
		/// <param name="bufferSize">Buffer size allocated by caller</param>
		/// <returns><see cref="XAttrData"/> describing the attribute data</returns>
		/// <remarks>
		/// If the attribute data is larger than <paramref name="bufferSize"/>, return a <see cref="XAttrData"/> indicating the required size.
		/// </remarks>
		ValueTask<XAttrData> GetXAttribute(string name, int bufferSize, CancellationToken cancellationToken);

		/// <summary>
		/// Gets a child node by name.
		/// </summary>
		/// <param name="name">Name of node</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Child node named by <paramref name="name"/></returns>
		Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken);

		Task<IFuseOpenFile> CreateFile(string name, FuseOpenFlags flags, CancellationToken cancellationToken);
		Task<IFuseOpenFile> OpenFile(FuseOpenFlags openFlags, CancellationToken cancellationToken);
		Task DeleteFile(string name, CancellationToken cancellationToken);
		Task<IFuseNode> CreateDirectory(string name, CancellationToken cancellationToken);
		/// <summary>
		/// Opens the node as a directory.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>An object implementing <see cref="IFuseOpenDirectory"/> that represents the open directory</returns>
		/// <remarks>
		/// If the node does not represent a directory, it should throw <see cref="NotSupportedException"/>.
		/// </remarks>
		Task<IFuseOpenDirectory> OpenDirectory(CancellationToken cancellationToken);
		Task DeleteDirectory(string name, CancellationToken cancellationToken);
	}

	public interface IFuseOpenObject : IDisposable
	{
		IFuseNode Node { get; }
	}

	/// <summary>
	/// Provides functionality to interact with an open directory.
	/// </summary>
	public interface IFuseOpenDirectory : IFuseOpenObject
	{
		long NextOffset { get; }

		/// <summary>
		/// Reads the next directory entry.
		/// </summary>
		/// <returns>An object implementing <see cref="IFuseNode"/> representing the node</returns>
		Task<IFuseNode?> ReadNextAsync(CancellationToken cancellationToken);
		/// <summary>
		/// Seeks to an offset.
		/// </summary>
		/// <param name="offset">Target offset</param>
		void Seek(long offset);
	}

	public class FuseBufferList
	{
		internal FuseBufferList(ref fuse_bufvec bufv)
		{
			this._bufv = bufv;
			this.bufs = MemoryMarshal.CreateSpan(ref bufv.buf, this.BufferCount).ToArray();
		}

		private fuse_bufvec _bufv;
		private fuse_buf[] bufs;

		public int BufferCount => (int)Math.Min(int.MaxValue, this._bufv.count.value);
		public ulong GetBufferSize(int index) => this.bufs[index].size.value;
		public Span<byte> GetBytes(int index)
		{
			unsafe
			{
				return new Span<byte>(this.bufs[index].mem.ToPointer(), (int)Math.Min(int.MaxValue, this.bufs[index].size.value));
			}
		}
	}

	public interface IFuseOpenFile : IFuseOpenObject
	{
		Task FlushAsync(CancellationToken cancellationToken);
		Task FsyncAsync(CancellationToken cancellationToken);
		Task<int> ReadAsync(long startOffset, byte[] buf, CancellationToken cancellationToken);
		Task<int> WriteAsync(long startOffset, FuseBufferList bufferList, CancellationToken cancellationToken);
	}
}
