using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Titanis.Dynamic
{
	public enum AccessModifier
	{
		Private = 0,
		PrivateProtected,
		Internal,
		ProtectedInternal,
		Protected,
		Public,
	}

	public static class ReflExtensions
	{
		public static AccessModifier GetAccessModifier(this MethodInfo method)
		{
			if (method is null) throw new ArgumentNullException(nameof(method));

			var attr = method.Attributes;
			if (0 != (attr & MethodAttributes.Private))
				return AccessModifier.Private;
			else if (0 != (attr & MethodAttributes.Assembly))
				return AccessModifier.Internal;
			else if (0 != (attr & MethodAttributes.FamANDAssem))
				return AccessModifier.PrivateProtected;
			else if (0 != (attr & MethodAttributes.Family))
				return AccessModifier.Protected;
			else if (0 != (attr & MethodAttributes.FamORAssem))
				return AccessModifier.ProtectedInternal;
			else
				return AccessModifier.Public;
		}
	}
}
