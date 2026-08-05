using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
		protected sealed override Task<int> InvokeAsync(string command, Token[] args, int startIndex, CancellationToken cancellationToken)
		{
			Debug.Assert(this.Context != null);
			var context = this.Context!;

			if (args != null && ((args.Length <= startIndex) || (args.Length > startIndex && IsDistressCall(args[startIndex].Text))))
			{
				this.PrintHelpText(command, context.MetadataContext);
				return Task.FromResult(0);
			}
			else if (args is null)
				throw new ArgumentNullException(nameof(args));

			if (startIndex < args.Length)
			{
				string subcmdName = args[startIndex].Text;
				var subcmd = this.TryGetSubcommand(subcmdName);
				if (subcmd != null)
				{
					return subcmd.InvokeAsync(context, command + " " + subcmdName, args, startIndex + 1, cancellationToken);
				}
				else
				{
					this.WriteError($"The subcommand `{subcmdName}` is not implemented.  Use -h to see a list of available subcommands.");
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
		public sealed override void PrintHelpText(IDocWriter writer, string commandName, CommandMetadataContext context) => BuildCommandHelpText(this.GetType().GetTypeInfo(), writer, commandName, context, CommandHelpOptions.Default);
		public static void BuildCommandHelpText(Type commandType_, IDocWriter writer, string commandName, CommandMetadataContext context, CommandHelpOptions options)
		{
			if (context is null) throw new ArgumentNullException(nameof(context));

			var commandTypeDescr = context.Resolver.GetDescriptor(commandType_);
			var commandAttrs = commandTypeDescr.GetAttributes().OfType<Attribute>().ToArray();
			var desc = commandAttrs.OfType<DescriptionAttribute>().FirstOrDefault()?.Description;

			if (0 != (options & CommandHelpOptions.Description))
			{
				writer
					.WriteLine(desc)
					.WriteLine();
			}

			writer
				.WriteHeading("Synopsis")
				;

			writer.BeginCodeBlock();
			writer.WriteText($"{commandName} <subcommand>");
			writer.EndCodeBlock();
			writer.WriteLine();

			writer.WriteSubheading("Subcommands");

			SubcommandAttribute[] attrs = commandAttrs.OfType<SubcommandAttribute>().ToArray();
			Array.Sort(attrs, (x, y) => x.Name.CompareTo(y.Name));
			TextTable tbl = new TextTable() { LeftMargin = "  " };
			foreach (var attr in attrs)
			{
				var submd = Command.GetCommandMetadata(attr.CommandType, context);
				var subdesc = submd.Description;

				tbl.AddRow(FormattedTextFactory.Builder()
					.Bold()
					.Link(attr.Name, $"#{commandName.ToLower()}-{attr.Name.ToLower()}")
					.PopStyle().Build(), new FormattedText(subdesc));
			}
			writer.WriteTable(tbl, "Command", "Description");
			writer.WriteLine().WriteLine($"For help on a subcommand, use `{commandName} <subcommand> -h`");
		}
	}
}
