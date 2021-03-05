using System.ComponentModel;
using Titanis.Cli;

namespace CommandSample
{
	[Command(HelpText = "Sample command implementation")]
	internal class Program : Command
	{
		static int Main(string[] args)
			=> RunProgramAsync<Program>(args);

		[Parameter(10)]
		[Mandatory]
		[Category(ParameterCategories.Output)]
		[DescriptionResource(typeof(Messages), nameof(Messages.ResourceManager))]
		public string Message { get; set; }

		[Parameter]
		[Category(ParameterCategories.Output)]
		[Description("Number of times to print the message")]
		[DefaultValue(1)]
		public int Count { get; set; }

		[Parameter]
		[Description("Loops continuously")]
		public SwitchParam Loop { get; set; }

		[ParameterGroup]
		public TimeParameters? TimeParams { get; set; }

		protected sealed override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (this.Count < 0)
				context.LogError(new ParameterValidationError(nameof(Count), "The value must be positive."));
		}

		protected async sealed override Task<int> RunAsync(CancellationToken cancellationToken)
		{
			await Task.Yield();

			if (this.Loop.IsSet)
				this.WriteWarning("Running in loop mode.  Press CTRL+C to end.");

			DateTime stopTime = DateTime.MaxValue;
			if (this.TimeParams?.Duration != null)
			{
				stopTime = DateTime.Now + this.TimeParams.Duration.TimeSpan;
			}

			do
			{
				if (DateTime.Now >= stopTime)
					break;

				for (int i = 0; i < this.Count; i++)
				{
					this.WriteMessage(this.Message);
				}
			} while (this.Loop.IsSet && !cancellationToken.IsCancellationRequested);
			return 0;
		}
	}
}
