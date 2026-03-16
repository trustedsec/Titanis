using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Provides file system access.
	/// </summary>
	public interface IFileAccess
	{
		string[] GetFiles(string directory, string searchPattern);
		/// <summary>
		/// Converts a relative file path to an absolute path.
		/// </summary>
		/// <param name="filePath">File path</param>
		/// <returns>An absolute file path</returns>
		/// <remarks>
		/// If <paramref name="filePath"/> is already absolute, it is returned unchanged.  This enables this method to be called on a path multiple times.
		/// </remarks>
		string ResolveFsPath(string filePath);

		/// <summary>
		/// Reads bytes from a file.
		/// </summary>
		/// <param name="fileName">Name of file</param>
		/// <returns>A <see cref="byte"/> array with the contents of <paramref name="fileName"/></returns>
		byte[] ReadAllBytesFrom(string fileName);

		/// <summary>
		/// Determines whether a file exists.
		/// </summary>
		/// <param name="path">File name to check</param>
		/// <returns><see langword="true"/> if <paramref name="path"/> names an existing file; otherwise, <see langword="false"/></returns>
		bool FileExists(string path);
	}
}
