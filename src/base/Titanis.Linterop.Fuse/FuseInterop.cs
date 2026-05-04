using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using int64_t = long;
using uint64_t = ulong;
using int32_t = int;
using uint32_t = uint;
using int16_t = short;
using uint16_t = ushort;
using System.Runtime.InteropServices;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Titanis.Linterop.Fuse;

using mode_t = PosixFileMode;

[StructLayout(LayoutKind.Explicit, Size = 112)]
struct statvfs
{

}

static class FuseNativeMethods
{
	const string Libc = "libc";
	const string LibfuseName = "libfuse3.so";

	[DllImport(Libc)]
	internal static extern int statvfs(ref statvfs stat);
	[DllImport(Libc)]
	internal static extern int umount([MarshalAs(UnmanagedType.LPStr)] string path);

	[DllImport(LibfuseName, SetLastError = true)]
	internal static extern FuseSessionHandle fuse_session_new(
		in fuse_args args,
		ref fuse_lowlevel_ops op,
		int /* size_t */ op_size,
		IntPtr userdata);

	[DllImport(LibfuseName)]
	internal static extern void fuse_session_destroy(IntPtr se);

	[DllImport(LibfuseName, SetLastError = true)]
	internal static extern int fuse_session_mount(FuseSessionHandle se, [MarshalAs(UnmanagedType.LPStr)] string mountpoint);

	[DllImport(LibfuseName)]
	internal static extern int fuse_session_loop(FuseSessionHandle se);

	[DllImport(LibfuseName)]
	internal static extern int fuse_session_exit(FuseSessionHandle se);

	#region Reply
	[DllImport(LibfuseName)]
	internal static extern int fuse_reply_err(fuse_req_t req, LinuxErrorCode err);

	[DllImport(LibfuseName)]
	internal static extern int fuse_reply_statfs(fuse_req_t req, ref readonly statvfs stbuf);

	[DllImport(LibfuseName)]
	internal static extern int fuse_reply_open(fuse_req_t req, ref readonly fuse_file_info fi);

	[DllImport(LibfuseName)]
	internal static extern int fuse_reply_create(fuse_req_t req, ref readonly fuse_entry_param entry, ref readonly fuse_file_info fi);

	[DllImport(LibfuseName)]
	internal static extern int fuse_reply_attr(fuse_req_t req, ref readonly stat attr, double attr_timeout);

	[DllImport(LibfuseName)]
	internal static extern int fuse_reply_xattr(fuse_req_t req, size_t count);

	[DllImport(LibfuseName)]
	internal static extern int fuse_reply_write(fuse_req_t req, size_t count);

	[DllImport(LibfuseName)]
	internal static extern int fuse_reply_data(fuse_req_t req, ref readonly fuse_bufvec bufv, FuseBufferCopyFlags flags);

	[DllImport(LibfuseName)]
	internal static extern int fuse_reply_entry(fuse_req_t req, ref readonly fuse_entry_param e);

	[DllImport(LibfuseName)]
	internal static extern int fuse_reply_buf(
		fuse_req_t req,
		IntPtr buf,
		size_t size
		);
	#endregion

	[DllImport(LibfuseName)]
	internal static extern unsafe size_t fuse_add_direntry(
		fuse_req_t req,
		byte* buf,
		size_t bufsize,
		[MarshalAs(UnmanagedType.LPStr)] string name,
		in stat stbuf,
		off_t off
		);
}

[Flags]
enum FuseBufferCopyFlags
{
	None = 0,

	NoSplice = (1 << 1),
	ForceSplice = (1 << 2),
	SpliceMove = (1 << 3),
	SpliceNonBlock = (1 << 4)
}

struct fuse_arg
{
	[MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 0)]
	internal string? value;

	public static implicit operator fuse_arg(string? value) => new fuse_arg() { value = value };
}
struct fuse_args
{
	/** Argument count */
	internal int argc;

	/** Argument vector.  NULL terminated */

	internal unsafe IntPtr* /* char ** */ argv;
	//internal fuse_arg[] /* char ** */ argv;

	/** Is 'argv' allocated? */
	internal int allocated;
}

struct fuse_cmdline_opts
{
	internal int singlethread;
	internal int foreground;
	internal int debug;
	internal int nodefault_subtype;
	[MarshalAs(UnmanagedType.LPStr)]
	internal string mountpoint;
	internal int show_version;
	internal int show_help;
	internal int clone_fd;
	internal uint max_idle_threads; /* discouraged, due to thread
	                                * destruct overhead */

	/* Added in libfuse-3.12 */
	internal uint max_threads;
}

[Flags]
public enum FuseAttrMask
{
	None = 0,

	Mode = (1 << 0),
	Uid = (1 << 1),
	Gid = (1 << 2),
	Size = (1 << 3),
	AccessTime = (1 << 4),
	ModifiedTime = (1 << 5),
	AccessTimeNow = (1 << 7),
	ModifiedTimeNow = (1 << 8),
	Force = (1 << 9),
	ChangeTime = (1 << 10),
	KillSuid = (1 << 11),
	KillSgid = (1 << 12),
	File = (1 << 13),
	KillPriv = (1 << 14),
	Open = (1 << 15),
	TimesSet = (1 << 16),
	Touch = (1 << 17),
}

[Flags]
public enum FuseCaps : uint
{
	/**
	 * Indicates that the filesystem supports asynchronous read requests.
	 *
	 * If this capability is not requested/available, the kernel will
	 * ensure that there is at most one pending read request per
	 * file-handle at any time, and will attempt to order read requests by
	 * increasing offset.
	 *
	 * This feature is enabled by default when supported by the kernel.
	 */
	AsyncRead = (1 << 0),

	/**
	 * Indicates that the filesystem supports "remote" locking.
	 *
	 * This feature is enabled by default when supported by the kernel,
	 * and if getlk() and setlk() handlers are implemented.
	 */
	RemoteLocking = (1 << 1),

	/**
	 * Indicates that the filesystem supports the O_TRUNC open flag.  If
	 * disabled, and an application specifies O_TRUNC, fuse first calls
	 * truncate() and then open() with O_TRUNC filtered out.
	 *
	 * This feature is enabled by default when supported by the kernel.
	 */
	OpenTruncate = (1 << 3),

	/**
	 * Indicates that the filesystem supports lookups of "." and "..".
	 *
	 * This feature is disabled by default.
	 */
	LookupDotDot = (1 << 4),

	/**
	 * Indicates that the kernel should not apply the umask to the
	 * file mode on create operations.
	 *
	 * This feature is disabled by default.
	 */
	DontMask = (1 << 6),

	/**
	 * Indicates that libfuse should try to use splice() when writing to
	 * the fuse device. This may improve performance.
	 *
	 * This feature is disabled by default.
	 */
	SpliceWrite = (1 << 7),

	/**
	 * Indicates that libfuse should try to move pages instead of copying when
	 * writing to / reading from the fuse device. This may improve performance.
	 *
	 * This feature is disabled by default.
	 */
	SpliceMove = (1 << 8),

	/**
	 * Indicates that libfuse should try to use splice() when reading from
	 * the fuse device. This may improve performance.
	 *
	 * This feature is enabled by default when supported by the kernel and
	 * if the filesystem implements a write_buf() handler.
	 */
	SpliceRead = (1 << 9),

	/**
	 * If set, the calls to flock(2) will be emulated using POSIX locks and must
	 * then be handled by the filesystem's setlock() handler.
	 *
	 * If not set, flock(2) calls will be handled by the FUSE kernel module
	 * internally (so any access that does not go through the kernel cannot be taken
	 * into account).
	 *
	 * This feature is enabled by default when supported by the kernel and
	 * if the filesystem implements a flock() handler.
	 */
	EmulateFlocks = (1 << 10),

	/**
	 * Indicates that the filesystem supports ioctl's on directories.
	 *
	 * This feature is enabled by default when supported by the kernel.
	 */
	IoctlDir = (1 << 11),

	/**
	 * Traditionally, while a file is open the FUSE kernel module only
	 * asks the filesystem for an update of the file's attributes when a
	 * client attempts to read beyond EOF. This is unsuitable for
	 * e.g. network filesystems, where the file contents may change
	 * without the kernel knowing about it.
	 *
	 * If this flag is set, FUSE will check the validity of the attributes
	 * on every read. If the attributes are no longer valid (i.e., if the
	 * *attr_timeout* passed to fuse_reply_attr() or set in `struct
	 * fuse_entry_param` has passed), it will first issue a `getattr`
	 * request. If the new mtime differs from the previous value, any
	 * cached file *contents* will be invalidated as well.
	 *
	 * This flag should always be set when available. If all file changes
	 * go through the kernel, *attr_timeout* should be set to a very large
	 * number to avoid unnecessary getattr() calls.
	 *
	 * This feature is enabled by default when supported by the kernel.
	 */
	AutoInvalidateData = (1 << 12),

	/**
	 * Indicates that the filesystem supports readdirplus.
	 *
	 * This feature is enabled by default when supported by the kernel and if the
	 * filesystem implements a readdirplus() handler.
	 */
	ReadDirPlus = (1 << 13),

	/**
	 * Indicates that the filesystem supports adaptive readdirplus.
	 *
	 * If FUSE_CAP_READDIRPLUS is not set, this flag has no effect.
	 *
	 * If FUSE_CAP_READDIRPLUS is set and this flag is not set, the kernel
	 * will always issue readdirplus() requests to retrieve directory
	 * contents.
	 *
	 * If FUSE_CAP_READDIRPLUS is set and this flag is set, the kernel
	 * will issue both readdir() and readdirplus() requests, depending on
	 * how much information is expected to be required.
	 *
	 * As of Linux 4.20, the algorithm is as follows: when userspace
	 * starts to read directory entries, issue a READDIRPLUS request to
	 * the filesystem. If any entry attributes have been looked up by the
	 * time userspace requests the next batch of entries continue with
	 * READDIRPLUS, otherwise switch to plain READDIR.  This will reasult
	 * in eg plain "ls" triggering READDIRPLUS first then READDIR after
	 * that because it doesn't do lookups.  "ls -l" should result in all
	 * READDIRPLUS, except if dentries are already cached.
	 *
	 * This feature is enabled by default when supported by the kernel and
	 * if the filesystem implements both a readdirplus() and a readdir()
	 * handler.
	 */
	ReadDirPlusAuto = (1 << 14),

	/**
	 * Indicates that the filesystem supports asynchronous direct I/O submission.
	 *
	 * If this capability is not requested/available, the kernel will ensure that
	 * there is at most one pending read and one pending write request per direct
	 * I/O file-handle at any time.
	 *
	 * This feature is enabled by default when supported by the kernel.
	 */
	AsyncDirectIo = (1 << 15),

	/**
	 * Indicates that writeback caching should be enabled. This means that
	 * individual write request may be buffered and merged in the kernel
	 * before they are send to the filesystem.
	 *
	 * This feature is disabled by default.
	 */
	WritebackCache = (1 << 16),

	/**
	 * Indicates support for zero-message opens. If this flag is set in
	 * the `capable` field of the `fuse_conn_info` structure, then the
	 * filesystem may return `ENOSYS` from the open() handler to indicate
	 * success. Further attempts to open files will be handled in the
	 * kernel. (If this flag is not set, returning ENOSYS will be treated
	 * as an error and signaled to the caller).
	 *
	 * Setting (or unsetting) this flag in the `want` field has *no
	 * effect*.
	 */
	OpenNosys = (1 << 17),

	/**
	 * Indicates support for parallel directory operations. If this flag
	 * is unset, the FUSE kernel module will ensure that lookup() and
	 * readdir() requests are never issued concurrently for the same
	 * directory.
	 *
	 * This feature is enabled by default when supported by the kernel.
	 */
	ParallelDirOps = (1 << 18),

	/**
	 * Indicates support for POSIX ACLs.
	 *
	 * If this feature is enabled, the kernel will cache and have
	 * responsibility for enforcing ACLs. ACL will be stored as xattrs and
	 * passed to userspace, which is responsible for updating the ACLs in
	 * the filesystem, keeping the file mode in sync with the ACL, and
	 * ensuring inheritance of default ACLs when new filesystem nodes are
	 * created. Note that this requires that the file system is able to
	 * parse and interpret the xattr representation of ACLs.
	 *
	 * Enabling this feature implicitly turns on the
	 * ``default_permissions`` mount option (even if it was not passed to
	 * mount(2)).
	 *
	 * This feature is disabled by default.
	 */
	PosixAcl = (1 << 19),

	/**
	 * Indicates that the filesystem is responsible for unsetting
	 * setuid and setgid bits when a file is written, truncated, or
	 * its owner is changed.
	 *
	 * This feature is enabled by default when supported by the kernel.
	 */
	HandleKillPriv = (1 << 20),

	/**
	 * Indicates that the kernel supports caching symlinks in its page cache.
	 *
	 * When this feature is enabled, symlink targets are saved in the page cache.
	 * You can invalidate a cached link by calling:
	 * `fuse_lowlevel_notify_inval_inode(se, ino, 0, 0);`
	 *
	 * This feature is disabled by default.
	 * If the kernel supports it (>= 4.20), you can enable this feature by
	 * setting this flag in the `want` field of the `fuse_conn_info` structure.
	 */
	CacheSymlinks = (1 << 23),

	/**
	 * Indicates support for zero-message opendirs. If this flag is set in
	 * the `capable` field of the `fuse_conn_info` structure, then the filesystem
	 * may return `ENOSYS` from the opendir() handler to indicate success. Further
	 * opendir and releasedir messages will be handled in the kernel. (If this
	 * flag is not set, returning ENOSYS will be treated as an error and signalled
	 * to the caller.)
	 *
	 * Setting (or unsetting) this flag in the `want` field has *no effect*.
	 */
	OpenDirNosys = (1 << 24),

	/**
	 * Indicates support for invalidating cached pages only on explicit request.
	 *
	 * If this flag is set in the `capable` field of the `fuse_conn_info` structure,
	 * then the FUSE kernel module supports invalidating cached pages only on
	 * explicit request by the filesystem through fuse_lowlevel_notify_inval_inode()
	 * or fuse_invalidate_path().
	 *
	 * By setting this flag in the `want` field of the `fuse_conn_info` structure,
	 * the filesystem is responsible for invalidating cached pages through explicit
	 * requests to the kernel.
	 *
	 * Note that setting this flag does not prevent the cached pages from being
	 * flushed by OS itself and/or through user actions.
	 *
	 * Note that if both FUSE_CAP_EXPLICIT_INVAL_DATA and FUSE_CAP_AUTO_INVAL_DATA
	 * are set in the `capable` field of the `fuse_conn_info` structure then
	 * FUSE_CAP_AUTO_INVAL_DATA takes precedence.
	 *
	 * This feature is disabled by default.
	 */
	ExplicitInvalidateData = (1 << 25),

	/**
	 * Indicates support that dentries can be expired or invalidated.
	 * 
	 * Expiring dentries, instead of invalidating them, makes a difference for 
	 * overmounted dentries, where plain invalidation would detach all submounts 
	 * before dropping the dentry from the cache. If only expiry is set on the 
	 * dentry, then any overmounts are left alone and until ->d_revalidate() 
	 * is called.
	 * 
	 * Note: ->d_revalidate() is not called for the case of following a submount,
	 * so invalidation will only be triggered for the non-overmounted case. 
	 * The dentry could also be mounted in a different mount instance, in which case
	 * any submounts will still be detached.
*/
	ExpireOnly = (1 << 26),
}

struct fuse_conn_info
{
	internal readonly uint proto_major;
	internal readonly uint proto_minor;
	internal uint max_write;
	internal uint max_read;
	internal uint max_readahead;
	internal readonly FuseCaps capable;
	internal FuseCaps want;
	internal uint max_background;
	internal uint congestion_threshold;
	internal uint time_gran;
	internal unsafe fixed uint reserved[22];
}

struct fuse_req_t
{
	private IntPtr ptr;
	public override string ToString() => $"0x{this.ptr.ToInt64():X}";

	public static implicit operator ulong(fuse_req_t req) => (ulong)req.ptr.ToInt64();
}

struct fuse_ino_t : IEquatable<fuse_ino_t>
{
	internal fuse_ino_t(ulong value)
	{
		this.value = value;
	}

	internal ulong value;

	public static implicit operator fuse_ino_t(ulong value) => new fuse_ino_t(value);
	public override bool Equals(object? obj)
	{
		return obj is fuse_ino_t t && Equals(t);
	}

	public bool Equals(fuse_ino_t other)
	{
		return value == other.value;
	}

	public override int GetHashCode()
	{
		return System.HashCode.Combine(value);
	}

	public static implicit operator ulong(fuse_ino_t ino) => ino.value;

	public static bool operator ==(fuse_ino_t left, fuse_ino_t right)
	{
		return left.Equals(right);
	}

	public static bool operator !=(fuse_ino_t left, fuse_ino_t right)
	{
		return !(left == right);
	}
}

[Flags]
enum FuseFileInfoOptions
{
	None = 0,
	WritePage = (1 << 0),
	DirectIo = (1 << 1),
	KeepCache = (1 << 2),
	Flush = (1 << 3),
	NonSeekable = (1 << 4),
	FlockRelease = (1 << 5),
	CacheReadDir = (1 << 6),
	NoFlush = (1 << 7),
}

[Flags]
public enum FuseOpenFlags
{
	ReadOnly = 0,
	WriteOnly = 1,
	ReadWrite = 2,

	AccessMask = 3,

	// Creates if it doesn't exist
	Create = 0x40,

	// When used with Create, fails if the file already exists
	Exclusive = 0x80,
	NoCtty = 0x100,

	// Truncates if exist, what if it doesn't?
	Truncate = 0x200,

	// TODO: Does this create or open only?
	Append = 0x400,

	NonBlock = 0x800,
	DSync = 0x1000,
	Direct = 0x4000,
	LargeFile = 0x8000,
	Directory = 0x1_0000,
	NoFollow = 0x2_0000,
	NoATime = 0x4_0000,
	CloseOnExec = 0x8_0000,

	Path = 0x20_0000,
}

/**
 * Information about an open file.
 *
 * File Handles are created by the open, opendir, and create methods and closed
 * by the release and releasedir methods.  Multiple file handles may be
 * concurrently open for the same file.  Generally, a client will create one
 * file handle per file descriptor, though in some cases multiple file
 * descriptors can share a single file handle.
 */
struct fuse_file_info
{
	internal static unsafe int StructSize => sizeof(fuse_file_info);

	/** Open flags.	 Available in open() and release() */
	internal FuseOpenFlags flags;

	internal FuseFileInfoOptions options;

	private uint padding2;

	/** File handle id.  May be filled in by filesystem in create,
	 * open, and opendir().  Available in most other file operations on the
	 * same file handle. */
	internal uint64_t fh;

	/** Lock owner id.  Available in locking operations and flush */
	internal uint64_t lock_owner;

	/** Requested poll events.  Available in ->poll.  Only set on kernels
	    which support it.  If unsupported, this field is set to zero. */
	internal uint32_t poll_events;
}

struct flock { }

struct fuse_pollhandle { }

enum fuse_buf_flags
{
	None = 0,

	IsFileDescriptor = (1 << 1),
	FileSeek = (1 << 2),
	FileRetry = (1 << 3)
}

struct fuse_buf
{
	internal size_t size;
	internal fuse_buf_flags flags;
	internal IntPtr mem;
	internal int fd;
	internal off_t pos;
}
struct fuse_bufvec
{
	internal size_t count;
	internal size_t idx;
	internal size_t off;
	internal fuse_buf buf;
}
struct fuse_forget_data { }

//struct mode_t
//{
//	public mode_t(int value) { this.value = value; }
//	public mode_t(PosixFileMode value) { this.value = (int)value; }

//	internal int value;
//}

struct dev_t
{
	internal ulong value;
}

struct size_t
{
	internal size_t(int value)
	{
		this.value = (uint)value;
	}

	internal UIntPtr value;

	public static implicit operator size_t(int value) => new size_t(value);
	public static implicit operator ulong(size_t size) => size.value;
}

struct off_t
{
	internal off_t(long value)
	{
		this.value = new IntPtr(value);
	}

	internal IntPtr value;

	public static implicit operator off_t(long value) => new off_t(value);
	public static implicit operator long(off_t size) => size.value;
}

struct timespec
{
	public timespec(long sec, long nsec)
	{
		this.sec = sec;
		this.nsec = nsec;
	}
	public long sec;
	public long nsec;

	public DateTime ToDateTime()
	{
		var ticks = this.sec * 10_000_000 + (this.nsec / 100);
		return DateTime.UnixEpoch + TimeSpan.FromTicks(ticks);
	}
}

struct stat
{
	internal unsafe static int StructSize => sizeof(stat);

	internal dev_t st_dev;  /* Device.  */
	internal UIntPtr st_ino;  /* File serial number.  */
	internal UIntPtr st_nlink;  /* Link count.  */
	internal mode_t st_mode;   /* File mode.  */
	internal uint st_uid;        /* User ID of the file's owner.  */
	internal uint st_gid;        /* Group ID of the file's group. */
	private int __pad0;
	internal dev_t st_rdev; /* Device number, if device.  */
	internal IntPtr st_size;  /* Size of file, in bytes.  */
	internal long st_blksize; /* Optimal block size for I/O.  */
	internal long st_blocks;    /* Number 512-byte blocks allocated. */

	internal timespec st_atime;   /* Time of last access.  */
	internal timespec st_mtime;   /* Time of last modification.  */
	internal timespec st_ctime;   /* Time of last status change.  */

	private unsafe fixed long __glibc_reserved[3];
};

delegate void FuseInitFunc(IntPtr /* void* */ userdata, ref fuse_conn_info conn);
delegate void FuseDestroyFunc(IntPtr userdata);
delegate void FuseLookupFunc(fuse_req_t req, fuse_ino_t parent, [MarshalAs(UnmanagedType.LPStr)] string name);
delegate void FuseForgetFunc(fuse_req_t req, fuse_ino_t ino, uint64_t nlookup);
delegate void FuseGetAttrFunc(fuse_req_t req, fuse_ino_t ino, IntPtr fi);
//delegate void FuseGetAttrFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi);
delegate void FuseSetAttrFunc(fuse_req_t req, fuse_ino_t ino, ref stat attr, FuseAttrMask to_set, ref fuse_file_info fi);
delegate void FuseReadLinkFunc(fuse_req_t req, fuse_ino_t ino);
delegate void FuseMakeNodeFunc(fuse_req_t req, fuse_ino_t parent, [MarshalAs(UnmanagedType.LPStr)] string name, mode_t mode, dev_t rdev);
delegate void FuseMkdirFunc(fuse_req_t req, fuse_ino_t parent, [MarshalAs(UnmanagedType.LPStr)] string name, mode_t mode);
delegate void FuseUnlinkFunc(fuse_req_t req, fuse_ino_t parent, [MarshalAs(UnmanagedType.LPStr)] string name);
delegate void FuseRmdirFunc(fuse_req_t req, fuse_ino_t parent, [MarshalAs(UnmanagedType.LPStr)] string name);
delegate void FuseSymlinkFunc(fuse_req_t req, [MarshalAs(UnmanagedType.LPStr)] string link, fuse_ino_t parent, [MarshalAs(UnmanagedType.LPStr)] string name);
delegate void FuseRenameFunc(fuse_req_t req, fuse_ino_t parent, [MarshalAs(UnmanagedType.LPStr)] string name, fuse_ino_t newparent, [MarshalAs(UnmanagedType.LPStr)] string newname, uint flags);
delegate void FuseLinkFunc(fuse_req_t req, fuse_ino_t ino, fuse_ino_t newparent,
		  [MarshalAs(UnmanagedType.LPStr)] string newname);
delegate void FuseOpenFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi);
delegate void FuseReadFunc(fuse_req_t req, fuse_ino_t ino, size_t size, off_t off, ref fuse_file_info fi);
delegate void FuseWriteFunc(fuse_req_t req, fuse_ino_t ino, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] buf, nint size, off_t off, ref fuse_file_info fi);
delegate void FuseFlushFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi);
delegate void FuseReleaseFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi);
delegate void FuseFsyncFunc(fuse_req_t req, fuse_ino_t ino, int datasync, ref fuse_file_info fi);
delegate void FuseOpenDirFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi);
delegate void FuseReadDirFunc(fuse_req_t req, fuse_ino_t ino, size_t size, off_t off, ref fuse_file_info fi);
delegate void FuseReleaseDirFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi);
delegate void FuseFsyncDirFunc(fuse_req_t req, fuse_ino_t ino, int datasync, ref fuse_file_info fi);
delegate void FuseStatfsFunc(fuse_req_t req, fuse_ino_t ino);
delegate void FuseSetXAttrFunc(fuse_req_t req, fuse_ino_t ino, [MarshalAs(UnmanagedType.LPStr)] string name, [MarshalAs(UnmanagedType.LPStr)] string value, size_t size, int flags);
delegate void FuseGetXAttrFunc(fuse_req_t req, fuse_ino_t ino, [MarshalAs(UnmanagedType.LPStr)] string name, size_t size);
delegate void FuseListXAttrFunc(fuse_req_t req, fuse_ino_t ino, size_t size);
delegate void FuseRemoveXAttrFunc(fuse_req_t req, fuse_ino_t ino, [MarshalAs(UnmanagedType.LPStr)] string name);
delegate void FuseAccessFunc(fuse_req_t req, fuse_ino_t ino, PosixFileMode mask);
delegate void FuseCreateFunc(fuse_req_t req, fuse_ino_t parent, [MarshalAs(UnmanagedType.LPStr)] string name, mode_t mode, ref fuse_file_info fi);
delegate void FuseGetLockFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi, ref flock lock_);
delegate void FuseSetLockFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi, ref flock lock_, int sleep);
delegate void FuseBmapFunc(fuse_req_t req, fuse_ino_t ino, size_t blocksize, uint64_t idx);

//#if FUSE_USE_VERSION < 35
//	delegate void (*ioctl) (fuse_req_t req, fuse_ino_t ino, int cmd,
//		       void *arg, ref fuse_file_info fi, unsigned flags,
//		       const void *in_buf, size_t in_bufsz, size_t out_bufsz);
//#else
delegate void FuseIoctlFunc(fuse_req_t req, fuse_ino_t ino, uint cmd, IntPtr arg, ref fuse_file_info fi, uint flags, IntPtr /* const void* */ in_buf, size_t in_bufsz, size_t out_bufsz);
//#endif

delegate void FusePollFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi, ref fuse_pollhandle ph);
delegate void FuseWriteBufFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_bufvec bufv, off_t off, ref fuse_file_info fi);
delegate void FuseRetrieveReplyFunc(fuse_req_t req, IntPtr cookie, fuse_ino_t ino, off_t offset, ref fuse_bufvec bufv);
delegate void FuseForgetMultiFunc(fuse_req_t req, size_t count, ref fuse_forget_data forgets);
delegate void FuseFlockFunc(fuse_req_t req, fuse_ino_t ino, ref fuse_file_info fi, int op);
delegate void FuseFAllocateFunc(fuse_req_t req, fuse_ino_t ino, int mode, off_t offset, off_t length, ref fuse_file_info fi);
delegate void FuseReadDirPlusFunc(fuse_req_t req, fuse_ino_t ino, size_t size, off_t off, ref fuse_file_info fi);
delegate void FuseCopyFileRangeFunc(fuse_req_t req, fuse_ino_t ino_in, off_t off_in, ref fuse_file_info fi_in, fuse_ino_t ino_out, off_t off_out, ref fuse_file_info fi_out, size_t len, int flags);
delegate void FuseLSeekFunc(fuse_req_t req, fuse_ino_t ino, off_t off, int whence, ref fuse_file_info fi);

struct fuse_lowlevel_ops
{
	internal static unsafe int StructSize => sizeof(fuse_lowlevel_ops);

	internal FuseInitFunc init;
	internal FuseDestroyFunc destroy;
	internal FuseLookupFunc lookup;
	internal FuseForgetFunc forget;
	internal FuseGetAttrFunc getattr;
	internal FuseSetAttrFunc setattr;
	internal FuseReadLinkFunc readlink;
	internal FuseMakeNodeFunc mknod;
	internal FuseMkdirFunc mkdir;
	internal FuseUnlinkFunc unlink;
	internal FuseRmdirFunc rmdir;
	internal FuseSymlinkFunc symlink;
	internal FuseRenameFunc rename;
	internal FuseLinkFunc link;
	internal FuseOpenFunc open;
	internal FuseReadFunc read;
	internal FuseWriteFunc write;
	internal FuseFlushFunc flush;
	internal FuseReleaseFunc release;
	internal FuseFsyncFunc fsync;
	internal FuseOpenDirFunc opendir;
	internal FuseReadDirFunc readdir;
	internal FuseReleaseDirFunc releasedir;
	internal FuseFsyncDirFunc fsyncdir;
	internal FuseStatfsFunc statfs;
	internal FuseSetXAttrFunc setxattr;
	internal FuseGetXAttrFunc getxattr;
	internal FuseListXAttrFunc listxattr;
	internal FuseRemoveXAttrFunc removexattr;
	internal FuseAccessFunc access;
	internal FuseCreateFunc create;
	internal FuseGetLockFunc getlk;
	internal FuseSetLockFunc setlk;
	internal FuseBmapFunc bmap;

	internal FuseIoctlFunc ioctl;

	internal FusePollFunc poll;
	internal FuseWriteBufFunc write_buf;
	internal FuseRetrieveReplyFunc retrieve_reply;
	internal FuseForgetMultiFunc forget_multi;
	internal FuseFlockFunc flock;
	internal FuseFAllocateFunc fallocate;
	internal FuseReadDirPlusFunc readdirplus;
	internal FuseCopyFileRangeFunc copy_file_range;
	internal FuseLSeekFunc lseek;
}

struct fuse_entry_param
{
	/** Unique inode number
	 *
	 * In lookup, zero means negative entry (from version 2.5)
	 * Returning ENOENT also means negative entry, but by setting zero
	 * ino the kernel may cache negative entries for entry_timeout
	 * seconds.
	 */
	internal fuse_ino_t ino;

	/** Generation number for this entry.
	 *
	 * If the file system will be exported over NFS, the
	 * ino/generation pairs need to be unique over the file
	 * system's lifetime (rather than just the mount time). So if
	 * the file system reuses an inode after it has been deleted,
	 * it must assign a new, previously unused generation number
	 * to the inode at the same time.
	 *
	 */
	internal uint64_t generation;

	/** Inode attributes.
	 *
	 * Even if attr_timeout == 0, attr must be correct. For example,
	 * for open(), FUSE uses attr.st_size from lookup() to determine
	 * how many bytes to request. If this value is not correct,
	 * incorrect data will be returned.
	 */
	internal stat attr;

	/** Validity timeout (in seconds) for inode attributes. If
	    attributes only change as a result of requests that come
	    through the kernel, this should be set to a very large
	    value. */
	internal double attr_timeout;

	/** Validity timeout (in seconds) for the name. If directory
	    entries are changed/deleted only as a result of requests
	    that come through the kernel, this should be set to a very
	    large value. */
	internal double entry_timeout;
};
