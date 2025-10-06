using System;

namespace Titanis.Cli
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class CommandAttribute : Attribute
	{
		public CommandAttribute()
		{
		}

		// TODO: Complete deprecation
		[Obsolete("Use DescriptionAttribute instead.", false)]
		public string? HelpText { get; set; }
	}
}