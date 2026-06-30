using System.Buffers.Binary;
using System.Text;
using Titanis.Linterop.Fuse;
using Titanis.Winterop.Security;

namespace Titanis.Smb2.Fusion;

/// <summary>
/// Base class for files and directories on an SMB share.
/// </summary>
abstract class SmbFileNodeBase : IFuseNode
{
	internal SmbFileNodeBase(SmbMountInfo mountInfo, UncPath path)
	{
		this._mountInfo = mountInfo;
		this.SharedPath = path;

		this._daclCache = new CacheElement<SecurityDescriptor?>(this.GetDacl);
		this._saclCache = new CacheElement<SecurityDescriptor?>(this.GetSacl);
		this._ownerCache = new CacheElement<SecurityDescriptor?>(this.GetOwner);
	}

	protected readonly SmbMountInfo _mountInfo;
	protected Smb2Client Client => this._mountInfo.smbClient;

	/// <summary>
	/// Gets the UNC path of the node.
	/// </summary>
	public UncPath SharedPath { get; }

	/// <inheritdoc/>
	public abstract string Name { get; }
	/// <inheritdoc/>
	public abstract PosixFileMode Mode { get; }

	/// <inheritdoc/>
	public abstract LinuxFileType FileType { get; }

	public uint Uid => this._mountInfo.uid;

	public uint Gid => this._mountInfo.gid;

	public virtual long FileSize { get => 0x1000; set => throw new NotSupportedException(); }

	public long BlockSize => 0x1000;

	public long BlockCount => 8;

	public virtual DateTime? LastAccessTime { get => null; set => throw new NotSupportedException(); }

	public virtual DateTime? LastWriteTime { get => null; set => throw new NotSupportedException(); }

	public virtual DateTime? LastChangeTime { get => null; set => throw new NotSupportedException(); }

	public virtual FileAttributes NtfsAttributes => 0;

	/// <inheritdoc/>
	public virtual Task<IFuseOpenDirectory> OpenDirectory(CancellationToken cancellationToken) { throw new NotSupportedException(); }
	/// <inheritdoc/>
	public virtual Task<IFuseOpenFile> OpenFile(FuseOpenFlags openFlags, CancellationToken cancellationToken) { throw new NotSupportedException(); }

	private CacheElement<SecurityDescriptor?> _daclCache;
	private CacheElement<SecurityDescriptor?> _saclCache;
	private CacheElement<SecurityDescriptor?> _ownerCache;

	private static readonly string[] XAttrNames = [
		Smb2XAttrNames.NtfsAttribName,
		Smb2XAttrNames.DaclText,
		Smb2XAttrNames.OwnerSid,
		];
	public string[]? GetXAttributeNames() => XAttrNames;
	public async ValueTask<XAttrData> GetXAttribute(string name, int bufferSize, CancellationToken cancellationToken)
	{
		switch (name)
		{
			case Smb2XAttrNames.NtfsAttribName:
				if (bufferSize == 0)
					return new XAttrData(4);
				else
				{
					byte[] buf = new byte[4];
					BinaryPrimitives.WriteInt32BigEndian(buf, (int)this.NtfsAttributes);
				}
				break;
			case Smb2XAttrNames.DaclText:
			case Smb2XAttrNames.SaclText:
			case Smb2XAttrNames.OwnerSid:
				{
					var sd = await (name switch
					{
						Smb2XAttrNames.DaclText => this._daclCache.GetValue(TimeSpan.FromSeconds(5), cancellationToken),
						Smb2XAttrNames.SaclText => this._saclCache.GetValue(TimeSpan.FromSeconds(5), cancellationToken),
						Smb2XAttrNames.OwnerSid => this._ownerCache.GetValue(TimeSpan.FromSeconds(5), cancellationToken),
					}).ConfigureAwait(false);
					if (sd != null)
					{
						if (name is Smb2XAttrNames.OwnerSid)
						{
							return new XAttrData(sd?.Owner?.ToString());
						}
						else
							return new XAttrData(sd?.ToString());
					}
				}
				break;
		}

		return XAttrData.NotPresent;
	}


	private Task<SecurityDescriptor?> GetDacl(CancellationToken cancellationToken) => this.GetSecurity(SecurityInfo.Dacl, Smb2FileAccessRights.ReadControl | Smb2FileAccessRights.ReadAttributes | Smb2FileAccessRights.Synchronize, cancellationToken);
	private Task<SecurityDescriptor?> GetOwner(CancellationToken cancellationToken) => this.GetSecurity(SecurityInfo.Owner, Smb2FileAccessRights.ReadControl | Smb2FileAccessRights.ReadAttributes | Smb2FileAccessRights.Synchronize, cancellationToken);
	private Task<SecurityDescriptor?> GetSacl(CancellationToken cancellationToken) => this.GetSecurity(SecurityInfo.Sacl, Smb2FileAccessRights.AccessSystemSecurity | Smb2FileAccessRights.ReadAttributes | Smb2FileAccessRights.Synchronize, cancellationToken);
	private async Task<SecurityDescriptor?> GetSecurity(SecurityInfo info, Smb2FileAccessRights access, CancellationToken cancellationToken)
	{
		var file = await this.Client.CreateFileAsync(SharedPath, Smb2CreateInfo.ForCreateFile(
			desiredAccess: access,
			createDisposition: Smb2CreateDisposition.OpenExisting,
			extraOptions: this._mountInfo.extraCreateOptions
			), FileAccess.Read, cancellationToken).ConfigureAwait(false);
		await using (file)
		{
			var sd = await file.GetSecurityAsync(info, 2048, cancellationToken).ConfigureAwait(false);
			return sd;
		}
	}

	/// <inheritdoc/>
	public abstract Task<IFuseNode?> Lookup(string name, CancellationToken cancellationToken);

	public virtual Task<IFuseOpenFile> CreateFile(string name, FuseOpenFlags flags, CancellationToken cancellationToken)
	{
		throw new NotSupportedException();
	}

	public virtual Task<IFuseNode> CreateDirectory(string name, CancellationToken cancellationToken)
	{
		throw new NotSupportedException();
	}

	public virtual Task DeleteDirectory(string name, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public virtual Task DeleteFile(string name, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
