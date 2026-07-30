using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Titanis.Winterop.Registry
{



	/// <summary>
	/// Represents a Windows registry path.
	/// </summary>
	[TypeConverter(typeof(RegistryPathConverter))]
	public sealed class RegistryPath : IEquatable<RegistryPath>
	{
		/// <summary>
		/// Represents a registry path, including the predefined hive and the key path.
		/// </summary>
		/// <param name="root">The root registry key</param>
		/// <param name="keyPath">The path to the registry key within the specified hive. If null, an empty string is used.</param>
		public RegistryPath(PredefinedKey root, string? keyPath)
		{
			Root = root;
			KeyPath = keyPath ?? string.Empty;
		}

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
			return $"{RegistryRootKey.GetRootName(Root)}{((KeyPath.Length != 0) ? '\\' + KeyPath : String.Empty)}";
		}

		public bool IsRootPath => (KeyPath == string.Empty);

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

			return (this.IsRootPath) ? new RegistryPath(this.Root, subkeyName) : new RegistryPath(this.Root, $"{this.KeyPath}\\{subkeyName}");
		}

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


				int i = Array.IndexOf(RegistryRootKey.RootNames, rootName.ToUpper());
				if (i < 0)
					i = Array.IndexOf(RegistryRootKey.RootShortNames, rootName.ToUpper());
				if (i >= 0)
				{
					uint rootval = (uint)((uint)PredefinedKey.ClassesRoot + i);
					if ((rootval) > ((uint)PredefinedKey.CurrentConfig))
					{
						root = (rootval == (uint)PredefinedKey.CurrentConfig + 1) ? PredefinedKey.PerformanceText : PredefinedKey.PerformanceNlsText;
					}
					else
					{
						root = (PredefinedKey)rootval;
					}
					return true;
				}
			}

			root = 0;
			return false;
		}

		/// <summary>
		/// Parses a registry path into its components.
		/// </summary>
		/// <param name="path">Registry path to parse, beginning with its hive</param>
		/// <returns>A <see cref="RegistryPath"/> describing the components of the registry path</returns>
		/// <exception cref="ArgumentException"><paramref name="path"/>is <see langword="null"/> or an invalid registry path.</exception>
		public static RegistryPath Parse(string path)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));
			var validRootNames = RegistryRootKey.RootNames.Concat(RegistryRootKey.RootShortNames);
			var rootName = validRootNames.FirstOrDefault(name => path.StartsWith(name, StringComparison.OrdinalIgnoreCase));
			PredefinedKey root;
			if (rootName is null)
			{
				throw new ArgumentException($"Predefined key name cannot be null. Must be one of {string.Join(' ', validRootNames)}");
			}
			else if (!TryResolveRootName(rootName, out root))
			{
				throw new ArgumentException($"Predefined key name is invalid. Must be one of {string.Join(' ', validRootNames)}");
			}
			if (path.Length == rootName.Length)
				return new RegistryPath(root, string.Empty);

			char separator = path[rootName.Length];
			var rgx =
				separator == '/' ? rgxPathSlash
				: separator == '\\' ? rgxPathBackslash
				: throw CreateBadPathException(nameof(path));

			var match = rgx.Match(path);
			if (!match.Success)
				throw CreateBadPathException(nameof(path));

			string keyPath = match.Groups["keypath"].Value;
			if (separator == '/')
				keyPath = keyPath.Replace('/', '\\');
			keyPath = keyPath.TrimEnd('\\');
			return new RegistryPath(root, keyPath);
		}

		/// <summary>
		/// Gets the subkey name from a registry path string.
		/// </summary>
		/// <param name="keyPath">Registry key path</param>
		/// <returns>The key name at the end of the path</returns>
		/// <remarks>
		/// This method is analogous to <see cref="Path.GetFileName(string?)"/>.
		/// The registry allows a slash (`/`) as part of a key name and
		/// does not treat it as a path separator.
		/// </remarks>
		public static string GetSubkeyNameFromPath(string keyPath)
		{
			int isep = keyPath.LastIndexOf('\\');
			return (isep > 0) ? keyPath.Substring(isep + 1) : keyPath;
		}

		/// <summary>
		/// Gets the subkey name from a registry path string.
		/// </summary>
		/// <param name="keyPath">Registry key path</param>
		/// <returns>The key name at the end of the path</returns>
		/// <remarks>
		/// This method is analogous to <see cref="Path.GetFileName(string?)"/>.
		/// The registry allows a slash (`/`) as part of a key name and
		/// does not treat it as a path separator.
		/// </remarks>
		public static string? GetParentKeyNameFromPath(string keyPath)
		{
			int isep = keyPath.LastIndexOf('\\');
			return (isep > 0) ? keyPath.Substring(0, isep) : null;
		}

		/// <summary>
		/// Combines two registry path strings.
		/// </summary>
		/// <param name="path1">First path to combine</param>
		/// <param name="path2">Next path to combine</param>
		/// <returns>A path combining <paramref name="path1"/> and <paramref name="path2"/>.</returns>
		public static string? Combine(string path1, string? path2)
		{
			if (string.IsNullOrEmpty(path1))
				return path2;
			if (string.IsNullOrEmpty(path2))
				return path1;

			if (!path1.EndsWith('\\'))
				path1 += '\\';

			path1 += path2;

			return path1;
		}

		private static Exception CreateBadPathException(string argName)
		{
			return new ArgumentException(@"Registry key path must be formatted as a UNC path of the form <root>\<path> or <root>/<path>", argName);
		}

		public bool Equals(RegistryPath other)
		{
			return (other.KeyPath == this.KeyPath && other.Root == this.Root);
		}

		public override int GetHashCode() => this.ToString().GetHashCode();

		public static bool operator ==(RegistryPath r1, RegistryPath r2)
		{
			return r1.Equals(r2);
		}

		public static bool operator !=(RegistryPath r1, RegistryPath r2)
		{
			return !(r1 == r2);
		}

		private static readonly Regex rgxPathSlash = new Regex(@"^(?<root>\w+)(/(?<keypath>.*))?$");
		private static readonly Regex rgxPathBackslash = new Regex(@"^(?<root>\w+)(\\(?<keypath>.*))?$");
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
