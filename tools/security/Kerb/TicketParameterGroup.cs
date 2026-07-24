using System.ComponentModel;
using Titanis.Security.Kerberos;

namespace Titanis.Cli.Kerb
{
	public class TicketParameterGroup : ParameterGroupBase
	{
		#region Ticket options
		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Requests a forwardable ticket")]
		public SwitchParam Forwardable { get; set; }

		[Parameter]
		[Advanced]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Requests a proxiable ticket")]
		public SwitchParam Proxiable { get; set; }

		[Parameter]
		[Advanced]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Requests a postdated ticket with the specified start date")]
		public DateTime? Postdate { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Requests a renewable ticket")]
		public SwitchParam Renewable { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Requests a ticket renewable until the specified time (implies -Renewable)")]
		public DateTime? RenewTill { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("End time")]
		public DateTime? EndTime { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Accepts a renewable ticket if the end time is over the limit")]
		public SwitchParam RenewableOk { get; set; }
		#endregion

		[Parameter]
		[Description("Comment to associate with ticket")]
		public string? TicketComment { get; set; }

		public TicketParameters GetTicketParameters(ILog? log, KdcOptions defaultOptions)
		{
			TicketParameters ticketParameters = new();

			ticketParameters.TicketComment = this.TicketComment;

			if (this.EndTime.HasValue)
				ticketParameters.EndTime = this.EndTime.Value.ToUniversalTime();

			bool optionsSpecified = false
				| this.RenewableOk.IsSpecified
				| this.Forwardable.IsSpecified
				| this.Renewable.IsSpecified
				| this.RenewableOk.IsSpecified
				| this.Postdate.HasValue
				| this.RenewTill.HasValue
				;
			KdcOptions options;
			if (optionsSpecified)
			{
				options = KdcOptions.None;
				if (this.Postdate.HasValue)
				{
					options |= KdcOptions.Postdated;
					ticketParameters.StartTime = this.Postdate.Value.ToUniversalTime();
				}

				if (this.RenewableOk.IsSet)
					options |= KdcOptions.RenewableOK;
				if (this.Forwardable.IsSet)
					options |= KdcOptions.Forwardable;

				if (this.RenewTill.HasValue)
				{
					options |= KdcOptions.Renewable;
					ticketParameters.RenewTill = this.RenewTill.Value.ToUniversalTime();
				}
			}
			else
			{
				options = defaultOptions;
				var end = KerberosClient.GetDefaultEndTime();
				ticketParameters.RenewTill = ticketParameters.EndTime = end;
			}

			ticketParameters.Options = options;
			return ticketParameters;
		}
	}
}
