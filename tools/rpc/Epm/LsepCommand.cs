using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Epm;

namespace Titanis.DceRpc.Cli
{
	/// <task category="RPC">Enumerate dynamic RPC endpoints</task>
	[Command]
	[Description("Lists the dynamic RPC endpoints registered with the endpoint mapper")]
	[OutputRecordType(typeof(EndpointEntry))]
	[DetailedHelpText(@"Queries to the endpoint mapper are usually unauthenticated.  This is different from anonymous authentication in that no security context is established.")]
	[Example("List all endpoints", "{0} LUMON-DC1")]
	public sealed class LsepCommand : EpmCommand
	{
		[Parameter]
		[Description("Filter for object ID")]
		public Guid? ObjectId { get; set; }

		[Parameter]
		[Description("Filter for interface ID")]
		public Guid? InterfaceId { get; set; }

		[Parameter]
		[Description("Filter for max. version")]
		public RpcVersion? UpToVersion { get; set; }

		[Parameter]
		[Description("Filter for exact version")]
		public RpcVersion? ExactVersion { get; set; }

		[Parameter]
		[Description("Filter for compatible version")]
		public RpcVersion? CompatVersion { get; set; }

		[Parameter]
		[Description("Filter for major version")]
		public ushort? MajorVersion { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			var versionFilters = 0;
			if (this.UpToVersion.HasValue) versionFilters++;
			if (this.CompatVersion.HasValue) versionFilters++;
			if (this.ExactVersion.HasValue) versionFilters++;
			if (this.MajorVersion.HasValue) versionFilters++;

			if (versionFilters > 0 && !this.InterfaceId.HasValue)
				context.LogError("A version filter requires an interface ID to be specified.");
			if (versionFilters > 1)
				context.LogError("Only one version filter may be specified.");
		}

		protected sealed override async Task<int> RunAsync(EpmClient client, CancellationToken cancellationToken)
		{
			RpcVersion version = default;
			InquiryVersionOptions versionOptions = InquiryVersionOptions.All;
			if (this.ExactVersion.HasValue)
				(version, versionOptions) = (this.ExactVersion.Value, InquiryVersionOptions.Exact);
			else if (this.UpToVersion.HasValue)
				(version, versionOptions) = (this.UpToVersion.Value, InquiryVersionOptions.UpTo);
			else if (this.CompatVersion.HasValue)
				(version, versionOptions) = (this.CompatVersion.Value, InquiryVersionOptions.Compatible);
			else if (this.MajorVersion.HasValue)
				(version, versionOptions) = (new RpcVersion(this.MajorVersion.Value, 0), InquiryVersionOptions.MajorOnly);

			var eps = await client.Lookup(PageSize, this.ObjectId, this.InterfaceId, versionOptions, version, cancellationToken);
			WriteRecords(eps);

			if (eps.Count == 0)
			{
				WriteVerbose("No endpoints returned.");
				return 1;
			}
			else
				return 0;
		}
	}
}
