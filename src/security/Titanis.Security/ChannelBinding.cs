using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security
{
	/// <summary>
	/// Represents a channel binding
	/// </summary>
	public abstract class ChannelBinding
	{
		/// <summary>
		/// Gets the name of the channel binding type.
		/// </summary>
		public abstract string Name { get; }
		/// <summary>
		/// Gets the unhashed bytes to pass to the authentication context
		/// </summary>
		/// <returns></returns>
		public byte[] GetBytes()
		{
			var buf = new byte[this.RequiredLength];
			int cb = this.GetBytes(buf);
			if (cb < buf.Length)
			{
				// Shouldn't happen
				Array.Resize(ref buf, cb);
			}
			return buf;
		}

		public abstract int GetBytes(Span<byte> buffer);

		public abstract int RequiredLength { get; }
	}
}
