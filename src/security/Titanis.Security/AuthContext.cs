using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Titanis.Asn1;

namespace Titanis.Security
{
	[Flags]
	public enum MessageSignOptions
	{
		None = 0,

		/// <summary>
		/// The sign request is for 
		/// </summary>
		SpnegoMechList = 1
	}

	public abstract class AuthContext : IDisposable
	{
		/// <summary>
		/// Provides a unique identifier to correlate this authentication context with a higher-level operation.
		/// </summary>
		public Guid CorrelationId { get; set; } = Guid.NewGuid();
		/// <summary>
		/// Gets a value indicating whether authentication is complete.
		/// </summary>
		public abstract bool IsComplete { get; }
		/// <summary>
		/// Gets the token to send to the remote party.
		/// </summary>
		public abstract ReadOnlySpan<byte> Token { get; }

		/// <summary>
		/// Gets the mechanism ID that identifies this authentication mechanism within a GSS API context.
		/// </summary>
		public virtual Asn1Oid MechOid => Asn1Oid.Empty;
		/// <summary>
		/// Gets the number of legs required for authentication.
		/// </summary>
		/// <value>Number of legs, or <c>0</c> if undetermined.</value>
		public virtual int Legs { get; } = 0;

		/// <summary>
		/// Gets a value indicating whether the cortext represents an anonymous authentication request.
		/// </summary>
		public virtual bool IsAnonymous => false;

		/// <summary>
		/// Gets a value indicating whether thin context has a session key.
		/// </summary>
		public abstract bool HasSessionKey { get; }
		/// <summary>
		/// Gets the size of the session key.
		/// </summary>
		public abstract int SessionKeySize { get; }
		/// <summary>
		/// Gets the session key, if available.
		/// </summary>
		/// <returns>The bytes of the session key</returns>
		/// <exception cref="InvalidOperationException">No session key is available.</exception>
		/// <seealso cref="HasSessionKey"/>
		/// <seealso cref="SessionKeySize"/>
		public ReadOnlySpan<byte> GetSessionKey()
		{
			if (!this.HasSessionKey)
				throw new InvalidOperationException(Messages.Security_NoSessionKey);

			return this.GetSessionKeyImpl();
		}
		/// <summary>
		/// When implemented in a derived class, gets the session key.
		/// </summary>
		/// <returns>Bytes composing the session key</returns>
		protected abstract ReadOnlySpan<byte> GetSessionKeyImpl();

		/// <summary>
		/// Increments the sequence number expected on the next message received.
		/// </summary>
		public abstract void IncrementRecvSeqNbr();

		#region Signing
		/// <summary>
		/// Gets the size of a signing token.
		/// </summary>
		/// <remarks>
		/// If the context does not signing, this property returns <c>0</c>.
		/// </remarks>
		public virtual int SignTokenSize => 0;

		#region Capabilities

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

		private bool _isUsed;
		protected void VerifyNew()
		{
			if (this._isUsed)
				throw new InvalidOperationException("Cannot change required capabilities once the context has begun negotiation.");
		}
		private protected void MarkUsed()
		{
			this._isUsed = true;
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
		/// <seealso cref="AuthContext.RequiredCapabilities"/>
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
		#endregion

		/// <summary>
		/// Signs a message.
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="macBuffer">Buffer for MAC</param>
		/// <param name="options"><see cref="MessageSignOptions"/> value</param>
		public void SignMessage(
			Span<byte> message,
			Span<byte> macBuffer,
			MessageSignOptions options
			)
			=> this.SignMessage(new MessageSignParams(macBuffer, SecBufferList.Create(SecBuffer.Integrity(message))), options);

		/// <summary>
		/// Signs a message.
		/// </summary>
		/// <param name="signParams">Message sign parameters</param>
		public abstract void SignMessage(
			in MessageSignParams signParams,
			MessageSignOptions options
			);

		/// <summary>
		/// Verifies a signed message.
		/// </summary>
		/// <param name="message">Message to sign</param>
		/// <param name="mac">Message authentication code</param>
		/// <param name="options"><see cref="MessageSignOptions"/> value</param>
		public void VerifyMessage(Span<byte> message, Span<byte> mac, MessageSignOptions options)
			=> this.VerifyMessage(new MessageVerifyParams(mac, SecBufferList.Create(SecBuffer.Integrity(message))), options);

		/// <summary>
		/// Verifies a signed message.
		/// </summary>
		/// <param name="verifyParams">Message sign parameters</param>
		/// <param name="options"><see cref="MessageSignOptions"/> value</param>
		public abstract void VerifyMessage(
			in MessageVerifyParams verifyParams,
			MessageSignOptions options
			);
		#endregion
		#region Sealing
		/// <summary>
		/// Gets the required buffer sizes for wrapping a message.
		/// </summary>
		/// <param name="options">Options</param>
		/// <param name="requiredHeaderSize">Size required for the header buffer</param>
		/// <param name="requiredTrailerSize">Size required for the trailer buffer</param>
		/// <remarks>
		/// By calling this overload, the caller indicates that it can accommodate separate buffers for a header and a trailer.
		/// </remarks>
		public virtual void GetWrapBufferSizes(WrapOptions options, out int requiredHeaderSize, out int requiredTrailerSize)
		{
			requiredHeaderSize = this.GetWrapTokenSize(options);
			requiredTrailerSize = 0;
		}
		/// <summary>
		/// Gets the sizes of the header and trailer from a message buffer.
		/// </summary>
		/// <param name="messageBuffer">Buffer containing received message</param>
		/// <param name="options">Options</param>
		/// <param name="headerSize">Size of the header</param>
		/// <param name="trailerSize">Size of the trailer</param>
		/// <remarks>
		/// This method is for the case where the message and token are combined into the same buffer (e.g. LDAP, SASL).
		/// </remarks>
		public virtual void GetUnwrapBufferSizes(ReadOnlySpan<byte> messageBuffer, WrapOptions options, out int headerSize, out int trailerSize)
		{
			this.GetWrapBufferSizes(options, out headerSize, out trailerSize);
		}
		/// <summary>
		/// Gets the required token buffer size for wrapping a message.
		/// </summary>
		/// <param name="options">Options</param>
		/// <returns>Size required for the token buffer in bytes</returns>
		/// <remarks>
		/// By calling this overload, the caller indicates that it can only accommodate a single token buffer.  In other words, it cannot accommodate a separate buffer for both a header and a trailer.
		/// </remarks>
		public abstract int GetWrapTokenSize(WrapOptions options);

		/// <summary>
		/// Seals a message.
		/// </summary>
		/// <param name="sealParams">Message sealing parameters</param>
		public abstract void SealMessage(
			in MessageSealParams sealParams
			);

		/// <summary>
		/// Unseals a message.
		/// </summary>
		/// <param name="unsealParams">Message unsealing parameters</param>
		public abstract void UnsealMessage(
			in MessageSealParams unsealParams
			);
		#endregion





		protected virtual void OnDisposing(bool disposing)
		{

		}

		#region Dispose pattern
		private bool isDisposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!isDisposed)
			{
				if (disposing)
				{
					this.OnDisposing(disposing);
				}

				isDisposed = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}

	[Flags]
	public enum WrapOptions
	{
		None = 0,
		/// <summary>
		/// Message buffer requests confidentiality
		/// </summary>
		Confidentiality = 1,
		/// <summary>
		/// Wrap message for RPC
		/// </summary>
		Rpc = 0x10,
	}
}
