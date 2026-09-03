using ms_tsch;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;

namespace Titanis.Msrpc.Mstsch
{
	public class TschClient : RpcServiceClient<ITaskSchedulerServiceClientProxy>
	{
		public const string TschPipeName = "atsvc";

		public sealed override string? WellKnownPipeName => TschPipeName;
		public sealed override bool SupportsDynamicTcp => true;
		public sealed override bool SupportsNdr64 => true;
		public sealed override bool SupportsReauthOverNamedPipes => true;

		public async Task<uint> GetHighestVersion(CancellationToken cancellationToken)
		{
			RpcPointer<uint> pVersion = new RpcPointer<uint>();
			int hr = await this._proxy.SchRpcHighestVersion(pVersion, cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);
			return pVersion.value;
		}

		public async Task<IList<string>> EnumTasks(string path, uint flags, CancellationToken cancellationToken)
		{
			List<string> result = new List<string>();
			RpcPointer<uint> pStartIndex = new RpcPointer<uint>(0);
			const uint batchSize = 64;

			int hr;
			do
			{
				RpcPointer<uint> pcNames = new RpcPointer<uint>();
				RpcPointer<RpcPointer<string>[]> pNames = new RpcPointer<RpcPointer<string>[]>();
				hr = await this._proxy.SchRpcEnumTasks(
					path, flags, pStartIndex, batchSize,
					pcNames, pNames,
					cancellationToken).ConfigureAwait(false);

				if (hr != 0 && hr != 0x00080005) // S_FALSE = more data
					ThrowOnFailure(hr);

				if (pNames.value != null)
				{
					foreach (var name in pNames.value)
					{
						if (name?.value != null)
							result.Add(name.value);
					}
				}
			} while (hr == 0x00080005);

			return result;
		}

		public async Task<IList<string>> EnumFolders(string path, uint flags, CancellationToken cancellationToken)
		{
			List<string> result = new List<string>();
			RpcPointer<uint> pStartIndex = new RpcPointer<uint>(0);
			const uint batchSize = 64;

			int hr;
			do
			{
				RpcPointer<uint> pcNames = new RpcPointer<uint>();
				RpcPointer<RpcPointer<string>[]> pNames = new RpcPointer<RpcPointer<string>[]>();
				hr = await this._proxy.SchRpcEnumFolders(
					path, flags, pStartIndex, batchSize,
					pcNames, pNames,
					cancellationToken).ConfigureAwait(false);

				if (hr != 0 && hr != 0x00080005)
					ThrowOnFailure(hr);

				if (pNames.value != null)
				{
					foreach (var name in pNames.value)
					{
						if (name?.value != null)
							result.Add(name.value);
					}
				}
			} while (hr == 0x00080005);

			return result;
		}

		public async Task<string> RetrieveTask(string path, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pulNumLanguages = new RpcPointer<uint>(0);
			RpcPointer<string> pXml = new RpcPointer<string>();
			int hr = await this._proxy.SchRpcRetrieveTask(
				path, "\0", pulNumLanguages, pXml,
				cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);
			return pXml.value;
		}

		public async Task<string> RegisterTask(
			string path,
			string xml,
			TaskCreationFlags flags,
			string? sddl,
			TaskLogonType logonType,
			CancellationToken cancellationToken)
		{
			RpcPointer<string> pActualPath = new RpcPointer<string>();
			RpcPointer<RpcPointer<TASK_XML_ERROR_INFO>> pErrorInfo = new RpcPointer<RpcPointer<TASK_XML_ERROR_INFO>>();
			int hr = await this._proxy.SchRpcRegisterTask(
				path, xml, (uint)flags, sddl, (uint)logonType,
				0, null,
				pActualPath, pErrorInfo,
				cancellationToken).ConfigureAwait(false);

			if (hr < 0 && pErrorInfo.value?.value.line > 0)
			{
				var err = pErrorInfo.value.value;
				throw new TschException(hr,
					$"XML error at line {err.line}, column {err.column}: " +
					$"node={err.node?.value}, value={err.value?.value}");
			}

			ThrowOnFailure(hr);
			return pActualPath.value ?? path;
		}

		public async Task DeleteTask(string path, CancellationToken cancellationToken)
		{
			int hr = await this._proxy.SchRpcDelete(path, 0, cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);
		}

		public async Task<Guid> RunTask(string path, string[]? args, uint flags, CancellationToken cancellationToken)
		{
			return await RunTask(path, args, flags, 0, null, cancellationToken).ConfigureAwait(false);
		}

		public async Task<Guid> RunTask(string path, string[]? args, uint flags, uint sessionId, string? user, CancellationToken cancellationToken)
		{
			uint cArgs = (uint)(args?.Length ?? 0);
			RpcPointer<string>[]? pArgs = null;
			if (args != null && args.Length > 0)
			{
				pArgs = new RpcPointer<string>[args.Length];
				for (int i = 0; i < args.Length; i++)
					pArgs[i] = new RpcPointer<string>(args[i]);
			}

			RpcPointer<string> pUser = user != null ? new RpcPointer<string>(user) : null;
			RpcPointer<Guid> pGuid = new RpcPointer<Guid>();
			int hr = await this._proxy.SchRpcRun(
				path, cArgs, pArgs ?? Array.Empty<RpcPointer<string>>(),
				flags, sessionId, pUser,
				pGuid, cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);
			return pGuid.value;
		}

		public async Task StopTask(string path, CancellationToken cancellationToken)
		{
			int hr = await this._proxy.SchRpcStop(path, 0, cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);
		}

		public async Task StopInstance(Guid instanceId, CancellationToken cancellationToken)
		{
			int hr = await this._proxy.SchRpcStopInstance(instanceId, 0, cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);
		}

		public async Task<TaskInfo> GetTaskInfo(string path, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pEnabled = new RpcPointer<uint>();
			RpcPointer<uint> pState = new RpcPointer<uint>();
			int hr = await this._proxy.SchRpcGetTaskInfo(
				path, 0, pEnabled, pState,
				cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);

			return new TaskInfo
			{
				Path = path,
				Enabled = pEnabled.value != 0,
				State = (TaskState)pState.value
			};
		}

		public async Task<LastRunInfo> GetLastRunInfo(string path, CancellationToken cancellationToken)
		{
			RpcPointer<SYSTEMTIME> pLastRuntime = new RpcPointer<SYSTEMTIME>();
			RpcPointer<uint> pLastReturnCode = new RpcPointer<uint>();
			int hr = await this._proxy.SchRpcGetLastRunInfo(
				path, pLastRuntime, pLastReturnCode,
				cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);

			return new LastRunInfo
			{
				LastRunTime = SystemTimeToDateTime(pLastRuntime.value),
				LastReturnCode = pLastReturnCode.value
			};
		}

		public async Task EnableTask(string path, bool enable, CancellationToken cancellationToken)
		{
			int hr = await this._proxy.SchRpcEnableTask(
				path, enable ? 1U : 0U,
				cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);
		}

		public async Task CreateFolder(string path, string? sddl, CancellationToken cancellationToken)
		{
			int hr = await this._proxy.SchRpcCreateFolder(
				path, sddl, 0,
				cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);
		}

		public async Task<IList<Guid>> EnumInstances(string? path, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pcGuids = new RpcPointer<uint>();
			RpcPointer<Guid[]> pGuids = new RpcPointer<Guid[]>();
			int hr = await this._proxy.SchRpcEnumInstances(
				path, 0, pcGuids, pGuids,
				cancellationToken).ConfigureAwait(false);
			ThrowOnFailure(hr);
			return pGuids.value ?? Array.Empty<Guid>();
		}

		private static DateTime? SystemTimeToDateTime(SYSTEMTIME st)
		{
			if (st.wYear == 0) return null;
			try
			{
				return new DateTime(st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond, st.wMilliseconds, DateTimeKind.Local);
			}
			catch
			{
				return null;
			}
		}

		private static void ThrowOnFailure(int hr)
		{
			if (hr < 0)
				throw new TschException(hr);
		}
	}

	public class TschException : Exception
	{
		public int HResult { get; }

		public TschException(int hr)
			: base($"Task Scheduler operation failed with HRESULT 0x{hr:X8}")
		{
			this.HResult = hr;
		}

		public TschException(int hr, string message)
			: base(message)
		{
			this.HResult = hr;
		}
	}

	public class TaskInfo
	{
		public string Path { get; set; }
		public bool Enabled { get; set; }
		public TaskState State { get; set; }
	}

	public class LastRunInfo
	{
		public DateTime? LastRunTime { get; set; }
		public uint LastReturnCode { get; set; }
	}

	// [MS-TSCH] § 2.3.13
	public enum TaskState : uint
	{
		Unknown = 0,
		Disabled = 1,
		Queued = 2,
		Ready = 3,
		Running = 4
	}

	// [MS-TSCH] § 2.5.1
	[Flags]
	public enum TaskCreationFlags : uint
	{
		Create = 2,
		Update = 4,
		CreateOrUpdate = 6,
		Disable = 8,
		DontAddPrincipalAce = 0x10,
		IgnoreRegistrationTriggers = 0x20,
		ValidateOnly = 0x1
	}

	// [MS-TSCH] § 2.5.3
	public enum TaskLogonType : uint
	{
		None = 0,
		Password = 1,
		S4U = 2,
		InteractiveToken = 3,
		Group = 4,
		ServiceAccount = 5,
		InteractiveTokenOrPassword = 6
	}
}
