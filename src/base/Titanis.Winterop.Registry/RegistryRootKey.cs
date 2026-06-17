using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Winterop.Registry
{
	/// <summary>
	/// Specifies a predefined root key in the Windows registry.
	/// </summary>
	public enum PredefinedKey : uint
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		Invalid = 0,
		ClassesRoot = 0x80000000,
		CurrentUser,
		LocalMachine,
		Users,
		PerformanceData,
		CurrentConfig,
		PerformanceText = 0x80000050,
		PerformanceNlsText = 0x80000060,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}

	public class RegistryRootKey
	{
		// [MS-RRP] § 3.1.1.7 Predefined Keys
		public static readonly string[] RootNames = new string[]
		{
			"HKEY_CLASSES_ROOT",
			"HKEY_CURRENT_USER",
			"HKEY_LOCAL_MACHINE",
			"HKEY_USERS",
			"HKEY_PERFORMANCE_DATA",
			"HKEY_CURRENT_CONFIG",
			"HKEY_PERFORMANCE_TEXT",
			"HKEY_PERFORMANCE_NLS_TEXT"
		};

		public static readonly string[] RootShortNames = new string[]
		{
			"HKCR",
			"HKCU",
			"HKLM",
			"HKU",
			"HKPD",
			"HKCC",
			"HKPT",
			"HKPNT"
		};

		public static string GetRootName(PredefinedKey rootKey, bool getShortName = false)
		{
			return rootKey switch
			{
				PredefinedKey.ClassesRoot => getShortName ? "HKCR" : "HKEY_CLASSES_ROOT",
				PredefinedKey.CurrentUser => getShortName ? "HKCU" : "HKEY_CURRENT_USER",
				PredefinedKey.LocalMachine => getShortName ? "HKLM" : "HKEY_LOCAL_MACHINE",
				PredefinedKey.Users => getShortName ? "HKU" : "HKEY_USERS",
				PredefinedKey.PerformanceData => getShortName ? "HKPD" : "HKEY_PERFORMANCE_DATA",
				PredefinedKey.CurrentConfig => getShortName ? "HKCC" : "HKEY_CURRENT_CONFIG",
				PredefinedKey.PerformanceText => getShortName ? "HKPT" : "HKEY_PERFORMANCE_TEXT",
				PredefinedKey.PerformanceNlsText => getShortName ? "HKPNT" : "HKEY_PERFORMANCE_NLS_TEXT",
				_ => throw new ArgumentException("Not a valid root key.", nameof(rootKey))
			};
		}

		public static PredefinedKey ResolveRootKey(string rootName)
		{
			if (rootName.StartsWith("0x") && uint.TryParse(rootName.AsSpan(2), System.Globalization.NumberStyles.HexNumber, null, out var ul)
	|| uint.TryParse(rootName, out ul)
	)
			{
				if ((ul >= (uint)PredefinedKey.ClassesRoot && ul <= (uint)PredefinedKey.CurrentConfig)
					|| ul == (uint)PredefinedKey.PerformanceText
					|| ul == (uint)PredefinedKey.PerformanceNlsText)
				{
					return (PredefinedKey)ul;
				}
				else
				{
					throw new ArgumentException($"Invalid root hex value\n", nameof(rootName));
				}

			}

			PredefinedKey rootKey = rootName.ToUpper() switch
			{
				"HKCR" or "HKEY_CLASSES_ROOT" => PredefinedKey.ClassesRoot,
				"HKCU" or "HKEY_CURRENT_USER" => PredefinedKey.CurrentUser,
				"HKLM" or "HKEY_LOCAL_MACHINE" => PredefinedKey.LocalMachine,
				"HKPD" or "HKEY_PERFORMANCE_DATA" => PredefinedKey.PerformanceData,
				"HKU" or "HKEY_USERS" => PredefinedKey.Users,
				"HKCC" or "HKEY_CURRENT_CONFIG" => PredefinedKey.CurrentConfig,
				"HKPT" or "HKEY_PERFORMANCE_TEXT" => PredefinedKey.PerformanceText,
				"HKPNT" or "HKEY_PERFORMANCE_NLS_TEXT" => PredefinedKey.PerformanceNlsText,
				_ => throw new ArgumentException("Invalid root key name\n", nameof(rootName))
			};
			return rootKey;
		}

		public static bool TryResolveRootKey(string rootName, out PredefinedKey rootKey)
		{
			try
			{
				rootKey = ResolveRootKey(rootName);
				return true;
			}
			catch (ArgumentException)
			{
				rootKey = PredefinedKey.Invalid;
				return false;
			}
		}

	}
}
