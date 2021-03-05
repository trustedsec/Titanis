using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Titanis.IO
{
	public static class PathHelper
	{
		public static bool IsWildcard(char c)
		{
			return (c == '*' || c == '?');
		}
		public static bool IsPathSeparator(char c)
		{
			return (c == '/' || c == '\\');
		}

		public static bool TryParseSearchPattern(
			string path,
			out string directoryName,
			out string? searchPattern)
		{
			if (!string.IsNullOrEmpty(path))
			{
				bool hasWildcard = false;
				for (int i = path.Length - 1; i >= 0; i--)
				{
					char c = path[i];
					if (IsWildcard(c))
						hasWildcard = true;
					else if (IsPathSeparator(c))
					{
						if (hasWildcard)
						{
							directoryName = path.Substring(0, i);
							searchPattern = path.Substring(i + 1);
							return true;
						}
						break;
					}
				}

				if (hasWildcard)
				{
					directoryName = string.Empty;
					searchPattern = path;
					return true;
				}
			}

			directoryName = path;
			searchPattern = null;
			return false;
		}

		/// <summary>
		/// Searches a series of directories for a file.
		/// </summary>
		/// <param name="fileName">File name to search for</param>
		/// <param name="defaultExtension">Extension to add if <paramref name="fileName"/> doesn't have one</param>
		/// <param name="directories">Directories to search</param>
		/// <returns>The full path of <paramref name="fileName"/>, if found; otherwise, <c>null</c>.</returns>
		public static string? SearchPath(string fileName, string defaultExtension, IList<string> directories)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
			if (!fileName.Contains('.'))
				fileName += defaultExtension;
			return SearchPath(fileName, directories);
		}

		/// <summary>
		/// Searches a series of directories for a file.
		/// </summary>
		/// <param name="fileName">File name to search for</param>
		/// <param name="directories">Directories to search</param>
		/// <returns>The full path of <paramref name="fileName"/>, if found; otherwise, <c>null</c>.</returns>
		public static string? SearchPath(string fileName, IList<string> directories)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));

			if (Path.IsPathRooted(fileName))
				return fileName;

			foreach (var dir in directories)
			{
				string fullName = Path.Combine(dir, fileName);
				if (File.Exists(fullName))
					return fullName;
			}

			return null;
		}
	}
}
