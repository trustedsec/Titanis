using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Cli
{
	/// <summary>
	/// Implements a command that accepts subcommands.
	/// </summary>
	/// <remarks>
	/// A derived class specifies subcommands with the <see cref="SubcommandAttribute"/>.
	/// </remarks>
	public abstract class MultiCommand : CommandBase
	{
		/// <inheritdoc/>
		public sealed override Task<int> InvokeAsync(string command, Token[] args, int startIndex, CancellationToken cancellationToken)
		{
			if (args != null && ((args.Length <= startIndex) || (args.Length > startIndex && args[startIndex].Text == "-?")))
			{
				string helpText = this.GetHelpText(command, this.Context.MetadataContext);
				this.Context.WriteMessage(helpText);
				return Task.FromResult(0);
			}
			else

			if (args is null)
				throw new ArgumentNullException(nameof(args));
			if (startIndex < args.Length)
			{
				string subcmdName = args[startIndex].Text;
				CommandBase subcmd = this.TryGetSubcommand(subcmdName);
				if (subcmd != null)
				{
					return subcmd.InvokeAsync(this.Context, command + " " + subcmdName, args, startIndex + 1, cancellationToken);
				}
				else
				{
					this.WriteError($"The subcommand `{subcmdName}` is not implemented.  Use -? to see a list of available subcommands.");
					return Task.FromResult<int>(-1);
				}
			}
			throw new ArgumentException(Messages.Cli_NoSubcommandProvided);
		}

		/// <summary>
		/// Gets the subcommand implementation.
		/// </summary>
		/// <param name="subcmdName">Name of subcommand to get</param>
		/// <returns>An instance of <see cref="CommandBase"/> that implements <paramref name="subcmdName"/></returns>
		/// <remarks>
		/// This implementation checks for <see cref="SubcommandAttribute"/>s applied to the class
		/// and instantiates the attribute matching <paramref name="subcmdName"/>.
		/// </remarks>
		protected virtual CommandBase? TryGetSubcommand(string subcmdName)
		{
			SubcommandAttribute[] attrs = this.GetType().GetTypeInfo().GetCustomAttributes<SubcommandAttribute>().ToArray();
			foreach (var attr in attrs)
			{
				if (subcmdName.Equals(attr.Name, StringComparison.OrdinalIgnoreCase))
					return Activator.CreateInstance(attr.CommandType) as CommandBase;
			}

			return null;
		}

		/// <inheritdoc/>
		public sealed override void GetHelpText(IDocWriter writer, string commandName, CommandMetadataContext context) => BuildCommandHelpText(this.GetType().GetTypeInfo(), writer, commandName, context);
		public static void BuildCommandHelpText(TypeInfo commandType, IDocWriter writer, string commandName, CommandMetadataContext context)
		{
			if (context is null) throw new ArgumentNullException(nameof(context));

			string desc = context.Resolver.GetCustomAttribute<DescriptionAttribute>(commandType, true)?.Description;

			writer
				.WriteBodyTextLine(desc)
				.AppendLine()
				.WriteHeading("Synopsis")
				;

			writer.BeginCodeBlock();
			writer.WriteBodyText($"{commandName} <subcommand>");
			writer.EndCodeBlock();
			writer.AppendLine();

			writer.WriteSubheading("Subcommands");

			SubcommandAttribute[] attrs = context.Resolver.GetCustomAttributes<SubcommandAttribute>(commandType, true).ToArray();
			TextTable tbl = new TextTable() { LeftMargin = "  " };
			foreach (var attr in attrs)
			{
				var submd = Command.GetCommandMetadata(attr.CommandType, context);
				var subdesc = submd.Description;
				tbl.AddRow($"##doc[link;{attr.Name};#{commandName} {attr.Name}]", subdesc);
			}
			writer.WriteTable(tbl, "Command", "Description");
			writer.AppendLine().WriteBodyTextLine($"For help on a subcommand, use `{commandName} <subcommand> -?`");
		}
	}
}
