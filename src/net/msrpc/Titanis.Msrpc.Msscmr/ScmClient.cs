using ms_scmr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Communication;
using Titanis.IO;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msscmr
{
	/// <summary>
	/// Implements a client to work with the Service Control Manager.
	/// </summary>
	public partial class ScmClient : RpcServiceClient<svcctlClientProxy>
	{
		public ScmClient()
		{
		}

		public const string PipeName = "svcctl";
		private const int BufferSize = 1024;

		public async Task<Scm> OpenScm(ScmAccess access, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> pHandle = new RpcPointer<RpcContextHandle>();
			var res = (Win32ErrorCode)await this._proxy.ROpenSCManager2(null, (uint)access, pHandle, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return new Scm(pHandle.value, this);
		}

		internal async Task<Service> CreateService(
			RpcContextHandle handle,
			string serviceName,
			ServiceConfig config,
			ServiceAccess access,
			CancellationToken cancellationToken
			)
		{
			if (config is null)
				throw new ArgumentNullException(nameof(config));

			byte[]? deps = (config.Dependencies == null)
				? null
				: Encoding.Unicode.GetBytes(string.Join("\0", config.Dependencies) + "\0\0")
				;

			byte[]? passwordBytes = (config.StartPassword == null)
				? null
				: Encoding.Unicode.GetBytes(config.StartPassword);

			RpcPointer<uint>? pTag = (config.TagId > 0)
				? new RpcPointer<uint>((uint)config.TagId)
				: null;
			RpcPointer<RpcContextHandle> pHandle = new RpcPointer<RpcContextHandle>();
			var res = (Win32ErrorCode)await this._proxy.RCreateServiceW(
				handle,
				serviceName,
				config.DisplayName,
				(uint)access,
				(uint)config.ServiceType,
				(uint)config.StartType,
				(uint)config.ErrorControl,
				config.BinaryPathName,
				config.LoadOrderGroup,
				pTag,
				deps,
				(uint)((deps != null) ? deps.Length : 0),
				config.ServiceStartName,
				passwordBytes,
				(uint)(passwordBytes != null ? passwordBytes.Length : 0),
				pHandle,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new Service(pHandle.value, serviceName, this);
		}

		internal async Task<IList<EnumServiceStatusInfo>> GetServicesInGroup(
			RpcContextHandle hscm,
			string group,
			ServiceTypes types,
			ServiceStates states,
			CancellationToken cancellationToken
			)
		{
			int cbBuf = BufferSize;
			RpcPointer<byte[]> pBuf = new RpcPointer<byte[]>(new byte[cbBuf]);
			RpcPointer<uint> pcbNeeded = new RpcPointer<uint>();
			RpcPointer<uint> pcEntries = new RpcPointer<uint>();
			RpcPointer<uint> pResumeIndex = new RpcPointer<uint>();
			Win32ErrorCode res;
			List<EnumServiceStatusInfo> infos = new List<EnumServiceStatusInfo>();
			do
			{
				res = (Win32ErrorCode)await this._proxy.REnumServiceGroupW(
					hscm,
					(uint)types,
					(uint)states,
					pBuf,
					(uint)cbBuf,
					pcbNeeded,
					pcEntries,
					pResumeIndex,
					group,
					cancellationToken
					).ConfigureAwait(false);

				ByteMemoryReader reader = new ByteMemoryReader(pBuf.value);
				reader.ReadServiceStatusInfoArray((int)pcEntries.value, infos);
			} while (res == Win32ErrorCode.ERROR_MORE_DATA);
			res.CheckAndThrow();

			return infos;
		}


		internal async Task<ScmLockStatus> QueryLockStatus(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			RpcPointer<QUERY_SERVICE_LOCK_STATUSW> pBuf = new RpcPointer<QUERY_SERVICE_LOCK_STATUSW>();
			RpcPointer<uint> pcbNeeded = new RpcPointer<uint>();
			var res = (Win32ErrorCode)await this._proxy.RQueryServiceLockStatusW(
				handle,
				pBuf,
				BufferSize,
				pcbNeeded,
				cancellationToken
				).ConfigureAwait(false);
			if (res == Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER)
			{
				res = (Win32ErrorCode)await this._proxy.RQueryServiceLockStatusW(
					handle,
					pBuf,
					pcbNeeded.value,
					pcbNeeded,
					cancellationToken
					).ConfigureAwait(false);
			}

			res.CheckAndThrow();

			return new ScmLockStatus
			{
				IsLocked = pBuf.value.fIsLocked != 0,
				LockOwner = pBuf.value.lpLockOwner?.value,
				LockDuration = (int)pBuf.value.dwLockDuration
			};
		}

		internal async Task ChangeConfig(RpcContextHandle handle, ServiceConfig config, CancellationToken cancellationToken)
		{
			if (config is null)
				throw new ArgumentNullException(nameof(config));

			byte[] deps = (config.Dependencies == null)
				? null
				: Encoding.Unicode.GetBytes(string.Join("\0", config.Dependencies) + "\0\0")
				;
			RpcPointer<uint> pTag = (config.TagId > 0)
				? new RpcPointer<uint>((uint)config.TagId)
				: null;

			byte[] passwordBytes = (!string.IsNullOrEmpty(config.StartPassword)
				? Encoding.Unicode.GetBytes(config.StartPassword)
				: null);
			var res = (Win32ErrorCode)await this._proxy.RChangeServiceConfigW(
				handle,
				(uint)config.ServiceType,
				(uint)config.StartType,
				(uint)config.ErrorControl,
				config.BinaryPathName,
				config.LoadOrderGroup,
				pTag,
				deps,
				(uint)((deps != null) ? deps.Length : 0),
				config.ServiceStartName,
				passwordBytes,
				(uint)(passwordBytes != null ? passwordBytes.Length : 0),
				config.DisplayName,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		internal async Task SetServiceDescription(RpcContextHandle handle, string description, CancellationToken cancellationToken)
		{
			uint infoLevel = 1;

			var res = (Win32ErrorCode)await this._proxy.RChangeServiceConfig2W(
				handle,
				new SC_RPC_CONFIG_INFOW
				{
					dwInfoLevel = infoLevel,
					unnamed_1 = new Unnamed_2
					{
						dwInfoLevel = infoLevel,
						psd = new RpcPointer<SERVICE_DESCRIPTIONW>(new SERVICE_DESCRIPTIONW
						{
							lpDescription = new RpcPointer<string>(description)
						})
					}
				},
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		internal async Task<string> GetServiceDisplayName(RpcContextHandle handle, string serviceName, CancellationToken cancellationToken)
		{
			int cch = 512;
			RpcPointer<uint> pcch = new RpcPointer<uint>((uint)cch);
			RpcPointer<string> pDisplayName = new RpcPointer<string>(new string('\0', cch));
			var res = (Win32ErrorCode)await this._proxy.RGetServiceDisplayNameW(
				handle,
				serviceName,
				pDisplayName,
				pcch,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return pDisplayName.value;
		}

		internal async Task<string> GetServiceKeyName(RpcContextHandle handle, string displayName, CancellationToken cancellationToken)
		{
			int cch = 512;
			RpcPointer<uint> pcch = new RpcPointer<uint>((uint)cch);
			RpcPointer<string> pKeyName = new RpcPointer<string>(new string('\0', cch));
			var res = (Win32ErrorCode)await this._proxy.RGetServiceKeyNameW(
				handle,
				displayName,
				pKeyName,
				pcch,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return pKeyName.value;
		}

		internal async Task StartService(RpcContextHandle handle, string[] argv, CancellationToken cancellationToken)
		{
			var argArray = (argv == null)
				? Array.Empty<STRING_PTRSW>()
				: argv.ConvertAll<string, STRING_PTRSW>(s => new STRING_PTRSW { StringPtr = new RpcPointer<string>(s) });
			;
			var res = (Win32ErrorCode)await this._proxy.RStartServiceW(
				handle,
				(argv != null) ? (uint)argv.Length : 0U,
				argArray,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		internal async Task<IList<EnumServiceStatusInfo>> GetDependentServices(
			RpcContextHandle handle,
			ServiceStates states,
			CancellationToken cancellationToken)
		{
			const int cbBuf = BufferSize;
			RpcPointer<byte[]> pBuf = new RpcPointer<byte[]>(new byte[cbBuf]);
			RpcPointer<uint> pcbNeeded = new RpcPointer<uint>();
			RpcPointer<uint> pcEntries = new RpcPointer<uint>();
			List<EnumServiceStatusInfo> infos = new List<EnumServiceStatusInfo>();

			var res = (Win32ErrorCode)await this._proxy.REnumDependentServicesW(
				handle,
				(uint)states,
				pBuf,
				cbBuf,
				pcbNeeded,
				pcEntries,
				cancellationToken
				).ConfigureAwait(false);

			if (res == Win32ErrorCode.ERROR_MORE_DATA)
			{
				res = (Win32ErrorCode)await this._proxy.REnumDependentServicesW(
					handle,
					(uint)states,
					pBuf,
					pcbNeeded.value,
					pcbNeeded,
					pcEntries,
					cancellationToken
					).ConfigureAwait(false);
			}
			res.CheckAndThrow();

			ByteMemoryReader reader = new ByteMemoryReader(pBuf.value);
			reader.ReadServiceStatusInfoArray((int)pcEntries.value, infos);

			return infos;
		}

		public async Task NotifyBootConfigStatus(int status, CancellationToken cancellationToken)
		{
			var res = (Win32ErrorCode)await this._proxy.RNotifyBootConfigStatus(LocalName, (uint)status, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		internal async Task<ScmLock> LockScm(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> pLock = new RpcPointer<RpcContextHandle>();
			var res = (Win32ErrorCode)await this._proxy.RLockServiceDatabase(
				handle,
				pLock,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return new ScmLock(pLock.value, this);
		}

		internal async Task<ServiceStatus> ControlService(RpcContextHandle handle, ServiceControl control, CancellationToken cancellationToken)
		{
			RpcPointer<SERVICE_STATUS> pStatus = new RpcPointer<SERVICE_STATUS>();
			var res = (Win32ErrorCode)await this._proxy.RControlService(
				handle,
				(uint)control,
				pStatus,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new ServiceStatus(pStatus.value);
		}

		internal async Task SetStatus(RpcContextHandle handle, ServiceStatus status, CancellationToken cancellationToken)
		{
			if (status is null)
				throw new ArgumentNullException(nameof(status));

			SERVICE_STATUS svcStatus = new SERVICE_STATUS
			{
				dwServiceType = (uint)status.ServiceType,
				dwCurrentState = (uint)status.CurrentState,
				dwControlsAccepted = (uint)status.ControlsAccepted,
				dwWin32ExitCode = (uint)status.Win32ExitCode,
				dwServiceSpecificExitCode = (uint)status.ServiceSpecificExitCode,
				dwCheckPoint = (uint)status.Checkpoint,
				dwWaitHint = (uint)status.WaitHint
			};
			var res = (Win32ErrorCode)await this._proxy.RSetServiceStatus(
				handle,
				svcStatus,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

		}

		internal async Task<ServiceStatus> QueryStatus(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			RpcPointer<SERVICE_STATUS> pStatus = new RpcPointer<SERVICE_STATUS>();
			var res = (Win32ErrorCode)await this._proxy.RQueryServiceStatus(
				handle,
				pStatus,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return new ServiceStatus(pStatus.value);
		}

		internal async Task<ServiceStatusProcess> QueryServiceProcessInfo(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			int bufferSize = ScmReader.ServiceStatusProcessStructSize;
			var pBuf = new RpcPointer<byte[]>(new byte[bufferSize]);
			RpcPointer<uint> pcbNeeded = new RpcPointer<uint>();
			var res = (Win32ErrorCode)await this._proxy.RQueryServiceStatusEx(
				handle,
				SC_STATUS_TYPE.SC_STATUS_PROCESS_INFO,
				pBuf,
				(uint)bufferSize,
				pcbNeeded,
				cancellationToken
				).ConfigureAwait(false);
			if (res == Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER)
			{
				bufferSize = (int)pcbNeeded.value;
				pBuf = new RpcPointer<byte[]>(new byte[bufferSize]);
				res = (Win32ErrorCode)await this._proxy.RQueryServiceStatusEx(
					handle,
					SC_STATUS_TYPE.SC_STATUS_PROCESS_INFO,
					pBuf,
					(uint)bufferSize,
					pcbNeeded,
					cancellationToken
					).ConfigureAwait(false);
			}
			res.CheckAndThrow();

			SERVICE_STATUS_PROCESS status = ScmReader.ReadStatusProcessStructFrom(pBuf.value);
			return new ServiceStatusProcess(status);
		}

		internal async Task SetSecurity(RpcContextHandle handle, byte[] sd, SecurityInfo sections, CancellationToken cancellationToken)
		{
			var res = (Win32ErrorCode)await this._proxy.RSetServiceObjectSecurity(
				handle,
				(uint)sections,
				sd,
				(uint)sd.Length,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		internal async Task<byte[]> QuerySecurity(RpcContextHandle handle, SecurityInfo info, CancellationToken cancellationToken)
		{
			RpcPointer<byte[]> pBuf = new RpcPointer<byte[]>(null);
			RpcPointer<uint> pcbNeeded = new RpcPointer<uint>();
			var res = (Win32ErrorCode)await this._proxy.RQueryServiceObjectSecurity(
				handle,
				(uint)info,
				pBuf,
				BufferSize,
				pcbNeeded,
				cancellationToken
				).ConfigureAwait(false);
			if (res == Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER)
			{
				res = (Win32ErrorCode)await this._proxy.RQueryServiceObjectSecurity(
					handle,
					(uint)info,
					pBuf,
					pcbNeeded.value,
					pcbNeeded,
					cancellationToken
					).ConfigureAwait(false);
			}
			res.CheckAndThrow();

			return pBuf.value;
		}

		internal async Task CloseScm(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			await this._proxy.RCloseServiceHandle(new RpcPointer<RpcContextHandle>(handle), cancellationToken).ConfigureAwait(false);
		}


		internal async Task<IList<EnumServiceStatusInfo>> GetServices(
			RpcContextHandle hscm,
			ServiceTypes types,
			ServiceStates states,
			CancellationToken cancellationToken
			)
		{
			int cbBuf = BufferSize;
			RpcPointer<byte[]> pBuf = new RpcPointer<byte[]>(new byte[cbBuf]);
			RpcPointer<uint> pcbNeeded = new RpcPointer<uint>();
			RpcPointer<uint> pcEntries = new RpcPointer<uint>();
			RpcPointer<uint> pResumeIndex = new RpcPointer<uint>();
			Win32ErrorCode res;
			List<EnumServiceStatusInfo> infos = new List<EnumServiceStatusInfo>();
			do
			{
				res = (Win32ErrorCode)await this._proxy.REnumServicesStatusW(
					hscm,
					(uint)types,
					(uint)states,
					pBuf,
					(uint)cbBuf,
					pcbNeeded,
					pcEntries,
					pResumeIndex,
					cancellationToken
					).ConfigureAwait(false);

				ByteMemoryReader reader = new ByteMemoryReader(pBuf.value);
				reader.ReadServiceStatusInfoArray((int)pcEntries.value, infos);
			} while (res == Win32ErrorCode.ERROR_MORE_DATA);
			res.CheckAndThrow();

			return infos;
		}

		internal async Task<Service> OpenService(RpcContextHandle hscm, string serviceName, ServiceAccess access, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(serviceName))
				throw new ArgumentNullException(nameof(serviceName));

			RpcPointer<RpcContextHandle> pHandle = new RpcPointer<RpcContextHandle>();
			var res = (Win32ErrorCode)await this._proxy.ROpenServiceW(
				hscm,
				serviceName,
				(uint)access,
				pHandle,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new Service(pHandle.value, serviceName, this);
		}

		internal async Task<ServiceConfig> QueryServiceConfig(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			RpcPointer<QUERY_SERVICE_CONFIGW> pConfig = new RpcPointer<QUERY_SERVICE_CONFIGW>();
			RpcPointer<uint> pcbNeeded = new RpcPointer<uint>();
			var res = (Win32ErrorCode)await this._proxy.RQueryServiceConfigW(
				handle,
				pConfig,
				BufferSize,
				pcbNeeded,
				cancellationToken
				).ConfigureAwait(false);
			if (res == Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER)
			{
				res = (Win32ErrorCode)await this._proxy.RQueryServiceConfigW(
					handle,
					pConfig,
					pcbNeeded.value,
					pcbNeeded,
					cancellationToken
					).ConfigureAwait(false);
			}

			res.CheckAndThrow();

			string deps = pConfig.value.lpDependencies?.value;
			return new ServiceConfig
			{
				ServiceType = (ServiceTypes)pConfig.value.dwServiceType,
				StartType = (ServiceStartType)pConfig.value.dwStartType,
				ErrorControl = (ServiceErrorControl)pConfig.value.dwErrorControl,
				BinaryPathName = pConfig.value.lpBinaryPathName?.value,
				LoadOrderGroup = pConfig.value.lpLoadOrderGroup?.value,
				TagId = (int)pConfig.value.dwTagId,
				Dependencies = deps?.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries),
				ServiceStartName = pConfig.value.lpServiceStartName?.value,
				DisplayName = pConfig.value.lpDisplayName?.value
			};
		}

		enum ServiceConfigInfoLevel
		{
			Description = 1,
			FailureActions,
		}

		internal async Task<ServiceTrigger[]> QueryServiceTriggers(RpcContextHandle handle, string serviceName, CancellationToken cancellationToken)
		{
			RpcPointer<SC_RPC_CONFIG_INFOW> pInfo = new();
			const int TriggerInfo = 8;
			var res = (Win32ErrorCode)await this._proxy.RQueryServiceConfigEx(
				handle,
				TriggerInfo,
				pInfo,
				cancellationToken
				).ConfigureAwait(false);

			res.CheckAndThrow();

			var triggerInfos = pInfo.value.unnamed_1.psti.value.pTriggers?.value;
			if (triggerInfos == null)
				return Array.Empty<ServiceTrigger>();

			List<ServiceTrigger> triggers = new List<ServiceTrigger>(triggerInfos.Length);

			foreach (var triggerInfo in triggerInfos)
			{
				ServiceTriggerSubtypes.TryGetSubtypeInfo(triggerInfo.pTriggerSubtype.value, out var subtypeInfo);

				var dataStructs = triggerInfo.pDataItems?.value;
				var dataItems = (dataStructs != null) ? Array.ConvertAll(triggerInfo.pDataItems.value, r => ParseTriggerDataItem((ServiceTriggerType)triggerInfo.dwTriggerType, triggerInfo.pTriggerSubtype, r)) : Array.Empty<object>();

				triggers.Add(new ServiceTrigger(serviceName, (ServiceTriggerType)triggerInfo.dwTriggerType, triggerInfo.pTriggerSubtype.value, subtypeInfo.Name ?? triggerInfo.pTriggerSubtype.value.ToString(), (ServiceTriggerAction)triggerInfo.dwAction, dataItems));
			}

			return triggers.ToArray();
		}

		enum TriggerDataType
		{
			Binary = 1,
			UnicodeString = 2
		}
		private object ParseTriggerDataItem(ServiceTriggerType dwTriggerType, RpcPointer<Guid> pTriggerSubtype, SERVICE_TRIGGER_SPECIFIC_DATA_ITEM dataItem)
		{
			object? data = null;
			if ((TriggerDataType)dataItem.dwDataType == TriggerDataType.UnicodeString)
			{
				data = Encoding.Unicode.GetString(dataItem.pData.value);
			}
			else if ((TriggerDataType)dataItem.dwDataType == TriggerDataType.Binary)
			{
				data = dataItem.pData.value;
			}
			else if ((TriggerDataType)dataItem.dwDataType == TriggerDataType.Binary)
			{
				data = dataItem.pData.value;
			}
			else
			{
				data = dataItem.pData.value;
			}

			//if (pTriggerSubtype.value == ServiceTriggerSubtypes.RpcInterfaceEvent)
			//{
			//	data = new Guid(dataItem.pData.value);
			//}

			return data;
		}

		internal async Task<string> QueryServiceDescription(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			Memory<byte> mem = await QueryServiceConfig2(handle, ServiceConfigInfoLevel.Description, cancellationToken).ConfigureAwait(false);

			if (mem.Length > 4)
			{
				ByteMemoryReader reader = new ByteMemoryReader(mem);
				int offset = reader.ReadInt32();
				return reader.ExtractZStringUni(offset);
			}
			else
			{
				return null;
			}
		}

		internal async Task<ServiceFailureActions> QueryServiceFailureActions(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			Memory<byte> mem = await QueryServiceConfig2(handle, ServiceConfigInfoLevel.FailureActions, cancellationToken).ConfigureAwait(false);

			if (mem.Length > 4)
			{
				ByteMemoryReader reader = new ByteMemoryReader(mem);
				return reader.ReadServiceFailureActions();
			}
			else
			{
				return null;
			}
		}

		private async Task<Memory<byte>> QueryServiceConfig2(RpcContextHandle handle, ServiceConfigInfoLevel infoLevel, CancellationToken cancellationToken)
		{
			uint cbBuf = BufferSize;

			RpcPointer<byte[]> pConfig = new RpcPointer<byte[]>(new byte[cbBuf]);
			RpcPointer<uint> pcbNeeded = new RpcPointer<uint>();
			var res = (Win32ErrorCode)await this._proxy.RQueryServiceConfig2W(
				handle,
				(uint)infoLevel,
				pConfig,
				cbBuf,
				pcbNeeded,
				cancellationToken
				).ConfigureAwait(false);
			if (res == Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER)
			{
				cbBuf = pcbNeeded.value;
				pConfig = new RpcPointer<byte[]>(new byte[cbBuf]);
				res = (Win32ErrorCode)await this._proxy.RQueryServiceConfig2W(
					handle,
					1,
					pConfig,
					cbBuf,
					pcbNeeded,
					cancellationToken
					).ConfigureAwait(false);
			}

			res.CheckAndThrow();

			var mem = new Memory<byte>(pConfig.value, 0, (int)pcbNeeded.value);
			return mem;
		}

		internal async Task DeleteService(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var res = (Win32ErrorCode)await this._proxy.RDeleteService(handle, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		internal async Task UnlockScm(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			var res = (Win32ErrorCode)await this._proxy.RUnlockServiceDatabase(new RpcPointer<RpcContextHandle>(handle), cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}
	}
}
