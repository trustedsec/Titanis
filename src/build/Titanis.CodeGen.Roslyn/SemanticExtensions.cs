using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Titanis.CodeGen
{
	public static class SemanticExtensions
	{
		public static Diagnostic Create(this DiagnosticDescriptor descriptor,
			Location location,
			params object?[] args
			)
			=> Diagnostic.Create(descriptor, location, args);

		public static bool IsDefined(this ISymbol symbol, Type attributeType)
		{
			var isDefined = symbol.GetAttributes()
				.Any(r => r.AttributeClass.Matches(attributeType));
			return isDefined;
		}

		public static AttributeData? TryGetAttribute(this ISymbol symbol, Type attributeType)
		{
			var attr = symbol.GetAttributes()
				.FirstOrDefault(r => r.AttributeClass.Matches(attributeType));
			return attr;
		}

		public static bool TryGetArgument<TValue>(this AttributeData? attribute, string argName, out TValue value)
		{
			if (attribute != null)
			{
				foreach (var arg in attribute.NamedArguments)
				{
					if (arg.Key.Equals(argName, StringComparison.Ordinal))
					{
						if (arg.Value.Value is TValue typedValue)
						{
							value = typedValue;
							return true;
						}
					}
				}
			}

			value = default;
			return false;
		}

		public static ISymbol? TryGetArgument(this AttributeData? attribute,
			string argName,
			INamedTypeSymbol containingType,
			ISymbol member,
			SourceProductionContext context)
		{
			if (attribute.TryGetArgument(argName, out string memberName))
			{
				var argNode = attribute.NamedArgNode(argName, CancellationToken.None);
				if (argNode != null)
				{
					var expr = argNode.Expression;
					if (!expr.IsNameOfExpression())
					{
						context.ReportDiagnostic(Diagnostic.Create(
							Diagnostics.MemberRefNotNameof_Type_Member_AttrType_AttributeArg_Member,
							(argNode?.Expression ?? attribute.ApplicationSyntaxReference.GetSyntax(context.CancellationToken)).GetLocation(),
							containingType.FullName(), member.Name, attribute.AttributeClass.Name, argName, memberName
							));
					}
				}

				var namedMember = containingType.GetMembers().FirstOrDefault(r => r.Name == memberName);
				if (namedMember == null)
				{
					context.ReportDiagnostic(Diagnostic.Create(
						Diagnostics.UndefinedMemberRef_Type_Member_AttrType_AttributeArg_Member,
						(argNode?.Expression ?? attribute.ApplicationSyntaxReference.GetSyntax(context.CancellationToken)).GetLocation(),
						containingType.FullName(), member.Name, attribute.AttributeClass.Name, argName, memberName
						));
				}
				return namedMember;
			}

			return null;
		}

		public static ITypeSymbol? DataType(this ISymbol member)
		{
			return (member is IFieldSymbol field) ? field.Type
				: (member is IPropertySymbol prop) ? prop.Type
				: (member is IMethodSymbol method) ? method.ReturnType
				: null;
		}

		public static ISymbol? TryGetArgument(this AttributeData? attribute,
			int argIndex,
			INamedTypeSymbol containingType,
			ISymbol member,
			SourceProductionContext context)
		{
			if (attribute.TryGetArgument(argIndex, out string memberName))
			{
				var argNode = attribute.NamedArgNode(argIndex, CancellationToken.None);
				if (argNode != null)
				{
					var expr = argNode.Expression;
					if (!expr.IsNameOfExpression())
					{
						context.ReportDiagnostic(Diagnostic.Create(
							Diagnostics.MemberRefNotNameof_Type_Member_AttrType_AttributeArg_Member,
							(argNode?.Expression ?? attribute.ApplicationSyntaxReference.GetSyntax(context.CancellationToken)).GetLocation(),
							containingType.FullName(), member.Name, attribute.AttributeClass.Name, "#" + argIndex, memberName
							));
					}
				}

				var namedMember = containingType.GetMembers().FirstOrDefault(r => r.Name == memberName);
				if (namedMember == null)
				{
					context.ReportDiagnostic(Diagnostic.Create(
						Diagnostics.UndefinedMemberRef_Type_Member_AttrType_AttributeArg_Member,
						(argNode?.Expression ?? attribute.ApplicationSyntaxReference.GetSyntax(context.CancellationToken)).GetLocation(),
						containingType.FullName(), member.Name, attribute.AttributeClass.Name, "#" + argIndex, memberName
						));
				}
				return namedMember;
			}

			return null;
		}

		public static SyntaxNode ArgSyntaxOrAttribute(this AttributeSyntax attribute, int argIndex) => argIndex < attribute.ArgumentList.Arguments.Count ? attribute.ArgumentList.Arguments[argIndex] : attribute;
		public static SyntaxNode ArgSyntaxOrAttribute(this AttributeData attribute, int argIndex) => ((AttributeSyntax)attribute.ApplicationSyntaxReference.GetSyntax()).ArgSyntaxOrAttribute(argIndex);

		public static bool TryGetArgument<TValue>(this AttributeData? attribute, int argIndex, out TValue value)
		{
			if (attribute != null)
			{
				if (argIndex < attribute.ConstructorArguments.Length)
				{
					var arg = attribute.ConstructorArguments[argIndex];
					if (arg.Value is TValue typedValue)
					{
						value = typedValue;
						return true;
					}
				}
			}

			value = default;
			return false;
		}

		public static object? ConstructorArg(this AttributeData attribute, int argIndex)
		{
			if (attribute != null
				&& attribute.ConstructorArguments.Length > argIndex
				)
			{
				var arg = attribute.ConstructorArguments[argIndex];
				return arg.Value;
			}
			else
				return default;
		}

		public static string? GetSimpleName(this NameSyntax? name)
		{
			return name switch
			{
				SimpleNameSyntax simp => simp.Identifier.Text,
				QualifiedNameSyntax qual => qual.Right.Identifier.Text,
				_ => null
			};
		}

		public static bool HasModifier(this MemberDeclarationSyntax? memberDecl, SyntaxKind kind)
			=> memberDecl.Modifiers.Any(r => r.IsKind(kind));

		public static bool IsPartial(this MemberDeclarationSyntax? memberDecl)
			=> memberDecl.HasModifier(SyntaxKind.PartialKeyword);

		public static string? FullName(this INamespaceSymbol ns)
		{
			if (ns.IsGlobalNamespace)
				return null;
			else
			{
				var containingNS = ns.ContainingNamespace.FullName();
				var fullName =
					string.IsNullOrEmpty(containingNS) ? ns.Name
					: (containingNS + '.' + ns.Name);
				return fullName;
			}
		}

		/// <summary>
		/// Gets the full name of a type symbol.
		/// </summary>
		/// <param name="type">Type symbol of interest</param>
		/// <returns></returns>
		public static string? FullName(this ITypeSymbol type)
		{
			if (type.TypeKind == TypeKind.Array)
				return ((IArrayTypeSymbol)type).ElementType.FullName() + "[]";
			else if (type.TypeKind == TypeKind.TypeParameter)
				return type.Name;
			else
			{
				var ns = type.ContainingNamespace?.FullName();
				return string.IsNullOrEmpty(ns)
					? type.Name
					: (ns + '.' + type.Name);
			}
		}

		/// <summary>
		/// Tests whether a <see cref="ITypeSymbol"/> matches a <see cref="Type"/>.
		/// </summary>
		/// <param name="typeSymbol"><see cref="ITypeSymbol"/> to test</param>
		/// <param name="matchType"><see cref="Type"/> to test against</param>
		/// <returns><see langword="true"/> if <paramref name="typeSymbol"/> refers to <paramref name="matchType"/>; otherwise, <see langword="false"/>.</returns>
		public static bool Matches(this ITypeSymbol typeSymbol, Type matchType)
			=> typeSymbol is INamedTypeSymbol type
			&& (type.FullName() == matchType.FullName);

		/// <summary>
		/// Gets a named argument, if specified.
		/// </summary>
		/// <param name="attribute">Attribute</param>
		/// <param name="argName">Argument name of interest</param>
		/// <param name="cancellationToken">Cancellation token that make be used to cancel the operation</param>
		/// <returns>A <see cref="AttributeArgumentListSyntax"/> representing the named argument, if specified; otherwise, <see langword="null"/></returns>
		public static AttributeArgumentSyntax? NamedArgNode(this AttributeData attribute, string argName, CancellationToken cancellationToken)
		{
			var app = attribute.ApplicationSyntaxReference.GetSyntax(cancellationToken) as AttributeSyntax;
			if (app?.ArgumentList != null)
			{
				foreach (var arg in app.ArgumentList.Arguments)
				{
					var name = arg.NameEquals?.Name?.Identifier.ValueText;
					if (name.Equals(argName, StringComparison.Ordinal))
						return arg;
				}
			}

			return null;
		}

		/// <summary>
		/// Gets a named argument, if specified.
		/// </summary>
		/// <param name="attribute">Attribute</param>
		/// <param name="argIndex">Argument of interest</param>
		/// <param name="cancellationToken">Cancellation token that make be used to cancel the operation</param>
		/// <returns>A <see cref="AttributeArgumentListSyntax"/> representing the named argument, if specified; otherwise, <see langword="null"/></returns>
		public static AttributeArgumentSyntax? NamedArgNode(this AttributeData attribute, int argIndex, CancellationToken cancellationToken)
		{
			var app = attribute.ApplicationSyntaxReference.GetSyntax(cancellationToken) as AttributeSyntax;
			if (app?.ArgumentList != null)
			{
				// TODO: This may retrieve named arguments

				if (argIndex < app.ArgumentList.Arguments.Count)
				{
					return app.ArgumentList.Arguments[argIndex];
				}
			}

			return null;
		}

		/// <summary>
		/// Gets the location for a <see cref="SyntaxNode"/>.
		/// </summary>
		/// <param name="node">Node of interest</param>
		/// <returns>A <see cref="Location"/> describing <paramref name="node"/>.</returns>

		public static Location GetLocation(this SyntaxReference node)
			=> Location.Create(node.SyntaxTree, node.Span);

		/// <summary>
		/// Gets the location for a <see cref="SyntaxNode"/>.
		/// </summary>
		/// <param name="node">Node of interest</param>
		/// <returns>A <see cref="Location"/> describing <paramref name="node"/>.</returns>
		public static Location GetLocation(this SyntaxNode node)
			=> Location.Create(node.SyntaxTree, node.Span);

		/// <summary>
		/// Determines whether an expression uses <see langword="nameof"/>.
		/// </summary>
		/// <param name="expression">Expression to check</param>
		/// <returns><see langword="true"/> if <paramref name="expression"/> is <see langword="nameof"/>; otherwise, <see langword="false"/></returns>
		public static bool IsNameOfExpression(this ExpressionSyntax expression)
		{
			return (expression is InvocationExpressionSyntax { Expression: IdentifierNameSyntax { Identifier: SyntaxToken { ValueText: "nameof" } } });
		}
	}
}
