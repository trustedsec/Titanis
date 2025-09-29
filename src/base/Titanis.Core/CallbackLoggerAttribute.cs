using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Marks an object that implements a callback as a logger.
	/// </summary>
	/// <remarks>
	/// This attribute isn't used at runtime.  It assists in tracking the various callback loggers.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class CallbackLoggerAttribute : Attribute
	{
	}

	/// <summary>
	/// Marks a callback interface.
	/// </summary>
	/// <remarks>
	/// This attribute isn't used at runtime.  It assists in tracking the callback interfaces.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class CallbackAttribute : Attribute
	{
	}
}
