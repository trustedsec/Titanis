namespace dceidl {
    using System;
    using System.Threading.Tasks;
    using Titanis;
    using Titanis.DceRpc;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct twr_t : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.tower_length);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.tower_length = decoder.ReadUInt32();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.tower_octet_string);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.tower_octet_string = decoder.ReadArrayHeader<byte>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.tower_octet_string.Length); i++
            ) {
                byte elem_0 = this.tower_octet_string[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.tower_octet_string.Length); i++
            ) {
                byte elem_0 = this.tower_octet_string[i];
                elem_0 = decoder.ReadByte();
                this.tower_octet_string[i] = elem_0;
            }
        }
        public uint tower_length;
        public byte[] tower_octet_string;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ndr_format_t : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.int_rep);
            encoder.WriteValue(this.char_rep);
            encoder.WriteValue(this.float_rep);
            encoder.WriteValue(this.reserved);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.int_rep = decoder.ReadByte();
            this.char_rep = decoder.ReadByte();
            this.float_rep = decoder.ReadByte();
            this.reserved = decoder.ReadByte();
        }
        public byte int_rep;
        public byte char_rep;
        public byte float_rep;
        public byte reserved;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ndr_context_handle : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.context_handle_attributes);
            encoder.WriteValue(this.context_handle_uuid);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.context_handle_attributes = decoder.ReadUInt32();
            this.context_handle_uuid = decoder.ReadUuid();
        }
        public uint context_handle_attributes;
        public System.Guid context_handle_uuid;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ISO_MULTI_LINGUAL : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.row);
            encoder.WriteValue(this.column);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.row = decoder.ReadByte();
            this.column = decoder.ReadByte();
        }
        public byte row;
        public byte column;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct ISO_UCS : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.group);
            encoder.WriteValue(this.plane);
            encoder.WriteValue(this.row);
            encoder.WriteValue(this.column);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.group = decoder.ReadByte();
            this.plane = decoder.ReadByte();
            this.row = decoder.ReadByte();
            this.column = decoder.ReadByte();
        }
        public byte group;
        public byte plane;
        public byte row;
        public byte column;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct rpc_if_id_t : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.uuid);
            encoder.WriteValue(this.vers_major);
            encoder.WriteValue(this.vers_minor);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.uuid = decoder.ReadUuid();
            this.vers_major = decoder.ReadUInt16();
            this.vers_minor = decoder.ReadUInt16();
        }
        public System.Guid uuid;
        public ushort vers_major;
        public ushort vers_minor;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct rpc_if_id_vector_t : Titanis.DceRpc.IRpcConformantStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.count);
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.count = decoder.ReadUInt32();
        }
        public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteArrayHeader(this.if_id);
        }
        public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder) {
            this.if_id = decoder.ReadArrayHeader<RpcPointer<Titanis.DceRpc.RpcInterfaceId>>();
        }
        public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.if_id.Length); i++
            ) {
                RpcPointer<Titanis.DceRpc.RpcInterfaceId> elem_0 = this.if_id[i];
                encoder.WritePointer(elem_0);
            }
        }
        public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.if_id.Length); i++
            ) {
                RpcPointer<Titanis.DceRpc.RpcInterfaceId> elem_0 = this.if_id[i];
                elem_0 = decoder.ReadFullPointer<Titanis.DceRpc.RpcInterfaceId>();
                this.if_id[i] = elem_0;
            }
        }
        public uint count;
        public RpcPointer<Titanis.DceRpc.RpcInterfaceId>[] if_id;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
            for (int i = 0; (i < this.if_id.Length); i++
            ) {
                RpcPointer<Titanis.DceRpc.RpcInterfaceId> elem_0 = this.if_id[i];
                if ((null != elem_0)) {
                    encoder.WriteValue(elem_0.value);
                }
            }
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
            for (int i = 0; (i < this.if_id.Length); i++
            ) {
                RpcPointer<Titanis.DceRpc.RpcInterfaceId> elem_0 = this.if_id[i];
                if ((null != elem_0)) {
                    elem_0.value = decoder.ReadRpcInterfaceId();
                }
                this.if_id[i] = elem_0;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9")]
    public struct rpc_stats_vector_t : Titanis.DceRpc.IRpcFixedStruct {
        public void Encode(Titanis.DceRpc.IRpcEncoder encoder) {
            encoder.WriteValue(this.count);
            if ((this.stats == null)) {
                this.stats = new uint[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                uint elem_0 = this.stats[i];
                encoder.WriteValue(elem_0);
            }
        }
        public void Decode(Titanis.DceRpc.IRpcDecoder decoder) {
            this.count = decoder.ReadUInt32();
            if ((this.stats == null)) {
                this.stats = new uint[1];
            }
            for (int i = 0; (i < 1); i++
            ) {
                uint elem_0 = this.stats[i];
                elem_0 = decoder.ReadUInt32();
                this.stats[i] = elem_0;
            }
        }
        public uint count;
        public uint[] stats;
        public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder) {
        }
        public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder) {
        }
    }
}
