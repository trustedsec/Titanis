using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Specifies the type of RPC pointer.
	/// </summary>
	// [C706] § 4.2.20.1 - Pointers / Syntax
	public enum RpcPointerType
	{
		Default = 0,
		Unique,
		Ptr,
		Ref,

		// TODO: Separate this to a separate enum
		DisableConsistencyCheck,
	}

}
