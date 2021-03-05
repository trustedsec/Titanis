using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Titanis.Dynamic
{
	public struct StubGenerateParams
	{
		public TypeInfo BaseClass { get; set; }
		public Type[]? InterfaceTypes { get; set; }
		public string StubTypeName { get; set; }
	}
}
