using Titanis.Msrpc.Mstsch.Xml;

namespace Titanis.Msrpc.Mstsch
{
	public abstract class TaskAction
	{
		internal abstract Xml.actionBaseType ActionXml { get; }
		public override string ToString() => this.ActionXml.ToString();

		public abstract void Accept(ITaskActionVisitor visitor);

		public static ExecuteAction Execute(string command, string workingDirectory, string? args)
		{
			return new ExecuteAction(new Xml.execType
			{
				Command = command,
				WorkingDirectory = workingDirectory,
				Arguments = args
			});
		}
	}

	public interface ITaskActionVisitor
	{
		void Visit(ShowMessageAction action);
		void Visit(SendEmailAction action);
		void Visit(ComHandlerAction action);
		void Visit(ExecuteAction action);
	}

	public sealed class ShowMessageAction : TaskAction
	{
		private showMessageType showMessageType;

		internal ShowMessageAction(showMessageType showMessageType)
		{
			this.showMessageType = showMessageType;
		}

		internal sealed override Xml.actionBaseType ActionXml => this.showMessageType;

		public sealed override void Accept(ITaskActionVisitor visitor) => visitor.Visit(this);
	}

	public sealed class SendEmailAction : TaskAction
	{
		private sendEmailType sendEmailType;

		internal SendEmailAction(sendEmailType sendEmailType)
		{
			this.sendEmailType = sendEmailType;
		}

		internal sealed override Xml.actionBaseType ActionXml => this.sendEmailType;

		public sealed override void Accept(ITaskActionVisitor visitor) => visitor.Visit(this);
	}

	public sealed class ComHandlerAction : TaskAction
	{
		private comHandlerType comHandlerType;

		internal ComHandlerAction(comHandlerType comHandlerType)
		{
			this.comHandlerType = comHandlerType;
		}

		internal sealed override Xml.actionBaseType ActionXml => this.comHandlerType;

		public sealed override void Accept(ITaskActionVisitor visitor) => visitor.Visit(this);
	}

	public sealed class ExecuteAction : TaskAction
	{
		private execType execType;

		internal ExecuteAction(execType execType)
		{
			this.execType = execType;
		}

		internal sealed override Xml.actionBaseType ActionXml => this.execType;

		public sealed override void Accept(ITaskActionVisitor visitor) => visitor.Visit(this);
	}
}
