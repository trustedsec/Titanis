using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace Titanis.Msrpc.Msscmr.Security
{
	public class ServiceSecurity : ObjectSecurity
	{
		internal ServiceSecurity(CommonSecurityDescriptor sd)
			: base(sd)
		{

		}

		public override Type AccessRightType => typeof(ServiceAccess);

		public override Type AccessRuleType => typeof(ServiceAccessRule);

		public override Type AuditRuleType => typeof(ServiceAuditRule);

		public override AccessRule AccessRuleFactory(
			IdentityReference identityReference,
			int accessMask,
			bool isInherited,
			InheritanceFlags inheritanceFlags,
			PropagationFlags propagationFlags,
			AccessControlType type)
		{
			return new ServiceAccessRule(
				identityReference,
				accessMask,
				isInherited,
				inheritanceFlags,
				propagationFlags,
				type
				);
		}

		public override AuditRule AuditRuleFactory(
			IdentityReference identityReference,
			int accessMask,
			bool isInherited,
			InheritanceFlags inheritanceFlags,
			PropagationFlags propagationFlags,
			AuditFlags flags)
		{
			return new ServiceAuditRule(
				identityReference,
				accessMask,
				isInherited,
				inheritanceFlags,
				propagationFlags,
				flags
				);
		}

		protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			throw new NotImplementedException();
		}

		protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			throw new NotImplementedException();
		}
	}
}
