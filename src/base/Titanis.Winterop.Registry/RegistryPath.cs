using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Titanis.Winterop.Registry
{
	/// <summary>
	/// Specifies a predefined root key in the Windows registry.
	/// </summary>
	public enum PredefinedKey : uint
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        HKEY_CLASSES_ROOT = 0x80000000,
        HKEY_CURRENT_USER,
		HKEY_LOCAL_MACHINE,
		HKEY_USERS,
		HKEY_CURRENT_CONFIG,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}

	/// <summary>
	/// Represents a Windows registry path.
	/// </summary>
	[TypeConverter(typeof(RegistryPathConverter))]
	public sealed class RegistryPath
	{
		/// <summary>
		/// Represents a registry path, including the predefined hive and the key path.
		/// </summary>
		/// <param name="server">Name of server (if remote)</param>
		/// <param name="root">The root registry key</param>
		/// <param name="keyPath">The path to the registry key within the specified hive. If null, an empty string is used.</param>
		public RegistryPath(string? server, PredefinedKey root, string? keyPath)
		{
			ServerName = server;
			Root = root;
			KeyPath = keyPath ?? string.Empty;
		}

		/// <summary>
		/// Name of server containing the registry (if remote).
		/// </summary>
		public string? ServerName { get; }

		/// <summary>
		/// Gets the predefined key representing the hive in the registry.
		/// </summary>
		public PredefinedKey Root { get; }

		/// <summary>
		/// Gets the path of the key within the hive.
		/// </summary>
		public string KeyPath { get; }

		/// <inheritdoc/>
		public override string ToString()
		{
			return $"\\\\{ServerName}\\{Root}\\{KeyPath}";
		}

		/// <summary>
		/// Gets the final key component of <see cref="KeyPath"/>.
		/// </summary>
		public string KeyName
		{
			get
			{
				int isep = KeyPath.LastIndexOf('\\');
				return isep > 0 ? KeyPath.Substring(isep + 1)
					: KeyPath;
			}
		}

		public RegistryPath Append(string subkeyName)
		{
			ArgumentException.ThrowIfNullOrEmpty(subkeyName);

			var subkeyPath = new RegistryPath(this.ServerName, this.Root, $"{this.KeyPath}\\{subkeyName}");
			return subkeyPath;
		}

		#region Predefined roots
		private static readonly string[] predefinedRootNames = new string[]
		{
			"HKEY_CLASSES_ROOT",
			"HKEY_CURRENT_USER",
			"HKEY_LOCAL_MACHINE",
			"HKEY_USERS",
			"HKEY_CURRENT_CONFIG",
		};
		private static readonly string[] predefinedRootShortNames = new string[]
		{
			"HKCR",
			"HKCU",
			"HKLM",
			"HKU",
			"HKCC",
		};

		/// <summary>
		/// Attempts to resolve a root key name to a <see cref="PredefinedKey"/> value.
		/// </summary>
		/// <param name="rootName">Name of root</param>
		/// <param name="root"><see cref="PredefinedKey"/> value</param>
		/// <returns><see langword="true"/> if <paramref name="rootName"/> was resolved; otherwise, <see langword="false"/></returns>
		public static bool TryResolveRootName(string? rootName, out PredefinedKey root)
		{
			if (rootName != null)
			{
				if (rootName.StartsWith("0x") && uint.TryParse(rootName.AsSpan(2), System.Globalization.NumberStyles.HexNumber, null, out var ul)
					|| uint.TryParse(rootName, out ul)
					)
				{
					root = (PredefinedKey)ul;
					return true;
				}

				int i = Array.IndexOf(predefinedRootNames, rootName.ToUpper());
				if (i >= 0)
				{
					root = (PredefinedKey)((uint)PredefinedKey.HKEY_CLASSES_ROOT + i);
					return true;
				}

				i = Array.IndexOf(predefinedRootShortNames, rootName.ToUpper());
				if (i >= 0)
				{
					root = (PredefinedKey)((uint)PredefinedKey.HKEY_CLASSES_ROOT + i);
					return true;
				}
			}

			root = 0;
			return false;
		}
		#endregion

		/// <summary>
		/// Parses a registry path into its components.
		/// </summary>
		/// <param name="path">Registry path to parse, beginning with its hive</param>
		/// <returns>A <see cref="RegistryPath"/> describing the components of the registry path</returns>
		/// <exception cref="ArgumentException"><paramref name="path"/>is <see langword="null"/> or an invalid registry path.</exception>
		public static RegistryPath Parse(string path)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));

			var rgx =
				path.StartsWith("//") ? rgxPathSlash
				: path.StartsWith("\\\\") ? rgxPathBackslash
				: throw CreateBadPathException(nameof(path));

			var match = rgx.Match(path);
			if (!match.Success)
				throw CreateBadPathException(nameof(path));

			var serverName = match.Groups["server"].Value;
			var rootName = match.Groups["root"].Value;
			if (!TryResolveRootName(rootName, out var root))
				throw new ArgumentException($"Key name '{rootName}' is not a supported predefined root key.  Consult the documentation for a list of valid names.", nameof(path));

			string keyPath = match.Groups["keypath"].Value;
			if (path[0] == '/')
				keyPath = keyPath.Replace('/', '\\');

			return new RegistryPath(serverName, root, keyPath);
		}

		private static Exception CreateBadPathException(string argName)
		{
			return new ArgumentException(@"Registry key path must be formatted as a UNC path of the form \\<serverName>\<root>\<path> or //<serverName>/<root>/<path>", argName);
		}

		private static readonly Regex rgxPathSlash = new Regex(@"^//(?<server>(\w|-|\.)*)/(?<root>\w+)(/(?<keypath>.*))?$");
		private static readonly Regex rgxPathBackslash = new Regex(@"^\\\\(?<server>(\w|-|\.)*)\\(?<root>\w+)(\\(?<keypath>.*))?$");
	}

	/// <summary>
	/// Provides type conversion between <see cref="RegistryPath"/> and <see cref="string"/>.
	/// </summary>
	public class RegistryPathConverter : TypeConverter
	{
		/// <inheritdoc/>
		/// <remarks>
		/// This implementation only supports conversion from <see cref="string"/>.
		/// </remarks>
		public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		/// <inheritdoc/>
		/// <remarks>
		/// This implementation only supports conversion from <see cref="string"/>.
		/// </remarks>
		public override object? ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
		{
			if (value is string s)
				return RegistryPath.Parse(s);
			return base.ConvertFrom(context, culture, value);
		}
	}
}
