#pragma warning disable

namespace ms_samr {
    using System;
    using System.Threading.Tasks;
    using Titanis;
	using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct RPC_STRING : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WriteValue(this.MaximumLength);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt16();
            this.MaximumLength = decoder.ReadUInt16();
            this.Buffer = decoder.ReadPointer<ArraySegment<byte>>();
        }
        public ushort Length;
        public ushort MaximumLength;
        public RpcPointer<ArraySegment<byte>> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value, true);
                for (int i = 0; (i < this.Buffer.value.Count); i++
                ) {
                    byte elem_0 = this.Buffer.value.Item(i);
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArraySegmentHeader<byte>();
                for (int i = 0; (i < this.Buffer.value.Count); i++
                ) {
                    byte elem_0 = this.Buffer.value.Item(i);
                    elem_0 = decoder.ReadUnsignedChar();
                    this.Buffer.value.Item(i) = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct OLD_LARGE_INTEGER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.LowPart);
            encoder.WriteValue(this.HighPart);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.LowPart = decoder.ReadUInt32();
            this.HighPart = decoder.ReadInt32();
        }
        public uint LowPart;
        public int HighPart;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct ENCRYPTED_LM_OWF_PASSWORD : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((this.data == null)) {
                this.data = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.data[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((this.data == null)) {
                this.data = new byte[16];
            }
            for (int i = 0; (i < 16); i++
            ) {
                byte elem_0 = this.data[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.data[i] = elem_0;
            }
        }
        public byte[] data;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_ULONG_ARRAY : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Count);
            encoder.WritePointer(this.Element);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Count = decoder.ReadUInt32();
            this.Element = decoder.ReadPointer<uint[]>();
        }
        public uint Count;
        public RpcPointer<uint[]> Element;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Element)) {
                encoder.WriteArrayHeader(this.Element.value);
                for (int i = 0; (i < this.Element.value.Length); i++
                ) {
                    uint elem_0 = this.Element.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Element)) {
                this.Element.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < this.Element.value.Length); i++
                ) {
                    uint elem_0 = this.Element.value[i];
                    elem_0 = decoder.ReadUInt32();
                    this.Element.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_SID_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WritePointer(this.SidPointer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.SidPointer = decoder.ReadPointer<ms_dtyp.RPC_SID>();
        }
        public RpcPointer<ms_dtyp.RPC_SID> SidPointer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.SidPointer)) {
                encoder.WriteConformantStruct(this.SidPointer.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(this.SidPointer.value);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.SidPointer)) {
                this.SidPointer.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref this.SidPointer.value);
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_PSID_ARRAY : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Count);
            encoder.WritePointer(this.Sids);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Count = decoder.ReadUInt32();
            this.Sids = decoder.ReadPointer<SAMPR_SID_INFORMATION[]>();
        }
        public uint Count;
        public RpcPointer<SAMPR_SID_INFORMATION[]> Sids;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Sids)) {
                encoder.WriteArrayHeader(this.Sids.value);
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Sids)) {
                this.Sids.value = decoder.ReadArrayHeader<SAMPR_SID_INFORMATION>();
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
                    elem_0 = decoder.ReadFixedStruct<SAMPR_SID_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Sids.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
                    decoder.ReadStructDeferral<SAMPR_SID_INFORMATION>(ref elem_0);
                    this.Sids.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_PSID_ARRAY_OUT : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Count);
            encoder.WritePointer(this.Sids);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Count = decoder.ReadUInt32();
            this.Sids = decoder.ReadPointer<SAMPR_SID_INFORMATION[]>();
        }
        public uint Count;
        public RpcPointer<SAMPR_SID_INFORMATION[]> Sids;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Sids)) {
                encoder.WriteArrayHeader(this.Sids.value);
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Sids)) {
                this.Sids.value = decoder.ReadArrayHeader<SAMPR_SID_INFORMATION>();
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
                    elem_0 = decoder.ReadFixedStruct<SAMPR_SID_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Sids.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Sids.value.Length); i++
                ) {
                    SAMPR_SID_INFORMATION elem_0 = this.Sids.value[i];
                    decoder.ReadStructDeferral<SAMPR_SID_INFORMATION>(ref elem_0);
                    this.Sids.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_RETURNED_USTRING_ARRAY : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Count);
            encoder.WritePointer(this.Element);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Count = decoder.ReadUInt32();
            this.Element = decoder.ReadPointer<ms_dtyp.RPC_UNICODE_STRING[]>();
        }
        public uint Count;
        public RpcPointer<ms_dtyp.RPC_UNICODE_STRING[]> Element;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Element)) {
                encoder.WriteArrayHeader(this.Element.value);
                for (int i = 0; (i < this.Element.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Element.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Element.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Element.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Element)) {
                this.Element.value = decoder.ReadArrayHeader<ms_dtyp.RPC_UNICODE_STRING>();
                for (int i = 0; (i < this.Element.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Element.value[i];
                    elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Element.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Element.value.Length); i++
                ) {
                    ms_dtyp.RPC_UNICODE_STRING elem_0 = this.Element.value[i];
                    decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
                    this.Element.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public enum SID_NAME_USE : int {
        SidTypeUser = 1,
        SidTypeGroup = 2,
        SidTypeDomain = 3,
        SidTypeAlias = 4,
        SidTypeWellKnownGroup = 5,
        SidTypeDeletedAccount = 6,
        SidTypeInvalid = 7,
        SidTypeUnknown = 8,
        SidTypeComputer = 9,
        SidTypeLabel = 10,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct RPC_SHORT_BLOB : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WriteValue(this.MaximumLength);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt16();
            this.MaximumLength = decoder.ReadUInt16();
            this.Buffer = decoder.ReadPointer<ArraySegment<ushort>>();
        }
        public ushort Length;
        public ushort MaximumLength;
        public RpcPointer<ArraySegment<ushort>> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value, true);
                for (int i = 0; (i < this.Buffer.value.Count); i++
                ) {
                    ushort elem_0 = this.Buffer.value.Item(i);
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArraySegmentHeader<ushort>();
                for (int i = 0; (i < this.Buffer.value.Count); i++
                ) {
                    ushort elem_0 = this.Buffer.value.Item(i);
                    elem_0 = decoder.ReadUInt16();
                    this.Buffer.value.Item(i) = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_RID_ENUMERATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.RelativeId);
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.RelativeId = decoder.ReadUInt32();
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public uint RelativeId;
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_ENUMERATION_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadPointer<SAMPR_RID_ENUMERATION[]>();
        }
        public uint EntriesRead;
        public RpcPointer<SAMPR_RID_ENUMERATION[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_RID_ENUMERATION elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_RID_ENUMERATION elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<SAMPR_RID_ENUMERATION>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_RID_ENUMERATION elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<SAMPR_RID_ENUMERATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_RID_ENUMERATION elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<SAMPR_RID_ENUMERATION>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_SR_SECURITY_DESCRIPTOR : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WritePointer(this.SecurityDescriptor);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt32();
            this.SecurityDescriptor = decoder.ReadPointer<byte[]>();
        }
        public uint Length;
        public RpcPointer<byte[]> SecurityDescriptor;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.SecurityDescriptor)) {
                encoder.WriteArrayHeader(this.SecurityDescriptor.value);
                for (int i = 0; (i < this.SecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.SecurityDescriptor.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.SecurityDescriptor)) {
                this.SecurityDescriptor.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.SecurityDescriptor.value.Length); i++
                ) {
                    byte elem_0 = this.SecurityDescriptor.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.SecurityDescriptor.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct GROUP_MEMBERSHIP : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.RelativeId);
            encoder.WriteValue(this.Attributes);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.RelativeId = decoder.ReadUInt32();
            this.Attributes = decoder.ReadUInt32();
        }
        public uint RelativeId;
        public uint Attributes;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_GET_GROUPS_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.MembershipCount);
            encoder.WritePointer(this.Groups);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.MembershipCount = decoder.ReadUInt32();
            this.Groups = decoder.ReadPointer<GROUP_MEMBERSHIP[]>();
        }
        public uint MembershipCount;
        public RpcPointer<GROUP_MEMBERSHIP[]> Groups;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Groups)) {
                encoder.WriteArrayHeader(this.Groups.value);
                for (int i = 0; (i < this.Groups.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.Groups.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
                }
                for (int i = 0; (i < this.Groups.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.Groups.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Groups)) {
                this.Groups.value = decoder.ReadArrayHeader<GROUP_MEMBERSHIP>();
                for (int i = 0; (i < this.Groups.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.Groups.value[i];
                    elem_0 = decoder.ReadFixedStruct<GROUP_MEMBERSHIP>(Titanis.DceRpc.NdrAlignment._4Byte);
                    this.Groups.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Groups.value.Length); i++
                ) {
                    GROUP_MEMBERSHIP elem_0 = this.Groups.value[i];
                    decoder.ReadStructDeferral<GROUP_MEMBERSHIP>(ref elem_0);
                    this.Groups.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_GET_MEMBERS_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.MemberCount);
            encoder.WritePointer(this.Members);
            encoder.WritePointer(this.Attributes);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.MemberCount = decoder.ReadUInt32();
            this.Members = decoder.ReadPointer<uint[]>();
            this.Attributes = decoder.ReadPointer<uint[]>();
        }
        public uint MemberCount;
        public RpcPointer<uint[]> Members;
        public RpcPointer<uint[]> Attributes;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Members)) {
                encoder.WriteArrayHeader(this.Members.value);
                for (int i = 0; (i < this.Members.value.Length); i++
                ) {
                    uint elem_0 = this.Members.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
            if ((null != this.Attributes)) {
                encoder.WriteArrayHeader(this.Attributes.value);
                for (int i = 0; (i < this.Attributes.value.Length); i++
                ) {
                    uint elem_0 = this.Attributes.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Members)) {
                this.Members.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < this.Members.value.Length); i++
                ) {
                    uint elem_0 = this.Members.value[i];
                    elem_0 = decoder.ReadUInt32();
                    this.Members.value[i] = elem_0;
                }
            }
            if ((null != this.Attributes)) {
                this.Attributes.value = decoder.ReadArrayHeader<uint>();
                for (int i = 0; (i < this.Attributes.value.Length); i++
                ) {
                    uint elem_0 = this.Attributes.value[i];
                    elem_0 = decoder.ReadUInt32();
                    this.Attributes.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_REVISION_INFO_V1 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Revision);
            encoder.WriteValue(this.SupportedFeatures);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Revision = decoder.ReadUInt32();
            this.SupportedFeatures = decoder.ReadUInt32();
        }
        public uint Revision;
        public uint SupportedFeatures;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_REVISION_INFO : Titanis.DceRpc.IRpcFixedStruct {
        public uint unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.unionSwitch);
            encoder.Align(Titanis.DceRpc.NdrAlignment._4Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.V1, Titanis.DceRpc.NdrAlignment._4Byte);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = decoder.ReadUInt32();
            decoder.Align(Titanis.DceRpc.NdrAlignment._4Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.V1 = decoder.ReadFixedStruct<SAMPR_REVISION_INFO_V1>(Titanis.DceRpc.NdrAlignment._4Byte);
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteStructDeferral(this.V1);
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                decoder.ReadStructDeferral<SAMPR_REVISION_INFO_V1>(ref this.V1);
            }
        }
        public SAMPR_REVISION_INFO_V1 V1;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct USER_DOMAIN_PASSWORD_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.MinPasswordLength);
            encoder.WriteValue(this.PasswordProperties);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.MinPasswordLength = decoder.ReadUInt16();
            this.PasswordProperties = decoder.ReadUInt32();
        }
        public ushort MinPasswordLength;
        public uint PasswordProperties;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public enum DOMAIN_SERVER_ENABLE_STATE : int {
        DomainServerEnabled = 1,
        DomainServerDisabled = 2,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct DOMAIN_STATE_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.DomainServerState)));
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.DomainServerState = ((DOMAIN_SERVER_ENABLE_STATE)(decoder.ReadInt16()));
        }
        public DOMAIN_SERVER_ENABLE_STATE DomainServerState;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public enum DOMAIN_SERVER_ROLE : int {
        DomainServerRoleBackup = 2,
        DomainServerRolePrimary = 3,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct DOMAIN_PASSWORD_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.MinPasswordLength);
            encoder.WriteValue(this.PasswordHistoryLength);
            encoder.WriteValue(this.PasswordProperties);
            encoder.WriteFixedStruct(this.MaxPasswordAge, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.MinPasswordAge, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.MinPasswordLength = decoder.ReadUInt16();
            this.PasswordHistoryLength = decoder.ReadUInt16();
            this.PasswordProperties = decoder.ReadUInt32();
            this.MaxPasswordAge = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.MinPasswordAge = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public ushort MinPasswordLength;
        public ushort PasswordHistoryLength;
        public uint PasswordProperties;
        public OLD_LARGE_INTEGER MaxPasswordAge;
        public OLD_LARGE_INTEGER MinPasswordAge;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.MaxPasswordAge);
            encoder.WriteStructDeferral(this.MinPasswordAge);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.MaxPasswordAge);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.MinPasswordAge);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct DOMAIN_LOGOFF_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.ForceLogoff, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ForceLogoff = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public OLD_LARGE_INTEGER ForceLogoff;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ForceLogoff);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.ForceLogoff);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct DOMAIN_SERVER_ROLE_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.DomainServerRole)));
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.DomainServerRole = ((DOMAIN_SERVER_ROLE)(decoder.ReadInt16()));
        }
        public DOMAIN_SERVER_ROLE DomainServerRole;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct DOMAIN_MODIFIED_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.DomainModifiedCount, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.CreationTime, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.DomainModifiedCount = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.CreationTime = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public OLD_LARGE_INTEGER DomainModifiedCount;
        public OLD_LARGE_INTEGER CreationTime;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.DomainModifiedCount);
            encoder.WriteStructDeferral(this.CreationTime);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.DomainModifiedCount);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.CreationTime);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct DOMAIN_MODIFIED_INFORMATION2 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.DomainModifiedCount, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.CreationTime, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.ModifiedCountAtLastPromotion, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.DomainModifiedCount = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.CreationTime = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.ModifiedCountAtLastPromotion = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public OLD_LARGE_INTEGER DomainModifiedCount;
        public OLD_LARGE_INTEGER CreationTime;
        public OLD_LARGE_INTEGER ModifiedCountAtLastPromotion;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.DomainModifiedCount);
            encoder.WriteStructDeferral(this.CreationTime);
            encoder.WriteStructDeferral(this.ModifiedCountAtLastPromotion);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.DomainModifiedCount);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.CreationTime);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.ModifiedCountAtLastPromotion);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_GENERAL_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.ForceLogoff, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.OemInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.DomainName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.ReplicaSourceNodeName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.DomainModifiedCount, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteValue(this.DomainServerState);
            encoder.WriteValue(this.DomainServerRole);
            encoder.WriteValue(this.UasCompatibilityRequired);
            encoder.WriteValue(this.UserCount);
            encoder.WriteValue(this.GroupCount);
            encoder.WriteValue(this.AliasCount);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ForceLogoff = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.OemInformation = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.DomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.ReplicaSourceNodeName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.DomainModifiedCount = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.DomainServerState = decoder.ReadUInt32();
            this.DomainServerRole = decoder.ReadUInt32();
            this.UasCompatibilityRequired = decoder.ReadUnsignedChar();
            this.UserCount = decoder.ReadUInt32();
            this.GroupCount = decoder.ReadUInt32();
            this.AliasCount = decoder.ReadUInt32();
        }
        public OLD_LARGE_INTEGER ForceLogoff;
        public ms_dtyp.RPC_UNICODE_STRING OemInformation;
        public ms_dtyp.RPC_UNICODE_STRING DomainName;
        public ms_dtyp.RPC_UNICODE_STRING ReplicaSourceNodeName;
        public OLD_LARGE_INTEGER DomainModifiedCount;
        public uint DomainServerState;
        public uint DomainServerRole;
        public byte UasCompatibilityRequired;
        public uint UserCount;
        public uint GroupCount;
        public uint AliasCount;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ForceLogoff);
            encoder.WriteStructDeferral(this.OemInformation);
            encoder.WriteStructDeferral(this.DomainName);
            encoder.WriteStructDeferral(this.ReplicaSourceNodeName);
            encoder.WriteStructDeferral(this.DomainModifiedCount);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.ForceLogoff);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.OemInformation);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DomainName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ReplicaSourceNodeName);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.DomainModifiedCount);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_GENERAL_INFORMATION2 : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.I1, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.LockoutDuration, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.LockoutObservationWindow, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteValue(this.LockoutThreshold);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.I1 = decoder.ReadFixedStruct<SAMPR_DOMAIN_GENERAL_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.LockoutDuration = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.LockoutObservationWindow = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.LockoutThreshold = decoder.ReadUInt16();
        }
        public SAMPR_DOMAIN_GENERAL_INFORMATION I1;
        public ms_dtyp.LARGE_INTEGER LockoutDuration;
        public ms_dtyp.LARGE_INTEGER LockoutObservationWindow;
        public ushort LockoutThreshold;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.I1);
            encoder.WriteStructDeferral(this.LockoutDuration);
            encoder.WriteStructDeferral(this.LockoutObservationWindow);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAMPR_DOMAIN_GENERAL_INFORMATION>(ref this.I1);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LockoutDuration);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LockoutObservationWindow);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_OEM_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.OemInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.OemInformation = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING OemInformation;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.OemInformation);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.OemInformation);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_NAME_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.DomainName, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.DomainName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING DomainName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.DomainName);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.DomainName);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_REPLICATION_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.ReplicaSourceNodeName, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ReplicaSourceNodeName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING ReplicaSourceNodeName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ReplicaSourceNodeName);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ReplicaSourceNodeName);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_LOCKOUT_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.LockoutDuration, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.LockoutObservationWindow, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteValue(this.LockoutThreshold);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.LockoutDuration = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.LockoutObservationWindow = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.LockoutThreshold = decoder.ReadUInt16();
        }
        public ms_dtyp.LARGE_INTEGER LockoutDuration;
        public ms_dtyp.LARGE_INTEGER LockoutObservationWindow;
        public ushort LockoutThreshold;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.LockoutDuration);
            encoder.WriteStructDeferral(this.LockoutObservationWindow);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LockoutDuration);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LockoutObservationWindow);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public enum DOMAIN_INFORMATION_CLASS : int {
        DomainPasswordInformation = 1,
        DomainGeneralInformation = 2,
        DomainLogoffInformation = 3,
        DomainOemInformation = 4,
        DomainNameInformation = 5,
        DomainReplicationInformation = 6,
        DomainServerRoleInformation = 7,
        DomainModifiedInformation = 8,
        DomainStateInformation = 9,
        DomainGeneralInformation2 = 11,
        DomainLockoutInformation = 12,
        DomainModifiedInformation2 = 13,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_INFO_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public DOMAIN_INFORMATION_CLASS unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.unionSwitch)));
            encoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.Password, Titanis.DceRpc.NdrAlignment._4Byte);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteFixedStruct(this.General, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteFixedStruct(this.Logoff, Titanis.DceRpc.NdrAlignment._4Byte);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WriteFixedStruct(this.Oem, Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 7)) {
                                    encoder.WriteFixedStruct(this.Role, Titanis.DceRpc.NdrAlignment._2Byte);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 6)) {
                                        encoder.WriteFixedStruct(this.Replication, Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            encoder.WriteFixedStruct(this.Modified, Titanis.DceRpc.NdrAlignment._4Byte);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                encoder.WriteFixedStruct(this.State, Titanis.DceRpc.NdrAlignment._2Byte);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 11)) {
                                                    encoder.WriteFixedStruct(this.General2, Titanis.DceRpc.NdrAlignment._8Byte);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 12)) {
                                                        encoder.WriteFixedStruct(this.Lockout, Titanis.DceRpc.NdrAlignment._8Byte);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 13)) {
                                                            encoder.WriteFixedStruct(this.Modified2, Titanis.DceRpc.NdrAlignment._4Byte);
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
            this.unionSwitch = ((DOMAIN_INFORMATION_CLASS)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.Password = decoder.ReadFixedStruct<DOMAIN_PASSWORD_INFORMATION>(Titanis.DceRpc.NdrAlignment._4Byte);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.General = decoder.ReadFixedStruct<SAMPR_DOMAIN_GENERAL_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.Logoff = decoder.ReadFixedStruct<DOMAIN_LOGOFF_INFORMATION>(Titanis.DceRpc.NdrAlignment._4Byte);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            this.Oem = decoder.ReadFixedStruct<SAMPR_DOMAIN_OEM_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                this.Name = decoder.ReadFixedStruct<SAMPR_DOMAIN_NAME_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 7)) {
                                    this.Role = decoder.ReadFixedStruct<DOMAIN_SERVER_ROLE_INFORMATION>(Titanis.DceRpc.NdrAlignment._2Byte);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 6)) {
                                        this.Replication = decoder.ReadFixedStruct<SAMPR_DOMAIN_REPLICATION_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            this.Modified = decoder.ReadFixedStruct<DOMAIN_MODIFIED_INFORMATION>(Titanis.DceRpc.NdrAlignment._4Byte);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                this.State = decoder.ReadFixedStruct<DOMAIN_STATE_INFORMATION>(Titanis.DceRpc.NdrAlignment._2Byte);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 11)) {
                                                    this.General2 = decoder.ReadFixedStruct<SAMPR_DOMAIN_GENERAL_INFORMATION2>(Titanis.DceRpc.NdrAlignment._8Byte);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 12)) {
                                                        this.Lockout = decoder.ReadFixedStruct<SAMPR_DOMAIN_LOCKOUT_INFORMATION>(Titanis.DceRpc.NdrAlignment._8Byte);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 13)) {
                                                            this.Modified2 = decoder.ReadFixedStruct<DOMAIN_MODIFIED_INFORMATION2>(Titanis.DceRpc.NdrAlignment._4Byte);
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
                encoder.WriteStructDeferral(this.Password);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteStructDeferral(this.General);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteStructDeferral(this.Logoff);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WriteStructDeferral(this.Oem);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WriteStructDeferral(this.Name);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 7)) {
                                    encoder.WriteStructDeferral(this.Role);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 6)) {
                                        encoder.WriteStructDeferral(this.Replication);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            encoder.WriteStructDeferral(this.Modified);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                encoder.WriteStructDeferral(this.State);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 11)) {
                                                    encoder.WriteStructDeferral(this.General2);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 12)) {
                                                        encoder.WriteStructDeferral(this.Lockout);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 13)) {
                                                            encoder.WriteStructDeferral(this.Modified2);
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
                decoder.ReadStructDeferral<DOMAIN_PASSWORD_INFORMATION>(ref this.Password);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    decoder.ReadStructDeferral<SAMPR_DOMAIN_GENERAL_INFORMATION>(ref this.General);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        decoder.ReadStructDeferral<DOMAIN_LOGOFF_INFORMATION>(ref this.Logoff);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            decoder.ReadStructDeferral<SAMPR_DOMAIN_OEM_INFORMATION>(ref this.Oem);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                decoder.ReadStructDeferral<SAMPR_DOMAIN_NAME_INFORMATION>(ref this.Name);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 7)) {
                                    decoder.ReadStructDeferral<DOMAIN_SERVER_ROLE_INFORMATION>(ref this.Role);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 6)) {
                                        decoder.ReadStructDeferral<SAMPR_DOMAIN_REPLICATION_INFORMATION>(ref this.Replication);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            decoder.ReadStructDeferral<DOMAIN_MODIFIED_INFORMATION>(ref this.Modified);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                decoder.ReadStructDeferral<DOMAIN_STATE_INFORMATION>(ref this.State);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 11)) {
                                                    decoder.ReadStructDeferral<SAMPR_DOMAIN_GENERAL_INFORMATION2>(ref this.General2);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 12)) {
                                                        decoder.ReadStructDeferral<SAMPR_DOMAIN_LOCKOUT_INFORMATION>(ref this.Lockout);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 13)) {
                                                            decoder.ReadStructDeferral<DOMAIN_MODIFIED_INFORMATION2>(ref this.Modified2);
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
        public DOMAIN_PASSWORD_INFORMATION Password;
        public SAMPR_DOMAIN_GENERAL_INFORMATION General;
        public DOMAIN_LOGOFF_INFORMATION Logoff;
        public SAMPR_DOMAIN_OEM_INFORMATION Oem;
        public SAMPR_DOMAIN_NAME_INFORMATION Name;
        public DOMAIN_SERVER_ROLE_INFORMATION Role;
        public SAMPR_DOMAIN_REPLICATION_INFORMATION Replication;
        public DOMAIN_MODIFIED_INFORMATION Modified;
        public DOMAIN_STATE_INFORMATION State;
        public SAMPR_DOMAIN_GENERAL_INFORMATION2 General2;
        public SAMPR_DOMAIN_LOCKOUT_INFORMATION Lockout;
        public DOMAIN_MODIFIED_INFORMATION2 Modified2;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public enum DOMAIN_DISPLAY_INFORMATION : int {
        DomainDisplayUser = 1,
        DomainDisplayMachine = 2,
        DomainDisplayGroup = 3,
        DomainDisplayOemUser = 4,
        DomainDisplayOemGroup = 5,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_DISPLAY_USER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Index);
            encoder.WriteValue(this.Rid);
            encoder.WriteValue(this.AccountControl);
            encoder.WriteFixedStruct(this.AccountName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.FullName, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Index = decoder.ReadUInt32();
            this.Rid = decoder.ReadUInt32();
            this.AccountControl = decoder.ReadUInt32();
            this.AccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public uint Index;
        public uint Rid;
        public uint AccountControl;
        public ms_dtyp.RPC_UNICODE_STRING AccountName;
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public ms_dtyp.RPC_UNICODE_STRING FullName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.AccountName);
            encoder.WriteStructDeferral(this.AdminComment);
            encoder.WriteStructDeferral(this.FullName);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AccountName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_DISPLAY_MACHINE : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Index);
            encoder.WriteValue(this.Rid);
            encoder.WriteValue(this.AccountControl);
            encoder.WriteFixedStruct(this.AccountName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Index = decoder.ReadUInt32();
            this.Rid = decoder.ReadUInt32();
            this.AccountControl = decoder.ReadUInt32();
            this.AccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public uint Index;
        public uint Rid;
        public uint AccountControl;
        public ms_dtyp.RPC_UNICODE_STRING AccountName;
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.AccountName);
            encoder.WriteStructDeferral(this.AdminComment);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AccountName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_DISPLAY_GROUP : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Index);
            encoder.WriteValue(this.Rid);
            encoder.WriteValue(this.Attributes);
            encoder.WriteFixedStruct(this.AccountName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Index = decoder.ReadUInt32();
            this.Rid = decoder.ReadUInt32();
            this.Attributes = decoder.ReadUInt32();
            this.AccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public uint Index;
        public uint Rid;
        public uint Attributes;
        public ms_dtyp.RPC_UNICODE_STRING AccountName;
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.AccountName);
            encoder.WriteStructDeferral(this.AdminComment);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AccountName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_DISPLAY_OEM_USER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Index);
            encoder.WriteFixedStruct(this.OemAccountName, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Index = decoder.ReadUInt32();
            this.OemAccountName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public uint Index;
        public RPC_STRING OemAccountName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.OemAccountName);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<RPC_STRING>(ref this.OemAccountName);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_DISPLAY_OEM_GROUP : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Index);
            encoder.WriteFixedStruct(this.OemAccountName, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Index = decoder.ReadUInt32();
            this.OemAccountName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public uint Index;
        public RPC_STRING OemAccountName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.OemAccountName);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<RPC_STRING>(ref this.OemAccountName);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_DISPLAY_USER_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadPointer<SAMPR_DOMAIN_DISPLAY_USER[]>();
        }
        public uint EntriesRead;
        public RpcPointer<SAMPR_DOMAIN_DISPLAY_USER[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_USER elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_USER elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<SAMPR_DOMAIN_DISPLAY_USER>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_USER elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_USER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_USER elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_USER>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_DISPLAY_MACHINE_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadPointer<SAMPR_DOMAIN_DISPLAY_MACHINE[]>();
        }
        public uint EntriesRead;
        public RpcPointer<SAMPR_DOMAIN_DISPLAY_MACHINE[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_MACHINE elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_MACHINE elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<SAMPR_DOMAIN_DISPLAY_MACHINE>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_MACHINE elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_MACHINE>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_MACHINE elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_MACHINE>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_DISPLAY_GROUP_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadPointer<SAMPR_DOMAIN_DISPLAY_GROUP[]>();
        }
        public uint EntriesRead;
        public RpcPointer<SAMPR_DOMAIN_DISPLAY_GROUP[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_GROUP elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_GROUP elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<SAMPR_DOMAIN_DISPLAY_GROUP>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_GROUP elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_GROUP>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_GROUP elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_GROUP>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_DISPLAY_OEM_USER_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadPointer<SAMPR_DOMAIN_DISPLAY_OEM_USER[]>();
        }
        public uint EntriesRead;
        public RpcPointer<SAMPR_DOMAIN_DISPLAY_OEM_USER[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_OEM_USER elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_OEM_USER elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<SAMPR_DOMAIN_DISPLAY_OEM_USER>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_OEM_USER elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_OEM_USER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_OEM_USER elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_OEM_USER>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DOMAIN_DISPLAY_OEM_GROUP_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.EntriesRead);
            encoder.WritePointer(this.Buffer);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EntriesRead = decoder.ReadUInt32();
            this.Buffer = decoder.ReadPointer<SAMPR_DOMAIN_DISPLAY_OEM_GROUP[]>();
        }
        public uint EntriesRead;
        public RpcPointer<SAMPR_DOMAIN_DISPLAY_OEM_GROUP[]> Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Buffer)) {
                encoder.WriteArrayHeader(this.Buffer.value);
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_OEM_GROUP elem_0 = this.Buffer.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_OEM_GROUP elem_0 = this.Buffer.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Buffer)) {
                this.Buffer.value = decoder.ReadArrayHeader<SAMPR_DOMAIN_DISPLAY_OEM_GROUP>();
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_OEM_GROUP elem_0 = this.Buffer.value[i];
                    elem_0 = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_OEM_GROUP>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.Buffer.value[i] = elem_0;
                }
                for (int i = 0; (i < this.Buffer.value.Length); i++
                ) {
                    SAMPR_DOMAIN_DISPLAY_OEM_GROUP elem_0 = this.Buffer.value[i];
                    decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_OEM_GROUP>(ref elem_0);
                    this.Buffer.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_DISPLAY_INFO_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public DOMAIN_DISPLAY_INFORMATION unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.unionSwitch)));
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.UserInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteFixedStruct(this.MachineInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteFixedStruct(this.GroupInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WriteFixedStruct(this.OemUserInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WriteFixedStruct(this.OemGroupInformation, Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                        }
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = ((DOMAIN_DISPLAY_INFORMATION)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.UserInformation = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_USER_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.MachineInformation = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_MACHINE_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.GroupInformation = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_GROUP_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            this.OemUserInformation = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_OEM_USER_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                this.OemGroupInformation = decoder.ReadFixedStruct<SAMPR_DOMAIN_DISPLAY_OEM_GROUP_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                        }
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteStructDeferral(this.UserInformation);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteStructDeferral(this.MachineInformation);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteStructDeferral(this.GroupInformation);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WriteStructDeferral(this.OemUserInformation);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WriteStructDeferral(this.OemGroupInformation);
                            }
                        }
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_USER_BUFFER>(ref this.UserInformation);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_MACHINE_BUFFER>(ref this.MachineInformation);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_GROUP_BUFFER>(ref this.GroupInformation);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_OEM_USER_BUFFER>(ref this.OemUserInformation);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                decoder.ReadStructDeferral<SAMPR_DOMAIN_DISPLAY_OEM_GROUP_BUFFER>(ref this.OemGroupInformation);
                            }
                        }
                    }
                }
            }
        }
        public SAMPR_DOMAIN_DISPLAY_USER_BUFFER UserInformation;
        public SAMPR_DOMAIN_DISPLAY_MACHINE_BUFFER MachineInformation;
        public SAMPR_DOMAIN_DISPLAY_GROUP_BUFFER GroupInformation;
        public SAMPR_DOMAIN_DISPLAY_OEM_USER_BUFFER OemUserInformation;
        public SAMPR_DOMAIN_DISPLAY_OEM_GROUP_BUFFER OemGroupInformation;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct GROUP_ATTRIBUTE_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Attributes);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Attributes = decoder.ReadUInt32();
        }
        public uint Attributes;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_GROUP_GENERAL_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.Attributes);
            encoder.WriteValue(this.MemberCount);
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.Attributes = decoder.ReadUInt32();
            this.MemberCount = decoder.ReadUInt32();
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public uint Attributes;
        public uint MemberCount;
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
            encoder.WriteStructDeferral(this.AdminComment);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_GROUP_NAME_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_GROUP_ADM_COMMENT_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.AdminComment);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public enum GROUP_INFORMATION_CLASS : int {
        GroupGeneralInformation = 1,
        GroupNameInformation = 2,
        GroupAttributeInformation = 3,
        GroupAdminCommentInformation = 4,
        GroupReplicationInformation = 5,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_GROUP_INFO_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public GROUP_INFORMATION_CLASS unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.unionSwitch)));
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.General, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteFixedStruct(this.Attribute, Titanis.DceRpc.NdrAlignment._4Byte);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WriteFixedStruct(this.DoNotUse, Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                        }
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = ((GROUP_INFORMATION_CLASS)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.General = decoder.ReadFixedStruct<SAMPR_GROUP_GENERAL_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.Name = decoder.ReadFixedStruct<SAMPR_GROUP_NAME_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.Attribute = decoder.ReadFixedStruct<GROUP_ATTRIBUTE_INFORMATION>(Titanis.DceRpc.NdrAlignment._4Byte);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            this.AdminComment = decoder.ReadFixedStruct<SAMPR_GROUP_ADM_COMMENT_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                this.DoNotUse = decoder.ReadFixedStruct<SAMPR_GROUP_GENERAL_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                        }
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteStructDeferral(this.General);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteStructDeferral(this.Name);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteStructDeferral(this.Attribute);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WriteStructDeferral(this.AdminComment);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WriteStructDeferral(this.DoNotUse);
                            }
                        }
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                decoder.ReadStructDeferral<SAMPR_GROUP_GENERAL_INFORMATION>(ref this.General);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    decoder.ReadStructDeferral<SAMPR_GROUP_NAME_INFORMATION>(ref this.Name);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        decoder.ReadStructDeferral<GROUP_ATTRIBUTE_INFORMATION>(ref this.Attribute);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            decoder.ReadStructDeferral<SAMPR_GROUP_ADM_COMMENT_INFORMATION>(ref this.AdminComment);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                decoder.ReadStructDeferral<SAMPR_GROUP_GENERAL_INFORMATION>(ref this.DoNotUse);
                            }
                        }
                    }
                }
            }
        }
        public SAMPR_GROUP_GENERAL_INFORMATION General;
        public SAMPR_GROUP_NAME_INFORMATION Name;
        public GROUP_ATTRIBUTE_INFORMATION Attribute;
        public SAMPR_GROUP_ADM_COMMENT_INFORMATION AdminComment;
        public SAMPR_GROUP_GENERAL_INFORMATION DoNotUse;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_ALIAS_GENERAL_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.MemberCount);
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.MemberCount = decoder.ReadUInt32();
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public uint MemberCount;
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
            encoder.WriteStructDeferral(this.AdminComment);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_ALIAS_NAME_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING Name;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Name);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Name);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_ALIAS_ADM_COMMENT_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.AdminComment);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public enum ALIAS_INFORMATION_CLASS : int {
        AliasGeneralInformation = 1,
        AliasNameInformation = 2,
        AliasAdminCommentInformation = 3,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_ALIAS_INFO_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public ALIAS_INFORMATION_CLASS unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.unionSwitch)));
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.General, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = ((ALIAS_INFORMATION_CLASS)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.General = decoder.ReadFixedStruct<SAMPR_ALIAS_GENERAL_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.Name = decoder.ReadFixedStruct<SAMPR_ALIAS_NAME_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.AdminComment = decoder.ReadFixedStruct<SAMPR_ALIAS_ADM_COMMENT_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteStructDeferral(this.General);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteStructDeferral(this.Name);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteStructDeferral(this.AdminComment);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                decoder.ReadStructDeferral<SAMPR_ALIAS_GENERAL_INFORMATION>(ref this.General);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    decoder.ReadStructDeferral<SAMPR_ALIAS_NAME_INFORMATION>(ref this.Name);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        decoder.ReadStructDeferral<SAMPR_ALIAS_ADM_COMMENT_INFORMATION>(ref this.AdminComment);
                    }
                }
            }
        }
        public SAMPR_ALIAS_GENERAL_INFORMATION General;
        public SAMPR_ALIAS_NAME_INFORMATION Name;
        public SAMPR_ALIAS_ADM_COMMENT_INFORMATION AdminComment;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_ENCRYPTED_USER_PASSWORD : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((this.Buffer == null)) {
                this.Buffer = new byte[516];
            }
            for (int i = 0; (i < 516); i++
            ) {
                byte elem_0 = this.Buffer[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((this.Buffer == null)) {
                this.Buffer = new byte[516];
            }
            for (int i = 0; (i < 516); i++
            ) {
                byte elem_0 = this.Buffer[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.Buffer[i] = elem_0;
            }
        }
        public byte[] Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_ENCRYPTED_USER_PASSWORD_NEW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((this.Buffer == null)) {
                this.Buffer = new byte[532];
            }
            for (int i = 0; (i < 532); i++
            ) {
                byte elem_0 = this.Buffer[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((this.Buffer == null)) {
                this.Buffer = new byte[532];
            }
            for (int i = 0; (i < 532); i++
            ) {
                byte elem_0 = this.Buffer[i];
                elem_0 = decoder.ReadUnsignedChar();
                this.Buffer[i] = elem_0;
            }
        }
        public byte[] Buffer;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct USER_PRIMARY_GROUP_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.PrimaryGroupId);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.PrimaryGroupId = decoder.ReadUInt32();
        }
        public uint PrimaryGroupId;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct USER_CONTROL_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.UserAccountControl);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UserAccountControl = decoder.ReadUInt32();
        }
        public uint UserAccountControl;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct USER_EXPIRES_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.AccountExpires, Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.AccountExpires = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
        }
        public OLD_LARGE_INTEGER AccountExpires;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.AccountExpires);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.AccountExpires);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_LOGON_HOURS : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.UnitsPerWeek);
            encoder.WritePointer(this.LogonHours);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UnitsPerWeek = decoder.ReadUInt16();
            this.LogonHours = decoder.ReadPointer<ArraySegment<byte>>();
        }
        public ushort UnitsPerWeek;
        public RpcPointer<ArraySegment<byte>> LogonHours;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.LogonHours)) {
                encoder.WriteArrayHeader(this.LogonHours.value, true);
                for (int i = 0; (i < this.LogonHours.value.Count); i++
                ) {
                    byte elem_0 = this.LogonHours.value.Item(i);
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.LogonHours)) {
                this.LogonHours.value = decoder.ReadArraySegmentHeader<byte>();
                for (int i = 0; (i < this.LogonHours.value.Count); i++
                ) {
                    byte elem_0 = this.LogonHours.value.Item(i);
                    elem_0 = decoder.ReadUnsignedChar();
                    this.LogonHours.value.Item(i) = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_ALL_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.LastLogon, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.LastLogoff, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.PasswordLastSet, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.AccountExpires, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.PasswordCanChange, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.PasswordMustChange, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.UserName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.FullName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.HomeDirectory, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.HomeDirectoryDrive, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.ScriptPath, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.ProfilePath, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.WorkStations, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.UserComment, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.Parameters, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.LmOwfPassword, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.NtOwfPassword, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.PrivateData, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.SecurityDescriptor, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.UserId);
            encoder.WriteValue(this.PrimaryGroupId);
            encoder.WriteValue(this.UserAccountControl);
            encoder.WriteValue(this.WhichFields);
            encoder.WriteFixedStruct(this.LogonHours, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.BadPasswordCount);
            encoder.WriteValue(this.LogonCount);
            encoder.WriteValue(this.CountryCode);
            encoder.WriteValue(this.CodePage);
            encoder.WriteValue(this.LmPasswordPresent);
            encoder.WriteValue(this.NtPasswordPresent);
            encoder.WriteValue(this.PasswordExpired);
            encoder.WriteValue(this.PrivateDataSensitive);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.LastLogon = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.LastLogoff = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.PasswordLastSet = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.AccountExpires = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.PasswordCanChange = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.PasswordMustChange = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.HomeDirectory = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.HomeDirectoryDrive = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.ScriptPath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.ProfilePath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.WorkStations = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.UserComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.Parameters = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.LmOwfPassword = decoder.ReadFixedStruct<RPC_SHORT_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.NtOwfPassword = decoder.ReadFixedStruct<RPC_SHORT_BLOB>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.PrivateData = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.SecurityDescriptor = decoder.ReadFixedStruct<SAMPR_SR_SECURITY_DESCRIPTOR>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.UserId = decoder.ReadUInt32();
            this.PrimaryGroupId = decoder.ReadUInt32();
            this.UserAccountControl = decoder.ReadUInt32();
            this.WhichFields = decoder.ReadUInt32();
            this.LogonHours = decoder.ReadFixedStruct<SAMPR_LOGON_HOURS>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.BadPasswordCount = decoder.ReadUInt16();
            this.LogonCount = decoder.ReadUInt16();
            this.CountryCode = decoder.ReadUInt16();
            this.CodePage = decoder.ReadUInt16();
            this.LmPasswordPresent = decoder.ReadUnsignedChar();
            this.NtPasswordPresent = decoder.ReadUnsignedChar();
            this.PasswordExpired = decoder.ReadUnsignedChar();
            this.PrivateDataSensitive = decoder.ReadUnsignedChar();
        }
        public OLD_LARGE_INTEGER LastLogon;
        public OLD_LARGE_INTEGER LastLogoff;
        public OLD_LARGE_INTEGER PasswordLastSet;
        public OLD_LARGE_INTEGER AccountExpires;
        public OLD_LARGE_INTEGER PasswordCanChange;
        public OLD_LARGE_INTEGER PasswordMustChange;
        public ms_dtyp.RPC_UNICODE_STRING UserName;
        public ms_dtyp.RPC_UNICODE_STRING FullName;
        public ms_dtyp.RPC_UNICODE_STRING HomeDirectory;
        public ms_dtyp.RPC_UNICODE_STRING HomeDirectoryDrive;
        public ms_dtyp.RPC_UNICODE_STRING ScriptPath;
        public ms_dtyp.RPC_UNICODE_STRING ProfilePath;
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public ms_dtyp.RPC_UNICODE_STRING WorkStations;
        public ms_dtyp.RPC_UNICODE_STRING UserComment;
        public ms_dtyp.RPC_UNICODE_STRING Parameters;
        public RPC_SHORT_BLOB LmOwfPassword;
        public RPC_SHORT_BLOB NtOwfPassword;
        public ms_dtyp.RPC_UNICODE_STRING PrivateData;
        public SAMPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor;
        public uint UserId;
        public uint PrimaryGroupId;
        public uint UserAccountControl;
        public uint WhichFields;
        public SAMPR_LOGON_HOURS LogonHours;
        public ushort BadPasswordCount;
        public ushort LogonCount;
        public ushort CountryCode;
        public ushort CodePage;
        public byte LmPasswordPresent;
        public byte NtPasswordPresent;
        public byte PasswordExpired;
        public byte PrivateDataSensitive;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.LastLogon);
            encoder.WriteStructDeferral(this.LastLogoff);
            encoder.WriteStructDeferral(this.PasswordLastSet);
            encoder.WriteStructDeferral(this.AccountExpires);
            encoder.WriteStructDeferral(this.PasswordCanChange);
            encoder.WriteStructDeferral(this.PasswordMustChange);
            encoder.WriteStructDeferral(this.UserName);
            encoder.WriteStructDeferral(this.FullName);
            encoder.WriteStructDeferral(this.HomeDirectory);
            encoder.WriteStructDeferral(this.HomeDirectoryDrive);
            encoder.WriteStructDeferral(this.ScriptPath);
            encoder.WriteStructDeferral(this.ProfilePath);
            encoder.WriteStructDeferral(this.AdminComment);
            encoder.WriteStructDeferral(this.WorkStations);
            encoder.WriteStructDeferral(this.UserComment);
            encoder.WriteStructDeferral(this.Parameters);
            encoder.WriteStructDeferral(this.LmOwfPassword);
            encoder.WriteStructDeferral(this.NtOwfPassword);
            encoder.WriteStructDeferral(this.PrivateData);
            encoder.WriteStructDeferral(this.SecurityDescriptor);
            encoder.WriteStructDeferral(this.LogonHours);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogon);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogoff);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordLastSet);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.AccountExpires);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordCanChange);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordMustChange);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectory);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectoryDrive);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ScriptPath);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ProfilePath);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.WorkStations);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserComment);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Parameters);
            decoder.ReadStructDeferral<RPC_SHORT_BLOB>(ref this.LmOwfPassword);
            decoder.ReadStructDeferral<RPC_SHORT_BLOB>(ref this.NtOwfPassword);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.PrivateData);
            decoder.ReadStructDeferral<SAMPR_SR_SECURITY_DESCRIPTOR>(ref this.SecurityDescriptor);
            decoder.ReadStructDeferral<SAMPR_LOGON_HOURS>(ref this.LogonHours);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_GENERAL_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.UserName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.FullName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.PrimaryGroupId);
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.UserComment, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.PrimaryGroupId = decoder.ReadUInt32();
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.UserComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING UserName;
        public ms_dtyp.RPC_UNICODE_STRING FullName;
        public uint PrimaryGroupId;
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public ms_dtyp.RPC_UNICODE_STRING UserComment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.UserName);
            encoder.WriteStructDeferral(this.FullName);
            encoder.WriteStructDeferral(this.AdminComment);
            encoder.WriteStructDeferral(this.UserComment);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserComment);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_PREFERENCES_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.UserComment, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.Reserved1, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.CountryCode);
            encoder.WriteValue(this.CodePage);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UserComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.Reserved1 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.CountryCode = decoder.ReadUInt16();
            this.CodePage = decoder.ReadUInt16();
        }
        public ms_dtyp.RPC_UNICODE_STRING UserComment;
        public ms_dtyp.RPC_UNICODE_STRING Reserved1;
        public ushort CountryCode;
        public ushort CodePage;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.UserComment);
            encoder.WriteStructDeferral(this.Reserved1);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserComment);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Reserved1);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_PARAMETERS_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.Parameters, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Parameters = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING Parameters;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.Parameters);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.Parameters);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_LOGON_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.UserName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.FullName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.UserId);
            encoder.WriteValue(this.PrimaryGroupId);
            encoder.WriteFixedStruct(this.HomeDirectory, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.HomeDirectoryDrive, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.ScriptPath, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.ProfilePath, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.WorkStations, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.LastLogon, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.LastLogoff, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.PasswordLastSet, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.PasswordCanChange, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.PasswordMustChange, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.LogonHours, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.BadPasswordCount);
            encoder.WriteValue(this.LogonCount);
            encoder.WriteValue(this.UserAccountControl);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.UserId = decoder.ReadUInt32();
            this.PrimaryGroupId = decoder.ReadUInt32();
            this.HomeDirectory = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.HomeDirectoryDrive = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.ScriptPath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.ProfilePath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.WorkStations = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.LastLogon = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.LastLogoff = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.PasswordLastSet = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.PasswordCanChange = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.PasswordMustChange = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.LogonHours = decoder.ReadFixedStruct<SAMPR_LOGON_HOURS>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.BadPasswordCount = decoder.ReadUInt16();
            this.LogonCount = decoder.ReadUInt16();
            this.UserAccountControl = decoder.ReadUInt32();
        }
        public ms_dtyp.RPC_UNICODE_STRING UserName;
        public ms_dtyp.RPC_UNICODE_STRING FullName;
        public uint UserId;
        public uint PrimaryGroupId;
        public ms_dtyp.RPC_UNICODE_STRING HomeDirectory;
        public ms_dtyp.RPC_UNICODE_STRING HomeDirectoryDrive;
        public ms_dtyp.RPC_UNICODE_STRING ScriptPath;
        public ms_dtyp.RPC_UNICODE_STRING ProfilePath;
        public ms_dtyp.RPC_UNICODE_STRING WorkStations;
        public OLD_LARGE_INTEGER LastLogon;
        public OLD_LARGE_INTEGER LastLogoff;
        public OLD_LARGE_INTEGER PasswordLastSet;
        public OLD_LARGE_INTEGER PasswordCanChange;
        public OLD_LARGE_INTEGER PasswordMustChange;
        public SAMPR_LOGON_HOURS LogonHours;
        public ushort BadPasswordCount;
        public ushort LogonCount;
        public uint UserAccountControl;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.UserName);
            encoder.WriteStructDeferral(this.FullName);
            encoder.WriteStructDeferral(this.HomeDirectory);
            encoder.WriteStructDeferral(this.HomeDirectoryDrive);
            encoder.WriteStructDeferral(this.ScriptPath);
            encoder.WriteStructDeferral(this.ProfilePath);
            encoder.WriteStructDeferral(this.WorkStations);
            encoder.WriteStructDeferral(this.LastLogon);
            encoder.WriteStructDeferral(this.LastLogoff);
            encoder.WriteStructDeferral(this.PasswordLastSet);
            encoder.WriteStructDeferral(this.PasswordCanChange);
            encoder.WriteStructDeferral(this.PasswordMustChange);
            encoder.WriteStructDeferral(this.LogonHours);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectory);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectoryDrive);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ScriptPath);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ProfilePath);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.WorkStations);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogon);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogoff);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordLastSet);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordCanChange);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordMustChange);
            decoder.ReadStructDeferral<SAMPR_LOGON_HOURS>(ref this.LogonHours);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_ACCOUNT_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.UserName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.FullName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.UserId);
            encoder.WriteValue(this.PrimaryGroupId);
            encoder.WriteFixedStruct(this.HomeDirectory, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.HomeDirectoryDrive, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.ScriptPath, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.ProfilePath, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.WorkStations, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.LastLogon, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.LastLogoff, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.LogonHours, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.BadPasswordCount);
            encoder.WriteValue(this.LogonCount);
            encoder.WriteFixedStruct(this.PasswordLastSet, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteFixedStruct(this.AccountExpires, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteValue(this.UserAccountControl);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.UserId = decoder.ReadUInt32();
            this.PrimaryGroupId = decoder.ReadUInt32();
            this.HomeDirectory = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.HomeDirectoryDrive = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.ScriptPath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.ProfilePath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.WorkStations = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.LastLogon = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.LastLogoff = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.LogonHours = decoder.ReadFixedStruct<SAMPR_LOGON_HOURS>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.BadPasswordCount = decoder.ReadUInt16();
            this.LogonCount = decoder.ReadUInt16();
            this.PasswordLastSet = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.AccountExpires = decoder.ReadFixedStruct<OLD_LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._4Byte);
            this.UserAccountControl = decoder.ReadUInt32();
        }
        public ms_dtyp.RPC_UNICODE_STRING UserName;
        public ms_dtyp.RPC_UNICODE_STRING FullName;
        public uint UserId;
        public uint PrimaryGroupId;
        public ms_dtyp.RPC_UNICODE_STRING HomeDirectory;
        public ms_dtyp.RPC_UNICODE_STRING HomeDirectoryDrive;
        public ms_dtyp.RPC_UNICODE_STRING ScriptPath;
        public ms_dtyp.RPC_UNICODE_STRING ProfilePath;
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public ms_dtyp.RPC_UNICODE_STRING WorkStations;
        public OLD_LARGE_INTEGER LastLogon;
        public OLD_LARGE_INTEGER LastLogoff;
        public SAMPR_LOGON_HOURS LogonHours;
        public ushort BadPasswordCount;
        public ushort LogonCount;
        public OLD_LARGE_INTEGER PasswordLastSet;
        public OLD_LARGE_INTEGER AccountExpires;
        public uint UserAccountControl;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.UserName);
            encoder.WriteStructDeferral(this.FullName);
            encoder.WriteStructDeferral(this.HomeDirectory);
            encoder.WriteStructDeferral(this.HomeDirectoryDrive);
            encoder.WriteStructDeferral(this.ScriptPath);
            encoder.WriteStructDeferral(this.ProfilePath);
            encoder.WriteStructDeferral(this.AdminComment);
            encoder.WriteStructDeferral(this.WorkStations);
            encoder.WriteStructDeferral(this.LastLogon);
            encoder.WriteStructDeferral(this.LastLogoff);
            encoder.WriteStructDeferral(this.LogonHours);
            encoder.WriteStructDeferral(this.PasswordLastSet);
            encoder.WriteStructDeferral(this.AccountExpires);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectory);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectoryDrive);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ScriptPath);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ProfilePath);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.WorkStations);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogon);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.LastLogoff);
            decoder.ReadStructDeferral<SAMPR_LOGON_HOURS>(ref this.LogonHours);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.PasswordLastSet);
            decoder.ReadStructDeferral<OLD_LARGE_INTEGER>(ref this.AccountExpires);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_A_NAME_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.UserName, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING UserName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.UserName);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_F_NAME_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.FullName, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING FullName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.FullName);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_NAME_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.UserName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.FullName, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.FullName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING UserName;
        public ms_dtyp.RPC_UNICODE_STRING FullName;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.UserName);
            encoder.WriteStructDeferral(this.FullName);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserName);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.FullName);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_HOME_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.HomeDirectory, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.HomeDirectoryDrive, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.HomeDirectory = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.HomeDirectoryDrive = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING HomeDirectory;
        public ms_dtyp.RPC_UNICODE_STRING HomeDirectoryDrive;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.HomeDirectory);
            encoder.WriteStructDeferral(this.HomeDirectoryDrive);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectory);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.HomeDirectoryDrive);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_SCRIPT_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.ScriptPath, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ScriptPath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING ScriptPath;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ScriptPath);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ScriptPath);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_PROFILE_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.ProfilePath, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ProfilePath = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING ProfilePath;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ProfilePath);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ProfilePath);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_ADMIN_COMMENT_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.AdminComment = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING AdminComment;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.AdminComment);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.AdminComment);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_WORKSTATIONS_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.WorkStations, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.WorkStations = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public ms_dtyp.RPC_UNICODE_STRING WorkStations;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.WorkStations);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.WorkStations);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_LOGON_HOURS_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.LogonHours, Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.LogonHours = decoder.ReadFixedStruct<SAMPR_LOGON_HOURS>(Titanis.DceRpc.NdrAlignment.NativePtr);
        }
        public SAMPR_LOGON_HOURS LogonHours;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.LogonHours);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAMPR_LOGON_HOURS>(ref this.LogonHours);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_INTERNAL1_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.EncryptedNtOwfPassword, Titanis.DceRpc.NdrAlignment._1Byte);
            encoder.WriteFixedStruct(this.EncryptedLmOwfPassword, Titanis.DceRpc.NdrAlignment._1Byte);
            encoder.WriteValue(this.NtPasswordPresent);
            encoder.WriteValue(this.LmPasswordPresent);
            encoder.WriteValue(this.PasswordExpired);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.EncryptedNtOwfPassword = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
            this.EncryptedLmOwfPassword = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
            this.NtPasswordPresent = decoder.ReadUnsignedChar();
            this.LmPasswordPresent = decoder.ReadUnsignedChar();
            this.PasswordExpired = decoder.ReadUnsignedChar();
        }
        public ENCRYPTED_LM_OWF_PASSWORD EncryptedNtOwfPassword;
        public ENCRYPTED_LM_OWF_PASSWORD EncryptedLmOwfPassword;
        public byte NtPasswordPresent;
        public byte LmPasswordPresent;
        public byte PasswordExpired;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.EncryptedNtOwfPassword);
            encoder.WriteStructDeferral(this.EncryptedLmOwfPassword);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref this.EncryptedNtOwfPassword);
            decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref this.EncryptedLmOwfPassword);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_INTERNAL4_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.I1, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.UserPassword, Titanis.DceRpc.NdrAlignment._1Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.I1 = decoder.ReadFixedStruct<SAMPR_USER_ALL_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.UserPassword = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
        }
        public SAMPR_USER_ALL_INFORMATION I1;
        public SAMPR_ENCRYPTED_USER_PASSWORD UserPassword;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.I1);
            encoder.WriteStructDeferral(this.UserPassword);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAMPR_USER_ALL_INFORMATION>(ref this.I1);
            decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD>(ref this.UserPassword);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_INTERNAL4_INFORMATION_NEW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.I1, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.UserPassword, Titanis.DceRpc.NdrAlignment._1Byte);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.I1 = decoder.ReadFixedStruct<SAMPR_USER_ALL_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.UserPassword = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD_NEW>(Titanis.DceRpc.NdrAlignment._1Byte);
        }
        public SAMPR_USER_ALL_INFORMATION I1;
        public SAMPR_ENCRYPTED_USER_PASSWORD_NEW UserPassword;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.I1);
            encoder.WriteStructDeferral(this.UserPassword);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAMPR_USER_ALL_INFORMATION>(ref this.I1);
            decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD_NEW>(ref this.UserPassword);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_INTERNAL5_INFORMATION : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.UserPassword, Titanis.DceRpc.NdrAlignment._1Byte);
            encoder.WriteValue(this.PasswordExpired);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UserPassword = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
            this.PasswordExpired = decoder.ReadUnsignedChar();
        }
        public SAMPR_ENCRYPTED_USER_PASSWORD UserPassword;
        public byte PasswordExpired;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.UserPassword);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD>(ref this.UserPassword);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_INTERNAL5_INFORMATION_NEW : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.UserPassword, Titanis.DceRpc.NdrAlignment._1Byte);
            encoder.WriteValue(this.PasswordExpired);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.UserPassword = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD_NEW>(Titanis.DceRpc.NdrAlignment._1Byte);
            this.PasswordExpired = decoder.ReadUnsignedChar();
        }
        public SAMPR_ENCRYPTED_USER_PASSWORD_NEW UserPassword;
        public byte PasswordExpired;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.UserPassword);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD_NEW>(ref this.UserPassword);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public enum USER_INFORMATION_CLASS : int {
        UserGeneralInformation = 1,
        UserPreferencesInformation = 2,
        UserLogonInformation = 3,
        UserLogonHoursInformation = 4,
        UserAccountInformation = 5,
        UserNameInformation = 6,
        UserAccountNameInformation = 7,
        UserFullNameInformation = 8,
        UserPrimaryGroupInformation = 9,
        UserHomeInformation = 10,
        UserScriptInformation = 11,
        UserProfileInformation = 12,
        UserAdminCommentInformation = 13,
        UserWorkStationsInformation = 14,
        UserControlInformation = 16,
        UserExpiresInformation = 17,
        UserInternal1Information = 18,
        UserParametersInformation = 20,
        UserAllInformation = 21,
        UserInternal4Information = 23,
        UserInternal5Information = 24,
        UserInternal4InformationNew = 25,
        UserInternal5InformationNew = 26,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAMPR_USER_INFO_BUFFER : Titanis.DceRpc.IRpcFixedStruct {
        public USER_INFORMATION_CLASS unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.unionSwitch)));
            encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.General, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteFixedStruct(this.Preferences, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteFixedStruct(this.Logon, Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WriteFixedStruct(this.LogonHours, Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WriteFixedStruct(this.Account, Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    encoder.WriteFixedStruct(this.Name, Titanis.DceRpc.NdrAlignment.NativePtr);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        encoder.WriteFixedStruct(this.AccountName, Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            encoder.WriteFixedStruct(this.FullName, Titanis.DceRpc.NdrAlignment.NativePtr);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                encoder.WriteFixedStruct(this.PrimaryGroup, Titanis.DceRpc.NdrAlignment._4Byte);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 10)) {
                                                    encoder.WriteFixedStruct(this.Home, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 11)) {
                                                        encoder.WriteFixedStruct(this.Script, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 12)) {
                                                            encoder.WriteFixedStruct(this.Profile, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 13)) {
                                                                encoder.WriteFixedStruct(this.AdminComment, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 14)) {
                                                                    encoder.WriteFixedStruct(this.WorkStations, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                }
                                                                else {
                                                                    if ((((int)(this.unionSwitch)) == 16)) {
                                                                        encoder.WriteFixedStruct(this.Control, Titanis.DceRpc.NdrAlignment._4Byte);
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.unionSwitch)) == 17)) {
                                                                            encoder.WriteFixedStruct(this.Expires, Titanis.DceRpc.NdrAlignment._4Byte);
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.unionSwitch)) == 18)) {
                                                                                encoder.WriteFixedStruct(this.Internal1, Titanis.DceRpc.NdrAlignment._1Byte);
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.unionSwitch)) == 20)) {
                                                                                    encoder.WriteFixedStruct(this.Parameters, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.unionSwitch)) == 21)) {
                                                                                        encoder.WriteFixedStruct(this.All, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                    }
                                                                                    else {
                                                                                        if ((((int)(this.unionSwitch)) == 23)) {
                                                                                            encoder.WriteFixedStruct(this.Internal4, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                        }
                                                                                        else {
                                                                                            if ((((int)(this.unionSwitch)) == 24)) {
                                                                                                encoder.WriteFixedStruct(this.Internal5, Titanis.DceRpc.NdrAlignment._1Byte);
                                                                                            }
                                                                                            else {
                                                                                                if ((((int)(this.unionSwitch)) == 25)) {
                                                                                                    encoder.WriteFixedStruct(this.Internal4New, Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                                }
                                                                                                else {
                                                                                                    if ((((int)(this.unionSwitch)) == 26)) {
                                                                                                        encoder.WriteFixedStruct(this.Internal5New, Titanis.DceRpc.NdrAlignment._1Byte);
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
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = ((USER_INFORMATION_CLASS)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.General = decoder.ReadFixedStruct<SAMPR_USER_GENERAL_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.Preferences = decoder.ReadFixedStruct<SAMPR_USER_PREFERENCES_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.Logon = decoder.ReadFixedStruct<SAMPR_USER_LOGON_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            this.LogonHours = decoder.ReadFixedStruct<SAMPR_USER_LOGON_HOURS_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                this.Account = decoder.ReadFixedStruct<SAMPR_USER_ACCOUNT_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    this.Name = decoder.ReadFixedStruct<SAMPR_USER_NAME_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        this.AccountName = decoder.ReadFixedStruct<SAMPR_USER_A_NAME_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            this.FullName = decoder.ReadFixedStruct<SAMPR_USER_F_NAME_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                this.PrimaryGroup = decoder.ReadFixedStruct<USER_PRIMARY_GROUP_INFORMATION>(Titanis.DceRpc.NdrAlignment._4Byte);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 10)) {
                                                    this.Home = decoder.ReadFixedStruct<SAMPR_USER_HOME_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 11)) {
                                                        this.Script = decoder.ReadFixedStruct<SAMPR_USER_SCRIPT_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 12)) {
                                                            this.Profile = decoder.ReadFixedStruct<SAMPR_USER_PROFILE_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 13)) {
                                                                this.AdminComment = decoder.ReadFixedStruct<SAMPR_USER_ADMIN_COMMENT_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 14)) {
                                                                    this.WorkStations = decoder.ReadFixedStruct<SAMPR_USER_WORKSTATIONS_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                }
                                                                else {
                                                                    if ((((int)(this.unionSwitch)) == 16)) {
                                                                        this.Control = decoder.ReadFixedStruct<USER_CONTROL_INFORMATION>(Titanis.DceRpc.NdrAlignment._4Byte);
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.unionSwitch)) == 17)) {
                                                                            this.Expires = decoder.ReadFixedStruct<USER_EXPIRES_INFORMATION>(Titanis.DceRpc.NdrAlignment._4Byte);
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.unionSwitch)) == 18)) {
                                                                                this.Internal1 = decoder.ReadFixedStruct<SAMPR_USER_INTERNAL1_INFORMATION>(Titanis.DceRpc.NdrAlignment._1Byte);
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.unionSwitch)) == 20)) {
                                                                                    this.Parameters = decoder.ReadFixedStruct<SAMPR_USER_PARAMETERS_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.unionSwitch)) == 21)) {
                                                                                        this.All = decoder.ReadFixedStruct<SAMPR_USER_ALL_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                    }
                                                                                    else {
                                                                                        if ((((int)(this.unionSwitch)) == 23)) {
                                                                                            this.Internal4 = decoder.ReadFixedStruct<SAMPR_USER_INTERNAL4_INFORMATION>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                        }
                                                                                        else {
                                                                                            if ((((int)(this.unionSwitch)) == 24)) {
                                                                                                this.Internal5 = decoder.ReadFixedStruct<SAMPR_USER_INTERNAL5_INFORMATION>(Titanis.DceRpc.NdrAlignment._1Byte);
                                                                                            }
                                                                                            else {
                                                                                                if ((((int)(this.unionSwitch)) == 25)) {
                                                                                                    this.Internal4New = decoder.ReadFixedStruct<SAMPR_USER_INTERNAL4_INFORMATION_NEW>(Titanis.DceRpc.NdrAlignment.NativePtr);
                                                                                                }
                                                                                                else {
                                                                                                    if ((((int)(this.unionSwitch)) == 26)) {
                                                                                                        this.Internal5New = decoder.ReadFixedStruct<SAMPR_USER_INTERNAL5_INFORMATION_NEW>(Titanis.DceRpc.NdrAlignment._1Byte);
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
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteStructDeferral(this.General);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteStructDeferral(this.Preferences);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteStructDeferral(this.Logon);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            encoder.WriteStructDeferral(this.LogonHours);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                encoder.WriteStructDeferral(this.Account);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    encoder.WriteStructDeferral(this.Name);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        encoder.WriteStructDeferral(this.AccountName);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            encoder.WriteStructDeferral(this.FullName);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                encoder.WriteStructDeferral(this.PrimaryGroup);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 10)) {
                                                    encoder.WriteStructDeferral(this.Home);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 11)) {
                                                        encoder.WriteStructDeferral(this.Script);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 12)) {
                                                            encoder.WriteStructDeferral(this.Profile);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 13)) {
                                                                encoder.WriteStructDeferral(this.AdminComment);
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 14)) {
                                                                    encoder.WriteStructDeferral(this.WorkStations);
                                                                }
                                                                else {
                                                                    if ((((int)(this.unionSwitch)) == 16)) {
                                                                        encoder.WriteStructDeferral(this.Control);
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.unionSwitch)) == 17)) {
                                                                            encoder.WriteStructDeferral(this.Expires);
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.unionSwitch)) == 18)) {
                                                                                encoder.WriteStructDeferral(this.Internal1);
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.unionSwitch)) == 20)) {
                                                                                    encoder.WriteStructDeferral(this.Parameters);
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.unionSwitch)) == 21)) {
                                                                                        encoder.WriteStructDeferral(this.All);
                                                                                    }
                                                                                    else {
                                                                                        if ((((int)(this.unionSwitch)) == 23)) {
                                                                                            encoder.WriteStructDeferral(this.Internal4);
                                                                                        }
                                                                                        else {
                                                                                            if ((((int)(this.unionSwitch)) == 24)) {
                                                                                                encoder.WriteStructDeferral(this.Internal5);
                                                                                            }
                                                                                            else {
                                                                                                if ((((int)(this.unionSwitch)) == 25)) {
                                                                                                    encoder.WriteStructDeferral(this.Internal4New);
                                                                                                }
                                                                                                else {
                                                                                                    if ((((int)(this.unionSwitch)) == 26)) {
                                                                                                        encoder.WriteStructDeferral(this.Internal5New);
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
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                decoder.ReadStructDeferral<SAMPR_USER_GENERAL_INFORMATION>(ref this.General);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    decoder.ReadStructDeferral<SAMPR_USER_PREFERENCES_INFORMATION>(ref this.Preferences);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        decoder.ReadStructDeferral<SAMPR_USER_LOGON_INFORMATION>(ref this.Logon);
                    }
                    else {
                        if ((((int)(this.unionSwitch)) == 4)) {
                            decoder.ReadStructDeferral<SAMPR_USER_LOGON_HOURS_INFORMATION>(ref this.LogonHours);
                        }
                        else {
                            if ((((int)(this.unionSwitch)) == 5)) {
                                decoder.ReadStructDeferral<SAMPR_USER_ACCOUNT_INFORMATION>(ref this.Account);
                            }
                            else {
                                if ((((int)(this.unionSwitch)) == 6)) {
                                    decoder.ReadStructDeferral<SAMPR_USER_NAME_INFORMATION>(ref this.Name);
                                }
                                else {
                                    if ((((int)(this.unionSwitch)) == 7)) {
                                        decoder.ReadStructDeferral<SAMPR_USER_A_NAME_INFORMATION>(ref this.AccountName);
                                    }
                                    else {
                                        if ((((int)(this.unionSwitch)) == 8)) {
                                            decoder.ReadStructDeferral<SAMPR_USER_F_NAME_INFORMATION>(ref this.FullName);
                                        }
                                        else {
                                            if ((((int)(this.unionSwitch)) == 9)) {
                                                decoder.ReadStructDeferral<USER_PRIMARY_GROUP_INFORMATION>(ref this.PrimaryGroup);
                                            }
                                            else {
                                                if ((((int)(this.unionSwitch)) == 10)) {
                                                    decoder.ReadStructDeferral<SAMPR_USER_HOME_INFORMATION>(ref this.Home);
                                                }
                                                else {
                                                    if ((((int)(this.unionSwitch)) == 11)) {
                                                        decoder.ReadStructDeferral<SAMPR_USER_SCRIPT_INFORMATION>(ref this.Script);
                                                    }
                                                    else {
                                                        if ((((int)(this.unionSwitch)) == 12)) {
                                                            decoder.ReadStructDeferral<SAMPR_USER_PROFILE_INFORMATION>(ref this.Profile);
                                                        }
                                                        else {
                                                            if ((((int)(this.unionSwitch)) == 13)) {
                                                                decoder.ReadStructDeferral<SAMPR_USER_ADMIN_COMMENT_INFORMATION>(ref this.AdminComment);
                                                            }
                                                            else {
                                                                if ((((int)(this.unionSwitch)) == 14)) {
                                                                    decoder.ReadStructDeferral<SAMPR_USER_WORKSTATIONS_INFORMATION>(ref this.WorkStations);
                                                                }
                                                                else {
                                                                    if ((((int)(this.unionSwitch)) == 16)) {
                                                                        decoder.ReadStructDeferral<USER_CONTROL_INFORMATION>(ref this.Control);
                                                                    }
                                                                    else {
                                                                        if ((((int)(this.unionSwitch)) == 17)) {
                                                                            decoder.ReadStructDeferral<USER_EXPIRES_INFORMATION>(ref this.Expires);
                                                                        }
                                                                        else {
                                                                            if ((((int)(this.unionSwitch)) == 18)) {
                                                                                decoder.ReadStructDeferral<SAMPR_USER_INTERNAL1_INFORMATION>(ref this.Internal1);
                                                                            }
                                                                            else {
                                                                                if ((((int)(this.unionSwitch)) == 20)) {
                                                                                    decoder.ReadStructDeferral<SAMPR_USER_PARAMETERS_INFORMATION>(ref this.Parameters);
                                                                                }
                                                                                else {
                                                                                    if ((((int)(this.unionSwitch)) == 21)) {
                                                                                        decoder.ReadStructDeferral<SAMPR_USER_ALL_INFORMATION>(ref this.All);
                                                                                    }
                                                                                    else {
                                                                                        if ((((int)(this.unionSwitch)) == 23)) {
                                                                                            decoder.ReadStructDeferral<SAMPR_USER_INTERNAL4_INFORMATION>(ref this.Internal4);
                                                                                        }
                                                                                        else {
                                                                                            if ((((int)(this.unionSwitch)) == 24)) {
                                                                                                decoder.ReadStructDeferral<SAMPR_USER_INTERNAL5_INFORMATION>(ref this.Internal5);
                                                                                            }
                                                                                            else {
                                                                                                if ((((int)(this.unionSwitch)) == 25)) {
                                                                                                    decoder.ReadStructDeferral<SAMPR_USER_INTERNAL4_INFORMATION_NEW>(ref this.Internal4New);
                                                                                                }
                                                                                                else {
                                                                                                    if ((((int)(this.unionSwitch)) == 26)) {
                                                                                                        decoder.ReadStructDeferral<SAMPR_USER_INTERNAL5_INFORMATION_NEW>(ref this.Internal5New);
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
                }
            }
        }
        public SAMPR_USER_GENERAL_INFORMATION General;
        public SAMPR_USER_PREFERENCES_INFORMATION Preferences;
        public SAMPR_USER_LOGON_INFORMATION Logon;
        public SAMPR_USER_LOGON_HOURS_INFORMATION LogonHours;
        public SAMPR_USER_ACCOUNT_INFORMATION Account;
        public SAMPR_USER_NAME_INFORMATION Name;
        public SAMPR_USER_A_NAME_INFORMATION AccountName;
        public SAMPR_USER_F_NAME_INFORMATION FullName;
        public USER_PRIMARY_GROUP_INFORMATION PrimaryGroup;
        public SAMPR_USER_HOME_INFORMATION Home;
        public SAMPR_USER_SCRIPT_INFORMATION Script;
        public SAMPR_USER_PROFILE_INFORMATION Profile;
        public SAMPR_USER_ADMIN_COMMENT_INFORMATION AdminComment;
        public SAMPR_USER_WORKSTATIONS_INFORMATION WorkStations;
        public USER_CONTROL_INFORMATION Control;
        public USER_EXPIRES_INFORMATION Expires;
        public SAMPR_USER_INTERNAL1_INFORMATION Internal1;
        public SAMPR_USER_PARAMETERS_INFORMATION Parameters;
        public SAMPR_USER_ALL_INFORMATION All;
        public SAMPR_USER_INTERNAL4_INFORMATION Internal4;
        public SAMPR_USER_INTERNAL5_INFORMATION Internal5;
        public SAMPR_USER_INTERNAL4_INFORMATION_NEW Internal4New;
        public SAMPR_USER_INTERNAL5_INFORMATION_NEW Internal5New;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public enum PASSWORD_POLICY_VALIDATION_TYPE : int {
        SamValidateAuthentication = 1,
        SamValidatePasswordChange = 2,
        SamValidatePasswordReset = 3,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAM_VALIDATE_PASSWORD_HASH : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.Length);
            encoder.WritePointer(this.Hash);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.Length = decoder.ReadUInt32();
            this.Hash = decoder.ReadPointer<byte[]>();
        }
        public uint Length;
        public RpcPointer<byte[]> Hash;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((null != this.Hash)) {
                encoder.WriteArrayHeader(this.Hash.value);
                for (int i = 0; (i < this.Hash.value.Length); i++
                ) {
                    byte elem_0 = this.Hash.value[i];
                    encoder.WriteValue(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((null != this.Hash)) {
                this.Hash.value = decoder.ReadArrayHeader<byte>();
                for (int i = 0; (i < this.Hash.value.Length); i++
                ) {
                    byte elem_0 = this.Hash.value[i];
                    elem_0 = decoder.ReadUnsignedChar();
                    this.Hash.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAM_VALIDATE_PERSISTED_FIELDS : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.PresentFields);
            encoder.WriteFixedStruct(this.PasswordLastSet, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.BadPasswordTime, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.LockoutTime, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteValue(this.BadPasswordCount);
            encoder.WriteValue(this.PasswordHistoryLength);
            encoder.WritePointer(this.PasswordHistory);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.PresentFields = decoder.ReadUInt32();
            this.PasswordLastSet = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.BadPasswordTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.LockoutTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.BadPasswordCount = decoder.ReadUInt32();
            this.PasswordHistoryLength = decoder.ReadUInt32();
            this.PasswordHistory = decoder.ReadPointer<SAM_VALIDATE_PASSWORD_HASH[]>();
        }
        public uint PresentFields;
        public ms_dtyp.LARGE_INTEGER PasswordLastSet;
        public ms_dtyp.LARGE_INTEGER BadPasswordTime;
        public ms_dtyp.LARGE_INTEGER LockoutTime;
        public uint BadPasswordCount;
        public uint PasswordHistoryLength;
        public RpcPointer<SAM_VALIDATE_PASSWORD_HASH[]> PasswordHistory;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.PasswordLastSet);
            encoder.WriteStructDeferral(this.BadPasswordTime);
            encoder.WriteStructDeferral(this.LockoutTime);
            if ((null != this.PasswordHistory)) {
                encoder.WriteArrayHeader(this.PasswordHistory.value);
                for (int i = 0; (i < this.PasswordHistory.value.Length); i++
                ) {
                    SAM_VALIDATE_PASSWORD_HASH elem_0 = this.PasswordHistory.value[i];
                    encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
                }
                for (int i = 0; (i < this.PasswordHistory.value.Length); i++
                ) {
                    SAM_VALIDATE_PASSWORD_HASH elem_0 = this.PasswordHistory.value[i];
                    encoder.WriteStructDeferral(elem_0);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.PasswordLastSet);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.BadPasswordTime);
            decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.LockoutTime);
            if ((null != this.PasswordHistory)) {
                this.PasswordHistory.value = decoder.ReadArrayHeader<SAM_VALIDATE_PASSWORD_HASH>();
                for (int i = 0; (i < this.PasswordHistory.value.Length); i++
                ) {
                    SAM_VALIDATE_PASSWORD_HASH elem_0 = this.PasswordHistory.value[i];
                    elem_0 = decoder.ReadFixedStruct<SAM_VALIDATE_PASSWORD_HASH>(Titanis.DceRpc.NdrAlignment.NativePtr);
                    this.PasswordHistory.value[i] = elem_0;
                }
                for (int i = 0; (i < this.PasswordHistory.value.Length); i++
                ) {
                    SAM_VALIDATE_PASSWORD_HASH elem_0 = this.PasswordHistory.value[i];
                    decoder.ReadStructDeferral<SAM_VALIDATE_PASSWORD_HASH>(ref elem_0);
                    this.PasswordHistory.value[i] = elem_0;
                }
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public enum SAM_VALIDATE_VALIDATION_STATUS : int {
        SamValidateSuccess = 0,
        SamValidatePasswordMustChange = 1,
        SamValidateAccountLockedOut = 2,
        SamValidatePasswordExpired = 3,
        SamValidatePasswordIncorrect = 4,
        SamValidatePasswordIsInHistory = 5,
        SamValidatePasswordTooShort = 6,
        SamValidatePasswordTooLong = 7,
        SamValidatePasswordNotComplexEnough = 8,
        SamValidatePasswordTooRecent = 9,
        SamValidatePasswordFilterError = 10,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAM_VALIDATE_STANDARD_OUTPUT_ARG : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.ChangedPersistedFields, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteValue(((short)(this.ValidationStatus)));
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.ChangedPersistedFields = decoder.ReadFixedStruct<SAM_VALIDATE_PERSISTED_FIELDS>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.ValidationStatus = ((SAM_VALIDATE_VALIDATION_STATUS)(decoder.ReadInt16()));
        }
        public SAM_VALIDATE_PERSISTED_FIELDS ChangedPersistedFields;
        public SAM_VALIDATE_VALIDATION_STATUS ValidationStatus;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.ChangedPersistedFields);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAM_VALIDATE_PERSISTED_FIELDS>(ref this.ChangedPersistedFields);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAM_VALIDATE_AUTHENTICATION_INPUT_ARG : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.InputPersistedFields, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteValue(this.PasswordMatched);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.InputPersistedFields = decoder.ReadFixedStruct<SAM_VALIDATE_PERSISTED_FIELDS>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.PasswordMatched = decoder.ReadUnsignedChar();
        }
        public SAM_VALIDATE_PERSISTED_FIELDS InputPersistedFields;
        public byte PasswordMatched;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.InputPersistedFields);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAM_VALIDATE_PERSISTED_FIELDS>(ref this.InputPersistedFields);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAM_VALIDATE_PASSWORD_CHANGE_INPUT_ARG : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.InputPersistedFields, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.ClearPassword, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.UserAccountName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.HashedPassword, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.PasswordMatch);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.InputPersistedFields = decoder.ReadFixedStruct<SAM_VALIDATE_PERSISTED_FIELDS>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.ClearPassword = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.UserAccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.HashedPassword = decoder.ReadFixedStruct<SAM_VALIDATE_PASSWORD_HASH>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.PasswordMatch = decoder.ReadUnsignedChar();
        }
        public SAM_VALIDATE_PERSISTED_FIELDS InputPersistedFields;
        public ms_dtyp.RPC_UNICODE_STRING ClearPassword;
        public ms_dtyp.RPC_UNICODE_STRING UserAccountName;
        public SAM_VALIDATE_PASSWORD_HASH HashedPassword;
        public byte PasswordMatch;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.InputPersistedFields);
            encoder.WriteStructDeferral(this.ClearPassword);
            encoder.WriteStructDeferral(this.UserAccountName);
            encoder.WriteStructDeferral(this.HashedPassword);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAM_VALIDATE_PERSISTED_FIELDS>(ref this.InputPersistedFields);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ClearPassword);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserAccountName);
            decoder.ReadStructDeferral<SAM_VALIDATE_PASSWORD_HASH>(ref this.HashedPassword);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAM_VALIDATE_PASSWORD_RESET_INPUT_ARG : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteFixedStruct(this.InputPersistedFields, Titanis.DceRpc.NdrAlignment._8Byte);
            encoder.WriteFixedStruct(this.ClearPassword, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.UserAccountName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteFixedStruct(this.HashedPassword, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteValue(this.PasswordMustChangeAtNextLogon);
            encoder.WriteValue(this.ClearLockout);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.InputPersistedFields = decoder.ReadFixedStruct<SAM_VALIDATE_PERSISTED_FIELDS>(Titanis.DceRpc.NdrAlignment._8Byte);
            this.ClearPassword = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.UserAccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.HashedPassword = decoder.ReadFixedStruct<SAM_VALIDATE_PASSWORD_HASH>(Titanis.DceRpc.NdrAlignment.NativePtr);
            this.PasswordMustChangeAtNextLogon = decoder.ReadUnsignedChar();
            this.ClearLockout = decoder.ReadUnsignedChar();
        }
        public SAM_VALIDATE_PERSISTED_FIELDS InputPersistedFields;
        public ms_dtyp.RPC_UNICODE_STRING ClearPassword;
        public ms_dtyp.RPC_UNICODE_STRING UserAccountName;
        public SAM_VALIDATE_PASSWORD_HASH HashedPassword;
        public byte PasswordMustChangeAtNextLogon;
        public byte ClearLockout;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteStructDeferral(this.InputPersistedFields);
            encoder.WriteStructDeferral(this.ClearPassword);
            encoder.WriteStructDeferral(this.UserAccountName);
            encoder.WriteStructDeferral(this.HashedPassword);
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            decoder.ReadStructDeferral<SAM_VALIDATE_PERSISTED_FIELDS>(ref this.InputPersistedFields);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ClearPassword);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.UserAccountName);
            decoder.ReadStructDeferral<SAM_VALIDATE_PASSWORD_HASH>(ref this.HashedPassword);
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAM_VALIDATE_INPUT_ARG : Titanis.DceRpc.IRpcFixedStruct {
        public PASSWORD_POLICY_VALIDATION_TYPE unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.unionSwitch)));
            encoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.ValidateAuthenticationInput, Titanis.DceRpc.NdrAlignment._8Byte);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteFixedStruct(this.ValidatePasswordChangeInput, Titanis.DceRpc.NdrAlignment._8Byte);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteFixedStruct(this.ValidatePasswordResetInput, Titanis.DceRpc.NdrAlignment._8Byte);
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = ((PASSWORD_POLICY_VALIDATION_TYPE)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.ValidateAuthenticationInput = decoder.ReadFixedStruct<SAM_VALIDATE_AUTHENTICATION_INPUT_ARG>(Titanis.DceRpc.NdrAlignment._8Byte);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.ValidatePasswordChangeInput = decoder.ReadFixedStruct<SAM_VALIDATE_PASSWORD_CHANGE_INPUT_ARG>(Titanis.DceRpc.NdrAlignment._8Byte);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.ValidatePasswordResetInput = decoder.ReadFixedStruct<SAM_VALIDATE_PASSWORD_RESET_INPUT_ARG>(Titanis.DceRpc.NdrAlignment._8Byte);
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteStructDeferral(this.ValidateAuthenticationInput);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteStructDeferral(this.ValidatePasswordChangeInput);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteStructDeferral(this.ValidatePasswordResetInput);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                decoder.ReadStructDeferral<SAM_VALIDATE_AUTHENTICATION_INPUT_ARG>(ref this.ValidateAuthenticationInput);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    decoder.ReadStructDeferral<SAM_VALIDATE_PASSWORD_CHANGE_INPUT_ARG>(ref this.ValidatePasswordChangeInput);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        decoder.ReadStructDeferral<SAM_VALIDATE_PASSWORD_RESET_INPUT_ARG>(ref this.ValidatePasswordResetInput);
                    }
                }
            }
        }
        public SAM_VALIDATE_AUTHENTICATION_INPUT_ARG ValidateAuthenticationInput;
        public SAM_VALIDATE_PASSWORD_CHANGE_INPUT_ARG ValidatePasswordChangeInput;
        public SAM_VALIDATE_PASSWORD_RESET_INPUT_ARG ValidatePasswordResetInput;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public struct SAM_VALIDATE_OUTPUT_ARG : Titanis.DceRpc.IRpcFixedStruct {
        public PASSWORD_POLICY_VALIDATION_TYPE unionSwitch;
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(((short)(this.unionSwitch)));
            encoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteFixedStruct(this.ValidateAuthenticationOutput, Titanis.DceRpc.NdrAlignment._8Byte);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteFixedStruct(this.ValidatePasswordChangeOutput, Titanis.DceRpc.NdrAlignment._8Byte);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteFixedStruct(this.ValidatePasswordResetOutput, Titanis.DceRpc.NdrAlignment._8Byte);
                    }
                }
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.unionSwitch = ((PASSWORD_POLICY_VALIDATION_TYPE)(decoder.ReadInt16()));
            decoder.Align(Titanis.DceRpc.NdrAlignment._8Byte);
            if ((((int)(this.unionSwitch)) == 1)) {
                this.ValidateAuthenticationOutput = decoder.ReadFixedStruct<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(Titanis.DceRpc.NdrAlignment._8Byte);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    this.ValidatePasswordChangeOutput = decoder.ReadFixedStruct<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(Titanis.DceRpc.NdrAlignment._8Byte);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        this.ValidatePasswordResetOutput = decoder.ReadFixedStruct<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(Titanis.DceRpc.NdrAlignment._8Byte);
                    }
                }
            }
        }
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                encoder.WriteStructDeferral(this.ValidateAuthenticationOutput);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    encoder.WriteStructDeferral(this.ValidatePasswordChangeOutput);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        encoder.WriteStructDeferral(this.ValidatePasswordResetOutput);
                    }
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            if ((((int)(this.unionSwitch)) == 1)) {
                decoder.ReadStructDeferral<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(ref this.ValidateAuthenticationOutput);
            }
            else {
                if ((((int)(this.unionSwitch)) == 2)) {
                    decoder.ReadStructDeferral<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(ref this.ValidatePasswordChangeOutput);
                }
                else {
                    if ((((int)(this.unionSwitch)) == 3)) {
                        decoder.ReadStructDeferral<SAM_VALIDATE_STANDARD_OUTPUT_ARG>(ref this.ValidatePasswordResetOutput);
                    }
                }
            }
        }
        public SAM_VALIDATE_STANDARD_OUTPUT_ARG ValidateAuthenticationOutput;
        public SAM_VALIDATE_STANDARD_OUTPUT_ARG ValidatePasswordChangeOutput;
        public SAM_VALIDATE_STANDARD_OUTPUT_ARG ValidatePasswordResetOutput;
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    [System.Runtime.InteropServices.GuidAttribute("12345778-1234-abcd-ef00-0123456789ac")]
    [Titanis.DceRpc.RpcVersionAttribute(1, 0)]
    public interface samr {
        Task<int> SamrConnect(RpcPointer<char> ServerName, RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle, uint DesiredAccess, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrCloseHandle(RpcPointer<Titanis.DceRpc.RpcContextHandle> SamHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrSetSecurityObject(Titanis.DceRpc.RpcContextHandle ObjectHandle, uint SecurityInformation, SAMPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrQuerySecurityObject(Titanis.DceRpc.RpcContextHandle ObjectHandle, uint SecurityInformation, RpcPointer<RpcPointer<SAMPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor, System.Threading.CancellationToken cancellationToken);
        Task Opnum4NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> SamrLookupDomainInSamServer(Titanis.DceRpc.RpcContextHandle ServerHandle, ms_dtyp.RPC_UNICODE_STRING Name, RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> DomainId, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrEnumerateDomainsInSamServer(Titanis.DceRpc.RpcContextHandle ServerHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrOpenDomain(Titanis.DceRpc.RpcContextHandle ServerHandle, uint DesiredAccess, ms_dtyp.RPC_SID DomainId, RpcPointer<Titanis.DceRpc.RpcContextHandle> DomainHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrQueryInformationDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrSetInformationDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, SAMPR_DOMAIN_INFO_BUFFER DomainInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrCreateGroupInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> GroupHandle, RpcPointer<uint> RelativeId, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrEnumerateGroupsInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrCreateUserInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle, RpcPointer<uint> RelativeId, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrEnumerateUsersInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, uint UserAccountControl, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrCreateAliasInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING AccountName, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> AliasHandle, RpcPointer<uint> RelativeId, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrEnumerateAliasesInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrGetAliasMembership(Titanis.DceRpc.RpcContextHandle DomainHandle, SAMPR_PSID_ARRAY SidArray, RpcPointer<SAMPR_ULONG_ARRAY> Membership, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrLookupNamesInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, uint Count, ArraySegment<ms_dtyp.RPC_UNICODE_STRING> Names, RpcPointer<SAMPR_ULONG_ARRAY> RelativeIds, RpcPointer<SAMPR_ULONG_ARRAY> Use, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrLookupIdsInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, uint Count, ArraySegment<uint> RelativeIds, RpcPointer<SAMPR_RETURNED_USTRING_ARRAY> Names, RpcPointer<SAMPR_ULONG_ARRAY> Use, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrOpenGroup(Titanis.DceRpc.RpcContextHandle DomainHandle, uint DesiredAccess, uint GroupId, RpcPointer<Titanis.DceRpc.RpcContextHandle> GroupHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrQueryInformationGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, GROUP_INFORMATION_CLASS GroupInformationClass, RpcPointer<RpcPointer<SAMPR_GROUP_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrSetInformationGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, GROUP_INFORMATION_CLASS GroupInformationClass, SAMPR_GROUP_INFO_BUFFER Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrAddMemberToGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, uint MemberId, uint Attributes, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrDeleteGroup(RpcPointer<Titanis.DceRpc.RpcContextHandle> GroupHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrRemoveMemberFromGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, uint MemberId, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrGetMembersInGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, RpcPointer<RpcPointer<SAMPR_GET_MEMBERS_BUFFER>> Members, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrSetMemberAttributesOfGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, uint MemberId, uint Attributes, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrOpenAlias(Titanis.DceRpc.RpcContextHandle DomainHandle, uint DesiredAccess, uint AliasId, RpcPointer<Titanis.DceRpc.RpcContextHandle> AliasHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrQueryInformationAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, ALIAS_INFORMATION_CLASS AliasInformationClass, RpcPointer<RpcPointer<SAMPR_ALIAS_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrSetInformationAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, ALIAS_INFORMATION_CLASS AliasInformationClass, SAMPR_ALIAS_INFO_BUFFER Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrDeleteAlias(RpcPointer<Titanis.DceRpc.RpcContextHandle> AliasHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrAddMemberToAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, ms_dtyp.RPC_SID MemberId, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrRemoveMemberFromAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, ms_dtyp.RPC_SID MemberId, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrGetMembersInAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, RpcPointer<SAMPR_PSID_ARRAY_OUT> Members, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrOpenUser(Titanis.DceRpc.RpcContextHandle DomainHandle, uint DesiredAccess, uint UserId, RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrDeleteUser(RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrQueryInformationUser(Titanis.DceRpc.RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrSetInformationUser(Titanis.DceRpc.RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, SAMPR_USER_INFO_BUFFER Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrChangePasswordUser(Titanis.DceRpc.RpcContextHandle UserHandle, byte LmPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmEncryptedWithNewLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithOldLm, byte NtPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtEncryptedWithNewNt, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithOldNt, byte NtCrossEncryptionPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithNewLm, byte LmCrossEncryptionPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithNewNt, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrGetGroupsForUser(Titanis.DceRpc.RpcContextHandle UserHandle, RpcPointer<RpcPointer<SAMPR_GET_GROUPS_BUFFER>> Groups, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrQueryDisplayInformation(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrGetDisplayEnumerationIndex(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, ms_dtyp.RPC_UNICODE_STRING Prefix, RpcPointer<uint> Index, System.Threading.CancellationToken cancellationToken);
        Task Opnum42NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum43NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> SamrGetUserDomainPasswordInformation(Titanis.DceRpc.RpcContextHandle UserHandle, RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrRemoveMemberFromForeignDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, ms_dtyp.RPC_SID MemberSid, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrQueryInformationDomain2(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrQueryInformationUser2(Titanis.DceRpc.RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrQueryDisplayInformation2(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrGetDisplayEnumerationIndex2(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, ms_dtyp.RPC_UNICODE_STRING Prefix, RpcPointer<uint> Index, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrCreateUser2InDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint AccountType, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle, RpcPointer<uint> GrantedAccess, RpcPointer<uint> RelativeId, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrQueryDisplayInformation3(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrAddMultipleMembersToAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, SAMPR_PSID_ARRAY MembersBuffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrRemoveMultipleMembersFromAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, SAMPR_PSID_ARRAY MembersBuffer, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrOemChangePasswordUser2(RpcPointer<RPC_STRING> ServerName, RPC_STRING UserName, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewLm, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrUnicodeChangePasswordUser2(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> ServerName, ms_dtyp.RPC_UNICODE_STRING UserName, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldNt, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtOwfPasswordEncryptedWithNewNt, byte LmPresent, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewNt, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrGetDomainPasswordInformation(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused, RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrConnect2(string ServerName, RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle, uint DesiredAccess, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrSetInformationUser2(Titanis.DceRpc.RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, SAMPR_USER_INFO_BUFFER Buffer, System.Threading.CancellationToken cancellationToken);
        Task Opnum59NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum60NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum61NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> SamrConnect4(string ServerName, RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle, uint ClientRevision, uint DesiredAccess, System.Threading.CancellationToken cancellationToken);
        Task Opnum63NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task<int> SamrConnect5(string ServerName, uint DesiredAccess, uint InVersion, SAMPR_REVISION_INFO InRevisionInfo, RpcPointer<uint> OutVersion, RpcPointer<SAMPR_REVISION_INFO> OutRevisionInfo, RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrRidToSid(Titanis.DceRpc.RpcContextHandle ObjectHandle, uint Rid, RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> Sid, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrSetDSRMPassword(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused, uint UserId, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> EncryptedNtOwfPassword, System.Threading.CancellationToken cancellationToken);
        Task<int> SamrValidatePassword(PASSWORD_POLICY_VALIDATION_TYPE ValidationType, SAM_VALIDATE_INPUT_ARG InputArg, RpcPointer<RpcPointer<SAM_VALIDATE_OUTPUT_ARG>> OutputArg, System.Threading.CancellationToken cancellationToken);
        Task Opnum68NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
        Task Opnum69NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    [Titanis.DceRpc.IidAttribute("12345778-1234-abcd-ef00-0123456789ac")]
    public class samrClientProxy : Titanis.DceRpc.Client.RpcClientProxy, samr, Titanis.DceRpc.IRpcClientProxy {
        private static System.Guid _interfaceUuid = new System.Guid("12345778-1234-abcd-ef00-0123456789ac");
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
        public virtual async Task<int> SamrConnect(RpcPointer<char> ServerName, RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle, uint DesiredAccess, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(ServerName);
            if ((null != ServerName)) {
                encoder.WriteValue(ServerName.value);
            }
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ServerHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrCloseHandle(RpcPointer<Titanis.DceRpc.RpcContextHandle> SamHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(SamHandle.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            SamHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrSetSecurityObject(Titanis.DceRpc.RpcContextHandle ObjectHandle, uint SecurityInformation, SAMPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(ObjectHandle);
            encoder.WriteValue(SecurityInformation);
            encoder.WriteFixedStruct(SecurityDescriptor, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(SecurityDescriptor);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrQuerySecurityObject(Titanis.DceRpc.RpcContextHandle ObjectHandle, uint SecurityInformation, RpcPointer<RpcPointer<SAMPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(ObjectHandle);
            encoder.WriteValue(SecurityInformation);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            SecurityDescriptor.value = decoder.ReadOutPointer<SAMPR_SR_SECURITY_DESCRIPTOR>(SecurityDescriptor.value);
            if ((null != SecurityDescriptor.value)) {
                SecurityDescriptor.value.value = decoder.ReadFixedStruct<SAMPR_SR_SECURITY_DESCRIPTOR>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<SAMPR_SR_SECURITY_DESCRIPTOR>(ref SecurityDescriptor.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum4NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> SamrLookupDomainInSamServer(Titanis.DceRpc.RpcContextHandle ServerHandle, ms_dtyp.RPC_UNICODE_STRING Name, RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> DomainId, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(ServerHandle);
            encoder.WriteFixedStruct(Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Name);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            DomainId.value = decoder.ReadOutPointer<ms_dtyp.RPC_SID>(DomainId.value);
            if ((null != DomainId.value)) {
                DomainId.value.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref DomainId.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrEnumerateDomainsInSamServer(Titanis.DceRpc.RpcContextHandle ServerHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(ServerHandle);
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteValue(PreferedMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EnumerationContext.value = decoder.ReadUInt32();
            Buffer.value = decoder.ReadOutPointer<SAMPR_ENUMERATION_BUFFER>(Buffer.value);
            if ((null != Buffer.value)) {
                Buffer.value.value = decoder.ReadFixedStruct<SAMPR_ENUMERATION_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<SAMPR_ENUMERATION_BUFFER>(ref Buffer.value.value);
            }
            CountReturned.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrOpenDomain(Titanis.DceRpc.RpcContextHandle ServerHandle, uint DesiredAccess, ms_dtyp.RPC_SID DomainId, RpcPointer<Titanis.DceRpc.RpcContextHandle> DomainHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(ServerHandle);
            encoder.WriteValue(DesiredAccess);
            encoder.WriteConformantStruct(DomainId, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(DomainId);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            DomainHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrQueryInformationDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(((short)(DomainInformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Buffer.value = decoder.ReadOutPointer<SAMPR_DOMAIN_INFO_BUFFER>(Buffer.value);
            if ((null != Buffer.value)) {
                Buffer.value.value = decoder.ReadUnion<SAMPR_DOMAIN_INFO_BUFFER>();
                decoder.ReadStructDeferral<SAMPR_DOMAIN_INFO_BUFFER>(ref Buffer.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrSetInformationDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, SAMPR_DOMAIN_INFO_BUFFER DomainInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(((short)(DomainInformationClass)));
            encoder.WriteUnion(DomainInformation);
            encoder.WriteStructDeferral(DomainInformation);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrCreateGroupInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> GroupHandle, RpcPointer<uint> RelativeId, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteFixedStruct(Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Name);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            GroupHandle.value = decoder.ReadContextHandle();
            RelativeId.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrEnumerateGroupsInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteValue(PreferedMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EnumerationContext.value = decoder.ReadUInt32();
            Buffer.value = decoder.ReadOutPointer<SAMPR_ENUMERATION_BUFFER>(Buffer.value);
            if ((null != Buffer.value)) {
                Buffer.value.value = decoder.ReadFixedStruct<SAMPR_ENUMERATION_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<SAMPR_ENUMERATION_BUFFER>(ref Buffer.value.value);
            }
            CountReturned.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrCreateUserInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle, RpcPointer<uint> RelativeId, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteFixedStruct(Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Name);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            UserHandle.value = decoder.ReadContextHandle();
            RelativeId.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrEnumerateUsersInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, uint UserAccountControl, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteValue(UserAccountControl);
            encoder.WriteValue(PreferedMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EnumerationContext.value = decoder.ReadUInt32();
            Buffer.value = decoder.ReadOutPointer<SAMPR_ENUMERATION_BUFFER>(Buffer.value);
            if ((null != Buffer.value)) {
                Buffer.value.value = decoder.ReadFixedStruct<SAMPR_ENUMERATION_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<SAMPR_ENUMERATION_BUFFER>(ref Buffer.value.value);
            }
            CountReturned.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrCreateAliasInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING AccountName, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> AliasHandle, RpcPointer<uint> RelativeId, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteFixedStruct(AccountName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(AccountName);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            AliasHandle.value = decoder.ReadContextHandle();
            RelativeId.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrEnumerateAliasesInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, RpcPointer<uint> EnumerationContext, RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer, uint PreferedMaximumLength, RpcPointer<uint> CountReturned, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(EnumerationContext.value);
            encoder.WriteValue(PreferedMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            EnumerationContext.value = decoder.ReadUInt32();
            Buffer.value = decoder.ReadOutPointer<SAMPR_ENUMERATION_BUFFER>(Buffer.value);
            if ((null != Buffer.value)) {
                Buffer.value.value = decoder.ReadFixedStruct<SAMPR_ENUMERATION_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<SAMPR_ENUMERATION_BUFFER>(ref Buffer.value.value);
            }
            CountReturned.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrGetAliasMembership(Titanis.DceRpc.RpcContextHandle DomainHandle, SAMPR_PSID_ARRAY SidArray, RpcPointer<SAMPR_ULONG_ARRAY> Membership, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteFixedStruct(SidArray, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(SidArray);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Membership.value = decoder.ReadFixedStruct<SAMPR_ULONG_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SAMPR_ULONG_ARRAY>(ref Membership.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrLookupNamesInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, uint Count, ArraySegment<ms_dtyp.RPC_UNICODE_STRING> Names, RpcPointer<SAMPR_ULONG_ARRAY> RelativeIds, RpcPointer<SAMPR_ULONG_ARRAY> Use, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(Count);
            encoder.WriteArrayHeader(Names, true);
            for (int i = 0; (i < Names.Count); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names.Item(i);
                encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
            }
            for (int i = 0; (i < Names.Count); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names.Item(i);
                encoder.WriteStructDeferral(elem_0);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            RelativeIds.value = decoder.ReadFixedStruct<SAMPR_ULONG_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SAMPR_ULONG_ARRAY>(ref RelativeIds.value);
            Use.value = decoder.ReadFixedStruct<SAMPR_ULONG_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SAMPR_ULONG_ARRAY>(ref Use.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrLookupIdsInDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, uint Count, ArraySegment<uint> RelativeIds, RpcPointer<SAMPR_RETURNED_USTRING_ARRAY> Names, RpcPointer<SAMPR_ULONG_ARRAY> Use, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(Count);
            if ((RelativeIds != null)) {
                encoder.WriteArrayHeader(RelativeIds, true);
                for (int i = 0; (i < RelativeIds.Count); i++
                ) {
                    uint elem_0 = RelativeIds.Item(i);
                    encoder.WriteValue(elem_0);
                }
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Names.value = decoder.ReadFixedStruct<SAMPR_RETURNED_USTRING_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SAMPR_RETURNED_USTRING_ARRAY>(ref Names.value);
            Use.value = decoder.ReadFixedStruct<SAMPR_ULONG_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SAMPR_ULONG_ARRAY>(ref Use.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrOpenGroup(Titanis.DceRpc.RpcContextHandle DomainHandle, uint DesiredAccess, uint GroupId, RpcPointer<Titanis.DceRpc.RpcContextHandle> GroupHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(DesiredAccess);
            encoder.WriteValue(GroupId);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            GroupHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrQueryInformationGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, GROUP_INFORMATION_CLASS GroupInformationClass, RpcPointer<RpcPointer<SAMPR_GROUP_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(GroupHandle);
            encoder.WriteValue(((short)(GroupInformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Buffer.value = decoder.ReadOutPointer<SAMPR_GROUP_INFO_BUFFER>(Buffer.value);
            if ((null != Buffer.value)) {
                Buffer.value.value = decoder.ReadUnion<SAMPR_GROUP_INFO_BUFFER>();
                decoder.ReadStructDeferral<SAMPR_GROUP_INFO_BUFFER>(ref Buffer.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrSetInformationGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, GROUP_INFORMATION_CLASS GroupInformationClass, SAMPR_GROUP_INFO_BUFFER Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(GroupHandle);
            encoder.WriteValue(((short)(GroupInformationClass)));
            encoder.WriteUnion(Buffer);
            encoder.WriteStructDeferral(Buffer);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrAddMemberToGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, uint MemberId, uint Attributes, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(GroupHandle);
            encoder.WriteValue(MemberId);
            encoder.WriteValue(Attributes);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrDeleteGroup(RpcPointer<Titanis.DceRpc.RpcContextHandle> GroupHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(GroupHandle.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            GroupHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrRemoveMemberFromGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, uint MemberId, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(GroupHandle);
            encoder.WriteValue(MemberId);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrGetMembersInGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, RpcPointer<RpcPointer<SAMPR_GET_MEMBERS_BUFFER>> Members, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(GroupHandle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Members.value = decoder.ReadOutPointer<SAMPR_GET_MEMBERS_BUFFER>(Members.value);
            if ((null != Members.value)) {
                Members.value.value = decoder.ReadFixedStruct<SAMPR_GET_MEMBERS_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<SAMPR_GET_MEMBERS_BUFFER>(ref Members.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrSetMemberAttributesOfGroup(Titanis.DceRpc.RpcContextHandle GroupHandle, uint MemberId, uint Attributes, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(GroupHandle);
            encoder.WriteValue(MemberId);
            encoder.WriteValue(Attributes);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrOpenAlias(Titanis.DceRpc.RpcContextHandle DomainHandle, uint DesiredAccess, uint AliasId, RpcPointer<Titanis.DceRpc.RpcContextHandle> AliasHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(DesiredAccess);
            encoder.WriteValue(AliasId);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            AliasHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrQueryInformationAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, ALIAS_INFORMATION_CLASS AliasInformationClass, RpcPointer<RpcPointer<SAMPR_ALIAS_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AliasHandle);
            encoder.WriteValue(((short)(AliasInformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Buffer.value = decoder.ReadOutPointer<SAMPR_ALIAS_INFO_BUFFER>(Buffer.value);
            if ((null != Buffer.value)) {
                Buffer.value.value = decoder.ReadUnion<SAMPR_ALIAS_INFO_BUFFER>();
                decoder.ReadStructDeferral<SAMPR_ALIAS_INFO_BUFFER>(ref Buffer.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrSetInformationAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, ALIAS_INFORMATION_CLASS AliasInformationClass, SAMPR_ALIAS_INFO_BUFFER Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AliasHandle);
            encoder.WriteValue(((short)(AliasInformationClass)));
            encoder.WriteUnion(Buffer);
            encoder.WriteStructDeferral(Buffer);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrDeleteAlias(RpcPointer<Titanis.DceRpc.RpcContextHandle> AliasHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AliasHandle.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            AliasHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrAddMemberToAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, ms_dtyp.RPC_SID MemberId, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AliasHandle);
            encoder.WriteConformantStruct(MemberId, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(MemberId);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrRemoveMemberFromAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, ms_dtyp.RPC_SID MemberId, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AliasHandle);
            encoder.WriteConformantStruct(MemberId, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(MemberId);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrGetMembersInAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, RpcPointer<SAMPR_PSID_ARRAY_OUT> Members, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AliasHandle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Members.value = decoder.ReadFixedStruct<SAMPR_PSID_ARRAY_OUT>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SAMPR_PSID_ARRAY_OUT>(ref Members.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrOpenUser(Titanis.DceRpc.RpcContextHandle DomainHandle, uint DesiredAccess, uint UserId, RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(DesiredAccess);
            encoder.WriteValue(UserId);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            UserHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrDeleteUser(RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(UserHandle.value);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            UserHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrQueryInformationUser(Titanis.DceRpc.RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(UserHandle);
            encoder.WriteValue(((short)(UserInformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Buffer.value = decoder.ReadOutPointer<SAMPR_USER_INFO_BUFFER>(Buffer.value);
            if ((null != Buffer.value)) {
                Buffer.value.value = decoder.ReadUnion<SAMPR_USER_INFO_BUFFER>();
                decoder.ReadStructDeferral<SAMPR_USER_INFO_BUFFER>(ref Buffer.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrSetInformationUser(Titanis.DceRpc.RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, SAMPR_USER_INFO_BUFFER Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(37);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(UserHandle);
            encoder.WriteValue(((short)(UserInformationClass)));
            encoder.WriteUnion(Buffer);
            encoder.WriteStructDeferral(Buffer);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrChangePasswordUser(Titanis.DceRpc.RpcContextHandle UserHandle, byte LmPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmEncryptedWithNewLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithOldLm, byte NtPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtEncryptedWithNewNt, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithOldNt, byte NtCrossEncryptionPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithNewLm, byte LmCrossEncryptionPresent, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithNewNt, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(38);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(UserHandle);
            encoder.WriteValue(LmPresent);
            encoder.WritePointer(OldLmEncryptedWithNewLm);
            if ((null != OldLmEncryptedWithNewLm)) {
                encoder.WriteFixedStruct(OldLmEncryptedWithNewLm.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(OldLmEncryptedWithNewLm.value);
            }
            encoder.WritePointer(NewLmEncryptedWithOldLm);
            if ((null != NewLmEncryptedWithOldLm)) {
                encoder.WriteFixedStruct(NewLmEncryptedWithOldLm.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(NewLmEncryptedWithOldLm.value);
            }
            encoder.WriteValue(NtPresent);
            encoder.WritePointer(OldNtEncryptedWithNewNt);
            if ((null != OldNtEncryptedWithNewNt)) {
                encoder.WriteFixedStruct(OldNtEncryptedWithNewNt.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(OldNtEncryptedWithNewNt.value);
            }
            encoder.WritePointer(NewNtEncryptedWithOldNt);
            if ((null != NewNtEncryptedWithOldNt)) {
                encoder.WriteFixedStruct(NewNtEncryptedWithOldNt.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(NewNtEncryptedWithOldNt.value);
            }
            encoder.WriteValue(NtCrossEncryptionPresent);
            encoder.WritePointer(NewNtEncryptedWithNewLm);
            if ((null != NewNtEncryptedWithNewLm)) {
                encoder.WriteFixedStruct(NewNtEncryptedWithNewLm.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(NewNtEncryptedWithNewLm.value);
            }
            encoder.WriteValue(LmCrossEncryptionPresent);
            encoder.WritePointer(NewLmEncryptedWithNewNt);
            if ((null != NewLmEncryptedWithNewNt)) {
                encoder.WriteFixedStruct(NewLmEncryptedWithNewNt.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(NewLmEncryptedWithNewNt.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrGetGroupsForUser(Titanis.DceRpc.RpcContextHandle UserHandle, RpcPointer<RpcPointer<SAMPR_GET_GROUPS_BUFFER>> Groups, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(39);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(UserHandle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Groups.value = decoder.ReadOutPointer<SAMPR_GET_GROUPS_BUFFER>(Groups.value);
            if ((null != Groups.value)) {
                Groups.value.value = decoder.ReadFixedStruct<SAMPR_GET_GROUPS_BUFFER>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<SAMPR_GET_GROUPS_BUFFER>(ref Groups.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrQueryDisplayInformation(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(40);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(((short)(DisplayInformationClass)));
            encoder.WriteValue(Index);
            encoder.WriteValue(EntryCount);
            encoder.WriteValue(PreferredMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TotalAvailable.value = decoder.ReadUInt32();
            TotalReturned.value = decoder.ReadUInt32();
            Buffer.value = decoder.ReadUnion<SAMPR_DISPLAY_INFO_BUFFER>();
            decoder.ReadStructDeferral<SAMPR_DISPLAY_INFO_BUFFER>(ref Buffer.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrGetDisplayEnumerationIndex(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, ms_dtyp.RPC_UNICODE_STRING Prefix, RpcPointer<uint> Index, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(41);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(((short)(DisplayInformationClass)));
            encoder.WriteFixedStruct(Prefix, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Prefix);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Index.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
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
        public virtual async Task<int> SamrGetUserDomainPasswordInformation(Titanis.DceRpc.RpcContextHandle UserHandle, RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(44);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(UserHandle);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            PasswordInformation.value = decoder.ReadFixedStruct<USER_DOMAIN_PASSWORD_INFORMATION>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<USER_DOMAIN_PASSWORD_INFORMATION>(ref PasswordInformation.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrRemoveMemberFromForeignDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, ms_dtyp.RPC_SID MemberSid, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(45);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteConformantStruct(MemberSid, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(MemberSid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrQueryInformationDomain2(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_INFORMATION_CLASS DomainInformationClass, RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(46);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(((short)(DomainInformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Buffer.value = decoder.ReadOutPointer<SAMPR_DOMAIN_INFO_BUFFER>(Buffer.value);
            if ((null != Buffer.value)) {
                Buffer.value.value = decoder.ReadUnion<SAMPR_DOMAIN_INFO_BUFFER>();
                decoder.ReadStructDeferral<SAMPR_DOMAIN_INFO_BUFFER>(ref Buffer.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrQueryInformationUser2(Titanis.DceRpc.RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(47);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(UserHandle);
            encoder.WriteValue(((short)(UserInformationClass)));
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Buffer.value = decoder.ReadOutPointer<SAMPR_USER_INFO_BUFFER>(Buffer.value);
            if ((null != Buffer.value)) {
                Buffer.value.value = decoder.ReadUnion<SAMPR_USER_INFO_BUFFER>();
                decoder.ReadStructDeferral<SAMPR_USER_INFO_BUFFER>(ref Buffer.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrQueryDisplayInformation2(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(48);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(((short)(DisplayInformationClass)));
            encoder.WriteValue(Index);
            encoder.WriteValue(EntryCount);
            encoder.WriteValue(PreferredMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TotalAvailable.value = decoder.ReadUInt32();
            TotalReturned.value = decoder.ReadUInt32();
            Buffer.value = decoder.ReadUnion<SAMPR_DISPLAY_INFO_BUFFER>();
            decoder.ReadStructDeferral<SAMPR_DISPLAY_INFO_BUFFER>(ref Buffer.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrGetDisplayEnumerationIndex2(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, ms_dtyp.RPC_UNICODE_STRING Prefix, RpcPointer<uint> Index, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(49);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(((short)(DisplayInformationClass)));
            encoder.WriteFixedStruct(Prefix, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Prefix);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Index.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrCreateUser2InDomain(Titanis.DceRpc.RpcContextHandle DomainHandle, ms_dtyp.RPC_UNICODE_STRING Name, uint AccountType, uint DesiredAccess, RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle, RpcPointer<uint> GrantedAccess, RpcPointer<uint> RelativeId, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(50);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteFixedStruct(Name, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Name);
            encoder.WriteValue(AccountType);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            UserHandle.value = decoder.ReadContextHandle();
            GrantedAccess.value = decoder.ReadUInt32();
            RelativeId.value = decoder.ReadUInt32();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrQueryDisplayInformation3(Titanis.DceRpc.RpcContextHandle DomainHandle, DOMAIN_DISPLAY_INFORMATION DisplayInformationClass, uint Index, uint EntryCount, uint PreferredMaximumLength, RpcPointer<uint> TotalAvailable, RpcPointer<uint> TotalReturned, RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(51);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(DomainHandle);
            encoder.WriteValue(((short)(DisplayInformationClass)));
            encoder.WriteValue(Index);
            encoder.WriteValue(EntryCount);
            encoder.WriteValue(PreferredMaximumLength);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            TotalAvailable.value = decoder.ReadUInt32();
            TotalReturned.value = decoder.ReadUInt32();
            Buffer.value = decoder.ReadUnion<SAMPR_DISPLAY_INFO_BUFFER>();
            decoder.ReadStructDeferral<SAMPR_DISPLAY_INFO_BUFFER>(ref Buffer.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrAddMultipleMembersToAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, SAMPR_PSID_ARRAY MembersBuffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(52);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AliasHandle);
            encoder.WriteFixedStruct(MembersBuffer, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(MembersBuffer);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrRemoveMultipleMembersFromAlias(Titanis.DceRpc.RpcContextHandle AliasHandle, SAMPR_PSID_ARRAY MembersBuffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(53);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(AliasHandle);
            encoder.WriteFixedStruct(MembersBuffer, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(MembersBuffer);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrOemChangePasswordUser2(RpcPointer<RPC_STRING> ServerName, RPC_STRING UserName, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewLm, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(54);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(ServerName);
            if ((null != ServerName)) {
                encoder.WriteFixedStruct(ServerName.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ServerName.value);
            }
            encoder.WriteFixedStruct(UserName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(UserName);
            encoder.WritePointer(NewPasswordEncryptedWithOldLm);
            if ((null != NewPasswordEncryptedWithOldLm)) {
                encoder.WriteFixedStruct(NewPasswordEncryptedWithOldLm.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(NewPasswordEncryptedWithOldLm.value);
            }
            encoder.WritePointer(OldLmOwfPasswordEncryptedWithNewLm);
            if ((null != OldLmOwfPasswordEncryptedWithNewLm)) {
                encoder.WriteFixedStruct(OldLmOwfPasswordEncryptedWithNewLm.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(OldLmOwfPasswordEncryptedWithNewLm.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrUnicodeChangePasswordUser2(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> ServerName, ms_dtyp.RPC_UNICODE_STRING UserName, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldNt, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtOwfPasswordEncryptedWithNewNt, byte LmPresent, RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewNt, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(55);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(ServerName);
            if ((null != ServerName)) {
                encoder.WriteFixedStruct(ServerName.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(ServerName.value);
            }
            encoder.WriteFixedStruct(UserName, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(UserName);
            encoder.WritePointer(NewPasswordEncryptedWithOldNt);
            if ((null != NewPasswordEncryptedWithOldNt)) {
                encoder.WriteFixedStruct(NewPasswordEncryptedWithOldNt.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(NewPasswordEncryptedWithOldNt.value);
            }
            encoder.WritePointer(OldNtOwfPasswordEncryptedWithNewNt);
            if ((null != OldNtOwfPasswordEncryptedWithNewNt)) {
                encoder.WriteFixedStruct(OldNtOwfPasswordEncryptedWithNewNt.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(OldNtOwfPasswordEncryptedWithNewNt.value);
            }
            encoder.WriteValue(LmPresent);
            encoder.WritePointer(NewPasswordEncryptedWithOldLm);
            if ((null != NewPasswordEncryptedWithOldLm)) {
                encoder.WriteFixedStruct(NewPasswordEncryptedWithOldLm.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(NewPasswordEncryptedWithOldLm.value);
            }
            encoder.WritePointer(OldLmOwfPasswordEncryptedWithNewNt);
            if ((null != OldLmOwfPasswordEncryptedWithNewNt)) {
                encoder.WriteFixedStruct(OldLmOwfPasswordEncryptedWithNewNt.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(OldLmOwfPasswordEncryptedWithNewNt.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrGetDomainPasswordInformation(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused, RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(56);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(Unused);
            if ((null != Unused)) {
                encoder.WriteFixedStruct(Unused.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Unused.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            PasswordInformation.value = decoder.ReadFixedStruct<USER_DOMAIN_PASSWORD_INFORMATION>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<USER_DOMAIN_PASSWORD_INFORMATION>(ref PasswordInformation.value);
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrConnect2(string ServerName, RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle, uint DesiredAccess, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(57);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((ServerName == null));
            if ((ServerName != null)) {
                encoder.WriteWideCharString(ServerName);
            }
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ServerHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrSetInformationUser2(Titanis.DceRpc.RpcContextHandle UserHandle, USER_INFORMATION_CLASS UserInformationClass, SAMPR_USER_INFO_BUFFER Buffer, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(58);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(UserHandle);
            encoder.WriteValue(((short)(UserInformationClass)));
            encoder.WriteUnion(Buffer);
            encoder.WriteStructDeferral(Buffer);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum59NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(59);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum60NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(60);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum61NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(61);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> SamrConnect4(string ServerName, RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle, uint ClientRevision, uint DesiredAccess, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(62);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((ServerName == null));
            if ((ServerName != null)) {
                encoder.WriteWideCharString(ServerName);
            }
            encoder.WriteValue(ClientRevision);
            encoder.WriteValue(DesiredAccess);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            ServerHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum63NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(63);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task<int> SamrConnect5(string ServerName, uint DesiredAccess, uint InVersion, SAMPR_REVISION_INFO InRevisionInfo, RpcPointer<uint> OutVersion, RpcPointer<SAMPR_REVISION_INFO> OutRevisionInfo, RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(64);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteUniqueReferentId((ServerName == null));
            if ((ServerName != null)) {
                encoder.WriteWideCharString(ServerName);
            }
            encoder.WriteValue(DesiredAccess);
            encoder.WriteValue(InVersion);
            encoder.WriteUnion(InRevisionInfo);
            encoder.WriteStructDeferral(InRevisionInfo);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            OutVersion.value = decoder.ReadUInt32();
            OutRevisionInfo.value = decoder.ReadUnion<SAMPR_REVISION_INFO>();
            decoder.ReadStructDeferral<SAMPR_REVISION_INFO>(ref OutRevisionInfo.value);
            ServerHandle.value = decoder.ReadContextHandle();
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrRidToSid(Titanis.DceRpc.RpcContextHandle ObjectHandle, uint Rid, RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> Sid, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(65);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteContextHandle(ObjectHandle);
            encoder.WriteValue(Rid);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            Sid.value = decoder.ReadOutPointer<ms_dtyp.RPC_SID>(Sid.value);
            if ((null != Sid.value)) {
                Sid.value.value = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
                decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref Sid.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrSetDSRMPassword(RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused, uint UserId, RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> EncryptedNtOwfPassword, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(66);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WritePointer(Unused);
            if ((null != Unused)) {
                encoder.WriteFixedStruct(Unused.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Unused.value);
            }
            encoder.WriteValue(UserId);
            encoder.WritePointer(EncryptedNtOwfPassword);
            if ((null != EncryptedNtOwfPassword)) {
                encoder.WriteFixedStruct(EncryptedNtOwfPassword.value, Titanis.DceRpc.NdrAlignment._1Byte);
                encoder.WriteStructDeferral(EncryptedNtOwfPassword.value);
            }
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task<int> SamrValidatePassword(PASSWORD_POLICY_VALIDATION_TYPE ValidationType, SAM_VALIDATE_INPUT_ARG InputArg, RpcPointer<RpcPointer<SAM_VALIDATE_OUTPUT_ARG>> OutputArg, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(67);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            encoder.WriteValue(((short)(ValidationType)));
            encoder.WriteUnion(InputArg);
            encoder.WriteStructDeferral(InputArg);
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
            OutputArg.value = decoder.ReadOutPointer<SAM_VALIDATE_OUTPUT_ARG>(OutputArg.value);
            if ((null != OutputArg.value)) {
                OutputArg.value.value = decoder.ReadUnion<SAM_VALIDATE_OUTPUT_ARG>();
                decoder.ReadStructDeferral<SAM_VALIDATE_OUTPUT_ARG>(ref OutputArg.value.value);
            }
            int retval;
            retval = decoder.ReadInt32();
            return retval;
        }
        public virtual async Task Opnum68NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(68);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
        public virtual async Task Opnum69NotUsedOnWire(System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(69);
            Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
            var sendTask = this.SendRequestAsync(req, cancellationToken);
            Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.3")]
    public class samrStub : Titanis.DceRpc.Server.RpcServiceStub {
        private static System.Guid _interfaceUuid = new System.Guid("12345778-1234-abcd-ef00-0123456789ac");
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
        private samr _obj;
        public samrStub(samr obj) {
            this._obj = obj;
            this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
                    this.Invoke_SamrConnect,
                    this.Invoke_SamrCloseHandle,
                    this.Invoke_SamrSetSecurityObject,
                    this.Invoke_SamrQuerySecurityObject,
                    this.Invoke_Opnum4NotUsedOnWire,
                    this.Invoke_SamrLookupDomainInSamServer,
                    this.Invoke_SamrEnumerateDomainsInSamServer,
                    this.Invoke_SamrOpenDomain,
                    this.Invoke_SamrQueryInformationDomain,
                    this.Invoke_SamrSetInformationDomain,
                    this.Invoke_SamrCreateGroupInDomain,
                    this.Invoke_SamrEnumerateGroupsInDomain,
                    this.Invoke_SamrCreateUserInDomain,
                    this.Invoke_SamrEnumerateUsersInDomain,
                    this.Invoke_SamrCreateAliasInDomain,
                    this.Invoke_SamrEnumerateAliasesInDomain,
                    this.Invoke_SamrGetAliasMembership,
                    this.Invoke_SamrLookupNamesInDomain,
                    this.Invoke_SamrLookupIdsInDomain,
                    this.Invoke_SamrOpenGroup,
                    this.Invoke_SamrQueryInformationGroup,
                    this.Invoke_SamrSetInformationGroup,
                    this.Invoke_SamrAddMemberToGroup,
                    this.Invoke_SamrDeleteGroup,
                    this.Invoke_SamrRemoveMemberFromGroup,
                    this.Invoke_SamrGetMembersInGroup,
                    this.Invoke_SamrSetMemberAttributesOfGroup,
                    this.Invoke_SamrOpenAlias,
                    this.Invoke_SamrQueryInformationAlias,
                    this.Invoke_SamrSetInformationAlias,
                    this.Invoke_SamrDeleteAlias,
                    this.Invoke_SamrAddMemberToAlias,
                    this.Invoke_SamrRemoveMemberFromAlias,
                    this.Invoke_SamrGetMembersInAlias,
                    this.Invoke_SamrOpenUser,
                    this.Invoke_SamrDeleteUser,
                    this.Invoke_SamrQueryInformationUser,
                    this.Invoke_SamrSetInformationUser,
                    this.Invoke_SamrChangePasswordUser,
                    this.Invoke_SamrGetGroupsForUser,
                    this.Invoke_SamrQueryDisplayInformation,
                    this.Invoke_SamrGetDisplayEnumerationIndex,
                    this.Invoke_Opnum42NotUsedOnWire,
                    this.Invoke_Opnum43NotUsedOnWire,
                    this.Invoke_SamrGetUserDomainPasswordInformation,
                    this.Invoke_SamrRemoveMemberFromForeignDomain,
                    this.Invoke_SamrQueryInformationDomain2,
                    this.Invoke_SamrQueryInformationUser2,
                    this.Invoke_SamrQueryDisplayInformation2,
                    this.Invoke_SamrGetDisplayEnumerationIndex2,
                    this.Invoke_SamrCreateUser2InDomain,
                    this.Invoke_SamrQueryDisplayInformation3,
                    this.Invoke_SamrAddMultipleMembersToAlias,
                    this.Invoke_SamrRemoveMultipleMembersFromAlias,
                    this.Invoke_SamrOemChangePasswordUser2,
                    this.Invoke_SamrUnicodeChangePasswordUser2,
                    this.Invoke_SamrGetDomainPasswordInformation,
                    this.Invoke_SamrConnect2,
                    this.Invoke_SamrSetInformationUser2,
                    this.Invoke_Opnum59NotUsedOnWire,
                    this.Invoke_Opnum60NotUsedOnWire,
                    this.Invoke_Opnum61NotUsedOnWire,
                    this.Invoke_SamrConnect4,
                    this.Invoke_Opnum63NotUsedOnWire,
                    this.Invoke_SamrConnect5,
                    this.Invoke_SamrRidToSid,
                    this.Invoke_SamrSetDSRMPassword,
                    this.Invoke_SamrValidatePassword,
                    this.Invoke_Opnum68NotUsedOnWire,
                    this.Invoke_Opnum69NotUsedOnWire};
        }
        private async Task Invoke_SamrConnect(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<char> ServerName;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            uint DesiredAccess;
            ServerName = decoder.ReadPointer<char>();
            if ((null != ServerName)) {
                ServerName.value = decoder.ReadWideChar();
            }
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrConnect(ServerName, ServerHandle, DesiredAccess, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(ServerHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrCloseHandle(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> SamHandle;
            SamHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            SamHandle.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.SamrCloseHandle(SamHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(SamHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrSetSecurityObject(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle ObjectHandle;
            uint SecurityInformation;
            SAMPR_SR_SECURITY_DESCRIPTOR SecurityDescriptor;
            ObjectHandle = decoder.ReadContextHandle();
            SecurityInformation = decoder.ReadUInt32();
            SecurityDescriptor = decoder.ReadFixedStruct<SAMPR_SR_SECURITY_DESCRIPTOR>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SAMPR_SR_SECURITY_DESCRIPTOR>(ref SecurityDescriptor);
            var invokeTask = this._obj.SamrSetSecurityObject(ObjectHandle, SecurityInformation, SecurityDescriptor, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrQuerySecurityObject(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle ObjectHandle;
            uint SecurityInformation;
            RpcPointer<RpcPointer<SAMPR_SR_SECURITY_DESCRIPTOR>> SecurityDescriptor = new RpcPointer<RpcPointer<SAMPR_SR_SECURITY_DESCRIPTOR>>();
            ObjectHandle = decoder.ReadContextHandle();
            SecurityInformation = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrQuerySecurityObject(ObjectHandle, SecurityInformation, SecurityDescriptor, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(SecurityDescriptor.value);
            if ((null != SecurityDescriptor.value)) {
                encoder.WriteFixedStruct(SecurityDescriptor.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(SecurityDescriptor.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum4NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum4NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_SamrLookupDomainInSamServer(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle ServerHandle;
            ms_dtyp.RPC_UNICODE_STRING Name;
            RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> DomainId = new RpcPointer<RpcPointer<ms_dtyp.RPC_SID>>();
            ServerHandle = decoder.ReadContextHandle();
            Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
            var invokeTask = this._obj.SamrLookupDomainInSamServer(ServerHandle, Name, DomainId, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(DomainId.value);
            if ((null != DomainId.value)) {
                encoder.WriteConformantStruct(DomainId.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(DomainId.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrEnumerateDomainsInSamServer(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle ServerHandle;
            RpcPointer<uint> EnumerationContext;
            RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>>();
            uint PreferedMaximumLength;
            RpcPointer<uint> CountReturned = new RpcPointer<uint>();
            ServerHandle = decoder.ReadContextHandle();
            EnumerationContext = new RpcPointer<uint>();
            EnumerationContext.value = decoder.ReadUInt32();
            PreferedMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrEnumerateDomainsInSamServer(ServerHandle, EnumerationContext, Buffer, PreferedMaximumLength, CountReturned, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(EnumerationContext.value);
            encoder.WritePointer(Buffer.value);
            if ((null != Buffer.value)) {
                encoder.WriteFixedStruct(Buffer.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Buffer.value.value);
            }
            encoder.WriteValue(CountReturned.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrOpenDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle ServerHandle;
            uint DesiredAccess;
            ms_dtyp.RPC_SID DomainId;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> DomainHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            ServerHandle = decoder.ReadContextHandle();
            DesiredAccess = decoder.ReadUInt32();
            DomainId = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref DomainId);
            var invokeTask = this._obj.SamrOpenDomain(ServerHandle, DesiredAccess, DomainId, DomainHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(DomainHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrQueryInformationDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            DOMAIN_INFORMATION_CLASS DomainInformationClass;
            RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>>();
            DomainHandle = decoder.ReadContextHandle();
            DomainInformationClass = ((DOMAIN_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.SamrQueryInformationDomain(DomainHandle, DomainInformationClass, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Buffer.value);
            if ((null != Buffer.value)) {
                encoder.WriteUnion(Buffer.value.value);
                encoder.WriteStructDeferral(Buffer.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrSetInformationDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            DOMAIN_INFORMATION_CLASS DomainInformationClass;
            SAMPR_DOMAIN_INFO_BUFFER DomainInformation;
            DomainHandle = decoder.ReadContextHandle();
            DomainInformationClass = ((DOMAIN_INFORMATION_CLASS)(decoder.ReadInt16()));
            DomainInformation = decoder.ReadUnion<SAMPR_DOMAIN_INFO_BUFFER>();
            decoder.ReadStructDeferral<SAMPR_DOMAIN_INFO_BUFFER>(ref DomainInformation);
            var invokeTask = this._obj.SamrSetInformationDomain(DomainHandle, DomainInformationClass, DomainInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrCreateGroupInDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            ms_dtyp.RPC_UNICODE_STRING Name;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> GroupHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            RpcPointer<uint> RelativeId = new RpcPointer<uint>();
            DomainHandle = decoder.ReadContextHandle();
            Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrCreateGroupInDomain(DomainHandle, Name, DesiredAccess, GroupHandle, RelativeId, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(GroupHandle.value);
            encoder.WriteValue(RelativeId.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrEnumerateGroupsInDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            RpcPointer<uint> EnumerationContext;
            RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>>();
            uint PreferedMaximumLength;
            RpcPointer<uint> CountReturned = new RpcPointer<uint>();
            DomainHandle = decoder.ReadContextHandle();
            EnumerationContext = new RpcPointer<uint>();
            EnumerationContext.value = decoder.ReadUInt32();
            PreferedMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrEnumerateGroupsInDomain(DomainHandle, EnumerationContext, Buffer, PreferedMaximumLength, CountReturned, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(EnumerationContext.value);
            encoder.WritePointer(Buffer.value);
            if ((null != Buffer.value)) {
                encoder.WriteFixedStruct(Buffer.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Buffer.value.value);
            }
            encoder.WriteValue(CountReturned.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrCreateUserInDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            ms_dtyp.RPC_UNICODE_STRING Name;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            RpcPointer<uint> RelativeId = new RpcPointer<uint>();
            DomainHandle = decoder.ReadContextHandle();
            Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrCreateUserInDomain(DomainHandle, Name, DesiredAccess, UserHandle, RelativeId, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(UserHandle.value);
            encoder.WriteValue(RelativeId.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrEnumerateUsersInDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            RpcPointer<uint> EnumerationContext;
            uint UserAccountControl;
            RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>>();
            uint PreferedMaximumLength;
            RpcPointer<uint> CountReturned = new RpcPointer<uint>();
            DomainHandle = decoder.ReadContextHandle();
            EnumerationContext = new RpcPointer<uint>();
            EnumerationContext.value = decoder.ReadUInt32();
            UserAccountControl = decoder.ReadUInt32();
            PreferedMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrEnumerateUsersInDomain(DomainHandle, EnumerationContext, UserAccountControl, Buffer, PreferedMaximumLength, CountReturned, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(EnumerationContext.value);
            encoder.WritePointer(Buffer.value);
            if ((null != Buffer.value)) {
                encoder.WriteFixedStruct(Buffer.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Buffer.value.value);
            }
            encoder.WriteValue(CountReturned.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrCreateAliasInDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            ms_dtyp.RPC_UNICODE_STRING AccountName;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> AliasHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            RpcPointer<uint> RelativeId = new RpcPointer<uint>();
            DomainHandle = decoder.ReadContextHandle();
            AccountName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref AccountName);
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrCreateAliasInDomain(DomainHandle, AccountName, DesiredAccess, AliasHandle, RelativeId, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(AliasHandle.value);
            encoder.WriteValue(RelativeId.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrEnumerateAliasesInDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            RpcPointer<uint> EnumerationContext;
            RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_ENUMERATION_BUFFER>>();
            uint PreferedMaximumLength;
            RpcPointer<uint> CountReturned = new RpcPointer<uint>();
            DomainHandle = decoder.ReadContextHandle();
            EnumerationContext = new RpcPointer<uint>();
            EnumerationContext.value = decoder.ReadUInt32();
            PreferedMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrEnumerateAliasesInDomain(DomainHandle, EnumerationContext, Buffer, PreferedMaximumLength, CountReturned, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(EnumerationContext.value);
            encoder.WritePointer(Buffer.value);
            if ((null != Buffer.value)) {
                encoder.WriteFixedStruct(Buffer.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Buffer.value.value);
            }
            encoder.WriteValue(CountReturned.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrGetAliasMembership(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            SAMPR_PSID_ARRAY SidArray;
            RpcPointer<SAMPR_ULONG_ARRAY> Membership = new RpcPointer<SAMPR_ULONG_ARRAY>();
            DomainHandle = decoder.ReadContextHandle();
            SidArray = decoder.ReadFixedStruct<SAMPR_PSID_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SAMPR_PSID_ARRAY>(ref SidArray);
            var invokeTask = this._obj.SamrGetAliasMembership(DomainHandle, SidArray, Membership, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(Membership.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Membership.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrLookupNamesInDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            uint Count;
            ArraySegment<ms_dtyp.RPC_UNICODE_STRING> Names;
            RpcPointer<SAMPR_ULONG_ARRAY> RelativeIds = new RpcPointer<SAMPR_ULONG_ARRAY>();
            RpcPointer<SAMPR_ULONG_ARRAY> Use = new RpcPointer<SAMPR_ULONG_ARRAY>();
            DomainHandle = decoder.ReadContextHandle();
            Count = decoder.ReadUInt32();
            Names = decoder.ReadArraySegmentHeader<ms_dtyp.RPC_UNICODE_STRING>();
            for (int i = 0; (i < Names.Count); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names.Item(i);
                elem_0 = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                Names.Item(i) = elem_0;
            }
            for (int i = 0; (i < Names.Count); i++
            ) {
                ms_dtyp.RPC_UNICODE_STRING elem_0 = Names.Item(i);
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref elem_0);
                Names.Item(i) = elem_0;
            }
            var invokeTask = this._obj.SamrLookupNamesInDomain(DomainHandle, Count, Names, RelativeIds, Use, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(RelativeIds.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(RelativeIds.value);
            encoder.WriteFixedStruct(Use.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Use.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrLookupIdsInDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            uint Count;
            ArraySegment<uint> RelativeIds;
            RpcPointer<SAMPR_RETURNED_USTRING_ARRAY> Names = new RpcPointer<SAMPR_RETURNED_USTRING_ARRAY>();
            RpcPointer<SAMPR_ULONG_ARRAY> Use = new RpcPointer<SAMPR_ULONG_ARRAY>();
            DomainHandle = decoder.ReadContextHandle();
            Count = decoder.ReadUInt32();
            RelativeIds = decoder.ReadArraySegmentHeader<uint>();
            for (int i = 0; (i < RelativeIds.Count); i++
            ) {
                uint elem_0 = RelativeIds.Item(i);
                elem_0 = decoder.ReadUInt32();
                RelativeIds.Item(i) = elem_0;
            }
            var invokeTask = this._obj.SamrLookupIdsInDomain(DomainHandle, Count, RelativeIds, Names, Use, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(Names.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Names.value);
            encoder.WriteFixedStruct(Use.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Use.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrOpenGroup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            uint DesiredAccess;
            uint GroupId;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> GroupHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            DomainHandle = decoder.ReadContextHandle();
            DesiredAccess = decoder.ReadUInt32();
            GroupId = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrOpenGroup(DomainHandle, DesiredAccess, GroupId, GroupHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(GroupHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrQueryInformationGroup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle GroupHandle;
            GROUP_INFORMATION_CLASS GroupInformationClass;
            RpcPointer<RpcPointer<SAMPR_GROUP_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_GROUP_INFO_BUFFER>>();
            GroupHandle = decoder.ReadContextHandle();
            GroupInformationClass = ((GROUP_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.SamrQueryInformationGroup(GroupHandle, GroupInformationClass, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Buffer.value);
            if ((null != Buffer.value)) {
                encoder.WriteUnion(Buffer.value.value);
                encoder.WriteStructDeferral(Buffer.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrSetInformationGroup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle GroupHandle;
            GROUP_INFORMATION_CLASS GroupInformationClass;
            SAMPR_GROUP_INFO_BUFFER Buffer;
            GroupHandle = decoder.ReadContextHandle();
            GroupInformationClass = ((GROUP_INFORMATION_CLASS)(decoder.ReadInt16()));
            Buffer = decoder.ReadUnion<SAMPR_GROUP_INFO_BUFFER>();
            decoder.ReadStructDeferral<SAMPR_GROUP_INFO_BUFFER>(ref Buffer);
            var invokeTask = this._obj.SamrSetInformationGroup(GroupHandle, GroupInformationClass, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrAddMemberToGroup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle GroupHandle;
            uint MemberId;
            uint Attributes;
            GroupHandle = decoder.ReadContextHandle();
            MemberId = decoder.ReadUInt32();
            Attributes = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrAddMemberToGroup(GroupHandle, MemberId, Attributes, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrDeleteGroup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> GroupHandle;
            GroupHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            GroupHandle.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.SamrDeleteGroup(GroupHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(GroupHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrRemoveMemberFromGroup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle GroupHandle;
            uint MemberId;
            GroupHandle = decoder.ReadContextHandle();
            MemberId = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrRemoveMemberFromGroup(GroupHandle, MemberId, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrGetMembersInGroup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle GroupHandle;
            RpcPointer<RpcPointer<SAMPR_GET_MEMBERS_BUFFER>> Members = new RpcPointer<RpcPointer<SAMPR_GET_MEMBERS_BUFFER>>();
            GroupHandle = decoder.ReadContextHandle();
            var invokeTask = this._obj.SamrGetMembersInGroup(GroupHandle, Members, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Members.value);
            if ((null != Members.value)) {
                encoder.WriteFixedStruct(Members.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Members.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrSetMemberAttributesOfGroup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle GroupHandle;
            uint MemberId;
            uint Attributes;
            GroupHandle = decoder.ReadContextHandle();
            MemberId = decoder.ReadUInt32();
            Attributes = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrSetMemberAttributesOfGroup(GroupHandle, MemberId, Attributes, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrOpenAlias(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            uint DesiredAccess;
            uint AliasId;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> AliasHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            DomainHandle = decoder.ReadContextHandle();
            DesiredAccess = decoder.ReadUInt32();
            AliasId = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrOpenAlias(DomainHandle, DesiredAccess, AliasId, AliasHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(AliasHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrQueryInformationAlias(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AliasHandle;
            ALIAS_INFORMATION_CLASS AliasInformationClass;
            RpcPointer<RpcPointer<SAMPR_ALIAS_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_ALIAS_INFO_BUFFER>>();
            AliasHandle = decoder.ReadContextHandle();
            AliasInformationClass = ((ALIAS_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.SamrQueryInformationAlias(AliasHandle, AliasInformationClass, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Buffer.value);
            if ((null != Buffer.value)) {
                encoder.WriteUnion(Buffer.value.value);
                encoder.WriteStructDeferral(Buffer.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrSetInformationAlias(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AliasHandle;
            ALIAS_INFORMATION_CLASS AliasInformationClass;
            SAMPR_ALIAS_INFO_BUFFER Buffer;
            AliasHandle = decoder.ReadContextHandle();
            AliasInformationClass = ((ALIAS_INFORMATION_CLASS)(decoder.ReadInt16()));
            Buffer = decoder.ReadUnion<SAMPR_ALIAS_INFO_BUFFER>();
            decoder.ReadStructDeferral<SAMPR_ALIAS_INFO_BUFFER>(ref Buffer);
            var invokeTask = this._obj.SamrSetInformationAlias(AliasHandle, AliasInformationClass, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrDeleteAlias(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> AliasHandle;
            AliasHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            AliasHandle.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.SamrDeleteAlias(AliasHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(AliasHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrAddMemberToAlias(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AliasHandle;
            ms_dtyp.RPC_SID MemberId;
            AliasHandle = decoder.ReadContextHandle();
            MemberId = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref MemberId);
            var invokeTask = this._obj.SamrAddMemberToAlias(AliasHandle, MemberId, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrRemoveMemberFromAlias(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AliasHandle;
            ms_dtyp.RPC_SID MemberId;
            AliasHandle = decoder.ReadContextHandle();
            MemberId = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref MemberId);
            var invokeTask = this._obj.SamrRemoveMemberFromAlias(AliasHandle, MemberId, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrGetMembersInAlias(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AliasHandle;
            RpcPointer<SAMPR_PSID_ARRAY_OUT> Members = new RpcPointer<SAMPR_PSID_ARRAY_OUT>();
            AliasHandle = decoder.ReadContextHandle();
            var invokeTask = this._obj.SamrGetMembersInAlias(AliasHandle, Members, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(Members.value, Titanis.DceRpc.NdrAlignment.NativePtr);
            encoder.WriteStructDeferral(Members.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrOpenUser(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            uint DesiredAccess;
            uint UserId;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            DomainHandle = decoder.ReadContextHandle();
            DesiredAccess = decoder.ReadUInt32();
            UserId = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrOpenUser(DomainHandle, DesiredAccess, UserId, UserHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(UserHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrDeleteUser(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle;
            UserHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            UserHandle.value = decoder.ReadContextHandle();
            var invokeTask = this._obj.SamrDeleteUser(UserHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(UserHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrQueryInformationUser(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle UserHandle;
            USER_INFORMATION_CLASS UserInformationClass;
            RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>>();
            UserHandle = decoder.ReadContextHandle();
            UserInformationClass = ((USER_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.SamrQueryInformationUser(UserHandle, UserInformationClass, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Buffer.value);
            if ((null != Buffer.value)) {
                encoder.WriteUnion(Buffer.value.value);
                encoder.WriteStructDeferral(Buffer.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrSetInformationUser(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle UserHandle;
            USER_INFORMATION_CLASS UserInformationClass;
            SAMPR_USER_INFO_BUFFER Buffer;
            UserHandle = decoder.ReadContextHandle();
            UserInformationClass = ((USER_INFORMATION_CLASS)(decoder.ReadInt16()));
            Buffer = decoder.ReadUnion<SAMPR_USER_INFO_BUFFER>();
            decoder.ReadStructDeferral<SAMPR_USER_INFO_BUFFER>(ref Buffer);
            var invokeTask = this._obj.SamrSetInformationUser(UserHandle, UserInformationClass, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrChangePasswordUser(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle UserHandle;
            byte LmPresent;
            RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmEncryptedWithNewLm;
            RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithOldLm;
            byte NtPresent;
            RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtEncryptedWithNewNt;
            RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithOldNt;
            byte NtCrossEncryptionPresent;
            RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewNtEncryptedWithNewLm;
            byte LmCrossEncryptionPresent;
            RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> NewLmEncryptedWithNewNt;
            UserHandle = decoder.ReadContextHandle();
            LmPresent = decoder.ReadUnsignedChar();
            OldLmEncryptedWithNewLm = decoder.ReadPointer<ENCRYPTED_LM_OWF_PASSWORD>();
            if ((null != OldLmEncryptedWithNewLm)) {
                OldLmEncryptedWithNewLm.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref OldLmEncryptedWithNewLm.value);
            }
            NewLmEncryptedWithOldLm = decoder.ReadPointer<ENCRYPTED_LM_OWF_PASSWORD>();
            if ((null != NewLmEncryptedWithOldLm)) {
                NewLmEncryptedWithOldLm.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref NewLmEncryptedWithOldLm.value);
            }
            NtPresent = decoder.ReadUnsignedChar();
            OldNtEncryptedWithNewNt = decoder.ReadPointer<ENCRYPTED_LM_OWF_PASSWORD>();
            if ((null != OldNtEncryptedWithNewNt)) {
                OldNtEncryptedWithNewNt.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref OldNtEncryptedWithNewNt.value);
            }
            NewNtEncryptedWithOldNt = decoder.ReadPointer<ENCRYPTED_LM_OWF_PASSWORD>();
            if ((null != NewNtEncryptedWithOldNt)) {
                NewNtEncryptedWithOldNt.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref NewNtEncryptedWithOldNt.value);
            }
            NtCrossEncryptionPresent = decoder.ReadUnsignedChar();
            NewNtEncryptedWithNewLm = decoder.ReadPointer<ENCRYPTED_LM_OWF_PASSWORD>();
            if ((null != NewNtEncryptedWithNewLm)) {
                NewNtEncryptedWithNewLm.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref NewNtEncryptedWithNewLm.value);
            }
            LmCrossEncryptionPresent = decoder.ReadUnsignedChar();
            NewLmEncryptedWithNewNt = decoder.ReadPointer<ENCRYPTED_LM_OWF_PASSWORD>();
            if ((null != NewLmEncryptedWithNewNt)) {
                NewLmEncryptedWithNewNt.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref NewLmEncryptedWithNewNt.value);
            }
            var invokeTask = this._obj.SamrChangePasswordUser(UserHandle, LmPresent, OldLmEncryptedWithNewLm, NewLmEncryptedWithOldLm, NtPresent, OldNtEncryptedWithNewNt, NewNtEncryptedWithOldNt, NtCrossEncryptionPresent, NewNtEncryptedWithNewLm, LmCrossEncryptionPresent, NewLmEncryptedWithNewNt, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrGetGroupsForUser(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle UserHandle;
            RpcPointer<RpcPointer<SAMPR_GET_GROUPS_BUFFER>> Groups = new RpcPointer<RpcPointer<SAMPR_GET_GROUPS_BUFFER>>();
            UserHandle = decoder.ReadContextHandle();
            var invokeTask = this._obj.SamrGetGroupsForUser(UserHandle, Groups, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Groups.value);
            if ((null != Groups.value)) {
                encoder.WriteFixedStruct(Groups.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
                encoder.WriteStructDeferral(Groups.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrQueryDisplayInformation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            DOMAIN_DISPLAY_INFORMATION DisplayInformationClass;
            uint Index;
            uint EntryCount;
            uint PreferredMaximumLength;
            RpcPointer<uint> TotalAvailable = new RpcPointer<uint>();
            RpcPointer<uint> TotalReturned = new RpcPointer<uint>();
            RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer = new RpcPointer<SAMPR_DISPLAY_INFO_BUFFER>();
            DomainHandle = decoder.ReadContextHandle();
            DisplayInformationClass = ((DOMAIN_DISPLAY_INFORMATION)(decoder.ReadInt16()));
            Index = decoder.ReadUInt32();
            EntryCount = decoder.ReadUInt32();
            PreferredMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrQueryDisplayInformation(DomainHandle, DisplayInformationClass, Index, EntryCount, PreferredMaximumLength, TotalAvailable, TotalReturned, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(TotalAvailable.value);
            encoder.WriteValue(TotalReturned.value);
            encoder.WriteUnion(Buffer.value);
            encoder.WriteStructDeferral(Buffer.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrGetDisplayEnumerationIndex(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            DOMAIN_DISPLAY_INFORMATION DisplayInformationClass;
            ms_dtyp.RPC_UNICODE_STRING Prefix;
            RpcPointer<uint> Index = new RpcPointer<uint>();
            DomainHandle = decoder.ReadContextHandle();
            DisplayInformationClass = ((DOMAIN_DISPLAY_INFORMATION)(decoder.ReadInt16()));
            Prefix = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Prefix);
            var invokeTask = this._obj.SamrGetDisplayEnumerationIndex(DomainHandle, DisplayInformationClass, Prefix, Index, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(Index.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum42NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum42NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum43NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum43NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_SamrGetUserDomainPasswordInformation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle UserHandle;
            RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation = new RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION>();
            UserHandle = decoder.ReadContextHandle();
            var invokeTask = this._obj.SamrGetUserDomainPasswordInformation(UserHandle, PasswordInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(PasswordInformation.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(PasswordInformation.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrRemoveMemberFromForeignDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            ms_dtyp.RPC_SID MemberSid;
            DomainHandle = decoder.ReadContextHandle();
            MemberSid = decoder.ReadConformantStruct<ms_dtyp.RPC_SID>(Titanis.DceRpc.NdrAlignment._4Byte);
            decoder.ReadStructDeferral<ms_dtyp.RPC_SID>(ref MemberSid);
            var invokeTask = this._obj.SamrRemoveMemberFromForeignDomain(DomainHandle, MemberSid, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrQueryInformationDomain2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            DOMAIN_INFORMATION_CLASS DomainInformationClass;
            RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_DOMAIN_INFO_BUFFER>>();
            DomainHandle = decoder.ReadContextHandle();
            DomainInformationClass = ((DOMAIN_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.SamrQueryInformationDomain2(DomainHandle, DomainInformationClass, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Buffer.value);
            if ((null != Buffer.value)) {
                encoder.WriteUnion(Buffer.value.value);
                encoder.WriteStructDeferral(Buffer.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrQueryInformationUser2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle UserHandle;
            USER_INFORMATION_CLASS UserInformationClass;
            RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>> Buffer = new RpcPointer<RpcPointer<SAMPR_USER_INFO_BUFFER>>();
            UserHandle = decoder.ReadContextHandle();
            UserInformationClass = ((USER_INFORMATION_CLASS)(decoder.ReadInt16()));
            var invokeTask = this._obj.SamrQueryInformationUser2(UserHandle, UserInformationClass, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Buffer.value);
            if ((null != Buffer.value)) {
                encoder.WriteUnion(Buffer.value.value);
                encoder.WriteStructDeferral(Buffer.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrQueryDisplayInformation2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            DOMAIN_DISPLAY_INFORMATION DisplayInformationClass;
            uint Index;
            uint EntryCount;
            uint PreferredMaximumLength;
            RpcPointer<uint> TotalAvailable = new RpcPointer<uint>();
            RpcPointer<uint> TotalReturned = new RpcPointer<uint>();
            RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer = new RpcPointer<SAMPR_DISPLAY_INFO_BUFFER>();
            DomainHandle = decoder.ReadContextHandle();
            DisplayInformationClass = ((DOMAIN_DISPLAY_INFORMATION)(decoder.ReadInt16()));
            Index = decoder.ReadUInt32();
            EntryCount = decoder.ReadUInt32();
            PreferredMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrQueryDisplayInformation2(DomainHandle, DisplayInformationClass, Index, EntryCount, PreferredMaximumLength, TotalAvailable, TotalReturned, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(TotalAvailable.value);
            encoder.WriteValue(TotalReturned.value);
            encoder.WriteUnion(Buffer.value);
            encoder.WriteStructDeferral(Buffer.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrGetDisplayEnumerationIndex2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            DOMAIN_DISPLAY_INFORMATION DisplayInformationClass;
            ms_dtyp.RPC_UNICODE_STRING Prefix;
            RpcPointer<uint> Index = new RpcPointer<uint>();
            DomainHandle = decoder.ReadContextHandle();
            DisplayInformationClass = ((DOMAIN_DISPLAY_INFORMATION)(decoder.ReadInt16()));
            Prefix = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Prefix);
            var invokeTask = this._obj.SamrGetDisplayEnumerationIndex2(DomainHandle, DisplayInformationClass, Prefix, Index, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(Index.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrCreateUser2InDomain(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            ms_dtyp.RPC_UNICODE_STRING Name;
            uint AccountType;
            uint DesiredAccess;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> UserHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            RpcPointer<uint> GrantedAccess = new RpcPointer<uint>();
            RpcPointer<uint> RelativeId = new RpcPointer<uint>();
            DomainHandle = decoder.ReadContextHandle();
            Name = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Name);
            AccountType = decoder.ReadUInt32();
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrCreateUser2InDomain(DomainHandle, Name, AccountType, DesiredAccess, UserHandle, GrantedAccess, RelativeId, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(UserHandle.value);
            encoder.WriteValue(GrantedAccess.value);
            encoder.WriteValue(RelativeId.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrQueryDisplayInformation3(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle DomainHandle;
            DOMAIN_DISPLAY_INFORMATION DisplayInformationClass;
            uint Index;
            uint EntryCount;
            uint PreferredMaximumLength;
            RpcPointer<uint> TotalAvailable = new RpcPointer<uint>();
            RpcPointer<uint> TotalReturned = new RpcPointer<uint>();
            RpcPointer<SAMPR_DISPLAY_INFO_BUFFER> Buffer = new RpcPointer<SAMPR_DISPLAY_INFO_BUFFER>();
            DomainHandle = decoder.ReadContextHandle();
            DisplayInformationClass = ((DOMAIN_DISPLAY_INFORMATION)(decoder.ReadInt16()));
            Index = decoder.ReadUInt32();
            EntryCount = decoder.ReadUInt32();
            PreferredMaximumLength = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrQueryDisplayInformation3(DomainHandle, DisplayInformationClass, Index, EntryCount, PreferredMaximumLength, TotalAvailable, TotalReturned, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(TotalAvailable.value);
            encoder.WriteValue(TotalReturned.value);
            encoder.WriteUnion(Buffer.value);
            encoder.WriteStructDeferral(Buffer.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrAddMultipleMembersToAlias(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AliasHandle;
            SAMPR_PSID_ARRAY MembersBuffer;
            AliasHandle = decoder.ReadContextHandle();
            MembersBuffer = decoder.ReadFixedStruct<SAMPR_PSID_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SAMPR_PSID_ARRAY>(ref MembersBuffer);
            var invokeTask = this._obj.SamrAddMultipleMembersToAlias(AliasHandle, MembersBuffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrRemoveMultipleMembersFromAlias(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle AliasHandle;
            SAMPR_PSID_ARRAY MembersBuffer;
            AliasHandle = decoder.ReadContextHandle();
            MembersBuffer = decoder.ReadFixedStruct<SAMPR_PSID_ARRAY>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<SAMPR_PSID_ARRAY>(ref MembersBuffer);
            var invokeTask = this._obj.SamrRemoveMultipleMembersFromAlias(AliasHandle, MembersBuffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrOemChangePasswordUser2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<RPC_STRING> ServerName;
            RPC_STRING UserName;
            RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm;
            RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewLm;
            ServerName = decoder.ReadPointer<RPC_STRING>();
            if ((null != ServerName)) {
                ServerName.value = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<RPC_STRING>(ref ServerName.value);
            }
            UserName = decoder.ReadFixedStruct<RPC_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<RPC_STRING>(ref UserName);
            NewPasswordEncryptedWithOldLm = decoder.ReadPointer<SAMPR_ENCRYPTED_USER_PASSWORD>();
            if ((null != NewPasswordEncryptedWithOldLm)) {
                NewPasswordEncryptedWithOldLm.value = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD>(ref NewPasswordEncryptedWithOldLm.value);
            }
            OldLmOwfPasswordEncryptedWithNewLm = decoder.ReadPointer<ENCRYPTED_LM_OWF_PASSWORD>();
            if ((null != OldLmOwfPasswordEncryptedWithNewLm)) {
                OldLmOwfPasswordEncryptedWithNewLm.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref OldLmOwfPasswordEncryptedWithNewLm.value);
            }
            var invokeTask = this._obj.SamrOemChangePasswordUser2(ServerName, UserName, NewPasswordEncryptedWithOldLm, OldLmOwfPasswordEncryptedWithNewLm, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrUnicodeChangePasswordUser2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<ms_dtyp.RPC_UNICODE_STRING> ServerName;
            ms_dtyp.RPC_UNICODE_STRING UserName;
            RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldNt;
            RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldNtOwfPasswordEncryptedWithNewNt;
            byte LmPresent;
            RpcPointer<SAMPR_ENCRYPTED_USER_PASSWORD> NewPasswordEncryptedWithOldLm;
            RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> OldLmOwfPasswordEncryptedWithNewNt;
            ServerName = decoder.ReadPointer<ms_dtyp.RPC_UNICODE_STRING>();
            if ((null != ServerName)) {
                ServerName.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref ServerName.value);
            }
            UserName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
            decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref UserName);
            NewPasswordEncryptedWithOldNt = decoder.ReadPointer<SAMPR_ENCRYPTED_USER_PASSWORD>();
            if ((null != NewPasswordEncryptedWithOldNt)) {
                NewPasswordEncryptedWithOldNt.value = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD>(ref NewPasswordEncryptedWithOldNt.value);
            }
            OldNtOwfPasswordEncryptedWithNewNt = decoder.ReadPointer<ENCRYPTED_LM_OWF_PASSWORD>();
            if ((null != OldNtOwfPasswordEncryptedWithNewNt)) {
                OldNtOwfPasswordEncryptedWithNewNt.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref OldNtOwfPasswordEncryptedWithNewNt.value);
            }
            LmPresent = decoder.ReadUnsignedChar();
            NewPasswordEncryptedWithOldLm = decoder.ReadPointer<SAMPR_ENCRYPTED_USER_PASSWORD>();
            if ((null != NewPasswordEncryptedWithOldLm)) {
                NewPasswordEncryptedWithOldLm.value = decoder.ReadFixedStruct<SAMPR_ENCRYPTED_USER_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<SAMPR_ENCRYPTED_USER_PASSWORD>(ref NewPasswordEncryptedWithOldLm.value);
            }
            OldLmOwfPasswordEncryptedWithNewNt = decoder.ReadPointer<ENCRYPTED_LM_OWF_PASSWORD>();
            if ((null != OldLmOwfPasswordEncryptedWithNewNt)) {
                OldLmOwfPasswordEncryptedWithNewNt.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref OldLmOwfPasswordEncryptedWithNewNt.value);
            }
            var invokeTask = this._obj.SamrUnicodeChangePasswordUser2(ServerName, UserName, NewPasswordEncryptedWithOldNt, OldNtOwfPasswordEncryptedWithNewNt, LmPresent, NewPasswordEncryptedWithOldLm, OldLmOwfPasswordEncryptedWithNewNt, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrGetDomainPasswordInformation(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused;
            RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION> PasswordInformation = new RpcPointer<USER_DOMAIN_PASSWORD_INFORMATION>();
            Unused = decoder.ReadPointer<ms_dtyp.RPC_UNICODE_STRING>();
            if ((null != Unused)) {
                Unused.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Unused.value);
            }
            var invokeTask = this._obj.SamrGetDomainPasswordInformation(Unused, PasswordInformation, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteFixedStruct(PasswordInformation.value, Titanis.DceRpc.NdrAlignment._4Byte);
            encoder.WriteStructDeferral(PasswordInformation.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrConnect2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            uint DesiredAccess;
            if ((decoder.ReadReferentId() == 0)) {
                ServerName = null;
            }
            else {
                ServerName = decoder.ReadWideCharString();
            }
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrConnect2(ServerName, ServerHandle, DesiredAccess, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(ServerHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrSetInformationUser2(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle UserHandle;
            USER_INFORMATION_CLASS UserInformationClass;
            SAMPR_USER_INFO_BUFFER Buffer;
            UserHandle = decoder.ReadContextHandle();
            UserInformationClass = ((USER_INFORMATION_CLASS)(decoder.ReadInt16()));
            Buffer = decoder.ReadUnion<SAMPR_USER_INFO_BUFFER>();
            decoder.ReadStructDeferral<SAMPR_USER_INFO_BUFFER>(ref Buffer);
            var invokeTask = this._obj.SamrSetInformationUser2(UserHandle, UserInformationClass, Buffer, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum59NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum59NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum60NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum60NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum61NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum61NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_SamrConnect4(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            uint ClientRevision;
            uint DesiredAccess;
            if ((decoder.ReadReferentId() == 0)) {
                ServerName = null;
            }
            else {
                ServerName = decoder.ReadWideCharString();
            }
            ClientRevision = decoder.ReadUInt32();
            DesiredAccess = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrConnect4(ServerName, ServerHandle, ClientRevision, DesiredAccess, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteContextHandle(ServerHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum63NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum63NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_SamrConnect5(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            string ServerName;
            uint DesiredAccess;
            uint InVersion;
            SAMPR_REVISION_INFO InRevisionInfo;
            RpcPointer<uint> OutVersion = new RpcPointer<uint>();
            RpcPointer<SAMPR_REVISION_INFO> OutRevisionInfo = new RpcPointer<SAMPR_REVISION_INFO>();
            RpcPointer<Titanis.DceRpc.RpcContextHandle> ServerHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
            if ((decoder.ReadReferentId() == 0)) {
                ServerName = null;
            }
            else {
                ServerName = decoder.ReadWideCharString();
            }
            DesiredAccess = decoder.ReadUInt32();
            InVersion = decoder.ReadUInt32();
            InRevisionInfo = decoder.ReadUnion<SAMPR_REVISION_INFO>();
            decoder.ReadStructDeferral<SAMPR_REVISION_INFO>(ref InRevisionInfo);
            var invokeTask = this._obj.SamrConnect5(ServerName, DesiredAccess, InVersion, InRevisionInfo, OutVersion, OutRevisionInfo, ServerHandle, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(OutVersion.value);
            encoder.WriteUnion(OutRevisionInfo.value);
            encoder.WriteStructDeferral(OutRevisionInfo.value);
            encoder.WriteContextHandle(ServerHandle.value);
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrRidToSid(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            Titanis.DceRpc.RpcContextHandle ObjectHandle;
            uint Rid;
            RpcPointer<RpcPointer<ms_dtyp.RPC_SID>> Sid = new RpcPointer<RpcPointer<ms_dtyp.RPC_SID>>();
            ObjectHandle = decoder.ReadContextHandle();
            Rid = decoder.ReadUInt32();
            var invokeTask = this._obj.SamrRidToSid(ObjectHandle, Rid, Sid, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(Sid.value);
            if ((null != Sid.value)) {
                encoder.WriteConformantStruct(Sid.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
                encoder.WriteStructDeferral(Sid.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrSetDSRMPassword(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            RpcPointer<ms_dtyp.RPC_UNICODE_STRING> Unused;
            uint UserId;
            RpcPointer<ENCRYPTED_LM_OWF_PASSWORD> EncryptedNtOwfPassword;
            Unused = decoder.ReadPointer<ms_dtyp.RPC_UNICODE_STRING>();
            if ((null != Unused)) {
                Unused.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(Titanis.DceRpc.NdrAlignment.NativePtr);
                decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref Unused.value);
            }
            UserId = decoder.ReadUInt32();
            EncryptedNtOwfPassword = decoder.ReadPointer<ENCRYPTED_LM_OWF_PASSWORD>();
            if ((null != EncryptedNtOwfPassword)) {
                EncryptedNtOwfPassword.value = decoder.ReadFixedStruct<ENCRYPTED_LM_OWF_PASSWORD>(Titanis.DceRpc.NdrAlignment._1Byte);
                decoder.ReadStructDeferral<ENCRYPTED_LM_OWF_PASSWORD>(ref EncryptedNtOwfPassword.value);
            }
            var invokeTask = this._obj.SamrSetDSRMPassword(Unused, UserId, EncryptedNtOwfPassword, cancellationToken);
            var retval = await invokeTask;
            encoder.WriteValue(retval);
        }
        private async Task Invoke_SamrValidatePassword(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            PASSWORD_POLICY_VALIDATION_TYPE ValidationType;
            SAM_VALIDATE_INPUT_ARG InputArg;
            RpcPointer<RpcPointer<SAM_VALIDATE_OUTPUT_ARG>> OutputArg = new RpcPointer<RpcPointer<SAM_VALIDATE_OUTPUT_ARG>>();
            ValidationType = ((PASSWORD_POLICY_VALIDATION_TYPE)(decoder.ReadInt16()));
            InputArg = decoder.ReadUnion<SAM_VALIDATE_INPUT_ARG>();
            decoder.ReadStructDeferral<SAM_VALIDATE_INPUT_ARG>(ref InputArg);
            var invokeTask = this._obj.SamrValidatePassword(ValidationType, InputArg, OutputArg, cancellationToken);
            var retval = await invokeTask;
            encoder.WritePointer(OutputArg.value);
            if ((null != OutputArg.value)) {
                encoder.WriteUnion(OutputArg.value.value);
                encoder.WriteStructDeferral(OutputArg.value.value);
            }
            encoder.WriteValue(retval);
        }
        private async Task Invoke_Opnum68NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum68NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
        private async Task Invoke_Opnum69NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken) {
            var invokeTask = this._obj.Opnum69NotUsedOnWire(cancellationToken);
            await invokeTask;
        }
    }
}
