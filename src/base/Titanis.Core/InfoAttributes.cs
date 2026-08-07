using System;
using System.Collections.Generic;
using System.Text;

// This exists in Titanis.Core to avoid dependencies on Titanis.Info for just the attributes

namespace Titanis.Info
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class PrimaryKeyAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class InfoKeyAttribute : Attribute
	{
	}
}
