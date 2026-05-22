using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Winterop.Registry;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Msrrp.Cli
{
	/// <task category="Registry">Get a security descriptor of a registry key</task>
	[Command]
	[Description("Gets the security descriptor of a registry key")]
	[DetailedHelpText(@"By default, this command requests the DACL, owner, and group.  If any of the switches are specified, then only those components specified are included.")]
	[Example("Request DACL, owner, and group of HKCU\\Software", "{0} LUMON-FS1 HKCU\\Software")]
	[Example("Request DACL only", "{0} -IncludeDacl LUMON-FS1 HKCU\\Software")]
	[Example("Request DACL and ownner", "{0} -IncludeDacl -IncludeOwner LUMON-FS1 HKCU\\Software")]
	internal class GetsdCommand : RegistryKeyCommand
	{
		[Parameter]
		[Description("Request the DACL")]
		[DefaultValue(true)]
		public SwitchParam IncludeDacl { get; set; }
		[Parameter]
		[Description("Request the owner")]
		[DefaultValue(true)]
		public SwitchParam IncludeOwner { get; set; }
		[Parameter]
		[Description("Request the group")]
		[DefaultValue(true)]
		public SwitchParam IncludeGroup { get; set; }
		[Parameter]
		[Description("Request the SACL")]
		public SwitchParam IncludeSacl { get; set; }

		protected override RegistryAccessRights RequiredKeyAccess => (RegistryAccessRights)SecurityDescriptor.GetRightsToRead(this._securityInfo);

		private SecurityInfo _securityInfo;

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			SwitchParamFlags reqFlags;
			if (this.IncludeDacl.IsSpecified || this.IncludeOwner.IsSpecified || this.IncludeGroup.IsSpecified || this.IncludeSacl.IsSpecified)
				reqFlags = SwitchParamFlags.SpecifiedAndSet;
			else
				reqFlags = SwitchParamFlags.Set;

			SecurityInfo secInfo = 0;
			if (this.IncludeDacl.Flags == reqFlags) secInfo |= SecurityInfo.Dacl;
			if (this.IncludeOwner.Flags == reqFlags) secInfo |= SecurityInfo.Owner;
			if (this.IncludeGroup.Flags == reqFlags) secInfo |= SecurityInfo.Group;
			if (this.IncludeSacl.Flags == reqFlags) secInfo |= SecurityInfo.Sacl;

			this._securityInfo = secInfo;
		}

		protected override async Task<int> RunAsync(RegistryKey key, RemoteRegistryClient client, CancellationToken cancellationToken)
		{
			var sd = await key.GetSecurityDescriptor(this._securityInfo, cancellationToken);
			this.WriteRecord(sd.ToSddlString(SecurityDescriptorSections.All));

			return 0;
		}
	}
}
