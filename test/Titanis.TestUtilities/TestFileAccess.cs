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

	public string ResolveFsPath(FileSpec filePath)
	{
		string path = filePath.FileName;
		if (!path.StartsWith(TestFsPrefix))
			path = Path.Combine(TestFsPrefix, path);

		return filePath.FileName;
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

	public byte[] ReadAllBytesFrom(FileSpec fileName)
	{
		string resName = this.FileNameToResourceName(this.ResolveFsPath(fileName));
		var resStream = this._resourceAssembly.GetManifestResourceStream(resName);

		if (resStream is null)
			throw new FileNotFoundException($"No test file found with name: {fileName}");

		byte[] bytes = new byte[resStream.Length];
		resStream.Read(bytes);
		resStream.Close();
		return bytes;
	}

	public string ReadAllTextFrom(FileSpec fileName)
	{
		StreamReader reader = this.OpenTextFile(this.ResolveFsPath(fileName));
		string text = reader.ReadToEnd();
		reader.Close();
		return text;
	}

	private StreamReader OpenTextFile(string fileName)
	{
		string resName = this.FileNameToResourceName(fileName);
		var resStream = this._resourceAssembly.GetManifestResourceStream(resName);

		if (resStream is null)
			throw new FileNotFoundException($"No test file found with name: {fileName}");

		StreamReader reader = new StreamReader(resStream);
		return reader;
	}

	public IEnumerable<string> ReadLinesFrom(FileSpec fileName)
	{
		StreamReader reader = this.OpenTextFile(this.ResolveFsPath(fileName));
		while (reader.Peek() >= 0)
		{
			yield return reader.ReadLine();
		}
	}

	private string FileNameToResourceName(string fileName)
	{
		if (fileName.StartsWith(TestFsPrefix))
			fileName = fileName.Substring(TestFsPrefix.Length + 1);

		var resName = this._resNamePrefix + fileName;
		return resName;
	}

	public bool FileExists(FileSpec path)
	{
		var resName = this.FileNameToResourceName(this.ResolveFsPath(path));
		var info = this._resourceAssembly.GetManifestResourceInfo(resName);
		return info != null;
	}

	public Stream OpenRead(FileSpec path) => throw new NotImplementedException();

	private Dictionary<string, byte[]> _writtenFiles = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);

	public void WriteAllTextTo(FileSpec fileName, string contents)
	{
		var path = this.ResolveFsPath(fileName);
		this._writtenFiles[path] = Encoding.UTF8.GetBytes(contents);
	}

	public void WriteAllBytesTo(FileSpec fileName, byte[] contents)
	{
		var path = this.ResolveFsPath(fileName);
		this._writtenFiles[path] = contents;
	}
}
