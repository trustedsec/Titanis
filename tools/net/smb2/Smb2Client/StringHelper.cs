using System.Text;

namespace Titanis
{
	static class StringHelper
	{
		public static StringBuilder AppendIf(this StringBuilder sb, Winterop.FileAttributes attributes, Winterop.FileAttributes flag, char trueChar, char falseChar)
		{
			sb.Append((0 != (attributes & flag)) ? trueChar : falseChar);
			return sb;
		}
	}
}
