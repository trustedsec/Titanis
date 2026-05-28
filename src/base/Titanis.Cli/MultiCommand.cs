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
				string helpText = this.GetHelpText(command, context.MetadataContext);
				context.WriteMessage(helpText);
				return Task.FromResult(0);
			}
			else if (args.Length > startIndex && IsZshCompletionRequest(args[startIndex].Text))
			{
				string rc = this.GetZshCompletionScript(command, context.MetadataContext);
				Console.WriteLine(rc);
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
		public sealed override void GetHelpText(IDocWriter writer, string commandName, CommandMetadataContext context) => BuildCommandHelpText(this.GetType().GetTypeInfo(), writer, commandName, context);
		public static void BuildCommandHelpText(TypeInfo commandType, IDocWriter writer, string commandName, CommandMetadataContext context)
		{
			if (context is null) throw new ArgumentNullException(nameof(context));

			var desc = context.Resolver.GetCustomAttribute<DescriptionAttribute>(commandType, true)?.Description;

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
			Array.Sort(attrs, (x, y) => x.Name.CompareTo(y.Name));
			TextTable tbl = new TextTable() { LeftMargin = "  " };
			foreach (var attr in attrs)
			{
				var submd = Command.GetCommandMetadata(attr.CommandType, context);
				var subdesc = submd.Description;
				tbl.AddRow($"##doc[link;{attr.Name};#{commandName} {attr.Name}]", subdesc);
			}
			writer.WriteTable(tbl, "Command", "Description");
			writer.AppendLine().WriteBodyTextLine($"For help on a subcommand, use `{commandName} <subcommand> -h`");
		}

		public override void GetZshCompletionScript(TextWriter writer, string commandName, string prefix, CommandMetadataContext context)
		{
			if (context is null) throw new ArgumentNullException(nameof(context));

			var commandType = this.GetType();

			var desc = context.Resolver.GetCustomAttribute<DescriptionAttribute>(commandType, true)?.Description;

			writer.WriteLine(@$"{prefix}() {{
  typeset -A opt_args
  local context state line
  local curcontext=""$curcontext""
  local ret=1

  _arguments -C -A ""-*"" '1: :_Smb2Client_subcommands' '*::args:->args' && ret=0
  case ""$state"" in
  (args)
    local subcmd=${{words[1]}}
    if (( $+functions[{prefix}_${{subcmd}}] ))
    then
      {prefix}_${{subcmd}} && ret=0
    fi
  esac

  return ret
}}

{prefix}_subcommands() {{
  local -a commands=(");

			SubcommandAttribute[] attrs = context.Resolver.GetCustomAttributes<SubcommandAttribute>(commandType, true).ToArray();
			var submds = new CommandMetadata[attrs.Length];
			for (int i = 0; i < attrs.Length; i++)
			{
				SubcommandAttribute? attr = attrs[i];
				var submd = Command.GetCommandMetadata(attr.CommandType, context);
				submds[i] = submd;
				var subdesc = submd.Description;

				writer.WriteLine($"    '{attr.Name}:{subdesc}'");
			}

			writer.WriteLine(@"  )

  _describe -t commands 'command' commands ""$@""
}
");

			for (int i = 0; i < attrs.Length; i++)
			{
				var attr = attrs[i];
				var cmd = Activator.CreateInstance(attr.CommandType) as CommandBase;
				if (cmd is null)
					continue;

				cmd.GetZshCompletionScript(writer, attr.Name, $"{prefix}_{attr.Name}", context);
			}

		}
	}
}
