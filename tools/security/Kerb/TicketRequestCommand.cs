using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Cli;
using Titanis.Security.Kerberos;

namespace Kerb;
/// <summary>
/// Base class for commands that request a ticket
/// </summary>
abstract class TicketRequestCommand : KdcCommand
{

	[Parameter]
	[Category(ParameterCategories.Output)]
	[Description("Name of file to write ticket to")]
	public string? OutputFileName { get; set; }

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
	public string? TicketCache { get; set; }

	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		if (string.IsNullOrEmpty(this.OutputFileName))
		{
			if (string.IsNullOrEmpty(this.TicketCache))
			{
				context.LogError(new ParameterValidationError(null, $"-{nameof(OutputFileName)} is required unless -{nameof(TicketCache)} is specified"));
			}
		}
		else
		{
			string outFileName = this.ResolveFsPath(this.OutputFileName);
			if (File.Exists(outFileName) && !(this.Overwrite.IsSet || this.Append.IsSet))
			{
				context.LogError($"Output file '{outFileName}' already exists.  Specify a different file name or use -Overwrite to overwrite it or -Append to append to it.");
			}
		}
	}

	protected abstract Task<IList<TicketInfo>> RequestTickets(KerberosClient krb, CancellationToken cancellationToken);

	protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		string? outFileName = string.IsNullOrEmpty(this.OutputFileName) ? null : this.ResolveFsPath(this.OutputFileName);

		KerberosClient krb = this.CreateKerberosClient();
		if (!string.IsNullOrEmpty(this.TicketCache))
		{
			krb.TicketCache = new TicketCacheFile(this.TicketCache, krb);
		}

		// Load tickets from file, if it exists
		List<TicketInfo> tickets = new List<TicketInfo>();
		if ((outFileName is not null) && this.Append.IsSet && File.Exists(outFileName))
		{
			TicketInfo[] existingTickets = krb.LoadTicketsFromFile(File.ReadAllBytes(outFileName), out _);
			this.WriteVerbose($"Loaded {existingTickets.Length} ticket(s) from {outFileName}.");
			tickets.AddRange(existingTickets);
		}

		var newTickets = await this.RequestTickets(krb, cancellationToken);
		if (newTickets is not null)
		{
			if (newTickets.Count > 0 && !string.IsNullOrEmpty(outFileName))
			{
				tickets.AddRange(newTickets);
				var tgtBytes = krb.ExportTickets(tickets, KerberosClient.GetFormatFromFileName(outFileName));
				File.WriteAllBytes(outFileName, tgtBytes);

				this.WriteVerbose($"Exported {tickets.Count} ticket(s) to {outFileName}");
			}
		}

		return 0;
	}
}
