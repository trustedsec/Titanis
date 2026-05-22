using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Mssamr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.SamTool;


/// <task category="SAM;Enumeration">Get the members of a group</task>
[Command]
[Description("Gets the members of a group")]
[DetailedHelpText("You may specify a group either as a name, decimal RID, or hex RID prefixed with 0x.  You may specify multiple groups.")]
[Example("Look up domain administrators", "LUMON-DC1 -UserName LUMON\\milchick -Password Br3@kr00m! -EncryptRpc \"Domain Admins\"", Tag = "milchickNtlm_DA")]
[Example("Look up multiple groups", "LUMON-FS1 -UserName LUMON\\milchick -Password Br3@kr00m! -EncryptRpc Administrators, \"Domain Admins\", \"Enterprise Admins\"", Tag = "milchickNtlm_multi")]
[OutputRecordType(typeof(SamMembership))]
public class GroupMembersCommand : SamDomainEnumCommand
{
	protected override SamDomainAccessRights RequiredDomainAccess => SamDomainAccessRights.Lookup
	;

	[Parameter(After = nameof(ServerName))]
	public string[] GroupRidOrName { get; set; }

	private HashSet<uint>? _rids;
	private HashSet<string>? _names;

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		HashSet<uint> rids = [];
		var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		foreach (var groupRidOrName in this.GroupRidOrName)
		{
			if ((groupRidOrName.StartsWith("0x") && uint.TryParse(groupRidOrName, System.Globalization.NumberStyles.HexNumber, null, out var aliasRid))
				|| uint.TryParse(groupRidOrName, out aliasRid)
				)
			{
				rids.Add(aliasRid);
			}
			else
			{
				names.Add(groupRidOrName);
			}
		}

		this._rids = rids;
		this._names = names;
	}

	protected override async Task RunAsync(SamDomain domain, SamEntry domainInfo, Sam sam, CancellationToken cancellationToken)
	{
		var names = this._names!;

		HashSet<uint> groupRids = [];
		HashSet<uint> aliasRids = [];
		if (names.Count > 0)
		{
			// Attempt to resolve unresolved names

			try
			{
				var entries = await domain.LookupNamesAsync(names.ToArray(), cancellationToken);
				foreach (var entry in entries)
				{
					this.WriteVerbose($"Name '{entry.Name}' resolved to as a {entry.EntryType} with ID {entry.Id}.");

					names.Remove(entry.Name);

					this.WriteVerbose($"Resolved '{entry.Name}' as type '{entry.EntryType}' within domain '{domain.DomainName}'.");
					switch (entry.EntryType)
					{
						case SamEntryType.Alias:
							aliasRids.Add(entry.Id);
							break;
						case SamEntryType.Group:
							groupRids.Add(entry.Id);
							break;
						default:
							this.WriteWarning($"Name '{entry.Name}' resolved as type '{entry.EntryType}', but cannot enumerate members.");
							break;
					}
				}
			}
			catch
			{

			}
		}

		var allRids = this._rids!;

		// Determine types of other IDs
		{
			foreach (var rid in allRids)
			{
				try
				{
					var entry = await domain.LookupIdAsync(rid, cancellationToken);

					this.WriteVerbose($"Resolved ID {entry.Id} as '{entry.Name}' as type '{entry.EntryType}' within domain '{domain.DomainName}'.");
					switch (entry.EntryType)
					{
						case SamEntryType.Alias:
							aliasRids.Add(entry.Id);
							break;
						case SamEntryType.Group:
							groupRids.Add(entry.Id);
							break;
						default:
							this.WriteWarning($"Name '{entry.Name}' resolved as type '{entry.EntryType}', but cannot enumerate members.");
							break;
					}
				}
				catch
				{

				}
			}
		}

		// Enumerate aliases
		foreach (var rid in aliasRids.ToArray())
		{
			try
			{
				var alias = await domain.OpenAliasAsync(rid, SamAliasAccessRights.ListMembers, cancellationToken);
				allRids.Remove(rid);
				var members = await alias.GetMembersAsync(cancellationToken);

				foreach (var member in members)
				{
					var membership = new SamMembership
					{
						DomainName = domain.DomainName,
						DomainSid = domain.Sid,
						GroupRid = rid,
						MemberSid = member,
					};
					this.WriteRecord(membership);
				}
			}
			catch
			{

			}
		}

		// Enumerate groups
		foreach (var rid in groupRids.ToArray())
		{
			try
			{
				var group = await domain.OpenGroupAsync(rid, SamGroupAccessRights.ListMembers, cancellationToken);
				allRids.Remove(rid);
				var members = await group.GetMembersAsync(cancellationToken);

				foreach (var member in members)
				{
					var membership = new SamMembership
					{
						DomainName = domain.DomainName,
						DomainSid = domain.Sid,
						GroupRid = rid,
						MemberSid = domain.Sid.Concat(member.ObjectId),
					};
					this.WriteRecord(membership);
				}
			}
			catch
			{

			}
		}
	}
}
