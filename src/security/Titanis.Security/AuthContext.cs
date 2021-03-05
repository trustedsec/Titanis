using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
		/// Gets a value indicating whether authentication is complete.
		/// </summary>
		public abstract bool IsComplete { get; }
		/// <summary>
		/// Gets the token to send to the remote party.
		/// </summary>
		public abstract ReadOnlySpan<byte> Token { get; }

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
		/// <returns></returns>
		protected abstract ReadOnlySpan<byte> GetSessionKeyImpl();

		#region Signing
		/// <summary>
		/// Gets the size of a signing token.
		/// </summary>
		/// <remarks>
		/// If the context does not signing, this property returns <c>0</c>.
		/// </remarks>
		public virtual int SignTokenSize => 0;

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
		/// Gets the size required for the sealing token header.
		/// </summary>
		/// <remarks>
		/// If the context does not support sealing, this property returns <c>0</c>.
		/// </remarks>
		public virtual int SealHeaderSize => 0;
		/// <summary>
		/// Gets the size required for the sealing token trailer.
		/// </summary>
		/// <remarks>
		/// If the context does not support sealing, this property returns <c>0</c>.
		/// </remarks>
		public virtual int SealTrailerSize => 0;

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
}
