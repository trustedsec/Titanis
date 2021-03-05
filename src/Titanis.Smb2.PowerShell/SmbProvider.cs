using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Management.Automation.Remoting;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanis.Net;
using Titanis.Security;
using Titanis.Security.Kerberos;
using Titanis.Security.Ntlm;
using Titanis.Security.Spnego;

namespace Titanis.Smb2.PowerShell
{
	public static class SmbItemClasses
	{
		public const string File = "File";
		public const string Directory = "Directory";
		public const string Symlink = "Symlink";
		public const string SymlinkDir = "SymlinkDir";
		public const string MountPoint = "MountPoint";
		public const string Junction = "Junction";
	}

	/// <summary>
	/// Implements a <see cref="NavigationCmdletProvider"/> for the SMB namespace.
	/// </summary>
	[CmdletProvider(ProviderName, ProviderCapabilities.None)]
	public partial class SmbProvider : NavigationCmdletProvider
	{
		public const string ProviderName = "Titanis.Smb2";

		public SmbProvider()
		{
		}

		protected override ProviderInfo Start(ProviderInfo providerInfo)
		{
			return new SmbProviderInfo(base.Start(providerInfo), this);
		}

		internal SmbProviderInfo smb => ((SmbProviderInfo)this.ProviderInfo);
		public Smb2Client SmbClient => this.smb.SmbClient;

		private void BeginOperation(Action<CancellationToken> func)
		{
			var prevSource = this._cancelSource;
			var cancelSource = prevSource ??= (this._cancelSource = new CancellationTokenSource());

			try
			{
				func(cancelSource.Token);
			}
			finally
			{
				this._cancelSource = prevSource;
			}
		}
		private TResult BeginOperation<TResult>(Func<CancellationToken, TResult> func)
		{
			var prevSource = this._cancelSource;
			var cancelSource = prevSource ??= (this._cancelSource = new CancellationTokenSource());

			try
			{
				return func(cancelSource.Token);
			}
			finally
			{
				this._cancelSource = prevSource;
			}
		}

		private CancellationTokenSource? _cancelSource;
		protected override void StopProcessing()
		{
			this._cancelSource?.Cancel();
		}
		protected override void Stop()
		{
			base.Stop();
		}

		protected override Collection<PSDriveInfo> InitializeDefaultDrives()
		{
			return new Collection<PSDriveInfo>() { new PSDriveInfo("smb", this.ProviderInfo, "smb://", "Titanis SMB drive", null) };
		}

		protected override bool IsValidPath(string path)
		{
			return UncPath.TryParse(path, out _);
		}

		#region Drives
		protected override object NewDriveDynamicParameters()
		{
			return new SmbConnectionParameters();
		}
		protected override PSDriveInfo NewDrive(PSDriveInfo drive)
		{
			if (drive.Root == "smb://")
				return new SmbRootDriveInfo(drive);

			return this.BeginOperation(cancellationToken =>
			{
				var uncPath = UncPath.Parse(drive.Root);

				var baseParms = this.smb.GetConnectParametersFor(uncPath.ServerName, true);
				var parms = (SmbConnectionParameters)this.DynamicParameters;
				parms = parms.MergeOnto(baseParms);
				this.smb.SetConnectParameters(uncPath.ServerName, parms);

				if (string.IsNullOrEmpty(uncPath.ShareName))
					throw new ArgumentException("The UNC path must include a share name");

				var client = this.SmbClient;
				var share = client.GetShare(uncPath, cancellationToken).Result;
				try
				{
					var conn_ = share;
					share = null;

					return new SmbShareDriveInfo(drive, this, uncPath, share);
				}
				finally
				{
					share?.Dispose();
				}
			});
		}
		#endregion

		protected override object NewItemDynamicParameters(string path, string itemTypeName, object newItemValue)
		{
			return GetNewItemParamsCore(itemTypeName);
		}

		private static SmbNewItemParams? GetNewItemParamsCore(string itemTypeName)
		{
			if (itemTypeName != null)
			{
				if (itemTypeName.Equals(SmbItemClasses.Directory, StringComparison.OrdinalIgnoreCase))
					return new SmbNewDirectoryItemParams();
				else if (itemTypeName.Equals(SmbItemClasses.MountPoint, StringComparison.OrdinalIgnoreCase))
					return new SmbMountPointItemParams();
				else if (itemTypeName.Equals(SmbItemClasses.Junction, StringComparison.OrdinalIgnoreCase))
					return new SmbMountPointItemParams();
				else if (itemTypeName.Equals(SmbItemClasses.SymlinkDir, StringComparison.OrdinalIgnoreCase))
					return new SmbSymlinkDirItemParams();
				else if (itemTypeName.Equals(SmbItemClasses.Symlink, StringComparison.OrdinalIgnoreCase))
					return new SmbSymlinkItemParams();
			}
			return null;
		}

		protected override void NewItem(string path, string itemTypeName, object newItemValue)
		{
			UncPath uncPath = UncPath.Parse(path);

			this.BeginOperation(cancellationToken =>
			{
				var newItemParams = (SmbNewItemParams)this.DynamicParameters;
				return newItemParams.Create(this.SmbClient, uncPath, cancellationToken);
			});
		}

		protected override bool IsItemContainer(string path)
		{
			if (!UncPath.TryParse(path, out var uncPath))
				return false;

			if (string.IsNullOrEmpty(uncPath.ShareRelativePath))
				return true;

			return this.BeginOperation(cancellationToken =>
			{
				using (var file = this.smb.SmbClient.CreateFileAsync(uncPath, new Smb2CreateInfo
				{
					CreateDisposition = Smb2CreateDisposition.Open,
					DesiredAccess = (uint)Smb2FileAccessRights.DefaultOpenReadAccess,
					ShareAccess = Smb2ShareAccess.Read,
					ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
					CreateOptions = Smb2FileCreateOptions.SynchronousIoNonalert,
					FileAttributes = WinFileInfo.FileAttributes.Normal
				}, FileAccess.Read, cancellationToken).Result)
				{
					return file.IsDirectory;
				}
			});
		}

		protected override void GetItem(string path)
		{
			base.GetItem(path);
		}

		protected override void GetChildItems(string path, bool recurse)
		{
			base.GetChildItems(path, recurse);
		}

		protected override bool ConvertPath(string path, string filter, ref string updatedPath, ref string updatedFilter)
		{
			return base.ConvertPath(path, filter, ref updatedPath, ref updatedFilter);
		}

		protected override void GetChildItems(string path, bool recurse, uint depth)
		{
			UncPath uncPath = UncPath.Parse(path);

			this.BeginOperation(cancellationToken =>
			{
				using (var dir = this.smb.SmbClient.OpenDirectoryAsync(uncPath, cancellationToken).Result)
				{
					foreach (var entry in dir.QueryDirAsync("*", Smb2Directory.Smb2DirQueryOptions.QueryReparseInfo, cancellationToken).Result)
					{
						if (entry.FileName is "." or "..")
							continue;
						UncPath itemPath = uncPath.Append(entry.FileName);
						var smbItem = new SmbItem(itemPath, entry);
						this.WriteItemObject(smbItem, itemPath.ToString(), 0 != (entry.FileAttributes & WinFileInfo.FileAttributes.Directory));
					}
				}
			});
		}

		protected override string GetChildName(string path)
		{
			return base.GetChildName(path);
		}

		protected override void GetChildNames(string path, ReturnContainers returnContainers)
		{
			base.GetChildNames(path, returnContainers);
		}

		protected override bool ItemExists(string path)
		{
			CancellationToken token = CancellationToken.None;

			if (!UncPath.TryParse(path, out UncPath uncPath))
				return false;

			if (string.IsNullOrEmpty(uncPath.ShareRelativePath))
				return true;

			return this.BeginOperation(cancellationToken =>
			{
				try
				{
					using (this.SmbClient.CreateFileAsync(UncPath.Parse(path), new Smb2CreateInfo
					{
						CreateDisposition = Smb2CreateDisposition.Open,
						DesiredAccess = (uint)Smb2FileAccessRights.DefaultOpenReadAccess,
						ShareAccess = Smb2ShareAccess.Read,
						ImpersonationLevel = Smb2ImpersonationLevel.Impersonation,
						CreateOptions = Smb2FileCreateOptions.SynchronousIoNonalert | Smb2FileCreateOptions.OpenReparsePoint,
						FileAttributes = WinFileInfo.FileAttributes.Normal
					}, FileAccess.Read, token).Result)
					{
						return true;
					}
				}
				catch
				{
					return false;
				}
			});
		}
	}

	partial class SmbProvider : IContentCmdletProvider
	{
		public void ClearContent(string path)
		{
			throw new NotImplementedException();
		}

		public object ClearContentDynamicParameters(string path)
		{
			throw new NotImplementedException();
		}

		public IContentReader GetContentReader(string path)
		{
			throw new NotImplementedException();
		}

		public object GetContentReaderDynamicParameters(string path)
		{
			return new SmbGetContentParams();
		}

		public IContentWriter GetContentWriter(string path)
		{
			throw new NotImplementedException();
		}

		public object GetContentWriterDynamicParameters(string path)
		{
			return null;
		}
	}

	public partial class SmbProviderInfo : ProviderInfo
	{
		internal SmbProviderInfo(ProviderInfo providerInfo, SmbProvider provider) : base(providerInfo)
		{
			this.Provider = provider;

			var client = new Smb2Client(
				new PlatformSocketService(),
				this,
				this,
				this,
				null
				);
			this.SmbClient = client;
		}

		public SmbProvider Provider { get; }
		public Smb2Client SmbClient { get; private set; }

		#region Connection parameters
		private SmbConnectionParameters _defaultConnectParameters = SmbConnectionParameters.GetDefault();
		internal SmbConnectionParameters? DefaultConnectParameters
		{
			get => _defaultConnectParameters;
			set
			{
				if (value is null) throw new ArgumentNullException(nameof(value));
				_defaultConnectParameters = value;
			}
		}

		private Dictionary<string, SmbConnectionParameters> _connectParams = new Dictionary<string, SmbConnectionParameters>(StringComparer.OrdinalIgnoreCase);

		internal SmbConnectionParameters? GetConnectParametersFor(
			string serverName,
			bool defaultIfNone
			)
		{
			this._connectParams.TryGetValue(serverName, out var parms);
			if (defaultIfNone && parms == null)
				return DefaultConnectParameters;

			return parms;
		}
		internal void SetConnectParameters(
			string serverName,
			SmbConnectionParameters parameters
			)
		{
			if (string.IsNullOrEmpty(serverName)) throw new ArgumentException($"'{nameof(serverName)}' cannot be null or empty.", nameof(serverName));
			if (parameters is null) throw new ArgumentNullException(nameof(parameters));

			lock (this._connectParams)
			{
				this._connectParams[serverName] = parameters;
			}
		}
		#endregion
	}

	partial class SmbProviderInfo : IClientCredentialService
	{

		class KdcLocator : IKdcLocator
		{
			private readonly EndPoint _kdcEP;

			public KdcLocator(EndPoint kdcEP)
			{
				this._kdcEP = kdcEP;
			}
			public EndPoint FindKdc(string realm)
			{
				return this._kdcEP;
			}
		}

		AuthClientContext IClientCredentialService.GetAuthContextForResource(string resourceType, string resourceKey)
		{
			if (resourceType != ResourceTypes.Server)
				return null;

			var serverName = resourceKey;
			var parms = this.GetConnectParametersFor(serverName, true);

			// Create SPNEGO context required by SMB2
			var authContext = new SpnegoClientContext();

			string targetService = "cifs/" + serverName;

			if (!string.IsNullOrEmpty(parms.Kdc))
			{
				var port = parms.KdcPort.Value;
				if (IPAddress.TryParse(serverName, out var _))
					this.WriteWarning("The server name within the UNC path is an IP address.  This will probably result in Kerberos authentication failing.");

				EndPoint kdcEP;
				if (IPAddress.TryParse(parms.Kdc, out var kdcAddr))
					kdcEP = new IPEndPoint(kdcAddr, port);
				else
					kdcEP = new DnsEndPoint(parms.Kdc, port);

				KerberosClient krb = new KerberosClient(new KdcLocator(kdcEP));
				krb.Workstation = parms.Workstation;

				KerberosCredential cred;
				if (parms.Password != null)
					cred = new KerberosPasswordCredential(parms.UserName, parms.UserDomain, parms.Password);
				else if (parms.NtlmHash != null)
					cred = new KerberosHashCredential(parms.UserName, parms.UserDomain, EType.Rc4Hmac, parms.NtlmHash.Bytes);
				else
					throw new InvalidOperationException("KDC option specified, but no suitable credentials were provided.");

				var krbContext = new KerberosClientContext(cred, krb, "cifs", parms.HostName, parms.UserDomain);

				authContext.Contexts.Add(krbContext);
			}

			// Create NTLM context based on parameters
			NtlmCredential? ntlmCred;
			if (parms.Password != null)
			{
				ntlmCred = new NtlmPasswordCredential(parms.UserName, parms.UserDomain, parms.Password);
			}
			else if (parms.NtlmHash != null)
			{
				ntlmCred = new NtlmHashCredential(parms.UserName, parms.UserDomain, new Buffer128(), new Buffer128(parms.NtlmHash.Bytes));
			}
			else
				ntlmCred = null;

			if (ntlmCred != null)
			{
				var ntlmContext = new NtlmClientContext(ntlmCred)
				{
					Workstation = parms.Workstation,
					WorkstationDomain = parms.UserDomain,
					TargetName = targetService,
					ClientChannelBindingsUnhashed = new byte[16]
				};
				ntlmContext.ClientConfigFlags |= NegotiateFlags.D_NegotiateSign;
				// UNDONE: SMB doesn't use the provider's sealing capability
				//if (this.Encrypt.IsSet)
				//	ntlmContext.ClientConfigFlags |= NegotiateFlags.E_NegotiateSeal;

				authContext.Contexts.Add(ntlmContext);
			}

			return authContext;
		}

		private void WriteWarning(string v)
		{
			throw new NotImplementedException();
		}
	}

	partial class SmbProviderInfo : INameResolverService
	{
		public Task<IPAddress[]> ResolveAsync(string hostName, CancellationToken cancellationToken)
		{
			var parms = this.GetConnectParametersFor(hostName, false);

			if (parms != null)
				hostName = parms.HostName;

			return PlatformNameResolverService.ResolveAsync(hostName, this.DefaultConnectParameters.NameResolveOptions.Value, cancellationToken);
		}
	}
	partial class SmbProviderInfo : ISmbOptionsService
	{
		public Smb2ConnectionOptions? GetConnectionOptionsFor(string serverName)
		{
			var parms = this.GetConnectParametersFor(serverName, true);
			return parms.ToConnectionOptions();
		}
	}
}
