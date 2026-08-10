using System.ComponentModel;
using System.Net;
using Titanis.Ldap;
using Titanis.Msrpc.Msdrsr;
using Titanis.Net;

namespace Titanis.Cli.Dsrep;

[Command]
[Description("Requests replica changes")]
public class ReplicateObjectsCommand : ReplicateCommand
{
	[Parameter(After = nameof(RpcCommand.ServerName))]
	[Description("DN, GUID, or SID of object to retrieve")]
	public DsobjSpec[]? ObjectName { get; set; }

	[Parameter]
	[DefaultValue(1)]
	[Description("Number of parallel requests")]
	public int Parallelize { get; set; }

	protected override int GetDegreeOfParallelism() => this.Parallelize;

	protected sealed override ExtendedOpRequest GetExop() => ExtendedOpRequest.ReplObject;

	protected override async IAsyncEnumerable<DsName> GetObjectNames(CancellationToken cancellationToken)
	{
		LdapClient? ldapClient = null;
		var objSpecs = this.ObjectName;
		if (objSpecs.IsNullOrEmpty())
		{
			objSpecs = [new DsobjSpec(LdapFilter.Parse("(objectClass=*)"))];
		}
		foreach (var objSpec in objSpecs)
		{
			var objName = objSpec.Dsname;
			if (objName is not null)
			{
				yield return objName;
			}
			else
			{
				ldapClient ??= await LdapClient.Connect(new DnsEndPoint(this.ServerName, 389), null, this.RequireService<ISocketService>(), this.RequireService<IClientCredentialService>(), cancellationToken);

				var filter = objSpec.Filter ?? LdapFilter.Parse($"(samAccountName={objSpec.Name})");
				LdapQuery query = new(ldapClient.DomainRoot, LdapSearchScope.Subtree, filter, [])
				{
					PageSize = 20,
					Options = LdapQueryOptions.AllPages
				};

				var results = await ldapClient.Search(query, cancellationToken);
				if (results.EntryCount == 0)
					this.WriteWarning($"The object specification '{objSpec?.ToString()}' did not return any results");
			}
		}
	}
}