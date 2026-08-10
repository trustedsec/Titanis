using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Titanis.Winterop.Security
{
	public record struct SidAndAttributes(SecurityIdentifier sid, SidAttributes attributes);
	public class AccessToken
	{
		public AccessToken(
			SecurityIdentifier principal,
			ImmutableArray<SidAndAttributes> groups,
			ImmutableArray<string> privileges
			)
		{
			if (principal is null) throw new ArgumentNullException(nameof(principal));
			this.Principal = principal;
			this.Groups = groups;
			Privileges = privileges;
		}

		public SecurityIdentifier Principal { get; }
		public ImmutableArray<SidAndAttributes> Groups { get; }
		public ImmutableArray<string> Privileges { get; }

		// [MS-DTYP] § 2.5.3.1.1 SidInToken
		public bool SidInToken(SecurityIdentifier sid, SecurityIdentifier? selfSubstitute)
		{
			if (sid is null) throw new ArgumentNullException(nameof(sid));
			if (selfSubstitute != null && sid.AsWellKnownSid() == WellKnownSid.PrincipalSelf)
			{
				sid = selfSubstitute;
			}

			if (this.Principal == sid)
				return true;
			foreach (var group in this.Groups)
			{
				if (group.sid == sid)
					return true;
			}
			return false;
		}

		public enum AuthzResult
		{
			Unknown = -1,
			False = 0,
			True = 1,
		}

		// [MS-DTYP] § 2.5.3.1.5 EvaluateAceCondition
		internal bool? AuthzBasepEvaluateAceCondition(
			AccessControlList? sacl,
			ReadOnlySpan<byte> applicationData
			)
		{
			throw new NotImplementedException();
		}

		// [MS-DTYP] § 2.5.3.2 Access Check Algorithm Pseudocode
		public bool EvaluateTokenAgainstDescriptor(
			SecurityDescriptor sd,
			uint requestedAccess,
			ObjectAccessType[]? objectTree,
			SecurityIdentifier? principalSelf,
			out uint grantedAccess,
			out uint deniedAccess,
			out uint auditGrantedAccess,
			out uint auditDeniedAccess
			)
		{
			throw new NotImplementedException();
			if (sd is null) throw new ArgumentNullException(nameof(sd));

			grantedAccess = 0;
			deniedAccess = 0;

			bool requestMax = (0 != (requestedAccess & (uint)FileAccessRights.MaxAllowed));
			requestedAccess &= ~(uint)FileAccessRights.MaxAllowed;

			if (requestMax || (0 != (requestedAccess & (uint)FileAccessRights.AccessSystemSecurity)))
			{
				if (this.Privileges.Contains(nameof(Privilege.SeSecurityPrivilege)))
					grantedAccess |= (uint)FileAccessRights.AccessSystemSecurity;
				else
					deniedAccess |= (uint)FileAccessRights.AccessSystemSecurity;
			}

			if (requestMax || (0 != (requestedAccess & (uint)FileAccessRights.WriteOwner)))
			{
				if (this.Privileges.Contains(nameof(Privilege.SeTakeOwnershipPrivilege)))
					grantedAccess |= (uint)FileAccessRights.WriteOwner;
			}

			if (sd.Owner != null && this.SidInToken(sd.Owner, principalSelf))
			{
				// TODO: [MS-DTYP]: IF DACL does not contain ACEs from object owner THEN
				// Does this mean ANY ACE?
				grantedAccess |= (uint)(FileAccessRights.WriteDac | FileAccessRights.ReadControl);
			}

			// TODO: What to do if DACL is not present?
			if (sd.Dacl != null)
			{
				foreach (var ace in sd.Dacl.Entries)
				{
					if (0 == (ace.AceFlags & AccessControlEntryFlags.InheritOnly))
					{
						if (this.SidInToken(ace.Trustee, principalSelf))
						{
							switch ((ace.AceType, ace))
							{
								case (AccessControlEntryType.AccessAllowed or AccessControlEntryType.AccessDenied, SimpleAce simp):
									if (ace.AceType == AccessControlEntryType.AccessAllowed)
										grantedAccess |= simp.AccessMask;
									else
										deniedAccess |= simp.AccessMask;

									if (objectTree != null)
									{
										for (int i = 0; i < objectTree.Length; i++)
										{
											ref var obj = ref objectTree[i];
											if (ace.AceType == AccessControlEntryType.AccessAllowed)
												obj.GrantedAccess |= simp.AccessMask;
											else
												obj.DeniedAccess |= simp.AccessMask;
										}
									}
									break;
								case (AccessControlEntryType.AccessAllowedObject or AccessControlEntryType.AccessDeniedObject, ObjectAce objace):
									if (objectTree != null)
									{
										for (int i = 0; i < objectTree.Length; i++)
										{
											ref var objtype = ref objectTree[i];

										}
									}
									break;
							}
						}
					}
				}
			}
		}
	}

	public record struct ObjectAccessType(Guid ObjectType, int Remaining, int Level)
	{
		public uint GrantedAccess { get; set; }
		public uint DeniedAccess { get; set; }
	}
}
