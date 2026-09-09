namespace SchRpc
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum credFlag : int
	{
		credFlagDefault = 1
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct TASK_USER_CRED : IRpcFixedStruct
	{
		public RpcPointer<string> userId;
		public RpcPointer<string> password;
		public uint flags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.userId);
			encoder.WriteUniquePointer(this.password);
			encoder.WriteValue(this.flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.userId = decoder.ReadUniquePointer<string>();
			this.password = decoder.ReadUniquePointer<string>();
			this.flags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.userId is not null)
			{
				encoder.WriteWideCharString(this.userId.value);
			}

			if (this.password is not null)
			{
				encoder.WriteWideCharString(this.password.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.userId is not null)
			{
				this.userId.value = decoder.ReadWideCharString();
			}

			if (this.password is not null)
			{
				this.password.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct TASK_XML_ERROR_INFO : IRpcFixedStruct
	{
		public uint line;
		public uint column;
		public RpcPointer<string> node;
		public RpcPointer<string> value;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.line);
			encoder.WriteValue(this.column);
			encoder.WriteUniquePointer(this.node);
			encoder.WriteUniquePointer(this.value);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.line = decoder.ReadUInt32();
			this.column = decoder.ReadUInt32();
			this.node = decoder.ReadUniquePointer<string>();
			this.value = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.node is not null)
			{
				encoder.WriteWideCharString(this.node.value);
			}

			if (this.value is not null)
			{
				encoder.WriteWideCharString(this.value.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.node is not null)
			{
				this.node.value = decoder.ReadWideCharString();
			}

			if (this.value is not null)
			{
				this.value.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11"), GuidAttribute("86d35949-83c9-4044-b424-db363231fd0c"), RpcVersionAttribute(1, 0)]
	public partial interface ITaskSchedulerService
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcHighestVersion(RpcPointer<uint> pVersion, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcRegisterTask(string path, string xml, uint flags, string sddl, uint logonType, uint cCreds, TASK_USER_CRED[] pCreds, RpcPointer<RpcPointer<string>> pActualPath, RpcPointer<RpcPointer<TASK_XML_ERROR_INFO>> pErrorInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcRetrieveTask(string path, string lpcwszLanguagesBuffer, uint pulNumLanguages, RpcPointer<RpcPointer<string>> pXml, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcCreateFolder(string path, string sddl, uint flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcSetSecurity(string path, string sddl, uint flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcGetSecurity(string path, uint securityInformation, RpcPointer<RpcPointer<string>> sddl, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcEnumFolders(string path, uint flags, RpcPointer<uint> pStartIndex, uint cRequested, RpcPointer<uint> pcNames, RpcPointer<RpcPointer<RpcPointer<string>[]>> pNames, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcEnumTasks(string path, uint flags, RpcPointer<uint> startIndex, uint cRequested, RpcPointer<uint> pcNames, RpcPointer<RpcPointer<RpcPointer<string>[]>> pNames, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcEnumInstances(string path, uint flags, RpcPointer<uint> pcGuids, RpcPointer<RpcPointer<Guid[]>> pGuids, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcGetInstanceInfo(Guid guid, RpcPointer<RpcPointer<string>> pPath, RpcPointer<uint> pState, RpcPointer<RpcPointer<string>> pCurrentAction, RpcPointer<RpcPointer<string>> pInfo, RpcPointer<uint> pcGroupInstances, RpcPointer<RpcPointer<Guid[]>> pGroupInstances, RpcPointer<uint> pEnginePID, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcStopInstance(Guid guid, uint flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcStop(string path, uint flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcRun(string path, uint cArgs, RpcPointer<string>[] pArgs, uint flags, uint sessionId, string user, RpcPointer<Guid> pGuid, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcDelete(string path, uint flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcRename(string path, string newName, uint flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcScheduledRuntimes(string path, RpcPointer<ms_dtyp.SYSTEMTIME> start, RpcPointer<ms_dtyp.SYSTEMTIME> end, uint flags, uint cRequested, RpcPointer<uint> pcRuntimes, RpcPointer<RpcPointer<ms_dtyp.SYSTEMTIME[]>> pRuntimes, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcGetLastRunInfo(string path, RpcPointer<ms_dtyp.SYSTEMTIME> pLastRuntime, RpcPointer<uint> pLastReturnCode, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcGetTaskInfo(string path, uint flags, RpcPointer<uint> pEnabled, RpcPointer<uint> pState, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcGetNumberOfMissedRuns(string path, RpcPointer<uint> pNumberOfMissedRuns, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<int> SchRpcEnableTask(string path, uint enabled, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11"), IidAttribute("86d35949-83c9-4044-b424-db363231fd0c")]
	public partial class ITaskSchedulerServiceClientProxy : Titanis.DceRpc.Client.RpcClientProxy, ITaskSchedulerService, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcHighestVersion(RpcPointer<uint> pVersion, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcRegisterTask(string path, string xml, uint flags, string sddl, uint logonType, uint cCreds, TASK_USER_CRED[] pCreds, RpcPointer<RpcPointer<string>> pActualPath, RpcPointer<RpcPointer<TASK_XML_ERROR_INFO>> pErrorInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(path is null);
			if (path is not null)
				encoder.WriteWideCharString(path);
			encoder.WriteWideCharString(xml);
			encoder.WriteValue(flags);
			encoder.WriteUniqueReferentId(sddl is null);
			if (sddl is not null)
				encoder.WriteWideCharString(sddl);
			encoder.WriteValue(logonType);
			encoder.WriteValue(cCreds);
			encoder.WriteUniqueReferentId(pCreds is null);
			if (pCreds is not null)
			{
				encoder.WriteArrayHeader(pCreds);
				for (int i = 0; i < pCreds.Length; i++)
				{
					TASK_USER_CRED elem_0 = pCreds[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

			for (int i = 0; i < pCreds.Length; i++)
			{
				TASK_USER_CRED elem_0 = pCreds[i];
				encoder.WriteStructDeferral(elem_0);
			}
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pActualPath.value = decoder.ReadOutUniquePointer<string>(pActualPath.value);
			if (pActualPath.value is not null)
			{
				pActualPath.value.value = decoder.ReadWideCharString();
			}

			pErrorInfo.value = decoder.ReadOutUniquePointer<TASK_XML_ERROR_INFO>(pErrorInfo.value);
			if (pErrorInfo.value is not null)
			{
				pErrorInfo.value.value = decoder.ReadFixedStruct<TASK_XML_ERROR_INFO>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<TASK_XML_ERROR_INFO>(ref pErrorInfo.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcRetrieveTask(string path, string lpcwszLanguagesBuffer, uint pulNumLanguages, RpcPointer<RpcPointer<string>> pXml, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteWideCharString(lpcwszLanguagesBuffer);
			encoder.WriteValue(pulNumLanguages);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pXml.value = decoder.ReadOutUniquePointer<string>(pXml.value);
			if (pXml.value is not null)
			{
				pXml.value.value = decoder.ReadWideCharString();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcCreateFolder(string path, string sddl, uint flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteUniqueReferentId(sddl is null);
			if (sddl is not null)
				encoder.WriteWideCharString(sddl);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcSetSecurity(string path, string sddl, uint flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteWideCharString(sddl);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcGetSecurity(string path, uint securityInformation, RpcPointer<RpcPointer<string>> sddl, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(securityInformation);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			sddl.value = decoder.ReadOutUniquePointer<string>(sddl.value);
			if (sddl.value is not null)
			{
				sddl.value.value = decoder.ReadWideCharString();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcEnumFolders(string path, uint flags, RpcPointer<uint> pStartIndex, uint cRequested, RpcPointer<uint> pcNames, RpcPointer<RpcPointer<RpcPointer<string>[]>> pNames, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			encoder.WriteValue(pStartIndex.value);
			encoder.WriteValue(cRequested);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pStartIndex.value = decoder.ReadUInt32();
			pcNames.value = decoder.ReadUInt32();
			pNames.value = decoder.ReadOutUniquePointer<RpcPointer<string>[]>(pNames.value);
			if (pNames.value is not null)
			{
				pNames.value.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < pNames.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = pNames.value.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					pNames.value.value[i] = elem_0;
				}

				for (int i = 0; i < pNames.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = pNames.value.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					pNames.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcEnumTasks(string path, uint flags, RpcPointer<uint> startIndex, uint cRequested, RpcPointer<uint> pcNames, RpcPointer<RpcPointer<RpcPointer<string>[]>> pNames, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			encoder.WriteValue(startIndex.value);
			encoder.WriteValue(cRequested);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			startIndex.value = decoder.ReadUInt32();
			pcNames.value = decoder.ReadUInt32();
			pNames.value = decoder.ReadOutUniquePointer<RpcPointer<string>[]>(pNames.value);
			if (pNames.value is not null)
			{
				pNames.value.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < pNames.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = pNames.value.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					pNames.value.value[i] = elem_0;
				}

				for (int i = 0; i < pNames.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = pNames.value.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					pNames.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcEnumInstances(string path, uint flags, RpcPointer<uint> pcGuids, RpcPointer<RpcPointer<Guid[]>> pGuids, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(path is null);
			if (path is not null)
				encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pcGuids.value = decoder.ReadUInt32();
			pGuids.value = decoder.ReadOutUniquePointer<Guid[]>(pGuids.value);
			if (pGuids.value is not null)
			{
				pGuids.value.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < pGuids.value.value.Length; i++)
				{
					Guid elem_0 = pGuids.value.value[i];
					elem_0 = decoder.ReadUuid();
					pGuids.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcGetInstanceInfo(Guid guid, RpcPointer<RpcPointer<string>> pPath, RpcPointer<uint> pState, RpcPointer<RpcPointer<string>> pCurrentAction, RpcPointer<RpcPointer<string>> pInfo, RpcPointer<uint> pcGroupInstances, RpcPointer<RpcPointer<Guid[]>> pGroupInstances, RpcPointer<uint> pEnginePID, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(guid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pPath.value = decoder.ReadOutUniquePointer<string>(pPath.value);
			if (pPath.value is not null)
			{
				pPath.value.value = decoder.ReadWideCharString();
			}

			pState.value = decoder.ReadUInt32();
			pCurrentAction.value = decoder.ReadOutUniquePointer<string>(pCurrentAction.value);
			if (pCurrentAction.value is not null)
			{
				pCurrentAction.value.value = decoder.ReadWideCharString();
			}

			pInfo.value = decoder.ReadOutUniquePointer<string>(pInfo.value);
			if (pInfo.value is not null)
			{
				pInfo.value.value = decoder.ReadWideCharString();
			}

			pcGroupInstances.value = decoder.ReadUInt32();
			pGroupInstances.value = decoder.ReadOutUniquePointer<Guid[]>(pGroupInstances.value);
			if (pGroupInstances.value is not null)
			{
				pGroupInstances.value.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < pGroupInstances.value.value.Length; i++)
				{
					Guid elem_0 = pGroupInstances.value.value[i];
					elem_0 = decoder.ReadUuid();
					pGroupInstances.value.value[i] = elem_0;
				}
			}

			pEnginePID.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcStopInstance(Guid guid, uint flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(guid);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcStop(string path, uint flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(path is null);
			if (path is not null)
				encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcRun(string path, uint cArgs, RpcPointer<string>[] pArgs, uint flags, uint sessionId, string user, RpcPointer<Guid> pGuid, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(cArgs);
			encoder.WriteUniqueReferentId(pArgs is null);
			if (pArgs is not null)
			{
				encoder.WriteArrayHeader(pArgs);
				for (int i = 0; i < pArgs.Length; i++)
				{
					RpcPointer<string> elem_0 = pArgs[i];
					encoder.WriteUniquePointer(elem_0);
				}
			}

			for (int i = 0; i < pArgs.Length; i++)
			{
				RpcPointer<string> elem_0 = pArgs[i];
				if (elem_0 is not null)
				{
					encoder.WriteWideCharString(elem_0.value);
				}
			}

			encoder.WriteValue(flags);
			encoder.WriteValue(sessionId);
			encoder.WriteUniqueReferentId(user is null);
			if (user is not null)
				encoder.WriteWideCharString(user);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pGuid.value = decoder.ReadUuid();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcDelete(string path, uint flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcRename(string path, string newName, uint flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteWideCharString(newName);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcScheduledRuntimes(string path, RpcPointer<ms_dtyp.SYSTEMTIME> start, RpcPointer<ms_dtyp.SYSTEMTIME> end, uint flags, uint cRequested, RpcPointer<uint> pcRuntimes, RpcPointer<RpcPointer<ms_dtyp.SYSTEMTIME[]>> pRuntimes, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteUniquePointer(start);
			if (start is not null)
			{
				encoder.WriteFixedStruct(start.value, NdrAlignment._2Byte);
				encoder.WriteStructDeferral(start.value);
			}

			encoder.WriteUniquePointer(end);
			if (end is not null)
			{
				encoder.WriteFixedStruct(end.value, NdrAlignment._2Byte);
				encoder.WriteStructDeferral(end.value);
			}

			encoder.WriteValue(flags);
			encoder.WriteValue(cRequested);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pcRuntimes.value = decoder.ReadUInt32();
			pRuntimes.value = decoder.ReadOutUniquePointer<ms_dtyp.SYSTEMTIME[]>(pRuntimes.value);
			if (pRuntimes.value is not null)
			{
				pRuntimes.value.value = decoder.ReadArrayHeader<ms_dtyp.SYSTEMTIME>();
				for (int i = 0; i < pRuntimes.value.value.Length; i++)
				{
					ms_dtyp.SYSTEMTIME elem_0 = pRuntimes.value.value[i];
					elem_0 = decoder.ReadFixedStruct<ms_dtyp.SYSTEMTIME>(NdrAlignment._2Byte);
					pRuntimes.value.value[i] = elem_0;
				}

				for (int i = 0; i < pRuntimes.value.value.Length; i++)
				{
					ms_dtyp.SYSTEMTIME elem_0 = pRuntimes.value.value[i];
					decoder.ReadStructDeferral<ms_dtyp.SYSTEMTIME>(ref elem_0);
					pRuntimes.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcGetLastRunInfo(string path, RpcPointer<ms_dtyp.SYSTEMTIME> pLastRuntime, RpcPointer<uint> pLastReturnCode, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pLastRuntime.value = decoder.ReadFixedStruct<ms_dtyp.SYSTEMTIME>(NdrAlignment._2Byte);
			decoder.ReadStructDeferral<ms_dtyp.SYSTEMTIME>(ref pLastRuntime.value);
			pLastReturnCode.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcGetTaskInfo(string path, uint flags, RpcPointer<uint> pEnabled, RpcPointer<uint> pState, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pEnabled.value = decoder.ReadUInt32();
			pState.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcGetNumberOfMissedRuns(string path, RpcPointer<uint> pNumberOfMissedRuns, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pNumberOfMissedRuns.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<int> SchRpcEnableTask(string path, uint enabled, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(enabled);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(ITaskSchedulerService);
		private static Guid _interfaceUuid = new Guid("86d35949-83c9-4044-b424-db363231fd0c");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial class ITaskSchedulerServiceStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcHighestVersion(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pVersion = new RpcPointer<uint>();
			var invokeTask = this._obj.SchRpcHighestVersion(pVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pVersion.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcRegisterTask(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			string xml;
			uint flags;
			string sddl;
			uint logonType;
			uint cCreds;
			TASK_USER_CRED[] pCreds;
			RpcPointer<RpcPointer<string>> pActualPath = new RpcPointer<RpcPointer<string>>();
			RpcPointer<RpcPointer<TASK_XML_ERROR_INFO>> pErrorInfo = new RpcPointer<RpcPointer<TASK_XML_ERROR_INFO>>();
			if (decoder.ReadReferentId() == 0)
				path = null;
			else
				path = decoder.ReadWideCharString();
			xml = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				sddl = null;
			else
				sddl = decoder.ReadWideCharString();
			logonType = decoder.ReadUInt32();
			cCreds = decoder.ReadUInt32();
			pCreds = decoder.ReadArrayHeader<TASK_USER_CRED>();
			for (int i = 0; i < pCreds.Length; i++)
			{
				TASK_USER_CRED elem_0 = pCreds[i];
				elem_0 = decoder.ReadFixedStruct<TASK_USER_CRED>(NdrAlignment.NativePtr);
				pCreds[i] = elem_0;
			}

			for (int i = 0; i < pCreds.Length; i++)
			{
				TASK_USER_CRED elem_0 = pCreds[i];
				decoder.ReadStructDeferral<TASK_USER_CRED>(ref elem_0);
				pCreds[i] = elem_0;
			}

			var invokeTask = this._obj.SchRpcRegisterTask(path, xml, flags, sddl, logonType, cCreds, pCreds, pActualPath, pErrorInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pActualPath.value);
			if (pActualPath.value is not null)
			{
				encoder.WriteWideCharString(pActualPath.value.value);
			}

			encoder.WriteUniquePointer(pErrorInfo.value);
			if (pErrorInfo.value is not null)
			{
				encoder.WriteFixedStruct(pErrorInfo.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(pErrorInfo.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcRetrieveTask(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			string lpcwszLanguagesBuffer;
			uint pulNumLanguages;
			RpcPointer<RpcPointer<string>> pXml = new RpcPointer<RpcPointer<string>>();
			path = decoder.ReadWideCharString();
			lpcwszLanguagesBuffer = decoder.ReadWideCharString();
			pulNumLanguages = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcRetrieveTask(path, lpcwszLanguagesBuffer, pulNumLanguages, pXml, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pXml.value);
			if (pXml.value is not null)
			{
				encoder.WriteWideCharString(pXml.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcCreateFolder(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			string sddl;
			uint flags;
			path = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				sddl = null;
			else
				sddl = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcCreateFolder(path, sddl, flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcSetSecurity(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			string sddl;
			uint flags;
			path = decoder.ReadWideCharString();
			sddl = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcSetSecurity(path, sddl, flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcGetSecurity(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint securityInformation;
			RpcPointer<RpcPointer<string>> sddl = new RpcPointer<RpcPointer<string>>();
			path = decoder.ReadWideCharString();
			securityInformation = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcGetSecurity(path, securityInformation, sddl, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(sddl.value);
			if (sddl.value is not null)
			{
				encoder.WriteWideCharString(sddl.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcEnumFolders(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint flags;
			RpcPointer<uint> pStartIndex;
			uint cRequested;
			RpcPointer<uint> pcNames = new RpcPointer<uint>();
			RpcPointer<RpcPointer<RpcPointer<string>[]>> pNames = new RpcPointer<RpcPointer<RpcPointer<string>[]>>();
			path = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			pStartIndex = new RpcPointer<uint>();
			pStartIndex.value = decoder.ReadUInt32();
			cRequested = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcEnumFolders(path, flags, pStartIndex, cRequested, pcNames, pNames, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pStartIndex.value);
			encoder.WriteValue(pcNames.value);
			encoder.WriteUniquePointer(pNames.value);
			if (pNames.value is not null)
			{
				encoder.WriteArrayHeader(pNames.value.value);
				for (int i = 0; i < pNames.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = pNames.value.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < pNames.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = pNames.value.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteWideCharString(elem_0.value);
					}
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcEnumTasks(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint flags;
			RpcPointer<uint> startIndex;
			uint cRequested;
			RpcPointer<uint> pcNames = new RpcPointer<uint>();
			RpcPointer<RpcPointer<RpcPointer<string>[]>> pNames = new RpcPointer<RpcPointer<RpcPointer<string>[]>>();
			path = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			startIndex = new RpcPointer<uint>();
			startIndex.value = decoder.ReadUInt32();
			cRequested = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcEnumTasks(path, flags, startIndex, cRequested, pcNames, pNames, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(startIndex.value);
			encoder.WriteValue(pcNames.value);
			encoder.WriteUniquePointer(pNames.value);
			if (pNames.value is not null)
			{
				encoder.WriteArrayHeader(pNames.value.value);
				for (int i = 0; i < pNames.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = pNames.value.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < pNames.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = pNames.value.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteWideCharString(elem_0.value);
					}
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcEnumInstances(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint flags;
			RpcPointer<uint> pcGuids = new RpcPointer<uint>();
			RpcPointer<RpcPointer<Guid[]>> pGuids = new RpcPointer<RpcPointer<Guid[]>>();
			if (decoder.ReadReferentId() == 0)
				path = null;
			else
				path = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcEnumInstances(path, flags, pcGuids, pGuids, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pcGuids.value);
			encoder.WriteUniquePointer(pGuids.value);
			if (pGuids.value is not null)
			{
				encoder.WriteArrayHeader(pGuids.value.value);
				for (int i = 0; i < pGuids.value.value.Length; i++)
				{
					Guid elem_0 = pGuids.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcGetInstanceInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid guid;
			RpcPointer<RpcPointer<string>> pPath = new RpcPointer<RpcPointer<string>>();
			RpcPointer<uint> pState = new RpcPointer<uint>();
			RpcPointer<RpcPointer<string>> pCurrentAction = new RpcPointer<RpcPointer<string>>();
			RpcPointer<RpcPointer<string>> pInfo = new RpcPointer<RpcPointer<string>>();
			RpcPointer<uint> pcGroupInstances = new RpcPointer<uint>();
			RpcPointer<RpcPointer<Guid[]>> pGroupInstances = new RpcPointer<RpcPointer<Guid[]>>();
			RpcPointer<uint> pEnginePID = new RpcPointer<uint>();
			guid = decoder.ReadUuid();
			var invokeTask = this._obj.SchRpcGetInstanceInfo(guid, pPath, pState, pCurrentAction, pInfo, pcGroupInstances, pGroupInstances, pEnginePID, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pPath.value);
			if (pPath.value is not null)
			{
				encoder.WriteWideCharString(pPath.value.value);
			}

			encoder.WriteValue(pState.value);
			encoder.WriteUniquePointer(pCurrentAction.value);
			if (pCurrentAction.value is not null)
			{
				encoder.WriteWideCharString(pCurrentAction.value.value);
			}

			encoder.WriteUniquePointer(pInfo.value);
			if (pInfo.value is not null)
			{
				encoder.WriteWideCharString(pInfo.value.value);
			}

			encoder.WriteValue(pcGroupInstances.value);
			encoder.WriteUniquePointer(pGroupInstances.value);
			if (pGroupInstances.value is not null)
			{
				encoder.WriteArrayHeader(pGroupInstances.value.value);
				for (int i = 0; i < pGroupInstances.value.value.Length; i++)
				{
					Guid elem_0 = pGroupInstances.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(pEnginePID.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcStopInstance(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid guid;
			uint flags;
			guid = decoder.ReadUuid();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcStopInstance(guid, flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcStop(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint flags;
			if (decoder.ReadReferentId() == 0)
				path = null;
			else
				path = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcStop(path, flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcRun(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint cArgs;
			RpcPointer<string>[] pArgs;
			uint flags;
			uint sessionId;
			string user;
			RpcPointer<Guid> pGuid = new RpcPointer<Guid>();
			path = decoder.ReadWideCharString();
			cArgs = decoder.ReadUInt32();
			pArgs = decoder.ReadArrayHeader<RpcPointer<string>>();
			for (int i = 0; i < pArgs.Length; i++)
			{
				RpcPointer<string> elem_0 = pArgs[i];
				elem_0 = decoder.ReadUniquePointer<string>();
				pArgs[i] = elem_0;
			}

			for (int i = 0; i < pArgs.Length; i++)
			{
				RpcPointer<string> elem_0 = pArgs[i];
				if (elem_0 is not null)
				{
					elem_0.value = decoder.ReadWideCharString();
				}

				pArgs[i] = elem_0;
			}

			flags = decoder.ReadUInt32();
			sessionId = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				user = null;
			else
				user = decoder.ReadWideCharString();
			var invokeTask = this._obj.SchRpcRun(path, cArgs, pArgs, flags, sessionId, user, pGuid, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pGuid.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcDelete(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint flags;
			path = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcDelete(path, flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcRename(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			string newName;
			uint flags;
			path = decoder.ReadWideCharString();
			newName = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcRename(path, newName, flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcScheduledRuntimes(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			RpcPointer<ms_dtyp.SYSTEMTIME> start;
			RpcPointer<ms_dtyp.SYSTEMTIME> end;
			uint flags;
			uint cRequested;
			RpcPointer<uint> pcRuntimes = new RpcPointer<uint>();
			RpcPointer<RpcPointer<ms_dtyp.SYSTEMTIME[]>> pRuntimes = new RpcPointer<RpcPointer<ms_dtyp.SYSTEMTIME[]>>();
			path = decoder.ReadWideCharString();
			start = decoder.ReadUniquePointer<ms_dtyp.SYSTEMTIME>();
			if (start is not null)
			{
				start.value = decoder.ReadFixedStruct<ms_dtyp.SYSTEMTIME>(NdrAlignment._2Byte);
				decoder.ReadStructDeferral<ms_dtyp.SYSTEMTIME>(ref start.value);
			}

			end = decoder.ReadUniquePointer<ms_dtyp.SYSTEMTIME>();
			if (end is not null)
			{
				end.value = decoder.ReadFixedStruct<ms_dtyp.SYSTEMTIME>(NdrAlignment._2Byte);
				decoder.ReadStructDeferral<ms_dtyp.SYSTEMTIME>(ref end.value);
			}

			flags = decoder.ReadUInt32();
			cRequested = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcScheduledRuntimes(path, start, end, flags, cRequested, pcRuntimes, pRuntimes, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pcRuntimes.value);
			encoder.WriteUniquePointer(pRuntimes.value);
			if (pRuntimes.value is not null)
			{
				encoder.WriteArrayHeader(pRuntimes.value.value);
				for (int i = 0; i < pRuntimes.value.value.Length; i++)
				{
					ms_dtyp.SYSTEMTIME elem_0 = pRuntimes.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._2Byte);
				}

				for (int i = 0; i < pRuntimes.value.value.Length; i++)
				{
					ms_dtyp.SYSTEMTIME elem_0 = pRuntimes.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcGetLastRunInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			RpcPointer<ms_dtyp.SYSTEMTIME> pLastRuntime = new RpcPointer<ms_dtyp.SYSTEMTIME>();
			RpcPointer<uint> pLastReturnCode = new RpcPointer<uint>();
			path = decoder.ReadWideCharString();
			var invokeTask = this._obj.SchRpcGetLastRunInfo(path, pLastRuntime, pLastReturnCode, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pLastRuntime.value, NdrAlignment._2Byte);
			encoder.WriteStructDeferral(pLastRuntime.value);
			encoder.WriteValue(pLastReturnCode.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcGetTaskInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint flags;
			RpcPointer<uint> pEnabled = new RpcPointer<uint>();
			RpcPointer<uint> pState = new RpcPointer<uint>();
			path = decoder.ReadWideCharString();
			flags = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcGetTaskInfo(path, flags, pEnabled, pState, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pEnabled.value);
			encoder.WriteValue(pState.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcGetNumberOfMissedRuns(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			RpcPointer<uint> pNumberOfMissedRuns = new RpcPointer<uint>();
			path = decoder.ReadWideCharString();
			var invokeTask = this._obj.SchRpcGetNumberOfMissedRuns(path, pNumberOfMissedRuns, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pNumberOfMissedRuns.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_SchRpcEnableTask(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string path;
			uint enabled;
			path = decoder.ReadWideCharString();
			enabled = decoder.ReadUInt32();
			var invokeTask = this._obj.SchRpcEnableTask(path, enabled, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("86d35949-83c9-4044-b424-db363231fd0c");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private ITaskSchedulerService _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public ITaskSchedulerServiceStub(ITaskSchedulerService obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_SchRpcHighestVersion, this.Invoke_SchRpcRegisterTask, this.Invoke_SchRpcRetrieveTask, this.Invoke_SchRpcCreateFolder, this.Invoke_SchRpcSetSecurity, this.Invoke_SchRpcGetSecurity, this.Invoke_SchRpcEnumFolders, this.Invoke_SchRpcEnumTasks, this.Invoke_SchRpcEnumInstances, this.Invoke_SchRpcGetInstanceInfo, this.Invoke_SchRpcStopInstance, this.Invoke_SchRpcStop, this.Invoke_SchRpcRun, this.Invoke_SchRpcDelete, this.Invoke_SchRpcRename, this.Invoke_SchRpcScheduledRuntimes, this.Invoke_SchRpcGetLastRunInfo, this.Invoke_SchRpcGetTaskInfo, this.Invoke_SchRpcGetNumberOfMissedRuns, this.Invoke_SchRpcEnableTask};
		}
	}
}