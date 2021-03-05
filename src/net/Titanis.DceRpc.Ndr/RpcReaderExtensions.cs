using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.DceRpc
{
	internal static class RpcReaderExtensions
	{

		internal static unsafe SyntaxId ReadSyntaxId(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(SyntaxId.StructSize))
			{
				SyntaxId* pStruc = (SyntaxId*)(pBuf);
				return *pStruc;
			}
		}

		internal static unsafe void WriteSyntaxId(this ByteWriter writer, in SyntaxId syntax)
		{
			fixed (byte* pByte = writer.Consume(SyntaxId.StructSize))
			{
				SyntaxId* pStruc = (SyntaxId*)(pByte);
				*pStruc = syntax;
			}
		}
	}
}
