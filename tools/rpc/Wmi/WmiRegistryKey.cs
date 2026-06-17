using System.ComponentModel;
using Titanis;
using Titanis.Winterop;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Wmi.Registry
{

	class WmiRegistryKey : IRegistryKey
	{
		internal WmiRegistryKey(dynamic stdregprov, RegistryPath keyPath, ILog? log)
		{
			this.stdregprov = stdregprov;
			this.KeyPath = keyPath;
			this._log = log;

		}

		internal readonly dynamic stdregprov;
		public RegistryPath KeyPath { get; }
		private readonly ILog? _log;

		internal uint RootKeyHandle => (uint)this.KeyPath.Root;

		public string KeyName => this.KeyPath.KeyName;

		string IRegistryKey.KeyPath => this.KeyPath.ToString();

		public override string ToString() => this.KeyPath.ToString();

		public async Task<IRegistryKey> OpenSubkey(string subkeyPath, RegistryAccessRights access, RegistryKeyOptions options, CancellationToken cancellationToken)
		{
			if (options != RegistryKeyOptions.None)
			{
				_log?.WriteWarning("Wmi registry provider doesn't support RegistrykeyOptions");
			}
			var keyPath = this.KeyPath.Append(subkeyPath);
			return new WmiRegistryKey(stdregprov, keyPath, _log);
		}

		public Task<RegistryKeyInfo> QueryInfo(CancellationToken cancellationToken)
		{
			throw new NotSupportedException("WMI can't retreive Class Name, SecurityDescriptor or LastWriteTime");
		}

		private async Task<RegistryData> GetRegistryData(string name, RegistryValueTypeAlt regType)
		{
			var registryValue = regType switch
			{
				RegistryValueTypeAlt.SZ => (await this.stdregprov.GetStringValue(RootKeyHandle, this.KeyPath.KeyPath, name).ConfigureAwait(false)),
				RegistryValueTypeAlt.EXPAND_SZ => (await this.stdregprov.GetExpandedStringValue(RootKeyHandle, this.KeyPath.KeyPath, name).ConfigureAwait(false)),
				RegistryValueTypeAlt.BINARY => (await this.stdregprov.GetBinaryValue(RootKeyHandle, this.KeyPath.KeyPath, name).ConfigureAwait(false)),
				RegistryValueTypeAlt.DWORD => (await this.stdregprov.GetDWORDValue(RootKeyHandle, this.KeyPath.KeyPath, name).ConfigureAwait(false)),
				RegistryValueTypeAlt.MULTI_SZ => (await this.stdregprov.GetMultiStringValue(RootKeyHandle, this.KeyPath.KeyPath, name).ConfigureAwait(false)),
				RegistryValueTypeAlt.QWORD => (await this.stdregprov.GetQWORDValue(RootKeyHandle, this.KeyPath.KeyPath, name).ConfigureAwait(false)),
				_ => throw new Win32Exception((int)Win32ErrorCode.ERROR_INVALID_PARAMETER, "Unsupported registry value type"),
			};
			((Win32ErrorCode)registryValue.ReturnValue).CheckAndThrow();
			return regType switch
			{

				RegistryValueTypeAlt.SZ => RegistryData.CreateString((string)registryValue.sValue),
				RegistryValueTypeAlt.EXPAND_SZ => RegistryData.CreateExpandableString((string)registryValue.sValue),
				RegistryValueTypeAlt.BINARY => RegistryData.CreateBinary(((Array)registryValue.uValue)?.OfType<byte>().ToArray() ?? Array.Empty<byte>()),
				RegistryValueTypeAlt.DWORD => RegistryData.CreateDword((uint)registryValue.uValue),
				RegistryValueTypeAlt.MULTI_SZ => RegistryData.CreateRegMultiString(((Array)registryValue.sValue)?.OfType<string>().ToArray() ?? Array.Empty<string>()),
				RegistryValueTypeAlt.QWORD => RegistryData.CreateQword((ulong)registryValue.uValue),
				_ => throw new Win32Exception((int)Win32ErrorCode.ERROR_INVALID_PARAMETER, "Unsupported registry value type")
			};
		}

		public async Task<RegistryValueInfo> GetValue(string? name, CancellationToken cancellationToken)
		{
			//If we're grabbing the default value we're going to assume its REG_SZ
			if (string.IsNullOrEmpty(name))
			{
				return (await GetRegistryData(string.Empty, RegistryValueTypeAlt.SZ).ConfigureAwait(false)).AsRegistryValueInfo(string.Empty);
			}
			//First we need to enumerate values to get the values type
			var regEntries = await this.stdregprov.EnumValues(RootKeyHandle, this.KeyPath.KeyPath).ConfigureAwait(false);
			((Win32ErrorCode)regEntries.ReturnValue).CheckAndThrow();
			string[]? sNames = ((Array)regEntries.sNames)?.OfType<string>().ToArray();
			int[]? types = ((Array)regEntries.Types)?.OfType<int>().ToArray();
			if (sNames is not null)
			{
				var idx = Array.FindIndex(sNames, valName => valName.Equals(name, StringComparison.OrdinalIgnoreCase));
				if (idx == -1)
				{
					throw new Win32Exception((int)Win32ErrorCode.ERROR_NOT_FOUND, $"{name} not found under {this.KeyPath}");
				}
				int typeNum = types![idx]; //If sNames is populated and has this index Types will as well
										   //Then we can actually get / return the value
				return (await GetRegistryData(name, (RegistryValueTypeAlt)typeNum).ConfigureAwait(false)).AsRegistryValueInfo(name);
			}
			throw new Win32Exception((int)Win32ErrorCode.ERROR_NOT_FOUND, $"{name} not found under {this.KeyPath}");
		}

		public async IAsyncEnumerable<RegistryValueInfo> GetValues(bool includeData, CancellationToken cancellationToken)
		{
			//For everything else we enumerate values to get types then return those values as we see them.
			var regEntries = await this.stdregprov.EnumValues(RootKeyHandle, this.KeyPath.KeyPath).ConfigureAwait(false);
			((Win32ErrorCode)regEntries.ReturnValue).CheckAndThrow();
			string[]? sNames = ((Array)regEntries.sNames)?.OfType<string>().ToArray();
			int[]? types = ((Array)regEntries.Types)?.OfType<int>().ToArray();
			if (sNames != null)
			{
				for (int idx = 0; idx < sNames.Length; idx++)
				{
					int typeNum = types![idx]; //If sNames is populated and has this index Types will as well
					string name = sNames[idx];
					if (includeData)
					{
						RegistryValueInfo retData;
						try
						{
							retData = (await GetRegistryData(name, (RegistryValueTypeAlt)typeNum).ConfigureAwait(false)).AsRegistryValueInfo(name);
						}
						catch (Win32Exception ex)
						{
							//When enumerating values under a key we don't want failure to retreive one value (for example REG_NONE type) to stop enumeration
							continue;
						}
						yield return retData;
					}
					else
					{
						yield return new RegistryValueInfo(name, (RegistryValueType)typeNum, 0, null, null);
					}
				}
			}
			else
			{
				//For whatever reason, EnumValues will return null in the case there is a populated default key
				//but no other value's under the key.  We therefore check for a default value when enum returns null;
				RegistryValueInfo? defaultValueName;
				try
				{
					defaultValueName = await GetValue(null, cancellationToken);
				}
				catch (Win32Exception)
				{
					defaultValueName = null;
				}
				if (defaultValueName is not null) yield return defaultValueName;
			}
		}

		public async IAsyncEnumerable<RegistrySubkeyInfo> GetSubkeyNames(CancellationToken cancellationToken)
		{
			var regKeys = (await this.stdregprov.EnumKey(RootKeyHandle, this.KeyPath.KeyPath).ConfigureAwait(false));
			((Win32ErrorCode)regKeys.ReturnValue).CheckAndThrow();
			var subkeyNames = ((System.Array)regKeys.sNames)?.OfType<string>().ToArray();
			if (subkeyNames is not null)
			{
				foreach (var subkey in subkeyNames)
				{
					//we can't get class name string via WMI
					yield return new RegistrySubkeyInfo(subkey, null);
				}
			}
		}

		public ValueTask DisposeAsync() => ValueTask.CompletedTask;
	}
}
