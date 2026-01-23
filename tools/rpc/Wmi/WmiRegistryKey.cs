using System.ComponentModel;
using System.IO;
using Titanis;
using Titanis.Msrpc.Mswmi;
using Titanis.Security.Kerberos;
using Titanis.Winterop;
using Titanis.Winterop.Registry;

namespace Wmi.Registry
{

	record struct RegistryValue(string name, RegistryValueKind kind, object? data);

	class WmiRegistryKey : IRegistryKey
	{
		internal WmiRegistryKey(dynamic stdregprov, RegistryPath keyPath)
		{
			this.stdregprov = stdregprov;
			this.KeyPath = keyPath;
		}

		internal readonly dynamic stdregprov;
		public RegistryPath KeyPath { get; }

		internal uint RootKeyHandle => (uint)this.KeyPath.Root;

		public string KeyName => this.KeyPath.KeyName;

		public async Task EnumerateValues(
			Func<string, RegistryValueKind, bool> includeDataPredicate,
			Action<string, RegistryValueKind, object?> iterator,
			RegistryKeyEnumerateOptions options,
			CancellationToken cancellationToken
			)
		{
			var rootKey = this.RootKeyHandle;
			var keyPath = this.KeyPath.KeyPath;

			dynamic regEntries = await ((Task<WmiInstanceObject>)this.stdregprov.EnumValues(rootKey, keyPath, cancellationToken)).ConfigureAwait(false);
			((Win32ErrorCode)regEntries.ReturnValue).CheckAndThrow();
			var names = ((object[]?)regEntries.sNames);

			// Should be the same, but just in case...
			if (names == null)
			{
				// If the key only contains the default value, WMI returns null for both arrays.
				// The problem is, this leaves no way to determine what the type of the default value is.
				// It is usually REG_SZ, but not always.  But it turns out, regardless of the type, GetString converts the value to a string before returning it.

				object? data;
				try
				{
					data =
						(includeDataPredicate is null || includeDataPredicate(string.Empty, RegistryValueKind.REG_SZ)) ? await this.GetValue(string.Empty, RegistryValueKind.REG_SZ, options, cancellationToken)
						: null;
				}
				catch (Exception ex)
				{
					if (0 == (options & RegistryKeyEnumerateOptions.ContinueOnException))
						throw;

					if (0 != (options & RegistryKeyEnumerateOptions.PassExceptionToIterator))
						data = ex;
					else
						data = null;
				}

				iterator(string.Empty, RegistryValueKind.REG_SZ, data);
			}

			var types = ((object[])regEntries.Types);
			int count = Math.Min(names.Length, types.Length);
			for (int i = 0; i < count; i++)
			{
				var name = (string)names[i];
				var kind = (RegistryValueKind)(int)types[i];

				object? data =
					(includeDataPredicate is null || includeDataPredicate(name, kind)) ? await this.GetValue(name, kind, options, cancellationToken)
					: null;

				iterator(name, kind, data);
			}
		}

		public async Task<string[]> GetSubkeyNames(CancellationToken cancellationToken)
		{
			var rootKey = this.RootKeyHandle;
			var keyPath = this.KeyPath.KeyPath;

			var regKeys = (await this.stdregprov.EnumKey(rootKey, keyPath).ConfigureAwait(false));
			((Win32ErrorCode)regKeys.ReturnValue).CheckAndThrow();
			var subkeyNames = ((object[])regKeys.sNames)?.OfType<string>();
			return subkeyNames;
		}

		internal async Task<object?> GetValue(
			string valueName,
			RegistryValueKind regType,
			RegistryKeyEnumerateOptions options,
			CancellationToken cancellationToken
			)
		{
			var registry = this.stdregprov;
			var rootKeyHandle = this.RootKeyHandle;
			var keyPath = this.KeyPath.KeyPath;

			try
			{
				var wmiobjValue = await ((Task<WmiInstanceObject>)(regType switch
				{
					RegistryValueKind.REG_SZ => registry.GetStringValue(rootKeyHandle, keyPath, valueName),
					RegistryValueKind.REG_EXPAND_SZ => registry.GetExpandedStringValue(rootKeyHandle, keyPath, valueName),
					RegistryValueKind.REG_DWORD => registry.GetDWORDValue(rootKeyHandle, keyPath, valueName),
					RegistryValueKind.REG_MULTI_SZ => registry.GetMultiStringValue(rootKeyHandle, keyPath, valueName),
					RegistryValueKind.REG_QWORD => registry.GetQWORDValue(rootKeyHandle, keyPath, valueName),
					RegistryValueKind.REG_BINARY => registry.GetBinaryValue(rootKeyHandle, keyPath, valueName),
					_ => throw new Win32Exception((int)Win32ErrorCode.ERROR_INVALID_PARAMETER, $"Unsupported registry value type '{regType}' for value '{valueName}'"),
				})).ConfigureAwait(false);

				((Hresult)((dynamic)wmiobjValue).ReturnValue).CheckAndThrow();

				dynamic dynValue = wmiobjValue;
				var data = regType switch
				{
					RegistryValueKind.REG_SZ => (object)(string)dynValue.sValue,
					RegistryValueKind.REG_EXPAND_SZ => (string)dynValue.sValue,
					RegistryValueKind.REG_BINARY => Array.ConvertAll((object[])dynValue.uValue, r => (byte)r),
					RegistryValueKind.REG_DWORD => (uint)dynValue.uValue,
					RegistryValueKind.REG_MULTI_SZ => Array.ConvertAll((object[])dynValue.sValue, r => (string)r),
					RegistryValueKind.REG_QWORD => (ulong)dynValue.uValue,
					_ => throw new NotSupportedException($"Unsupported registry value type '{regType}' for value '{valueName}'")
				};

				return data;
			}
			catch (Exception ex)
			{
				if (0 == (options & RegistryKeyEnumerateOptions.ContinueOnException))
					throw;

				if (0 != (options & RegistryKeyEnumerateOptions.PassExceptionToIterator))
					return ex;
				else
					return null;
			}
		}

		public async Task<IRegistryKey> OpenSubkey(string subkeyName, CancellationToken cancellationToken)
		{
			var keyPath = this.KeyPath;
			var subkeyPath = keyPath.Append(subkeyName);
			return new WmiRegistryKey(this.stdregprov, subkeyPath);
		}

	}
}
