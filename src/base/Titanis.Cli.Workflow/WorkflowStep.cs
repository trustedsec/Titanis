using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Cli.Workflow
{
	/// <summary>
	/// Specifies the behavior of a <see cref="WorkflowStep"/>.
	/// </summary>
	[Flags]
	public enum WorkflowStepOptions
	{
		/// <summary>
		/// Default behavior
		/// </summary>
		None = 0,

		/// <summary>
		/// Continues the workflow even if this step fails.
		/// </summary>
		ContinueOnError = 1,

		/// <summary>
		/// Skip the step during rollback.
		/// </summary>
		SkipOnRollback = 2,
	}

	/// <summary>
	/// Provides factory methods to create workflow steps.
	/// </summary>
	public static class WorkflowFactory
	{
		/// <summary>
		/// Creates a workflow step that executes a delegate.
		/// </summary>
		/// <param name="description">Description</param>
		/// <param name="action">Delegate to execute</param>
		/// <param name="options">Workflow options</param>
		/// <returns>The created <see cref="WorkflowStep"/></returns>
		/// <exception cref="ArgumentException"><paramref name="description"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="action"/> is <see langword="null"/>.</exception>
		public static WorkflowStep Execute(
			string description, Func<WorkflowContext, CancellationToken, Task> action,
			WorkflowStepOptions options = WorkflowStepOptions.None)
		{
			if (string.IsNullOrEmpty(description)) throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));
			if (action is null) throw new ArgumentNullException(nameof(action));

			return new WorkflowDelegateStep(description, action, options);
		}
		/// <summary>
		/// Creates a workflow step that executes a delegate and supports rollback.
		/// </summary>
		/// <param name="description">Description</param>
		/// <param name="action">Delegate to execute</param>
		/// <param name="rollbackMessage">Message to display during rollback</param>
		/// <param name="rollbackAction">Delegate to execute during rollback</param>
		/// <param name="options">Workflow options</param>
		/// <returns>The created <see cref="WorkflowStep"/></returns>
		/// <exception cref="ArgumentException"><paramref name="description"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="action"/> is <see langword="null"/>.</exception>
		public static WorkflowStep Execute(
			string description, Func<WorkflowContext, CancellationToken, Task> action,
			string rollbackMessage, Func<WorkflowContext, CancellationToken, Task> rollbackAction,
			WorkflowStepOptions options = WorkflowStepOptions.None)
		{
			if (string.IsNullOrEmpty(description)) throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));
			if (action is null) throw new ArgumentNullException(nameof(action));
			if (string.IsNullOrEmpty(rollbackMessage)) throw new ArgumentException($"'{nameof(rollbackMessage)}' cannot be null or empty.", nameof(rollbackMessage));
			if (rollbackAction is null) throw new ArgumentNullException(nameof(rollbackAction));

			return new WorkflowDelegateRollbackStep(description, action, rollbackMessage, rollbackAction, options);
		}
	}

	/// <summary>
	/// Represents a workflow step.
	/// </summary>
	/// <seealso cref="WorkflowFactory"/>
	public abstract class WorkflowStep
	{
		/// <summary>
		/// Initializes a new <see cref="WorkflowStep"/>.
		/// </summary>
		/// <param name="options"></param>
		protected WorkflowStep(WorkflowStepOptions options)
		{
			this.Options = options;
		}

		/// <summary>
		/// Gets a <see cref="WorkflowStepOptions"/> that specifies behavior of this step.
		/// </summary>
		public WorkflowStepOptions Options { get; }
		/// <summary>
		/// Indicates whether the workflow continues if this step fails.
		/// </summary>
		public bool ContinueOnError => 0 != (this.Options & WorkflowStepOptions.ContinueOnError);
		/// <summary>
		/// Indicates whether this step is skipped during rollback.
		/// </summary>
		public bool SkipOnRollback => 0 != (this.Options & WorkflowStepOptions.SkipOnRollback);

		/// <summary>
		/// Gets a description of the step to display when executing.
		/// </summary>
		public abstract string Description { get; }

		/// <summary>
		/// Indicates whether the step supports rollback.
		/// </summary>
		public virtual bool SupportsRollback => false;
		/// <summary>
		/// Gets the message to display for this step during rollback.
		/// </summary>
		public virtual string? RollbackMessage => null;

		internal Task ExecuteAsyncInternal(WorkflowContext context, CancellationToken cancellationToken)
			=> this.ExecuteAsync(context, cancellationToken);
		/// <summary>
		/// Executes the step.
		/// </summary>
		/// <param name="context">Workflow context</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the request.</param>
		protected abstract Task ExecuteAsync(WorkflowContext context, CancellationToken cancellationToken);

		internal Task RollbackAsyncInternal(WorkflowContext context, CancellationToken cancellationToken)
			=> this.RollbackAsync(context, cancellationToken);
		/// <summary>
		/// Executes the rollback for the step.
		/// </summary>
		/// <param name="context">Workflow context</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the request.</param>
		protected virtual Task RollbackAsync(WorkflowContext context, CancellationToken cancellationToken)
		{
			return Task.FromException(new InvalidOperationException("The workflow step does not support rollback."));
		}
	}

	internal sealed class WorkflowDelegateRollbackStep : WorkflowDelegateStep
	{
		internal WorkflowDelegateRollbackStep(
			string description,
			Func<WorkflowContext, CancellationToken, Task> executeFunc,
			string rollbackMessage,
			Func<WorkflowContext, CancellationToken, Task> rollbackFunc,
			WorkflowStepOptions options
			) :
			base(description, executeFunc, options)
		{
			if (string.IsNullOrEmpty(rollbackMessage)) throw new ArgumentException($"'{nameof(rollbackMessage)}' cannot be null or empty.", nameof(rollbackMessage));
			if (rollbackFunc is null) throw new ArgumentNullException(nameof(rollbackFunc));

			this.RollbackMessage = rollbackMessage;
			this._rollbackFunc = rollbackFunc;
		}

		public sealed override string RollbackMessage { get; }

		private readonly Func<WorkflowContext, CancellationToken, Task> _rollbackFunc;

		public sealed override bool SupportsRollback => true;
		protected sealed override async Task RollbackAsync(WorkflowContext context, CancellationToken cancellationToken)
		{
			await this._rollbackFunc(context, cancellationToken).ConfigureAwait(false);
		}
	}
	internal class WorkflowDelegateStep : WorkflowStep
	{
		internal WorkflowDelegateStep(
			string description,
			Func<WorkflowContext, CancellationToken, Task> executeFunc,
			WorkflowStepOptions options
			)
			: base(options)
		{
			this.Description = description;
			this._executeFunc = executeFunc;
		}

		public sealed override string Description { get; }
		private readonly Func<WorkflowContext, CancellationToken, Task> _executeFunc;

		protected sealed override async Task ExecuteAsync(WorkflowContext context, CancellationToken cancellationToken)
		{
			await this._executeFunc(context, cancellationToken).ConfigureAwait(false);
		}
	}
}
