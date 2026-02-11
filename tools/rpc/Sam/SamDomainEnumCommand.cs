using System.ComponentModel;
using Titanis.Msrpc.Mssamr;

namespace Titanis.Cli.SamTool;
internal abstract class SamDomainEnumCommand : SamCommand
{
	[Parameter]
	[DefaultValue(true)]
	[Description("Continue even if errors occur")]
	public SwitchParam ContinueOnError { get; set; }

	protected sealed override SamServerAccess RequiredSamAccess => SamServerAccess.EnumerateDomains | SamServerAccess.LookupDomain;

	protected abstract SamDomainAccess RequiredDomainAccess { get; }

	protected sealed override async Task<int> RunAsync(Sam sam, CancellationToken cancellationToken)
	{
		var domains = await sam.GetDomains(cancellationToken);
		foreach (var domainInfo in domains)
		{
			SamDomain domain;
			try
			{
				this.WriteDiagnostic($"Opening domain '{domainInfo.Name}' ({domainInfo.Id}).");
				domain = await sam.OpenDomainAsync(domainInfo.Name, this.RequiredDomainAccess, cancellationToken);
			}
			catch (Exception ex)
			{
				if (this.ContinueOnError.IsSet)
				{
					this.WriteError($"Failed to open domain '{domainInfo.Name}' with error: {ex.Message}");
					continue;
				}
				else
					throw;
			}

			await this.RunAsync(domain, domainInfo, sam, cancellationToken);
		}

		return 0;
	}

	protected abstract Task RunAsync(SamDomain domain, SamEntry domainInfo, Sam sam, CancellationToken cancellationToken);
}
