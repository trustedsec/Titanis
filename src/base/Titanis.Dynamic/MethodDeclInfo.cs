using System;

namespace Titanis.Dynamic
{
	class MethodDeclInfo
	{
		public MethodDeclInfo(Type[] paramTypes, Type[][] reqmods, Type[][] optmods)
		{
			this.paramTypes = paramTypes;
			this.reqmods = reqmods;
			this.optmods = optmods;
		}

		public Type[] paramTypes { get; }
		public Type[][] reqmods { get; }
		public Type[][] optmods { get; }
	}
}