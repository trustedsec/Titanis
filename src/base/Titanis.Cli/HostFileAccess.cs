using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Provides access to the host file system.
	/// </summary>
	public class HostFileAccess : IFileAccess
	{
		/// <inheritdoc/>
		public string ResolveFsPath(FileSpec path)
		{
			if (path is null) throw new ArgumentNullException(nameof(path));
			if (path.IsResolved)
				return path.FileName;

			return Path.GetFullPath(path.FileName);
		}

		/// <inheritdoc/>
		public string[] GetFiles(string directory, string searchPattern)
		{
			return Directory.GetFiles(directory, searchPattern);
		}

		/// <inheritdoc/>
		public byte[] ReadAllBytesFrom(FileSpec fileName)
		{
			var path = this.ResolveFsPath(fileName);
			return File.ReadAllBytes(path);
		}

		/// <inheritdoc/>
		public string ReadAllTextFrom(FileSpec fileName)
		{
			var path = this.ResolveFsPath(fileName);
			return File.ReadAllText(path);
		}

		/// <inheritdoc/>
		public bool FileExists(FileSpec path) => File.Exists(this.ResolveFsPath(path));
		/// <inheritdoc/>
		public bool DirectoryExists(FileSpec path) => Directory.Exists(this.ResolveFsPath(path));

		/// <inheritdoc/>
		public void CreateDirectory(FileSpec path) => Directory.CreateDirectory(this.ResolveFsPath(path));

		/// <inheritdoc/>
		public Stream OpenRead(FileSpec path) => File.OpenRead(this.ResolveFsPath(path));

		/// <inheritdoc/>
		public void WriteAllTextTo(FileSpec fileName, string contents)
		{
			var path = this.ResolveFsPath(fileName);
			File.WriteAllText(path, contents);
		}

		/// <inheritdoc/>
		public void WriteAllBytesTo(FileSpec fileName, byte[] contents)
		{
			var path = this.ResolveFsPath(fileName);
			File.WriteAllBytes(path, contents);
		}

		/// <inheritdoc/>
		public IEnumerable<string> ReadLinesFrom(FileSpec fileName)
		{
			var path = this.ResolveFsPath(fileName);
			return File.ReadLines(path);
		}
	}
}
