using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.PduStruct;

namespace Titanis.Ssh
{
	[PduStruct]
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct SshString
	{
		internal uint length;

		[PduString(System.Runtime.InteropServices.CharSet.Ansi, nameof(length))]
		internal string str;
	}
}
