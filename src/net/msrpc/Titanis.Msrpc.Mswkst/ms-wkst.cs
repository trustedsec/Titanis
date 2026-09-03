namespace ms_wkst
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum NETSETUP_JOIN_STATUS : int
	{
		NetSetupUnknownStatus = 0,
		NetSetupUnjoined = 1,
		NetSetupWorkgroupName = 2,
		NetSetupDomainName = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum NETSETUP_NAME_TYPE : int
	{
		NetSetupUnknown = 0,
		NetSetupMachine = 1,
		NetSetupWorkgroup = 2,
		NetSetupDomain = 3,
		NetSetupNonExistentDomain = 4,
		NetSetupDnsMachine = 5
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public enum NET_COMPUTER_NAME_TYPE : int
	{
		NetPrimaryComputerName = 0,
		NetAlternateComputerNames = 1,
		NetAllComputerNames = 2,
		NetComputerNameTypeMax = 3
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct STAT_WORKSTATION_0 : IRpcFixedStruct
	{
		public ms_dtyp.LARGE_INTEGER StatisticsStartTime;
		public ms_dtyp.LARGE_INTEGER BytesReceived;
		public ms_dtyp.LARGE_INTEGER SmbsReceived;
		public ms_dtyp.LARGE_INTEGER PagingReadBytesRequested;
		public ms_dtyp.LARGE_INTEGER NonPagingReadBytesRequested;
		public ms_dtyp.LARGE_INTEGER CacheReadBytesRequested;
		public ms_dtyp.LARGE_INTEGER NetworkReadBytesRequested;
		public ms_dtyp.LARGE_INTEGER BytesTransmitted;
		public ms_dtyp.LARGE_INTEGER SmbsTransmitted;
		public ms_dtyp.LARGE_INTEGER PagingWriteBytesRequested;
		public ms_dtyp.LARGE_INTEGER NonPagingWriteBytesRequested;
		public ms_dtyp.LARGE_INTEGER CacheWriteBytesRequested;
		public ms_dtyp.LARGE_INTEGER NetworkWriteBytesRequested;
		public uint InitiallyFailedOperations;
		public uint FailedCompletionOperations;
		public uint ReadOperations;
		public uint RandomReadOperations;
		public uint ReadSmbs;
		public uint LargeReadSmbs;
		public uint SmallReadSmbs;
		public uint WriteOperations;
		public uint RandomWriteOperations;
		public uint WriteSmbs;
		public uint LargeWriteSmbs;
		public uint SmallWriteSmbs;
		public uint RawReadsDenied;
		public uint RawWritesDenied;
		public uint NetworkErrors;
		public uint Sessions;
		public uint FailedSessions;
		public uint Reconnects;
		public uint CoreConnects;
		public uint Lanman20Connects;
		public uint Lanman21Connects;
		public uint LanmanNtConnects;
		public uint ServerDisconnects;
		public uint HungSessions;
		public uint UseCount;
		public uint FailedUseCount;
		public uint CurrentCommands;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.StatisticsStartTime, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.BytesReceived, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.SmbsReceived, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.PagingReadBytesRequested, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.NonPagingReadBytesRequested, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.CacheReadBytesRequested, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.NetworkReadBytesRequested, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.BytesTransmitted, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.SmbsTransmitted, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.PagingWriteBytesRequested, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.NonPagingWriteBytesRequested, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.CacheWriteBytesRequested, NdrAlignment._8Byte);
			encoder.WriteFixedStruct(this.NetworkWriteBytesRequested, NdrAlignment._8Byte);
			encoder.WriteValue(this.InitiallyFailedOperations);
			encoder.WriteValue(this.FailedCompletionOperations);
			encoder.WriteValue(this.ReadOperations);
			encoder.WriteValue(this.RandomReadOperations);
			encoder.WriteValue(this.ReadSmbs);
			encoder.WriteValue(this.LargeReadSmbs);
			encoder.WriteValue(this.SmallReadSmbs);
			encoder.WriteValue(this.WriteOperations);
			encoder.WriteValue(this.RandomWriteOperations);
			encoder.WriteValue(this.WriteSmbs);
			encoder.WriteValue(this.LargeWriteSmbs);
			encoder.WriteValue(this.SmallWriteSmbs);
			encoder.WriteValue(this.RawReadsDenied);
			encoder.WriteValue(this.RawWritesDenied);
			encoder.WriteValue(this.NetworkErrors);
			encoder.WriteValue(this.Sessions);
			encoder.WriteValue(this.FailedSessions);
			encoder.WriteValue(this.Reconnects);
			encoder.WriteValue(this.CoreConnects);
			encoder.WriteValue(this.Lanman20Connects);
			encoder.WriteValue(this.Lanman21Connects);
			encoder.WriteValue(this.LanmanNtConnects);
			encoder.WriteValue(this.ServerDisconnects);
			encoder.WriteValue(this.HungSessions);
			encoder.WriteValue(this.UseCount);
			encoder.WriteValue(this.FailedUseCount);
			encoder.WriteValue(this.CurrentCommands);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.StatisticsStartTime = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.BytesReceived = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.SmbsReceived = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.PagingReadBytesRequested = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.NonPagingReadBytesRequested = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.CacheReadBytesRequested = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.NetworkReadBytesRequested = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.BytesTransmitted = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.SmbsTransmitted = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.PagingWriteBytesRequested = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.NonPagingWriteBytesRequested = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.CacheWriteBytesRequested = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.NetworkWriteBytesRequested = decoder.ReadFixedStruct<ms_dtyp.LARGE_INTEGER>(NdrAlignment._8Byte);
			this.InitiallyFailedOperations = decoder.ReadUInt32();
			this.FailedCompletionOperations = decoder.ReadUInt32();
			this.ReadOperations = decoder.ReadUInt32();
			this.RandomReadOperations = decoder.ReadUInt32();
			this.ReadSmbs = decoder.ReadUInt32();
			this.LargeReadSmbs = decoder.ReadUInt32();
			this.SmallReadSmbs = decoder.ReadUInt32();
			this.WriteOperations = decoder.ReadUInt32();
			this.RandomWriteOperations = decoder.ReadUInt32();
			this.WriteSmbs = decoder.ReadUInt32();
			this.LargeWriteSmbs = decoder.ReadUInt32();
			this.SmallWriteSmbs = decoder.ReadUInt32();
			this.RawReadsDenied = decoder.ReadUInt32();
			this.RawWritesDenied = decoder.ReadUInt32();
			this.NetworkErrors = decoder.ReadUInt32();
			this.Sessions = decoder.ReadUInt32();
			this.FailedSessions = decoder.ReadUInt32();
			this.Reconnects = decoder.ReadUInt32();
			this.CoreConnects = decoder.ReadUInt32();
			this.Lanman20Connects = decoder.ReadUInt32();
			this.Lanman21Connects = decoder.ReadUInt32();
			this.LanmanNtConnects = decoder.ReadUInt32();
			this.ServerDisconnects = decoder.ReadUInt32();
			this.HungSessions = decoder.ReadUInt32();
			this.UseCount = decoder.ReadUInt32();
			this.FailedUseCount = decoder.ReadUInt32();
			this.CurrentCommands = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.StatisticsStartTime);
			encoder.WriteStructDeferral(this.BytesReceived);
			encoder.WriteStructDeferral(this.SmbsReceived);
			encoder.WriteStructDeferral(this.PagingReadBytesRequested);
			encoder.WriteStructDeferral(this.NonPagingReadBytesRequested);
			encoder.WriteStructDeferral(this.CacheReadBytesRequested);
			encoder.WriteStructDeferral(this.NetworkReadBytesRequested);
			encoder.WriteStructDeferral(this.BytesTransmitted);
			encoder.WriteStructDeferral(this.SmbsTransmitted);
			encoder.WriteStructDeferral(this.PagingWriteBytesRequested);
			encoder.WriteStructDeferral(this.NonPagingWriteBytesRequested);
			encoder.WriteStructDeferral(this.CacheWriteBytesRequested);
			encoder.WriteStructDeferral(this.NetworkWriteBytesRequested);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.StatisticsStartTime);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.BytesReceived);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.SmbsReceived);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.PagingReadBytesRequested);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.NonPagingReadBytesRequested);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.CacheReadBytesRequested);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.NetworkReadBytesRequested);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.BytesTransmitted);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.SmbsTransmitted);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.PagingWriteBytesRequested);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.NonPagingWriteBytesRequested);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.CacheWriteBytesRequested);
			decoder.ReadStructDeferral<ms_dtyp.LARGE_INTEGER>(ref this.NetworkWriteBytesRequested);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_INFO_100 : IRpcFixedStruct
	{
		public uint wki100_platform_id;
		public RpcPointer<string> wki100_computername;
		public RpcPointer<string> wki100_langroup;
		public uint wki100_ver_major;
		public uint wki100_ver_minor;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wki100_platform_id);
			encoder.WriteUniquePointer(this.wki100_computername);
			encoder.WriteUniquePointer(this.wki100_langroup);
			encoder.WriteValue(this.wki100_ver_major);
			encoder.WriteValue(this.wki100_ver_minor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wki100_platform_id = decoder.ReadUInt32();
			this.wki100_computername = decoder.ReadUniquePointer<string>();
			this.wki100_langroup = decoder.ReadUniquePointer<string>();
			this.wki100_ver_major = decoder.ReadUInt32();
			this.wki100_ver_minor = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wki100_computername is not null)
			{
				encoder.WriteWideCharString(this.wki100_computername.value);
			}

			if (this.wki100_langroup is not null)
			{
				encoder.WriteWideCharString(this.wki100_langroup.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wki100_computername is not null)
			{
				this.wki100_computername.value = decoder.ReadWideCharString();
			}

			if (this.wki100_langroup is not null)
			{
				this.wki100_langroup.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_INFO_101 : IRpcFixedStruct
	{
		public uint wki101_platform_id;
		public RpcPointer<string> wki101_computername;
		public RpcPointer<string> wki101_langroup;
		public uint wki101_ver_major;
		public uint wki101_ver_minor;
		public RpcPointer<string> wki101_lanroot;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wki101_platform_id);
			encoder.WriteUniquePointer(this.wki101_computername);
			encoder.WriteUniquePointer(this.wki101_langroup);
			encoder.WriteValue(this.wki101_ver_major);
			encoder.WriteValue(this.wki101_ver_minor);
			encoder.WriteUniquePointer(this.wki101_lanroot);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wki101_platform_id = decoder.ReadUInt32();
			this.wki101_computername = decoder.ReadUniquePointer<string>();
			this.wki101_langroup = decoder.ReadUniquePointer<string>();
			this.wki101_ver_major = decoder.ReadUInt32();
			this.wki101_ver_minor = decoder.ReadUInt32();
			this.wki101_lanroot = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wki101_computername is not null)
			{
				encoder.WriteWideCharString(this.wki101_computername.value);
			}

			if (this.wki101_langroup is not null)
			{
				encoder.WriteWideCharString(this.wki101_langroup.value);
			}

			if (this.wki101_lanroot is not null)
			{
				encoder.WriteWideCharString(this.wki101_lanroot.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wki101_computername is not null)
			{
				this.wki101_computername.value = decoder.ReadWideCharString();
			}

			if (this.wki101_langroup is not null)
			{
				this.wki101_langroup.value = decoder.ReadWideCharString();
			}

			if (this.wki101_lanroot is not null)
			{
				this.wki101_lanroot.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_INFO_102 : IRpcFixedStruct
	{
		public uint wki102_platform_id;
		public RpcPointer<string> wki102_computername;
		public RpcPointer<string> wki102_langroup;
		public uint wki102_ver_major;
		public uint wki102_ver_minor;
		public RpcPointer<string> wki102_lanroot;
		public uint wki102_logged_on_users;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wki102_platform_id);
			encoder.WriteUniquePointer(this.wki102_computername);
			encoder.WriteUniquePointer(this.wki102_langroup);
			encoder.WriteValue(this.wki102_ver_major);
			encoder.WriteValue(this.wki102_ver_minor);
			encoder.WriteUniquePointer(this.wki102_lanroot);
			encoder.WriteValue(this.wki102_logged_on_users);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wki102_platform_id = decoder.ReadUInt32();
			this.wki102_computername = decoder.ReadUniquePointer<string>();
			this.wki102_langroup = decoder.ReadUniquePointer<string>();
			this.wki102_ver_major = decoder.ReadUInt32();
			this.wki102_ver_minor = decoder.ReadUInt32();
			this.wki102_lanroot = decoder.ReadUniquePointer<string>();
			this.wki102_logged_on_users = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wki102_computername is not null)
			{
				encoder.WriteWideCharString(this.wki102_computername.value);
			}

			if (this.wki102_langroup is not null)
			{
				encoder.WriteWideCharString(this.wki102_langroup.value);
			}

			if (this.wki102_lanroot is not null)
			{
				encoder.WriteWideCharString(this.wki102_lanroot.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wki102_computername is not null)
			{
				this.wki102_computername.value = decoder.ReadWideCharString();
			}

			if (this.wki102_langroup is not null)
			{
				this.wki102_langroup.value = decoder.ReadWideCharString();
			}

			if (this.wki102_lanroot is not null)
			{
				this.wki102_lanroot.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_INFO_502 : IRpcFixedStruct
	{
		public uint wki502_char_wait;
		public uint wki502_collection_time;
		public uint wki502_maximum_collection_count;
		public uint wki502_keep_conn;
		public uint wki502_max_cmds;
		public uint wki502_sess_timeout;
		public uint wki502_siz_char_buf;
		public uint wki502_max_threads;
		public uint wki502_lock_quota;
		public uint wki502_lock_increment;
		public uint wki502_lock_maximum;
		public uint wki502_pipe_increment;
		public uint wki502_pipe_maximum;
		public uint wki502_cache_file_timeout;
		public uint wki502_dormant_file_limit;
		public uint wki502_read_ahead_throughput;
		public uint wki502_num_mailslot_buffers;
		public uint wki502_num_srv_announce_buffers;
		public uint wki502_max_illegal_datagram_events;
		public uint wki502_illegal_datagram_event_reset_frequency;
		public int wki502_log_election_packets;
		public int wki502_use_opportunistic_locking;
		public int wki502_use_unlock_behind;
		public int wki502_use_close_behind;
		public int wki502_buf_named_pipes;
		public int wki502_use_lock_read_unlock;
		public int wki502_utilize_nt_caching;
		public int wki502_use_raw_read;
		public int wki502_use_raw_write;
		public int wki502_use_write_raw_data;
		public int wki502_use_encryption;
		public int wki502_buf_files_deny_write;
		public int wki502_buf_read_only_files;
		public int wki502_force_core_create_mode;
		public int wki502_use_512_byte_max_transfer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wki502_char_wait);
			encoder.WriteValue(this.wki502_collection_time);
			encoder.WriteValue(this.wki502_maximum_collection_count);
			encoder.WriteValue(this.wki502_keep_conn);
			encoder.WriteValue(this.wki502_max_cmds);
			encoder.WriteValue(this.wki502_sess_timeout);
			encoder.WriteValue(this.wki502_siz_char_buf);
			encoder.WriteValue(this.wki502_max_threads);
			encoder.WriteValue(this.wki502_lock_quota);
			encoder.WriteValue(this.wki502_lock_increment);
			encoder.WriteValue(this.wki502_lock_maximum);
			encoder.WriteValue(this.wki502_pipe_increment);
			encoder.WriteValue(this.wki502_pipe_maximum);
			encoder.WriteValue(this.wki502_cache_file_timeout);
			encoder.WriteValue(this.wki502_dormant_file_limit);
			encoder.WriteValue(this.wki502_read_ahead_throughput);
			encoder.WriteValue(this.wki502_num_mailslot_buffers);
			encoder.WriteValue(this.wki502_num_srv_announce_buffers);
			encoder.WriteValue(this.wki502_max_illegal_datagram_events);
			encoder.WriteValue(this.wki502_illegal_datagram_event_reset_frequency);
			encoder.WriteValue(this.wki502_log_election_packets);
			encoder.WriteValue(this.wki502_use_opportunistic_locking);
			encoder.WriteValue(this.wki502_use_unlock_behind);
			encoder.WriteValue(this.wki502_use_close_behind);
			encoder.WriteValue(this.wki502_buf_named_pipes);
			encoder.WriteValue(this.wki502_use_lock_read_unlock);
			encoder.WriteValue(this.wki502_utilize_nt_caching);
			encoder.WriteValue(this.wki502_use_raw_read);
			encoder.WriteValue(this.wki502_use_raw_write);
			encoder.WriteValue(this.wki502_use_write_raw_data);
			encoder.WriteValue(this.wki502_use_encryption);
			encoder.WriteValue(this.wki502_buf_files_deny_write);
			encoder.WriteValue(this.wki502_buf_read_only_files);
			encoder.WriteValue(this.wki502_force_core_create_mode);
			encoder.WriteValue(this.wki502_use_512_byte_max_transfer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wki502_char_wait = decoder.ReadUInt32();
			this.wki502_collection_time = decoder.ReadUInt32();
			this.wki502_maximum_collection_count = decoder.ReadUInt32();
			this.wki502_keep_conn = decoder.ReadUInt32();
			this.wki502_max_cmds = decoder.ReadUInt32();
			this.wki502_sess_timeout = decoder.ReadUInt32();
			this.wki502_siz_char_buf = decoder.ReadUInt32();
			this.wki502_max_threads = decoder.ReadUInt32();
			this.wki502_lock_quota = decoder.ReadUInt32();
			this.wki502_lock_increment = decoder.ReadUInt32();
			this.wki502_lock_maximum = decoder.ReadUInt32();
			this.wki502_pipe_increment = decoder.ReadUInt32();
			this.wki502_pipe_maximum = decoder.ReadUInt32();
			this.wki502_cache_file_timeout = decoder.ReadUInt32();
			this.wki502_dormant_file_limit = decoder.ReadUInt32();
			this.wki502_read_ahead_throughput = decoder.ReadUInt32();
			this.wki502_num_mailslot_buffers = decoder.ReadUInt32();
			this.wki502_num_srv_announce_buffers = decoder.ReadUInt32();
			this.wki502_max_illegal_datagram_events = decoder.ReadUInt32();
			this.wki502_illegal_datagram_event_reset_frequency = decoder.ReadUInt32();
			this.wki502_log_election_packets = decoder.ReadInt32();
			this.wki502_use_opportunistic_locking = decoder.ReadInt32();
			this.wki502_use_unlock_behind = decoder.ReadInt32();
			this.wki502_use_close_behind = decoder.ReadInt32();
			this.wki502_buf_named_pipes = decoder.ReadInt32();
			this.wki502_use_lock_read_unlock = decoder.ReadInt32();
			this.wki502_utilize_nt_caching = decoder.ReadInt32();
			this.wki502_use_raw_read = decoder.ReadInt32();
			this.wki502_use_raw_write = decoder.ReadInt32();
			this.wki502_use_write_raw_data = decoder.ReadInt32();
			this.wki502_use_encryption = decoder.ReadInt32();
			this.wki502_buf_files_deny_write = decoder.ReadInt32();
			this.wki502_buf_read_only_files = decoder.ReadInt32();
			this.wki502_force_core_create_mode = decoder.ReadInt32();
			this.wki502_use_512_byte_max_transfer = decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_INFO_1013 : IRpcFixedStruct
	{
		public uint wki1013_keep_conn;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wki1013_keep_conn);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wki1013_keep_conn = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_INFO_1018 : IRpcFixedStruct
	{
		public uint wki1018_sess_timeout;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wki1018_sess_timeout);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wki1018_sess_timeout = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_INFO_1046 : IRpcFixedStruct
	{
		public uint wki1046_dormant_file_limit;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wki1046_dormant_file_limit);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wki1046_dormant_file_limit = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_USER_INFO_0 : IRpcFixedStruct
	{
		public RpcPointer<string> wkui0_username;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.wkui0_username);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wkui0_username = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wkui0_username is not null)
			{
				encoder.WriteWideCharString(this.wkui0_username.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wkui0_username is not null)
			{
				this.wkui0_username.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_USER_INFO_1 : IRpcFixedStruct
	{
		public RpcPointer<string> wkui1_username;
		public RpcPointer<string> wkui1_logon_domain;
		public RpcPointer<string> wkui1_oth_domains;
		public RpcPointer<string> wkui1_logon_server;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.wkui1_username);
			encoder.WriteUniquePointer(this.wkui1_logon_domain);
			encoder.WriteUniquePointer(this.wkui1_oth_domains);
			encoder.WriteUniquePointer(this.wkui1_logon_server);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wkui1_username = decoder.ReadUniquePointer<string>();
			this.wkui1_logon_domain = decoder.ReadUniquePointer<string>();
			this.wkui1_oth_domains = decoder.ReadUniquePointer<string>();
			this.wkui1_logon_server = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wkui1_username is not null)
			{
				encoder.WriteWideCharString(this.wkui1_username.value);
			}

			if (this.wkui1_logon_domain is not null)
			{
				encoder.WriteWideCharString(this.wkui1_logon_domain.value);
			}

			if (this.wkui1_oth_domains is not null)
			{
				encoder.WriteWideCharString(this.wkui1_oth_domains.value);
			}

			if (this.wkui1_logon_server is not null)
			{
				encoder.WriteWideCharString(this.wkui1_logon_server.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wkui1_username is not null)
			{
				this.wkui1_username.value = decoder.ReadWideCharString();
			}

			if (this.wkui1_logon_domain is not null)
			{
				this.wkui1_logon_domain.value = decoder.ReadWideCharString();
			}

			if (this.wkui1_oth_domains is not null)
			{
				this.wkui1_oth_domains.value = decoder.ReadWideCharString();
			}

			if (this.wkui1_logon_server is not null)
			{
				this.wkui1_logon_server.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_TRANSPORT_INFO_0 : IRpcFixedStruct
	{
		public uint wkti0_quality_of_service;
		public uint wkti0_number_of_vcs;
		public RpcPointer<string> wkti0_transport_name;
		public RpcPointer<string> wkti0_transport_address;
		public uint wkti0_wan_ish;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.wkti0_quality_of_service);
			encoder.WriteValue(this.wkti0_number_of_vcs);
			encoder.WriteUniquePointer(this.wkti0_transport_name);
			encoder.WriteUniquePointer(this.wkti0_transport_address);
			encoder.WriteValue(this.wkti0_wan_ish);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.wkti0_quality_of_service = decoder.ReadUInt32();
			this.wkti0_number_of_vcs = decoder.ReadUInt32();
			this.wkti0_transport_name = decoder.ReadUniquePointer<string>();
			this.wkti0_transport_address = decoder.ReadUniquePointer<string>();
			this.wkti0_wan_ish = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.wkti0_transport_name is not null)
			{
				encoder.WriteWideCharString(this.wkti0_transport_name.value);
			}

			if (this.wkti0_transport_address is not null)
			{
				encoder.WriteWideCharString(this.wkti0_transport_address.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.wkti0_transport_name is not null)
			{
				this.wkti0_transport_name.value = decoder.ReadWideCharString();
			}

			if (this.wkti0_transport_address is not null)
			{
				this.wkti0_transport_address.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_INFO : IRpcFixedStruct
	{
		public uint unionSwitch;
		public RpcPointer<WKSTA_INFO_100> WkstaInfo100;
		public RpcPointer<WKSTA_INFO_101> WkstaInfo101;
		public RpcPointer<WKSTA_INFO_102> WkstaInfo102;
		public RpcPointer<WKSTA_INFO_502> WkstaInfo502;
		public RpcPointer<WKSTA_INFO_1013> WkstaInfo1013;
		public RpcPointer<WKSTA_INFO_1018> WkstaInfo1018;
		public RpcPointer<WKSTA_INFO_1046> WkstaInfo1046;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((uint)this.unionSwitch)
			{
				case 100U:
					encoder.WriteUniquePointer(this.WkstaInfo100);
					break;
				case 101U:
					encoder.WriteUniquePointer(this.WkstaInfo101);
					break;
				case 102U:
					encoder.WriteUniquePointer(this.WkstaInfo102);
					break;
				case 502U:
					encoder.WriteUniquePointer(this.WkstaInfo502);
					break;
				case 1013U:
					encoder.WriteUniquePointer(this.WkstaInfo1013);
					break;
				case 1018U:
					encoder.WriteUniquePointer(this.WkstaInfo1018);
					break;
				case 1046U:
					encoder.WriteUniquePointer(this.WkstaInfo1046);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((uint)this.unionSwitch)
			{
				case 100U:
					this.WkstaInfo100 = decoder.ReadUniquePointer<WKSTA_INFO_100>();
					break;
				case 101U:
					this.WkstaInfo101 = decoder.ReadUniquePointer<WKSTA_INFO_101>();
					break;
				case 102U:
					this.WkstaInfo102 = decoder.ReadUniquePointer<WKSTA_INFO_102>();
					break;
				case 502U:
					this.WkstaInfo502 = decoder.ReadUniquePointer<WKSTA_INFO_502>();
					break;
				case 1013U:
					this.WkstaInfo1013 = decoder.ReadUniquePointer<WKSTA_INFO_1013>();
					break;
				case 1018U:
					this.WkstaInfo1018 = decoder.ReadUniquePointer<WKSTA_INFO_1018>();
					break;
				case 1046U:
					this.WkstaInfo1046 = decoder.ReadUniquePointer<WKSTA_INFO_1046>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 100U:
					if (this.WkstaInfo100 is not null)
					{
						encoder.WriteFixedStruct(this.WkstaInfo100.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.WkstaInfo100.value);
					}

					break;
				case 101U:
					if (this.WkstaInfo101 is not null)
					{
						encoder.WriteFixedStruct(this.WkstaInfo101.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.WkstaInfo101.value);
					}

					break;
				case 102U:
					if (this.WkstaInfo102 is not null)
					{
						encoder.WriteFixedStruct(this.WkstaInfo102.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.WkstaInfo102.value);
					}

					break;
				case 502U:
					if (this.WkstaInfo502 is not null)
					{
						encoder.WriteFixedStruct(this.WkstaInfo502.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.WkstaInfo502.value);
					}

					break;
				case 1013U:
					if (this.WkstaInfo1013 is not null)
					{
						encoder.WriteFixedStruct(this.WkstaInfo1013.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.WkstaInfo1013.value);
					}

					break;
				case 1018U:
					if (this.WkstaInfo1018 is not null)
					{
						encoder.WriteFixedStruct(this.WkstaInfo1018.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.WkstaInfo1018.value);
					}

					break;
				case 1046U:
					if (this.WkstaInfo1046 is not null)
					{
						encoder.WriteFixedStruct(this.WkstaInfo1046.value, NdrAlignment._4Byte);
						encoder.WriteStructDeferral(this.WkstaInfo1046.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 100U:
					if (this.WkstaInfo100 is not null)
					{
						this.WkstaInfo100.value = decoder.ReadFixedStruct<WKSTA_INFO_100>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<WKSTA_INFO_100>(ref this.WkstaInfo100.value);
					}

					break;
				case 101U:
					if (this.WkstaInfo101 is not null)
					{
						this.WkstaInfo101.value = decoder.ReadFixedStruct<WKSTA_INFO_101>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<WKSTA_INFO_101>(ref this.WkstaInfo101.value);
					}

					break;
				case 102U:
					if (this.WkstaInfo102 is not null)
					{
						this.WkstaInfo102.value = decoder.ReadFixedStruct<WKSTA_INFO_102>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<WKSTA_INFO_102>(ref this.WkstaInfo102.value);
					}

					break;
				case 502U:
					if (this.WkstaInfo502 is not null)
					{
						this.WkstaInfo502.value = decoder.ReadFixedStruct<WKSTA_INFO_502>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<WKSTA_INFO_502>(ref this.WkstaInfo502.value);
					}

					break;
				case 1013U:
					if (this.WkstaInfo1013 is not null)
					{
						this.WkstaInfo1013.value = decoder.ReadFixedStruct<WKSTA_INFO_1013>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<WKSTA_INFO_1013>(ref this.WkstaInfo1013.value);
					}

					break;
				case 1018U:
					if (this.WkstaInfo1018 is not null)
					{
						this.WkstaInfo1018.value = decoder.ReadFixedStruct<WKSTA_INFO_1018>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<WKSTA_INFO_1018>(ref this.WkstaInfo1018.value);
					}

					break;
				case 1046U:
					if (this.WkstaInfo1046 is not null)
					{
						this.WkstaInfo1046.value = decoder.ReadFixedStruct<WKSTA_INFO_1046>(NdrAlignment._4Byte);
						decoder.ReadStructDeferral<WKSTA_INFO_1046>(ref this.WkstaInfo1046.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct USE_INFO_0 : IRpcFixedStruct
	{
		public RpcPointer<string> ui0_local;
		public RpcPointer<string> ui0_remote;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.ui0_local);
			encoder.WriteUniquePointer(this.ui0_remote);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ui0_local = decoder.ReadUniquePointer<string>();
			this.ui0_remote = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ui0_local is not null)
			{
				encoder.WriteWideCharString(this.ui0_local.value);
			}

			if (this.ui0_remote is not null)
			{
				encoder.WriteWideCharString(this.ui0_remote.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ui0_local is not null)
			{
				this.ui0_local.value = decoder.ReadWideCharString();
			}

			if (this.ui0_remote is not null)
			{
				this.ui0_remote.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct USE_INFO_1 : IRpcFixedStruct
	{
		public RpcPointer<string> ui1_local;
		public RpcPointer<string> ui1_remote;
		public RpcPointer<string> ui1_password;
		public uint ui1_status;
		public uint ui1_asg_type;
		public uint ui1_refcount;
		public uint ui1_usecount;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.ui1_local);
			encoder.WriteUniquePointer(this.ui1_remote);
			encoder.WriteUniquePointer(this.ui1_password);
			encoder.WriteValue(this.ui1_status);
			encoder.WriteValue(this.ui1_asg_type);
			encoder.WriteValue(this.ui1_refcount);
			encoder.WriteValue(this.ui1_usecount);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ui1_local = decoder.ReadUniquePointer<string>();
			this.ui1_remote = decoder.ReadUniquePointer<string>();
			this.ui1_password = decoder.ReadUniquePointer<string>();
			this.ui1_status = decoder.ReadUInt32();
			this.ui1_asg_type = decoder.ReadUInt32();
			this.ui1_refcount = decoder.ReadUInt32();
			this.ui1_usecount = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ui1_local is not null)
			{
				encoder.WriteWideCharString(this.ui1_local.value);
			}

			if (this.ui1_remote is not null)
			{
				encoder.WriteWideCharString(this.ui1_remote.value);
			}

			if (this.ui1_password is not null)
			{
				encoder.WriteWideCharString(this.ui1_password.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ui1_local is not null)
			{
				this.ui1_local.value = decoder.ReadWideCharString();
			}

			if (this.ui1_remote is not null)
			{
				this.ui1_remote.value = decoder.ReadWideCharString();
			}

			if (this.ui1_password is not null)
			{
				this.ui1_password.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct USE_INFO_2 : IRpcFixedStruct
	{
		public USE_INFO_1 ui2_useinfo;
		public RpcPointer<string> ui2_username;
		public RpcPointer<string> ui2_domainname;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ui2_useinfo, NdrAlignment.NativePtr);
			encoder.WriteUniquePointer(this.ui2_username);
			encoder.WriteUniquePointer(this.ui2_domainname);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ui2_useinfo = decoder.ReadFixedStruct<USE_INFO_1>(NdrAlignment.NativePtr);
			this.ui2_username = decoder.ReadUniquePointer<string>();
			this.ui2_domainname = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ui2_useinfo);
			if (this.ui2_username is not null)
			{
				encoder.WriteWideCharString(this.ui2_username.value);
			}

			if (this.ui2_domainname is not null)
			{
				encoder.WriteWideCharString(this.ui2_domainname.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<USE_INFO_1>(ref this.ui2_useinfo);
			if (this.ui2_username is not null)
			{
				this.ui2_username.value = decoder.ReadWideCharString();
			}

			if (this.ui2_domainname is not null)
			{
				this.ui2_domainname.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct USE_INFO_3 : IRpcFixedStruct
	{
		public USE_INFO_2 ui3_ui2;
		public uint ui3_flags;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.ui3_ui2, NdrAlignment.NativePtr);
			encoder.WriteValue(this.ui3_flags);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ui3_ui2 = decoder.ReadFixedStruct<USE_INFO_2>(NdrAlignment.NativePtr);
			this.ui3_flags = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.ui3_ui2);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<USE_INFO_2>(ref this.ui3_ui2);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct USE_INFO : IRpcFixedStruct
	{
		public uint unionSwitch;
		public RpcPointer<USE_INFO_0> UseInfo0;
		public RpcPointer<USE_INFO_1> UseInfo1;
		public RpcPointer<USE_INFO_2> UseInfo2;
		public RpcPointer<USE_INFO_3> UseInfo3;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.unionSwitch);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					encoder.WriteUniquePointer(this.UseInfo0);
					break;
				case 1U:
					encoder.WriteUniquePointer(this.UseInfo1);
					break;
				case 2U:
					encoder.WriteUniquePointer(this.UseInfo2);
					break;
				case 3U:
					encoder.WriteUniquePointer(this.UseInfo3);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.unionSwitch = decoder.ReadUInt32();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					this.UseInfo0 = decoder.ReadUniquePointer<USE_INFO_0>();
					break;
				case 1U:
					this.UseInfo1 = decoder.ReadUniquePointer<USE_INFO_1>();
					break;
				case 2U:
					this.UseInfo2 = decoder.ReadUniquePointer<USE_INFO_2>();
					break;
				case 3U:
					this.UseInfo3 = decoder.ReadUniquePointer<USE_INFO_3>();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					if (this.UseInfo0 is not null)
					{
						encoder.WriteFixedStruct(this.UseInfo0.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.UseInfo0.value);
					}

					break;
				case 1U:
					if (this.UseInfo1 is not null)
					{
						encoder.WriteFixedStruct(this.UseInfo1.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.UseInfo1.value);
					}

					break;
				case 2U:
					if (this.UseInfo2 is not null)
					{
						encoder.WriteFixedStruct(this.UseInfo2.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.UseInfo2.value);
					}

					break;
				case 3U:
					if (this.UseInfo3 is not null)
					{
						encoder.WriteFixedStruct(this.UseInfo3.value, NdrAlignment.NativePtr);
						encoder.WriteStructDeferral(this.UseInfo3.value);
					}

					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.unionSwitch)
			{
				case 0U:
					if (this.UseInfo0 is not null)
					{
						this.UseInfo0.value = decoder.ReadFixedStruct<USE_INFO_0>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<USE_INFO_0>(ref this.UseInfo0.value);
					}

					break;
				case 1U:
					if (this.UseInfo1 is not null)
					{
						this.UseInfo1.value = decoder.ReadFixedStruct<USE_INFO_1>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<USE_INFO_1>(ref this.UseInfo1.value);
					}

					break;
				case 2U:
					if (this.UseInfo2 is not null)
					{
						this.UseInfo2.value = decoder.ReadFixedStruct<USE_INFO_2>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<USE_INFO_2>(ref this.UseInfo2.value);
					}

					break;
				case 3U:
					if (this.UseInfo3 is not null)
					{
						this.UseInfo3.value = decoder.ReadFixedStruct<USE_INFO_3>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<USE_INFO_3>(ref this.UseInfo3.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct USE_INFO_0_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<USE_INFO_0[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<USE_INFO_0[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<USE_INFO_0>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<USE_INFO_0>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<USE_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct USE_INFO_1_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<USE_INFO_1[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<USE_INFO_1[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<USE_INFO_1>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_1 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<USE_INFO_1>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_1 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<USE_INFO_1>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct USE_INFO_2_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<USE_INFO_2[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<USE_INFO_2[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_2 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<USE_INFO_2>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_2 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<USE_INFO_2>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					USE_INFO_2 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<USE_INFO_2>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct _USE_ENUM_UNION : IRpcFixedStruct
	{
		public uint Level;
		public RpcPointer<USE_INFO_0_CONTAINER> Level0;
		public RpcPointer<USE_INFO_1_CONTAINER> Level1;
		public RpcPointer<USE_INFO_2_CONTAINER> Level2;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.Level);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
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
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.Level = decoder.ReadUInt32();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((uint)this.Level)
			{
				case 0U:
					this.Level0 = decoder.ReadUniquePointer<USE_INFO_0_CONTAINER>();
					break;
				case 1U:
					this.Level1 = decoder.ReadUniquePointer<USE_INFO_1_CONTAINER>();
					break;
				case 2U:
					this.Level2 = decoder.ReadUniquePointer<USE_INFO_2_CONTAINER>();
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
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((uint)this.Level)
			{
				case 0U:
					if (this.Level0 is not null)
					{
						this.Level0.value = decoder.ReadFixedStruct<USE_INFO_0_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<USE_INFO_0_CONTAINER>(ref this.Level0.value);
					}

					break;
				case 1U:
					if (this.Level1 is not null)
					{
						this.Level1.value = decoder.ReadFixedStruct<USE_INFO_1_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<USE_INFO_1_CONTAINER>(ref this.Level1.value);
					}

					break;
				case 2U:
					if (this.Level2 is not null)
					{
						this.Level2.value = decoder.ReadFixedStruct<USE_INFO_2_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<USE_INFO_2_CONTAINER>(ref this.Level2.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct USE_ENUM_STRUCT : IRpcFixedStruct
	{
		public uint Level;
		public _USE_ENUM_UNION UseInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.UseInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.UseInfo = decoder.ReadUnion<_USE_ENUM_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.UseInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<_USE_ENUM_UNION>(ref this.UseInfo);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_USER_INFO_0_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<WKSTA_USER_INFO_0[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<WKSTA_USER_INFO_0[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_USER_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_USER_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<WKSTA_USER_INFO_0>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_USER_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<WKSTA_USER_INFO_0>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_USER_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<WKSTA_USER_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_USER_INFO_1_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<WKSTA_USER_INFO_1[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<WKSTA_USER_INFO_1[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_USER_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_USER_INFO_1 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<WKSTA_USER_INFO_1>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_USER_INFO_1 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<WKSTA_USER_INFO_1>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_USER_INFO_1 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<WKSTA_USER_INFO_1>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct _WKSTA_USER_ENUM_UNION : IRpcFixedStruct
	{
		public uint Level;
		public RpcPointer<WKSTA_USER_INFO_0_CONTAINER> Level0;
		public RpcPointer<WKSTA_USER_INFO_1_CONTAINER> Level1;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.Level);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
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
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((uint)this.Level)
			{
				case 0U:
					this.Level0 = decoder.ReadUniquePointer<WKSTA_USER_INFO_0_CONTAINER>();
					break;
				case 1U:
					this.Level1 = decoder.ReadUniquePointer<WKSTA_USER_INFO_1_CONTAINER>();
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
						this.Level0.value = decoder.ReadFixedStruct<WKSTA_USER_INFO_0_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<WKSTA_USER_INFO_0_CONTAINER>(ref this.Level0.value);
					}

					break;
				case 1U:
					if (this.Level1 is not null)
					{
						this.Level1.value = decoder.ReadFixedStruct<WKSTA_USER_INFO_1_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<WKSTA_USER_INFO_1_CONTAINER>(ref this.Level1.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_USER_ENUM_STRUCT : IRpcFixedStruct
	{
		public uint Level;
		public _WKSTA_USER_ENUM_UNION WkstaUserInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.WkstaUserInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.WkstaUserInfo = decoder.ReadUnion<_WKSTA_USER_ENUM_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.WkstaUserInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<_WKSTA_USER_ENUM_UNION>(ref this.WkstaUserInfo);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_TRANSPORT_INFO_0_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<WKSTA_TRANSPORT_INFO_0[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<WKSTA_TRANSPORT_INFO_0[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<WKSTA_TRANSPORT_INFO_0>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<WKSTA_TRANSPORT_INFO_0>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					WKSTA_TRANSPORT_INFO_0 elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<WKSTA_TRANSPORT_INFO_0>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct _WKSTA_TRANSPORT_ENUM_UNION : IRpcFixedStruct
	{
		public uint Level;
		public RpcPointer<WKSTA_TRANSPORT_INFO_0_CONTAINER> Level0;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.Level);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
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
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((uint)this.Level)
			{
				case 0U:
					this.Level0 = decoder.ReadUniquePointer<WKSTA_TRANSPORT_INFO_0_CONTAINER>();
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
						this.Level0.value = decoder.ReadFixedStruct<WKSTA_TRANSPORT_INFO_0_CONTAINER>(NdrAlignment.NativePtr);
						decoder.ReadStructDeferral<WKSTA_TRANSPORT_INFO_0_CONTAINER>(ref this.Level0.value);
					}

					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct WKSTA_TRANSPORT_ENUM_STRUCT : IRpcFixedStruct
	{
		public uint Level;
		public _WKSTA_TRANSPORT_ENUM_UNION WkstaTransportInfo;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Level);
			encoder.WriteUnion(this.WkstaTransportInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Level = decoder.ReadUInt32();
			this.WkstaTransportInfo = decoder.ReadUnion<_WKSTA_TRANSPORT_ENUM_UNION>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.WkstaTransportInfo);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<_WKSTA_TRANSPORT_ENUM_UNION>(ref this.WkstaTransportInfo);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct JOINPR_USER_PASSWORD : IRpcFixedStruct
	{
		public byte[] Obfuscator;
		public char[] Buffer;
		public uint Length;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			if (this.Obfuscator == null)
				this.Obfuscator = new byte[8];
			for (int i = 0; i < 8; i++)
			{
				byte elem_0 = this.Obfuscator[i];
				encoder.WriteValue(elem_0);
			}

			if (this.Buffer == null)
				this.Buffer = new char[256];
			for (int i = 0; i < 256; i++)
			{
				char elem_0 = this.Buffer[i];
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(this.Length);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			if (this.Obfuscator == null)
				this.Obfuscator = new byte[8];
			for (int i = 0; i < 8; i++)
			{
				byte elem_0 = this.Obfuscator[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.Obfuscator[i] = elem_0;
			}

			if (this.Buffer == null)
				this.Buffer = new char[256];
			for (int i = 0; i < 256; i++)
			{
				char elem_0 = this.Buffer[i];
				elem_0 = decoder.ReadWideChar();
				this.Buffer[i] = elem_0;
			}

			this.Length = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct JOINPR_ENCRYPTED_USER_PASSWORD : IRpcFixedStruct
	{
		public byte[] Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			if (this.Buffer == null)
				this.Buffer = new byte[524];
			for (int i = 0; i < 524; i++)
			{
				byte elem_0 = this.Buffer[i];
				encoder.WriteValue(elem_0);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			if (this.Buffer == null)
				this.Buffer = new byte[524];
			for (int i = 0; i < 524; i++)
			{
				byte elem_0 = this.Buffer[i];
				elem_0 = decoder.ReadUnsignedChar();
				this.Buffer[i] = elem_0;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct UNICODE_STRING : IRpcFixedStruct
	{
		public ushort Length;
		public ushort MaximumLength;
		public RpcPointer<ArraySegment<ushort>> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.Length);
			encoder.WriteValue(this.MaximumLength);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.Length = decoder.ReadUInt16();
			this.MaximumLength = decoder.ReadUInt16();
			this.Buffer = decoder.ReadUniquePointer<ArraySegment<ushort>>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value, true);
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					ushort elem_0 = this.Buffer.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArraySegmentHeader<ushort>();
				for (int i = 0; i < this.Buffer.value.Count; i++)
				{
					ushort elem_0 = this.Buffer.value.Item(i);
					elem_0 = decoder.ReadUInt16();
					this.Buffer.value.Item(i) = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial struct NET_COMPUTER_NAME_ARRAY : IRpcFixedStruct
	{
		public uint EntryCount;
		public RpcPointer<UNICODE_STRING[]> ComputerNames;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntryCount);
			encoder.WriteUniquePointer(this.ComputerNames);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntryCount = decoder.ReadUInt32();
			this.ComputerNames = decoder.ReadUniquePointer<UNICODE_STRING[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ComputerNames is not null)
			{
				encoder.WriteArrayHeader(this.ComputerNames.value);
				for (int i = 0; i < this.ComputerNames.value.Length; i++)
				{
					UNICODE_STRING elem_0 = this.ComputerNames.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.ComputerNames.value.Length; i++)
				{
					UNICODE_STRING elem_0 = this.ComputerNames.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ComputerNames is not null)
			{
				this.ComputerNames.value = decoder.ReadArrayHeader<UNICODE_STRING>();
				for (int i = 0; i < this.ComputerNames.value.Length; i++)
				{
					UNICODE_STRING elem_0 = this.ComputerNames.value[i];
					elem_0 = decoder.ReadFixedStruct<UNICODE_STRING>(NdrAlignment.NativePtr);
					this.ComputerNames.value[i] = elem_0;
				}

				for (int i = 0; i < this.ComputerNames.value.Length; i++)
				{
					UNICODE_STRING elem_0 = this.ComputerNames.value[i];
					decoder.ReadStructDeferral<UNICODE_STRING>(ref elem_0);
					this.ComputerNames.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11"), GuidAttribute("6bffd098-a112-3610-9833-46c3f87e345a"), RpcVersionAttribute(1, 0)]
	public partial interface wkssvc
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrWkstaGetInfo(string ServerName, uint Level, RpcPointer<WKSTA_INFO> WkstaInfo, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrWkstaSetInfo(string ServerName, uint Level, WKSTA_INFO WkstaInfo, RpcPointer<uint> ErrorParameter, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrWkstaUserEnum(string ServerName, RpcPointer<WKSTA_USER_ENUM_STRUCT> UserInfo, uint PreferredMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task Opnum3NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task Opnum4NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrWkstaTransportEnum(string ServerName, RpcPointer<WKSTA_TRANSPORT_ENUM_STRUCT> TransportInfo, uint PreferredMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrWkstaTransportAdd(string ServerName, uint Level, WKSTA_TRANSPORT_INFO_0 TransportInfo, RpcPointer<uint> ErrorParameter, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrWkstaTransportDel(string ServerName, string TransportName, uint ForceLevel, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrUseAdd(string ServerName, uint Level, USE_INFO InfoStruct, RpcPointer<uint> ErrorParameter, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrUseGetInfo(string ServerName, string UseName, uint Level, RpcPointer<USE_INFO> InfoStruct, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrUseDel(string ServerName, string UseName, uint ForceLevel, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrUseEnum(string ServerName, RpcPointer<USE_ENUM_STRUCT> InfoStruct, uint PreferredMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task Opnum12NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrWorkstationStatisticsGet(string ServerName, string ServiceName, uint Level, uint Options, RpcPointer<RpcPointer<STAT_WORKSTATION_0>> Buffer, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task Opnum14NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task Opnum15NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task Opnum16NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task Opnum17NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task Opnum18NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task Opnum19NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrGetJoinInformation(string ServerName, RpcPointer<RpcPointer<string>> NameBuffer, RpcPointer<NETSETUP_JOIN_STATUS> BufferType, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task Opnum21NotUsedOnWire(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrJoinDomain2(string ServerName, string DomainNameParam, string MachineAccountOU, string AccountName, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password, uint Options, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrUnjoinDomain2(string ServerName, string AccountName, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password, uint Options, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrRenameMachineInDomain2(string ServerName, string MachineName, string AccountName, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password, uint Options, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrValidateName2(string ServerName, string NameToValidate, string AccountName, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password, NETSETUP_NAME_TYPE NameType, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrGetJoinableOUs2(string ServerName, string DomainNameParam, string AccountName, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password, RpcPointer<uint> OUCount, RpcPointer<RpcPointer<RpcPointer<string>[]>> OUs, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrAddAlternateComputerName(string ServerName, string AlternateName, string DomainAccount, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> EncryptedPassword, uint Reserved, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrRemoveAlternateComputerName(string ServerName, string AlternateName, string DomainAccount, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> EncryptedPassword, uint Reserved, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrSetPrimaryComputerName(string ServerName, string PrimaryName, string DomainAccount, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> EncryptedPassword, uint Reserved, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		Task<uint> NetrEnumerateComputerNames(string ServerName, NET_COMPUTER_NAME_TYPE NameType, uint Reserved, RpcPointer<RpcPointer<NET_COMPUTER_NAME_ARRAY>> ComputerNames, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11"), IidAttribute("6bffd098-a112-3610-9833-46c3f87e345a")]
	public partial class wkssvcClientProxy : Titanis.DceRpc.Client.RpcClientProxy, wkssvc, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrWkstaGetInfo(string ServerName, uint Level, RpcPointer<WKSTA_INFO> WkstaInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			WkstaInfo.value = decoder.ReadUnion<WKSTA_INFO>();
			decoder.ReadStructDeferral<WKSTA_INFO>(ref WkstaInfo.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrWkstaSetInfo(string ServerName, uint Level, WKSTA_INFO WkstaInfo, RpcPointer<uint> ErrorParameter, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(WkstaInfo);
			encoder.WriteStructDeferral(WkstaInfo);
			encoder.WriteUniquePointer(ErrorParameter);
			if (ErrorParameter is not null)
			{
				encoder.WriteValue(ErrorParameter.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ErrorParameter = decoder.ReadOutUniquePointer<uint>(ErrorParameter);
			if (ErrorParameter is not null)
			{
				ErrorParameter.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrWkstaUserEnum(string ServerName, RpcPointer<WKSTA_USER_ENUM_STRUCT> UserInfo, uint PreferredMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteFixedStruct(UserInfo.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(UserInfo.value);
			encoder.WriteValue(PreferredMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			UserInfo.value = decoder.ReadFixedStruct<WKSTA_USER_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<WKSTA_USER_ENUM_STRUCT>(ref UserInfo.value);
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Opnum3NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Opnum4NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrWkstaTransportEnum(string ServerName, RpcPointer<WKSTA_TRANSPORT_ENUM_STRUCT> TransportInfo, uint PreferredMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteFixedStruct(TransportInfo.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TransportInfo.value);
			encoder.WriteValue(PreferredMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			TransportInfo.value = decoder.ReadFixedStruct<WKSTA_TRANSPORT_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<WKSTA_TRANSPORT_ENUM_STRUCT>(ref TransportInfo.value);
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrWkstaTransportAdd(string ServerName, uint Level, WKSTA_TRANSPORT_INFO_0 TransportInfo, RpcPointer<uint> ErrorParameter, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteFixedStruct(TransportInfo, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TransportInfo);
			encoder.WriteUniquePointer(ErrorParameter);
			if (ErrorParameter is not null)
			{
				encoder.WriteValue(ErrorParameter.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ErrorParameter = decoder.ReadOutUniquePointer<uint>(ErrorParameter);
			if (ErrorParameter is not null)
			{
				ErrorParameter.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrWkstaTransportDel(string ServerName, string TransportName, uint ForceLevel, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(TransportName is null);
			if (TransportName is not null)
				encoder.WriteWideCharString(TransportName);
			encoder.WriteValue(ForceLevel);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrUseAdd(string ServerName, uint Level, USE_INFO InfoStruct, RpcPointer<uint> ErrorParameter, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(Level);
			encoder.WriteUnion(InfoStruct);
			encoder.WriteStructDeferral(InfoStruct);
			encoder.WriteUniquePointer(ErrorParameter);
			if (ErrorParameter is not null)
			{
				encoder.WriteValue(ErrorParameter.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ErrorParameter = decoder.ReadOutUniquePointer<uint>(ErrorParameter);
			if (ErrorParameter is not null)
			{
				ErrorParameter.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrUseGetInfo(string ServerName, string UseName, uint Level, RpcPointer<USE_INFO> InfoStruct, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(UseName);
			encoder.WriteValue(Level);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadUnion<USE_INFO>();
			decoder.ReadStructDeferral<USE_INFO>(ref InfoStruct.value);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrUseDel(string ServerName, string UseName, uint ForceLevel, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(UseName);
			encoder.WriteValue(ForceLevel);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrUseEnum(string ServerName, RpcPointer<USE_ENUM_STRUCT> InfoStruct, uint PreferredMaximumLength, RpcPointer<uint> TotalEntries, RpcPointer<uint> ResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteFixedStruct(InfoStruct.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(PreferredMaximumLength);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			InfoStruct.value = decoder.ReadFixedStruct<USE_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<USE_ENUM_STRUCT>(ref InfoStruct.value);
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Opnum12NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrWorkstationStatisticsGet(string ServerName, string ServiceName, uint Level, uint Options, RpcPointer<RpcPointer<STAT_WORKSTATION_0>> Buffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(ServiceName is null);
			if (ServiceName is not null)
				encoder.WriteWideCharString(ServiceName);
			encoder.WriteValue(Level);
			encoder.WriteValue(Options);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			Buffer.value = decoder.ReadOutUniquePointer<STAT_WORKSTATION_0>(Buffer.value);
			if (Buffer.value is not null)
			{
				Buffer.value.value = decoder.ReadFixedStruct<STAT_WORKSTATION_0>(NdrAlignment._8Byte);
				decoder.ReadStructDeferral<STAT_WORKSTATION_0>(ref Buffer.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Opnum14NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Opnum15NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Opnum16NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Opnum17NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Opnum18NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Opnum19NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrGetJoinInformation(string ServerName, RpcPointer<RpcPointer<string>> NameBuffer, RpcPointer<NETSETUP_JOIN_STATUS> BufferType, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniquePointer(NameBuffer.value);
			if (NameBuffer.value is not null)
			{
				encoder.WriteWideCharString(NameBuffer.value.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			NameBuffer.value = decoder.ReadOutUniquePointer<string>(NameBuffer.value);
			if (NameBuffer.value is not null)
			{
				NameBuffer.value.value = decoder.ReadWideCharString();
			}

			BufferType.value = (NETSETUP_JOIN_STATUS)decoder.ReadEnumShortValue();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Opnum21NotUsedOnWire(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrJoinDomain2(string ServerName, string DomainNameParam, string MachineAccountOU, string AccountName, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password, uint Options, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(DomainNameParam);
			encoder.WriteUniqueReferentId(MachineAccountOU is null);
			if (MachineAccountOU is not null)
				encoder.WriteWideCharString(MachineAccountOU);
			encoder.WriteUniqueReferentId(AccountName is null);
			if (AccountName is not null)
				encoder.WriteWideCharString(AccountName);
			encoder.WriteUniquePointer(Password);
			if (Password is not null)
			{
				encoder.WriteFixedStruct(Password.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(Password.value);
			}

			encoder.WriteValue(Options);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrUnjoinDomain2(string ServerName, string AccountName, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password, uint Options, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(AccountName is null);
			if (AccountName is not null)
				encoder.WriteWideCharString(AccountName);
			encoder.WriteUniquePointer(Password);
			if (Password is not null)
			{
				encoder.WriteFixedStruct(Password.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(Password.value);
			}

			encoder.WriteValue(Options);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrRenameMachineInDomain2(string ServerName, string MachineName, string AccountName, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password, uint Options, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(MachineName is null);
			if (MachineName is not null)
				encoder.WriteWideCharString(MachineName);
			encoder.WriteUniqueReferentId(AccountName is null);
			if (AccountName is not null)
				encoder.WriteWideCharString(AccountName);
			encoder.WriteUniquePointer(Password);
			if (Password is not null)
			{
				encoder.WriteFixedStruct(Password.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(Password.value);
			}

			encoder.WriteValue(Options);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrValidateName2(string ServerName, string NameToValidate, string AccountName, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password, NETSETUP_NAME_TYPE NameType, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(NameToValidate);
			encoder.WriteUniqueReferentId(AccountName is null);
			if (AccountName is not null)
				encoder.WriteWideCharString(AccountName);
			encoder.WriteUniquePointer(Password);
			if (Password is not null)
			{
				encoder.WriteFixedStruct(Password.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(Password.value);
			}

			encoder.WriteEnumShortValue((short)NameType);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrGetJoinableOUs2(string ServerName, string DomainNameParam, string AccountName, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password, RpcPointer<uint> OUCount, RpcPointer<RpcPointer<RpcPointer<string>[]>> OUs, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteWideCharString(DomainNameParam);
			encoder.WriteUniqueReferentId(AccountName is null);
			if (AccountName is not null)
				encoder.WriteWideCharString(AccountName);
			encoder.WriteUniquePointer(Password);
			if (Password is not null)
			{
				encoder.WriteFixedStruct(Password.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(Password.value);
			}

			encoder.WriteValue(OUCount.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			OUCount.value = decoder.ReadUInt32();
			OUs.value = decoder.ReadOutUniquePointer<RpcPointer<string>[]>(OUs.value);
			if (OUs.value is not null)
			{
				OUs.value.value = decoder.ReadArrayHeader<RpcPointer<string>>();
				for (int i = 0; i < OUs.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = OUs.value.value[i];
					elem_0 = decoder.ReadUniquePointer<string>();
					OUs.value.value[i] = elem_0;
				}

				for (int i = 0; i < OUs.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = OUs.value.value[i];
					if (elem_0 is not null)
					{
						elem_0.value = decoder.ReadWideCharString();
					}

					OUs.value.value[i] = elem_0;
				}
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrAddAlternateComputerName(string ServerName, string AlternateName, string DomainAccount, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> EncryptedPassword, uint Reserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(AlternateName is null);
			if (AlternateName is not null)
				encoder.WriteWideCharString(AlternateName);
			encoder.WriteUniqueReferentId(DomainAccount is null);
			if (DomainAccount is not null)
				encoder.WriteWideCharString(DomainAccount);
			encoder.WriteUniquePointer(EncryptedPassword);
			if (EncryptedPassword is not null)
			{
				encoder.WriteFixedStruct(EncryptedPassword.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(EncryptedPassword.value);
			}

			encoder.WriteValue(Reserved);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrRemoveAlternateComputerName(string ServerName, string AlternateName, string DomainAccount, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> EncryptedPassword, uint Reserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(AlternateName is null);
			if (AlternateName is not null)
				encoder.WriteWideCharString(AlternateName);
			encoder.WriteUniqueReferentId(DomainAccount is null);
			if (DomainAccount is not null)
				encoder.WriteWideCharString(DomainAccount);
			encoder.WriteUniquePointer(EncryptedPassword);
			if (EncryptedPassword is not null)
			{
				encoder.WriteFixedStruct(EncryptedPassword.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(EncryptedPassword.value);
			}

			encoder.WriteValue(Reserved);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrSetPrimaryComputerName(string ServerName, string PrimaryName, string DomainAccount, RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> EncryptedPassword, uint Reserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteUniqueReferentId(PrimaryName is null);
			if (PrimaryName is not null)
				encoder.WriteWideCharString(PrimaryName);
			encoder.WriteUniqueReferentId(DomainAccount is null);
			if (DomainAccount is not null)
				encoder.WriteWideCharString(DomainAccount);
			encoder.WriteUniquePointer(EncryptedPassword);
			if (EncryptedPassword is not null)
			{
				encoder.WriteFixedStruct(EncryptedPassword.value, NdrAlignment._1Byte);
				encoder.WriteStructDeferral(EncryptedPassword.value);
			}

			encoder.WriteValue(Reserved);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task<uint> NetrEnumerateComputerNames(string ServerName, NET_COMPUTER_NAME_TYPE NameType, uint Reserved, RpcPointer<RpcPointer<NET_COMPUTER_NAME_ARRAY>> ComputerNames, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteEnumShortValue((short)NameType);
			encoder.WriteValue(Reserved);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ComputerNames.value = decoder.ReadOutUniquePointer<NET_COMPUTER_NAME_ARRAY>(ComputerNames.value);
			if (ComputerNames.value is not null)
			{
				ComputerNames.value.value = decoder.ReadFixedStruct<NET_COMPUTER_NAME_ARRAY>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<NET_COMPUTER_NAME_ARRAY>(ref ComputerNames.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(wkssvc);
		private static Guid _interfaceUuid = new Guid("6bffd098-a112-3610-9833-46c3f87e345a");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
	public partial class wkssvcStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrWkstaGetInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			RpcPointer<WKSTA_INFO> WkstaInfo = new RpcPointer<WKSTA_INFO>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrWkstaGetInfo(ServerName, Level, WkstaInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnion(WkstaInfo.value);
			encoder.WriteStructDeferral(WkstaInfo.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrWkstaSetInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			WKSTA_INFO WkstaInfo;
			RpcPointer<uint> ErrorParameter;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			WkstaInfo = decoder.ReadUnion<WKSTA_INFO>();
			decoder.ReadStructDeferral<WKSTA_INFO>(ref WkstaInfo);
			ErrorParameter = decoder.ReadUniquePointer<uint>();
			if (ErrorParameter is not null)
			{
				ErrorParameter.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrWkstaSetInfo(ServerName, Level, WkstaInfo, ErrorParameter, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ErrorParameter);
			if (ErrorParameter is not null)
			{
				encoder.WriteValue(ErrorParameter.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrWkstaUserEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<WKSTA_USER_ENUM_STRUCT> UserInfo;
			uint PreferredMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			UserInfo = new RpcPointer<WKSTA_USER_ENUM_STRUCT>();
			UserInfo.value = decoder.ReadFixedStruct<WKSTA_USER_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<WKSTA_USER_ENUM_STRUCT>(ref UserInfo.value);
			PreferredMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrWkstaUserEnum(ServerName, UserInfo, PreferredMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(UserInfo.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(UserInfo.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_Opnum3NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum3NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_Opnum4NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum4NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrWkstaTransportEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<WKSTA_TRANSPORT_ENUM_STRUCT> TransportInfo;
			uint PreferredMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			TransportInfo = new RpcPointer<WKSTA_TRANSPORT_ENUM_STRUCT>();
			TransportInfo.value = decoder.ReadFixedStruct<WKSTA_TRANSPORT_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<WKSTA_TRANSPORT_ENUM_STRUCT>(ref TransportInfo.value);
			PreferredMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrWkstaTransportEnum(ServerName, TransportInfo, PreferredMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(TransportInfo.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(TransportInfo.value);
			encoder.WriteValue(TotalEntries.value);
			encoder.WriteUniquePointer(ResumeHandle);
			if (ResumeHandle is not null)
			{
				encoder.WriteValue(ResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrWkstaTransportAdd(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			WKSTA_TRANSPORT_INFO_0 TransportInfo;
			RpcPointer<uint> ErrorParameter;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			TransportInfo = decoder.ReadFixedStruct<WKSTA_TRANSPORT_INFO_0>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<WKSTA_TRANSPORT_INFO_0>(ref TransportInfo);
			ErrorParameter = decoder.ReadUniquePointer<uint>();
			if (ErrorParameter is not null)
			{
				ErrorParameter.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrWkstaTransportAdd(ServerName, Level, TransportInfo, ErrorParameter, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ErrorParameter);
			if (ErrorParameter is not null)
			{
				encoder.WriteValue(ErrorParameter.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrWkstaTransportDel(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string TransportName;
			uint ForceLevel;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				TransportName = null;
			else
				TransportName = decoder.ReadWideCharString();
			ForceLevel = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrWkstaTransportDel(ServerName, TransportName, ForceLevel, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrUseAdd(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint Level;
			USE_INFO InfoStruct;
			RpcPointer<uint> ErrorParameter;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			InfoStruct = decoder.ReadUnion<USE_INFO>();
			decoder.ReadStructDeferral<USE_INFO>(ref InfoStruct);
			ErrorParameter = decoder.ReadUniquePointer<uint>();
			if (ErrorParameter is not null)
			{
				ErrorParameter.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrUseAdd(ServerName, Level, InfoStruct, ErrorParameter, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ErrorParameter);
			if (ErrorParameter is not null)
			{
				encoder.WriteValue(ErrorParameter.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrUseGetInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string UseName;
			uint Level;
			RpcPointer<USE_INFO> InfoStruct = new RpcPointer<USE_INFO>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			UseName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrUseGetInfo(ServerName, UseName, Level, InfoStruct, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUnion(InfoStruct.value);
			encoder.WriteStructDeferral(InfoStruct.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrUseDel(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string UseName;
			uint ForceLevel;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			UseName = decoder.ReadWideCharString();
			ForceLevel = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrUseDel(ServerName, UseName, ForceLevel, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrUseEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<USE_ENUM_STRUCT> InfoStruct;
			uint PreferredMaximumLength;
			RpcPointer<uint> TotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> ResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			InfoStruct = new RpcPointer<USE_ENUM_STRUCT>();
			InfoStruct.value = decoder.ReadFixedStruct<USE_ENUM_STRUCT>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<USE_ENUM_STRUCT>(ref InfoStruct.value);
			PreferredMaximumLength = decoder.ReadUInt32();
			ResumeHandle = decoder.ReadUniquePointer<uint>();
			if (ResumeHandle is not null)
			{
				ResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrUseEnum(ServerName, InfoStruct, PreferredMaximumLength, TotalEntries, ResumeHandle, cancellationToken);
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

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_Opnum12NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum12NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrWorkstationStatisticsGet(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string ServiceName;
			uint Level;
			uint Options;
			RpcPointer<RpcPointer<STAT_WORKSTATION_0>> Buffer = new RpcPointer<RpcPointer<STAT_WORKSTATION_0>>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				ServiceName = null;
			else
				ServiceName = decoder.ReadWideCharString();
			Level = decoder.ReadUInt32();
			Options = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrWorkstationStatisticsGet(ServerName, ServiceName, Level, Options, Buffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(Buffer.value);
			if (Buffer.value is not null)
			{
				encoder.WriteFixedStruct(Buffer.value.value, NdrAlignment._8Byte);
				encoder.WriteStructDeferral(Buffer.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_Opnum14NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum14NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_Opnum15NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum15NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_Opnum16NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum16NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_Opnum17NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum17NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_Opnum18NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum18NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_Opnum19NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum19NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrGetJoinInformation(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<RpcPointer<string>> NameBuffer;
			RpcPointer<NETSETUP_JOIN_STATUS> BufferType = new RpcPointer<NETSETUP_JOIN_STATUS>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			NameBuffer = new RpcPointer<RpcPointer<string>>();
			NameBuffer.value = decoder.ReadUniquePointer<string>();
			if (NameBuffer.value is not null)
			{
				NameBuffer.value.value = decoder.ReadWideCharString();
			}

			var invokeTask = this._obj.NetrGetJoinInformation(ServerName, NameBuffer, BufferType, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(NameBuffer.value);
			if (NameBuffer.value is not null)
			{
				encoder.WriteWideCharString(NameBuffer.value.value);
			}

			encoder.WriteEnumShortValue((short)BufferType.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_Opnum21NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum21NotUsedOnWire(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrJoinDomain2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string DomainNameParam;
			string MachineAccountOU;
			string AccountName;
			RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password;
			uint Options;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			DomainNameParam = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				MachineAccountOU = null;
			else
				MachineAccountOU = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				AccountName = null;
			else
				AccountName = decoder.ReadWideCharString();
			Password = decoder.ReadUniquePointer<JOINPR_ENCRYPTED_USER_PASSWORD>();
			if (Password is not null)
			{
				Password.value = decoder.ReadFixedStruct<JOINPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<JOINPR_ENCRYPTED_USER_PASSWORD>(ref Password.value);
			}

			Options = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrJoinDomain2(ServerName, DomainNameParam, MachineAccountOU, AccountName, Password, Options, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrUnjoinDomain2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string AccountName;
			RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password;
			uint Options;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				AccountName = null;
			else
				AccountName = decoder.ReadWideCharString();
			Password = decoder.ReadUniquePointer<JOINPR_ENCRYPTED_USER_PASSWORD>();
			if (Password is not null)
			{
				Password.value = decoder.ReadFixedStruct<JOINPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<JOINPR_ENCRYPTED_USER_PASSWORD>(ref Password.value);
			}

			Options = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrUnjoinDomain2(ServerName, AccountName, Password, Options, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrRenameMachineInDomain2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string MachineName;
			string AccountName;
			RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password;
			uint Options;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				MachineName = null;
			else
				MachineName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				AccountName = null;
			else
				AccountName = decoder.ReadWideCharString();
			Password = decoder.ReadUniquePointer<JOINPR_ENCRYPTED_USER_PASSWORD>();
			if (Password is not null)
			{
				Password.value = decoder.ReadFixedStruct<JOINPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<JOINPR_ENCRYPTED_USER_PASSWORD>(ref Password.value);
			}

			Options = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrRenameMachineInDomain2(ServerName, MachineName, AccountName, Password, Options, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrValidateName2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string NameToValidate;
			string AccountName;
			RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password;
			NETSETUP_NAME_TYPE NameType;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			NameToValidate = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				AccountName = null;
			else
				AccountName = decoder.ReadWideCharString();
			Password = decoder.ReadUniquePointer<JOINPR_ENCRYPTED_USER_PASSWORD>();
			if (Password is not null)
			{
				Password.value = decoder.ReadFixedStruct<JOINPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<JOINPR_ENCRYPTED_USER_PASSWORD>(ref Password.value);
			}

			NameType = (NETSETUP_NAME_TYPE)decoder.ReadEnumShortValue();
			var invokeTask = this._obj.NetrValidateName2(ServerName, NameToValidate, AccountName, Password, NameType, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrGetJoinableOUs2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string DomainNameParam;
			string AccountName;
			RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> Password;
			RpcPointer<uint> OUCount;
			RpcPointer<RpcPointer<RpcPointer<string>[]>> OUs = new RpcPointer<RpcPointer<RpcPointer<string>[]>>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			DomainNameParam = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				AccountName = null;
			else
				AccountName = decoder.ReadWideCharString();
			Password = decoder.ReadUniquePointer<JOINPR_ENCRYPTED_USER_PASSWORD>();
			if (Password is not null)
			{
				Password.value = decoder.ReadFixedStruct<JOINPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<JOINPR_ENCRYPTED_USER_PASSWORD>(ref Password.value);
			}

			OUCount = new RpcPointer<uint>();
			OUCount.value = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrGetJoinableOUs2(ServerName, DomainNameParam, AccountName, Password, OUCount, OUs, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(OUCount.value);
			encoder.WriteUniquePointer(OUs.value);
			if (OUs.value is not null)
			{
				encoder.WriteArrayHeader(OUs.value.value);
				for (int i = 0; i < OUs.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = OUs.value.value[i];
					encoder.WriteUniquePointer(elem_0);
				}

				for (int i = 0; i < OUs.value.value.Length; i++)
				{
					RpcPointer<string> elem_0 = OUs.value.value[i];
					if (elem_0 is not null)
					{
						encoder.WriteWideCharString(elem_0.value);
					}
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrAddAlternateComputerName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string AlternateName;
			string DomainAccount;
			RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> EncryptedPassword;
			uint Reserved;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				AlternateName = null;
			else
				AlternateName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				DomainAccount = null;
			else
				DomainAccount = decoder.ReadWideCharString();
			EncryptedPassword = decoder.ReadUniquePointer<JOINPR_ENCRYPTED_USER_PASSWORD>();
			if (EncryptedPassword is not null)
			{
				EncryptedPassword.value = decoder.ReadFixedStruct<JOINPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<JOINPR_ENCRYPTED_USER_PASSWORD>(ref EncryptedPassword.value);
			}

			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrAddAlternateComputerName(ServerName, AlternateName, DomainAccount, EncryptedPassword, Reserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrRemoveAlternateComputerName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string AlternateName;
			string DomainAccount;
			RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> EncryptedPassword;
			uint Reserved;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				AlternateName = null;
			else
				AlternateName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				DomainAccount = null;
			else
				DomainAccount = decoder.ReadWideCharString();
			EncryptedPassword = decoder.ReadUniquePointer<JOINPR_ENCRYPTED_USER_PASSWORD>();
			if (EncryptedPassword is not null)
			{
				EncryptedPassword.value = decoder.ReadFixedStruct<JOINPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<JOINPR_ENCRYPTED_USER_PASSWORD>(ref EncryptedPassword.value);
			}

			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrRemoveAlternateComputerName(ServerName, AlternateName, DomainAccount, EncryptedPassword, Reserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrSetPrimaryComputerName(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			string PrimaryName;
			string DomainAccount;
			RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD> EncryptedPassword;
			uint Reserved;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				PrimaryName = null;
			else
				PrimaryName = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				DomainAccount = null;
			else
				DomainAccount = decoder.ReadWideCharString();
			EncryptedPassword = decoder.ReadUniquePointer<JOINPR_ENCRYPTED_USER_PASSWORD>();
			if (EncryptedPassword is not null)
			{
				EncryptedPassword.value = decoder.ReadFixedStruct<JOINPR_ENCRYPTED_USER_PASSWORD>(NdrAlignment._1Byte);
				decoder.ReadStructDeferral<JOINPR_ENCRYPTED_USER_PASSWORD>(ref EncryptedPassword.value);
			}

			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrSetPrimaryComputerName(ServerName, PrimaryName, DomainAccount, EncryptedPassword, Reserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public async Task Invoke_NetrEnumerateComputerNames(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			NET_COMPUTER_NAME_TYPE NameType;
			uint Reserved;
			RpcPointer<RpcPointer<NET_COMPUTER_NAME_ARRAY>> ComputerNames = new RpcPointer<RpcPointer<NET_COMPUTER_NAME_ARRAY>>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			NameType = (NET_COMPUTER_NAME_TYPE)decoder.ReadEnumShortValue();
			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrEnumerateComputerNames(ServerName, NameType, Reserved, ComputerNames, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ComputerNames.value);
			if (ComputerNames.value is not null)
			{
				encoder.WriteFixedStruct(ComputerNames.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ComputerNames.value.value);
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("6bffd098-a112-3610-9833-46c3f87e345a");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private wkssvc _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.11")]
		public wkssvcStub(wkssvc obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_NetrWkstaGetInfo, this.Invoke_NetrWkstaSetInfo, this.Invoke_NetrWkstaUserEnum, this.Invoke_Opnum3NotUsedOnWire, this.Invoke_Opnum4NotUsedOnWire, this.Invoke_NetrWkstaTransportEnum, this.Invoke_NetrWkstaTransportAdd, this.Invoke_NetrWkstaTransportDel, this.Invoke_NetrUseAdd, this.Invoke_NetrUseGetInfo, this.Invoke_NetrUseDel, this.Invoke_NetrUseEnum, this.Invoke_Opnum12NotUsedOnWire, this.Invoke_NetrWorkstationStatisticsGet, this.Invoke_Opnum14NotUsedOnWire, this.Invoke_Opnum15NotUsedOnWire, this.Invoke_Opnum16NotUsedOnWire, this.Invoke_Opnum17NotUsedOnWire, this.Invoke_Opnum18NotUsedOnWire, this.Invoke_Opnum19NotUsedOnWire, this.Invoke_NetrGetJoinInformation, this.Invoke_Opnum21NotUsedOnWire, this.Invoke_NetrJoinDomain2, this.Invoke_NetrUnjoinDomain2, this.Invoke_NetrRenameMachineInDomain2, this.Invoke_NetrValidateName2, this.Invoke_NetrGetJoinableOUs2, this.Invoke_NetrAddAlternateComputerName, this.Invoke_NetrRemoveAlternateComputerName, this.Invoke_NetrSetPrimaryComputerName, this.Invoke_NetrEnumerateComputerNames};
		}
	}
}