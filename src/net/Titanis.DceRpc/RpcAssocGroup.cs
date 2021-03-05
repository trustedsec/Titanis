using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	sealed class RpcAssocGroup
	{
		public uint GroupId { get; }

		internal RpcAssocGroup(uint groupId)
		{
			this.GroupId = groupId;
		}
	}
}
