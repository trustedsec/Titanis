using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Titanis.Security
{
	/// <summary>
	/// Specifies security capabilities to negotiate during authentication.
	/// </summary>
	/// <seealso cref="AuthClientContext.RequiredCapabilities"/>
	/// <seealso cref="AuthClientContext.NegotiatedCapabilities"/>
	// [RFC 1509]
	public enum SecurityCapabilities : uint
	{
		/// <summary>
		/// No options
		/// </summary>
		None = 0,
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
		/// Creates a new <see cref="AuthClientContext"/> with the same credentials that may be used
		/// for another authentication operation.
		/// </summary>
		/// <returns>The duplicate <see cref="AuthClientContext"/>.</returns>
		public AuthClientContext Duplicate()
		{
			var dup = this.DuplicateImpl();
			CopyFieldsTo(dup);
			return dup;
		}

		protected void CopyFieldsTo(AuthClientContext dup)
		{
			dup.TargetSpn = this.TargetSpn;
			dup.IsTargetSpnUntrusted = this.IsTargetSpnUntrusted;
			dup.RequiredCapabilities = this.RequiredCapabilities;
		}

		/// <summary>
		/// Creates a new <see cref="AuthClientContext"/> with the same credentials that may be used
		/// for another authentication operation.
		/// </summary>
		/// <returns>The duplicate <see cref="AuthClientContext"/>.</returns>
		protected abstract AuthClientContext DuplicateImpl();

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
		/// Gets the mechanism ID that identifies this authentication mechanism within a GSS API context.
		/// </summary>
		public virtual Oid? MechOid => null;

		public bool IsDceRpcStyle => (0 != (this._requiredCaps & SecurityCapabilities.DceStyle));
		public bool IsMutualAuthRequired => (0 != (this._requiredCaps & SecurityCapabilities.MutualAuthentication));

		private SecurityCapabilities _requiredCaps;
		/// <summary>
		/// Gets or sets a <see cref="SecurityCapabilities"/> that specifies
		/// capabilities that must be negotiated.
		/// </summary>
		/// <remarks>
		/// If the capabilities are not available, the negotiation fails.
		/// </remarks>
		public SecurityCapabilities RequiredCapabilities
		{
			get => this._requiredCaps;
			set
			{
				VerifyNew();
				this._requiredCaps = value;
			}
		}

		private bool _isInitialized;
		protected void VerifyNew()
		{
			if (this._isInitialized)
				throw new InvalidOperationException("Cannot change required capabilities once the context has begun negotiation.");
		}

		/// <summary>
		/// Gets a <see cref="SecurityCapabilities"/> that specifies
		/// which capabilities were negotiated.
		/// </summary>
		public abstract SecurityCapabilities NegotiatedCapabilities { get; }

		/// <summary>
		/// Gets a <see cref="SecurityCapabilities"/> indicating which capabilities the context can provide.
		/// </summary>
		/// This value is determined by the protocol and provider implementation, not by the negotiation.
		/// <seealso cref="RequiredCapabilities"/>
		/// <seealso cref="NegotiatedCapabilities"/>
		public abstract SecurityCapabilities SupportedCapabilities { get; }

		/// <summary>
		/// Gets a value indicating whether this context supports signing.
		/// </summary>
		public bool SupportsSigning => 0 != (this.NegotiatedCapabilities & SecurityCapabilities.Integrity);
		/// <summary>
		/// Gets a value indicating whether this context supports sealing.
		/// </summary>
		public bool SupportsEncryption => 0 != (this.NegotiatedCapabilities & SecurityCapabilities.Confidentiality);

		/// <summary>
		/// Gets or sets the target service principal name.
		/// </summary>
		/// <remarks>
		/// This property must be initialized before calling <see cref="Initialize()"/>
		/// </remarks>
		public abstract ServicePrincipalName? TargetSpn { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the SPN was supplied by an untrusted source.
		/// </summary>
		public bool IsTargetSpnUntrusted { get; set; }

		/// <summary>
		/// Initializes this authentication context.
		/// </summary>
		/// <returns>The token to send to the other party</returns>
		public ReadOnlySpan<byte> Initialize()
		{
			this._isInitialized = true;
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
			this._isInitialized = true;
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
	}
}
