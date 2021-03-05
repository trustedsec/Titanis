using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Provides an example of using a command class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class ExampleAttribute : Attribute
	{
		public ExampleAttribute(string caption, string commandLine, string? explanation = null)
		{
			this.Caption = caption;
			this.CommandLine = commandLine;
			this.Explanation = explanation;
		}

		public string Caption { get; }
		public string CommandLine { get; }
		public string? Explanation { get; }
	}
}
