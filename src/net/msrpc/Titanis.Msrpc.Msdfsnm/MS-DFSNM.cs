#pragma warning disable

namespace MS_DFSNM {
    using System;
    using System.Threading.Tasks;
    using Titanis;
    using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum DFS_TARGET_PRIORITY_CLASS : int {
        DfsInvalidPriorityClass = -1,
        DfsSiteCostNormalPriorityClass = 0,
        DfsGlobalHighPriorityClass = 1,
        DfsSiteCostHighPriorityClass = 2,
        DfsSiteCostLowPriorityClass = 3,
        DfsGlobalLowPriorityClass = 4,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_TARGET_PRIORITY : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((int)(this.TargetPriorityClass)));
            encoder.WriteValue(this.TargetPriorityRank);
            encoder.WriteValue(this.Reserved);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.TargetPriorityClass = ((DFS_TARGET_PRIORITY_CLASS)(decoder.ReadInt32()));
            this.TargetPriorityRank = decoder.ReadUInt16();
            this.Reserved = decoder.ReadUInt16();
        }
        public DFS_TARGET_PRIORITY_CLASS TargetPriorityClass;
        public ushort TargetPriorityRank;
        public ushort Reserved;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_STORAGE_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.State);
            encoder.WritePointer(this.ServerName);
            encoder.WritePointer(this.ShareName);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.State = decoder.ReadUInt32();
            this.ServerName = decoder.ReadUniquePointer<string>();
            this.ShareName = decoder.ReadUniquePointer<string>();
        }
        public uint State;
        public RpcPointer<string> ServerName;
        public RpcPointer<string> ShareName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ServerName)) {
                encoder.WriteWideCharString(this.ServerName.value);
            }
            if ((null != this.ShareName)) {
                encoder.WriteWideCharString(this.ShareName.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ServerName)) {
                this.ServerName.value = decoder.ReadWideCharString();
            }
            if ((null != this.ShareName)) {
                this.ShareName.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_STORAGE_INFO_1 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.State);
            encoder.WritePointer(this.ServerName);
            encoder.WritePointer(this.ShareName);
            encoder.WriteFixedStruct(this.TargetPriority, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.State = decoder.ReadUInt32();
            this.ServerName = decoder.ReadUniquePointer<string>();
            this.ShareName = decoder.ReadUniquePointer<string>();
            this.TargetPriority = decoder.ReadFixedStruct<DFS_TARGET_PRIORITY>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public uint State;
        public RpcPointer<string> ServerName;
        public RpcPointer<string> ShareName;
        public DFS_TARGET_PRIORITY TargetPriority;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ServerName)) {
                encoder.WriteWideCharString(this.ServerName.value);
            }
            if ((null != this.ShareName)) {
                encoder.WriteWideCharString(this.ShareName.value);
            }
            encoder.WriteStructDeferral(this.TargetPriority);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ServerName)) {
                this.ServerName.value = decoder.ReadWideCharString();
            }
            if ((null != this.ShareName)) {
                this.ShareName.value = decoder.ReadWideCharString();
            }
            decoder.ReadStructDeferral<DFS_TARGET_PRIORITY>(ref this.TargetPriority);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFSM_ROOT_LIST_ENTRY : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.ServerShare);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ServerShare = decoder.ReadUniquePointer<string>();
        }
        public RpcPointer<string> ServerShare;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.ServerShare)) {
                encoder.WriteWideCharString(this.ServerShare.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.ServerShare)) {
                this.ServerShare.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFSM_ROOT_LIST : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cEntries);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cEntries = decoder.ReadUInt32();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.Entry);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Entry = decoder.ReadArrayHeader<DFSM_ROOT_LIST_ENTRY>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.Entry.Length); i++
            ) {
                DFSM_ROOT_LIST_ENTRY elem_0 = this.Entry[i];
                encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.Entry.Length); i++
            ) {
                DFSM_ROOT_LIST_ENTRY elem_0 = this.Entry[i];
                elem_0 = decoder.ReadFixedStruct<DFSM_ROOT_LIST_ENTRY>(Titanis.DceRpc.NdrAlignment.NativePtr);
                this.Entry[i] = elem_0;
            }
        }
        public uint cEntries;
        public DFSM_ROOT_LIST_ENTRY[] Entry;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.Entry.Length); i++
            ) {
                DFSM_ROOT_LIST_ENTRY elem_0 = this.Entry[i];
                encoder.WriteStructDeferral(elem_0);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.Entry.Length); i++
            ) {
                DFSM_ROOT_LIST_ENTRY elem_0 = this.Entry[i];
                decoder.ReadStructDeferral<DFSM_ROOT_LIST_ENTRY>(ref elem_0);
                this.Entry[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public enum DFS_NAMESPACE_VERSION_ORIGIN : int {
        DFS_NAMESPACE_VERSION_ORIGIN_COMBINED = 0,
        DFS_NAMESPACE_VERSION_ORIGIN_SERVER = 1,
        DFS_NAMESPACE_VERSION_ORIGIN_DOMAIN = 2,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_SUPPORTED_NAMESPACE_VERSION_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.DomainDfsMajorVersion);
            encoder.WriteValue(this.DomainDfsMinorVersion);
            encoder.WriteValue(this.DomainDfsCapabilities);
            encoder.WriteValue(this.StandaloneDfsMajorVersion);
            encoder.WriteValue(this.StandaloneDfsMinorVersion);
            encoder.WriteValue(this.StandaloneDfsCapabilities);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.DomainDfsMajorVersion = decoder.ReadUInt32();
            this.DomainDfsMinorVersion = decoder.ReadUInt32();
            this.DomainDfsCapabilities = decoder.ReadUInt64();
            this.StandaloneDfsMajorVersion = decoder.ReadUInt32();
            this.StandaloneDfsMinorVersion = decoder.ReadUInt32();
            this.StandaloneDfsCapabilities = decoder.ReadUInt64();
        }
        public uint DomainDfsMajorVersion;
        public uint DomainDfsMinorVersion;
        public ulong DomainDfsCapabilities;
        public uint StandaloneDfsMajorVersion;
        public uint StandaloneDfsMinorVersion;
        public ulong StandaloneDfsCapabilities;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_1 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.EntryPath);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntryPath = decoder.ReadUniquePointer<string>();
        }
        public RpcPointer<string> EntryPath;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EntryPath)) {
                encoder.WriteWideCharString(this.EntryPath.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EntryPath)) {
                this.EntryPath.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_2 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.EntryPath);
            encoder.WritePointer(this.Comment);
            encoder.WriteValue(this.State);
            encoder.WriteValue(this.NumberOfStorages);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntryPath = decoder.ReadUniquePointer<string>();
            this.Comment = decoder.ReadUniquePointer<string>();
            this.State = decoder.ReadUInt32();
            this.NumberOfStorages = decoder.ReadUInt32();
        }
        public RpcPointer<string> EntryPath;
        public RpcPointer<string> Comment;
        public uint State;
        public uint NumberOfStorages;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EntryPath)) {
                encoder.WriteWideCharString(this.EntryPath.value);
            }
            if ((null != this.Comment)) {
                encoder.WriteWideCharString(this.Comment.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EntryPath)) {
                this.EntryPath.value = decoder.ReadWideCharString();
            }
            if ((null != this.Comment)) {
                this.Comment.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_3 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.EntryPath);
            encoder.WritePointer(this.Comment);
            encoder.WriteValue(this.State);
            encoder.WriteValue(this.NumberOfStorages);
            encoder.WritePointer(this.Storage);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntryPath = decoder.ReadUniquePointer<string>();
            this.Comment = decoder.ReadUniquePointer<string>();
            this.State = decoder.ReadUInt32();
            this.NumberOfStorages = decoder.ReadUInt32();
            this.Storage = decoder.ReadUniquePointer<DFS_STORAGE_INFO[]>();
        }
        public RpcPointer<string> EntryPath;
        public RpcPointer<string> Comment;
        public uint State;
        public uint NumberOfStorages;
        public RpcPointer<DFS_STORAGE_INFO[]> Storage;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EntryPath)) {
                encoder.WriteWideCharString(this.EntryPath.value);
            }
            if ((null != this.Comment)) {
                encoder.WriteWideCharString(this.Comment.value);
            }
            if ((null != this.Storage)) {
                encoder.WriteArrayHeader(this.Storage.value);
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO elem_0 = this.Storage.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO elem_0 = this.Storage.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EntryPath)) {
                this.EntryPath.value = decoder.ReadWideCharString();
            }
            if ((null != this.Comment)) {
                this.Comment.value = decoder.ReadWideCharString();
            }
            if ((null != this.Storage)) {
                this.Storage.value = decoder.ReadArrayHeader<DFS_STORAGE_INFO>();
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO elem_0 = this.Storage.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_STORAGE_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Storage.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO elem_0 = this.Storage.value[i];
                    decoder.ReadStructDeferral<DFS_STORAGE_INFO>(ref elem_0);
                    this.Storage.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_4 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.EntryPath);
            encoder.WritePointer(this.Comment);
            encoder.WriteValue(this.State);
            encoder.WriteValue(this.Timeout);
            encoder.WriteValue(this.Guid);
            encoder.WriteValue(this.NumberOfStorages);
            encoder.WritePointer(this.Storage);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntryPath = decoder.ReadUniquePointer<string>();
            this.Comment = decoder.ReadUniquePointer<string>();
            this.State = decoder.ReadUInt32();
            this.Timeout = decoder.ReadUInt32();
            this.Guid = decoder.ReadUuid();
            this.NumberOfStorages = decoder.ReadUInt32();
            this.Storage = decoder.ReadUniquePointer<DFS_STORAGE_INFO[]>();
        }
        public RpcPointer<string> EntryPath;
        public RpcPointer<string> Comment;
        public uint State;
        public uint Timeout;
        public System.Guid Guid;
        public uint NumberOfStorages;
        public RpcPointer<DFS_STORAGE_INFO[]> Storage;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EntryPath)) {
                encoder.WriteWideCharString(this.EntryPath.value);
            }
            if ((null != this.Comment)) {
                encoder.WriteWideCharString(this.Comment.value);
            }
            if ((null != this.Storage)) {
                encoder.WriteArrayHeader(this.Storage.value);
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO elem_0 = this.Storage.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO elem_0 = this.Storage.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EntryPath)) {
                this.EntryPath.value = decoder.ReadWideCharString();
            }
            if ((null != this.Comment)) {
                this.Comment.value = decoder.ReadWideCharString();
            }
            if ((null != this.Storage)) {
                this.Storage.value = decoder.ReadArrayHeader<DFS_STORAGE_INFO>();
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO elem_0 = this.Storage.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_STORAGE_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Storage.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO elem_0 = this.Storage.value[i];
                    decoder.ReadStructDeferral<DFS_STORAGE_INFO>(ref elem_0);
                    this.Storage.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_5 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.EntryPath);
            encoder.WritePointer(this.Comment);
            encoder.WriteValue(this.State);
            encoder.WriteValue(this.Timeout);
            encoder.WriteValue(this.Guid);
            encoder.WriteValue(this.PropertyFlags);
            encoder.WriteValue(this.MetadataSize);
            encoder.WriteValue(this.NumberOfStorages);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntryPath = decoder.ReadUniquePointer<string>();
            this.Comment = decoder.ReadUniquePointer<string>();
            this.State = decoder.ReadUInt32();
            this.Timeout = decoder.ReadUInt32();
            this.Guid = decoder.ReadUuid();
            this.PropertyFlags = decoder.ReadUInt32();
            this.MetadataSize = decoder.ReadUInt32();
            this.NumberOfStorages = decoder.ReadUInt32();
        }
        public RpcPointer<string> EntryPath;
        public RpcPointer<string> Comment;
        public uint State;
        public uint Timeout;
        public System.Guid Guid;
        public uint PropertyFlags;
        public uint MetadataSize;
        public uint NumberOfStorages;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EntryPath)) {
                encoder.WriteWideCharString(this.EntryPath.value);
            }
            if ((null != this.Comment)) {
                encoder.WriteWideCharString(this.Comment.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EntryPath)) {
                this.EntryPath.value = decoder.ReadWideCharString();
            }
            if ((null != this.Comment)) {
                this.Comment.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_6 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.EntryPath);
            encoder.WritePointer(this.Comment);
            encoder.WriteValue(this.State);
            encoder.WriteValue(this.Timeout);
            encoder.WriteValue(this.Guid);
            encoder.WriteValue(this.PropertyFlags);
            encoder.WriteValue(this.MetadataSize);
            encoder.WriteValue(this.NumberOfStorages);
            encoder.WritePointer(this.Storage);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntryPath = decoder.ReadUniquePointer<string>();
            this.Comment = decoder.ReadUniquePointer<string>();
            this.State = decoder.ReadUInt32();
            this.Timeout = decoder.ReadUInt32();
            this.Guid = decoder.ReadUuid();
            this.PropertyFlags = decoder.ReadUInt32();
            this.MetadataSize = decoder.ReadUInt32();
            this.NumberOfStorages = decoder.ReadUInt32();
            this.Storage = decoder.ReadUniquePointer<DFS_STORAGE_INFO_1[]>();
        }
        public RpcPointer<string> EntryPath;
        public RpcPointer<string> Comment;
        public uint State;
        public uint Timeout;
        public System.Guid Guid;
        public uint PropertyFlags;
        public uint MetadataSize;
        public uint NumberOfStorages;
        public RpcPointer<DFS_STORAGE_INFO_1[]> Storage;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EntryPath)) {
                encoder.WriteWideCharString(this.EntryPath.value);
            }
            if ((null != this.Comment)) {
                encoder.WriteWideCharString(this.Comment.value);
            }
            if ((null != this.Storage)) {
                encoder.WriteArrayHeader(this.Storage.value);
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO_1 elem_0 = this.Storage.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO_1 elem_0 = this.Storage.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EntryPath)) {
                this.EntryPath.value = decoder.ReadWideCharString();
            }
            if ((null != this.Comment)) {
                this.Comment.value = decoder.ReadWideCharString();
            }
            if ((null != this.Storage)) {
                this.Storage.value = decoder.ReadArrayHeader<DFS_STORAGE_INFO_1>();
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO_1 elem_0 = this.Storage.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_STORAGE_INFO_1>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Storage.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO_1 elem_0 = this.Storage.value[i];
                    decoder.ReadStructDeferral<DFS_STORAGE_INFO_1>(ref elem_0);
                    this.Storage.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_7 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.GenerationGuid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.GenerationGuid = decoder.ReadUuid();
        }
        public System.Guid GenerationGuid;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_8 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.EntryPath);
            encoder.WritePointer(this.Comment);
            encoder.WriteValue(this.State);
            encoder.WriteValue(this.Timeout);
            encoder.WriteValue(this.Guid);
            encoder.WriteValue(this.PropertyFlags);
            encoder.WriteValue(this.MetadataSize);
            encoder.WriteValue(this.SecurityDescriptorLength);
            encoder.WritePointer(this.pSecurityDescriptor);
            encoder.WriteValue(this.NumberOfStorages);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntryPath = decoder.ReadUniquePointer<string>();
            this.Comment = decoder.ReadUniquePointer<string>();
            this.State = decoder.ReadUInt32();
            this.Timeout = decoder.ReadUInt32();
            this.Guid = decoder.ReadUuid();
            this.PropertyFlags = decoder.ReadUInt32();
            this.MetadataSize = decoder.ReadUInt32();
            this.SecurityDescriptorLength = decoder.ReadUInt32();
            this.pSecurityDescriptor = decoder.ReadUniquePointer<byte[]>();
            this.NumberOfStorages = decoder.ReadUInt32();
        }
        public RpcPointer<string> EntryPath;
        public RpcPointer<string> Comment;
        public uint State;
        public uint Timeout;
        public System.Guid Guid;
        public uint PropertyFlags;
        public uint MetadataSize;
        public uint SecurityDescriptorLength;
        public RpcPointer<byte[]> pSecurityDescriptor;
        public uint NumberOfStorages;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EntryPath)) {
                encoder.WriteWideCharString(this.EntryPath.value);
            }
            if ((null != this.Comment)) {
                encoder.WriteWideCharString(this.Comment.value);
            }
            if ((null != this.pSecurityDescriptor)) {
                encoder.WriteArrayHeader(this.pSecurityDescriptor.value);
                for (int i = 0; (i < this.pSecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.pSecurityDescriptor.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EntryPath)) {
                this.EntryPath.value = decoder.ReadWideCharString();
            }
            if ((null != this.Comment)) {
                this.Comment.value = decoder.ReadWideCharString();
            }
            if ((null != this.pSecurityDescriptor)) {
                this.pSecurityDescriptor.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.pSecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.pSecurityDescriptor.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.pSecurityDescriptor.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_9 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.EntryPath);
            encoder.WritePointer(this.Comment);
            encoder.WriteValue(this.State);
            encoder.WriteValue(this.Timeout);
            encoder.WriteValue(this.Guid);
            encoder.WriteValue(this.PropertyFlags);
            encoder.WriteValue(this.MetadataSize);
            encoder.WriteValue(this.SecurityDescriptorLength);
            encoder.WritePointer(this.pSecurityDescriptor);
            encoder.WriteValue(this.NumberOfStorages);
            encoder.WritePointer(this.Storage);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntryPath = decoder.ReadUniquePointer<string>();
            this.Comment = decoder.ReadUniquePointer<string>();
            this.State = decoder.ReadUInt32();
            this.Timeout = decoder.ReadUInt32();
            this.Guid = decoder.ReadUuid();
            this.PropertyFlags = decoder.ReadUInt32();
            this.MetadataSize = decoder.ReadUInt32();
            this.SecurityDescriptorLength = decoder.ReadUInt32();
            this.pSecurityDescriptor = decoder.ReadUniquePointer<byte[]>();
            this.NumberOfStorages = decoder.ReadUInt32();
            this.Storage = decoder.ReadUniquePointer<DFS_STORAGE_INFO_1[]>();
        }
        public RpcPointer<string> EntryPath;
        public RpcPointer<string> Comment;
        public uint State;
        public uint Timeout;
        public System.Guid Guid;
        public uint PropertyFlags;
        public uint MetadataSize;
        public uint SecurityDescriptorLength;
        public RpcPointer<byte[]> pSecurityDescriptor;
        public uint NumberOfStorages;
        public RpcPointer<DFS_STORAGE_INFO_1[]> Storage;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.EntryPath)) {
                encoder.WriteWideCharString(this.EntryPath.value);
            }
            if ((null != this.Comment)) {
                encoder.WriteWideCharString(this.Comment.value);
            }
            if ((null != this.pSecurityDescriptor)) {
                encoder.WriteArrayHeader(this.pSecurityDescriptor.value);
                for (int i = 0; (i < this.pSecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.pSecurityDescriptor.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            if ((null != this.Storage)) {
                encoder.WriteArrayHeader(this.Storage.value);
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO_1 elem_0 = this.Storage.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO_1 elem_0 = this.Storage.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.EntryPath)) {
                this.EntryPath.value = decoder.ReadWideCharString();
            }
            if ((null != this.Comment)) {
                this.Comment.value = decoder.ReadWideCharString();
            }
            if ((null != this.pSecurityDescriptor)) {
                this.pSecurityDescriptor.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.pSecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.pSecurityDescriptor.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.pSecurityDescriptor.value[i] = elem_0;
                }
            }
            if ((null != this.Storage)) {
                this.Storage.value = decoder.ReadArrayHeader<DFS_STORAGE_INFO_1>();
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO_1 elem_0 = this.Storage.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_STORAGE_INFO_1>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Storage.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Storage.value.Length); i++
                ) {
                    DFS_STORAGE_INFO_1 elem_0 = this.Storage.value[i];
                    decoder.ReadStructDeferral<DFS_STORAGE_INFO_1>(ref elem_0);
                    this.Storage.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_50 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.NamespaceMajorVersion);
            encoder.WriteValue(this.NamespaceMinorVersion);
            encoder.WriteValue(this.NamespaceCapabilities);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.NamespaceMajorVersion = decoder.ReadUInt32();
            this.NamespaceMinorVersion = decoder.ReadUInt32();
            this.NamespaceCapabilities = decoder.ReadUInt64();
        }
        public uint NamespaceMajorVersion;
        public uint NamespaceMinorVersion;
        public ulong NamespaceCapabilities;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_100 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.Comment);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Comment = decoder.ReadUniquePointer<string>();
        }
        public RpcPointer<string> Comment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Comment)) {
                encoder.WriteWideCharString(this.Comment.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Comment)) {
                this.Comment.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_101 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.State);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.State = decoder.ReadUInt32();
        }
        public uint State;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_102 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Timeout);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Timeout = decoder.ReadUInt32();
        }
        public uint Timeout;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_103 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.PropertyFlagMask);
            encoder.WriteValue(this.PropertyFlags);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.PropertyFlagMask = decoder.ReadUInt32();
            this.PropertyFlags = decoder.ReadUInt32();
        }
        public uint PropertyFlagMask;
        public uint PropertyFlags;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_104 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.TargetPriority, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.TargetPriority = decoder.ReadFixedStruct<DFS_TARGET_PRIORITY>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public DFS_TARGET_PRIORITY TargetPriority;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.TargetPriority);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<DFS_TARGET_PRIORITY>(ref this.TargetPriority);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_105 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.Comment);
            encoder.WriteValue(this.State);
            encoder.WriteValue(this.Timeout);
            encoder.WriteValue(this.PropertyFlagMask);
            encoder.WriteValue(this.PropertyFlags);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Comment = decoder.ReadUniquePointer<string>();
            this.State = decoder.ReadUInt32();
            this.Timeout = decoder.ReadUInt32();
            this.PropertyFlagMask = decoder.ReadUInt32();
            this.PropertyFlags = decoder.ReadUInt32();
        }
        public RpcPointer<string> Comment;
        public uint State;
        public uint Timeout;
        public uint PropertyFlagMask;
        public uint PropertyFlags;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Comment)) {
                encoder.WriteWideCharString(this.Comment.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Comment)) {
                this.Comment.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_106 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.State);
            encoder.WriteFixedStruct(this.TargetPriority, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.State = decoder.ReadUInt32();
            this.TargetPriority = decoder.ReadFixedStruct<DFS_TARGET_PRIORITY>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public uint State;
        public DFS_TARGET_PRIORITY TargetPriority;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.TargetPriority);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<DFS_TARGET_PRIORITY>(ref this.TargetPriority);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_107 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.Comment);
            encoder.WriteValue(this.State);
            encoder.WriteValue(this.Timeout);
            encoder.WriteValue(this.PropertyFlagMask);
            encoder.WriteValue(this.PropertyFlags);
            encoder.WriteValue(this.SecurityDescriptorLength);
            encoder.WritePointer(this.pSecurityDescriptor);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Comment = decoder.ReadUniquePointer<string>();
            this.State = decoder.ReadUInt32();
            this.Timeout = decoder.ReadUInt32();
            this.PropertyFlagMask = decoder.ReadUInt32();
            this.PropertyFlags = decoder.ReadUInt32();
            this.SecurityDescriptorLength = decoder.ReadUInt32();
            this.pSecurityDescriptor = decoder.ReadUniquePointer<byte[]>();
        }
        public RpcPointer<string> Comment;
        public uint State;
        public uint Timeout;
        public uint PropertyFlagMask;
        public uint PropertyFlags;
        public uint SecurityDescriptorLength;
        public RpcPointer<byte[]> pSecurityDescriptor;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Comment)) {
                encoder.WriteWideCharString(this.Comment.value);
            }
            if ((null != this.pSecurityDescriptor)) {
                encoder.WriteArrayHeader(this.pSecurityDescriptor.value);
                for (int i = 0; (i < this.pSecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.pSecurityDescriptor.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Comment)) {
                this.Comment.value = decoder.ReadWideCharString();
            }
            if ((null != this.pSecurityDescriptor)) {
                this.pSecurityDescriptor.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.pSecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.pSecurityDescriptor.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.pSecurityDescriptor.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_150 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.SecurityDescriptorLength);
            encoder.WritePointer(this.pSecurityDescriptor);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.SecurityDescriptorLength = decoder.ReadUInt32();
            this.pSecurityDescriptor = decoder.ReadUniquePointer<byte[]>();
        }
        public uint SecurityDescriptorLength;
        public RpcPointer<byte[]> pSecurityDescriptor;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pSecurityDescriptor)) {
                encoder.WriteArrayHeader(this.pSecurityDescriptor.value);
                for (int i = 0; (i < this.pSecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.pSecurityDescriptor.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pSecurityDescriptor)) {
                this.pSecurityDescriptor.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.pSecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.pSecurityDescriptor.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.pSecurityDescriptor.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_200 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.FtDfsName);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.FtDfsName = decoder.ReadUniquePointer<string>();
        }
        public RpcPointer<string> FtDfsName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.FtDfsName)) {
                encoder.WriteWideCharString(this.FtDfsName.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.FtDfsName)) {
                this.FtDfsName.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_300 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Flags);
            encoder.WritePointer(this.DfsName);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Flags = decoder.ReadUInt32();
            this.DfsName = decoder.ReadUniquePointer<string>();
        }
        public uint Flags;
        public RpcPointer<string> DfsName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.DfsName)) {
                encoder.WriteWideCharString(this.DfsName.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.DfsName)) {
                this.DfsName.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_STRUCT : Titanis.DceRpc.IRpcFixedStruct {
        public uint unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.unionSwitch);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WritePointer(this.DfsInfo1);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WritePointer(this.DfsInfo2);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WritePointer(this.DfsInfo3);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WritePointer(this.DfsInfo4);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WritePointer(this.DfsInfo5);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    encoder.WritePointer(this.DfsInfo6);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        encoder.WritePointer(this.DfsInfo7);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            encoder.WritePointer(this.DfsInfo8);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                encoder.WritePointer(this.DfsInfo9);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 50)) {
                                                    encoder.WritePointer(this.DfsInfo50);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 100)) {
                                                        encoder.WritePointer(this.DfsInfo100);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 101)) {
                                                            encoder.WritePointer(this.DfsInfo101);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 102)) {
                                                                encoder.WritePointer(this.DfsInfo102);
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 103)) {
                                                                    encoder.WritePointer(this.DfsInfo103);
                                                                }
                                                                else {
                                                                    if ((((int)(this.unionSwitch)) == 104)) {
                                                                        encoder.WritePointer(this.DfsInfo104);
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.unionSwitch)) == 105)) {
                                                                            encoder.WritePointer(this.DfsInfo105);
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.unionSwitch)) == 106)) {
                                                                                encoder.WritePointer(this.DfsInfo106);
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.unionSwitch)) == 107)) {
                                                                                    encoder.WritePointer(this.DfsInfo107);
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.unionSwitch)) == 150)) {
                                                                                        encoder.WritePointer(this.DfsInfo150);
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
            this.unionSwitch = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.DfsInfo1 = decoder.ReadUniquePointer<DFS_INFO_1>();
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.DfsInfo2 = decoder.ReadUniquePointer<DFS_INFO_2>();
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.DfsInfo3 = decoder.ReadUniquePointer<DFS_INFO_3>();
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            this.DfsInfo4 = decoder.ReadUniquePointer<DFS_INFO_4>();
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                this.DfsInfo5 = decoder.ReadUniquePointer<DFS_INFO_5>();
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    this.DfsInfo6 = decoder.ReadUniquePointer<DFS_INFO_6>();
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        this.DfsInfo7 = decoder.ReadUniquePointer<DFS_INFO_7>();
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            this.DfsInfo8 = decoder.ReadUniquePointer<DFS_INFO_8>();
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                this.DfsInfo9 = decoder.ReadUniquePointer<DFS_INFO_9>();
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 50)) {
                                                    this.DfsInfo50 = decoder.ReadUniquePointer<DFS_INFO_50>();
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 100)) {
                                                        this.DfsInfo100 = decoder.ReadUniquePointer<DFS_INFO_100>();
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 101)) {
                                                            this.DfsInfo101 = decoder.ReadUniquePointer<DFS_INFO_101>();
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 102)) {
                                                                this.DfsInfo102 = decoder.ReadUniquePointer<DFS_INFO_102>();
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 103)) {
                                                                    this.DfsInfo103 = decoder.ReadUniquePointer<DFS_INFO_103>();
                                                                }
                                                                else {
                                                                    if ((((int)(this.unionSwitch)) == 104)) {
                                                                        this.DfsInfo104 = decoder.ReadUniquePointer<DFS_INFO_104>();
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.unionSwitch)) == 105)) {
                                                                            this.DfsInfo105 = decoder.ReadUniquePointer<DFS_INFO_105>();
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.unionSwitch)) == 106)) {
                                                                                this.DfsInfo106 = decoder.ReadUniquePointer<DFS_INFO_106>();
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.unionSwitch)) == 107)) {
                                                                                    this.DfsInfo107 = decoder.ReadUniquePointer<DFS_INFO_107>();
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.unionSwitch)) == 150)) {
                                                                                        this.DfsInfo150 = decoder.ReadUniquePointer<DFS_INFO_150>();
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
            if ((((int)(this.unionSwitch)) == 1)) {
                if ((null != this.DfsInfo1)) {
                    encoder.WriteFixedStruct(this.DfsInfo1.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(this.DfsInfo1.value);
                }
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    if ((null != this.DfsInfo2)) {
                        encoder.WriteFixedStruct(this.DfsInfo2.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                        encoder.WriteStructDeferral(this.DfsInfo2.value);
                    }
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        if ((null != this.DfsInfo3)) {
                            encoder.WriteFixedStruct(this.DfsInfo3.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                            encoder.WriteStructDeferral(this.DfsInfo3.value);
                        }
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            if ((null != this.DfsInfo4)) {
                                encoder.WriteFixedStruct(this.DfsInfo4.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                encoder.WriteStructDeferral(this.DfsInfo4.value);
                            }
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                if ((null != this.DfsInfo5)) {
                                    encoder.WriteFixedStruct(this.DfsInfo5.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                    encoder.WriteStructDeferral(this.DfsInfo5.value);
                                }
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    if ((null != this.DfsInfo6)) {
                                        encoder.WriteFixedStruct(this.DfsInfo6.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                        encoder.WriteStructDeferral(this.DfsInfo6.value);
                                    }
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        if ((null != this.DfsInfo7)) {
                                            encoder.WriteFixedStruct(this.DfsInfo7.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                            encoder.WriteStructDeferral(this.DfsInfo7.value);
                                        }
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            if ((null != this.DfsInfo8)) {
                                                encoder.WriteFixedStruct(this.DfsInfo8.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                encoder.WriteStructDeferral(this.DfsInfo8.value);
                                            }
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                if ((null != this.DfsInfo9)) {
                                                    encoder.WriteFixedStruct(this.DfsInfo9.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    encoder.WriteStructDeferral(this.DfsInfo9.value);
                                                }
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 50)) {
                                                    if ((null != this.DfsInfo50)) {
                                                        encoder.WriteFixedStruct(this.DfsInfo50.value, Titanis.DceRpc.NdrAlignment._8Byte);
                                                        encoder.WriteStructDeferral(this.DfsInfo50.value);
                                                    }
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 100)) {
                                                        if ((null != this.DfsInfo100)) {
                                                            encoder.WriteFixedStruct(this.DfsInfo100.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                            encoder.WriteStructDeferral(this.DfsInfo100.value);
                                                        }
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 101)) {
                                                            if ((null != this.DfsInfo101)) {
                                                                encoder.WriteFixedStruct(this.DfsInfo101.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                                                encoder.WriteStructDeferral(this.DfsInfo101.value);
                                                            }
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 102)) {
                                                                if ((null != this.DfsInfo102)) {
                                                                    encoder.WriteFixedStruct(this.DfsInfo102.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                                                    encoder.WriteStructDeferral(this.DfsInfo102.value);
                                                                }
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 103)) {
                                                                    if ((null != this.DfsInfo103)) {
                                                                        encoder.WriteFixedStruct(this.DfsInfo103.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                                                        encoder.WriteStructDeferral(this.DfsInfo103.value);
                                                                    }
                                                                }
                                                                else {
                                                                    if ((((int)(this.unionSwitch)) == 104)) {
                                                                        if ((null != this.DfsInfo104)) {
                                                                            encoder.WriteFixedStruct(this.DfsInfo104.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                                                            encoder.WriteStructDeferral(this.DfsInfo104.value);
                                                                        }
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.unionSwitch)) == 105)) {
                                                                            if ((null != this.DfsInfo105)) {
                                                                                encoder.WriteFixedStruct(this.DfsInfo105.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                encoder.WriteStructDeferral(this.DfsInfo105.value);
                                                                            }
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.unionSwitch)) == 106)) {
                                                                                if ((null != this.DfsInfo106)) {
                                                                                    encoder.WriteFixedStruct(this.DfsInfo106.value, Titanis.DceRpc.NdrAlignment._4Byte);
                                                                                    encoder.WriteStructDeferral(this.DfsInfo106.value);
                                                                                }
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.unionSwitch)) == 107)) {
                                                                                    if ((null != this.DfsInfo107)) {
                                                                                        encoder.WriteFixedStruct(this.DfsInfo107.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                        encoder.WriteStructDeferral(this.DfsInfo107.value);
                                                                                    }
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.unionSwitch)) == 150)) {
                                                                                        if ((null != this.DfsInfo150)) {
                                                                                            encoder.WriteFixedStruct(this.DfsInfo150.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                            encoder.WriteStructDeferral(this.DfsInfo150.value);
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
            if ((((int)(this.unionSwitch)) == 1)) {
                if ((null != this.DfsInfo1)) {
                    this.DfsInfo1.value = decoder.ReadFixedStruct<DFS_INFO_1>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFS_INFO_1>(ref this.DfsInfo1.value);
                }
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    if ((null != this.DfsInfo2)) {
                        this.DfsInfo2.value = decoder.ReadFixedStruct<DFS_INFO_2>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        decoder.ReadStructDeferral<DFS_INFO_2>(ref this.DfsInfo2.value);
                    }
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        if ((null != this.DfsInfo3)) {
                            this.DfsInfo3.value = decoder.ReadFixedStruct<DFS_INFO_3>(Titanis.DceRpc.NdrAlignment.NativePtr);
                            decoder.ReadStructDeferral<DFS_INFO_3>(ref this.DfsInfo3.value);
                        }
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            if ((null != this.DfsInfo4)) {
                                this.DfsInfo4.value = decoder.ReadFixedStruct<DFS_INFO_4>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                decoder.ReadStructDeferral<DFS_INFO_4>(ref this.DfsInfo4.value);
                            }
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                if ((null != this.DfsInfo5)) {
                                    this.DfsInfo5.value = decoder.ReadFixedStruct<DFS_INFO_5>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                    decoder.ReadStructDeferral<DFS_INFO_5>(ref this.DfsInfo5.value);
                                }
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    if ((null != this.DfsInfo6)) {
                                        this.DfsInfo6.value = decoder.ReadFixedStruct<DFS_INFO_6>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                        decoder.ReadStructDeferral<DFS_INFO_6>(ref this.DfsInfo6.value);
                                    }
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        if ((null != this.DfsInfo7)) {
                                            this.DfsInfo7.value = decoder.ReadFixedStruct<DFS_INFO_7>(Titanis.DceRpc.NdrAlignment._4Byte);
                                            decoder.ReadStructDeferral<DFS_INFO_7>(ref this.DfsInfo7.value);
                                        }
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            if ((null != this.DfsInfo8)) {
                                                this.DfsInfo8.value = decoder.ReadFixedStruct<DFS_INFO_8>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                decoder.ReadStructDeferral<DFS_INFO_8>(ref this.DfsInfo8.value);
                                            }
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                if ((null != this.DfsInfo9)) {
                                                    this.DfsInfo9.value = decoder.ReadFixedStruct<DFS_INFO_9>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    decoder.ReadStructDeferral<DFS_INFO_9>(ref this.DfsInfo9.value);
                                                }
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 50)) {
                                                    if ((null != this.DfsInfo50)) {
                                                        this.DfsInfo50.value = decoder.ReadFixedStruct<DFS_INFO_50>(Titanis.DceRpc.NdrAlignment._8Byte);
                                                        decoder.ReadStructDeferral<DFS_INFO_50>(ref this.DfsInfo50.value);
                                                    }
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 100)) {
                                                        if ((null != this.DfsInfo100)) {
                                                            this.DfsInfo100.value = decoder.ReadFixedStruct<DFS_INFO_100>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                            decoder.ReadStructDeferral<DFS_INFO_100>(ref this.DfsInfo100.value);
                                                        }
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 101)) {
                                                            if ((null != this.DfsInfo101)) {
                                                                this.DfsInfo101.value = decoder.ReadFixedStruct<DFS_INFO_101>(Titanis.DceRpc.NdrAlignment._4Byte);
                                                                decoder.ReadStructDeferral<DFS_INFO_101>(ref this.DfsInfo101.value);
                                                            }
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 102)) {
                                                                if ((null != this.DfsInfo102)) {
                                                                    this.DfsInfo102.value = decoder.ReadFixedStruct<DFS_INFO_102>(Titanis.DceRpc.NdrAlignment._4Byte);
                                                                    decoder.ReadStructDeferral<DFS_INFO_102>(ref this.DfsInfo102.value);
                                                                }
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 103)) {
                                                                    if ((null != this.DfsInfo103)) {
                                                                        this.DfsInfo103.value = decoder.ReadFixedStruct<DFS_INFO_103>(Titanis.DceRpc.NdrAlignment._4Byte);
                                                                        decoder.ReadStructDeferral<DFS_INFO_103>(ref this.DfsInfo103.value);
                                                                    }
                                                                }
                                                                else {
                                                                    if ((((int)(this.unionSwitch)) == 104)) {
                                                                        if ((null != this.DfsInfo104)) {
                                                                            this.DfsInfo104.value = decoder.ReadFixedStruct<DFS_INFO_104>(Titanis.DceRpc.NdrAlignment._4Byte);
                                                                            decoder.ReadStructDeferral<DFS_INFO_104>(ref this.DfsInfo104.value);
                                                                        }
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.unionSwitch)) == 105)) {
                                                                            if ((null != this.DfsInfo105)) {
                                                                                this.DfsInfo105.value = decoder.ReadFixedStruct<DFS_INFO_105>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                decoder.ReadStructDeferral<DFS_INFO_105>(ref this.DfsInfo105.value);
                                                                            }
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.unionSwitch)) == 106)) {
                                                                                if ((null != this.DfsInfo106)) {
                                                                                    this.DfsInfo106.value = decoder.ReadFixedStruct<DFS_INFO_106>(Titanis.DceRpc.NdrAlignment._4Byte);
                                                                                    decoder.ReadStructDeferral<DFS_INFO_106>(ref this.DfsInfo106.value);
                                                                                }
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.unionSwitch)) == 107)) {
                                                                                    if ((null != this.DfsInfo107)) {
                                                                                        this.DfsInfo107.value = decoder.ReadFixedStruct<DFS_INFO_107>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                        decoder.ReadStructDeferral<DFS_INFO_107>(ref this.DfsInfo107.value);
                                                                                    }
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.unionSwitch)) == 150)) {
                                                                                        if ((null != this.DfsInfo150)) {
                                                                                            this.DfsInfo150.value = decoder.ReadFixedStruct<DFS_INFO_150>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                            decoder.ReadStructDeferral<DFS_INFO_150>(ref this.DfsInfo150.value);
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
        public RpcPointer<DFS_INFO_1> DfsInfo1;
        public RpcPointer<DFS_INFO_2> DfsInfo2;
        public RpcPointer<DFS_INFO_3> DfsInfo3;
        public RpcPointer<DFS_INFO_4> DfsInfo4;
        public RpcPointer<DFS_INFO_5> DfsInfo5;
        public RpcPointer<DFS_INFO_6> DfsInfo6;
        public RpcPointer<DFS_INFO_7> DfsInfo7;
        public RpcPointer<DFS_INFO_8> DfsInfo8;
        public RpcPointer<DFS_INFO_9> DfsInfo9;
        public RpcPointer<DFS_INFO_50> DfsInfo50;
        public RpcPointer<DFS_INFO_100> DfsInfo100;
        public RpcPointer<DFS_INFO_101> DfsInfo101;
        public RpcPointer<DFS_INFO_102> DfsInfo102;
        public RpcPointer<DFS_INFO_103> DfsInfo103;
        public RpcPointer<DFS_INFO_104> DfsInfo104;
        public RpcPointer<DFS_INFO_105> DfsInfo105;
        public RpcPointer<DFS_INFO_106> DfsInfo106;
        public RpcPointer<DFS_INFO_107> DfsInfo107;
        public RpcPointer<DFS_INFO_150> DfsInfo150;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_1_CONTAINER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadUniquePointer<DFS_INFO_1[]>();
        }
        public uint EntriesRead;
        public RpcPointer<DFS_INFO_1[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_1 elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_1 elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<DFS_INFO_1>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_1 elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_INFO_1>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_1 elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<DFS_INFO_1>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_2_CONTAINER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadUniquePointer<DFS_INFO_2[]>();
        }
        public uint EntriesRead;
        public RpcPointer<DFS_INFO_2[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_2 elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_2 elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<DFS_INFO_2>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_2 elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_INFO_2>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_2 elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<DFS_INFO_2>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_3_CONTAINER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadUniquePointer<DFS_INFO_3[]>();
        }
        public uint EntriesRead;
        public RpcPointer<DFS_INFO_3[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_3 elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_3 elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<DFS_INFO_3>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_3 elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_INFO_3>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_3 elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<DFS_INFO_3>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_4_CONTAINER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadUniquePointer<DFS_INFO_4[]>();
        }
        public uint EntriesRead;
        public RpcPointer<DFS_INFO_4[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_4 elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_4 elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<DFS_INFO_4>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_4 elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_INFO_4>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_4 elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<DFS_INFO_4>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_5_CONTAINER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadUniquePointer<DFS_INFO_5[]>();
        }
        public uint EntriesRead;
        public RpcPointer<DFS_INFO_5[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_5 elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_5 elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<DFS_INFO_5>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_5 elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_INFO_5>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_5 elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<DFS_INFO_5>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_6_CONTAINER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadUniquePointer<DFS_INFO_6[]>();
        }
        public uint EntriesRead;
        public RpcPointer<DFS_INFO_6[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_6 elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_6 elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<DFS_INFO_6>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_6 elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_INFO_6>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_6 elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<DFS_INFO_6>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_8_CONTAINER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadUniquePointer<DFS_INFO_8[]>();
        }
        public uint EntriesRead;
        public RpcPointer<DFS_INFO_8[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_8 elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_8 elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<DFS_INFO_8>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_8 elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_INFO_8>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_8 elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<DFS_INFO_8>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_9_CONTAINER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadUniquePointer<DFS_INFO_9[]>();
        }
        public uint EntriesRead;
        public RpcPointer<DFS_INFO_9[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_9 elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_9 elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<DFS_INFO_9>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_9 elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_INFO_9>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_9 elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<DFS_INFO_9>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_200_CONTAINER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadUniquePointer<DFS_INFO_200[]>();
        }
        public uint EntriesRead;
        public RpcPointer<DFS_INFO_200[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_200 elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_200 elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<DFS_INFO_200>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_200 elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_INFO_200>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_200 elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<DFS_INFO_200>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_300_CONTAINER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadUniquePointer<DFS_INFO_300[]>();
        }
        public uint EntriesRead;
        public RpcPointer<DFS_INFO_300[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_300 elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_300 elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<DFS_INFO_300>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_300 elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<DFS_INFO_300>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    DFS_INFO_300 elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<DFS_INFO_300>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct Unnamed_1 : Titanis.DceRpc.IRpcFixedStruct {
        public uint Level;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Level);
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.Level)) == 1)) {
                encoder.WritePointer(this.DfsInfo1Container);
            }
            else {
                if ((((int)(this.Level)) == 2)) {
                    encoder.WritePointer(this.DfsInfo2Container);
                }
                else {
                    if ((((int)(this.Level)) == 3)) {
                        encoder.WritePointer(this.DfsInfo3Container);
                    }
                    else {
                        if ((((int)(this.Level)) == 4)) {
                            encoder.WritePointer(this.DfsInfo4Container);
                        }
                        else {
                            if ((((int)(this.Level)) == 5)) {
                                encoder.WritePointer(this.DfsInfo5Container);
                            }
                            else {
                                if ((((int)(this.Level)) == 6)) {
                                    encoder.WritePointer(this.DfsInfo6Container);
                                }
                                else {
                                    if ((((int)(this.Level)) == 8)) {
                                        encoder.WritePointer(this.DfsInfo8Container);
                                    }
                                    else {
                                        if ((((int)(this.Level)) == 9)) {
                                            encoder.WritePointer(this.DfsInfo9Container);
                                        }
                                        else {
                                            if ((((int)(this.Level)) == 200)) {
                                                encoder.WritePointer(this.DfsInfo200Container);
                                            }
                                            else {
                                                if ((((int)(this.Level)) == 300)) {
                                                    encoder.WritePointer(this.DfsInfo300Container);
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
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Level = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.Level)) == 1)) {
                this.DfsInfo1Container = decoder.ReadUniquePointer<DFS_INFO_1_CONTAINER>();
            }
            else {
                if ((((int)(this.Level)) == 2)) {
                    this.DfsInfo2Container = decoder.ReadUniquePointer<DFS_INFO_2_CONTAINER>();
                }
                else {
                    if ((((int)(this.Level)) == 3)) {
                        this.DfsInfo3Container = decoder.ReadUniquePointer<DFS_INFO_3_CONTAINER>();
                    }
                    else {
                        if ((((int)(this.Level)) == 4)) {
                            this.DfsInfo4Container = decoder.ReadUniquePointer<DFS_INFO_4_CONTAINER>();
                        }
                        else {
                            if ((((int)(this.Level)) == 5)) {
                                this.DfsInfo5Container = decoder.ReadUniquePointer<DFS_INFO_5_CONTAINER>();
                            }
                            else {
                                if ((((int)(this.Level)) == 6)) {
                                    this.DfsInfo6Container = decoder.ReadUniquePointer<DFS_INFO_6_CONTAINER>();
                                }
                                else {
                                    if ((((int)(this.Level)) == 8)) {
                                        this.DfsInfo8Container = decoder.ReadUniquePointer<DFS_INFO_8_CONTAINER>();
                                    }
                                    else {
                                        if ((((int)(this.Level)) == 9)) {
                                            this.DfsInfo9Container = decoder.ReadUniquePointer<DFS_INFO_9_CONTAINER>();
                                        }
                                        else {
                                            if ((((int)(this.Level)) == 200)) {
                                                this.DfsInfo200Container = decoder.ReadUniquePointer<DFS_INFO_200_CONTAINER>();
                                            }
                                            else {
                                                if ((((int)(this.Level)) == 300)) {
                                                    this.DfsInfo300Container = decoder.ReadUniquePointer<DFS_INFO_300_CONTAINER>();
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
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.Level)) == 1)) {
                if ((null != this.DfsInfo1Container)) {
                    encoder.WriteFixedStruct(this.DfsInfo1Container.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(this.DfsInfo1Container.value);
                }
            }
            else {
                if ((((int)(this.Level)) == 2)) {
                    if ((null != this.DfsInfo2Container)) {
                        encoder.WriteFixedStruct(this.DfsInfo2Container.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                        encoder.WriteStructDeferral(this.DfsInfo2Container.value);
                    }
                }
                else {
                    if ((((int)(this.Level)) == 3)) {
                        if ((null != this.DfsInfo3Container)) {
                            encoder.WriteFixedStruct(this.DfsInfo3Container.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                            encoder.WriteStructDeferral(this.DfsInfo3Container.value);
                        }
                    }
                    else {
                        if ((((int)(this.Level)) == 4)) {
                            if ((null != this.DfsInfo4Container)) {
                                encoder.WriteFixedStruct(this.DfsInfo4Container.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                encoder.WriteStructDeferral(this.DfsInfo4Container.value);
                            }
                        }
                        else {
                            if ((((int)(this.Level)) == 5)) {
                                if ((null != this.DfsInfo5Container)) {
                                    encoder.WriteFixedStruct(this.DfsInfo5Container.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                    encoder.WriteStructDeferral(this.DfsInfo5Container.value);
                                }
                            }
                            else {
                                if ((((int)(this.Level)) == 6)) {
                                    if ((null != this.DfsInfo6Container)) {
                                        encoder.WriteFixedStruct(this.DfsInfo6Container.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                        encoder.WriteStructDeferral(this.DfsInfo6Container.value);
                                    }
                                }
                                else {
                                    if ((((int)(this.Level)) == 8)) {
                                        if ((null != this.DfsInfo8Container)) {
                                            encoder.WriteFixedStruct(this.DfsInfo8Container.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                            encoder.WriteStructDeferral(this.DfsInfo8Container.value);
                                        }
                                    }
                                    else {
                                        if ((((int)(this.Level)) == 9)) {
                                            if ((null != this.DfsInfo9Container)) {
                                                encoder.WriteFixedStruct(this.DfsInfo9Container.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                encoder.WriteStructDeferral(this.DfsInfo9Container.value);
                                            }
                                        }
                                        else {
                                            if ((((int)(this.Level)) == 200)) {
                                                if ((null != this.DfsInfo200Container)) {
                                                    encoder.WriteFixedStruct(this.DfsInfo200Container.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    encoder.WriteStructDeferral(this.DfsInfo200Container.value);
                                                }
                                            }
                                            else {
                                                if ((((int)(this.Level)) == 300)) {
                                                    if ((null != this.DfsInfo300Container)) {
                                                        encoder.WriteFixedStruct(this.DfsInfo300Container.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                        encoder.WriteStructDeferral(this.DfsInfo300Container.value);
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
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.Level)) == 1)) {
                if ((null != this.DfsInfo1Container)) {
                    this.DfsInfo1Container.value = decoder.ReadFixedStruct<DFS_INFO_1_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFS_INFO_1_CONTAINER>(ref this.DfsInfo1Container.value);
                }
            }
            else {
                if ((((int)(this.Level)) == 2)) {
                    if ((null != this.DfsInfo2Container)) {
                        this.DfsInfo2Container.value = decoder.ReadFixedStruct<DFS_INFO_2_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        decoder.ReadStructDeferral<DFS_INFO_2_CONTAINER>(ref this.DfsInfo2Container.value);
                    }
                }
                else {
                    if ((((int)(this.Level)) == 3)) {
                        if ((null != this.DfsInfo3Container)) {
                            this.DfsInfo3Container.value = decoder.ReadFixedStruct<DFS_INFO_3_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                            decoder.ReadStructDeferral<DFS_INFO_3_CONTAINER>(ref this.DfsInfo3Container.value);
                        }
                    }
                    else {
                        if ((((int)(this.Level)) == 4)) {
                            if ((null != this.DfsInfo4Container)) {
                                this.DfsInfo4Container.value = decoder.ReadFixedStruct<DFS_INFO_4_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                decoder.ReadStructDeferral<DFS_INFO_4_CONTAINER>(ref this.DfsInfo4Container.value);
                            }
                        }
                        else {
                            if ((((int)(this.Level)) == 5)) {
                                if ((null != this.DfsInfo5Container)) {
                                    this.DfsInfo5Container.value = decoder.ReadFixedStruct<DFS_INFO_5_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                    decoder.ReadStructDeferral<DFS_INFO_5_CONTAINER>(ref this.DfsInfo5Container.value);
                                }
                            }
                            else {
                                if ((((int)(this.Level)) == 6)) {
                                    if ((null != this.DfsInfo6Container)) {
                                        this.DfsInfo6Container.value = decoder.ReadFixedStruct<DFS_INFO_6_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                        decoder.ReadStructDeferral<DFS_INFO_6_CONTAINER>(ref this.DfsInfo6Container.value);
                                    }
                                }
                                else {
                                    if ((((int)(this.Level)) == 8)) {
                                        if ((null != this.DfsInfo8Container)) {
                                            this.DfsInfo8Container.value = decoder.ReadFixedStruct<DFS_INFO_8_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                            decoder.ReadStructDeferral<DFS_INFO_8_CONTAINER>(ref this.DfsInfo8Container.value);
                                        }
                                    }
                                    else {
                                        if ((((int)(this.Level)) == 9)) {
                                            if ((null != this.DfsInfo9Container)) {
                                                this.DfsInfo9Container.value = decoder.ReadFixedStruct<DFS_INFO_9_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                decoder.ReadStructDeferral<DFS_INFO_9_CONTAINER>(ref this.DfsInfo9Container.value);
                                            }
                                        }
                                        else {
                                            if ((((int)(this.Level)) == 200)) {
                                                if ((null != this.DfsInfo200Container)) {
                                                    this.DfsInfo200Container.value = decoder.ReadFixedStruct<DFS_INFO_200_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    decoder.ReadStructDeferral<DFS_INFO_200_CONTAINER>(ref this.DfsInfo200Container.value);
                                                }
                                            }
                                            else {
                                                if ((((int)(this.Level)) == 300)) {
                                                    if ((null != this.DfsInfo300Container)) {
                                                        this.DfsInfo300Container.value = decoder.ReadFixedStruct<DFS_INFO_300_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                        decoder.ReadStructDeferral<DFS_INFO_300_CONTAINER>(ref this.DfsInfo300Container.value);
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
        }
        public RpcPointer<DFS_INFO_1_CONTAINER> DfsInfo1Container;
        public RpcPointer<DFS_INFO_2_CONTAINER> DfsInfo2Container;
        public RpcPointer<DFS_INFO_3_CONTAINER> DfsInfo3Container;
        public RpcPointer<DFS_INFO_4_CONTAINER> DfsInfo4Container;
        public RpcPointer<DFS_INFO_5_CONTAINER> DfsInfo5Container;
        public RpcPointer<DFS_INFO_6_CONTAINER> DfsInfo6Container;
        public RpcPointer<DFS_INFO_8_CONTAINER> DfsInfo8Container;
        public RpcPointer<DFS_INFO_9_CONTAINER> DfsInfo9Container;
        public RpcPointer<DFS_INFO_200_CONTAINER> DfsInfo200Container;
        public RpcPointer<DFS_INFO_300_CONTAINER> DfsInfo300Container;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct DFS_INFO_ENUM_STRUCT : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Level);
            encoder.WriteUnion(this.DfsInfoContainer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Level = decoder.ReadUInt32();
            this.DfsInfoContainer = decoder.ReadUnion<Unnamed_1>();
        }
        public uint Level;
        public Unnamed_1 DfsInfoContainer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.DfsInfoContainer);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<Unnamed_1>(ref this.DfsInfoContainer);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("4fc742e0-4a10-11cf-8273-00aa004ae673")]
    [Titanis.DceRpc.RpcVersionAttribute(3, 0)]
    public interface netdfs {
        Task<uint> NetrDfsManagerGetVersion(System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsAdd(string DfsEntryPath, string ServerName, string ShareName, string Comment, uint Flags, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsRemove(string DfsEntryPath, string ServerName, string ShareName, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsSetInfo(string DfsEntryPath, string ServerName, string ShareName, uint Level, DFS_INFO_STRUCT DfsInfo, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsGetInfo(string DfsEntryPath, string ServerName, string ShareName, uint Level, RpcPointer<DFS_INFO_STRUCT> DfsInfo, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsEnum(uint Level, uint PrefMaxLen, RpcPointer<DFS_INFO_ENUM_STRUCT> DfsEnum, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsMove(string DfsEntryPath, string NewDfsEntryPath, uint Flags, System.Threading.CancellationToken cancellationToken);
        Task Opnum7NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum8NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum9NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsAddFtRoot(string ServerName, string DcName, string RootShare, string FtDfsName, string Comment, string ConfigDN, byte NewFtDfs, uint ApiFlags, RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsRemoveFtRoot(string ServerName, string DcName, string RootShare, string FtDfsName, uint ApiFlags, RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsAddStdRoot(string ServerName, string RootShare, string Comment, uint ApiFlags, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsRemoveStdRoot(string ServerName, string RootShare, uint ApiFlags, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsManagerInitialize(string ServerName, uint Flags, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsAddStdRootForced(string ServerName, string RootShare, string Comment, string Share, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsGetDcAddress(string ServerName, RpcPointer<RpcPointer<string>> DcName, RpcPointer<byte> IsRoot, RpcPointer<uint> Timeout, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsSetDcAddress(string ServerName, string DcName, uint Timeout, uint Flags, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsFlushFtTable(string DcName, string wszFtDfsName, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsAdd2(string DfsEntryPath, string DcName, string ServerName, string ShareName, string Comment, uint Flags, RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsRemove2(string DfsEntryPath, string DcName, string ServerName, string ShareName, RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsEnumEx(string DfsEntryPath, uint Level, uint PrefMaxLen, RpcPointer<DFS_INFO_ENUM_STRUCT> DfsEnum, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsSetInfo2(string DfsEntryPath, string DcName, string ServerName, string ShareName, uint Level, DFS_INFO_STRUCT pDfsInfo, RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsAddRootTarget(RpcPointer<char> pDfsPath, RpcPointer<char> pTargetPath, uint MajorVersion, RpcPointer<char> pComment, byte NewNamespace, uint Flags, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsRemoveRootTarget(RpcPointer<char> pDfsPath, RpcPointer<char> pTargetPath, uint Flags, System.Threading.CancellationToken cancellationToken);
        Task<uint> NetrDfsGetSupportedNamespaceVersion(DFS_NAMESPACE_VERSION_ORIGIN Origin, RpcPointer<char> pName, RpcPointer<DFS_SUPPORTED_NAMESPACE_VERSION_INFO> pVersionInfo, System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("4fc742e0-4a10-11cf-8273-00aa004ae673")]
    public class netdfsClientProxy : Titanis.DceRpc.Client.RpcClientProxy, netdfs, Titanis.DceRpc.IRpcClientProxy {
        private static System.Guid _interfaceUuid = new System.Guid("4fc742e0-4a10-11cf-8273-00aa004ae673");
        public override System.Guid InterfaceUuid {
            get {
                return _interfaceUuid;
            }
        }
        public override Titanis.DceRpc.RpcVersion InterfaceVersion {
            get {
                return new Titanis.DceRpc.RpcVersion(3, 0);
            }
        }
        public virtual async Task<uint> NetrDfsManagerGetVersion(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsAdd(string DfsEntryPath, string ServerName, string ShareName, string Comment, uint Flags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(DfsEntryPath);
            encoder.WriteWideCharString(ServerName);
            encoder.WriteUniqueReferentId((ShareName == null));
            if ((ShareName != null)) {
                encoder.WriteWideCharString(ShareName);
            }
            encoder.WriteUniqueReferentId((Comment == null));
            if ((Comment != null)) {
                encoder.WriteWideCharString(Comment);
            }
            encoder.WriteValue(Flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsRemove(string DfsEntryPath, string ServerName, string ShareName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(DfsEntryPath);
            encoder.WriteUniqueReferentId((ServerName == null));
            if ((ServerName != null)) {
                encoder.WriteWideCharString(ServerName);
            }
            encoder.WriteUniqueReferentId((ShareName == null));
            if ((ShareName != null)) {
                encoder.WriteWideCharString(ShareName);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsSetInfo(string DfsEntryPath, string ServerName, string ShareName, uint Level, DFS_INFO_STRUCT DfsInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(DfsEntryPath);
            encoder.WriteUniqueReferentId((ServerName == null));
            if ((ServerName != null)) {
                encoder.WriteWideCharString(ServerName);
            }
            encoder.WriteUniqueReferentId((ShareName == null));
            if ((ShareName != null)) {
                encoder.WriteWideCharString(ShareName);
            }
            encoder.WriteValue(Level);
            encoder.WriteUnion(DfsInfo);
            encoder.WriteStructDeferral(DfsInfo);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsGetInfo(string DfsEntryPath, string ServerName, string ShareName, uint Level, RpcPointer<DFS_INFO_STRUCT> DfsInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(DfsEntryPath);
            encoder.WriteUniqueReferentId((ServerName == null));
            if ((ServerName != null)) {
                encoder.WriteWideCharString(ServerName);
            }
            encoder.WriteUniqueReferentId((ShareName == null));
            if ((ShareName != null)) {
                encoder.WriteWideCharString(ShareName);
            }
            encoder.WriteValue(Level);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            DfsInfo.value = decoder.ReadUnion<DFS_INFO_STRUCT>();
            decoder.ReadStructDeferral<DFS_INFO_STRUCT>(ref DfsInfo.value);
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsEnum(uint Level, uint PrefMaxLen, RpcPointer<DFS_INFO_ENUM_STRUCT> DfsEnum, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(Level);
            encoder.WriteValue(PrefMaxLen);
            encoder.WritePointer(DfsEnum);
            if ((null != DfsEnum)) {
                encoder.WriteFixedStruct(DfsEnum.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(DfsEnum.value);
            }
            encoder.WritePointer(ResumeHandle);
            if ((null != ResumeHandle)) {
                encoder.WriteValue(ResumeHandle.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            DfsEnum = decoder.ReadOutUniquePointer<DFS_INFO_ENUM_STRUCT>(DfsEnum);
            if ((null != DfsEnum)) {
                DfsEnum.value = decoder.ReadFixedStruct<DFS_INFO_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<DFS_INFO_ENUM_STRUCT>(ref DfsEnum.value);
            }
            ResumeHandle = decoder.ReadOutUniquePointer<uint>(ResumeHandle);
            if ((null != ResumeHandle)) {
                ResumeHandle.value = decoder.ReadUInt32();
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsMove(string DfsEntryPath, string NewDfsEntryPath, uint Flags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(DfsEntryPath);
            encoder.WriteWideCharString(NewDfsEntryPath);
            encoder.WriteValue(Flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum7NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum8NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum9NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<uint> NetrDfsAddFtRoot(string ServerName, string DcName, string RootShare, string FtDfsName, string Comment, string ConfigDN, byte NewFtDfs, uint ApiFlags, RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(ServerName);
            encoder.WriteWideCharString(DcName);
            encoder.WriteWideCharString(RootShare);
            encoder.WriteWideCharString(FtDfsName);
            encoder.WriteWideCharString(Comment);
            encoder.WriteWideCharString(ConfigDN);
            encoder.WriteValue(NewFtDfs);
            encoder.WriteValue(ApiFlags);
            encoder.WritePointer(ppRootList);
            if ((null != ppRootList)) {
                encoder.WritePointer(ppRootList.value);
                if ((null != ppRootList.value)) {
                    encoder.WriteConformantStruct(ppRootList.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppRootList.value.value);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppRootList = decoder.ReadOutUniquePointer<RpcPointer<DFSM_ROOT_LIST>>(ppRootList);
            if ((null != ppRootList)) {
                ppRootList.value = decoder.ReadUniquePointer<DFSM_ROOT_LIST>();
                if ((null != ppRootList.value)) {
                    ppRootList.value.value = decoder.ReadConformantStruct<DFSM_ROOT_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFSM_ROOT_LIST>(ref ppRootList.value.value);
                }
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsRemoveFtRoot(string ServerName, string DcName, string RootShare, string FtDfsName, uint ApiFlags, RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(ServerName);
            encoder.WriteWideCharString(DcName);
            encoder.WriteWideCharString(RootShare);
            encoder.WriteWideCharString(FtDfsName);
            encoder.WriteValue(ApiFlags);
            encoder.WritePointer(ppRootList);
            if ((null != ppRootList)) {
                encoder.WritePointer(ppRootList.value);
                if ((null != ppRootList.value)) {
                    encoder.WriteConformantStruct(ppRootList.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppRootList.value.value);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppRootList = decoder.ReadOutUniquePointer<RpcPointer<DFSM_ROOT_LIST>>(ppRootList);
            if ((null != ppRootList)) {
                ppRootList.value = decoder.ReadUniquePointer<DFSM_ROOT_LIST>();
                if ((null != ppRootList.value)) {
                    ppRootList.value.value = decoder.ReadConformantStruct<DFSM_ROOT_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFSM_ROOT_LIST>(ref ppRootList.value.value);
                }
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsAddStdRoot(string ServerName, string RootShare, string Comment, uint ApiFlags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(ServerName);
            encoder.WriteWideCharString(RootShare);
            encoder.WriteWideCharString(Comment);
            encoder.WriteValue(ApiFlags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsRemoveStdRoot(string ServerName, string RootShare, uint ApiFlags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(ServerName);
            encoder.WriteWideCharString(RootShare);
            encoder.WriteValue(ApiFlags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsManagerInitialize(string ServerName, uint Flags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(ServerName);
            encoder.WriteValue(Flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsAddStdRootForced(string ServerName, string RootShare, string Comment, string Share, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(ServerName);
            encoder.WriteWideCharString(RootShare);
            encoder.WriteWideCharString(Comment);
            encoder.WriteWideCharString(Share);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsGetDcAddress(string ServerName, RpcPointer<RpcPointer<string>> DcName, RpcPointer<byte> IsRoot, RpcPointer<uint> Timeout, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(ServerName);
            encoder.WritePointer(DcName.value);
            if ((null != DcName.value)) {
                encoder.WriteWideCharString(DcName.value.value);
            }
            encoder.WriteValue(IsRoot.value);
            encoder.WriteValue(Timeout.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            DcName.value = decoder.ReadOutUniquePointer<string>(DcName.value);
            if ((null != DcName.value)) {
                DcName.value.value = decoder.ReadWideCharString();
            }
            IsRoot.value = decoder.ReadUnsignedChar();
            Timeout.value = decoder.ReadUInt32();
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsSetDcAddress(string ServerName, string DcName, uint Timeout, uint Flags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(ServerName);
            encoder.WriteWideCharString(DcName);
            encoder.WriteValue(Timeout);
            encoder.WriteValue(Flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsFlushFtTable(string DcName, string wszFtDfsName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(DcName);
            encoder.WriteWideCharString(wszFtDfsName);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsAdd2(string DfsEntryPath, string DcName, string ServerName, string ShareName, string Comment, uint Flags, RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(DfsEntryPath);
            encoder.WriteWideCharString(DcName);
            encoder.WriteWideCharString(ServerName);
            encoder.WriteUniqueReferentId((ShareName == null));
            if ((ShareName != null)) {
                encoder.WriteWideCharString(ShareName);
            }
            encoder.WriteUniqueReferentId((Comment == null));
            if ((Comment != null)) {
                encoder.WriteWideCharString(Comment);
            }
            encoder.WriteValue(Flags);
            encoder.WritePointer(ppRootList);
            if ((null != ppRootList)) {
                encoder.WritePointer(ppRootList.value);
                if ((null != ppRootList.value)) {
                    encoder.WriteConformantStruct(ppRootList.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppRootList.value.value);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppRootList = decoder.ReadOutUniquePointer<RpcPointer<DFSM_ROOT_LIST>>(ppRootList);
            if ((null != ppRootList)) {
                ppRootList.value = decoder.ReadUniquePointer<DFSM_ROOT_LIST>();
                if ((null != ppRootList.value)) {
                    ppRootList.value.value = decoder.ReadConformantStruct<DFSM_ROOT_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFSM_ROOT_LIST>(ref ppRootList.value.value);
                }
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsRemove2(string DfsEntryPath, string DcName, string ServerName, string ShareName, RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(DfsEntryPath);
            encoder.WriteWideCharString(DcName);
            encoder.WriteUniqueReferentId((ServerName == null));
            if ((ServerName != null)) {
                encoder.WriteWideCharString(ServerName);
            }
            encoder.WriteUniqueReferentId((ShareName == null));
            if ((ShareName != null)) {
                encoder.WriteWideCharString(ShareName);
            }
            encoder.WritePointer(ppRootList);
            if ((null != ppRootList)) {
                encoder.WritePointer(ppRootList.value);
                if ((null != ppRootList.value)) {
                    encoder.WriteConformantStruct(ppRootList.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppRootList.value.value);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppRootList = decoder.ReadOutUniquePointer<RpcPointer<DFSM_ROOT_LIST>>(ppRootList);
            if ((null != ppRootList)) {
                ppRootList.value = decoder.ReadUniquePointer<DFSM_ROOT_LIST>();
                if ((null != ppRootList.value)) {
                    ppRootList.value.value = decoder.ReadConformantStruct<DFSM_ROOT_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFSM_ROOT_LIST>(ref ppRootList.value.value);
                }
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsEnumEx(string DfsEntryPath, uint Level, uint PrefMaxLen, RpcPointer<DFS_INFO_ENUM_STRUCT> DfsEnum, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(DfsEntryPath);
            encoder.WriteValue(Level);
            encoder.WriteValue(PrefMaxLen);
            encoder.WritePointer(DfsEnum);
            if ((null != DfsEnum)) {
                encoder.WriteFixedStruct(DfsEnum.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(DfsEnum.value);
            }
            encoder.WritePointer(ResumeHandle);
            if ((null != ResumeHandle)) {
                encoder.WriteValue(ResumeHandle.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            DfsEnum = decoder.ReadOutUniquePointer<DFS_INFO_ENUM_STRUCT>(DfsEnum);
            if ((null != DfsEnum)) {
                DfsEnum.value = decoder.ReadFixedStruct<DFS_INFO_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<DFS_INFO_ENUM_STRUCT>(ref DfsEnum.value);
            }
            ResumeHandle = decoder.ReadOutUniquePointer<uint>(ResumeHandle);
            if ((null != ResumeHandle)) {
                ResumeHandle.value = decoder.ReadUInt32();
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsSetInfo2(string DfsEntryPath, string DcName, string ServerName, string ShareName, uint Level, DFS_INFO_STRUCT pDfsInfo, RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(DfsEntryPath);
            encoder.WriteWideCharString(DcName);
            encoder.WriteUniqueReferentId((ServerName == null));
            if ((ServerName != null)) {
                encoder.WriteWideCharString(ServerName);
            }
            encoder.WriteUniqueReferentId((ShareName == null));
            if ((ShareName != null)) {
                encoder.WriteWideCharString(ShareName);
            }
            encoder.WriteValue(Level);
            encoder.WriteUnion(pDfsInfo);
            encoder.WriteStructDeferral(pDfsInfo);
            encoder.WritePointer(ppRootList);
            if ((null != ppRootList)) {
                encoder.WritePointer(ppRootList.value);
                if ((null != ppRootList.value)) {
                    encoder.WriteConformantStruct(ppRootList.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppRootList.value.value);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppRootList = decoder.ReadOutUniquePointer<RpcPointer<DFSM_ROOT_LIST>>(ppRootList);
            if ((null != ppRootList)) {
                ppRootList.value = decoder.ReadUniquePointer<DFSM_ROOT_LIST>();
                if ((null != ppRootList.value)) {
                    ppRootList.value.value = decoder.ReadConformantStruct<DFSM_ROOT_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFSM_ROOT_LIST>(ref ppRootList.value.value);
                }
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsAddRootTarget(RpcPointer<char> pDfsPath, RpcPointer<char> pTargetPath, uint MajorVersion, RpcPointer<char> pComment, byte NewNamespace, uint Flags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(pDfsPath);
            if ((null != pDfsPath)) {
                encoder.WriteValue(pDfsPath.value);
            }
            encoder.WritePointer(pTargetPath);
            if ((null != pTargetPath)) {
                encoder.WriteValue(pTargetPath.value);
            }
            encoder.WriteValue(MajorVersion);
            encoder.WritePointer(pComment);
            if ((null != pComment)) {
                encoder.WriteValue(pComment.value);
            }
            encoder.WriteValue(NewNamespace);
            encoder.WriteValue(Flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsRemoveRootTarget(RpcPointer<char> pDfsPath, RpcPointer<char> pTargetPath, uint Flags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(pDfsPath);
            if ((null != pDfsPath)) {
                encoder.WriteValue(pDfsPath.value);
            }
            encoder.WritePointer(pTargetPath);
            if ((null != pTargetPath)) {
                encoder.WriteValue(pTargetPath.value);
            }
            encoder.WriteValue(Flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> NetrDfsGetSupportedNamespaceVersion(DFS_NAMESPACE_VERSION_ORIGIN Origin, RpcPointer<char> pName, RpcPointer<DFS_SUPPORTED_NAMESPACE_VERSION_INFO> pVersionInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(((short)(Origin)));
            encoder.WritePointer(pName);
            if ((null != pName)) {
                encoder.WriteValue(pName.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            pVersionInfo.value = decoder.ReadFixedStruct<DFS_SUPPORTED_NAMESPACE_VERSION_INFO>(Titanis.DceRpc.NdrAlignment._8Byte);
            decoder.ReadStructDeferral<DFS_SUPPORTED_NAMESPACE_VERSION_INFO>(ref pVersionInfo.value);
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class netdfsStub : Titanis.DceRpc.Server.RpcServiceStub {
        private static System.Guid _interfaceUuid = new System.Guid("4fc742e0-4a10-11cf-8273-00aa004ae673");
        public override System.Guid InterfaceUuid {
            get {
                return _interfaceUuid;
            }
        }
        public override Titanis.DceRpc.RpcVersion InterfaceVersion {
            get {
                return new Titanis.DceRpc.RpcVersion(3, 0);
            }
        }
        private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
        public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable {
            get {
                return this._dispatchTable;
            }
        }
        private netdfs _obj;
        public netdfsStub(netdfs obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_NetrDfsManagerGetVersion,
                    this.Invoke_NetrDfsAdd,
                    this.Invoke_NetrDfsRemove,
                    this.Invoke_NetrDfsSetInfo,
                    this.Invoke_NetrDfsGetInfo,
                    this.Invoke_NetrDfsEnum,
                    this.Invoke_NetrDfsMove,
                    this.Invoke_Opnum7NotUsedOnWire,
                    this.Invoke_Opnum8NotUsedOnWire,
                    this.Invoke_Opnum9NotUsedOnWire,
                    this.Invoke_NetrDfsAddFtRoot,
                    this.Invoke_NetrDfsRemoveFtRoot,
                    this.Invoke_NetrDfsAddStdRoot,
                    this.Invoke_NetrDfsRemoveStdRoot,
                    this.Invoke_NetrDfsManagerInitialize,
                    this.Invoke_NetrDfsAddStdRootForced,
                    this.Invoke_NetrDfsGetDcAddress,
                    this.Invoke_NetrDfsSetDcAddress,
                    this.Invoke_NetrDfsFlushFtTable,
                    this.Invoke_NetrDfsAdd2,
                    this.Invoke_NetrDfsRemove2,
                    this.Invoke_NetrDfsEnumEx,
                    this.Invoke_NetrDfsSetInfo2,
                    this.Invoke_NetrDfsAddRootTarget,
                    this.Invoke_NetrDfsRemoveRootTarget,
                    this.Invoke_NetrDfsGetSupportedNamespaceVersion};
        }
        private async Task Invoke_NetrDfsManagerGetVersion(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.NetrDfsManagerGetVersion(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsAdd(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DfsEntryPath;
            string ServerName;
            string ShareName;
            string Comment;
            uint Flags;
            DfsEntryPath = decoder.ReadWideCharString();
            ServerName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                ShareName = null;
            }
            else {
                ShareName = decoder.ReadWideCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                Comment = null;
            }
            else {
                Comment = decoder.ReadWideCharString();
            }
            Flags = decoder.ReadUInt32();
            var invokeTask = this._obj.NetrDfsAdd(DfsEntryPath, ServerName, ShareName, Comment, Flags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsRemove(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DfsEntryPath;
            string ServerName;
            string ShareName;
            DfsEntryPath = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                ServerName = null;
            }
            else {
                ServerName = decoder.ReadWideCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                ShareName = null;
            }
            else {
                ShareName = decoder.ReadWideCharString();
            }
            var invokeTask = this._obj.NetrDfsRemove(DfsEntryPath, ServerName, ShareName, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsSetInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DfsEntryPath;
            string ServerName;
            string ShareName;
            uint Level;
            DFS_INFO_STRUCT DfsInfo;
            DfsEntryPath = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                ServerName = null;
            }
            else {
                ServerName = decoder.ReadWideCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                ShareName = null;
            }
            else {
                ShareName = decoder.ReadWideCharString();
            }
            Level = decoder.ReadUInt32();
            DfsInfo = decoder.ReadUnion<DFS_INFO_STRUCT>();
            decoder.ReadStructDeferral<DFS_INFO_STRUCT>(ref DfsInfo);
            var invokeTask = this._obj.NetrDfsSetInfo(DfsEntryPath, ServerName, ShareName, Level, DfsInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsGetInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DfsEntryPath;
            string ServerName;
            string ShareName;
            uint Level;
            RpcPointer<DFS_INFO_STRUCT> DfsInfo = new RpcPointer<DFS_INFO_STRUCT>();
            DfsEntryPath = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                ServerName = null;
            }
            else {
                ServerName = decoder.ReadWideCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                ShareName = null;
            }
            else {
                ShareName = decoder.ReadWideCharString();
            }
            Level = decoder.ReadUInt32();
            var invokeTask = this._obj.NetrDfsGetInfo(DfsEntryPath, ServerName, ShareName, Level, DfsInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteUnion(DfsInfo.value);
            encoder.WriteStructDeferral(DfsInfo.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint Level;
            uint PrefMaxLen;
            RpcPointer<DFS_INFO_ENUM_STRUCT> DfsEnum;
            RpcPointer<uint> ResumeHandle;
            Level = decoder.ReadUInt32();
            PrefMaxLen = decoder.ReadUInt32();
            DfsEnum = decoder.ReadUniquePointer<DFS_INFO_ENUM_STRUCT>();
            if ((null != DfsEnum)) {
                DfsEnum.value = decoder.ReadFixedStruct<DFS_INFO_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<DFS_INFO_ENUM_STRUCT>(ref DfsEnum.value);
            }
            ResumeHandle = decoder.ReadUniquePointer<uint>();
            if ((null != ResumeHandle)) {
                ResumeHandle.value = decoder.ReadUInt32();
            }
            var invokeTask = this._obj.NetrDfsEnum(Level, PrefMaxLen, DfsEnum, ResumeHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(DfsEnum);
            if ((null != DfsEnum)) {
                encoder.WriteFixedStruct(DfsEnum.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(DfsEnum.value);
            }
            encoder.WritePointer(ResumeHandle);
            if ((null != ResumeHandle)) {
                encoder.WriteValue(ResumeHandle.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsMove(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DfsEntryPath;
            string NewDfsEntryPath;
            uint Flags;
            DfsEntryPath = decoder.ReadWideCharString();
            NewDfsEntryPath = decoder.ReadWideCharString();
            Flags = decoder.ReadUInt32();
            var invokeTask = this._obj.NetrDfsMove(DfsEntryPath, NewDfsEntryPath, Flags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum7NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum7NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum8NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum8NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum9NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum9NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_NetrDfsAddFtRoot(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            string DcName;
            string RootShare;
            string FtDfsName;
            string Comment;
            string ConfigDN;
            byte NewFtDfs;
            uint ApiFlags;
            RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList;
            ServerName = decoder.ReadWideCharString();
            DcName = decoder.ReadWideCharString();
            RootShare = decoder.ReadWideCharString();
            FtDfsName = decoder.ReadWideCharString();
            Comment = decoder.ReadWideCharString();
            ConfigDN = decoder.ReadWideCharString();
            NewFtDfs = decoder.ReadUnsignedChar();
            ApiFlags = decoder.ReadUInt32();
            ppRootList = decoder.ReadUniquePointer<RpcPointer<DFSM_ROOT_LIST>>();
            if ((null != ppRootList)) {
                ppRootList.value = decoder.ReadUniquePointer<DFSM_ROOT_LIST>();
                if ((null != ppRootList.value)) {
                    ppRootList.value.value = decoder.ReadConformantStruct<DFSM_ROOT_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFSM_ROOT_LIST>(ref ppRootList.value.value);
                }
            }
            var invokeTask = this._obj.NetrDfsAddFtRoot(ServerName, DcName, RootShare, FtDfsName, Comment, ConfigDN, NewFtDfs, ApiFlags, ppRootList, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppRootList);
            if ((null != ppRootList)) {
                encoder.WritePointer(ppRootList.value);
                if ((null != ppRootList.value)) {
                    encoder.WriteConformantStruct(ppRootList.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppRootList.value.value);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsRemoveFtRoot(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            string DcName;
            string RootShare;
            string FtDfsName;
            uint ApiFlags;
            RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList;
            ServerName = decoder.ReadWideCharString();
            DcName = decoder.ReadWideCharString();
            RootShare = decoder.ReadWideCharString();
            FtDfsName = decoder.ReadWideCharString();
            ApiFlags = decoder.ReadUInt32();
            ppRootList = decoder.ReadUniquePointer<RpcPointer<DFSM_ROOT_LIST>>();
            if ((null != ppRootList)) {
                ppRootList.value = decoder.ReadUniquePointer<DFSM_ROOT_LIST>();
                if ((null != ppRootList.value)) {
                    ppRootList.value.value = decoder.ReadConformantStruct<DFSM_ROOT_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFSM_ROOT_LIST>(ref ppRootList.value.value);
                }
            }
            var invokeTask = this._obj.NetrDfsRemoveFtRoot(ServerName, DcName, RootShare, FtDfsName, ApiFlags, ppRootList, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppRootList);
            if ((null != ppRootList)) {
                encoder.WritePointer(ppRootList.value);
                if ((null != ppRootList.value)) {
                    encoder.WriteConformantStruct(ppRootList.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppRootList.value.value);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsAddStdRoot(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            string RootShare;
            string Comment;
            uint ApiFlags;
            ServerName = decoder.ReadWideCharString();
            RootShare = decoder.ReadWideCharString();
            Comment = decoder.ReadWideCharString();
            ApiFlags = decoder.ReadUInt32();
            var invokeTask = this._obj.NetrDfsAddStdRoot(ServerName, RootShare, Comment, ApiFlags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsRemoveStdRoot(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            string RootShare;
            uint ApiFlags;
            ServerName = decoder.ReadWideCharString();
            RootShare = decoder.ReadWideCharString();
            ApiFlags = decoder.ReadUInt32();
            var invokeTask = this._obj.NetrDfsRemoveStdRoot(ServerName, RootShare, ApiFlags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsManagerInitialize(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            uint Flags;
            ServerName = decoder.ReadWideCharString();
            Flags = decoder.ReadUInt32();
            var invokeTask = this._obj.NetrDfsManagerInitialize(ServerName, Flags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsAddStdRootForced(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            string RootShare;
            string Comment;
            string Share;
            ServerName = decoder.ReadWideCharString();
            RootShare = decoder.ReadWideCharString();
            Comment = decoder.ReadWideCharString();
            Share = decoder.ReadWideCharString();
            var invokeTask = this._obj.NetrDfsAddStdRootForced(ServerName, RootShare, Comment, Share, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsGetDcAddress(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            RpcPointer<RpcPointer<string>> DcName;
            RpcPointer<byte> IsRoot;
            RpcPointer<uint> Timeout;
            ServerName = decoder.ReadWideCharString();
            DcName = new RpcPointer<RpcPointer<string>>();
            DcName.value = decoder.ReadUniquePointer<string>();
            if ((null != DcName.value)) {
                DcName.value.value = decoder.ReadWideCharString();
            }
            IsRoot = new RpcPointer<byte>();
            IsRoot.value = decoder.ReadUnsignedChar();
            Timeout = new RpcPointer<uint>();
            Timeout.value = decoder.ReadUInt32();
            var invokeTask = this._obj.NetrDfsGetDcAddress(ServerName, DcName, IsRoot, Timeout, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(DcName.value);
            if ((null != DcName.value)) {
                encoder.WriteWideCharString(DcName.value.value);
            }
            encoder.WriteValue(IsRoot.value);
            encoder.WriteValue(Timeout.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsSetDcAddress(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            string DcName;
            uint Timeout;
            uint Flags;
            ServerName = decoder.ReadWideCharString();
            DcName = decoder.ReadWideCharString();
            Timeout = decoder.ReadUInt32();
            Flags = decoder.ReadUInt32();
            var invokeTask = this._obj.NetrDfsSetDcAddress(ServerName, DcName, Timeout, Flags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsFlushFtTable(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DcName;
            string wszFtDfsName;
            DcName = decoder.ReadWideCharString();
            wszFtDfsName = decoder.ReadWideCharString();
            var invokeTask = this._obj.NetrDfsFlushFtTable(DcName, wszFtDfsName, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsAdd2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DfsEntryPath;
            string DcName;
            string ServerName;
            string ShareName;
            string Comment;
            uint Flags;
            RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList;
            DfsEntryPath = decoder.ReadWideCharString();
            DcName = decoder.ReadWideCharString();
            ServerName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                ShareName = null;
            }
            else {
                ShareName = decoder.ReadWideCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                Comment = null;
            }
            else {
                Comment = decoder.ReadWideCharString();
            }
            Flags = decoder.ReadUInt32();
            ppRootList = decoder.ReadUniquePointer<RpcPointer<DFSM_ROOT_LIST>>();
            if ((null != ppRootList)) {
                ppRootList.value = decoder.ReadUniquePointer<DFSM_ROOT_LIST>();
                if ((null != ppRootList.value)) {
                    ppRootList.value.value = decoder.ReadConformantStruct<DFSM_ROOT_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFSM_ROOT_LIST>(ref ppRootList.value.value);
                }
            }
            var invokeTask = this._obj.NetrDfsAdd2(DfsEntryPath, DcName, ServerName, ShareName, Comment, Flags, ppRootList, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppRootList);
            if ((null != ppRootList)) {
                encoder.WritePointer(ppRootList.value);
                if ((null != ppRootList.value)) {
                    encoder.WriteConformantStruct(ppRootList.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppRootList.value.value);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsRemove2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DfsEntryPath;
            string DcName;
            string ServerName;
            string ShareName;
            RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList;
            DfsEntryPath = decoder.ReadWideCharString();
            DcName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                ServerName = null;
            }
            else {
                ServerName = decoder.ReadWideCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                ShareName = null;
            }
            else {
                ShareName = decoder.ReadWideCharString();
            }
            ppRootList = decoder.ReadUniquePointer<RpcPointer<DFSM_ROOT_LIST>>();
            if ((null != ppRootList)) {
                ppRootList.value = decoder.ReadUniquePointer<DFSM_ROOT_LIST>();
                if ((null != ppRootList.value)) {
                    ppRootList.value.value = decoder.ReadConformantStruct<DFSM_ROOT_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFSM_ROOT_LIST>(ref ppRootList.value.value);
                }
            }
            var invokeTask = this._obj.NetrDfsRemove2(DfsEntryPath, DcName, ServerName, ShareName, ppRootList, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppRootList);
            if ((null != ppRootList)) {
                encoder.WritePointer(ppRootList.value);
                if ((null != ppRootList.value)) {
                    encoder.WriteConformantStruct(ppRootList.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppRootList.value.value);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsEnumEx(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DfsEntryPath;
            uint Level;
            uint PrefMaxLen;
            RpcPointer<DFS_INFO_ENUM_STRUCT> DfsEnum;
            RpcPointer<uint> ResumeHandle;
            DfsEntryPath = decoder.ReadWideCharString();
            Level = decoder.ReadUInt32();
            PrefMaxLen = decoder.ReadUInt32();
            DfsEnum = decoder.ReadUniquePointer<DFS_INFO_ENUM_STRUCT>();
            if ((null != DfsEnum)) {
                DfsEnum.value = decoder.ReadFixedStruct<DFS_INFO_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<DFS_INFO_ENUM_STRUCT>(ref DfsEnum.value);
            }
            ResumeHandle = decoder.ReadUniquePointer<uint>();
            if ((null != ResumeHandle)) {
                ResumeHandle.value = decoder.ReadUInt32();
            }
            var invokeTask = this._obj.NetrDfsEnumEx(DfsEntryPath, Level, PrefMaxLen, DfsEnum, ResumeHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(DfsEnum);
            if ((null != DfsEnum)) {
                encoder.WriteFixedStruct(DfsEnum.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(DfsEnum.value);
            }
            encoder.WritePointer(ResumeHandle);
            if ((null != ResumeHandle)) {
                encoder.WriteValue(ResumeHandle.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsSetInfo2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string DfsEntryPath;
            string DcName;
            string ServerName;
            string ShareName;
            uint Level;
            DFS_INFO_STRUCT pDfsInfo;
            RpcPointer<RpcPointer<DFSM_ROOT_LIST>> ppRootList;
            DfsEntryPath = decoder.ReadWideCharString();
            DcName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                ServerName = null;
            }
            else {
                ServerName = decoder.ReadWideCharString();
            }
            if ((decoder.ReadReferentId() == 0)) {
                ShareName = null;
            }
            else {
                ShareName = decoder.ReadWideCharString();
            }
            Level = decoder.ReadUInt32();
            pDfsInfo = decoder.ReadUnion<DFS_INFO_STRUCT>();
            decoder.ReadStructDeferral<DFS_INFO_STRUCT>(ref pDfsInfo);
            ppRootList = decoder.ReadUniquePointer<RpcPointer<DFSM_ROOT_LIST>>();
            if ((null != ppRootList)) {
                ppRootList.value = decoder.ReadUniquePointer<DFSM_ROOT_LIST>();
                if ((null != ppRootList.value)) {
                    ppRootList.value.value = decoder.ReadConformantStruct<DFSM_ROOT_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<DFSM_ROOT_LIST>(ref ppRootList.value.value);
                }
            }
            var invokeTask = this._obj.NetrDfsSetInfo2(DfsEntryPath, DcName, ServerName, ShareName, Level, pDfsInfo, ppRootList, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppRootList);
            if ((null != ppRootList)) {
                encoder.WritePointer(ppRootList.value);
                if ((null != ppRootList.value)) {
                    encoder.WriteConformantStruct(ppRootList.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppRootList.value.value);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsAddRootTarget(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<char> pDfsPath;
            RpcPointer<char> pTargetPath;
            uint MajorVersion;
            RpcPointer<char> pComment;
            byte NewNamespace;
            uint Flags;
            pDfsPath = decoder.ReadUniquePointer<char>();
            if ((null != pDfsPath)) {
                pDfsPath.value = decoder.ReadWideChar();
            }
            pTargetPath = decoder.ReadUniquePointer<char>();
            if ((null != pTargetPath)) {
                pTargetPath.value = decoder.ReadWideChar();
            }
            MajorVersion = decoder.ReadUInt32();
            pComment = decoder.ReadUniquePointer<char>();
            if ((null != pComment)) {
                pComment.value = decoder.ReadWideChar();
            }
            NewNamespace = decoder.ReadUnsignedChar();
            Flags = decoder.ReadUInt32();
            var invokeTask = this._obj.NetrDfsAddRootTarget(pDfsPath, pTargetPath, MajorVersion, pComment, NewNamespace, Flags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsRemoveRootTarget(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<char> pDfsPath;
            RpcPointer<char> pTargetPath;
            uint Flags;
            pDfsPath = decoder.ReadUniquePointer<char>();
            if ((null != pDfsPath)) {
                pDfsPath.value = decoder.ReadWideChar();
            }
            pTargetPath = decoder.ReadUniquePointer<char>();
            if ((null != pTargetPath)) {
                pTargetPath.value = decoder.ReadWideChar();
            }
            Flags = decoder.ReadUInt32();
            var invokeTask = this._obj.NetrDfsRemoveRootTarget(pDfsPath, pTargetPath, Flags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_NetrDfsGetSupportedNamespaceVersion(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            DFS_NAMESPACE_VERSION_ORIGIN Origin;
            RpcPointer<char> pName;
            RpcPointer<DFS_SUPPORTED_NAMESPACE_VERSION_INFO> pVersionInfo = new RpcPointer<DFS_SUPPORTED_NAMESPACE_VERSION_INFO>();
            Origin = ((DFS_NAMESPACE_VERSION_ORIGIN)(decoder.ReadInt16()));
            pName = decoder.ReadUniquePointer<char>();
            if ((null != pName)) {
                pName.value = decoder.ReadWideChar();
            }
            var invokeTask = this._obj.NetrDfsGetSupportedNamespaceVersion(Origin, pName, pVersionInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(pVersionInfo.value, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteStructDeferral(pVersionInfo.value);
            encoder.WriteValue(retval);
        }
    }
}
