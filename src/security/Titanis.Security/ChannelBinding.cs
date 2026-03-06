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
		public abstract byte[] GetBytes();
	}
}
