#pragma warning disable

namespace ms_tsch
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;
	using Titanis.DceRpc.Client;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct TASK_XML_ERROR_INFO : IRpcFixedStruct
	{
		public uint line;
		public uint column;
		public RpcPointer<string> node;
		public RpcPointer<string> value;

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.line);
			encoder.WriteValue(this.column);
			encoder.WriteUniquePointer(this.node);
			encoder.WriteUniquePointer(this.value);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.line = decoder.ReadUInt32();
			this.column = decoder.ReadUInt32();
			this.node = decoder.ReadUniquePointer<string>();
			this.value = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct TASK_USER_CRED : IRpcFixedStruct
	{
		public RpcPointer<string> userId;
		public RpcPointer<string> password;
		public uint flags;

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.userId);
			encoder.WriteUniquePointer(this.password);
			encoder.WriteValue(this.flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.userId = decoder.ReadUniquePointer<string>();
			this.password = decoder.ReadUniquePointer<string>();
			this.flags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SYSTEMTIME : IRpcFixedStruct
	{
		public ushort wYear;
		public ushort wMonth;
		public ushort wDayOfWeek;
		public ushort wDay;
		public ushort wHour;
		public ushort wMinute;
		public ushort wSecond;
		public ushort wMilliseconds;

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wYear);
			encoder.WriteValue(this.wMonth);
			encoder.WriteValue(this.wDayOfWeek);
			encoder.WriteValue(this.wDay);
			encoder.WriteValue(this.wHour);
			encoder.WriteValue(this.wMinute);
			encoder.WriteValue(this.wSecond);
			encoder.WriteValue(this.wMilliseconds);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wYear = decoder.ReadUInt16();
			this.wMonth = decoder.ReadUInt16();
			this.wDayOfWeek = decoder.ReadUInt16();
			this.wDay = decoder.ReadUInt16();
			this.wHour = decoder.ReadUInt16();
			this.wMinute = decoder.ReadUInt16();
			this.wSecond = decoder.ReadUInt16();
			this.wMilliseconds = decoder.ReadUInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	// [MS-TSCH] § 2.3.13
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	[GuidAttribute("86d35949-83c9-4044-b424-db363231fd0c")]
	[RpcVersionAttribute(1, 0)]
	public interface ITaskSchedulerService
	{
		// Opnum 0
		Task<int> SchRpcHighestVersion(RpcPointer<uint> pVersion, CancellationToken cancellationToken);
		// Opnum 1
		Task<int> SchRpcRegisterTask(string path, string xml, uint flags, string sddl, uint logonType, uint cCreds, TASK_USER_CRED[] pCreds, RpcPointer<string> pActualPath, RpcPointer<RpcPointer<TASK_XML_ERROR_INFO>> pErrorInfo, CancellationToken cancellationToken);
		// Opnum 2
		Task<int> SchRpcRetrieveTask(string path, string lpcwszLanguagesBuffer, RpcPointer<uint> pulNumLanguages, RpcPointer<string> pXml, CancellationToken cancellationToken);
		// Opnum 3
		Task<int> SchRpcCreateFolder(string path, string sddl, uint flags, CancellationToken cancellationToken);
		// Opnum 4
		Task<int> SchRpcSetSecurity(string path, string sddl, uint flags, CancellationToken cancellationToken);
		// Opnum 5
		Task<int> SchRpcGetSecurity(string path, uint securityInformation, RpcPointer<string> pSddl, CancellationToken cancellationToken);
		// Opnum 6
		Task<int> SchRpcEnumFolders(string path, uint flags, RpcPointer<uint> pStartIndex, uint cRequested, RpcPointer<uint> pcNames, RpcPointer<RpcPointer<string>[]> pNames, CancellationToken cancellationToken);
		// Opnum 7
		Task<int> SchRpcEnumTasks(string path, uint flags, RpcPointer<uint> pStartIndex, uint cRequested, RpcPointer<uint> pcNames, RpcPointer<RpcPointer<string>[]> pNames, CancellationToken cancellationToken);
		// Opnum 8
		Task<int> SchRpcEnumInstances(string path, uint flags, RpcPointer<uint> pcGuids, RpcPointer<Guid[]> pGuids, CancellationToken cancellationToken);
		// Opnum 9
		Task<int> SchRpcGetInstanceInfo(Guid guid, RpcPointer<string> pPath, RpcPointer<uint> pState, RpcPointer<string> pCurrentAction, RpcPointer<string> pInfo, RpcPointer<uint> pcGroupInstances, RpcPointer<Guid[]> pGroupInstances, RpcPointer<uint> pEnginePID, CancellationToken cancellationToken);
		// Opnum 10
		Task<int> SchRpcStopInstance(Guid guid, uint flags, CancellationToken cancellationToken);
		// Opnum 11
		Task<int> SchRpcStop(string path, uint flags, CancellationToken cancellationToken);
		// Opnum 12
		Task<int> SchRpcRun(string path, uint cArgs, RpcPointer<string>[] pArgs, uint flags, uint sessionId, RpcPointer<string> user, RpcPointer<Guid> pGuid, CancellationToken cancellationToken);
		// Opnum 13
		Task<int> SchRpcDelete(string path, uint flags, CancellationToken cancellationToken);
		// Opnum 14
		Task<int> SchRpcRename(string path, string newName, uint flags, CancellationToken cancellationToken);
		// Opnum 15
		Task<int> SchRpcScheduledRuntimes(string path, RpcPointer<SYSTEMTIME> pStart, RpcPointer<SYSTEMTIME> pEnd, uint flags, uint cRequested, RpcPointer<uint> pcRuntimes, RpcPointer<SYSTEMTIME[]> pRuntimes, CancellationToken cancellationToken);
		// Opnum 16
		Task<int> SchRpcGetLastRunInfo(string path, RpcPointer<SYSTEMTIME> pLastRuntime, RpcPointer<uint> pLastReturnCode, CancellationToken cancellationToken);
		// Opnum 17
		Task<int> SchRpcGetTaskInfo(string path, uint flags, RpcPointer<uint> pEnabled, RpcPointer<uint> pState, CancellationToken cancellationToken);
		// Opnum 18
		Task<int> SchRpcGetNumberOfMissedRuns(string path, RpcPointer<uint> pNumberOfMissedRuns, CancellationToken cancellationToken);
		// Opnum 19
		Task<int> SchRpcEnableTask(string path, uint enabled, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	[IidAttribute("86d35949-83c9-4044-b424-db363231fd0c")]
	public partial class ITaskSchedulerServiceClientProxy : RpcClientProxy, ITaskSchedulerService, IRpcClientProxy
	{
		/// <inheritdoc/>
		public override Type InterfaceType => typeof(ITaskSchedulerService);
		private static Guid _interfaceUuid = new Guid("86d35949-83c9-4044-b424-db363231fd0c");
		public override Guid InterfaceUuid
		{
			get { return _interfaceUuid; }
		}
		public override RpcVersion InterfaceVersion
		{
			get { return new RpcVersion(1, 0); }
		}

		// Opnum 0: SchRpcHighestVersion
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcHighestVersion(RpcPointer<uint> pVersion, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 1: SchRpcRegisterTask
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcRegisterTask(string path, string xml, uint flags, string sddl, uint logonType, uint cCreds, TASK_USER_CRED[] pCreds, RpcPointer<string> pActualPath, RpcPointer<RpcPointer<TASK_XML_ERROR_INFO>> pErrorInfo, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(path == null);
			if (path != null)
			{
				encoder.WriteWideCharString(path);
			}
			encoder.WriteWideCharString(xml);
			encoder.WriteValue(flags);
			encoder.WriteUniqueReferentId(sddl == null);
			if (sddl != null)
			{
				encoder.WriteWideCharString(sddl);
			}
			encoder.WriteValue(logonType);
			encoder.WriteValue(cCreds);
			encoder.WriteUniqueReferentId(pCreds == null || pCreds.Length == 0);
			if (pCreds != null && pCreds.Length > 0)
			{
				encoder.WriteArrayHeader(pCreds);
				for (int i = 0; i < pCreds.Length; i++)
				{
					encoder.WriteFixedStruct(pCreds[i], NdrAlignment.NativePtr);
				}
				for (int i = 0; i < pCreds.Length; i++)
				{
					encoder.WriteStructDeferral(pCreds[i]);
				}
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pActualPath.value = null;
			var hasActualPath = decoder.ReadUniquePointer<string>();
			if (hasActualPath is not null)
			{
				pActualPath.value = decoder.ReadWideCharString();
			}
			pErrorInfo.value = decoder.ReadUniquePointer<TASK_XML_ERROR_INFO>();
			if (pErrorInfo.value is not null)
			{
				pErrorInfo.value.value = decoder.ReadFixedStruct<TASK_XML_ERROR_INFO>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<TASK_XML_ERROR_INFO>(ref pErrorInfo.value.value);
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 2: SchRpcRetrieveTask
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcRetrieveTask(string path, string lpcwszLanguagesBuffer, RpcPointer<uint> pulNumLanguages, RpcPointer<string> pXml, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteWideCharString(lpcwszLanguagesBuffer);
			encoder.WriteValue(pulNumLanguages.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pXml.value = null;
			var hasXml = decoder.ReadUniquePointer<string>();
			if (hasXml is not null)
			{
				pXml.value = decoder.ReadWideCharString();
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 3: SchRpcCreateFolder
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcCreateFolder(string path, string sddl, uint flags, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteUniqueReferentId(sddl == null);
			if (sddl != null)
			{
				encoder.WriteWideCharString(sddl);
			}
			encoder.WriteValue(flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 4: SchRpcSetSecurity
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcSetSecurity(string path, string sddl, uint flags, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteWideCharString(sddl);
			encoder.WriteValue(flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 5: SchRpcGetSecurity
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcGetSecurity(string path, uint securityInformation, RpcPointer<string> pSddl, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(securityInformation);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pSddl.value = null;
			var hasSddl = decoder.ReadUniquePointer<string>();
			if (hasSddl is not null)
			{
				pSddl.value = decoder.ReadWideCharString();
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 6: SchRpcEnumFolders
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcEnumFolders(string path, uint flags, RpcPointer<uint> pStartIndex, uint cRequested, RpcPointer<uint> pcNames, RpcPointer<RpcPointer<string>[]> pNames, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			encoder.WriteValue(pStartIndex.value);
			encoder.WriteValue(cRequested);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pStartIndex.value = decoder.ReadUInt32();
			pcNames.value = decoder.ReadUInt32();
			pNames.value = null;
			var hasNames = decoder.ReadUniquePointer<RpcPointer<string>[]>();
			if (hasNames is not null)
			{
				pNames.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < pNames.value.Length; i++)
				{
					pNames.value[i] = decoder.ReadUniquePointer<string>();
				}
				for (int i = 0; i < pNames.value.Length; i++)
				{
					if (pNames.value[i] is not null)
					{
						pNames.value[i].value = decoder.ReadWideCharString();
					}
				}
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 7: SchRpcEnumTasks
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcEnumTasks(string path, uint flags, RpcPointer<uint> pStartIndex, uint cRequested, RpcPointer<uint> pcNames, RpcPointer<RpcPointer<string>[]> pNames, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			encoder.WriteValue(pStartIndex.value);
			encoder.WriteValue(cRequested);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pStartIndex.value = decoder.ReadUInt32();
			pcNames.value = decoder.ReadUInt32();
			pNames.value = null;
			var hasNames = decoder.ReadUniquePointer<RpcPointer<string>[]>();
			if (hasNames is not null)
			{
				pNames.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < pNames.value.Length; i++)
				{
					pNames.value[i] = decoder.ReadUniquePointer<string>();
				}
				for (int i = 0; i < pNames.value.Length; i++)
				{
					if (pNames.value[i] is not null)
					{
						pNames.value[i].value = decoder.ReadWideCharString();
					}
				}
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 8: SchRpcEnumInstances
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcEnumInstances(string path, uint flags, RpcPointer<uint> pcGuids, RpcPointer<Guid[]> pGuids, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(path == null);
			if (path != null)
			{
				encoder.WriteWideCharString(path);
			}
			encoder.WriteValue(flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pcGuids.value = decoder.ReadUInt32();
			pGuids.value = null;
			var hasGuids = decoder.ReadUniquePointer<Guid[]>();
			if (hasGuids is not null)
			{
				pGuids.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < pGuids.value.Length; i++)
				{
					pGuids.value[i] = decoder.ReadUuid();
				}
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 9: SchRpcGetInstanceInfo
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcGetInstanceInfo(Guid guid, RpcPointer<string> pPath, RpcPointer<uint> pState, RpcPointer<string> pCurrentAction, RpcPointer<string> pInfo, RpcPointer<uint> pcGroupInstances, RpcPointer<Guid[]> pGroupInstances, RpcPointer<uint> pEnginePID, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(guid);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pPath.value = null;
			var hasPath = decoder.ReadUniquePointer<string>();
			if (hasPath is not null)
			{
				pPath.value = decoder.ReadWideCharString();
			}
			pState.value = decoder.ReadUInt32();
			pCurrentAction.value = null;
			var hasAction = decoder.ReadUniquePointer<string>();
			if (hasAction is not null)
			{
				pCurrentAction.value = decoder.ReadWideCharString();
			}
			pInfo.value = null;
			var hasInfo = decoder.ReadUniquePointer<string>();
			if (hasInfo is not null)
			{
				pInfo.value = decoder.ReadWideCharString();
			}
			pcGroupInstances.value = decoder.ReadUInt32();
			pGroupInstances.value = null;
			var hasGroupGuids = decoder.ReadUniquePointer<Guid[]>();
			if (hasGroupGuids is not null)
			{
				pGroupInstances.value = decoder.ReadArrayHeader<Guid>();
				for (int i = 0; i < pGroupInstances.value.Length; i++)
				{
					pGroupInstances.value[i] = decoder.ReadUuid();
				}
			}
			pEnginePID.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 10: SchRpcStopInstance
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcStopInstance(Guid guid, uint flags, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(guid);
			encoder.WriteValue(flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 11: SchRpcStop
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcStop(string path, uint flags, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(path == null);
			if (path != null)
			{
				encoder.WriteWideCharString(path);
			}
			encoder.WriteValue(flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 12: SchRpcRun
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcRun(string path, uint cArgs, RpcPointer<string>[] pArgs, uint flags, uint sessionId, RpcPointer<string> user, RpcPointer<Guid> pGuid, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(cArgs);
			encoder.WriteUniqueReferentId(pArgs == null || pArgs.Length == 0);
			if (pArgs != null && pArgs.Length > 0)
			{
				encoder.WriteArrayHeader(pArgs);
				for (int i = 0; i < pArgs.Length; i++)
				{
					encoder.WriteUniquePointer(pArgs[i]);
				}
				for (int i = 0; i < pArgs.Length; i++)
				{
					if (pArgs[i] is not null)
					{
						encoder.WriteWideCharString(pArgs[i].value);
					}
				}
			}
			encoder.WriteValue(flags);
			encoder.WriteValue(sessionId);
			encoder.WriteUniqueReferentId(user?.value == null);
			if (user?.value != null)
			{
				encoder.WriteWideCharString(user.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pGuid.value = decoder.ReadUuid();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 13: SchRpcDelete
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcDelete(string path, uint flags, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 14: SchRpcRename
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcRename(string path, string newName, uint flags, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteWideCharString(newName);
			encoder.WriteValue(flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 15: SchRpcScheduledRuntimes
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcScheduledRuntimes(string path, RpcPointer<SYSTEMTIME> pStart, RpcPointer<SYSTEMTIME> pEnd, uint flags, uint cRequested, RpcPointer<uint> pcRuntimes, RpcPointer<SYSTEMTIME[]> pRuntimes, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteUniquePointer(pStart);
			if (pStart is not null)
			{
				encoder.WriteFixedStruct(pStart.value, NdrAlignment._2Byte);
			}
			encoder.WriteUniquePointer(pEnd);
			if (pEnd is not null)
			{
				encoder.WriteFixedStruct(pEnd.value, NdrAlignment._2Byte);
			}
			encoder.WriteValue(flags);
			encoder.WriteValue(cRequested);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pcRuntimes.value = decoder.ReadUInt32();
			pRuntimes.value = null;
			var hasRuntimes = decoder.ReadUniquePointer<SYSTEMTIME[]>();
			if (hasRuntimes is not null)
			{
				pRuntimes.value = decoder.ReadArrayHeader<SYSTEMTIME>();
				for (int i = 0; i < pRuntimes.value.Length; i++)
				{
					pRuntimes.value[i] = decoder.ReadFixedStruct<SYSTEMTIME>(NdrAlignment._2Byte);
				}
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 16: SchRpcGetLastRunInfo
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcGetLastRunInfo(string path, RpcPointer<SYSTEMTIME> pLastRuntime, RpcPointer<uint> pLastReturnCode, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pLastRuntime.value = decoder.ReadFixedStruct<SYSTEMTIME>(NdrAlignment._2Byte);
			pLastReturnCode.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 17: SchRpcGetTaskInfo
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcGetTaskInfo(string path, uint flags, RpcPointer<uint> pEnabled, RpcPointer<uint> pState, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pEnabled.value = decoder.ReadUInt32();
			pState.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 18: SchRpcGetNumberOfMissedRuns
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcGetNumberOfMissedRuns(string path, RpcPointer<uint> pNumberOfMissedRuns, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			pNumberOfMissedRuns.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		// Opnum 19: SchRpcEnableTask
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public virtual async Task<int> SchRpcEnableTask(string path, uint enabled, CancellationToken cancellationToken)
		{
			IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(path);
			encoder.WriteValue(enabled);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
}
