using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Security
{
	/// <summary>
	/// Specifies the level of authentication provided by a secure channel.
	/// </summary>
	/// <seealso cref="ISecureChannel.AuthLevel"/>
	public enum AuthLevel
	{
		None = 0,
		Authenticated,
		Integrity,
		Privacy
	}

	/// <summary>
	/// Exposes functionality implemented by a secure channel.
	/// </summary>
	public interface ISecureChannel
	{
		/// <summary>
		/// Gets a <see cref="AuthLevel"/> value specifying the level of authentication provided by this channel.
		/// </summary>
		AuthLevel AuthLevel { get; }
		/// <summary>
		/// Gets a value indicating whether this channel has negotiated a session key
		/// </summary>
		bool HasSessionKey { get; }
		/// <summary>
		/// Gets the session key, if available.
		/// </summary>
		/// <returns>A byte array containing the session key, if available;
		/// otherwise, <see langword="null"/></returns>
		byte[]? GetSessionKey();
	}
}
