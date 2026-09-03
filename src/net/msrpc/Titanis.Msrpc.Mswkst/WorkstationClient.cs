using ms_wkst;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Titanis.Crypto;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Winterop;

namespace Titanis.Msrpc.Mswkst
{
	public class WorkstationClient : RpcServiceClient<ms_wkst.wkssvcClientProxy>
	{
		// [MS-WKST] § 1.9 Standards Assignments
		public override string? WellKnownPipeName => "wkssvc";

		public async Task<WorkstationInfo> GetInfo(
			string serverName,
			WorkstationInfoLevel[] levels,
			CancellationToken cancellationToken)
		{
			WorkstationInfo info = new WorkstationInfo();
			bool success = false;
			Win32ErrorCode lastError = Win32ErrorCode.ERROR_SUCCESS;
			foreach (var level in levels)
			{
				DceRpc.RpcPointer<ms_wkst.WKSTA_INFO> wkstaInfo = new();
				var res = (Win32ErrorCode)await _proxy.NetrWkstaGetInfo(
					serverName,
					(uint)level,
					wkstaInfo,
					cancellationToken
					).ConfigureAwait(false);

				if (res == Win32ErrorCode.ERROR_SUCCESS)
				{
					success = true;
					info = (WorkstationInfoLevel)wkstaInfo.value.unionSwitch switch
					{
						WorkstationInfoLevel.Level100 => FromWkstaInfo(info, wkstaInfo.value.WkstaInfo100.value),
						WorkstationInfoLevel.Level101 => FromWkstaInfo(info, wkstaInfo.value.WkstaInfo101.value),
						WorkstationInfoLevel.Level102 => FromWkstaInfo(info, wkstaInfo.value.WkstaInfo102.value),
						WorkstationInfoLevel.Level502 => FromWkstaInfo(info, wkstaInfo.value.WkstaInfo502.value),
					};
				}
				else
				{
					lastError = res;
				}
			}

			if (success)
				return info;
			else
				lastError.CheckAndThrow();

			// Shouldn't get here
			throw new NotImplementedException();
		}

		private WorkstationInfo FromWkstaInfo(WorkstationInfo info, in WKSTA_INFO_100 rpc)
		{
			info.Platform = (Platform)rpc.wki100_platform_id;
			info.ComputerName = rpc.wki100_computername.value;
			info.Domain = rpc.wki100_langroup.value;
			info.OsVersion = new Version((int)rpc.wki100_ver_major, (int)rpc.wki100_ver_minor);
			return info;
		}

		private WorkstationInfo FromWkstaInfo(WorkstationInfo info, in WKSTA_INFO_101 rpc)
		{
			info.Platform = (Platform)rpc.wki101_platform_id;
			info.ComputerName = rpc.wki101_computername.value;
			info.Domain = rpc.wki101_langroup.value;
			info.OsVersion = new Version((int)rpc.wki101_ver_major, (int)rpc.wki101_ver_minor);
			return info;
		}
		private WorkstationInfo FromWkstaInfo(WorkstationInfo info, in WKSTA_INFO_102 rpc)
		{
			info.Platform = (Platform)rpc.wki102_platform_id;
			info.ComputerName = rpc.wki102_computername.value;
			info.Domain = rpc.wki102_langroup.value;
			info.OsVersion = new Version((int)rpc.wki102_ver_major, (int)rpc.wki102_ver_minor);
			info.LoggedOnUserCount = rpc.wki102_logged_on_users;
			return info;
		}

		private WorkstationInfo FromWkstaInfo(WorkstationInfo info, in WKSTA_INFO_502 rpc)
		{
			info.OutgoingSmbConnectionIdleTimeout = (int)rpc.wki502_keep_conn;
			info.MaxCommands = (int)rpc.wki502_max_cmds;
			info.IncomingSmbConnectionIdleTimeout = (int)rpc.wki502_sess_timeout;
			info.DormantFileLimit = (int)rpc.wki502_dormant_file_limit;
			return info;
		}




		public async Task<WorkstationJoinInfo> GetJoinInfo(string serverName, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<string>> nameBuffer = new();
			RpcPointer<NETSETUP_JOIN_STATUS> bufferType = new();
			var res = (Win32ErrorCode)await _proxy.NetrGetJoinInformation(
				serverName,
				nameBuffer,
				bufferType,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return new WorkstationJoinInfo
			{
				Domain = nameBuffer.value?.value,
				Status = (JoinStatus)bufferType.value
			};
		}





		public async IAsyncEnumerable<LoggedOnUserInfo> GetLoggedOnUsers(string serverName, CancellationToken cancellationToken)
		{
			var maxSize = 16 * 1024;
			RpcPointer<uint> resumeHandle = new();
			var level = UserInfoLevel.Level1;
			while (true)
			{
				RpcPointer<uint> totalEntries = new();
				RpcPointer<WKSTA_USER_ENUM_STRUCT> userInfo = new(new WKSTA_USER_ENUM_STRUCT
				{
					Level = (uint)level,
					WkstaUserInfo = new _WKSTA_USER_ENUM_UNION
					{
						Level = (uint)level,
						Level1 = new RpcPointer<WKSTA_USER_INFO_1_CONTAINER>(),
					}
				});
				Win32ErrorCode res = (Win32ErrorCode)await _proxy.NetrWkstaUserEnum(
					serverName,
					userInfo,
					(uint)maxSize,
					totalEntries,
					resumeHandle,
					cancellationToken
					).ConfigureAwait(false);

				if (res is Win32ErrorCode.ERROR_SUCCESS or Win32ErrorCode.ERROR_MORE_DATA)
				{
					switch ((UserInfoLevel)userInfo.value.Level)
					{
						case UserInfoLevel.Level0:
							foreach (var entry in userInfo.value.WkstaUserInfo.Level0.value.Buffer.value)
							{
								yield return new LoggedOnUserInfo
								{
									UserName = entry.wkui0_username.value
								};
							}
							break;
						case UserInfoLevel.Level1:
							foreach (var entry in userInfo.value.WkstaUserInfo.Level1.value.Buffer.value)
							{
								yield return new LoggedOnUserInfo
								{
									UserName = entry.wkui1_username.value,
									LogonDomain = entry.wkui1_logon_domain.value,
									OtherDomain = entry.wkui1_oth_domains?.value,
									LogonServer = entry.wkui1_logon_server?.value
								};
							}
							break;
					}

					if (res != Win32ErrorCode.ERROR_MORE_DATA)
						break;
				}
				else
				{
					res.CheckAndThrow();
					break;
				}
			}
		}


		public async Task<string[]> GetComputerNames(string serverName, CancellationToken cancellationToken)
		{
			RpcPointer<RpcPointer<NET_COMPUTER_NAME_ARRAY>> computerNames = new();
			var res = (Win32ErrorCode)await this._proxy.NetrEnumerateComputerNames(
				serverName,
				NET_COMPUTER_NAME_TYPE.NetAllComputerNames,
				0,
				computerNames,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return Array.ConvertAll(computerNames.value.value.ComputerNames.value, r => Encoding.Unicode.GetString(MemoryMarshal.Cast<ushort, byte>(r.Buffer.value.AsSpan())));
		}

		public async Task AddAlternateNames(string serverName, string altName, CancellationToken cancellationToken)
		{
			var res = (Win32ErrorCode)await _proxy.NetrAddAlternateComputerName(
				serverName,
				altName,
				null,
				null,
				0,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		// [MS-WKST] § 2.2.1.1 JOIN_MAX_PASSWORD_LENGTH
		const int MaxJoinPassword = 256;
		public async Task Join(
			string serverName,
			string domain,
			string? machineAccountOU,
			string? accountName,
			string? password,
			CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(serverName);
			if (password != null && password.Length > MaxJoinPassword)
				throw new ArgumentException("The join password is too long.", nameof(password));

			var res = (Win32ErrorCode)await _proxy.NetrJoinDomain2(
				serverName,
				domain,
				machineAccountOU,
				accountName,
				(password != null) ? new RpcPointer<JOINPR_ENCRYPTED_USER_PASSWORD>(new JOINPR_ENCRYPTED_USER_PASSWORD
				{
					Buffer = EncryptPassword(password),
				}) : null,
				1,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		// [MS-WKST] § 2.2.1.2 JOIN_OBFUSCATOR_LENGTH
		const int ObfuscatorLength = 8;
		private byte[] EncryptPassword(string password)
		{
			// [MS-WKST] § 2.2.5.18.1 Password Encoding
			byte[] encodedPassword = new byte[(password.Length + 2) * 2];
			Encoding.Unicode.GetBytes(password, encodedPassword.Slice(2));
			byte seed = 0xAB;
			encodedPassword[0] = seed;
			{
				byte acc;
				encodedPassword[2] = acc = (byte)(encodedPassword[2] ^ (seed | 0x43));
				for (int i = 3; i < encodedPassword.Length - 2; i++)
				{
					encodedPassword[i] = acc = (byte)(encodedPassword[i] ^ acc ^ seed);
				}
			}

			byte[] buf = RandomNumberGenerator.GetBytes(MaxJoinPassword * 2);
			encodedPassword.CopyTo(buf.AsSpan()[^encodedPassword.Length..]);

			// [MS-WKST] § 2.2.5.18.2 Initializing JOINPR_USER_PASSWORD
			var obf = RandomNumberGenerator.GetBytes(ObfuscatorLength);
			ms_wkst.JOINPR_USER_PASSWORD pwstruc = new JOINPR_USER_PASSWORD
			{
				Obfuscator = obf,
				Buffer = MemoryMarshal.Cast<byte, char>(buf).ToArray(),
				Length = (uint)encodedPassword.Length,
			};

			IO.ByteWriter writer = new();
			var encoder = RpcEncoding.MsrpcNdr.CreateEncoder(writer, null);
			encoder.WriteFixedStruct(pwstruc, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pwstruc);
			var encoded = writer.GetData().ToArray();

			var sessionKey = this._proxy.SecureChannel.GetSessionKey();
			if (sessionKey is null || sessionKey.Length != 16)
				throw new NotSupportedException($"The SMB session does not have a usable session key");

			// [MS-WKST] § 2.2.5.18.3 Encryption and Decryption
			Span<byte> md5Hash = stackalloc byte[128 / 8];
			Md5Context md5 = new Md5Context();
			md5.Initialize();
			md5.HashData(sessionKey);
			md5.HashData(obf);
			md5.HashFinal(md5Hash);
			Rc4Context rc4 = new Rc4Context();
			rc4.Initialize(md5Hash);
			rc4.Transform(encoded.Slice(8), encoded.Slice(8));

			return encoded;
		}

		public async Task Unjoin(
			string serverName,
			string? accountName,
			string? password,
			CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(serverName);

			var res = (Win32ErrorCode)await _proxy.NetrUnjoinDomain2(
				serverName,
				null,
				null,
				0,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}
	}
}
