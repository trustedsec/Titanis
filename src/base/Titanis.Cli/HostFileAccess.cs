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
		public string ResolveFsPath(string path)
		{
			return Path.GetFullPath(path);
		}

		public string[] GetFiles(string directory, string searchPattern)
		{
			return Directory.GetFiles(directory, searchPattern);
		}

		public byte[] ReadAllBytesFrom(string fileName)
		{
			fileName = this.ResolveFsPath(fileName);
			return File.ReadAllBytes(fileName);
		}

		public bool FileExists(string path) => File.Exists(this.ResolveFsPath(path));

		public void WriteAllBytesTo(string fileName, byte[] contents)
		{
			fileName = this.ResolveFsPath(fileName);
			File.WriteAllBytes(fileName, contents);
		}
	}
}
