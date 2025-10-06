using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Socks
{

	[Serializable]
	public class Socks5Exception : IOException, IHaveErrorCode
	{
		public Socks5Exception(Socks5ResultCode errorCode, string? message = null)
			: base(message ?? GetErrorMessage(errorCode))
		{
			this.ErrorCode = errorCode;
		}

		private static string? GetErrorMessage(Socks5ResultCode errorCode)
		{
			return errorCode switch
			{
				//Socks5ResultCode.Success => throw new NotImplementedException(),
				Socks5ResultCode.GeneralServerFailure => "General SOCKS server failure",
				Socks5ResultCode.ConnectionNotAllowedByRuleset => "Connection not allowed by ruleset",
				Socks5ResultCode.NetworkUnreachable => "Network unreachable",
				Socks5ResultCode.HostUnreachable => "Host unreachable",
				Socks5ResultCode.ConnectionRefused => "Connection refused",
				Socks5ResultCode.TtlExpired => "TTL expired",
				Socks5ResultCode.CommandNotSupported => "Command not supported",
				Socks5ResultCode.AddressTypeNotSupported => "Address type not supported",
				_ => $"Unknown SOCKS 5 error: {errorCode}"
			};
		}

		public Socks5Exception(string message, Exception inner) : base(message, inner) { }
		protected Socks5Exception(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			this.ErrorCode = (Socks5ResultCode)info.GetInt32(nameof(ErrorCode));
		}

		public Socks5ResultCode ErrorCode { get; }

		int IHaveErrorCode.ErrorCode => (int)this.ErrorCode;
	}
}
