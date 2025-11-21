using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Ldap
{
	public class BinaryString
	{
		public BinaryString(byte[] bytes, Exception? decodeError = null)
		{
			ArgumentNullException.ThrowIfNull(bytes);
			Bytes = bytes;
			DecodeException = decodeError;
		}

		public byte[] Bytes { get; }
		public Exception? DecodeException { get; }

		public override string ToString() => BinaryHelper.ToHexString(this.Bytes);
	}
}
