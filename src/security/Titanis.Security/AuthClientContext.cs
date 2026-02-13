using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Security
{
	/// <summary>
	/// Specifies security capabilities to negotiate during authentication.
	/// </summary>
	/// <seealso cref="AuthContext.RequiredCapabilities"/>
	/// <seealso cref="AuthContext.NegotiatedCapabilities"/>
	// [RFC 1509]
	[Flags]
	public enum SecurityCapabilities : uint
	{
		/// <summary>
		/// No options
		/// </summary>
		None = 0,

		#region RFC 1509
		/*
		 * These values match the constants declared in the SSPI headers
		 */

		Rfc1509Mask = Delegation | MutualAuthentication | ReplayDetection | SequenceDetection | Confidentiality,

		/// <summary>
		/// Delegate credentials to remote peer
		/// </summary>
		Delegation = 1,
		/// <summary>
		/// Request that remote peer authenticate itself
		/// </summary>
		MutualAuthentication = 2,
		/// <summary>
		/// Enable replay detection for signed or sealed messages
		/// </summary>
		ReplayDetection = 4,
		/// <summary>
		/// Enable detection of out-of-sequence signed or sealed messages
		/// </summary>
		SequenceDetection = 8,
		/// <summary>
		/// Request confidentiality services
		/// </summary>
		Confidentiality = 0x10,
		#endregion

		/// <summary>
		/// Request integrity services
		/// </summary>
		Integrity = 0x20,

		// [RFC 4757]
		DceStyle = 0x1000,
		IdentifyOnly = 0x2000,
		ExtendedError = 0x4000,
	}

	/// <summary>
	/// Represents the client side of an authentication context.
	/// </summary>
	public abstract class AuthClientContext : AuthContext
	{
		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		/// <remarks>
		/// If there is no user (e.g., guest or anonymous), implementations must return an empty string.
		/// </remarks>
		public abstract string UserName { get; }
		/// <summary>
		/// Gets the identifier to use for this authentication type within an RPC negotiation.
		/// </summary>
		public virtual byte RpcAuthType { get; } = 0;

		/// <summary>
		/// Gets or sets the target service principal name.
		/// </summary>
		/// <remarks>
		/// This property must be initialized before calling <see cref="Initialize()"/>
		/// </remarks>
		public abstract SecurityPrincipalName? TargetSpn { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the SPN was supplied by an untrusted source.
		/// </summary>
		public bool IsTargetSpnUntrusted { get; set; }
		/// <summary>
		/// Gets or sets the channel binding.
		/// </summary>
		public ChannelBinding? ChannelBinding { get; set; }

		/// <summary>
		/// Initializes this authentication context.
		/// </summary>
		/// <returns>The token to send to the other party</returns>
		public ReadOnlySpan<byte> Initialize()
		{
			this.MarkUsed();
			return this.InitializeImpl();
		}
		/// <summary>
		/// Initializes this authentication context.
		/// </summary>
		/// <returns>The token to send to the other party</returns>
		protected abstract ReadOnlySpan<byte> InitializeImpl();
		/// <summary>
		/// Initializes this authentication context.
		/// </summary>
		/// <param name="token">Token received from other party</param>
		/// <returns>The token to send to the other party</returns>
		public ReadOnlySpan<byte> Initialize(ReadOnlySpan<byte> token)
		{
			this.MarkUsed();
			return (token.Length == 0)
				? this.Initialize()
				: this.InitializeWithToken(token);
		}

		/// <summary>
		/// When implemented in a derived class, continues the negotiation.
		/// </summary>
		/// <param name="token">Token received from the other party</param>
		/// <returns>The token to send to the other party</returns>
		protected abstract ReadOnlySpan<byte> InitializeWithToken(ReadOnlySpan<byte> token);

		/// <summary>
		/// Gets the implementing security context.
		/// </summary>
		/// <returns>The inner mechanism security context, if wrapped; otherwise, <see langword="this"/></returns>
		/// <remarks>
		/// This allows the caller to interact directly with the underlying security
		/// context.
		/// </remarks>
		public virtual AuthClientContext GetMechContext() => this;
	}
}
