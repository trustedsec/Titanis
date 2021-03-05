using System.Text;
using Titanis.Cli;

namespace Titanis
{
	public sealed class FileAttributeFormatter : IOutputFormatter
	{
		public static string? FormatValue(object? value)
		{
			if (value is Winterop.FileAttributes attrs)
				return AttrString(attrs);
			else
				throw new ArgumentOutOfRangeException("Value is not the correct type.", nameof(value));
		}
		public string? FormatValue(object? value, string format, OutputField field, OutputStyle outputStyle)
			=> FormatValue(value);

		private static string AttrString(Winterop.FileAttributes attrs)
		{
			StringBuilder sb = new StringBuilder(13);
			sb
				.AppendIf(attrs, Winterop.FileAttributes.Directory, 'D', '-')
				.AppendIf(attrs, Winterop.FileAttributes.ReadOnly, 'R', '-')
				.AppendIf(attrs, Winterop.FileAttributes.Archive, 'A', '-')
				.AppendIf(attrs, Winterop.FileAttributes.System, 'S', '-')
				.AppendIf(attrs, Winterop.FileAttributes.Hidden, 'H', '-')
				.AppendIf(attrs, Winterop.FileAttributes.Offline, 'O', '-')
				.AppendIf(attrs, Winterop.FileAttributes.NotContentIndexed, 'i', '-')
				.AppendIf(attrs, Winterop.FileAttributes.NoScrubData, 'x', '-')
				.AppendIf(attrs, Winterop.FileAttributes.IntegrityStream, 'V', '-')
				.AppendIf(attrs, Winterop.FileAttributes.Encrypted, 'E', '-')
				.AppendIf(attrs, Winterop.FileAttributes.Temporary, 'T', '-')
				.AppendIf(attrs, Winterop.FileAttributes.ReparsePoint, 'M', '-')
				.AppendIf(attrs, Winterop.FileAttributes.SparseFile, 'F', '-')
				;
			return sb.ToString();
		}
	}
}