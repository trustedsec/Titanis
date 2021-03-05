using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Cli.Workflow
{
	/// <summary>
	/// Thrown when a workflow execution fails.
	/// </summary>
	[Serializable]
	public class WorkflowException : Exception
	{
		public WorkflowException(string message, int failedStepIndex, WorkflowContext context, Exception inner)
			: base(message, inner)
		{
			this.FailedStepIndex = failedStepIndex;
			this.WorkflowContext = context;
			if (inner != null)
				this.HResult = inner.HResult;
		}

		protected WorkflowException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

		public int FailedStepIndex { get; }
		public WorkflowContext WorkflowContext { get; }
	}
}
