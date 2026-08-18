namespace ms_srvs
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct CONNECTION_INFO_0 : IRpcFixedStruct
	{
		public uint coni0_id;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.coni0_id);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.coni0_id = decoder.ReadUInt32();
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
	public partial struct CONNECT_INFO_0_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<CONNECTION_INFO_0[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<CONNECTION_INFO_0[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					CONNECTION_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					CONNECTION_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<CONNECTION_INFO_0>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					CONNECTION_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<CONNECTION_INFO_0>(NdrAlignment._4Byte);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					CONNECTION_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<CONNECTION_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct CONNECTION_INFO_1 : IRpcFixedStruct
	{
		public uint coni1_id;
		public uint coni1_type;
		public uint coni1_num_opens;
		public uint coni1_num_users;
		public uint coni1_time;
		public RpcPointer<string> coni1_username;
		public RpcPointer<string> coni1_netname;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.coni1_id);
			encoder.WriteValue(this.coni1_type);
			encoder.WriteValue(this.coni1_num_opens);
			encoder.WriteValue(this.coni1_num_users);
			encoder.WriteValue(this.coni1_time);
			encoder.WriteUniquePointer(this.coni1_username);
			encoder.WriteUniquePointer(this.coni1_netname);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.coni1_id = decoder.ReadUInt32();
			this.coni1_type = decoder.ReadUInt32();
			this.coni1_num_opens = decoder.ReadUInt32();
			this.coni1_num_users = decoder.ReadUInt32();
			this.coni1_time = decoder.ReadUInt32();
			this.coni1_username = decoder.ReadUniquePointer<string>();
			this.coni1_netname = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.coni1_username is not null)
			{
				encoder.WriteWideCharString(this.coni1_username.value);
			}

			if (this.coni1_netname is not null)
			{
				encoder.WriteWideCharString(this.coni1_netname.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.coni1_username is not null)
			{
				this.coni1_username.value = decoder.ReadWideCharString();
			}

			if (this.coni1_netname is not null)
			{
				this.coni1_netname.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct CONNECT_INFO_1_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<CONNECTION_INFO_1[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<CONNECTION_INFO_1[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					CONNECTION_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					CONNECTION_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<CONNECTION_INFO_1>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					CONNECTION_INFO_1 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<CONNECTION_INFO_1>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					CONNECTION_INFO_1 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<CONNECTION_INFO_1>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct CONNECT_ENUM_UNION : IRpcFixedStruct
	{
		public uint Level;
		public RpcPointer<CONNECT_INFO_0_CONTAINER> Level0;
		public RpcPointer<CONNECT_INFO_1_CONTAINER> Level1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.Level);
			switch ((uint)this.Level)
			{
				case 0U:
					encoder.WriteUniquePointer(this.Level0);
					break;
				case 1U:
					encoder.WriteUniquePointer(this.Level1);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.Level = decoder.ReadUInt32();
			switch ((uint)this.Level)
			{
				case 0U:
					this.Level0 = decoder.ReadUniquePointer<CONNECT_INFO_0_CONTAINER>();
					break;
				case 1U:
					this.Level1 = decoder.ReadUniquePointer<CONNECT_INFO_1_CONTAINER>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						encoder.WriteFixedStruct(this.Level0.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level0.value);
					}

					break;
				case 1U:
					if (this.Level1 is not null)
					{
						encoder.WriteFixedStruct(this.Level1.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level1.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						this.Level0.value = decoder.ReadFixedStruct<CONNECT_INFO_0_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<CONNECT_INFO_0_CONTAINER>(ref this.Level0.value);
					}

					break;
				case 1U:
					if (this.Level1 is not null)
					{
						this.Level1.value = decoder.ReadFixedStruct<CONNECT_INFO_1_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<CONNECT_INFO_1_CONTAINER>(ref this.Level1.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct CONNECT_ENUM_STRUCT : IRpcFixedStruct
	{
		public uint Level;
		public CONNECT_ENUM_UNION ConnectInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.ConnectInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.ConnectInfo = decoder.ReadUnion<CONNECT_ENUM_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ConnectInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<CONNECT_ENUM_UNION>(ref this.ConnectInfo);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct FILE_INFO_2 : IRpcFixedStruct
	{
		public uint fi2_id;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.fi2_id);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.fi2_id = decoder.ReadUInt32();
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
	public partial struct FILE_INFO_2_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<FILE_INFO_2[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<FILE_INFO_2[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					FILE_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					FILE_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<FILE_INFO_2>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					FILE_INFO_2 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<FILE_INFO_2>(NdrAlignment._4Byte);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					FILE_INFO_2 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<FILE_INFO_2>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct FILE_INFO_3 : IRpcFixedStruct
	{
		public uint fi3_id;
		public uint fi3_permissions;
		public uint fi3_num_locks;
		public RpcPointer<string> fi3_pathname;
		public RpcPointer<string> fi3_username;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.fi3_id);
			encoder.WriteValue(this.fi3_permissions);
			encoder.WriteValue(this.fi3_num_locks);
			encoder.WriteUniquePointer(this.fi3_pathname);
			encoder.WriteUniquePointer(this.fi3_username);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.fi3_id = decoder.ReadUInt32();
			this.fi3_permissions = decoder.ReadUInt32();
			this.fi3_num_locks = decoder.ReadUInt32();
			this.fi3_pathname = decoder.ReadUniquePointer<string>();
			this.fi3_username = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.fi3_pathname is not null)
			{
				encoder.WriteWideCharString(this.fi3_pathname.value);
			}

			if (this.fi3_username is not null)
			{
				encoder.WriteWideCharString(this.fi3_username.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.fi3_pathname is not null)
			{
				this.fi3_pathname.value = decoder.ReadWideCharString();
			}

			if (this.fi3_username is not null)
			{
				this.fi3_username.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct FILE_INFO_3_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<FILE_INFO_3[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<FILE_INFO_3[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					FILE_INFO_3 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					FILE_INFO_3 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<FILE_INFO_3>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					FILE_INFO_3 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<FILE_INFO_3>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					FILE_INFO_3 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<FILE_INFO_3>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct FILE_ENUM_UNION : IRpcFixedStruct
	{
		public uint Level;
		public RpcPointer<FILE_INFO_2_CONTAINER> Level2;
		public RpcPointer<FILE_INFO_3_CONTAINER> Level3;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.Level);
			switch ((uint)this.Level)
			{
				case 2U:
					encoder.WriteUniquePointer(this.Level2);
					break;
				case 3U:
					encoder.WriteUniquePointer(this.Level3);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.Level = decoder.ReadUInt32();
			switch ((uint)this.Level)
			{
				case 2U:
					this.Level2 = decoder.ReadUniquePointer<FILE_INFO_2_CONTAINER>();
					break;
				case 3U:
					this.Level3 = decoder.ReadUniquePointer<FILE_INFO_3_CONTAINER>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.Level)
			{
				case 2U:
					if (this.Level2 is not null)
					{
						encoder.WriteFixedStruct(this.Level2.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level2.value);
					}

					break;
				case 3U:
					if (this.Level3 is not null)
					{
						encoder.WriteFixedStruct(this.Level3.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level3.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.Level)
			{
				case 2U:
					if (this.Level2 is not null)
					{
						this.Level2.value = decoder.ReadFixedStruct<FILE_INFO_2_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<FILE_INFO_2_CONTAINER>(ref this.Level2.value);
					}

					break;
				case 3U:
					if (this.Level3 is not null)
					{
						this.Level3.value = decoder.ReadFixedStruct<FILE_INFO_3_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<FILE_INFO_3_CONTAINER>(ref this.Level3.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct FILE_ENUM_STRUCT : IRpcFixedStruct
	{
		public uint Level;
		public FILE_ENUM_UNION FileInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.FileInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.FileInfo = decoder.ReadUnion<FILE_ENUM_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.FileInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<FILE_ENUM_UNION>(ref this.FileInfo);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct FILE_INFO : IRpcFixedStruct
	{
		public uint unionSwitch;
		public RpcPointer<FILE_INFO_2> FileInfo2;
		public RpcPointer<FILE_INFO_3> FileInfo3;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 2U:
					encoder.WriteUniquePointer(this.FileInfo2);
					break;
				case 3U:
					encoder.WriteUniquePointer(this.FileInfo3);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 2U:
					this.FileInfo2 = decoder.ReadUniquePointer<FILE_INFO_2>();
					break;
				case 3U:
					this.FileInfo3 = decoder.ReadUniquePointer<FILE_INFO_3>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 2U:
					if (this.FileInfo2 is not null)
					{
						encoder.WriteFixedStruct(this.FileInfo2.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.FileInfo2.value);
					}

					break;
				case 3U:
					if (this.FileInfo3 is not null)
					{
						encoder.WriteFixedStruct(this.FileInfo3.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.FileInfo3.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 2U:
					if (this.FileInfo2 is not null)
					{
						this.FileInfo2.value = decoder.ReadFixedStruct<FILE_INFO_2>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<FILE_INFO_2>(ref this.FileInfo2.value);
					}

					break;
				case 3U:
					if (this.FileInfo3 is not null)
					{
						this.FileInfo3.value = decoder.ReadFixedStruct<FILE_INFO_3>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<FILE_INFO_3>(ref this.FileInfo3.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_INFO_0 : IRpcFixedStruct
	{
		public RpcPointer<string> sesi0_cname;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.sesi0_cname);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sesi0_cname = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.sesi0_cname is not null)
			{
				encoder.WriteWideCharString(this.sesi0_cname.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.sesi0_cname is not null)
			{
				this.sesi0_cname.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_INFO_0_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SESSION_INFO_0[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SESSION_INFO_0[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SESSION_INFO_0>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SESSION_INFO_0>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SESSION_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_INFO_1 : IRpcFixedStruct
	{
		public RpcPointer<string> sesi1_cname;
		public RpcPointer<string> sesi1_username;
		public uint sesi1_num_opens;
		public uint sesi1_time;
		public uint sesi1_idle_time;
		public uint sesi1_user_flags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.sesi1_cname);
			encoder.WriteUniquePointer(this.sesi1_username);
			encoder.WriteValue(this.sesi1_num_opens);
			encoder.WriteValue(this.sesi1_time);
			encoder.WriteValue(this.sesi1_idle_time);
			encoder.WriteValue(this.sesi1_user_flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sesi1_cname = decoder.ReadUniquePointer<string>();
			this.sesi1_username = decoder.ReadUniquePointer<string>();
			this.sesi1_num_opens = decoder.ReadUInt32();
			this.sesi1_time = decoder.ReadUInt32();
			this.sesi1_idle_time = decoder.ReadUInt32();
			this.sesi1_user_flags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.sesi1_cname is not null)
			{
				encoder.WriteWideCharString(this.sesi1_cname.value);
			}

			if (this.sesi1_username is not null)
			{
				encoder.WriteWideCharString(this.sesi1_username.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.sesi1_cname is not null)
			{
				this.sesi1_cname.value = decoder.ReadWideCharString();
			}

			if (this.sesi1_username is not null)
			{
				this.sesi1_username.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_INFO_1_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SESSION_INFO_1[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SESSION_INFO_1[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SESSION_INFO_1>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_1 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SESSION_INFO_1>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_1 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SESSION_INFO_1>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_INFO_2 : IRpcFixedStruct
	{
		public RpcPointer<string> sesi2_cname;
		public RpcPointer<string> sesi2_username;
		public uint sesi2_num_opens;
		public uint sesi2_time;
		public uint sesi2_idle_time;
		public uint sesi2_user_flags;
		public RpcPointer<string> sesi2_cltype_name;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.sesi2_cname);
			encoder.WriteUniquePointer(this.sesi2_username);
			encoder.WriteValue(this.sesi2_num_opens);
			encoder.WriteValue(this.sesi2_time);
			encoder.WriteValue(this.sesi2_idle_time);
			encoder.WriteValue(this.sesi2_user_flags);
			encoder.WriteUniquePointer(this.sesi2_cltype_name);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sesi2_cname = decoder.ReadUniquePointer<string>();
			this.sesi2_username = decoder.ReadUniquePointer<string>();
			this.sesi2_num_opens = decoder.ReadUInt32();
			this.sesi2_time = decoder.ReadUInt32();
			this.sesi2_idle_time = decoder.ReadUInt32();
			this.sesi2_user_flags = decoder.ReadUInt32();
			this.sesi2_cltype_name = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.sesi2_cname is not null)
			{
				encoder.WriteWideCharString(this.sesi2_cname.value);
			}

			if (this.sesi2_username is not null)
			{
				encoder.WriteWideCharString(this.sesi2_username.value);
			}

			if (this.sesi2_cltype_name is not null)
			{
				encoder.WriteWideCharString(this.sesi2_cltype_name.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.sesi2_cname is not null)
			{
				this.sesi2_cname.value = decoder.ReadWideCharString();
			}

			if (this.sesi2_username is not null)
			{
				this.sesi2_username.value = decoder.ReadWideCharString();
			}

			if (this.sesi2_cltype_name is not null)
			{
				this.sesi2_cltype_name.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_INFO_2_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SESSION_INFO_2[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SESSION_INFO_2[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SESSION_INFO_2>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_2 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SESSION_INFO_2>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_2 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SESSION_INFO_2>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_INFO_10 : IRpcFixedStruct
	{
		public RpcPointer<string> sesi10_cname;
		public RpcPointer<string> sesi10_username;
		public uint sesi10_time;
		public uint sesi10_idle_time;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.sesi10_cname);
			encoder.WriteUniquePointer(this.sesi10_username);
			encoder.WriteValue(this.sesi10_time);
			encoder.WriteValue(this.sesi10_idle_time);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sesi10_cname = decoder.ReadUniquePointer<string>();
			this.sesi10_username = decoder.ReadUniquePointer<string>();
			this.sesi10_time = decoder.ReadUInt32();
			this.sesi10_idle_time = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.sesi10_cname is not null)
			{
				encoder.WriteWideCharString(this.sesi10_cname.value);
			}

			if (this.sesi10_username is not null)
			{
				encoder.WriteWideCharString(this.sesi10_username.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.sesi10_cname is not null)
			{
				this.sesi10_cname.value = decoder.ReadWideCharString();
			}

			if (this.sesi10_username is not null)
			{
				this.sesi10_username.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_INFO_10_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SESSION_INFO_10[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SESSION_INFO_10[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_10 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_10 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SESSION_INFO_10>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_10 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SESSION_INFO_10>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_10 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SESSION_INFO_10>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_INFO_502 : IRpcFixedStruct
	{
		public RpcPointer<string> sesi502_cname;
		public RpcPointer<string> sesi502_username;
		public uint sesi502_num_opens;
		public uint sesi502_time;
		public uint sesi502_idle_time;
		public uint sesi502_user_flags;
		public RpcPointer<string> sesi502_cltype_name;
		public RpcPointer<string> sesi502_transport;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.sesi502_cname);
			encoder.WriteUniquePointer(this.sesi502_username);
			encoder.WriteValue(this.sesi502_num_opens);
			encoder.WriteValue(this.sesi502_time);
			encoder.WriteValue(this.sesi502_idle_time);
			encoder.WriteValue(this.sesi502_user_flags);
			encoder.WriteUniquePointer(this.sesi502_cltype_name);
			encoder.WriteUniquePointer(this.sesi502_transport);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sesi502_cname = decoder.ReadUniquePointer<string>();
			this.sesi502_username = decoder.ReadUniquePointer<string>();
			this.sesi502_num_opens = decoder.ReadUInt32();
			this.sesi502_time = decoder.ReadUInt32();
			this.sesi502_idle_time = decoder.ReadUInt32();
			this.sesi502_user_flags = decoder.ReadUInt32();
			this.sesi502_cltype_name = decoder.ReadUniquePointer<string>();
			this.sesi502_transport = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.sesi502_cname is not null)
			{
				encoder.WriteWideCharString(this.sesi502_cname.value);
			}

			if (this.sesi502_username is not null)
			{
				encoder.WriteWideCharString(this.sesi502_username.value);
			}

			if (this.sesi502_cltype_name is not null)
			{
				encoder.WriteWideCharString(this.sesi502_cltype_name.value);
			}

			if (this.sesi502_transport is not null)
			{
				encoder.WriteWideCharString(this.sesi502_transport.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.sesi502_cname is not null)
			{
				this.sesi502_cname.value = decoder.ReadWideCharString();
			}

			if (this.sesi502_username is not null)
			{
				this.sesi502_username.value = decoder.ReadWideCharString();
			}

			if (this.sesi502_cltype_name is not null)
			{
				this.sesi502_cltype_name.value = decoder.ReadWideCharString();
			}

			if (this.sesi502_transport is not null)
			{
				this.sesi502_transport.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_INFO_502_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SESSION_INFO_502[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SESSION_INFO_502[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_502 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_502 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SESSION_INFO_502>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_502 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SESSION_INFO_502>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SESSION_INFO_502 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SESSION_INFO_502>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_ENUM_UNION : IRpcFixedStruct
	{
		public uint Level;
		public RpcPointer<SESSION_INFO_0_CONTAINER> Level0;
		public RpcPointer<SESSION_INFO_1_CONTAINER> Level1;
		public RpcPointer<SESSION_INFO_2_CONTAINER> Level2;
		public RpcPointer<SESSION_INFO_10_CONTAINER> Level10;
		public RpcPointer<SESSION_INFO_502_CONTAINER> Level502;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.Level);
			switch ((uint)this.Level)
			{
				case 0U:
					encoder.WriteUniquePointer(this.Level0);
					break;
				case 1U:
					encoder.WriteUniquePointer(this.Level1);
					break;
				case 2U:
					encoder.WriteUniquePointer(this.Level2);
					break;
				case 10U:
					encoder.WriteUniquePointer(this.Level10);
					break;
				case 502U:
					encoder.WriteUniquePointer(this.Level502);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.Level = decoder.ReadUInt32();
			switch ((uint)this.Level)
			{
				case 0U:
					this.Level0 = decoder.ReadUniquePointer<SESSION_INFO_0_CONTAINER>();
					break;
				case 1U:
					this.Level1 = decoder.ReadUniquePointer<SESSION_INFO_1_CONTAINER>();
					break;
				case 2U:
					this.Level2 = decoder.ReadUniquePointer<SESSION_INFO_2_CONTAINER>();
					break;
				case 10U:
					this.Level10 = decoder.ReadUniquePointer<SESSION_INFO_10_CONTAINER>();
					break;
				case 502U:
					this.Level502 = decoder.ReadUniquePointer<SESSION_INFO_502_CONTAINER>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						encoder.WriteFixedStruct(this.Level0.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level0.value);
					}

					break;
				case 1U:
					if (this.Level1 is not null)
					{
						encoder.WriteFixedStruct(this.Level1.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level1.value);
					}

					break;
				case 2U:
					if (this.Level2 is not null)
					{
						encoder.WriteFixedStruct(this.Level2.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level2.value);
					}

					break;
				case 10U:
					if (this.Level10 is not null)
					{
						encoder.WriteFixedStruct(this.Level10.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level10.value);
					}

					break;
				case 502U:
					if (this.Level502 is not null)
					{
						encoder.WriteFixedStruct(this.Level502.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level502.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						this.Level0.value = decoder.ReadFixedStruct<SESSION_INFO_0_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SESSION_INFO_0_CONTAINER>(ref this.Level0.value);
					}

					break;
				case 1U:
					if (this.Level1 is not null)
					{
						this.Level1.value = decoder.ReadFixedStruct<SESSION_INFO_1_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SESSION_INFO_1_CONTAINER>(ref this.Level1.value);
					}

					break;
				case 2U:
					if (this.Level2 is not null)
					{
						this.Level2.value = decoder.ReadFixedStruct<SESSION_INFO_2_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SESSION_INFO_2_CONTAINER>(ref this.Level2.value);
					}

					break;
				case 10U:
					if (this.Level10 is not null)
					{
						this.Level10.value = decoder.ReadFixedStruct<SESSION_INFO_10_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SESSION_INFO_10_CONTAINER>(ref this.Level10.value);
					}

					break;
				case 502U:
					if (this.Level502 is not null)
					{
						this.Level502.value = decoder.ReadFixedStruct<SESSION_INFO_502_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SESSION_INFO_502_CONTAINER>(ref this.Level502.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SESSION_ENUM_STRUCT : IRpcFixedStruct
	{
		public uint Level;
		public SESSION_ENUM_UNION SessionInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.SessionInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.SessionInfo = decoder.ReadUnion<SESSION_ENUM_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.SessionInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SESSION_ENUM_UNION>(ref this.SessionInfo);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_502_I : IRpcFixedStruct
	{
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
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.shi502_netname);
			encoder.WriteValue(this.shi502_type);
			encoder.WriteUniquePointer(this.shi502_remark);
			encoder.WriteValue(this.shi502_permissions);
			encoder.WriteValue(this.shi502_max_uses);
			encoder.WriteValue(this.shi502_current_uses);
			encoder.WriteUniquePointer(this.shi502_path);
			encoder.WriteUniquePointer(this.shi502_passwd);
			encoder.WriteValue(this.shi502_reserved);
			encoder.WriteUniquePointer(this.shi502_security_descriptor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.shi502_netname = decoder.ReadUniquePointer<string>();
			this.shi502_type = decoder.ReadUInt32();
			this.shi502_remark = decoder.ReadUniquePointer<string>();
			this.shi502_permissions = decoder.ReadUInt32();
			this.shi502_max_uses = decoder.ReadUInt32();
			this.shi502_current_uses = decoder.ReadUInt32();
			this.shi502_path = decoder.ReadUniquePointer<string>();
			this.shi502_passwd = decoder.ReadUniquePointer<string>();
			this.shi502_reserved = decoder.ReadUInt32();
			this.shi502_security_descriptor = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.shi502_netname is not null)
			{
				encoder.WriteWideCharString(this.shi502_netname.value);
			}

			if (this.shi502_remark is not null)
			{
				encoder.WriteWideCharString(this.shi502_remark.value);
			}

			if (this.shi502_path is not null)
			{
				encoder.WriteWideCharString(this.shi502_path.value);
			}

			if (this.shi502_passwd is not null)
			{
				encoder.WriteWideCharString(this.shi502_passwd.value);
			}

			if (this.shi502_security_descriptor is not null)
			{
				encoder.WriteArrayHeader(this.shi502_security_descriptor.value);
				for (int i = 0; i < this.shi502_security_descriptor.value.Length; i++)
				{
					byte elem_0 = this.shi502_security_descriptor.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.shi502_netname is not null)
			{
				this.shi502_netname.value = decoder.ReadWideCharString();
			}

			if (this.shi502_remark is not null)
			{
				this.shi502_remark.value = decoder.ReadWideCharString();
			}

			if (this.shi502_path is not null)
			{
				this.shi502_path.value = decoder.ReadWideCharString();
			}

			if (this.shi502_passwd is not null)
			{
				this.shi502_passwd.value = decoder.ReadWideCharString();
			}

			if (this.shi502_security_descriptor is not null)
			{
				this.shi502_security_descriptor.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.shi502_security_descriptor.value.Length; i++)
				{
					byte elem_0 = this.shi502_security_descriptor.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.shi502_security_descriptor.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_503_I : IRpcFixedStruct
	{
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
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.shi503_netname);
			encoder.WriteValue(this.shi503_type);
			encoder.WriteUniquePointer(this.shi503_remark);
			encoder.WriteValue(this.shi503_permissions);
			encoder.WriteValue(this.shi503_max_uses);
			encoder.WriteValue(this.shi503_current_uses);
			encoder.WriteUniquePointer(this.shi503_path);
			encoder.WriteUniquePointer(this.shi503_passwd);
			encoder.WriteUniquePointer(this.shi503_servername);
			encoder.WriteValue(this.shi503_reserved);
			encoder.WriteUniquePointer(this.shi503_security_descriptor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.shi503_netname = decoder.ReadUniquePointer<string>();
			this.shi503_type = decoder.ReadUInt32();
			this.shi503_remark = decoder.ReadUniquePointer<string>();
			this.shi503_permissions = decoder.ReadUInt32();
			this.shi503_max_uses = decoder.ReadUInt32();
			this.shi503_current_uses = decoder.ReadUInt32();
			this.shi503_path = decoder.ReadUniquePointer<string>();
			this.shi503_passwd = decoder.ReadUniquePointer<string>();
			this.shi503_servername = decoder.ReadUniquePointer<string>();
			this.shi503_reserved = decoder.ReadUInt32();
			this.shi503_security_descriptor = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.shi503_netname is not null)
			{
				encoder.WriteWideCharString(this.shi503_netname.value);
			}

			if (this.shi503_remark is not null)
			{
				encoder.WriteWideCharString(this.shi503_remark.value);
			}

			if (this.shi503_path is not null)
			{
				encoder.WriteWideCharString(this.shi503_path.value);
			}

			if (this.shi503_passwd is not null)
			{
				encoder.WriteWideCharString(this.shi503_passwd.value);
			}

			if (this.shi503_servername is not null)
			{
				encoder.WriteWideCharString(this.shi503_servername.value);
			}

			if (this.shi503_security_descriptor is not null)
			{
				encoder.WriteArrayHeader(this.shi503_security_descriptor.value);
				for (int i = 0; i < this.shi503_security_descriptor.value.Length; i++)
				{
					byte elem_0 = this.shi503_security_descriptor.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.shi503_netname is not null)
			{
				this.shi503_netname.value = decoder.ReadWideCharString();
			}

			if (this.shi503_remark is not null)
			{
				this.shi503_remark.value = decoder.ReadWideCharString();
			}

			if (this.shi503_path is not null)
			{
				this.shi503_path.value = decoder.ReadWideCharString();
			}

			if (this.shi503_passwd is not null)
			{
				this.shi503_passwd.value = decoder.ReadWideCharString();
			}

			if (this.shi503_servername is not null)
			{
				this.shi503_servername.value = decoder.ReadWideCharString();
			}

			if (this.shi503_security_descriptor is not null)
			{
				this.shi503_security_descriptor.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.shi503_security_descriptor.value.Length; i++)
				{
					byte elem_0 = this.shi503_security_descriptor.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.shi503_security_descriptor.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_503_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_503_I[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SHARE_INFO_503_I[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_503_I elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_503_I elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_503_I>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_503_I elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_503_I>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_503_I elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_503_I>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_1501_I : IRpcFixedStruct
	{
		public uint shi1501_reserved;
		public RpcPointer<byte[]> shi1501_security_descriptor;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.shi1501_reserved);
			encoder.WriteUniquePointer(this.shi1501_security_descriptor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.shi1501_reserved = decoder.ReadUInt32();
			this.shi1501_security_descriptor = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.shi1501_security_descriptor is not null)
			{
				encoder.WriteArrayHeader(this.shi1501_security_descriptor.value);
				for (int i = 0; i < this.shi1501_security_descriptor.value.Length; i++)
				{
					byte elem_0 = this.shi1501_security_descriptor.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.shi1501_security_descriptor is not null)
			{
				this.shi1501_security_descriptor.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.shi1501_security_descriptor.value.Length; i++)
				{
					byte elem_0 = this.shi1501_security_descriptor.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.shi1501_security_descriptor.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_0 : IRpcFixedStruct
	{
		public RpcPointer<string> shi0_netname;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.shi0_netname);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.shi0_netname = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.shi0_netname is not null)
			{
				encoder.WriteWideCharString(this.shi0_netname.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.shi0_netname is not null)
			{
				this.shi0_netname.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_0_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_0[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SHARE_INFO_0[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_0>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_0>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_1 : IRpcFixedStruct
	{
		public RpcPointer<string> shi1_netname;
		public uint shi1_type;
		public RpcPointer<string> shi1_remark;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.shi1_netname);
			encoder.WriteValue(this.shi1_type);
			encoder.WriteUniquePointer(this.shi1_remark);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.shi1_netname = decoder.ReadUniquePointer<string>();
			this.shi1_type = decoder.ReadUInt32();
			this.shi1_remark = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.shi1_netname is not null)
			{
				encoder.WriteWideCharString(this.shi1_netname.value);
			}

			if (this.shi1_remark is not null)
			{
				encoder.WriteWideCharString(this.shi1_remark.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.shi1_netname is not null)
			{
				this.shi1_netname.value = decoder.ReadWideCharString();
			}

			if (this.shi1_remark is not null)
			{
				this.shi1_remark.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_1_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_1[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SHARE_INFO_1[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_1>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_1 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_1>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_1 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_1>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_2 : IRpcFixedStruct
	{
		public RpcPointer<string> shi2_netname;
		public uint shi2_type;
		public RpcPointer<string> shi2_remark;
		public uint shi2_permissions;
		public uint shi2_max_uses;
		public uint shi2_current_uses;
		public RpcPointer<string> shi2_path;
		public RpcPointer<string> shi2_passwd;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.shi2_netname);
			encoder.WriteValue(this.shi2_type);
			encoder.WriteUniquePointer(this.shi2_remark);
			encoder.WriteValue(this.shi2_permissions);
			encoder.WriteValue(this.shi2_max_uses);
			encoder.WriteValue(this.shi2_current_uses);
			encoder.WriteUniquePointer(this.shi2_path);
			encoder.WriteUniquePointer(this.shi2_passwd);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.shi2_netname = decoder.ReadUniquePointer<string>();
			this.shi2_type = decoder.ReadUInt32();
			this.shi2_remark = decoder.ReadUniquePointer<string>();
			this.shi2_permissions = decoder.ReadUInt32();
			this.shi2_max_uses = decoder.ReadUInt32();
			this.shi2_current_uses = decoder.ReadUInt32();
			this.shi2_path = decoder.ReadUniquePointer<string>();
			this.shi2_passwd = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.shi2_netname is not null)
			{
				encoder.WriteWideCharString(this.shi2_netname.value);
			}

			if (this.shi2_remark is not null)
			{
				encoder.WriteWideCharString(this.shi2_remark.value);
			}

			if (this.shi2_path is not null)
			{
				encoder.WriteWideCharString(this.shi2_path.value);
			}

			if (this.shi2_passwd is not null)
			{
				encoder.WriteWideCharString(this.shi2_passwd.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.shi2_netname is not null)
			{
				this.shi2_netname.value = decoder.ReadWideCharString();
			}

			if (this.shi2_remark is not null)
			{
				this.shi2_remark.value = decoder.ReadWideCharString();
			}

			if (this.shi2_path is not null)
			{
				this.shi2_path.value = decoder.ReadWideCharString();
			}

			if (this.shi2_passwd is not null)
			{
				this.shi2_passwd.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_2_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_2[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SHARE_INFO_2[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_2>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_2 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_2>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_2 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_2>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_501 : IRpcFixedStruct
	{
		public RpcPointer<string> shi501_netname;
		public uint shi501_type;
		public RpcPointer<string> shi501_remark;
		public uint shi501_flags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.shi501_netname);
			encoder.WriteValue(this.shi501_type);
			encoder.WriteUniquePointer(this.shi501_remark);
			encoder.WriteValue(this.shi501_flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.shi501_netname = decoder.ReadUniquePointer<string>();
			this.shi501_type = decoder.ReadUInt32();
			this.shi501_remark = decoder.ReadUniquePointer<string>();
			this.shi501_flags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.shi501_netname is not null)
			{
				encoder.WriteWideCharString(this.shi501_netname.value);
			}

			if (this.shi501_remark is not null)
			{
				encoder.WriteWideCharString(this.shi501_remark.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.shi501_netname is not null)
			{
				this.shi501_netname.value = decoder.ReadWideCharString();
			}

			if (this.shi501_remark is not null)
			{
				this.shi501_remark.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_501_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_501[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SHARE_INFO_501[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_501 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_501 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_501>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_501 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_501>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_501 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_501>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_502_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SHARE_INFO_502_I[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SHARE_INFO_502_I[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_502_I elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_502_I elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SHARE_INFO_502_I>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_502_I elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SHARE_INFO_502_I>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SHARE_INFO_502_I elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SHARE_INFO_502_I>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_ENUM_UNION : IRpcFixedStruct
	{
		public uint Level;
		public RpcPointer<SHARE_INFO_0_CONTAINER> Level0;
		public RpcPointer<SHARE_INFO_1_CONTAINER> Level1;
		public RpcPointer<SHARE_INFO_2_CONTAINER> Level2;
		public RpcPointer<SHARE_INFO_501_CONTAINER> Level501;
		public RpcPointer<SHARE_INFO_502_CONTAINER> Level502;
		public RpcPointer<SHARE_INFO_503_CONTAINER> Level503;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.Level);
			switch ((uint)this.Level)
			{
				case 0U:
					encoder.WriteUniquePointer(this.Level0);
					break;
				case 1U:
					encoder.WriteUniquePointer(this.Level1);
					break;
				case 2U:
					encoder.WriteUniquePointer(this.Level2);
					break;
				case 501U:
					encoder.WriteUniquePointer(this.Level501);
					break;
				case 502U:
					encoder.WriteUniquePointer(this.Level502);
					break;
				case 503U:
					encoder.WriteUniquePointer(this.Level503);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.Level = decoder.ReadUInt32();
			switch ((uint)this.Level)
			{
				case 0U:
					this.Level0 = decoder.ReadUniquePointer<SHARE_INFO_0_CONTAINER>();
					break;
				case 1U:
					this.Level1 = decoder.ReadUniquePointer<SHARE_INFO_1_CONTAINER>();
					break;
				case 2U:
					this.Level2 = decoder.ReadUniquePointer<SHARE_INFO_2_CONTAINER>();
					break;
				case 501U:
					this.Level501 = decoder.ReadUniquePointer<SHARE_INFO_501_CONTAINER>();
					break;
				case 502U:
					this.Level502 = decoder.ReadUniquePointer<SHARE_INFO_502_CONTAINER>();
					break;
				case 503U:
					this.Level503 = decoder.ReadUniquePointer<SHARE_INFO_503_CONTAINER>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						encoder.WriteFixedStruct(this.Level0.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level0.value);
					}

					break;
				case 1U:
					if (this.Level1 is not null)
					{
						encoder.WriteFixedStruct(this.Level1.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level1.value);
					}

					break;
				case 2U:
					if (this.Level2 is not null)
					{
						encoder.WriteFixedStruct(this.Level2.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level2.value);
					}

					break;
				case 501U:
					if (this.Level501 is not null)
					{
						encoder.WriteFixedStruct(this.Level501.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level501.value);
					}

					break;
				case 502U:
					if (this.Level502 is not null)
					{
						encoder.WriteFixedStruct(this.Level502.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level502.value);
					}

					break;
				case 503U:
					if (this.Level503 is not null)
					{
						encoder.WriteFixedStruct(this.Level503.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level503.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						this.Level0.value = decoder.ReadFixedStruct<SHARE_INFO_0_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_0_CONTAINER>(ref this.Level0.value);
					}

					break;
				case 1U:
					if (this.Level1 is not null)
					{
						this.Level1.value = decoder.ReadFixedStruct<SHARE_INFO_1_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_1_CONTAINER>(ref this.Level1.value);
					}

					break;
				case 2U:
					if (this.Level2 is not null)
					{
						this.Level2.value = decoder.ReadFixedStruct<SHARE_INFO_2_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_2_CONTAINER>(ref this.Level2.value);
					}

					break;
				case 501U:
					if (this.Level501 is not null)
					{
						this.Level501.value = decoder.ReadFixedStruct<SHARE_INFO_501_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_501_CONTAINER>(ref this.Level501.value);
					}

					break;
				case 502U:
					if (this.Level502 is not null)
					{
						this.Level502.value = decoder.ReadFixedStruct<SHARE_INFO_502_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_502_CONTAINER>(ref this.Level502.value);
					}

					break;
				case 503U:
					if (this.Level503 is not null)
					{
						this.Level503.value = decoder.ReadFixedStruct<SHARE_INFO_503_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_503_CONTAINER>(ref this.Level503.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_ENUM_STRUCT : IRpcFixedStruct
	{
		public uint Level;
		public SHARE_ENUM_UNION ShareInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.ShareInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.ShareInfo = decoder.ReadUnion<SHARE_ENUM_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ShareInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SHARE_ENUM_UNION>(ref this.ShareInfo);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_1004 : IRpcFixedStruct
	{
		public RpcPointer<string> shi1004_remark;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.shi1004_remark);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.shi1004_remark = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.shi1004_remark is not null)
			{
				encoder.WriteWideCharString(this.shi1004_remark.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.shi1004_remark is not null)
			{
				this.shi1004_remark.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SHARE_INFO_1006 : IRpcFixedStruct
	{
		public uint shi1006_max_uses;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.shi1006_max_uses);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.shi1006_max_uses = decoder.ReadUInt32();
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
	public partial struct SHARE_INFO_1005 : IRpcFixedStruct
	{
		public uint shi1005_flags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.shi1005_flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.shi1005_flags = decoder.ReadUInt32();
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
	public partial struct SHARE_INFO : IRpcFixedStruct
	{
		public uint unionSwitch;
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
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					encoder.WriteUniquePointer(this.ShareInfo0);
					break;
				case 1U:
					encoder.WriteUniquePointer(this.ShareInfo1);
					break;
				case 2U:
					encoder.WriteUniquePointer(this.ShareInfo2);
					break;
				case 502U:
					encoder.WriteUniquePointer(this.ShareInfo502);
					break;
				case 1004U:
					encoder.WriteUniquePointer(this.ShareInfo1004);
					break;
				case 1006U:
					encoder.WriteUniquePointer(this.ShareInfo1006);
					break;
				case 1501U:
					encoder.WriteUniquePointer(this.ShareInfo1501);
					break;
				case 1005U:
					encoder.WriteUniquePointer(this.ShareInfo1005);
					break;
				case 501U:
					encoder.WriteUniquePointer(this.ShareInfo501);
					break;
				case 503U:
					encoder.WriteUniquePointer(this.ShareInfo503);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					this.ShareInfo0 = decoder.ReadUniquePointer<SHARE_INFO_0>();
					break;
				case 1U:
					this.ShareInfo1 = decoder.ReadUniquePointer<SHARE_INFO_1>();
					break;
				case 2U:
					this.ShareInfo2 = decoder.ReadUniquePointer<SHARE_INFO_2>();
					break;
				case 502U:
					this.ShareInfo502 = decoder.ReadUniquePointer<SHARE_INFO_502_I>();
					break;
				case 1004U:
					this.ShareInfo1004 = decoder.ReadUniquePointer<SHARE_INFO_1004>();
					break;
				case 1006U:
					this.ShareInfo1006 = decoder.ReadUniquePointer<SHARE_INFO_1006>();
					break;
				case 1501U:
					this.ShareInfo1501 = decoder.ReadUniquePointer<SHARE_INFO_1501_I>();
					break;
				case 1005U:
					this.ShareInfo1005 = decoder.ReadUniquePointer<SHARE_INFO_1005>();
					break;
				case 501U:
					this.ShareInfo501 = decoder.ReadUniquePointer<SHARE_INFO_501>();
					break;
				case 503U:
					this.ShareInfo503 = decoder.ReadUniquePointer<SHARE_INFO_503_I>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					if (this.ShareInfo0 is not null)
					{
						encoder.WriteFixedStruct(this.ShareInfo0.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ShareInfo0.value);
					}

					break;
				case 1U:
					if (this.ShareInfo1 is not null)
					{
						encoder.WriteFixedStruct(this.ShareInfo1.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ShareInfo1.value);
					}

					break;
				case 2U:
					if (this.ShareInfo2 is not null)
					{
						encoder.WriteFixedStruct(this.ShareInfo2.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ShareInfo2.value);
					}

					break;
				case 502U:
					if (this.ShareInfo502 is not null)
					{
						encoder.WriteFixedStruct(this.ShareInfo502.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ShareInfo502.value);
					}

					break;
				case 1004U:
					if (this.ShareInfo1004 is not null)
					{
						encoder.WriteFixedStruct(this.ShareInfo1004.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ShareInfo1004.value);
					}

					break;
				case 1006U:
					if (this.ShareInfo1006 is not null)
					{
						encoder.WriteFixedStruct(this.ShareInfo1006.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ShareInfo1006.value);
					}

					break;
				case 1501U:
					if (this.ShareInfo1501 is not null)
					{
						encoder.WriteFixedStruct(this.ShareInfo1501.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ShareInfo1501.value);
					}

					break;
				case 1005U:
					if (this.ShareInfo1005 is not null)
					{
						encoder.WriteFixedStruct(this.ShareInfo1005.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ShareInfo1005.value);
					}

					break;
				case 501U:
					if (this.ShareInfo501 is not null)
					{
						encoder.WriteFixedStruct(this.ShareInfo501.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ShareInfo501.value);
					}

					break;
				case 503U:
					if (this.ShareInfo503 is not null)
					{
						encoder.WriteFixedStruct(this.ShareInfo503.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ShareInfo503.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					if (this.ShareInfo0 is not null)
					{
						this.ShareInfo0.value = decoder.ReadFixedStruct<SHARE_INFO_0>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_0>(ref this.ShareInfo0.value);
					}

					break;
				case 1U:
					if (this.ShareInfo1 is not null)
					{
						this.ShareInfo1.value = decoder.ReadFixedStruct<SHARE_INFO_1>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_1>(ref this.ShareInfo1.value);
					}

					break;
				case 2U:
					if (this.ShareInfo2 is not null)
					{
						this.ShareInfo2.value = decoder.ReadFixedStruct<SHARE_INFO_2>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_2>(ref this.ShareInfo2.value);
					}

					break;
				case 502U:
					if (this.ShareInfo502 is not null)
					{
						this.ShareInfo502.value = decoder.ReadFixedStruct<SHARE_INFO_502_I>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_502_I>(ref this.ShareInfo502.value);
					}

					break;
				case 1004U:
					if (this.ShareInfo1004 is not null)
					{
						this.ShareInfo1004.value = decoder.ReadFixedStruct<SHARE_INFO_1004>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_1004>(ref this.ShareInfo1004.value);
					}

					break;
				case 1006U:
					if (this.ShareInfo1006 is not null)
					{
						this.ShareInfo1006.value = decoder.ReadFixedStruct<SHARE_INFO_1006>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SHARE_INFO_1006>(ref this.ShareInfo1006.value);
					}

					break;
				case 1501U:
					if (this.ShareInfo1501 is not null)
					{
						this.ShareInfo1501.value = decoder.ReadFixedStruct<SHARE_INFO_1501_I>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_1501_I>(ref this.ShareInfo1501.value);
					}

					break;
				case 1005U:
					if (this.ShareInfo1005 is not null)
					{
						this.ShareInfo1005.value = decoder.ReadFixedStruct<SHARE_INFO_1005>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SHARE_INFO_1005>(ref this.ShareInfo1005.value);
					}

					break;
				case 501U:
					if (this.ShareInfo501 is not null)
					{
						this.ShareInfo501.value = decoder.ReadFixedStruct<SHARE_INFO_501>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_501>(ref this.ShareInfo501.value);
					}

					break;
				case 503U:
					if (this.ShareInfo503 is not null)
					{
						this.ShareInfo503.value = decoder.ReadFixedStruct<SHARE_INFO_503_I>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SHARE_INFO_503_I>(ref this.ShareInfo503.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_INFO_102 : IRpcFixedStruct
	{
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
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv102_platform_id);
			encoder.WriteUniquePointer(this.sv102_name);
			encoder.WriteValue(this.sv102_version_major);
			encoder.WriteValue(this.sv102_version_minor);
			encoder.WriteValue(this.sv102_type);
			encoder.WriteUniquePointer(this.sv102_comment);
			encoder.WriteValue(this.sv102_users);
			encoder.WriteValue(this.sv102_disc);
			encoder.WriteValue(this.sv102_hidden);
			encoder.WriteValue(this.sv102_announce);
			encoder.WriteValue(this.sv102_anndelta);
			encoder.WriteValue(this.sv102_licenses);
			encoder.WriteUniquePointer(this.sv102_userpath);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv102_platform_id = decoder.ReadUInt32();
			this.sv102_name = decoder.ReadUniquePointer<string>();
			this.sv102_version_major = decoder.ReadUInt32();
			this.sv102_version_minor = decoder.ReadUInt32();
			this.sv102_type = decoder.ReadUInt32();
			this.sv102_comment = decoder.ReadUniquePointer<string>();
			this.sv102_users = decoder.ReadUInt32();
			this.sv102_disc = decoder.ReadInt32();
			this.sv102_hidden = decoder.ReadInt32();
			this.sv102_announce = decoder.ReadUInt32();
			this.sv102_anndelta = decoder.ReadUInt32();
			this.sv102_licenses = decoder.ReadUInt32();
			this.sv102_userpath = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.sv102_name is not null)
			{
				encoder.WriteWideCharString(this.sv102_name.value);
			}

			if (this.sv102_comment is not null)
			{
				encoder.WriteWideCharString(this.sv102_comment.value);
			}

			if (this.sv102_userpath is not null)
			{
				encoder.WriteWideCharString(this.sv102_userpath.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.sv102_name is not null)
			{
				this.sv102_name.value = decoder.ReadWideCharString();
			}

			if (this.sv102_comment is not null)
			{
				this.sv102_comment.value = decoder.ReadWideCharString();
			}

			if (this.sv102_userpath is not null)
			{
				this.sv102_userpath.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_INFO_103 : IRpcFixedStruct
	{
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
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv103_platform_id);
			encoder.WriteUniquePointer(this.sv103_name);
			encoder.WriteValue(this.sv103_version_major);
			encoder.WriteValue(this.sv103_version_minor);
			encoder.WriteValue(this.sv103_type);
			encoder.WriteUniquePointer(this.sv103_comment);
			encoder.WriteValue(this.sv103_users);
			encoder.WriteValue(this.sv103_disc);
			encoder.WriteValue(this.sv103_hidden);
			encoder.WriteValue(this.sv103_announce);
			encoder.WriteValue(this.sv103_anndelta);
			encoder.WriteValue(this.sv103_licenses);
			encoder.WriteUniquePointer(this.sv103_userpath);
			encoder.WriteValue(this.sv103_capabilities);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv103_platform_id = decoder.ReadUInt32();
			this.sv103_name = decoder.ReadUniquePointer<string>();
			this.sv103_version_major = decoder.ReadUInt32();
			this.sv103_version_minor = decoder.ReadUInt32();
			this.sv103_type = decoder.ReadUInt32();
			this.sv103_comment = decoder.ReadUniquePointer<string>();
			this.sv103_users = decoder.ReadUInt32();
			this.sv103_disc = decoder.ReadInt32();
			this.sv103_hidden = decoder.ReadInt32();
			this.sv103_announce = decoder.ReadUInt32();
			this.sv103_anndelta = decoder.ReadUInt32();
			this.sv103_licenses = decoder.ReadUInt32();
			this.sv103_userpath = decoder.ReadUniquePointer<string>();
			this.sv103_capabilities = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.sv103_name is not null)
			{
				encoder.WriteWideCharString(this.sv103_name.value);
			}

			if (this.sv103_comment is not null)
			{
				encoder.WriteWideCharString(this.sv103_comment.value);
			}

			if (this.sv103_userpath is not null)
			{
				encoder.WriteWideCharString(this.sv103_userpath.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.sv103_name is not null)
			{
				this.sv103_name.value = decoder.ReadWideCharString();
			}

			if (this.sv103_comment is not null)
			{
				this.sv103_comment.value = decoder.ReadWideCharString();
			}

			if (this.sv103_userpath is not null)
			{
				this.sv103_userpath.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_INFO_502 : IRpcFixedStruct
	{
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
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
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
	public partial struct SERVER_INFO_503 : IRpcFixedStruct
	{
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
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
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
			encoder.WriteUniquePointer(this.sv503_domain);
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
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
			this.sv503_domain = decoder.ReadUniquePointer<string>();
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.sv503_domain is not null)
			{
				encoder.WriteWideCharString(this.sv503_domain.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.sv503_domain is not null)
			{
				this.sv503_domain.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_INFO_599 : IRpcFixedStruct
	{
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
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
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
			encoder.WriteUniquePointer(this.sv599_domain);
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
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
			this.sv599_domain = decoder.ReadUniquePointer<string>();
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.sv599_domain is not null)
			{
				encoder.WriteWideCharString(this.sv599_domain.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.sv599_domain is not null)
			{
				this.sv599_domain.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_INFO_1005 : IRpcFixedStruct
	{
		public RpcPointer<string> sv1005_comment;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.sv1005_comment);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1005_comment = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.sv1005_comment is not null)
			{
				encoder.WriteWideCharString(this.sv1005_comment.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.sv1005_comment is not null)
			{
				this.sv1005_comment.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_INFO_1107 : IRpcFixedStruct
	{
		public uint sv1107_users;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1107_users);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1107_users = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1010 : IRpcFixedStruct
	{
		public int sv1010_disc;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1010_disc);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1010_disc = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1016 : IRpcFixedStruct
	{
		public int sv1016_hidden;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1016_hidden);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1016_hidden = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1017 : IRpcFixedStruct
	{
		public uint sv1017_announce;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1017_announce);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1017_announce = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1018 : IRpcFixedStruct
	{
		public uint sv1018_anndelta;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1018_anndelta);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1018_anndelta = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1501 : IRpcFixedStruct
	{
		public uint sv1501_sessopens;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1501_sessopens);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1501_sessopens = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1502 : IRpcFixedStruct
	{
		public uint sv1502_sessvcs;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1502_sessvcs);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1502_sessvcs = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1503 : IRpcFixedStruct
	{
		public uint sv1503_opensearch;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1503_opensearch);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1503_opensearch = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1506 : IRpcFixedStruct
	{
		public uint sv1506_maxworkitems;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1506_maxworkitems);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1506_maxworkitems = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1510 : IRpcFixedStruct
	{
		public uint sv1510_sessusers;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1510_sessusers);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1510_sessusers = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1511 : IRpcFixedStruct
	{
		public uint sv1511_sessconns;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1511_sessconns);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1511_sessconns = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1512 : IRpcFixedStruct
	{
		public uint sv1512_maxnonpagedmemoryusage;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1512_maxnonpagedmemoryusage);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1512_maxnonpagedmemoryusage = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1513 : IRpcFixedStruct
	{
		public uint sv1513_maxpagedmemoryusage;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1513_maxpagedmemoryusage);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1513_maxpagedmemoryusage = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1514 : IRpcFixedStruct
	{
		public int sv1514_enablesoftcompat;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1514_enablesoftcompat);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1514_enablesoftcompat = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1515 : IRpcFixedStruct
	{
		public int sv1515_enableforcedlogoff;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1515_enableforcedlogoff);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1515_enableforcedlogoff = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1516 : IRpcFixedStruct
	{
		public int sv1516_timesource;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1516_timesource);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1516_timesource = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1518 : IRpcFixedStruct
	{
		public int sv1518_lmannounce;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1518_lmannounce);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1518_lmannounce = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1523 : IRpcFixedStruct
	{
		public uint sv1523_maxkeepsearch;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1523_maxkeepsearch);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1523_maxkeepsearch = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1528 : IRpcFixedStruct
	{
		public uint sv1528_scavtimeout;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1528_scavtimeout);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1528_scavtimeout = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1529 : IRpcFixedStruct
	{
		public uint sv1529_minrcvqueue;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1529_minrcvqueue);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1529_minrcvqueue = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1530 : IRpcFixedStruct
	{
		public uint sv1530_minfreeworkitems;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1530_minfreeworkitems);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1530_minfreeworkitems = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1533 : IRpcFixedStruct
	{
		public uint sv1533_maxmpxct;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1533_maxmpxct);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1533_maxmpxct = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1534 : IRpcFixedStruct
	{
		public uint sv1534_oplockbreakwait;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1534_oplockbreakwait);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1534_oplockbreakwait = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1535 : IRpcFixedStruct
	{
		public uint sv1535_oplockbreakresponsewait;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1535_oplockbreakresponsewait);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1535_oplockbreakresponsewait = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1536 : IRpcFixedStruct
	{
		public int sv1536_enableoplocks;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1536_enableoplocks);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1536_enableoplocks = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1538 : IRpcFixedStruct
	{
		public int sv1538_enablefcbopens;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1538_enablefcbopens);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1538_enablefcbopens = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1539 : IRpcFixedStruct
	{
		public int sv1539_enableraw;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1539_enableraw);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1539_enableraw = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1540 : IRpcFixedStruct
	{
		public int sv1540_enablesharednetdrives;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1540_enablesharednetdrives);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1540_enablesharednetdrives = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1541 : IRpcFixedStruct
	{
		public int sv1541_minfreeconnections;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1541_minfreeconnections);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1541_minfreeconnections = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1542 : IRpcFixedStruct
	{
		public int sv1542_maxfreeconnections;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1542_maxfreeconnections);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1542_maxfreeconnections = decoder.ReadInt32();
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
	public partial struct SERVER_INFO_1543 : IRpcFixedStruct
	{
		public uint sv1543_initsesstable;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1543_initsesstable);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1543_initsesstable = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1544 : IRpcFixedStruct
	{
		public uint sv1544_initconntable;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1544_initconntable);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1544_initconntable = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1545 : IRpcFixedStruct
	{
		public uint sv1545_initfiletable;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1545_initfiletable);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1545_initfiletable = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1546 : IRpcFixedStruct
	{
		public uint sv1546_initsearchtable;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1546_initsearchtable);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1546_initsearchtable = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1547 : IRpcFixedStruct
	{
		public uint sv1547_alertschedule;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1547_alertschedule);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1547_alertschedule = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1548 : IRpcFixedStruct
	{
		public uint sv1548_errorthreshold;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1548_errorthreshold);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1548_errorthreshold = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1549 : IRpcFixedStruct
	{
		public uint sv1549_networkerrorthreshold;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1549_networkerrorthreshold);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1549_networkerrorthreshold = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1550 : IRpcFixedStruct
	{
		public uint sv1550_diskspacethreshold;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1550_diskspacethreshold);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1550_diskspacethreshold = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1552 : IRpcFixedStruct
	{
		public uint sv1552_maxlinkdelay;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1552_maxlinkdelay);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1552_maxlinkdelay = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1553 : IRpcFixedStruct
	{
		public uint sv1553_minlinkthroughput;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1553_minlinkthroughput);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1553_minlinkthroughput = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1554 : IRpcFixedStruct
	{
		public uint sv1554_linkinfovalidtime;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1554_linkinfovalidtime);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1554_linkinfovalidtime = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1555 : IRpcFixedStruct
	{
		public uint sv1555_scavqosinfoupdatetime;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1555_scavqosinfoupdatetime);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1555_scavqosinfoupdatetime = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO_1556 : IRpcFixedStruct
	{
		public uint sv1556_maxworkitemidletime;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.sv1556_maxworkitemidletime);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.sv1556_maxworkitemidletime = decoder.ReadUInt32();
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
	public partial struct SERVER_INFO : IRpcFixedStruct
	{
		public uint unionSwitch;
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
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 100U:
					encoder.WriteUniquePointer(this.ServerInfo100);
					break;
				case 101U:
					encoder.WriteUniquePointer(this.ServerInfo101);
					break;
				case 102U:
					encoder.WriteUniquePointer(this.ServerInfo102);
					break;
				case 103U:
					encoder.WriteUniquePointer(this.ServerInfo103);
					break;
				case 502U:
					encoder.WriteUniquePointer(this.ServerInfo502);
					break;
				case 503U:
					encoder.WriteUniquePointer(this.ServerInfo503);
					break;
				case 599U:
					encoder.WriteUniquePointer(this.ServerInfo599);
					break;
				case 1005U:
					encoder.WriteUniquePointer(this.ServerInfo1005);
					break;
				case 1107U:
					encoder.WriteUniquePointer(this.ServerInfo1107);
					break;
				case 1010U:
					encoder.WriteUniquePointer(this.ServerInfo1010);
					break;
				case 1016U:
					encoder.WriteUniquePointer(this.ServerInfo1016);
					break;
				case 1017U:
					encoder.WriteUniquePointer(this.ServerInfo1017);
					break;
				case 1018U:
					encoder.WriteUniquePointer(this.ServerInfo1018);
					break;
				case 1501U:
					encoder.WriteUniquePointer(this.ServerInfo1501);
					break;
				case 1502U:
					encoder.WriteUniquePointer(this.ServerInfo1502);
					break;
				case 1503U:
					encoder.WriteUniquePointer(this.ServerInfo1503);
					break;
				case 1506U:
					encoder.WriteUniquePointer(this.ServerInfo1506);
					break;
				case 1510U:
					encoder.WriteUniquePointer(this.ServerInfo1510);
					break;
				case 1511U:
					encoder.WriteUniquePointer(this.ServerInfo1511);
					break;
				case 1512U:
					encoder.WriteUniquePointer(this.ServerInfo1512);
					break;
				case 1513U:
					encoder.WriteUniquePointer(this.ServerInfo1513);
					break;
				case 1514U:
					encoder.WriteUniquePointer(this.ServerInfo1514);
					break;
				case 1515U:
					encoder.WriteUniquePointer(this.ServerInfo1515);
					break;
				case 1516U:
					encoder.WriteUniquePointer(this.ServerInfo1516);
					break;
				case 1518U:
					encoder.WriteUniquePointer(this.ServerInfo1518);
					break;
				case 1523U:
					encoder.WriteUniquePointer(this.ServerInfo1523);
					break;
				case 1528U:
					encoder.WriteUniquePointer(this.ServerInfo1528);
					break;
				case 1529U:
					encoder.WriteUniquePointer(this.ServerInfo1529);
					break;
				case 1530U:
					encoder.WriteUniquePointer(this.ServerInfo1530);
					break;
				case 1533U:
					encoder.WriteUniquePointer(this.ServerInfo1533);
					break;
				case 1534U:
					encoder.WriteUniquePointer(this.ServerInfo1534);
					break;
				case 1535U:
					encoder.WriteUniquePointer(this.ServerInfo1535);
					break;
				case 1536U:
					encoder.WriteUniquePointer(this.ServerInfo1536);
					break;
				case 1538U:
					encoder.WriteUniquePointer(this.ServerInfo1538);
					break;
				case 1539U:
					encoder.WriteUniquePointer(this.ServerInfo1539);
					break;
				case 1540U:
					encoder.WriteUniquePointer(this.ServerInfo1540);
					break;
				case 1541U:
					encoder.WriteUniquePointer(this.ServerInfo1541);
					break;
				case 1542U:
					encoder.WriteUniquePointer(this.ServerInfo1542);
					break;
				case 1543U:
					encoder.WriteUniquePointer(this.ServerInfo1543);
					break;
				case 1544U:
					encoder.WriteUniquePointer(this.ServerInfo1544);
					break;
				case 1545U:
					encoder.WriteUniquePointer(this.ServerInfo1545);
					break;
				case 1546U:
					encoder.WriteUniquePointer(this.ServerInfo1546);
					break;
				case 1547U:
					encoder.WriteUniquePointer(this.ServerInfo1547);
					break;
				case 1548U:
					encoder.WriteUniquePointer(this.ServerInfo1548);
					break;
				case 1549U:
					encoder.WriteUniquePointer(this.ServerInfo1549);
					break;
				case 1550U:
					encoder.WriteUniquePointer(this.ServerInfo1550);
					break;
				case 1552U:
					encoder.WriteUniquePointer(this.ServerInfo1552);
					break;
				case 1553U:
					encoder.WriteUniquePointer(this.ServerInfo1553);
					break;
				case 1554U:
					encoder.WriteUniquePointer(this.ServerInfo1554);
					break;
				case 1555U:
					encoder.WriteUniquePointer(this.ServerInfo1555);
					break;
				case 1556U:
					encoder.WriteUniquePointer(this.ServerInfo1556);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 100U:
					this.ServerInfo100 = decoder.ReadUniquePointer<ms_dtyp.SERVER_INFO_100>();
					break;
				case 101U:
					this.ServerInfo101 = decoder.ReadUniquePointer<ms_dtyp.SERVER_INFO_101>();
					break;
				case 102U:
					this.ServerInfo102 = decoder.ReadUniquePointer<SERVER_INFO_102>();
					break;
				case 103U:
					this.ServerInfo103 = decoder.ReadUniquePointer<SERVER_INFO_103>();
					break;
				case 502U:
					this.ServerInfo502 = decoder.ReadUniquePointer<SERVER_INFO_502>();
					break;
				case 503U:
					this.ServerInfo503 = decoder.ReadUniquePointer<SERVER_INFO_503>();
					break;
				case 599U:
					this.ServerInfo599 = decoder.ReadUniquePointer<SERVER_INFO_599>();
					break;
				case 1005U:
					this.ServerInfo1005 = decoder.ReadUniquePointer<SERVER_INFO_1005>();
					break;
				case 1107U:
					this.ServerInfo1107 = decoder.ReadUniquePointer<SERVER_INFO_1107>();
					break;
				case 1010U:
					this.ServerInfo1010 = decoder.ReadUniquePointer<SERVER_INFO_1010>();
					break;
				case 1016U:
					this.ServerInfo1016 = decoder.ReadUniquePointer<SERVER_INFO_1016>();
					break;
				case 1017U:
					this.ServerInfo1017 = decoder.ReadUniquePointer<SERVER_INFO_1017>();
					break;
				case 1018U:
					this.ServerInfo1018 = decoder.ReadUniquePointer<SERVER_INFO_1018>();
					break;
				case 1501U:
					this.ServerInfo1501 = decoder.ReadUniquePointer<SERVER_INFO_1501>();
					break;
				case 1502U:
					this.ServerInfo1502 = decoder.ReadUniquePointer<SERVER_INFO_1502>();
					break;
				case 1503U:
					this.ServerInfo1503 = decoder.ReadUniquePointer<SERVER_INFO_1503>();
					break;
				case 1506U:
					this.ServerInfo1506 = decoder.ReadUniquePointer<SERVER_INFO_1506>();
					break;
				case 1510U:
					this.ServerInfo1510 = decoder.ReadUniquePointer<SERVER_INFO_1510>();
					break;
				case 1511U:
					this.ServerInfo1511 = decoder.ReadUniquePointer<SERVER_INFO_1511>();
					break;
				case 1512U:
					this.ServerInfo1512 = decoder.ReadUniquePointer<SERVER_INFO_1512>();
					break;
				case 1513U:
					this.ServerInfo1513 = decoder.ReadUniquePointer<SERVER_INFO_1513>();
					break;
				case 1514U:
					this.ServerInfo1514 = decoder.ReadUniquePointer<SERVER_INFO_1514>();
					break;
				case 1515U:
					this.ServerInfo1515 = decoder.ReadUniquePointer<SERVER_INFO_1515>();
					break;
				case 1516U:
					this.ServerInfo1516 = decoder.ReadUniquePointer<SERVER_INFO_1516>();
					break;
				case 1518U:
					this.ServerInfo1518 = decoder.ReadUniquePointer<SERVER_INFO_1518>();
					break;
				case 1523U:
					this.ServerInfo1523 = decoder.ReadUniquePointer<SERVER_INFO_1523>();
					break;
				case 1528U:
					this.ServerInfo1528 = decoder.ReadUniquePointer<SERVER_INFO_1528>();
					break;
				case 1529U:
					this.ServerInfo1529 = decoder.ReadUniquePointer<SERVER_INFO_1529>();
					break;
				case 1530U:
					this.ServerInfo1530 = decoder.ReadUniquePointer<SERVER_INFO_1530>();
					break;
				case 1533U:
					this.ServerInfo1533 = decoder.ReadUniquePointer<SERVER_INFO_1533>();
					break;
				case 1534U:
					this.ServerInfo1534 = decoder.ReadUniquePointer<SERVER_INFO_1534>();
					break;
				case 1535U:
					this.ServerInfo1535 = decoder.ReadUniquePointer<SERVER_INFO_1535>();
					break;
				case 1536U:
					this.ServerInfo1536 = decoder.ReadUniquePointer<SERVER_INFO_1536>();
					break;
				case 1538U:
					this.ServerInfo1538 = decoder.ReadUniquePointer<SERVER_INFO_1538>();
					break;
				case 1539U:
					this.ServerInfo1539 = decoder.ReadUniquePointer<SERVER_INFO_1539>();
					break;
				case 1540U:
					this.ServerInfo1540 = decoder.ReadUniquePointer<SERVER_INFO_1540>();
					break;
				case 1541U:
					this.ServerInfo1541 = decoder.ReadUniquePointer<SERVER_INFO_1541>();
					break;
				case 1542U:
					this.ServerInfo1542 = decoder.ReadUniquePointer<SERVER_INFO_1542>();
					break;
				case 1543U:
					this.ServerInfo1543 = decoder.ReadUniquePointer<SERVER_INFO_1543>();
					break;
				case 1544U:
					this.ServerInfo1544 = decoder.ReadUniquePointer<SERVER_INFO_1544>();
					break;
				case 1545U:
					this.ServerInfo1545 = decoder.ReadUniquePointer<SERVER_INFO_1545>();
					break;
				case 1546U:
					this.ServerInfo1546 = decoder.ReadUniquePointer<SERVER_INFO_1546>();
					break;
				case 1547U:
					this.ServerInfo1547 = decoder.ReadUniquePointer<SERVER_INFO_1547>();
					break;
				case 1548U:
					this.ServerInfo1548 = decoder.ReadUniquePointer<SERVER_INFO_1548>();
					break;
				case 1549U:
					this.ServerInfo1549 = decoder.ReadUniquePointer<SERVER_INFO_1549>();
					break;
				case 1550U:
					this.ServerInfo1550 = decoder.ReadUniquePointer<SERVER_INFO_1550>();
					break;
				case 1552U:
					this.ServerInfo1552 = decoder.ReadUniquePointer<SERVER_INFO_1552>();
					break;
				case 1553U:
					this.ServerInfo1553 = decoder.ReadUniquePointer<SERVER_INFO_1553>();
					break;
				case 1554U:
					this.ServerInfo1554 = decoder.ReadUniquePointer<SERVER_INFO_1554>();
					break;
				case 1555U:
					this.ServerInfo1555 = decoder.ReadUniquePointer<SERVER_INFO_1555>();
					break;
				case 1556U:
					this.ServerInfo1556 = decoder.ReadUniquePointer<SERVER_INFO_1556>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 100U:
					if (this.ServerInfo100 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo100.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ServerInfo100.value);
					}

					break;
				case 101U:
					if (this.ServerInfo101 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo101.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ServerInfo101.value);
					}

					break;
				case 102U:
					if (this.ServerInfo102 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo102.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ServerInfo102.value);
					}

					break;
				case 103U:
					if (this.ServerInfo103 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo103.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ServerInfo103.value);
					}

					break;
				case 502U:
					if (this.ServerInfo502 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo502.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo502.value);
					}

					break;
				case 503U:
					if (this.ServerInfo503 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo503.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ServerInfo503.value);
					}

					break;
				case 599U:
					if (this.ServerInfo599 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo599.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ServerInfo599.value);
					}

					break;
				case 1005U:
					if (this.ServerInfo1005 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1005.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ServerInfo1005.value);
					}

					break;
				case 1107U:
					if (this.ServerInfo1107 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1107.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1107.value);
					}

					break;
				case 1010U:
					if (this.ServerInfo1010 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1010.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1010.value);
					}

					break;
				case 1016U:
					if (this.ServerInfo1016 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1016.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1016.value);
					}

					break;
				case 1017U:
					if (this.ServerInfo1017 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1017.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1017.value);
					}

					break;
				case 1018U:
					if (this.ServerInfo1018 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1018.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1018.value);
					}

					break;
				case 1501U:
					if (this.ServerInfo1501 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1501.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1501.value);
					}

					break;
				case 1502U:
					if (this.ServerInfo1502 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1502.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1502.value);
					}

					break;
				case 1503U:
					if (this.ServerInfo1503 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1503.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1503.value);
					}

					break;
				case 1506U:
					if (this.ServerInfo1506 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1506.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1506.value);
					}

					break;
				case 1510U:
					if (this.ServerInfo1510 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1510.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1510.value);
					}

					break;
				case 1511U:
					if (this.ServerInfo1511 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1511.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1511.value);
					}

					break;
				case 1512U:
					if (this.ServerInfo1512 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1512.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1512.value);
					}

					break;
				case 1513U:
					if (this.ServerInfo1513 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1513.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1513.value);
					}

					break;
				case 1514U:
					if (this.ServerInfo1514 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1514.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1514.value);
					}

					break;
				case 1515U:
					if (this.ServerInfo1515 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1515.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1515.value);
					}

					break;
				case 1516U:
					if (this.ServerInfo1516 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1516.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1516.value);
					}

					break;
				case 1518U:
					if (this.ServerInfo1518 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1518.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1518.value);
					}

					break;
				case 1523U:
					if (this.ServerInfo1523 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1523.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1523.value);
					}

					break;
				case 1528U:
					if (this.ServerInfo1528 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1528.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1528.value);
					}

					break;
				case 1529U:
					if (this.ServerInfo1529 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1529.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1529.value);
					}

					break;
				case 1530U:
					if (this.ServerInfo1530 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1530.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1530.value);
					}

					break;
				case 1533U:
					if (this.ServerInfo1533 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1533.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1533.value);
					}

					break;
				case 1534U:
					if (this.ServerInfo1534 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1534.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1534.value);
					}

					break;
				case 1535U:
					if (this.ServerInfo1535 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1535.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1535.value);
					}

					break;
				case 1536U:
					if (this.ServerInfo1536 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1536.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1536.value);
					}

					break;
				case 1538U:
					if (this.ServerInfo1538 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1538.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1538.value);
					}

					break;
				case 1539U:
					if (this.ServerInfo1539 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1539.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1539.value);
					}

					break;
				case 1540U:
					if (this.ServerInfo1540 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1540.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1540.value);
					}

					break;
				case 1541U:
					if (this.ServerInfo1541 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1541.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1541.value);
					}

					break;
				case 1542U:
					if (this.ServerInfo1542 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1542.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1542.value);
					}

					break;
				case 1543U:
					if (this.ServerInfo1543 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1543.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1543.value);
					}

					break;
				case 1544U:
					if (this.ServerInfo1544 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1544.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1544.value);
					}

					break;
				case 1545U:
					if (this.ServerInfo1545 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1545.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1545.value);
					}

					break;
				case 1546U:
					if (this.ServerInfo1546 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1546.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1546.value);
					}

					break;
				case 1547U:
					if (this.ServerInfo1547 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1547.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1547.value);
					}

					break;
				case 1548U:
					if (this.ServerInfo1548 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1548.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1548.value);
					}

					break;
				case 1549U:
					if (this.ServerInfo1549 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1549.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1549.value);
					}

					break;
				case 1550U:
					if (this.ServerInfo1550 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1550.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1550.value);
					}

					break;
				case 1552U:
					if (this.ServerInfo1552 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1552.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1552.value);
					}

					break;
				case 1553U:
					if (this.ServerInfo1553 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1553.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1553.value);
					}

					break;
				case 1554U:
					if (this.ServerInfo1554 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1554.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1554.value);
					}

					break;
				case 1555U:
					if (this.ServerInfo1555 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1555.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1555.value);
					}

					break;
				case 1556U:
					if (this.ServerInfo1556 is not null)
					{
						encoder.WriteFixedStruct(this.ServerInfo1556.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.ServerInfo1556.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 100U:
					if (this.ServerInfo100 is not null)
					{
						this.ServerInfo100.value = decoder.ReadFixedStruct<ms_dtyp.SERVER_INFO_100>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<ms_dtyp.SERVER_INFO_100>(ref this.ServerInfo100.value);
					}

					break;
				case 101U:
					if (this.ServerInfo101 is not null)
					{
						this.ServerInfo101.value = decoder.ReadFixedStruct<ms_dtyp.SERVER_INFO_101>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<ms_dtyp.SERVER_INFO_101>(ref this.ServerInfo101.value);
					}

					break;
				case 102U:
					if (this.ServerInfo102 is not null)
					{
						this.ServerInfo102.value = decoder.ReadFixedStruct<SERVER_INFO_102>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_INFO_102>(ref this.ServerInfo102.value);
					}

					break;
				case 103U:
					if (this.ServerInfo103 is not null)
					{
						this.ServerInfo103.value = decoder.ReadFixedStruct<SERVER_INFO_103>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_INFO_103>(ref this.ServerInfo103.value);
					}

					break;
				case 502U:
					if (this.ServerInfo502 is not null)
					{
						this.ServerInfo502.value = decoder.ReadFixedStruct<SERVER_INFO_502>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_502>(ref this.ServerInfo502.value);
					}

					break;
				case 503U:
					if (this.ServerInfo503 is not null)
					{
						this.ServerInfo503.value = decoder.ReadFixedStruct<SERVER_INFO_503>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_INFO_503>(ref this.ServerInfo503.value);
					}

					break;
				case 599U:
					if (this.ServerInfo599 is not null)
					{
						this.ServerInfo599.value = decoder.ReadFixedStruct<SERVER_INFO_599>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_INFO_599>(ref this.ServerInfo599.value);
					}

					break;
				case 1005U:
					if (this.ServerInfo1005 is not null)
					{
						this.ServerInfo1005.value = decoder.ReadFixedStruct<SERVER_INFO_1005>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_INFO_1005>(ref this.ServerInfo1005.value);
					}

					break;
				case 1107U:
					if (this.ServerInfo1107 is not null)
					{
						this.ServerInfo1107.value = decoder.ReadFixedStruct<SERVER_INFO_1107>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1107>(ref this.ServerInfo1107.value);
					}

					break;
				case 1010U:
					if (this.ServerInfo1010 is not null)
					{
						this.ServerInfo1010.value = decoder.ReadFixedStruct<SERVER_INFO_1010>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1010>(ref this.ServerInfo1010.value);
					}

					break;
				case 1016U:
					if (this.ServerInfo1016 is not null)
					{
						this.ServerInfo1016.value = decoder.ReadFixedStruct<SERVER_INFO_1016>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1016>(ref this.ServerInfo1016.value);
					}

					break;
				case 1017U:
					if (this.ServerInfo1017 is not null)
					{
						this.ServerInfo1017.value = decoder.ReadFixedStruct<SERVER_INFO_1017>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1017>(ref this.ServerInfo1017.value);
					}

					break;
				case 1018U:
					if (this.ServerInfo1018 is not null)
					{
						this.ServerInfo1018.value = decoder.ReadFixedStruct<SERVER_INFO_1018>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1018>(ref this.ServerInfo1018.value);
					}

					break;
				case 1501U:
					if (this.ServerInfo1501 is not null)
					{
						this.ServerInfo1501.value = decoder.ReadFixedStruct<SERVER_INFO_1501>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1501>(ref this.ServerInfo1501.value);
					}

					break;
				case 1502U:
					if (this.ServerInfo1502 is not null)
					{
						this.ServerInfo1502.value = decoder.ReadFixedStruct<SERVER_INFO_1502>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1502>(ref this.ServerInfo1502.value);
					}

					break;
				case 1503U:
					if (this.ServerInfo1503 is not null)
					{
						this.ServerInfo1503.value = decoder.ReadFixedStruct<SERVER_INFO_1503>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1503>(ref this.ServerInfo1503.value);
					}

					break;
				case 1506U:
					if (this.ServerInfo1506 is not null)
					{
						this.ServerInfo1506.value = decoder.ReadFixedStruct<SERVER_INFO_1506>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1506>(ref this.ServerInfo1506.value);
					}

					break;
				case 1510U:
					if (this.ServerInfo1510 is not null)
					{
						this.ServerInfo1510.value = decoder.ReadFixedStruct<SERVER_INFO_1510>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1510>(ref this.ServerInfo1510.value);
					}

					break;
				case 1511U:
					if (this.ServerInfo1511 is not null)
					{
						this.ServerInfo1511.value = decoder.ReadFixedStruct<SERVER_INFO_1511>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1511>(ref this.ServerInfo1511.value);
					}

					break;
				case 1512U:
					if (this.ServerInfo1512 is not null)
					{
						this.ServerInfo1512.value = decoder.ReadFixedStruct<SERVER_INFO_1512>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1512>(ref this.ServerInfo1512.value);
					}

					break;
				case 1513U:
					if (this.ServerInfo1513 is not null)
					{
						this.ServerInfo1513.value = decoder.ReadFixedStruct<SERVER_INFO_1513>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1513>(ref this.ServerInfo1513.value);
					}

					break;
				case 1514U:
					if (this.ServerInfo1514 is not null)
					{
						this.ServerInfo1514.value = decoder.ReadFixedStruct<SERVER_INFO_1514>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1514>(ref this.ServerInfo1514.value);
					}

					break;
				case 1515U:
					if (this.ServerInfo1515 is not null)
					{
						this.ServerInfo1515.value = decoder.ReadFixedStruct<SERVER_INFO_1515>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1515>(ref this.ServerInfo1515.value);
					}

					break;
				case 1516U:
					if (this.ServerInfo1516 is not null)
					{
						this.ServerInfo1516.value = decoder.ReadFixedStruct<SERVER_INFO_1516>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1516>(ref this.ServerInfo1516.value);
					}

					break;
				case 1518U:
					if (this.ServerInfo1518 is not null)
					{
						this.ServerInfo1518.value = decoder.ReadFixedStruct<SERVER_INFO_1518>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1518>(ref this.ServerInfo1518.value);
					}

					break;
				case 1523U:
					if (this.ServerInfo1523 is not null)
					{
						this.ServerInfo1523.value = decoder.ReadFixedStruct<SERVER_INFO_1523>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1523>(ref this.ServerInfo1523.value);
					}

					break;
				case 1528U:
					if (this.ServerInfo1528 is not null)
					{
						this.ServerInfo1528.value = decoder.ReadFixedStruct<SERVER_INFO_1528>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1528>(ref this.ServerInfo1528.value);
					}

					break;
				case 1529U:
					if (this.ServerInfo1529 is not null)
					{
						this.ServerInfo1529.value = decoder.ReadFixedStruct<SERVER_INFO_1529>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1529>(ref this.ServerInfo1529.value);
					}

					break;
				case 1530U:
					if (this.ServerInfo1530 is not null)
					{
						this.ServerInfo1530.value = decoder.ReadFixedStruct<SERVER_INFO_1530>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1530>(ref this.ServerInfo1530.value);
					}

					break;
				case 1533U:
					if (this.ServerInfo1533 is not null)
					{
						this.ServerInfo1533.value = decoder.ReadFixedStruct<SERVER_INFO_1533>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1533>(ref this.ServerInfo1533.value);
					}

					break;
				case 1534U:
					if (this.ServerInfo1534 is not null)
					{
						this.ServerInfo1534.value = decoder.ReadFixedStruct<SERVER_INFO_1534>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1534>(ref this.ServerInfo1534.value);
					}

					break;
				case 1535U:
					if (this.ServerInfo1535 is not null)
					{
						this.ServerInfo1535.value = decoder.ReadFixedStruct<SERVER_INFO_1535>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1535>(ref this.ServerInfo1535.value);
					}

					break;
				case 1536U:
					if (this.ServerInfo1536 is not null)
					{
						this.ServerInfo1536.value = decoder.ReadFixedStruct<SERVER_INFO_1536>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1536>(ref this.ServerInfo1536.value);
					}

					break;
				case 1538U:
					if (this.ServerInfo1538 is not null)
					{
						this.ServerInfo1538.value = decoder.ReadFixedStruct<SERVER_INFO_1538>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1538>(ref this.ServerInfo1538.value);
					}

					break;
				case 1539U:
					if (this.ServerInfo1539 is not null)
					{
						this.ServerInfo1539.value = decoder.ReadFixedStruct<SERVER_INFO_1539>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1539>(ref this.ServerInfo1539.value);
					}

					break;
				case 1540U:
					if (this.ServerInfo1540 is not null)
					{
						this.ServerInfo1540.value = decoder.ReadFixedStruct<SERVER_INFO_1540>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1540>(ref this.ServerInfo1540.value);
					}

					break;
				case 1541U:
					if (this.ServerInfo1541 is not null)
					{
						this.ServerInfo1541.value = decoder.ReadFixedStruct<SERVER_INFO_1541>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1541>(ref this.ServerInfo1541.value);
					}

					break;
				case 1542U:
					if (this.ServerInfo1542 is not null)
					{
						this.ServerInfo1542.value = decoder.ReadFixedStruct<SERVER_INFO_1542>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1542>(ref this.ServerInfo1542.value);
					}

					break;
				case 1543U:
					if (this.ServerInfo1543 is not null)
					{
						this.ServerInfo1543.value = decoder.ReadFixedStruct<SERVER_INFO_1543>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1543>(ref this.ServerInfo1543.value);
					}

					break;
				case 1544U:
					if (this.ServerInfo1544 is not null)
					{
						this.ServerInfo1544.value = decoder.ReadFixedStruct<SERVER_INFO_1544>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1544>(ref this.ServerInfo1544.value);
					}

					break;
				case 1545U:
					if (this.ServerInfo1545 is not null)
					{
						this.ServerInfo1545.value = decoder.ReadFixedStruct<SERVER_INFO_1545>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1545>(ref this.ServerInfo1545.value);
					}

					break;
				case 1546U:
					if (this.ServerInfo1546 is not null)
					{
						this.ServerInfo1546.value = decoder.ReadFixedStruct<SERVER_INFO_1546>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1546>(ref this.ServerInfo1546.value);
					}

					break;
				case 1547U:
					if (this.ServerInfo1547 is not null)
					{
						this.ServerInfo1547.value = decoder.ReadFixedStruct<SERVER_INFO_1547>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1547>(ref this.ServerInfo1547.value);
					}

					break;
				case 1548U:
					if (this.ServerInfo1548 is not null)
					{
						this.ServerInfo1548.value = decoder.ReadFixedStruct<SERVER_INFO_1548>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1548>(ref this.ServerInfo1548.value);
					}

					break;
				case 1549U:
					if (this.ServerInfo1549 is not null)
					{
						this.ServerInfo1549.value = decoder.ReadFixedStruct<SERVER_INFO_1549>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1549>(ref this.ServerInfo1549.value);
					}

					break;
				case 1550U:
					if (this.ServerInfo1550 is not null)
					{
						this.ServerInfo1550.value = decoder.ReadFixedStruct<SERVER_INFO_1550>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1550>(ref this.ServerInfo1550.value);
					}

					break;
				case 1552U:
					if (this.ServerInfo1552 is not null)
					{
						this.ServerInfo1552.value = decoder.ReadFixedStruct<SERVER_INFO_1552>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1552>(ref this.ServerInfo1552.value);
					}

					break;
				case 1553U:
					if (this.ServerInfo1553 is not null)
					{
						this.ServerInfo1553.value = decoder.ReadFixedStruct<SERVER_INFO_1553>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1553>(ref this.ServerInfo1553.value);
					}

					break;
				case 1554U:
					if (this.ServerInfo1554 is not null)
					{
						this.ServerInfo1554.value = decoder.ReadFixedStruct<SERVER_INFO_1554>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1554>(ref this.ServerInfo1554.value);
					}

					break;
				case 1555U:
					if (this.ServerInfo1555 is not null)
					{
						this.ServerInfo1555.value = decoder.ReadFixedStruct<SERVER_INFO_1555>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1555>(ref this.ServerInfo1555.value);
					}

					break;
				case 1556U:
					if (this.ServerInfo1556 is not null)
					{
						this.ServerInfo1556.value = decoder.ReadFixedStruct<SERVER_INFO_1556>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<SERVER_INFO_1556>(ref this.ServerInfo1556.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DISK_INFO : IRpcFixedStruct
	{
		public ArraySegment<char> Disk;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.Disk.Count; i++)
			{
				char elem_0 = this.Disk.Item(i);
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Disk = decoder.ReadArraySegmentHeader<char>(3);
			for (int i = 0; i < this.Disk.Count; i++)
			{
				char elem_0 = this.Disk.Item(i);
				elem_0 = decoder.ReadWideChar();
				this.Disk.Item(i) = elem_0;
			}
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
	public partial struct DISK_ENUM_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<ArraySegment<DISK_INFO>> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<ArraySegment<DISK_INFO>>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value, true);
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					DISK_INFO elem_0 = this.Buffer.value.Item(i);
					encoder.WriteFixedStruct(elem_0, NdrAlignment._2Byte);
				}

				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					DISK_INFO elem_0 = this.Buffer.value.Item(i);
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArraySegmentHeader<DISK_INFO>();
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					DISK_INFO elem_0 = this.Buffer.value.Item(i);
					elem_0 = decoder.ReadFixedStruct<DISK_INFO>(NdrAlignment._2Byte);
					this.Buffer.value.Item(i) = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					DISK_INFO elem_0 = this.Buffer.value.Item(i);
					decoder.ReadStructDeferral<DISK_INFO>(ref elem_0);
					this.Buffer.value.Item(i) = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_TRANSPORT_INFO_0 : IRpcFixedStruct
	{
		public uint svti0_numberofvcs;
		public RpcPointer<string> svti0_transportname;
		public RpcPointer<byte[]> svti0_transportaddress;
		public uint svti0_transportaddresslength;
		public RpcPointer<string> svti0_networkaddress;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.svti0_numberofvcs);
			encoder.WriteUniquePointer(this.svti0_transportname);
			encoder.WriteUniquePointer(this.svti0_transportaddress);
			encoder.WriteValue(this.svti0_transportaddresslength);
			encoder.WriteUniquePointer(this.svti0_networkaddress);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.svti0_numberofvcs = decoder.ReadUInt32();
			this.svti0_transportname = decoder.ReadUniquePointer<string>();
			this.svti0_transportaddress = decoder.ReadUniquePointer<byte[]>();
			this.svti0_transportaddresslength = decoder.ReadUInt32();
			this.svti0_networkaddress = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.svti0_transportname is not null)
			{
				encoder.WriteWideCharString(this.svti0_transportname.value);
			}

			if (this.svti0_transportaddress is not null)
			{
				encoder.WriteArrayHeader(this.svti0_transportaddress.value);
				for (int i = 0; i < this.svti0_transportaddress.value.Length; i++)
				{
					byte elem_0 = this.svti0_transportaddress.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.svti0_networkaddress is not null)
			{
				encoder.WriteWideCharString(this.svti0_networkaddress.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.svti0_transportname is not null)
			{
				this.svti0_transportname.value = decoder.ReadWideCharString();
			}

			if (this.svti0_transportaddress is not null)
			{
				this.svti0_transportaddress.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.svti0_transportaddress.value.Length; i++)
				{
					byte elem_0 = this.svti0_transportaddress.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.svti0_transportaddress.value[i] = elem_0;
				}
			}

			if (this.svti0_networkaddress is not null)
			{
				this.svti0_networkaddress.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_XPORT_INFO_0_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SERVER_TRANSPORT_INFO_0[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SERVER_TRANSPORT_INFO_0[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SERVER_TRANSPORT_INFO_0>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_0>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_TRANSPORT_INFO_1 : IRpcFixedStruct
	{
		public uint svti1_numberofvcs;
		public RpcPointer<string> svti1_transportname;
		public RpcPointer<byte[]> svti1_transportaddress;
		public uint svti1_transportaddresslength;
		public RpcPointer<string> svti1_networkaddress;
		public RpcPointer<string> svti1_domain;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.svti1_numberofvcs);
			encoder.WriteUniquePointer(this.svti1_transportname);
			encoder.WriteUniquePointer(this.svti1_transportaddress);
			encoder.WriteValue(this.svti1_transportaddresslength);
			encoder.WriteUniquePointer(this.svti1_networkaddress);
			encoder.WriteUniquePointer(this.svti1_domain);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.svti1_numberofvcs = decoder.ReadUInt32();
			this.svti1_transportname = decoder.ReadUniquePointer<string>();
			this.svti1_transportaddress = decoder.ReadUniquePointer<byte[]>();
			this.svti1_transportaddresslength = decoder.ReadUInt32();
			this.svti1_networkaddress = decoder.ReadUniquePointer<string>();
			this.svti1_domain = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.svti1_transportname is not null)
			{
				encoder.WriteWideCharString(this.svti1_transportname.value);
			}

			if (this.svti1_transportaddress is not null)
			{
				encoder.WriteArrayHeader(this.svti1_transportaddress.value);
				for (int i = 0; i < this.svti1_transportaddress.value.Length; i++)
				{
					byte elem_0 = this.svti1_transportaddress.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.svti1_networkaddress is not null)
			{
				encoder.WriteWideCharString(this.svti1_networkaddress.value);
			}

			if (this.svti1_domain is not null)
			{
				encoder.WriteWideCharString(this.svti1_domain.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.svti1_transportname is not null)
			{
				this.svti1_transportname.value = decoder.ReadWideCharString();
			}

			if (this.svti1_transportaddress is not null)
			{
				this.svti1_transportaddress.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.svti1_transportaddress.value.Length; i++)
				{
					byte elem_0 = this.svti1_transportaddress.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.svti1_transportaddress.value[i] = elem_0;
				}
			}

			if (this.svti1_networkaddress is not null)
			{
				this.svti1_networkaddress.value = decoder.ReadWideCharString();
			}

			if (this.svti1_domain is not null)
			{
				this.svti1_domain.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_XPORT_INFO_1_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SERVER_TRANSPORT_INFO_1[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SERVER_TRANSPORT_INFO_1[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SERVER_TRANSPORT_INFO_1>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_1 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_1>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_1 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_1>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_TRANSPORT_INFO_2 : IRpcFixedStruct
	{
		public uint svti2_numberofvcs;
		public RpcPointer<string> svti2_transportname;
		public RpcPointer<byte[]> svti2_transportaddress;
		public uint svti2_transportaddresslength;
		public RpcPointer<string> svti2_networkaddress;
		public RpcPointer<string> svti2_domain;
		public uint svti2_flags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.svti2_numberofvcs);
			encoder.WriteUniquePointer(this.svti2_transportname);
			encoder.WriteUniquePointer(this.svti2_transportaddress);
			encoder.WriteValue(this.svti2_transportaddresslength);
			encoder.WriteUniquePointer(this.svti2_networkaddress);
			encoder.WriteUniquePointer(this.svti2_domain);
			encoder.WriteValue(this.svti2_flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.svti2_numberofvcs = decoder.ReadUInt32();
			this.svti2_transportname = decoder.ReadUniquePointer<string>();
			this.svti2_transportaddress = decoder.ReadUniquePointer<byte[]>();
			this.svti2_transportaddresslength = decoder.ReadUInt32();
			this.svti2_networkaddress = decoder.ReadUniquePointer<string>();
			this.svti2_domain = decoder.ReadUniquePointer<string>();
			this.svti2_flags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.svti2_transportname is not null)
			{
				encoder.WriteWideCharString(this.svti2_transportname.value);
			}

			if (this.svti2_transportaddress is not null)
			{
				encoder.WriteArrayHeader(this.svti2_transportaddress.value);
				for (int i = 0; i < this.svti2_transportaddress.value.Length; i++)
				{
					byte elem_0 = this.svti2_transportaddress.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.svti2_networkaddress is not null)
			{
				encoder.WriteWideCharString(this.svti2_networkaddress.value);
			}

			if (this.svti2_domain is not null)
			{
				encoder.WriteWideCharString(this.svti2_domain.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.svti2_transportname is not null)
			{
				this.svti2_transportname.value = decoder.ReadWideCharString();
			}

			if (this.svti2_transportaddress is not null)
			{
				this.svti2_transportaddress.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.svti2_transportaddress.value.Length; i++)
				{
					byte elem_0 = this.svti2_transportaddress.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.svti2_transportaddress.value[i] = elem_0;
				}
			}

			if (this.svti2_networkaddress is not null)
			{
				this.svti2_networkaddress.value = decoder.ReadWideCharString();
			}

			if (this.svti2_domain is not null)
			{
				this.svti2_domain.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_XPORT_INFO_2_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SERVER_TRANSPORT_INFO_2[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SERVER_TRANSPORT_INFO_2[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SERVER_TRANSPORT_INFO_2>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_2 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_2>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_2 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_2>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_TRANSPORT_INFO_3 : IRpcFixedStruct
	{
		public uint svti3_numberofvcs;
		public RpcPointer<string> svti3_transportname;
		public RpcPointer<byte[]> svti3_transportaddress;
		public uint svti3_transportaddresslength;
		public RpcPointer<string> svti3_networkaddress;
		public RpcPointer<string> svti3_domain;
		public uint svti3_flags;
		public uint svti3_passwordlength;
		public byte[] svti3_password;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.svti3_numberofvcs);
			encoder.WriteUniquePointer(this.svti3_transportname);
			encoder.WriteUniquePointer(this.svti3_transportaddress);
			encoder.WriteValue(this.svti3_transportaddresslength);
			encoder.WriteUniquePointer(this.svti3_networkaddress);
			encoder.WriteUniquePointer(this.svti3_domain);
			encoder.WriteValue(this.svti3_flags);
			encoder.WriteValue(this.svti3_passwordlength);
			if (this.svti3_password == null)
				this.svti3_password = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				byte elem_0 = this.svti3_password[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.svti3_numberofvcs = decoder.ReadUInt32();
			this.svti3_transportname = decoder.ReadUniquePointer<string>();
			this.svti3_transportaddress = decoder.ReadUniquePointer<byte[]>();
			this.svti3_transportaddresslength = decoder.ReadUInt32();
			this.svti3_networkaddress = decoder.ReadUniquePointer<string>();
			this.svti3_domain = decoder.ReadUniquePointer<string>();
			this.svti3_flags = decoder.ReadUInt32();
			this.svti3_passwordlength = decoder.ReadUInt32();
			if (this.svti3_password == null)
				this.svti3_password = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				byte elem_0 = this.svti3_password[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.svti3_password[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.svti3_transportname is not null)
			{
				encoder.WriteWideCharString(this.svti3_transportname.value);
			}

			if (this.svti3_transportaddress is not null)
			{
				encoder.WriteArrayHeader(this.svti3_transportaddress.value);
				for (int i = 0; i < this.svti3_transportaddress.value.Length; i++)
				{
					byte elem_0 = this.svti3_transportaddress.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			if (this.svti3_networkaddress is not null)
			{
				encoder.WriteWideCharString(this.svti3_networkaddress.value);
			}

			if (this.svti3_domain is not null)
			{
				encoder.WriteWideCharString(this.svti3_domain.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.svti3_transportname is not null)
			{
				this.svti3_transportname.value = decoder.ReadWideCharString();
			}

			if (this.svti3_transportaddress is not null)
			{
				this.svti3_transportaddress.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.svti3_transportaddress.value.Length; i++)
				{
					byte elem_0 = this.svti3_transportaddress.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.svti3_transportaddress.value[i] = elem_0;
				}
			}

			if (this.svti3_networkaddress is not null)
			{
				this.svti3_networkaddress.value = decoder.ReadWideCharString();
			}

			if (this.svti3_domain is not null)
			{
				this.svti3_domain.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_XPORT_INFO_3_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SERVER_TRANSPORT_INFO_3[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SERVER_TRANSPORT_INFO_3[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_3 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_3 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SERVER_TRANSPORT_INFO_3>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_3 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_3>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_TRANSPORT_INFO_3 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_3>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct TRANSPORT_INFO : IRpcFixedStruct
	{
		public uint unionSwitch;
		public SERVER_TRANSPORT_INFO_0 Transport0;
		public SERVER_TRANSPORT_INFO_1 Transport1;
		public SERVER_TRANSPORT_INFO_2 Transport2;
		public SERVER_TRANSPORT_INFO_3 Transport3;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					encoder.WriteFixedStruct(this.Transport0, NdrAlignment.NativePtr);
					break;
				case 1U:
					encoder.WriteFixedStruct(this.Transport1, NdrAlignment.NativePtr);
					break;
				case 2U:
					encoder.WriteFixedStruct(this.Transport2, NdrAlignment.NativePtr);
					break;
				case 3U:
					encoder.WriteFixedStruct(this.Transport3, NdrAlignment.NativePtr);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					this.Transport0 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_0>(NdrAlignment.NativePtr);
					break;
				case 1U:
					this.Transport1 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_1>(NdrAlignment.NativePtr);
					break;
				case 2U:
					this.Transport2 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_2>(NdrAlignment.NativePtr);
					break;
				case 3U:
					this.Transport3 = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_3>(NdrAlignment.NativePtr);
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					encoder.WriteStructDeferral(this.Transport0);
					break;
				case 1U:
					encoder.WriteStructDeferral(this.Transport1);
					break;
				case 2U:
					encoder.WriteStructDeferral(this.Transport2);
					break;
				case 3U:
					encoder.WriteStructDeferral(this.Transport3);
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_0>(ref this.Transport0);
					break;
				case 1U:
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_1>(ref this.Transport1);
					break;
				case 2U:
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_2>(ref this.Transport2);
					break;
				case 3U:
					decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_3>(ref this.Transport3);
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_XPORT_ENUM_UNION : IRpcFixedStruct
	{
		public uint Level;
		public RpcPointer<SERVER_XPORT_INFO_0_CONTAINER> Level0;
		public RpcPointer<SERVER_XPORT_INFO_1_CONTAINER> Level1;
		public RpcPointer<SERVER_XPORT_INFO_2_CONTAINER> Level2;
		public RpcPointer<SERVER_XPORT_INFO_3_CONTAINER> Level3;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.Level);
			switch ((uint)this.Level)
			{
				case 0U:
					encoder.WriteUniquePointer(this.Level0);
					break;
				case 1U:
					encoder.WriteUniquePointer(this.Level1);
					break;
				case 2U:
					encoder.WriteUniquePointer(this.Level2);
					break;
				case 3U:
					encoder.WriteUniquePointer(this.Level3);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.Level = decoder.ReadUInt32();
			switch ((uint)this.Level)
			{
				case 0U:
					this.Level0 = decoder.ReadUniquePointer<SERVER_XPORT_INFO_0_CONTAINER>();
					break;
				case 1U:
					this.Level1 = decoder.ReadUniquePointer<SERVER_XPORT_INFO_1_CONTAINER>();
					break;
				case 2U:
					this.Level2 = decoder.ReadUniquePointer<SERVER_XPORT_INFO_2_CONTAINER>();
					break;
				case 3U:
					this.Level3 = decoder.ReadUniquePointer<SERVER_XPORT_INFO_3_CONTAINER>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						encoder.WriteFixedStruct(this.Level0.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level0.value);
					}

					break;
				case 1U:
					if (this.Level1 is not null)
					{
						encoder.WriteFixedStruct(this.Level1.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level1.value);
					}

					break;
				case 2U:
					if (this.Level2 is not null)
					{
						encoder.WriteFixedStruct(this.Level2.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level2.value);
					}

					break;
				case 3U:
					if (this.Level3 is not null)
					{
						encoder.WriteFixedStruct(this.Level3.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level3.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						this.Level0.value = decoder.ReadFixedStruct<SERVER_XPORT_INFO_0_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_XPORT_INFO_0_CONTAINER>(ref this.Level0.value);
					}

					break;
				case 1U:
					if (this.Level1 is not null)
					{
						this.Level1.value = decoder.ReadFixedStruct<SERVER_XPORT_INFO_1_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_XPORT_INFO_1_CONTAINER>(ref this.Level1.value);
					}

					break;
				case 2U:
					if (this.Level2 is not null)
					{
						this.Level2.value = decoder.ReadFixedStruct<SERVER_XPORT_INFO_2_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_XPORT_INFO_2_CONTAINER>(ref this.Level2.value);
					}

					break;
				case 3U:
					if (this.Level3 is not null)
					{
						this.Level3.value = decoder.ReadFixedStruct<SERVER_XPORT_INFO_3_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_XPORT_INFO_3_CONTAINER>(ref this.Level3.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_XPORT_ENUM_STRUCT : IRpcFixedStruct
	{
		public uint Level;
		public SERVER_XPORT_ENUM_UNION XportInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.XportInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.XportInfo = decoder.ReadUnion<SERVER_XPORT_ENUM_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.XportInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<SERVER_XPORT_ENUM_UNION>(ref this.XportInfo);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct ADT_SECURITY_DESCRIPTOR : IRpcFixedStruct
	{
		public uint Length;
		public RpcPointer<byte[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					byte elem_0 = this.Buffer.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					byte elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadUnsignedChar();
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct STAT_SERVER_0 : IRpcFixedStruct
	{
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
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
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
	public partial struct TIME_OF_DAY_INFO : IRpcFixedStruct
	{
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
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
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
	public partial struct NET_DFS_ENTRY_ID : IRpcFixedStruct
	{
		public Guid Uid;
		public RpcPointer<string> Prefix;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Uid);
			encoder.WriteUniquePointer(this.Prefix);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Uid = decoder.ReadUuid();
			this.Prefix = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Prefix is not null)
			{
				encoder.WriteWideCharString(this.Prefix.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Prefix is not null)
			{
				this.Prefix.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct NET_DFS_ENTRY_ID_CONTAINER : IRpcFixedStruct
	{
		public uint Count;
		public RpcPointer<NET_DFS_ENTRY_ID[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Count);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Count = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<NET_DFS_ENTRY_ID[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					NET_DFS_ENTRY_ID elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					NET_DFS_ENTRY_ID elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<NET_DFS_ENTRY_ID>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					NET_DFS_ENTRY_ID elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<NET_DFS_ENTRY_ID>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					NET_DFS_ENTRY_ID elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<NET_DFS_ENTRY_ID>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DFS_SITENAME_INFO : IRpcFixedStruct
	{
		public uint SiteFlags;
		public RpcPointer<string> SiteName;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.SiteFlags);
			encoder.WriteUniquePointer(this.SiteName);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.SiteFlags = decoder.ReadUInt32();
			this.SiteName = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.SiteName is not null)
			{
				encoder.WriteWideCharString(this.SiteName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.SiteName is not null)
			{
				this.SiteName.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct DFS_SITELIST_INFO : IRpcConformantStruct
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeHeader(IRpcEncoder encoder)
		{
			encoder.WriteArrayHeader(this.Site);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeHeader(IRpcDecoder decoder)
		{
			this.Site = decoder.ReadArrayHeader<DFS_SITENAME_INFO>();
		}

		public uint cSites;
		public DFS_SITENAME_INFO[] Site;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeConformantArrayField(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.Site.Length; i++)
			{
				DFS_SITENAME_INFO elem_0 = this.Site[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeConformantArrayField(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.Site.Length; i++)
			{
				DFS_SITENAME_INFO elem_0 = this.Site[i];
				elem_0 = decoder.ReadFixedStruct<DFS_SITENAME_INFO>(NdrAlignment.NativePtr);
				this.Site[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.cSites);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.cSites = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			for (int i = 0; i < this.Site.Length; i++)
			{
				DFS_SITENAME_INFO elem_0 = this.Site[i];
				encoder.WriteStructDeferral(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			for (int i = 0; i < this.Site.Length; i++)
			{
				DFS_SITENAME_INFO elem_0 = this.Site[i];
				decoder.ReadStructDeferral<DFS_SITENAME_INFO>(ref elem_0);
				this.Site[i] = elem_0;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_ALIAS_INFO_0 : IRpcFixedStruct
	{
		public RpcPointer<string> srvai0_alias;
		public RpcPointer<string> srvai0_target;
		public byte srvai0_default;
		public uint srvai0_reserved;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.srvai0_alias);
			encoder.WriteUniquePointer(this.srvai0_target);
			encoder.WriteValue(this.srvai0_default);
			encoder.WriteValue(this.srvai0_reserved);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.srvai0_alias = decoder.ReadUniquePointer<string>();
			this.srvai0_target = decoder.ReadUniquePointer<string>();
			this.srvai0_default = decoder.ReadUnsignedChar();
			this.srvai0_reserved = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.srvai0_alias is not null)
			{
				encoder.WriteWideCharString(this.srvai0_alias.value);
			}

			if (this.srvai0_target is not null)
			{
				encoder.WriteWideCharString(this.srvai0_target.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.srvai0_alias is not null)
			{
				this.srvai0_alias.value = decoder.ReadWideCharString();
			}

			if (this.srvai0_target is not null)
			{
				this.srvai0_target.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_ALIAS_INFO_0_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<SERVER_ALIAS_INFO_0[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<SERVER_ALIAS_INFO_0[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_ALIAS_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_ALIAS_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<SERVER_ALIAS_INFO_0>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_ALIAS_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<SERVER_ALIAS_INFO_0>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					SERVER_ALIAS_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<SERVER_ALIAS_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct _SERVER_ALIAS_ENUM_UNION : IRpcFixedStruct
	{
		public uint Level;
		public RpcPointer<SERVER_ALIAS_INFO_0_CONTAINER> Level0;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.Level);
			switch ((uint)this.Level)
			{
				case 0U:
					encoder.WriteUniquePointer(this.Level0);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.Level = decoder.ReadUInt32();
			switch ((uint)this.Level)
			{
				case 0U:
					this.Level0 = decoder.ReadUniquePointer<SERVER_ALIAS_INFO_0_CONTAINER>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						encoder.WriteFixedStruct(this.Level0.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.Level0.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						this.Level0.value = decoder.ReadFixedStruct<SERVER_ALIAS_INFO_0_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_ALIAS_INFO_0_CONTAINER>(ref this.Level0.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_ALIAS_ENUM_STRUCT : IRpcFixedStruct
	{
		public uint Level;
		public _SERVER_ALIAS_ENUM_UNION ServerAliasInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.ServerAliasInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.ServerAliasInfo = decoder.ReadUnion<_SERVER_ALIAS_ENUM_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ServerAliasInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<_SERVER_ALIAS_ENUM_UNION>(ref this.ServerAliasInfo);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct SERVER_ALIAS_INFO : IRpcFixedStruct
	{
		public uint unionSwitch;
		public RpcPointer<SERVER_ALIAS_INFO_0> ServerAliasInfo0;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					encoder.WriteUniquePointer(this.ServerAliasInfo0);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					this.ServerAliasInfo0 = decoder.ReadUniquePointer<SERVER_ALIAS_INFO_0>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					if (this.ServerAliasInfo0 is not null)
					{
						encoder.WriteFixedStruct(this.ServerAliasInfo0.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.ServerAliasInfo0.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					if (this.ServerAliasInfo0 is not null)
					{
						this.ServerAliasInfo0.value = decoder.ReadFixedStruct<SERVER_ALIAS_INFO_0>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<SERVER_ALIAS_INFO_0>(ref this.ServerAliasInfo0.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("4b324fc8-1670-01d3-1278-5a47bf6ee188"), RpcVersionAttribute(3, 0)]
	public partial interface srvsvc
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum0NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum1NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum2NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum3NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum4NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum5NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum6NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum7NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrConnectionEnum(string ServerName, string Qualifier, RpcPointer<CONNECT_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrFileEnum(string ServerName, string BasePath, string UserName, RpcPointer<FILE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrFileGetInfo(string ServerName, uint FileId, uint Level, RpcPointer<FILE_INFO> InfoStruct, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrFileClose(string ServerName, uint FileId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrSessionEnum(string ServerName, string ClientName, string UserName, RpcPointer<SESSION_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrSessionDel(string ServerName, string ClientName, string UserName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareAdd(string ServerName, uint Level, SHARE_INFO InfoStruct, RpcPointer<uint> ParmErr, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareEnum(string ServerName, RpcPointer<SHARE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareGetInfo(string ServerName, string NetName, uint Level, RpcPointer<SHARE_INFO> InfoStruct, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareSetInfo(string ServerName, string NetName, uint Level, SHARE_INFO ShareInfo, RpcPointer<uint> ParmErr, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareDel(string ServerName, string NetName, uint Reserved, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareDelSticky(string ServerName, string NetName, uint Reserved, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareCheck(string ServerName, string Device, RpcPointer<uint> Type, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerGetInfo(string ServerName, uint Level, RpcPointer<SERVER_INFO> InfoStruct, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerSetInfo(string ServerName, uint Level, SERVER_INFO ServerInfo, RpcPointer<uint> ParmErr, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerDiskEnum(string ServerName, uint Level, RpcPointer<DISK_ENUM_CONTAINER> DiskInfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerStatisticsGet(string ServerName, string Service, uint Level, uint Options, RpcPointer<RpcPointer<STAT_SERVER_0>> InfoStruct, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerTransportAdd(string ServerName, uint Level, SERVER_TRANSPORT_INFO_0 Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerTransportEnum(string ServerName, RpcPointer<SERVER_XPORT_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerTransportDel(string ServerName, uint Level, SERVER_TRANSPORT_INFO_0 Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrRemoteTOD(string ServerName, RpcPointer<RpcPointer<TIME_OF_DAY_INFO>> BufferPtr, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum29NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetprPathType(string ServerName, string PathName, RpcPointer<uint> PathType, uint Flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetprPathCanonicalize(string ServerName, string PathName, RpcPointer<byte[]> Outbuf, uint OutbufLen, string Prefix, RpcPointer<uint> PathType, uint Flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> NetprPathCompare(string ServerName, string PathName1, string PathName2, uint PathType, uint Flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetprNameValidate(string ServerName, string Name, uint NameType, uint Flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetprNameCanonicalize(string ServerName, string Name, RpcPointer<char[]> Outbuf, uint OutbufLen, uint NameType, uint Flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> NetprNameCompare(string ServerName, string Name1, string Name2, uint NameType, uint Flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareEnumSticky(string ServerName, RpcPointer<SHARE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareDelStart(string ServerName, string NetName, uint Reserved, RpcPointer<RpcContextHandle> ContextHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareDelCommit(RpcPointer<RpcContextHandle> ContextHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrpGetFileSecurity(string ServerName, string ShareName, string lpFileName, uint RequestedInformation, RpcPointer<RpcPointer<ADT_SECURITY_DESCRIPTOR>> SecurityDescriptor, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrpSetFileSecurity(string ServerName, string ShareName, string lpFileName, uint SecurityInformation, ADT_SECURITY_DESCRIPTOR SecurityDescriptor, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerTransportAddEx(string ServerName, uint Level, TRANSPORT_INFO Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum42NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrDfsGetVersion(string ServerName, RpcPointer<uint> Version, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrDfsCreateLocalPartition(string ServerName, string ShareName, Guid EntryUid, string EntryPrefix, string ShortName, NET_DFS_ENTRY_ID_CONTAINER RelationInfo, int Force, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrDfsDeleteLocalPartition(string ServerName, Guid Uid, string Prefix, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrDfsSetLocalVolumeState(string ServerName, Guid Uid, string Prefix, uint State, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum47NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrDfsCreateExitPoint(string ServerName, Guid Uid, string Prefix, uint Type, uint ShortPrefixLen, RpcPointer<char[]> ShortPrefix, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrDfsDeleteExitPoint(string ServerName, Guid Uid, string Prefix, uint Type, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrDfsModifyPrefix(string ServerName, Guid Uid, string Prefix, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrDfsFixLocalVolume(string ServerName, string VolumeName, uint EntryType, uint ServiceType, string StgId, Guid EntryUid, string EntryPrefix, NET_DFS_ENTRY_ID_CONTAINER RelationInfo, uint CreateDisposition, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrDfsManagerReportSiteInfo(string ServerName, RpcPointer<RpcPointer<DFS_SITELIST_INFO>> ppSiteInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerTransportDelEx(string ServerName, uint Level, TRANSPORT_INFO Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerAliasAdd(string ServerName, uint Level, SERVER_ALIAS_INFO InfoStruct, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerAliasEnum(string ServerName, RpcPointer<SERVER_ALIAS_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrServerAliasDel(string ServerName, uint Level, SERVER_ALIAS_INFO InfoStruct, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<uint> NetrShareDelEx(string ServerName, uint Level, SHARE_INFO ShareInfo, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("4b324fc8-1670-01d3-1278-5a47bf6ee188")]
	public partial class srvsvcClientProxy : Titanis.DceRpc.Client.RpcClientProxy, srvsvc, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum0NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum1NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum2NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum3NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum4NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum5NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum6NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum7NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrConnectionEnum(string ServerName, string Qualifier, RpcPointer<CONNECT_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(Qualifier is null);
			if (Qualifier is not null)
				encoder.WriteWideCharString(Qualifier);
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadFixedStruct<CONNECT_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<CONNECT_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutUniquePointer<uint>(ResumeHandle);
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrFileEnum(string ServerName, string BasePath, string UserName, RpcPointer<FILE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(BasePath is null);
			if (BasePath is not null)
				encoder.WriteWideCharString(BasePath);
			encoder.WriteUniqueReferentId(UserName is null);
			if (UserName is not null)
				encoder.WriteWideCharString(UserName);
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadFixedStruct<FILE_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FILE_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutUniquePointer<uint>(ResumeHandle);
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrFileGetInfo(string ServerName, uint FileId, uint Level, RpcPointer<FILE_INFO> InfoStruct, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(FileId);
			encoder.WriteValue(Level);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadUnion<FILE_INFO>();
			decoder.ReadStructDeferral<FILE_INFO>(ref InfoStruct.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrFileClose(string ServerName, uint FileId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(FileId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrSessionEnum(string ServerName, string ClientName, string UserName, RpcPointer<SESSION_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(ClientName is null);
			if (ClientName is not null)
				encoder.WriteWideCharString(ClientName);
			encoder.WriteUniqueReferentId(UserName is null);
			if (UserName is not null)
				encoder.WriteWideCharString(UserName);
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadFixedStruct<SESSION_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SESSION_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutUniquePointer<uint>(ResumeHandle);
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrSessionDel(string ServerName, string ClientName, string UserName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(ClientName is null);
			if (ClientName is not null)
				encoder.WriteWideCharString(ClientName);
			encoder.WriteUniqueReferentId(UserName is null);
			if (UserName is not null)
				encoder.WriteWideCharString(UserName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareAdd(string ServerName, uint Level, SHARE_INFO InfoStruct, RpcPointer<uint> ParmErr, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(InfoStruct);
			encoder.WriteStructDeferral(InfoStruct);
			encoder.WriteUniquePointer(ParmErr);
			if (ParmErr is not null)
			{
				encoder.WriteValue(ParmErr.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ParmErr = decoder.ReadOutUniquePointer<uint>(ParmErr);
			if (ParmErr is not null)
			{
				ParmErr.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareEnum(string ServerName, RpcPointer<SHARE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadFixedStruct<SHARE_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SHARE_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutUniquePointer<uint>(ResumeHandle);
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareGetInfo(string ServerName, string NetName, uint Level, RpcPointer<SHARE_INFO> InfoStruct, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(NetName);
			encoder.WriteValue(Level);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadUnion<SHARE_INFO>();
			decoder.ReadStructDeferral<SHARE_INFO>(ref InfoStruct.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareSetInfo(string ServerName, string NetName, uint Level, SHARE_INFO ShareInfo, RpcPointer<uint> ParmErr, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(NetName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(ShareInfo);
			encoder.WriteStructDeferral(ShareInfo);
			encoder.WriteUniquePointer(ParmErr);
			if (ParmErr is not null)
			{
				encoder.WriteValue(ParmErr.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ParmErr = decoder.ReadOutUniquePointer<uint>(ParmErr);
			if (ParmErr is not null)
			{
				ParmErr.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareDel(string ServerName, string NetName, uint Reserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(NetName);
			encoder.WriteValue(Reserved);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareDelSticky(string ServerName, string NetName, uint Reserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(NetName);
			encoder.WriteValue(Reserved);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareCheck(string ServerName, string Device, RpcPointer<uint> Type, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(Device);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Type.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerGetInfo(string ServerName, uint Level, RpcPointer<SERVER_INFO> InfoStruct, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadUnion<SERVER_INFO>();
			decoder.ReadStructDeferral<SERVER_INFO>(ref InfoStruct.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerSetInfo(string ServerName, uint Level, SERVER_INFO ServerInfo, RpcPointer<uint> ParmErr, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(ServerInfo);
			encoder.WriteStructDeferral(ServerInfo);
			encoder.WriteUniquePointer(ParmErr);
			if (ParmErr is not null)
			{
				encoder.WriteValue(ParmErr.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ParmErr = decoder.ReadOutUniquePointer<uint>(ParmErr);
			if (ParmErr is not null)
			{
				ParmErr.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerDiskEnum(string ServerName, uint Level, RpcPointer<DISK_ENUM_CONTAINER> DiskInfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteFixedStruct(DiskInfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(DiskInfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			DiskInfoStruct.value = decoder.ReadFixedStruct<DISK_ENUM_CONTAINER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<DISK_ENUM_CONTAINER>(ref DiskInfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutUniquePointer<uint>(ResumeHandle);
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerStatisticsGet(string ServerName, string Service, uint Level, uint Options, RpcPointer<RpcPointer<STAT_SERVER_0>> InfoStruct, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(Service is null);
			if (Service is not null)
				encoder.WriteWideCharString(Service);
			encoder.WriteValue(Level);
			encoder.WriteValue(Options);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadOutUniquePointer<STAT_SERVER_0>(InfoStruct.value);
			if (InfoStruct.value is not null)
			{
				InfoStruct.value.value = decoder.ReadFixedStruct<STAT_SERVER_0>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<STAT_SERVER_0>(ref InfoStruct.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerTransportAdd(string ServerName, uint Level, SERVER_TRANSPORT_INFO_0 Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteFixedStruct(Buffer, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Buffer);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerTransportEnum(string ServerName, RpcPointer<SERVER_XPORT_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadFixedStruct<SERVER_XPORT_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_XPORT_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutUniquePointer<uint>(ResumeHandle);
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerTransportDel(string ServerName, uint Level, SERVER_TRANSPORT_INFO_0 Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteFixedStruct(Buffer, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(Buffer);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrRemoteTOD(string ServerName, RpcPointer<RpcPointer<TIME_OF_DAY_INFO>> BufferPtr, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			BufferPtr.value = decoder.ReadOutUniquePointer<TIME_OF_DAY_INFO>(BufferPtr.value);
			if (BufferPtr.value is not null)
			{
				BufferPtr.value.value = decoder.ReadFixedStruct<TIME_OF_DAY_INFO>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<TIME_OF_DAY_INFO>(ref BufferPtr.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum29NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetprPathType(string ServerName, string PathName, RpcPointer<uint> PathType, uint Flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(PathName);
			encoder.WriteValue(Flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			PathType.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetprPathCanonicalize(string ServerName, string PathName, RpcPointer<byte[]> Outbuf, uint OutbufLen, string Prefix, RpcPointer<uint> PathType, uint Flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(PathName);
			encoder.WriteValue(OutbufLen);
			encoder.WriteWideCharString(Prefix);
			encoder.WriteValue(PathType.value);
			encoder.WriteValue(Flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Outbuf.value = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < Outbuf.value.Length; i++)
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> NetprPathCompare(string ServerName, string PathName1, string PathName2, uint PathType, uint Flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(PathName1);
			encoder.WriteWideCharString(PathName2);
			encoder.WriteValue(PathType);
			encoder.WriteValue(Flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetprNameValidate(string ServerName, string Name, uint NameType, uint Flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(Name);
			encoder.WriteValue(NameType);
			encoder.WriteValue(Flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetprNameCanonicalize(string ServerName, string Name, RpcPointer<char[]> Outbuf, uint OutbufLen, uint NameType, uint Flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(Name);
			encoder.WriteValue(OutbufLen);
			encoder.WriteValue(NameType);
			encoder.WriteValue(Flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Outbuf.value = decoder.ReadArrayHeader<char>();
			for (int i = 0; i < Outbuf.value.Length; i++)
			{
				char elem_0 = Outbuf.value[i];
				elem_0 = decoder.ReadWideChar();
				Outbuf.value[i] = elem_0;
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> NetprNameCompare(string ServerName, string Name1, string Name2, uint NameType, uint Flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(Name1);
			encoder.WriteWideCharString(Name2);
			encoder.WriteValue(NameType);
			encoder.WriteValue(Flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareEnumSticky(string ServerName, RpcPointer<SHARE_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(36);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadFixedStruct<SHARE_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SHARE_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutUniquePointer<uint>(ResumeHandle);
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareDelStart(string ServerName, string NetName, uint Reserved, RpcPointer<RpcContextHandle> ContextHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(37);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(NetName);
			encoder.WriteValue(Reserved);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ContextHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareDelCommit(RpcPointer<RpcContextHandle> ContextHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(38);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(ContextHandle.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ContextHandle.value = decoder.ReadContextHandle();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrpGetFileSecurity(string ServerName, string ShareName, string lpFileName, uint RequestedInformation, RpcPointer<RpcPointer<ADT_SECURITY_DESCRIPTOR>> SecurityDescriptor, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(39);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(ShareName is null);
			if (ShareName is not null)
				encoder.WriteWideCharString(ShareName);
			encoder.WriteWideCharString(lpFileName);
			encoder.WriteValue(RequestedInformation);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			SecurityDescriptor.value = decoder.ReadOutUniquePointer<ADT_SECURITY_DESCRIPTOR>(SecurityDescriptor.value);
			if (SecurityDescriptor.value is not null)
			{
				SecurityDescriptor.value.value = decoder.ReadFixedStruct<ADT_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ADT_SECURITY_DESCRIPTOR>(ref SecurityDescriptor.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrpSetFileSecurity(string ServerName, string ShareName, string lpFileName, uint SecurityInformation, ADT_SECURITY_DESCRIPTOR SecurityDescriptor, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(40);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(ShareName is null);
			if (ShareName is not null)
				encoder.WriteWideCharString(ShareName);
			encoder.WriteWideCharString(lpFileName);
			encoder.WriteValue(SecurityInformation);
			encoder.WriteFixedStruct(SecurityDescriptor, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(SecurityDescriptor);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerTransportAddEx(string ServerName, uint Level, TRANSPORT_INFO Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(41);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(Buffer);
			encoder.WriteStructDeferral(Buffer);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum42NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(42);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrDfsGetVersion(string ServerName, RpcPointer<uint> Version, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(43);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Version.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrDfsCreateLocalPartition(string ServerName, string ShareName, Guid EntryUid, string EntryPrefix, string ShortName, NET_DFS_ENTRY_ID_CONTAINER RelationInfo, int Force, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(44);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(ShareName);
			encoder.WriteValue(EntryUid);
			encoder.WriteWideCharString(EntryPrefix);
			encoder.WriteWideCharString(ShortName);
			encoder.WriteFixedStruct(RelationInfo, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(RelationInfo);
			encoder.WriteValue(Force);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrDfsDeleteLocalPartition(string ServerName, Guid Uid, string Prefix, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(45);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Uid);
			encoder.WriteWideCharString(Prefix);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrDfsSetLocalVolumeState(string ServerName, Guid Uid, string Prefix, uint State, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(46);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Uid);
			encoder.WriteWideCharString(Prefix);
			encoder.WriteValue(State);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum47NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(47);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrDfsCreateExitPoint(string ServerName, Guid Uid, string Prefix, uint Type, uint ShortPrefixLen, RpcPointer<char[]> ShortPrefix, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(48);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Uid);
			encoder.WriteWideCharString(Prefix);
			encoder.WriteValue(Type);
			encoder.WriteValue(ShortPrefixLen);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ShortPrefix.value = decoder.ReadArrayHeader<char>();
			for (int i = 0; i < ShortPrefix.value.Length; i++)
			{
				char elem_0 = ShortPrefix.value[i];
				elem_0 = decoder.ReadWideChar();
				ShortPrefix.value[i] = elem_0;
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrDfsDeleteExitPoint(string ServerName, Guid Uid, string Prefix, uint Type, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(49);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Uid);
			encoder.WriteWideCharString(Prefix);
			encoder.WriteValue(Type);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrDfsModifyPrefix(string ServerName, Guid Uid, string Prefix, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(50);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Uid);
			encoder.WriteWideCharString(Prefix);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrDfsFixLocalVolume(string ServerName, string VolumeName, uint EntryType, uint ServiceType, string StgId, Guid EntryUid, string EntryPrefix, NET_DFS_ENTRY_ID_CONTAINER RelationInfo, uint CreateDisposition, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(51);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(VolumeName);
			encoder.WriteValue(EntryType);
			encoder.WriteValue(ServiceType);
			encoder.WriteWideCharString(StgId);
			encoder.WriteValue(EntryUid);
			encoder.WriteWideCharString(EntryPrefix);
			encoder.WriteFixedStruct(RelationInfo, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(RelationInfo);
			encoder.WriteValue(CreateDisposition);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrDfsManagerReportSiteInfo(string ServerName, RpcPointer<RpcPointer<DFS_SITELIST_INFO>> ppSiteInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(52);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniquePointer(ppSiteInfo);
			if (ppSiteInfo is not null)
			{
				encoder.WriteUniquePointer(ppSiteInfo.value);
				if (ppSiteInfo.value is not null)
				{
					encoder.WriteConformantStruct(ppSiteInfo.value.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(ppSiteInfo.value.value);
				}
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppSiteInfo = decoder.ReadOutUniquePointer<RpcPointer<DFS_SITELIST_INFO>>(ppSiteInfo);
			if (ppSiteInfo is not null)
			{
				ppSiteInfo.value = decoder.ReadUniquePointer<DFS_SITELIST_INFO>();
				if (ppSiteInfo.value is not null)
				{
					ppSiteInfo.value.value = decoder.ReadConformantStruct<DFS_SITELIST_INFO>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<DFS_SITELIST_INFO>(ref ppSiteInfo.value.value);
				}
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerTransportDelEx(string ServerName, uint Level, TRANSPORT_INFO Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(53);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(Buffer);
			encoder.WriteStructDeferral(Buffer);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerAliasAdd(string ServerName, uint Level, SERVER_ALIAS_INFO InfoStruct, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(54);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(InfoStruct);
			encoder.WriteStructDeferral(InfoStruct);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerAliasEnum(string ServerName, RpcPointer<SERVER_ALIAS_ENUM_STRUCT> InfoStruct, uint PreferedMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(55);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadFixedStruct<SERVER_ALIAS_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_ALIAS_ENUM_STRUCT>(ref InfoStruct.value);
			TotalEntries.value = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadOutUniquePointer<uint>(ResumeHandle);
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrServerAliasDel(string ServerName, uint Level, SERVER_ALIAS_INFO InfoStruct, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(56);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(InfoStruct);
			encoder.WriteStructDeferral(InfoStruct);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<uint> NetrShareDelEx(string ServerName, uint Level, SHARE_INFO ShareInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(57);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(ShareInfo);
			encoder.WriteStructDeferral(ShareInfo);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(srvsvc);
		private static Guid _interfaceUuid = new Guid("4b324fc8-1670-01d3-1278-5a47bf6ee188");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(3, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class srvsvcStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum3NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum3NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum4NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum4NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum5NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum5NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum6NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum6NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum7NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum7NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrConnectionEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string Qualifier;
			RpcPointer<CONNECT_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				Qualifier = null;
			else
				Qualifier = decoder.ReadWideCharString();
			InfoStruct = new RpcPointer<CONNECT_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<CONNECT_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<CONNECT_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrConnectionEnum(ServerName, Qualifier, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrFileEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string BasePath;
			string UserName;
			RpcPointer<FILE_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				BasePath = null;
			else
				BasePath = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				UserName = null;
			else
				UserName = decoder.ReadWideCharString();
			InfoStruct = new RpcPointer<FILE_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<FILE_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<FILE_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrFileEnum(ServerName, BasePath, UserName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrFileGetInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint FileId;
			uint Level;
			RpcPointer<FILE_INFO> InfoStruct = new RpcPointer<FILE_INFO>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			FileId = decoder.ReadUInt32();
			Level = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrFileGetInfo(ServerName, FileId, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnion(InfoStruct.value);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrFileClose(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint FileId;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			FileId = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrFileClose(ServerName, FileId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrSessionEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string ClientName;
			string UserName;
			RpcPointer<SESSION_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				ClientName = null;
			else
				ClientName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				UserName = null;
			else
				UserName = decoder.ReadWideCharString();
			InfoStruct = new RpcPointer<SESSION_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<SESSION_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SESSION_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrSessionEnum(ServerName, ClientName, UserName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrSessionDel(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string ClientName;
			string UserName;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				ClientName = null;
			else
				ClientName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				UserName = null;
			else
				UserName = decoder.ReadWideCharString();
			var invokeTask = this._obj.NetrSessionDel(ServerName, ClientName, UserName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareAdd(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			SHARE_INFO InfoStruct;
			RpcPointer<uint> ParmErr;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			InfoStruct = decoder.ReadUnion<SHARE_INFO>();
			decoder.ReadStructDeferral<SHARE_INFO>(ref InfoStruct);
			ParmErr = decoder.ReadUniquePointer<uint>();
			if (ParmErr is not null)
			{
				ParmErr.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrShareAdd(ServerName, Level, InfoStruct, ParmErr, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ParmErr);
			if (ParmErr is not null)
			{
				encoder.WriteValue(ParmErr.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<SHARE_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			InfoStruct = new RpcPointer<SHARE_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<SHARE_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SHARE_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrShareEnum(ServerName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareGetInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string NetName;
			uint Level;
			RpcPointer<SHARE_INFO> InfoStruct = new RpcPointer<SHARE_INFO>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			NetName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrShareGetInfo(ServerName, NetName, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnion(InfoStruct.value);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareSetInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string NetName;
			uint Level;
			SHARE_INFO ShareInfo;
			RpcPointer<uint> ParmErr;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			NetName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			ShareInfo = decoder.ReadUnion<SHARE_INFO>();
			decoder.ReadStructDeferral<SHARE_INFO>(ref ShareInfo);
			ParmErr = decoder.ReadUniquePointer<uint>();
			if (ParmErr is not null)
			{
				ParmErr.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrShareSetInfo(ServerName, NetName, Level, ShareInfo, ParmErr, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ParmErr);
			if (ParmErr is not null)
			{
				encoder.WriteValue(ParmErr.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareDel(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string NetName;
			uint Reserved;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			NetName = decoder.ReadWideCharString();
			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrShareDel(ServerName, NetName, Reserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareDelSticky(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string NetName;
			uint Reserved;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			NetName = decoder.ReadWideCharString();
			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrShareDelSticky(ServerName, NetName, Reserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareCheck(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string Device;
			RpcPointer<uint> Type = new RpcPointer<uint>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Device = decoder.ReadWideCharString();
			var invokeTask = this._obj.NetrShareCheck(ServerName, Device, Type, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(Type.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerGetInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<SERVER_INFO> InfoStruct = new RpcPointer<SERVER_INFO>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrServerGetInfo(ServerName, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnion(InfoStruct.value);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerSetInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			SERVER_INFO ServerInfo;
			RpcPointer<uint> ParmErr;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			ServerInfo = decoder.ReadUnion<SERVER_INFO>();
			decoder.ReadStructDeferral<SERVER_INFO>(ref ServerInfo);
			ParmErr = decoder.ReadUniquePointer<uint>();
			if (ParmErr is not null)
			{
				ParmErr.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrServerSetInfo(ServerName, Level, ServerInfo, ParmErr, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ParmErr);
			if (ParmErr is not null)
			{
				encoder.WriteValue(ParmErr.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerDiskEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<DISK_ENUM_CONTAINER> DiskInfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			DiskInfoStruct = new RpcPointer<DISK_ENUM_CONTAINER>();
			DiskInfoStruct.value = decoder.ReadFixedStruct<DISK_ENUM_CONTAINER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<DISK_ENUM_CONTAINER>(ref DiskInfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrServerDiskEnum(ServerName, Level, DiskInfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(DiskInfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(DiskInfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerStatisticsGet(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string Service;
			uint Level;
			uint Options;
			RpcPointer<RpcPointer<STAT_SERVER_0>> InfoStruct = new RpcPointer<RpcPointer<STAT_SERVER_0>>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				Service = null;
			else
				Service = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			Options = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrServerStatisticsGet(ServerName, Service, Level, Options, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(InfoStruct.value);
			if (InfoStruct.value is not null)
			{
				encoder.WriteFixedStruct(InfoStruct.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(InfoStruct.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerTransportAdd(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			SERVER_TRANSPORT_INFO_0 Buffer;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			Buffer = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_0>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_0>(ref Buffer);
			var invokeTask = this._obj.NetrServerTransportAdd(ServerName, Level, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerTransportEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<SERVER_XPORT_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			InfoStruct = new RpcPointer<SERVER_XPORT_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<SERVER_XPORT_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_XPORT_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrServerTransportEnum(ServerName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerTransportDel(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			SERVER_TRANSPORT_INFO_0 Buffer;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			Buffer = decoder.ReadFixedStruct<SERVER_TRANSPORT_INFO_0>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_TRANSPORT_INFO_0>(ref Buffer);
			var invokeTask = this._obj.NetrServerTransportDel(ServerName, Level, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrRemoteTOD(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<RpcPointer<TIME_OF_DAY_INFO>> BufferPtr = new RpcPointer<RpcPointer<TIME_OF_DAY_INFO>>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			var invokeTask = this._obj.NetrRemoteTOD(ServerName, BufferPtr, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(BufferPtr.value);
			if (BufferPtr.value is not null)
			{
				encoder.WriteFixedStruct(BufferPtr.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(BufferPtr.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum29NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum29NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetprPathType(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string PathName;
			RpcPointer<uint> PathType = new RpcPointer<uint>();
			uint Flags;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			PathName = decoder.ReadWideCharString();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprPathType(ServerName, PathName, PathType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(PathType.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetprPathCanonicalize(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string PathName;
			RpcPointer<byte[]> Outbuf = new RpcPointer<byte[]>();
			uint OutbufLen;
			string Prefix;
			RpcPointer<uint> PathType;
			uint Flags;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			PathName = decoder.ReadWideCharString();
			OutbufLen = decoder.ReadUInt32();
			Prefix = decoder.ReadWideCharString();
			PathType = new RpcPointer<uint>();
			PathType.value = decoder.ReadUInt32();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprPathCanonicalize(ServerName, PathName, Outbuf, OutbufLen, Prefix, PathType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(Outbuf.value);
			for (int i = 0; i < Outbuf.value.Length; i++)
			{
				byte elem_0 = Outbuf.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(PathType.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetprPathCompare(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string PathName1;
			string PathName2;
			uint PathType;
			uint Flags;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			PathName1 = decoder.ReadWideCharString();
			PathName2 = decoder.ReadWideCharString();
			PathType = decoder.ReadUInt32();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprPathCompare(ServerName, PathName1, PathName2, PathType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetprNameValidate(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string Name;
			uint NameType;
			uint Flags;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Name = decoder.ReadWideCharString();
			NameType = decoder.ReadUInt32();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprNameValidate(ServerName, Name, NameType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetprNameCanonicalize(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string Name;
			RpcPointer<char[]> Outbuf = new RpcPointer<char[]>();
			uint OutbufLen;
			uint NameType;
			uint Flags;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Name = decoder.ReadWideCharString();
			OutbufLen = decoder.ReadUInt32();
			NameType = decoder.ReadUInt32();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprNameCanonicalize(ServerName, Name, Outbuf, OutbufLen, NameType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(Outbuf.value);
			for (int i = 0; i < Outbuf.value.Length; i++)
			{
				char elem_0 = Outbuf.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetprNameCompare(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string Name1;
			string Name2;
			uint NameType;
			uint Flags;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Name1 = decoder.ReadWideCharString();
			Name2 = decoder.ReadWideCharString();
			NameType = decoder.ReadUInt32();
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.NetprNameCompare(ServerName, Name1, Name2, NameType, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareEnumSticky(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<SHARE_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			InfoStruct = new RpcPointer<SHARE_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<SHARE_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SHARE_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrShareEnumSticky(ServerName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareDelStart(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string NetName;
			uint Reserved;
			RpcPointer<RpcContextHandle> ContextHandle = new RpcPointer<RpcContextHandle>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			NetName = decoder.ReadWideCharString();
			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrShareDelStart(ServerName, NetName, Reserved, ContextHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ContextHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareDelCommit(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> ContextHandle;
			ContextHandle = new RpcPointer<RpcContextHandle>();
			ContextHandle.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.NetrShareDelCommit(ContextHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(ContextHandle.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrpGetFileSecurity(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string ShareName;
			string lpFileName;
			uint RequestedInformation;
			RpcPointer<RpcPointer<ADT_SECURITY_DESCRIPTOR>> SecurityDescriptor = new RpcPointer<RpcPointer<ADT_SECURITY_DESCRIPTOR>>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				ShareName = null;
			else
				ShareName = decoder.ReadWideCharString();
			lpFileName = decoder.ReadWideCharString();
			RequestedInformation = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrpGetFileSecurity(ServerName, ShareName, lpFileName, RequestedInformation, SecurityDescriptor, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(SecurityDescriptor.value);
			if (SecurityDescriptor.value is not null)
			{
				encoder.WriteFixedStruct(SecurityDescriptor.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(SecurityDescriptor.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrpSetFileSecurity(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string ShareName;
			string lpFileName;
			uint SecurityInformation;
			ADT_SECURITY_DESCRIPTOR SecurityDescriptor;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				ShareName = null;
			else
				ShareName = decoder.ReadWideCharString();
			lpFileName = decoder.ReadWideCharString();
			SecurityInformation = decoder.ReadUInt32();
			SecurityDescriptor = decoder.ReadFixedStruct<ADT_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ADT_SECURITY_DESCRIPTOR>(ref SecurityDescriptor);
			var invokeTask = this._obj.NetrpSetFileSecurity(ServerName, ShareName, lpFileName, SecurityInformation, SecurityDescriptor, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerTransportAddEx(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			TRANSPORT_INFO Buffer;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			Buffer = decoder.ReadUnion<TRANSPORT_INFO>();
			decoder.ReadStructDeferral<TRANSPORT_INFO>(ref Buffer);
			var invokeTask = this._obj.NetrServerTransportAddEx(ServerName, Level, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum42NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum42NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrDfsGetVersion(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<uint> Version = new RpcPointer<uint>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			var invokeTask = this._obj.NetrDfsGetVersion(ServerName, Version, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(Version.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrDfsCreateLocalPartition(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string ShareName;
			Guid EntryUid;
			string EntryPrefix;
			string ShortName;
			NET_DFS_ENTRY_ID_CONTAINER RelationInfo;
			int Force;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			ShareName = decoder.ReadWideCharString();
			EntryUid = decoder.ReadUuid();
			EntryPrefix = decoder.ReadWideCharString();
			ShortName = decoder.ReadWideCharString();
			RelationInfo = decoder.ReadFixedStruct<NET_DFS_ENTRY_ID_CONTAINER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<NET_DFS_ENTRY_ID_CONTAINER>(ref RelationInfo);
			Force = decoder.ReadInt32();
			var invokeTask = this._obj.NetrDfsCreateLocalPartition(ServerName, ShareName, EntryUid, EntryPrefix, ShortName, RelationInfo, Force, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrDfsDeleteLocalPartition(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			Guid Uid;
			string Prefix;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Uid = decoder.ReadUuid();
			Prefix = decoder.ReadWideCharString();
			var invokeTask = this._obj.NetrDfsDeleteLocalPartition(ServerName, Uid, Prefix, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrDfsSetLocalVolumeState(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			Guid Uid;
			string Prefix;
			uint State;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Uid = decoder.ReadUuid();
			Prefix = decoder.ReadWideCharString();
			State = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrDfsSetLocalVolumeState(ServerName, Uid, Prefix, State, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum47NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum47NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrDfsCreateExitPoint(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			Guid Uid;
			string Prefix;
			uint Type;
			uint ShortPrefixLen;
			RpcPointer<char[]> ShortPrefix = new RpcPointer<char[]>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Uid = decoder.ReadUuid();
			Prefix = decoder.ReadWideCharString();
			Type = decoder.ReadUInt32();
			ShortPrefixLen = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrDfsCreateExitPoint(ServerName, Uid, Prefix, Type, ShortPrefixLen, ShortPrefix, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(ShortPrefix.value);
			for (int i = 0; i < ShortPrefix.value.Length; i++)
			{
				char elem_0 = ShortPrefix.value[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrDfsDeleteExitPoint(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			Guid Uid;
			string Prefix;
			uint Type;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Uid = decoder.ReadUuid();
			Prefix = decoder.ReadWideCharString();
			Type = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrDfsDeleteExitPoint(ServerName, Uid, Prefix, Type, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrDfsModifyPrefix(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			Guid Uid;
			string Prefix;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Uid = decoder.ReadUuid();
			Prefix = decoder.ReadWideCharString();
			var invokeTask = this._obj.NetrDfsModifyPrefix(ServerName, Uid, Prefix, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrDfsFixLocalVolume(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string VolumeName;
			uint EntryType;
			uint ServiceType;
			string StgId;
			Guid EntryUid;
			string EntryPrefix;
			NET_DFS_ENTRY_ID_CONTAINER RelationInfo;
			uint CreateDisposition;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			VolumeName = decoder.ReadWideCharString();
			EntryType = decoder.ReadUInt32();
			ServiceType = decoder.ReadUInt32();
			StgId = decoder.ReadWideCharString();
			EntryUid = decoder.ReadUuid();
			EntryPrefix = decoder.ReadWideCharString();
			RelationInfo = decoder.ReadFixedStruct<NET_DFS_ENTRY_ID_CONTAINER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<NET_DFS_ENTRY_ID_CONTAINER>(ref RelationInfo);
			CreateDisposition = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrDfsFixLocalVolume(ServerName, VolumeName, EntryType, ServiceType, StgId, EntryUid, EntryPrefix, RelationInfo, CreateDisposition, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrDfsManagerReportSiteInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<RpcPointer<DFS_SITELIST_INFO>> ppSiteInfo;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			ppSiteInfo = decoder.ReadUniquePointer<RpcPointer<DFS_SITELIST_INFO>>();
			if (ppSiteInfo is not null)
			{
				ppSiteInfo.value = decoder.ReadUniquePointer<DFS_SITELIST_INFO>();
				if (ppSiteInfo.value is not null)
				{
					ppSiteInfo.value.value = decoder.ReadConformantStruct<DFS_SITELIST_INFO>(NdrAlignment.NativePtr);
					decoder.ReadStructDeferral<DFS_SITELIST_INFO>(ref ppSiteInfo.value.value);
				}
			}

			var invokeTask = this._obj.NetrDfsManagerReportSiteInfo(ServerName, ppSiteInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppSiteInfo);
			if (ppSiteInfo is not null)
			{
				encoder.WriteUniquePointer(ppSiteInfo.value);
				if (ppSiteInfo.value is not null)
				{
					encoder.WriteConformantStruct(ppSiteInfo.value.value, NdrAlignment.NativePtr);
					encoder.WriteStructDeferral(ppSiteInfo.value.value);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerTransportDelEx(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			TRANSPORT_INFO Buffer;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			Buffer = decoder.ReadUnion<TRANSPORT_INFO>();
			decoder.ReadStructDeferral<TRANSPORT_INFO>(ref Buffer);
			var invokeTask = this._obj.NetrServerTransportDelEx(ServerName, Level, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerAliasAdd(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			SERVER_ALIAS_INFO InfoStruct;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			InfoStruct = decoder.ReadUnion<SERVER_ALIAS_INFO>();
			decoder.ReadStructDeferral<SERVER_ALIAS_INFO>(ref InfoStruct);
			var invokeTask = this._obj.NetrServerAliasAdd(ServerName, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerAliasEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<SERVER_ALIAS_ENUM_STRUCT> InfoStruct;
			uint PreferedMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			InfoStruct = new RpcPointer<SERVER_ALIAS_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<SERVER_ALIAS_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<SERVER_ALIAS_ENUM_STRUCT>(ref InfoStruct.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrServerAliasEnum(ServerName, InfoStruct, PreferedMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrServerAliasDel(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			SERVER_ALIAS_INFO InfoStruct;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			InfoStruct = decoder.ReadUnion<SERVER_ALIAS_INFO>();
			decoder.ReadStructDeferral<SERVER_ALIAS_INFO>(ref InfoStruct);
			var invokeTask = this._obj.NetrServerAliasDel(ServerName, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_NetrShareDelEx(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			SHARE_INFO ShareInfo;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			ShareInfo = decoder.ReadUnion<SHARE_INFO>();
			decoder.ReadStructDeferral<SHARE_INFO>(ref ShareInfo);
			var invokeTask = this._obj.NetrShareDelEx(ServerName, Level, ShareInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("4b324fc8-1670-01d3-1278-5a47bf6ee188");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(3, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private srvsvc _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public srvsvcStub(srvsvc obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_Opnum3NotUsedOnWire, this.Invoke_Opnum4NotUsedOnWire, this.Invoke_Opnum5NotUsedOnWire, this.Invoke_Opnum6NotUsedOnWire, this.Invoke_Opnum7NotUsedOnWire, this.Invoke_NetrConnectionEnum, this.Invoke_NetrFileEnum, this.Invoke_NetrFileGetInfo, this.Invoke_NetrFileClose, this.Invoke_NetrSessionEnum, this.Invoke_NetrSessionDel, this.Invoke_NetrShareAdd, this.Invoke_NetrShareEnum, this.Invoke_NetrShareGetInfo, this.Invoke_NetrShareSetInfo, this.Invoke_NetrShareDel, this.Invoke_NetrShareDelSticky, this.Invoke_NetrShareCheck, this.Invoke_NetrServerGetInfo, this.Invoke_NetrServerSetInfo, this.Invoke_NetrServerDiskEnum, this.Invoke_NetrServerStatisticsGet, this.Invoke_NetrServerTransportAdd, this.Invoke_NetrServerTransportEnum, this.Invoke_NetrServerTransportDel, this.Invoke_NetrRemoteTOD, this.Invoke_Opnum29NotUsedOnWire, this.Invoke_NetprPathType, this.Invoke_NetprPathCanonicalize, this.Invoke_NetprPathCompare, this.Invoke_NetprNameValidate, this.Invoke_NetprNameCanonicalize, this.Invoke_NetprNameCompare, this.Invoke_NetrShareEnumSticky, this.Invoke_NetrShareDelStart, this.Invoke_NetrShareDelCommit, this.Invoke_NetrpGetFileSecurity, this.Invoke_NetrpSetFileSecurity, this.Invoke_NetrServerTransportAddEx, this.Invoke_Opnum42NotUsedOnWire, this.Invoke_NetrDfsGetVersion, this.Invoke_NetrDfsCreateLocalPartition, this.Invoke_NetrDfsDeleteLocalPartition, this.Invoke_NetrDfsSetLocalVolumeState, this.Invoke_Opnum47NotUsedOnWire, this.Invoke_NetrDfsCreateExitPoint, this.Invoke_NetrDfsDeleteExitPoint, this.Invoke_NetrDfsModifyPrefix, this.Invoke_NetrDfsFixLocalVolume, this.Invoke_NetrDfsManagerReportSiteInfo, this.Invoke_NetrServerTransportDelEx, this.Invoke_NetrServerAliasAdd, this.Invoke_NetrServerAliasEnum, this.Invoke_NetrServerAliasDel, this.Invoke_NetrShareDelEx};
		}
	}
}