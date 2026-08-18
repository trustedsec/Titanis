using System.ComponentModel;
using Titanis.Cli;

namespace Titanis.Cli.Dsrep;

[Subcommand("dcinfo", typeof(DcinfoCommand))]
[Subcommand("rep", typeof(ReplicateObjectsCommand))]
[Subcommand("repnc", typeof(ReplicateNcCommand))]
[Subcommand("sites", typeof(ListSitesCommand))]
[Subcommand("roles", typeof(ListRolesCommand))]
[Subcommand("domains", typeof(ListDomainsCommand))]
[Subcommand("partitions", typeof(ListPartitionsCommand))]
[Subcommand("gcs", typeof(ListGcsCommand))]
[Subcommand("neighbors", typeof(NeighborsCommand))]
[Subcommand("repsto", typeof(RepsToCommand))]
[Subcommand("cursors", typeof(CursorsCommand))]
[Subcommand("objmetadata", typeof(ObjectMetadataCommand))]
[Subcommand("attrmetadata", typeof(AttributeMetadataCommand))]
[Subcommand("queue", typeof(QueueCommand))]
[Subcommand("kccfailures", typeof(KccFailuresCommand))]
[Subcommand("utdvec", typeof(UptodateVectorsCommand))]
[Subcommand("addsidhist", typeof(AddSidHistoryCommand))]
[Subcommand("readngckey", typeof(ReadNgcKeyCommand))]
[Subcommand("writengckey", typeof(WriteNgcKeyCommand))]
[Subcommand("crackname", typeof(CrackNameCommand))]
[Description("Interacts with Directory Replication Service")]
internal class Program : MultiCommand
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);
}
