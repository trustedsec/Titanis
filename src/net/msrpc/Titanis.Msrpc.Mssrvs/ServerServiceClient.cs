using ms_srvs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Winterop;

namespace Titanis.Msrpc.Mswkst
{
	/// <summary>
	/// Specifies a share information level
	/// </summary>
	public enum ShareInfoLevel
	{
		/// <summary>
		/// Requests a <see cref="SHARE_INFO_1"/>.
		/// </summary>
		Level1 = 1,
		/// <summary>
		/// Requests a <see cref="SHARE_INFO_2"/>.
		/// </summary>
		Level2 = 2,
		/// <summary>
		/// Requests a <see cref="SHARE_INFO_501"/>.
		/// </summary>
		Level501 = 501,
		/// <summary>
		/// Requests a <see cref="SHARE_INFO_502_I"/>.
		/// </summary>
		Level502 = 502,
		/// <summary>
		/// Requests a <see cref="SHARE_INFO_503_I"/>.
		/// </summary>
		Level503 = 503,
	}

	/// <summary>
	/// Specifies an open file info level.
	/// </summary>
	public enum OpenFileInfoLevel
	{
		/// <summary>
		/// Requests a <see cref="FILE_INFO_2"/>.
		/// </summary>
		Level2 = 2,
		/// <summary>
		/// Requests a <see cref="FILE_INFO_3"/>.
		/// </summary>
		Level3 = 3,
	}

	/// <summary>
	/// Specifies a session info level.
	/// </summary>
	public enum SessionInfoLevel
	{
		/// <summary>
		/// Requests a <see cref="SESSION_INFO_0"/>.
		/// </summary>
		Level0 = 0,
		/// <summary>
		/// Requests a <see cref="SESSION_INFO_1"/>.
		/// </summary>
		Level1 = 1,
		/// <summary>
		/// Requests a <see cref="SESSION_INFO_2"/>.
		/// </summary>
		Level2 = 2,
		/// <summary>
		/// Requests a <see cref="SESSION_INFO_10"/>.
		/// </summary>
		Level10 = 10,
		/// <summary>
		/// Requests a <see cref="SESSION_INFO_502"/>.
		/// </summary>
		Level502 = 502,
	}

	public class ServerServiceClient : RpcServiceClient<srvsvcClientProxy>
	{

		public ServerServiceClient()
		{
		}

		public const string PipeName = "srvsvc";

		public async Task<IList<ConnectionInfo>> GetConnections(
			string shareOrComputerName,
			CancellationToken cancellationToken)
		{
			RpcPointer<uint> pEntries = new RpcPointer<uint>();
			RpcPointer<uint> pResumeHandle = new RpcPointer<uint>();

			List<ConnectionInfo> connections = new List<ConnectionInfo>();
			Win32ErrorCode res;
			do
			{
				var pInfo = new RpcPointer<CONNECT_ENUM_STRUCT>(new CONNECT_ENUM_STRUCT
				{
					Level = 1,
					ConnectInfo = new CONNECT_ENUM_UNION
					{
						Level = 1,
						Level1 = new RpcPointer<CONNECT_INFO_1_CONTAINER>()
					}
				});
				res = (Win32ErrorCode)await this._proxy.NetrConnectionEnum(
					LocalName,
					shareOrComputerName,
					pInfo,
					64 * 1024,
					pEntries,
					pResumeHandle,
					cancellationToken
					).ConfigureAwait(false);

				var entries = pInfo.value.ConnectInfo.Level1?.value.Buffer?.value;
				if (entries != null)
				{
					foreach (var entry in entries)
					{
						connections.Add(new ConnectionInfo(entry));
					}
				}
			} while (res == Win32ErrorCode.ERROR_MORE_DATA);
			res.CheckAndThrow();

			return connections;
		}

		public Task<IList<OpenFileInfo>> GetOpenFiles(
			string? serverName,
			string? basePath,
			string? userName,
			OpenFileInfoLevel level,
			CancellationToken cancellationToken
			)
			=> this.GetOpenFiles(serverName, basePath, userName, level, DefaultReturnBufferSize, cancellationToken);
		public async Task<IList<OpenFileInfo>> GetOpenFiles(
			string? serverName,
			string? basePath,
			string? userName,
			OpenFileInfoLevel level,
			int bufferSize,
			CancellationToken cancellationToken
			)
		{
			RpcPointer<uint> pEntries = new RpcPointer<uint>();
			RpcPointer<uint> pResumeHandle = new RpcPointer<uint>();

			List<OpenFileInfo> files = new List<OpenFileInfo>();
			Win32ErrorCode res;
			do
			{
				var pInfo = new RpcPointer<FILE_ENUM_STRUCT>(level switch
				{
					OpenFileInfoLevel.Level2 => new FILE_ENUM_STRUCT
					{
						Level = 2,
						FileInfo = new FILE_ENUM_UNION
						{
							Level = 2,
							Level2 = new RpcPointer<FILE_INFO_2_CONTAINER>()
						}
					},
					OpenFileInfoLevel.Level3 => new FILE_ENUM_STRUCT
					{
						Level = 3,
						FileInfo = new FILE_ENUM_UNION
						{
							Level = 3,
							Level3 = new RpcPointer<FILE_INFO_3_CONTAINER>()
						}
					},
					_ => throw new ArgumentException("The level requested is not valid."),
				});
				res = (Win32ErrorCode)await this._proxy.NetrFileEnum(
					serverName ?? LocalName,
					basePath,
					userName,
					pInfo,
					(uint)bufferSize,
					pEntries,
					pResumeHandle,
					cancellationToken
					).ConfigureAwait(false);

				switch (level)
				{
					case OpenFileInfoLevel.Level2:
						GetEntriesFrom(files, pInfo.value.FileInfo.Level2?.value.Buffer?.value, r => new OpenFileInfo(r));
						break;
					case OpenFileInfoLevel.Level3:
						GetEntriesFrom(files, pInfo.value.FileInfo.Level3?.value.Buffer?.value, r => new OpenFileInfo(r));
						break;
				}
			} while (res == Win32ErrorCode.ERROR_MORE_DATA);
			res.CheckAndThrow();

			return files;
		}

		public async Task<OpenFileInfo> GetOpenFileInfo(int fileId, CancellationToken cancellationToken)
		{
			RpcPointer<FILE_INFO> pInfo = new RpcPointer<FILE_INFO>();
			Win32ErrorCode res = (Win32ErrorCode)await this._proxy.NetrFileGetInfo(
				LocalName,
				(uint)fileId,
				3,
				pInfo,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new OpenFileInfo(pInfo.value.FileInfo3.value);
		}

		public async Task CloseFile(int fileId, CancellationToken cancellationToken)
		{
			Win32ErrorCode res = (Win32ErrorCode)await this._proxy.NetrFileClose(
				LocalName,
				(uint)fileId,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		public async Task<IList<SessionInfo>> GetSessions(
			string? serverName,
			string? clientName,
			string? userName,
			SessionInfoLevel level,
			int bufferSize,
			CancellationToken cancellationToken
			)
		{
			RpcPointer<uint> pEntries = new RpcPointer<uint>();
			RpcPointer<uint> pResumeHandle = new RpcPointer<uint>();

			List<SessionInfo> sessions = new List<SessionInfo>();
			Win32ErrorCode res;
			do
			{
				var pInfo = new RpcPointer<SESSION_ENUM_STRUCT>(level switch
				{
					SessionInfoLevel.Level0 => new SESSION_ENUM_STRUCT
					{
						Level = 0,
						SessionInfo = new SESSION_ENUM_UNION
						{
							Level = 0,
							Level0 = new RpcPointer<SESSION_INFO_0_CONTAINER>()
						}
					},
					SessionInfoLevel.Level1 => new SESSION_ENUM_STRUCT
					{
						Level = 1,
						SessionInfo = new SESSION_ENUM_UNION
						{
							Level = 1,
							Level1 = new RpcPointer<SESSION_INFO_1_CONTAINER>()
						}
					},
					SessionInfoLevel.Level2 => new SESSION_ENUM_STRUCT
					{
						Level = 2,
						SessionInfo = new SESSION_ENUM_UNION
						{
							Level = 2,
							Level2 = new RpcPointer<SESSION_INFO_2_CONTAINER>()
						}
					},
					SessionInfoLevel.Level10 => new SESSION_ENUM_STRUCT
					{
						Level = 10,
						SessionInfo = new SESSION_ENUM_UNION
						{
							Level = 10,
							Level10 = new RpcPointer<SESSION_INFO_10_CONTAINER>()
						}
					},
					SessionInfoLevel.Level502 => new SESSION_ENUM_STRUCT
					{
						Level = 502,
						SessionInfo = new SESSION_ENUM_UNION
						{
							Level = 502,
							Level502 = new RpcPointer<SESSION_INFO_502_CONTAINER>()
						}
					},
				});
				res = (Win32ErrorCode)await this._proxy.NetrSessionEnum(
					serverName ?? LocalName,
					clientName,
					userName,
					pInfo,
					(uint)bufferSize,
					pEntries,
					pResumeHandle,
					cancellationToken
					).ConfigureAwait(false);

				switch (level)
				{
					case SessionInfoLevel.Level0:
						GetEntriesFrom(sessions, pInfo.value.SessionInfo.Level0?.value.Buffer?.value, r => new SessionInfo(r));
						break;
					case SessionInfoLevel.Level1:
						GetEntriesFrom(sessions, pInfo.value.SessionInfo.Level1?.value.Buffer?.value, r => new SessionInfo(r));
						break;
					case SessionInfoLevel.Level2:
						GetEntriesFrom(sessions, pInfo.value.SessionInfo.Level2?.value.Buffer?.value, r => new SessionInfo(r));
						break;
					case SessionInfoLevel.Level10:
						GetEntriesFrom(sessions, pInfo.value.SessionInfo.Level10?.value.Buffer?.value, r => new SessionInfo(r));
						break;
					case SessionInfoLevel.Level502:
						GetEntriesFrom(sessions, pInfo.value.SessionInfo.Level502?.value.Buffer?.value, r => new SessionInfo(r));
						break;
				}
			} while (res == Win32ErrorCode.ERROR_MORE_DATA);
			res.CheckAndThrow();

			return sessions;
		}

		public async Task CloseSession(string? clientName, string? userName, CancellationToken cancellationToken)
		{
			Win32ErrorCode res = (Win32ErrorCode)await this._proxy.NetrSessionDel(
				LocalName,
				clientName,
				userName,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		public async Task AddShare(
			string shareName,
			string path,
			SharePermissions permissions,
			ShareType shareType,
			string remark,
			CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(shareName))
				throw new ArgumentException($"'{nameof(shareName)}' cannot be null or empty", nameof(shareName));
			if (string.IsNullOrEmpty(path))
				throw new ArgumentException($"'{nameof(path)}' cannot be null or empty", nameof(path));

			RpcPointer<uint> pParmErr = new RpcPointer<uint>();
			Win32ErrorCode res = (Win32ErrorCode)await this._proxy.NetrShareAdd(
				LocalName,
				2,
				new RpcPointer<SHARE_INFO>(new SHARE_INFO
				{
					unionSwitch = 2,
					ShareInfo2 = new RpcPointer<SHARE_INFO_2>(new SHARE_INFO_2
					{
						shi2_netname = new RpcPointer<string>(shareName),
						shi2_type = (uint)shareType,
						shi2_remark = StringToPointer(remark),
						shi2_permissions = (uint)permissions,
						shi2_path = new RpcPointer<string>(path),
					})
				}),
				pParmErr,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		public async Task AddShare(ShareInfo shareInfo, CancellationToken cancellationToken)
		{
			if (shareInfo is null)
				throw new ArgumentNullException(nameof(shareInfo));

			if (shareInfo.ShareName == null)
				throw new ArgumentException($"{nameof(shareInfo.ShareName)} is null.", nameof(shareInfo));
			if (shareInfo.Path == null)
				throw new ArgumentException($"{nameof(shareInfo.Path)} is null.", nameof(shareInfo));

			RpcPointer<uint> pParmErr = new RpcPointer<uint>();
			var res = (Win32ErrorCode)await this._proxy.NetrShareAdd(
				LocalName,
				2,
				new RpcPointer<SHARE_INFO>(new SHARE_INFO
				{
					unionSwitch = 2,
					ShareInfo2 = new RpcPointer<SHARE_INFO_2>(new SHARE_INFO_2
					{
						shi2_netname = StringToPointer(shareInfo.ShareName),
						shi2_type = (uint)shareInfo.ShareType,
						shi2_remark = StringToPointer(shareInfo.Remark),
						shi2_permissions = (uint)shareInfo.Permissions,
						shi2_max_uses = (uint)shareInfo.MaxUses,
						shi2_current_uses = (uint)shareInfo.CurrentUses,
						shi2_path = StringToPointer(shareInfo.Path),
						shi2_passwd = StringToPointer(shareInfo.Password)
					})
				}),
				pParmErr,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		private delegate Task<uint> ShareEnumFunc(
			string ServerName,
			RpcPointer<SHARE_ENUM_STRUCT> InfoStruct,
			uint PreferedMaximumLength,
			RpcPointer<uint> TotalEntries,
			RpcPointer<uint> ResumeHandle,
			CancellationToken cancellationToken);

		public Task<IList<ShareInfo>> GetShares(CancellationToken cancellationToken)
			=> this.GetShares(LocalName, ShareInfoLevel.Level2, DefaultReturnBufferSize, cancellationToken);
		public Task<IList<ShareInfo>> GetShares(string serverName, ShareInfoLevel level, int bufferSize, CancellationToken cancellationToken)
			=> EnumShares(serverName, level, bufferSize, this._proxy.NetrShareEnum, cancellationToken);

		public Task<IList<ShareInfo>> GetStickyShares(CancellationToken cancellationToken)
			=> this.GetStickyShares(LocalName, ShareInfoLevel.Level2, DefaultReturnBufferSize, cancellationToken);
		public Task<IList<ShareInfo>> GetStickyShares(string serverName, ShareInfoLevel level, int bufferSize, CancellationToken cancellationToken)
			=> EnumShares(serverName, level, bufferSize, this._proxy.NetrShareEnumSticky, cancellationToken);

		public const int DefaultReturnBufferSize = -1;
		private static async Task<IList<ShareInfo>> EnumShares(
			string? serverName,
			ShareInfoLevel level,
			int bufferSize,
			ShareEnumFunc enumFunc,
			CancellationToken cancellationToken)
		{
			RpcPointer<uint> pEntries = new RpcPointer<uint>();
			RpcPointer<uint> pResumeHandle = new RpcPointer<uint>();

			var shares = new List<ShareInfo>();
			Win32ErrorCode res;
			do
			{
				var pInfo = new RpcPointer<SHARE_ENUM_STRUCT>(level switch
				{
					ShareInfoLevel.Level1 => ShareEnumLevel1Struct(),
					ShareInfoLevel.Level2 => ShareEnumLevel2Struct(),
					ShareInfoLevel.Level501 => ShareEnumLevel501Struct(),
					ShareInfoLevel.Level502 => ShareEnumLevel502Struct(),
					ShareInfoLevel.Level503 => ShareEnumLevel503Struct(),
					_ => throw new ArgumentException("The requested level is not valid.")
				});

				res = (Win32ErrorCode)await enumFunc(
					serverName ?? LocalName,
					pInfo,
					(uint)bufferSize,
					pEntries,
					pResumeHandle,
					cancellationToken
					).ConfigureAwait(false);

				switch (level)
				{
					case ShareInfoLevel.Level1:
						GetEntriesFrom(shares, pInfo.value.ShareInfo.Level1?.value.Buffer?.value, r => new ShareInfo(r));
						break;
					case ShareInfoLevel.Level2:
						GetEntriesFrom(shares, pInfo.value.ShareInfo.Level2?.value.Buffer?.value, r => new ShareInfo(r));
						break;
					case ShareInfoLevel.Level501:
						GetEntriesFrom(shares, pInfo.value.ShareInfo.Level501?.value.Buffer?.value, r => new ShareInfo(r));
						break;
					case ShareInfoLevel.Level502:
						GetEntriesFrom(shares, pInfo.value.ShareInfo.Level502?.value.Buffer?.value, r => new ShareInfo(r));
						break;
					case ShareInfoLevel.Level503:
						GetEntriesFrom(shares, pInfo.value.ShareInfo.Level503?.value.Buffer?.value, r => new ShareInfo(r));
						break;
				}
			} while (res == Win32ErrorCode.ERROR_MORE_DATA);
			res.CheckAndThrow();

			return shares;
		}

		private static void GetEntriesFrom<TShareInfo, TOutput>(
			List<TOutput> shares,
			TShareInfo[]? entries,
			Func<TShareInfo, TOutput> selector
			)
		{
			if (entries != null)
			{
				foreach (var entry in entries)
				{
					shares.Add(selector(entry));
				}
			}
		}

		private static SHARE_ENUM_STRUCT ShareEnumLevel1Struct()
		{
			return new SHARE_ENUM_STRUCT
			{
				Level = 1,
				ShareInfo = new SHARE_ENUM_UNION
				{
					Level = 1,
					Level1 = new RpcPointer<SHARE_INFO_1_CONTAINER>()
				}
			};
		}

		private static SHARE_ENUM_STRUCT ShareEnumLevel2Struct()
		{
			return new SHARE_ENUM_STRUCT
			{
				Level = 2,
				ShareInfo = new SHARE_ENUM_UNION
				{
					Level = 2,
					Level2 = new RpcPointer<SHARE_INFO_2_CONTAINER>()
				}
			};
		}

		private static SHARE_ENUM_STRUCT ShareEnumLevel501Struct()
		{
			return new SHARE_ENUM_STRUCT
			{
				Level = 501,
				ShareInfo = new SHARE_ENUM_UNION
				{
					Level = 501,
					Level501 = new RpcPointer<SHARE_INFO_501_CONTAINER>()
				}
			};
		}

		private static SHARE_ENUM_STRUCT ShareEnumLevel502Struct()
		{
			return new SHARE_ENUM_STRUCT
			{
				Level = 502,
				ShareInfo = new SHARE_ENUM_UNION
				{
					Level = 502,
					Level502 = new RpcPointer<SHARE_INFO_502_CONTAINER>()
				}
			};
		}

		private static SHARE_ENUM_STRUCT ShareEnumLevel503Struct()
		{
			return new SHARE_ENUM_STRUCT
			{
				Level = 503,
				ShareInfo = new SHARE_ENUM_UNION
				{
					Level = 503,
					Level503 = new RpcPointer<SHARE_INFO_503_CONTAINER>()
				}
			};
		}
	}
}
