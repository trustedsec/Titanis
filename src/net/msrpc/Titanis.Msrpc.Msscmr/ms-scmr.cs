namespace ms_scmr
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct STRING_PTRSA : IRpcFixedStruct
	{
		public RpcPointer<string> StringPtr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.StringPtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.StringPtr = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.StringPtr is not null)
			{
				encoder.WriteUnsignedCharString(this.StringPtr.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.StringPtr is not null)
			{
				this.StringPtr.value = decoder.ReadUnsignedCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct STRING_PTRSW : IRpcFixedStruct
	{
		public RpcPointer<string> StringPtr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.StringPtr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.StringPtr = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.StringPtr is not null)
			{
				encoder.WriteWideCharString(this.StringPtr.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.StringPtr is not null)
			{
				this.StringPtr.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_STATUS : IRpcFixedStruct
	{
		public uint dwServiceType;
		public uint dwCurrentState;
		public uint dwControlsAccepted;
		public uint dwWin32ExitCode;
		public uint dwServiceSpecificExitCode;
		public uint dwCheckPoint;
		public uint dwWaitHint;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwServiceType);
			encoder.WriteValue(this.dwCurrentState);
			encoder.WriteValue(this.dwControlsAccepted);
			encoder.WriteValue(this.dwWin32ExitCode);
			encoder.WriteValue(this.dwServiceSpecificExitCode);
			encoder.WriteValue(this.dwCheckPoint);
			encoder.WriteValue(this.dwWaitHint);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwServiceType = decoder.ReadUInt32();
			this.dwCurrentState = decoder.ReadUInt32();
			this.dwControlsAccepted = decoder.ReadUInt32();
			this.dwWin32ExitCode = decoder.ReadUInt32();
			this.dwServiceSpecificExitCode = decoder.ReadUInt32();
			this.dwCheckPoint = decoder.ReadUInt32();
			this.dwWaitHint = decoder.ReadUInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_STATUS_PROCESS : IRpcFixedStruct
	{
		public uint dwServiceType;
		public uint dwCurrentState;
		public uint dwControlsAccepted;
		public uint dwWin32ExitCode;
		public uint dwServiceSpecificExitCode;
		public uint dwCheckPoint;
		public uint dwWaitHint;
		public uint dwProcessId;
		public uint dwServiceFlags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwServiceType);
			encoder.WriteValue(this.dwCurrentState);
			encoder.WriteValue(this.dwControlsAccepted);
			encoder.WriteValue(this.dwWin32ExitCode);
			encoder.WriteValue(this.dwServiceSpecificExitCode);
			encoder.WriteValue(this.dwCheckPoint);
			encoder.WriteValue(this.dwWaitHint);
			encoder.WriteValue(this.dwProcessId);
			encoder.WriteValue(this.dwServiceFlags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwServiceType = decoder.ReadUInt32();
			this.dwCurrentState = decoder.ReadUInt32();
			this.dwControlsAccepted = decoder.ReadUInt32();
			this.dwWin32ExitCode = decoder.ReadUInt32();
			this.dwServiceSpecificExitCode = decoder.ReadUInt32();
			this.dwCheckPoint = decoder.ReadUInt32();
			this.dwWaitHint = decoder.ReadUInt32();
			this.dwProcessId = decoder.ReadUInt32();
			this.dwServiceFlags = decoder.ReadUInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct QUERY_SERVICE_CONFIGW : IRpcFixedStruct
	{
		public uint dwServiceType;
		public uint dwStartType;
		public uint dwErrorControl;
		public RpcPointer<string> lpBinaryPathName;
		public RpcPointer<string> lpLoadOrderGroup;
		public uint dwTagId;
		public RpcPointer<string> lpDependencies;
		public RpcPointer<string> lpServiceStartName;
		public RpcPointer<string> lpDisplayName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwServiceType);
			encoder.WriteValue(this.dwStartType);
			encoder.WriteValue(this.dwErrorControl);
			encoder.WriteUniquePointer(this.lpBinaryPathName);
			encoder.WriteUniquePointer(this.lpLoadOrderGroup);
			encoder.WriteValue(this.dwTagId);
			encoder.WriteUniquePointer(this.lpDependencies);
			encoder.WriteUniquePointer(this.lpServiceStartName);
			encoder.WriteUniquePointer(this.lpDisplayName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwServiceType = decoder.ReadUInt32();
			this.dwStartType = decoder.ReadUInt32();
			this.dwErrorControl = decoder.ReadUInt32();
			this.lpBinaryPathName = decoder.ReadUniquePointer<string>();
			this.lpLoadOrderGroup = decoder.ReadUniquePointer<string>();
			this.dwTagId = decoder.ReadUInt32();
			this.lpDependencies = decoder.ReadUniquePointer<string>();
			this.lpServiceStartName = decoder.ReadUniquePointer<string>();
			this.lpDisplayName = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpBinaryPathName is not null)
			{
				encoder.WriteWideCharString(this.lpBinaryPathName.value);
			}

			if (this.lpLoadOrderGroup is not null)
			{
				encoder.WriteWideCharString(this.lpLoadOrderGroup.value);
			}

			if (this.lpDependencies is not null)
			{
				encoder.WriteWideCharString(this.lpDependencies.value);
			}

			if (this.lpServiceStartName is not null)
			{
				encoder.WriteWideCharString(this.lpServiceStartName.value);
			}

			if (this.lpDisplayName is not null)
			{
				encoder.WriteWideCharString(this.lpDisplayName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpBinaryPathName is not null)
			{
				this.lpBinaryPathName.value = decoder.ReadWideCharString();
			}

			if (this.lpLoadOrderGroup is not null)
			{
				this.lpLoadOrderGroup.value = decoder.ReadWideCharString();
			}

			if (this.lpDependencies is not null)
			{
				this.lpDependencies.value = decoder.ReadWideCharString();
			}

			if (this.lpServiceStartName is not null)
			{
				this.lpServiceStartName.value = decoder.ReadWideCharString();
			}

			if (this.lpDisplayName is not null)
			{
				this.lpDisplayName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct QUERY_SERVICE_LOCK_STATUSW : IRpcFixedStruct
	{
		public uint fIsLocked;
		public RpcPointer<string> lpLockOwner;
		public uint dwLockDuration;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.fIsLocked);
			encoder.WriteUniquePointer(this.lpLockOwner);
			encoder.WriteValue(this.dwLockDuration);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.fIsLocked = decoder.ReadUInt32();
			this.lpLockOwner = decoder.ReadUniquePointer<string>();
			this.dwLockDuration = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpLockOwner is not null)
			{
				encoder.WriteWideCharString(this.lpLockOwner.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpLockOwner is not null)
			{
				this.lpLockOwner.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct QUERY_SERVICE_CONFIGA : IRpcFixedStruct
	{
		public uint dwServiceType;
		public uint dwStartType;
		public uint dwErrorControl;
		public RpcPointer<string> lpBinaryPathName;
		public RpcPointer<string> lpLoadOrderGroup;
		public uint dwTagId;
		public RpcPointer<string> lpDependencies;
		public RpcPointer<string> lpServiceStartName;
		public RpcPointer<string> lpDisplayName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwServiceType);
			encoder.WriteValue(this.dwStartType);
			encoder.WriteValue(this.dwErrorControl);
			encoder.WriteUniquePointer(this.lpBinaryPathName);
			encoder.WriteUniquePointer(this.lpLoadOrderGroup);
			encoder.WriteValue(this.dwTagId);
			encoder.WriteUniquePointer(this.lpDependencies);
			encoder.WriteUniquePointer(this.lpServiceStartName);
			encoder.WriteUniquePointer(this.lpDisplayName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwServiceType = decoder.ReadUInt32();
			this.dwStartType = decoder.ReadUInt32();
			this.dwErrorControl = decoder.ReadUInt32();
			this.lpBinaryPathName = decoder.ReadUniquePointer<string>();
			this.lpLoadOrderGroup = decoder.ReadUniquePointer<string>();
			this.dwTagId = decoder.ReadUInt32();
			this.lpDependencies = decoder.ReadUniquePointer<string>();
			this.lpServiceStartName = decoder.ReadUniquePointer<string>();
			this.lpDisplayName = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpBinaryPathName is not null)
			{
				encoder.WriteUnsignedCharString(this.lpBinaryPathName.value);
			}

			if (this.lpLoadOrderGroup is not null)
			{
				encoder.WriteUnsignedCharString(this.lpLoadOrderGroup.value);
			}

			if (this.lpDependencies is not null)
			{
				encoder.WriteUnsignedCharString(this.lpDependencies.value);
			}

			if (this.lpServiceStartName is not null)
			{
				encoder.WriteUnsignedCharString(this.lpServiceStartName.value);
			}

			if (this.lpDisplayName is not null)
			{
				encoder.WriteUnsignedCharString(this.lpDisplayName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpBinaryPathName is not null)
			{
				this.lpBinaryPathName.value = decoder.ReadUnsignedCharString();
			}

			if (this.lpLoadOrderGroup is not null)
			{
				this.lpLoadOrderGroup.value = decoder.ReadUnsignedCharString();
			}

			if (this.lpDependencies is not null)
			{
				this.lpDependencies.value = decoder.ReadUnsignedCharString();
			}

			if (this.lpServiceStartName is not null)
			{
				this.lpServiceStartName.value = decoder.ReadUnsignedCharString();
			}

			if (this.lpDisplayName is not null)
			{
				this.lpDisplayName.value = decoder.ReadUnsignedCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct QUERY_SERVICE_LOCK_STATUSA : IRpcFixedStruct
	{
		public uint fIsLocked;
		public RpcPointer<string> lpLockOwner;
		public uint dwLockDuration;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.fIsLocked);
			encoder.WriteUniquePointer(this.lpLockOwner);
			encoder.WriteValue(this.dwLockDuration);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.fIsLocked = decoder.ReadUInt32();
			this.lpLockOwner = decoder.ReadUniquePointer<string>();
			this.dwLockDuration = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpLockOwner is not null)
			{
				encoder.WriteUnsignedCharString(this.lpLockOwner.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpLockOwner is not null)
			{
				this.lpLockOwner.value = decoder.ReadUnsignedCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_DESCRIPTIONA : IRpcFixedStruct
	{
		public RpcPointer<string> lpDescription;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.lpDescription);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.lpDescription = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpDescription is not null)
			{
				encoder.WriteUnsignedCharString(this.lpDescription.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpDescription is not null)
			{
				this.lpDescription.value = decoder.ReadUnsignedCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum SC_ACTION_TYPE : int
	{
		SC_ACTION_NONE = 0,
		SC_ACTION_RESTART = 1,
		SC_ACTION_REBOOT = 2,
		SC_ACTION_RUN_COMMAND = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SC_ACTION : IRpcFixedStruct
	{
		public SC_ACTION_TYPE Type;
		public uint Delay;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue((int)this.Type);
			encoder.WriteValue(this.Delay);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Type = (SC_ACTION_TYPE)decoder.ReadInt32();
			this.Delay = decoder.ReadUInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_FAILURE_ACTIONSA : IRpcFixedStruct
	{
		public uint dwResetPeriod;
		public RpcPointer<string> lpRebootMsg;
		public RpcPointer<string> lpCommand;
		public uint cActions;
		public RpcPointer<SC_ACTION[]> lpsaActions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwResetPeriod);
			encoder.WriteUniquePointer(this.lpRebootMsg);
			encoder.WriteUniquePointer(this.lpCommand);
			encoder.WriteValue(this.cActions);
			encoder.WriteUniquePointer(this.lpsaActions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwResetPeriod = decoder.ReadUInt32();
			this.lpRebootMsg = decoder.ReadUniquePointer<string>();
			this.lpCommand = decoder.ReadUniquePointer<string>();
			this.cActions = decoder.ReadUInt32();
			this.lpsaActions = decoder.ReadUniquePointer<SC_ACTION[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpRebootMsg is not null)
			{
				encoder.WriteUnsignedCharString(this.lpRebootMsg.value);
			}

			if (this.lpCommand is not null)
			{
				encoder.WriteUnsignedCharString(this.lpCommand.value);
			}

			if (this.lpsaActions is not null)
			{
				encoder.WriteArrayHeader(this.lpsaActions.value);
				for (int i = 0; i < this.lpsaActions.value.Length; i++)
				{
					SC_ACTION elem_0 = this.lpsaActions.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.lpsaActions.value.Length; i++)
				{
					SC_ACTION elem_0 = this.lpsaActions.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpRebootMsg is not null)
			{
				this.lpRebootMsg.value = decoder.ReadUnsignedCharString();
			}

			if (this.lpCommand is not null)
			{
				this.lpCommand.value = decoder.ReadUnsignedCharString();
			}

			if (this.lpsaActions is not null)
			{
				this.lpsaActions.value = decoder.ReadArrayHeader<SC_ACTION>();
				for (int i = 0; i < this.lpsaActions.value.Length; i++)
				{
					SC_ACTION elem_0 = this.lpsaActions.value[i];
					elem_0 = decoder.ReadFixedStruct<SC_ACTION>(NdrAlignment._4Byte);
					this.lpsaActions.value[i] = elem_0;
				}

				for (int i = 0; i < this.lpsaActions.value.Length; i++)
				{
					SC_ACTION elem_0 = this.lpsaActions.value[i];
					decoder.ReadStructDeferral<SC_ACTION>(ref elem_0);
					this.lpsaActions.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_DELAYED_AUTO_START_INFO : IRpcFixedStruct
	{
		public int fDelayedAutostart;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.fDelayedAutostart);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.fDelayedAutostart = decoder.ReadInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_FAILURE_ACTIONS_FLAG : IRpcFixedStruct
	{
		public int fFailureActionsOnNonCrashFailures;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.fFailureActionsOnNonCrashFailures);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.fFailureActionsOnNonCrashFailures = decoder.ReadInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_SID_INFO : IRpcFixedStruct
	{
		public uint dwServiceSidType;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwServiceSidType);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwServiceSidType = decoder.ReadUInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_PRESHUTDOWN_INFO : IRpcFixedStruct
	{
		public uint dwPreshutdownTimeout;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwPreshutdownTimeout);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwPreshutdownTimeout = decoder.ReadUInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_DESCRIPTIONW : IRpcFixedStruct
	{
		public RpcPointer<string> lpDescription;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.lpDescription);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.lpDescription = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpDescription is not null)
			{
				encoder.WriteWideCharString(this.lpDescription.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpDescription is not null)
			{
				this.lpDescription.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_FAILURE_ACTIONSW : IRpcFixedStruct
	{
		public uint dwResetPeriod;
		public RpcPointer<string> lpRebootMsg;
		public RpcPointer<string> lpCommand;
		public uint cActions;
		public RpcPointer<SC_ACTION[]> lpsaActions;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwResetPeriod);
			encoder.WriteUniquePointer(this.lpRebootMsg);
			encoder.WriteUniquePointer(this.lpCommand);
			encoder.WriteValue(this.cActions);
			encoder.WriteUniquePointer(this.lpsaActions);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwResetPeriod = decoder.ReadUInt32();
			this.lpRebootMsg = decoder.ReadUniquePointer<string>();
			this.lpCommand = decoder.ReadUniquePointer<string>();
			this.cActions = decoder.ReadUInt32();
			this.lpsaActions = decoder.ReadUniquePointer<SC_ACTION[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpRebootMsg is not null)
			{
				encoder.WriteWideCharString(this.lpRebootMsg.value);
			}

			if (this.lpCommand is not null)
			{
				encoder.WriteWideCharString(this.lpCommand.value);
			}

			if (this.lpsaActions is not null)
			{
				encoder.WriteArrayHeader(this.lpsaActions.value);
				for (int i = 0; i < this.lpsaActions.value.Length; i++)
				{
					SC_ACTION elem_0 = this.lpsaActions.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.lpsaActions.value.Length; i++)
				{
					SC_ACTION elem_0 = this.lpsaActions.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpRebootMsg is not null)
			{
				this.lpRebootMsg.value = decoder.ReadWideCharString();
			}

			if (this.lpCommand is not null)
			{
				this.lpCommand.value = decoder.ReadWideCharString();
			}

			if (this.lpsaActions is not null)
			{
				this.lpsaActions.value = decoder.ReadArrayHeader<SC_ACTION>();
				for (int i = 0; i < this.lpsaActions.value.Length; i++)
				{
					SC_ACTION elem_0 = this.lpsaActions.value[i];
					elem_0 = decoder.ReadFixedStruct<SC_ACTION>(NdrAlignment._4Byte);
					this.lpsaActions.value[i] = elem_0;
				}

				for (int i = 0; i < this.lpsaActions.value.Length; i++)
				{
					SC_ACTION elem_0 = this.lpsaActions.value[i];
					decoder.ReadStructDeferral<SC_ACTION>(ref elem_0);
					this.lpsaActions.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum SC_STATUS_TYPE : int
	{
		SC_STATUS_PROCESS_INFO = 0
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum SC_ENUM_TYPE : int
	{
		SC_ENUM_PROCESS_INFO = 0
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_PREFERRED_NODE_INFO : IRpcFixedStruct
	{
		public ushort usPreferredNode;
		public byte fDelete;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.usPreferredNode);
			encoder.WriteValue(this.fDelete);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.usPreferredNode = decoder.ReadUInt16();
			this.fDelete = decoder.ReadUnsignedChar();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_TRIGGER_SPECIFIC_DATA_ITEM : IRpcFixedStruct
	{
		public uint dwDataType;
		public uint cbData;
		public RpcPointer<byte[]> pData;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwDataType);
			encoder.WriteValue(this.cbData);
			encoder.WriteUniquePointer(this.pData);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwDataType = decoder.ReadUInt32();
			this.cbData = decoder.ReadUInt32();
			this.pData = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pData is not null)
			{
				encoder.WriteArrayHeader(this.pData.value);
				for (int i = 0; i < this.pData.value.Length; i++)
				{
					byte elem_0 = this.pData.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pData is not null)
			{
				this.pData.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pData.value.Length; i++)
				{
					byte elem_0 = this.pData.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pData.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_TRIGGER : IRpcFixedStruct
	{
		public uint dwTriggerType;
		public uint dwAction;
		public RpcPointer<Guid> pTriggerSubtype;
		public uint cDataItems;
		public RpcPointer<SERVICE_TRIGGER_SPECIFIC_DATA_ITEM[]> pDataItems;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwTriggerType);
			encoder.WriteValue(this.dwAction);
			encoder.WriteUniquePointer(this.pTriggerSubtype);
			encoder.WriteValue(this.cDataItems);
			encoder.WriteUniquePointer(this.pDataItems);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwTriggerType = decoder.ReadUInt32();
			this.dwAction = decoder.ReadUInt32();
			this.pTriggerSubtype = decoder.ReadUniquePointer<Guid>();
			this.cDataItems = decoder.ReadUInt32();
			this.pDataItems = decoder.ReadUniquePointer<SERVICE_TRIGGER_SPECIFIC_DATA_ITEM[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pTriggerSubtype is not null)
			{
				encoder.WriteValue(this.pTriggerSubtype.value);
			}

			if (this.pDataItems is not null)
			{
				encoder.WriteArrayHeader(this.pDataItems.value);
				for (int i = 0; i < this.pDataItems.value.Length; i++)
				{
					SERVICE_TRIGGER_SPECIFIC_DATA_ITEM elem_0 = this.pDataItems.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.pDataItems.value.Length; i++)
				{
					SERVICE_TRIGGER_SPECIFIC_DATA_ITEM elem_0 = this.pDataItems.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pTriggerSubtype is not null)
			{
				this.pTriggerSubtype.value = decoder.ReadUuid();
			}

			if (this.pDataItems is not null)
			{
				this.pDataItems.value = decoder.ReadArrayHeader<SERVICE_TRIGGER_SPECIFIC_DATA_ITEM>();
				for (int i = 0; i < this.pDataItems.value.Length; i++)
				{
					SERVICE_TRIGGER_SPECIFIC_DATA_ITEM elem_0 = this.pDataItems.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVICE_TRIGGER_SPECIFIC_DATA_ITEM>(NdrAlignment.NativePtr);
					this.pDataItems.value[i] = elem_0;
				}

				for (int i = 0; i < this.pDataItems.value.Length; i++)
				{
					SERVICE_TRIGGER_SPECIFIC_DATA_ITEM elem_0 = this.pDataItems.value[i];
					decoder.ReadStructDeferral<SERVICE_TRIGGER_SPECIFIC_DATA_ITEM>(ref elem_0);
					this.pDataItems.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_TRIGGER_INFO : IRpcFixedStruct
	{
		public uint cTriggers;
		public RpcPointer<SERVICE_TRIGGER[]> pTriggers;
		public RpcPointer<byte> pReserved;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cTriggers);
			encoder.WriteUniquePointer(this.pTriggers);
			encoder.WriteUniquePointer(this.pReserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cTriggers = decoder.ReadUInt32();
			this.pTriggers = decoder.ReadUniquePointer<SERVICE_TRIGGER[]>();
			this.pReserved = decoder.ReadUniquePointer<byte>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pTriggers is not null)
			{
				encoder.WriteArrayHeader(this.pTriggers.value);
				for (int i = 0; i < this.pTriggers.value.Length; i++)
				{
					SERVICE_TRIGGER elem_0 = this.pTriggers.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.pTriggers.value.Length; i++)
				{
					SERVICE_TRIGGER elem_0 = this.pTriggers.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			if (this.pReserved is not null)
			{
				encoder.WriteValue(this.pReserved.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pTriggers is not null)
			{
				this.pTriggers.value = decoder.ReadArrayHeader<SERVICE_TRIGGER>();
				for (int i = 0; i < this.pTriggers.value.Length; i++)
				{
					SERVICE_TRIGGER elem_0 = this.pTriggers.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVICE_TRIGGER>(NdrAlignment.NativePtr);
					this.pTriggers.value[i] = elem_0;
				}

				for (int i = 0; i < this.pTriggers.value.Length; i++)
				{
					SERVICE_TRIGGER elem_0 = this.pTriggers.value[i];
					decoder.ReadStructDeferral<SERVICE_TRIGGER>(ref elem_0);
					this.pTriggers.value[i] = elem_0;
				}
			}

			if (this.pReserved is not null)
			{
				this.pReserved.value = decoder.ReadUnsignedChar();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ENUM_SERVICE_STATUSA : IRpcFixedStruct
	{
		public RpcPointer<byte> lpServiceName;
		public RpcPointer<byte> lpDisplayName;
		public SERVICE_STATUS ServiceStatus;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.lpServiceName);
			encoder.WriteUniquePointer(this.lpDisplayName);
			encoder.WriteFixedStruct(this.ServiceStatus, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.lpServiceName = decoder.ReadUniquePointer<byte>();
			this.lpDisplayName = decoder.ReadUniquePointer<byte>();
			this.ServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpServiceName is not null)
			{
				encoder.WriteValue(this.lpServiceName.value);
			}

			if (this.lpDisplayName is not null)
			{
				encoder.WriteValue(this.lpDisplayName.value);
			}

			encoder.WriteStructDeferral(this.ServiceStatus);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpServiceName is not null)
			{
				this.lpServiceName.value = decoder.ReadUnsignedChar();
			}

			if (this.lpDisplayName is not null)
			{
				this.lpDisplayName.value = decoder.ReadUnsignedChar();
			}

			decoder.ReadStructDeferral<SERVICE_STATUS>(ref this.ServiceStatus);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ENUM_SERVICE_STATUSW : IRpcFixedStruct
	{
		public RpcPointer<char> lpServiceName;
		public RpcPointer<char> lpDisplayName;
		public SERVICE_STATUS ServiceStatus;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.lpServiceName);
			encoder.WriteUniquePointer(this.lpDisplayName);
			encoder.WriteFixedStruct(this.ServiceStatus, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.lpServiceName = decoder.ReadUniquePointer<char>();
			this.lpDisplayName = decoder.ReadUniquePointer<char>();
			this.ServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpServiceName is not null)
			{
				encoder.WriteValue(this.lpServiceName.value);
			}

			if (this.lpDisplayName is not null)
			{
				encoder.WriteValue(this.lpDisplayName.value);
			}

			encoder.WriteStructDeferral(this.ServiceStatus);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpServiceName is not null)
			{
				this.lpServiceName.value = decoder.ReadWideChar();
			}

			if (this.lpDisplayName is not null)
			{
				this.lpDisplayName.value = decoder.ReadWideChar();
			}

			decoder.ReadStructDeferral<SERVICE_STATUS>(ref this.ServiceStatus);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ENUM_SERVICE_STATUS_PROCESSA : IRpcFixedStruct
	{
		public RpcPointer<byte> lpServiceName;
		public RpcPointer<byte> lpDisplayName;
		public SERVICE_STATUS_PROCESS ServiceStatusProcess;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.lpServiceName);
			encoder.WriteUniquePointer(this.lpDisplayName);
			encoder.WriteFixedStruct(this.ServiceStatusProcess, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.lpServiceName = decoder.ReadUniquePointer<byte>();
			this.lpDisplayName = decoder.ReadUniquePointer<byte>();
			this.ServiceStatusProcess = decoder.ReadFixedStruct<SERVICE_STATUS_PROCESS>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpServiceName is not null)
			{
				encoder.WriteValue(this.lpServiceName.value);
			}

			if (this.lpDisplayName is not null)
			{
				encoder.WriteValue(this.lpDisplayName.value);
			}

			encoder.WriteStructDeferral(this.ServiceStatusProcess);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpServiceName is not null)
			{
				this.lpServiceName.value = decoder.ReadUnsignedChar();
			}

			if (this.lpDisplayName is not null)
			{
				this.lpDisplayName.value = decoder.ReadUnsignedChar();
			}

			decoder.ReadStructDeferral<SERVICE_STATUS_PROCESS>(ref this.ServiceStatusProcess);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ENUM_SERVICE_STATUS_PROCESSW : IRpcFixedStruct
	{
		public RpcPointer<char> lpServiceName;
		public RpcPointer<char> lpDisplayName;
		public SERVICE_STATUS_PROCESS ServiceStatusProcess;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.lpServiceName);
			encoder.WriteUniquePointer(this.lpDisplayName);
			encoder.WriteFixedStruct(this.ServiceStatusProcess, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.lpServiceName = decoder.ReadUniquePointer<char>();
			this.lpDisplayName = decoder.ReadUniquePointer<char>();
			this.ServiceStatusProcess = decoder.ReadFixedStruct<SERVICE_STATUS_PROCESS>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpServiceName is not null)
			{
				encoder.WriteValue(this.lpServiceName.value);
			}

			if (this.lpDisplayName is not null)
			{
				encoder.WriteValue(this.lpDisplayName.value);
			}

			encoder.WriteStructDeferral(this.ServiceStatusProcess);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpServiceName is not null)
			{
				this.lpServiceName.value = decoder.ReadWideChar();
			}

			if (this.lpDisplayName is not null)
			{
				this.lpDisplayName.value = decoder.ReadWideChar();
			}

			decoder.ReadStructDeferral<SERVICE_STATUS_PROCESS>(ref this.ServiceStatusProcess);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_DESCRIPTION_WOW64 : IRpcFixedStruct
	{
		public uint dwDescriptionOffset;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwDescriptionOffset);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwDescriptionOffset = decoder.ReadUInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_FAILURE_ACTIONS_WOW64 : IRpcFixedStruct
	{
		public uint dwResetPeriod;
		public uint dwRebootMsgOffset;
		public uint dwCommandOffset;
		public uint cActions;
		public uint dwsaActionsOffset;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwResetPeriod);
			encoder.WriteValue(this.dwRebootMsgOffset);
			encoder.WriteValue(this.dwCommandOffset);
			encoder.WriteValue(this.cActions);
			encoder.WriteValue(this.dwsaActionsOffset);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwResetPeriod = decoder.ReadUInt32();
			this.dwRebootMsgOffset = decoder.ReadUInt32();
			this.dwCommandOffset = decoder.ReadUInt32();
			this.cActions = decoder.ReadUInt32();
			this.dwsaActionsOffset = decoder.ReadUInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_REQUIRED_PRIVILEGES_INFO_WOW64 : IRpcFixedStruct
	{
		public uint dwRequiredPrivilegesOffset;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwRequiredPrivilegesOffset);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwRequiredPrivilegesOffset = decoder.ReadUInt32();
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

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_RPC_REQUIRED_PRIVILEGES_INFO : IRpcFixedStruct
	{
		public uint cbRequiredPrivileges;
		public RpcPointer<byte[]> pRequiredPrivileges;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cbRequiredPrivileges);
			encoder.WriteUniquePointer(this.pRequiredPrivileges);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cbRequiredPrivileges = decoder.ReadUInt32();
			this.pRequiredPrivileges = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pRequiredPrivileges is not null)
			{
				encoder.WriteArrayHeader(this.pRequiredPrivileges.value);
				for (int i = 0; i < this.pRequiredPrivileges.value.Length; i++)
				{
					byte elem_0 = this.pRequiredPrivileges.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pRequiredPrivileges is not null)
			{
				this.pRequiredPrivileges.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.pRequiredPrivileges.value.Length; i++)
				{
					byte elem_0 = this.pRequiredPrivileges.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.pRequiredPrivileges.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct Unnamed_1 : IRpcFixedStruct
	{
		public uint dwInfoLevel;
		public RpcPointer<SERVICE_DESCRIPTIONA> psd;
		public RpcPointer<SERVICE_FAILURE_ACTIONSA> psfa;
		public RpcPointer<SERVICE_DELAYED_AUTO_START_INFO> psda;
		public RpcPointer<SERVICE_FAILURE_ACTIONS_FLAG> psfaf;
		public RpcPointer<SERVICE_SID_INFO> pssid;
		public RpcPointer<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO> psrp;
		public RpcPointer<SERVICE_PRESHUTDOWN_INFO> psps;
		public RpcPointer<SERVICE_TRIGGER_INFO> psti;
		public RpcPointer<SERVICE_PREFERRED_NODE_INFO> pspn;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwInfoLevel);
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					encoder.WriteUniquePointer(this.psd);
					break;
				case 2U:
					encoder.WriteUniquePointer(this.psfa);
					break;
				case 3U:
					encoder.WriteUniquePointer(this.psda);
					break;
				case 4U:
					encoder.WriteUniquePointer(this.psfaf);
					break;
				case 5U:
					encoder.WriteUniquePointer(this.pssid);
					break;
				case 6U:
					encoder.WriteUniquePointer(this.psrp);
					break;
				case 7U:
					encoder.WriteUniquePointer(this.psps);
					break;
				case 8U:
					encoder.WriteUniquePointer(this.psti);
					break;
				case 9U:
					encoder.WriteUniquePointer(this.pspn);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.dwInfoLevel = decoder.ReadUInt32();
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					this.psd = decoder.ReadUniquePointer<SERVICE_DESCRIPTIONA>();
					break;
				case 2U:
					this.psfa = decoder.ReadUniquePointer<SERVICE_FAILURE_ACTIONSA>();
					break;
				case 3U:
					this.psda = decoder.ReadUniquePointer<SERVICE_DELAYED_AUTO_START_INFO>();
					break;
				case 4U:
					this.psfaf = decoder.ReadUniquePointer<SERVICE_FAILURE_ACTIONS_FLAG>();
					break;
				case 5U:
					this.pssid = decoder.ReadUniquePointer<SERVICE_SID_INFO>();
					break;
				case 6U:
					this.psrp = decoder.ReadUniquePointer<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>();
					break;
				case 7U:
					this.psps = decoder.ReadUniquePointer<SERVICE_PRESHUTDOWN_INFO>();
					break;
				case 8U:
					this.psti = decoder.ReadUniquePointer<SERVICE_TRIGGER_INFO>();
					break;
				case 9U:
					this.pspn = decoder.ReadUniquePointer<SERVICE_PREFERRED_NODE_INFO>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					if (this.psd is not null)
					{
						encoder.WriteFixedStruct(this.psd.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.psd.value);
					}

					break;
				case 2U:
					if (this.psfa is not null)
					{
						encoder.WriteFixedStruct(this.psfa.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.psfa.value);
					}

					break;
				case 3U:
					if (this.psda is not null)
					{
						encoder.WriteFixedStruct(this.psda.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.psda.value);
					}

					break;
				case 4U:
					if (this.psfaf is not null)
					{
						encoder.WriteFixedStruct(this.psfaf.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.psfaf.value);
					}

					break;
				case 5U:
					if (this.pssid is not null)
					{
						encoder.WriteFixedStruct(this.pssid.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.pssid.value);
					}

					break;
				case 6U:
					if (this.psrp is not null)
					{
						encoder.WriteFixedStruct(this.psrp.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.psrp.value);
					}

					break;
				case 7U:
					if (this.psps is not null)
					{
						encoder.WriteFixedStruct(this.psps.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.psps.value);
					}

					break;
				case 8U:
					if (this.psti is not null)
					{
						encoder.WriteFixedStruct(this.psti.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.psti.value);
					}

					break;
				case 9U:
					if (this.pspn is not null)
					{
						encoder.WriteFixedStruct(this.pspn.value, NdrAlignment._2Byte);
						encoder.WriteStructDeferral(this.pspn.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					if (this.psd is not null)
					{
						this.psd.value = decoder.ReadFixedStruct<SERVICE_DESCRIPTIONA>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVICE_DESCRIPTIONA>(ref this.psd.value);
					}

					break;
				case 2U:
					if (this.psfa is not null)
					{
						this.psfa.value = decoder.ReadFixedStruct<SERVICE_FAILURE_ACTIONSA>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVICE_FAILURE_ACTIONSA>(ref this.psfa.value);
					}

					break;
				case 3U:
					if (this.psda is not null)
					{
						this.psda.value = decoder.ReadFixedStruct<SERVICE_DELAYED_AUTO_START_INFO>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVICE_DELAYED_AUTO_START_INFO>(ref this.psda.value);
					}

					break;
				case 4U:
					if (this.psfaf is not null)
					{
						this.psfaf.value = decoder.ReadFixedStruct<SERVICE_FAILURE_ACTIONS_FLAG>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVICE_FAILURE_ACTIONS_FLAG>(ref this.psfaf.value);
					}

					break;
				case 5U:
					if (this.pssid is not null)
					{
						this.pssid.value = decoder.ReadFixedStruct<SERVICE_SID_INFO>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVICE_SID_INFO>(ref this.pssid.value);
					}

					break;
				case 6U:
					if (this.psrp is not null)
					{
						this.psrp.value = decoder.ReadFixedStruct<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>(ref this.psrp.value);
					}

					break;
				case 7U:
					if (this.psps is not null)
					{
						this.psps.value = decoder.ReadFixedStruct<SERVICE_PRESHUTDOWN_INFO>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVICE_PRESHUTDOWN_INFO>(ref this.psps.value);
					}

					break;
				case 8U:
					if (this.psti is not null)
					{
						this.psti.value = decoder.ReadFixedStruct<SERVICE_TRIGGER_INFO>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVICE_TRIGGER_INFO>(ref this.psti.value);
					}

					break;
				case 9U:
					if (this.pspn is not null)
					{
						this.pspn.value = decoder.ReadFixedStruct<SERVICE_PREFERRED_NODE_INFO>(NdrAlignment._2Byte);
						decoder.ReadStructDeferral<SERVICE_PREFERRED_NODE_INFO>(ref this.pspn.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SC_RPC_CONFIG_INFOA : IRpcFixedStruct
	{
		public uint dwInfoLevel;
		public Unnamed_1 unnamed_1;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwInfoLevel);
			encoder.WriteUnion(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwInfoLevel = decoder.ReadUInt32();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_1>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<Unnamed_1>(ref this.unnamed_1);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct Unnamed_2 : IRpcFixedStruct
	{
		public uint dwInfoLevel;
		public RpcPointer<SERVICE_DESCRIPTIONW> psd;
		public RpcPointer<SERVICE_FAILURE_ACTIONSW> psfa;
		public RpcPointer<SERVICE_DELAYED_AUTO_START_INFO> psda;
		public RpcPointer<SERVICE_FAILURE_ACTIONS_FLAG> psfaf;
		public RpcPointer<SERVICE_SID_INFO> pssid;
		public RpcPointer<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO> psrp;
		public RpcPointer<SERVICE_PRESHUTDOWN_INFO> psps;
		public RpcPointer<SERVICE_TRIGGER_INFO> psti;
		public RpcPointer<SERVICE_PREFERRED_NODE_INFO> pspn;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwInfoLevel);
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					encoder.WriteUniquePointer(this.psd);
					break;
				case 2U:
					encoder.WriteUniquePointer(this.psfa);
					break;
				case 3U:
					encoder.WriteUniquePointer(this.psda);
					break;
				case 4U:
					encoder.WriteUniquePointer(this.psfaf);
					break;
				case 5U:
					encoder.WriteUniquePointer(this.pssid);
					break;
				case 6U:
					encoder.WriteUniquePointer(this.psrp);
					break;
				case 7U:
					encoder.WriteUniquePointer(this.psps);
					break;
				case 8U:
					encoder.WriteUniquePointer(this.psti);
					break;
				case 9U:
					encoder.WriteUniquePointer(this.pspn);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.dwInfoLevel = decoder.ReadUInt32();
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					this.psd = decoder.ReadUniquePointer<SERVICE_DESCRIPTIONW>();
					break;
				case 2U:
					this.psfa = decoder.ReadUniquePointer<SERVICE_FAILURE_ACTIONSW>();
					break;
				case 3U:
					this.psda = decoder.ReadUniquePointer<SERVICE_DELAYED_AUTO_START_INFO>();
					break;
				case 4U:
					this.psfaf = decoder.ReadUniquePointer<SERVICE_FAILURE_ACTIONS_FLAG>();
					break;
				case 5U:
					this.pssid = decoder.ReadUniquePointer<SERVICE_SID_INFO>();
					break;
				case 6U:
					this.psrp = decoder.ReadUniquePointer<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>();
					break;
				case 7U:
					this.psps = decoder.ReadUniquePointer<SERVICE_PRESHUTDOWN_INFO>();
					break;
				case 8U:
					this.psti = decoder.ReadUniquePointer<SERVICE_TRIGGER_INFO>();
					break;
				case 9U:
					this.pspn = decoder.ReadUniquePointer<SERVICE_PREFERRED_NODE_INFO>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					if (this.psd is not null)
					{
						encoder.WriteFixedStruct(this.psd.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.psd.value);
					}

					break;
				case 2U:
					if (this.psfa is not null)
					{
						encoder.WriteFixedStruct(this.psfa.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.psfa.value);
					}

					break;
				case 3U:
					if (this.psda is not null)
					{
						encoder.WriteFixedStruct(this.psda.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.psda.value);
					}

					break;
				case 4U:
					if (this.psfaf is not null)
					{
						encoder.WriteFixedStruct(this.psfaf.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.psfaf.value);
					}

					break;
				case 5U:
					if (this.pssid is not null)
					{
						encoder.WriteFixedStruct(this.pssid.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.pssid.value);
					}

					break;
				case 6U:
					if (this.psrp is not null)
					{
						encoder.WriteFixedStruct(this.psrp.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.psrp.value);
					}

					break;
				case 7U:
					if (this.psps is not null)
					{
						encoder.WriteFixedStruct(this.psps.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.psps.value);
					}

					break;
				case 8U:
					if (this.psti is not null)
					{
						encoder.WriteFixedStruct(this.psti.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.psti.value);
					}

					break;
				case 9U:
					if (this.pspn is not null)
					{
						encoder.WriteFixedStruct(this.pspn.value, NdrAlignment._2Byte);
						encoder.WriteStructDeferral(this.pspn.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					if (this.psd is not null)
					{
						this.psd.value = decoder.ReadFixedStruct<SERVICE_DESCRIPTIONW>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVICE_DESCRIPTIONW>(ref this.psd.value);
					}

					break;
				case 2U:
					if (this.psfa is not null)
					{
						this.psfa.value = decoder.ReadFixedStruct<SERVICE_FAILURE_ACTIONSW>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVICE_FAILURE_ACTIONSW>(ref this.psfa.value);
					}

					break;
				case 3U:
					if (this.psda is not null)
					{
						this.psda.value = decoder.ReadFixedStruct<SERVICE_DELAYED_AUTO_START_INFO>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVICE_DELAYED_AUTO_START_INFO>(ref this.psda.value);
					}

					break;
				case 4U:
					if (this.psfaf is not null)
					{
						this.psfaf.value = decoder.ReadFixedStruct<SERVICE_FAILURE_ACTIONS_FLAG>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVICE_FAILURE_ACTIONS_FLAG>(ref this.psfaf.value);
					}

					break;
				case 5U:
					if (this.pssid is not null)
					{
						this.pssid.value = decoder.ReadFixedStruct<SERVICE_SID_INFO>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVICE_SID_INFO>(ref this.pssid.value);
					}

					break;
				case 6U:
					if (this.psrp is not null)
					{
						this.psrp.value = decoder.ReadFixedStruct<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>(ref this.psrp.value);
					}

					break;
				case 7U:
					if (this.psps is not null)
					{
						this.psps.value = decoder.ReadFixedStruct<SERVICE_PRESHUTDOWN_INFO>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVICE_PRESHUTDOWN_INFO>(ref this.psps.value);
					}

					break;
				case 8U:
					if (this.psti is not null)
					{
						this.psti.value = decoder.ReadFixedStruct<SERVICE_TRIGGER_INFO>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVICE_TRIGGER_INFO>(ref this.psti.value);
					}

					break;
				case 9U:
					if (this.pspn is not null)
					{
						this.pspn.value = decoder.ReadFixedStruct<SERVICE_PREFERRED_NODE_INFO>(NdrAlignment._2Byte);
						decoder.ReadStructDeferral<SERVICE_PREFERRED_NODE_INFO>(ref this.pspn.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SC_RPC_CONFIG_INFOW : IRpcFixedStruct
	{
		public uint dwInfoLevel;
		public Unnamed_2 unnamed_1;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwInfoLevel);
			encoder.WriteUnion(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwInfoLevel = decoder.ReadUInt32();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_2>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<Unnamed_2>(ref this.unnamed_1);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_1 : IRpcFixedStruct
	{
		public ulong ullThreadId;
		public uint dwNotifyMask;
		public byte[] CallbackAddressArray;
		public byte[] CallbackParamAddressArray;
		public SERVICE_STATUS_PROCESS ServiceStatus;
		public uint dwNotificationStatus;
		public uint dwSequence;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ullThreadId);
			encoder.WriteValue(this.dwNotifyMask);
			if (this.CallbackAddressArray == null)
				this.CallbackAddressArray = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.CallbackAddressArray[i];
				encoder.WriteValue(elem_0);
			}

			if (this.CallbackParamAddressArray == null)
				this.CallbackParamAddressArray = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.CallbackParamAddressArray[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteFixedStruct(this.ServiceStatus, NdrAlignment._4Byte);
			encoder.WriteValue(this.dwNotificationStatus);
			encoder.WriteValue(this.dwSequence);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ullThreadId = decoder.ReadUInt64();
			this.dwNotifyMask = decoder.ReadUInt32();
			if (this.CallbackAddressArray == null)
				this.CallbackAddressArray = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.CallbackAddressArray[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.CallbackAddressArray[i] = elem_0;
			}

			if (this.CallbackParamAddressArray == null)
				this.CallbackParamAddressArray = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.CallbackParamAddressArray[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.CallbackParamAddressArray[i] = elem_0;
			}

			this.ServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS_PROCESS>(NdrAlignment._4Byte);
			this.dwNotificationStatus = decoder.ReadUInt32();
			this.dwSequence = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ServiceStatus);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SERVICE_STATUS_PROCESS>(ref this.ServiceStatus);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_2 : IRpcFixedStruct
	{
		public ulong ullThreadId;
		public uint dwNotifyMask;
		public byte[] CallbackAddressArray;
		public byte[] CallbackParamAddressArray;
		public SERVICE_STATUS_PROCESS ServiceStatus;
		public uint dwNotificationStatus;
		public uint dwSequence;
		public uint dwNotificationTriggered;
		public RpcPointer<string> pszServiceNames;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.ullThreadId);
			encoder.WriteValue(this.dwNotifyMask);
			if (this.CallbackAddressArray == null)
				this.CallbackAddressArray = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.CallbackAddressArray[i];
				encoder.WriteValue(elem_0);
			}

			if (this.CallbackParamAddressArray == null)
				this.CallbackParamAddressArray = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.CallbackParamAddressArray[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteFixedStruct(this.ServiceStatus, NdrAlignment._4Byte);
			encoder.WriteValue(this.dwNotificationStatus);
			encoder.WriteValue(this.dwSequence);
			encoder.WriteValue(this.dwNotificationTriggered);
			encoder.WriteUniquePointer(this.pszServiceNames);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ullThreadId = decoder.ReadUInt64();
			this.dwNotifyMask = decoder.ReadUInt32();
			if (this.CallbackAddressArray == null)
				this.CallbackAddressArray = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.CallbackAddressArray[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.CallbackAddressArray[i] = elem_0;
			}

			if (this.CallbackParamAddressArray == null)
				this.CallbackParamAddressArray = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte elem_0 = this.CallbackParamAddressArray[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.CallbackParamAddressArray[i] = elem_0;
			}

			this.ServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS_PROCESS>(NdrAlignment._4Byte);
			this.dwNotificationStatus = decoder.ReadUInt32();
			this.dwSequence = decoder.ReadUInt32();
			this.dwNotificationTriggered = decoder.ReadUInt32();
			this.pszServiceNames = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ServiceStatus);
			if (this.pszServiceNames is not null)
			{
				encoder.WriteWideCharString(this.pszServiceNames.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SERVICE_STATUS_PROCESS>(ref this.ServiceStatus);
			if (this.pszServiceNames is not null)
			{
				this.pszServiceNames.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct Unnamed_3 : IRpcFixedStruct
	{
		public uint dwInfoLevel;
		public RpcPointer<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_1> pStatusChangeParam1;
		public RpcPointer<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_2> pStatusChangeParams;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.dwInfoLevel);
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					encoder.WriteUniquePointer(this.pStatusChangeParam1);
					break;
				case 2U:
					encoder.WriteUniquePointer(this.pStatusChangeParams);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.dwInfoLevel = decoder.ReadUInt32();
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					this.pStatusChangeParam1 = decoder.ReadUniquePointer<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_1>();
					break;
				case 2U:
					this.pStatusChangeParams = decoder.ReadUniquePointer<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_2>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					if (this.pStatusChangeParam1 is not null)
					{
						encoder.WriteFixedStruct(this.pStatusChangeParam1.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pStatusChangeParam1.value);
					}

					break;
				case 2U:
					if (this.pStatusChangeParams is not null)
					{
						encoder.WriteFixedStruct(this.pStatusChangeParams.value, NdrAlignment._8Byte);
						encoder.WriteStructDeferral(this.pStatusChangeParams.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.dwInfoLevel)
			{
				case 1U:
					if (this.pStatusChangeParam1 is not null)
					{
						this.pStatusChangeParam1.value = decoder.ReadFixedStruct<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_1>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_1>(ref this.pStatusChangeParam1.value);
					}

					break;
				case 2U:
					if (this.pStatusChangeParams is not null)
					{
						this.pStatusChangeParams.value = decoder.ReadFixedStruct<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_2>(NdrAlignment._8Byte);
						decoder.ReadStructDeferral<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_2>(ref this.pStatusChangeParams.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SC_RPC_NOTIFY_PARAMS : IRpcFixedStruct
	{
		public uint dwInfoLevel;
		public Unnamed_3 unnamed_1;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwInfoLevel);
			encoder.WriteUnion(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwInfoLevel = decoder.ReadUInt32();
			this.unnamed_1 = decoder.ReadUnion<Unnamed_3>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.unnamed_1);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<Unnamed_3>(ref this.unnamed_1);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SC_RPC_NOTIFY_PARAMS_LIST : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.NotifyParamsArray);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.NotifyParamsArray = decoder.ReadArrayHeader<SC_RPC_NOTIFY_PARAMS>();
		}

		public uint cElements;
		public SC_RPC_NOTIFY_PARAMS[] NotifyParamsArray;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.NotifyParamsArray.Length; i++)
			{
				SC_RPC_NOTIFY_PARAMS elem_0 = this.NotifyParamsArray[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.NotifyParamsArray.Length; i++)
			{
				SC_RPC_NOTIFY_PARAMS elem_0 = this.NotifyParamsArray[i];
				elem_0 = decoder.ReadFixedStruct<SC_RPC_NOTIFY_PARAMS>(NdrAlignment.NativePtr);
				this.NotifyParamsArray[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cElements);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cElements = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.NotifyParamsArray.Length; i++)
			{
				SC_RPC_NOTIFY_PARAMS elem_0 = this.NotifyParamsArray[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.NotifyParamsArray.Length; i++)
			{
				SC_RPC_NOTIFY_PARAMS elem_0 = this.NotifyParamsArray[i];
				decoder.ReadStructDeferral<SC_RPC_NOTIFY_PARAMS>(ref elem_0);
				this.NotifyParamsArray[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_CONTROL_STATUS_REASON_IN_PARAMSA : IRpcFixedStruct
	{
		public uint dwReason;
		public RpcPointer<string> pszComment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwReason);
			encoder.WriteUniquePointer(this.pszComment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwReason = decoder.ReadUInt32();
			this.pszComment = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszComment is not null)
			{
				encoder.WriteUnsignedCharString(this.pszComment.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszComment is not null)
			{
				this.pszComment.value = decoder.ReadUnsignedCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS : IRpcFixedStruct
	{
		public SERVICE_STATUS_PROCESS ServiceStatus;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ServiceStatus, NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS_PROCESS>(NdrAlignment._4Byte);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ServiceStatus);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SERVICE_STATUS_PROCESS>(ref this.ServiceStatus);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SC_RPC_SERVICE_CONTROL_IN_PARAMSA : IRpcFixedStruct
	{
		public uint unionSwitch;
		public RpcPointer<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSA> psrInParams;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteUniquePointer(this.psrInParams);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.psrInParams = decoder.ReadUniquePointer<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSA>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					if (this.psrInParams is not null)
					{
						encoder.WriteFixedStruct(this.psrInParams.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.psrInParams.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					if (this.psrInParams is not null)
					{
						this.psrInParams.value = decoder.ReadFixedStruct<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSA>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSA>(ref this.psrInParams.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SC_RPC_SERVICE_CONTROL_OUT_PARAMSA : IRpcFixedStruct
	{
		public uint unionSwitch;
		public RpcPointer<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS> psrOutParams;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteUniquePointer(this.psrOutParams);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.psrOutParams = decoder.ReadUniquePointer<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					if (this.psrOutParams is not null)
					{
						encoder.WriteFixedStruct(this.psrOutParams.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.psrOutParams.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					if (this.psrOutParams is not null)
					{
						this.psrOutParams.value = decoder.ReadFixedStruct<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>(ref this.psrOutParams.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVICE_CONTROL_STATUS_REASON_IN_PARAMSW : IRpcFixedStruct
	{
		public uint dwReason;
		public RpcPointer<string> pszComment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.dwReason);
			encoder.WriteUniquePointer(this.pszComment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.dwReason = decoder.ReadUInt32();
			this.pszComment = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.pszComment is not null)
			{
				encoder.WriteWideCharString(this.pszComment.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.pszComment is not null)
			{
				this.pszComment.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SC_RPC_SERVICE_CONTROL_IN_PARAMSW : IRpcFixedStruct
	{
		public uint unionSwitch;
		public RpcPointer<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSW> psrInParams;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteUniquePointer(this.psrInParams);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.psrInParams = decoder.ReadUniquePointer<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSW>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					if (this.psrInParams is not null)
					{
						encoder.WriteFixedStruct(this.psrInParams.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.psrInParams.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					if (this.psrInParams is not null)
					{
						this.psrInParams.value = decoder.ReadFixedStruct<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSW>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSW>(ref this.psrInParams.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SC_RPC_SERVICE_CONTROL_OUT_PARAMSW : IRpcFixedStruct
	{
		public uint unionSwitch;
		public RpcPointer<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS> psrOutParams;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					encoder.WriteUniquePointer(this.psrOutParams);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					this.psrOutParams = decoder.ReadUniquePointer<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					if (this.psrOutParams is not null)
					{
						encoder.WriteFixedStruct(this.psrOutParams.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.psrOutParams.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 1U:
					if (this.psrOutParams is not null)
					{
						this.psrOutParams.value = decoder.ReadFixedStruct<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>(ref this.psrOutParams.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("367abb81-9844-35f1-ad32-98f038001003"), RpcVersionAttribute(2, 0)]
	public partial interface svcctl
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RCloseServiceHandle(RpcPointer<RpcContextHandle> hSCObject, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RControlService(RpcContextHandle hService, uint dwControl, RpcPointer<SERVICE_STATUS> lpServiceStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RDeleteService(RpcContextHandle hService, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RLockServiceDatabase(RpcContextHandle hSCManager, RpcPointer<RpcContextHandle> lpLock, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RQueryServiceObjectSecurity(RpcContextHandle hService, uint dwSecurityInformation, RpcPointer<byte[]> lpSecurityDescriptor, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RSetServiceObjectSecurity(RpcContextHandle hService, uint dwSecurityInformation, byte[] lpSecurityDescriptor, uint cbBufSize, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RQueryServiceStatus(RpcContextHandle hService, RpcPointer<SERVICE_STATUS> lpServiceStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RSetServiceStatus(RpcContextHandle hServiceStatus, SERVICE_STATUS lpServiceStatus, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RUnlockServiceDatabase(RpcPointer<RpcContextHandle> Lock, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RNotifyBootConfigStatus(string lpMachineName, uint BootAcceptable, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum10NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RChangeServiceConfigW(RpcContextHandle hService, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, string lpDisplayName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RCreateServiceW(RpcContextHandle hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> REnumDependentServicesW(RpcContextHandle hService, uint dwServiceState, RpcPointer<byte[]> lpServices, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> REnumServicesStatusW(RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> ROpenSCManagerW(string lpMachineName, string lpDatabaseName, uint dwDesiredAccess, RpcPointer<RpcContextHandle> lpScHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> ROpenServiceW(RpcContextHandle hSCManager, string lpServiceName, uint dwDesiredAccess, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RQueryServiceConfigW(RpcContextHandle hService, RpcPointer<QUERY_SERVICE_CONFIGW> lpServiceConfig, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RQueryServiceLockStatusW(RpcContextHandle hSCManager, RpcPointer<QUERY_SERVICE_LOCK_STATUSW> lpLockStatus, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RStartServiceW(RpcContextHandle hService, uint argc, STRING_PTRSW[] argv, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RGetServiceDisplayNameW(RpcContextHandle hSCManager, string lpServiceName, RpcPointer<string> lpDisplayName, RpcPointer<uint> lpcchBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RGetServiceKeyNameW(RpcContextHandle hSCManager, string lpDisplayName, RpcPointer<string> lpServiceName, RpcPointer<uint> lpcchBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum22NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RChangeServiceConfigA(RpcContextHandle hService, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, string lpDisplayName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RCreateServiceA(RpcContextHandle hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> REnumDependentServicesA(RpcContextHandle hService, uint dwServiceState, RpcPointer<byte[]> lpServices, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> REnumServicesStatusA(RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> ROpenSCManagerA(string lpMachineName, string lpDatabaseName, uint dwDesiredAccess, RpcPointer<RpcContextHandle> lpScHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> ROpenServiceA(RpcContextHandle hSCManager, string lpServiceName, uint dwDesiredAccess, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RQueryServiceConfigA(RpcContextHandle hService, RpcPointer<QUERY_SERVICE_CONFIGA> lpServiceConfig, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RQueryServiceLockStatusA(RpcContextHandle hSCManager, RpcPointer<QUERY_SERVICE_LOCK_STATUSA> lpLockStatus, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RStartServiceA(RpcContextHandle hService, uint argc, STRING_PTRSA[] argv, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RGetServiceDisplayNameA(RpcContextHandle hSCManager, string lpServiceName, RpcPointer<string> lpDisplayName, RpcPointer<uint> lpcchBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RGetServiceKeyNameA(RpcContextHandle hSCManager, string lpDisplayName, RpcPointer<string> lpKeyName, RpcPointer<uint> lpcchBuffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum34NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> REnumServiceGroupW(RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RChangeServiceConfig2A(RpcContextHandle hService, SC_RPC_CONFIG_INFOA Info, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RChangeServiceConfig2W(RpcContextHandle hService, SC_RPC_CONFIG_INFOW Info, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RQueryServiceConfig2A(RpcContextHandle hService, uint dwInfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RQueryServiceConfig2W(RpcContextHandle hService, uint dwInfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RQueryServiceStatusEx(RpcContextHandle hService, SC_STATUS_TYPE InfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> REnumServicesStatusExA(RpcContextHandle hSCManager, SC_ENUM_TYPE InfoLevel, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> REnumServicesStatusExW(RpcContextHandle hSCManager, SC_ENUM_TYPE InfoLevel, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum43NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RCreateServiceWOW64A(RpcContextHandle hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RCreateServiceWOW64W(RpcContextHandle hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum46NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RNotifyServiceStatusChange(RpcContextHandle hService, SC_RPC_NOTIFY_PARAMS NotifyParams, Guid pClientProcessGuid, RpcPointer<Guid> pSCMProcessGuid, RpcPointer<int> pfCreateRemoteQueue, RpcPointer<RpcContextHandle> phNotify, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> RGetNotifyResults(RpcContextHandle hNotify, RpcPointer<RpcPointer<SC_RPC_NOTIFY_PARAMS_LIST>> ppNotifyParams, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RCloseNotifyHandle(RpcPointer<RpcContextHandle> phNotify, RpcPointer<int> pfApcFired, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RControlServiceExA(RpcContextHandle hService, uint dwControl, uint dwInfoLevel, SC_RPC_SERVICE_CONTROL_IN_PARAMSA pControlInParams, RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSA> pControlOutParams, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RControlServiceExW(RpcContextHandle hService, uint dwControl, uint dwInfoLevel, SC_RPC_SERVICE_CONTROL_IN_PARAMSW pControlInParams, RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSW> pControlOutParams, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum52NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum53NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum54NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum55NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RQueryServiceConfigEx(RpcContextHandle hService, uint dwInfoLevel, RpcPointer<SC_RPC_CONFIG_INFOW> pInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum57NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum58NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum59NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> RCreateWowService(RpcContextHandle hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, ushort dwServiceWowType, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum61NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum62NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum63NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> ROpenSCManager2(string DatabaseName, uint DesiredAccess, RpcPointer<RpcContextHandle> ScmHandle, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("367abb81-9844-35f1-ad32-98f038001003")]
	public partial class svcctlClientProxy : Titanis.DceRpc.Client.RpcClientProxy, svcctl, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RCloseServiceHandle(RpcPointer<RpcContextHandle> hSCObject, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCObject.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			hSCObject.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RControlService(RpcContextHandle hService, uint dwControl, RpcPointer<SERVICE_STATUS> lpServiceStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwControl);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpServiceStatus.value = decoder.ReadFixedStruct<SERVICE_STATUS>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<SERVICE_STATUS>(ref lpServiceStatus.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RDeleteService(RpcContextHandle hService, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RLockServiceDatabase(RpcContextHandle hSCManager, RpcPointer<RpcContextHandle> lpLock, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpLock.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RQueryServiceObjectSecurity(RpcContextHandle hService, uint dwSecurityInformation, RpcPointer<byte[]> lpSecurityDescriptor, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwSecurityInformation);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpSecurityDescriptor.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpSecurityDescriptor.value.Length; i++)
			{
				byte elem_0 = lpSecurityDescriptor.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpSecurityDescriptor.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RSetServiceObjectSecurity(RpcContextHandle hService, uint dwSecurityInformation, byte[] lpSecurityDescriptor, uint cbBufSize, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwSecurityInformation);
			if (lpSecurityDescriptor is not null)
			{
				encoder.WriteArrayHeader(lpSecurityDescriptor);
				for (int i = 0; i < lpSecurityDescriptor.Length; i++)
				{
					byte elem_0 = lpSecurityDescriptor[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RQueryServiceStatus(RpcContextHandle hService, RpcPointer<SERVICE_STATUS> lpServiceStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpServiceStatus.value = decoder.ReadFixedStruct<SERVICE_STATUS>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<SERVICE_STATUS>(ref lpServiceStatus.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RSetServiceStatus(RpcContextHandle hServiceStatus, SERVICE_STATUS lpServiceStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hServiceStatus);
			encoder.WriteFixedStruct(lpServiceStatus, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(lpServiceStatus);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RUnlockServiceDatabase(RpcPointer<RpcContextHandle> Lock, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(Lock.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Lock.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RNotifyBootConfigStatus(string lpMachineName, uint BootAcceptable, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(lpMachineName is null);
			if (lpMachineName is not null)
				encoder.WriteWideCharString(lpMachineName);
			encoder.WriteValue(BootAcceptable);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum10NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RChangeServiceConfigW(RpcContextHandle hService, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, string lpDisplayName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwStartType);
			encoder.WriteValue(dwErrorControl);
			encoder.WriteUniqueReferentId(lpBinaryPathName is null);
			if (lpBinaryPathName is not null)
				encoder.WriteWideCharString(lpBinaryPathName);
			encoder.WriteUniqueReferentId(lpLoadOrderGroup is null);
			if (lpLoadOrderGroup is not null)
				encoder.WriteWideCharString(lpLoadOrderGroup);
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteUniqueReferentId(lpDependencies is null);
			if (lpDependencies is not null)
			{
				encoder.WriteArrayHeader(lpDependencies);
				for (int i = 0; i < lpDependencies.Length; i++)
				{
					byte elem_0 = lpDependencies[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwDependSize);
			encoder.WriteUniqueReferentId(lpServiceStartName is null);
			if (lpServiceStartName is not null)
				encoder.WriteWideCharString(lpServiceStartName);
			encoder.WriteUniqueReferentId(lpPassword is null);
			if (lpPassword is not null)
			{
				encoder.WriteArrayHeader(lpPassword);
				for (int i = 0; i < lpPassword.Length; i++)
				{
					byte elem_0 = lpPassword[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwPwSize);
			encoder.WriteUniqueReferentId(lpDisplayName is null);
			if (lpDisplayName is not null)
				encoder.WriteWideCharString(lpDisplayName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpdwTagId = decoder.ReadOutUniquePointer<uint>(lpdwTagId);
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RCreateServiceW(RpcContextHandle hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteWideCharString(lpServiceName);
			encoder.WriteUniqueReferentId(lpDisplayName is null);
			if (lpDisplayName is not null)
				encoder.WriteWideCharString(lpDisplayName);
			encoder.WriteValue(dwDesiredAccess);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwStartType);
			encoder.WriteValue(dwErrorControl);
			encoder.WriteWideCharString(lpBinaryPathName);
			encoder.WriteUniqueReferentId(lpLoadOrderGroup is null);
			if (lpLoadOrderGroup is not null)
				encoder.WriteWideCharString(lpLoadOrderGroup);
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteUniqueReferentId(lpDependencies is null);
			if (lpDependencies is not null)
			{
				encoder.WriteArrayHeader(lpDependencies);
				for (int i = 0; i < lpDependencies.Length; i++)
				{
					byte elem_0 = lpDependencies[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwDependSize);
			encoder.WriteUniqueReferentId(lpServiceStartName is null);
			if (lpServiceStartName is not null)
				encoder.WriteWideCharString(lpServiceStartName);
			encoder.WriteUniqueReferentId(lpPassword is null);
			if (lpPassword is not null)
			{
				encoder.WriteArrayHeader(lpPassword);
				for (int i = 0; i < lpPassword.Length; i++)
				{
					byte elem_0 = lpPassword[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwPwSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpdwTagId = decoder.ReadOutUniquePointer<uint>(lpdwTagId);
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpServiceHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> REnumDependentServicesW(RpcContextHandle hService, uint dwServiceState, RpcPointer<byte[]> lpServices, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwServiceState);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpServices.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpServices.value.Length; i++)
			{
				byte elem_0 = lpServices.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpServices.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			lpServicesReturned.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> REnumServicesStatusW(RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwServiceState);
			encoder.WriteValue(cbBufSize);
			encoder.WriteUniquePointer(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				encoder.WriteValue(lpResumeIndex.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpBuffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpBuffer.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			lpServicesReturned.value = decoder.ReadUInt32();
			lpResumeIndex = decoder.ReadOutUniquePointer<uint>(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				lpResumeIndex.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> ROpenSCManagerW(string lpMachineName, string lpDatabaseName, uint dwDesiredAccess, RpcPointer<RpcContextHandle> lpScHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(lpMachineName is null);
			if (lpMachineName is not null)
				encoder.WriteWideCharString(lpMachineName);
			encoder.WriteUniqueReferentId(lpDatabaseName is null);
			if (lpDatabaseName is not null)
				encoder.WriteWideCharString(lpDatabaseName);
			encoder.WriteValue(dwDesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpScHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> ROpenServiceW(RpcContextHandle hSCManager, string lpServiceName, uint dwDesiredAccess, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteWideCharString(lpServiceName);
			encoder.WriteValue(dwDesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpServiceHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RQueryServiceConfigW(RpcContextHandle hService, RpcPointer<QUERY_SERVICE_CONFIGW> lpServiceConfig, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpServiceConfig.value = decoder.ReadFixedStruct<QUERY_SERVICE_CONFIGW>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<QUERY_SERVICE_CONFIGW>(ref lpServiceConfig.value);
			pcbBytesNeeded.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RQueryServiceLockStatusW(RpcContextHandle hSCManager, RpcPointer<QUERY_SERVICE_LOCK_STATUSW> lpLockStatus, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpLockStatus.value = decoder.ReadFixedStruct<QUERY_SERVICE_LOCK_STATUSW>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<QUERY_SERVICE_LOCK_STATUSW>(ref lpLockStatus.value);
			pcbBytesNeeded.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RStartServiceW(RpcContextHandle hService, uint argc, STRING_PTRSW[] argv, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(argc);
			encoder.WriteUniqueReferentId(argv is null);
			if (argv is not null)
			{
				encoder.WriteArrayHeader(argv);
				for (int i = 0; i < argv.Length; i++)
				{
					STRING_PTRSW elem_0 = argv[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}
			}

			for (int i = 0; i < argv.Length; i++)
			{
				STRING_PTRSW elem_0 = argv[i];
				encoder.WriteStructDeferral(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RGetServiceDisplayNameW(RpcContextHandle hSCManager, string lpServiceName, RpcPointer<string> lpDisplayName, RpcPointer<uint> lpcchBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteWideCharString(lpServiceName);
			encoder.WriteValue(lpcchBuffer.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpDisplayName.value = decoder.ReadWideCharString();
			lpcchBuffer.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RGetServiceKeyNameW(RpcContextHandle hSCManager, string lpDisplayName, RpcPointer<string> lpServiceName, RpcPointer<uint> lpcchBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteWideCharString(lpDisplayName);
			encoder.WriteValue(lpcchBuffer.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpServiceName.value = decoder.ReadWideCharString();
			lpcchBuffer.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum22NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RChangeServiceConfigA(RpcContextHandle hService, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, string lpDisplayName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwStartType);
			encoder.WriteValue(dwErrorControl);
			encoder.WriteUniqueReferentId(lpBinaryPathName is null);
			if (lpBinaryPathName is not null)
				encoder.WriteUnsignedCharString(lpBinaryPathName);
			encoder.WriteUniqueReferentId(lpLoadOrderGroup is null);
			if (lpLoadOrderGroup is not null)
				encoder.WriteUnsignedCharString(lpLoadOrderGroup);
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteUniqueReferentId(lpDependencies is null);
			if (lpDependencies is not null)
			{
				encoder.WriteArrayHeader(lpDependencies);
				for (int i = 0; i < lpDependencies.Length; i++)
				{
					byte elem_0 = lpDependencies[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwDependSize);
			encoder.WriteUniqueReferentId(lpServiceStartName is null);
			if (lpServiceStartName is not null)
				encoder.WriteUnsignedCharString(lpServiceStartName);
			encoder.WriteUniqueReferentId(lpPassword is null);
			if (lpPassword is not null)
			{
				encoder.WriteArrayHeader(lpPassword);
				for (int i = 0; i < lpPassword.Length; i++)
				{
					byte elem_0 = lpPassword[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwPwSize);
			encoder.WriteUniqueReferentId(lpDisplayName is null);
			if (lpDisplayName is not null)
				encoder.WriteUnsignedCharString(lpDisplayName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpdwTagId = decoder.ReadOutUniquePointer<uint>(lpdwTagId);
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RCreateServiceA(RpcContextHandle hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteUnsignedCharString(lpServiceName);
			encoder.WriteUniqueReferentId(lpDisplayName is null);
			if (lpDisplayName is not null)
				encoder.WriteUnsignedCharString(lpDisplayName);
			encoder.WriteValue(dwDesiredAccess);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwStartType);
			encoder.WriteValue(dwErrorControl);
			encoder.WriteUnsignedCharString(lpBinaryPathName);
			encoder.WriteUniqueReferentId(lpLoadOrderGroup is null);
			if (lpLoadOrderGroup is not null)
				encoder.WriteUnsignedCharString(lpLoadOrderGroup);
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteUniqueReferentId(lpDependencies is null);
			if (lpDependencies is not null)
			{
				encoder.WriteArrayHeader(lpDependencies);
				for (int i = 0; i < lpDependencies.Length; i++)
				{
					byte elem_0 = lpDependencies[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwDependSize);
			encoder.WriteUniqueReferentId(lpServiceStartName is null);
			if (lpServiceStartName is not null)
				encoder.WriteUnsignedCharString(lpServiceStartName);
			encoder.WriteUniqueReferentId(lpPassword is null);
			if (lpPassword is not null)
			{
				encoder.WriteArrayHeader(lpPassword);
				for (int i = 0; i < lpPassword.Length; i++)
				{
					byte elem_0 = lpPassword[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwPwSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpdwTagId = decoder.ReadOutUniquePointer<uint>(lpdwTagId);
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpServiceHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> REnumDependentServicesA(RpcContextHandle hService, uint dwServiceState, RpcPointer<byte[]> lpServices, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwServiceState);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpServices.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpServices.value.Length; i++)
			{
				byte elem_0 = lpServices.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpServices.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			lpServicesReturned.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> REnumServicesStatusA(RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwServiceState);
			encoder.WriteValue(cbBufSize);
			encoder.WriteUniquePointer(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				encoder.WriteValue(lpResumeIndex.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpBuffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpBuffer.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			lpServicesReturned.value = decoder.ReadUInt32();
			lpResumeIndex = decoder.ReadOutUniquePointer<uint>(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				lpResumeIndex.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> ROpenSCManagerA(string lpMachineName, string lpDatabaseName, uint dwDesiredAccess, RpcPointer<RpcContextHandle> lpScHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(lpMachineName is null);
			if (lpMachineName is not null)
				encoder.WriteUnsignedCharString(lpMachineName);
			encoder.WriteUniqueReferentId(lpDatabaseName is null);
			if (lpDatabaseName is not null)
				encoder.WriteUnsignedCharString(lpDatabaseName);
			encoder.WriteValue(dwDesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpScHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> ROpenServiceA(RpcContextHandle hSCManager, string lpServiceName, uint dwDesiredAccess, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteUnsignedCharString(lpServiceName);
			encoder.WriteValue(dwDesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpServiceHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RQueryServiceConfigA(RpcContextHandle hService, RpcPointer<QUERY_SERVICE_CONFIGA> lpServiceConfig, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpServiceConfig.value = decoder.ReadFixedStruct<QUERY_SERVICE_CONFIGA>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<QUERY_SERVICE_CONFIGA>(ref lpServiceConfig.value);
			pcbBytesNeeded.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RQueryServiceLockStatusA(RpcContextHandle hSCManager, RpcPointer<QUERY_SERVICE_LOCK_STATUSA> lpLockStatus, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpLockStatus.value = decoder.ReadFixedStruct<QUERY_SERVICE_LOCK_STATUSA>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<QUERY_SERVICE_LOCK_STATUSA>(ref lpLockStatus.value);
			pcbBytesNeeded.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RStartServiceA(RpcContextHandle hService, uint argc, STRING_PTRSA[] argv, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(argc);
			encoder.WriteUniqueReferentId(argv is null);
			if (argv is not null)
			{
				encoder.WriteArrayHeader(argv);
				for (int i = 0; i < argv.Length; i++)
				{
					STRING_PTRSA elem_0 = argv[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}
			}

			for (int i = 0; i < argv.Length; i++)
			{
				STRING_PTRSA elem_0 = argv[i];
				encoder.WriteStructDeferral(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RGetServiceDisplayNameA(RpcContextHandle hSCManager, string lpServiceName, RpcPointer<string> lpDisplayName, RpcPointer<uint> lpcchBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteUnsignedCharString(lpServiceName);
			encoder.WriteValue(lpcchBuffer.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpDisplayName.value = decoder.ReadUnsignedCharString();
			lpcchBuffer.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RGetServiceKeyNameA(RpcContextHandle hSCManager, string lpDisplayName, RpcPointer<string> lpKeyName, RpcPointer<uint> lpcchBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteUnsignedCharString(lpDisplayName);
			encoder.WriteValue(lpcchBuffer.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpKeyName.value = decoder.ReadUnsignedCharString();
			lpcchBuffer.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum34NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> REnumServiceGroupW(RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwServiceState);
			encoder.WriteValue(cbBufSize);
			encoder.WriteUniquePointer(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				encoder.WriteValue(lpResumeIndex.value);
			}

			encoder.WriteUniqueReferentId(pszGroupName is null);
			if (pszGroupName is not null)
				encoder.WriteWideCharString(pszGroupName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpBuffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpBuffer.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			lpServicesReturned.value = decoder.ReadUInt32();
			lpResumeIndex = decoder.ReadOutUniquePointer<uint>(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				lpResumeIndex.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RChangeServiceConfig2A(RpcContextHandle hService, SC_RPC_CONFIG_INFOA Info, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteFixedStruct(Info, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Info);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RChangeServiceConfig2W(RpcContextHandle hService, SC_RPC_CONFIG_INFOW Info, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(37);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteFixedStruct(Info, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Info);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RQueryServiceConfig2A(RpcContextHandle hService, uint dwInfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(38);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwInfoLevel);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpBuffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpBuffer.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RQueryServiceConfig2W(RpcContextHandle hService, uint dwInfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(39);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwInfoLevel);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpBuffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpBuffer.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RQueryServiceStatusEx(RpcContextHandle hService, SC_STATUS_TYPE InfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(40);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue((int)InfoLevel);
			encoder.WriteValue(cbBufSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpBuffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpBuffer.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> REnumServicesStatusExA(RpcContextHandle hSCManager, SC_ENUM_TYPE InfoLevel, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(41);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteValue((int)InfoLevel);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwServiceState);
			encoder.WriteValue(cbBufSize);
			encoder.WriteUniquePointer(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				encoder.WriteValue(lpResumeIndex.value);
			}

			encoder.WriteUniqueReferentId(pszGroupName is null);
			if (pszGroupName is not null)
				encoder.WriteUnsignedCharString(pszGroupName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpBuffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpBuffer.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			lpServicesReturned.value = decoder.ReadUInt32();
			lpResumeIndex = decoder.ReadOutUniquePointer<uint>(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				lpResumeIndex.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> REnumServicesStatusExW(RpcContextHandle hSCManager, SC_ENUM_TYPE InfoLevel, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(42);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteValue((int)InfoLevel);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwServiceState);
			encoder.WriteValue(cbBufSize);
			encoder.WriteUniquePointer(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				encoder.WriteValue(lpResumeIndex.value);
			}

			encoder.WriteUniqueReferentId(pszGroupName is null);
			if (pszGroupName is not null)
				encoder.WriteWideCharString(pszGroupName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpBuffer.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpBuffer.value[i] = elem_0;
			}

			pcbBytesNeeded.value = decoder.ReadUInt32();
			lpServicesReturned.value = decoder.ReadUInt32();
			lpResumeIndex = decoder.ReadOutUniquePointer<uint>(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				lpResumeIndex.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum43NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(43);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RCreateServiceWOW64A(RpcContextHandle hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(44);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteUnsignedCharString(lpServiceName);
			encoder.WriteUniqueReferentId(lpDisplayName is null);
			if (lpDisplayName is not null)
				encoder.WriteUnsignedCharString(lpDisplayName);
			encoder.WriteValue(dwDesiredAccess);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwStartType);
			encoder.WriteValue(dwErrorControl);
			encoder.WriteUnsignedCharString(lpBinaryPathName);
			encoder.WriteUniqueReferentId(lpLoadOrderGroup is null);
			if (lpLoadOrderGroup is not null)
				encoder.WriteUnsignedCharString(lpLoadOrderGroup);
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteUniqueReferentId(lpDependencies is null);
			if (lpDependencies is not null)
			{
				encoder.WriteArrayHeader(lpDependencies);
				for (int i = 0; i < lpDependencies.Length; i++)
				{
					byte elem_0 = lpDependencies[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwDependSize);
			encoder.WriteUniqueReferentId(lpServiceStartName is null);
			if (lpServiceStartName is not null)
				encoder.WriteUnsignedCharString(lpServiceStartName);
			encoder.WriteUniqueReferentId(lpPassword is null);
			if (lpPassword is not null)
			{
				encoder.WriteArrayHeader(lpPassword);
				for (int i = 0; i < lpPassword.Length; i++)
				{
					byte elem_0 = lpPassword[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwPwSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpdwTagId = decoder.ReadOutUniquePointer<uint>(lpdwTagId);
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpServiceHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RCreateServiceWOW64W(RpcContextHandle hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(45);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteWideCharString(lpServiceName);
			encoder.WriteUniqueReferentId(lpDisplayName is null);
			if (lpDisplayName is not null)
				encoder.WriteWideCharString(lpDisplayName);
			encoder.WriteValue(dwDesiredAccess);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwStartType);
			encoder.WriteValue(dwErrorControl);
			encoder.WriteWideCharString(lpBinaryPathName);
			encoder.WriteUniqueReferentId(lpLoadOrderGroup is null);
			if (lpLoadOrderGroup is not null)
				encoder.WriteWideCharString(lpLoadOrderGroup);
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteUniqueReferentId(lpDependencies is null);
			if (lpDependencies is not null)
			{
				encoder.WriteArrayHeader(lpDependencies);
				for (int i = 0; i < lpDependencies.Length; i++)
				{
					byte elem_0 = lpDependencies[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwDependSize);
			encoder.WriteUniqueReferentId(lpServiceStartName is null);
			if (lpServiceStartName is not null)
				encoder.WriteWideCharString(lpServiceStartName);
			encoder.WriteUniqueReferentId(lpPassword is null);
			if (lpPassword is not null)
			{
				encoder.WriteArrayHeader(lpPassword);
				for (int i = 0; i < lpPassword.Length; i++)
				{
					byte elem_0 = lpPassword[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwPwSize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpdwTagId = decoder.ReadOutUniquePointer<uint>(lpdwTagId);
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpServiceHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum46NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(46);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RNotifyServiceStatusChange(RpcContextHandle hService, SC_RPC_NOTIFY_PARAMS NotifyParams, Guid pClientProcessGuid, RpcPointer<Guid> pSCMProcessGuid, RpcPointer<int> pfCreateRemoteQueue, RpcPointer<RpcContextHandle> phNotify, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(47);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteFixedStruct(NotifyParams, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(NotifyParams);
			encoder.WriteValue(pClientProcessGuid);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pSCMProcessGuid.value = decoder.ReadUuid();
			pfCreateRemoteQueue.value = decoder.ReadInt32();
			phNotify.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> RGetNotifyResults(RpcContextHandle hNotify, RpcPointer<RpcPointer<SC_RPC_NOTIFY_PARAMS_LIST>> ppNotifyParams, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(48);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hNotify);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppNotifyParams.value = decoder.ReadOutUniquePointer<SC_RPC_NOTIFY_PARAMS_LIST>(ppNotifyParams.value);
			if (ppNotifyParams.value is not null)
			{
				ppNotifyParams.value.value = decoder.ReadConformantStruct<SC_RPC_NOTIFY_PARAMS_LIST>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<SC_RPC_NOTIFY_PARAMS_LIST>(ref ppNotifyParams.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RCloseNotifyHandle(RpcPointer<RpcContextHandle> phNotify, RpcPointer<int> pfApcFired, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(49);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(phNotify.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phNotify.value = decoder.ReadContextHandle();
			pfApcFired.value = decoder.ReadInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RControlServiceExA(RpcContextHandle hService, uint dwControl, uint dwInfoLevel, SC_RPC_SERVICE_CONTROL_IN_PARAMSA pControlInParams, RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSA> pControlOutParams, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(50);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwControl);
			encoder.WriteValue(dwInfoLevel);
			encoder.WriteUnion(pControlInParams);
			encoder.WriteStructDeferral(pControlInParams);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pControlOutParams.value = decoder.ReadUnion<SC_RPC_SERVICE_CONTROL_OUT_PARAMSA>();
			decoder.ReadStructDeferral<SC_RPC_SERVICE_CONTROL_OUT_PARAMSA>(ref pControlOutParams.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RControlServiceExW(RpcContextHandle hService, uint dwControl, uint dwInfoLevel, SC_RPC_SERVICE_CONTROL_IN_PARAMSW pControlInParams, RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSW> pControlOutParams, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(51);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwControl);
			encoder.WriteValue(dwInfoLevel);
			encoder.WriteUnion(pControlInParams);
			encoder.WriteStructDeferral(pControlInParams);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pControlOutParams.value = decoder.ReadUnion<SC_RPC_SERVICE_CONTROL_OUT_PARAMSW>();
			decoder.ReadStructDeferral<SC_RPC_SERVICE_CONTROL_OUT_PARAMSW>(ref pControlOutParams.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum52NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(52);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum53NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(53);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum54NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(54);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum55NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(55);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RQueryServiceConfigEx(RpcContextHandle hService, uint dwInfoLevel, RpcPointer<SC_RPC_CONFIG_INFOW> pInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(56);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hService);
			encoder.WriteValue(dwInfoLevel);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pInfo.value = decoder.ReadFixedStruct<SC_RPC_CONFIG_INFOW>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SC_RPC_CONFIG_INFOW>(ref pInfo.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum57NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(57);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum58NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(58);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum59NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(59);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> RCreateWowService(RpcContextHandle hSCManager, string lpServiceName, string lpDisplayName, uint dwDesiredAccess, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, ushort dwServiceWowType, RpcPointer<RpcContextHandle> lpServiceHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(60);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hSCManager);
			encoder.WriteWideCharString(lpServiceName);
			encoder.WriteUniqueReferentId(lpDisplayName is null);
			if (lpDisplayName is not null)
				encoder.WriteWideCharString(lpDisplayName);
			encoder.WriteValue(dwDesiredAccess);
			encoder.WriteValue(dwServiceType);
			encoder.WriteValue(dwStartType);
			encoder.WriteValue(dwErrorControl);
			encoder.WriteWideCharString(lpBinaryPathName);
			encoder.WriteUniqueReferentId(lpLoadOrderGroup is null);
			if (lpLoadOrderGroup is not null)
				encoder.WriteWideCharString(lpLoadOrderGroup);
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteUniqueReferentId(lpDependencies is null);
			if (lpDependencies is not null)
			{
				encoder.WriteArrayHeader(lpDependencies);
				for (int i = 0; i < lpDependencies.Length; i++)
				{
					byte elem_0 = lpDependencies[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwDependSize);
			encoder.WriteUniqueReferentId(lpServiceStartName is null);
			if (lpServiceStartName is not null)
				encoder.WriteWideCharString(lpServiceStartName);
			encoder.WriteUniqueReferentId(lpPassword is null);
			if (lpPassword is not null)
			{
				encoder.WriteArrayHeader(lpPassword);
				for (int i = 0; i < lpPassword.Length; i++)
				{
					byte elem_0 = lpPassword[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(dwPwSize);
			encoder.WriteValue(dwServiceWowType);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpdwTagId = decoder.ReadOutUniquePointer<uint>(lpdwTagId);
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpServiceHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum61NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(58);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum62NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(58);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum63NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(58);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> ROpenSCManager2(string DatabaseName, uint DesiredAccess, RpcPointer<RpcContextHandle> ScmHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(64);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(DatabaseName is null);
			if (DatabaseName is not null)
				encoder.WriteWideCharString(DatabaseName);
			encoder.WriteValue(DesiredAccess);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ScmHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(svcctl);
		private static Guid _interfaceUuid = new Guid("367abb81-9844-35f1-ad32-98f038001003");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(2, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class svcctlStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RCloseServiceHandle(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> hSCObject;
			hSCObject = new RpcPointer<RpcContextHandle>();
			hSCObject.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.RCloseServiceHandle(hSCObject, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(hSCObject.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RControlService(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwControl;
			RpcPointer<SERVICE_STATUS> lpServiceStatus = new RpcPointer<SERVICE_STATUS>();
			hService = decoder.ReadContextHandle();
			dwControl = decoder.ReadUInt32();
			var invokeTask = this._obj.RControlService(hService, dwControl, lpServiceStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(lpServiceStatus.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(lpServiceStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RDeleteService(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			hService = decoder.ReadContextHandle();
			var invokeTask = this._obj.RDeleteService(hService, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RLockServiceDatabase(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			RpcPointer<RpcContextHandle> lpLock = new RpcPointer<RpcContextHandle>();
			hSCManager = decoder.ReadContextHandle();
			var invokeTask = this._obj.RLockServiceDatabase(hSCManager, lpLock, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(lpLock.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RQueryServiceObjectSecurity(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwSecurityInformation;
			RpcPointer<byte[]> lpSecurityDescriptor = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			hService = decoder.ReadContextHandle();
			dwSecurityInformation = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RQueryServiceObjectSecurity(hService, dwSecurityInformation, lpSecurityDescriptor, cbBufSize, pcbBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpSecurityDescriptor.value);
			for (int i = 0; i < lpSecurityDescriptor.value.Length; i++)
			{
				byte elem_0 = lpSecurityDescriptor.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RSetServiceObjectSecurity(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwSecurityInformation;
			byte[] lpSecurityDescriptor;
			uint cbBufSize;
			hService = decoder.ReadContextHandle();
			dwSecurityInformation = decoder.ReadUInt32();
			lpSecurityDescriptor = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpSecurityDescriptor.Length; i++)
			{
				byte elem_0 = lpSecurityDescriptor[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpSecurityDescriptor[i] = elem_0;
			}

			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RSetServiceObjectSecurity(hService, dwSecurityInformation, lpSecurityDescriptor, cbBufSize, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RQueryServiceStatus(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			RpcPointer<SERVICE_STATUS> lpServiceStatus = new RpcPointer<SERVICE_STATUS>();
			hService = decoder.ReadContextHandle();
			var invokeTask = this._obj.RQueryServiceStatus(hService, lpServiceStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(lpServiceStatus.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(lpServiceStatus.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RSetServiceStatus(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hServiceStatus;
			SERVICE_STATUS lpServiceStatus;
			hServiceStatus = decoder.ReadContextHandle();
			lpServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<SERVICE_STATUS>(ref lpServiceStatus);
			var invokeTask = this._obj.RSetServiceStatus(hServiceStatus, lpServiceStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RUnlockServiceDatabase(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> Lock;
			Lock = new RpcPointer<RpcContextHandle>();
			Lock.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.RUnlockServiceDatabase(Lock, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(Lock.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RNotifyBootConfigStatus(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string lpMachineName;
			uint BootAcceptable;
			if (decoder.ReadReferentId() == 0)
				lpMachineName = null;
			else
				lpMachineName = decoder.ReadWideCharString();
			BootAcceptable = decoder.ReadUInt32();
			var invokeTask = this._obj.RNotifyBootConfigStatus(lpMachineName, BootAcceptable, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum10NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum10NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RChangeServiceConfigW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwServiceType;
			uint dwStartType;
			uint dwErrorControl;
			string lpBinaryPathName;
			string lpLoadOrderGroup;
			RpcPointer<uint> lpdwTagId;
			byte[] lpDependencies;
			uint dwDependSize;
			string lpServiceStartName;
			byte[] lpPassword;
			uint dwPwSize;
			string lpDisplayName;
			hService = decoder.ReadContextHandle();
			dwServiceType = decoder.ReadUInt32();
			dwStartType = decoder.ReadUInt32();
			dwErrorControl = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpBinaryPathName = null;
			else
				lpBinaryPathName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				lpLoadOrderGroup = null;
			else
				lpLoadOrderGroup = decoder.ReadWideCharString();
			lpdwTagId = decoder.ReadUniquePointer<uint>();
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpDependencies = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpDependencies.Length; i++)
			{
				byte elem_0 = lpDependencies[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpDependencies[i] = elem_0;
			}

			dwDependSize = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpServiceStartName = null;
			else
				lpServiceStartName = decoder.ReadWideCharString();
			lpPassword = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpPassword.Length; i++)
			{
				byte elem_0 = lpPassword[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpPassword[i] = elem_0;
			}

			dwPwSize = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpDisplayName = null;
			else
				lpDisplayName = decoder.ReadWideCharString();
			var invokeTask = this._obj.RChangeServiceConfigW(hService, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpDisplayName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RCreateServiceW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpServiceName;
			string lpDisplayName;
			uint dwDesiredAccess;
			uint dwServiceType;
			uint dwStartType;
			uint dwErrorControl;
			string lpBinaryPathName;
			string lpLoadOrderGroup;
			RpcPointer<uint> lpdwTagId;
			byte[] lpDependencies;
			uint dwDependSize;
			string lpServiceStartName;
			byte[] lpPassword;
			uint dwPwSize;
			RpcPointer<RpcContextHandle> lpServiceHandle = new RpcPointer<RpcContextHandle>();
			hSCManager = decoder.ReadContextHandle();
			lpServiceName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				lpDisplayName = null;
			else
				lpDisplayName = decoder.ReadWideCharString();
			dwDesiredAccess = decoder.ReadUInt32();
			dwServiceType = decoder.ReadUInt32();
			dwStartType = decoder.ReadUInt32();
			dwErrorControl = decoder.ReadUInt32();
			lpBinaryPathName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				lpLoadOrderGroup = null;
			else
				lpLoadOrderGroup = decoder.ReadWideCharString();
			lpdwTagId = decoder.ReadUniquePointer<uint>();
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpDependencies = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpDependencies.Length; i++)
			{
				byte elem_0 = lpDependencies[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpDependencies[i] = elem_0;
			}

			dwDependSize = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpServiceStartName = null;
			else
				lpServiceStartName = decoder.ReadWideCharString();
			lpPassword = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpPassword.Length; i++)
			{
				byte elem_0 = lpPassword[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpPassword[i] = elem_0;
			}

			dwPwSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RCreateServiceW(hSCManager, lpServiceName, lpDisplayName, dwDesiredAccess, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpServiceHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteContextHandle(lpServiceHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_REnumDependentServicesW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwServiceState;
			RpcPointer<byte[]> lpServices = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			RpcPointer<uint> lpServicesReturned = new RpcPointer<uint>();
			hService = decoder.ReadContextHandle();
			dwServiceState = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.REnumDependentServicesW(hService, dwServiceState, lpServices, cbBufSize, pcbBytesNeeded, lpServicesReturned, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpServices.value);
			for (int i = 0; i < lpServices.value.Length; i++)
			{
				byte elem_0 = lpServices.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(lpServicesReturned.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_REnumServicesStatusW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			uint dwServiceType;
			uint dwServiceState;
			RpcPointer<byte[]> lpBuffer = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			RpcPointer<uint> lpServicesReturned = new RpcPointer<uint>();
			RpcPointer<uint> lpResumeIndex;
			hSCManager = decoder.ReadContextHandle();
			dwServiceType = decoder.ReadUInt32();
			dwServiceState = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			lpResumeIndex = decoder.ReadUniquePointer<uint>();
			if (lpResumeIndex is not null)
			{
				lpResumeIndex.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.REnumServicesStatusW(hSCManager, dwServiceType, dwServiceState, lpBuffer, cbBufSize, pcbBytesNeeded, lpServicesReturned, lpResumeIndex, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpBuffer.value);
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(lpServicesReturned.value);
			encoder.WriteUniquePointer(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				encoder.WriteValue(lpResumeIndex.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ROpenSCManagerW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string lpMachineName;
			string lpDatabaseName;
			uint dwDesiredAccess;
			RpcPointer<RpcContextHandle> lpScHandle = new RpcPointer<RpcContextHandle>();
			if (decoder.ReadReferentId() == 0)
				lpMachineName = null;
			else
				lpMachineName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				lpDatabaseName = null;
			else
				lpDatabaseName = decoder.ReadWideCharString();
			dwDesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.ROpenSCManagerW(lpMachineName, lpDatabaseName, dwDesiredAccess, lpScHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(lpScHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ROpenServiceW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpServiceName;
			uint dwDesiredAccess;
			RpcPointer<RpcContextHandle> lpServiceHandle = new RpcPointer<RpcContextHandle>();
			hSCManager = decoder.ReadContextHandle();
			lpServiceName = decoder.ReadWideCharString();
			dwDesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.ROpenServiceW(hSCManager, lpServiceName, dwDesiredAccess, lpServiceHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(lpServiceHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RQueryServiceConfigW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			RpcPointer<QUERY_SERVICE_CONFIGW> lpServiceConfig = new RpcPointer<QUERY_SERVICE_CONFIGW>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			hService = decoder.ReadContextHandle();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RQueryServiceConfigW(hService, lpServiceConfig, cbBufSize, pcbBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(lpServiceConfig.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpServiceConfig.value);
			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RQueryServiceLockStatusW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			RpcPointer<QUERY_SERVICE_LOCK_STATUSW> lpLockStatus = new RpcPointer<QUERY_SERVICE_LOCK_STATUSW>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			hSCManager = decoder.ReadContextHandle();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RQueryServiceLockStatusW(hSCManager, lpLockStatus, cbBufSize, pcbBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(lpLockStatus.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpLockStatus.value);
			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RStartServiceW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint argc;
			STRING_PTRSW[] argv;
			hService = decoder.ReadContextHandle();
			argc = decoder.ReadUInt32();
			argv = decoder.ReadArrayHeader<STRING_PTRSW>();
			for (int i = 0; i < argv.Length; i++)
			{
				STRING_PTRSW elem_0 = argv[i];
				elem_0 = decoder.ReadFixedStruct<STRING_PTRSW>(NdrAlignment.NativePtr);
				argv[i] = elem_0;
			}

			for (int i = 0; i < argv.Length; i++)
			{
				STRING_PTRSW elem_0 = argv[i];
				decoder.ReadStructDeferral<STRING_PTRSW>(ref elem_0);
				argv[i] = elem_0;
			}

			var invokeTask = this._obj.RStartServiceW(hService, argc, argv, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RGetServiceDisplayNameW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpServiceName;
			RpcPointer<string> lpDisplayName = new RpcPointer<string>();
			RpcPointer<uint> lpcchBuffer;
			hSCManager = decoder.ReadContextHandle();
			lpServiceName = decoder.ReadWideCharString();
			lpcchBuffer = new RpcPointer<uint>();
			lpcchBuffer.value = decoder.ReadUInt32();
			var invokeTask = this._obj.RGetServiceDisplayNameW(hSCManager, lpServiceName, lpDisplayName, lpcchBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteWideCharString(lpDisplayName.value);
			encoder.WriteValue(lpcchBuffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RGetServiceKeyNameW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpDisplayName;
			RpcPointer<string> lpServiceName = new RpcPointer<string>();
			RpcPointer<uint> lpcchBuffer;
			hSCManager = decoder.ReadContextHandle();
			lpDisplayName = decoder.ReadWideCharString();
			lpcchBuffer = new RpcPointer<uint>();
			lpcchBuffer.value = decoder.ReadUInt32();
			var invokeTask = this._obj.RGetServiceKeyNameW(hSCManager, lpDisplayName, lpServiceName, lpcchBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteWideCharString(lpServiceName.value);
			encoder.WriteValue(lpcchBuffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum22NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum22NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RChangeServiceConfigA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwServiceType;
			uint dwStartType;
			uint dwErrorControl;
			string lpBinaryPathName;
			string lpLoadOrderGroup;
			RpcPointer<uint> lpdwTagId;
			byte[] lpDependencies;
			uint dwDependSize;
			string lpServiceStartName;
			byte[] lpPassword;
			uint dwPwSize;
			string lpDisplayName;
			hService = decoder.ReadContextHandle();
			dwServiceType = decoder.ReadUInt32();
			dwStartType = decoder.ReadUInt32();
			dwErrorControl = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpBinaryPathName = null;
			else
				lpBinaryPathName = decoder.ReadUnsignedCharString();
			if (decoder.ReadReferentId() == 0)
				lpLoadOrderGroup = null;
			else
				lpLoadOrderGroup = decoder.ReadUnsignedCharString();
			lpdwTagId = decoder.ReadUniquePointer<uint>();
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpDependencies = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpDependencies.Length; i++)
			{
				byte elem_0 = lpDependencies[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpDependencies[i] = elem_0;
			}

			dwDependSize = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpServiceStartName = null;
			else
				lpServiceStartName = decoder.ReadUnsignedCharString();
			lpPassword = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpPassword.Length; i++)
			{
				byte elem_0 = lpPassword[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpPassword[i] = elem_0;
			}

			dwPwSize = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpDisplayName = null;
			else
				lpDisplayName = decoder.ReadUnsignedCharString();
			var invokeTask = this._obj.RChangeServiceConfigA(hService, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpDisplayName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RCreateServiceA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpServiceName;
			string lpDisplayName;
			uint dwDesiredAccess;
			uint dwServiceType;
			uint dwStartType;
			uint dwErrorControl;
			string lpBinaryPathName;
			string lpLoadOrderGroup;
			RpcPointer<uint> lpdwTagId;
			byte[] lpDependencies;
			uint dwDependSize;
			string lpServiceStartName;
			byte[] lpPassword;
			uint dwPwSize;
			RpcPointer<RpcContextHandle> lpServiceHandle = new RpcPointer<RpcContextHandle>();
			hSCManager = decoder.ReadContextHandle();
			lpServiceName = decoder.ReadUnsignedCharString();
			if (decoder.ReadReferentId() == 0)
				lpDisplayName = null;
			else
				lpDisplayName = decoder.ReadUnsignedCharString();
			dwDesiredAccess = decoder.ReadUInt32();
			dwServiceType = decoder.ReadUInt32();
			dwStartType = decoder.ReadUInt32();
			dwErrorControl = decoder.ReadUInt32();
			lpBinaryPathName = decoder.ReadUnsignedCharString();
			if (decoder.ReadReferentId() == 0)
				lpLoadOrderGroup = null;
			else
				lpLoadOrderGroup = decoder.ReadUnsignedCharString();
			lpdwTagId = decoder.ReadUniquePointer<uint>();
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpDependencies = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpDependencies.Length; i++)
			{
				byte elem_0 = lpDependencies[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpDependencies[i] = elem_0;
			}

			dwDependSize = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpServiceStartName = null;
			else
				lpServiceStartName = decoder.ReadUnsignedCharString();
			lpPassword = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpPassword.Length; i++)
			{
				byte elem_0 = lpPassword[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpPassword[i] = elem_0;
			}

			dwPwSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RCreateServiceA(hSCManager, lpServiceName, lpDisplayName, dwDesiredAccess, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpServiceHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteContextHandle(lpServiceHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_REnumDependentServicesA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwServiceState;
			RpcPointer<byte[]> lpServices = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			RpcPointer<uint> lpServicesReturned = new RpcPointer<uint>();
			hService = decoder.ReadContextHandle();
			dwServiceState = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.REnumDependentServicesA(hService, dwServiceState, lpServices, cbBufSize, pcbBytesNeeded, lpServicesReturned, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpServices.value);
			for (int i = 0; i < lpServices.value.Length; i++)
			{
				byte elem_0 = lpServices.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(lpServicesReturned.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_REnumServicesStatusA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			uint dwServiceType;
			uint dwServiceState;
			RpcPointer<byte[]> lpBuffer = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			RpcPointer<uint> lpServicesReturned = new RpcPointer<uint>();
			RpcPointer<uint> lpResumeIndex;
			hSCManager = decoder.ReadContextHandle();
			dwServiceType = decoder.ReadUInt32();
			dwServiceState = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			lpResumeIndex = decoder.ReadUniquePointer<uint>();
			if (lpResumeIndex is not null)
			{
				lpResumeIndex.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.REnumServicesStatusA(hSCManager, dwServiceType, dwServiceState, lpBuffer, cbBufSize, pcbBytesNeeded, lpServicesReturned, lpResumeIndex, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpBuffer.value);
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(lpServicesReturned.value);
			encoder.WriteUniquePointer(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				encoder.WriteValue(lpResumeIndex.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ROpenSCManagerA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string lpMachineName;
			string lpDatabaseName;
			uint dwDesiredAccess;
			RpcPointer<RpcContextHandle> lpScHandle = new RpcPointer<RpcContextHandle>();
			if (decoder.ReadReferentId() == 0)
				lpMachineName = null;
			else
				lpMachineName = decoder.ReadUnsignedCharString();
			if (decoder.ReadReferentId() == 0)
				lpDatabaseName = null;
			else
				lpDatabaseName = decoder.ReadUnsignedCharString();
			dwDesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.ROpenSCManagerA(lpMachineName, lpDatabaseName, dwDesiredAccess, lpScHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(lpScHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ROpenServiceA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpServiceName;
			uint dwDesiredAccess;
			RpcPointer<RpcContextHandle> lpServiceHandle = new RpcPointer<RpcContextHandle>();
			hSCManager = decoder.ReadContextHandle();
			lpServiceName = decoder.ReadUnsignedCharString();
			dwDesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.ROpenServiceA(hSCManager, lpServiceName, dwDesiredAccess, lpServiceHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(lpServiceHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RQueryServiceConfigA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			RpcPointer<QUERY_SERVICE_CONFIGA> lpServiceConfig = new RpcPointer<QUERY_SERVICE_CONFIGA>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			hService = decoder.ReadContextHandle();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RQueryServiceConfigA(hService, lpServiceConfig, cbBufSize, pcbBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(lpServiceConfig.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpServiceConfig.value);
			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RQueryServiceLockStatusA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			RpcPointer<QUERY_SERVICE_LOCK_STATUSA> lpLockStatus = new RpcPointer<QUERY_SERVICE_LOCK_STATUSA>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			hSCManager = decoder.ReadContextHandle();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RQueryServiceLockStatusA(hSCManager, lpLockStatus, cbBufSize, pcbBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(lpLockStatus.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpLockStatus.value);
			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RStartServiceA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint argc;
			STRING_PTRSA[] argv;
			hService = decoder.ReadContextHandle();
			argc = decoder.ReadUInt32();
			argv = decoder.ReadArrayHeader<STRING_PTRSA>();
			for (int i = 0; i < argv.Length; i++)
			{
				STRING_PTRSA elem_0 = argv[i];
				elem_0 = decoder.ReadFixedStruct<STRING_PTRSA>(NdrAlignment.NativePtr);
				argv[i] = elem_0;
			}

			for (int i = 0; i < argv.Length; i++)
			{
				STRING_PTRSA elem_0 = argv[i];
				decoder.ReadStructDeferral<STRING_PTRSA>(ref elem_0);
				argv[i] = elem_0;
			}

			var invokeTask = this._obj.RStartServiceA(hService, argc, argv, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RGetServiceDisplayNameA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpServiceName;
			RpcPointer<string> lpDisplayName = new RpcPointer<string>();
			RpcPointer<uint> lpcchBuffer;
			hSCManager = decoder.ReadContextHandle();
			lpServiceName = decoder.ReadUnsignedCharString();
			lpcchBuffer = new RpcPointer<uint>();
			lpcchBuffer.value = decoder.ReadUInt32();
			var invokeTask = this._obj.RGetServiceDisplayNameA(hSCManager, lpServiceName, lpDisplayName, lpcchBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnsignedCharString(lpDisplayName.value);
			encoder.WriteValue(lpcchBuffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RGetServiceKeyNameA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpDisplayName;
			RpcPointer<string> lpKeyName = new RpcPointer<string>();
			RpcPointer<uint> lpcchBuffer;
			hSCManager = decoder.ReadContextHandle();
			lpDisplayName = decoder.ReadUnsignedCharString();
			lpcchBuffer = new RpcPointer<uint>();
			lpcchBuffer.value = decoder.ReadUInt32();
			var invokeTask = this._obj.RGetServiceKeyNameA(hSCManager, lpDisplayName, lpKeyName, lpcchBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnsignedCharString(lpKeyName.value);
			encoder.WriteValue(lpcchBuffer.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum34NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum34NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_REnumServiceGroupW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			uint dwServiceType;
			uint dwServiceState;
			RpcPointer<byte[]> lpBuffer = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			RpcPointer<uint> lpServicesReturned = new RpcPointer<uint>();
			RpcPointer<uint> lpResumeIndex;
			string pszGroupName;
			hSCManager = decoder.ReadContextHandle();
			dwServiceType = decoder.ReadUInt32();
			dwServiceState = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			lpResumeIndex = decoder.ReadUniquePointer<uint>();
			if (lpResumeIndex is not null)
			{
				lpResumeIndex.value = decoder.ReadUInt32();
			}

			if (decoder.ReadReferentId() == 0)
				pszGroupName = null;
			else
				pszGroupName = decoder.ReadWideCharString();
			var invokeTask = this._obj.REnumServiceGroupW(hSCManager, dwServiceType, dwServiceState, lpBuffer, cbBufSize, pcbBytesNeeded, lpServicesReturned, lpResumeIndex, pszGroupName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpBuffer.value);
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(lpServicesReturned.value);
			encoder.WriteUniquePointer(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				encoder.WriteValue(lpResumeIndex.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RChangeServiceConfig2A(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			SC_RPC_CONFIG_INFOA Info;
			hService = decoder.ReadContextHandle();
			Info = decoder.ReadFixedStruct<SC_RPC_CONFIG_INFOA>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SC_RPC_CONFIG_INFOA>(ref Info);
			var invokeTask = this._obj.RChangeServiceConfig2A(hService, Info, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RChangeServiceConfig2W(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			SC_RPC_CONFIG_INFOW Info;
			hService = decoder.ReadContextHandle();
			Info = decoder.ReadFixedStruct<SC_RPC_CONFIG_INFOW>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SC_RPC_CONFIG_INFOW>(ref Info);
			var invokeTask = this._obj.RChangeServiceConfig2W(hService, Info, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RQueryServiceConfig2A(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwInfoLevel;
			RpcPointer<byte[]> lpBuffer = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			hService = decoder.ReadContextHandle();
			dwInfoLevel = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RQueryServiceConfig2A(hService, dwInfoLevel, lpBuffer, cbBufSize, pcbBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpBuffer.value);
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RQueryServiceConfig2W(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwInfoLevel;
			RpcPointer<byte[]> lpBuffer = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			hService = decoder.ReadContextHandle();
			dwInfoLevel = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RQueryServiceConfig2W(hService, dwInfoLevel, lpBuffer, cbBufSize, pcbBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpBuffer.value);
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RQueryServiceStatusEx(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			SC_STATUS_TYPE InfoLevel;
			RpcPointer<byte[]> lpBuffer = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			hService = decoder.ReadContextHandle();
			InfoLevel = (SC_STATUS_TYPE)decoder.ReadInt32();
			cbBufSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RQueryServiceStatusEx(hService, InfoLevel, lpBuffer, cbBufSize, pcbBytesNeeded, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpBuffer.value);
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_REnumServicesStatusExA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			SC_ENUM_TYPE InfoLevel;
			uint dwServiceType;
			uint dwServiceState;
			RpcPointer<byte[]> lpBuffer = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			RpcPointer<uint> lpServicesReturned = new RpcPointer<uint>();
			RpcPointer<uint> lpResumeIndex;
			string pszGroupName;
			hSCManager = decoder.ReadContextHandle();
			InfoLevel = (SC_ENUM_TYPE)decoder.ReadInt32();
			dwServiceType = decoder.ReadUInt32();
			dwServiceState = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			lpResumeIndex = decoder.ReadUniquePointer<uint>();
			if (lpResumeIndex is not null)
			{
				lpResumeIndex.value = decoder.ReadUInt32();
			}

			if (decoder.ReadReferentId() == 0)
				pszGroupName = null;
			else
				pszGroupName = decoder.ReadUnsignedCharString();
			var invokeTask = this._obj.REnumServicesStatusExA(hSCManager, InfoLevel, dwServiceType, dwServiceState, lpBuffer, cbBufSize, pcbBytesNeeded, lpServicesReturned, lpResumeIndex, pszGroupName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpBuffer.value);
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(lpServicesReturned.value);
			encoder.WriteUniquePointer(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				encoder.WriteValue(lpResumeIndex.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_REnumServicesStatusExW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			SC_ENUM_TYPE InfoLevel;
			uint dwServiceType;
			uint dwServiceState;
			RpcPointer<byte[]> lpBuffer = new RpcPointer<byte[]>();
			uint cbBufSize;
			RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
			RpcPointer<uint> lpServicesReturned = new RpcPointer<uint>();
			RpcPointer<uint> lpResumeIndex;
			string pszGroupName;
			hSCManager = decoder.ReadContextHandle();
			InfoLevel = (SC_ENUM_TYPE)decoder.ReadInt32();
			dwServiceType = decoder.ReadUInt32();
			dwServiceState = decoder.ReadUInt32();
			cbBufSize = decoder.ReadUInt32();
			lpResumeIndex = decoder.ReadUniquePointer<uint>();
			if (lpResumeIndex is not null)
			{
				lpResumeIndex.value = decoder.ReadUInt32();
			}

			if (decoder.ReadReferentId() == 0)
				pszGroupName = null;
			else
				pszGroupName = decoder.ReadWideCharString();
			var invokeTask = this._obj.REnumServicesStatusExW(hSCManager, InfoLevel, dwServiceType, dwServiceState, lpBuffer, cbBufSize, pcbBytesNeeded, lpServicesReturned, lpResumeIndex, pszGroupName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(lpBuffer.value);
			for (int i = 0; i < lpBuffer.value.Length; i++)
			{
				byte elem_0 = lpBuffer.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(pcbBytesNeeded.value);
			encoder.WriteValue(lpServicesReturned.value);
			encoder.WriteUniquePointer(lpResumeIndex);
			if (lpResumeIndex is not null)
			{
				encoder.WriteValue(lpResumeIndex.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum43NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum43NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RCreateServiceWOW64A(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpServiceName;
			string lpDisplayName;
			uint dwDesiredAccess;
			uint dwServiceType;
			uint dwStartType;
			uint dwErrorControl;
			string lpBinaryPathName;
			string lpLoadOrderGroup;
			RpcPointer<uint> lpdwTagId;
			byte[] lpDependencies;
			uint dwDependSize;
			string lpServiceStartName;
			byte[] lpPassword;
			uint dwPwSize;
			RpcPointer<RpcContextHandle> lpServiceHandle = new RpcPointer<RpcContextHandle>();
			hSCManager = decoder.ReadContextHandle();
			lpServiceName = decoder.ReadUnsignedCharString();
			if (decoder.ReadReferentId() == 0)
				lpDisplayName = null;
			else
				lpDisplayName = decoder.ReadUnsignedCharString();
			dwDesiredAccess = decoder.ReadUInt32();
			dwServiceType = decoder.ReadUInt32();
			dwStartType = decoder.ReadUInt32();
			dwErrorControl = decoder.ReadUInt32();
			lpBinaryPathName = decoder.ReadUnsignedCharString();
			if (decoder.ReadReferentId() == 0)
				lpLoadOrderGroup = null;
			else
				lpLoadOrderGroup = decoder.ReadUnsignedCharString();
			lpdwTagId = decoder.ReadUniquePointer<uint>();
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpDependencies = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpDependencies.Length; i++)
			{
				byte elem_0 = lpDependencies[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpDependencies[i] = elem_0;
			}

			dwDependSize = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpServiceStartName = null;
			else
				lpServiceStartName = decoder.ReadUnsignedCharString();
			lpPassword = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpPassword.Length; i++)
			{
				byte elem_0 = lpPassword[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpPassword[i] = elem_0;
			}

			dwPwSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RCreateServiceWOW64A(hSCManager, lpServiceName, lpDisplayName, dwDesiredAccess, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpServiceHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteContextHandle(lpServiceHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RCreateServiceWOW64W(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpServiceName;
			string lpDisplayName;
			uint dwDesiredAccess;
			uint dwServiceType;
			uint dwStartType;
			uint dwErrorControl;
			string lpBinaryPathName;
			string lpLoadOrderGroup;
			RpcPointer<uint> lpdwTagId;
			byte[] lpDependencies;
			uint dwDependSize;
			string lpServiceStartName;
			byte[] lpPassword;
			uint dwPwSize;
			RpcPointer<RpcContextHandle> lpServiceHandle = new RpcPointer<RpcContextHandle>();
			hSCManager = decoder.ReadContextHandle();
			lpServiceName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				lpDisplayName = null;
			else
				lpDisplayName = decoder.ReadWideCharString();
			dwDesiredAccess = decoder.ReadUInt32();
			dwServiceType = decoder.ReadUInt32();
			dwStartType = decoder.ReadUInt32();
			dwErrorControl = decoder.ReadUInt32();
			lpBinaryPathName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				lpLoadOrderGroup = null;
			else
				lpLoadOrderGroup = decoder.ReadWideCharString();
			lpdwTagId = decoder.ReadUniquePointer<uint>();
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpDependencies = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpDependencies.Length; i++)
			{
				byte elem_0 = lpDependencies[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpDependencies[i] = elem_0;
			}

			dwDependSize = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpServiceStartName = null;
			else
				lpServiceStartName = decoder.ReadWideCharString();
			lpPassword = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpPassword.Length; i++)
			{
				byte elem_0 = lpPassword[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpPassword[i] = elem_0;
			}

			dwPwSize = decoder.ReadUInt32();
			var invokeTask = this._obj.RCreateServiceWOW64W(hSCManager, lpServiceName, lpDisplayName, dwDesiredAccess, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpServiceHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteContextHandle(lpServiceHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum46NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum46NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RNotifyServiceStatusChange(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			SC_RPC_NOTIFY_PARAMS NotifyParams;
			Guid pClientProcessGuid;
			RpcPointer<Guid> pSCMProcessGuid = new RpcPointer<Guid>();
			RpcPointer<int> pfCreateRemoteQueue = new RpcPointer<int>();
			RpcPointer<RpcContextHandle> phNotify = new RpcPointer<RpcContextHandle>();
			hService = decoder.ReadContextHandle();
			NotifyParams = decoder.ReadFixedStruct<SC_RPC_NOTIFY_PARAMS>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SC_RPC_NOTIFY_PARAMS>(ref NotifyParams);
			pClientProcessGuid = decoder.ReadUuid();
			var invokeTask = this._obj.RNotifyServiceStatusChange(hService, NotifyParams, pClientProcessGuid, pSCMProcessGuid, pfCreateRemoteQueue, phNotify, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pSCMProcessGuid.value);
			encoder.WriteValue(pfCreateRemoteQueue.value);
			encoder.WriteContextHandle(phNotify.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RGetNotifyResults(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hNotify;
			RpcPointer<RpcPointer<SC_RPC_NOTIFY_PARAMS_LIST>> ppNotifyParams = new RpcPointer<RpcPointer<SC_RPC_NOTIFY_PARAMS_LIST>>();
			hNotify = decoder.ReadContextHandle();
			var invokeTask = this._obj.RGetNotifyResults(hNotify, ppNotifyParams, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppNotifyParams.value);
			if (ppNotifyParams.value is not null)
			{
				encoder.WriteConformantStruct(ppNotifyParams.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppNotifyParams.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RCloseNotifyHandle(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> phNotify;
			RpcPointer<int> pfApcFired = new RpcPointer<int>();
			phNotify = new RpcPointer<RpcContextHandle>();
			phNotify.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.RCloseNotifyHandle(phNotify, pfApcFired, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phNotify.value);
			encoder.WriteValue(pfApcFired.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RControlServiceExA(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwControl;
			uint dwInfoLevel;
			SC_RPC_SERVICE_CONTROL_IN_PARAMSA pControlInParams;
			RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSA> pControlOutParams = new RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSA>();
			hService = decoder.ReadContextHandle();
			dwControl = decoder.ReadUInt32();
			dwInfoLevel = decoder.ReadUInt32();
			pControlInParams = decoder.ReadUnion<SC_RPC_SERVICE_CONTROL_IN_PARAMSA>();
			decoder.ReadStructDeferral<SC_RPC_SERVICE_CONTROL_IN_PARAMSA>(ref pControlInParams);
			var invokeTask = this._obj.RControlServiceExA(hService, dwControl, dwInfoLevel, pControlInParams, pControlOutParams, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnion(pControlOutParams.value);
			encoder.WriteStructDeferral(pControlOutParams.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RControlServiceExW(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwControl;
			uint dwInfoLevel;
			SC_RPC_SERVICE_CONTROL_IN_PARAMSW pControlInParams;
			RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSW> pControlOutParams = new RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSW>();
			hService = decoder.ReadContextHandle();
			dwControl = decoder.ReadUInt32();
			dwInfoLevel = decoder.ReadUInt32();
			pControlInParams = decoder.ReadUnion<SC_RPC_SERVICE_CONTROL_IN_PARAMSW>();
			decoder.ReadStructDeferral<SC_RPC_SERVICE_CONTROL_IN_PARAMSW>(ref pControlInParams);
			var invokeTask = this._obj.RControlServiceExW(hService, dwControl, dwInfoLevel, pControlInParams, pControlOutParams, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnion(pControlOutParams.value);
			encoder.WriteStructDeferral(pControlOutParams.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum52NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum52NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum53NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum53NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum54NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum54NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum55NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum55NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RQueryServiceConfigEx(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hService;
			uint dwInfoLevel;
			RpcPointer<SC_RPC_CONFIG_INFOW> pInfo = new RpcPointer<SC_RPC_CONFIG_INFOW>();
			hService = decoder.ReadContextHandle();
			dwInfoLevel = decoder.ReadUInt32();
			var invokeTask = this._obj.RQueryServiceConfigEx(hService, dwInfoLevel, pInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pInfo.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum57NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum57NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum58NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum58NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum59NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum59NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_RCreateWowService(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hSCManager;
			string lpServiceName;
			string lpDisplayName;
			uint dwDesiredAccess;
			uint dwServiceType;
			uint dwStartType;
			uint dwErrorControl;
			string lpBinaryPathName;
			string lpLoadOrderGroup;
			RpcPointer<uint> lpdwTagId;
			byte[] lpDependencies;
			uint dwDependSize;
			string lpServiceStartName;
			byte[] lpPassword;
			uint dwPwSize;
			ushort dwServiceWowType;
			RpcPointer<RpcContextHandle> lpServiceHandle = new RpcPointer<RpcContextHandle>();
			hSCManager = decoder.ReadContextHandle();
			lpServiceName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				lpDisplayName = null;
			else
				lpDisplayName = decoder.ReadWideCharString();
			dwDesiredAccess = decoder.ReadUInt32();
			dwServiceType = decoder.ReadUInt32();
			dwStartType = decoder.ReadUInt32();
			dwErrorControl = decoder.ReadUInt32();
			lpBinaryPathName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				lpLoadOrderGroup = null;
			else
				lpLoadOrderGroup = decoder.ReadWideCharString();
			lpdwTagId = decoder.ReadUniquePointer<uint>();
			if (lpdwTagId is not null)
			{
				lpdwTagId.value = decoder.ReadUInt32();
			}

			lpDependencies = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpDependencies.Length; i++)
			{
				byte elem_0 = lpDependencies[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpDependencies[i] = elem_0;
			}

			dwDependSize = decoder.ReadUInt32();
			if (decoder.ReadReferentId() == 0)
				lpServiceStartName = null;
			else
				lpServiceStartName = decoder.ReadWideCharString();
			lpPassword = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpPassword.Length; i++)
			{
				byte elem_0 = lpPassword[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpPassword[i] = elem_0;
			}

			dwPwSize = decoder.ReadUInt32();
			dwServiceWowType = decoder.ReadUInt16();
			var invokeTask = this._obj.RCreateWowService(hSCManager, lpServiceName, lpDisplayName, dwDesiredAccess, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, dwServiceWowType, lpServiceHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(lpdwTagId);
			if (lpdwTagId is not null)
			{
				encoder.WriteValue(lpdwTagId.value);
			}

			encoder.WriteContextHandle(lpServiceHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_ROpenSCManager2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string DatabaseName;
			uint DesiredAccess;
			RpcPointer<RpcContextHandle> ScmHandle = new RpcPointer<RpcContextHandle>();
			if (decoder.ReadReferentId() == 0)
				DatabaseName = null;
			else
				DatabaseName = decoder.ReadWideCharString();
			DesiredAccess = decoder.ReadUInt32();
			var invokeTask = this._obj.ROpenSCManager2(DatabaseName, DesiredAccess, ScmHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ScmHandle.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("367abb81-9844-35f1-ad32-98f038001003");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(2, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private svcctl _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public svcctlStub(svcctl obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_RCloseServiceHandle, this.Invoke_RControlService, this.Invoke_RDeleteService, this.Invoke_RLockServiceDatabase, this.Invoke_RQueryServiceObjectSecurity, this.Invoke_RSetServiceObjectSecurity, this.Invoke_RQueryServiceStatus, this.Invoke_RSetServiceStatus, this.Invoke_RUnlockServiceDatabase, this.Invoke_RNotifyBootConfigStatus, this.Invoke_Opnum10NotUsedOnWire, this.Invoke_RChangeServiceConfigW, this.Invoke_RCreateServiceW, this.Invoke_REnumDependentServicesW, this.Invoke_REnumServicesStatusW, this.Invoke_ROpenSCManagerW, this.Invoke_ROpenServiceW, this.Invoke_RQueryServiceConfigW, this.Invoke_RQueryServiceLockStatusW, this.Invoke_RStartServiceW, this.Invoke_RGetServiceDisplayNameW, this.Invoke_RGetServiceKeyNameW, this.Invoke_Opnum22NotUsedOnWire, this.Invoke_RChangeServiceConfigA, this.Invoke_RCreateServiceA, this.Invoke_REnumDependentServicesA, this.Invoke_REnumServicesStatusA, this.Invoke_ROpenSCManagerA, this.Invoke_ROpenServiceA, this.Invoke_RQueryServiceConfigA, this.Invoke_RQueryServiceLockStatusA, this.Invoke_RStartServiceA, this.Invoke_RGetServiceDisplayNameA, this.Invoke_RGetServiceKeyNameA, this.Invoke_Opnum34NotUsedOnWire, this.Invoke_REnumServiceGroupW, this.Invoke_RChangeServiceConfig2A, this.Invoke_RChangeServiceConfig2W, this.Invoke_RQueryServiceConfig2A, this.Invoke_RQueryServiceConfig2W, this.Invoke_RQueryServiceStatusEx, this.Invoke_REnumServicesStatusExA, this.Invoke_REnumServicesStatusExW, this.Invoke_Opnum43NotUsedOnWire, this.Invoke_RCreateServiceWOW64A, this.Invoke_RCreateServiceWOW64W, this.Invoke_Opnum46NotUsedOnWire, this.Invoke_RNotifyServiceStatusChange, this.Invoke_RGetNotifyResults, this.Invoke_RCloseNotifyHandle, this.Invoke_RControlServiceExA, this.Invoke_RControlServiceExW, this.Invoke_Opnum52NotUsedOnWire, this.Invoke_Opnum53NotUsedOnWire, this.Invoke_Opnum54NotUsedOnWire, this.Invoke_Opnum55NotUsedOnWire, this.Invoke_RQueryServiceConfigEx, this.Invoke_Opnum57NotUsedOnWire, this.Invoke_Opnum58NotUsedOnWire, this.Invoke_Opnum59NotUsedOnWire, this.Invoke_RCreateWowService, 
null,null,null, this.Invoke_ROpenSCManager2};
		}
	}
}