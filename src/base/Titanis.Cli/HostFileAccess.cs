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
		public string ResolveFsPath(FileSpec path)
		{
			if (path is null) throw new ArgumentNullException(nameof(path));
			if (path.IsResolved)
				return path.FileName;

			return Path.GetFullPath(path.FileName);
		}

		public string[] GetFiles(string directory, string searchPattern)
		{
			return Directory.GetFiles(directory, searchPattern);
		}

		public byte[] ReadAllBytesFrom(FileSpec fileName)
		{
			var path = this.ResolveFsPath(fileName);
			return File.ReadAllBytes(path);
		}

		public string ReadAllTextFrom(FileSpec fileName)
		{
			var path = this.ResolveFsPath(fileName);
			return File.ReadAllText(path);
		}

		public bool FileExists(FileSpec path) => File.Exists(this.ResolveFsPath(path));

		public Stream OpenRead(FileSpec path) => File.OpenRead(this.ResolveFsPath(path));

		public void WriteAllTextTo(FileSpec fileName, string contents)
		{
			var path = this.ResolveFsPath(fileName);
			File.WriteAllText(path, contents);
		}

		public void WriteAllBytesTo(FileSpec fileName, byte[] contents)
		{
			var path = this.ResolveFsPath(fileName);
			File.WriteAllBytes(path, contents);
		}

		public IEnumerable<string> ReadLinesFrom(FileSpec fileName)
		{
			var path = this.ResolveFsPath(fileName);
			return File.ReadLines(path);
		}
	}
}
