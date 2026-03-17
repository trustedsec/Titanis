using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;

namespace Titanis;

public class TestFileAccess : IFileAccess
{
	/// <summary>
	/// Prefix for test files
	/// </summary>
	/// <remarks>
	/// This string deliberately begins with an invalid name so that if it is passed to normal file system functions, they will fail.
	/// </remarks>
	public const string TestFsPrefix = ":testdata";

	public TestFileAccess(Assembly resourceAssembly, string rootNamespace)
	{
		this._resourceAssembly = resourceAssembly;
		this._resNamePrefix = rootNamespace + ".TestData.";
	}

	private readonly Assembly _resourceAssembly;
	//private readonly string _rootNS;
	private readonly string _resNamePrefix;

	public string ResolveFsPath(string filePath)
	{
		if (!filePath.StartsWith(TestFsPrefix))
			filePath = Path.Combine(TestFsPrefix, filePath);

		return filePath;
	}

	public string[] GetFiles(string directory, string searchPattern)
	{
		if (directory != TestFsPrefix)
			throw new ArgumentException($"Directory must be the test FS prefix.", nameof(directory));

		WildcardPattern pattern = new WildcardPattern(searchPattern);

		var resNames = this._resourceAssembly.GetManifestResourceNames();

		List<string> found = new List<string>(resNames.Length);
		foreach (var name_ in resNames)
		{
			if (!name_.StartsWith(this._resNamePrefix))
				continue;

			string name = name_.Substring(this._resNamePrefix.Length);

			if (pattern.Matches(name))
				found.Add(Path.Combine(TestFsPrefix, name));
		}


		foreach (var name in this._writtenFiles.Keys)
		{
			if (pattern.Matches(name))
				found.Add(name);
		}

		return found.ToArray();
	}

	public byte[] ReadAllBytesFrom(string fileName)
	{
		string resName = this.FileNameToResourceName(fileName);
		var resStream = this._resourceAssembly.GetManifestResourceStream(resName);

		if (resStream is null)
			throw new FileNotFoundException($"No test file found with name: {fileName}");

		byte[] bytes = new byte[resStream.Length];
		resStream.Read(bytes);
		resStream.Close();
		return bytes;
	}

	private string FileNameToResourceName(string fileName)
	{
		if (fileName.StartsWith(TestFsPrefix))
			fileName = fileName.Substring(TestFsPrefix.Length + 1);

		var resName = this._resNamePrefix + fileName;
		return resName;
	}

	public bool FileExists(string path)
	{
		var resName = this.FileNameToResourceName(path);
		var info = this._resourceAssembly.GetManifestResourceInfo(resName);
		return info != null;
	}

	private Dictionary<string, byte[]> _writtenFiles = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);

	public void WriteAllBytesTo(string fileName, byte[] contents)
	{
		fileName = this.ResolveFsPath(fileName);
		this._writtenFiles[fileName] = contents;
	}
}
