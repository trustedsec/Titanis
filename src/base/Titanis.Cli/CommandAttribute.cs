using System;

namespace Titanis.Cli
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class CommandAttribute : Attribute
	{
		public CommandAttribute()
		{
		}

		public string HelpText { get; set; }
	}
}