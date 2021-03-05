using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Cli.Workflow
{
	/// <summary>
	/// Implements a command that executes a workflow.
	/// </summary>
	/// <remarks>
	/// Implement <see cref="PrepareWorkflow(CancellationToken)"/> to create the workflow
	/// to execute.
	/// </remarks>
	public abstract class WorkflowCommand : Command
	{
		[Parameter]
		[Description("Execute the workflow rollback phase")]
		public SwitchParam Rollback { get; set; }

		/// <summary>
		/// Called if the workflow fails.
		/// </summary>
		/// <param name="exception"><see cref="WorkflowException"/> describing the failure</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <remarks>
		/// Check <see cref="Rollback"/> to determine whether the error occurred during rollback.
		/// </remarks>
		protected virtual Task OnWorkflowFailed(WorkflowException? exception, CancellationToken cancellationToken) => Task.CompletedTask;
		protected virtual Task OnWorkflowCompleted(CancellationToken cancellationToken) => Task.CompletedTask;

		protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
		{
			var wf = await this.PrepareWorkflow(cancellationToken);

			if (wf != null)
			{
				if (!this.Rollback.IsSet)
				{
					WorkflowException? exception = null;
					try
					{
						await wf.ExecuteAsync(this.Log, cancellationToken);
					}
					catch (WorkflowException ex)
					{
						exception = ex;
					}

					if (exception != null)
					{
						await this.OnWorkflowFailed(exception, cancellationToken);

						bool rollback = false;
						while (true)
						{
							var resp = this.Context.Prompt("The workflow has failed.  Rollback? [y/N] ");
							if (resp.Equals("Y", StringComparison.OrdinalIgnoreCase))
							{
								rollback = true;
								break;
							}
							else if (string.IsNullOrEmpty(resp) || resp.Equals("N", StringComparison.OrdinalIgnoreCase))
							{
								break;
							}
						}

						if (rollback)
						{
							this.Rollback = new SwitchParam(SwitchParamFlags.Set);
							await wf.RollbackAsync(exception, cancellationToken, true);
						}
					}
				}
				else
				{
					await wf.RollbackAsync(this.Log, cancellationToken, true);
				}

				await this.OnWorkflowCompleted(cancellationToken);
			}

			return 0;
		}

		protected abstract Task<Workflow> PrepareWorkflow(CancellationToken cancellationToken);
	}
}
