using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{
	public interface ICryptHandler
	{
		void TransformBlock(ReadOnlySpan<byte> source, Span<byte> target);

		int BlockSizeBytes { get; }
	}
}
