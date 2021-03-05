using System;

namespace Titanis.Msrpc.Mslsar
{
	public class SecretInfo
	{
		public byte[] CurrentValue { get; set; }
		public DateTime CurrentValueSetTime { get; set; }
		public byte[] OldValue { get; set; }
		public DateTime OldValueSetTime { get; set; }
	}
}