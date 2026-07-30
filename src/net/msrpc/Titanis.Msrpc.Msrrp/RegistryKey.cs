using ms_dtyp;
using System.Buffers.Binary;
using System.Numerics;
using System.Text;
using System.Threading;
using Titanis.DceRpc;
using Titanis.Winterop;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp
{

	public partial class RegistryKey
	{
		internal RegistryKey(string name, string path, RpcContextHandle hkey, RemoteRegistryClient owner)
		{
			this._hkey = hkey;
			this._owner = owner;

			this.KeyName = name;
			this.KeyPath = path;
		}

		private readonly RpcContextHandle _hkey;
		private readonly RemoteRegistryClient _owner;

		public string KeyName { get; }
		public string KeyPath { get; }

		public override string ToString() => this.KeyPath;


		public async Task<RegistryKey> CreateSubkey(string subkeyPath, RegistryAccessRights access, RegistryKeyOptions options, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> phkResult = new();
			Win32ErrorCode res = (Win32ErrorCode)await this._owner.proxy.BaseRegCreateKey(this._hkey, (subkeyPath + '\0').ToRpcUnicodeString(), default, (uint)options, (uint)access, null, phkResult, new RpcPointer<uint>(), cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return new RegistryKey(RegistryPath.GetSubkeyNameFromPath(subkeyPath), RegistryPath.Combine(this.KeyPath, subkeyPath), phkResult.value, this._owner);
		}

		public Task SetValue(string? valueName, string str, CancellationToken cancellationToken) => this.SetValue(valueName, RegistryValueType.String, Encoding.Unicode.GetBytes(str + '\0'), cancellationToken);

		public async Task SetValue(string? valueName, RegistryValueType valueKind, byte[] data, CancellationToken cancellationToken)
		{
			var res = (Win32ErrorCode)await _owner.proxy.BaseRegSetValue(
				_hkey,
				(valueName + "\0").ToRpcUnicodeString(),
				(uint)valueKind,
				data,
				(uint)data.Length,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}


		async Task<IRegistryKey> IRegistryKey.OpenSubkey(string subkeyPath, RegistryAccessRights access, RegistryKeyOptions options, CancellationToken cancellationToken) => await OpenSubkey(subkeyPath, access, options, cancellationToken).ConfigureAwait(false);
		public async Task<RegistryKey> OpenSubkey(string subkeyPath, RegistryAccessRights access, RegistryKeyOptions options, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> phkResult = new();
			Win32ErrorCode res = (Win32ErrorCode)await this._owner.proxy.BaseRegOpenKey(this._hkey, (subkeyPath + '\0').ToRpcUnicodeString(), (uint)options, (uint)access, phkResult, cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();

			return new RegistryKey(RegistryPath.GetSubkeyNameFromPath(subkeyPath), RegistryPath.Combine(this.KeyPath, subkeyPath), phkResult.value, this._owner);
		}

		public async Task<RegistryKeyInfo> QueryInfo(CancellationToken cancellationToken)
		{
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpClassOut = new();
			RpcPointer<uint> lpcSubKeys = new();
			RpcPointer<uint> lpcbMaxSubKeyLen = new();
			RpcPointer<uint> lpcbMaxClassLen = new();
			RpcPointer<uint> lpcValues = new();
			RpcPointer<uint> lpcbMaxValueNameLen = new();
			RpcPointer<uint> lpcbMaxValueLen = new();
			RpcPointer<uint> lpcbSecurityDescriptor = new();
			RpcPointer<ms_dtyp.FILETIME> lpftLastWriteTime = new();
			var res = (Win32ErrorCode)await this._owner.proxy.BaseRegQueryInfoKey(
				this._hkey,
				new ms_dtyp.RPC_UNICODE_STRING
				{
					Buffer = new RpcPointer<ArraySegment<char>>(new ArraySegment<char>(new char[16], 0, 0)),
					Length = 0,
					MaximumLength = 32
				},
				lpClassOut,
				lpcSubKeys,
				lpcbMaxSubKeyLen,
				lpcbMaxClassLen,
				lpcValues,
				lpcbMaxValueNameLen,
				lpcbMaxValueLen,
				lpcbSecurityDescriptor,
				lpftLastWriteTime,
				cancellationToken
				).ConfigureAwait(false);
			res.CheckAndThrow();

			return new RegistryKeyInfo
			{
				ClassName = lpClassOut.value.AsString(),
				SubkeyCount = (int)lpcSubKeys.value,
				MaxSubkeyLength = (int)lpcbMaxSubKeyLen.value,
				MaxClassLength = (int)lpcbMaxClassLen.value,
				ValueCount = (int)lpcValues.value,
				MaxValueNameLength = (int)lpcbMaxValueNameLen.value,
				MaxValueDataLength = (int)lpcbMaxValueLen.value,
				SecurityDescriptorLength = (int)lpcbSecurityDescriptor.value,
				LastWriteTime = lpftLastWriteTime.value.ToDateTime()
			};
		}

		public async Task SaveKey(string fileName, RegistrySaveFormat format, CancellationToken cancellationToken)
		{
			var res = (Win32ErrorCode)await this._owner.proxy.BaseRegSaveKeyEx(
				this._hkey,
				fileName.ToRpcUnicodeString(),
				null,
				(uint)format,
				cancellationToken).ConfigureAwait(false);
			res.CheckAndThrow();
		}

		public async IAsyncEnumerable<RegistrySubkeyInfo> GetSubkeyNames(CancellationToken cancellationToken)
		{
			var keyInfo = await this.QueryInfo(cancellationToken).ConfigureAwait(false);

			int index = 0;
			Win32ErrorCode res;
			ms_dtyp.RPC_UNICODE_STRING lpNameIn = new() { MaximumLength = (ushort)(keyInfo.MaxSubkeyLength * 2) };
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpNameOut = new();
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpClassIn = new(new ms_dtyp.RPC_UNICODE_STRING() { MaximumLength = (ushort)(keyInfo.MaxClassLength * 2) });
			RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> lplpClassOut = new();
			while ((res = (Win32ErrorCode)await this._owner.proxy.BaseRegEnumKey(
				this._hkey,
				(uint)index++,
				lpNameIn,
				lpNameOut,
				lpClassIn,
				lplpClassOut,
				new RpcPointer<ms_dtyp.FILETIME>(),
				cancellationToken
				).ConfigureAwait(false)) == Win32ErrorCode.ERROR_SUCCESS)
			{
				var name = lpNameOut.value.AsString().TrimEnd('\0');
				var className = lplpClassOut.value.value.AsString()?.TrimEnd('\0');

				yield return new RegistrySubkeyInfo(name, className);
			}

			if (res is not Win32ErrorCode.ERROR_SUCCESS and not Win32ErrorCode.ERROR_NO_MORE_ITEMS)
				res.CheckAndThrow();
		}

		public IAsyncEnumerable<RegistryValueInfo> GetValueNames(CancellationToken cancellationToken) => this.GetValues(false, cancellationToken);
		public async IAsyncEnumerable<RegistryValueInfo> GetValues(bool includeData, CancellationToken cancellationToken)
		{
			var keyInfo = await this.QueryInfo(cancellationToken).ConfigureAwait(false);

			int cbBuffer = keyInfo.MaxValueDataLength;
			keyInfo.MaxValueNameLength++;

			int index = 0;
			Win32ErrorCode res;
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpValueNameOut = new();
			RpcPointer<uint> lpType = new();

			byte[] stubBuffer = new byte[cbBuffer];
			RpcPointer<ArraySegment<byte>> lpData = includeData ? new(new ArraySegment<byte>(stubBuffer, 0, 0)) : null;

			ms_dtyp.RPC_UNICODE_STRING lpValueNameIn = new() { MaximumLength = (ushort)(keyInfo.MaxValueNameLength * 2), Buffer = new RpcPointer<ArraySegment<char>>(new ArraySegment<char>(new char[keyInfo.MaxValueNameLength], 0, 0)) };
			RpcPointer<uint> lpcbData = new(includeData ? (uint)cbBuffer : 0);
			RpcPointer<uint> lpcbLen = new(0U);
			while ((res = (Win32ErrorCode)await this._owner.proxy.BaseRegEnumValue(
				this._hkey,
				(uint)index,
				lpValueNameIn,
				lpValueNameOut,
				lpType,
				lpData,
				lpcbData,
				lpcbLen,
				cancellationToken
				).ConfigureAwait(false)) == Win32ErrorCode.ERROR_SUCCESS)
			{
				var valueBuf = lpData?.value.Array;
				var data = (valueBuf != null) ? valueBuf.AsSpan(0, Math.Min((int)lpcbLen.value, cbBuffer)).ToArray() : null;

				yield return new RegistryValueInfo(
					lpValueNameOut.value.AsString(true) ?? string.Empty,
					(RegistryValueType)lpType.value,
					(int)lpcbLen.value,
					data,
					TryDecodeValue((RegistryValueType)lpType.value, data, false)
					);

				index++;

				if (includeData)
					lpData.value = (new ArraySegment<byte>(stubBuffer, 0, 0));
				lpcbData.value = includeData ? (uint)cbBuffer : 0;
				lpcbLen.value = 0U;
			}

			if (res is not Win32ErrorCode.ERROR_SUCCESS and not Win32ErrorCode.ERROR_NO_MORE_ITEMS)
				res.CheckAndThrow();
		}

		public async Task<SecurityDescriptor> GetSecurityDescriptor(SecurityInfo securityInfo, CancellationToken cancellationToken)
		{
			RpcPointer<ms_rrp.RPC_SECURITY_DESCRIPTOR> pRpcSecurityDescriptorOut = new();
			var res = (Win32ErrorCode)await this._owner.proxy.BaseRegGetKeySecurity(
				this._hkey,
				(uint)securityInfo,
				default,
				pRpcSecurityDescriptorOut,
				cancellationToken).ConfigureAwait(false);
			if (res is Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER)
			{
				pRpcSecurityDescriptorOut = new RpcPointer<ms_rrp.RPC_SECURITY_DESCRIPTOR>(new ms_rrp.RPC_SECURITY_DESCRIPTOR
				{
					cbInSecurityDescriptor = pRpcSecurityDescriptorOut.value.cbInSecurityDescriptor,
					lpSecurityDescriptor = new RpcPointer<ArraySegment<byte>>(new ArraySegment<byte>(new byte[pRpcSecurityDescriptorOut.value.cbInSecurityDescriptor], 0, 0))
				});
				res = (Win32ErrorCode)await this._owner.proxy.BaseRegGetKeySecurity(
					this._hkey,
					(uint)securityInfo,
					pRpcSecurityDescriptorOut.value,
					pRpcSecurityDescriptorOut,
					cancellationToken).ConfigureAwait(false);
			}

			if (res is not Win32ErrorCode.ERROR_SUCCESS)
				res.CheckAndThrow();

			return new SecurityDescriptor(pRpcSecurityDescriptorOut.value.lpSecurityDescriptor.value);
		}
		public async Task<SecurityDescriptor> SetSecurityDescriptor(SecurityInfo securityInfo, SecurityDescriptor sd, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(sd);

			var sdBytes = sd.ToByteArray();

			RpcPointer<ms_rrp.RPC_SECURITY_DESCRIPTOR> pRpcSecurityDescriptorOut = new();
			var res = (Win32ErrorCode)await this._owner.proxy.BaseRegSetKeySecurity(
				this._hkey,
				(uint)securityInfo,
				new ms_rrp.RPC_SECURITY_DESCRIPTOR
				{
					cbInSecurityDescriptor = (uint)sdBytes.Length,
					cbOutSecurityDescriptor = (uint)sdBytes.Length,
					lpSecurityDescriptor = new RpcPointer<ArraySegment<byte>>(sdBytes)
				},
				cancellationToken).ConfigureAwait(false);

			if (res is not Win32ErrorCode.ERROR_SUCCESS)
				res.CheckAndThrow();

			return new SecurityDescriptor(pRpcSecurityDescriptorOut.value.lpSecurityDescriptor.value);
		}

		public async Task<RegistryValueInfo> GetValue(string? name, CancellationToken cancellationToken)
		{
			uint len = 0;

			RpcPointer<uint> lpType = new();
			ms_dtyp.RPC_UNICODE_STRING lpValueName = string.IsNullOrEmpty(name) ? new ms_dtyp.RPC_UNICODE_STRING
			{
				Buffer = new RpcPointer<ArraySegment<char>>(new char[1]),
				Length = 2,
				MaximumLength = 2,
			} : (name + '\0').ToRpcUnicodeString();
			//ms_dtyp.RPC_UNICODE_STRING lpValueName = name.ToRpcUnicodeString();
			RpcPointer<uint> lpcbLen = new(0U);
			RpcPointer<uint> lpcbData = new(len);
			RpcPointer<ArraySegment<byte>> lpData = new(new ArraySegment<byte>(new byte[len], 0, 0));
			var res = (Win32ErrorCode)await this._owner.proxy.BaseRegQueryValue(
				this._hkey,
				lpValueName,
				lpType,
				lpData,
				lpcbData,
				lpcbLen,
				cancellationToken).ConfigureAwait(false);

			if (res is Win32ErrorCode.ERROR_MORE_DATA)
			{
				lpData.value = new ArraySegment<byte>(new byte[lpcbData.value], 0, 0);
				lpcbLen.value = 0;
				res = (Win32ErrorCode)await this._owner.proxy.BaseRegQueryValue(
					this._hkey,
					lpValueName,
					lpType,
					lpData,
					lpcbData,
					lpcbLen,
					cancellationToken).ConfigureAwait(false);
			}

			if (res is not Win32ErrorCode.ERROR_SUCCESS)
				res.CheckAndThrow();

			byte[]? data = lpData.value.Array;
			if (data != null && lpcbLen.value < data.Length)
				Array.Resize(ref data, (int)lpcbLen.value);
			return new RegistryValueInfo(name, (RegistryValueType)lpType.value, 0, data, TryDecodeValue((RegistryValueType)lpType.value, data, false));
		}

		public async Task DeleteKey(string subkeyPath, CancellationToken cancellationToken)
		{
			var result = (Win32ErrorCode)await this._owner.proxy.BaseRegDeleteKey(
				this._hkey,
				(subkeyPath + '\0').ToRpcUnicodeString(),
				cancellationToken ).ConfigureAwait(false);
			result.CheckAndThrow();
		}

		public async Task DeleteValue(string valueName, CancellationToken cancellationToken)
		{
			if(valueName == null)
			{
				throw new ArgumentException("Argument can not be null", nameof(valueName));
			}
			RPC_UNICODE_STRING lpValueName = valueName == string.Empty ? new RPC_UNICODE_STRING
			{
				Buffer = new RpcPointer<ArraySegment<char>>(new char[1]),
				Length = 2,
				MaximumLength = 2,
			} : (valueName + '\0').ToRpcUnicodeString();
			var result = (Win32ErrorCode)await this._owner.proxy.BaseRegDeleteValue(
				this._hkey,
				lpValueName,
				cancellationToken).ConfigureAwait(false);
			result.CheckAndThrow();
		}

		public static object? TryDecodeValue(RegistryValueType valueType, byte[]? data, bool undecodedAsBytes)
		{
			if (data is null)
				return null;

			return (valueType, data.Length) switch
			{
				(RegistryValueType.Qword, 8) => BinaryPrimitives.ReadUInt64LittleEndian(data),
				(RegistryValueType.DwordLE, 4) => BinaryPrimitives.ReadUInt32LittleEndian(data),
				(RegistryValueType.DwordBE, 4) => BinaryPrimitives.ReadUInt32BigEndian(data),
				(RegistryValueType.String or RegistryValueType.ExpandString, _) => TryDecodeUtf16String(data),
				(RegistryValueType.MultiString, _) => TryDecodeUtf16MultiString(data),
				// (RegistryValueType.Binary, _) => null,
				_ => undecodedAsBytes ? data : null
			};
		}

		private static string? TryDecodeUtf16String(byte[] bytes)
		{
			int length = bytes.Length;

			if ((length % 2) != 0)
				return null;

			if (length >= 2 && bytes[^1] == 0 && bytes[^2] == 0)
				length -= 2;

			try
			{
				var str = Encoding.Unicode.GetString(bytes, 0, length);
				return str;
			}
			catch
			{
				return null;
			}
		}

		private static string[]? TryDecodeUtf16MultiString(byte[] bytes)
		{
			int startIndex = 0;
			List<string> strs = new List<string>();

			for (int i = 2; i < bytes.Length; i += 2)
			{
				var c = BinaryPrimitives.ReadUInt16LittleEndian(bytes.AsSpan(i - 2, 2));
				if (c == 0)
				{
					try
					{
						strs.Add(Encoding.Unicode.GetString(bytes.AsSpan(startIndex, i - startIndex - 2)));
					}
					catch
					{
						return null;
					}
					startIndex = i;
				}
			}

			return strs.ToArray();
		}
	}

	partial class RegistryKey : IRegistryKey, IDisposable, IAsyncDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_ = this.Close(CancellationToken.None);
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public Task Close(CancellationToken cancellationToken)
		{
			return this._owner.proxy.BaseRegCloseKey(new RpcPointer<RpcContextHandle>(this._hkey), cancellationToken);
		}

		public async ValueTask DisposeAsync()
		{
			await this.Close(CancellationToken.None).ConfigureAwait(false);
		}
	}
}