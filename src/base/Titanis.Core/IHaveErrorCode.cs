using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Exposes an error code.
	/// </summary>
	public interface IHaveErrorCode
	{
		int ErrorCode { get; }
	}
}
