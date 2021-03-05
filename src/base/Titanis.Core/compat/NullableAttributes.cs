using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Diagnostics.CodeAnalysis
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	internal class AllowNullAttribute : Attribute
	{
	}
}
