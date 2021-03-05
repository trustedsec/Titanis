using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Titanis.Cli
{
    public static class HelpGenerator
    {
        private const string HELPFORMAT = "({0}) {1}:\n\tDescription: {2}";
        private const string MANDATORYHELP = "Mandatory Arguments\r\n";
        private const string OPTIONALHELP = "Optional Arguments:\r\n";
        private const string POSITIONALHELP = "Positional Arguments:\r\n";

        public static string GenerateFullHelp(Type commandType, StringBuilder fullHelp, CommandMetadataContext context, bool showLongHelp = false, string commandName = null)
        {
			if (context is null) throw new ArgumentNullException(nameof(context));

			var metadata = Command.GetCommandMetadata(commandType, context);
            
            var topHelp = new StringBuilder($"{metadata.Description}\r\n\r\n{commandName ?? "Arguments"}: ");
            var positionalHelp = new StringBuilder(POSITIONALHELP);
            var mandatoryHelp = new StringBuilder(MANDATORYHELP);
            var optionalHelp = new StringBuilder(OPTIONALHELP);
            foreach (var param in metadata.PositionalParameters)
            {
                topHelp.Append($"{param.Name} ");
                positionalHelp.AppendLine(GenerateHelpLine(param));
            }

            HashSet<string> handledNamed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var param in metadata.ParametersByName)
            {
                if (handledNamed.Contains(param.Value.Name))
                    continue;
                var optNames = metadata.ParametersByName.Where(m => string.Equals(m.Value.Name, param.Value.Name, StringComparison.OrdinalIgnoreCase)).Select(m => m.Key);
                foreach (var n in optNames)
                {
                    handledNamed.Add(n);
                }

                var aliases = optNames.Where(m => !string.Equals(m, param.Value.Name, StringComparison.OrdinalIgnoreCase)).ToArray();

                if (param.Value.IsPositional)
                {
                    continue;
                }
                else if (param.Value.IsMandatory)
                {
                    topHelp.Append($"-{param.Value.Name} ");
                    mandatoryHelp.AppendLine(GenerateHelpLine(param.Value, aliases.Length == 0 ? null : aliases));
                }
                else
                {
                    
                    topHelp.Append($"[-{param.Value.Name}] ");
                    optionalHelp.AppendLine(GenerateHelpLine(param.Value, aliases.Length == 0 ? null : aliases));
                }
            }


            fullHelp.AppendLine(topHelp.ToString()).AppendLine();
            if (positionalHelp.Length > POSITIONALHELP.Length)
            {
                fullHelp.AppendLine(positionalHelp.ToString());
            }

            if (mandatoryHelp.Length > MANDATORYHELP.Length)
            {
                fullHelp.AppendLine(mandatoryHelp.ToString());
            }

            if (optionalHelp.Length > OPTIONALHELP.Length)
            {
                fullHelp.AppendLine(optionalHelp.ToString());
            }

            if (metadata.longHelp != null && showLongHelp)
            {
                fullHelp.AppendLine(metadata.ToString());
            }

            return fullHelp.ToString();
        }

        private static string GenerateHelpLine(ParameterMetadata param, string[]? aliases = null)
        {

            string helpline = String.Format(HELPFORMAT,
                param.ParameterType,
                !param.IsPositional ? "-" + param.Name : param.Name,
                //(width < MinConsoleWidth) ? param.Description ?? "No Description Provided" : PrettyDescription(param.Description, width));
                param.Description ?? Messages.Cli_NoDescriptionProvided);
            if (!aliases.IsNullOrEmpty())
            {
                helpline += $"\n\tAliases: -{String.Join(", -", aliases)}";
            }

            return helpline;
        }
    }

}