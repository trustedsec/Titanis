using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace Titanis.Msrpc.Msscmr.Security
{
	public class ServiceAccessRule : AccessRule
	{
		public ServiceAccessRule(
			IdentityReference identity,
			int accessMask,
			bool isInherited,
			InheritanceFlags inheritanceFlags,
			PropagationFlags propagationFlags,
			AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}
	}
}
