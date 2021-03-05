#pragma warning disable

namespace ms_srvs
{
	using System;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct CONNECTION_INFO_0 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.coni0_id);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.coni0_id = decoder.ReadUInt32();
		}
		public uint coni0_id;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct CONNECT_INFO_0_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<CONNECTION_INFO_0[]>();
		}
		public uint EntriesRead;
		public RpcPointer<CONNECTION_INFO_0[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					CONNECTION_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					CONNECTION_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<CONNECTION_INFO_0>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					CONNECTION_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<CONNECTION_INFO_0>(Titanis.DceRpc.NdrAlignment._4Byte);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					CONNECTION_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<CONNECTION_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct CONNECTION_INFO_1 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.coni1_id);
			encoder.WriteValue(this.coni1_type);
			encoder.WriteValue(this.coni1_num_opens);
			encoder.WriteValue(this.coni1_num_users);
			encoder.WriteValue(this.coni1_time);
			encoder.WritePointer(this.coni1_username);
			encoder.WritePointer(this.coni1_netname);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.coni1_id = decoder.ReadUInt32();
			this.coni1_type = decoder.ReadUInt32();
			this.coni1_num_opens = decoder.ReadUInt32();
			this.coni1_num_users = decoder.ReadUInt32();
			this.coni1_time = decoder.ReadUInt32();
			this.coni1_username = decoder.ReadPointer<string>();
			this.coni1_netname = decoder.ReadPointer<string>();
		}
		public uint coni1_id;
		public uint coni1_type;
		public uint coni1_num_opens;
		public uint coni1_num_users;
		public uint coni1_time;
		public RpcPointer<string> coni1_username;
		public RpcPointer<string> coni1_netname;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.coni1_username))
			{
				encoder.WriteWideCharString(this.coni1_username.value);
			}
			if ((null != this.coni1_netname))
			{
				encoder.WriteWideCharString(this.coni1_netname.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.coni1_username))
			{
				this.coni1_username.value = decoder.ReadWideCharString();
			}
			if ((null != this.coni1_netname))
			{
				this.coni1_netname.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct CONNECT_INFO_1_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<CONNECTION_INFO_1[]>();
		}
		public uint EntriesRead;
		public RpcPointer<CONNECTION_INFO_1[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					CONNECTION_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					CONNECTION_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<CONNECTION_INFO_1>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					CONNECTION_INFO_1 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<CONNECTION_INFO_1>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					CONNECTION_INFO_1 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<CONNECTION_INFO_1>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct CONNECT_ENUM_UNION : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint Level;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 0))
			{
				encoder.WritePointer(this.Level0);
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					encoder.WritePointer(this.Level1);
				}
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 0))
			{
				this.Level0 = decoder.ReadPointer<CONNECT_INFO_0_CONTAINER>();
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					this.Level1 = decoder.ReadPointer<CONNECT_INFO_1_CONTAINER>();
				}
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.Level)) == 0))
			{
				if ((null != this.Level0))
				{
					encoder.WriteFixedStruct(this.Level0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(this.Level0.value);
				}
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					if ((null != this.Level1))
					{
						encoder.WriteFixedStruct(this.Level1.value, Titanis.DceRpc.NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level1.value);
					}
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.Level)) == 0))
			{
				if ((null != this.Level0))
				{
					this.Level0.value = decoder.ReadFixedStruct<CONNECT_INFO_0_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<CONNECT_INFO_0_CONTAINER>(ref this.Level0.value);
				}
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					if ((null != this.Level1))
					{
						this.Level1.value = decoder.ReadFixedStruct<CONNECT_INFO_1_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<CONNECT_INFO_1_CONTAINER>(ref this.Level1.value);
					}
				}
			}
		}
		public RpcPointer<CONNECT_INFO_0_CONTAINER> Level0;
		public RpcPointer<CONNECT_INFO_1_CONTAINER> Level1;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct CONNECT_ENUM_STRUCT : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.ConnectInfo);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.ConnectInfo = decoder.ReadUnion<CONNECT_ENUM_UNION>();
		}
		public uint Level;
		public CONNECT_ENUM_UNION ConnectInfo;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ConnectInfo);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<CONNECT_ENUM_UNION>(ref this.ConnectInfo);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct FILE_INFO_2 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.fi2_id);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.fi2_id = decoder.ReadUInt32();
		}
		public uint fi2_id;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct FILE_INFO_2_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<FILE_INFO_2[]>();
		}
		public uint EntriesRead;
		public RpcPointer<FILE_INFO_2[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					FILE_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					FILE_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<FILE_INFO_2>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					FILE_INFO_2 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<FILE_INFO_2>(Titanis.DceRpc.NdrAlignment._4Byte);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					FILE_INFO_2 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<FILE_INFO_2>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct FILE_INFO_3 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.fi3_id);
			encoder.WriteValue(this.fi3_permissions);
			encoder.WriteValue(this.fi3_num_locks);
			encoder.WritePointer(this.fi3_pathname);
			encoder.WritePointer(this.fi3_username);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.fi3_id = decoder.ReadUInt32();
			this.fi3_permissions = decoder.ReadUInt32();
			this.fi3_num_locks = decoder.ReadUInt32();
			this.fi3_pathname = decoder.ReadPointer<string>();
			this.fi3_username = decoder.ReadPointer<string>();
		}
		public uint fi3_id;
		public uint fi3_permissions;
		public uint fi3_num_locks;
		public RpcPointer<string> fi3_pathname;
		public RpcPointer<string> fi3_username;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.fi3_pathname))
			{
				encoder.WriteWideCharString(this.fi3_pathname.value);
			}
			if ((null != this.fi3_username))
			{
				encoder.WriteWideCharString(this.fi3_username.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.fi3_pathname))
			{
				this.fi3_pathname.value = decoder.ReadWideCharString();
			}
			if ((null != this.fi3_username))
			{
				this.fi3_username.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct FILE_INFO_3_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<FILE_INFO_3[]>();
		}
		public uint EntriesRead;
		public RpcPointer<FILE_INFO_3[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					FILE_INFO_3 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					FILE_INFO_3 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<FILE_INFO_3>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					FILE_INFO_3 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<FILE_INFO_3>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					FILE_INFO_3 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<FILE_INFO_3>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct FILE_ENUM_UNION : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint Level;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 2))
			{
				encoder.WritePointer(this.Level2);
			}
			else
			{
				if ((((int)(this.Level)) == 3))
				{
					encoder.WritePointer(this.Level3);
				}
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 2))
			{
				this.Level2 = decoder.ReadPointer<FILE_INFO_2_CONTAINER>();
			}
			else
			{
				if ((((int)(this.Level)) == 3))
				{
					this.Level3 = decoder.ReadPointer<FILE_INFO_3_CONTAINER>();
				}
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.Level)) == 2))
			{
				if ((null != this.Level2))
				{
					encoder.WriteFixedStruct(this.Level2.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(this.Level2.value);
				}
			}
			else
			{
				if ((((int)(this.Level)) == 3))
				{
					if ((null != this.Level3))
					{
						encoder.WriteFixedStruct(this.Level3.value, Titanis.DceRpc.NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level3.value);
					}
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.Level)) == 2))
			{
				if ((null != this.Level2))
				{
					this.Level2.value = decoder.ReadFixedStruct<FILE_INFO_2_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<FILE_INFO_2_CONTAINER>(ref this.Level2.value);
				}
			}
			else
			{
				if ((((int)(this.Level)) == 3))
				{
					if ((null != this.Level3))
					{
						this.Level3.value = decoder.ReadFixedStruct<FILE_INFO_3_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<FILE_INFO_3_CONTAINER>(ref this.Level3.value);
					}
				}
			}
		}
		public RpcPointer<FILE_INFO_2_CONTAINER> Level2;
		public RpcPointer<FILE_INFO_3_CONTAINER> Level3;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct FILE_ENUM_STRUCT : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.FileInfo);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.FileInfo = decoder.ReadUnion<FILE_ENUM_UNION>();
		}
		public uint Level;
		public FILE_ENUM_UNION FileInfo;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.FileInfo);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FILE_ENUM_UNION>(ref this.FileInfo);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct FILE_INFO : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint unionSwitch;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.unionSwitch);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.unionSwitch)) == 2))
			{
				encoder.WritePointer(this.FileInfo2);
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 3))
				{
					encoder.WritePointer(this.FileInfo3);
				}
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.unionSwitch = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.unionSwitch)) == 2))
			{
				this.FileInfo2 = decoder.ReadPointer<FILE_INFO_2>();
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 3))
				{
					this.FileInfo3 = decoder.ReadPointer<FILE_INFO_3>();
				}
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.unionSwitch)) == 2))
			{
				if ((null != this.FileInfo2))
				{
					encoder.WriteFixedStruct(this.FileInfo2.value, Titanis.DceRpc.NdrAlignment._4Byte);
					encoder.WriteStructDeferral(this.FileInfo2.value);
				}
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 3))
				{
					if ((null != this.FileInfo3))
					{
						encoder.WriteFixedStruct(this.FileInfo3.value, Titanis.DceRpc.NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.FileInfo3.value);
					}
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.unionSwitch)) == 2))
			{
				if ((null != this.FileInfo2))
				{
					this.FileInfo2.value = decoder.ReadFixedStruct<FILE_INFO_2>(Titanis.DceRpc.NdrAlignment._4Byte);
					decoder.ReadStructDeferral<FILE_INFO_2>(ref this.FileInfo2.value);
				}
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 3))
				{
					if ((null != this.FileInfo3))
					{
						this.FileInfo3.value = decoder.ReadFixedStruct<FILE_INFO_3>(Titanis.DceRpc.NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<FILE_INFO_3>(ref this.FileInfo3.value);
					}
				}
			}
		}
		public RpcPointer<FILE_INFO_2> FileInfo2;
		public RpcPointer<FILE_INFO_3> FileInfo3;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_INFO_0 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.sesi0_cname);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sesi0_cname = decoder.ReadPointer<string>();
		}
		public RpcPointer<string> sesi0_cname;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sesi0_cname))
			{
				encoder.WriteWideCharString(this.sesi0_cname.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sesi0_cname))
			{
				this.sesi0_cname.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_INFO_0_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SESSION_INFO_0[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SESSION_INFO_0[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SESSION_INFO_0>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SESSION_INFO_0>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SESSION_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_INFO_1 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.sesi1_cname);
			encoder.WritePointer(this.sesi1_username);
			encoder.WriteValue(this.sesi1_num_opens);
			encoder.WriteValue(this.sesi1_time);
			encoder.WriteValue(this.sesi1_idle_time);
			encoder.WriteValue(this.sesi1_user_flags);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sesi1_cname = decoder.ReadPointer<string>();
			this.sesi1_username = decoder.ReadPointer<string>();
			this.sesi1_num_opens = decoder.ReadUInt32();
			this.sesi1_time = decoder.ReadUInt32();
			this.sesi1_idle_time = decoder.ReadUInt32();
			this.sesi1_user_flags = decoder.ReadUInt32();
		}
		public RpcPointer<string> sesi1_cname;
		public RpcPointer<string> sesi1_username;
		public uint sesi1_num_opens;
		public uint sesi1_time;
		public uint sesi1_idle_time;
		public uint sesi1_user_flags;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sesi1_cname))
			{
				encoder.WriteWideCharString(this.sesi1_cname.value);
			}
			if ((null != this.sesi1_username))
			{
				encoder.WriteWideCharString(this.sesi1_username.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sesi1_cname))
			{
				this.sesi1_cname.value = decoder.ReadWideCharString();
			}
			if ((null != this.sesi1_username))
			{
				this.sesi1_username.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_INFO_1_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SESSION_INFO_1[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SESSION_INFO_1[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SESSION_INFO_1>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_1 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SESSION_INFO_1>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_1 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SESSION_INFO_1>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_INFO_2 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.sesi2_cname);
			encoder.WritePointer(this.sesi2_username);
			encoder.WriteValue(this.sesi2_num_opens);
			encoder.WriteValue(this.sesi2_time);
			encoder.WriteValue(this.sesi2_idle_time);
			encoder.WriteValue(this.sesi2_user_flags);
			encoder.WritePointer(this.sesi2_cltype_name);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sesi2_cname = decoder.ReadPointer<string>();
			this.sesi2_username = decoder.ReadPointer<string>();
			this.sesi2_num_opens = decoder.ReadUInt32();
			this.sesi2_time = decoder.ReadUInt32();
			this.sesi2_idle_time = decoder.ReadUInt32();
			this.sesi2_user_flags = decoder.ReadUInt32();
			this.sesi2_cltype_name = decoder.ReadPointer<string>();
		}
		public RpcPointer<string> sesi2_cname;
		public RpcPointer<string> sesi2_username;
		public uint sesi2_num_opens;
		public uint sesi2_time;
		public uint sesi2_idle_time;
		public uint sesi2_user_flags;
		public RpcPointer<string> sesi2_cltype_name;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sesi2_cname))
			{
				encoder.WriteWideCharString(this.sesi2_cname.value);
			}
			if ((null != this.sesi2_username))
			{
				encoder.WriteWideCharString(this.sesi2_username.value);
			}
			if ((null != this.sesi2_cltype_name))
			{
				encoder.WriteWideCharString(this.sesi2_cltype_name.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sesi2_cname))
			{
				this.sesi2_cname.value = decoder.ReadWideCharString();
			}
			if ((null != this.sesi2_username))
			{
				this.sesi2_username.value = decoder.ReadWideCharString();
			}
			if ((null != this.sesi2_cltype_name))
			{
				this.sesi2_cltype_name.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_INFO_2_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SESSION_INFO_2[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SESSION_INFO_2[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SESSION_INFO_2>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_2 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SESSION_INFO_2>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_2 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SESSION_INFO_2>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_INFO_10 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.sesi10_cname);
			encoder.WritePointer(this.sesi10_username);
			encoder.WriteValue(this.sesi10_time);
			encoder.WriteValue(this.sesi10_idle_time);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sesi10_cname = decoder.ReadPointer<string>();
			this.sesi10_username = decoder.ReadPointer<string>();
			this.sesi10_time = decoder.ReadUInt32();
			this.sesi10_idle_time = decoder.ReadUInt32();
		}
		public RpcPointer<string> sesi10_cname;
		public RpcPointer<string> sesi10_username;
		public uint sesi10_time;
		public uint sesi10_idle_time;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sesi10_cname))
			{
				encoder.WriteWideCharString(this.sesi10_cname.value);
			}
			if ((null != this.sesi10_username))
			{
				encoder.WriteWideCharString(this.sesi10_username.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sesi10_cname))
			{
				this.sesi10_cname.value = decoder.ReadWideCharString();
			}
			if ((null != this.sesi10_username))
			{
				this.sesi10_username.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_INFO_10_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SESSION_INFO_10[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SESSION_INFO_10[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_10 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_10 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SESSION_INFO_10>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_10 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SESSION_INFO_10>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_10 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SESSION_INFO_10>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_INFO_502 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.sesi502_cname);
			encoder.WritePointer(this.sesi502_username);
			encoder.WriteValue(this.sesi502_num_opens);
			encoder.WriteValue(this.sesi502_time);
			encoder.WriteValue(this.sesi502_idle_time);
			encoder.WriteValue(this.sesi502_user_flags);
			encoder.WritePointer(this.sesi502_cltype_name);
			encoder.WritePointer(this.sesi502_transport);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sesi502_cname = decoder.ReadPointer<string>();
			this.sesi502_username = decoder.ReadPointer<string>();
			this.sesi502_num_opens = decoder.ReadUInt32();
			this.sesi502_time = decoder.ReadUInt32();
			this.sesi502_idle_time = decoder.ReadUInt32();
			this.sesi502_user_flags = decoder.ReadUInt32();
			this.sesi502_cltype_name = decoder.ReadPointer<string>();
			this.sesi502_transport = decoder.ReadPointer<string>();
		}
		public RpcPointer<string> sesi502_cname;
		public RpcPointer<string> sesi502_username;
		public uint sesi502_num_opens;
		public uint sesi502_time;
		public uint sesi502_idle_time;
		public uint sesi502_user_flags;
		public RpcPointer<string> sesi502_cltype_name;
		public RpcPointer<string> sesi502_transport;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sesi502_cname))
			{
				encoder.WriteWideCharString(this.sesi502_cname.value);
			}
			if ((null != this.sesi502_username))
			{
				encoder.WriteWideCharString(this.sesi502_username.value);
			}
			if ((null != this.sesi502_cltype_name))
			{
				encoder.WriteWideCharString(this.sesi502_cltype_name.value);
			}
			if ((null != this.sesi502_transport))
			{
				encoder.WriteWideCharString(this.sesi502_transport.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sesi502_cname))
			{
				this.sesi502_cname.value = decoder.ReadWideCharString();
			}
			if ((null != this.sesi502_username))
			{
				this.sesi502_username.value = decoder.ReadWideCharString();
			}
			if ((null != this.sesi502_cltype_name))
			{
				this.sesi502_cltype_name.value = decoder.ReadWideCharString();
			}
			if ((null != this.sesi502_transport))
			{
				this.sesi502_transport.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_INFO_502_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SESSION_INFO_502[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SESSION_INFO_502[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_502 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_502 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SESSION_INFO_502>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_502 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SESSION_INFO_502>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SESSION_INFO_502 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SESSION_INFO_502>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_ENUM_UNION : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint Level;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 0))
			{
				encoder.WritePointer(this.Level0);
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					encoder.WritePointer(this.Level1);
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						encoder.WritePointer(this.Level2);
					}
					else
					{
						if ((((int)(this.Level)) == 10))
						{
							encoder.WritePointer(this.Level10);
						}
						else
						{
							if ((((int)(this.Level)) == 502))
							{
								encoder.WritePointer(this.Level502);
							}
						}
					}
				}
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 0))
			{
				this.Level0 = decoder.ReadPointer<SESSION_INFO_0_CONTAINER>();
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					this.Level1 = decoder.ReadPointer<SESSION_INFO_1_CONTAINER>();
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						this.Level2 = decoder.ReadPointer<SESSION_INFO_2_CONTAINER>();
					}
					else
					{
						if ((((int)(this.Level)) == 10))
						{
							this.Level10 = decoder.ReadPointer<SESSION_INFO_10_CONTAINER>();
						}
						else
						{
							if ((((int)(this.Level)) == 502))
							{
								this.Level502 = decoder.ReadPointer<SESSION_INFO_502_CONTAINER>();
							}
						}
					}
				}
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.Level)) == 0))
			{
				if ((null != this.Level0))
				{
					encoder.WriteFixedStruct(this.Level0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(this.Level0.value);
				}
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					if ((null != this.Level1))
					{
						encoder.WriteFixedStruct(this.Level1.value, Titanis.DceRpc.NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level1.value);
					}
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						if ((null != this.Level2))
						{
							encoder.WriteFixedStruct(this.Level2.value, Titanis.DceRpc.NdrAlignment.NativePtr);
							encoder.WriteStructDeferral(this.Level2.value);
						}
					}
					else
					{
						if ((((int)(this.Level)) == 10))
						{
							if ((null != this.Level10))
							{
								encoder.WriteFixedStruct(this.Level10.value, Titanis.DceRpc.NdrAlignment.NativePtr);
								encoder.WriteStructDeferral(this.Level10.value);
							}
						}
						else
						{
							if ((((int)(this.Level)) == 502))
							{
								if ((null != this.Level502))
								{
									encoder.WriteFixedStruct(this.Level502.value, Titanis.DceRpc.NdrAlignment.NativePtr);
									encoder.WriteStructDeferral(this.Level502.value);
								}
							}
						}
					}
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.Level)) == 0))
			{
				if ((null != this.Level0))
				{
					this.Level0.value = decoder.ReadFixedStruct<SESSION_INFO_0_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<SESSION_INFO_0_CONTAINER>(ref this.Level0.value);
				}
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					if ((null != this.Level1))
					{
						this.Level1.value = decoder.ReadFixedStruct<SESSION_INFO_1_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SESSION_INFO_1_CONTAINER>(ref this.Level1.value);
					}
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						if ((null != this.Level2))
						{
							this.Level2.value = decoder.ReadFixedStruct<SESSION_INFO_2_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
							decoder.ReadStructDeferral<SESSION_INFO_2_CONTAINER>(ref this.Level2.value);
						}
					}
					else
					{
						if ((((int)(this.Level)) == 10))
						{
							if ((null != this.Level10))
							{
								this.Level10.value = decoder.ReadFixedStruct<SESSION_INFO_10_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
								decoder.ReadStructDeferral<SESSION_INFO_10_CONTAINER>(ref this.Level10.value);
							}
						}
						else
						{
							if ((((int)(this.Level)) == 502))
							{
								if ((null != this.Level502))
								{
									this.Level502.value = decoder.ReadFixedStruct<SESSION_INFO_502_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
									decoder.ReadStructDeferral<SESSION_INFO_502_CONTAINER>(ref this.Level502.value);
								}
							}
						}
					}
				}
			}
		}
		public RpcPointer<SESSION_INFO_0_CONTAINER> Level0;
		public RpcPointer<SESSION_INFO_1_CONTAINER> Level1;
		public RpcPointer<SESSION_INFO_2_CONTAINER> Level2;
		public RpcPointer<SESSION_INFO_10_CONTAINER> Level10;
		public RpcPointer<SESSION_INFO_502_CONTAINER> Level502;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SESSION_ENUM_STRUCT : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.SessionInfo);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.SessionInfo = decoder.ReadUnion<SESSION_ENUM_UNION>();
		}
		public uint Level;
		public SESSION_ENUM_UNION SessionInfo;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.SessionInfo);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SESSION_ENUM_UNION>(ref this.SessionInfo);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_502_I : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.shi502_netname);
			encoder.WriteValue(this.shi502_type);
			encoder.WritePointer(this.shi502_remark);
			encoder.WriteValue(this.shi502_permissions);
			encoder.WriteValue(this.shi502_max_uses);
			encoder.WriteValue(this.shi502_current_uses);
			encoder.WritePointer(this.shi502_path);
			encoder.WritePointer(this.shi502_passwd);
			encoder.WriteValue(this.shi502_reserved);
			encoder.WritePointer(this.shi502_security_descriptor);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.shi502_netname = decoder.ReadPointer<string>();
			this.shi502_type = decoder.ReadUInt32();
			this.shi502_remark = decoder.ReadPointer<string>();
			this.shi502_permissions = decoder.ReadUInt32();
			this.shi502_max_uses = decoder.ReadUInt32();
			this.shi502_current_uses = decoder.ReadUInt32();
			this.shi502_path = decoder.ReadPointer<string>();
			this.shi502_passwd = decoder.ReadPointer<string>();
			this.shi502_reserved = decoder.ReadUInt32();
			this.shi502_security_descriptor = decoder.ReadPointer<byte[]>();
		}
		public RpcPointer<string> shi502_netname;
		public uint shi502_type;
		public RpcPointer<string> shi502_remark;
		public uint shi502_permissions;
		public uint shi502_max_uses;
		public uint shi502_current_uses;
		public RpcPointer<string> shi502_path;
		public RpcPointer<string> shi502_passwd;
		public uint shi502_reserved;
		public RpcPointer<byte[]> shi502_security_descriptor;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.shi502_netname))
			{
				encoder.WriteWideCharString(this.shi502_netname.value);
			}
			if ((null != this.shi502_remark))
			{
				encoder.WriteWideCharString(this.shi502_remark.value);
			}
			if ((null != this.shi502_path))
			{
				encoder.WriteWideCharString(this.shi502_path.value);
			}
			if ((null != this.shi502_passwd))
			{
				encoder.WriteWideCharString(this.shi502_passwd.value);
			}
			if ((null != this.shi502_security_descriptor))
			{
				encoder.WriteArrayHeader(this.shi502_security_descriptor.value);
				for (int i = 0; (i < this.shi502_security_descriptor.value.Length); i++
				)
				{
					byte elem_0 = this.shi502_security_descriptor.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.shi502_netname))
			{
				this.shi502_netname.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi502_remark))
			{
				this.shi502_remark.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi502_path))
			{
				this.shi502_path.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi502_passwd))
			{
				this.shi502_passwd.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi502_security_descriptor))
			{
				this.shi502_security_descriptor.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; (i < this.shi502_security_descriptor.value.Length); i++
				)
				{
					byte elem_0 = this.shi502_security_descriptor.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.shi502_security_descriptor.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_503_I : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.shi503_netname);
			encoder.WriteValue(this.shi503_type);
			encoder.WritePointer(this.shi503_remark);
			encoder.WriteValue(this.shi503_permissions);
			encoder.WriteValue(this.shi503_max_uses);
			encoder.WriteValue(this.shi503_current_uses);
			encoder.WritePointer(this.shi503_path);
			encoder.WritePointer(this.shi503_passwd);
			encoder.WritePointer(this.shi503_servername);
			encoder.WriteValue(this.shi503_reserved);
			encoder.WritePointer(this.shi503_security_descriptor);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.shi503_netname = decoder.ReadPointer<string>();
			this.shi503_type = decoder.ReadUInt32();
			this.shi503_remark = decoder.ReadPointer<string>();
			this.shi503_permissions = decoder.ReadUInt32();
			this.shi503_max_uses = decoder.ReadUInt32();
			this.shi503_current_uses = decoder.ReadUInt32();
			this.shi503_path = decoder.ReadPointer<string>();
			this.shi503_passwd = decoder.ReadPointer<string>();
			this.shi503_servername = decoder.ReadPointer<string>();
			this.shi503_reserved = decoder.ReadUInt32();
			this.shi503_security_descriptor = decoder.ReadPointer<byte[]>();
		}
		public RpcPointer<string> shi503_netname;
		public uint shi503_type;
		public RpcPointer<string> shi503_remark;
		public uint shi503_permissions;
		public uint shi503_max_uses;
		public uint shi503_current_uses;
		public RpcPointer<string> shi503_path;
		public RpcPointer<string> shi503_passwd;
		public RpcPointer<string> shi503_servername;
		public uint shi503_reserved;
		public RpcPointer<byte[]> shi503_security_descriptor;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.shi503_netname))
			{
				encoder.WriteWideCharString(this.shi503_netname.value);
			}
			if ((null != this.shi503_remark))
			{
				encoder.WriteWideCharString(this.shi503_remark.value);
			}
			if ((null != this.shi503_path))
			{
				encoder.WriteWideCharString(this.shi503_path.value);
			}
			if ((null != this.shi503_passwd))
			{
				encoder.WriteWideCharString(this.shi503_passwd.value);
			}
			if ((null != this.shi503_servername))
			{
				encoder.WriteWideCharString(this.shi503_servername.value);
			}
			if ((null != this.shi503_security_descriptor))
			{
				encoder.WriteArrayHeader(this.shi503_security_descriptor.value);
				for (int i = 0; (i < this.shi503_security_descriptor.value.Length); i++
				)
				{
					byte elem_0 = this.shi503_security_descriptor.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.shi503_netname))
			{
				this.shi503_netname.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi503_remark))
			{
				this.shi503_remark.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi503_path))
			{
				this.shi503_path.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi503_passwd))
			{
				this.shi503_passwd.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi503_servername))
			{
				this.shi503_servername.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi503_security_descriptor))
			{
				this.shi503_security_descriptor.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; (i < this.shi503_security_descriptor.value.Length); i++
				)
				{
					byte elem_0 = this.shi503_security_descriptor.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.shi503_security_descriptor.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_503_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SHARE_INFO_503_I[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_503_I[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_503_I elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_503_I elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_503_I>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_503_I elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_503_I>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_503_I elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_503_I>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_1501_I : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.shi1501_reserved);
			encoder.WritePointer(this.shi1501_security_descriptor);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.shi1501_reserved = decoder.ReadUInt32();
			this.shi1501_security_descriptor = decoder.ReadPointer<byte[]>();
		}
		public uint shi1501_reserved;
		public RpcPointer<byte[]> shi1501_security_descriptor;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.shi1501_security_descriptor))
			{
				encoder.WriteArrayHeader(this.shi1501_security_descriptor.value);
				for (int i = 0; (i < this.shi1501_security_descriptor.value.Length); i++
				)
				{
					byte elem_0 = this.shi1501_security_descriptor.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.shi1501_security_descriptor))
			{
				this.shi1501_security_descriptor.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; (i < this.shi1501_security_descriptor.value.Length); i++
				)
				{
					byte elem_0 = this.shi1501_security_descriptor.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.shi1501_security_descriptor.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_0 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.shi0_netname);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.shi0_netname = decoder.ReadPointer<string>();
		}
		public RpcPointer<string> shi0_netname;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.shi0_netname))
			{
				encoder.WriteWideCharString(this.shi0_netname.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.shi0_netname))
			{
				this.shi0_netname.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_0_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SHARE_INFO_0[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_0[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_0>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_0>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_1 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.shi1_netname);
			encoder.WriteValue(this.shi1_type);
			encoder.WritePointer(this.shi1_remark);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.shi1_netname = decoder.ReadPointer<string>();
			this.shi1_type = decoder.ReadUInt32();
			this.shi1_remark = decoder.ReadPointer<string>();
		}
		public RpcPointer<string> shi1_netname;
		public uint shi1_type;
		public RpcPointer<string> shi1_remark;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.shi1_netname))
			{
				encoder.WriteWideCharString(this.shi1_netname.value);
			}
			if ((null != this.shi1_remark))
			{
				encoder.WriteWideCharString(this.shi1_remark.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.shi1_netname))
			{
				this.shi1_netname.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi1_remark))
			{
				this.shi1_remark.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_1_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SHARE_INFO_1[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_1[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_1>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_1 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_1>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_1 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_1>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_2 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.shi2_netname);
			encoder.WriteValue(this.shi2_type);
			encoder.WritePointer(this.shi2_remark);
			encoder.WriteValue(this.shi2_permissions);
			encoder.WriteValue(this.shi2_max_uses);
			encoder.WriteValue(this.shi2_current_uses);
			encoder.WritePointer(this.shi2_path);
			encoder.WritePointer(this.shi2_passwd);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.shi2_netname = decoder.ReadPointer<string>();
			this.shi2_type = decoder.ReadUInt32();
			this.shi2_remark = decoder.ReadPointer<string>();
			this.shi2_permissions = decoder.ReadUInt32();
			this.shi2_max_uses = decoder.ReadUInt32();
			this.shi2_current_uses = decoder.ReadUInt32();
			this.shi2_path = decoder.ReadPointer<string>();
			this.shi2_passwd = decoder.ReadPointer<string>();
		}
		public RpcPointer<string> shi2_netname;
		public uint shi2_type;
		public RpcPointer<string> shi2_remark;
		public uint shi2_permissions;
		public uint shi2_max_uses;
		public uint shi2_current_uses;
		public RpcPointer<string> shi2_path;
		public RpcPointer<string> shi2_passwd;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.shi2_netname))
			{
				encoder.WriteWideCharString(this.shi2_netname.value);
			}
			if ((null != this.shi2_remark))
			{
				encoder.WriteWideCharString(this.shi2_remark.value);
			}
			if ((null != this.shi2_path))
			{
				encoder.WriteWideCharString(this.shi2_path.value);
			}
			if ((null != this.shi2_passwd))
			{
				encoder.WriteWideCharString(this.shi2_passwd.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.shi2_netname))
			{
				this.shi2_netname.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi2_remark))
			{
				this.shi2_remark.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi2_path))
			{
				this.shi2_path.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi2_passwd))
			{
				this.shi2_passwd.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_2_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SHARE_INFO_2[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_2[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_2>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_2 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_2>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_2 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_2>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_501 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.shi501_netname);
			encoder.WriteValue(this.shi501_type);
			encoder.WritePointer(this.shi501_remark);
			encoder.WriteValue(this.shi501_flags);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.shi501_netname = decoder.ReadPointer<string>();
			this.shi501_type = decoder.ReadUInt32();
			this.shi501_remark = decoder.ReadPointer<string>();
			this.shi501_flags = decoder.ReadUInt32();
		}
		public RpcPointer<string> shi501_netname;
		public uint shi501_type;
		public RpcPointer<string> shi501_remark;
		public uint shi501_flags;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.shi501_netname))
			{
				encoder.WriteWideCharString(this.shi501_netname.value);
			}
			if ((null != this.shi501_remark))
			{
				encoder.WriteWideCharString(this.shi501_remark.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.shi501_netname))
			{
				this.shi501_netname.value = decoder.ReadWideCharString();
			}
			if ((null != this.shi501_remark))
			{
				this.shi501_remark.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_501_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SHARE_INFO_501[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_501[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_501 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_501 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_501>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_501 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_501>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_501 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_501>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_502_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SHARE_INFO_502_I[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_502_I[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_502_I elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_502_I elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_502_I>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_502_I elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_502_I>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SHARE_INFO_502_I elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_502_I>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_ENUM_UNION : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint Level;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 0))
			{
				encoder.WritePointer(this.Level0);
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					encoder.WritePointer(this.Level1);
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						encoder.WritePointer(this.Level2);
					}
					else
					{
						if ((((int)(this.Level)) == 501))
						{
							encoder.WritePointer(this.Level501);
						}
						else
						{
							if ((((int)(this.Level)) == 502))
							{
								encoder.WritePointer(this.Level502);
							}
							else
							{
								if ((((int)(this.Level)) == 503))
								{
									encoder.WritePointer(this.Level503);
								}
							}
						}
					}
				}
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 0))
			{
				this.Level0 = decoder.ReadPointer<SHARE_INFO_0_CONTAINER>();
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					this.Level1 = decoder.ReadPointer<SHARE_INFO_1_CONTAINER>();
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						this.Level2 = decoder.ReadPointer<SHARE_INFO_2_CONTAINER>();
					}
					else
					{
						if ((((int)(this.Level)) == 501))
						{
							this.Level501 = decoder.ReadPointer<SHARE_INFO_501_CONTAINER>();
						}
						else
						{
							if ((((int)(this.Level)) == 502))
							{
								this.Level502 = decoder.ReadPointer<SHARE_INFO_502_CONTAINER>();
							}
							else
							{
								if ((((int)(this.Level)) == 503))
								{
									this.Level503 = decoder.ReadPointer<SHARE_INFO_503_CONTAINER>();
								}
							}
						}
					}
				}
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.Level)) == 0))
			{
				if ((null != this.Level0))
				{
					encoder.WriteFixedStruct(this.Level0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(this.Level0.value);
				}
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					if ((null != this.Level1))
					{
						encoder.WriteFixedStruct(this.Level1.value, Titanis.DceRpc.NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level1.value);
					}
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						if ((null != this.Level2))
						{
							encoder.WriteFixedStruct(this.Level2.value, Titanis.DceRpc.NdrAlignment.NativePtr);
							encoder.WriteStructDeferral(this.Level2.value);
						}
					}
					else
					{
						if ((((int)(this.Level)) == 501))
						{
							if ((null != this.Level501))
							{
								encoder.WriteFixedStruct(this.Level501.value, Titanis.DceRpc.NdrAlignment.NativePtr);
								encoder.WriteStructDeferral(this.Level501.value);
							}
						}
						else
						{
							if ((((int)(this.Level)) == 502))
							{
								if ((null != this.Level502))
								{
									encoder.WriteFixedStruct(this.Level502.value, Titanis.DceRpc.NdrAlignment.NativePtr);
									encoder.WriteStructDeferral(this.Level502.value);
								}
							}
							else
							{
								if ((((int)(this.Level)) == 503))
								{
									if ((null != this.Level503))
									{
										encoder.WriteFixedStruct(this.Level503.value, Titanis.DceRpc.NdrAlignment.NativePtr);
										encoder.WriteStructDeferral(this.Level503.value);
									}
								}
							}
						}
					}
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.Level)) == 0))
			{
				if ((null != this.Level0))
				{
					this.Level0.value = decoder.ReadFixedStruct<SHARE_INFO_0_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<SHARE_INFO_0_CONTAINER>(ref this.Level0.value);
				}
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					if ((null != this.Level1))
					{
						this.Level1.value = decoder.ReadFixedStruct<SHARE_INFO_1_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_1_CONTAINER>(ref this.Level1.value);
					}
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						if ((null != this.Level2))
						{
							this.Level2.value = decoder.ReadFixedStruct<SHARE_INFO_2_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
							decoder.ReadStructDeferral<SHARE_INFO_2_CONTAINER>(ref this.Level2.value);
						}
					}
					else
					{
						if ((((int)(this.Level)) == 501))
						{
							if ((null != this.Level501))
							{
								this.Level501.value = decoder.ReadFixedStruct<SHARE_INFO_501_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
								decoder.ReadStructDeferral<SHARE_INFO_501_CONTAINER>(ref this.Level501.value);
							}
						}
						else
						{
							if ((((int)(this.Level)) == 502))
							{
								if ((null != this.Level502))
								{
									this.Level502.value = decoder.ReadFixedStruct<SHARE_INFO_502_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
									decoder.ReadStructDeferral<SHARE_INFO_502_CONTAINER>(ref this.Level502.value);
								}
							}
							else
							{
								if ((((int)(this.Level)) == 503))
								{
									if ((null != this.Level503))
									{
										this.Level503.value = decoder.ReadFixedStruct<SHARE_INFO_503_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
										decoder.ReadStructDeferral<SHARE_INFO_503_CONTAINER>(ref this.Level503.value);
									}
								}
							}
						}
					}
				}
			}
		}
		public RpcPointer<SHARE_INFO_0_CONTAINER> Level0;
		public RpcPointer<SHARE_INFO_1_CONTAINER> Level1;
		public RpcPointer<SHARE_INFO_2_CONTAINER> Level2;
		public RpcPointer<SHARE_INFO_501_CONTAINER> Level501;
		public RpcPointer<SHARE_INFO_502_CONTAINER> Level502;
		public RpcPointer<SHARE_INFO_503_CONTAINER> Level503;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_ENUM_STRUCT : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.ShareInfo);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.ShareInfo = decoder.ReadUnion<SHARE_ENUM_UNION>();
		}
		public uint Level;
		public SHARE_ENUM_UNION ShareInfo;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ShareInfo);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SHARE_ENUM_UNION>(ref this.ShareInfo);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_1004 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.shi1004_remark);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.shi1004_remark = decoder.ReadPointer<string>();
		}
		public RpcPointer<string> shi1004_remark;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.shi1004_remark))
			{
				encoder.WriteWideCharString(this.shi1004_remark.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.shi1004_remark))
			{
				this.shi1004_remark.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_1006 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.shi1006_max_uses);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.shi1006_max_uses = decoder.ReadUInt32();
		}
		public uint shi1006_max_uses;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO_1005 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.shi1005_flags);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.shi1005_flags = decoder.ReadUInt32();
		}
		public uint shi1005_flags;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SHARE_INFO : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint unionSwitch;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.unionSwitch);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.unionSwitch)) == 0))
			{
				encoder.WritePointer(this.ShareInfo0);
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 1))
				{
					encoder.WritePointer(this.ShareInfo1);
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 2))
					{
						encoder.WritePointer(this.ShareInfo2);
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 502))
						{
							encoder.WritePointer(this.ShareInfo502);
						}
						else
						{
							if ((((int)(this.unionSwitch)) == 1004))
							{
								encoder.WritePointer(this.ShareInfo1004);
							}
							else
							{
								if ((((int)(this.unionSwitch)) == 1006))
								{
									encoder.WritePointer(this.ShareInfo1006);
								}
								else
								{
									if ((((int)(this.unionSwitch)) == 1501))
									{
										encoder.WritePointer(this.ShareInfo1501);
									}
									else
									{
										if ((((int)(this.unionSwitch)) == 1005))
										{
											encoder.WritePointer(this.ShareInfo1005);
										}
										else
										{
											if ((((int)(this.unionSwitch)) == 501))
											{
												encoder.WritePointer(this.ShareInfo501);
											}
											else
											{
												if ((((int)(this.unionSwitch)) == 503))
												{
													encoder.WritePointer(this.ShareInfo503);
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
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.unionSwitch = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.unionSwitch)) == 0))
			{
				this.ShareInfo0 = decoder.ReadPointer<SHARE_INFO_0>();
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 1))
				{
					this.ShareInfo1 = decoder.ReadPointer<SHARE_INFO_1>();
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 2))
					{
						this.ShareInfo2 = decoder.ReadPointer<SHARE_INFO_2>();
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 502))
						{
							this.ShareInfo502 = decoder.ReadPointer<SHARE_INFO_502_I>();
						}
						else
						{
							if ((((int)(this.unionSwitch)) == 1004))
							{
								this.ShareInfo1004 = decoder.ReadPointer<SHARE_INFO_1004>();
							}
							else
							{
								if ((((int)(this.unionSwitch)) == 1006))
								{
									this.ShareInfo1006 = decoder.ReadPointer<SHARE_INFO_1006>();
								}
								else
								{
									if ((((int)(this.unionSwitch)) == 1501))
									{
										this.ShareInfo1501 = decoder.ReadPointer<SHARE_INFO_1501_I>();
									}
									else
									{
										if ((((int)(this.unionSwitch)) == 1005))
										{
											this.ShareInfo1005 = decoder.ReadPointer<SHARE_INFO_1005>();
										}
										else
										{
											if ((((int)(this.unionSwitch)) == 501))
											{
												this.ShareInfo501 = decoder.ReadPointer<SHARE_INFO_501>();
											}
											else
											{
												if ((((int)(this.unionSwitch)) == 503))
												{
													this.ShareInfo503 = decoder.ReadPointer<SHARE_INFO_503_I>();
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
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.unionSwitch)) == 0))
			{
				if ((null != this.ShareInfo0))
				{
					encoder.WriteFixedStruct(this.ShareInfo0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(this.ShareInfo0.value);
				}
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 1))
				{
					if ((null != this.ShareInfo1))
					{
						encoder.WriteFixedStruct(this.ShareInfo1.value, Titanis.DceRpc.NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ShareInfo1.value);
					}
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 2))
					{
						if ((null != this.ShareInfo2))
						{
							encoder.WriteFixedStruct(this.ShareInfo2.value, Titanis.DceRpc.NdrAlignment.NativePtr);
							encoder.WriteStructDeferral(this.ShareInfo2.value);
						}
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 502))
						{
							if ((null != this.ShareInfo502))
							{
								encoder.WriteFixedStruct(this.ShareInfo502.value, Titanis.DceRpc.NdrAlignment.NativePtr);
								encoder.WriteStructDeferral(this.ShareInfo502.value);
							}
						}
						else
						{
							if ((((int)(this.unionSwitch)) == 1004))
							{
								if ((null != this.ShareInfo1004))
								{
									encoder.WriteFixedStruct(this.ShareInfo1004.value, Titanis.DceRpc.NdrAlignment.NativePtr);
									encoder.WriteStructDeferral(this.ShareInfo1004.value);
								}
							}
							else
							{
								if ((((int)(this.unionSwitch)) == 1006))
								{
									if ((null != this.ShareInfo1006))
									{
										encoder.WriteFixedStruct(this.ShareInfo1006.value, Titanis.DceRpc.NdrAlignment._4Byte);
										encoder.WriteStructDeferral(this.ShareInfo1006.value);
									}
								}
								else
								{
									if ((((int)(this.unionSwitch)) == 1501))
									{
										if ((null != this.ShareInfo1501))
										{
											encoder.WriteFixedStruct(this.ShareInfo1501.value, Titanis.DceRpc.NdrAlignment.NativePtr);
											encoder.WriteStructDeferral(this.ShareInfo1501.value);
										}
									}
									else
									{
										if ((((int)(this.unionSwitch)) == 1005))
										{
											if ((null != this.ShareInfo1005))
											{
												encoder.WriteFixedStruct(this.ShareInfo1005.value, Titanis.DceRpc.NdrAlignment._4Byte);
												encoder.WriteStructDeferral(this.ShareInfo1005.value);
											}
										}
										else
										{
											if ((((int)(this.unionSwitch)) == 501))
											{
												if ((null != this.ShareInfo501))
												{
													encoder.WriteFixedStruct(this.ShareInfo501.value, Titanis.DceRpc.NdrAlignment.NativePtr);
													encoder.WriteStructDeferral(this.ShareInfo501.value);
												}
											}
											else
											{
												if ((((int)(this.unionSwitch)) == 503))
												{
													if ((null != this.ShareInfo503))
													{
														encoder.WriteFixedStruct(this.ShareInfo503.value, Titanis.DceRpc.NdrAlignment.NativePtr);
														encoder.WriteStructDeferral(this.ShareInfo503.value);
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
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.unionSwitch)) == 0))
			{
				if ((null != this.ShareInfo0))
				{
					this.ShareInfo0.value = decoder.ReadFixedStruct<SHARE_INFO_0>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<SHARE_INFO_0>(ref this.ShareInfo0.value);
				}
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 1))
				{
					if ((null != this.ShareInfo1))
					{
						this.ShareInfo1.value = decoder.ReadFixedStruct<SHARE_INFO_1>(Titanis.DceRpc.NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_1>(ref this.ShareInfo1.value);
					}
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 2))
					{
						if ((null != this.ShareInfo2))
						{
							this.ShareInfo2.value = decoder.ReadFixedStruct<SHARE_INFO_2>(Titanis.DceRpc.NdrAlignment.NativePtr);
							decoder.ReadStructDeferral<SHARE_INFO_2>(ref this.ShareInfo2.value);
						}
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 502))
						{
							if ((null != this.ShareInfo502))
							{
								this.ShareInfo502.value = decoder.ReadFixedStruct<SHARE_INFO_502_I>(Titanis.DceRpc.NdrAlignment.NativePtr);
								decoder.ReadStructDeferral<SHARE_INFO_502_I>(ref this.ShareInfo502.value);
							}
						}
						else
						{
							if ((((int)(this.unionSwitch)) == 1004))
							{
								if ((null != this.ShareInfo1004))
								{
									this.ShareInfo1004.value = decoder.ReadFixedStruct<SHARE_INFO_1004>(Titanis.DceRpc.NdrAlignment.NativePtr);
									decoder.ReadStructDeferral<SHARE_INFO_1004>(ref this.ShareInfo1004.value);
								}
							}
							else
							{
								if ((((int)(this.unionSwitch)) == 1006))
								{
									if ((null != this.ShareInfo1006))
									{
										this.ShareInfo1006.value = decoder.ReadFixedStruct<SHARE_INFO_1006>(Titanis.DceRpc.NdrAlignment._4Byte);
										decoder.ReadStructDeferral<SHARE_INFO_1006>(ref this.ShareInfo1006.value);
									}
								}
								else
								{
									if ((((int)(this.unionSwitch)) == 1501))
									{
										if ((null != this.ShareInfo1501))
										{
											this.ShareInfo1501.value = decoder.ReadFixedStruct<SHARE_INFO_1501_I>(Titanis.DceRpc.NdrAlignment.NativePtr);
											decoder.ReadStructDeferral<SHARE_INFO_1501_I>(ref this.ShareInfo1501.value);
										}
									}
									else
									{
										if ((((int)(this.unionSwitch)) == 1005))
										{
											if ((null != this.ShareInfo1005))
											{
												this.ShareInfo1005.value = decoder.ReadFixedStruct<SHARE_INFO_1005>(Titanis.DceRpc.NdrAlignment._4Byte);
												decoder.ReadStructDeferral<SHARE_INFO_1005>(ref this.ShareInfo1005.value);
											}
										}
										else
										{
											if ((((int)(this.unionSwitch)) == 501))
											{
												if ((null != this.ShareInfo501))
												{
													this.ShareInfo501.value = decoder.ReadFixedStruct<SHARE_INFO_501>(Titanis.DceRpc.NdrAlignment.NativePtr);
													decoder.ReadStructDeferral<SHARE_INFO_501>(ref this.ShareInfo501.value);
												}
											}
											else
											{
												if ((((int)(this.unionSwitch)) == 503))
												{
													if ((null != this.ShareInfo503))
													{
														this.ShareInfo503.value = decoder.ReadFixedStruct<SHARE_INFO_503_I>(Titanis.DceRpc.NdrAlignment.NativePtr);
														decoder.ReadStructDeferral<SHARE_INFO_503_I>(ref this.ShareInfo503.value);
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
		public RpcPointer<SHARE_INFO_0> ShareInfo0;
		public RpcPointer<SHARE_INFO_1> ShareInfo1;
		public RpcPointer<SHARE_INFO_2> ShareInfo2;
		public RpcPointer<SHARE_INFO_502_I> ShareInfo502;
		public RpcPointer<SHARE_INFO_1004> ShareInfo1004;
		public RpcPointer<SHARE_INFO_1006> ShareInfo1006;
		public RpcPointer<SHARE_INFO_1501_I> ShareInfo1501;
		public RpcPointer<SHARE_INFO_1005> ShareInfo1005;
		public RpcPointer<SHARE_INFO_501> ShareInfo501;
		public RpcPointer<SHARE_INFO_503_I> ShareInfo503;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_102 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv102_platform_id);
			encoder.WritePointer(this.sv102_name);
			encoder.WriteValue(this.sv102_version_major);
			encoder.WriteValue(this.sv102_version_minor);
			encoder.WriteValue(this.sv102_type);
			encoder.WritePointer(this.sv102_comment);
			encoder.WriteValue(this.sv102_users);
			encoder.WriteValue(this.sv102_disc);
			encoder.WriteValue(this.sv102_hidden);
			encoder.WriteValue(this.sv102_announce);
			encoder.WriteValue(this.sv102_anndelta);
			encoder.WriteValue(this.sv102_licenses);
			encoder.WritePointer(this.sv102_userpath);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv102_platform_id = decoder.ReadUInt32();
			this.sv102_name = decoder.ReadPointer<string>();
			this.sv102_version_major = decoder.ReadUInt32();
			this.sv102_version_minor = decoder.ReadUInt32();
			this.sv102_type = decoder.ReadUInt32();
			this.sv102_comment = decoder.ReadPointer<string>();
			this.sv102_users = decoder.ReadUInt32();
			this.sv102_disc = decoder.ReadInt32();
			this.sv102_hidden = decoder.ReadInt32();
			this.sv102_announce = decoder.ReadUInt32();
			this.sv102_anndelta = decoder.ReadUInt32();
			this.sv102_licenses = decoder.ReadUInt32();
			this.sv102_userpath = decoder.ReadPointer<string>();
		}
		public uint sv102_platform_id;
		public RpcPointer<string> sv102_name;
		public uint sv102_version_major;
		public uint sv102_version_minor;
		public uint sv102_type;
		public RpcPointer<string> sv102_comment;
		public uint sv102_users;
		public int sv102_disc;
		public int sv102_hidden;
		public uint sv102_announce;
		public uint sv102_anndelta;
		public uint sv102_licenses;
		public RpcPointer<string> sv102_userpath;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sv102_name))
			{
				encoder.WriteWideCharString(this.sv102_name.value);
			}
			if ((null != this.sv102_comment))
			{
				encoder.WriteWideCharString(this.sv102_comment.value);
			}
			if ((null != this.sv102_userpath))
			{
				encoder.WriteWideCharString(this.sv102_userpath.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sv102_name))
			{
				this.sv102_name.value = decoder.ReadWideCharString();
			}
			if ((null != this.sv102_comment))
			{
				this.sv102_comment.value = decoder.ReadWideCharString();
			}
			if ((null != this.sv102_userpath))
			{
				this.sv102_userpath.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_103 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv103_platform_id);
			encoder.WritePointer(this.sv103_name);
			encoder.WriteValue(this.sv103_version_major);
			encoder.WriteValue(this.sv103_version_minor);
			encoder.WriteValue(this.sv103_type);
			encoder.WritePointer(this.sv103_comment);
			encoder.WriteValue(this.sv103_users);
			encoder.WriteValue(this.sv103_disc);
			encoder.WriteValue(this.sv103_hidden);
			encoder.WriteValue(this.sv103_announce);
			encoder.WriteValue(this.sv103_anndelta);
			encoder.WriteValue(this.sv103_licenses);
			encoder.WritePointer(this.sv103_userpath);
			encoder.WriteValue(this.sv103_capabilities);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv103_platform_id = decoder.ReadUInt32();
			this.sv103_name = decoder.ReadPointer<string>();
			this.sv103_version_major = decoder.ReadUInt32();
			this.sv103_version_minor = decoder.ReadUInt32();
			this.sv103_type = decoder.ReadUInt32();
			this.sv103_comment = decoder.ReadPointer<string>();
			this.sv103_users = decoder.ReadUInt32();
			this.sv103_disc = decoder.ReadInt32();
			this.sv103_hidden = decoder.ReadInt32();
			this.sv103_announce = decoder.ReadUInt32();
			this.sv103_anndelta = decoder.ReadUInt32();
			this.sv103_licenses = decoder.ReadUInt32();
			this.sv103_userpath = decoder.ReadPointer<string>();
			this.sv103_capabilities = decoder.ReadUInt32();
		}
		public uint sv103_platform_id;
		public RpcPointer<string> sv103_name;
		public uint sv103_version_major;
		public uint sv103_version_minor;
		public uint sv103_type;
		public RpcPointer<string> sv103_comment;
		public uint sv103_users;
		public int sv103_disc;
		public int sv103_hidden;
		public uint sv103_announce;
		public uint sv103_anndelta;
		public uint sv103_licenses;
		public RpcPointer<string> sv103_userpath;
		public uint sv103_capabilities;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sv103_name))
			{
				encoder.WriteWideCharString(this.sv103_name.value);
			}
			if ((null != this.sv103_comment))
			{
				encoder.WriteWideCharString(this.sv103_comment.value);
			}
			if ((null != this.sv103_userpath))
			{
				encoder.WriteWideCharString(this.sv103_userpath.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sv103_name))
			{
				this.sv103_name.value = decoder.ReadWideCharString();
			}
			if ((null != this.sv103_comment))
			{
				this.sv103_comment.value = decoder.ReadWideCharString();
			}
			if ((null != this.sv103_userpath))
			{
				this.sv103_userpath.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_502 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv502_sessopens);
			encoder.WriteValue(this.sv502_sessvcs);
			encoder.WriteValue(this.sv502_opensearch);
			encoder.WriteValue(this.sv502_sizreqbuf);
			encoder.WriteValue(this.sv502_initworkitems);
			encoder.WriteValue(this.sv502_maxworkitems);
			encoder.WriteValue(this.sv502_rawworkitems);
			encoder.WriteValue(this.sv502_irpstacksize);
			encoder.WriteValue(this.sv502_maxrawbuflen);
			encoder.WriteValue(this.sv502_sessusers);
			encoder.WriteValue(this.sv502_sessconns);
			encoder.WriteValue(this.sv502_maxpagedmemoryusage);
			encoder.WriteValue(this.sv502_maxnonpagedmemoryusage);
			encoder.WriteValue(this.sv502_enablesoftcompat);
			encoder.WriteValue(this.sv502_enableforcedlogoff);
			encoder.WriteValue(this.sv502_timesource);
			encoder.WriteValue(this.sv502_acceptdownlevelapis);
			encoder.WriteValue(this.sv502_lmannounce);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv502_sessopens = decoder.ReadUInt32();
			this.sv502_sessvcs = decoder.ReadUInt32();
			this.sv502_opensearch = decoder.ReadUInt32();
			this.sv502_sizreqbuf = decoder.ReadUInt32();
			this.sv502_initworkitems = decoder.ReadUInt32();
			this.sv502_maxworkitems = decoder.ReadUInt32();
			this.sv502_rawworkitems = decoder.ReadUInt32();
			this.sv502_irpstacksize = decoder.ReadUInt32();
			this.sv502_maxrawbuflen = decoder.ReadUInt32();
			this.sv502_sessusers = decoder.ReadUInt32();
			this.sv502_sessconns = decoder.ReadUInt32();
			this.sv502_maxpagedmemoryusage = decoder.ReadUInt32();
			this.sv502_maxnonpagedmemoryusage = decoder.ReadUInt32();
			this.sv502_enablesoftcompat = decoder.ReadInt32();
			this.sv502_enableforcedlogoff = decoder.ReadInt32();
			this.sv502_timesource = decoder.ReadInt32();
			this.sv502_acceptdownlevelapis = decoder.ReadInt32();
			this.sv502_lmannounce = decoder.ReadInt32();
		}
		public uint sv502_sessopens;
		public uint sv502_sessvcs;
		public uint sv502_opensearch;
		public uint sv502_sizreqbuf;
		public uint sv502_initworkitems;
		public uint sv502_maxworkitems;
		public uint sv502_rawworkitems;
		public uint sv502_irpstacksize;
		public uint sv502_maxrawbuflen;
		public uint sv502_sessusers;
		public uint sv502_sessconns;
		public uint sv502_maxpagedmemoryusage;
		public uint sv502_maxnonpagedmemoryusage;
		public int sv502_enablesoftcompat;
		public int sv502_enableforcedlogoff;
		public int sv502_timesource;
		public int sv502_acceptdownlevelapis;
		public int sv502_lmannounce;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_503 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv503_sessopens);
			encoder.WriteValue(this.sv503_sessvcs);
			encoder.WriteValue(this.sv503_opensearch);
			encoder.WriteValue(this.sv503_sizreqbuf);
			encoder.WriteValue(this.sv503_initworkitems);
			encoder.WriteValue(this.sv503_maxworkitems);
			encoder.WriteValue(this.sv503_rawworkitems);
			encoder.WriteValue(this.sv503_irpstacksize);
			encoder.WriteValue(this.sv503_maxrawbuflen);
			encoder.WriteValue(this.sv503_sessusers);
			encoder.WriteValue(this.sv503_sessconns);
			encoder.WriteValue(this.sv503_maxpagedmemoryusage);
			encoder.WriteValue(this.sv503_maxnonpagedmemoryusage);
			encoder.WriteValue(this.sv503_enablesoftcompat);
			encoder.WriteValue(this.sv503_enableforcedlogoff);
			encoder.WriteValue(this.sv503_timesource);
			encoder.WriteValue(this.sv503_acceptdownlevelapis);
			encoder.WriteValue(this.sv503_lmannounce);
			encoder.WritePointer(this.sv503_domain);
			encoder.WriteValue(this.sv503_maxcopyreadlen);
			encoder.WriteValue(this.sv503_maxcopywritelen);
			encoder.WriteValue(this.sv503_minkeepsearch);
			encoder.WriteValue(this.sv503_maxkeepsearch);
			encoder.WriteValue(this.sv503_minkeepcomplsearch);
			encoder.WriteValue(this.sv503_maxkeepcomplsearch);
			encoder.WriteValue(this.sv503_threadcountadd);
			encoder.WriteValue(this.sv503_numblockthreads);
			encoder.WriteValue(this.sv503_scavtimeout);
			encoder.WriteValue(this.sv503_minrcvqueue);
			encoder.WriteValue(this.sv503_minfreeworkitems);
			encoder.WriteValue(this.sv503_xactmemsize);
			encoder.WriteValue(this.sv503_threadpriority);
			encoder.WriteValue(this.sv503_maxmpxct);
			encoder.WriteValue(this.sv503_oplockbreakwait);
			encoder.WriteValue(this.sv503_oplockbreakresponsewait);
			encoder.WriteValue(this.sv503_enableoplocks);
			encoder.WriteValue(this.sv503_enableoplockforceclose);
			encoder.WriteValue(this.sv503_enablefcbopens);
			encoder.WriteValue(this.sv503_enableraw);
			encoder.WriteValue(this.sv503_enablesharednetdrives);
			encoder.WriteValue(this.sv503_minfreeconnections);
			encoder.WriteValue(this.sv503_maxfreeconnections);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv503_sessopens = decoder.ReadUInt32();
			this.sv503_sessvcs = decoder.ReadUInt32();
			this.sv503_opensearch = decoder.ReadUInt32();
			this.sv503_sizreqbuf = decoder.ReadUInt32();
			this.sv503_initworkitems = decoder.ReadUInt32();
			this.sv503_maxworkitems = decoder.ReadUInt32();
			this.sv503_rawworkitems = decoder.ReadUInt32();
			this.sv503_irpstacksize = decoder.ReadUInt32();
			this.sv503_maxrawbuflen = decoder.ReadUInt32();
			this.sv503_sessusers = decoder.ReadUInt32();
			this.sv503_sessconns = decoder.ReadUInt32();
			this.sv503_maxpagedmemoryusage = decoder.ReadUInt32();
			this.sv503_maxnonpagedmemoryusage = decoder.ReadUInt32();
			this.sv503_enablesoftcompat = decoder.ReadInt32();
			this.sv503_enableforcedlogoff = decoder.ReadInt32();
			this.sv503_timesource = decoder.ReadInt32();
			this.sv503_acceptdownlevelapis = decoder.ReadInt32();
			this.sv503_lmannounce = decoder.ReadInt32();
			this.sv503_domain = decoder.ReadPointer<string>();
			this.sv503_maxcopyreadlen = decoder.ReadUInt32();
			this.sv503_maxcopywritelen = decoder.ReadUInt32();
			this.sv503_minkeepsearch = decoder.ReadUInt32();
			this.sv503_maxkeepsearch = decoder.ReadUInt32();
			this.sv503_minkeepcomplsearch = decoder.ReadUInt32();
			this.sv503_maxkeepcomplsearch = decoder.ReadUInt32();
			this.sv503_threadcountadd = decoder.ReadUInt32();
			this.sv503_numblockthreads = decoder.ReadUInt32();
			this.sv503_scavtimeout = decoder.ReadUInt32();
			this.sv503_minrcvqueue = decoder.ReadUInt32();
			this.sv503_minfreeworkitems = decoder.ReadUInt32();
			this.sv503_xactmemsize = decoder.ReadUInt32();
			this.sv503_threadpriority = decoder.ReadUInt32();
			this.sv503_maxmpxct = decoder.ReadUInt32();
			this.sv503_oplockbreakwait = decoder.ReadUInt32();
			this.sv503_oplockbreakresponsewait = decoder.ReadUInt32();
			this.sv503_enableoplocks = decoder.ReadInt32();
			this.sv503_enableoplockforceclose = decoder.ReadInt32();
			this.sv503_enablefcbopens = decoder.ReadInt32();
			this.sv503_enableraw = decoder.ReadInt32();
			this.sv503_enablesharednetdrives = decoder.ReadInt32();
			this.sv503_minfreeconnections = decoder.ReadUInt32();
			this.sv503_maxfreeconnections = decoder.ReadUInt32();
		}
		public uint sv503_sessopens;
		public uint sv503_sessvcs;
		public uint sv503_opensearch;
		public uint sv503_sizreqbuf;
		public uint sv503_initworkitems;
		public uint sv503_maxworkitems;
		public uint sv503_rawworkitems;
		public uint sv503_irpstacksize;
		public uint sv503_maxrawbuflen;
		public uint sv503_sessusers;
		public uint sv503_sessconns;
		public uint sv503_maxpagedmemoryusage;
		public uint sv503_maxnonpagedmemoryusage;
		public int sv503_enablesoftcompat;
		public int sv503_enableforcedlogoff;
		public int sv503_timesource;
		public int sv503_acceptdownlevelapis;
		public int sv503_lmannounce;
		public RpcPointer<string> sv503_domain;
		public uint sv503_maxcopyreadlen;
		public uint sv503_maxcopywritelen;
		public uint sv503_minkeepsearch;
		public uint sv503_maxkeepsearch;
		public uint sv503_minkeepcomplsearch;
		public uint sv503_maxkeepcomplsearch;
		public uint sv503_threadcountadd;
		public uint sv503_numblockthreads;
		public uint sv503_scavtimeout;
		public uint sv503_minrcvqueue;
		public uint sv503_minfreeworkitems;
		public uint sv503_xactmemsize;
		public uint sv503_threadpriority;
		public uint sv503_maxmpxct;
		public uint sv503_oplockbreakwait;
		public uint sv503_oplockbreakresponsewait;
		public int sv503_enableoplocks;
		public int sv503_enableoplockforceclose;
		public int sv503_enablefcbopens;
		public int sv503_enableraw;
		public int sv503_enablesharednetdrives;
		public uint sv503_minfreeconnections;
		public uint sv503_maxfreeconnections;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sv503_domain))
			{
				encoder.WriteWideCharString(this.sv503_domain.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sv503_domain))
			{
				this.sv503_domain.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_599 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv599_sessopens);
			encoder.WriteValue(this.sv599_sessvcs);
			encoder.WriteValue(this.sv599_opensearch);
			encoder.WriteValue(this.sv599_sizreqbuf);
			encoder.WriteValue(this.sv599_initworkitems);
			encoder.WriteValue(this.sv599_maxworkitems);
			encoder.WriteValue(this.sv599_rawworkitems);
			encoder.WriteValue(this.sv599_irpstacksize);
			encoder.WriteValue(this.sv599_maxrawbuflen);
			encoder.WriteValue(this.sv599_sessusers);
			encoder.WriteValue(this.sv599_sessconns);
			encoder.WriteValue(this.sv599_maxpagedmemoryusage);
			encoder.WriteValue(this.sv599_maxnonpagedmemoryusage);
			encoder.WriteValue(this.sv599_enablesoftcompat);
			encoder.WriteValue(this.sv599_enableforcedlogoff);
			encoder.WriteValue(this.sv599_timesource);
			encoder.WriteValue(this.sv599_acceptdownlevelapis);
			encoder.WriteValue(this.sv599_lmannounce);
			encoder.WritePointer(this.sv599_domain);
			encoder.WriteValue(this.sv599_maxcopyreadlen);
			encoder.WriteValue(this.sv599_maxcopywritelen);
			encoder.WriteValue(this.sv599_minkeepsearch);
			encoder.WriteValue(this.sv599_maxkeepsearch);
			encoder.WriteValue(this.sv599_minkeepcomplsearch);
			encoder.WriteValue(this.sv599_maxkeepcomplsearch);
			encoder.WriteValue(this.sv599_threadcountadd);
			encoder.WriteValue(this.sv599_numblockthreads);
			encoder.WriteValue(this.sv599_scavtimeout);
			encoder.WriteValue(this.sv599_minrcvqueue);
			encoder.WriteValue(this.sv599_minfreeworkitems);
			encoder.WriteValue(this.sv599_xactmemsize);
			encoder.WriteValue(this.sv599_threadpriority);
			encoder.WriteValue(this.sv599_maxmpxct);
			encoder.WriteValue(this.sv599_oplockbreakwait);
			encoder.WriteValue(this.sv599_oplockbreakresponsewait);
			encoder.WriteValue(this.sv599_enableoplocks);
			encoder.WriteValue(this.sv599_enableoplockforceclose);
			encoder.WriteValue(this.sv599_enablefcbopens);
			encoder.WriteValue(this.sv599_enableraw);
			encoder.WriteValue(this.sv599_enablesharednetdrives);
			encoder.WriteValue(this.sv599_minfreeconnections);
			encoder.WriteValue(this.sv599_maxfreeconnections);
			encoder.WriteValue(this.sv599_initsesstable);
			encoder.WriteValue(this.sv599_initconntable);
			encoder.WriteValue(this.sv599_initfiletable);
			encoder.WriteValue(this.sv599_initsearchtable);
			encoder.WriteValue(this.sv599_alertschedule);
			encoder.WriteValue(this.sv599_errorthreshold);
			encoder.WriteValue(this.sv599_networkerrorthreshold);
			encoder.WriteValue(this.sv599_diskspacethreshold);
			encoder.WriteValue(this.sv599_reserved);
			encoder.WriteValue(this.sv599_maxlinkdelay);
			encoder.WriteValue(this.sv599_minlinkthroughput);
			encoder.WriteValue(this.sv599_linkinfovalidtime);
			encoder.WriteValue(this.sv599_scavqosinfoupdatetime);
			encoder.WriteValue(this.sv599_maxworkitemidletime);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv599_sessopens = decoder.ReadUInt32();
			this.sv599_sessvcs = decoder.ReadUInt32();
			this.sv599_opensearch = decoder.ReadUInt32();
			this.sv599_sizreqbuf = decoder.ReadUInt32();
			this.sv599_initworkitems = decoder.ReadUInt32();
			this.sv599_maxworkitems = decoder.ReadUInt32();
			this.sv599_rawworkitems = decoder.ReadUInt32();
			this.sv599_irpstacksize = decoder.ReadUInt32();
			this.sv599_maxrawbuflen = decoder.ReadUInt32();
			this.sv599_sessusers = decoder.ReadUInt32();
			this.sv599_sessconns = decoder.ReadUInt32();
			this.sv599_maxpagedmemoryusage = decoder.ReadUInt32();
			this.sv599_maxnonpagedmemoryusage = decoder.ReadUInt32();
			this.sv599_enablesoftcompat = decoder.ReadInt32();
			this.sv599_enableforcedlogoff = decoder.ReadInt32();
			this.sv599_timesource = decoder.ReadInt32();
			this.sv599_acceptdownlevelapis = decoder.ReadInt32();
			this.sv599_lmannounce = decoder.ReadInt32();
			this.sv599_domain = decoder.ReadPointer<string>();
			this.sv599_maxcopyreadlen = decoder.ReadUInt32();
			this.sv599_maxcopywritelen = decoder.ReadUInt32();
			this.sv599_minkeepsearch = decoder.ReadUInt32();
			this.sv599_maxkeepsearch = decoder.ReadUInt32();
			this.sv599_minkeepcomplsearch = decoder.ReadUInt32();
			this.sv599_maxkeepcomplsearch = decoder.ReadUInt32();
			this.sv599_threadcountadd = decoder.ReadUInt32();
			this.sv599_numblockthreads = decoder.ReadUInt32();
			this.sv599_scavtimeout = decoder.ReadUInt32();
			this.sv599_minrcvqueue = decoder.ReadUInt32();
			this.sv599_minfreeworkitems = decoder.ReadUInt32();
			this.sv599_xactmemsize = decoder.ReadUInt32();
			this.sv599_threadpriority = decoder.ReadUInt32();
			this.sv599_maxmpxct = decoder.ReadUInt32();
			this.sv599_oplockbreakwait = decoder.ReadUInt32();
			this.sv599_oplockbreakresponsewait = decoder.ReadUInt32();
			this.sv599_enableoplocks = decoder.ReadInt32();
			this.sv599_enableoplockforceclose = decoder.ReadInt32();
			this.sv599_enablefcbopens = decoder.ReadInt32();
			this.sv599_enableraw = decoder.ReadInt32();
			this.sv599_enablesharednetdrives = decoder.ReadInt32();
			this.sv599_minfreeconnections = decoder.ReadUInt32();
			this.sv599_maxfreeconnections = decoder.ReadUInt32();
			this.sv599_initsesstable = decoder.ReadUInt32();
			this.sv599_initconntable = decoder.ReadUInt32();
			this.sv599_initfiletable = decoder.ReadUInt32();
			this.sv599_initsearchtable = decoder.ReadUInt32();
			this.sv599_alertschedule = decoder.ReadUInt32();
			this.sv599_errorthreshold = decoder.ReadUInt32();
			this.sv599_networkerrorthreshold = decoder.ReadUInt32();
			this.sv599_diskspacethreshold = decoder.ReadUInt32();
			this.sv599_reserved = decoder.ReadUInt32();
			this.sv599_maxlinkdelay = decoder.ReadUInt32();
			this.sv599_minlinkthroughput = decoder.ReadUInt32();
			this.sv599_linkinfovalidtime = decoder.ReadUInt32();
			this.sv599_scavqosinfoupdatetime = decoder.ReadUInt32();
			this.sv599_maxworkitemidletime = decoder.ReadUInt32();
		}
		public uint sv599_sessopens;
		public uint sv599_sessvcs;
		public uint sv599_opensearch;
		public uint sv599_sizreqbuf;
		public uint sv599_initworkitems;
		public uint sv599_maxworkitems;
		public uint sv599_rawworkitems;
		public uint sv599_irpstacksize;
		public uint sv599_maxrawbuflen;
		public uint sv599_sessusers;
		public uint sv599_sessconns;
		public uint sv599_maxpagedmemoryusage;
		public uint sv599_maxnonpagedmemoryusage;
		public int sv599_enablesoftcompat;
		public int sv599_enableforcedlogoff;
		public int sv599_timesource;
		public int sv599_acceptdownlevelapis;
		public int sv599_lmannounce;
		public RpcPointer<string> sv599_domain;
		public uint sv599_maxcopyreadlen;
		public uint sv599_maxcopywritelen;
		public uint sv599_minkeepsearch;
		public uint sv599_maxkeepsearch;
		public uint sv599_minkeepcomplsearch;
		public uint sv599_maxkeepcomplsearch;
		public uint sv599_threadcountadd;
		public uint sv599_numblockthreads;
		public uint sv599_scavtimeout;
		public uint sv599_minrcvqueue;
		public uint sv599_minfreeworkitems;
		public uint sv599_xactmemsize;
		public uint sv599_threadpriority;
		public uint sv599_maxmpxct;
		public uint sv599_oplockbreakwait;
		public uint sv599_oplockbreakresponsewait;
		public int sv599_enableoplocks;
		public int sv599_enableoplockforceclose;
		public int sv599_enablefcbopens;
		public int sv599_enableraw;
		public int sv599_enablesharednetdrives;
		public uint sv599_minfreeconnections;
		public uint sv599_maxfreeconnections;
		public uint sv599_initsesstable;
		public uint sv599_initconntable;
		public uint sv599_initfiletable;
		public uint sv599_initsearchtable;
		public uint sv599_alertschedule;
		public uint sv599_errorthreshold;
		public uint sv599_networkerrorthreshold;
		public uint sv599_diskspacethreshold;
		public uint sv599_reserved;
		public uint sv599_maxlinkdelay;
		public uint sv599_minlinkthroughput;
		public uint sv599_linkinfovalidtime;
		public uint sv599_scavqosinfoupdatetime;
		public uint sv599_maxworkitemidletime;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sv599_domain))
			{
				encoder.WriteWideCharString(this.sv599_domain.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sv599_domain))
			{
				this.sv599_domain.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1005 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.sv1005_comment);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1005_comment = decoder.ReadPointer<string>();
		}
		public RpcPointer<string> sv1005_comment;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.sv1005_comment))
			{
				encoder.WriteWideCharString(this.sv1005_comment.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.sv1005_comment))
			{
				this.sv1005_comment.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1107 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1107_users);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1107_users = decoder.ReadUInt32();
		}
		public uint sv1107_users;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1010 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1010_disc);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1010_disc = decoder.ReadInt32();
		}
		public int sv1010_disc;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1016 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1016_hidden);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1016_hidden = decoder.ReadInt32();
		}
		public int sv1016_hidden;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1017 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1017_announce);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1017_announce = decoder.ReadUInt32();
		}
		public uint sv1017_announce;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1018 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1018_anndelta);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1018_anndelta = decoder.ReadUInt32();
		}
		public uint sv1018_anndelta;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1501 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1501_sessopens);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1501_sessopens = decoder.ReadUInt32();
		}
		public uint sv1501_sessopens;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1502 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1502_sessvcs);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1502_sessvcs = decoder.ReadUInt32();
		}
		public uint sv1502_sessvcs;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1503 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1503_opensearch);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1503_opensearch = decoder.ReadUInt32();
		}
		public uint sv1503_opensearch;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1506 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1506_maxworkitems);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1506_maxworkitems = decoder.ReadUInt32();
		}
		public uint sv1506_maxworkitems;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1510 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1510_sessusers);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1510_sessusers = decoder.ReadUInt32();
		}
		public uint sv1510_sessusers;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1511 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1511_sessconns);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1511_sessconns = decoder.ReadUInt32();
		}
		public uint sv1511_sessconns;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1512 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1512_maxnonpagedmemoryusage);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1512_maxnonpagedmemoryusage = decoder.ReadUInt32();
		}
		public uint sv1512_maxnonpagedmemoryusage;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1513 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1513_maxpagedmemoryusage);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1513_maxpagedmemoryusage = decoder.ReadUInt32();
		}
		public uint sv1513_maxpagedmemoryusage;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1514 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1514_enablesoftcompat);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1514_enablesoftcompat = decoder.ReadInt32();
		}
		public int sv1514_enablesoftcompat;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1515 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1515_enableforcedlogoff);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1515_enableforcedlogoff = decoder.ReadInt32();
		}
		public int sv1515_enableforcedlogoff;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1516 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1516_timesource);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1516_timesource = decoder.ReadInt32();
		}
		public int sv1516_timesource;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1518 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1518_lmannounce);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1518_lmannounce = decoder.ReadInt32();
		}
		public int sv1518_lmannounce;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1523 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1523_maxkeepsearch);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1523_maxkeepsearch = decoder.ReadUInt32();
		}
		public uint sv1523_maxkeepsearch;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1528 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1528_scavtimeout);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1528_scavtimeout = decoder.ReadUInt32();
		}
		public uint sv1528_scavtimeout;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1529 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1529_minrcvqueue);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1529_minrcvqueue = decoder.ReadUInt32();
		}
		public uint sv1529_minrcvqueue;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1530 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1530_minfreeworkitems);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1530_minfreeworkitems = decoder.ReadUInt32();
		}
		public uint sv1530_minfreeworkitems;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1533 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1533_maxmpxct);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1533_maxmpxct = decoder.ReadUInt32();
		}
		public uint sv1533_maxmpxct;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1534 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1534_oplockbreakwait);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1534_oplockbreakwait = decoder.ReadUInt32();
		}
		public uint sv1534_oplockbreakwait;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1535 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1535_oplockbreakresponsewait);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1535_oplockbreakresponsewait = decoder.ReadUInt32();
		}
		public uint sv1535_oplockbreakresponsewait;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1536 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1536_enableoplocks);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1536_enableoplocks = decoder.ReadInt32();
		}
		public int sv1536_enableoplocks;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1538 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1538_enablefcbopens);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1538_enablefcbopens = decoder.ReadInt32();
		}
		public int sv1538_enablefcbopens;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1539 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1539_enableraw);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1539_enableraw = decoder.ReadInt32();
		}
		public int sv1539_enableraw;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1540 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1540_enablesharednetdrives);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1540_enablesharednetdrives = decoder.ReadInt32();
		}
		public int sv1540_enablesharednetdrives;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1541 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1541_minfreeconnections);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1541_minfreeconnections = decoder.ReadInt32();
		}
		public int sv1541_minfreeconnections;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1542 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1542_maxfreeconnections);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1542_maxfreeconnections = decoder.ReadInt32();
		}
		public int sv1542_maxfreeconnections;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1543 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1543_initsesstable);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1543_initsesstable = decoder.ReadUInt32();
		}
		public uint sv1543_initsesstable;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1544 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1544_initconntable);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1544_initconntable = decoder.ReadUInt32();
		}
		public uint sv1544_initconntable;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1545 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1545_initfiletable);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1545_initfiletable = decoder.ReadUInt32();
		}
		public uint sv1545_initfiletable;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1546 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1546_initsearchtable);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1546_initsearchtable = decoder.ReadUInt32();
		}
		public uint sv1546_initsearchtable;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1547 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1547_alertschedule);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1547_alertschedule = decoder.ReadUInt32();
		}
		public uint sv1547_alertschedule;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1548 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1548_errorthreshold);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1548_errorthreshold = decoder.ReadUInt32();
		}
		public uint sv1548_errorthreshold;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1549 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1549_networkerrorthreshold);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1549_networkerrorthreshold = decoder.ReadUInt32();
		}
		public uint sv1549_networkerrorthreshold;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1550 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1550_diskspacethreshold);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1550_diskspacethreshold = decoder.ReadUInt32();
		}
		public uint sv1550_diskspacethreshold;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1552 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1552_maxlinkdelay);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1552_maxlinkdelay = decoder.ReadUInt32();
		}
		public uint sv1552_maxlinkdelay;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1553 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1553_minlinkthroughput);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1553_minlinkthroughput = decoder.ReadUInt32();
		}
		public uint sv1553_minlinkthroughput;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1554 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1554_linkinfovalidtime);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1554_linkinfovalidtime = decoder.ReadUInt32();
		}
		public uint sv1554_linkinfovalidtime;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1555 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1555_scavqosinfoupdatetime);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1555_scavqosinfoupdatetime = decoder.ReadUInt32();
		}
		public uint sv1555_scavqosinfoupdatetime;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO_1556 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1556_maxworkitemidletime);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sv1556_maxworkitemidletime = decoder.ReadUInt32();
		}
		public uint sv1556_maxworkitemidletime;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_INFO : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint unionSwitch;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.unionSwitch);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.unionSwitch)) == 100))
			{
				encoder.WritePointer(this.ServerInfo100);
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 101))
				{
					encoder.WritePointer(this.ServerInfo101);
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 102))
					{
						encoder.WritePointer(this.ServerInfo102);
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 103))
						{
							encoder.WritePointer(this.ServerInfo103);
						}
						else
						{
							if ((((int)(this.unionSwitch)) == 502))
							{
								encoder.WritePointer(this.ServerInfo502);
							}
							else
							{
								if ((((int)(this.unionSwitch)) == 503))
								{
									encoder.WritePointer(this.ServerInfo503);
								}
								else
								{
									if ((((int)(this.unionSwitch)) == 599))
									{
										encoder.WritePointer(this.ServerInfo599);
									}
									else
									{
										if ((((int)(this.unionSwitch)) == 1005))
										{
											encoder.WritePointer(this.ServerInfo1005);
										}
										else
										{
											if ((((int)(this.unionSwitch)) == 1107))
											{
												encoder.WritePointer(this.ServerInfo1107);
											}
											else
											{
												if ((((int)(this.unionSwitch)) == 1010))
												{
													encoder.WritePointer(this.ServerInfo1010);
												}
												else
												{
													if ((((int)(this.unionSwitch)) == 1016))
													{
														encoder.WritePointer(this.ServerInfo1016);
													}
													else
													{
														if ((((int)(this.unionSwitch)) == 1017))
														{
															encoder.WritePointer(this.ServerInfo1017);
														}
														else
														{
															if ((((int)(this.unionSwitch)) == 1018))
															{
																encoder.WritePointer(this.ServerInfo1018);
															}
															else
															{
																if ((((int)(this.unionSwitch)) == 1501))
																{
																	encoder.WritePointer(this.ServerInfo1501);
																}
																else
																{
																	if ((((int)(this.unionSwitch)) == 1502))
																	{
																		encoder.WritePointer(this.ServerInfo1502);
																	}
																	else
																	{
																		if ((((int)(this.unionSwitch)) == 1503))
																		{
																			encoder.WritePointer(this.ServerInfo1503);
																		}
																		else
																		{
																			if ((((int)(this.unionSwitch)) == 1506))
																			{
																				encoder.WritePointer(this.ServerInfo1506);
																			}
																			else
																			{
																				if ((((int)(this.unionSwitch)) == 1510))
																				{
																					encoder.WritePointer(this.ServerInfo1510);
																				}
																				else
																				{
																					if ((((int)(this.unionSwitch)) == 1511))
																					{
																						encoder.WritePointer(this.ServerInfo1511);
																					}
																					else
																					{
																						if ((((int)(this.unionSwitch)) == 1512))
																						{
																							encoder.WritePointer(this.ServerInfo1512);
																						}
																						else
																						{
																							if ((((int)(this.unionSwitch)) == 1513))
																							{
																								encoder.WritePointer(this.ServerInfo1513);
																							}
																							else
																							{
																								if ((((int)(this.unionSwitch)) == 1514))
																								{
																									encoder.WritePointer(this.ServerInfo1514);
																								}
																								else
																								{
																									if ((((int)(this.unionSwitch)) == 1515))
																									{
																										encoder.WritePointer(this.ServerInfo1515);
																									}
																									else
																									{
																										if ((((int)(this.unionSwitch)) == 1516))
																										{
																											encoder.WritePointer(this.ServerInfo1516);
																										}
																										else
																										{
																											if ((((int)(this.unionSwitch)) == 1518))
																											{
																												encoder.WritePointer(this.ServerInfo1518);
																											}
																											else
																											{
																												if ((((int)(this.unionSwitch)) == 1523))
																												{
																													encoder.WritePointer(this.ServerInfo1523);
																												}
																												else
																												{
																													if ((((int)(this.unionSwitch)) == 1528))
																													{
																														encoder.WritePointer(this.ServerInfo1528);
																													}
																													else
																													{
																														if ((((int)(this.unionSwitch)) == 1529))
																														{
																															encoder.WritePointer(this.ServerInfo1529);
																														}
																														else
																														{
																															if ((((int)(this.unionSwitch)) == 1530))
																															{
																																encoder.WritePointer(this.ServerInfo1530);
																															}
																															else
																															{
																																if ((((int)(this.unionSwitch)) == 1533))
																																{
																																	encoder.WritePointer(this.ServerInfo1533);
																																}
																																else
																																{
																																	if ((((int)(this.unionSwitch)) == 1534))
																																	{
																																		encoder.WritePointer(this.ServerInfo1534);
																																	}
																																	else
																																	{
																																		if ((((int)(this.unionSwitch)) == 1535))
																																		{
																																			encoder.WritePointer(this.ServerInfo1535);
																																		}
																																		else
																																		{
																																			if ((((int)(this.unionSwitch)) == 1536))
																																			{
																																				encoder.WritePointer(this.ServerInfo1536);
																																			}
																																			else
																																			{
																																				if ((((int)(this.unionSwitch)) == 1538))
																																				{
																																					encoder.WritePointer(this.ServerInfo1538);
																																				}
																																				else
																																				{
																																					if ((((int)(this.unionSwitch)) == 1539))
																																					{
																																						encoder.WritePointer(this.ServerInfo1539);
																																					}
																																					else
																																					{
																																						if ((((int)(this.unionSwitch)) == 1540))
																																						{
																																							encoder.WritePointer(this.ServerInfo1540);
																																						}
																																						else
																																						{
																																							if ((((int)(this.unionSwitch)) == 1541))
																																							{
																																								encoder.WritePointer(this.ServerInfo1541);
																																							}
																																							else
																																							{
																																								if ((((int)(this.unionSwitch)) == 1542))
																																								{
																																									encoder.WritePointer(this.ServerInfo1542);
																																								}
																																								else
																																								{
																																									if ((((int)(this.unionSwitch)) == 1543))
																																									{
																																										encoder.WritePointer(this.ServerInfo1543);
																																									}
																																									else
																																									{
																																										if ((((int)(this.unionSwitch)) == 1544))
																																										{
																																											encoder.WritePointer(this.ServerInfo1544);
																																										}
																																										else
																																										{
																																											if ((((int)(this.unionSwitch)) == 1545))
																																											{
																																												encoder.WritePointer(this.ServerInfo1545);
																																											}
																																											else
																																											{
																																												if ((((int)(this.unionSwitch)) == 1546))
																																												{
																																													encoder.WritePointer(this.ServerInfo1546);
																																												}
																																												else
																																												{
																																													if ((((int)(this.unionSwitch)) == 1547))
																																													{
																																														encoder.WritePointer(this.ServerInfo1547);
																																													}
																																													else
																																													{
																																														if ((((int)(this.unionSwitch)) == 1548))
																																														{
																																															encoder.WritePointer(this.ServerInfo1548);
																																														}
																																														else
																																														{
																																															if ((((int)(this.unionSwitch)) == 1549))
																																															{
																																																encoder.WritePointer(this.ServerInfo1549);
																																															}
																																															else
																																															{
																																																if ((((int)(this.unionSwitch)) == 1550))
																																																{
																																																	encoder.WritePointer(this.ServerInfo1550);
																																																}
																																																else
																																																{
																																																	if ((((int)(this.unionSwitch)) == 1552))
																																																	{
																																																		encoder.WritePointer(this.ServerInfo1552);
																																																	}
																																																	else
																																																	{
																																																		if ((((int)(this.unionSwitch)) == 1553))
																																																		{
																																																			encoder.WritePointer(this.ServerInfo1553);
																																																		}
																																																		else
																																																		{
																																																			if ((((int)(this.unionSwitch)) == 1554))
																																																			{
																																																				encoder.WritePointer(this.ServerInfo1554);
																																																			}
																																																			else
																																																			{
																																																				if ((((int)(this.unionSwitch)) == 1555))
																																																				{
																																																					encoder.WritePointer(this.ServerInfo1555);
																																																				}
																																																				else
																																																				{
																																																					if ((((int)(this.unionSwitch)) == 1556))
																																																					{
																																																						encoder.WritePointer(this.ServerInfo1556);
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
					}
				}
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.unionSwitch = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.unionSwitch)) == 100))
			{
				this.ServerInfo100 = decoder.ReadPointer<ms_dtyp.SERVER_INFO_100>();
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 101))
				{
					this.ServerInfo101 = decoder.ReadPointer<ms_dtyp.SERVER_INFO_101>();
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 102))
					{
						this.ServerInfo102 = decoder.ReadPointer<SERVER_INFO_102>();
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 103))
						{
							this.ServerInfo103 = decoder.ReadPointer<SERVER_INFO_103>();
						}
						else
						{
							if ((((int)(this.unionSwitch)) == 502))
							{
								this.ServerInfo502 = decoder.ReadPointer<SERVER_INFO_502>();
							}
							else
							{
								if ((((int)(this.unionSwitch)) == 503))
								{
									this.ServerInfo503 = decoder.ReadPointer<SERVER_INFO_503>();
								}
								else
								{
									if ((((int)(this.unionSwitch)) == 599))
									{
										this.ServerInfo599 = decoder.ReadPointer<SERVER_INFO_599>();
									}
									else
									{
										if ((((int)(this.unionSwitch)) == 1005))
										{
											this.ServerInfo1005 = decoder.ReadPointer<SERVER_INFO_1005>();
										}
										else
										{
											if ((((int)(this.unionSwitch)) == 1107))
											{
												this.ServerInfo1107 = decoder.ReadPointer<SERVER_INFO_1107>();
											}
											else
											{
												if ((((int)(this.unionSwitch)) == 1010))
												{
													this.ServerInfo1010 = decoder.ReadPointer<SERVER_INFO_1010>();
												}
												else
												{
													if ((((int)(this.unionSwitch)) == 1016))
													{
														this.ServerInfo1016 = decoder.ReadPointer<SERVER_INFO_1016>();
													}
													else
													{
														if ((((int)(this.unionSwitch)) == 1017))
														{
															this.ServerInfo1017 = decoder.ReadPointer<SERVER_INFO_1017>();
														}
														else
														{
															if ((((int)(this.unionSwitch)) == 1018))
															{
																this.ServerInfo1018 = decoder.ReadPointer<SERVER_INFO_1018>();
															}
															else
															{
																if ((((int)(this.unionSwitch)) == 1501))
																{
																	this.ServerInfo1501 = decoder.ReadPointer<SERVER_INFO_1501>();
																}
																else
																{
																	if ((((int)(this.unionSwitch)) == 1502))
																	{
																		this.ServerInfo1502 = decoder.ReadPointer<SERVER_INFO_1502>();
																	}
																	else
																	{
																		if ((((int)(this.unionSwitch)) == 1503))
																		{
																			this.ServerInfo1503 = decoder.ReadPointer<SERVER_INFO_1503>();
																		}
																		else
																		{
																			if ((((int)(this.unionSwitch)) == 1506))
																			{
																				this.ServerInfo1506 = decoder.ReadPointer<SERVER_INFO_1506>();
																			}
																			else
																			{
																				if ((((int)(this.unionSwitch)) == 1510))
																				{
																					this.ServerInfo1510 = decoder.ReadPointer<SERVER_INFO_1510>();
																				}
																				else
																				{
																					if ((((int)(this.unionSwitch)) == 1511))
																					{
																						this.ServerInfo1511 = decoder.ReadPointer<SERVER_INFO_1511>();
																					}
																					else
																					{
																						if ((((int)(this.unionSwitch)) == 1512))
																						{
																							this.ServerInfo1512 = decoder.ReadPointer<SERVER_INFO_1512>();
																						}
																						else
																						{
																							if ((((int)(this.unionSwitch)) == 1513))
																							{
																								this.ServerInfo1513 = decoder.ReadPointer<SERVER_INFO_1513>();
																							}
																							else
																							{
																								if ((((int)(this.unionSwitch)) == 1514))
																								{
																									this.ServerInfo1514 = decoder.ReadPointer<SERVER_INFO_1514>();
																								}
																								else
																								{
																									if ((((int)(this.unionSwitch)) == 1515))
																									{
																										this.ServerInfo1515 = decoder.ReadPointer<SERVER_INFO_1515>();
																									}
																									else
																									{
																										if ((((int)(this.unionSwitch)) == 1516))
																										{
																											this.ServerInfo1516 = decoder.ReadPointer<SERVER_INFO_1516>();
																										}
																										else
																										{
																											if ((((int)(this.unionSwitch)) == 1518))
																											{
																												this.ServerInfo1518 = decoder.ReadPointer<SERVER_INFO_1518>();
																											}
																											else
																											{
																												if ((((int)(this.unionSwitch)) == 1523))
																												{
																													this.ServerInfo1523 = decoder.ReadPointer<SERVER_INFO_1523>();
																												}
																												else
																												{
																													if ((((int)(this.unionSwitch)) == 1528))
																													{
																														this.ServerInfo1528 = decoder.ReadPointer<SERVER_INFO_1528>();
																													}
																													else
																													{
																														if ((((int)(this.unionSwitch)) == 1529))
																														{
																															this.ServerInfo1529 = decoder.ReadPointer<SERVER_INFO_1529>();
																														}
																														else
																														{
																															if ((((int)(this.unionSwitch)) == 1530))
																															{
																																this.ServerInfo1530 = decoder.ReadPointer<SERVER_INFO_1530>();
																															}
																															else
																															{
																																if ((((int)(this.unionSwitch)) == 1533))
																																{
																																	this.ServerInfo1533 = decoder.ReadPointer<SERVER_INFO_1533>();
																																}
																																else
																																{
																																	if ((((int)(this.unionSwitch)) == 1534))
																																	{
																																		this.ServerInfo1534 = decoder.ReadPointer<SERVER_INFO_1534>();
																																	}
																																	else
																																	{
																																		if ((((int)(this.unionSwitch)) == 1535))
																																		{
																																			this.ServerInfo1535 = decoder.ReadPointer<SERVER_INFO_1535>();
																																		}
																																		else
																																		{
																																			if ((((int)(this.unionSwitch)) == 1536))
																																			{
																																				this.ServerInfo1536 = decoder.ReadPointer<SERVER_INFO_1536>();
																																			}
																																			else
																																			{
																																				if ((((int)(this.unionSwitch)) == 1538))
																																				{
																																					this.ServerInfo1538 = decoder.ReadPointer<SERVER_INFO_1538>();
																																				}
																																				else
																																				{
																																					if ((((int)(this.unionSwitch)) == 1539))
																																					{
																																						this.ServerInfo1539 = decoder.ReadPointer<SERVER_INFO_1539>();
																																					}
																																					else
																																					{
																																						if ((((int)(this.unionSwitch)) == 1540))
																																						{
																																							this.ServerInfo1540 = decoder.ReadPointer<SERVER_INFO_1540>();
																																						}
																																						else
																																						{
																																							if ((((int)(this.unionSwitch)) == 1541))
																																							{
																																								this.ServerInfo1541 = decoder.ReadPointer<SERVER_INFO_1541>();
																																							}
																																							else
																																							{
																																								if ((((int)(this.unionSwitch)) == 1542))
																																								{
																																									this.ServerInfo1542 = decoder.ReadPointer<SERVER_INFO_1542>();
																																								}
																																								else
																																								{
																																									if ((((int)(this.unionSwitch)) == 1543))
																																									{
																																										this.ServerInfo1543 = decoder.ReadPointer<SERVER_INFO_1543>();
																																									}
																																									else
																																									{
																																										if ((((int)(this.unionSwitch)) == 1544))
																																										{
																																											this.ServerInfo1544 = decoder.ReadPointer<SERVER_INFO_1544>();
																																										}
																																										else
																																										{
																																											if ((((int)(this.unionSwitch)) == 1545))
																																											{
																																												this.ServerInfo1545 = decoder.ReadPointer<SERVER_INFO_1545>();
																																											}
																																											else
																																											{
																																												if ((((int)(this.unionSwitch)) == 1546))
																																												{
																																													this.ServerInfo1546 = decoder.ReadPointer<SERVER_INFO_1546>();
																																												}
																																												else
																																												{
																																													if ((((int)(this.unionSwitch)) == 1547))
																																													{
																																														this.ServerInfo1547 = decoder.ReadPointer<SERVER_INFO_1547>();
																																													}
																																													else
																																													{
																																														if ((((int)(this.unionSwitch)) == 1548))
																																														{
																																															this.ServerInfo1548 = decoder.ReadPointer<SERVER_INFO_1548>();
																																														}
																																														else
																																														{
																																															if ((((int)(this.unionSwitch)) == 1549))
																																															{
																																																this.ServerInfo1549 = decoder.ReadPointer<SERVER_INFO_1549>();
																																															}
																																															else
																																															{
																																																if ((((int)(this.unionSwitch)) == 1550))
																																																{
																																																	this.ServerInfo1550 = decoder.ReadPointer<SERVER_INFO_1550>();
																																																}
																																																else
																																																{
																																																	if ((((int)(this.unionSwitch)) == 1552))
																																																	{
																																																		this.ServerInfo1552 = decoder.ReadPointer<SERVER_INFO_1552>();
																																																	}
																																																	else
																																																	{
																																																		if ((((int)(this.unionSwitch)) == 1553))
																																																		{
																																																			this.ServerInfo1553 = decoder.ReadPointer<SERVER_INFO_1553>();
																																																		}
																																																		else
																																																		{
																																																			if ((((int)(this.unionSwitch)) == 1554))
																																																			{
																																																				this.ServerInfo1554 = decoder.ReadPointer<SERVER_INFO_1554>();
																																																			}
																																																			else
																																																			{
																																																				if ((((int)(this.unionSwitch)) == 1555))
																																																				{
																																																					this.ServerInfo1555 = decoder.ReadPointer<SERVER_INFO_1555>();
																																																				}
																																																				else
																																																				{
																																																					if ((((int)(this.unionSwitch)) == 1556))
																																																					{
																																																						this.ServerInfo1556 = decoder.ReadPointer<SERVER_INFO_1556>();
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
					}
				}
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.unionSwitch)) == 100))
			{
				if ((null != this.ServerInfo100))
				{
					encoder.WriteFixedStruct(this.ServerInfo100.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(this.ServerInfo100.value);
				}
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 101))
				{
					if ((null != this.ServerInfo101))
					{
						encoder.WriteFixedStruct(this.ServerInfo101.value, Titanis.DceRpc.NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ServerInfo101.value);
					}
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 102))
					{
						if ((null != this.ServerInfo102))
						{
							encoder.WriteFixedStruct(this.ServerInfo102.value, Titanis.DceRpc.NdrAlignment.NativePtr);
							encoder.WriteStructDeferral(this.ServerInfo102.value);
						}
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 103))
						{
							if ((null != this.ServerInfo103))
							{
								encoder.WriteFixedStruct(this.ServerInfo103.value, Titanis.DceRpc.NdrAlignment.NativePtr);
								encoder.WriteStructDeferral(this.ServerInfo103.value);
							}
						}
						else
						{
							if ((((int)(this.unionSwitch)) == 502))
							{
								if ((null != this.ServerInfo502))
								{
									encoder.WriteFixedStruct(this.ServerInfo502.value, Titanis.DceRpc.NdrAlignment._4Byte);
									encoder.WriteStructDeferral(this.ServerInfo502.value);
								}
							}
							else
							{
								if ((((int)(this.unionSwitch)) == 503))
								{
									if ((null != this.ServerInfo503))
									{
										encoder.WriteFixedStruct(this.ServerInfo503.value, Titanis.DceRpc.NdrAlignment.NativePtr);
										encoder.WriteStructDeferral(this.ServerInfo503.value);
									}
								}
								else
								{
									if ((((int)(this.unionSwitch)) == 599))
									{
										if ((null != this.ServerInfo599))
										{
											encoder.WriteFixedStruct(this.ServerInfo599.value, Titanis.DceRpc.NdrAlignment.NativePtr);
											encoder.WriteStructDeferral(this.ServerInfo599.value);
										}
									}
									else
									{
										if ((((int)(this.unionSwitch)) == 1005))
										{
											if ((null != this.ServerInfo1005))
											{
												encoder.WriteFixedStruct(this.ServerInfo1005.value, Titanis.DceRpc.NdrAlignment.NativePtr);
												encoder.WriteStructDeferral(this.ServerInfo1005.value);
											}
										}
										else
										{
											if ((((int)(this.unionSwitch)) == 1107))
											{
												if ((null != this.ServerInfo1107))
												{
													encoder.WriteFixedStruct(this.ServerInfo1107.value, Titanis.DceRpc.NdrAlignment._4Byte);
													encoder.WriteStructDeferral(this.ServerInfo1107.value);
												}
											}
											else
											{
												if ((((int)(this.unionSwitch)) == 1010))
												{
													if ((null != this.ServerInfo1010))
													{
														encoder.WriteFixedStruct(this.ServerInfo1010.value, Titanis.DceRpc.NdrAlignment._4Byte);
														encoder.WriteStructDeferral(this.ServerInfo1010.value);
													}
												}
												else
												{
													if ((((int)(this.unionSwitch)) == 1016))
													{
														if ((null != this.ServerInfo1016))
														{
															encoder.WriteFixedStruct(this.ServerInfo1016.value, Titanis.DceRpc.NdrAlignment._4Byte);
															encoder.WriteStructDeferral(this.ServerInfo1016.value);
														}
													}
													else
													{
														if ((((int)(this.unionSwitch)) == 1017))
														{
															if ((null != this.ServerInfo1017))
															{
																encoder.WriteFixedStruct(this.ServerInfo1017.value, Titanis.DceRpc.NdrAlignment._4Byte);
																encoder.WriteStructDeferral(this.ServerInfo1017.value);
															}
														}
														else
														{
															if ((((int)(this.unionSwitch)) == 1018))
															{
																if ((null != this.ServerInfo1018))
																{
																	encoder.WriteFixedStruct(this.ServerInfo1018.value, Titanis.DceRpc.NdrAlignment._4Byte);
																	encoder.WriteStructDeferral(this.ServerInfo1018.value);
																}
															}
															else
															{
																if ((((int)(this.unionSwitch)) == 1501))
																{
																	if ((null != this.ServerInfo1501))
																	{
																		encoder.WriteFixedStruct(this.ServerInfo1501.value, Titanis.DceRpc.NdrAlignment._4Byte);
																		encoder.WriteStructDeferral(this.ServerInfo1501.value);
																	}
																}
																else
																{
																	if ((((int)(this.unionSwitch)) == 1502))
																	{
																		if ((null != this.ServerInfo1502))
																		{
																			encoder.WriteFixedStruct(this.ServerInfo1502.value, Titanis.DceRpc.NdrAlignment._4Byte);
																			encoder.WriteStructDeferral(this.ServerInfo1502.value);
																		}
																	}
																	else
																	{
																		if ((((int)(this.unionSwitch)) == 1503))
																		{
																			if ((null != this.ServerInfo1503))
																			{
																				encoder.WriteFixedStruct(this.ServerInfo1503.value, Titanis.DceRpc.NdrAlignment._4Byte);
																				encoder.WriteStructDeferral(this.ServerInfo1503.value);
																			}
																		}
																		else
																		{
																			if ((((int)(this.unionSwitch)) == 1506))
																			{
																				if ((null != this.ServerInfo1506))
																				{
																					encoder.WriteFixedStruct(this.ServerInfo1506.value, Titanis.DceRpc.NdrAlignment._4Byte);
																					encoder.WriteStructDeferral(this.ServerInfo1506.value);
																				}
																			}
																			else
																			{
																				if ((((int)(this.unionSwitch)) == 1510))
																				{
																					if ((null != this.ServerInfo1510))
																					{
																						encoder.WriteFixedStruct(this.ServerInfo1510.value, Titanis.DceRpc.NdrAlignment._4Byte);
																						encoder.WriteStructDeferral(this.ServerInfo1510.value);
																					}
																				}
																				else
																				{
																					if ((((int)(this.unionSwitch)) == 1511))
																					{
																						if ((null != this.ServerInfo1511))
																						{
																							encoder.WriteFixedStruct(this.ServerInfo1511.value, Titanis.DceRpc.NdrAlignment._4Byte);
																							encoder.WriteStructDeferral(this.ServerInfo1511.value);
																						}
																					}
																					else
																					{
																						if ((((int)(this.unionSwitch)) == 1512))
																						{
																							if ((null != this.ServerInfo1512))
																							{
																								encoder.WriteFixedStruct(this.ServerInfo1512.value, Titanis.DceRpc.NdrAlignment._4Byte);
																								encoder.WriteStructDeferral(this.ServerInfo1512.value);
																							}
																						}
																						else
																						{
																							if ((((int)(this.unionSwitch)) == 1513))
																							{
																								if ((null != this.ServerInfo1513))
																								{
																									encoder.WriteFixedStruct(this.ServerInfo1513.value, Titanis.DceRpc.NdrAlignment._4Byte);
																									encoder.WriteStructDeferral(this.ServerInfo1513.value);
																								}
																							}
																							else
																							{
																								if ((((int)(this.unionSwitch)) == 1514))
																								{
																									if ((null != this.ServerInfo1514))
																									{
																										encoder.WriteFixedStruct(this.ServerInfo1514.value, Titanis.DceRpc.NdrAlignment._4Byte);
																										encoder.WriteStructDeferral(this.ServerInfo1514.value);
																									}
																								}
																								else
																								{
																									if ((((int)(this.unionSwitch)) == 1515))
																									{
																										if ((null != this.ServerInfo1515))
																										{
																											encoder.WriteFixedStruct(this.ServerInfo1515.value, Titanis.DceRpc.NdrAlignment._4Byte);
																											encoder.WriteStructDeferral(this.ServerInfo1515.value);
																										}
																									}
																									else
																									{
																										if ((((int)(this.unionSwitch)) == 1516))
																										{
																											if ((null != this.ServerInfo1516))
																											{
																												encoder.WriteFixedStruct(this.ServerInfo1516.value, Titanis.DceRpc.NdrAlignment._4Byte);
																												encoder.WriteStructDeferral(this.ServerInfo1516.value);
																											}
																										}
																										else
																										{
																											if ((((int)(this.unionSwitch)) == 1518))
																											{
																												if ((null != this.ServerInfo1518))
																												{
																													encoder.WriteFixedStruct(this.ServerInfo1518.value, Titanis.DceRpc.NdrAlignment._4Byte);
																													encoder.WriteStructDeferral(this.ServerInfo1518.value);
																												}
																											}
																											else
																											{
																												if ((((int)(this.unionSwitch)) == 1523))
																												{
																													if ((null != this.ServerInfo1523))
																													{
																														encoder.WriteFixedStruct(this.ServerInfo1523.value, Titanis.DceRpc.NdrAlignment._4Byte);
																														encoder.WriteStructDeferral(this.ServerInfo1523.value);
																													}
																												}
																												else
																												{
																													if ((((int)(this.unionSwitch)) == 1528))
																													{
																														if ((null != this.ServerInfo1528))
																														{
																															encoder.WriteFixedStruct(this.ServerInfo1528.value, Titanis.DceRpc.NdrAlignment._4Byte);
																															encoder.WriteStructDeferral(this.ServerInfo1528.value);
																														}
																													}
																													else
																													{
																														if ((((int)(this.unionSwitch)) == 1529))
																														{
																															if ((null != this.ServerInfo1529))
																															{
																																encoder.WriteFixedStruct(this.ServerInfo1529.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																encoder.WriteStructDeferral(this.ServerInfo1529.value);
																															}
																														}
																														else
																														{
																															if ((((int)(this.unionSwitch)) == 1530))
																															{
																																if ((null != this.ServerInfo1530))
																																{
																																	encoder.WriteFixedStruct(this.ServerInfo1530.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																	encoder.WriteStructDeferral(this.ServerInfo1530.value);
																																}
																															}
																															else
																															{
																																if ((((int)(this.unionSwitch)) == 1533))
																																{
																																	if ((null != this.ServerInfo1533))
																																	{
																																		encoder.WriteFixedStruct(this.ServerInfo1533.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																		encoder.WriteStructDeferral(this.ServerInfo1533.value);
																																	}
																																}
																																else
																																{
																																	if ((((int)(this.unionSwitch)) == 1534))
																																	{
																																		if ((null != this.ServerInfo1534))
																																		{
																																			encoder.WriteFixedStruct(this.ServerInfo1534.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																			encoder.WriteStructDeferral(this.ServerInfo1534.value);
																																		}
																																	}
																																	else
																																	{
																																		if ((((int)(this.unionSwitch)) == 1535))
																																		{
																																			if ((null != this.ServerInfo1535))
																																			{
																																				encoder.WriteFixedStruct(this.ServerInfo1535.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																				encoder.WriteStructDeferral(this.ServerInfo1535.value);
																																			}
																																		}
																																		else
																																		{
																																			if ((((int)(this.unionSwitch)) == 1536))
																																			{
																																				if ((null != this.ServerInfo1536))
																																				{
																																					encoder.WriteFixedStruct(this.ServerInfo1536.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																					encoder.WriteStructDeferral(this.ServerInfo1536.value);
																																				}
																																			}
																																			else
																																			{
																																				if ((((int)(this.unionSwitch)) == 1538))
																																				{
																																					if ((null != this.ServerInfo1538))
																																					{
																																						encoder.WriteFixedStruct(this.ServerInfo1538.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																						encoder.WriteStructDeferral(this.ServerInfo1538.value);
																																					}
																																				}
																																				else
																																				{
																																					if ((((int)(this.unionSwitch)) == 1539))
																																					{
																																						if ((null != this.ServerInfo1539))
																																						{
																																							encoder.WriteFixedStruct(this.ServerInfo1539.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																							encoder.WriteStructDeferral(this.ServerInfo1539.value);
																																						}
																																					}
																																					else
																																					{
																																						if ((((int)(this.unionSwitch)) == 1540))
																																						{
																																							if ((null != this.ServerInfo1540))
																																							{
																																								encoder.WriteFixedStruct(this.ServerInfo1540.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																								encoder.WriteStructDeferral(this.ServerInfo1540.value);
																																							}
																																						}
																																						else
																																						{
																																							if ((((int)(this.unionSwitch)) == 1541))
																																							{
																																								if ((null != this.ServerInfo1541))
																																								{
																																									encoder.WriteFixedStruct(this.ServerInfo1541.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																									encoder.WriteStructDeferral(this.ServerInfo1541.value);
																																								}
																																							}
																																							else
																																							{
																																								if ((((int)(this.unionSwitch)) == 1542))
																																								{
																																									if ((null != this.ServerInfo1542))
																																									{
																																										encoder.WriteFixedStruct(this.ServerInfo1542.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																										encoder.WriteStructDeferral(this.ServerInfo1542.value);
																																									}
																																								}
																																								else
																																								{
																																									if ((((int)(this.unionSwitch)) == 1543))
																																									{
																																										if ((null != this.ServerInfo1543))
																																										{
																																											encoder.WriteFixedStruct(this.ServerInfo1543.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																											encoder.WriteStructDeferral(this.ServerInfo1543.value);
																																										}
																																									}
																																									else
																																									{
																																										if ((((int)(this.unionSwitch)) == 1544))
																																										{
																																											if ((null != this.ServerInfo1544))
																																											{
																																												encoder.WriteFixedStruct(this.ServerInfo1544.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																												encoder.WriteStructDeferral(this.ServerInfo1544.value);
																																											}
																																										}
																																										else
																																										{
																																											if ((((int)(this.unionSwitch)) == 1545))
																																											{
																																												if ((null != this.ServerInfo1545))
																																												{
																																													encoder.WriteFixedStruct(this.ServerInfo1545.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																													encoder.WriteStructDeferral(this.ServerInfo1545.value);
																																												}
																																											}
																																											else
																																											{
																																												if ((((int)(this.unionSwitch)) == 1546))
																																												{
																																													if ((null != this.ServerInfo1546))
																																													{
																																														encoder.WriteFixedStruct(this.ServerInfo1546.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																														encoder.WriteStructDeferral(this.ServerInfo1546.value);
																																													}
																																												}
																																												else
																																												{
																																													if ((((int)(this.unionSwitch)) == 1547))
																																													{
																																														if ((null != this.ServerInfo1547))
																																														{
																																															encoder.WriteFixedStruct(this.ServerInfo1547.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																															encoder.WriteStructDeferral(this.ServerInfo1547.value);
																																														}
																																													}
																																													else
																																													{
																																														if ((((int)(this.unionSwitch)) == 1548))
																																														{
																																															if ((null != this.ServerInfo1548))
																																															{
																																																encoder.WriteFixedStruct(this.ServerInfo1548.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																																encoder.WriteStructDeferral(this.ServerInfo1548.value);
																																															}
																																														}
																																														else
																																														{
																																															if ((((int)(this.unionSwitch)) == 1549))
																																															{
																																																if ((null != this.ServerInfo1549))
																																																{
																																																	encoder.WriteFixedStruct(this.ServerInfo1549.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																																	encoder.WriteStructDeferral(this.ServerInfo1549.value);
																																																}
																																															}
																																															else
																																															{
																																																if ((((int)(this.unionSwitch)) == 1550))
																																																{
																																																	if ((null != this.ServerInfo1550))
																																																	{
																																																		encoder.WriteFixedStruct(this.ServerInfo1550.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																																		encoder.WriteStructDeferral(this.ServerInfo1550.value);
																																																	}
																																																}
																																																else
																																																{
																																																	if ((((int)(this.unionSwitch)) == 1552))
																																																	{
																																																		if ((null != this.ServerInfo1552))
																																																		{
																																																			encoder.WriteFixedStruct(this.ServerInfo1552.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																																			encoder.WriteStructDeferral(this.ServerInfo1552.value);
																																																		}
																																																	}
																																																	else
																																																	{
																																																		if ((((int)(this.unionSwitch)) == 1553))
																																																		{
																																																			if ((null != this.ServerInfo1553))
																																																			{
																																																				encoder.WriteFixedStruct(this.ServerInfo1553.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																																				encoder.WriteStructDeferral(this.ServerInfo1553.value);
																																																			}
																																																		}
																																																		else
																																																		{
																																																			if ((((int)(this.unionSwitch)) == 1554))
																																																			{
																																																				if ((null != this.ServerInfo1554))
																																																				{
																																																					encoder.WriteFixedStruct(this.ServerInfo1554.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																																					encoder.WriteStructDeferral(this.ServerInfo1554.value);
																																																				}
																																																			}
																																																			else
																																																			{
																																																				if ((((int)(this.unionSwitch)) == 1555))
																																																				{
																																																					if ((null != this.ServerInfo1555))
																																																					{
																																																						encoder.WriteFixedStruct(this.ServerInfo1555.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																																						encoder.WriteStructDeferral(this.ServerInfo1555.value);
																																																					}
																																																				}
																																																				else
																																																				{
																																																					if ((((int)(this.unionSwitch)) == 1556))
																																																					{
																																																						if ((null != this.ServerInfo1556))
																																																						{
																																																							encoder.WriteFixedStruct(this.ServerInfo1556.value, Titanis.DceRpc.NdrAlignment._4Byte);
																																																							encoder.WriteStructDeferral(this.ServerInfo1556.value);
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
						}
					}
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.unionSwitch)) == 100))
			{
				if ((null != this.ServerInfo100))
				{
					this.ServerInfo100.value = decoder.ReadFixedStruct<ms_dtyp.SERVER_INFO_100>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<ms_dtyp.SERVER_INFO_100>(ref this.ServerInfo100.value);
				}
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 101))
				{
					if ((null != this.ServerInfo101))
					{
						this.ServerInfo101.value = decoder.ReadFixedStruct<ms_dtyp.SERVER_INFO_101>(Titanis.DceRpc.NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<ms_dtyp.SERVER_INFO_101>(ref this.ServerInfo101.value);
					}
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 102))
					{
						if ((null != this.ServerInfo102))
						{
							this.ServerInfo102.value = decoder.ReadFixedStruct<SERVER_INFO_102>(Titanis.DceRpc.NdrAlignment.NativePtr);
							decoder.ReadStructDeferral<SERVER_INFO_102>(ref this.ServerInfo102.value);
						}
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 103))
						{
							if ((null != this.ServerInfo103))
							{
								this.ServerInfo103.value = decoder.ReadFixedStruct<SERVER_INFO_103>(Titanis.DceRpc.NdrAlignment.NativePtr);
								decoder.ReadStructDeferral<SERVER_INFO_103>(ref this.ServerInfo103.value);
							}
						}
						else
						{
							if ((((int)(this.unionSwitch)) == 502))
							{
								if ((null != this.ServerInfo502))
								{
									this.ServerInfo502.value = decoder.ReadFixedStruct<SERVER_INFO_502>(Titanis.DceRpc.NdrAlignment._4Byte);
									decoder.ReadStructDeferral<SERVER_INFO_502>(ref this.ServerInfo502.value);
								}
							}
							else
							{
								if ((((int)(this.unionSwitch)) == 503))
								{
									if ((null != this.ServerInfo503))
									{
										this.ServerInfo503.value = decoder.ReadFixedStruct<SERVER_INFO_503>(Titanis.DceRpc.NdrAlignment.NativePtr);
										decoder.ReadStructDeferral<SERVER_INFO_503>(ref this.ServerInfo503.value);
									}
								}
								else
								{
									if ((((int)(this.unionSwitch)) == 599))
									{
										if ((null != this.ServerInfo599))
										{
											this.ServerInfo599.value = decoder.ReadFixedStruct<SERVER_INFO_599>(Titanis.DceRpc.NdrAlignment.NativePtr);
											decoder.ReadStructDeferral<SERVER_INFO_599>(ref this.ServerInfo599.value);
										}
									}
									else
									{
										if ((((int)(this.unionSwitch)) == 1005))
										{
											if ((null != this.ServerInfo1005))
											{
												this.ServerInfo1005.value = decoder.ReadFixedStruct<SERVER_INFO_1005>(Titanis.DceRpc.NdrAlignment.NativePtr);
												decoder.ReadStructDeferral<SERVER_INFO_1005>(ref this.ServerInfo1005.value);
											}
										}
										else
										{
											if ((((int)(this.unionSwitch)) == 1107))
											{
												if ((null != this.ServerInfo1107))
												{
													this.ServerInfo1107.value = decoder.ReadFixedStruct<SERVER_INFO_1107>(Titanis.DceRpc.NdrAlignment._4Byte);
													decoder.ReadStructDeferral<SERVER_INFO_1107>(ref this.ServerInfo1107.value);
												}
											}
											else
											{
												if ((((int)(this.unionSwitch)) == 1010))
												{
													if ((null != this.ServerInfo1010))
													{
														this.ServerInfo1010.value = decoder.ReadFixedStruct<SERVER_INFO_1010>(Titanis.DceRpc.NdrAlignment._4Byte);
														decoder.ReadStructDeferral<SERVER_INFO_1010>(ref this.ServerInfo1010.value);
													}
												}
												else
												{
													if ((((int)(this.unionSwitch)) == 1016))
													{
														if ((null != this.ServerInfo1016))
														{
															this.ServerInfo1016.value = decoder.ReadFixedStruct<SERVER_INFO_1016>(Titanis.DceRpc.NdrAlignment._4Byte);
															decoder.ReadStructDeferral<SERVER_INFO_1016>(ref this.ServerInfo1016.value);
														}
													}
													else
													{
														if ((((int)(this.unionSwitch)) == 1017))
														{
															if ((null != this.ServerInfo1017))
															{
																this.ServerInfo1017.value = decoder.ReadFixedStruct<SERVER_INFO_1017>(Titanis.DceRpc.NdrAlignment._4Byte);
																decoder.ReadStructDeferral<SERVER_INFO_1017>(ref this.ServerInfo1017.value);
															}
														}
														else
														{
															if ((((int)(this.unionSwitch)) == 1018))
															{
																if ((null != this.ServerInfo1018))
																{
																	this.ServerInfo1018.value = decoder.ReadFixedStruct<SERVER_INFO_1018>(Titanis.DceRpc.NdrAlignment._4Byte);
																	decoder.ReadStructDeferral<SERVER_INFO_1018>(ref this.ServerInfo1018.value);
																}
															}
															else
															{
																if ((((int)(this.unionSwitch)) == 1501))
																{
																	if ((null != this.ServerInfo1501))
																	{
																		this.ServerInfo1501.value = decoder.ReadFixedStruct<SERVER_INFO_1501>(Titanis.DceRpc.NdrAlignment._4Byte);
																		decoder.ReadStructDeferral<SERVER_INFO_1501>(ref this.ServerInfo1501.value);
																	}
																}
																else
																{
																	if ((((int)(this.unionSwitch)) == 1502))
																	{
																		if ((null != this.ServerInfo1502))
																		{
																			this.ServerInfo1502.value = decoder.ReadFixedStruct<SERVER_INFO_1502>(Titanis.DceRpc.NdrAlignment._4Byte);
																			decoder.ReadStructDeferral<SERVER_INFO_1502>(ref this.ServerInfo1502.value);
																		}
																	}
																	else
																	{
																		if ((((int)(this.unionSwitch)) == 1503))
																		{
																			if ((null != this.ServerInfo1503))
																			{
																				this.ServerInfo1503.value = decoder.ReadFixedStruct<SERVER_INFO_1503>(Titanis.DceRpc.NdrAlignment._4Byte);
																				decoder.ReadStructDeferral<SERVER_INFO_1503>(ref this.ServerInfo1503.value);
																			}
																		}
																		else
																		{
																			if ((((int)(this.unionSwitch)) == 1506))
																			{
																				if ((null != this.ServerInfo1506))
																				{
																					this.ServerInfo1506.value = decoder.ReadFixedStruct<SERVER_INFO_1506>(Titanis.DceRpc.NdrAlignment._4Byte);
																					decoder.ReadStructDeferral<SERVER_INFO_1506>(ref this.ServerInfo1506.value);
																				}
																			}
																			else
																			{
																				if ((((int)(this.unionSwitch)) == 1510))
																				{
																					if ((null != this.ServerInfo1510))
																					{
																						this.ServerInfo1510.value = decoder.ReadFixedStruct<SERVER_INFO_1510>(Titanis.DceRpc.NdrAlignment._4Byte);
																						decoder.ReadStructDeferral<SERVER_INFO_1510>(ref this.ServerInfo1510.value);
																					}
																				}
																				else
																				{
																					if ((((int)(this.unionSwitch)) == 1511))
																					{
																						if ((null != this.ServerInfo1511))
																						{
																							this.ServerInfo1511.value = decoder.ReadFixedStruct<SERVER_INFO_1511>(Titanis.DceRpc.NdrAlignment._4Byte);
																							decoder.ReadStructDeferral<SERVER_INFO_1511>(ref this.ServerInfo1511.value);
																						}
																					}
																					else
																					{
																						if ((((int)(this.unionSwitch)) == 1512))
																						{
																							if ((null != this.ServerInfo1512))
																							{
																								this.ServerInfo1512.value = decoder.ReadFixedStruct<SERVER_INFO_1512>(Titanis.DceRpc.NdrAlignment._4Byte);
																								decoder.ReadStructDeferral<SERVER_INFO_1512>(ref this.ServerInfo1512.value);
																							}
																						}
																						else
																						{
																							if ((((int)(this.unionSwitch)) == 1513))
																							{
																								if ((null != this.ServerInfo1513))
																								{
																									this.ServerInfo1513.value = decoder.ReadFixedStruct<SERVER_INFO_1513>(Titanis.DceRpc.NdrAlignment._4Byte);
																									decoder.ReadStructDeferral<SERVER_INFO_1513>(ref this.ServerInfo1513.value);
																								}
																							}
																							else
																							{
																								if ((((int)(this.unionSwitch)) == 1514))
																								{
																									if ((null != this.ServerInfo1514))
																									{
																										this.ServerInfo1514.value = decoder.ReadFixedStruct<SERVER_INFO_1514>(Titanis.DceRpc.NdrAlignment._4Byte);
																										decoder.ReadStructDeferral<SERVER_INFO_1514>(ref this.ServerInfo1514.value);
																									}
																								}
																								else
																								{
																									if ((((int)(this.unionSwitch)) == 1515))
																									{
																										if ((null != this.ServerInfo1515))
																										{
																											this.ServerInfo1515.value = decoder.ReadFixedStruct<SERVER_INFO_1515>(Titanis.DceRpc.NdrAlignment._4Byte);
																											decoder.ReadStructDeferral<SERVER_INFO_1515>(ref this.ServerInfo1515.value);
																										}
																									}
																									else
																									{
																										if ((((int)(this.unionSwitch)) == 1516))
																										{
																											if ((null != this.ServerInfo1516))
																											{
																												this.ServerInfo1516.value = decoder.ReadFixedStruct<SERVER_INFO_1516>(Titanis.DceRpc.NdrAlignment._4Byte);
																												decoder.ReadStructDeferral<SERVER_INFO_1516>(ref this.ServerInfo1516.value);
																											}
																										}
																										else
																										{
																											if ((((int)(this.unionSwitch)) == 1518))
																											{
																												if ((null != this.ServerInfo1518))
																												{
																													this.ServerInfo1518.value = decoder.ReadFixedStruct<SERVER_INFO_1518>(Titanis.DceRpc.NdrAlignment._4Byte);
																													decoder.ReadStructDeferral<SERVER_INFO_1518>(ref this.ServerInfo1518.value);
																												}
																											}
																											else
																											{
																												if ((((int)(this.unionSwitch)) == 1523))
																												{
																													if ((null != this.ServerInfo1523))
																													{
																														this.ServerInfo1523.value = decoder.ReadFixedStruct<SERVER_INFO_1523>(Titanis.DceRpc.NdrAlignment._4Byte);
																														decoder.ReadStructDeferral<SERVER_INFO_1523>(ref this.ServerInfo1523.value);
																													}
																												}
																												else
																												{
																													if ((((int)(this.unionSwitch)) == 1528))
																													{
																														if ((null != this.ServerInfo1528))
																														{
																															this.ServerInfo1528.value = decoder.ReadFixedStruct<SERVER_INFO_1528>(Titanis.DceRpc.NdrAlignment._4Byte);
																															decoder.ReadStructDeferral<SERVER_INFO_1528>(ref this.ServerInfo1528.value);
																														}
																													}
																													else
																													{
																														if ((((int)(this.unionSwitch)) == 1529))
																														{
																															if ((null != this.ServerInfo1529))
																															{
																																this.ServerInfo1529.value = decoder.ReadFixedStruct<SERVER_INFO_1529>(Titanis.DceRpc.NdrAlignment._4Byte);
																																decoder.ReadStructDeferral<SERVER_INFO_1529>(ref this.ServerInfo1529.value);
																															}
																														}
																														else
																														{
																															if ((((int)(this.unionSwitch)) == 1530))
																															{
																																if ((null != this.ServerInfo1530))
																																{
																																	this.ServerInfo1530.value = decoder.ReadFixedStruct<SERVER_INFO_1530>(Titanis.DceRpc.NdrAlignment._4Byte);
																																	decoder.ReadStructDeferral<SERVER_INFO_1530>(ref this.ServerInfo1530.value);
																																}
																															}
																															else
																															{
																																if ((((int)(this.unionSwitch)) == 1533))
																																{
																																	if ((null != this.ServerInfo1533))
																																	{
																																		this.ServerInfo1533.value = decoder.ReadFixedStruct<SERVER_INFO_1533>(Titanis.DceRpc.NdrAlignment._4Byte);
																																		decoder.ReadStructDeferral<SERVER_INFO_1533>(ref this.ServerInfo1533.value);
																																	}
																																}
																																else
																																{
																																	if ((((int)(this.unionSwitch)) == 1534))
																																	{
																																		if ((null != this.ServerInfo1534))
																																		{
																																			this.ServerInfo1534.value = decoder.ReadFixedStruct<SERVER_INFO_1534>(Titanis.DceRpc.NdrAlignment._4Byte);
																																			decoder.ReadStructDeferral<SERVER_INFO_1534>(ref this.ServerInfo1534.value);
																																		}
																																	}
																																	else
																																	{
																																		if ((((int)(this.unionSwitch)) == 1535))
																																		{
																																			if ((null != this.ServerInfo1535))
																																			{
																																				this.ServerInfo1535.value = decoder.ReadFixedStruct<SERVER_INFO_1535>(Titanis.DceRpc.NdrAlignment._4Byte);
																																				decoder.ReadStructDeferral<SERVER_INFO_1535>(ref this.ServerInfo1535.value);
																																			}
																																		}
																																		else
																																		{
																																			if ((((int)(this.unionSwitch)) == 1536))
																																			{
																																				if ((null != this.ServerInfo1536))
																																				{
																																					this.ServerInfo1536.value = decoder.ReadFixedStruct<SERVER_INFO_1536>(Titanis.DceRpc.NdrAlignment._4Byte);
																																					decoder.ReadStructDeferral<SERVER_INFO_1536>(ref this.ServerInfo1536.value);
																																				}
																																			}
																																			else
																																			{
																																				if ((((int)(this.unionSwitch)) == 1538))
																																				{
																																					if ((null != this.ServerInfo1538))
																																					{
																																						this.ServerInfo1538.value = decoder.ReadFixedStruct<SERVER_INFO_1538>(Titanis.DceRpc.NdrAlignment._4Byte);
																																						decoder.ReadStructDeferral<SERVER_INFO_1538>(ref this.ServerInfo1538.value);
																																					}
																																				}
																																				else
																																				{
																																					if ((((int)(this.unionSwitch)) == 1539))
																																					{
																																						if ((null != this.ServerInfo1539))
																																						{
																																							this.ServerInfo1539.value = decoder.ReadFixedStruct<SERVER_INFO_1539>(Titanis.DceRpc.NdrAlignment._4Byte);
																																							decoder.ReadStructDeferral<SERVER_INFO_1539>(ref this.ServerInfo1539.value);
																																						}
																																					}
																																					else
																																					{
																																						if ((((int)(this.unionSwitch)) == 1540))
																																						{
																																							if ((null != this.ServerInfo1540))
																																							{
																																								this.ServerInfo1540.value = decoder.ReadFixedStruct<SERVER_INFO_1540>(Titanis.DceRpc.NdrAlignment._4Byte);
																																								decoder.ReadStructDeferral<SERVER_INFO_1540>(ref this.ServerInfo1540.value);
																																							}
																																						}
																																						else
																																						{
																																							if ((((int)(this.unionSwitch)) == 1541))
																																							{
																																								if ((null != this.ServerInfo1541))
																																								{
																																									this.ServerInfo1541.value = decoder.ReadFixedStruct<SERVER_INFO_1541>(Titanis.DceRpc.NdrAlignment._4Byte);
																																									decoder.ReadStructDeferral<SERVER_INFO_1541>(ref this.ServerInfo1541.value);
																																								}
																																							}
																																							else
																																							{
																																								if ((((int)(this.unionSwitch)) == 1542))
																																								{
																																									if ((null != this.ServerInfo1542))
																																									{
																																										this.ServerInfo1542.value = decoder.ReadFixedStruct<SERVER_INFO_1542>(Titanis.DceRpc.NdrAlignment._4Byte);
																																										decoder.ReadStructDeferral<SERVER_INFO_1542>(ref this.ServerInfo1542.value);
																																									}
																																								}
																																								else
																																								{
																																									if ((((int)(this.unionSwitch)) == 1543))
																																									{
																																										if ((null != this.ServerInfo1543))
																																										{
																																											this.ServerInfo1543.value = decoder.ReadFixedStruct<SERVER_INFO_1543>(Titanis.DceRpc.NdrAlignment._4Byte);
																																											decoder.ReadStructDeferral<SERVER_INFO_1543>(ref this.ServerInfo1543.value);
																																										}
																																									}
																																									else
																																									{
																																										if ((((int)(this.unionSwitch)) == 1544))
																																										{
																																											if ((null != this.ServerInfo1544))
																																											{
																																												this.ServerInfo1544.value = decoder.ReadFixedStruct<SERVER_INFO_1544>(Titanis.DceRpc.NdrAlignment._4Byte);
																																												decoder.ReadStructDeferral<SERVER_INFO_1544>(ref this.ServerInfo1544.value);
																																											}
																																										}
																																										else
																																										{
																																											if ((((int)(this.unionSwitch)) == 1545))
																																											{
																																												if ((null != this.ServerInfo1545))
																																												{
																																													this.ServerInfo1545.value = decoder.ReadFixedStruct<SERVER_INFO_1545>(Titanis.DceRpc.NdrAlignment._4Byte);
																																													decoder.ReadStructDeferral<SERVER_INFO_1545>(ref this.ServerInfo1545.value);
																																												}
																																											}
																																											else
																																											{
																																												if ((((int)(this.unionSwitch)) == 1546))
																																												{
																																													if ((null != this.ServerInfo1546))
																																													{
																																														this.ServerInfo1546.value = decoder.ReadFixedStruct<SERVER_INFO_1546>(Titanis.DceRpc.NdrAlignment._4Byte);
																																														decoder.ReadStructDeferral<SERVER_INFO_1546>(ref this.ServerInfo1546.value);
																																													}
																																												}
																																												else
																																												{
																																													if ((((int)(this.unionSwitch)) == 1547))
																																													{
																																														if ((null != this.ServerInfo1547))
																																														{
																																															this.ServerInfo1547.value = decoder.ReadFixedStruct<SERVER_INFO_1547>(Titanis.DceRpc.NdrAlignment._4Byte);
																																															decoder.ReadStructDeferral<SERVER_INFO_1547>(ref this.ServerInfo1547.value);
																																														}
																																													}
																																													else
																																													{
																																														if ((((int)(this.unionSwitch)) == 1548))
																																														{
																																															if ((null != this.ServerInfo1548))
																																															{
																																																this.ServerInfo1548.value = decoder.ReadFixedStruct<SERVER_INFO_1548>(Titanis.DceRpc.NdrAlignment._4Byte);
																																																decoder.ReadStructDeferral<SERVER_INFO_1548>(ref this.ServerInfo1548.value);
																																															}
																																														}
																																														else
																																														{
																																															if ((((int)(this.unionSwitch)) == 1549))
																																															{
																																																if ((null != this.ServerInfo1549))
																																																{
																																																	this.ServerInfo1549.value = decoder.ReadFixedStruct<SERVER_INFO_1549>(Titanis.DceRpc.NdrAlignment._4Byte);
																																																	decoder.ReadStructDeferral<SERVER_INFO_1549>(ref this.ServerInfo1549.value);
																																																}
																																															}
																																															else
																																															{
																																																if ((((int)(this.unionSwitch)) == 1550))
																																																{
																																																	if ((null != this.ServerInfo1550))
																																																	{
																																																		this.ServerInfo1550.value = decoder.ReadFixedStruct<SERVER_INFO_1550>(Titanis.DceRpc.NdrAlignment._4Byte);
																																																		decoder.ReadStructDeferral<SERVER_INFO_1550>(ref this.ServerInfo1550.value);
																																																	}
																																																}
																																																else
																																																{
																																																	if ((((int)(this.unionSwitch)) == 1552))
																																																	{
																																																		if ((null != this.ServerInfo1552))
																																																		{
																																																			this.ServerInfo1552.value = decoder.ReadFixedStruct<SERVER_INFO_1552>(Titanis.DceRpc.NdrAlignment._4Byte);
																																																			decoder.ReadStructDeferral<SERVER_INFO_1552>(ref this.ServerInfo1552.value);
																																																		}
																																																	}
																																																	else
																																																	{
																																																		if ((((int)(this.unionSwitch)) == 1553))
																																																		{
																																																			if ((null != this.ServerInfo1553))
																																																			{
																																																				this.ServerInfo1553.value = decoder.ReadFixedStruct<SERVER_INFO_1553>(Titanis.DceRpc.NdrAlignment._4Byte);
																																																				decoder.ReadStructDeferral<SERVER_INFO_1553>(ref this.ServerInfo1553.value);
																																																			}
																																																		}
																																																		else
																																																		{
																																																			if ((((int)(this.unionSwitch)) == 1554))
																																																			{
																																																				if ((null != this.ServerInfo1554))
																																																				{
																																																					this.ServerInfo1554.value = decoder.ReadFixedStruct<SERVER_INFO_1554>(Titanis.DceRpc.NdrAlignment._4Byte);
																																																					decoder.ReadStructDeferral<SERVER_INFO_1554>(ref this.ServerInfo1554.value);
																																																				}
																																																			}
																																																			else
																																																			{
																																																				if ((((int)(this.unionSwitch)) == 1555))
																																																				{
																																																					if ((null != this.ServerInfo1555))
																																																					{
																																																						this.ServerInfo1555.value = decoder.ReadFixedStruct<SERVER_INFO_1555>(Titanis.DceRpc.NdrAlignment._4Byte);
																																																						decoder.ReadStructDeferral<SERVER_INFO_1555>(ref this.ServerInfo1555.value);
																																																					}
																																																				}
																																																				else
																																																				{
																																																					if ((((int)(this.unionSwitch)) == 1556))
																																																					{
																																																						if ((null != this.ServerInfo1556))
																																																						{
																																																							this.ServerInfo1556.value = decoder.ReadFixedStruct<SERVER_INFO_1556>(Titanis.DceRpc.NdrAlignment._4Byte);
																																																							decoder.ReadStructDeferral<SERVER_INFO_1556>(ref this.ServerInfo1556.value);
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
						}
					}
				}
			}
		}
		public RpcPointer<ms_dtyp.SERVER_INFO_100> ServerInfo100;
		public RpcPointer<ms_dtyp.SERVER_INFO_101> ServerInfo101;
		public RpcPointer<SERVER_INFO_102> ServerInfo102;
		public RpcPointer<SERVER_INFO_103> ServerInfo103;
		public RpcPointer<SERVER_INFO_502> ServerInfo502;
		public RpcPointer<SERVER_INFO_503> ServerInfo503;
		public RpcPointer<SERVER_INFO_599> ServerInfo599;
		public RpcPointer<SERVER_INFO_1005> ServerInfo1005;
		public RpcPointer<SERVER_INFO_1107> ServerInfo1107;
		public RpcPointer<SERVER_INFO_1010> ServerInfo1010;
		public RpcPointer<SERVER_INFO_1016> ServerInfo1016;
		public RpcPointer<SERVER_INFO_1017> ServerInfo1017;
		public RpcPointer<SERVER_INFO_1018> ServerInfo1018;
		public RpcPointer<SERVER_INFO_1501> ServerInfo1501;
		public RpcPointer<SERVER_INFO_1502> ServerInfo1502;
		public RpcPointer<SERVER_INFO_1503> ServerInfo1503;
		public RpcPointer<SERVER_INFO_1506> ServerInfo1506;
		public RpcPointer<SERVER_INFO_1510> ServerInfo1510;
		public RpcPointer<SERVER_INFO_1511> ServerInfo1511;
		public RpcPointer<SERVER_INFO_1512> ServerInfo1512;
		public RpcPointer<SERVER_INFO_1513> ServerInfo1513;
		public RpcPointer<SERVER_INFO_1514> ServerInfo1514;
		public RpcPointer<SERVER_INFO_1515> ServerInfo1515;
		public RpcPointer<SERVER_INFO_1516> ServerInfo1516;
		public RpcPointer<SERVER_INFO_1518> ServerInfo1518;
		public RpcPointer<SERVER_INFO_1523> ServerInfo1523;
		public RpcPointer<SERVER_INFO_1528> ServerInfo1528;
		public RpcPointer<SERVER_INFO_1529> ServerInfo1529;
		public RpcPointer<SERVER_INFO_1530> ServerInfo1530;
		public RpcPointer<SERVER_INFO_1533> ServerInfo1533;
		public RpcPointer<SERVER_INFO_1534> ServerInfo1534;
		public RpcPointer<SERVER_INFO_1535> ServerInfo1535;
		public RpcPointer<SERVER_INFO_1536> ServerInfo1536;
		public RpcPointer<SERVER_INFO_1538> ServerInfo1538;
		public RpcPointer<SERVER_INFO_1539> ServerInfo1539;
		public RpcPointer<SERVER_INFO_1540> ServerInfo1540;
		public RpcPointer<SERVER_INFO_1541> ServerInfo1541;
		public RpcPointer<SERVER_INFO_1542> ServerInfo1542;
		public RpcPointer<SERVER_INFO_1543> ServerInfo1543;
		public RpcPointer<SERVER_INFO_1544> ServerInfo1544;
		public RpcPointer<SERVER_INFO_1545> ServerInfo1545;
		public RpcPointer<SERVER_INFO_1546> ServerInfo1546;
		public RpcPointer<SERVER_INFO_1547> ServerInfo1547;
		public RpcPointer<SERVER_INFO_1548> ServerInfo1548;
		public RpcPointer<SERVER_INFO_1549> ServerInfo1549;
		public RpcPointer<SERVER_INFO_1550> ServerInfo1550;
		public RpcPointer<SERVER_INFO_1552> ServerInfo1552;
		public RpcPointer<SERVER_INFO_1553> ServerInfo1553;
		public RpcPointer<SERVER_INFO_1554> ServerInfo1554;
		public RpcPointer<SERVER_INFO_1555> ServerInfo1555;
		public RpcPointer<SERVER_INFO_1556> ServerInfo1556;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct DISK_INFO : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			for (int i = 0; (i < this.Disk.Count); i++
			)
			{
				char elem_0 = this.Disk.Item(i);
				encoder.WriteValue(elem_0);
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Disk = decoder.ReadArraySegmentHeader<char>(3);
			for (int i = 0; (i < this.Disk.Count); i++
			)
			{
				char elem_0 = this.Disk.Item(i);
				elem_0 = decoder.ReadWideChar();
				this.Disk.Item(i) = elem_0;
			}
		}
		public ArraySegment<char> Disk;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct DISK_ENUM_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<ArraySegment<DISK_INFO>>();
		}
		public uint EntriesRead;
		public RpcPointer<ArraySegment<DISK_INFO>> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value, true);
				for (int i = 0; (i < this.Buffer.value.Count); i++
				)
				{
					DISK_INFO elem_0 = this.Buffer.value.Item(i);
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._2Byte);
				}
				for (int i = 0; (i < this.Buffer.value.Count); i++
				)
				{
					DISK_INFO elem_0 = this.Buffer.value.Item(i);
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArraySegmentHeader<DISK_INFO>();
				for (int i = 0; (i < this.Buffer.value.Count); i++
				)
				{
					DISK_INFO elem_0 = this.Buffer.value.Item(i);
					elem_0 = decoder.ReadFixedStruct<DISK_INFO>(Titanis.DceRpc.NdrAlignment._2Byte);
					this.Buffer.value.Item(i) = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Count); i++
				)
				{
					DISK_INFO elem_0 = this.Buffer.value.Item(i);
					decoder.ReadStructDeferral<DISK_INFO>(ref elem_0);
					this.Buffer.value.Item(i) = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_TRANSPORT_INFO_0 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.svti0_numberofvcs);
			encoder.WritePointer(this.svti0_transportname);
			encoder.WritePointer(this.svti0_transportaddress);
			encoder.WriteValue(this.svti0_transportaddresslength);
			encoder.WritePointer(this.svti0_networkaddress);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.svti0_numberofvcs = decoder.ReadUInt32();
			this.svti0_transportname = decoder.ReadPointer<string>();
			this.svti0_transportaddress = decoder.ReadPointer<byte[]>();
			this.svti0_transportaddresslength = decoder.ReadUInt32();
			this.svti0_networkaddress = decoder.ReadPointer<string>();
		}
		public uint svti0_numberofvcs;
		public RpcPointer<string> svti0_transportname;
		public RpcPointer<byte[]> svti0_transportaddress;
		public uint svti0_transportaddresslength;
		public RpcPointer<string> svti0_networkaddress;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.svti0_transportname))
			{
				encoder.WriteWideCharString(this.svti0_transportname.value);
			}
			if ((null != this.svti0_transportaddress))
			{
				encoder.WriteArrayHeader(this.svti0_transportaddress.value);
				for (int i = 0; (i < this.svti0_transportaddress.value.Length); i++
				)
				{
					byte elem_0 = this.svti0_transportaddress.value[i];
					encoder.WriteValue(elem_0);
				}
			}
			if ((null != this.svti0_networkaddress))
			{
				encoder.WriteWideCharString(this.svti0_networkaddress.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.svti0_transportname))
			{
				this.svti0_transportname.value = decoder.ReadWideCharString();
			}
			if ((null != this.svti0_transportaddress))
			{
				this.svti0_transportaddress.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; (i < this.svti0_transportaddress.value.Length); i++
				)
				{
					byte elem_0 = this.svti0_transportaddress.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.svti0_transportaddress.value[i] = elem_0;
				}
			}
			if ((null != this.svti0_networkaddress))
			{
				this.svti0_networkaddress.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_XPORT_INFO_0_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SERVER_TRANSPORT_INFO_0[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SERVER_TRANSPORT_INFO_0[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SERVER_TRANSPORT_INFO_0>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_0>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_TRANSPORT_INFO_1 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.svti1_numberofvcs);
			encoder.WritePointer(this.svti1_transportname);
			encoder.WritePointer(this.svti1_transportaddress);
			encoder.WriteValue(this.svti1_transportaddresslength);
			encoder.WritePointer(this.svti1_networkaddress);
			encoder.WritePointer(this.svti1_domain);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.svti1_numberofvcs = decoder.ReadUInt32();
			this.svti1_transportname = decoder.ReadPointer<string>();
			this.svti1_transportaddress = decoder.ReadPointer<byte[]>();
			this.svti1_transportaddresslength = decoder.ReadUInt32();
			this.svti1_networkaddress = decoder.ReadPointer<string>();
			this.svti1_domain = decoder.ReadPointer<string>();
		}
		public uint svti1_numberofvcs;
		public RpcPointer<string> svti1_transportname;
		public RpcPointer<byte[]> svti1_transportaddress;
		public uint svti1_transportaddresslength;
		public RpcPointer<string> svti1_networkaddress;
		public RpcPointer<string> svti1_domain;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.svti1_transportname))
			{
				encoder.WriteWideCharString(this.svti1_transportname.value);
			}
			if ((null != this.svti1_transportaddress))
			{
				encoder.WriteArrayHeader(this.svti1_transportaddress.value);
				for (int i = 0; (i < this.svti1_transportaddress.value.Length); i++
				)
				{
					byte elem_0 = this.svti1_transportaddress.value[i];
					encoder.WriteValue(elem_0);
				}
			}
			if ((null != this.svti1_networkaddress))
			{
				encoder.WriteWideCharString(this.svti1_networkaddress.value);
			}
			if ((null != this.svti1_domain))
			{
				encoder.WriteWideCharString(this.svti1_domain.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.svti1_transportname))
			{
				this.svti1_transportname.value = decoder.ReadWideCharString();
			}
			if ((null != this.svti1_transportaddress))
			{
				this.svti1_transportaddress.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; (i < this.svti1_transportaddress.value.Length); i++
				)
				{
					byte elem_0 = this.svti1_transportaddress.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.svti1_transportaddress.value[i] = elem_0;
				}
			}
			if ((null != this.svti1_networkaddress))
			{
				this.svti1_networkaddress.value = decoder.ReadWideCharString();
			}
			if ((null != this.svti1_domain))
			{
				this.svti1_domain.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_XPORT_INFO_1_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SERVER_TRANSPORT_INFO_1[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SERVER_TRANSPORT_INFO_1[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SERVER_TRANSPORT_INFO_1>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_1 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_1>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_1 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_1>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_TRANSPORT_INFO_2 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.svti2_numberofvcs);
			encoder.WritePointer(this.svti2_transportname);
			encoder.WritePointer(this.svti2_transportaddress);
			encoder.WriteValue(this.svti2_transportaddresslength);
			encoder.WritePointer(this.svti2_networkaddress);
			encoder.WritePointer(this.svti2_domain);
			encoder.WriteValue(this.svti2_flags);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.svti2_numberofvcs = decoder.ReadUInt32();
			this.svti2_transportname = decoder.ReadPointer<string>();
			this.svti2_transportaddress = decoder.ReadPointer<byte[]>();
			this.svti2_transportaddresslength = decoder.ReadUInt32();
			this.svti2_networkaddress = decoder.ReadPointer<string>();
			this.svti2_domain = decoder.ReadPointer<string>();
			this.svti2_flags = decoder.ReadUInt32();
		}
		public uint svti2_numberofvcs;
		public RpcPointer<string> svti2_transportname;
		public RpcPointer<byte[]> svti2_transportaddress;
		public uint svti2_transportaddresslength;
		public RpcPointer<string> svti2_networkaddress;
		public RpcPointer<string> svti2_domain;
		public uint svti2_flags;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.svti2_transportname))
			{
				encoder.WriteWideCharString(this.svti2_transportname.value);
			}
			if ((null != this.svti2_transportaddress))
			{
				encoder.WriteArrayHeader(this.svti2_transportaddress.value);
				for (int i = 0; (i < this.svti2_transportaddress.value.Length); i++
				)
				{
					byte elem_0 = this.svti2_transportaddress.value[i];
					encoder.WriteValue(elem_0);
				}
			}
			if ((null != this.svti2_networkaddress))
			{
				encoder.WriteWideCharString(this.svti2_networkaddress.value);
			}
			if ((null != this.svti2_domain))
			{
				encoder.WriteWideCharString(this.svti2_domain.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.svti2_transportname))
			{
				this.svti2_transportname.value = decoder.ReadWideCharString();
			}
			if ((null != this.svti2_transportaddress))
			{
				this.svti2_transportaddress.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; (i < this.svti2_transportaddress.value.Length); i++
				)
				{
					byte elem_0 = this.svti2_transportaddress.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.svti2_transportaddress.value[i] = elem_0;
				}
			}
			if ((null != this.svti2_networkaddress))
			{
				this.svti2_networkaddress.value = decoder.ReadWideCharString();
			}
			if ((null != this.svti2_domain))
			{
				this.svti2_domain.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_XPORT_INFO_2_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SERVER_TRANSPORT_INFO_2[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SERVER_TRANSPORT_INFO_2[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SERVER_TRANSPORT_INFO_2>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_2 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_2>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_2 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_2>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_TRANSPORT_INFO_3 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.svti3_numberofvcs);
			encoder.WritePointer(this.svti3_transportname);
			encoder.WritePointer(this.svti3_transportaddress);
			encoder.WriteValue(this.svti3_transportaddresslength);
			encoder.WritePointer(this.svti3_networkaddress);
			encoder.WritePointer(this.svti3_domain);
			encoder.WriteValue(this.svti3_flags);
			encoder.WriteValue(this.svti3_passwordlength);
			if ((this.svti3_password == null))
			{
				this.svti3_password = new byte[256];
			}
			for (int i = 0; (i < 256); i++
			)
			{
				byte elem_0 = this.svti3_password[i];
				encoder.WriteValue(elem_0);
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.svti3_numberofvcs = decoder.ReadUInt32();
			this.svti3_transportname = decoder.ReadPointer<string>();
			this.svti3_transportaddress = decoder.ReadPointer<byte[]>();
			this.svti3_transportaddresslength = decoder.ReadUInt32();
			this.svti3_networkaddress = decoder.ReadPointer<string>();
			this.svti3_domain = decoder.ReadPointer<string>();
			this.svti3_flags = decoder.ReadUInt32();
			this.svti3_passwordlength = decoder.ReadUInt32();
			if ((this.svti3_password == null))
			{
				this.svti3_password = new byte[256];
			}
			for (int i = 0; (i < 256); i++
			)
			{
				byte elem_0 = this.svti3_password[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.svti3_password[i] = elem_0;
			}
		}
		public uint svti3_numberofvcs;
		public RpcPointer<string> svti3_transportname;
		public RpcPointer<byte[]> svti3_transportaddress;
		public uint svti3_transportaddresslength;
		public RpcPointer<string> svti3_networkaddress;
		public RpcPointer<string> svti3_domain;
		public uint svti3_flags;
		public uint svti3_passwordlength;
		public byte[] svti3_password;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.svti3_transportname))
			{
				encoder.WriteWideCharString(this.svti3_transportname.value);
			}
			if ((null != this.svti3_transportaddress))
			{
				encoder.WriteArrayHeader(this.svti3_transportaddress.value);
				for (int i = 0; (i < this.svti3_transportaddress.value.Length); i++
				)
				{
					byte elem_0 = this.svti3_transportaddress.value[i];
					encoder.WriteValue(elem_0);
				}
			}
			if ((null != this.svti3_networkaddress))
			{
				encoder.WriteWideCharString(this.svti3_networkaddress.value);
			}
			if ((null != this.svti3_domain))
			{
				encoder.WriteWideCharString(this.svti3_domain.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.svti3_transportname))
			{
				this.svti3_transportname.value = decoder.ReadWideCharString();
			}
			if ((null != this.svti3_transportaddress))
			{
				this.svti3_transportaddress.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; (i < this.svti3_transportaddress.value.Length); i++
				)
				{
					byte elem_0 = this.svti3_transportaddress.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.svti3_transportaddress.value[i] = elem_0;
				}
			}
			if ((null != this.svti3_networkaddress))
			{
				this.svti3_networkaddress.value = decoder.ReadWideCharString();
			}
			if ((null != this.svti3_domain))
			{
				this.svti3_domain.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_XPORT_INFO_3_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SERVER_TRANSPORT_INFO_3[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SERVER_TRANSPORT_INFO_3[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_3 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_3 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SERVER_TRANSPORT_INFO_3>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_3 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_3>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_TRANSPORT_INFO_3 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_3>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct TRANSPORT_INFO : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint unionSwitch;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.unionSwitch);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.unionSwitch)) == 0))
			{
				encoder.WriteFixedStruct(this.Transport0, Titanis.DceRpc.NdrAlignment.NativePtr);
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 1))
				{
					encoder.WriteFixedStruct(this.Transport1, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 2))
					{
						encoder.WriteFixedStruct(this.Transport2, Titanis.DceRpc.NdrAlignment.NativePtr);
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 3))
						{
							encoder.WriteFixedStruct(this.Transport3, Titanis.DceRpc.NdrAlignment.NativePtr);
						}
					}
				}
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.unionSwitch = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.unionSwitch)) == 0))
			{
				this.Transport0 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_0>(Titanis.DceRpc.NdrAlignment.NativePtr);
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 1))
				{
					this.Transport1 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_1>(Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 2))
					{
						this.Transport2 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_2>(Titanis.DceRpc.NdrAlignment.NativePtr);
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 3))
						{
							this.Transport3 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_3>(Titanis.DceRpc.NdrAlignment.NativePtr);
						}
					}
				}
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.unionSwitch)) == 0))
			{
				encoder.WriteStructDeferral(this.Transport0);
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 1))
				{
					encoder.WriteStructDeferral(this.Transport1);
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 2))
					{
						encoder.WriteStructDeferral(this.Transport2);
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 3))
						{
							encoder.WriteStructDeferral(this.Transport3);
						}
					}
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.unionSwitch)) == 0))
			{
				decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_0>(ref this.Transport0);
			}
			else
			{
				if ((((int)(this.unionSwitch)) == 1))
				{
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_1>(ref this.Transport1);
				}
				else
				{
					if ((((int)(this.unionSwitch)) == 2))
					{
						decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_2>(ref this.Transport2);
					}
					else
					{
						if ((((int)(this.unionSwitch)) == 3))
						{
							decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_3>(ref this.Transport3);
						}
					}
				}
			}
		}
		public SERVER_TRANSPORT_INFO_0 Transport0;
		public SERVER_TRANSPORT_INFO_1 Transport1;
		public SERVER_TRANSPORT_INFO_2 Transport2;
		public SERVER_TRANSPORT_INFO_3 Transport3;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_XPORT_ENUM_UNION : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint Level;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 0))
			{
				encoder.WritePointer(this.Level0);
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					encoder.WritePointer(this.Level1);
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						encoder.WritePointer(this.Level2);
					}
					else
					{
						if ((((int)(this.Level)) == 3))
						{
							encoder.WritePointer(this.Level3);
						}
					}
				}
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 0))
			{
				this.Level0 = decoder.ReadPointer<SERVER_XPORT_INFO_0_CONTAINER>();
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					this.Level1 = decoder.ReadPointer<SERVER_XPORT_INFO_1_CONTAINER>();
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						this.Level2 = decoder.ReadPointer<SERVER_XPORT_INFO_2_CONTAINER>();
					}
					else
					{
						if ((((int)(this.Level)) == 3))
						{
							this.Level3 = decoder.ReadPointer<SERVER_XPORT_INFO_3_CONTAINER>();
						}
					}
				}
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.Level)) == 0))
			{
				if ((null != this.Level0))
				{
					encoder.WriteFixedStruct(this.Level0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(this.Level0.value);
				}
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					if ((null != this.Level1))
					{
						encoder.WriteFixedStruct(this.Level1.value, Titanis.DceRpc.NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level1.value);
					}
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						if ((null != this.Level2))
						{
							encoder.WriteFixedStruct(this.Level2.value, Titanis.DceRpc.NdrAlignment.NativePtr);
							encoder.WriteStructDeferral(this.Level2.value);
						}
					}
					else
					{
						if ((((int)(this.Level)) == 3))
						{
							if ((null != this.Level3))
							{
								encoder.WriteFixedStruct(this.Level3.value, Titanis.DceRpc.NdrAlignment.NativePtr);
								encoder.WriteStructDeferral(this.Level3.value);
							}
						}
					}
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.Level)) == 0))
			{
				if ((null != this.Level0))
				{
					this.Level0.value = decoder.ReadFixedStruct<SERVER_XPORT_INFO_0_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<SERVER_XPORT_INFO_0_CONTAINER>(ref this.Level0.value);
				}
			}
			else
			{
				if ((((int)(this.Level)) == 1))
				{
					if ((null != this.Level1))
					{
						this.Level1.value = decoder.ReadFixedStruct<SERVER_XPORT_INFO_1_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_XPORT_INFO_1_CONTAINER>(ref this.Level1.value);
					}
				}
				else
				{
					if ((((int)(this.Level)) == 2))
					{
						if ((null != this.Level2))
						{
							this.Level2.value = decoder.ReadFixedStruct<SERVER_XPORT_INFO_2_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
							decoder.ReadStructDeferral<SERVER_XPORT_INFO_2_CONTAINER>(ref this.Level2.value);
						}
					}
					else
					{
						if ((((int)(this.Level)) == 3))
						{
							if ((null != this.Level3))
							{
								this.Level3.value = decoder.ReadFixedStruct<SERVER_XPORT_INFO_3_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
								decoder.ReadStructDeferral<SERVER_XPORT_INFO_3_CONTAINER>(ref this.Level3.value);
							}
						}
					}
				}
			}
		}
		public RpcPointer<SERVER_XPORT_INFO_0_CONTAINER> Level0;
		public RpcPointer<SERVER_XPORT_INFO_1_CONTAINER> Level1;
		public RpcPointer<SERVER_XPORT_INFO_2_CONTAINER> Level2;
		public RpcPointer<SERVER_XPORT_INFO_3_CONTAINER> Level3;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_XPORT_ENUM_STRUCT : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.XportInfo);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.XportInfo = decoder.ReadUnion<SERVER_XPORT_ENUM_UNION>();
		}
		public uint Level;
		public SERVER_XPORT_ENUM_UNION XportInfo;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.XportInfo);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SERVER_XPORT_ENUM_UNION>(ref this.XportInfo);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct ADT_SECURITY_DESCRIPTOR : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<byte[]>();
		}
		public uint Length;
		public RpcPointer<byte[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					byte elem_0 = this.Buffer.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					byte elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct STAT_SERVER_0 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sts0_start);
			encoder.WriteValue(this.sts0_fopens);
			encoder.WriteValue(this.sts0_devopens);
			encoder.WriteValue(this.sts0_jobsqueued);
			encoder.WriteValue(this.sts0_sopens);
			encoder.WriteValue(this.sts0_stimedout);
			encoder.WriteValue(this.sts0_serrorout);
			encoder.WriteValue(this.sts0_pwerrors);
			encoder.WriteValue(this.sts0_permerrors);
			encoder.WriteValue(this.sts0_syserrors);
			encoder.WriteValue(this.sts0_bytessent_low);
			encoder.WriteValue(this.sts0_bytessent_high);
			encoder.WriteValue(this.sts0_bytesrcvd_low);
			encoder.WriteValue(this.sts0_bytesrcvd_high);
			encoder.WriteValue(this.sts0_avresponse);
			encoder.WriteValue(this.sts0_reqbufneed);
			encoder.WriteValue(this.sts0_bigbufneed);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.sts0_start = decoder.ReadUInt32();
			this.sts0_fopens = decoder.ReadUInt32();
			this.sts0_devopens = decoder.ReadUInt32();
			this.sts0_jobsqueued = decoder.ReadUInt32();
			this.sts0_sopens = decoder.ReadUInt32();
			this.sts0_stimedout = decoder.ReadUInt32();
			this.sts0_serrorout = decoder.ReadUInt32();
			this.sts0_pwerrors = decoder.ReadUInt32();
			this.sts0_permerrors = decoder.ReadUInt32();
			this.sts0_syserrors = decoder.ReadUInt32();
			this.sts0_bytessent_low = decoder.ReadUInt32();
			this.sts0_bytessent_high = decoder.ReadUInt32();
			this.sts0_bytesrcvd_low = decoder.ReadUInt32();
			this.sts0_bytesrcvd_high = decoder.ReadUInt32();
			this.sts0_avresponse = decoder.ReadUInt32();
			this.sts0_reqbufneed = decoder.ReadUInt32();
			this.sts0_bigbufneed = decoder.ReadUInt32();
		}
		public uint sts0_start;
		public uint sts0_fopens;
		public uint sts0_devopens;
		public uint sts0_jobsqueued;
		public uint sts0_sopens;
		public uint sts0_stimedout;
		public uint sts0_serrorout;
		public uint sts0_pwerrors;
		public uint sts0_permerrors;
		public uint sts0_syserrors;
		public uint sts0_bytessent_low;
		public uint sts0_bytessent_high;
		public uint sts0_bytesrcvd_low;
		public uint sts0_bytesrcvd_high;
		public uint sts0_avresponse;
		public uint sts0_reqbufneed;
		public uint sts0_bigbufneed;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct TIME_OF_DAY_INFO : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.tod_elapsedt);
			encoder.WriteValue(this.tod_msecs);
			encoder.WriteValue(this.tod_hours);
			encoder.WriteValue(this.tod_mins);
			encoder.WriteValue(this.tod_secs);
			encoder.WriteValue(this.tod_hunds);
			encoder.WriteValue(this.tod_timezone);
			encoder.WriteValue(this.tod_tinterval);
			encoder.WriteValue(this.tod_day);
			encoder.WriteValue(this.tod_month);
			encoder.WriteValue(this.tod_year);
			encoder.WriteValue(this.tod_weekday);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.tod_elapsedt = decoder.ReadUInt32();
			this.tod_msecs = decoder.ReadUInt32();
			this.tod_hours = decoder.ReadUInt32();
			this.tod_mins = decoder.ReadUInt32();
			this.tod_secs = decoder.ReadUInt32();
			this.tod_hunds = decoder.ReadUInt32();
			this.tod_timezone = decoder.ReadInt32();
			this.tod_tinterval = decoder.ReadUInt32();
			this.tod_day = decoder.ReadUInt32();
			this.tod_month = decoder.ReadUInt32();
			this.tod_year = decoder.ReadUInt32();
			this.tod_weekday = decoder.ReadUInt32();
		}
		public uint tod_elapsedt;
		public uint tod_msecs;
		public uint tod_hours;
		public uint tod_mins;
		public uint tod_secs;
		public uint tod_hunds;
		public int tod_timezone;
		public uint tod_tinterval;
		public uint tod_day;
		public uint tod_month;
		public uint tod_year;
		public uint tod_weekday;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct NET_DFS_ENTRY_ID : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Uid);
			encoder.WritePointer(this.Prefix);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Uid = decoder.ReadUuid();
			this.Prefix = decoder.ReadPointer<string>();
		}
		public System.Guid Uid;
		public RpcPointer<string> Prefix;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Prefix))
			{
				encoder.WriteWideCharString(this.Prefix.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Prefix))
			{
				this.Prefix.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct NET_DFS_ENTRY_ID_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Count);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Count = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<NET_DFS_ENTRY_ID[]>();
		}
		public uint Count;
		public RpcPointer<NET_DFS_ENTRY_ID[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					NET_DFS_ENTRY_ID elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					NET_DFS_ENTRY_ID elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<NET_DFS_ENTRY_ID>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					NET_DFS_ENTRY_ID elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<NET_DFS_ENTRY_ID>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					NET_DFS_ENTRY_ID elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<NET_DFS_ENTRY_ID>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct DFS_SITENAME_INFO : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.SiteFlags);
			encoder.WritePointer(this.SiteName);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.SiteFlags = decoder.ReadUInt32();
			this.SiteName = decoder.ReadPointer<string>();
		}
		public uint SiteFlags;
		public RpcPointer<string> SiteName;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.SiteName))
			{
				encoder.WriteWideCharString(this.SiteName.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.SiteName))
			{
				this.SiteName.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct DFS_SITELIST_INFO : Titanis.DceRpc.IRpcConformantStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cSites);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.cSites = decoder.ReadUInt32();
		}
		public void EncodeHeader(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.Site);
		}
		public void DecodeHeader(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Site = decoder.ReadArrayHeader<DFS_SITENAME_INFO>();
		}
		public void EncodeConformantArrayField(Titanis.DceRpc.IRpcEncoder encoder)
		{
			for (int i = 0; (i < this.Site.Length); i++
			)
			{
				DFS_SITENAME_INFO elem_0 = this.Site[i];
				encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
			}
		}
		public void DecodeConformantArrayField(Titanis.DceRpc.IRpcDecoder decoder)
		{
			for (int i = 0; (i < this.Site.Length); i++
			)
			{
				DFS_SITENAME_INFO elem_0 = this.Site[i];
				elem_0 = decoder.ReadFixedStruct<DFS_SITENAME_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
				this.Site[i] = elem_0;
			}
		}
		public uint cSites;
		public DFS_SITENAME_INFO[] Site;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			for (int i = 0; (i < this.Site.Length); i++
			)
			{
				DFS_SITENAME_INFO elem_0 = this.Site[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			for (int i = 0; (i < this.Site.Length); i++
			)
			{
				DFS_SITENAME_INFO elem_0 = this.Site[i];
				decoder.ReadStructDeferral<DFS_SITENAME_INFO>(ref elem_0);
				this.Site[i] = elem_0;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_ALIAS_INFO_0 : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.srvai0_alias);
			encoder.WritePointer(this.srvai0_target);
			encoder.WriteValue(this.srvai0_default);
			encoder.WriteValue(this.srvai0_reserved);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.srvai0_alias = decoder.ReadPointer<string>();
			this.srvai0_target = decoder.ReadPointer<string>();
			this.srvai0_default = decoder.ReadUnsignedChar();
			this.srvai0_reserved = decoder.ReadUInt32();
		}
		public RpcPointer<string> srvai0_alias;
		public RpcPointer<string> srvai0_target;
		public byte srvai0_default;
		public uint srvai0_reserved;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.srvai0_alias))
			{
				encoder.WriteWideCharString(this.srvai0_alias.value);
			}
			if ((null != this.srvai0_target))
			{
				encoder.WriteWideCharString(this.srvai0_target.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.srvai0_alias))
			{
				this.srvai0_alias.value = decoder.ReadWideCharString();
			}
			if ((null != this.srvai0_target))
			{
				this.srvai0_target.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_ALIAS_INFO_0_CONTAINER : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WritePointer(this.Buffer);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadPointer<SERVER_ALIAS_INFO_0[]>();
		}
		public uint EntriesRead;
		public RpcPointer<SERVER_ALIAS_INFO_0[]> Buffer;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.Buffer))
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_ALIAS_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_ALIAS_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.Buffer))
			{
				this.Buffer.value = decoder.ReadArrayHeader<SERVER_ALIAS_INFO_0>();
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_ALIAS_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVER_ALIAS_INFO_0>(Titanis.DceRpc.NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}
				for (int i = 0; (i < this.Buffer.value.Length); i++
				)
				{
					SERVER_ALIAS_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SERVER_ALIAS_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct _SERVER_ALIAS_ENUM_UNION : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint Level;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 0))
			{
				encoder.WritePointer(this.Level0);
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.Level)) == 0))
			{
				this.Level0 = decoder.ReadPointer<SERVER_ALIAS_INFO_0_CONTAINER>();
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.Level)) == 0))
			{
				if ((null != this.Level0))
				{
					encoder.WriteFixedStruct(this.Level0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(this.Level0.value);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.Level)) == 0))
			{
				if ((null != this.Level0))
				{
					this.Level0.value = decoder.ReadFixedStruct<SERVER_ALIAS_INFO_0_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<SERVER_ALIAS_INFO_0_CONTAINER>(ref this.Level0.value);
				}
			}
		}
		public RpcPointer<SERVER_ALIAS_INFO_0_CONTAINER> Level0;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_ALIAS_ENUM_STRUCT : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.ServerAliasInfo);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.ServerAliasInfo = decoder.ReadUnion<_SERVER_ALIAS_ENUM_UNION>();
		}
		public uint Level;
		public _SERVER_ALIAS_ENUM_UNION ServerAliasInfo;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ServerAliasInfo);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<_SERVER_ALIAS_ENUM_UNION>(ref this.ServerAliasInfo);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public struct SERVER_ALIAS_INFO : Titanis.DceRpc.IRpcFixedStruct
	{
		public uint unionSwitch;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.unionSwitch);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.unionSwitch)) == 0))
			{
				encoder.WritePointer(this.ServerAliasInfo0);
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.unionSwitch = decoder.ReadUInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.unionSwitch)) == 0))
			{
				this.ServerAliasInfo0 = decoder.ReadPointer<SERVER_ALIAS_INFO_0>();
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.unionSwitch)) == 0))
			{
				if ((null != this.ServerAliasInfo0))
				{
					encoder.WriteFixedStruct(this.ServerAliasInfo0.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(this.ServerAliasInfo0.value);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.unionSwitch)) == 0))
			{
				if ((null != this.ServerAliasInfo0))
				{
					this.ServerAliasInfo0.value = decoder.ReadFixedStruct<SERVER_ALIAS_INFO_0>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<SERVER_ALIAS_INFO_0>(ref this.ServerAliasInfo0.value);
				}
			}
		}
		public RpcPointer<SERVER_ALIAS_INFO_0> ServerAliasInfo0;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	[System.Runtime.InteropServices.GuidAttribute("4b324fc8-1670-01d3-1278-5a47bf6ee188")]
	[Titanis.DceRpc.RpcVersionAttribute(3, 0)]
	public interface srvsvc
	{
		Task Opnum0NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task Opnum1NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task Opnum2NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task Opnum3NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task Opnum4NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task Opnum5NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task Opnum6NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task Opnum7NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrConnectionEnum(string ServerName, string Qualifier, RpcPointer<CONNECT_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrFileEnum(string ServerName, string BasePath, string UserName, RpcPointer<FILE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrFileGetInfo(string ServerName, uint FileId, uint Level, RpcPointer<FILE_INFO> InfoStruct, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrFileClose(string ServerName, uint FileId, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrSessionEnum(string ServerName, string ClientName, string UserName, RpcPointer<SESSION_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrSessionDel(string ServerName, string ClientName, string UserName, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareAdd(string ServerName, uint Level, RpcPointer<SHARE_INFO> InfoStruct, RpcPointer<uint> ParmErr, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareEnum(string ServerName, RpcPointer<SHARE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareGetInfo(string ServerName, string NetName, uint Level, RpcPointer<SHARE_INFO> InfoStruct, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareSetInfo(string ServerName, string NetName, uint Level, RpcPointer<SHARE_INFO> ShareInfo, RpcPointer<uint> ParmErr, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareDel(string ServerName, string NetName, uint Reserved, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareDelSticky(string ServerName, string NetName, uint Reserved, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareCheck(string ServerName, string Device, RpcPointer<uint> Type, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerGetInfo(string ServerName, uint Level, RpcPointer<SERVER_INFO> InfoStruct, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerSetInfo(string ServerName, uint Level, RpcPointer<SERVER_INFO> ServerInfo, RpcPointer<uint> ParmErr, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerDiskEnum(string ServerName, uint Level, RpcPointer<DISK_ENUM_CONTAINER> DiskInfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerStatisticsGet(string ServerName, string Service, uint Level, uint Options, RpcPointer<RpcPointer<STAT_SERVER_0>> InfoStruct, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerTransportAdd(string ServerName, uint Level, RpcPointer<SERVER_TRANSPORT_INFO_0> Buffer, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerTransportEnum(string ServerName, RpcPointer<SERVER_XPORT_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerTransportDel(string ServerName, uint Level, RpcPointer<SERVER_TRANSPORT_INFO_0> Buffer, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrRemoteTOD(string ServerName, RpcPointer<RpcPointer<TIME_OF_DAY_INFO>> BufferPtr, System.Threading.CancellationToken cancellationToken);
		Task Opnum29NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task<uint> NetprPathType(string ServerName, string PathName, RpcPointer<uint> PathType, uint Flags, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetprPathCanonicalize(string ServerName, string PathName, RpcPointer<byte[]> Outbuf, uint OutbufLen, string Prefix, RpcPointer<uint> PathType, uint Flags, System.Threading.CancellationToken cancellationToken);
		Task<int> NetprPathCompare(string ServerName, string PathName1, string PathName2, uint PathType, uint Flags, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetprNameValidate(string ServerName, string Name, uint NameType, uint Flags, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetprNameCanonicalize(string ServerName, string Name, RpcPointer<char[]> Outbuf, uint OutbufLen, uint NameType, uint Flags, System.Threading.CancellationToken cancellationToken);
		Task<int> NetprNameCompare(string ServerName, string Name1, string Name2, uint NameType, uint Flags, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareEnumSticky(string ServerName, RpcPointer<SHARE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareDelStart(string ServerName, string NetName, uint Reserved, RpcPointer<Titanis.DceRpc.RpcContextHandle> ContextHandle, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareDelCommit(RpcPointer<Titanis.DceRpc.RpcContextHandle> ContextHandle, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrpGetFileSecurity(string ServerName, string ShareName, string lpFileName, uint RequestedInformation, RpcPointer<RpcPointer<ADT_SECURITY_DESCRIPTOR>> SecurityDescriptor, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrpSetFileSecurity(string ServerName, string ShareName, string lpFileName, uint SecurityInformation, RpcPointer<ADT_SECURITY_DESCRIPTOR> SecurityDescriptor, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerTransportAddEx(string ServerName, uint Level, RpcPointer<TRANSPORT_INFO> Buffer, System.Threading.CancellationToken cancellationToken);
		Task Opnum42NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrDfsGetVersion(string ServerName, RpcPointer<uint> Version, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrDfsCreateLocalPartition(string ServerName, string ShareName, RpcPointer<System.Guid> EntryUid, string EntryPrefix, string ShortName, RpcPointer<NET_DFS_ENTRY_ID_CONTAINER> RelationInfo, int Force, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrDfsDeleteLocalPartition(string ServerName, RpcPointer<System.Guid> Uid, string Prefix, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrDfsSetLocalVolumeState(string ServerName, RpcPointer<System.Guid> Uid, string Prefix, uint State, System.Threading.CancellationToken cancellationToken);
		Task Opnum47NotUsedOnWire(System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrDfsCreateExitPoint(string ServerName, RpcPointer<System.Guid> Uid, string Prefix, uint Type, uint ShortPrefixLen, RpcPointer<char[]> ShortPrefix, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrDfsDeleteExitPoint(string ServerName, RpcPointer<System.Guid> Uid, string Prefix, uint Type, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrDfsModifyPrefix(string ServerName, RpcPointer<System.Guid> Uid, string Prefix, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrDfsFixLocalVolume(string ServerName, string VolumeName, uint EntryType, uint ServiceType, string StgId, RpcPointer<System.Guid> EntryUid, string EntryPrefix, RpcPointer<NET_DFS_ENTRY_ID_CONTAINER> RelationInfo, uint CreateDisposition, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrDfsManagerReportSiteInfo(string ServerName, RpcPointer<RpcPointer<DFS_SITELIST_INFO>> ppSiteInfo, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerTransportDelEx(string ServerName, uint Level, RpcPointer<TRANSPORT_INFO> Buffer, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerAliasAdd(string ServerName, uint Level, RpcPointer<SERVER_ALIAS_INFO> InfoStruct, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerAliasEnum(string ServerName, RpcPointer<SERVER_ALIAS_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrServerAliasDel(string ServerName, uint Level, RpcPointer<SERVER_ALIAS_INFO> InfoStruct, System.Threading.CancellationToken cancellationToken);
		Task<uint> NetrShareDelEx(string ServerName, uint Level, RpcPointer<SHARE_INFO> ShareInfo, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	[Titanis.DceRpc.IidAttribute("4b324fc8-1670-01d3-1278-5a47bf6ee188")]
	public class srvsvcClientProxy : Titanis.DceRpc.Client.RpcClientProxy, srvsvc, Titanis.DceRpc.IRpcClientProxy
	{
		private static System.Guid _interfaceUuid = new System.Guid("4b324fc8-1670-01d3-1278-5a47bf6ee188");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(3, 0);
			}
		}
		public virtual async Task Opnum0NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task Opnum1NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task Opnum2NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task Opnum3NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task Opnum4NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task Opnum5NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task Opnum6NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task Opnum7NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task<uint> NetrConnectionEnum(string ServerName, string Qualifier, RpcPointer<CONNECT_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteUniqueReferentId((Qualifier == null));
			if ((Qualifier != null))
			{
				encoder.WriteWideCharString(Qualifier);
			}
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadFixedStruct<CONNECT_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<CONNECT_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutPointer<uint>(ResumeHandle);
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrFileEnum(string ServerName, string BasePath, string UserName, RpcPointer<FILE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteUniqueReferentId((BasePath == null));
			if ((BasePath != null))
			{
				encoder.WriteWideCharString(BasePath);
			}
			encoder.WriteUniqueReferentId((UserName == null));
			if ((UserName != null))
			{
				encoder.WriteWideCharString(UserName);
			}
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadFixedStruct<FILE_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FILE_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutPointer<uint>(ResumeHandle);
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrFileGetInfo(string ServerName, uint FileId, uint Level, RpcPointer<FILE_INFO> InfoStruct, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(FileId);
			encoder.WriteValue(Level);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadUnion<FILE_INFO>();
			decoder.ReadStructDeferral<FILE_INFO>(ref InfoStruct.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrFileClose(string ServerName, uint FileId, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(FileId);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrSessionEnum(string ServerName, string ClientName, string UserName, RpcPointer<SESSION_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteUniqueReferentId((ClientName == null));
			if ((ClientName != null))
			{
				encoder.WriteWideCharString(ClientName);
			}
			encoder.WriteUniqueReferentId((UserName == null));
			if ((UserName != null))
			{
				encoder.WriteWideCharString(UserName);
			}
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadFixedStruct<SESSION_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SESSION_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutPointer<uint>(ResumeHandle);
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrSessionDel(string ServerName, string ClientName, string UserName, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteUniqueReferentId((ClientName == null));
			if ((ClientName != null))
			{
				encoder.WriteWideCharString(ClientName);
			}
			encoder.WriteUniqueReferentId((UserName == null));
			if ((UserName != null))
			{
				encoder.WriteWideCharString(UserName);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareAdd(string ServerName, uint Level, RpcPointer<SHARE_INFO> InfoStruct, RpcPointer<uint> ParmErr, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			encoder.WriteUnion(InfoStruct.value);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WritePointer(ParmErr);
			if ((null != ParmErr))
			{
				encoder.WriteValue(ParmErr.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ParmErr = decoder.ReadOutPointer<uint>(ParmErr);
			if ((null != ParmErr))
			{
				ParmErr.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareEnum(string ServerName, RpcPointer<SHARE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadFixedStruct<SHARE_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SHARE_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutPointer<uint>(ResumeHandle);
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareGetInfo(string ServerName, string NetName, uint Level, RpcPointer<SHARE_INFO> InfoStruct, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(NetName);
			encoder.WriteValue(Level);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadUnion<SHARE_INFO>();
			decoder.ReadStructDeferral<SHARE_INFO>(ref InfoStruct.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareSetInfo(string ServerName, string NetName, uint Level, RpcPointer<SHARE_INFO> ShareInfo, RpcPointer<uint> ParmErr, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(NetName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(ShareInfo.value);
			encoder.WriteStructDeferral(ShareInfo.value);
			encoder.WritePointer(ParmErr);
			if ((null != ParmErr))
			{
				encoder.WriteValue(ParmErr.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ParmErr = decoder.ReadOutPointer<uint>(ParmErr);
			if ((null != ParmErr))
			{
				ParmErr.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareDel(string ServerName, string NetName, uint Reserved, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(NetName);
			encoder.WriteValue(Reserved);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareDelSticky(string ServerName, string NetName, uint Reserved, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(NetName);
			encoder.WriteValue(Reserved);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareCheck(string ServerName, string Device, RpcPointer<uint> Type, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(Device);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			Type.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerGetInfo(string ServerName, uint Level, RpcPointer<SERVER_INFO> InfoStruct, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadUnion<SERVER_INFO>();
			decoder.ReadStructDeferral<SERVER_INFO>(ref InfoStruct.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerSetInfo(string ServerName, uint Level, RpcPointer<SERVER_INFO> ServerInfo, RpcPointer<uint> ParmErr, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			encoder.WriteUnion(ServerInfo.value);
			encoder.WriteStructDeferral(ServerInfo.value);
			encoder.WritePointer(ParmErr);
			if ((null != ParmErr))
			{
				encoder.WriteValue(ParmErr.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ParmErr = decoder.ReadOutPointer<uint>(ParmErr);
			if ((null != ParmErr))
			{
				ParmErr.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerDiskEnum(string ServerName, uint Level, RpcPointer<DISK_ENUM_CONTAINER> DiskInfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			encoder.WriteFixedStruct(DiskInfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(DiskInfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			DiskInfoStruct.value = decoder.ReadFixedStruct<DISK_ENUM_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<DISK_ENUM_CONTAINER>(ref DiskInfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutPointer<uint>(ResumeHandle);
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerStatisticsGet(string ServerName, string Service, uint Level, uint Options, RpcPointer<RpcPointer<STAT_SERVER_0>> InfoStruct, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteUniqueReferentId((Service == null));
			if ((Service != null))
			{
				encoder.WriteWideCharString(Service);
			}
			encoder.WriteValue(Level);
			encoder.WriteValue(Options);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadOutPointer<STAT_SERVER_0>(InfoStruct.value);
			if ((null != InfoStruct.value))
			{
				InfoStruct.value.value = decoder.ReadFixedStruct<STAT_SERVER_0>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<STAT_SERVER_0>(ref InfoStruct.value.value);
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerTransportAdd(string ServerName, uint Level, RpcPointer<SERVER_TRANSPORT_INFO_0> Buffer, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			encoder.WriteFixedStruct(Buffer.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Buffer.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerTransportEnum(string ServerName, RpcPointer<SERVER_XPORT_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadFixedStruct<SERVER_XPORT_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_XPORT_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutPointer<uint>(ResumeHandle);
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerTransportDel(string ServerName, uint Level, RpcPointer<SERVER_TRANSPORT_INFO_0> Buffer, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			encoder.WriteFixedStruct(Buffer.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Buffer.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrRemoteTOD(string ServerName, RpcPointer<RpcPointer<TIME_OF_DAY_INFO>> BufferPtr, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			BufferPtr.value = decoder.ReadOutPointer<TIME_OF_DAY_INFO>(BufferPtr.value);
			if ((null != BufferPtr.value))
			{
				BufferPtr.value.value = decoder.ReadFixedStruct<TIME_OF_DAY_INFO>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<TIME_OF_DAY_INFO>(ref BufferPtr.value.value);
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task Opnum29NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task<uint> NetprPathType(string ServerName, string PathName, RpcPointer<uint> PathType, uint Flags, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(PathName);
			encoder.WriteValue(Flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			PathType.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetprPathCanonicalize(string ServerName, string PathName, RpcPointer<byte[]> Outbuf, uint OutbufLen, string Prefix, RpcPointer<uint> PathType, uint Flags, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(PathName);
			encoder.WriteValue(OutbufLen);
			encoder.WriteWideCharString(Prefix);
			encoder.WriteValue(PathType.value);
			encoder.WriteValue(Flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			Outbuf.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; (i < Outbuf.value.Length); i++
			)
			{
				byte elem_0 = Outbuf.value[i];
				elem_0 = decoder.ReadUnsignedChar();
				Outbuf.value[i] = elem_0;
			}
			PathType.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<int> NetprPathCompare(string ServerName, string PathName1, string PathName2, uint PathType, uint Flags, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(PathName1);
			encoder.WriteWideCharString(PathName2);
			encoder.WriteValue(PathType);
			encoder.WriteValue(Flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<uint> NetprNameValidate(string ServerName, string Name, uint NameType, uint Flags, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(Name);
			encoder.WriteValue(NameType);
			encoder.WriteValue(Flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetprNameCanonicalize(string ServerName, string Name, RpcPointer<char[]> Outbuf, uint OutbufLen, uint NameType, uint Flags, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(Name);
			encoder.WriteValue(OutbufLen);
			encoder.WriteValue(NameType);
			encoder.WriteValue(Flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			Outbuf.value = decoder.ReadArrayHeader<char>();
			for (int i = 0; (i < Outbuf.value.Length); i++
			)
			{
				char elem_0 = Outbuf.value[i];
				elem_0 = decoder.ReadWideChar();
				Outbuf.value[i] = elem_0;
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<int> NetprNameCompare(string ServerName, string Name1, string Name2, uint NameType, uint Flags, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(Name1);
			encoder.WriteWideCharString(Name2);
			encoder.WriteValue(NameType);
			encoder.WriteValue(Flags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareEnumSticky(string ServerName, RpcPointer<SHARE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadFixedStruct<SHARE_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SHARE_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutPointer<uint>(ResumeHandle);
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareDelStart(string ServerName, string NetName, uint Reserved, RpcPointer<Titanis.DceRpc.RpcContextHandle> ContextHandle, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(37);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(NetName);
			encoder.WriteValue(Reserved);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ContextHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareDelCommit(RpcPointer<Titanis.DceRpc.RpcContextHandle> ContextHandle, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(38);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ContextHandle.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ContextHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrpGetFileSecurity(string ServerName, string ShareName, string lpFileName, uint RequestedInformation, RpcPointer<RpcPointer<ADT_SECURITY_DESCRIPTOR>> SecurityDescriptor, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(39);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteUniqueReferentId((ShareName == null));
			if ((ShareName != null))
			{
				encoder.WriteWideCharString(ShareName);
			}
			encoder.WriteWideCharString(lpFileName);
			encoder.WriteValue(RequestedInformation);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			SecurityDescriptor.value = decoder.ReadOutPointer<ADT_SECURITY_DESCRIPTOR>(SecurityDescriptor.value);
			if ((null != SecurityDescriptor.value))
			{
				SecurityDescriptor.value.value = decoder.ReadFixedStruct<ADT_SECURITY_DESCRIPTOR>(Titanis.DceRpc.NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ADT_SECURITY_DESCRIPTOR>(ref SecurityDescriptor.value.value);
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrpSetFileSecurity(string ServerName, string ShareName, string lpFileName, uint SecurityInformation, RpcPointer<ADT_SECURITY_DESCRIPTOR> SecurityDescriptor, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(40);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteUniqueReferentId((ShareName == null));
			if ((ShareName != null))
			{
				encoder.WriteWideCharString(ShareName);
			}
			encoder.WriteWideCharString(lpFileName);
			encoder.WriteValue(SecurityInformation);
			encoder.WriteFixedStruct(SecurityDescriptor.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SecurityDescriptor.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerTransportAddEx(string ServerName, uint Level, RpcPointer<TRANSPORT_INFO> Buffer, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(41);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			encoder.WriteUnion(Buffer.value);
			encoder.WriteStructDeferral(Buffer.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task Opnum42NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(42);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task<uint> NetrDfsGetVersion(string ServerName, RpcPointer<uint> Version, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(43);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			Version.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrDfsCreateLocalPartition(string ServerName, string ShareName, RpcPointer<System.Guid> EntryUid, string EntryPrefix, string ShortName, RpcPointer<NET_DFS_ENTRY_ID_CONTAINER> RelationInfo, int Force, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(44);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(ShareName);
			encoder.WriteValue(EntryUid.value);
			encoder.WriteWideCharString(EntryPrefix);
			encoder.WriteWideCharString(ShortName);
			encoder.WriteFixedStruct(RelationInfo.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(RelationInfo.value);
			encoder.WriteValue(Force);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrDfsDeleteLocalPartition(string ServerName, RpcPointer<System.Guid> Uid, string Prefix, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(45);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Uid.value);
			encoder.WriteWideCharString(Prefix);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrDfsSetLocalVolumeState(string ServerName, RpcPointer<System.Guid> Uid, string Prefix, uint State, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(46);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Uid.value);
			encoder.WriteWideCharString(Prefix);
			encoder.WriteValue(State);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task Opnum47NotUsedOnWire(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(47);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
		}
		public virtual async Task<uint> NetrDfsCreateExitPoint(string ServerName, RpcPointer<System.Guid> Uid, string Prefix, uint Type, uint ShortPrefixLen, RpcPointer<char[]> ShortPrefix, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(48);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Uid.value);
			encoder.WriteWideCharString(Prefix);
			encoder.WriteValue(Type);
			encoder.WriteValue(ShortPrefixLen);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ShortPrefix.value = decoder.ReadArrayHeader<char>();
			for (int i = 0; (i < ShortPrefix.value.Length); i++
			)
			{
				char elem_0 = ShortPrefix.value[i];
				elem_0 = decoder.ReadWideChar();
				ShortPrefix.value[i] = elem_0;
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrDfsDeleteExitPoint(string ServerName, RpcPointer<System.Guid> Uid, string Prefix, uint Type, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(49);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Uid.value);
			encoder.WriteWideCharString(Prefix);
			encoder.WriteValue(Type);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrDfsModifyPrefix(string ServerName, RpcPointer<System.Guid> Uid, string Prefix, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(50);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Uid.value);
			encoder.WriteWideCharString(Prefix);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrDfsFixLocalVolume(string ServerName, string VolumeName, uint EntryType, uint ServiceType, string StgId, RpcPointer<System.Guid> EntryUid, string EntryPrefix, RpcPointer<NET_DFS_ENTRY_ID_CONTAINER> RelationInfo, uint CreateDisposition, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(51);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteWideCharString(VolumeName);
			encoder.WriteValue(EntryType);
			encoder.WriteValue(ServiceType);
			encoder.WriteWideCharString(StgId);
			encoder.WriteValue(EntryUid.value);
			encoder.WriteWideCharString(EntryPrefix);
			encoder.WriteFixedStruct(RelationInfo.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(RelationInfo.value);
			encoder.WriteValue(CreateDisposition);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrDfsManagerReportSiteInfo(string ServerName, RpcPointer<RpcPointer<DFS_SITELIST_INFO>> ppSiteInfo, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(52);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WritePointer(ppSiteInfo);
			if ((null != ppSiteInfo))
			{
				encoder.WritePointer(ppSiteInfo.value);
				if ((null != ppSiteInfo.value))
				{
					encoder.WriteConformantStruct(ppSiteInfo.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(ppSiteInfo.value.value);
				}
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppSiteInfo = decoder.ReadOutPointer<RpcPointer<DFS_SITELIST_INFO>>(ppSiteInfo);
			if ((null != ppSiteInfo))
			{
				ppSiteInfo.value = decoder.ReadPointer<DFS_SITELIST_INFO>();
				if ((null != ppSiteInfo.value))
				{
					ppSiteInfo.value.value = decoder.ReadConformantStruct<DFS_SITELIST_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<DFS_SITELIST_INFO>(ref ppSiteInfo.value.value);
				}
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerTransportDelEx(string ServerName, uint Level, RpcPointer<TRANSPORT_INFO> Buffer, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(53);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			encoder.WriteUnion(Buffer.value);
			encoder.WriteStructDeferral(Buffer.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerAliasAdd(string ServerName, uint Level, RpcPointer<SERVER_ALIAS_INFO> InfoStruct, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(54);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			encoder.WriteUnion(InfoStruct.value);
			encoder.WriteStructDeferral(InfoStruct.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerAliasEnum(string ServerName, RpcPointer<SERVER_ALIAS_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(55);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			InfoStruct.value = decoder.ReadFixedStruct<SERVER_ALIAS_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_ALIAS_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutPointer<uint>(ResumeHandle);
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrServerAliasDel(string ServerName, uint Level, RpcPointer<SERVER_ALIAS_INFO> InfoStruct, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(56);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			encoder.WriteUnion(InfoStruct.value);
			encoder.WriteStructDeferral(InfoStruct.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
		public virtual async Task<uint> NetrShareDelEx(string ServerName, uint Level, RpcPointer<SHARE_INFO> ShareInfo, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(57);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((ServerName == null));
			if ((ServerName != null))
			{
				encoder.WriteWideCharString(ServerName);
			}
			encoder.WriteValue(Level);
			encoder.WriteUnion(ShareInfo.value);
			encoder.WriteStructDeferral(ShareInfo.value);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.2")]
	public class srvsvcStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("4b324fc8-1670-01d3-1278-5a47bf6ee188");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(3, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private srvsvc _obj;
		public srvsvcStub(srvsvc obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_Opnum3NotUsedOnWire,
					this.Invoke_Opnum4NotUsedOnWire,
					this.Invoke_Opnum5NotUsedOnWire,
					this.Invoke_Opnum6NotUsedOnWire,
					this.Invoke_Opnum7NotUsedOnWire,
					this.Invoke_NetrConnectionEnum,
					this.Invoke_NetrFileEnum,
					this.Invoke_NetrFileGetInfo,
					this.Invoke_NetrFileClose,
					this.Invoke_NetrSessionEnum,
					this.Invoke_NetrSessionDel,
					this.Invoke_NetrShareAdd,
					this.Invoke_NetrShareEnum,
					this.Invoke_NetrShareGetInfo,
					this.Invoke_NetrShareSetInfo,
					this.Invoke_NetrShareDel,
					this.Invoke_NetrShareDelSticky,
					this.Invoke_NetrShareCheck,
					this.Invoke_NetrServerGetInfo,
					this.Invoke_NetrServerSetInfo,
					this.Invoke_NetrServerDiskEnum,
					this.Invoke_NetrServerStatisticsGet,
					this.Invoke_NetrServerTransportAdd,
					this.Invoke_NetrServerTransportEnum,
					this.Invoke_NetrServerTransportDel,
					this.Invoke_NetrRemoteTOD,
					this.Invoke_Opnum29NotUsedOnWire,
					this.Invoke_NetprPathType,
					this.Invoke_NetprPathCanonicalize,
					this.Invoke_NetprPathCompare,
					this.Invoke_NetprNameValidate,
					this.Invoke_NetprNameCanonicalize,
					this.Invoke_NetprNameCompare,
					this.Invoke_NetrShareEnumSticky,
					this.Invoke_NetrShareDelStart,
					this.Invoke_NetrShareDelCommit,
					this.Invoke_NetrpGetFileSecurity,
					this.Invoke_NetrpSetFileSecurity,
					this.Invoke_NetrServerTransportAddEx,
					this.Invoke_Opnum42NotUsedOnWire,
					this.Invoke_NetrDfsGetVersion,
					this.Invoke_NetrDfsCreateLocalPartition,
					this.Invoke_NetrDfsDeleteLocalPartition,
					this.Invoke_NetrDfsSetLocalVolumeState,
					this.Invoke_Opnum47NotUsedOnWire,
					this.Invoke_NetrDfsCreateExitPoint,
					this.Invoke_NetrDfsDeleteExitPoint,
					this.Invoke_NetrDfsModifyPrefix,
					this.Invoke_NetrDfsFixLocalVolume,
					this.Invoke_NetrDfsManagerReportSiteInfo,
					this.Invoke_NetrServerTransportDelEx,
					this.Invoke_NetrServerAliasAdd,
					this.Invoke_NetrServerAliasEnum,
					this.Invoke_NetrServerAliasDel,
					this.Invoke_NetrShareDelEx};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_Opnum3NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum3NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_Opnum4NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum4NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_Opnum5NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum5NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_Opnum6NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum6NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_Opnum7NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum7NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_NetrConnectionEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string Qualifier;
			RpcPointer<CONNECT_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				Qualifier = null;
			}
			else
			{
				Qualifier = decoder.ReadWideCharString();
			}
			InfoStruct = new RpcPointer<CONNECT_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<CONNECT_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<CONNECT_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadPointer<uint>();
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrConnectionEnum(ServerName, Qualifier, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrFileEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string BasePath;
			string UserName;
			RpcPointer<FILE_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				BasePath = null;
			}
			else
			{
				BasePath = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				UserName = null;
			}
			else
			{
				UserName = decoder.ReadWideCharString();
			}
			InfoStruct = new RpcPointer<FILE_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<FILE_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FILE_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadPointer<uint>();
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrFileEnum(ServerName, BasePath, UserName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrFileGetInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint FileId;
			uint Level;
			RpcPointer<FILE_INFO> InfoStruct = new RpcPointer<FILE_INFO>();
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			FileId = decoder.ReadUInt32();
			Level = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrFileGetInfo(ServerName, FileId, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnion(InfoStruct.value);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrFileClose(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint FileId;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			FileId = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrFileClose(ServerName, FileId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrSessionEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string ClientName;
			string UserName;
			RpcPointer<SESSION_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				ClientName = null;
			}
			else
			{
				ClientName = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				UserName = null;
			}
			else
			{
				UserName = decoder.ReadWideCharString();
			}
			InfoStruct = new RpcPointer<SESSION_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<SESSION_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SESSION_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadPointer<uint>();
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrSessionEnum(ServerName, ClientName, UserName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrSessionDel(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string ClientName;
			string UserName;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				ClientName = null;
			}
			else
			{
				ClientName = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				UserName = null;
			}
			else
			{
				UserName = decoder.ReadWideCharString();
			}
			var invokeTask = this._obj.NetrSessionDel(ServerName, ClientName, UserName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareAdd(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<SHARE_INFO> InfoStruct;
			RpcPointer<uint> ParmErr;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			InfoStruct = new RpcPointer<SHARE_INFO>();
			InfoStruct.value = decoder.ReadUnion<SHARE_INFO>();
			decoder.ReadStructDeferral<SHARE_INFO>(ref InfoStruct.value);
			ParmErr = decoder.ReadPointer<uint>();
			if ((null != ParmErr))
			{
				ParmErr.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrShareAdd(ServerName, Level, InfoStruct, ParmErr, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ParmErr);
			if ((null != ParmErr))
			{
				encoder.WriteValue(ParmErr.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<SHARE_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			InfoStruct = new RpcPointer<SHARE_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<SHARE_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SHARE_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadPointer<uint>();
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrShareEnum(ServerName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareGetInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string NetName;
			uint Level;
			RpcPointer<SHARE_INFO> InfoStruct = new RpcPointer<SHARE_INFO>();
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			NetName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrShareGetInfo(ServerName, NetName, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnion(InfoStruct.value);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareSetInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string NetName;
			uint Level;
			RpcPointer<SHARE_INFO> ShareInfo;
			RpcPointer<uint> ParmErr;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			NetName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			ShareInfo = new RpcPointer<SHARE_INFO>();
			ShareInfo.value = decoder.ReadUnion<SHARE_INFO>();
			decoder.ReadStructDeferral<SHARE_INFO>(ref ShareInfo.value);
			ParmErr = decoder.ReadPointer<uint>();
			if ((null != ParmErr))
			{
				ParmErr.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrShareSetInfo(ServerName, NetName, Level, ShareInfo, ParmErr, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ParmErr);
			if ((null != ParmErr))
			{
				encoder.WriteValue(ParmErr.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareDel(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string NetName;
			uint Reserved;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			NetName = decoder.ReadWideCharString();
			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrShareDel(ServerName, NetName, Reserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareDelSticky(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string NetName;
			uint Reserved;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			NetName = decoder.ReadWideCharString();
			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrShareDelSticky(ServerName, NetName, Reserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareCheck(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string Device;
			RpcPointer<uint> Type = new RpcPointer<uint>();
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Device = decoder.ReadWideCharString();
			var invokeTask = this._obj.NetrShareCheck(ServerName, Device, Type, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(Type.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerGetInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<SERVER_INFO> InfoStruct = new RpcPointer<SERVER_INFO>();
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrServerGetInfo(ServerName, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnion(InfoStruct.value);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerSetInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<SERVER_INFO> ServerInfo;
			RpcPointer<uint> ParmErr;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			ServerInfo = new RpcPointer<SERVER_INFO>();
			ServerInfo.value = decoder.ReadUnion<SERVER_INFO>();
			decoder.ReadStructDeferral<SERVER_INFO>(ref ServerInfo.value);
			ParmErr = decoder.ReadPointer<uint>();
			if ((null != ParmErr))
			{
				ParmErr.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrServerSetInfo(ServerName, Level, ServerInfo, ParmErr, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ParmErr);
			if ((null != ParmErr))
			{
				encoder.WriteValue(ParmErr.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerDiskEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<DISK_ENUM_CONTAINER> DiskInfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			DiskInfoStruct = new RpcPointer<DISK_ENUM_CONTAINER>();
			DiskInfoStruct.value = decoder.ReadFixedStruct<DISK_ENUM_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<DISK_ENUM_CONTAINER>(ref DiskInfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadPointer<uint>();
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrServerDiskEnum(ServerName, Level, DiskInfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(DiskInfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(DiskInfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerStatisticsGet(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string Service;
			uint Level;
			uint Options;
			RpcPointer<RpcPointer<STAT_SERVER_0>> InfoStruct = new RpcPointer<RpcPointer<STAT_SERVER_0>>();
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				Service = null;
			}
			else
			{
				Service = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			Options = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrServerStatisticsGet(ServerName, Service, Level, Options, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(InfoStruct.value);
			if ((null != InfoStruct.value))
			{
				encoder.WriteFixedStruct(InfoStruct.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(InfoStruct.value.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerTransportAdd(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<SERVER_TRANSPORT_INFO_0> Buffer;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			Buffer = new RpcPointer<SERVER_TRANSPORT_INFO_0>();
			Buffer.value = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_0>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_0>(ref Buffer.value);
			var invokeTask = this._obj.NetrServerTransportAdd(ServerName, Level, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerTransportEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<SERVER_XPORT_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			InfoStruct = new RpcPointer<SERVER_XPORT_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<SERVER_XPORT_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_XPORT_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadPointer<uint>();
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrServerTransportEnum(ServerName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerTransportDel(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<SERVER_TRANSPORT_INFO_0> Buffer;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			Buffer = new RpcPointer<SERVER_TRANSPORT_INFO_0>();
			Buffer.value = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_0>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_0>(ref Buffer.value);
			var invokeTask = this._obj.NetrServerTransportDel(ServerName, Level, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrRemoteTOD(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<RpcPointer<TIME_OF_DAY_INFO>> BufferPtr = new RpcPointer<RpcPointer<TIME_OF_DAY_INFO>>();
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			var invokeTask = this._obj.NetrRemoteTOD(ServerName, BufferPtr, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(BufferPtr.value);
			if ((null != BufferPtr.value))
			{
				encoder.WriteFixedStruct(BufferPtr.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(BufferPtr.value.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum29NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum29NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_NetprPathType(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string PathName;
			RpcPointer<uint> PathType = new RpcPointer<uint>();
			uint Flags;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			PathName = decoder.ReadWideCharString();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprPathType(ServerName, PathName, PathType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(PathType.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetprPathCanonicalize(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string PathName;
			RpcPointer<byte[]> Outbuf = new RpcPointer<byte[]>();
			uint OutbufLen;
			string Prefix;
			RpcPointer<uint> PathType;
			uint Flags;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			PathName = decoder.ReadWideCharString();
			OutbufLen = decoder.ReadUInt32();
			Prefix = decoder.ReadWideCharString();
			PathType = new RpcPointer<uint>();
			PathType.value = decoder.ReadUInt32();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprPathCanonicalize(ServerName, PathName, Outbuf, OutbufLen, Prefix, PathType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(Outbuf.value);
			for (int i = 0; (i < Outbuf.value.Length); i++
			)
			{
				byte elem_0 = Outbuf.value[i];
				encoder.WriteValue(elem_0);
			}
			encoder.WriteValue(PathType.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetprPathCompare(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string PathName1;
			string PathName2;
			uint PathType;
			uint Flags;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			PathName1 = decoder.ReadWideCharString();
			PathName2 = decoder.ReadWideCharString();
			PathType = decoder.ReadUInt32();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprPathCompare(ServerName, PathName1, PathName2, PathType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetprNameValidate(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string Name;
			uint NameType;
			uint Flags;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Name = decoder.ReadWideCharString();
			NameType = decoder.ReadUInt32();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprNameValidate(ServerName, Name, NameType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetprNameCanonicalize(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string Name;
			RpcPointer<char[]> Outbuf = new RpcPointer<char[]>();
			uint OutbufLen;
			uint NameType;
			uint Flags;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Name = decoder.ReadWideCharString();
			OutbufLen = decoder.ReadUInt32();
			NameType = decoder.ReadUInt32();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprNameCanonicalize(ServerName, Name, Outbuf, OutbufLen, NameType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(Outbuf.value);
			for (int i = 0; (i < Outbuf.value.Length); i++
			)
			{
				char elem_0 = Outbuf.value[i];
				encoder.WriteValue(elem_0);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetprNameCompare(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string Name1;
			string Name2;
			uint NameType;
			uint Flags;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Name1 = decoder.ReadWideCharString();
			Name2 = decoder.ReadWideCharString();
			NameType = decoder.ReadUInt32();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprNameCompare(ServerName, Name1, Name2, NameType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareEnumSticky(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<SHARE_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			InfoStruct = new RpcPointer<SHARE_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<SHARE_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SHARE_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadPointer<uint>();
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrShareEnumSticky(ServerName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareDelStart(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string NetName;
			uint Reserved;
			RpcPointer<Titanis.DceRpc.RpcContextHandle> ContextHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			NetName = decoder.ReadWideCharString();
			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrShareDelStart(ServerName, NetName, Reserved, ContextHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ContextHandle.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareDelCommit(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<Titanis.DceRpc.RpcContextHandle> ContextHandle;
			ContextHandle = new RpcPointer<Titanis.DceRpc.RpcContextHandle>();
			ContextHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.NetrShareDelCommit(ContextHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ContextHandle.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrpGetFileSecurity(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string ShareName;
			string lpFileName;
			uint RequestedInformation;
			RpcPointer<RpcPointer<ADT_SECURITY_DESCRIPTOR>> SecurityDescriptor = new RpcPointer<RpcPointer<ADT_SECURITY_DESCRIPTOR>>();
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				ShareName = null;
			}
			else
			{
				ShareName = decoder.ReadWideCharString();
			}
			lpFileName = decoder.ReadWideCharString();
			RequestedInformation = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrpGetFileSecurity(ServerName, ShareName, lpFileName, RequestedInformation, SecurityDescriptor, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(SecurityDescriptor.value);
			if ((null != SecurityDescriptor.value))
			{
				encoder.WriteFixedStruct(SecurityDescriptor.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(SecurityDescriptor.value.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrpSetFileSecurity(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string ShareName;
			string lpFileName;
			uint SecurityInformation;
			RpcPointer<ADT_SECURITY_DESCRIPTOR> SecurityDescriptor;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				ShareName = null;
			}
			else
			{
				ShareName = decoder.ReadWideCharString();
			}
			lpFileName = decoder.ReadWideCharString();
			SecurityInformation = decoder.ReadUInt32();
			SecurityDescriptor = new RpcPointer<ADT_SECURITY_DESCRIPTOR>();
			SecurityDescriptor.value = decoder.ReadFixedStruct<ADT_SECURITY_DESCRIPTOR>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ADT_SECURITY_DESCRIPTOR>(ref SecurityDescriptor.value);
			var invokeTask = this._obj.NetrpSetFileSecurity(ServerName, ShareName, lpFileName, SecurityInformation, SecurityDescriptor, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerTransportAddEx(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<TRANSPORT_INFO> Buffer;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			Buffer = new RpcPointer<TRANSPORT_INFO>();
			Buffer.value = decoder.ReadUnion<TRANSPORT_INFO>();
			decoder.ReadStructDeferral<TRANSPORT_INFO>(ref Buffer.value);
			var invokeTask = this._obj.NetrServerTransportAddEx(ServerName, Level, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum42NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum42NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_NetrDfsGetVersion(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<uint> Version = new RpcPointer<uint>();
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			var invokeTask = this._obj.NetrDfsGetVersion(ServerName, Version, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(Version.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrDfsCreateLocalPartition(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string ShareName;
			RpcPointer<System.Guid> EntryUid;
			string EntryPrefix;
			string ShortName;
			RpcPointer<NET_DFS_ENTRY_ID_CONTAINER> RelationInfo;
			int Force;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			ShareName = decoder.ReadWideCharString();
			EntryUid = new RpcPointer<System.Guid>();
			EntryUid.value = decoder.ReadUuid();
			EntryPrefix = decoder.ReadWideCharString();
			ShortName = decoder.ReadWideCharString();
			RelationInfo = new RpcPointer<NET_DFS_ENTRY_ID_CONTAINER>();
			RelationInfo.value = decoder.ReadFixedStruct<NET_DFS_ENTRY_ID_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<NET_DFS_ENTRY_ID_CONTAINER>(ref RelationInfo.value);
			Force = decoder.ReadInt32();
			var invokeTask = this._obj.NetrDfsCreateLocalPartition(ServerName, ShareName, EntryUid, EntryPrefix, ShortName, RelationInfo, Force, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrDfsDeleteLocalPartition(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<System.Guid> Uid;
			string Prefix;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Uid = new RpcPointer<System.Guid>();
			Uid.value = decoder.ReadUuid();
			Prefix = decoder.ReadWideCharString();
			var invokeTask = this._obj.NetrDfsDeleteLocalPartition(ServerName, Uid, Prefix, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrDfsSetLocalVolumeState(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<System.Guid> Uid;
			string Prefix;
			uint State;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Uid = new RpcPointer<System.Guid>();
			Uid.value = decoder.ReadUuid();
			Prefix = decoder.ReadWideCharString();
			State = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrDfsSetLocalVolumeState(ServerName, Uid, Prefix, State, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum47NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum47NotUsedOnWire(cancellationToken);
			await invokeTask;
		}
		private async Task Invoke_NetrDfsCreateExitPoint(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<System.Guid> Uid;
			string Prefix;
			uint Type;
			uint ShortPrefixLen;
			RpcPointer<char[]> ShortPrefix = new RpcPointer<char[]>();
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Uid = new RpcPointer<System.Guid>();
			Uid.value = decoder.ReadUuid();
			Prefix = decoder.ReadWideCharString();
			Type = decoder.ReadUInt32();
			ShortPrefixLen = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrDfsCreateExitPoint(ServerName, Uid, Prefix, Type, ShortPrefixLen, ShortPrefix, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(ShortPrefix.value);
			for (int i = 0; (i < ShortPrefix.value.Length); i++
			)
			{
				char elem_0 = ShortPrefix.value[i];
				encoder.WriteValue(elem_0);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrDfsDeleteExitPoint(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<System.Guid> Uid;
			string Prefix;
			uint Type;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Uid = new RpcPointer<System.Guid>();
			Uid.value = decoder.ReadUuid();
			Prefix = decoder.ReadWideCharString();
			Type = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrDfsDeleteExitPoint(ServerName, Uid, Prefix, Type, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrDfsModifyPrefix(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<System.Guid> Uid;
			string Prefix;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Uid = new RpcPointer<System.Guid>();
			Uid.value = decoder.ReadUuid();
			Prefix = decoder.ReadWideCharString();
			var invokeTask = this._obj.NetrDfsModifyPrefix(ServerName, Uid, Prefix, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrDfsFixLocalVolume(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			string VolumeName;
			uint EntryType;
			uint ServiceType;
			string StgId;
			RpcPointer<System.Guid> EntryUid;
			string EntryPrefix;
			RpcPointer<NET_DFS_ENTRY_ID_CONTAINER> RelationInfo;
			uint CreateDisposition;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			VolumeName = decoder.ReadWideCharString();
			EntryType = decoder.ReadUInt32();
			ServiceType = decoder.ReadUInt32();
			StgId = decoder.ReadWideCharString();
			EntryUid = new RpcPointer<System.Guid>();
			EntryUid.value = decoder.ReadUuid();
			EntryPrefix = decoder.ReadWideCharString();
			RelationInfo = new RpcPointer<NET_DFS_ENTRY_ID_CONTAINER>();
			RelationInfo.value = decoder.ReadFixedStruct<NET_DFS_ENTRY_ID_CONTAINER>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<NET_DFS_ENTRY_ID_CONTAINER>(ref RelationInfo.value);
			CreateDisposition = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrDfsFixLocalVolume(ServerName, VolumeName, EntryType, ServiceType, StgId, EntryUid, EntryPrefix, RelationInfo, CreateDisposition, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrDfsManagerReportSiteInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<RpcPointer<DFS_SITELIST_INFO>> ppSiteInfo;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			ppSiteInfo = decoder.ReadPointer<RpcPointer<DFS_SITELIST_INFO>>();
			if ((null != ppSiteInfo))
			{
				ppSiteInfo.value = decoder.ReadPointer<DFS_SITELIST_INFO>();
				if ((null != ppSiteInfo.value))
				{
					ppSiteInfo.value.value = decoder.ReadConformantStruct<DFS_SITELIST_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<DFS_SITELIST_INFO>(ref ppSiteInfo.value.value);
				}
			}
			var invokeTask = this._obj.NetrDfsManagerReportSiteInfo(ServerName, ppSiteInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ppSiteInfo);
			if ((null != ppSiteInfo))
			{
				encoder.WritePointer(ppSiteInfo.value);
				if ((null != ppSiteInfo.value))
				{
					encoder.WriteConformantStruct(ppSiteInfo.value.value, Titanis.DceRpc.NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(ppSiteInfo.value.value);
				}
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerTransportDelEx(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<TRANSPORT_INFO> Buffer;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			Buffer = new RpcPointer<TRANSPORT_INFO>();
			Buffer.value = decoder.ReadUnion<TRANSPORT_INFO>();
			decoder.ReadStructDeferral<TRANSPORT_INFO>(ref Buffer.value);
			var invokeTask = this._obj.NetrServerTransportDelEx(ServerName, Level, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerAliasAdd(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<SERVER_ALIAS_INFO> InfoStruct;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			InfoStruct = new RpcPointer<SERVER_ALIAS_INFO>();
			InfoStruct.value = decoder.ReadUnion<SERVER_ALIAS_INFO>();
			decoder.ReadStructDeferral<SERVER_ALIAS_INFO>(ref InfoStruct.value);
			var invokeTask = this._obj.NetrServerAliasAdd(ServerName, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerAliasEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<SERVER_ALIAS_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			InfoStruct = new RpcPointer<SERVER_ALIAS_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<SERVER_ALIAS_ENUM_STRUCT>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_ALIAS_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadPointer<uint>();
			if ((null != ResumeHandle))
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}
			var invokeTask = this._obj.NetrServerAliasEnum(ServerName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WritePointer(ResumeHandle);
			if ((null != ResumeHandle))
			{
				encoder.WriteValue(ResumeHandle.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrServerAliasDel(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<SERVER_ALIAS_INFO> InfoStruct;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			InfoStruct = new RpcPointer<SERVER_ALIAS_INFO>();
			InfoStruct.value = decoder.ReadUnion<SERVER_ALIAS_INFO>();
			decoder.ReadStructDeferral<SERVER_ALIAS_INFO>(ref InfoStruct.value);
			var invokeTask = this._obj.NetrServerAliasDel(ServerName, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NetrShareDelEx(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<SHARE_INFO> ShareInfo;
			if ((decoder.ReadReferentId() == 0))
			{
				ServerName = null;
			}
			else
			{
				ServerName = decoder.ReadWideCharString();
			}
			Level = decoder.ReadUInt32();
			ShareInfo = new RpcPointer<SHARE_INFO>();
			ShareInfo.value = decoder.ReadUnion<SHARE_INFO>();
			decoder.ReadStructDeferral<SHARE_INFO>(ref ShareInfo.value);
			var invokeTask = this._obj.NetrShareDelEx(ServerName, Level, ShareInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
	}
}
