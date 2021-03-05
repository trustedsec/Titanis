using System;

namespace Titanis.Dynamic
{
	public class MethodDeclInfo
	{
		public Type[] paramTypes { get; set; }
		public Type[][] reqmods { get; set; }
		public Type[][] optmods { get; set; }
	}
}