using System.Buffers.Binary;
using System.Numerics;
using System.Text;
using System.Threading;
using Titanis.DceRpc;
using Titanis.Winterop;

namespace Titanis.Msrpc.Msrrp
{
	// [MS-RRP] § 3.1.1.5 Values
	public enum RegistryValueType
	{
		None = 0,
		String = 1,
		ExpandString = 2,
		Binary = 3,
		DwordLE = 4,
		DwordBE = 5,
		MultiString = 7,
		Qword = 11,
	}

	// [MS-RRP] § 3.1.5.27 BaseRegSaveKeyEx (Opnum 31
	public enum RegistrySaveFormat
	{
		Original,
		Latest = 2,
		NotCompressed = 4,
	}

	public class RegistryKeyInfo
	{
		public string ClassName { get; set; }
		public int SubkeyCount { get; set; }
		public int MaxSubkeyLength { get; set; }
		public int MaxClassLength { get; set; }
		public int ValueCount { get; set; }
		public int MaxValueNameLength { get; set; }
		public int MaxValueDataLength { get; set; }
		public int SecurityDescriptorLength { get; set; }
		public DateTime LastWriteTime { get; set; }
	}

	public class RegistrySubkeyInfo
	{
		public RegistrySubkeyInfo(string keyName, string? className)
		{
			this.KeyName = keyName;
			this.ClassName = className;
		}

		public string KeyName { get; }
		public string? ClassName { get; }
	}

	public class RegistryValueInfo
	{
		public RegistryValueInfo(
			string name,
			RegistryValueType valueType,
			int dataLength,
			byte[]? bytes,
			object? typedValue)
		{
			this.Name = name;
			this.ValueType = valueType;
			this.DataLength = dataLength;
			this.Bytes = bytes;
			this.TypedValue = typedValue;
		}

		public string Name { get; }
		public RegistryValueType ValueType { get; }
		public int DataLength { get; }
		public byte[]? Bytes { get; }
		public object? TypedValue { get; }
	}

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
			int cbLen = keyInfo.MaxValueDataLength;


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
					TryDecodeValue((RegistryValueType)lpType.value, data)
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

		public async Task<RegistryValueInfo> GetValue(string? name, CancellationToken cancellationToken)
		{
			uint len = 0;

			RpcPointer<uint> lpType = new(0xAA55);
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
			return new RegistryValueInfo(name, (RegistryValueType)lpType.value, 0, data, null);
		}

		internal static object? TryDecodeValue(RegistryValueType valueType, byte[]? data)
		{
			if (data is null)
				return null;

			return (valueType, data.Length) switch
			{
				(RegistryValueType.Qword, 8) => BinaryPrimitives.ReadUInt64LittleEndian(data),
				(RegistryValueType.DwordLE, 4) => BinaryPrimitives.ReadUInt32LittleEndian(data),
				(RegistryValueType.DwordBE, 4) => BinaryPrimitives.ReadUInt32BigEndian(data),
				(RegistryValueType.String, _) => TryDecodeUtf16String(data),
				(RegistryValueType.MultiString, _) => TryDecodeUtf16MultiString(data),
				(RegistryValueType.Binary, _) => null,
				_ => null
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

			for (int i = 2; i <= bytes.Length; i += 2)
			{
				var c = BinaryPrimitives.ReadUInt16LittleEndian(bytes.AsSpan(i - 2, 2));
				if (c == 0)
				{
					try
					{
						strs.Add(Encoding.Unicode.GetString(bytes.AsSpan(startIndex, i - startIndex)));
					}
					catch
					{
						return null;
					}
				}
			}

			return strs.ToArray();
		}
	}

	partial class RegistryKey : IDisposable, IAsyncDisposable
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