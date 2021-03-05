using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc.WireProtocol
{
	public enum PduType : byte
	{
		Request = 0,
		Ping = 1,
		Response = 2,
		Fault = 3,
		Working = 4,
		NoCall = 5,
		Reject = 6,
		Ack = 7,
		CLCancel = 8,
		Fack = 9,
		CancelAck = 10,
		Bind = 11,
		BindAck = 12,
		BindNak = 13,
		AlterContext = 14,
		AlterContextResp = 15,
		Shutdown = 17,
		CoCancel = 18,
		Orphaned = 19,

		Auth3 = 16,
	}
}
