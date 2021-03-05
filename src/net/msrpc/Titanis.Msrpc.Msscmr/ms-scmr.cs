#pragma warning disable

namespace ms_scmr {
    using System;
    using System.Threading.Tasks;
    using Titanis;
    using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct STRING_PTRSA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.StringPtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.StringPtr = decoder.ReadPointer<string>();
        }
        public RpcPointer<string> StringPtr;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.StringPtr)) {
                encoder.WriteUnsignedCharString(this.StringPtr.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.StringPtr)) {
                this.StringPtr.value = decoder.ReadUnsignedCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct STRING_PTRSW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.StringPtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.StringPtr = decoder.ReadPointer<string>();
        }
        public RpcPointer<string> StringPtr;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.StringPtr)) {
                encoder.WriteWideCharString(this.StringPtr.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.StringPtr)) {
                this.StringPtr.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_STATUS : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwServiceType);
            encoder.WriteValue(this.dwCurrentState);
            encoder.WriteValue(this.dwControlsAccepted);
            encoder.WriteValue(this.dwWin32ExitCode);
            encoder.WriteValue(this.dwServiceSpecificExitCode);
            encoder.WriteValue(this.dwCheckPoint);
            encoder.WriteValue(this.dwWaitHint);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwServiceType = decoder.ReadUInt32();
            this.dwCurrentState = decoder.ReadUInt32();
            this.dwControlsAccepted = decoder.ReadUInt32();
            this.dwWin32ExitCode = decoder.ReadUInt32();
            this.dwServiceSpecificExitCode = decoder.ReadUInt32();
            this.dwCheckPoint = decoder.ReadUInt32();
            this.dwWaitHint = decoder.ReadUInt32();
        }
        public uint dwServiceType;
        public uint dwCurrentState;
        public uint dwControlsAccepted;
        public uint dwWin32ExitCode;
        public uint dwServiceSpecificExitCode;
        public uint dwCheckPoint;
        public uint dwWaitHint;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_STATUS_PROCESS : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
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
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
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
        public uint dwServiceType;
        public uint dwCurrentState;
        public uint dwControlsAccepted;
        public uint dwWin32ExitCode;
        public uint dwServiceSpecificExitCode;
        public uint dwCheckPoint;
        public uint dwWaitHint;
        public uint dwProcessId;
        public uint dwServiceFlags;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct QUERY_SERVICE_CONFIGW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwServiceType);
            encoder.WriteValue(this.dwStartType);
            encoder.WriteValue(this.dwErrorControl);
            encoder.WritePointer(this.lpBinaryPathName);
            encoder.WritePointer(this.lpLoadOrderGroup);
            encoder.WriteValue(this.dwTagId);
            encoder.WritePointer(this.lpDependencies);
            encoder.WritePointer(this.lpServiceStartName);
            encoder.WritePointer(this.lpDisplayName);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwServiceType = decoder.ReadUInt32();
            this.dwStartType = decoder.ReadUInt32();
            this.dwErrorControl = decoder.ReadUInt32();
            this.lpBinaryPathName = decoder.ReadPointer<string>();
            this.lpLoadOrderGroup = decoder.ReadPointer<string>();
            this.dwTagId = decoder.ReadUInt32();
            this.lpDependencies = decoder.ReadPointer<string>();
            this.lpServiceStartName = decoder.ReadPointer<string>();
            this.lpDisplayName = decoder.ReadPointer<string>();
        }
        public uint dwServiceType;
        public uint dwStartType;
        public uint dwErrorControl;
        public RpcPointer<string> lpBinaryPathName;
        public RpcPointer<string> lpLoadOrderGroup;
        public uint dwTagId;
        public RpcPointer<string> lpDependencies;
        public RpcPointer<string> lpServiceStartName;
        public RpcPointer<string> lpDisplayName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpBinaryPathName)) {
                encoder.WriteWideCharString(this.lpBinaryPathName.value);
            }
            if ((null != this.lpLoadOrderGroup)) {
                encoder.WriteWideCharString(this.lpLoadOrderGroup.value);
            }
            if ((null != this.lpDependencies)) {
                encoder.WriteWideCharString(this.lpDependencies.value);
            }
            if ((null != this.lpServiceStartName)) {
                encoder.WriteWideCharString(this.lpServiceStartName.value);
            }
            if ((null != this.lpDisplayName)) {
                encoder.WriteWideCharString(this.lpDisplayName.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpBinaryPathName)) {
                this.lpBinaryPathName.value = decoder.ReadWideCharString();
            }
            if ((null != this.lpLoadOrderGroup)) {
                this.lpLoadOrderGroup.value = decoder.ReadWideCharString();
            }
            if ((null != this.lpDependencies)) {
                this.lpDependencies.value = decoder.ReadWideCharString();
            }
            if ((null != this.lpServiceStartName)) {
                this.lpServiceStartName.value = decoder.ReadWideCharString();
            }
            if ((null != this.lpDisplayName)) {
                this.lpDisplayName.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct QUERY_SERVICE_LOCK_STATUSW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.fIsLocked);
            encoder.WritePointer(this.lpLockOwner);
            encoder.WriteValue(this.dwLockDuration);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.fIsLocked = decoder.ReadUInt32();
            this.lpLockOwner = decoder.ReadPointer<string>();
            this.dwLockDuration = decoder.ReadUInt32();
        }
        public uint fIsLocked;
        public RpcPointer<string> lpLockOwner;
        public uint dwLockDuration;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpLockOwner)) {
                encoder.WriteWideCharString(this.lpLockOwner.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpLockOwner)) {
                this.lpLockOwner.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct QUERY_SERVICE_CONFIGA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwServiceType);
            encoder.WriteValue(this.dwStartType);
            encoder.WriteValue(this.dwErrorControl);
            encoder.WritePointer(this.lpBinaryPathName);
            encoder.WritePointer(this.lpLoadOrderGroup);
            encoder.WriteValue(this.dwTagId);
            encoder.WritePointer(this.lpDependencies);
            encoder.WritePointer(this.lpServiceStartName);
            encoder.WritePointer(this.lpDisplayName);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwServiceType = decoder.ReadUInt32();
            this.dwStartType = decoder.ReadUInt32();
            this.dwErrorControl = decoder.ReadUInt32();
            this.lpBinaryPathName = decoder.ReadPointer<string>();
            this.lpLoadOrderGroup = decoder.ReadPointer<string>();
            this.dwTagId = decoder.ReadUInt32();
            this.lpDependencies = decoder.ReadPointer<string>();
            this.lpServiceStartName = decoder.ReadPointer<string>();
            this.lpDisplayName = decoder.ReadPointer<string>();
        }
        public uint dwServiceType;
        public uint dwStartType;
        public uint dwErrorControl;
        public RpcPointer<string> lpBinaryPathName;
        public RpcPointer<string> lpLoadOrderGroup;
        public uint dwTagId;
        public RpcPointer<string> lpDependencies;
        public RpcPointer<string> lpServiceStartName;
        public RpcPointer<string> lpDisplayName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpBinaryPathName)) {
                encoder.WriteUnsignedCharString(this.lpBinaryPathName.value);
            }
            if ((null != this.lpLoadOrderGroup)) {
                encoder.WriteUnsignedCharString(this.lpLoadOrderGroup.value);
            }
            if ((null != this.lpDependencies)) {
                encoder.WriteUnsignedCharString(this.lpDependencies.value);
            }
            if ((null != this.lpServiceStartName)) {
                encoder.WriteUnsignedCharString(this.lpServiceStartName.value);
            }
            if ((null != this.lpDisplayName)) {
                encoder.WriteUnsignedCharString(this.lpDisplayName.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpBinaryPathName)) {
                this.lpBinaryPathName.value = decoder.ReadUnsignedCharString();
            }
            if ((null != this.lpLoadOrderGroup)) {
                this.lpLoadOrderGroup.value = decoder.ReadUnsignedCharString();
            }
            if ((null != this.lpDependencies)) {
                this.lpDependencies.value = decoder.ReadUnsignedCharString();
            }
            if ((null != this.lpServiceStartName)) {
                this.lpServiceStartName.value = decoder.ReadUnsignedCharString();
            }
            if ((null != this.lpDisplayName)) {
                this.lpDisplayName.value = decoder.ReadUnsignedCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct QUERY_SERVICE_LOCK_STATUSA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.fIsLocked);
            encoder.WritePointer(this.lpLockOwner);
            encoder.WriteValue(this.dwLockDuration);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.fIsLocked = decoder.ReadUInt32();
            this.lpLockOwner = decoder.ReadPointer<string>();
            this.dwLockDuration = decoder.ReadUInt32();
        }
        public uint fIsLocked;
        public RpcPointer<string> lpLockOwner;
        public uint dwLockDuration;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpLockOwner)) {
                encoder.WriteUnsignedCharString(this.lpLockOwner.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpLockOwner)) {
                this.lpLockOwner.value = decoder.ReadUnsignedCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_DESCRIPTIONA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.lpDescription);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.lpDescription = decoder.ReadPointer<string>();
        }
        public RpcPointer<string> lpDescription;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpDescription)) {
                encoder.WriteUnsignedCharString(this.lpDescription.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpDescription)) {
                this.lpDescription.value = decoder.ReadUnsignedCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum SC_ACTION_TYPE : int {
        SC_ACTION_NONE = 0,
        SC_ACTION_RESTART = 1,
        SC_ACTION_REBOOT = 2,
        SC_ACTION_RUN_COMMAND = 3,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SC_ACTION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((int)(this.Type)));
            encoder.WriteValue(this.Delay);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Type = ((SC_ACTION_TYPE)(decoder.ReadInt32()));
            this.Delay = decoder.ReadUInt32();
        }
        public SC_ACTION_TYPE Type;
        public uint Delay;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_FAILURE_ACTIONSA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwResetPeriod);
            encoder.WritePointer(this.lpRebootMsg);
            encoder.WritePointer(this.lpCommand);
            encoder.WriteValue(this.cActions);
            encoder.WritePointer(this.lpsaActions);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwResetPeriod = decoder.ReadUInt32();
            this.lpRebootMsg = decoder.ReadPointer<string>();
            this.lpCommand = decoder.ReadPointer<string>();
            this.cActions = decoder.ReadUInt32();
            this.lpsaActions = decoder.ReadPointer<SC_ACTION[]>();
        }
        public uint dwResetPeriod;
        public RpcPointer<string> lpRebootMsg;
        public RpcPointer<string> lpCommand;
        public uint cActions;
        public RpcPointer<SC_ACTION[]> lpsaActions;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpRebootMsg)) {
                encoder.WriteUnsignedCharString(this.lpRebootMsg.value);
            }
            if ((null != this.lpCommand)) {
                encoder.WriteUnsignedCharString(this.lpCommand.value);
            }
            if ((null != this.lpsaActions)) {
                encoder.WriteArrayHeader(this.lpsaActions.value);
                for (int i = 0; (i < this.lpsaActions.value.Length); i++
                ) {
                    SC_ACTION elem_0 = this.lpsaActions.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
                for (int i = 0; (i < this.lpsaActions.value.Length); i++
                ) {
                    SC_ACTION elem_0 = this.lpsaActions.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpRebootMsg)) {
                this.lpRebootMsg.value = decoder.ReadUnsignedCharString();
            }
            if ((null != this.lpCommand)) {
                this.lpCommand.value = decoder.ReadUnsignedCharString();
            }
            if ((null != this.lpsaActions)) {
                this.lpsaActions.value = decoder.ReadArrayHeader<SC_ACTION>();
                for (int i = 0; (i < this.lpsaActions.value.Length); i++
                ) {
                    SC_ACTION elem_0 = this.lpsaActions.value[i];
                    elem_0 = decoder.ReadFixedStruct<SC_ACTION>(Titanis.DceRpc.NdrAlignment._4Byte);
                    this.lpsaActions.value[i] = elem_0;
                }
                for (int i = 0; (i < this.lpsaActions.value.Length); i++
                ) {
                    SC_ACTION elem_0 = this.lpsaActions.value[i];
                    decoder.ReadStructDeferral<SC_ACTION>(ref elem_0);
                    this.lpsaActions.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_DELAYED_AUTO_START_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.fDelayedAutostart);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.fDelayedAutostart = decoder.ReadInt32();
        }
        public int fDelayedAutostart;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_FAILURE_ACTIONS_FLAG : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.fFailureActionsOnNonCrashFailures);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.fFailureActionsOnNonCrashFailures = decoder.ReadInt32();
        }
        public int fFailureActionsOnNonCrashFailures;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_SID_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwServiceSidType);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwServiceSidType = decoder.ReadUInt32();
        }
        public uint dwServiceSidType;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_PRESHUTDOWN_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwPreshutdownTimeout);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwPreshutdownTimeout = decoder.ReadUInt32();
        }
        public uint dwPreshutdownTimeout;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_DESCRIPTIONW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.lpDescription);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.lpDescription = decoder.ReadPointer<string>();
        }
        public RpcPointer<string> lpDescription;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpDescription)) {
                encoder.WriteWideCharString(this.lpDescription.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpDescription)) {
                this.lpDescription.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_FAILURE_ACTIONSW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwResetPeriod);
            encoder.WritePointer(this.lpRebootMsg);
            encoder.WritePointer(this.lpCommand);
            encoder.WriteValue(this.cActions);
            encoder.WritePointer(this.lpsaActions);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwResetPeriod = decoder.ReadUInt32();
            this.lpRebootMsg = decoder.ReadPointer<string>();
            this.lpCommand = decoder.ReadPointer<string>();
            this.cActions = decoder.ReadUInt32();
            this.lpsaActions = decoder.ReadPointer<SC_ACTION[]>();
        }
        public uint dwResetPeriod;
        public RpcPointer<string> lpRebootMsg;
        public RpcPointer<string> lpCommand;
        public uint cActions;
        public RpcPointer<SC_ACTION[]> lpsaActions;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpRebootMsg)) {
                encoder.WriteWideCharString(this.lpRebootMsg.value);
            }
            if ((null != this.lpCommand)) {
                encoder.WriteWideCharString(this.lpCommand.value);
            }
            if ((null != this.lpsaActions)) {
                encoder.WriteArrayHeader(this.lpsaActions.value);
                for (int i = 0; (i < this.lpsaActions.value.Length); i++
                ) {
                    SC_ACTION elem_0 = this.lpsaActions.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
                for (int i = 0; (i < this.lpsaActions.value.Length); i++
                ) {
                    SC_ACTION elem_0 = this.lpsaActions.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpRebootMsg)) {
                this.lpRebootMsg.value = decoder.ReadWideCharString();
            }
            if ((null != this.lpCommand)) {
                this.lpCommand.value = decoder.ReadWideCharString();
            }
            if ((null != this.lpsaActions)) {
                this.lpsaActions.value = decoder.ReadArrayHeader<SC_ACTION>();
                for (int i = 0; (i < this.lpsaActions.value.Length); i++
                ) {
                    SC_ACTION elem_0 = this.lpsaActions.value[i];
                    elem_0 = decoder.ReadFixedStruct<SC_ACTION>(Titanis.DceRpc.NdrAlignment._4Byte);
                    this.lpsaActions.value[i] = elem_0;
                }
                for (int i = 0; (i < this.lpsaActions.value.Length); i++
                ) {
                    SC_ACTION elem_0 = this.lpsaActions.value[i];
                    decoder.ReadStructDeferral<SC_ACTION>(ref elem_0);
                    this.lpsaActions.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum SC_STATUS_TYPE : int {
        SC_STATUS_PROCESS_INFO = 0,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum SC_ENUM_TYPE : int {
        SC_ENUM_PROCESS_INFO = 0,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_PREFERRED_NODE_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.usPreferredNode);
            encoder.WriteValue(this.fDelete);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.usPreferredNode = decoder.ReadUInt16();
            this.fDelete = decoder.ReadUnsignedChar();
        }
        public ushort usPreferredNode;
        public byte fDelete;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_TRIGGER_SPECIFIC_DATA_ITEM : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwDataType);
            encoder.WriteValue(this.cbData);
            encoder.WritePointer(this.pData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwDataType = decoder.ReadUInt32();
            this.cbData = decoder.ReadUInt32();
            this.pData = decoder.ReadPointer<byte[]>();
        }
        public uint dwDataType;
        public uint cbData;
        public RpcPointer<byte[]> pData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pData)) {
                encoder.WriteArrayHeader(this.pData.value);
                for (int i = 0; (i < this.pData.value.Length); i++
                ) {
                    byte elem_0 = this.pData.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pData)) {
                this.pData.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.pData.value.Length); i++
                ) {
                    byte elem_0 = this.pData.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.pData.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_TRIGGER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwTriggerType);
            encoder.WriteValue(this.dwAction);
            encoder.WritePointer(this.pTriggerSubtype);
            encoder.WriteValue(this.cDataItems);
            encoder.WritePointer(this.pDataItems);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwTriggerType = decoder.ReadUInt32();
            this.dwAction = decoder.ReadUInt32();
            this.pTriggerSubtype = decoder.ReadPointer<System.Guid>();
            this.cDataItems = decoder.ReadUInt32();
            this.pDataItems = decoder.ReadPointer<SERVICE_TRIGGER_SPECIFIC_DATA_ITEM[]>();
        }
        public uint dwTriggerType;
        public uint dwAction;
        public RpcPointer<System.Guid> pTriggerSubtype;
        public uint cDataItems;
        public RpcPointer<SERVICE_TRIGGER_SPECIFIC_DATA_ITEM[]> pDataItems;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pTriggerSubtype)) {
                encoder.WriteValue(this.pTriggerSubtype.value);
            }
            if ((null != this.pDataItems)) {
                encoder.WriteArrayHeader(this.pDataItems.value);
                for (int i = 0; (i < this.pDataItems.value.Length); i++
                ) {
                    SERVICE_TRIGGER_SPECIFIC_DATA_ITEM elem_0 = this.pDataItems.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.pDataItems.value.Length); i++
                ) {
                    SERVICE_TRIGGER_SPECIFIC_DATA_ITEM elem_0 = this.pDataItems.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pTriggerSubtype)) {
                this.pTriggerSubtype.value = decoder.ReadUuid();
            }
            if ((null != this.pDataItems)) {
                this.pDataItems.value = decoder.ReadArrayHeader<SERVICE_TRIGGER_SPECIFIC_DATA_ITEM>();
                for (int i = 0; (i < this.pDataItems.value.Length); i++
                ) {
                    SERVICE_TRIGGER_SPECIFIC_DATA_ITEM elem_0 = this.pDataItems.value[i];
                    elem_0 = decoder.ReadFixedStruct<SERVICE_TRIGGER_SPECIFIC_DATA_ITEM>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.pDataItems.value[i] = elem_0;
                }
                for (int i = 0; (i < this.pDataItems.value.Length); i++
                ) {
                    SERVICE_TRIGGER_SPECIFIC_DATA_ITEM elem_0 = this.pDataItems.value[i];
                    decoder.ReadStructDeferral<SERVICE_TRIGGER_SPECIFIC_DATA_ITEM>(ref elem_0);
                    this.pDataItems.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_TRIGGER_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cTriggers);
            encoder.WritePointer(this.pTriggers);
            encoder.WritePointer(this.pReserved);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cTriggers = decoder.ReadUInt32();
            this.pTriggers = decoder.ReadPointer<SERVICE_TRIGGER[]>();
            this.pReserved = decoder.ReadPointer<byte>();
        }
        public uint cTriggers;
        public RpcPointer<SERVICE_TRIGGER[]> pTriggers;
        public RpcPointer<byte> pReserved;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pTriggers)) {
                encoder.WriteArrayHeader(this.pTriggers.value);
                for (int i = 0; (i < this.pTriggers.value.Length); i++
                ) {
                    SERVICE_TRIGGER elem_0 = this.pTriggers.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.pTriggers.value.Length); i++
                ) {
                    SERVICE_TRIGGER elem_0 = this.pTriggers.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
            if ((null != this.pReserved)) {
                encoder.WriteValue(this.pReserved.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pTriggers)) {
                this.pTriggers.value = decoder.ReadArrayHeader<SERVICE_TRIGGER>();
                for (int i = 0; (i < this.pTriggers.value.Length); i++
                ) {
                    SERVICE_TRIGGER elem_0 = this.pTriggers.value[i];
                    elem_0 = decoder.ReadFixedStruct<SERVICE_TRIGGER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.pTriggers.value[i] = elem_0;
                }
                for (int i = 0; (i < this.pTriggers.value.Length); i++
                ) {
                    SERVICE_TRIGGER elem_0 = this.pTriggers.value[i];
                    decoder.ReadStructDeferral<SERVICE_TRIGGER>(ref elem_0);
                    this.pTriggers.value[i] = elem_0;
                }
            }
            if ((null != this.pReserved)) {
                this.pReserved.value = decoder.ReadUnsignedChar();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENUM_SERVICE_STATUSA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.lpServiceName);
            encoder.WritePointer(this.lpDisplayName);
            encoder.WriteFixedStruct(this.ServiceStatus, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.lpServiceName = decoder.ReadPointer<byte>();
            this.lpDisplayName = decoder.ReadPointer<byte>();
            this.ServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public RpcPointer<byte> lpServiceName;
        public RpcPointer<byte> lpDisplayName;
        public SERVICE_STATUS ServiceStatus;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpServiceName)) {
                encoder.WriteValue(this.lpServiceName.value);
            }
            if ((null != this.lpDisplayName)) {
                encoder.WriteValue(this.lpDisplayName.value);
            }
            encoder.WriteStructDeferral(this.ServiceStatus);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpServiceName)) {
                this.lpServiceName.value = decoder.ReadUnsignedChar();
            }
            if ((null != this.lpDisplayName)) {
                this.lpDisplayName.value = decoder.ReadUnsignedChar();
            }
            decoder.ReadStructDeferral<SERVICE_STATUS>(ref this.ServiceStatus);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENUM_SERVICE_STATUSW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.lpServiceName);
            encoder.WritePointer(this.lpDisplayName);
            encoder.WriteFixedStruct(this.ServiceStatus, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.lpServiceName = decoder.ReadPointer<char>();
            this.lpDisplayName = decoder.ReadPointer<char>();
            this.ServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public RpcPointer<char> lpServiceName;
        public RpcPointer<char> lpDisplayName;
        public SERVICE_STATUS ServiceStatus;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpServiceName)) {
                encoder.WriteValue(this.lpServiceName.value);
            }
            if ((null != this.lpDisplayName)) {
                encoder.WriteValue(this.lpDisplayName.value);
            }
            encoder.WriteStructDeferral(this.ServiceStatus);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpServiceName)) {
                this.lpServiceName.value = decoder.ReadWideChar();
            }
            if ((null != this.lpDisplayName)) {
                this.lpDisplayName.value = decoder.ReadWideChar();
            }
            decoder.ReadStructDeferral<SERVICE_STATUS>(ref this.ServiceStatus);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENUM_SERVICE_STATUS_PROCESSA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.lpServiceName);
            encoder.WritePointer(this.lpDisplayName);
            encoder.WriteFixedStruct(this.ServiceStatusProcess, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.lpServiceName = decoder.ReadPointer<byte>();
            this.lpDisplayName = decoder.ReadPointer<byte>();
            this.ServiceStatusProcess = decoder.ReadFixedStruct<SERVICE_STATUS_PROCESS>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public RpcPointer<byte> lpServiceName;
        public RpcPointer<byte> lpDisplayName;
        public SERVICE_STATUS_PROCESS ServiceStatusProcess;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpServiceName)) {
                encoder.WriteValue(this.lpServiceName.value);
            }
            if ((null != this.lpDisplayName)) {
                encoder.WriteValue(this.lpDisplayName.value);
            }
            encoder.WriteStructDeferral(this.ServiceStatusProcess);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpServiceName)) {
                this.lpServiceName.value = decoder.ReadUnsignedChar();
            }
            if ((null != this.lpDisplayName)) {
                this.lpDisplayName.value = decoder.ReadUnsignedChar();
            }
            decoder.ReadStructDeferral<SERVICE_STATUS_PROCESS>(ref this.ServiceStatusProcess);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENUM_SERVICE_STATUS_PROCESSW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.lpServiceName);
            encoder.WritePointer(this.lpDisplayName);
            encoder.WriteFixedStruct(this.ServiceStatusProcess, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.lpServiceName = decoder.ReadPointer<char>();
            this.lpDisplayName = decoder.ReadPointer<char>();
            this.ServiceStatusProcess = decoder.ReadFixedStruct<SERVICE_STATUS_PROCESS>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public RpcPointer<char> lpServiceName;
        public RpcPointer<char> lpDisplayName;
        public SERVICE_STATUS_PROCESS ServiceStatusProcess;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.lpServiceName)) {
                encoder.WriteValue(this.lpServiceName.value);
            }
            if ((null != this.lpDisplayName)) {
                encoder.WriteValue(this.lpDisplayName.value);
            }
            encoder.WriteStructDeferral(this.ServiceStatusProcess);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.lpServiceName)) {
                this.lpServiceName.value = decoder.ReadWideChar();
            }
            if ((null != this.lpDisplayName)) {
                this.lpDisplayName.value = decoder.ReadWideChar();
            }
            decoder.ReadStructDeferral<SERVICE_STATUS_PROCESS>(ref this.ServiceStatusProcess);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_DESCRIPTION_WOW64 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwDescriptionOffset);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwDescriptionOffset = decoder.ReadUInt32();
        }
        public uint dwDescriptionOffset;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_FAILURE_ACTIONS_WOW64 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwResetPeriod);
            encoder.WriteValue(this.dwRebootMsgOffset);
            encoder.WriteValue(this.dwCommandOffset);
            encoder.WriteValue(this.cActions);
            encoder.WriteValue(this.dwsaActionsOffset);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwResetPeriod = decoder.ReadUInt32();
            this.dwRebootMsgOffset = decoder.ReadUInt32();
            this.dwCommandOffset = decoder.ReadUInt32();
            this.cActions = decoder.ReadUInt32();
            this.dwsaActionsOffset = decoder.ReadUInt32();
        }
        public uint dwResetPeriod;
        public uint dwRebootMsgOffset;
        public uint dwCommandOffset;
        public uint cActions;
        public uint dwsaActionsOffset;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_REQUIRED_PRIVILEGES_INFO_WOW64 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwRequiredPrivilegesOffset);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwRequiredPrivilegesOffset = decoder.ReadUInt32();
        }
        public uint dwRequiredPrivilegesOffset;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_RPC_REQUIRED_PRIVILEGES_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cbRequiredPrivileges);
            encoder.WritePointer(this.pRequiredPrivileges);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cbRequiredPrivileges = decoder.ReadUInt32();
            this.pRequiredPrivileges = decoder.ReadPointer<byte[]>();
        }
        public uint cbRequiredPrivileges;
        public RpcPointer<byte[]> pRequiredPrivileges;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pRequiredPrivileges)) {
                encoder.WriteArrayHeader(this.pRequiredPrivileges.value);
                for (int i = 0; (i < this.pRequiredPrivileges.value.Length); i++
                ) {
                    byte elem_0 = this.pRequiredPrivileges.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pRequiredPrivileges)) {
                this.pRequiredPrivileges.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.pRequiredPrivileges.value.Length); i++
                ) {
                    byte elem_0 = this.pRequiredPrivileges.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.pRequiredPrivileges.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct Unnamed_1 : Titanis.DceRpc.IRpcFixedStruct {
        public uint dwInfoLevel;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwInfoLevel);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.dwInfoLevel)) == 1)) {
                encoder.WritePointer(this.psd);
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    encoder.WritePointer(this.psfa);
                }
                else {
                    if ((((int)(this.dwInfoLevel)) == 3)) {
                        encoder.WritePointer(this.psda);
                    }
                    else {
                        if ((((int)(this.dwInfoLevel)) == 4)) {
                            encoder.WritePointer(this.psfaf);
                        }
                        else {
                            if ((((int)(this.dwInfoLevel)) == 5)) {
                                encoder.WritePointer(this.pssid);
                            }
                            else {
                                if ((((int)(this.dwInfoLevel)) == 6)) {
                                    encoder.WritePointer(this.psrp);
                                }
                                else {
                                    if ((((int)(this.dwInfoLevel)) == 7)) {
                                        encoder.WritePointer(this.psps);
                                    }
                                    else {
                                        if ((((int)(this.dwInfoLevel)) == 8)) {
                                            encoder.WritePointer(this.psti);
                                        }
                                        else {
                                            if ((((int)(this.dwInfoLevel)) == 9)) {
                                                encoder.WritePointer(this.pspn);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwInfoLevel = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.dwInfoLevel)) == 1)) {
                this.psd = decoder.ReadPointer<SERVICE_DESCRIPTIONA>();
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    this.psfa = decoder.ReadPointer<SERVICE_FAILURE_ACTIONSA>();
                }
                else {
                    if ((((int)(this.dwInfoLevel)) == 3)) {
                        this.psda = decoder.ReadPointer<SERVICE_DELAYED_AUTO_START_INFO>();
                    }
                    else {
                        if ((((int)(this.dwInfoLevel)) == 4)) {
                            this.psfaf = decoder.ReadPointer<SERVICE_FAILURE_ACTIONS_FLAG>();
                        }
                        else {
                            if ((((int)(this.dwInfoLevel)) == 5)) {
                                this.pssid = decoder.ReadPointer<SERVICE_SID_INFO>();
                            }
                            else {
                                if ((((int)(this.dwInfoLevel)) == 6)) {
                                    this.psrp = decoder.ReadPointer<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>();
                                }
                                else {
                                    if ((((int)(this.dwInfoLevel)) == 7)) {
                                        this.psps = decoder.ReadPointer<SERVICE_PRESHUTDOWN_INFO>();
                                    }
                                    else {
                                        if ((((int)(this.dwInfoLevel)) == 8)) {
                                            this.psti = decoder.ReadPointer<SERVICE_TRIGGER_INFO>();
                                        }
                                        else {
                                            if ((((int)(this.dwInfoLevel)) == 9)) {
                                                this.pspn = decoder.ReadPointer<SERVICE_PREFERRED_NODE_INFO>();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.dwInfoLevel)) == 1)) {
                if ((null != this.psd)) {
                    encoder.WriteFixedStruct(this.psd.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(this.psd.value);
                }
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    if ((null != this.psfa)) {
                        encoder.WriteFixedStruct(this.psfa.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                        encoder.WriteStructDeferral(this.psfa.value);
                    }
                }
                else {
                    if ((((int)(this.dwInfoLevel)) == 3)) {
                        if ((null != this.psda)) {
                            encoder.WriteFixedStruct(this.psda.value, Titanis.DceRpc.NdrAlignment._4Byte);
                            encoder.WriteStructDeferral(this.psda.value);
                        }
                    }
                    else {
                        if ((((int)(this.dwInfoLevel)) == 4)) {
                            if ((null != this.psfaf)) {
                                encoder.WriteFixedStruct(this.psfaf.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                encoder.WriteStructDeferral(this.psfaf.value);
                            }
                        }
                        else {
                            if ((((int)(this.dwInfoLevel)) == 5)) {
                                if ((null != this.pssid)) {
                                    encoder.WriteFixedStruct(this.pssid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                    encoder.WriteStructDeferral(this.pssid.value);
                                }
                            }
                            else {
                                if ((((int)(this.dwInfoLevel)) == 6)) {
                                    if ((null != this.psrp)) {
                                        encoder.WriteFixedStruct(this.psrp.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                        encoder.WriteStructDeferral(this.psrp.value);
                                    }
                                }
                                else {
                                    if ((((int)(this.dwInfoLevel)) == 7)) {
                                        if ((null != this.psps)) {
                                            encoder.WriteFixedStruct(this.psps.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                            encoder.WriteStructDeferral(this.psps.value);
                                        }
                                    }
                                    else {
                                        if ((((int)(this.dwInfoLevel)) == 8)) {
                                            if ((null != this.psti)) {
                                                encoder.WriteFixedStruct(this.psti.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                encoder.WriteStructDeferral(this.psti.value);
                                            }
                                        }
                                        else {
                                            if ((((int)(this.dwInfoLevel)) == 9)) {
                                                if ((null != this.pspn)) {
                                                    encoder.WriteFixedStruct(this.pspn.value, Titanis.DceRpc.NdrAlignment._2Byte);
                                                    encoder.WriteStructDeferral(this.pspn.value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.dwInfoLevel)) == 1)) {
                if ((null != this.psd)) {
                    this.psd.value = decoder.ReadFixedStruct<SERVICE_DESCRIPTIONA>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<SERVICE_DESCRIPTIONA>(ref this.psd.value);
                }
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    if ((null != this.psfa)) {
                        this.psfa.value = decoder.ReadFixedStruct<SERVICE_FAILURE_ACTIONSA>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        decoder.ReadStructDeferral<SERVICE_FAILURE_ACTIONSA>(ref this.psfa.value);
                    }
                }
                else {
                    if ((((int)(this.dwInfoLevel)) == 3)) {
                        if ((null != this.psda)) {
                            this.psda.value = decoder.ReadFixedStruct<SERVICE_DELAYED_AUTO_START_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
                            decoder.ReadStructDeferral<SERVICE_DELAYED_AUTO_START_INFO>(ref this.psda.value);
                        }
                    }
                    else {
                        if ((((int)(this.dwInfoLevel)) == 4)) {
                            if ((null != this.psfaf)) {
                                this.psfaf.value = decoder.ReadFixedStruct<SERVICE_FAILURE_ACTIONS_FLAG>(Titanis.DceRpc.NdrAlignment._4Byte);
                                decoder.ReadStructDeferral<SERVICE_FAILURE_ACTIONS_FLAG>(ref this.psfaf.value);
                            }
                        }
                        else {
                            if ((((int)(this.dwInfoLevel)) == 5)) {
                                if ((null != this.pssid)) {
                                    this.pssid.value = decoder.ReadFixedStruct<SERVICE_SID_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
                                    decoder.ReadStructDeferral<SERVICE_SID_INFO>(ref this.pssid.value);
                                }
                            }
                            else {
                                if ((((int)(this.dwInfoLevel)) == 6)) {
                                    if ((null != this.psrp)) {
                                        this.psrp.value = decoder.ReadFixedStruct<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                        decoder.ReadStructDeferral<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>(ref this.psrp.value);
                                    }
                                }
                                else {
                                    if ((((int)(this.dwInfoLevel)) == 7)) {
                                        if ((null != this.psps)) {
                                            this.psps.value = decoder.ReadFixedStruct<SERVICE_PRESHUTDOWN_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
                                            decoder.ReadStructDeferral<SERVICE_PRESHUTDOWN_INFO>(ref this.psps.value);
                                        }
                                    }
                                    else {
                                        if ((((int)(this.dwInfoLevel)) == 8)) {
                                            if ((null != this.psti)) {
                                                this.psti.value = decoder.ReadFixedStruct<SERVICE_TRIGGER_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                decoder.ReadStructDeferral<SERVICE_TRIGGER_INFO>(ref this.psti.value);
                                            }
                                        }
                                        else {
                                            if ((((int)(this.dwInfoLevel)) == 9)) {
                                                if ((null != this.pspn)) {
                                                    this.pspn.value = decoder.ReadFixedStruct<SERVICE_PREFERRED_NODE_INFO>(Titanis.DceRpc.NdrAlignment._2Byte);
                                                    decoder.ReadStructDeferral<SERVICE_PREFERRED_NODE_INFO>(ref this.pspn.value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public RpcPointer<SERVICE_DESCRIPTIONA> psd;
        public RpcPointer<SERVICE_FAILURE_ACTIONSA> psfa;
        public RpcPointer<SERVICE_DELAYED_AUTO_START_INFO> psda;
        public RpcPointer<SERVICE_FAILURE_ACTIONS_FLAG> psfaf;
        public RpcPointer<SERVICE_SID_INFO> pssid;
        public RpcPointer<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO> psrp;
        public RpcPointer<SERVICE_PRESHUTDOWN_INFO> psps;
        public RpcPointer<SERVICE_TRIGGER_INFO> psti;
        public RpcPointer<SERVICE_PREFERRED_NODE_INFO> pspn;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SC_RPC_CONFIG_INFOA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwInfoLevel);
            encoder.WriteUnion(this.unnamed_1);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwInfoLevel = decoder.ReadUInt32();
            this.unnamed_1 = decoder.ReadUnion<Unnamed_1>();
        }
        public uint dwInfoLevel;
        public Unnamed_1 unnamed_1;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.unnamed_1);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<Unnamed_1>(ref this.unnamed_1);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct Unnamed_2 : Titanis.DceRpc.IRpcFixedStruct {
        public uint dwInfoLevel;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwInfoLevel);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.dwInfoLevel)) == 1)) {
                encoder.WritePointer(this.psd);
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    encoder.WritePointer(this.psfa);
                }
                else {
                    if ((((int)(this.dwInfoLevel)) == 3)) {
                        encoder.WritePointer(this.psda);
                    }
                    else {
                        if ((((int)(this.dwInfoLevel)) == 4)) {
                            encoder.WritePointer(this.psfaf);
                        }
                        else {
                            if ((((int)(this.dwInfoLevel)) == 5)) {
                                encoder.WritePointer(this.pssid);
                            }
                            else {
                                if ((((int)(this.dwInfoLevel)) == 6)) {
                                    encoder.WritePointer(this.psrp);
                                }
                                else {
                                    if ((((int)(this.dwInfoLevel)) == 7)) {
                                        encoder.WritePointer(this.psps);
                                    }
                                    else {
                                        if ((((int)(this.dwInfoLevel)) == 8)) {
                                            encoder.WritePointer(this.psti);
                                        }
                                        else {
                                            if ((((int)(this.dwInfoLevel)) == 9)) {
                                                encoder.WritePointer(this.pspn);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwInfoLevel = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.dwInfoLevel)) == 1)) {
                this.psd = decoder.ReadPointer<SERVICE_DESCRIPTIONW>();
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    this.psfa = decoder.ReadPointer<SERVICE_FAILURE_ACTIONSW>();
                }
                else {
                    if ((((int)(this.dwInfoLevel)) == 3)) {
                        this.psda = decoder.ReadPointer<SERVICE_DELAYED_AUTO_START_INFO>();
                    }
                    else {
                        if ((((int)(this.dwInfoLevel)) == 4)) {
                            this.psfaf = decoder.ReadPointer<SERVICE_FAILURE_ACTIONS_FLAG>();
                        }
                        else {
                            if ((((int)(this.dwInfoLevel)) == 5)) {
                                this.pssid = decoder.ReadPointer<SERVICE_SID_INFO>();
                            }
                            else {
                                if ((((int)(this.dwInfoLevel)) == 6)) {
                                    this.psrp = decoder.ReadPointer<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>();
                                }
                                else {
                                    if ((((int)(this.dwInfoLevel)) == 7)) {
                                        this.psps = decoder.ReadPointer<SERVICE_PRESHUTDOWN_INFO>();
                                    }
                                    else {
                                        if ((((int)(this.dwInfoLevel)) == 8)) {
                                            this.psti = decoder.ReadPointer<SERVICE_TRIGGER_INFO>();
                                        }
                                        else {
                                            if ((((int)(this.dwInfoLevel)) == 9)) {
                                                this.pspn = decoder.ReadPointer<SERVICE_PREFERRED_NODE_INFO>();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.dwInfoLevel)) == 1)) {
                if ((null != this.psd)) {
                    encoder.WriteFixedStruct(this.psd.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(this.psd.value);
                }
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    if ((null != this.psfa)) {
                        encoder.WriteFixedStruct(this.psfa.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                        encoder.WriteStructDeferral(this.psfa.value);
                    }
                }
                else {
                    if ((((int)(this.dwInfoLevel)) == 3)) {
                        if ((null != this.psda)) {
                            encoder.WriteFixedStruct(this.psda.value, Titanis.DceRpc.NdrAlignment._4Byte);
                            encoder.WriteStructDeferral(this.psda.value);
                        }
                    }
                    else {
                        if ((((int)(this.dwInfoLevel)) == 4)) {
                            if ((null != this.psfaf)) {
                                encoder.WriteFixedStruct(this.psfaf.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                encoder.WriteStructDeferral(this.psfaf.value);
                            }
                        }
                        else {
                            if ((((int)(this.dwInfoLevel)) == 5)) {
                                if ((null != this.pssid)) {
                                    encoder.WriteFixedStruct(this.pssid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                    encoder.WriteStructDeferral(this.pssid.value);
                                }
                            }
                            else {
                                if ((((int)(this.dwInfoLevel)) == 6)) {
                                    if ((null != this.psrp)) {
                                        encoder.WriteFixedStruct(this.psrp.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                        encoder.WriteStructDeferral(this.psrp.value);
                                    }
                                }
                                else {
                                    if ((((int)(this.dwInfoLevel)) == 7)) {
                                        if ((null != this.psps)) {
                                            encoder.WriteFixedStruct(this.psps.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                            encoder.WriteStructDeferral(this.psps.value);
                                        }
                                    }
                                    else {
                                        if ((((int)(this.dwInfoLevel)) == 8)) {
                                            if ((null != this.psti)) {
                                                encoder.WriteFixedStruct(this.psti.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                encoder.WriteStructDeferral(this.psti.value);
                                            }
                                        }
                                        else {
                                            if ((((int)(this.dwInfoLevel)) == 9)) {
                                                if ((null != this.pspn)) {
                                                    encoder.WriteFixedStruct(this.pspn.value, Titanis.DceRpc.NdrAlignment._2Byte);
                                                    encoder.WriteStructDeferral(this.pspn.value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.dwInfoLevel)) == 1)) {
                if ((null != this.psd)) {
                    this.psd.value = decoder.ReadFixedStruct<SERVICE_DESCRIPTIONW>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<SERVICE_DESCRIPTIONW>(ref this.psd.value);
                }
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    if ((null != this.psfa)) {
                        this.psfa.value = decoder.ReadFixedStruct<SERVICE_FAILURE_ACTIONSW>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        decoder.ReadStructDeferral<SERVICE_FAILURE_ACTIONSW>(ref this.psfa.value);
                    }
                }
                else {
                    if ((((int)(this.dwInfoLevel)) == 3)) {
                        if ((null != this.psda)) {
                            this.psda.value = decoder.ReadFixedStruct<SERVICE_DELAYED_AUTO_START_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
                            decoder.ReadStructDeferral<SERVICE_DELAYED_AUTO_START_INFO>(ref this.psda.value);
                        }
                    }
                    else {
                        if ((((int)(this.dwInfoLevel)) == 4)) {
                            if ((null != this.psfaf)) {
                                this.psfaf.value = decoder.ReadFixedStruct<SERVICE_FAILURE_ACTIONS_FLAG>(Titanis.DceRpc.NdrAlignment._4Byte);
                                decoder.ReadStructDeferral<SERVICE_FAILURE_ACTIONS_FLAG>(ref this.psfaf.value);
                            }
                        }
                        else {
                            if ((((int)(this.dwInfoLevel)) == 5)) {
                                if ((null != this.pssid)) {
                                    this.pssid.value = decoder.ReadFixedStruct<SERVICE_SID_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
                                    decoder.ReadStructDeferral<SERVICE_SID_INFO>(ref this.pssid.value);
                                }
                            }
                            else {
                                if ((((int)(this.dwInfoLevel)) == 6)) {
                                    if ((null != this.psrp)) {
                                        this.psrp.value = decoder.ReadFixedStruct<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                        decoder.ReadStructDeferral<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO>(ref this.psrp.value);
                                    }
                                }
                                else {
                                    if ((((int)(this.dwInfoLevel)) == 7)) {
                                        if ((null != this.psps)) {
                                            this.psps.value = decoder.ReadFixedStruct<SERVICE_PRESHUTDOWN_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
                                            decoder.ReadStructDeferral<SERVICE_PRESHUTDOWN_INFO>(ref this.psps.value);
                                        }
                                    }
                                    else {
                                        if ((((int)(this.dwInfoLevel)) == 8)) {
                                            if ((null != this.psti)) {
                                                this.psti.value = decoder.ReadFixedStruct<SERVICE_TRIGGER_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                decoder.ReadStructDeferral<SERVICE_TRIGGER_INFO>(ref this.psti.value);
                                            }
                                        }
                                        else {
                                            if ((((int)(this.dwInfoLevel)) == 9)) {
                                                if ((null != this.pspn)) {
                                                    this.pspn.value = decoder.ReadFixedStruct<SERVICE_PREFERRED_NODE_INFO>(Titanis.DceRpc.NdrAlignment._2Byte);
                                                    decoder.ReadStructDeferral<SERVICE_PREFERRED_NODE_INFO>(ref this.pspn.value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public RpcPointer<SERVICE_DESCRIPTIONW> psd;
        public RpcPointer<SERVICE_FAILURE_ACTIONSW> psfa;
        public RpcPointer<SERVICE_DELAYED_AUTO_START_INFO> psda;
        public RpcPointer<SERVICE_FAILURE_ACTIONS_FLAG> psfaf;
        public RpcPointer<SERVICE_SID_INFO> pssid;
        public RpcPointer<SERVICE_RPC_REQUIRED_PRIVILEGES_INFO> psrp;
        public RpcPointer<SERVICE_PRESHUTDOWN_INFO> psps;
        public RpcPointer<SERVICE_TRIGGER_INFO> psti;
        public RpcPointer<SERVICE_PREFERRED_NODE_INFO> pspn;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SC_RPC_CONFIG_INFOW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwInfoLevel);
            encoder.WriteUnion(this.unnamed_1);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwInfoLevel = decoder.ReadUInt32();
            this.unnamed_1 = decoder.ReadUnion<Unnamed_2>();
        }
        public uint dwInfoLevel;
        public Unnamed_2 unnamed_1;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.unnamed_1);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<Unnamed_2>(ref this.unnamed_1);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_1 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ullThreadId);
            encoder.WriteValue(this.dwNotifyMask);
            if ((this.CallbackAddressArray == null)) {
                this.CallbackAddressArray = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.CallbackAddressArray[i];
                encoder.WriteValue(elem_0);
            }
            if ((this.CallbackParamAddressArray == null)) {
                this.CallbackParamAddressArray = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.CallbackParamAddressArray[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteFixedStruct(this.ServiceStatus, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteValue(this.dwNotificationStatus);
            encoder.WriteValue(this.dwSequence);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ullThreadId = decoder.ReadUInt64();
            this.dwNotifyMask = decoder.ReadUInt32();
            if ((this.CallbackAddressArray == null)) {
                this.CallbackAddressArray = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.CallbackAddressArray[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.CallbackAddressArray[i] = elem_0;
            }
            if ((this.CallbackParamAddressArray == null)) {
                this.CallbackParamAddressArray = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.CallbackParamAddressArray[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.CallbackParamAddressArray[i] = elem_0;
            }
            this.ServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS_PROCESS>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.dwNotificationStatus = decoder.ReadUInt32();
            this.dwSequence = decoder.ReadUInt32();
        }
        public ulong ullThreadId;
        public uint dwNotifyMask;
        public byte[] CallbackAddressArray;
        public byte[] CallbackParamAddressArray;
        public SERVICE_STATUS_PROCESS ServiceStatus;
        public uint dwNotificationStatus;
        public uint dwSequence;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ServiceStatus);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SERVICE_STATUS_PROCESS>(ref this.ServiceStatus);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_2 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.ullThreadId);
            encoder.WriteValue(this.dwNotifyMask);
            if ((this.CallbackAddressArray == null)) {
                this.CallbackAddressArray = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.CallbackAddressArray[i];
                encoder.WriteValue(elem_0);
            }
            if ((this.CallbackParamAddressArray == null)) {
                this.CallbackParamAddressArray = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.CallbackParamAddressArray[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteFixedStruct(this.ServiceStatus, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteValue(this.dwNotificationStatus);
            encoder.WriteValue(this.dwSequence);
            encoder.WriteValue(this.dwNotificationTriggered);
            encoder.WritePointer(this.pszServiceNames);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ullThreadId = decoder.ReadUInt64();
            this.dwNotifyMask = decoder.ReadUInt32();
            if ((this.CallbackAddressArray == null)) {
                this.CallbackAddressArray = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.CallbackAddressArray[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.CallbackAddressArray[i] = elem_0;
            }
            if ((this.CallbackParamAddressArray == null)) {
                this.CallbackParamAddressArray = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.CallbackParamAddressArray[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.CallbackParamAddressArray[i] = elem_0;
            }
            this.ServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS_PROCESS>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.dwNotificationStatus = decoder.ReadUInt32();
            this.dwSequence = decoder.ReadUInt32();
            this.dwNotificationTriggered = decoder.ReadUInt32();
            this.pszServiceNames = decoder.ReadPointer<string>();
        }
        public ulong ullThreadId;
        public uint dwNotifyMask;
        public byte[] CallbackAddressArray;
        public byte[] CallbackParamAddressArray;
        public SERVICE_STATUS_PROCESS ServiceStatus;
        public uint dwNotificationStatus;
        public uint dwSequence;
        public uint dwNotificationTriggered;
        public RpcPointer<string> pszServiceNames;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ServiceStatus);
            if ((null != this.pszServiceNames)) {
                encoder.WriteWideCharString(this.pszServiceNames.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SERVICE_STATUS_PROCESS>(ref this.ServiceStatus);
            if ((null != this.pszServiceNames)) {
                this.pszServiceNames.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct Unnamed_3 : Titanis.DceRpc.IRpcFixedStruct {
        public uint dwInfoLevel;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwInfoLevel);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.dwInfoLevel)) == 1)) {
                encoder.WritePointer(this.pStatusChangeParam1);
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    encoder.WritePointer(this.pStatusChangeParams);
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwInfoLevel = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.dwInfoLevel)) == 1)) {
                this.pStatusChangeParam1 = decoder.ReadPointer<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_1>();
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    this.pStatusChangeParams = decoder.ReadPointer<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_2>();
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.dwInfoLevel)) == 1)) {
                if ((null != this.pStatusChangeParam1)) {
                    encoder.WriteFixedStruct(this.pStatusChangeParam1.value, Titanis.DceRpc.NdrAlignment._8Byte);
                    encoder.WriteStructDeferral(this.pStatusChangeParam1.value);
                }
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    if ((null != this.pStatusChangeParams)) {
                        encoder.WriteFixedStruct(this.pStatusChangeParams.value, Titanis.DceRpc.NdrAlignment._8Byte);
                        encoder.WriteStructDeferral(this.pStatusChangeParams.value);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.dwInfoLevel)) == 1)) {
                if ((null != this.pStatusChangeParam1)) {
                    this.pStatusChangeParam1.value = decoder.ReadFixedStruct<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_1>(Titanis.DceRpc.NdrAlignment._8Byte);
                    decoder.ReadStructDeferral<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_1>(ref this.pStatusChangeParam1.value);
                }
            }
            else {
                if ((((int)(this.dwInfoLevel)) == 2)) {
                    if ((null != this.pStatusChangeParams)) {
                        this.pStatusChangeParams.value = decoder.ReadFixedStruct<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_2>(Titanis.DceRpc.NdrAlignment._8Byte);
                        decoder.ReadStructDeferral<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_2>(ref this.pStatusChangeParams.value);
                    }
                }
            }
        }
        public RpcPointer<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_1> pStatusChangeParam1;
        public RpcPointer<SERVICE_NOTIFY_STATUS_CHANGE_PARAMS_2> pStatusChangeParams;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SC_RPC_NOTIFY_PARAMS : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwInfoLevel);
            encoder.WriteUnion(this.unnamed_1);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwInfoLevel = decoder.ReadUInt32();
            this.unnamed_1 = decoder.ReadUnion<Unnamed_3>();
        }
        public uint dwInfoLevel;
        public Unnamed_3 unnamed_1;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.unnamed_1);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<Unnamed_3>(ref this.unnamed_1);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SC_RPC_NOTIFY_PARAMS_LIST : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cElements);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cElements = decoder.ReadUInt32();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.NotifyParamsArray);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.NotifyParamsArray = decoder.ReadArrayHeader<SC_RPC_NOTIFY_PARAMS>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.NotifyParamsArray.Length); i++
            ) {
                SC_RPC_NOTIFY_PARAMS elem_0 = this.NotifyParamsArray[i];
                encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.NotifyParamsArray.Length); i++
            ) {
                SC_RPC_NOTIFY_PARAMS elem_0 = this.NotifyParamsArray[i];
                elem_0 = decoder.ReadFixedStruct<SC_RPC_NOTIFY_PARAMS>(Titanis.DceRpc.NdrAlignment.NativePtr);
                this.NotifyParamsArray[i] = elem_0;
            }
        }
        public uint cElements;
        public SC_RPC_NOTIFY_PARAMS[] NotifyParamsArray;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.NotifyParamsArray.Length); i++
            ) {
                SC_RPC_NOTIFY_PARAMS elem_0 = this.NotifyParamsArray[i];
                encoder.WriteStructDeferral(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.NotifyParamsArray.Length); i++
            ) {
                SC_RPC_NOTIFY_PARAMS elem_0 = this.NotifyParamsArray[i];
                decoder.ReadStructDeferral<SC_RPC_NOTIFY_PARAMS>(ref elem_0);
                this.NotifyParamsArray[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_CONTROL_STATUS_REASON_IN_PARAMSA : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwReason);
            encoder.WritePointer(this.pszComment);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwReason = decoder.ReadUInt32();
            this.pszComment = decoder.ReadPointer<string>();
        }
        public uint dwReason;
        public RpcPointer<string> pszComment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pszComment)) {
                encoder.WriteUnsignedCharString(this.pszComment.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pszComment)) {
                this.pszComment.value = decoder.ReadUnsignedCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.ServiceStatus, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS_PROCESS>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public SERVICE_STATUS_PROCESS ServiceStatus;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ServiceStatus);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SERVICE_STATUS_PROCESS>(ref this.ServiceStatus);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SC_RPC_SERVICE_CONTROL_IN_PARAMSA : Titanis.DceRpc.IRpcFixedStruct {
        public uint unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.unionSwitch);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WritePointer(this.psrInParams);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.psrInParams = decoder.ReadPointer<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSA>();
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                if ((null != this.psrInParams)) {
                    encoder.WriteFixedStruct(this.psrInParams.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(this.psrInParams.value);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                if ((null != this.psrInParams)) {
                    this.psrInParams.value = decoder.ReadFixedStruct<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSA>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSA>(ref this.psrInParams.value);
                }
            }
        }
        public RpcPointer<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSA> psrInParams;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SC_RPC_SERVICE_CONTROL_OUT_PARAMSA : Titanis.DceRpc.IRpcFixedStruct {
        public uint unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.unionSwitch);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WritePointer(this.psrOutParams);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.psrOutParams = decoder.ReadPointer<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>();
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                if ((null != this.psrOutParams)) {
                    encoder.WriteFixedStruct(this.psrOutParams.value, Titanis.DceRpc.NdrAlignment._4Byte);
                    encoder.WriteStructDeferral(this.psrOutParams.value);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                if ((null != this.psrOutParams)) {
                    this.psrOutParams.value = decoder.ReadFixedStruct<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>(Titanis.DceRpc.NdrAlignment._4Byte);
                    decoder.ReadStructDeferral<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>(ref this.psrOutParams.value);
                }
            }
        }
        public RpcPointer<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS> psrOutParams;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SERVICE_CONTROL_STATUS_REASON_IN_PARAMSW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwReason);
            encoder.WritePointer(this.pszComment);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwReason = decoder.ReadUInt32();
            this.pszComment = decoder.ReadPointer<string>();
        }
        public uint dwReason;
        public RpcPointer<string> pszComment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pszComment)) {
                encoder.WriteWideCharString(this.pszComment.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pszComment)) {
                this.pszComment.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SC_RPC_SERVICE_CONTROL_IN_PARAMSW : Titanis.DceRpc.IRpcFixedStruct {
        public uint unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.unionSwitch);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WritePointer(this.psrInParams);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.psrInParams = decoder.ReadPointer<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSW>();
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                if ((null != this.psrInParams)) {
                    encoder.WriteFixedStruct(this.psrInParams.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(this.psrInParams.value);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                if ((null != this.psrInParams)) {
                    this.psrInParams.value = decoder.ReadFixedStruct<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSW>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSW>(ref this.psrInParams.value);
                }
            }
        }
        public RpcPointer<SERVICE_CONTROL_STATUS_REASON_IN_PARAMSW> psrInParams;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct SC_RPC_SERVICE_CONTROL_OUT_PARAMSW : Titanis.DceRpc.IRpcFixedStruct {
        public uint unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.unionSwitch);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WritePointer(this.psrOutParams);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.psrOutParams = decoder.ReadPointer<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>();
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                if ((null != this.psrOutParams)) {
                    encoder.WriteFixedStruct(this.psrOutParams.value, Titanis.DceRpc.NdrAlignment._4Byte);
                    encoder.WriteStructDeferral(this.psrOutParams.value);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                if ((null != this.psrOutParams)) {
                    this.psrOutParams.value = decoder.ReadFixedStruct<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>(Titanis.DceRpc.NdrAlignment._4Byte);
                    decoder.ReadStructDeferral<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS>(ref this.psrOutParams.value);
                }
            }
        }
        public RpcPointer<SERVICE_CONTROL_STATUS_REASON_OUT_PARAMS> psrOutParams;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("367abb81-9844-35f1-ad32-98f038001003")]
    [Titanis.DceRpc.RpcVersionAttribute(2, 0)]
    public interface svcctl {
        Task<uint> RCloseServiceHandle(RpcPointer<Titanis.DceRpc.RpcContextHandle> hSCObject, System.Threading.CancellationToken cancellationToken);
        Task<uint> RControlService(Titanis.DceRpc.RpcContextHandle hService, uint dwControl, RpcPointer<SERVICE_STATUS> lpServiceStatus, System.Threading.CancellationToken cancellationToken);
        Task<uint> RDeleteService(Titanis.DceRpc.RpcContextHandle hService, System.Threading.CancellationToken cancellationToken);
        Task<uint> RLockServiceDatabase(Titanis.DceRpc.RpcContextHandle hSCManager, RpcPointer<Titanis.DceRpc.RpcContextHandle> lpLock, System.Threading.CancellationToken cancellationToken);
        Task<uint> RQueryServiceObjectSecurity(Titanis.DceRpc.RpcContextHandle hService, uint dwSecurityInformation, RpcPointer<byte[]> lpSecurityDescriptor, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task<uint> RSetServiceObjectSecurity(Titanis.DceRpc.RpcContextHandle hService, uint dwSecurityInformation, byte[] lpSecurityDescriptor, uint cbBufSize, System.Threading.CancellationToken cancellationToken);
        Task<uint> RQueryServiceStatus(Titanis.DceRpc.RpcContextHandle hService, RpcPointer<SERVICE_STATUS> lpServiceStatus, System.Threading.CancellationToken cancellationToken);
        Task<uint> RSetServiceStatus(Titanis.DceRpc.RpcContextHandle hServiceStatus, SERVICE_STATUS lpServiceStatus, System.Threading.CancellationToken cancellationToken);
        Task<uint> RUnlockServiceDatabase(RpcPointer<Titanis.DceRpc.RpcContextHandle> Lock, System.Threading.CancellationToken cancellationToken);
        Task<uint> RNotifyBootConfigStatus(string lpMachineName, uint BootAcceptable, System.Threading.CancellationToken cancellationToken);
        Task Opnum10NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> RChangeServiceConfigW(Titanis.DceRpc.RpcContextHandle hService, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, string lpDisplayName, System.Threading.CancellationToken cancellationToken);
        Task<uint> RCreateServiceW(
                    Titanis.DceRpc.RpcContextHandle hSCManager, 
                    string lpServiceName, 
                    string lpDisplayName, 
                    uint dwDesiredAccess, 
                    uint dwServiceType, 
                    uint dwStartType, 
                    uint dwErrorControl, 
                    string lpBinaryPathName, 
                    string lpLoadOrderGroup, 
                    RpcPointer<uint> lpdwTagId, 
                    byte[] lpDependencies, 
                    uint dwDependSize, 
                    string lpServiceStartName, 
                    byte[] lpPassword, 
                    uint dwPwSize, 
                    RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, 
                    System.Threading.CancellationToken cancellationToken);
        Task<uint> REnumDependentServicesW(Titanis.DceRpc.RpcContextHandle hService, uint dwServiceState, RpcPointer<byte[]> lpServices, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, System.Threading.CancellationToken cancellationToken);
        Task<uint> REnumServicesStatusW(Titanis.DceRpc.RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, System.Threading.CancellationToken cancellationToken);
        Task<uint> ROpenSCManagerW(string lpMachineName, string lpDatabaseName, uint dwDesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> lpScHandle, System.Threading.CancellationToken cancellationToken);
        Task<uint> ROpenServiceW(Titanis.DceRpc.RpcContextHandle hSCManager, string lpServiceName, uint dwDesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, System.Threading.CancellationToken cancellationToken);
        Task<uint> RQueryServiceConfigW(Titanis.DceRpc.RpcContextHandle hService, RpcPointer<QUERY_SERVICE_CONFIGW> lpServiceConfig, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task<uint> RQueryServiceLockStatusW(Titanis.DceRpc.RpcContextHandle hSCManager, RpcPointer<QUERY_SERVICE_LOCK_STATUSW> lpLockStatus, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task<uint> RStartServiceW(Titanis.DceRpc.RpcContextHandle hService, uint argc, STRING_PTRSW[] argv, System.Threading.CancellationToken cancellationToken);
        Task<uint> RGetServiceDisplayNameW(Titanis.DceRpc.RpcContextHandle hSCManager, string lpServiceName, RpcPointer<string> lpDisplayName, RpcPointer<uint> lpcchBuffer, System.Threading.CancellationToken cancellationToken);
        Task<uint> RGetServiceKeyNameW(Titanis.DceRpc.RpcContextHandle hSCManager, string lpDisplayName, RpcPointer<string> lpServiceName, RpcPointer<uint> lpcchBuffer, System.Threading.CancellationToken cancellationToken);
        Task Opnum22NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> RChangeServiceConfigA(Titanis.DceRpc.RpcContextHandle hService, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, string lpDisplayName, System.Threading.CancellationToken cancellationToken);
        Task<uint> RCreateServiceA(
                    Titanis.DceRpc.RpcContextHandle hSCManager, 
                    string lpServiceName, 
                    string lpDisplayName, 
                    uint dwDesiredAccess, 
                    uint dwServiceType, 
                    uint dwStartType, 
                    uint dwErrorControl, 
                    string lpBinaryPathName, 
                    string lpLoadOrderGroup, 
                    RpcPointer<uint> lpdwTagId, 
                    byte[] lpDependencies, 
                    uint dwDependSize, 
                    string lpServiceStartName, 
                    byte[] lpPassword, 
                    uint dwPwSize, 
                    RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, 
                    System.Threading.CancellationToken cancellationToken);
        Task<uint> REnumDependentServicesA(Titanis.DceRpc.RpcContextHandle hService, uint dwServiceState, RpcPointer<byte[]> lpServices, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, System.Threading.CancellationToken cancellationToken);
        Task<uint> REnumServicesStatusA(Titanis.DceRpc.RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, System.Threading.CancellationToken cancellationToken);
        Task<uint> ROpenSCManagerA(string lpMachineName, string lpDatabaseName, uint dwDesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> lpScHandle, System.Threading.CancellationToken cancellationToken);
        Task<uint> ROpenServiceA(Titanis.DceRpc.RpcContextHandle hSCManager, string lpServiceName, uint dwDesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, System.Threading.CancellationToken cancellationToken);
        Task<uint> RQueryServiceConfigA(Titanis.DceRpc.RpcContextHandle hService, RpcPointer<QUERY_SERVICE_CONFIGA> lpServiceConfig, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task<uint> RQueryServiceLockStatusA(Titanis.DceRpc.RpcContextHandle hSCManager, RpcPointer<QUERY_SERVICE_LOCK_STATUSA> lpLockStatus, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task<uint> RStartServiceA(Titanis.DceRpc.RpcContextHandle hService, uint argc, STRING_PTRSA[] argv, System.Threading.CancellationToken cancellationToken);
        Task<uint> RGetServiceDisplayNameA(Titanis.DceRpc.RpcContextHandle hSCManager, string lpServiceName, RpcPointer<string> lpDisplayName, RpcPointer<uint> lpcchBuffer, System.Threading.CancellationToken cancellationToken);
        Task<uint> RGetServiceKeyNameA(Titanis.DceRpc.RpcContextHandle hSCManager, string lpDisplayName, RpcPointer<string> lpKeyName, RpcPointer<uint> lpcchBuffer, System.Threading.CancellationToken cancellationToken);
        Task Opnum34NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> REnumServiceGroupW(Titanis.DceRpc.RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, System.Threading.CancellationToken cancellationToken);
        Task<uint> RChangeServiceConfig2A(Titanis.DceRpc.RpcContextHandle hService, SC_RPC_CONFIG_INFOA Info, System.Threading.CancellationToken cancellationToken);
        Task<uint> RChangeServiceConfig2W(Titanis.DceRpc.RpcContextHandle hService, SC_RPC_CONFIG_INFOW Info, System.Threading.CancellationToken cancellationToken);
        Task<uint> RQueryServiceConfig2A(Titanis.DceRpc.RpcContextHandle hService, uint dwInfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task<uint> RQueryServiceConfig2W(Titanis.DceRpc.RpcContextHandle hService, uint dwInfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task<uint> RQueryServiceStatusEx(Titanis.DceRpc.RpcContextHandle hService, SC_STATUS_TYPE InfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken);
        Task<uint> REnumServicesStatusExA(Titanis.DceRpc.RpcContextHandle hSCManager, SC_ENUM_TYPE InfoLevel, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, System.Threading.CancellationToken cancellationToken);
        Task<uint> REnumServicesStatusExW(Titanis.DceRpc.RpcContextHandle hSCManager, SC_ENUM_TYPE InfoLevel, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, System.Threading.CancellationToken cancellationToken);
        Task Opnum43NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> RCreateServiceWOW64A(
                    Titanis.DceRpc.RpcContextHandle hSCManager, 
                    string lpServiceName, 
                    string lpDisplayName, 
                    uint dwDesiredAccess, 
                    uint dwServiceType, 
                    uint dwStartType, 
                    uint dwErrorControl, 
                    string lpBinaryPathName, 
                    string lpLoadOrderGroup, 
                    RpcPointer<uint> lpdwTagId, 
                    byte[] lpDependencies, 
                    uint dwDependSize, 
                    string lpServiceStartName, 
                    byte[] lpPassword, 
                    uint dwPwSize, 
                    RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, 
                    System.Threading.CancellationToken cancellationToken);
        Task<uint> RCreateServiceWOW64W(
                    Titanis.DceRpc.RpcContextHandle hSCManager, 
                    string lpServiceName, 
                    string lpDisplayName, 
                    uint dwDesiredAccess, 
                    uint dwServiceType, 
                    uint dwStartType, 
                    uint dwErrorControl, 
                    string lpBinaryPathName, 
                    string lpLoadOrderGroup, 
                    RpcPointer<uint> lpdwTagId, 
                    byte[] lpDependencies, 
                    uint dwDependSize, 
                    string lpServiceStartName, 
                    byte[] lpPassword, 
                    uint dwPwSize, 
                    RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, 
                    System.Threading.CancellationToken cancellationToken);
        Task Opnum46NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> RNotifyServiceStatusChange(Titanis.DceRpc.RpcContextHandle hService, SC_RPC_NOTIFY_PARAMS NotifyParams, System.Guid pClientProcessGuid, RpcPointer<System.Guid> pSCMProcessGuid, RpcPointer<int> pfCreateRemoteQueue, RpcPointer<Titanis.DceRpc.RpcContextHandle> phNotify, System.Threading.CancellationToken cancellationToken);
        Task<int> RGetNotifyResults(Titanis.DceRpc.RpcContextHandle hNotify, RpcPointer<RpcPointer<SC_RPC_NOTIFY_PARAMS_LIST>> ppNotifyParams, System.Threading.CancellationToken cancellationToken);
        Task<uint> RCloseNotifyHandle(RpcPointer<Titanis.DceRpc.RpcContextHandle> phNotify, RpcPointer<int> pfApcFired, System.Threading.CancellationToken cancellationToken);
        Task<uint> RControlServiceExA(Titanis.DceRpc.RpcContextHandle hService, uint dwControl, uint dwInfoLevel, SC_RPC_SERVICE_CONTROL_IN_PARAMSA pControlInParams, RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSA> pControlOutParams, System.Threading.CancellationToken cancellationToken);
        Task<uint> RControlServiceExW(Titanis.DceRpc.RpcContextHandle hService, uint dwControl, uint dwInfoLevel, SC_RPC_SERVICE_CONTROL_IN_PARAMSW pControlInParams, RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSW> pControlOutParams, System.Threading.CancellationToken cancellationToken);
        Task Opnum52NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum53NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum54NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum55NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> RQueryServiceConfigEx(Titanis.DceRpc.RpcContextHandle hService, uint dwInfoLevel, RpcPointer<SC_RPC_CONFIG_INFOW> pInfo, System.Threading.CancellationToken cancellationToken);
        Task Opnum57NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum58NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum59NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> RCreateWowService(
                    Titanis.DceRpc.RpcContextHandle hSCManager, 
                    string lpServiceName, 
                    string lpDisplayName, 
                    uint dwDesiredAccess, 
                    uint dwServiceType, 
                    uint dwStartType, 
                    uint dwErrorControl, 
                    string lpBinaryPathName, 
                    string lpLoadOrderGroup, 
                    RpcPointer<uint> lpdwTagId, 
                    byte[] lpDependencies, 
                    uint dwDependSize, 
                    string lpServiceStartName, 
                    byte[] lpPassword, 
                    uint dwPwSize, 
                    ushort dwServiceWowType, 
                    RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, 
                    System.Threading.CancellationToken cancellationToken);
        Task<uint> ROpenSCManager2(string DatabaseName, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> ScmHandle, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("367abb81-9844-35f1-ad32-98f038001003")]
    public class svcctlClientProxy : Titanis.DceRpc.Client.RpcClientProxy, svcctl, Titanis.DceRpc.IRpcClientProxy {
        private static System.Guid _interfaceUuid = new System.Guid("367abb81-9844-35f1-ad32-98f038001003");
        public override System.Guid InterfaceUuid {
            get {
                return _interfaceUuid;
            }
        }
        public override Titanis.DceRpc.RpcVersion InterfaceVersion {
            get {
                return new Titanis.DceRpc.RpcVersion(2, 0);
            }
        }
        public virtual async Task<uint> RCloseServiceHandle(RpcPointer<Titanis.DceRpc.RpcContextHandle> hSCObject, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCObject.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            hSCObject.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RControlService(Titanis.DceRpc.RpcContextHandle hService, uint dwControl, RpcPointer<SERVICE_STATUS> lpServiceStatus, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwControl);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpServiceStatus.value = decoder.ReadFixedStruct<SERVICE_STATUS>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<SERVICE_STATUS>(ref lpServiceStatus.value);
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RDeleteService(Titanis.DceRpc.RpcContextHandle hService, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RLockServiceDatabase(Titanis.DceRpc.RpcContextHandle hSCManager, RpcPointer<Titanis.DceRpc.RpcContextHandle> lpLock, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpLock.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RQueryServiceObjectSecurity(Titanis.DceRpc.RpcContextHandle hService, uint dwSecurityInformation, RpcPointer<byte[]> lpSecurityDescriptor, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwSecurityInformation);
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpSecurityDescriptor.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpSecurityDescriptor.value.Length); i++
            ) {
                byte elem_0 = lpSecurityDescriptor.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpSecurityDescriptor.value[i] = elem_0;
            }
            pcbBytesNeeded.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RSetServiceObjectSecurity(Titanis.DceRpc.RpcContextHandle hService, uint dwSecurityInformation, byte[] lpSecurityDescriptor, uint cbBufSize, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwSecurityInformation);
            if ((lpSecurityDescriptor != null)) {
                encoder.WriteArrayHeader(lpSecurityDescriptor);
                for (int i = 0; (i < lpSecurityDescriptor.Length); i++
                ) {
                    byte elem_0 = lpSecurityDescriptor[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RQueryServiceStatus(Titanis.DceRpc.RpcContextHandle hService, RpcPointer<SERVICE_STATUS> lpServiceStatus, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpServiceStatus.value = decoder.ReadFixedStruct<SERVICE_STATUS>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<SERVICE_STATUS>(ref lpServiceStatus.value);
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RSetServiceStatus(Titanis.DceRpc.RpcContextHandle hServiceStatus, SERVICE_STATUS lpServiceStatus, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hServiceStatus);
            encoder.WriteFixedStruct(lpServiceStatus, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(lpServiceStatus);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RUnlockServiceDatabase(RpcPointer<Titanis.DceRpc.RpcContextHandle> Lock, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(Lock.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Lock.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RNotifyBootConfigStatus(string lpMachineName, uint BootAcceptable, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((lpMachineName == null));
            if ((lpMachineName != null)) {
                encoder.WriteWideCharString(lpMachineName);
            }
            encoder.WriteValue(BootAcceptable);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum10NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<uint> RChangeServiceConfigW(Titanis.DceRpc.RpcContextHandle hService, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, string lpDisplayName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwStartType);
            encoder.WriteValue(dwErrorControl);
            encoder.WriteUniqueReferentId((lpBinaryPathName == null));
            if ((lpBinaryPathName != null)) {
                encoder.WriteWideCharString(lpBinaryPathName);
            }
            encoder.WriteUniqueReferentId((lpLoadOrderGroup == null));
            if ((lpLoadOrderGroup != null)) {
                encoder.WriteWideCharString(lpLoadOrderGroup);
            }
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteUniqueReferentId((lpDependencies == null));
            if ((lpDependencies != null)) {
                encoder.WriteArrayHeader(lpDependencies);
                for (int i = 0; (i < lpDependencies.Length); i++
                ) {
                    byte elem_0 = lpDependencies[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwDependSize);
            encoder.WriteUniqueReferentId((lpServiceStartName == null));
            if ((lpServiceStartName != null)) {
                encoder.WriteWideCharString(lpServiceStartName);
            }
            encoder.WriteUniqueReferentId((lpPassword == null));
            if ((lpPassword != null)) {
                encoder.WriteArrayHeader(lpPassword);
                for (int i = 0; (i < lpPassword.Length); i++
                ) {
                    byte elem_0 = lpPassword[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwPwSize);
            encoder.WriteUniqueReferentId((lpDisplayName == null));
            if ((lpDisplayName != null)) {
                encoder.WriteWideCharString(lpDisplayName);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpdwTagId = decoder.ReadOutPointer<uint>(lpdwTagId);
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RCreateServiceW(
                    Titanis.DceRpc.RpcContextHandle hSCManager, 
                    string lpServiceName, 
                    string lpDisplayName, 
                    uint dwDesiredAccess, 
                    uint dwServiceType, 
                    uint dwStartType, 
                    uint dwErrorControl, 
                    string lpBinaryPathName, 
                    string lpLoadOrderGroup, 
                    RpcPointer<uint> lpdwTagId, 
                    byte[] lpDependencies, 
                    uint dwDependSize, 
                    string lpServiceStartName, 
                    byte[] lpPassword, 
                    uint dwPwSize, 
                    RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, 
                    System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteWideCharString(lpServiceName);
            encoder.WriteUniqueReferentId((lpDisplayName == null));
            if ((lpDisplayName != null)) {
                encoder.WriteWideCharString(lpDisplayName);
            }
            encoder.WriteValue(dwDesiredAccess);
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwStartType);
            encoder.WriteValue(dwErrorControl);
            encoder.WriteWideCharString(lpBinaryPathName);
            encoder.WriteUniqueReferentId((lpLoadOrderGroup == null));
            if ((lpLoadOrderGroup != null)) {
                encoder.WriteWideCharString(lpLoadOrderGroup);
            }
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteUniqueReferentId((lpDependencies == null));
            if ((lpDependencies != null)) {
                encoder.WriteArrayHeader(lpDependencies);
                for (int i = 0; (i < lpDependencies.Length); i++
                ) {
                    byte elem_0 = lpDependencies[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwDependSize);
            encoder.WriteUniqueReferentId((lpServiceStartName == null));
            if ((lpServiceStartName != null)) {
                encoder.WriteWideCharString(lpServiceStartName);
            }
            encoder.WriteUniqueReferentId((lpPassword == null));
            if ((lpPassword != null)) {
                encoder.WriteArrayHeader(lpPassword);
                for (int i = 0; (i < lpPassword.Length); i++
                ) {
                    byte elem_0 = lpPassword[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwPwSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpdwTagId = decoder.ReadOutPointer<uint>(lpdwTagId);
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpServiceHandle.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> REnumDependentServicesW(Titanis.DceRpc.RpcContextHandle hService, uint dwServiceState, RpcPointer<byte[]> lpServices, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwServiceState);
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpServices.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpServices.value.Length); i++
            ) {
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
        public virtual async Task<uint> REnumServicesStatusW(Titanis.DceRpc.RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwServiceState);
            encoder.WriteValue(cbBufSize);
            encoder.WritePointer(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                encoder.WriteValue(lpResumeIndex.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpBuffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpBuffer.value[i] = elem_0;
            }
            pcbBytesNeeded.value = decoder.ReadUInt32();
            lpServicesReturned.value = decoder.ReadUInt32();
            lpResumeIndex = decoder.ReadOutPointer<uint>(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                lpResumeIndex.value = decoder.ReadUInt32();
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> ROpenSCManagerW(string lpMachineName, string lpDatabaseName, uint dwDesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> lpScHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((lpMachineName == null));
            if ((lpMachineName != null)) {
                encoder.WriteWideCharString(lpMachineName);
            }
            encoder.WriteUniqueReferentId((lpDatabaseName == null));
            if ((lpDatabaseName != null)) {
                encoder.WriteWideCharString(lpDatabaseName);
            }
            encoder.WriteValue(dwDesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpScHandle.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> ROpenServiceW(Titanis.DceRpc.RpcContextHandle hSCManager, string lpServiceName, uint dwDesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteWideCharString(lpServiceName);
            encoder.WriteValue(dwDesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpServiceHandle.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RQueryServiceConfigW(Titanis.DceRpc.RpcContextHandle hService, RpcPointer<QUERY_SERVICE_CONFIGW> lpServiceConfig, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpServiceConfig.value = decoder.ReadFixedStruct<QUERY_SERVICE_CONFIGW>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<QUERY_SERVICE_CONFIGW>(ref lpServiceConfig.value);
            pcbBytesNeeded.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RQueryServiceLockStatusW(Titanis.DceRpc.RpcContextHandle hSCManager, RpcPointer<QUERY_SERVICE_LOCK_STATUSW> lpLockStatus, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpLockStatus.value = decoder.ReadFixedStruct<QUERY_SERVICE_LOCK_STATUSW>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<QUERY_SERVICE_LOCK_STATUSW>(ref lpLockStatus.value);
            pcbBytesNeeded.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RStartServiceW(Titanis.DceRpc.RpcContextHandle hService, uint argc, STRING_PTRSW[] argv, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(argc);
            encoder.WriteUniqueReferentId((argv == null));
            if ((argv != null)) {
                encoder.WriteArrayHeader(argv);
                for (int i = 0; (i < argv.Length); i++
                ) {
                    STRING_PTRSW elem_0 = argv[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
            }
            for (int i = 0; (i < argv.Length); i++
            ) {
                STRING_PTRSW elem_0 = argv[i];
                encoder.WriteStructDeferral(elem_0);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RGetServiceDisplayNameW(Titanis.DceRpc.RpcContextHandle hSCManager, string lpServiceName, RpcPointer<string> lpDisplayName, RpcPointer<uint> lpcchBuffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteWideCharString(lpServiceName);
            encoder.WriteValue(lpcchBuffer.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpDisplayName.value = decoder.ReadWideCharString();
            lpcchBuffer.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RGetServiceKeyNameW(Titanis.DceRpc.RpcContextHandle hSCManager, string lpDisplayName, RpcPointer<string> lpServiceName, RpcPointer<uint> lpcchBuffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteWideCharString(lpDisplayName);
            encoder.WriteValue(lpcchBuffer.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpServiceName.value = decoder.ReadWideCharString();
            lpcchBuffer.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum22NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<uint> RChangeServiceConfigA(Titanis.DceRpc.RpcContextHandle hService, uint dwServiceType, uint dwStartType, uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, RpcPointer<uint> lpdwTagId, byte[] lpDependencies, uint dwDependSize, string lpServiceStartName, byte[] lpPassword, uint dwPwSize, string lpDisplayName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwStartType);
            encoder.WriteValue(dwErrorControl);
            encoder.WriteUniqueReferentId((lpBinaryPathName == null));
            if ((lpBinaryPathName != null)) {
                encoder.WriteUnsignedCharString(lpBinaryPathName);
            }
            encoder.WriteUniqueReferentId((lpLoadOrderGroup == null));
            if ((lpLoadOrderGroup != null)) {
                encoder.WriteUnsignedCharString(lpLoadOrderGroup);
            }
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteUniqueReferentId((lpDependencies == null));
            if ((lpDependencies != null)) {
                encoder.WriteArrayHeader(lpDependencies);
                for (int i = 0; (i < lpDependencies.Length); i++
                ) {
                    byte elem_0 = lpDependencies[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwDependSize);
            encoder.WriteUniqueReferentId((lpServiceStartName == null));
            if ((lpServiceStartName != null)) {
                encoder.WriteUnsignedCharString(lpServiceStartName);
            }
            encoder.WriteUniqueReferentId((lpPassword == null));
            if ((lpPassword != null)) {
                encoder.WriteArrayHeader(lpPassword);
                for (int i = 0; (i < lpPassword.Length); i++
                ) {
                    byte elem_0 = lpPassword[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwPwSize);
            encoder.WriteUniqueReferentId((lpDisplayName == null));
            if ((lpDisplayName != null)) {
                encoder.WriteUnsignedCharString(lpDisplayName);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpdwTagId = decoder.ReadOutPointer<uint>(lpdwTagId);
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RCreateServiceA(
                    Titanis.DceRpc.RpcContextHandle hSCManager, 
                    string lpServiceName, 
                    string lpDisplayName, 
                    uint dwDesiredAccess, 
                    uint dwServiceType, 
                    uint dwStartType, 
                    uint dwErrorControl, 
                    string lpBinaryPathName, 
                    string lpLoadOrderGroup, 
                    RpcPointer<uint> lpdwTagId, 
                    byte[] lpDependencies, 
                    uint dwDependSize, 
                    string lpServiceStartName, 
                    byte[] lpPassword, 
                    uint dwPwSize, 
                    RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, 
                    System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteUnsignedCharString(lpServiceName);
            encoder.WriteUniqueReferentId((lpDisplayName == null));
            if ((lpDisplayName != null)) {
                encoder.WriteUnsignedCharString(lpDisplayName);
            }
            encoder.WriteValue(dwDesiredAccess);
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwStartType);
            encoder.WriteValue(dwErrorControl);
            encoder.WriteUnsignedCharString(lpBinaryPathName);
            encoder.WriteUniqueReferentId((lpLoadOrderGroup == null));
            if ((lpLoadOrderGroup != null)) {
                encoder.WriteUnsignedCharString(lpLoadOrderGroup);
            }
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteUniqueReferentId((lpDependencies == null));
            if ((lpDependencies != null)) {
                encoder.WriteArrayHeader(lpDependencies);
                for (int i = 0; (i < lpDependencies.Length); i++
                ) {
                    byte elem_0 = lpDependencies[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwDependSize);
            encoder.WriteUniqueReferentId((lpServiceStartName == null));
            if ((lpServiceStartName != null)) {
                encoder.WriteUnsignedCharString(lpServiceStartName);
            }
            encoder.WriteUniqueReferentId((lpPassword == null));
            if ((lpPassword != null)) {
                encoder.WriteArrayHeader(lpPassword);
                for (int i = 0; (i < lpPassword.Length); i++
                ) {
                    byte elem_0 = lpPassword[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwPwSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpdwTagId = decoder.ReadOutPointer<uint>(lpdwTagId);
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpServiceHandle.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> REnumDependentServicesA(Titanis.DceRpc.RpcContextHandle hService, uint dwServiceState, RpcPointer<byte[]> lpServices, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwServiceState);
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpServices.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpServices.value.Length); i++
            ) {
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
        public virtual async Task<uint> REnumServicesStatusA(Titanis.DceRpc.RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwServiceState);
            encoder.WriteValue(cbBufSize);
            encoder.WritePointer(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                encoder.WriteValue(lpResumeIndex.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpBuffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpBuffer.value[i] = elem_0;
            }
            pcbBytesNeeded.value = decoder.ReadUInt32();
            lpServicesReturned.value = decoder.ReadUInt32();
            lpResumeIndex = decoder.ReadOutPointer<uint>(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                lpResumeIndex.value = decoder.ReadUInt32();
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> ROpenSCManagerA(string lpMachineName, string lpDatabaseName, uint dwDesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> lpScHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((lpMachineName == null));
            if ((lpMachineName != null)) {
                encoder.WriteUnsignedCharString(lpMachineName);
            }
            encoder.WriteUniqueReferentId((lpDatabaseName == null));
            if ((lpDatabaseName != null)) {
                encoder.WriteUnsignedCharString(lpDatabaseName);
            }
            encoder.WriteValue(dwDesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpScHandle.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> ROpenServiceA(Titanis.DceRpc.RpcContextHandle hSCManager, string lpServiceName, uint dwDesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteUnsignedCharString(lpServiceName);
            encoder.WriteValue(dwDesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpServiceHandle.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RQueryServiceConfigA(Titanis.DceRpc.RpcContextHandle hService, RpcPointer<QUERY_SERVICE_CONFIGA> lpServiceConfig, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpServiceConfig.value = decoder.ReadFixedStruct<QUERY_SERVICE_CONFIGA>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<QUERY_SERVICE_CONFIGA>(ref lpServiceConfig.value);
            pcbBytesNeeded.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RQueryServiceLockStatusA(Titanis.DceRpc.RpcContextHandle hSCManager, RpcPointer<QUERY_SERVICE_LOCK_STATUSA> lpLockStatus, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpLockStatus.value = decoder.ReadFixedStruct<QUERY_SERVICE_LOCK_STATUSA>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<QUERY_SERVICE_LOCK_STATUSA>(ref lpLockStatus.value);
            pcbBytesNeeded.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RStartServiceA(Titanis.DceRpc.RpcContextHandle hService, uint argc, STRING_PTRSA[] argv, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(argc);
            encoder.WriteUniqueReferentId((argv == null));
            if ((argv != null)) {
                encoder.WriteArrayHeader(argv);
                for (int i = 0; (i < argv.Length); i++
                ) {
                    STRING_PTRSA elem_0 = argv[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
            }
            for (int i = 0; (i < argv.Length); i++
            ) {
                STRING_PTRSA elem_0 = argv[i];
                encoder.WriteStructDeferral(elem_0);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RGetServiceDisplayNameA(Titanis.DceRpc.RpcContextHandle hSCManager, string lpServiceName, RpcPointer<string> lpDisplayName, RpcPointer<uint> lpcchBuffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteUnsignedCharString(lpServiceName);
            encoder.WriteValue(lpcchBuffer.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpDisplayName.value = decoder.ReadUnsignedCharString();
            lpcchBuffer.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RGetServiceKeyNameA(Titanis.DceRpc.RpcContextHandle hSCManager, string lpDisplayName, RpcPointer<string> lpKeyName, RpcPointer<uint> lpcchBuffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteUnsignedCharString(lpDisplayName);
            encoder.WriteValue(lpcchBuffer.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpKeyName.value = decoder.ReadUnsignedCharString();
            lpcchBuffer.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum34NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<uint> REnumServiceGroupW(Titanis.DceRpc.RpcContextHandle hSCManager, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwServiceState);
            encoder.WriteValue(cbBufSize);
            encoder.WritePointer(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                encoder.WriteValue(lpResumeIndex.value);
            }
            encoder.WriteUniqueReferentId((pszGroupName == null));
            if ((pszGroupName != null)) {
                encoder.WriteWideCharString(pszGroupName);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpBuffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpBuffer.value[i] = elem_0;
            }
            pcbBytesNeeded.value = decoder.ReadUInt32();
            lpServicesReturned.value = decoder.ReadUInt32();
            lpResumeIndex = decoder.ReadOutPointer<uint>(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                lpResumeIndex.value = decoder.ReadUInt32();
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RChangeServiceConfig2A(Titanis.DceRpc.RpcContextHandle hService, SC_RPC_CONFIG_INFOA Info, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteFixedStruct(Info, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Info);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RChangeServiceConfig2W(Titanis.DceRpc.RpcContextHandle hService, SC_RPC_CONFIG_INFOW Info, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(37);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteFixedStruct(Info, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Info);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RQueryServiceConfig2A(Titanis.DceRpc.RpcContextHandle hService, uint dwInfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(38);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwInfoLevel);
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpBuffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpBuffer.value[i] = elem_0;
            }
            pcbBytesNeeded.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RQueryServiceConfig2W(Titanis.DceRpc.RpcContextHandle hService, uint dwInfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(39);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwInfoLevel);
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpBuffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpBuffer.value[i] = elem_0;
            }
            pcbBytesNeeded.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RQueryServiceStatusEx(Titanis.DceRpc.RpcContextHandle hService, SC_STATUS_TYPE InfoLevel, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(40);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(((int)(InfoLevel)));
            encoder.WriteValue(cbBufSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpBuffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpBuffer.value[i] = elem_0;
            }
            pcbBytesNeeded.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> REnumServicesStatusExA(Titanis.DceRpc.RpcContextHandle hSCManager, SC_ENUM_TYPE InfoLevel, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(41);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteValue(((int)(InfoLevel)));
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwServiceState);
            encoder.WriteValue(cbBufSize);
            encoder.WritePointer(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                encoder.WriteValue(lpResumeIndex.value);
            }
            encoder.WriteUniqueReferentId((pszGroupName == null));
            if ((pszGroupName != null)) {
                encoder.WriteUnsignedCharString(pszGroupName);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpBuffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpBuffer.value[i] = elem_0;
            }
            pcbBytesNeeded.value = decoder.ReadUInt32();
            lpServicesReturned.value = decoder.ReadUInt32();
            lpResumeIndex = decoder.ReadOutPointer<uint>(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                lpResumeIndex.value = decoder.ReadUInt32();
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> REnumServicesStatusExW(Titanis.DceRpc.RpcContextHandle hSCManager, SC_ENUM_TYPE InfoLevel, uint dwServiceType, uint dwServiceState, RpcPointer<byte[]> lpBuffer, uint cbBufSize, RpcPointer<uint> pcbBytesNeeded, RpcPointer<uint> lpServicesReturned, RpcPointer<uint> lpResumeIndex, string pszGroupName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(42);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteValue(((int)(InfoLevel)));
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwServiceState);
            encoder.WriteValue(cbBufSize);
            encoder.WritePointer(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                encoder.WriteValue(lpResumeIndex.value);
            }
            encoder.WriteUniqueReferentId((pszGroupName == null));
            if ((pszGroupName != null)) {
                encoder.WriteWideCharString(pszGroupName);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpBuffer.value = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpBuffer.value[i] = elem_0;
            }
            pcbBytesNeeded.value = decoder.ReadUInt32();
            lpServicesReturned.value = decoder.ReadUInt32();
            lpResumeIndex = decoder.ReadOutPointer<uint>(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                lpResumeIndex.value = decoder.ReadUInt32();
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum43NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(43);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<uint> RCreateServiceWOW64A(
                    Titanis.DceRpc.RpcContextHandle hSCManager, 
                    string lpServiceName, 
                    string lpDisplayName, 
                    uint dwDesiredAccess, 
                    uint dwServiceType, 
                    uint dwStartType, 
                    uint dwErrorControl, 
                    string lpBinaryPathName, 
                    string lpLoadOrderGroup, 
                    RpcPointer<uint> lpdwTagId, 
                    byte[] lpDependencies, 
                    uint dwDependSize, 
                    string lpServiceStartName, 
                    byte[] lpPassword, 
                    uint dwPwSize, 
                    RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, 
                    System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(44);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteUnsignedCharString(lpServiceName);
            encoder.WriteUniqueReferentId((lpDisplayName == null));
            if ((lpDisplayName != null)) {
                encoder.WriteUnsignedCharString(lpDisplayName);
            }
            encoder.WriteValue(dwDesiredAccess);
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwStartType);
            encoder.WriteValue(dwErrorControl);
            encoder.WriteUnsignedCharString(lpBinaryPathName);
            encoder.WriteUniqueReferentId((lpLoadOrderGroup == null));
            if ((lpLoadOrderGroup != null)) {
                encoder.WriteUnsignedCharString(lpLoadOrderGroup);
            }
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteUniqueReferentId((lpDependencies == null));
            if ((lpDependencies != null)) {
                encoder.WriteArrayHeader(lpDependencies);
                for (int i = 0; (i < lpDependencies.Length); i++
                ) {
                    byte elem_0 = lpDependencies[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwDependSize);
            encoder.WriteUniqueReferentId((lpServiceStartName == null));
            if ((lpServiceStartName != null)) {
                encoder.WriteUnsignedCharString(lpServiceStartName);
            }
            encoder.WriteUniqueReferentId((lpPassword == null));
            if ((lpPassword != null)) {
                encoder.WriteArrayHeader(lpPassword);
                for (int i = 0; (i < lpPassword.Length); i++
                ) {
                    byte elem_0 = lpPassword[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwPwSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpdwTagId = decoder.ReadOutPointer<uint>(lpdwTagId);
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpServiceHandle.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RCreateServiceWOW64W(
                    Titanis.DceRpc.RpcContextHandle hSCManager, 
                    string lpServiceName, 
                    string lpDisplayName, 
                    uint dwDesiredAccess, 
                    uint dwServiceType, 
                    uint dwStartType, 
                    uint dwErrorControl, 
                    string lpBinaryPathName, 
                    string lpLoadOrderGroup, 
                    RpcPointer<uint> lpdwTagId, 
                    byte[] lpDependencies, 
                    uint dwDependSize, 
                    string lpServiceStartName, 
                    byte[] lpPassword, 
                    uint dwPwSize, 
                    RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, 
                    System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(45);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteWideCharString(lpServiceName);
            encoder.WriteUniqueReferentId((lpDisplayName == null));
            if ((lpDisplayName != null)) {
                encoder.WriteWideCharString(lpDisplayName);
            }
            encoder.WriteValue(dwDesiredAccess);
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwStartType);
            encoder.WriteValue(dwErrorControl);
            encoder.WriteWideCharString(lpBinaryPathName);
            encoder.WriteUniqueReferentId((lpLoadOrderGroup == null));
            if ((lpLoadOrderGroup != null)) {
                encoder.WriteWideCharString(lpLoadOrderGroup);
            }
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteUniqueReferentId((lpDependencies == null));
            if ((lpDependencies != null)) {
                encoder.WriteArrayHeader(lpDependencies);
                for (int i = 0; (i < lpDependencies.Length); i++
                ) {
                    byte elem_0 = lpDependencies[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwDependSize);
            encoder.WriteUniqueReferentId((lpServiceStartName == null));
            if ((lpServiceStartName != null)) {
                encoder.WriteWideCharString(lpServiceStartName);
            }
            encoder.WriteUniqueReferentId((lpPassword == null));
            if ((lpPassword != null)) {
                encoder.WriteArrayHeader(lpPassword);
                for (int i = 0; (i < lpPassword.Length); i++
                ) {
                    byte elem_0 = lpPassword[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwPwSize);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpdwTagId = decoder.ReadOutPointer<uint>(lpdwTagId);
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpServiceHandle.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum46NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(46);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<uint> RNotifyServiceStatusChange(Titanis.DceRpc.RpcContextHandle hService, SC_RPC_NOTIFY_PARAMS NotifyParams, System.Guid pClientProcessGuid, RpcPointer<System.Guid> pSCMProcessGuid, RpcPointer<int> pfCreateRemoteQueue, RpcPointer<Titanis.DceRpc.RpcContextHandle> phNotify, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(47);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteFixedStruct(NotifyParams, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(NotifyParams);
            encoder.WriteValue(pClientProcessGuid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pSCMProcessGuid.value = decoder.ReadUuid();
            pfCreateRemoteQueue.value = decoder.ReadInt32();
            phNotify.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<int> RGetNotifyResults(Titanis.DceRpc.RpcContextHandle hNotify, RpcPointer<RpcPointer<SC_RPC_NOTIFY_PARAMS_LIST>> ppNotifyParams, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(48);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hNotify);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppNotifyParams.value = decoder.ReadOutPointer<SC_RPC_NOTIFY_PARAMS_LIST>(ppNotifyParams.value);
            if ((null != ppNotifyParams.value)) {
                ppNotifyParams.value.value = decoder.ReadConformantStruct<SC_RPC_NOTIFY_PARAMS_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<SC_RPC_NOTIFY_PARAMS_LIST>(ref ppNotifyParams.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<uint> RCloseNotifyHandle(RpcPointer<Titanis.DceRpc.RpcContextHandle> phNotify, RpcPointer<int> pfApcFired, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(49);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(phNotify.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            phNotify.value = decoder.ReadContextHandle();
            pfApcFired.value = decoder.ReadInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RControlServiceExA(Titanis.DceRpc.RpcContextHandle hService, uint dwControl, uint dwInfoLevel, SC_RPC_SERVICE_CONTROL_IN_PARAMSA pControlInParams, RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSA> pControlOutParams, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(50);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwControl);
            encoder.WriteValue(dwInfoLevel);
            encoder.WriteUnion(pControlInParams);
            encoder.WriteStructDeferral(pControlInParams);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pControlOutParams.value = decoder.ReadUnion<SC_RPC_SERVICE_CONTROL_OUT_PARAMSA>();
            decoder.ReadStructDeferral<SC_RPC_SERVICE_CONTROL_OUT_PARAMSA>(ref pControlOutParams.value);
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> RControlServiceExW(Titanis.DceRpc.RpcContextHandle hService, uint dwControl, uint dwInfoLevel, SC_RPC_SERVICE_CONTROL_IN_PARAMSW pControlInParams, RpcPointer<SC_RPC_SERVICE_CONTROL_OUT_PARAMSW> pControlOutParams, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(51);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwControl);
            encoder.WriteValue(dwInfoLevel);
            encoder.WriteUnion(pControlInParams);
            encoder.WriteStructDeferral(pControlInParams);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pControlOutParams.value = decoder.ReadUnion<SC_RPC_SERVICE_CONTROL_OUT_PARAMSW>();
            decoder.ReadStructDeferral<SC_RPC_SERVICE_CONTROL_OUT_PARAMSW>(ref pControlOutParams.value);
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum52NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(52);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum53NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(53);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum54NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(54);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum55NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(55);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<uint> RQueryServiceConfigEx(Titanis.DceRpc.RpcContextHandle hService, uint dwInfoLevel, RpcPointer<SC_RPC_CONFIG_INFOW> pInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(56);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hService);
            encoder.WriteValue(dwInfoLevel);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pInfo.value = decoder.ReadFixedStruct<SC_RPC_CONFIG_INFOW>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SC_RPC_CONFIG_INFOW>(ref pInfo.value);
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum57NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(57);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum58NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(58);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum59NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(59);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<uint> RCreateWowService(
                    Titanis.DceRpc.RpcContextHandle hSCManager, 
                    string lpServiceName, 
                    string lpDisplayName, 
                    uint dwDesiredAccess, 
                    uint dwServiceType, 
                    uint dwStartType, 
                    uint dwErrorControl, 
                    string lpBinaryPathName, 
                    string lpLoadOrderGroup, 
                    RpcPointer<uint> lpdwTagId, 
                    byte[] lpDependencies, 
                    uint dwDependSize, 
                    string lpServiceStartName, 
                    byte[] lpPassword, 
                    uint dwPwSize, 
                    ushort dwServiceWowType, 
                    RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle, 
                    System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(60);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hSCManager);
            encoder.WriteWideCharString(lpServiceName);
            encoder.WriteUniqueReferentId((lpDisplayName == null));
            if ((lpDisplayName != null)) {
                encoder.WriteWideCharString(lpDisplayName);
            }
            encoder.WriteValue(dwDesiredAccess);
            encoder.WriteValue(dwServiceType);
            encoder.WriteValue(dwStartType);
            encoder.WriteValue(dwErrorControl);
            encoder.WriteWideCharString(lpBinaryPathName);
            encoder.WriteUniqueReferentId((lpLoadOrderGroup == null));
            if ((lpLoadOrderGroup != null)) {
                encoder.WriteWideCharString(lpLoadOrderGroup);
            }
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteUniqueReferentId((lpDependencies == null));
            if ((lpDependencies != null)) {
                encoder.WriteArrayHeader(lpDependencies);
                for (int i = 0; (i < lpDependencies.Length); i++
                ) {
                    byte elem_0 = lpDependencies[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwDependSize);
            encoder.WriteUniqueReferentId((lpServiceStartName == null));
            if ((lpServiceStartName != null)) {
                encoder.WriteWideCharString(lpServiceStartName);
            }
            encoder.WriteUniqueReferentId((lpPassword == null));
            if ((lpPassword != null)) {
                encoder.WriteArrayHeader(lpPassword);
                for (int i = 0; (i < lpPassword.Length); i++
                ) {
                    byte elem_0 = lpPassword[i];
                    encoder.WriteValue(elem_0);
                }
            }
            encoder.WriteValue(dwPwSize);
            encoder.WriteValue(dwServiceWowType);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            lpdwTagId = decoder.ReadOutPointer<uint>(lpdwTagId);
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpServiceHandle.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> ROpenSCManager2(string DatabaseName, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> ScmHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(64);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((DatabaseName == null));
            if ((DatabaseName != null)) {
                encoder.WriteWideCharString(DatabaseName);
            }
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ScmHandle.value = decoder.ReadContextHandle();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class svcctlStub : Titanis.DceRpc.Server.RpcServiceStub {
        private static System.Guid _interfaceUuid = new System.Guid("367abb81-9844-35f1-ad32-98f038001003");
        public override System.Guid InterfaceUuid {
            get {
                return _interfaceUuid;
            }
        }
        public override Titanis.DceRpc.RpcVersion InterfaceVersion {
            get {
                return new Titanis.DceRpc.RpcVersion(2, 0);
            }
        }
        private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
        public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable {
            get {
                return this._dispatchTable;
            }
        }
        private svcctl _obj;
        public svcctlStub(svcctl obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_RCloseServiceHandle,
                    this.Invoke_RControlService,
                    this.Invoke_RDeleteService,
                    this.Invoke_RLockServiceDatabase,
                    this.Invoke_RQueryServiceObjectSecurity,
                    this.Invoke_RSetServiceObjectSecurity,
                    this.Invoke_RQueryServiceStatus,
                    this.Invoke_RSetServiceStatus,
                    this.Invoke_RUnlockServiceDatabase,
                    this.Invoke_RNotifyBootConfigStatus,
                    this.Invoke_Opnum10NotUsedOnWire,
                    this.Invoke_RChangeServiceConfigW,
                    this.Invoke_RCreateServiceW,
                    this.Invoke_REnumDependentServicesW,
                    this.Invoke_REnumServicesStatusW,
                    this.Invoke_ROpenSCManagerW,
                    this.Invoke_ROpenServiceW,
                    this.Invoke_RQueryServiceConfigW,
                    this.Invoke_RQueryServiceLockStatusW,
                    this.Invoke_RStartServiceW,
                    this.Invoke_RGetServiceDisplayNameW,
                    this.Invoke_RGetServiceKeyNameW,
                    this.Invoke_Opnum22NotUsedOnWire,
                    this.Invoke_RChangeServiceConfigA,
                    this.Invoke_RCreateServiceA,
                    this.Invoke_REnumDependentServicesA,
                    this.Invoke_REnumServicesStatusA,
                    this.Invoke_ROpenSCManagerA,
                    this.Invoke_ROpenServiceA,
                    this.Invoke_RQueryServiceConfigA,
                    this.Invoke_RQueryServiceLockStatusA,
                    this.Invoke_RStartServiceA,
                    this.Invoke_RGetServiceDisplayNameA,
                    this.Invoke_RGetServiceKeyNameA,
                    this.Invoke_Opnum34NotUsedOnWire,
                    this.Invoke_REnumServiceGroupW,
                    this.Invoke_RChangeServiceConfig2A,
                    this.Invoke_RChangeServiceConfig2W,
                    this.Invoke_RQueryServiceConfig2A,
                    this.Invoke_RQueryServiceConfig2W,
                    this.Invoke_RQueryServiceStatusEx,
                    this.Invoke_REnumServicesStatusExA,
                    this.Invoke_REnumServicesStatusExW,
                    this.Invoke_Opnum43NotUsedOnWire,
                    this.Invoke_RCreateServiceWOW64A,
                    this.Invoke_RCreateServiceWOW64W,
                    this.Invoke_Opnum46NotUsedOnWire,
                    this.Invoke_RNotifyServiceStatusChange,
                    this.Invoke_RGetNotifyResults,
                    this.Invoke_RCloseNotifyHandle,
                    this.Invoke_RControlServiceExA,
                    this.Invoke_RControlServiceExW,
                    this.Invoke_Opnum52NotUsedOnWire,
                    this.Invoke_Opnum53NotUsedOnWire,
                    this.Invoke_Opnum54NotUsedOnWire,
                    this.Invoke_Opnum55NotUsedOnWire,
                    this.Invoke_RQueryServiceConfigEx,
                    this.Invoke_Opnum57NotUsedOnWire,
                    this.Invoke_Opnum58NotUsedOnWire,
                    this.Invoke_Opnum59NotUsedOnWire,
                    this.Invoke_RCreateWowService,
                    this.Invoke_ROpenSCManager2};
        }
        private async Task Invoke_RCloseServiceHandle(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> hSCObject;
            hSCObject = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hSCObject.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.RCloseServiceHandle(hSCObject, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(hSCObject.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RControlService(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            uint dwControl;
            RpcPointer<SERVICE_STATUS> lpServiceStatus = new RpcPointer<SERVICE_STATUS>();
            hService = decoder.ReadContextHandle();
            dwControl = decoder.ReadUInt32();
            var invokeTask = this._obj.RControlService(hService, dwControl, lpServiceStatus, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(lpServiceStatus.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(lpServiceStatus.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RDeleteService(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            hService = decoder.ReadContextHandle();
            var invokeTask = this._obj.RDeleteService(hService, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RLockServiceDatabase(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> lpLock = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hSCManager = decoder.ReadContextHandle();
            var invokeTask = this._obj.RLockServiceDatabase(hSCManager, lpLock, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(lpLock.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RQueryServiceObjectSecurity(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
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
            for (int i = 0; (i < lpSecurityDescriptor.value.Length); i++
            ) {
                byte elem_0 = lpSecurityDescriptor.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RSetServiceObjectSecurity(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            uint dwSecurityInformation;
            byte[] lpSecurityDescriptor;
            uint cbBufSize;
            hService = decoder.ReadContextHandle();
            dwSecurityInformation = decoder.ReadUInt32();
            lpSecurityDescriptor = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpSecurityDescriptor.Length); i++
            ) {
                byte elem_0 = lpSecurityDescriptor[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpSecurityDescriptor[i] = elem_0;
            }
            cbBufSize = decoder.ReadUInt32();
            var invokeTask = this._obj.RSetServiceObjectSecurity(hService, dwSecurityInformation, lpSecurityDescriptor, cbBufSize, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RQueryServiceStatus(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            RpcPointer<SERVICE_STATUS> lpServiceStatus = new RpcPointer<SERVICE_STATUS>();
            hService = decoder.ReadContextHandle();
            var invokeTask = this._obj.RQueryServiceStatus(hService, lpServiceStatus, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(lpServiceStatus.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(lpServiceStatus.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RSetServiceStatus(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hServiceStatus;
            SERVICE_STATUS lpServiceStatus;
            hServiceStatus = decoder.ReadContextHandle();
            lpServiceStatus = decoder.ReadFixedStruct<SERVICE_STATUS>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<SERVICE_STATUS>(ref lpServiceStatus);
            var invokeTask = this._obj.RSetServiceStatus(hServiceStatus, lpServiceStatus, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RUnlockServiceDatabase(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> Lock;
            Lock = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            Lock.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.RUnlockServiceDatabase(Lock, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(Lock.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RNotifyBootConfigStatus(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string lpMachineName;
            uint BootAcceptable;
            if ((decoder.ReadReferentId() == 0)) {
                lpMachineName = null;
            }
            else {
                lpMachineName = decoder.ReadWideCharString();
            }
            BootAcceptable = decoder.ReadUInt32();
            var invokeTask = this._obj.RNotifyBootConfigStatus(lpMachineName, BootAcceptable, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum10NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum10NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_RChangeServiceConfigW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
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
            if ((decoder.ReadReferentId() == 0)) {
                lpBinaryPathName = null;
            }
            else {
                lpBinaryPathName = decoder.ReadWideCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                lpLoadOrderGroup = null;
            }
            else {
                lpLoadOrderGroup = decoder.ReadWideCharString();
            }
            lpdwTagId = decoder.ReadPointer<uint>();
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpDependencies = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpDependencies.Length); i++
            ) {
                byte elem_0 = lpDependencies[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpDependencies[i] = elem_0;
            }
            dwDependSize = decoder.ReadUInt32();
            if ((decoder.ReadReferentId() == 0)) {
                lpServiceStartName = null;
            }
            else {
                lpServiceStartName = decoder.ReadWideCharString();
            }
            lpPassword = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpPassword.Length); i++
            ) {
                byte elem_0 = lpPassword[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpPassword[i] = elem_0;
            }
            dwPwSize = decoder.ReadUInt32();
            if ((decoder.ReadReferentId() == 0)) {
                lpDisplayName = null;
            }
            else {
                lpDisplayName = decoder.ReadWideCharString();
            }
            var invokeTask = this._obj.RChangeServiceConfigW(hService, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpDisplayName, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RCreateServiceW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
            RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hSCManager = decoder.ReadContextHandle();
            lpServiceName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                lpDisplayName = null;
            }
            else {
                lpDisplayName = decoder.ReadWideCharString();
            }
            dwDesiredAccess = decoder.ReadUInt32();
            dwServiceType = decoder.ReadUInt32();
            dwStartType = decoder.ReadUInt32();
            dwErrorControl = decoder.ReadUInt32();
            lpBinaryPathName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                lpLoadOrderGroup = null;
            }
            else {
                lpLoadOrderGroup = decoder.ReadWideCharString();
            }
            lpdwTagId = decoder.ReadPointer<uint>();
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpDependencies = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpDependencies.Length); i++
            ) {
                byte elem_0 = lpDependencies[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpDependencies[i] = elem_0;
            }
            dwDependSize = decoder.ReadUInt32();
            if ((decoder.ReadReferentId() == 0)) {
                lpServiceStartName = null;
            }
            else {
                lpServiceStartName = decoder.ReadWideCharString();
            }
            lpPassword = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpPassword.Length); i++
            ) {
                byte elem_0 = lpPassword[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpPassword[i] = elem_0;
            }
            dwPwSize = decoder.ReadUInt32();
            var invokeTask = this._obj.RCreateServiceW(hSCManager, lpServiceName, lpDisplayName, dwDesiredAccess, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpServiceHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteContextHandle(lpServiceHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_REnumDependentServicesW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
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
            for (int i = 0; (i < lpServices.value.Length); i++
            ) {
                byte elem_0 = lpServices.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(lpServicesReturned.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_REnumServicesStatusW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
            lpResumeIndex = decoder.ReadPointer<uint>();
            if ((null != lpResumeIndex)) {
                lpResumeIndex.value = decoder.ReadUInt32();
            }
            var invokeTask = this._obj.REnumServicesStatusW(hSCManager, dwServiceType, dwServiceState, lpBuffer, cbBufSize, pcbBytesNeeded, lpServicesReturned, lpResumeIndex, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(lpBuffer.value);
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(lpServicesReturned.value);
            encoder.WritePointer(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                encoder.WriteValue(lpResumeIndex.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ROpenSCManagerW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string lpMachineName;
            string lpDatabaseName;
            uint dwDesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> lpScHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            if ((decoder.ReadReferentId() == 0)) {
                lpMachineName = null;
            }
            else {
                lpMachineName = decoder.ReadWideCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                lpDatabaseName = null;
            }
            else {
                lpDatabaseName = decoder.ReadWideCharString();
            }
            dwDesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.ROpenSCManagerW(lpMachineName, lpDatabaseName, dwDesiredAccess, lpScHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(lpScHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ROpenServiceW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
            string lpServiceName;
            uint dwDesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hSCManager = decoder.ReadContextHandle();
            lpServiceName = decoder.ReadWideCharString();
            dwDesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.ROpenServiceW(hSCManager, lpServiceName, dwDesiredAccess, lpServiceHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(lpServiceHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RQueryServiceConfigW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            RpcPointer<QUERY_SERVICE_CONFIGW> lpServiceConfig = new RpcPointer<QUERY_SERVICE_CONFIGW>();
            uint cbBufSize;
            RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
            hService = decoder.ReadContextHandle();
            cbBufSize = decoder.ReadUInt32();
            var invokeTask = this._obj.RQueryServiceConfigW(hService, lpServiceConfig, cbBufSize, pcbBytesNeeded, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(lpServiceConfig.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(lpServiceConfig.value);
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RQueryServiceLockStatusW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
            RpcPointer<QUERY_SERVICE_LOCK_STATUSW> lpLockStatus = new RpcPointer<QUERY_SERVICE_LOCK_STATUSW>();
            uint cbBufSize;
            RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
            hSCManager = decoder.ReadContextHandle();
            cbBufSize = decoder.ReadUInt32();
            var invokeTask = this._obj.RQueryServiceLockStatusW(hSCManager, lpLockStatus, cbBufSize, pcbBytesNeeded, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(lpLockStatus.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(lpLockStatus.value);
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RStartServiceW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            uint argc;
            STRING_PTRSW[] argv;
            hService = decoder.ReadContextHandle();
            argc = decoder.ReadUInt32();
            argv = decoder.ReadArrayHeader<STRING_PTRSW>();
            for (int i = 0; (i < argv.Length); i++
            ) {
                STRING_PTRSW elem_0 = argv[i];
                elem_0 = decoder.ReadFixedStruct<STRING_PTRSW>(Titanis.DceRpc.NdrAlignment.NativePtr);
                argv[i] = elem_0;
            }
            for (int i = 0; (i < argv.Length); i++
            ) {
                STRING_PTRSW elem_0 = argv[i];
                decoder.ReadStructDeferral<STRING_PTRSW>(ref elem_0);
                argv[i] = elem_0;
            }
            var invokeTask = this._obj.RStartServiceW(hService, argc, argv, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RGetServiceDisplayNameW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
        private async Task Invoke_RGetServiceKeyNameW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
        private async Task Invoke_Opnum22NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum22NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_RChangeServiceConfigA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
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
            if ((decoder.ReadReferentId() == 0)) {
                lpBinaryPathName = null;
            }
            else {
                lpBinaryPathName = decoder.ReadUnsignedCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                lpLoadOrderGroup = null;
            }
            else {
                lpLoadOrderGroup = decoder.ReadUnsignedCharString();
            }
            lpdwTagId = decoder.ReadPointer<uint>();
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpDependencies = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpDependencies.Length); i++
            ) {
                byte elem_0 = lpDependencies[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpDependencies[i] = elem_0;
            }
            dwDependSize = decoder.ReadUInt32();
            if ((decoder.ReadReferentId() == 0)) {
                lpServiceStartName = null;
            }
            else {
                lpServiceStartName = decoder.ReadUnsignedCharString();
            }
            lpPassword = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpPassword.Length); i++
            ) {
                byte elem_0 = lpPassword[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpPassword[i] = elem_0;
            }
            dwPwSize = decoder.ReadUInt32();
            if ((decoder.ReadReferentId() == 0)) {
                lpDisplayName = null;
            }
            else {
                lpDisplayName = decoder.ReadUnsignedCharString();
            }
            var invokeTask = this._obj.RChangeServiceConfigA(hService, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpDisplayName, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RCreateServiceA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
            RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hSCManager = decoder.ReadContextHandle();
            lpServiceName = decoder.ReadUnsignedCharString();
            if ((decoder.ReadReferentId() == 0)) {
                lpDisplayName = null;
            }
            else {
                lpDisplayName = decoder.ReadUnsignedCharString();
            }
            dwDesiredAccess = decoder.ReadUInt32();
            dwServiceType = decoder.ReadUInt32();
            dwStartType = decoder.ReadUInt32();
            dwErrorControl = decoder.ReadUInt32();
            lpBinaryPathName = decoder.ReadUnsignedCharString();
            if ((decoder.ReadReferentId() == 0)) {
                lpLoadOrderGroup = null;
            }
            else {
                lpLoadOrderGroup = decoder.ReadUnsignedCharString();
            }
            lpdwTagId = decoder.ReadPointer<uint>();
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpDependencies = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpDependencies.Length); i++
            ) {
                byte elem_0 = lpDependencies[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpDependencies[i] = elem_0;
            }
            dwDependSize = decoder.ReadUInt32();
            if ((decoder.ReadReferentId() == 0)) {
                lpServiceStartName = null;
            }
            else {
                lpServiceStartName = decoder.ReadUnsignedCharString();
            }
            lpPassword = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpPassword.Length); i++
            ) {
                byte elem_0 = lpPassword[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpPassword[i] = elem_0;
            }
            dwPwSize = decoder.ReadUInt32();
            var invokeTask = this._obj.RCreateServiceA(hSCManager, lpServiceName, lpDisplayName, dwDesiredAccess, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpServiceHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteContextHandle(lpServiceHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_REnumDependentServicesA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
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
            for (int i = 0; (i < lpServices.value.Length); i++
            ) {
                byte elem_0 = lpServices.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(lpServicesReturned.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_REnumServicesStatusA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
            lpResumeIndex = decoder.ReadPointer<uint>();
            if ((null != lpResumeIndex)) {
                lpResumeIndex.value = decoder.ReadUInt32();
            }
            var invokeTask = this._obj.REnumServicesStatusA(hSCManager, dwServiceType, dwServiceState, lpBuffer, cbBufSize, pcbBytesNeeded, lpServicesReturned, lpResumeIndex, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(lpBuffer.value);
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(lpServicesReturned.value);
            encoder.WritePointer(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                encoder.WriteValue(lpResumeIndex.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ROpenSCManagerA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string lpMachineName;
            string lpDatabaseName;
            uint dwDesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> lpScHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            if ((decoder.ReadReferentId() == 0)) {
                lpMachineName = null;
            }
            else {
                lpMachineName = decoder.ReadUnsignedCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                lpDatabaseName = null;
            }
            else {
                lpDatabaseName = decoder.ReadUnsignedCharString();
            }
            dwDesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.ROpenSCManagerA(lpMachineName, lpDatabaseName, dwDesiredAccess, lpScHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(lpScHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ROpenServiceA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
            string lpServiceName;
            uint dwDesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hSCManager = decoder.ReadContextHandle();
            lpServiceName = decoder.ReadUnsignedCharString();
            dwDesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.ROpenServiceA(hSCManager, lpServiceName, dwDesiredAccess, lpServiceHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(lpServiceHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RQueryServiceConfigA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            RpcPointer<QUERY_SERVICE_CONFIGA> lpServiceConfig = new RpcPointer<QUERY_SERVICE_CONFIGA>();
            uint cbBufSize;
            RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
            hService = decoder.ReadContextHandle();
            cbBufSize = decoder.ReadUInt32();
            var invokeTask = this._obj.RQueryServiceConfigA(hService, lpServiceConfig, cbBufSize, pcbBytesNeeded, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(lpServiceConfig.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(lpServiceConfig.value);
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RQueryServiceLockStatusA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
            RpcPointer<QUERY_SERVICE_LOCK_STATUSA> lpLockStatus = new RpcPointer<QUERY_SERVICE_LOCK_STATUSA>();
            uint cbBufSize;
            RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
            hSCManager = decoder.ReadContextHandle();
            cbBufSize = decoder.ReadUInt32();
            var invokeTask = this._obj.RQueryServiceLockStatusA(hSCManager, lpLockStatus, cbBufSize, pcbBytesNeeded, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(lpLockStatus.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(lpLockStatus.value);
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RStartServiceA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            uint argc;
            STRING_PTRSA[] argv;
            hService = decoder.ReadContextHandle();
            argc = decoder.ReadUInt32();
            argv = decoder.ReadArrayHeader<STRING_PTRSA>();
            for (int i = 0; (i < argv.Length); i++
            ) {
                STRING_PTRSA elem_0 = argv[i];
                elem_0 = decoder.ReadFixedStruct<STRING_PTRSA>(Titanis.DceRpc.NdrAlignment.NativePtr);
                argv[i] = elem_0;
            }
            for (int i = 0; (i < argv.Length); i++
            ) {
                STRING_PTRSA elem_0 = argv[i];
                decoder.ReadStructDeferral<STRING_PTRSA>(ref elem_0);
                argv[i] = elem_0;
            }
            var invokeTask = this._obj.RStartServiceA(hService, argc, argv, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RGetServiceDisplayNameA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
        private async Task Invoke_RGetServiceKeyNameA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
        private async Task Invoke_Opnum34NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum34NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_REnumServiceGroupW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
            lpResumeIndex = decoder.ReadPointer<uint>();
            if ((null != lpResumeIndex)) {
                lpResumeIndex.value = decoder.ReadUInt32();
            }
            if ((decoder.ReadReferentId() == 0)) {
                pszGroupName = null;
            }
            else {
                pszGroupName = decoder.ReadWideCharString();
            }
            var invokeTask = this._obj.REnumServiceGroupW(hSCManager, dwServiceType, dwServiceState, lpBuffer, cbBufSize, pcbBytesNeeded, lpServicesReturned, lpResumeIndex, pszGroupName, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(lpBuffer.value);
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(lpServicesReturned.value);
            encoder.WritePointer(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                encoder.WriteValue(lpResumeIndex.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RChangeServiceConfig2A(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            SC_RPC_CONFIG_INFOA Info;
            hService = decoder.ReadContextHandle();
            Info = decoder.ReadFixedStruct<SC_RPC_CONFIG_INFOA>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SC_RPC_CONFIG_INFOA>(ref Info);
            var invokeTask = this._obj.RChangeServiceConfig2A(hService, Info, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RChangeServiceConfig2W(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            SC_RPC_CONFIG_INFOW Info;
            hService = decoder.ReadContextHandle();
            Info = decoder.ReadFixedStruct<SC_RPC_CONFIG_INFOW>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SC_RPC_CONFIG_INFOW>(ref Info);
            var invokeTask = this._obj.RChangeServiceConfig2W(hService, Info, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RQueryServiceConfig2A(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
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
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RQueryServiceConfig2W(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
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
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RQueryServiceStatusEx(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            SC_STATUS_TYPE InfoLevel;
            RpcPointer<byte[]> lpBuffer = new RpcPointer<byte[]>();
            uint cbBufSize;
            RpcPointer<uint> pcbBytesNeeded = new RpcPointer<uint>();
            hService = decoder.ReadContextHandle();
            InfoLevel = ((SC_STATUS_TYPE)(decoder.ReadInt32()));
            cbBufSize = decoder.ReadUInt32();
            var invokeTask = this._obj.RQueryServiceStatusEx(hService, InfoLevel, lpBuffer, cbBufSize, pcbBytesNeeded, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(lpBuffer.value);
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_REnumServicesStatusExA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
            InfoLevel = ((SC_ENUM_TYPE)(decoder.ReadInt32()));
            dwServiceType = decoder.ReadUInt32();
            dwServiceState = decoder.ReadUInt32();
            cbBufSize = decoder.ReadUInt32();
            lpResumeIndex = decoder.ReadPointer<uint>();
            if ((null != lpResumeIndex)) {
                lpResumeIndex.value = decoder.ReadUInt32();
            }
            if ((decoder.ReadReferentId() == 0)) {
                pszGroupName = null;
            }
            else {
                pszGroupName = decoder.ReadUnsignedCharString();
            }
            var invokeTask = this._obj.REnumServicesStatusExA(hSCManager, InfoLevel, dwServiceType, dwServiceState, lpBuffer, cbBufSize, pcbBytesNeeded, lpServicesReturned, lpResumeIndex, pszGroupName, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(lpBuffer.value);
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(lpServicesReturned.value);
            encoder.WritePointer(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                encoder.WriteValue(lpResumeIndex.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_REnumServicesStatusExW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
            InfoLevel = ((SC_ENUM_TYPE)(decoder.ReadInt32()));
            dwServiceType = decoder.ReadUInt32();
            dwServiceState = decoder.ReadUInt32();
            cbBufSize = decoder.ReadUInt32();
            lpResumeIndex = decoder.ReadPointer<uint>();
            if ((null != lpResumeIndex)) {
                lpResumeIndex.value = decoder.ReadUInt32();
            }
            if ((decoder.ReadReferentId() == 0)) {
                pszGroupName = null;
            }
            else {
                pszGroupName = decoder.ReadWideCharString();
            }
            var invokeTask = this._obj.REnumServicesStatusExW(hSCManager, InfoLevel, dwServiceType, dwServiceState, lpBuffer, cbBufSize, pcbBytesNeeded, lpServicesReturned, lpResumeIndex, pszGroupName, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteArrayHeader(lpBuffer.value);
            for (int i = 0; (i < lpBuffer.value.Length); i++
            ) {
                byte elem_0 = lpBuffer.value[i];
                encoder.WriteValue(elem_0);
            }
            encoder.WriteValue(pcbBytesNeeded.value);
            encoder.WriteValue(lpServicesReturned.value);
            encoder.WritePointer(lpResumeIndex);
            if ((null != lpResumeIndex)) {
                encoder.WriteValue(lpResumeIndex.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum43NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum43NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_RCreateServiceWOW64A(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
            RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hSCManager = decoder.ReadContextHandle();
            lpServiceName = decoder.ReadUnsignedCharString();
            if ((decoder.ReadReferentId() == 0)) {
                lpDisplayName = null;
            }
            else {
                lpDisplayName = decoder.ReadUnsignedCharString();
            }
            dwDesiredAccess = decoder.ReadUInt32();
            dwServiceType = decoder.ReadUInt32();
            dwStartType = decoder.ReadUInt32();
            dwErrorControl = decoder.ReadUInt32();
            lpBinaryPathName = decoder.ReadUnsignedCharString();
            if ((decoder.ReadReferentId() == 0)) {
                lpLoadOrderGroup = null;
            }
            else {
                lpLoadOrderGroup = decoder.ReadUnsignedCharString();
            }
            lpdwTagId = decoder.ReadPointer<uint>();
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpDependencies = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpDependencies.Length); i++
            ) {
                byte elem_0 = lpDependencies[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpDependencies[i] = elem_0;
            }
            dwDependSize = decoder.ReadUInt32();
            if ((decoder.ReadReferentId() == 0)) {
                lpServiceStartName = null;
            }
            else {
                lpServiceStartName = decoder.ReadUnsignedCharString();
            }
            lpPassword = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpPassword.Length); i++
            ) {
                byte elem_0 = lpPassword[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpPassword[i] = elem_0;
            }
            dwPwSize = decoder.ReadUInt32();
            var invokeTask = this._obj.RCreateServiceWOW64A(hSCManager, lpServiceName, lpDisplayName, dwDesiredAccess, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpServiceHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteContextHandle(lpServiceHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RCreateServiceWOW64W(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
            RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hSCManager = decoder.ReadContextHandle();
            lpServiceName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                lpDisplayName = null;
            }
            else {
                lpDisplayName = decoder.ReadWideCharString();
            }
            dwDesiredAccess = decoder.ReadUInt32();
            dwServiceType = decoder.ReadUInt32();
            dwStartType = decoder.ReadUInt32();
            dwErrorControl = decoder.ReadUInt32();
            lpBinaryPathName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                lpLoadOrderGroup = null;
            }
            else {
                lpLoadOrderGroup = decoder.ReadWideCharString();
            }
            lpdwTagId = decoder.ReadPointer<uint>();
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpDependencies = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpDependencies.Length); i++
            ) {
                byte elem_0 = lpDependencies[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpDependencies[i] = elem_0;
            }
            dwDependSize = decoder.ReadUInt32();
            if ((decoder.ReadReferentId() == 0)) {
                lpServiceStartName = null;
            }
            else {
                lpServiceStartName = decoder.ReadWideCharString();
            }
            lpPassword = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpPassword.Length); i++
            ) {
                byte elem_0 = lpPassword[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpPassword[i] = elem_0;
            }
            dwPwSize = decoder.ReadUInt32();
            var invokeTask = this._obj.RCreateServiceWOW64W(hSCManager, lpServiceName, lpDisplayName, dwDesiredAccess, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, lpServiceHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteContextHandle(lpServiceHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum46NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum46NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_RNotifyServiceStatusChange(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            SC_RPC_NOTIFY_PARAMS NotifyParams;
            System.Guid pClientProcessGuid;
            RpcPointer<System.Guid> pSCMProcessGuid = new RpcPointer<System.Guid>();
            RpcPointer<int> pfCreateRemoteQueue = new RpcPointer<int>();
            RpcPointer<Titanis.DceRpc.RpcContextHandle> phNotify = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hService = decoder.ReadContextHandle();
            NotifyParams = decoder.ReadFixedStruct<SC_RPC_NOTIFY_PARAMS>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SC_RPC_NOTIFY_PARAMS>(ref NotifyParams);
            pClientProcessGuid = decoder.ReadUuid();
            var invokeTask = this._obj.RNotifyServiceStatusChange(hService, NotifyParams, pClientProcessGuid, pSCMProcessGuid, pfCreateRemoteQueue, phNotify, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(pSCMProcessGuid.value);
            encoder.WriteValue(pfCreateRemoteQueue.value);
            encoder.WriteContextHandle(phNotify.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RGetNotifyResults(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hNotify;
            RpcPointer<RpcPointer<SC_RPC_NOTIFY_PARAMS_LIST>> ppNotifyParams = new RpcPointer<RpcPointer<SC_RPC_NOTIFY_PARAMS_LIST>>();
            hNotify = decoder.ReadContextHandle();
            var invokeTask = this._obj.RGetNotifyResults(hNotify, ppNotifyParams, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppNotifyParams.value);
            if ((null != ppNotifyParams.value)) {
                encoder.WriteConformantStruct(ppNotifyParams.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ppNotifyParams.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RCloseNotifyHandle(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> phNotify;
            RpcPointer<int> pfApcFired = new RpcPointer<int>();
            phNotify = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            phNotify.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.RCloseNotifyHandle(phNotify, pfApcFired, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(phNotify.value);
            encoder.WriteValue(pfApcFired.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_RControlServiceExA(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
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
        private async Task Invoke_RControlServiceExW(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
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
        private async Task Invoke_Opnum52NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum52NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum53NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum53NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum54NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum54NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum55NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum55NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_RQueryServiceConfigEx(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hService;
            uint dwInfoLevel;
            RpcPointer<SC_RPC_CONFIG_INFOW> pInfo = new RpcPointer<SC_RPC_CONFIG_INFOW>();
            hService = decoder.ReadContextHandle();
            dwInfoLevel = decoder.ReadUInt32();
            var invokeTask = this._obj.RQueryServiceConfigEx(hService, dwInfoLevel, pInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pInfo.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(pInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum57NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum57NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum58NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum58NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum59NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum59NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_RCreateWowService(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hSCManager;
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
            RpcPointer<Titanis.DceRpc.RpcContextHandle> lpServiceHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hSCManager = decoder.ReadContextHandle();
            lpServiceName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                lpDisplayName = null;
            }
            else {
                lpDisplayName = decoder.ReadWideCharString();
            }
            dwDesiredAccess = decoder.ReadUInt32();
            dwServiceType = decoder.ReadUInt32();
            dwStartType = decoder.ReadUInt32();
            dwErrorControl = decoder.ReadUInt32();
            lpBinaryPathName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                lpLoadOrderGroup = null;
            }
            else {
                lpLoadOrderGroup = decoder.ReadWideCharString();
            }
            lpdwTagId = decoder.ReadPointer<uint>();
            if ((null != lpdwTagId)) {
                lpdwTagId.value = decoder.ReadUInt32();
            }
            lpDependencies = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpDependencies.Length); i++
            ) {
                byte elem_0 = lpDependencies[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpDependencies[i] = elem_0;
            }
            dwDependSize = decoder.ReadUInt32();
            if ((decoder.ReadReferentId() == 0)) {
                lpServiceStartName = null;
            }
            else {
                lpServiceStartName = decoder.ReadWideCharString();
            }
            lpPassword = decoder.ReadArrayHeader<byte>();
            for (int i = 0; (i < lpPassword.Length); i++
            ) {
                byte elem_0 = lpPassword[i];
                elem_0 = decoder.ReadUnsignedChar();
                lpPassword[i] = elem_0;
            }
            dwPwSize = decoder.ReadUInt32();
            dwServiceWowType = decoder.ReadUInt16();
            var invokeTask = this._obj.RCreateWowService(hSCManager, lpServiceName, lpDisplayName, dwDesiredAccess, dwServiceType, dwStartType, dwErrorControl, lpBinaryPathName, lpLoadOrderGroup, lpdwTagId, lpDependencies, dwDependSize, lpServiceStartName, lpPassword, dwPwSize, dwServiceWowType, lpServiceHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(lpdwTagId);
            if ((null != lpdwTagId)) {
                encoder.WriteValue(lpdwTagId.value);
            }
            encoder.WriteContextHandle(lpServiceHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_ROpenSCManager2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DatabaseName;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> ScmHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            if ((decoder.ReadReferentId() == 0)) {
                DatabaseName = null;
            }
            else {
                DatabaseName = decoder.ReadWideCharString();
            }
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.ROpenSCManager2(DatabaseName, DesiredAccess, ScmHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(ScmHandle.value);
            encoder.WriteValue(retval);
        }
    }
}
