using ms_efsr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Winterop;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msefsr
{
	public class EfsClient : RpcServiceClient<efsrpcClientProxy>
	{
		public const string EfsPipeName = "efsrpc";

		public async Task<EfsOpenFile> OpenFile(string fileName, CancellationToken cancellationToken)
		{
			VerifyFileName(fileName);

			var pHandle = new DceRpc.RpcPointer<DceRpc.RpcContextHandle>();
			var res = (Win32ErrorCode)await this._proxy.EfsRpcOpenFileRaw(
				pHandle,
				fileName,
				0,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new EfsOpenFile(pHandle.value, this);
		}

		internal async Task CloseFile(RpcContextHandle handle, CancellationToken cancellationToken)
		{
			await this._proxy.EfsRpcCloseRaw(new RpcPointer<RpcContextHandle>(handle), cancellationToken).ConfigureAwait(false);
		}

		private static void VerifyFileName(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
		}

		public async Task EncryptFile(string fileName, CancellationToken cancellationToken)
		{
			VerifyFileName(fileName);

			var res = (Win32ErrorCode)await this._proxy.EfsRpcEncryptFileSrv(fileName, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		public async Task DecryptFile(string fileName, CancellationToken cancellationToken)
		{
			VerifyFileName(fileName);

			var res = (Win32ErrorCode)await this._proxy.EfsRpcDecryptFileSrv(fileName, 0, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		public async Task<EncryptionCertificateHash[]> QueryUsersOnFile(string fileName, CancellationToken cancellationToken)
		{
			VerifyFileName(fileName);

			RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>> users = new();
			var res = (Win32ErrorCode)await this._proxy.EfsRpcQueryUsersOnFile(
				fileName,
				users,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return ParseUserList(users);
		}

		private EncryptionCertificateHash[] ParseUserList(RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>> pUsers)
		{
			var rpcUsers = pUsers.value.value.Users.value;
			return Array.ConvertAll(rpcUsers, r =>
			{
				var rec = r.value;

				return new EncryptionCertificateHash(
					rec.lpDisplayInformation.value,
					rec.UserSid.value.ToSid(),
					rec.Hash.value.bData.value
					);
			});
		}

		public async Task<EncryptionCertificateHash[]> QueryRecoveryAgents(string fileName, CancellationToken cancellationToken)
		{
			VerifyFileName(fileName);

			RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH_LIST>> users = new();
			var res = (Win32ErrorCode)await this._proxy.EfsRpcQueryRecoveryAgents(
				fileName,
				users,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return ParseUserList(users);
		}

		public async Task RemoveUsersFromFile(
			string fileName,
			EncryptionCertificateHash[] users,
			CancellationToken cancellationToken)
		{
			VerifyFileName(fileName);

			ENCRYPTION_CERTIFICATE_HASH_LIST rpcUsers = ToRpcList(users);
			var res = (Win32ErrorCode)await this._proxy.EfsRpcRemoveUsersFromFile(
				fileName,
				rpcUsers,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		private static ENCRYPTION_CERTIFICATE_HASH_LIST ToRpcList(EncryptionCertificateHash[] users)
		{
			return new()
			{
				nCert_Hash = (uint)users.Length,
				Users = new RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE_HASH>[]>(Array.ConvertAll(users, r => r.ToRpc()))
			};
		}

		private static ENCRYPTION_CERTIFICATE_LIST ToRpcList(EncryptionCertificate[] users)
		{
			return new()
			{
				nUsers = (uint)users.Length,
				Users = new RpcPointer<RpcPointer<ENCRYPTION_CERTIFICATE>[]>(Array.ConvertAll(users, r => r.ToRpc()))
			};
		}

		public async Task AddUsersToFile(
			string fileName,
			EncryptionCertificate[] users,
			CancellationToken cancellationToken)
		{
			VerifyFileName(fileName);

			var rpcUsers = (users == null) ? default : ToRpcList(users);
			var res = (Win32ErrorCode)await this._proxy.EfsRpcAddUsersToFile(
				fileName,
				rpcUsers,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		[Flags]
		public enum EfsAddUsersOptions
		{
			None = 0,
			AddPolicyKeyType = 2,
			ReplaceDdf = 4,
		}

		public async Task AddUsersToFile(
			string fileName,
			EncryptionCertificate[] users,
			EfsAddUsersOptions options,
			CancellationToken cancellationToken)
		{
			VerifyFileName(fileName);

			var rpcUsers = (users == null) ? default : ToRpcList(users);
			var res = (Win32ErrorCode)await this._proxy.EfsRpcAddUsersToFileEx(
				(uint)options,
				null,
				fileName,
				rpcUsers,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		// [MS-EFSR] § 3.1.4.2.12 Receiving an EfsRpcFileKeyInfo Message (Opnum 12)
		enum KeyInfoClass
		{
			BasicKeyInfo = 1,
			CheckCompatibilityInfo = 2,
			UpdateKeyUsed = 0x100,
			CheckDecryptionStatus = 0x200,
			CheckEncryptionStatus = 0x400,
		}
		public async Task GetBasicFileKeyInfo(string fileName, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(fileName);

			RpcPointer<RpcPointer<EFS_RPC_BLOB>> keyInfo = new();
			var res = (Win32ErrorCode)await _proxy.EfsRpcFileKeyInfo(
				fileName,
				(uint)KeyInfoClass.BasicKeyInfo,
				keyInfo,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			// TODO: Parse metadata
			//keyInfo.value.value.bData;
		}

		public async Task GetBasicFileKeyInfo(string fileName, BasicFileKeyFlags flags, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(fileName);

			RpcPointer<RpcPointer<EFS_RPC_BLOB>> keyInfo = new();
			var res = (Win32ErrorCode)await _proxy.EfsRpcFileKeyInfoEx(
				(uint)flags,
				null,
				fileName,
				(uint)KeyInfoClass.BasicKeyInfo,
				keyInfo,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			// TODO: Parse metadata
			//keyInfo.value.value.bData;
		}

		private static EFS_RPC_BLOB SecurityDescriptorToBlob(SecurityDescriptor sd)
		{
			ArgumentNullException.ThrowIfNull(sd);

			var bytes = sd.ToByteArray();
			return new EFS_RPC_BLOB
			{
				cbData = (uint)bytes.Length,
				bData = new RpcPointer<byte[]>(bytes)
			};
		}

		// [MS-EFSR] § 3.1.4.2.13 Receiving an EfsRpcDuplicateEncryptionInfoFile Message (Opnum 13)
		enum CreateDisposition
		{
			CreateNew = 1,
			CreateAlways = 2,
		}
		public async Task DuplicateEncryptionInfoFile(string sourceFileName, string destFileName, FileAttributes attributes, bool overwrite, SecurityDescriptor sd, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(sourceFileName);
			ArgumentException.ThrowIfNullOrEmpty(destFileName);
			ArgumentNullException.ThrowIfNull(sd);

			var res = (Win32ErrorCode)await _proxy.EfsRpcDuplicateEncryptionInfoFile(
				sourceFileName,
				destFileName,
				(uint)(overwrite ? CreateDisposition.CreateAlways : CreateDisposition.CreateNew),
				(uint)attributes,
				new RpcPointer<EFS_RPC_BLOB>(SecurityDescriptorToBlob(sd)),
				0,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		public async Task<byte[]?> GetEncryptedFileMetadata(string fileName, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(fileName);

			RpcPointer<RpcPointer<EFS_RPC_BLOB>> efsStreamBlob = new();
			var res = (Win32ErrorCode)await _proxy.EfsRpcGetEncryptedFileMetadata(
				fileName,
				efsStreamBlob,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			// TODO: Parse metadata
			return efsStreamBlob.value?.value.bData?.value;
		}

		public async Task SetEncryptedFileMetadata(string fileName, byte[] metadata, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(fileName);
			ArgumentNullException.ThrowIfNull(metadata);

			RpcPointer<RpcPointer<EFS_RPC_BLOB>> efsStreamBlob = new();
			var res = (Win32ErrorCode)await _proxy.EfsRpcGetEncryptedFileMetadata(
				fileName,
				efsStreamBlob,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		public async Task EncryptFile(string fileName, string? protector, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(fileName);

			var res = (Win32ErrorCode)await _proxy.EfsRpcEncryptFileExSrv(
				fileName,
				protector,
				0,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();
		}
	}

	[Flags]
	public enum BasicFileKeyFlags : uint
	{
		None = 0,
	}
}
