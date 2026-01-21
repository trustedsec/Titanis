using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Msrrp
{
	/// <summary>
	/// Performs operations on strings as registry paths.
	/// </summary>
	public static class RegistryPath
	{
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
	}
}
