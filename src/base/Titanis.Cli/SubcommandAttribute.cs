using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class SubcommandAttribute : Attribute
	{
		public SubcommandAttribute(string name, Type handlerType)
		{
			this.Name = name;
			this.CommandType = handlerType;
		}

		public string Name { get; }
		public Type CommandType { get; }
	}
}
