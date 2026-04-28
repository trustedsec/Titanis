using System.Collections.Immutable;
using System.Diagnostics;

namespace Titanis.Ldap
{
	public class NamedBitGroup
	{
		internal NamedBitGroup(string attributeName, Type primaryEnum, Type? shortEnum, params Type[] otherEnums)
		{
			this.AttributeName = attributeName;
			this.PrimaryEnumType = primaryEnum;
			this.ShortEnumType = shortEnum;

			this._allTypes = [primaryEnum, shortEnum, .. otherEnums];

			Dictionary<string, ulong> namedBits = new Dictionary<string, ulong>(StringComparer.OrdinalIgnoreCase);
			foreach (var enumType in this._allTypes)
			{
				if (enumType is null)
					continue;

				var enumValues = Enum.GetValues(enumType);
				foreach (IConvertible enumValue in enumValues)
				{
					var name = Enum.GetName(enumType, enumValue);
					namedBits[name] = enumValue.ToUInt64(null);
				}
			}

			this.NamedBits = namedBits;
		}

		public Dictionary<string, ulong> NamedBits { get; }
		private Type[] _allTypes;

		public string AttributeName { get; }
		public Type PrimaryEnumType { get; }
		public Type? ShortEnumType { get; }
	}

	[Flags]
	public enum UserAccountControlComputedFlags
	{
		LockedOut = (1 << 4),
		PasswordExpired = (1 << 23),
	}

	[Flags]
	public enum UserAccountControlComputedShortFlags
	{
		LO = (1 << 4),
		PE = (1 << 23),
	}

	public static class NamedBitGroups
	{
		public static readonly NamedBitGroup UserAccountControl = new NamedBitGroup("userAccountControl", typeof(UserAccountControlFlags), typeof(UserAccountControlShortFlags));
		public static readonly NamedBitGroup UserAccountControlComputed = new NamedBitGroup("msDS-User-Account-Control-Computed", typeof(UserAccountControlComputedFlags), typeof(UserAccountControlComputedShortFlags));
		public static readonly NamedBitGroup SearchFlags = new NamedBitGroup("searchFlags", typeof(SearchFlags), typeof(SearchShortFlags));
		public static readonly NamedBitGroup SystemFlags = new NamedBitGroup("systemFlags", typeof(SystemFlags), typeof(SystemShortFlags));
		public static readonly NamedBitGroup SchemaFlagsEx = new NamedBitGroup("schemaFlagsEx", typeof(SchemaFlags), typeof(SchemaShortFlags));
		public static readonly NamedBitGroup GroupType = new NamedBitGroup("groupType", typeof(GroupTypeFlags), null);
		public static readonly NamedBitGroup InstanceType = new NamedBitGroup("instanceType", typeof(InstanceTypeFlags), typeof(InstanceTypeShortFlags));
		public static readonly NamedBitGroup NtdsaOptions = new NamedBitGroup("options", typeof(NtdsaOptionsFlags), typeof(NtdsaOptionsShortFlags));

		public static readonly NamedBitGroup SupportedEncryptionTypes = new NamedBitGroup("msDS-SupportedEncryptionTypes", typeof(SupportedEncryptionTypes), null);
		public static readonly NamedBitGroup SamAccountType = new NamedBitGroup("samAccountType", typeof(SamAccountType), null);

		public static readonly ImmutableArray<NamedBitGroup> AllGroups = [
			UserAccountControl,
			UserAccountControlComputed,
			SearchFlags,
			SystemFlags,
			SchemaFlagsEx,
			GroupType,
			InstanceType,
			NtdsaOptions,
			SupportedEncryptionTypes,
			SamAccountType,
		];

		internal static readonly Dictionary<string, NamedBitGroup> GroupsByName = AllGroups.ToDictionary(r => r.AttributeName, StringComparer.OrdinalIgnoreCase);
	}
}
