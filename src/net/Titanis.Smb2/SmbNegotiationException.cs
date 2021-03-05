using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	/// <summary>
	/// Specifies the features that failed the negotiation.
	/// </summary>
	[Flags]
	public enum SmbNegotiationFailureType
	{
		/// <summary>
		/// Unspecified failure
		/// </summary>
		Unspecified = 0,
		/// <summary>
		/// No agreement on signing algorithms
		/// </summary>
		Sign,
		/// <summary>
		/// No agreement on encryption algorithms
		/// </summary>
		Encrypt,
		/// <summary>
		/// No agreement on dialect
		/// </summary>
		Dialect,
		/// <summary>
		/// Missing preauthentication salt
		/// </summary>
		MissingPreauthSalt,
	}

	/// <summary>
	/// Thrown when an SMB2 connection fails the feature negotiation.
	/// </summary>
	[Serializable]
	public class SmbNegotiationException : Exception, IHaveErrorCode
	{
		public SmbNegotiationException(SmbNegotiationFailureType failureType)
			: base($"Negotiation failed because the remote endpoint did not support one or more required features: {failureType}")
		{
			this.FailureType = failureType;
		}

		/// <summary>
		/// Initializes a new <see cref="SmbNegotiationException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected SmbNegotiationException(
		  SerializationInfo info,
		  StreamingContext context) : base(info, context)
		{
			this.FailureType = (SmbNegotiationFailureType)info.GetInt32(nameof(FailureType));
		}

		/// <summary>
		/// Gets a <see cref="SmbNegotiationFailureType"/> that specifies the features that failed the negotiation.
		/// </summary>
		public SmbNegotiationFailureType FailureType { get; }

		int IHaveErrorCode.ErrorCode => (int)this.FailureType;

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(FailureType), (int)this.FailureType);
			base.GetObjectData(info, context);
		}
	}
}
