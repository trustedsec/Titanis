using Titanis.Winterop.Security;

namespace Titanis.Winterop.Registry
{

	// [MS-RRP] § 3.1.5.15
	[Flags]
	public enum RegistryKeyOptions
	{
		None = 0,

		BackupRestore = 4,
		OpenLink = 8,
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

		public override string ToString() => this.KeyName;
	}
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

	// [MS-RRP] § 3.1.1.5 Values
	public enum RegistryValueTypeAlt
	{
		NONE = 0,
		SZ = 1,
		EXPAND_SZ = 2,
		BINARY = 3,
		DWORD = 4,
		DWORD_BIG_ENDIAN = 5,
		MULTI_SZ = 7,
		QWORD = 11,
	}

	// [MS-RRP] § 3.1.5.27 BaseRegSaveKeyEx (Opnum 31
	public enum RegistrySaveFormat
	{
		Original,
		Latest = 2,
		NotCompressed = 4,
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

	public interface IRegistryKey : IAsyncDisposable
	{
		Task<IRegistryKey> OpenSubkey(string subkeyPath, RegistryAccessRights access, RegistryKeyOptions options, CancellationToken cancellationToken);
		Task<RegistryKeyInfo> QueryInfo(CancellationToken cancellationToken);
		Task<RegistryValueInfo> GetValue(string? name, CancellationToken cancellationToken);

		IAsyncEnumerable<RegistryValueInfo> GetValues(bool includeData, CancellationToken cancellationToken);
		IAsyncEnumerable<RegistrySubkeyInfo> GetSubkeyNames(CancellationToken cancellationToken);
		string KeyName { get; }
		string KeyPath { get; }
	}
}
