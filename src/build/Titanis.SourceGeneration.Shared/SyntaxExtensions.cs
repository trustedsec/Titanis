using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Titanis.CodeGen
{
    /// <summary>
    /// Provides extensions for syntax nodes.
    /// </summary>
    internal static class SyntaxExtensions
    {
		public static bool HasModifier(this MemberDeclarationSyntax? memberDecl, SyntaxKind kind)
			=> memberDecl?.Modifiers.Any(r => r.IsKind(kind)) ?? false;

		public static bool IsPartial(this MemberDeclarationSyntax? memberDecl)
			=> memberDecl.HasModifier(SyntaxKind.PartialKeyword);

		public static string? GetSimpleName(this NameSyntax? name)
		{
			return name switch
			{
				SimpleNameSyntax simp => simp.Identifier.Text,
				QualifiedNameSyntax qual => qual.Right.Identifier.Text,
				_ => null
			};
		}
    }
}
