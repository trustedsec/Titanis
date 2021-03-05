#pragma warning disable

namespace ms_efsr {
    using System;
    using System.Threading.Tasks;
    using Titanis;
    using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct EFS_RPC_BLOB : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cbData);
            encoder.WritePointer(this.bData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cbData = decoder.ReadUInt32();
            this.bData = decoder.ReadUniquePointer<byte[]>();
        }
        public uint cbData;
        public RpcPointer<byte[]> bData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.bData)) {
                encoder.WriteArrayHeader(this.bData.value);
                for (int i = 0; (i < this.bData.value.Length); i++
                ) {
                    byte elem_0 = this.bData.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.bData)) {
                this.bData.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.bData.value.Length); i++
                ) {
                    byte elem_0 = this.bData.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.bData.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct EFS_COMPATIBILITY_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EfsVersion);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EfsVersion = decoder.ReadUInt32();
        }
        public uint EfsVersion;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct EFS_HASH_BLOB : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cbData);
            encoder.WritePointer(this.bData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cbData = decoder.ReadUInt32();
            this.bData = decoder.ReadUniquePointer<byte[]>();
        }
        public uint cbData;
        public RpcPointer<byte[]> bData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.bData)) {
                encoder.WriteArrayHeader(this.bData.value);
                for (int i = 0; (i < this.bData.value.Length); i++
                ) {
                    byte elem_0 = this.bData.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.bData)) {
                this.bData.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.bData.value.Length); i++
                ) {
                    byte elem_0 = this.bData.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.bData.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENCRYPTION_CERTIFICATE_HASH : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cbTotalLength);
            encoder.WritePointer(this.UserSid);
            encoder.WritePointer(this.Hash);
            encoder.WritePointer(this.lpDisplayInformation);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cbTotalLength = decoder.ReadUInt32();
            this.UserSid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            this.Hash = decoder.ReadUniquePointer<EFS_HASH_BLOB>();
            this.lpDisplayInformation = decoder.ReadUniquePointer<string>();
        }
        public uint cbTotalLength;
        public RpcPointer<ms_dtyp.RPC_SID> UserSid;
        public RpcPointer<EFS_HASH_BLOB> Hash;
        public RpcPointer<string> lpDisplayInformation;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.UserSid)) {
                encoder.WriteConformantStruct(this.UserSid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.UserSid.value);
            }
            if ((null != this.Hash)) {
                encoder.WriteFixedStruct(this.Hash.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.Hash.value);
            }
            if ((null != this.lpDisplayInformation)) {
                encoder.WriteWideCharString(this.lpDisplayInformation.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.UserSid)) {
                this.UserSid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.UserSid.value);
            }
            if ((null != this.Hash)) {
                this.Hash.value = decoder.ReadFixedStruct<EFS_HASH_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_HASH_BLOB>(ref this.Hash.value);
            }
            if ((null != this.lpDisplayInformation)) {
                this.lpDisplayInformation.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENCRYPTION_CERTIFICATE_HASH_LIST : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.nCert_Hash);
            encoder.WritePointer(this.Users);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.nCert_Hash = decoder.ReadUInt32();
            this.Users = decoder.ReadUniquePointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH>[]>();
        }
        public uint nCert_Hash;
        public RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH>[]> Users;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Users)) {
                encoder.WriteArrayHeader(this.Users.value);
                for (int i = 0; (i < this.Users.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_CERTIFICATE_HASH> elem_0 = this.Users.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < this.Users.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_CERTIFICATE_HASH> elem_0 = this.Users.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                        encoder.WriteStructDeferral(elem_0.value);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Users)) {
                this.Users.value = decoder.ReadArrayHeader<RpcPointer<ENCRYPTION_CERTIFICATE_HASH>>();
                for (int i = 0; (i < this.Users.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_CERTIFICATE_HASH> elem_0 = this.Users.value[i];
                    elem_0 = decoder.ReadUniquePointer<ENCRYPTION_CERTIFICATE_HASH>();
                    this.Users.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Users.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_CERTIFICATE_HASH> elem_0 = this.Users.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadFixedStruct<ENCRYPTION_CERTIFICATE_HASH>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        decoder.ReadStructDeferral<ENCRYPTION_CERTIFICATE_HASH>(ref elem_0.value);
                    }
                    this.Users.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct EFS_CERTIFICATE_BLOB : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwCertEncodingType);
            encoder.WriteValue(this.cbData);
            encoder.WritePointer(this.bData);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwCertEncodingType = decoder.ReadUInt32();
            this.cbData = decoder.ReadUInt32();
            this.bData = decoder.ReadUniquePointer<byte[]>();
        }
        public uint dwCertEncodingType;
        public uint cbData;
        public RpcPointer<byte[]> bData;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.bData)) {
                encoder.WriteArrayHeader(this.bData.value);
                for (int i = 0; (i < this.bData.value.Length); i++
                ) {
                    byte elem_0 = this.bData.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.bData)) {
                this.bData.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.bData.value.Length); i++
                ) {
                    byte elem_0 = this.bData.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.bData.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENCRYPTION_CERTIFICATE : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cbTotalLength);
            encoder.WritePointer(this.UserSid);
            encoder.WritePointer(this.CertBlob);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cbTotalLength = decoder.ReadUInt32();
            this.UserSid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            this.CertBlob = decoder.ReadUniquePointer<EFS_CERTIFICATE_BLOB>();
        }
        public uint cbTotalLength;
        public RpcPointer<ms_dtyp.RPC_SID> UserSid;
        public RpcPointer<EFS_CERTIFICATE_BLOB> CertBlob;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.UserSid)) {
                encoder.WriteConformantStruct(this.UserSid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.UserSid.value);
            }
            if ((null != this.CertBlob)) {
                encoder.WriteFixedStruct(this.CertBlob.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.CertBlob.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.UserSid)) {
                this.UserSid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.UserSid.value);
            }
            if ((null != this.CertBlob)) {
                this.CertBlob.value = decoder.ReadFixedStruct<EFS_CERTIFICATE_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_CERTIFICATE_BLOB>(ref this.CertBlob.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENCRYPTION_CERTIFICATE_LIST : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.nUsers);
            encoder.WritePointer(this.Users);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.nUsers = decoder.ReadUInt32();
            this.Users = decoder.ReadUniquePointer<RpcPointer<ENCRYPTION_CERTIFICATE>[]>();
        }
        public uint nUsers;
        public RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE>[]> Users;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Users)) {
                encoder.WriteArrayHeader(this.Users.value);
                for (int i = 0; (i < this.Users.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_CERTIFICATE> elem_0 = this.Users.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < this.Users.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_CERTIFICATE> elem_0 = this.Users.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                        encoder.WriteStructDeferral(elem_0.value);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Users)) {
                this.Users.value = decoder.ReadArrayHeader<RpcPointer<ENCRYPTION_CERTIFICATE>>();
                for (int i = 0; (i < this.Users.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_CERTIFICATE> elem_0 = this.Users.value[i];
                    elem_0 = decoder.ReadUniquePointer<ENCRYPTION_CERTIFICATE>();
                    this.Users.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Users.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_CERTIFICATE> elem_0 = this.Users.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadFixedStruct<ENCRYPTION_CERTIFICATE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        decoder.ReadStructDeferral<ENCRYPTION_CERTIFICATE>(ref elem_0.value);
                    }
                    this.Users.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENCRYPTED_FILE_METADATA_SIGNATURE : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwEfsAccessType);
            encoder.WritePointer(this.CertificatesAdded);
            encoder.WritePointer(this.EncryptionCertificate);
            encoder.WritePointer(this.EfsStreamSignature);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwEfsAccessType = decoder.ReadUInt32();
            this.CertificatesAdded = decoder.ReadUniquePointer<ENCRYPTION_CERTIFICATE_HASH_LIST>();
            this.EncryptionCertificate = decoder.ReadUniquePointer<ENCRYPTION_CERTIFICATE>();
            this.EfsStreamSignature = decoder.ReadUniquePointer<EFS_RPC_BLOB>();
        }
        public uint dwEfsAccessType;
        public RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST> CertificatesAdded;
        public RpcPointer<ENCRYPTION_CERTIFICATE> EncryptionCertificate;
        public RpcPointer<EFS_RPC_BLOB> EfsStreamSignature;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.CertificatesAdded)) {
                encoder.WriteFixedStruct(this.CertificatesAdded.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.CertificatesAdded.value);
            }
            if ((null != this.EncryptionCertificate)) {
                encoder.WriteFixedStruct(this.EncryptionCertificate.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.EncryptionCertificate.value);
            }
            if ((null != this.EfsStreamSignature)) {
                encoder.WriteFixedStruct(this.EfsStreamSignature.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(this.EfsStreamSignature.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.CertificatesAdded)) {
                this.CertificatesAdded.value = decoder.ReadFixedStruct<ENCRYPTION_CERTIFICATE_HASH_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ENCRYPTION_CERTIFICATE_HASH_LIST>(ref this.CertificatesAdded.value);
            }
            if ((null != this.EncryptionCertificate)) {
                this.EncryptionCertificate.value = decoder.ReadFixedStruct<ENCRYPTION_CERTIFICATE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ENCRYPTION_CERTIFICATE>(ref this.EncryptionCertificate.value);
            }
            if ((null != this.EfsStreamSignature)) {
                this.EfsStreamSignature.value = decoder.ReadFixedStruct<EFS_RPC_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_RPC_BLOB>(ref this.EfsStreamSignature.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct EFS_KEY_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwVersion);
            encoder.WriteValue(this.Entropy);
            encoder.WriteValue(this.Algorithm);
            encoder.WriteValue(this.KeyLength);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwVersion = decoder.ReadUInt32();
            this.Entropy = decoder.ReadUInt32();
            this.Algorithm = decoder.ReadUInt32();
            this.KeyLength = decoder.ReadUInt32();
        }
        public uint dwVersion;
        public uint Entropy;
        public uint Algorithm;
        public uint KeyLength;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct EFS_DECRYPTION_STATUS_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.dwDecryptionError);
            encoder.WriteValue(this.dwHashOffset);
            encoder.WriteValue(this.cbHash);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.dwDecryptionError = decoder.ReadUInt32();
            this.dwHashOffset = decoder.ReadUInt32();
            this.cbHash = decoder.ReadUInt32();
        }
        public uint dwDecryptionError;
        public uint dwHashOffset;
        public uint cbHash;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct EFS_ENCRYPTION_STATUS_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.bHasCurrentKey);
            encoder.WriteValue(this.dwEncryptionError);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.bHasCurrentKey = decoder.ReadInt32();
            this.dwEncryptionError = decoder.ReadUInt32();
        }
        public int bHasCurrentKey;
        public uint dwEncryptionError;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENCRYPTION_PROTECTOR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.cbTotalLength);
            encoder.WritePointer(this.UserSid);
            encoder.WritePointer(this.lpProtectorDescriptor);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.cbTotalLength = decoder.ReadUInt32();
            this.UserSid = decoder.ReadUniquePointer<ms_dtyp.RPC_SID>();
            this.lpProtectorDescriptor = decoder.ReadUniquePointer<string>();
        }
        public uint cbTotalLength;
        public RpcPointer<ms_dtyp.RPC_SID> UserSid;
        public RpcPointer<string> lpProtectorDescriptor;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.UserSid)) {
                encoder.WriteConformantStruct(this.UserSid.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.UserSid.value);
            }
            if ((null != this.lpProtectorDescriptor)) {
                encoder.WriteWideCharString(this.lpProtectorDescriptor.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.UserSid)) {
                this.UserSid.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.UserSid.value);
            }
            if ((null != this.lpProtectorDescriptor)) {
                this.lpProtectorDescriptor.value = decoder.ReadWideCharString();
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public struct ENCRYPTION_PROTECTOR_LIST : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.nProtectors);
            encoder.WritePointer(this.pProtectors);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.nProtectors = decoder.ReadUInt32();
            this.pProtectors = decoder.ReadUniquePointer<RpcPointer<ENCRYPTION_PROTECTOR>[]>();
        }
        public uint nProtectors;
        public RpcPointer<RpcPointer<ENCRYPTION_PROTECTOR>[]> pProtectors;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.pProtectors)) {
                encoder.WriteArrayHeader(this.pProtectors.value);
                for (int i = 0; (i < this.pProtectors.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_PROTECTOR> elem_0 = this.pProtectors.value[i];
                    encoder.WritePointer(elem_0);
                }
                for (int i = 0; (i < this.pProtectors.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_PROTECTOR> elem_0 = this.pProtectors.value[i];
                    if ((null != elem_0)) {
                        encoder.WriteFixedStruct(elem_0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                        encoder.WriteStructDeferral(elem_0.value);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.pProtectors)) {
                this.pProtectors.value = decoder.ReadArrayHeader<RpcPointer<ENCRYPTION_PROTECTOR>>();
                for (int i = 0; (i < this.pProtectors.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_PROTECTOR> elem_0 = this.pProtectors.value[i];
                    elem_0 = decoder.ReadUniquePointer<ENCRYPTION_PROTECTOR>();
                    this.pProtectors.value[i] = elem_0;
                }
                for (int i = 0; (i < this.pProtectors.value.Length); i++
                ) {
                    RpcPointer<ENCRYPTION_PROTECTOR> elem_0 = this.pProtectors.value[i];
                    if ((null != elem_0)) {
                        elem_0.value = decoder.ReadFixedStruct<ENCRYPTION_PROTECTOR>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        decoder.ReadStructDeferral<ENCRYPTION_PROTECTOR>(ref elem_0.value);
                    }
                    this.pProtectors.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [System.Runtime.InteropServices.GuidAttribute("c681d488-d850-11d0-8c52-00c04fd90f7e")]
    [Titanis.DceRpc.RpcVersionAttribute(1, 0)]
    public interface efsrpc {
        Task<int> EfsRpcOpenFileRaw(RpcPointer<Titanis.DceRpc.RpcContextHandle> hContext, string FileName, int Flags, System.Threading.CancellationToken cancellationToken);
        Task<int> EfsRpcReadFileRaw(Titanis.DceRpc.RpcContextHandle hContext, RpcPointer<RpcPipe<byte>> EfsOutPipe, System.Threading.CancellationToken cancellationToken);
        Task<int> EfsRpcWriteFileRaw(Titanis.DceRpc.RpcContextHandle hContext, RpcPipe<byte> EfsInPipe, System.Threading.CancellationToken cancellationToken);
        Task EfsRpcCloseRaw(RpcPointer<Titanis.DceRpc.RpcContextHandle> hContext, System.Threading.CancellationToken cancellationToken);
        Task<int> EfsRpcEncryptFileSrv(string FileName, System.Threading.CancellationToken cancellationToken);
        Task<int> EfsRpcDecryptFileSrv(string FileName, uint OpenFlag, System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcQueryUsersOnFile(string FileName, RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>> Users, System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcQueryRecoveryAgents(string FileName, RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>> RecoveryAgents, System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcRemoveUsersFromFile(string FileName, ENCRYPTION_CERTIFICATE_HASH_LIST Users, System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcAddUsersToFile(string FileName, ENCRYPTION_CERTIFICATE_LIST EncryptionCertificates, System.Threading.CancellationToken cancellationToken);
        Task Opnum10NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcNotSupported(string Reserved1, string Reserved2, uint dwReserved1, uint dwReserved2, RpcPointer<EFS_RPC_BLOB> Reserved, int bReserved, System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcFileKeyInfo(string FileName, uint InfoClass, RpcPointer<RpcPointer<EFS_RPC_BLOB>> KeyInfo, System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcDuplicateEncryptionInfoFile(string SrcFileName, string DestFileName, uint dwCreationDisposition, uint dwAttributes, RpcPointer<EFS_RPC_BLOB> RelativeSD, int bInheritHandle, System.Threading.CancellationToken cancellationToken);
        Task Opnum14NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcAddUsersToFileEx(uint dwFlags, RpcPointer<EFS_RPC_BLOB> Reserved, string FileName, ENCRYPTION_CERTIFICATE_LIST EncryptionCertificates, System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcFileKeyInfoEx(uint dwFileKeyInfoFlags, RpcPointer<EFS_RPC_BLOB> Reserved, string FileName, uint InfoClass, RpcPointer<RpcPointer<EFS_RPC_BLOB>> KeyInfo, System.Threading.CancellationToken cancellationToken);
        Task Opnum17NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcGetEncryptedFileMetadata(string FileName, RpcPointer<RpcPointer<EFS_RPC_BLOB>> EfsStreamBlob, System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcSetEncryptedFileMetadata(string FileName, RpcPointer<EFS_RPC_BLOB> OldEfsStreamBlob, EFS_RPC_BLOB NewEfsStreamBlob, RpcPointer<ENCRYPTED_FILE_METADATA_SIGNATURE> NewEfsSignature, System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcFlushEfsCache(System.Threading.CancellationToken cancellationToken);
        Task<int> EfsRpcEncryptFileExSrv(string FileName, string ProtectorDescriptor, uint Flags, System.Threading.CancellationToken cancellationToken);
        Task<uint> EfsRpcQueryProtectors(string FileName, RpcPointer<RpcPointer<RpcPointer<ENCRYPTION_PROTECTOR_LIST>>> ppProtectorList, System.Threading.CancellationToken cancellationToken);
        Task Opnum23NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum24NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum25NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum26NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum27NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum28NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum29NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum30NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum31NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum32NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum33NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum34NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum35NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum36NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum37NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum38NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum39NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum40NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum41NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum42NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum43NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum44NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    [Titanis.DceRpc.IidAttribute("df1941c5-fe89-4e79-bf10-463657acf44d")]
    public class efsrpcClientProxy : Titanis.DceRpc.Client.RpcClientProxy, efsrpc, Titanis.DceRpc.IRpcClientProxy {
        private static System.Guid _interfaceUuid = new System.Guid("df1941c5-fe89-4e79-bf10-463657acf44d");
        public override System.Guid InterfaceUuid {
            get {
                return _interfaceUuid;
            }
        }
        public override Titanis.DceRpc.RpcVersion InterfaceVersion {
            get {
                return new Titanis.DceRpc.RpcVersion(1, 0);
            }
        }
        public virtual async Task<int> EfsRpcOpenFileRaw(RpcPointer<Titanis.DceRpc.RpcContextHandle> hContext, string FileName, int Flags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            encoder.WriteValue(Flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            hContext.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EfsRpcReadFileRaw(Titanis.DceRpc.RpcContextHandle hContext, RpcPointer<RpcPipe<byte>> EfsOutPipe, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hContext);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EfsOutPipe.value = decoder.ReadPipeHeader<byte>();
            for (var chunk_0 = decoder.ReadPipeChunkHeader<byte>(); (chunk_0 != null); chunk_0 = decoder.ReadPipeChunkHeader<byte>()) {
                for (int i = 0; (i < chunk_0.Length); i++
                ) {
                    byte elem_0 = chunk_0[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    chunk_0[i] = elem_0;
                    decoder.ReadPipeChunkTrailer<byte>(chunk_0);
                }
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EfsRpcWriteFileRaw(Titanis.DceRpc.RpcContextHandle hContext, RpcPipe<byte> EfsInPipe, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hContext);
            for (
            ; EfsInPipe.ReadNextChunk(); 
            ) {
                var chunk_0 = EfsInPipe.Chunk;
                encoder.WritePipeChunkHeader(chunk_0);
                for (int i = 0; (i < chunk_0.Length); i++
                ) {
                    byte elem_0 = chunk_0[i];
                    encoder.WriteValue(elem_0);
                }
                encoder.WritePipeChunkHeader(chunk_0);
            }
            encoder.WritePipeEnd();
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task EfsRpcCloseRaw(RpcPointer<Titanis.DceRpc.RpcContextHandle> hContext, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(hContext.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            hContext.value = decoder.ReadContextHandle();
        }
        public virtual async Task<int> EfsRpcEncryptFileSrv(string FileName, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> EfsRpcDecryptFileSrv(string FileName, uint OpenFlag, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            encoder.WriteValue(OpenFlag);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<uint> EfsRpcQueryUsersOnFile(string FileName, RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>> Users, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Users.value = decoder.ReadOutUniquePointer<ENCRYPTION_CERTIFICATE_HASH_LIST>(Users.value);
            if ((null != Users.value)) {
                Users.value.value = decoder.ReadFixedStruct<ENCRYPTION_CERTIFICATE_HASH_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ENCRYPTION_CERTIFICATE_HASH_LIST>(ref Users.value.value);
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> EfsRpcQueryRecoveryAgents(string FileName, RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>> RecoveryAgents, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            RecoveryAgents.value = decoder.ReadOutUniquePointer<ENCRYPTION_CERTIFICATE_HASH_LIST>(RecoveryAgents.value);
            if ((null != RecoveryAgents.value)) {
                RecoveryAgents.value.value = decoder.ReadFixedStruct<ENCRYPTION_CERTIFICATE_HASH_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ENCRYPTION_CERTIFICATE_HASH_LIST>(ref RecoveryAgents.value.value);
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> EfsRpcRemoveUsersFromFile(string FileName, ENCRYPTION_CERTIFICATE_HASH_LIST Users, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            encoder.WriteFixedStruct(Users, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Users);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> EfsRpcAddUsersToFile(string FileName, ENCRYPTION_CERTIFICATE_LIST EncryptionCertificates, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            encoder.WriteFixedStruct(EncryptionCertificates, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(EncryptionCertificates);
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
        public virtual async Task<uint> EfsRpcNotSupported(string Reserved1, string Reserved2, uint dwReserved1, uint dwReserved2, RpcPointer<EFS_RPC_BLOB> Reserved, int bReserved, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(Reserved1);
            encoder.WriteWideCharString(Reserved2);
            encoder.WriteValue(dwReserved1);
            encoder.WriteValue(dwReserved2);
            encoder.WritePointer(Reserved);
            if ((null != Reserved)) {
                encoder.WriteFixedStruct(Reserved.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Reserved.value);
            }
            encoder.WriteValue(bReserved);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> EfsRpcFileKeyInfo(string FileName, uint InfoClass, RpcPointer<RpcPointer<EFS_RPC_BLOB>> KeyInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            encoder.WriteValue(InfoClass);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            KeyInfo.value = decoder.ReadOutUniquePointer<EFS_RPC_BLOB>(KeyInfo.value);
            if ((null != KeyInfo.value)) {
                KeyInfo.value.value = decoder.ReadFixedStruct<EFS_RPC_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_RPC_BLOB>(ref KeyInfo.value.value);
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> EfsRpcDuplicateEncryptionInfoFile(string SrcFileName, string DestFileName, uint dwCreationDisposition, uint dwAttributes, RpcPointer<EFS_RPC_BLOB> RelativeSD, int bInheritHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(SrcFileName);
            encoder.WriteWideCharString(DestFileName);
            encoder.WriteValue(dwCreationDisposition);
            encoder.WriteValue(dwAttributes);
            encoder.WritePointer(RelativeSD);
            if ((null != RelativeSD)) {
                encoder.WriteFixedStruct(RelativeSD.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(RelativeSD.value);
            }
            encoder.WriteValue(bInheritHandle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum14NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<uint> EfsRpcAddUsersToFileEx(uint dwFlags, RpcPointer<EFS_RPC_BLOB> Reserved, string FileName, ENCRYPTION_CERTIFICATE_LIST EncryptionCertificates, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(dwFlags);
            encoder.WritePointer(Reserved);
            if ((null != Reserved)) {
                encoder.WriteFixedStruct(Reserved.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Reserved.value);
            }
            encoder.WriteWideCharString(FileName);
            encoder.WriteFixedStruct(EncryptionCertificates, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(EncryptionCertificates);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> EfsRpcFileKeyInfoEx(uint dwFileKeyInfoFlags, RpcPointer<EFS_RPC_BLOB> Reserved, string FileName, uint InfoClass, RpcPointer<RpcPointer<EFS_RPC_BLOB>> KeyInfo, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(dwFileKeyInfoFlags);
            encoder.WritePointer(Reserved);
            if ((null != Reserved)) {
                encoder.WriteFixedStruct(Reserved.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Reserved.value);
            }
            encoder.WriteWideCharString(FileName);
            encoder.WriteValue(InfoClass);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            KeyInfo.value = decoder.ReadOutUniquePointer<EFS_RPC_BLOB>(KeyInfo.value);
            if ((null != KeyInfo.value)) {
                KeyInfo.value.value = decoder.ReadFixedStruct<EFS_RPC_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_RPC_BLOB>(ref KeyInfo.value.value);
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum17NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<uint> EfsRpcGetEncryptedFileMetadata(string FileName, RpcPointer<RpcPointer<EFS_RPC_BLOB>> EfsStreamBlob, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EfsStreamBlob.value = decoder.ReadOutUniquePointer<EFS_RPC_BLOB>(EfsStreamBlob.value);
            if ((null != EfsStreamBlob.value)) {
                EfsStreamBlob.value.value = decoder.ReadFixedStruct<EFS_RPC_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_RPC_BLOB>(ref EfsStreamBlob.value.value);
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> EfsRpcSetEncryptedFileMetadata(string FileName, RpcPointer<EFS_RPC_BLOB> OldEfsStreamBlob, EFS_RPC_BLOB NewEfsStreamBlob, RpcPointer<ENCRYPTED_FILE_METADATA_SIGNATURE> NewEfsSignature, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            encoder.WritePointer(OldEfsStreamBlob);
            if ((null != OldEfsStreamBlob)) {
                encoder.WriteFixedStruct(OldEfsStreamBlob.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(OldEfsStreamBlob.value);
            }
            encoder.WriteFixedStruct(NewEfsStreamBlob, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(NewEfsStreamBlob);
            encoder.WritePointer(NewEfsSignature);
            if ((null != NewEfsSignature)) {
                encoder.WriteFixedStruct(NewEfsSignature.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(NewEfsSignature.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<uint> EfsRpcFlushEfsCache(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task<int> EfsRpcEncryptFileExSrv(string FileName, string ProtectorDescriptor, uint Flags, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            encoder.WriteUniqueReferentId((ProtectorDescriptor == null));
            if ((ProtectorDescriptor != null)) {
                encoder.WriteWideCharString(ProtectorDescriptor);
            }
            encoder.WriteValue(Flags);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<uint> EfsRpcQueryProtectors(string FileName, RpcPointer<RpcPointer<RpcPointer<ENCRYPTION_PROTECTOR_LIST>>> ppProtectorList, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteWideCharString(FileName);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ppProtectorList.value = decoder.ReadOutUniquePointer<RpcPointer<ENCRYPTION_PROTECTOR_LIST>>(ppProtectorList.value);
            if ((null != ppProtectorList.value)) {
                ppProtectorList.value.value = decoder.ReadUniquePointer<ENCRYPTION_PROTECTOR_LIST>();
                if ((null != ppProtectorList.value.value)) {
                    ppProtectorList.value.value.value = decoder.ReadFixedStruct<ENCRYPTION_PROTECTOR_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    decoder.ReadStructDeferral<ENCRYPTION_PROTECTOR_LIST>(ref ppProtectorList.value.value.value);
                }
            }
            uint retval;
            retval = decoder.ReadUInt32();
            return retval;
        }
        public virtual async Task Opnum23NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum24NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum25NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum26NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum27NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum28NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum29NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum30NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum31NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum32NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum33NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum34NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum35NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum36NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum37NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(37);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum38NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(38);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum39NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(39);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum40NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(40);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum41NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(41);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum42NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(42);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum43NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(43);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum44NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(44);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
    public class efsrpcStub : Titanis.DceRpc.Server.RpcServiceStub {
        private static System.Guid _interfaceUuid = new System.Guid("c681d488-d850-11d0-8c52-00c04fd90f7e");
        public override System.Guid InterfaceUuid {
            get {
                return _interfaceUuid;
            }
        }
        public override Titanis.DceRpc.RpcVersion InterfaceVersion {
            get {
                return new Titanis.DceRpc.RpcVersion(1, 0);
            }
        }
        private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
        public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable {
            get {
                return this._dispatchTable;
            }
        }
        private efsrpc _obj;
        public efsrpcStub(efsrpc obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_EfsRpcOpenFileRaw,
                    this.Invoke_EfsRpcReadFileRaw,
                    this.Invoke_EfsRpcWriteFileRaw,
                    this.Invoke_EfsRpcCloseRaw,
                    this.Invoke_EfsRpcEncryptFileSrv,
                    this.Invoke_EfsRpcDecryptFileSrv,
                    this.Invoke_EfsRpcQueryUsersOnFile,
                    this.Invoke_EfsRpcQueryRecoveryAgents,
                    this.Invoke_EfsRpcRemoveUsersFromFile,
                    this.Invoke_EfsRpcAddUsersToFile,
                    this.Invoke_Opnum10NotUsedOnWire,
                    this.Invoke_EfsRpcNotSupported,
                    this.Invoke_EfsRpcFileKeyInfo,
                    this.Invoke_EfsRpcDuplicateEncryptionInfoFile,
                    this.Invoke_Opnum14NotUsedOnWire,
                    this.Invoke_EfsRpcAddUsersToFileEx,
                    this.Invoke_EfsRpcFileKeyInfoEx,
                    this.Invoke_Opnum17NotUsedOnWire,
                    this.Invoke_EfsRpcGetEncryptedFileMetadata,
                    this.Invoke_EfsRpcSetEncryptedFileMetadata,
                    this.Invoke_EfsRpcFlushEfsCache,
                    this.Invoke_EfsRpcEncryptFileExSrv,
                    this.Invoke_EfsRpcQueryProtectors,
                    this.Invoke_Opnum23NotUsedOnWire,
                    this.Invoke_Opnum24NotUsedOnWire,
                    this.Invoke_Opnum25NotUsedOnWire,
                    this.Invoke_Opnum26NotUsedOnWire,
                    this.Invoke_Opnum27NotUsedOnWire,
                    this.Invoke_Opnum28NotUsedOnWire,
                    this.Invoke_Opnum29NotUsedOnWire,
                    this.Invoke_Opnum30NotUsedOnWire,
                    this.Invoke_Opnum31NotUsedOnWire,
                    this.Invoke_Opnum32NotUsedOnWire,
                    this.Invoke_Opnum33NotUsedOnWire,
                    this.Invoke_Opnum34NotUsedOnWire,
                    this.Invoke_Opnum35NotUsedOnWire,
                    this.Invoke_Opnum36NotUsedOnWire,
                    this.Invoke_Opnum37NotUsedOnWire,
                    this.Invoke_Opnum38NotUsedOnWire,
                    this.Invoke_Opnum39NotUsedOnWire,
                    this.Invoke_Opnum40NotUsedOnWire,
                    this.Invoke_Opnum41NotUsedOnWire,
                    this.Invoke_Opnum42NotUsedOnWire,
                    this.Invoke_Opnum43NotUsedOnWire,
                    this.Invoke_Opnum44NotUsedOnWire};
        }
        private async Task Invoke_EfsRpcOpenFileRaw(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> hContext = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            string FileName;
            int Flags;
            FileName = decoder.ReadWideCharString();
            Flags = decoder.ReadInt32();
            var invokeTask = this._obj.EfsRpcOpenFileRaw(hContext, FileName, Flags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(hContext.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcReadFileRaw(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hContext;
            RpcPointer<RpcPipe<byte>> EfsOutPipe = new RpcPointer<RpcPipe<byte>>();
            hContext = decoder.ReadContextHandle();
            var invokeTask = this._obj.EfsRpcReadFileRaw(hContext, EfsOutPipe, cancellationToken);
            var retval = await invokeTask;
            for (
            ; EfsOutPipe.value.ReadNextChunk(); 
            ) {
                var chunk_0 = EfsOutPipe.value.Chunk;
                encoder.WritePipeChunkHeader(chunk_0);
                for (int i = 0; (i < chunk_0.Length); i++
                ) {
                    byte elem_0 = chunk_0[i];
                    encoder.WriteValue(elem_0);
                }
                encoder.WritePipeChunkHeader(chunk_0);
            }
            encoder.WritePipeEnd();
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcWriteFileRaw(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle hContext;
            RpcPipe<byte> EfsInPipe;
            hContext = decoder.ReadContextHandle();
            EfsInPipe = decoder.ReadPipeHeader<byte>();
            for (var chunk_0 = decoder.ReadPipeChunkHeader<byte>(); (chunk_0 != null); chunk_0 = decoder.ReadPipeChunkHeader<byte>()) {
                for (int i = 0; (i < chunk_0.Length); i++
                ) {
                    byte elem_0 = chunk_0[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    chunk_0[i] = elem_0;
                    decoder.ReadPipeChunkTrailer<byte>(chunk_0);
                }
            }
            var invokeTask = this._obj.EfsRpcWriteFileRaw(hContext, EfsInPipe, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcCloseRaw(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> hContext;
            hContext = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            hContext.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.EfsRpcCloseRaw(hContext, cancellationToken);
            await invokeTask;
            encoder.WriteContextHandle(hContext.value);
        }
        private async Task Invoke_EfsRpcEncryptFileSrv(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            FileName = decoder.ReadWideCharString();
            var invokeTask = this._obj.EfsRpcEncryptFileSrv(FileName, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcDecryptFileSrv(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            uint OpenFlag;
            FileName = decoder.ReadWideCharString();
            OpenFlag = decoder.ReadUInt32();
            var invokeTask = this._obj.EfsRpcDecryptFileSrv(FileName, OpenFlag, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcQueryUsersOnFile(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>> Users = new RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>>();
            FileName = decoder.ReadWideCharString();
            var invokeTask = this._obj.EfsRpcQueryUsersOnFile(FileName, Users, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Users.value);
            if ((null != Users.value)) {
                encoder.WriteFixedStruct(Users.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Users.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcQueryRecoveryAgents(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>> RecoveryAgents = new RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>>();
            FileName = decoder.ReadWideCharString();
            var invokeTask = this._obj.EfsRpcQueryRecoveryAgents(FileName, RecoveryAgents, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(RecoveryAgents.value);
            if ((null != RecoveryAgents.value)) {
                encoder.WriteFixedStruct(RecoveryAgents.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(RecoveryAgents.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcRemoveUsersFromFile(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            ENCRYPTION_CERTIFICATE_HASH_LIST Users;
            FileName = decoder.ReadWideCharString();
            Users = decoder.ReadFixedStruct<ENCRYPTION_CERTIFICATE_HASH_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ENCRYPTION_CERTIFICATE_HASH_LIST>(ref Users);
            var invokeTask = this._obj.EfsRpcRemoveUsersFromFile(FileName, Users, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcAddUsersToFile(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            ENCRYPTION_CERTIFICATE_LIST EncryptionCertificates;
            FileName = decoder.ReadWideCharString();
            EncryptionCertificates = decoder.ReadFixedStruct<ENCRYPTION_CERTIFICATE_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ENCRYPTION_CERTIFICATE_LIST>(ref EncryptionCertificates);
            var invokeTask = this._obj.EfsRpcAddUsersToFile(FileName, EncryptionCertificates, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum10NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum10NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_EfsRpcNotSupported(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string Reserved1;
            string Reserved2;
            uint dwReserved1;
            uint dwReserved2;
            RpcPointer<EFS_RPC_BLOB> Reserved;
            int bReserved;
            Reserved1 = decoder.ReadWideCharString();
            Reserved2 = decoder.ReadWideCharString();
            dwReserved1 = decoder.ReadUInt32();
            dwReserved2 = decoder.ReadUInt32();
            Reserved = decoder.ReadUniquePointer<EFS_RPC_BLOB>();
            if ((null != Reserved)) {
                Reserved.value = decoder.ReadFixedStruct<EFS_RPC_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_RPC_BLOB>(ref Reserved.value);
            }
            bReserved = decoder.ReadInt32();
            var invokeTask = this._obj.EfsRpcNotSupported(Reserved1, Reserved2, dwReserved1, dwReserved2, Reserved, bReserved, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcFileKeyInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            uint InfoClass;
            RpcPointer<RpcPointer<EFS_RPC_BLOB>> KeyInfo = new RpcPointer<RpcPointer<EFS_RPC_BLOB>>();
            FileName = decoder.ReadWideCharString();
            InfoClass = decoder.ReadUInt32();
            var invokeTask = this._obj.EfsRpcFileKeyInfo(FileName, InfoClass, KeyInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(KeyInfo.value);
            if ((null != KeyInfo.value)) {
                encoder.WriteFixedStruct(KeyInfo.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(KeyInfo.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcDuplicateEncryptionInfoFile(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string SrcFileName;
            string DestFileName;
            uint dwCreationDisposition;
            uint dwAttributes;
            RpcPointer<EFS_RPC_BLOB> RelativeSD;
            int bInheritHandle;
            SrcFileName = decoder.ReadWideCharString();
            DestFileName = decoder.ReadWideCharString();
            dwCreationDisposition = decoder.ReadUInt32();
            dwAttributes = decoder.ReadUInt32();
            RelativeSD = decoder.ReadUniquePointer<EFS_RPC_BLOB>();
            if ((null != RelativeSD)) {
                RelativeSD.value = decoder.ReadFixedStruct<EFS_RPC_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_RPC_BLOB>(ref RelativeSD.value);
            }
            bInheritHandle = decoder.ReadInt32();
            var invokeTask = this._obj.EfsRpcDuplicateEncryptionInfoFile(SrcFileName, DestFileName, dwCreationDisposition, dwAttributes, RelativeSD, bInheritHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum14NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum14NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_EfsRpcAddUsersToFileEx(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint dwFlags;
            RpcPointer<EFS_RPC_BLOB> Reserved;
            string FileName;
            ENCRYPTION_CERTIFICATE_LIST EncryptionCertificates;
            dwFlags = decoder.ReadUInt32();
            Reserved = decoder.ReadUniquePointer<EFS_RPC_BLOB>();
            if ((null != Reserved)) {
                Reserved.value = decoder.ReadFixedStruct<EFS_RPC_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_RPC_BLOB>(ref Reserved.value);
            }
            FileName = decoder.ReadWideCharString();
            EncryptionCertificates = decoder.ReadFixedStruct<ENCRYPTION_CERTIFICATE_LIST>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ENCRYPTION_CERTIFICATE_LIST>(ref EncryptionCertificates);
            var invokeTask = this._obj.EfsRpcAddUsersToFileEx(dwFlags, Reserved, FileName, EncryptionCertificates, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcFileKeyInfoEx(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            uint dwFileKeyInfoFlags;
            RpcPointer<EFS_RPC_BLOB> Reserved;
            string FileName;
            uint InfoClass;
            RpcPointer<RpcPointer<EFS_RPC_BLOB>> KeyInfo = new RpcPointer<RpcPointer<EFS_RPC_BLOB>>();
            dwFileKeyInfoFlags = decoder.ReadUInt32();
            Reserved = decoder.ReadUniquePointer<EFS_RPC_BLOB>();
            if ((null != Reserved)) {
                Reserved.value = decoder.ReadFixedStruct<EFS_RPC_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_RPC_BLOB>(ref Reserved.value);
            }
            FileName = decoder.ReadWideCharString();
            InfoClass = decoder.ReadUInt32();
            var invokeTask = this._obj.EfsRpcFileKeyInfoEx(dwFileKeyInfoFlags, Reserved, FileName, InfoClass, KeyInfo, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(KeyInfo.value);
            if ((null != KeyInfo.value)) {
                encoder.WriteFixedStruct(KeyInfo.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(KeyInfo.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum17NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum17NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_EfsRpcGetEncryptedFileMetadata(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            RpcPointer<RpcPointer<EFS_RPC_BLOB>> EfsStreamBlob = new RpcPointer<RpcPointer<EFS_RPC_BLOB>>();
            FileName = decoder.ReadWideCharString();
            var invokeTask = this._obj.EfsRpcGetEncryptedFileMetadata(FileName, EfsStreamBlob, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(EfsStreamBlob.value);
            if ((null != EfsStreamBlob.value)) {
                encoder.WriteFixedStruct(EfsStreamBlob.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(EfsStreamBlob.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcSetEncryptedFileMetadata(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            RpcPointer<EFS_RPC_BLOB> OldEfsStreamBlob;
            EFS_RPC_BLOB NewEfsStreamBlob;
            RpcPointer<ENCRYPTED_FILE_METADATA_SIGNATURE> NewEfsSignature;
            FileName = decoder.ReadWideCharString();
            OldEfsStreamBlob = decoder.ReadUniquePointer<EFS_RPC_BLOB>();
            if ((null != OldEfsStreamBlob)) {
                OldEfsStreamBlob.value = decoder.ReadFixedStruct<EFS_RPC_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<EFS_RPC_BLOB>(ref OldEfsStreamBlob.value);
            }
            NewEfsStreamBlob = decoder.ReadFixedStruct<EFS_RPC_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<EFS_RPC_BLOB>(ref NewEfsStreamBlob);
            NewEfsSignature = decoder.ReadUniquePointer<ENCRYPTED_FILE_METADATA_SIGNATURE>();
            if ((null != NewEfsSignature)) {
                NewEfsSignature.value = decoder.ReadFixedStruct<ENCRYPTED_FILE_METADATA_SIGNATURE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ENCRYPTED_FILE_METADATA_SIGNATURE>(ref NewEfsSignature.value);
            }
            var invokeTask = this._obj.EfsRpcSetEncryptedFileMetadata(FileName, OldEfsStreamBlob, NewEfsStreamBlob, NewEfsSignature, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcFlushEfsCache(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.EfsRpcFlushEfsCache(cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcEncryptFileExSrv(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            string ProtectorDescriptor;
            uint Flags;
            FileName = decoder.ReadWideCharString();
            if ((decoder.ReadReferentId() == 0)) {
                ProtectorDescriptor = null;
            }
            else {
                ProtectorDescriptor = decoder.ReadWideCharString();
            }
            Flags = decoder.ReadUInt32();
            var invokeTask = this._obj.EfsRpcEncryptFileExSrv(FileName, ProtectorDescriptor, Flags, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_EfsRpcQueryProtectors(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string FileName;
            RpcPointer<RpcPointer<RpcPointer<ENCRYPTION_PROTECTOR_LIST>>> ppProtectorList = new RpcPointer<RpcPointer<RpcPointer<ENCRYPTION_PROTECTOR_LIST>>>();
            FileName = decoder.ReadWideCharString();
            var invokeTask = this._obj.EfsRpcQueryProtectors(FileName, ppProtectorList, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(ppProtectorList.value);
            if ((null != ppProtectorList.value)) {
                encoder.WritePointer(ppProtectorList.value.value);
                if ((null != ppProtectorList.value.value)) {
                    encoder.WriteFixedStruct(ppProtectorList.value.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                    encoder.WriteStructDeferral(ppProtectorList.value.value.value);
                }
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum23NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum23NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum24NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum24NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum25NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum25NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum26NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum26NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum27NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum27NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum28NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum28NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum29NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum29NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum30NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum30NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum31NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum31NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum32NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum32NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum33NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum33NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum34NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum34NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum35NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum35NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum36NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum36NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum37NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum37NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum38NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum38NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum39NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum39NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum40NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum40NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum41NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum41NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum42NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum42NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum43NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum43NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum44NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum44NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
    }
}
