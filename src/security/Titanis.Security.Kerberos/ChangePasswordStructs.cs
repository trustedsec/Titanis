using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{
	enum ChangepwVersion : ushort
	{
		Request = 1,
		Win2kResetPasswordVersionNumber = 0xff80,
	}

	// [RFC 3244]
	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct ChangepwMessage
	{
		public ushort MessageLength { get; set; }
		[DefaultValue(1)]
		public ChangepwVersion ProtocolVersionNumber { get; set; }
		public ushort ApreqLength { get; set; }
		[PduArraySize(nameof(ApreqLength))]
		public byte[] Apreqdata { get; set; }

		private int PrivLength => this.MessageLength - 6 - this.ApreqLength;
		[PduArraySize(nameof(PrivLength))]
		public byte[] PrivMessage { get; set; }
	}

	// [RFC 3244]
	public enum ChangepwStatus : ushort
	{
		Success = 0,
		Malformed = 1,
		HardError = 2,
		AuthError = 3,
		SoftError = 4,
		AccessDenied = 5,
		BadVersion = 6,
		InitialFlagNeeded = 7,
	}

	// [RFC 3244]
	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct ChangepwReplyMessage
	{
		internal const ushort ChangepwVersionNumber = 1;

		public ushort MessageLength { get; set; }
		[DefaultValue(1)]
		public ushort ProtocolVersionNumber { get; set; }
		public ushort ApreqLength { get; set; }
		[PduArraySize(nameof(ApreqLength))]
		public byte[] Apreqdata { get; set; }

		private int PrivLength => this.MessageLength - 6 - this.ApreqLength;
		[PduArraySize(nameof(PrivLength))]
		public byte[] PrivMessage { get; set; }
	}

	/// <summary>
	/// Thrown when an error occurs during a password change operation.
	/// </summary>
	[Serializable]
	public sealed class KrbChangePasswordException : Exception
	{
		public KrbChangePasswordException() { }
		public KrbChangePasswordException(ChangepwStatus status, string? message) : base(message ?? GetErrorMessage(status))
		{
			Status = status;
		}
		protected KrbChangePasswordException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			this.Status = (ChangepwStatus)info.GetInt16(nameof(Status));
		}

		public ChangepwStatus Status { get; }

		private static string GetErrorMessage(ChangepwStatus status) =>
			status switch
			{
				ChangepwStatus.Success => "Request succeeded.",
				ChangepwStatus.Malformed => "Request failed due to being malformed.",
				ChangepwStatus.HardError => "Request failed due to a hard error in processing the request.",
				ChangepwStatus.AuthError => "Request failed due to an error in authentication processing.",
				ChangepwStatus.SoftError => "Request failed due to a soft error in processing the request.",
				ChangepwStatus.AccessDenied => "Requestor not authorized.",
				ChangepwStatus.BadVersion => "Protocol version unsupported.",
				ChangepwStatus.InitialFlagNeeded => "Initial flag required.",
				_ => $"Unknown error returned by Kerberos changepw service: {(int)status}"
			};
		public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue(nameof(Status), (short)this.Status);
		}
	}
}
