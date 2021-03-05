using Titanis.Cli;
using Titanis.Cli.Workflow;

namespace WorkflowSample
{
	[Command(HelpText = "Demonstrates using a CLI workflow")]
	internal class Program : Command
	{
		static void Main(string[] args)
			=> RunProgramAsync<Program>(args);

		protected override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			async Task DoTask(WorkflowContext context, CancellationToken cx)
			{
				await Task.Delay(1000, cx);
			}

			await this.ExecuteFrameAsync(async cx =>
			{
				Console.WriteLine("Execute a simple workflow with 3 steps");
				{
					Workflow wf = new Workflow();
					wf.AddStep(WorkflowFactory.Execute("Step 1", DoTask));
					wf.AddStep(WorkflowFactory.Execute("Step 2", DoTask));
					wf.AddStep(WorkflowFactory.Execute("Step 3", DoTask));
					await wf.ExecuteAsync(this.Log, cx);
				}
				Console.WriteLine();
			});

			Console.WriteLine("Execute a simple workflow with a failure with continuation");
			{
				Workflow wf = new Workflow();
				wf.AddStep(WorkflowFactory.Execute("Step 1", DoTask));
				wf.AddStep(WorkflowFactory.Execute("Step 2", async (ctx, cx) => { throw new Exception("General failure"); }, WorkflowStepOptions.ContinueOnError));
				wf.AddStep(WorkflowFactory.Execute("Step 3", DoTask));
				await wf.ExecuteAsync(this.Log, cancellationToken);
			}
			Console.WriteLine();

			Console.WriteLine("Execute a simple workflow with a failure with rollback");
			{
				Workflow wf = new Workflow();
				wf.AddStep(WorkflowFactory.Execute("Step 1", DoTask, "Rolling back step 2", DoTask));
				wf.AddStep(WorkflowFactory.Execute("Step 2", async (ctx, cx) => { throw new Exception("General failure"); }));
				wf.AddStep(WorkflowFactory.Execute("Step 3", DoTask));
				try
				{
					await wf.ExecuteAsync(this.Log, cancellationToken);
				}
				catch (WorkflowException ex)
				{
					Console.WriteLine("Workflow failed, rolling back");

					await wf.RollbackAsync(ex, cancellationToken, true);
				}
			}

			return 0;
		}
	}
}
