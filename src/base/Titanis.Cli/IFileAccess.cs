using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Provides file system access.
	/// </summary>
	public interface IFileAccess
	{
		/// <summary>
		/// Gets a list of files in a directory that match a search pattern.
		/// </summary>
		/// <param name="directory">Path of directory to search</param>
		/// <param name="searchPattern">Search pattern</param>
		/// <returns>A list of file names relative to <paramref name="directory"/></returns>
		string[] GetFiles(string directory, string searchPattern);
		/// <summary>
		/// Converts a relative file path to an absolute path.
		/// </summary>
		/// <param name="filePath">File path, relative to current context</param>
		/// <returns>An absolute file path</returns>
		/// <remarks>
		/// If <paramref name="filePath"/> is already absolute, it is returned unchanged.  This enables this method to be called on a path multiple times.
		/// </remarks>
		string ResolveFsPath(FileSpec filePath);

		/// <summary>
		/// Reads bytes from a file.
		/// </summary>
		/// <param name="fileName">Name of file</param>
		/// <returns>A <see cref="byte"/> array with the contents of <paramref name="fileName"/></returns>
		byte[] ReadAllBytesFrom(FileSpec fileName);

		/// <summary>
		/// Reads text from a file.
		/// </summary>
		/// <param name="fileName">Name of file</param>
		/// <returns>The text read from <paramref name="fileName"/></returns>
		string ReadAllTextFrom(FileSpec fileName);

		/// <summary>
		/// Reads lines of text from a file.
		/// </summary>
		/// <param name="fileName">Name of file</param>
		/// <returns>An <see cref="IEnumerable{T}"/> returning the lines from the file</returns>
		IEnumerable<string> ReadLinesFrom(FileSpec fileName);

		/// <summary>
		/// Determines whether a file exists.
		/// </summary>
		/// <param name="path">File name to check</param>
		/// <returns><see langword="true"/> if <paramref name="path"/> names an existing file; otherwise, <see langword="false"/></returns>
		bool FileExists(FileSpec path);

		/// <summary>
		/// Opens a file for read access.
		/// </summary>
		/// <param name="path">File name to open</param>
		Stream OpenRead(FileSpec path);

		/// <summary>
		/// Writes text to a file.
		/// </summary>
		/// <param name="fileName">Name of file to write</param>
		/// <param name="contents">Text to write</param>
		/// <remarks>
		/// If the file exists, it is replaced.
		/// </remarks>
		void WriteAllTextTo(FileSpec fileName, string contents);

		/// <summary>
		/// Writes bytes to a file.
		/// </summary>
		/// <param name="fileName">Name of file to write</param>
		/// <param name="contents">Bytes to write</param>
		/// <remarks>
		/// If the file exists, it is replaced.
		/// </remarks>
		void WriteAllBytesTo(FileSpec fileName, byte[] contents);
	}
}
