using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Titanis.Cli;

namespace Titanis.CodeAnalysis.Cli
{
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class CliAnalyzer : DiagnosticAnalyzer
	{
		public const string DiagnosticId = "TitanisCodeAnalysisCli";

		private static LocalizableString MakeLocalizableString(string name)
		{
			LocalizableString str = new LocalizableResourceString(name, Resources.ResourceManager, typeof(Resources));
			return str;
		}

		private const string Category = "Usability";

		private static readonly DiagnosticDescriptor MissingDescriptionRule = new DiagnosticDescriptor(
			DiagnosticId,
			MakeLocalizableString(nameof(Resources.MissingDescription_Title)),
			MakeLocalizableString(nameof(Resources.MissingDescription_Format)),
			Category,
			DiagnosticSeverity.Error, isEnabledByDefault: true,
			description: MakeLocalizableString(nameof(Resources.MissingDescription_Description)));
		private static readonly DiagnosticDescriptor MissingExampleRule = new DiagnosticDescriptor(
			DiagnosticId,
			MakeLocalizableString(nameof(Resources.MissingExample_Title)),
			MakeLocalizableString(nameof(Resources.MissingExample_Format)),
			Category,
			DiagnosticSeverity.Warning, isEnabledByDefault: true,
			description: MakeLocalizableString(nameof(Resources.MissingExample_Description)));

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(MissingDescriptionRule); } }

		public override void Initialize(AnalysisContext context)
		{
			context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
			if (!Debugger.IsAttached)
			{
				context.EnableConcurrentExecution();
			}

			// TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
			// See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
			context.RegisterSymbolAction(AnalyzeCommandType, SymbolKind.NamedType);
			context.RegisterSymbolAction(AnalyzeParamProperty, SymbolKind.Property);
		}

		private static void AnalyzeCommandType(SymbolAnalysisContext context)
		{
			var namedType = (INamedTypeSymbol)context.Symbol;

			var commandType = context.Compilation.GetTypeByMetadataName(typeof(Command).FullName);
			if (
				!namedType.IsAbstract
				&& namedType.TypeKind == TypeKind.Class
				&& namedType.IsSubclassOf(commandType)
				)
			{
				var descrAttrType = context.Compilation.GetTypeByMetadataName(typeof(DescriptionAttribute).FullName);

				bool hasDescr = namedType.IsDefined(descrAttrType);
				if (!hasDescr)
					context.ReportDiagnostic(Diagnostic.Create(MissingDescriptionRule, namedType.Locations[0], namedType.Name));


				var exampleAttrType = context.Compilation.GetTypeByMetadataName(typeof(ExampleAttribute).FullName);

				bool hasExample = namedType.IsDefined(exampleAttrType);
				if (!hasExample)
					context.ReportDiagnostic(Diagnostic.Create(MissingExampleRule, namedType.Locations[0], namedType.Name));
			}
		}

		private static void AnalyzeParamProperty(SymbolAnalysisContext context)
		{
			var property = (IPropertySymbol)context.Symbol;

			var paramAttrType = context.Compilation.GetTypeByMetadataName(typeof(ParameterAttribute).FullName);
			bool isParam = property.IsDefined(paramAttrType);

			if (isParam)
			{
				var descrAttrType = context.Compilation.GetTypeByMetadataName(typeof(DescriptionAttribute).FullName);

				bool hasDescr = property.IsDefined(descrAttrType);
				if (!hasDescr)
					context.ReportDiagnostic(Diagnostic.Create(MissingDescriptionRule, property.Locations[0], property.Name));
			}
		}
	}

	static class SyntaxHelpers
	{
		public static bool IsSubclassOf(this ITypeSymbol type, ITypeSymbol baseType)
		{
			do
			{
				if (SymbolEqualityComparer.Default.Equals(type, baseType))
					return true;
				type = type.BaseType;
			} while (type != null);

			return false;
		}

		public static bool IsDefined(this ISymbol symbol, ITypeSymbol attributeType)
		{
			var attributes = symbol.GetAttributes();
			foreach (var attr in attributes)
			{
				if (attributeType.IsSubclassOf(attr.AttributeClass))
					return true;
			}

			return false;
		}
	}
}
