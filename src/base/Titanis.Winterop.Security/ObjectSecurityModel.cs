using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Titanis.Winterop.Security
{
	public abstract class ObjectSecurityModel
	{
		protected ObjectSecurityModel()
		{

		}

		public static bool IsGenericRight(uint accessBit)
		{
			return (accessBit == (accessBit & (uint)GenericAccessRights.Mask));
		}
		public static bool IsStandardRight(uint accessBit)
		{
			return (accessBit == (accessBit & (uint)StandardAccessRights.AllStandardRights));
		}

		public static ObjectSecurityModel File { get; } = new FileSecurityModel();
		public static ObjectSecurityModel Directory { get; } = new DirectorySecurityModel();
		public static ObjectSecurityModel RegistryKey { get; } = new RegistryKeySecurityModel();
		public static ObjectSecurityModel Scm { get; } = new ScmSecurityModel();
		public static ObjectSecurityModel Service { get; } = new ServiceSecurityModel();
		public static ObjectSecurityModel DirectoryObject { get; } = new DirectoryObjectSecurityModel();
		public static ObjectSecurityModel SamServer { get; } = new SamServerSecurityModel();
		public static ObjectSecurityModel SamDomain { get; } = new SamDomainSecurityModel();
		public static ObjectSecurityModel SamGroup { get; } = new SamGroupSecurityModel();
		public static ObjectSecurityModel SamAlias { get; } = new SamAliasSecurityModel();
		public static ObjectSecurityModel SamUser { get; } = new SamUserSecurityModel();
		// TODO: Process, Window station, Desktop

		public static ObjectSecurityModel? GetModelFor(SecurityObjectType objectType) =>
			objectType switch
			{
				SecurityObjectType.File => File,
				SecurityObjectType.Directory => Directory,
				SecurityObjectType.RegistryKey => RegistryKey,
				SecurityObjectType.SamServer => SamServer,
				SecurityObjectType.SamDomain => SamDomain,
				SecurityObjectType.SamGroup => SamGroup,
				SecurityObjectType.SamAlias => SamAlias,
				SecurityObjectType.SamUserAccount => SamUser,
				SecurityObjectType.DirectoryObject => DirectoryObject,
				SecurityObjectType.Scm => Scm,
				SecurityObjectType.Service => Service,
				_ => null
			};
		/// <summary>
		/// Gets the value of the <c>MAXIMUM_ALLOWED</c> value.
		/// </summary>
		public const uint MaxAllowedValue = 0x02000000;

		public uint MapGenericAccessToSpecific(GenericAccessRights access)
		{
			uint specific = 0;
			if (0 == (access & GenericAccessRights.GenericRead))
				specific |= this.GenericReadAccess;
			if (0 == (access & GenericAccessRights.GenericWrite))
				specific |= this.GenericWriteAccess;
			if (0 == (access & GenericAccessRights.GenericExecute))
				specific |= this.GenericExecuteAccess;

			return specific;
		}
		public string GetGenericAccessRightName(GenericAccessRights accessBit) =>
			accessBit switch
			{
				GenericAccessRights.GenericRead => "Generic All",
				GenericAccessRights.GenericWrite => "Generic Read",
				GenericAccessRights.GenericExecute => "Generic Write",
				GenericAccessRights.GenericAll => "Generic All",
				_ => throw new ArgumentException("The value is not a valid generic access right.", nameof(accessBit))
			};
		public string GetStandardAccessRightName(StandardAccessRights accessBit) =>
			accessBit switch
			{
				StandardAccessRights.Delete => "Delete",
				StandardAccessRights.ReadControl => "Read permissions",
				StandardAccessRights.WriteDac => "Change permissions",
				StandardAccessRights.WriteOwner => "Take ownership",
				StandardAccessRights.Synchronize => "Synchronize",
				_ => throw new ArgumentException("The value is not a valid standard access right.", nameof(accessBit))
			};
		public string GetSpecialAccessRightName(SpecialAccessRights accessBit) =>
			accessBit switch
			{
				SpecialAccessRights.AccessSystemSecurity => "Access System Security",
				SpecialAccessRights.MaxAllowed => "Maximum Allowed",
				_ => throw new ArgumentException("The value is not a valid special access right.", nameof(accessBit))
			};

		public bool IsSpecificRight(uint accessBit) => (accessBit == (accessBit & SpecificRightsMask));

		private uint CheckGeneric(ref uint accessMask, GenericAccessRights genericMask, uint specificMask, string name, List<AccessRightInfo> rights)
		{
			if (0 != (accessMask & (uint)genericMask)
				|| (accessMask & specificMask) == specificMask)
			{
				rights.Add(new AccessRightInfo(specificMask, name));
				return (accessMask & (specificMask | (uint)genericMask));
			}
			else
				return 0;
		}

		private uint CheckRights(uint accessMask, IReadOnlyList<AccessRightInfo> toCheck, List<AccessRightInfo> rights)
		{
			uint matched = 0;
			foreach (var right in toCheck)
			{
				if ((accessMask & right.AccessMask) == right.AccessMask)
				{
					rights.Add(right);
					matched |= right.AccessMask;
				}
			}

			return matched;
		}

		public AccessRightInfo[] GetAccessRights(uint accessMask, bool includeGeneric)
		{
			List<AccessRightInfo> rights = new List<AccessRightInfo>();

			uint matched = 0;
			if (includeGeneric)
			{
				matched |= CheckGeneric(ref accessMask, GenericAccessRights.GenericAll, this.GenericAllAccess, "All", rights);
				matched |= CheckGeneric(ref accessMask, GenericAccessRights.GenericRead, this.GenericReadAccess, "Read", rights);
				matched |= CheckGeneric(ref accessMask, GenericAccessRights.GenericRead, this.GenericWriteAccess, "Write", rights);
				matched |= CheckGeneric(ref accessMask, GenericAccessRights.GenericExecute, this.GenericExecuteAccess, "Execute", rights);
			}

			matched |= this.CheckRights(accessMask, this.GetSpecificRights(), rights);

			// TODO: What about left over?
			uint remaining = accessMask & ~matched;
			if (remaining != 0)
				rights.Add(new AccessRightInfo(remaining, "Special"));

			return rights.ToArray();
		}

		private ReadOnlyCollection<AccessRightInfo> _accessRights;
		public IReadOnlyList<AccessRightInfo> GetSpecificRights()
		{
			return (this._accessRights ??= new ReadOnlyCollection<AccessRightInfo>(this.GetSpecificRightsInternal()));
		}
		protected abstract IList<AccessRightInfo> GetSpecificRightsInternal();

		public const uint SpecificRightsMask = 0x0000FFFF;

		public abstract uint GenericReadAccess { get; }
		public abstract uint GenericWriteAccess { get; }
		public abstract uint GenericExecuteAccess { get; }
		public uint GenericAllAccess => this.GenericReadAccess | this.GenericWriteAccess | this.GenericExecuteAccess;
	}

	public struct AccessRightInfo
	{
		public AccessRightInfo(uint accessMask, string description)
		{
			AccessMask = accessMask;
			Description = description;
		}

		public uint AccessMask { get; }
		public string Description { get; }
	}

	internal class FileSecurityModel : ObjectSecurityModel
	{
		public override uint GenericReadAccess => (uint)(0
			| FileAccessRights.ReadAttributes
			| FileAccessRights.ReadData
			| FileAccessRights.ReadEa
			| FileAccessRights.StandardRightsRead
			| FileAccessRights.Synchronize
			);

		public override uint GenericWriteAccess => (uint)(0
			| FileAccessRights.AppendData
			| FileAccessRights.WriteAttributes
			| FileAccessRights.WriteData
			| FileAccessRights.WriteEa
			| FileAccessRights.StandardRightsWrite
			| FileAccessRights.Synchronize
			);

		public override uint GenericExecuteAccess => (uint)(0
			| FileAccessRights.Execute
			| FileAccessRights.ReadAttributes
			| FileAccessRights.StandardRightsExecute
			| FileAccessRights.Synchronize
			);

		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)FileAccessRights.ReadData, "Read data"),
			new AccessRightInfo((uint)FileAccessRights.WriteData, "Write data"),
			new AccessRightInfo((uint)FileAccessRights.AppendData, "Append data"),
			new AccessRightInfo((uint)FileAccessRights.ReadEa, "Read extended attributes"),
			new AccessRightInfo((uint)FileAccessRights.WriteEa, "Write extended attributes"),
			new AccessRightInfo((uint)FileAccessRights.DeleteChild, "Delete child"),
			new AccessRightInfo((uint)FileAccessRights.Execute, "Execute"),
			new AccessRightInfo((uint)FileAccessRights.ReadAttributes, "Read attributes"),
			new AccessRightInfo((uint)FileAccessRights.WriteAttributes, "Write attributes"),
			];
	}

	internal class DirectorySecurityModel : FileSecurityModel
	{
		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)FileAccessRights.ReadData, "List folder"),
			new AccessRightInfo((uint)FileAccessRights.WriteData, "Create files"),
			new AccessRightInfo((uint)FileAccessRights.AppendData, "Create folders"),
			new AccessRightInfo((uint)FileAccessRights.ReadEa, "Read extended attributes"),
			new AccessRightInfo((uint)FileAccessRights.WriteEa, "Write extended attributes"),
			new AccessRightInfo((uint)FileAccessRights.DeleteChild, "Delete child"),
			new AccessRightInfo((uint)FileAccessRights.Execute, "Traverse folder"),
			new AccessRightInfo((uint)FileAccessRights.ReadAttributes, "Read attributes"),
			new AccessRightInfo((uint)FileAccessRights.WriteAttributes, "Write attributes"),
			];
	}

	internal class RegistryKeySecurityModel : ObjectSecurityModel
	{
		public override uint GenericReadAccess => (uint)(0
			| (RegistryAccessRights)StandardAccessRights.ReadControl
			| RegistryAccessRights.QueryValue
			| RegistryAccessRights.EnumerateSubkeys
			| RegistryAccessRights.Notify
			);

		public override uint GenericWriteAccess => (uint)(0
			| (RegistryAccessRights)StandardAccessRights.ReadControl
			| RegistryAccessRights.SetValue
			| RegistryAccessRights.CreateSubkey
			);

		public override uint GenericExecuteAccess => this.GenericReadAccess;

		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)RegistryAccessRights.QueryValue, "Query value"),
			new AccessRightInfo((uint)RegistryAccessRights.SetValue, "Set value"),
			new AccessRightInfo((uint)RegistryAccessRights.CreateSubkey, "Create subkey"),
			new AccessRightInfo((uint)RegistryAccessRights.EnumerateSubkeys, "Enumerate subkeys"),
			new AccessRightInfo((uint)RegistryAccessRights.Notify, "Notify"),
			new AccessRightInfo((uint)RegistryAccessRights.CreateLink, "Create link"),
			];
	}

	internal class ScmSecurityModel : ObjectSecurityModel
	{
		public override uint GenericReadAccess => (uint)(0
			| (ScmAccessRights)StandardAccessRights.ReadControl
			| ScmAccessRights.EnumerateService
			| ScmAccessRights.QueryLockStatus
			);

		public override uint GenericWriteAccess => (uint)(0
			| (ScmAccessRights)StandardAccessRights.ReadControl
			| ScmAccessRights.CreateService
			| ScmAccessRights.ModifyBootConfig
			);

		public override uint GenericExecuteAccess => (uint)(0
			| ScmAccessRights.Connect
			| ScmAccessRights.Lock
			);

		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)ScmAccessRights.Connect, "Connect"),
			new AccessRightInfo((uint)ScmAccessRights.CreateService, "Create service"),
			new AccessRightInfo((uint)ScmAccessRights.EnumerateService, "Enumerate services"),
			new AccessRightInfo((uint)ScmAccessRights.Lock, "Lock"),
			new AccessRightInfo((uint)ScmAccessRights.QueryLockStatus, "Query lock status"),
			new AccessRightInfo((uint)ScmAccessRights.ModifyBootConfig, "Modify boot configuration"),
			];
	}

	internal class ServiceSecurityModel : ObjectSecurityModel
	{
		public override uint GenericReadAccess => (uint)(0
			| (ServiceAccessRights)StandardAccessRights.ReadControl
			| ServiceAccessRights.QueryConfig
			| ServiceAccessRights.QueryStatus
			| ServiceAccessRights.Interrogate
			| ServiceAccessRights.EnumerateDependents
			);

		public override uint GenericWriteAccess => (uint)(0
			| (ServiceAccessRights)StandardAccessRights.ReadControl
			| ServiceAccessRights.ChangeConfig
			);

		public override uint GenericExecuteAccess => (uint)(0
			| (ServiceAccessRights)StandardAccessRights.ReadControl
			| ServiceAccessRights.Start
			| ServiceAccessRights.Stop
			| ServiceAccessRights.PauseContinue
			| ServiceAccessRights.UserDefinedControl
			);

		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)ServiceAccessRights.QueryConfig, "Query configuration"),
			new AccessRightInfo((uint)ServiceAccessRights.ChangeConfig, "Change configuration"),
			new AccessRightInfo((uint)ServiceAccessRights.QueryStatus, "Query status"),
			new AccessRightInfo((uint)ServiceAccessRights.EnumerateDependents, "Enumerate dependents"),
			new AccessRightInfo((uint)ServiceAccessRights.Start, "Start service"),
			new AccessRightInfo((uint)ServiceAccessRights.Stop, "Stop"),
			new AccessRightInfo((uint)ServiceAccessRights.PauseContinue, "Pause and continue"),
			new AccessRightInfo((uint)ServiceAccessRights.Interrogate, "Interrogate"),
			new AccessRightInfo((uint)ServiceAccessRights.UserDefinedControl, "User-defined control"),
			];
	}

	internal class DirectoryObjectSecurityModel : ObjectSecurityModel
	{
		public override uint GenericReadAccess => (uint)(0
			| (DirectoryObjectAccessRights)StandardAccessRights.ReadControl
			);

		public override uint GenericWriteAccess => (uint)(0
			| (DirectoryObjectAccessRights)StandardAccessRights.ReadControl
			);

		public override uint GenericExecuteAccess => (uint)(0
			| (DirectoryObjectAccessRights)StandardAccessRights.ReadControl
			);

		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)DirectoryObjectAccessRights.ListObject, "List contents"),
			new AccessRightInfo((uint)DirectoryObjectAccessRights.ReadProperty, "Read all properties"),
			new AccessRightInfo((uint)DirectoryObjectAccessRights.WriteProperty, "Write all properties"),
			new AccessRightInfo((uint)DirectoryObjectAccessRights.DeleteTree, "Delete subtree"),
			new AccessRightInfo((uint)DirectoryObjectAccessRights.SelfWrite, "All validated rights"),
			new AccessRightInfo((uint)DirectoryObjectAccessRights.ControlAccess, "All extended rights"),
			new AccessRightInfo((uint)DirectoryObjectAccessRights.DeleteChild, "Delete all child objects"),
			new AccessRightInfo((uint)DirectoryObjectAccessRights.CreateChild, "Create all child objects"),
			new AccessRightInfo((uint)DirectoryObjectAccessRights.ListChildren, "List children"),
			];
	}

	internal class SamServerSecurityModel : ObjectSecurityModel
	{
		public override uint GenericReadAccess => (uint)(0
			| (SamServerAccessRights)StandardAccessRights.ReadControl
			| SamServerAccessRights.EnumerateDomains
			);

		public override uint GenericWriteAccess => (uint)(0
			| (SamServerAccessRights)StandardAccessRights.ReadControl
			| SamServerAccessRights.Shutdown
			| SamServerAccessRights.Initialize
			| SamServerAccessRights.CreateDomain
			);

		public override uint GenericExecuteAccess => (uint)(0
			| (SamServerAccessRights)StandardAccessRights.ReadControl
			| SamServerAccessRights.Connect
			| SamServerAccessRights.LookupDomain
			);

		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)SamServerAccessRights.Connect, "Connect to server"),
			//SamServerAccess.Shutdown => 
			//SamServerAccess.Initialize => 
			//SamServerAccess.CreateDomain => 
			new AccessRightInfo((uint)SamServerAccessRights.EnumerateDomains, "View domain objects"),
			new AccessRightInfo((uint)SamServerAccessRights.LookupDomain, "Perform SID-to-name translation"),
			];
	}

	internal class SamDomainSecurityModel : ObjectSecurityModel
	{
		public override uint GenericReadAccess => (uint)(0
			| (SamDomainAccessRights)StandardAccessRights.ReadControl
			| (SamDomainAccessRights)0x00020084
			);

		public override uint GenericWriteAccess => (uint)(0
			| (SamDomainAccessRights)StandardAccessRights.ReadControl
			| (SamDomainAccessRights)0x0002047A
			);

		public override uint GenericExecuteAccess => (uint)(0
			| (SamDomainAccessRights)StandardAccessRights.ReadControl
			| (SamDomainAccessRights)0x00020301
			);

		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)SamDomainAccessRights.ReadPasswordParams, "Read password policy"),
			new AccessRightInfo((uint)SamDomainAccessRights.WritePasswordParams, "Write password policy"),
			new AccessRightInfo((uint)SamDomainAccessRights.ReadOtherParams, "Read policy not related to passwords"),
			new AccessRightInfo((uint)SamDomainAccessRights.WriteOtherParams, "Write policy not related to passwords"),
			new AccessRightInfo((uint)SamDomainAccessRights.CreateUser, "Create user"),
			new AccessRightInfo((uint)SamDomainAccessRights.CreateGroup, "Create group"),
			new AccessRightInfo((uint)SamDomainAccessRights.CreateAlias, "Create alias"),
			new AccessRightInfo((uint)SamDomainAccessRights.GetAliasMembership, "Read alias membership"),
			new AccessRightInfo((uint)SamDomainAccessRights.ListAccounts, "List accounts"),
			new AccessRightInfo((uint)SamDomainAccessRights.Lookup, "Look up objects by name and SID"),
			new AccessRightInfo((uint)SamDomainAccessRights.AdministerServer, "Administer server"),
			];
	}

	internal class SamGroupSecurityModel : ObjectSecurityModel
	{
		public override uint GenericReadAccess => (uint)(0
			| (SamGroupAccessRights)StandardAccessRights.ReadControl
			| (SamGroupAccessRights)0x00020010
			);

		public override uint GenericWriteAccess => (uint)(0
			| (SamGroupAccessRights)StandardAccessRights.ReadControl
			| (SamGroupAccessRights)0x0002000E
			);

		public override uint GenericExecuteAccess => (uint)(0
			| (SamGroupAccessRights)StandardAccessRights.ReadControl
			| (SamGroupAccessRights)0x00020001
			);

		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)SamGroupAccessRights.ReadInfo, "Read attributes"),
			new AccessRightInfo((uint)SamGroupAccessRights.WriteAccount, "Write attributes"),
			new AccessRightInfo((uint)SamGroupAccessRights.AddMember, "Add member"),
			new AccessRightInfo((uint)SamGroupAccessRights.RemoveMember, "Remove member"),
			new AccessRightInfo((uint)SamGroupAccessRights.ListMembers, "List members"),
			];
	}

	internal class SamAliasSecurityModel : ObjectSecurityModel
	{
		public override uint GenericReadAccess => (uint)(0
			| (SamAliasAccessRights)StandardAccessRights.ReadControl
			| (SamAliasAccessRights)0x00020004
			);

		public override uint GenericWriteAccess => (uint)(0
			| (SamAliasAccessRights)StandardAccessRights.ReadControl
			| (SamAliasAccessRights)0x00020013
			);

		public override uint GenericExecuteAccess => (uint)(0
			| (SamAliasAccessRights)StandardAccessRights.ReadControl
			| (SamAliasAccessRights)0x00020008
			);

		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)SamAliasAccessRights.AddMember, "Add member"),
			new AccessRightInfo((uint)SamAliasAccessRights.RemoveMember, "Remove member"),
			new AccessRightInfo((uint)SamAliasAccessRights.ListMembers, "List members"),
			new AccessRightInfo((uint)SamAliasAccessRights.ReadInfo, "Read attributes"),
			new AccessRightInfo((uint)SamAliasAccessRights.WriteAccount, "Write attributes"),
			];
	}

	internal class SamUserSecurityModel : ObjectSecurityModel
	{
		public override uint GenericReadAccess => (uint)(0
			| (SamUserAccessRights)StandardAccessRights.ReadControl
			| (SamUserAccessRights)0x0002031A
			);

		public override uint GenericWriteAccess => (uint)(0
			| (SamUserAccessRights)StandardAccessRights.ReadControl
			| (SamUserAccessRights)0x00020044
			);

		public override uint GenericExecuteAccess => (uint)(0
			| (SamUserAccessRights)StandardAccessRights.ReadControl
			| (SamUserAccessRights)0x00020041
			);

		protected override IList<AccessRightInfo> GetSpecificRightsInternal() => [
			new AccessRightInfo((uint)SamUserAccessRights.ReadGeneral, "Read sundry attributes"),
			new AccessRightInfo((uint)SamUserAccessRights.ReadPreferences, "Read general information attributes"),
			new AccessRightInfo((uint)SamUserAccessRights.WritePreferences, "Write general information attributes"),
			new AccessRightInfo((uint)SamUserAccessRights.ReadLogon, "Read attributes related to logon statistics"),
			new AccessRightInfo((uint)SamUserAccessRights.ReadAccount, "Read administrative attributes"),
			new AccessRightInfo((uint)SamUserAccessRights.WriteAccount, "Write administrative attributes"),
			new AccessRightInfo((uint)SamUserAccessRights.ChangePassword, "Change password"),
			new AccessRightInfo((uint)SamUserAccessRights.ForcePasswordChange, "Reset password"),
			new AccessRightInfo((uint)SamUserAccessRights.ListGroups, "List group memberships"),
			//SamUserAccess.ReadGroupInfo => throw new NotImplementedException(),
			//SamUserAccess.WriteGroupInfo => throw new NotImplementedException(),
			];
	}
}
