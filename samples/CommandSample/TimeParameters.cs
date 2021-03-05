using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis;
using Titanis.Cli;

namespace CommandSample
{
	internal class TimeParameters
	{
		[Parameter]
		[Description("Duration to run the loop")]
		[DefaultValue("5s")]
		public Duration? Duration { get; set; }
	}
}
