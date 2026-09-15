using System.ComponentModel;
using System.Globalization;
using Titanis.Cli;
using Titanis.Msrpc.Mseven6;

namespace Even6;

[Command]
[Description("Queries the event log")]
[OutputRecordType(typeof(EventRecord), DefaultFields = [
	nameof(EventRecord.EventId),
	nameof(EventRecord.Name),
	nameof(EventRecord.Keywords),
	nameof(EventRecord.TimeCreated),
	nameof(EventRecord.Channel),
	nameof(EventRecord.Level),
	nameof(EventRecord.Task),
	nameof(EventRecord.Opcode),
	nameof(EventRecord.ProcessId),
	nameof(EventRecord.UserSid),
	], DefaultOutputStyle = OutputStyle.Table)]
[DetailedHelpText(@"If no query is specified, all events are listed, starting with the oldest.  You may specify an XPath query with -QueryXPath, or compose a query using the -ByXxx switches.  Each of the -ByXxx parameters accept multiple arguments.  Use -Verbose to see the generated query.

Use -OutputFields to display event data as columns, using either the name of the <Data> element, or @n to specify it by its zero-based ordinal.
")]
public class QueryCommand : QueryCommandBase
{
	protected override async Task<int> RunAsync(EventLog6Client client, CancellationToken cancellationToken)
	{
		string query = this.BuildQueryXPath();
		this.WriteVerbose($"Using query: {query}");

		var reader = await client.Query(this.Channel, query, this.PageSize, CultureInfo.CurrentCulture.LCID, EventLogReaderOptions.RenderMessages, cancellationToken);
		while (await reader.ReadNext(cancellationToken))
		{
			this.WriteRecord(reader.Current);
		}

		return 0;
	}
}