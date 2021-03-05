using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace Titanis.Msrpc.Msscmr.Security
{
	public class ServiceAuditRule : AuditRule
	{
		public ServiceAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, auditFlags)
		{
		}
	}
}
