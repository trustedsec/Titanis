using System.ComponentModel;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb;
/// <summary>
/// Base class for commands that request a ticket
/// </summary>
[OutputRecordType(typeof(TicketInfo), DefaultOutputStyle = OutputStyle.Table, DefaultFields = new string[]
{
		nameof(TicketInfo.ClientName), nameof(TicketInfo.ClientRealm), nameof(TicketInfo.TargetSpn), nameof(TicketInfo.EndTime), nameof(TicketInfo.KdcOptions)
})]
public abstract class TicketRequestCommand : Command
{

	[Parameter]
	[Category(ParameterCategories.Output)]
	[Description("Name of file to write ticket to")]
	[KerberosTicketFileSpec(false)]
	public FileSpec? OutputFileName { get; set; }

	[Parameter]
	[Category(ParameterCategories.Output)]
	[Description("Overwrites the output file, if it exists")]
	public SwitchParam Overwrite { get; set; }

	[Parameter]
	[Category(ParameterCategories.Output)]
	[Description("Appends to the output file, if it exists")]
	public SwitchParam Append { get; set; }

	[Parameter(EnvironmentVariable = KerberosClient.Krb5CacheVariableName)]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Name of ticket cache file")]
	[KerberosTicketFileSpec(false)]
	public FileSpec? TicketCache { get; set; }

	[Parameter]
	[Category(ParameterCategories.AuthenticationKerberos)]
	[Description("Name of client workstation")]
	public string? Workstation { get; set; }

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		if (this.OutputFileName is null)
		{
			if (this.TicketCache is null)
			{
				context.LogError(new ParameterValidationError(null, $"-{nameof(OutputFileName)} is required unless -{nameof(TicketCache)} is specified"));
			}
		}
		else
		{
			var outFileName = this.OutputFileName;
			if (this.FileAccessService.FileExists(outFileName) && !(this.Overwrite.IsSet || this.Append.IsSet))
			{
				context.LogError($"Output file '{outFileName}' already exists.  Specify a different file name or use -Overwrite to overwrite it or -Append to append to it.");
			}
		}
	}

	protected virtual KerberosClient CreateKerberosClient() => this.Services.CreateKerberosClient();

	protected abstract Task<IList<TicketInfo>?> RequestTickets(KerberosClient krb, CancellationToken cancellationToken);

	protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		var outFileName = this.OutputFileName;

		KerberosClient krb = this.CreateKerberosClient();
		if (this.TicketCache != null)
		{
			var ticketCacheFile = this.TicketCache;
			var cacheBytes = this.FileAccessService.FileExists(ticketCacheFile)
				? this.FileAccessService.ReadAllBytesFrom(ticketCacheFile)
				: [];
			krb.TicketCache = new TicketCacheFile(cacheBytes, ticketCacheFile.FileName, krb);
		}

		// Load tickets from file, if it exists
		List<TicketInfo> tickets = new List<TicketInfo>();
		if ((outFileName is not null) && this.Append.IsSet && this.FileAccessService.FileExists(outFileName))
		{
			TicketInfo[] existingTickets = krb.LoadTicketsFromFile(this.FileAccessService.ReadAllBytesFrom(outFileName), outFileName.FileName, out _);
			this.WriteVerbose($"Loaded {existingTickets.Length} ticket(s) from {outFileName}.");
			tickets.AddRange(existingTickets);
		}

		var newTickets = await this.RequestTickets(krb, cancellationToken);
		if (newTickets is not null)
		{
			this.WriteRecords(newTickets);
			if (newTickets.Count > 0 && outFileName != null)
			{
				tickets.AddRange(newTickets);
				var tgtBytes = krb.ExportTickets(tickets, KerberosClient.GetFormatFromFileName(outFileName.FileName));
				this.FileAccessService.WriteAllBytesTo(outFileName, tgtBytes);

				this.WriteVerbose($"Exported {tickets.Count} ticket(s) to {outFileName}");
			}
		}

		return 0;
	}
}
