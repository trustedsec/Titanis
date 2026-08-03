using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class SubcommandAttribute : Attribute
	{
		public SubcommandAttribute(string name, Type handlerType)
		{
			if (name != name.Trim())
				throw new Exception();
			this.Name = name;
			this.CommandType = handlerType;
		}

		public sealed override object TypeId => this.Name;
		public string Name { get; }
		public Type CommandType { get; }
	}
}
