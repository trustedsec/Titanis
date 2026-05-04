using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Titanis.Linterop.Fuse
{
	using mode_t = PosixFileMode;

	/// <summary>
	/// Implements a libfuse mount point.
	/// </summary>
	public class FuseMount
	{
		private FuseMount(IFuseNode rootNode, ILog? log, bool writable)
		{
			this._inodemgr = new InodeManager(RootNodeId, rootNode, log);
			this._log = log;
			this._writable = writable;
		}

		private readonly ILog? _log;
		private readonly bool _writable;

		/// <summary>
		/// Mounts a <see cref="IFuseNode"/>.
		/// </summary>
		/// <param name="mountpoint">File system path of mount point (must be absolute)</param>
		/// <param name="rootNode"><see cref="IFuseNode"/> to mount</param>
		/// <param name="log"><see cref="ILog"/> to receive logging</param>
		public static void Mount(string mountpoint, IFuseNode rootNode, ILog? log, bool writable, CancellationToken cancellationToken, params ReadOnlySpan<string> args)
		{
			ArgumentException.ThrowIfNullOrEmpty(mountpoint);
			ArgumentNullException.ThrowIfNull(rootNode);

			if (!mountpoint.StartsWith('/'))
				throw new ArgumentException("The mountpoint must be an absolute path.", nameof(mountpoint));

			FuseMount mount = new FuseMount(rootNode, log, writable);

			FuseSession session;
			using (var argv = new ArgvMarshal(args))
			{
				FuseSessionHandle h;
				unsafe
				{
					fixed (IntPtr* pArgv = argv.Argv)
					{
						fuse_args fuseargs = new fuse_args
						{
							argc = 1,
							argv = pArgv,
							allocated = 0
						};
						fuse_lowlevel_ops ops = new()
						{
							init = mount.fuse_init,
							destroy = mount.fuse_destroy,
							lookup = mount.fuse_lookup,
							forget = mount.fuse_forget,
							getattr = mount.fuse_getattr,
							setattr = writable ? mount.fuse_setattr : null,
							mknod = writable ? mount.fuse_mknod : null,
							mkdir = writable ? mount.fuse_mkdir : null,
							unlink = writable ? mount.fuse_unlink : null,
							rmdir = writable ? mount.fuse_rmdir : null,
							open = mount.fuse_open,
							read = mount.fuse_read,
							write = writable ? mount.fuse_write : null,
							flush = mount.fuse_flush,
							release = mount.fuse_release,
							fsync = mount.fuse_fsync,
							opendir = mount.fuse_opendir,
							readdir = mount.fuse_readdir,
							releasedir = mount.fuse_releasedir,
							setxattr = writable ? mount.fuse_setxattr : null,
							getxattr = mount.fuse_getxattr,
							listxattr = mount.fuse_listxattr,
							removexattr = writable ? mount.fuse_removexattr : null,
							access = mount.fuse_access,
							create = writable ? mount.fuse_create : null,
							write_buf = writable ? mount.fuse_write_buf : null,
#if DEBUG
							symlink = writable ? mount.fuse_symlink : null,
							rename = writable ? mount.fuse_rename : null,
							link = writable ? mount.fuse_link : null,
							statfs = mount.fuse_statfs,
							readlink = mount.fuse_readlink,
							fsyncdir = mount.fuse_fsyncdir,
							getlk = mount.fuse_getlk,
							setlk = mount.fuse_setlk,
							bmap = mount.fuse_bmap,
							ioctl = mount.fuse_ioctl,
							poll = mount.fuse_poll,
							retrieve_reply = mount.fuse_retrieve_reply,
							forget_multi = mount.fuse_forget_multi,
							flock = mount.fuse_flock,
							fallocate = mount.fuse_fallocate,
							readdirplus = mount.fuse_readdirplus,
							copy_file_range = writable ? mount.fuse_copy_file_range : null,
							lseek = mount.fuse_lseek,
#endif
						};
						h = FuseNativeMethods.fuse_session_new(
							fuseargs,
							ref ops,
							fuse_lowlevel_ops.StructSize,
							IntPtr.Zero
						);
						var res = Marshal.GetLastWin32Error();
						var msg = Marshal.GetLastPInvokeErrorMessage();
						if (h.IsInvalid)
							FuseHelper.ThrowLastError();

						session = new FuseSession(h);
					}
				}
			}

			session.Mount(mountpoint);
			session.RunLoop(cancellationToken);
			session.Dispose();

			FuseNativeMethods.umount(mountpoint);
		}

		private const int RootNodeId = 1;
		private const double DefaultAttributeTimeout = 1.0;
		private const double DefaultEntryTimeout = 1.0;

		private InodeManager _inodemgr;

		/// <summary>
		/// Connection info received by <see cref="fuse_lowlevel_ops.init"/> callback.
		/// </summary>
		private fuse_conn_info _connInfo;


		#region Opens
		private ConcurrentDictionary<long, IFuseOpenObject> _opens = new ConcurrentDictionary<long, IFuseOpenObject>();
		private long _lastOpenId;
		private ulong GetNextOpenId() => (ulong)Interlocked.Increment(ref this._lastOpenId);
		private IFuseOpenObject? TryGetOpen(ulong openId)
		{
			this._opens.TryGetValue((long)openId, out var open);
			return open;
		}
		private IFuseOpenObject GetOpen(ulong openId)
		{
			if (this._opens.TryGetValue((long)openId, out var open))
				return open;
			else
				throw new LinuxException(LinuxErrorCode.ENOENT);
		}
		private ulong AddOpen(IFuseOpenObject open)
		{
			var id = this.GetNextOpenId();
			this._opens.TryAdd((long)id, open);
			return id;
		}
		#endregion

		#region Thunks

		interface IRequestHandler
		{
			ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken);
		}

		private void HandleFuseRequest<T>(fuse_req_t req, fuse_ino_t ino, ref T handler)
			where T : struct, IRequestHandler
		{
			try
			{
				var inode = this._inodemgr.GetInode(ino);
				if (handler.Invoke(this, req, ino, inode, CancellationToken.None).Result)
					FuseNativeMethods.fuse_reply_err(req, 0);

				this._log?.WriteFuseCallSucceededMessage(req);
			}
			catch (Exception ex)
			{
				LinuxErrorCode errno = LinuxException.GetErrorCodeForException(ex);
				this._log?.WriteFuseCallFailedMessage(req, errno);
				FuseNativeMethods.fuse_reply_err(req, errno);
			}
		}

		private static void FuseSetBuf(fuse_req_t req, ArraySegment<byte> seg)
		{
			unsafe
			{
				fixed (byte* pBytes = seg.Array)
				{
					FuseNativeMethods.fuse_reply_buf(req, new IntPtr(pBytes + seg.Offset), seg.Count);
				}
			}
		}

		#endregion

		private void fuse_init(nint userdata, ref fuse_conn_info conn)
		{
			this._log?.WriteFusefuse_initMessage(
				conn.proto_major,
				conn.proto_minor,
				conn.capable
				);

			// Default: AsyncRead | RemoteLocking | OpenTruncate | SpliceRead | EmulateFlocks | IoctlDir | AutoInvalidateData | ReadDirPlus | ReadDirPlusAuto | AsyncDirectIo | ParallelDirOps | HandleKillPriv
			conn.want = FuseCaps.AsyncRead | FuseCaps.ParallelDirOps;
			//conn.want = FuseCaps.AsyncRead | FuseCaps.ReadDirPlus | FuseCaps.ReadDirPlusAuto | FuseCaps.ParallelDirOps;
			this._connInfo = conn;
		}

		#region Attributes
		struct GetAttrRequest : IRequestHandler
		{
			public ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var stat = GetAttributesOf(nodeInfo, mount._writable);

				FuseNativeMethods.fuse_reply_attr(req, in stat, DefaultAttributeTimeout);
				return ValueTask.FromResult(false);
			}
		}
		private void fuse_getattr(fuse_req_t req, fuse_ino_t ino, IntPtr /* fuse_file_info; not used */ fi)
		{
			this._log?.WriteFusefuse_getattrMessage(req, ino.value);
			var request = new GetAttrRequest();
			this.HandleFuseRequest(req, ino, ref request);
		}

		private static stat GetAttributesOf(InodeInfo nodeInfo, bool writable)
		{
			var stat = new stat
			{
				st_ino = new UIntPtr(nodeInfo.id.value),
				st_mode = GetEffectiveMode(nodeInfo.node.Mode, writable) | (PosixFileMode)nodeInfo.node.FileType,
				st_uid = nodeInfo.node.Uid,
				st_gid = nodeInfo.node.Gid,
				st_size = new nint(nodeInfo.node.FileSize),
				st_blksize = nodeInfo.node.BlockSize,
				st_blocks = nodeInfo.node.BlockCount,
				st_atime = nodeInfo.node.LastAccessTime.ToTimespec(),
				st_mtime = nodeInfo.node.LastWriteTime.ToTimespec(),
				st_ctime = nodeInfo.node.LastChangeTime.ToTimespec(),
				st_nlink = 2 // Number of hard links to this file
			};
			return stat;
		}
		#endregion

		#region Extended attributes
		struct GetXAttrRequest : IRequestHandler
		{
			internal string name;
			internal int bufferSize;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				try
				{
					FuseNativeMethods.fuse_reply_err(req, LinuxErrorCode.EOPNOTSUPP);
					return false;
					var attrData = await nodeInfo.node.GetXAttribute(name, bufferSize, cancellationToken).ConfigureAwait(false);

					if (!attrData.IsPresent)
						FuseNativeMethods.fuse_reply_err(req, LinuxErrorCode.EOPNOTSUPP);
					else if (bufferSize == 0)
						FuseNativeMethods.fuse_reply_xattr(req, attrData.RequiredSize);
					else if (attrData.RequiredSize > bufferSize)
						FuseNativeMethods.fuse_reply_err(req, LinuxErrorCode.ERANGE);
					else if (attrData.Data != null)
						FuseSetBuf(req, attrData.Data);
					else
						FuseNativeMethods.fuse_reply_err(req, LinuxErrorCode.ENOENT);
				}
				catch (KeyNotFoundException ex)
				{
					FuseNativeMethods.fuse_reply_err(req, LinuxErrorCode.EOPNOTSUPP);
					//FuseSetBuf(req, []);
				}

				return false;
			}
		}
		private void fuse_getxattr(fuse_req_t req, fuse_ino_t ino, string name, size_t size)
		{
			this._log?.WriteFusefuse_getxattrMessage(req, ino.value, name, size.value);

			var request = new GetXAttrRequest()
			{
				name = name,
				bufferSize = (int)Math.Min(int.MaxValue, size.value)
			};
			this.HandleFuseRequest(req, ino, ref request);
		}

		private void fuse_setxattr(fuse_req_t req, fuse_ino_t ino, string name, string value, size_t size, int flags)
		{
			this._log?.WriteFusefuse_setxattrMessage(req, ino, name);
			FuseNotSupported(req);
		}


		struct ListXAttrRequest : IRequestHandler
		{
			internal int bufferSize;

			public ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var names = nodeInfo.node.GetXAttributeNames();
				XAttrData xattr;
				if (names.IsNullOrEmpty())
					xattr = new XAttrData(0);
				else
				{
					string str = string.Join('\0', names) + '\0';
					if (str.Length > bufferSize)
						xattr = new XAttrData(str.Length);
					else
					{
						xattr = new XAttrData(str);
					}
				}

				if (bufferSize == 0)
					FuseNativeMethods.fuse_reply_xattr(req, xattr.RequiredSize);
				else if (xattr.RequiredSize > bufferSize)
					FuseNativeMethods.fuse_reply_err(req, LinuxErrorCode.ERANGE);
				else if (xattr.Data != null)
					FuseSetBuf(req, xattr.Data);
				else
					FuseNativeMethods.fuse_reply_err(req, LinuxErrorCode.ENOENT);

				return ValueTask.FromResult(false);
			}
		}
		private void fuse_listxattr(fuse_req_t req, fuse_ino_t ino, size_t size)
		{
			this._log?.WriteFusefuse_listxattrMessage(req, ino.value, size.value);

			ListXAttrRequest request = new ListXAttrRequest()
			{
				bufferSize = (int)Math.Min(int.MaxValue, size.value)
			};
			this.HandleFuseRequest(req, ino, ref request);
		}

		private void fuse_removexattr(fuse_req_t req, fuse_ino_t ino, string name)
		{
			this._log?.WriteFusefuse_removexattrMessage(req, ino);
			FuseNotSupported(req);
		}
		#endregion

		private void fuse_destroy(nint userdata)
		{
			this._log?.WriteFusefuse_destroyMessage();
			this._log?.WriteDiagnostic($"fuse_destroy");
		}

		struct LookupRequest : IRequestHandler
		{
			internal string name;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var child = await nodeInfo.node.Lookup(name, cancellationToken).ConfigureAwait(false);
				if (child is null)
					throw new KeyNotFoundException($"No child node named '{name}' within node at '{nodeInfo.path}'.");

				var childInode = mount._inodemgr.AllocOrRefInodeFor(Path.Combine(nodeInfo.path, name), child);
				fuse_entry_param entry = new fuse_entry_param
				{
					attr = GetAttributesOf(childInode, mount._writable),
					attr_timeout = DefaultAttributeTimeout,
					entry_timeout = DefaultEntryTimeout,
					ino = childInode.id,
					generation = 0
				};

				FuseNativeMethods.fuse_reply_entry(req, in entry);
				return false;
			}
		}
		private void fuse_lookup(fuse_req_t req, fuse_ino_t parent, string name)
		{
			this._log?.WriteFusefuse_lookupMessage(req, parent.value, name);

			LookupRequest request = new LookupRequest()
			{
				name = name
			};
			this.HandleFuseRequest(req, parent, ref request);
		}

		private void fuse_forget(fuse_req_t req, fuse_ino_t ino, ulong nlookup)
		{
			this._log?.WriteFusefuse_forgetMessage(req, ino);
			this._inodemgr.ForgetInode(ino, nlookup);
			FuseNativeMethods.fuse_reply_err(req, 0);
		}

		struct SetattrRequest : IRequestHandler
		{
			internal stat attr;
			internal FuseAttrMask toSet;
			internal ulong fh;

			public ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var attr = GetAttributesOf(nodeInfo, mount._writable);

				var now = DateTime.UtcNow;
				if (0 != (this.toSet & FuseAttrMask.ModifiedTime))
				{
					nodeInfo.node.LastWriteTime =
						(0 != (this.toSet & FuseAttrMask.ModifiedTimeNow)) ? now
						: this.attr.st_mtime.ToDateTime();
				}
				if (0 != (this.toSet & FuseAttrMask.AccessTime))
				{
					nodeInfo.node.LastAccessTime =
						(0 != (this.toSet & FuseAttrMask.AccessTimeNow)) ? now
						: this.attr.st_atime.ToDateTime();
				}
				if (0 != (this.toSet & FuseAttrMask.Size))
				{
					nodeInfo.node.FileSize = this.attr.st_size;
				}

				attr = GetAttributesOf(nodeInfo, mount._writable);
				FuseNativeMethods.fuse_reply_attr(req, attr, 0);
				return ValueTask.FromResult(false);
			}
		}

		private void fuse_setattr(fuse_req_t req, fuse_ino_t ino, ref stat attr, FuseAttrMask to_set, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_setattrMessage(req);

			var request = new SetattrRequest
			{
				attr = attr,
				toSet = to_set,
			};
			HandleFuseRequest(req, ino, ref request);
		}

		private void fuse_readlink(fuse_req_t req, fuse_ino_t ino)
		{
			this._log?.WriteFusefuse_readlinkMessage(req, ino);
			FuseNotSupported(req);
		}

		private void fuse_mknod(fuse_req_t req, fuse_ino_t parent, string name, mode_t mode, dev_t rdev)
		{
			this._log?.WriteFusefuse_mknodMessage(req, parent, name, mode);
			FuseNotSupported(req);
		}

		struct MkdirRequest : IRequestHandler
		{
			internal string name;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var dir = await nodeInfo.node.CreateDirectory(name, cancellationToken).ConfigureAwait(false);

				var childInode = mount._inodemgr.AllocOrRefInodeFor(Path.Combine(nodeInfo.path, name), dir);
				fuse_entry_param entry = new fuse_entry_param
				{
					attr = GetAttributesOf(childInode, mount._writable),
					attr_timeout = DefaultAttributeTimeout,
					entry_timeout = DefaultEntryTimeout,
					ino = childInode.id,
					generation = 0
				};
				FuseNativeMethods.fuse_reply_entry(req, in entry);
				return false;
			}
		}
		private void fuse_mkdir(fuse_req_t req, fuse_ino_t parent, string name, mode_t mode)
		{
			this._log?.WriteFusefuse_mkdirMessage(req, parent, name, mode);
			mode = this.GetEffectiveMode(mode);
			MkdirRequest request = new MkdirRequest()
			{
				name = name
			};
			this.HandleFuseRequest(req, parent, ref request);
		}

		struct UnlinkRequest : IRequestHandler
		{
			internal string name;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				await nodeInfo.node.DeleteFile(name, cancellationToken).ConfigureAwait(false);
				return true;
			}
		}
		private void fuse_unlink(fuse_req_t req, fuse_ino_t parent, string name)
		{
			this._log?.WriteFusefuse_unlinkMessage(req, parent, name);

			UnlinkRequest request = new UnlinkRequest
			{
				name = name
			};
			this.HandleFuseRequest(req, parent, ref request);
		}


		struct RmdirRequest : IRequestHandler
		{
			internal string name;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				await nodeInfo.node.DeleteDirectory(name, cancellationToken).ConfigureAwait(false);
				return true;
			}
		}
		private void fuse_rmdir(fuse_req_t req, fuse_ino_t parent, string name)
		{
			this._log?.WriteFusefuse_rmdirMessage(req, parent, name);

			RmdirRequest request = new RmdirRequest
			{
				name = name
			};
			this.HandleFuseRequest(req, parent, ref request);
		}

		private void fuse_symlink(fuse_req_t req, string link, fuse_ino_t parent, string name)
		{
			this._log?.WriteFusefuse_symlinkMessage(req, link, parent, name);
			FuseNotSupported(req);
		}

		private void fuse_rename(fuse_req_t req, fuse_ino_t parent, string name, fuse_ino_t newparent, string newname, uint flags)
		{
			this._log?.WriteFusefuse_renameMessage(req, parent, name, newparent, newname);
			FuseNotSupported(req);
		}

		private void fuse_link(fuse_req_t req, fuse_ino_t ino, fuse_ino_t newparent, string newname)
		{
			this._log?.WriteFusefuse_linkMessage(req, ino, newparent, newname);
			FuseNotSupported(req);
		}

		struct OpenRequest : IRequestHandler
		{
			internal FuseOpenFlags openFlags;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var openFlags = this.openFlags;

				if (!mount._writable)
					openFlags = FuseOpenFlags.ReadOnly;

				IFuseOpenFile file = await nodeInfo.node.OpenFile(openFlags, cancellationToken).ConfigureAwait(false);

				var id = mount.AddOpen(file);
				FuseNativeMethods.fuse_reply_open(req, new fuse_file_info
				{
					options = FuseFileInfoOptions.None,
					fh = id
				});

				return false;
			}
		}
		private void fuse_open(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_openMessage(req, ino, fi.flags);

			var request = new OpenRequest
			{
				openFlags = fi.flags
			};
			this.HandleFuseRequest(req, ino, ref request);
		}

		struct ReadRequest : IRequestHandler
		{
			internal int size;
			internal long offset;
			internal ulong fh;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var open = (IFuseOpenFile)mount.GetOpen(this.fh);

				byte[] buf = new byte[this.size];
				var cbRead = await open.ReadAsync(offset, buf, cancellationToken).ConfigureAwait(false);
				FuseSetBuf(req, new ArraySegment<byte>(buf, 0, cbRead));

				return false;
			}
		}
		private void fuse_read(fuse_req_t req, fuse_ino_t ino, size_t size, off_t off, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_readMessage(req, ino, off, size);

			var request = new ReadRequest()
			{
				size = (int)Math.Min(int.MaxValue, size.value),
				offset = off.value,
				fh = fi.fh
			};
			this.HandleFuseRequest(req, ino, ref request);
		}

		private void fuse_write(fuse_req_t req, fuse_ino_t ino, byte[] buf, nint size, off_t off, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_writeMessage(req, ino, off, size);
			FuseNotSupported(req);
		}

		struct FlushRequest : IRequestHandler
		{
			internal ulong fh;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var open = (IFuseOpenFile)mount.GetOpen(this.fh);
				await open.FlushAsync(cancellationToken).ConfigureAwait(false);

				return true;
			}
		}
		private void fuse_flush(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_flushMessage(req, ino);

			var request = new FlushRequest()
			{
				fh = fi.fh
			};
			this.HandleFuseRequest(req, ino, ref request);
		}

		private void fuse_release(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_releaseMessage(req, ino);
			ReleaseOpen(req, ino, fi);
		}

		private void ReleaseOpen(fuse_req_t req, fuse_ino_t ino, fuse_file_info fi)
		{
			var open = this.TryGetOpen(fi.fh);
			if (open == null)
				FuseNativeMethods.fuse_reply_err(req, LinuxErrorCode.ENOENT);

			open.Dispose();

			FuseNativeMethods.fuse_reply_err(req, 0);
		}

		struct FsyncRequest : IRequestHandler
		{
			internal ulong fh;
			internal bool dataOnly;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var open = (IFuseOpenFile)mount.GetOpen(this.fh);
				await open.FsyncAsync(cancellationToken).ConfigureAwait(false);

				return true;
			}
		}

		private void fuse_fsync(fuse_req_t req, fuse_ino_t ino, int datasync, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_fsyncdirMessage(req, ino);
			var request = new FsyncRequest()
			{
				fh = fi.fh,
				dataOnly = datasync != 0
			};
			this.HandleFuseRequest(req, ino, ref request);
		}

		struct OpendirRequest : IRequestHandler
		{
			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				IFuseOpenDirectory dir;
				try
				{
					dir = await nodeInfo.node.OpenDirectory(cancellationToken).ConfigureAwait(false);
				}
				catch (NotSupportedException ex)
				{
					throw new LinuxException(LinuxErrorCode.ENOTDIR);
				}

				var fi = new fuse_file_info
				{
					options = FuseFileInfoOptions.None,
					fh = mount.AddOpen(dir)
				};
				FuseNativeMethods.fuse_reply_open(req, ref fi);
				return false;
			}
		}
		private void fuse_opendir(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_opendirMessage(req, ino.value);

			var request = new OpendirRequest()
			{

			};
			this.HandleFuseRequest(req, ino, ref request);
		}

		struct ReaddirRequest : IRequestHandler
		{
			internal ulong fh;
			internal int size;
			internal long off;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var open = mount.GetOpen(fh);
				if (open is IFuseOpenDirectory dir)
				{
					FuseDirBuffer buf = new FuseDirBuffer(req, size);
					dir.Seek(off);

					IFuseNode? child;
					while ((child = await dir.ReadNextAsync(cancellationToken).ConfigureAwait(false)) != null)
					{
						InodeInfo childNode;
						string childName;
						if (child == dir.Node)
						{
							childName = ".";
							childNode = nodeInfo;
						}
						else
						{
							childName = child.Name;
							string childPath = Path.Combine(nodeInfo.path, child.Name);
							childNode = mount._inodemgr.AllocOrRefInodeFor(childPath, child);
						}

						var stat = GetAttributesOf(childNode, mount._writable);
						if (!buf.TryAppend(stat, childName, dir.NextOffset))
						{
							mount._inodemgr.ForgetInode(childNode, 1);
							break;
						}
						break;
					}

					FuseSetBuf(req, buf.AsSegment());
					return false;
				}
				else
					throw new LinuxException(LinuxErrorCode.ENOTDIR);
			}
		}
		private void fuse_readdir(fuse_req_t req, fuse_ino_t ino, size_t size, off_t off, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_readdirMessage(req, ino.value, size.value, off.value);

			var request = new ReaddirRequest()
			{
				fh = fi.fh,
				size = (int)Math.Min(int.MaxValue, size.value),
				off = off.value
			};
			this.HandleFuseRequest(req, ino, ref request);
		}

		private void fuse_releasedir(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_releasedirMessage(req, ino.value);

			var open = this.TryGetOpen(fi.fh);
			if (open == null)
				FuseNativeMethods.fuse_reply_err(req, LinuxErrorCode.ENOENT);

			open.Dispose();

			FuseNativeMethods.fuse_reply_err(req, 0);
		}

		private void fuse_fsyncdir(fuse_req_t req, fuse_ino_t ino, int datasync, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_fsyncdirMessage(req, ino);
			FuseNotSupported(req);
		}

		private void fuse_statfs(fuse_req_t req, fuse_ino_t ino)
		{
			this._log?.WriteFusefuse_statfsMessage(req, ino);
			statvfs stat = new statvfs();
			FuseNativeMethods.statvfs(ref stat);
			FuseNativeMethods.fuse_reply_statfs(req, stat);
		}

		private void fuse_access(fuse_req_t req, fuse_ino_t ino, PosixFileMode mask)
		{
			this._log?.WriteFusefuse_accessMessage(req, ino.value, mask);
			FuseNativeMethods.fuse_reply_err(req, 0);
		}

		struct CreateRequest : IRequestHandler
		{
			internal string name;
			internal mode_t mode;
			internal FuseOpenFlags flags;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				IFuseOpenFile file;
				try
				{
					var access = (this.flags & FuseOpenFlags.AccessMask) switch
					{
						FuseOpenFlags.ReadOnly => FileAccess.Read,
						FuseOpenFlags.WriteOnly => FileAccess.Write,
						FuseOpenFlags.ReadWrite => FileAccess.ReadWrite,
					};
					file = await nodeInfo.node.CreateFile(name, flags, cancellationToken).ConfigureAwait(false);
				}
				catch (NotSupportedException ex)
				{
					throw new LinuxException(LinuxErrorCode.EIO);
				}

				string childPath = Path.Combine(nodeInfo.path, this.name);
				var childIno = mount._inodemgr.AllocOrRefInodeFor(childPath, file.Node);
				var id = mount.AddOpen(file);
				var entry = new fuse_entry_param
				{
					ino = childIno.id,
					attr = GetAttributesOf(childIno, mount._writable)
				};
				FuseNativeMethods.fuse_reply_create(req, ref entry, new fuse_file_info
				{
					options = FuseFileInfoOptions.None,
					fh = id
				});

				return false;
			}
		}

		private mode_t GetEffectiveMode(mode_t mode) => GetEffectiveMode(mode, this._writable);
		private static mode_t GetEffectiveMode(mode_t mode, bool writable)
		{
			if (!writable)
			{
				var access = mode & PosixFileMode.Mode777;
				mode = (mode & ~PosixFileMode.Mode777) | (access & PosixFileMode.ModeReadExecuteAll);
			}
			return mode;
		}

		private void fuse_create(fuse_req_t req, fuse_ino_t parent, string name, mode_t mode, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_createMessage(req, parent, name, mode, fi.flags);
			mode = this.GetEffectiveMode(mode);
			var request = new CreateRequest
			{
				name = name,
				mode = mode,
				flags = fi.flags
			};
			HandleFuseRequest(req, parent, ref request);
		}

		private void fuse_getlk(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi, ref flock lock_)
		{
			this._log?.WriteFusefuse_getlkMessage(req, ino);
			FuseNotSupported(req);
		}

		private void fuse_setlk(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi, ref flock lock_, int sleep)
		{
			this._log?.WriteFusefuse_setlkMessage(req, ino);
			FuseNotSupported(req);
		}

		private void fuse_bmap(fuse_req_t req, fuse_ino_t ino, size_t blocksize, ulong idx)
		{
			this._log?.WriteFusefuse_bmapMessage(req, ino);
			FuseNotSupported(req);
		}

		private void fuse_ioctl(fuse_req_t req, fuse_ino_t ino, uint cmd, nint arg, ref fuse_file_info fi, uint flags, nint in_buf, size_t in_bufsz, size_t out_bufsz)
		{
			this._log?.WriteFusefuse_ioctlMessage(req, ino, cmd, arg, flags);
			FuseNotSupported(req);
		}

		private void fuse_poll(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi, ref fuse_pollhandle ph)
		{
			this._log?.WriteFusefuse_pollMessage(req, ino);
			FuseNotSupported(req);
		}

		struct WriteBufRequest : IRequestHandler
		{
			internal FuseBufferList bufferList;
			internal off_t off;
			internal ulong fh;

			public async ValueTask<bool> Invoke(FuseMount mount, fuse_req_t req, fuse_ino_t ino, InodeInfo nodeInfo, CancellationToken cancellationToken)
			{
				var file = (IFuseOpenFile)mount.GetOpen(this.fh);
				int cbWritten = await file.WriteAsync(off.value, bufferList, cancellationToken).ConfigureAwait(false);
				FuseNativeMethods.fuse_reply_write(req, cbWritten);
				return false;
			}
		}
		private void fuse_write_buf(fuse_req_t req, fuse_ino_t ino, ref fuse_bufvec bufv, off_t off, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_write_bufMessage(req, ino);

			var request = new WriteBufRequest
			{
				bufferList = new FuseBufferList(ref bufv),
				off = off,
				fh = fi.fh
			};
			HandleFuseRequest(req, ino, ref request);
		}

		private void fuse_retrieve_reply(fuse_req_t req, nint cookie, fuse_ino_t ino, off_t offset, ref fuse_bufvec bufv)
		{
			this._log?.WriteFusefuse_retrieve_replyMessage(req, ino);
			FuseNotSupported(req);
		}

		private void fuse_forget_multi(fuse_req_t req, size_t count, ref fuse_forget_data forgets)
		{
			this._log?.WriteFusefuse_forget_multiMessage(req);
			FuseNotSupported(req);
		}

		private void fuse_flock(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi, int op)
		{
			this._log?.WriteFusefuse_flockMessage(req, ino);
			FuseNotSupported(req);
		}

		private void fuse_fallocate(fuse_req_t req, fuse_ino_t ino, int mode, off_t offset, off_t length, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_fallocateMessage(req, ino);
			FuseNotSupported(req);
		}

		private void fuse_readdirplus(fuse_req_t req, fuse_ino_t ino, size_t size, off_t off, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_readdirplusMessage(req, ino);
			FuseNotSupported(req);
		}

		private void fuse_copy_file_range(fuse_req_t req, fuse_ino_t ino_in, off_t off_in, ref fuse_file_info fi_in, fuse_ino_t ino_out, off_t off_out, ref fuse_file_info fi_out, size_t len, int flags)
		{
			this._log?.WriteFusefuse_copy_file_rangeMessage(req, ino_in, ino_out);
			FuseNotSupported(req);
		}

		private void fuse_lseek(fuse_req_t req, fuse_ino_t ino, off_t off, int whence, ref fuse_file_info fi)
		{
			this._log?.WriteFusefuse_lseekMessage(req, ino);
			FuseNotSupported(req);
		}

		private void FuseNotSupported(fuse_req_t req, [CallerMemberName] string? caller = null)
		{
			this._log?.WriteError($"*** UNSUPPORTED CALL {caller}");
			FuseNativeMethods.fuse_reply_err(req, LinuxErrorCode.ENOSYS);
		}
	}
}
