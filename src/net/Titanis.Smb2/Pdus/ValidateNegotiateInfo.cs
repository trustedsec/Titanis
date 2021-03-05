using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Smb2.Pdus
{
	class ValidateNegotiateInfo
	{
		public ValidateNegotiateInfo()
		{

		}

		internal ValidateNegotiateInfoFixed hdr;
		internal Smb2Dialect[] dialects;

		public void WriteTo(ByteWriter writer)
		{
			this.hdr.dialectCount = (ushort)this.dialects.Length;
			writer.Write(ref this.hdr);
			writer.WriteUInt16SpanLE(MemoryMarshal.Cast<Smb2Dialect, ushort>(this.dialects));
		}
	}
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct ValidateNegotiateInfoFixed
	{
		internal static unsafe int StructSize => sizeof(ValidateNegotiateInfoFixed);

		internal Smb2Capabilities capabilities;
		internal Guid guid;
		internal Smb2SecurityMode securityMode;
		internal ushort dialectCount;
	}
}
