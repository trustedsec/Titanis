using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	public class FileSnapshotsInfo
	{
		internal FileSnapshotsInfo(int totalSnapshots, FileSnapshotInfo[] tokens)
		{
			this.TotalSnapshots = totalSnapshots;
			this.Snapshots = tokens;
		}

		public int TotalSnapshots { get; }
		public FileSnapshotInfo[] Snapshots { get; }
	}

	public struct FileSnapshotInfo
	{
		internal FileSnapshotInfo(string token, DateTime timestamp)
		{
			this.Token = token;
			this.Timestamp = timestamp;
		}

		private static readonly Regex rgxGmtToken = new Regex(@"^@GMT-(?<y>\d{4})\.(?<mo>\d\d)\.(?<d>\d\d)-(?<h>\d\d)\.(?<mi>\d\d)\.(?<s>\d\d)$");
		public static FileSnapshotInfo Parse(string token)
		{
			if (string.IsNullOrEmpty(token)) throw new ArgumentException($"'{nameof(token)}' cannot be null or empty.", nameof(token));
			var m = rgxGmtToken.Match(token);
			if (!m.Success)
				throw new ArgumentException("The string is not a valid @GMT token.", nameof(token));

			DateTime dt = new DateTime(
				int.Parse(m.Groups["y"].Value),
				int.Parse(m.Groups["mo"].Value),
				int.Parse(m.Groups["d"].Value),
				int.Parse(m.Groups["h"].Value),
				int.Parse(m.Groups["mi"].Value),
				int.Parse(m.Groups["s"].Value),
				DateTimeKind.Utc
				);

			return new FileSnapshotInfo(token, dt);
		}

		public string Token { get; }
		public DateTime Timestamp { get; }
	}
}
