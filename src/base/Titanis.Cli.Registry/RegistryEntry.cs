using System.ComponentModel;
using Titanis.Winterop.Registry;

namespace Titanis.Cli.Registry
{
	/// <summary>
	/// Represents an entry in the Windows registry, including its path, security descriptor, and associated data.
	/// </summary>
	public class RegistryEntry
	{
		[DisplayName("Root")]
		public PredefinedKey Root { get; }

		[DisplayName("Key Path")]
		public string SubKey { get; }

		//TODO: Should be re-added once SecurityDescriptor support is added to WMI registry provider
		//public SecurityDescriptor SecurityDescriptor { get; set; }
		//public SecurityIdentifier? Owner => this.SecurityDescriptor?.Owner;
		//public SecurityIdentifier? Group => this.SecurityDescriptor?.Group;
		//[DisplayName("DACL")]
		//public string? Dacl => this.SecurityDescriptor?.ToSddlString(SecurityDescriptorSections.Access);
		//[DisplayName("SACL")]
		//public string? Sacl => this.SecurityDescriptor?.ToSddlString(SecurityDescriptorSections.Audit);

		[DisplayName("Value")]
		public string? ValueName { get; }

		[DisplayName("Data")]
		public RegistryData? Data { get; }

		/// <summary>
		/// Creates a new <see cref="RegistryEntry"/> instance.
		/// </summary>
		/// <param name="root">root registry key for this entry</param>
		/// <param name="subKey">path to subkey relative to root</param>
		/// <param name="valueName">Name of value for this entry if present</param>
		/// <param name="data"><see cref="RegistryData"/> of this entry if present</param>
		public RegistryEntry(PredefinedKey root, string subKey, string? valueName = null, RegistryData? data = null)
		{
			Root = root;
			SubKey = subKey;
			ValueName = valueName;
			Data = data;
		}

		public RegistryEntry(RegistryPath basePath, string? valueName = null, RegistryData? data = null)
		{
			Root = basePath.Root;
			SubKey = basePath.KeyPath;
			ValueName = valueName;
			Data = data;
		}

		[DisplayName("Type")]
		public RegistryValueType Kind => Data?.Kind ?? RegistryValueType.None;

		//reg.exe export does "valuename"=hex(optional type number if not REG_BINARY):BB,
		//splits are made just before 80 characters.  First line with valuename can be longer and will include first byte + , prior to \\\n
		// Subsequent lines are indented with 2 spaces.
	}
}
