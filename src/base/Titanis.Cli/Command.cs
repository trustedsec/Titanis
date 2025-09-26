using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Reflection;

namespace Titanis.Cli
{
	/// <summary>
	/// Represents a command that accepts arguments.
	/// </summary>
	/// <remarks>
	/// To implement command behavior, override <see cref="RunAsync"/>.
	/// This method is called after parameters are parsed and initialized.
	/// <para>
	/// To implement custom parameter validation, implement <see cref="ValidateParameters(ParameterValidationContext)"/>.
	/// </para>
	/// </remarks>
	public abstract class Command : CommandBase, IValidateParameters
	{
		#region Common fields
		[Parameter]
		[Description("Determines the output style")]
		public OutputStyle? ConsoleOutputStyle { get; set; }

		[Parameter(ParameterFlags.OutputOnly)]
		[Description("Fields to display in output")]
		[ValueListProvider(typeof(FieldListProvider))]
		protected string[]? OutputFields { get; set; }
		#endregion

		/// <inheritdoc/>
		public sealed override Task<int> InvokeAsync(string command, Token[] args, int startIndex, CancellationToken cancellationToken)
		{
			if (args != null && args.Length > startIndex && args[startIndex].Text == "-?")
			{
				string helpText = this.GetHelpText(command, this.Context.MetadataContext);
				this.WriteMessage(helpText);
				return Task.FromResult(0);
			}
			else
			{
				// Check for environmental options
				{
					string optionsVar = command.Replace(' ', '_').ToUpper() + "_OPTIONS";
					var envOptions = this.Context.GetVariable(optionsVar);
					if (envOptions is string str)
					{
						try
						{
							this.WriteMessage($"Using options from environment {optionsVar}: {str}");
							var envTokens = CommandLineParser.Tokenize(str);
							var combinedArgs = new Token[envTokens.Length + args.Length];
							combinedArgs[0] = args[0];
							envTokens.CopyTo(combinedArgs, 1);
							args.Slice(1).CopyTo(combinedArgs.Slice(1 + envTokens.Length));
							args = combinedArgs;
						}
						catch (Exception ex)
						{
							this.WriteWarning($"Failed to parse environment options: {ex.Message}");
						}
					}
				}

				CommandMetadata metadata = GetCommandMetadata(this.GetType(), this.Context.MetadataContext);

				var validation = this.Parse(args, startIndex, metadata, true);
				if (validation.Errors.Count > 0)
					throw new SyntaxException(validation.GenerateMessage());

				try
				{
					if (metadata.OutputRecordType is not null)
					{
						var fields = OutputField.GetFieldsFor(metadata.OutputRecordType, this.Context.MetadataContext, this.OutputFields ?? metadata.DefaultOutputFields);
						this.SetOutputFormat(this.ConsoleOutputStyle ?? metadata.DefaultOutputStyle, fields);
					}
				}
				catch { }

				this.PrintBanner();
				return this.RunAsync(cancellationToken);
			}
		}

#if DEBUG
		private bool _baseValidateCalled;
#endif

		/// <inheritdoc/>
		void IValidateParameters.ValidateParameters(ParameterValidationContext context, ParameterGroupOptions options)
			=> this.ValidateParameters(context);
		/// <summary>
		/// Validates parameters before execution.
		/// </summary>
		/// <param name="context"><see cref="ParameterValidationContext"/> used to log errors</param>
		protected virtual void ValidateParameters(ParameterValidationContext context)
		{
#if DEBUG
			this._baseValidateCalled = true;
#endif
		}

		private static readonly string[] ParamColNames = new string[] { "Name", "Aliases", "Value", "Description" };


		/// <inheritdoc/>
		public sealed override void GetHelpText(IDocWriter writer, string commandName, CommandMetadataContext context) => BuildCommandHelpText(this.GetType(), writer, commandName, this, context);
		public static void BuildCommandHelpText(Type commandType, IDocWriter writer, string commandName, object? commandInstance, CommandMetadataContext context)
		{
			if (context is null) throw new ArgumentNullException(nameof(context));

			var desc = context.Resolver.GetCustomAttribute<DescriptionAttribute>(commandType, true)?.Description;

			writer
				.WriteBodyTextLine(desc)
				.AppendLine()
				.WriteHeading("Synopsis")
				;

			writer.BeginCodeBlock();
			writer.WriteBodyText($"{commandName}");

			var md = GetCommandMetadata(commandType, context);
			if (md.Parameters.Count > 0)
				writer.WriteBodyText(" [options]");
			foreach (var namedParam in md.ParametersByName.Values)
			{
				if (!namedParam.IsPositional && namedParam.IsMandatory)
				{
					if (namedParam.IsSwitch)
						writer.WriteBodyText($" -{namedParam.Name}");
					else
					{
						var ph = namedParam.Placeholder;
						if (string.IsNullOrEmpty(ph))
						{
							ph = namedParam.ElementType.Name;
							if (namedParam.IsList)
								ph += "...";
						}

						writer.WriteBodyText($" -{namedParam.Name} <{namedParam.Placeholder}>");
					}
				}
			}
			foreach (var posParam in md.PositionalParameters)
			{
				if (posParam.IsMandatory)
					writer.WriteBodyText($" <{posParam.Name}>");
				else
					writer.WriteBodyText($" [ <{posParam.Name}> ]");
			}
			writer.EndCodeBlock();


			HashSet<string> allParamNames = new HashSet<string>(md.ParametersByName.Keys);

			if (md.PositionalParameters.Count > 0)
			{
				writer.AppendLine().WriteHeading("Parameters");

				TextTable table = new TextTable() { LeftMargin = "  " };
				BuildParametersTable(table, md.PositionalParameters, allParamNames, commandInstance, context);

				writer.WriteTable(table, ParamColNames);
			}

			var namedParams = md.Parameters.Where(r => !r.IsPositional).ToList();
			if (namedParams.Count > 0)
			{
				writer.AppendLine().WriteHeading("Options").AppendLine();

				var groups = namedParams.GroupBy(r => r.Category);
				foreach (var group in groups)
				{
					if (!string.IsNullOrEmpty(group.Key))
					{
						writer.AppendLine().WriteSubheading($"{group.Key}");
					}

					TextTable table = new TextTable() { LeftMargin = Indent };
					BuildParametersTable(table, group, allParamNames, commandInstance, context);

					writer.WriteTable(table, "Name", "Aliases", "Value", "Description");
				}
			}

			var details = GetDetailedHelp(commandType, context);
			if (!string.IsNullOrEmpty(details))
			{
				writer.AppendLine().WriteHeading("Details").AppendLine().WriteBodyTextLine(string.Format(details, commandName));
			}

			var examples = GetExamples(commandType, context);
			if (examples.Count > 0)
			{
				writer.AppendLine().WriteHeading("Examples").AppendLine();

				int index = 0;
				foreach (var example in examples)
				{
					index++;

					writer.WriteSubheading($"Example {index} - {example.Caption}").AppendLine();
					writer.BeginCodeBlock();
					writer.WriteBodyTextLine(example.CommandLine.Replace("{0}", commandName) /* Use a simple Replace call instead of string.Format so that other { and } don't need to be escaped. string.Format(example.CommandLine, commandName) */ );
					writer.EndCodeBlock();

					if (!string.IsNullOrEmpty(example.Explanation))
						writer.WriteBodyTextLine(example.Explanation.Replace("{0}", commandName));
				}
			}
		}

		private static IList<ExampleAttribute> GetExamples(Type commandType, CommandMetadataContext context)
		{
			var examples = context.Resolver.GetCustomAttributes<ExampleAttribute>(commandType, true).ToList();
			return examples;
		}

		private static void BuildParametersTable(TextTable table, IEnumerable<ParameterMetadata> parameters, HashSet<string> allParamNames, object? commandInstance, CommandMetadataContext context)
		{
			foreach (var param in parameters)
			{
				var tr = table.AddRow();
				if (param.IsPositional)
				{
					tr.AddCell($"<{param.Name}>");
					tr.AddCell();
				}
				else
				{
					string prefix;
					{
						var initial = param.Name.Substring(0, 1);
						if (1 == allParamNames.Count(r => r.StartsWith(initial, StringComparison.OrdinalIgnoreCase)))
							prefix = $"-{initial}, ";
						else
							prefix = "    ";
					}

					tr.AddCell(prefix + '-' + param.Name);

					if (param.Aliases.Length > 0)
					{
						string alias0 = "-" + param.Aliases[0];
						tr.AddCell(alias0);
					}
					else
						tr.AddCell();
				}

				if (!string.IsNullOrEmpty(param.Placeholder))
					tr.AddCell($"<{param.Placeholder}>");
				else
					tr.AddCell();

				tr.AddCell(param.Description);

				if (param.HasDefaultValue)
				{
					table.AddRow(null, null, null, "  Default: " + (param.RawDefaultValue ?? "<null>"));
				}

				if (param.HasValueList)
				{
					var valueList = param.GetValueList(commandInstance, context);
					if (valueList != null)
					{
						table.AddRow();
						var pvHeaderRow = table.AddRow((TextTableCell?)null, null, null);
						pvHeaderRow.AddCell(new TextTableCell("Possible values:") { StyleOptions = TextStyleOptions.Bold });

						foreach (var value in valueList)
						{
							var valueRow = table.AddRow((TextTableCell?)null, null, null);
							valueRow.AddCell(new TextTableCell("  " + value?.ToString()));
						}

						table.AddRow();
					}
				}
			}
		}

		/// <summary>
		/// Runs the command implementation.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Result code of the command execution.</returns>
		/// <remarks>
		/// This method is called by <see cref="InvokeAsync(string, Token[], int, CancellationToken)"/>
		/// after parsing and setting parameters.
		/// </remarks>
		protected abstract Task<int> RunAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Gets the metadata describing the command and its parameters.
		/// </summary>
		/// <param name="commandType">Type implement a command (must inherit <see cref="Command"/>)</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="commandType"/> is <see langword="null"/></exception>
		/// <exception cref="ArgumentException"><paramref name="commandType"/> does not inherit <see cref="Command"/></exception>
		public static CommandMetadata GetCommandMetadata(Type commandType, CommandMetadataContext context)
		{
			return new CommandMetadata(commandType, context);
		}

		/// <summary>
		/// Gets a <see cref="TypeConverter"/> for a parameter type.
		/// </summary>
		/// <param name="paramType">Parameter type</param>
		/// <returns>A <see cref="TypeConverter"/> that can convert to <paramref name="paramType"/>.</returns>
		/// <exception cref="ArgumentException"><paramref name="paramType"/> does not have a <see cref="TypeConverter"/>.</exception>
		public static TypeConverter GetScalarParamConverter(Type paramType)
		{
			if (paramType.GetTypeInfo().IsEnum)
			{
				return new EnumConverter(paramType);
			}
			else
			{
				TypeCode typeCode = Type.GetTypeCode(paramType);
				switch (typeCode)
				{
					case TypeCode.Object:
						{
							var converter = TypeDescriptor.GetConverter(paramType);
							if (converter == null)
								throw new ArgumentException(string.Format(Messages.Cli_UnsupportedParamType, paramType.FullName));

							return converter;
						}
					case TypeCode.Boolean: return Singleton.SingleInstance<BooleanConverter>();
					case TypeCode.Char: return Singleton.SingleInstance<CharConverter>();
					case TypeCode.SByte: return new IntegerConverter<sbyte>(Singleton.SingleInstance<SByteConverter>(), sbyte.Parse, 8);
					case TypeCode.Byte: return new IntegerConverter<byte>(Singleton.SingleInstance<ByteConverter>(), byte.Parse, 8);
					case TypeCode.Int16: return new IntegerConverter<short>(Singleton.SingleInstance<Int16Converter>(), short.Parse, 16);
					case TypeCode.UInt16: return new IntegerConverter<ushort>(Singleton.SingleInstance<UInt16Converter>(), ushort.Parse, 16);
					case TypeCode.Int32: return new IntegerConverter<int>(Singleton.SingleInstance<Int32Converter>(), int.Parse, 32);
					case TypeCode.UInt32: return new IntegerConverter<uint>(Singleton.SingleInstance<UInt32Converter>(), uint.Parse, 32);
					case TypeCode.Int64: return new IntegerConverter<long>(Singleton.SingleInstance<Int64Converter>(), long.Parse, 64);
					case TypeCode.UInt64: return new IntegerConverter<ulong>(Singleton.SingleInstance<UInt64Converter>(), ulong.Parse, 64);
					case TypeCode.Single: return Singleton.SingleInstance<SingleConverter>();
					case TypeCode.Double: return Singleton.SingleInstance<DoubleConverter>();
					case TypeCode.Decimal: return Singleton.SingleInstance<DecimalConverter>();
					case TypeCode.DateTime: return Singleton.SingleInstance<DateTimeConverter>();
					case TypeCode.String: return Singleton.SingleInstance<StringConverter>();
					case TypeCode.Empty:
					//case TypeCode.DBNull:
					default:
						throw new ArgumentException(string.Format(Messages.Cli_UnsupportedParamType, paramType.FullName));
				}
			}
		}

		private ParameterValidationContext Parse(IList<Token> tokens, int startIndex, CommandMetadata metadata, bool useEnvDefaults)
		{
			Dictionary<ParameterMetadata, object?> setParams = new();

			ParameterConverterContext converterContext = new ParameterConverterContext(this);

			foreach (var group in metadata.ParameterGroups)
			{
				if (0 != (group.Options & ParameterGroupOptions.AlwaysInstantiate))
				{
					group.GetGroupObject(this, this);
				}
			}

			bool isFinalPos = false;
			bool endOfOptions = false;

			int positionalIndex = 0;
			for (int i = startIndex; i < tokens.Count; i++)
			{
				var token = tokens[i];
				if (!endOfOptions && token.OriginalText == "--'")
				{
					endOfOptions = true;
					continue;
				}

				string tokenText = token.Text;
				bool paramByName;

				ParameterMetadata parameter;
				string? argText;
				if ((tokenText.Length > 0) && (token.OriginalText.StartsWith("-")))
				{
					// This is a named parameter

					string paramName;

					{
						int sep = tokenText.IndexOf(':');
						if (sep > 0)
						{
							argText = tokenText.Substring(sep + 1);
							paramName = tokenText.Substring(1, sep - 1);
						}
						else
						{
							paramName = tokenText.Substring(1);
							argText = null;
						}
					}

					// Determine parameter
					if (!metadata.ParametersByName.TryGetValue(paramName, out parameter))
					{
						var matches = metadata.ParametersByName.Where(r => r.Key.StartsWith(paramName, StringComparison.OrdinalIgnoreCase)).ToArray();

						if (matches.Length == 1)
							parameter = matches[0].Value;
						else
							throw new UnrecognizedParameterException(paramName);
					}

					paramByName = true;

					if (!parameter.IsSwitch && argText == null)
					{
						// TODO: Support list arguments after :

						i++;
						if (i >= tokens.Count)
							throw new ParameterSyntaxException(paramName, string.Format(Messages.Cli_MissingParamValue, paramName));
						token = tokens[i];
						argText = token.Text;
					}
					else
					{
						// Don't consume argument
					}
				}
				else
				{
					// Positional parameter

					argText = tokenText;
					paramByName = false;

					isFinalPos = positionalIndex > metadata.PositionalParameters.Count;
					if (positionalIndex >= metadata.PositionalParameters.Count)
						throw new SyntaxException(Messages.Cli_TooManyArguments + ": " + argText);
					else
						parameter = metadata.PositionalParameters[positionalIndex];

					positionalIndex++;
				}

				object? argValue;
				if (parameter.IsSwitch)
				{
					argValue = argText == null ? new SwitchParam(SwitchParamFlags.Specified | SwitchParamFlags.Set) : SwitchParam.Parse(argText, false);
				}
				else
				{
					if (parameter.IsList)
					{
						List<object?> listArgs = new List<object?>();

						// Continue reading list parameters
						bool listCont = false;
						do
						{
							token = tokens[i];

							if (!listCont && !endOfOptions && token.Text.StartsWith("-"))
							{
								i--;
								break;
							}

							argText = token.Text;

							listCont = token.OriginalText.EndsWith(",");
							if (listCont)
								// Trim trailing ,
								argText = argText.Substring(0, argText.Length - 1);

							try
							{
								converterContext.Parameter = parameter;
								argValue = parameter.ConvertValue(argText, converterContext);
								listArgs.Add(argValue);
							}
							catch (Exception ex)
							{
								throw new ParameterSyntaxException(parameter.Name, string.Format(Messages.Cli_ArgFormatError, parameter.Name, tokenText, ex.Message), ex);
							}
						} while ((listCont || (!paramByName && parameter.IsList)) && ((++i) < tokens.Count));

						Array array = Array.CreateInstance(parameter.ElementType, listArgs.Count);
						for (int j = 0; j < array.Length; j++)
						{
							array.SetValue(listArgs[j], j);
						}
						argValue = array;
					}
					else
					{
						try
						{
							converterContext.Parameter = parameter;
							argValue = parameter.ConvertValue(argText, converterContext);
						}
						catch (Exception ex)
						{
							throw new ParameterSyntaxException(parameter.Name, string.Format(Messages.Cli_ArgFormatError, parameter.Name, tokenText, ex.Message), ex);
						}
					}
				}

				if (argValue != UnsetValue)
				{
					parameter.SetValue(this, argValue);
					setParams.Add(parameter, argValue);
				}
			}

			// Import environment defaults
			if (useEnvDefaults)
			{
				foreach (var param in metadata.Parameters)
				{
					if (!setParams.ContainsKey(param))
					{
						var envKey = $"TITANIS_DEFAULT_" + param.Name.ToUpper();
						var envValue = this.Context.GetVariable(envKey);
						if (envValue is not null)
						{
							if (param.IsList)
							{
								Array arrDefault;
								if (envValue is string str)
								{
									var defaultTokens = CommandLineParser.Tokenize(str);
									arrDefault = defaultTokens;
								}
								else if (envValue is Array arr)
								{
									arrDefault = arr;
								}
								else
								{
									arrDefault = new object[] { envValue };
								}

								Array arrCoerced = Array.CreateInstance(param.ElementType, arrDefault.Length);
								bool failed = false;
								for (int i = 0; i < arrDefault.Length; i++)
								{
									var defaultElem = arrDefault.GetValue(i);

									try
									{
										this.WriteMessage($"Importing default for '{param.Name}': {defaultElem}");
										converterContext.Parameter = param;
										var coerced = param.ConvertValue(defaultElem, converterContext);
										arrCoerced.SetValue(coerced, i);
									}
									catch (Exception ex)
									{
										failed = true;
										this.WriteWarning($"Failed to parse default value '{envValue}' for parameter '{param.Name}': {ex.Message}");
									}
								}

								if (!failed)
								{
									param.SetValue(this, arrCoerced);
									setParams.Add(param, arrCoerced);
								}
							}
							else
							{
								try
								{
									this.WriteMessage($"Importing default for '{param.Name}': {envValue}");
									converterContext.Parameter = param;
									var coerced = param.ConvertValue(envValue, converterContext);
									param.SetValue(this, coerced);
									setParams.Add(param, coerced);
								}
								catch (Exception ex)
								{
									this.WriteWarning($"Failed to parse default value '{envValue}' for parameter '{param.Name}': {ex.Message}");
								}
							}
						}
					}
				}
			}

			foreach (var param in metadata.Parameters)
			{
				if (!setParams.ContainsKey(param))
				{
					if (useEnvDefaults)
					{
						var envKey = $"TITANIS_DEFAULT_" + param.Name.ToUpper();
						var envValue = this.Context.GetVariable(envKey);
						if (envValue is not null)
						{
							try
							{
								converterContext.Parameter = param;
								var value = param.ConvertValue(envValue, converterContext);
							}
							catch (Exception ex)
							{
								this.WriteWarning($"Failed to parse default value '{envValue}' for parameter '{param.Name}': {ex.Message}");
							}
						}
					}

					if (param.IsMandatory)
						throw new MissingParameterException(param.Name);
					else if (param.HasDefaultValue)
						param.SetValue(this, param.DefaultValue);
				}
			}

			{
				var validateContext = new ParameterValidationContext();
				foreach (var group in metadata.ParameterGroups)
				{
					var groupObj = group.GetGroupObject(this, false);
					if (groupObj is IValidateParameters validator)
						validator.ValidateParameters(validateContext, group.Options);
				}

				return validateContext;
			}
		}

		private static object UnsetValue = new object();
	}
}
