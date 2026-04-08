using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Msrpc.Mssamr;
using Titanis.Winterop.Security;

namespace Titanis.Cli.SamTool;

/// <summary>
/// Describes a SAM membership.
/// </summary>
public class SamMembership
{
	public string? DomainName { get; set; }
	public SecurityIdentifier? DomainSid { get; set; }
	public string? GroupName { get; set; }
	public uint GroupRid { get; set; }
	public SecurityIdentifier? MemberSid { get; set; }
	public string? MemberName { get; set; }
}
