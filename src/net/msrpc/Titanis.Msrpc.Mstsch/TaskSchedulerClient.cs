using SchRpc;
using System.Xml.Serialization;
using Titanis.DceRpc.Client;
using Titanis.Security;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mstsch
{
	[Flags]
	public enum FolderEnumOptions
	{
		None = 0,
		IncludeHidden = 1,
	}

	public class TaskSchedulerClient : RpcServiceClient<SchRpc.ITaskSchedulerServiceClientProxy>
	{
		// [MS-TSCH] § 1.9 Standards Assignments
		public override bool SupportsDynamicTcp => true;
		public override bool RequiresEncryptionOverTcp => true;
		public override string? ServiceClass => ServiceClassNames.HostL;

		public async IAsyncEnumerable<string> GetFolders(
			string? path,
			int pageSize,
			FolderEnumOptions options,
			CancellationToken cancellationToken)
		{
			path ??= string.Empty;

			DceRpc.RpcPointer<uint> pStartIndex = new(0);
			do
			{
				DceRpc.RpcPointer<uint> pcNames = new();
				DceRpc.RpcPointer<DceRpc.RpcPointer<DceRpc.RpcPointer<string>[]>> pNames = new();
				var res = (Hresult)await _proxy.SchRpcEnumFolders(
					path,
					(uint)options,
					pStartIndex,
					(uint)pageSize,
					pcNames,
					pNames,
					cancellationToken
					).ConfigureAwait(false);
				res.CheckAndThrow();

				if (pcNames.value == 0)
					break;

				foreach (var pName in pNames.value?.value)
				{
					var name = pName.value;
					yield return name;
				}
			} while (true);
		}

		public async IAsyncEnumerable<string> GetTasks(
			string? path,
			int pageSize,
			FolderEnumOptions options,
			CancellationToken cancellationToken)
		{
			path ??= string.Empty;

			DceRpc.RpcPointer<uint> pStartIndex = new(0);
			do
			{
				DceRpc.RpcPointer<uint> pcNames = new();
				DceRpc.RpcPointer<DceRpc.RpcPointer<DceRpc.RpcPointer<string>[]>> pNames = new();
				var res = (Hresult)await _proxy.SchRpcEnumTasks(
					path,
					(uint)options,
					pStartIndex,
					(uint)pageSize,
					pcNames,
					pNames,
					cancellationToken
					).ConfigureAwait(false);
				res.CheckAndThrow();

				if (pcNames.value == 0)
					break;

				foreach (var pName in pNames.value?.value)
				{
					var name = pName.value;
					yield return name;
				}
			} while (true);
		}

		internal static readonly XmlSerializer serTask = new XmlSerializer(typeof(Xml.taskType));
		public async Task<TaskDefinition> RetrieveTask(
			string taskPath,
			CancellationToken cancellationToken
			)
		{
			ArgumentException.ThrowIfNullOrEmpty(taskPath);

			DceRpc.RpcPointer<DceRpc.RpcPointer<string>> pXml = new();
			var res = (Hresult)await _proxy.SchRpcRetrieveTask(
				taskPath,
				string.Empty,
				0,
				pXml,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			var xmlTask = (Xml.taskType)serTask.Deserialize(new StringReader(pXml.value.value));

			return new TaskDefinition(
				Path.GetFileName(taskPath),
				taskPath,
				pXml.value.value,
				xmlTask
				);
		}

		public async Task<Guid> StartTask(
			string taskPath,
			string[]? args,
			TaskStartOptions options,
			int sessionId,
			SecurityIdentifier? userSid,
			CancellationToken cancellationToken
			)
		{
			ArgumentException.ThrowIfNullOrEmpty(taskPath);
			args ??= [];

			DceRpc.RpcPointer<Guid> pGuid = new();
			var res = (Hresult)await _proxy.SchRpcRun(
				taskPath,
				(uint)args.Length,
				Array.ConvertAll(args, r => new DceRpc.RpcPointer<string>(r)),
				(uint)options,
				(uint)sessionId,
				userSid?.ToString(),
				pGuid,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return pGuid.value;
		}

		public async Task StopTask(
			string taskPath,
			CancellationToken cancellationToken
			)
		{
			ArgumentException.ThrowIfNullOrEmpty(taskPath);

			var res = (Hresult)await _proxy.SchRpcStop(
				taskPath,
				0,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		public async Task StopTaskInstance(
			Guid instance,
			CancellationToken cancellationToken
			)
		{
			var res = (Hresult)await _proxy.SchRpcStopInstance(
				instance,
				0,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		public async Task CreateTask(
			string taskPath,
			TaskDefinition taskDef,
			RegisterTaskOptions options,
			string? sddl,
			TaskLogonType logonType,
			string? userName,
			string? password,
			CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(taskPath);
			ArgumentNullException.ThrowIfNull(taskDef);

			DceRpc.RpcPointer<DceRpc.RpcPointer<string>> pActualPath = new();
			DceRpc.RpcPointer<DceRpc.RpcPointer<SchRpc.TASK_XML_ERROR_INFO>> pErrorInfo = new();
			TASK_USER_CRED[]? creds = (string.IsNullOrEmpty(userName)) ? null : [
				new SchRpc.TASK_USER_CRED
				{
					userId = new DceRpc.RpcPointer<string>(userName),
					password=new DceRpc.RpcPointer<string>( password),
					flags = 0
				}];
			var res = (Hresult)await _proxy.SchRpcRegisterTask(
				taskPath,
				taskDef.GetXml(),
				(uint)options,
				sddl,
				(uint)logonType,
				(creds != null) ? 1U : 0U,
				creds,
				pActualPath,
				pErrorInfo,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}
	}

	// [MS-TSCH] § 3.2.5.4.2 SchRpcRegisterTask (Opnum 1)
	[Flags]
	public enum RegisterTaskOptions
	{
		None = 0,

		ValidateOnly = 1,
		Create = 2,
		Overwrite = 4,
		Disable = 8,
		DontAddPrincipalAce = 0x10,
		IgnoreRegistrationTriggers = 0x20,
	}

	// [MS-TSCH] § 2.3.9 TASK_LOGON_TYPE
	public enum TaskLogonType
	{
		None = 0,
		Password = 1,
		S4u = 2,
		Interactive = 3,
		Group = 4,
		ServiceAccount = 5,
		InteractiveTokenOrPassword = 6,
	}

	// [MS-TSCH] § 3.2.5.4.13 SchRpcRun (Opnum 12)
	public enum TaskStartOptions
	{
		None = 0,
		RunAsSelf = 1,
		IgnoreConstraints = 2,
		UseSessionId = 4,
		UserSid = 8,
	}
}
