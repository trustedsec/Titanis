using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Titanis
{
	/// <summary>
	/// Represents a UNC path.
	/// </summary>
	[TypeConverter(typeof(UncPathConverter))]
	public sealed class UncPath
	{
		public UncPath(string serverName, int port, string? shareName, string? shareRelativePath)
		{
			if (string.IsNullOrEmpty(serverName)) throw new ArgumentException($"'{nameof(serverName)}' cannot be null or empty.", nameof(serverName));

			ServerName = serverName;
			Port = port;
			ShareName = shareName;

			ShareRelativePath = shareRelativePath?.Replace('/', '\\') ?? string.Empty;
		}
		public UncPath(string serverName, string? shareName, string? shareRelativePath)
			: this(serverName, 445, shareName, shareRelativePath ?? string.Empty)
		{
		}

		/// <summary>
		/// Gets the host name.
		/// </summary>
		public string ServerName { get; }
		/// <summary>
		/// Gets the remote port.
		/// </summary>
		public int Port { get; }
		/// <summary>
		/// Gets the share name.
		/// </summary>
		public string? ShareName { get; }
		/// <summary>
		/// Gets the path relative to the share, without a leading backslash.
		/// </summary>
		public string ShareRelativePath { get; }

		/// <summary>
		/// Gets a value indicating whether the UNC path specifies a path within the share.
		/// </summary>
		/// <remarks>
		/// A UNC path without a share-relative part may identify a server, a share, or the root of a share.
		/// </remarks>
		public bool HasShareRelativePath => !string.IsNullOrEmpty(this.ShareRelativePath);

		/// <summary>
		/// Gets the <see cref="UncPath"/> of the share containing this path.
		/// </summary>
		/// <returns></returns>
		public UncPath GetShare() => new UncPath(ServerName, Port, ShareName, null);

		/// <summary>
		/// Checks whether another <see cref="UncPath"/> matches the server and share name of this path.
		/// </summary>
		/// <param name="other">Other <see cref="UncPath"/> to check</param>
		/// <returns><see langword="true"/> if the server and share of <paramref name="other"/> match; otherwise, <see langword="false"/></returns>
		public bool MatchesServerAndShare(UncPath? other)
		{
			if (other is null) return false;
			bool matches = this.ServerName.Equals(other.ServerName, StringComparison.OrdinalIgnoreCase) && string.Equals(this.ShareName, other.ShareName, StringComparison.OrdinalIgnoreCase)
				&& (other.Port == this.Port);
			return matches;
		}

		// [MS-DFSC] § 2.2.1
		/// <summary>
		/// Gets the share path (without the file path) normalized for a DFS referral request.
		/// </summary>
		internal string DfsSharePath
			=> string.IsNullOrEmpty(ShareName) ? $@"\{ServerName}"
			: $@"\{ServerName}\{ShareName}";
		// [MS-DFSC] § 2.2.1
		/// <summary>
		/// Gets the path normalized for a DFS referral request.
		/// </summary>
		public string PathForDfsReferral
			=> string.IsNullOrEmpty(ShareName) ? $@"\{ServerName}"
			: string.IsNullOrEmpty(ShareRelativePath) ? $@"\{ServerName}\{ShareName}"
			: $@"\{ServerName}\{ShareName}\{ShareRelativePath}";

		/// <summary>
		/// Gets the path to the share of the form \\server\share without the path.
		/// </summary>
		/// <remarks>
		/// This path does not include the port or any other nonstandard features supported by
		/// <see cref="UncPath"/>.  It is suitable for sending to an SMB TREE_CONNECT.
		/// </remarks>
		public string SharePath => $@"\\{ServerName}\{ShareName}";

		/// <summary>
		/// Gets the path to the server.
		/// </summary>
		public UncPath ServerUncPath => new UncPath(ServerName, Port, null, null);

		/// <summary>
		/// Gets the path to the share.
		/// </summary>
		public UncPath ShareUncPath => new UncPath(ServerName, Port, ShareName, null);

		/// <inheritdoc/>
		public sealed override string ToString()
			=> string.IsNullOrEmpty(ShareName) ? $@"\\{ServerName}"
			: string.IsNullOrEmpty(ShareRelativePath) ? $@"\\{ServerName}\{ShareName}"
			: $@"\\{ServerName}\{ShareName}\{ShareRelativePath}";

		/// <summary>
		/// Gets the UNC path of the parent directory.
		/// </summary>
		/// <returns>A <see cref="UncPath"/> of the parent directory.</returns>
		/// <remarks>
		/// If this <see cref="UncPath"/> identifies the root of a share,
		/// then this method returns the current <see cref="UncPath"/>.
		/// </remarks>
		public UncPath GetDirectoryPath()
			=> !this.HasShareRelativePath ? this : new UncPath(ServerName, Port, ShareName, GetDirectoryName());

		private static void SplitPath(string? path, out string directoryName, out string fileName)
		{
			if (string.IsNullOrEmpty(path))
			{
				directoryName = string.Empty;
				fileName = string.Empty;
				return;
			}

			int isep = path.LastIndexOf('\\');
			if (isep < 0)
			{
				directoryName = string.Empty;
				fileName = path;
			}
			else if (isep == 0)
			{
				directoryName = "\\";
				fileName = path.Substring(1);
			}
			else if (path.EndsWith("\\"))
			{
				directoryName = path.TrimEnd('\\');
				fileName = string.Empty;
			}
			else
			{
				directoryName = path.Substring(0, isep);
				fileName = path.Substring(isep + 1);
			}
		}

		/// <summary>
		/// Gets the directory name portion of the path.
		/// </summary>
		/// <returns>The directory name without the file.</returns>
		public string GetDirectoryName()
		{
			SplitPath(this.ShareRelativePath, out var dirName, out _);
			return dirName;
		}

		/// <summary>
		/// Gets the file name portion of the path.
		/// </summary>
		/// <returns>The file name without the directory.</returns>
		public string GetFileName()
		{
			if (!this.HasShareRelativePath)
				return string.Empty;

			SplitPath(this.ShareRelativePath, out _, out var fileName);
			return fileName;
		}

		private static readonly Regex uncPattern = new Regex(@"^\\(\\)?(?<h>(\w|\.|\-)*)(:(?<p>\d+))?(\\(?<s>[^\\]*)(\\(?<pa>.*))?)?$");

		/// <summary>
		/// Attempts to parse a UNC path into its components.
		/// </summary>
		/// <param name="text">UNC path to parse, beginning with a double backslash</param>
		/// <param name="uncPath">The parsed path</param>
		/// <returns><see langword="true"/> if <paramref name="text"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
		public static bool TryParse(string? text, out UncPath? uncPath)
		{
			if (string.IsNullOrEmpty(text))
			{
				uncPath = null;
				return false;
			}

			var m = uncPattern.Match(text);
			if (!m.Success)
			{
				uncPath = null;
				return false;
			}
			else
			{
				uncPath = PathFromMath(m);
				return true;
			}
		}
		/// <summary>
		/// Parses a UNC path into its components.
		/// </summary>
		/// <param name="text">UNC path to parse, beginning with a double backslash</param>
		/// <returns>A <see cref="UncPath"/> describing the components of the UNC path.</returns>
		/// <exception cref="ArgumentException"><paramref name="text"/> is <see langword="null"/> or an invalid UNC path.</exception>
		public static UncPath Parse(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));

			text = text.Replace('/', '\\');

			// Parse the UNC path into ints components
			var m = uncPattern.Match(text);
			if (!m.Success)
				throw new ArgumentException(@"Invalid UNC path.  The UNC path must be of the form \\<server>[:<port>][\<share>[\<path>]]", nameof(text));
			else
			{
				return PathFromMath(m);
			}
		}

		public const int SmbTcpPort = 445;

		private static UncPath PathFromMath(Match m)
		{
			var portstr = m.Groups["p"].Value;

			return new UncPath(
				m.Groups["h"].Value,
				string.IsNullOrEmpty(portstr) ? SmbTcpPort : int.Parse(portstr),
				m.Groups["s"].Value,
				m.Groups["pa"].Value);
		}

		public static string Combine(string? path1, string? path2)
		{
			if (string.IsNullOrEmpty(path1))
				return path2;
			if (string.IsNullOrEmpty(path2))
				return path1;

			if (path2.StartsWith("/") || path2.StartsWith(@"\"))
				throw new ArgumentException($"Cannot append an absolute path.", nameof(path2));

			int extra = path1.EndsWith(@"\") ? 0 : 1;
			StringBuilder sb = new StringBuilder(path1.Length + extra + path2.Length);
			sb.Append(path1);
			if (extra > 0)
				sb.Append(@"\");
			sb.Append(path2);
			return sb.ToString();
		}

		public UncPath Append(params string[] fileNameParts)
		{
			string str = this.ToString();
			foreach (var part in fileNameParts)
			{
				str = Combine(str, part);
			}
			return Parse(str);
		}

		public UncPath WithServerName(string serverName) => new UncPath(serverName, this.Port, this.ShareName, this.ShareRelativePath);
	}

	/// <summary>
	/// Provides type conversion between <see cref="UncPath"/> and <see cref="string"/>.
	/// </summary>
	public class UncPathConverter : TypeConverter
	{
		/// <inheritdoc/>
		/// <remarks>
		/// This implementation only supports conversion from <see cref="string"/>.
		/// </remarks>
		public sealed override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <inheritdoc/>
		/// <remarks>
		/// This implementation only supports conversion from <see cref="string"/>.
		/// </remarks>
		public sealed override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		{
			if (value is string str)
				return UncPath.Parse(str);

			return base.ConvertFrom(context, culture, value);
		}
	}
}
