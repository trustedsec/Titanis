using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;
using Titanis.Security.Kerberos;

namespace Kerb
{
	internal class TicketParameterGroup
	{
		#region Ticket options
		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Requests a forwardable ticket")]
		public SwitchParam Forwardable { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Requests a forwardable ticket")]
		public SwitchParam Proxiable { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Requests a postdated ticket with the specified start date")]
		public DateTime? Postdate { get; set; }

		[Parameter]
		[Category(ParameterCategories.AuthenticationKerberos)]
		[Description("Requests a ticket renewable until the specified time")]
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

		public TicketParameters GetTicketParameters(ILog? log)
		{
			TicketParameters ticketParameters = new();
			KdcOptions options = 0;
			if (this.Postdate.HasValue)
			{
				options |= KdcOptions.Postdated;
				ticketParameters.StartTime = this.Postdate.Value.ToUniversalTime();
			}
			if (this.EndTime.HasValue)
				ticketParameters.EndTime = this.EndTime.Value.ToUniversalTime();

			if (this.RenewableOk.IsSet)
				options |= KdcOptions.RenewableOK;
			if (this.Forwardable.IsSet)
				options |= KdcOptions.Forwardable;

			if (this.RenewTill.HasValue)
			{
				options |= KdcOptions.Renewable;
				ticketParameters.RenewTill = this.RenewTill.Value.ToUniversalTime();
			}

			if (options == 0)
				options = KerberosClient.DefaultTgtOptions;

			ticketParameters.Options = options;
			return ticketParameters;
		}
	}
}
