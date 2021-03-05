using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;

namespace Titanis.CodeGen
{
	public enum InheritModifier
	{
		Instance,
		Static,
		Virtual,
		Abstract,
		Override,
		SealedOverride
	}

	public static class Code
	{
		#region Syntax lists
		public static SyntaxList<T> List<T>(params T[]? elements)
			where T : SyntaxNode
			=> SyntaxList.Create<T>(elements);

		public static SeparatedSyntaxList<T> SeparatedList<T>(params T[]? elements)
			where T : SyntaxNode
			=> SeparatedSyntaxList.Create<T>(elements);

		public static AttributeArgumentSyntax AsAttributeArg(this ExpressionSyntax expression)
			=> SyntaxFactory.AttributeArgument(expression);

		internal static ref SyntaxTokenList Append(this ref SyntaxTokenList list, SyntaxKind kind)
		{
			list = list.Add(SyntaxFactory.Token(kind));
			return ref list;
		}
		#endregion
		#region Object model
		public static MethodDeclarationSyntax DeclarePartialMethod(
			string name,
			params ParameterSyntax[] parameters
			)
			=> SyntaxFactory.MethodDeclaration(
				default,
				new SyntaxTokenList(SyntaxFactory.Token(SyntaxKind.PartialKeyword)),
				TypeRef(typeof(void)),
				null,
				SyntaxFactory.Identifier(name),
				null,
				SyntaxFactory.ParameterList(SeparatedList(parameters)),
				default,
				default,
				SyntaxFactory.Token(SyntaxKind.SemicolonToken)
				);

		public static AttributeSyntax GeneratedCode()
			=> SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(typeof(GeneratedCodeAttribute).FullName), SyntaxFactory.AttributeArgumentList(SeparatedList(Primitive("Titanis.SourceGen").AsAttributeArg(), Primitive("0.9.0").AsAttributeArg())));

		public static AttributeSyntax Attribute(NameSyntax type, params ExpressionSyntax[] arguments)
			=> SyntaxFactory.Attribute(type, SyntaxFactory.AttributeArgumentList(SeparatedList(Array.ConvertAll(arguments, r => r.AsAttributeArg()))));

		public static SyntaxList<AttributeListSyntax> AttributeList(
			params AttributeSyntax[]? attributes
			)
			=> List(SyntaxFactory.AttributeList(SeparatedList(attributes)));

		public static MethodDeclarationSyntax DeclareMethod(
			string name,
			TypeSyntax returnType,
			Accessibility access,
			InheritModifier inherit,
			ParameterSyntax[] parameters,
			BlockSyntax body
			)
			=> SyntaxFactory.MethodDeclaration(
				AttributeList(GeneratedCode()),
				GetModsFor(access, inherit),
				returnType,
				null,
				SyntaxFactory.Identifier(name),
				null,
				SyntaxFactory.ParameterList(SeparatedList(parameters)),
				default,
				body,
				new SyntaxToken()
				);

		public static MethodDeclarationSyntax DeclareMethod(
			string name,
			TypeSyntax returnType,
			Accessibility access,
			InheritModifier inherit,
			ParameterSyntax[] parameters,
			ExpressionSyntax body
			)
			=> SyntaxFactory.MethodDeclaration(
				AttributeList(GeneratedCode()),
				GetModsFor(access, inherit),
				returnType,
				null,
				SyntaxFactory.Identifier(name),
				null,
				SyntaxFactory.ParameterList(SeparatedList(parameters)),
				default,
				default,
				SyntaxFactory.ArrowExpressionClause(body),
				SyntaxFactory.Token(SyntaxKind.SemicolonToken)
				);

		public static ParameterSyntax DeclareParameter(string name, TypeSyntax type)
			=> SyntaxFactory.Parameter(default, default, type, SyntaxFactory.Identifier(name), default);

		public static ParameterSyntax DeclareParameter(string name, ITypeSymbol type)
			=> SyntaxFactory.Parameter(default, default, TypeRef(type), SyntaxFactory.Identifier(name), default);

		public static ParameterSyntax DeclareParameter(string name, Type type)
			=> SyntaxFactory.Parameter(default, default, TypeRef(type), SyntaxFactory.Identifier(name), default);

		private static SyntaxTokenList GetModsFor(Accessibility access, InheritModifier inherit)
		{
			SyntaxTokenList list = new SyntaxTokenList();
			switch (access)
			{
				case Accessibility.Private:
					list.Append(SyntaxKind.PrivateKeyword);
					break;
				case Accessibility.ProtectedAndInternal:
					list.Append(SyntaxKind.PrivateKeyword);
					list.Append(SyntaxKind.ProtectedKeyword);
					break;
				case Accessibility.Protected:
					list.Append(SyntaxKind.ProtectedKeyword);
					break;
				case Accessibility.Internal:
					list.Append(SyntaxKind.InternalKeyword);
					break;
				case Accessibility.ProtectedOrInternal:
					list.Append(SyntaxKind.ProtectedKeyword);
					list.Append(SyntaxKind.InternalKeyword);
					break;
				case Accessibility.Public:
					list.Append(SyntaxKind.PublicKeyword);
					break;
				default:
					break;
			}

			switch (inherit)
			{
				case InheritModifier.Instance:
					break;
				case InheritModifier.Static:
					list.Append(SyntaxKind.StaticKeyword);
					break;
				case InheritModifier.Virtual:
					list.Append(SyntaxKind.VirtualKeyword);
					break;
				case InheritModifier.Abstract:
					list.Append(SyntaxKind.AbstractKeyword);
					break;
				case InheritModifier.Override:
					list.Append(SyntaxKind.OverrideKeyword);
					break;
				case InheritModifier.SealedOverride:
					list.Append(SyntaxKind.SealedKeyword);
					list.Append(SyntaxKind.OverrideKeyword);
					break;
				default:
					break;
			}

			return list;
		}


		public static NamespaceDeclarationSyntax DeclareNamespace(string ns, params MemberDeclarationSyntax[] members)
			=> SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(ns), default, default,
				List(members));
		public static FieldDeclarationSyntax DeclareField(
			string name,
			TypeSyntax propertyType,
			Accessibility access,
			InheritModifier inherit
			)
			=> SyntaxFactory.FieldDeclaration(
				AttributeList(GeneratedCode()),
				GetModsFor(access, inherit),
				DeclareVariable(
					propertyType,
					name),
				SyntaxFactory.Token(SyntaxKind.SemicolonToken)
				);
		public static PropertyDeclarationSyntax DeclareProperty(
			string name,
			TypeSyntax propertyType,
			Accessibility access,
			InheritModifier inherit,
			ExpressionSyntax value)
			=> SyntaxFactory.PropertyDeclaration(
				AttributeList(GeneratedCode()),
				GetModsFor(access, inherit),
				propertyType,
				null,
				SyntaxFactory.Identifier(name),
				null,
				SyntaxFactory.ArrowExpressionClause(SyntaxFactory.Token(SyntaxKind.EqualsGreaterThanToken), value),
				null,
				SyntaxFactory.Token(SyntaxKind.SemicolonToken)
				);
		public static StructDeclarationSyntax DeclareStruct(
			string name,
			AttributeSyntax[]? attributes = null,
			TypeSyntax[]? baseTypes = null,
			MemberDeclarationSyntax[]? members = null
			)

			=> SyntaxFactory.StructDeclaration(
				AttributeList(attributes),
				SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PartialKeyword)),
				SyntaxFactory.Token(SyntaxKind.StructKeyword),
				SyntaxFactory.Identifier(name),
				null,
				BaseList(baseTypes),
				default,
				SyntaxFactory.Token(SyntaxKind.OpenBraceToken),
				List(members),
				SyntaxFactory.Token(SyntaxKind.CloseBraceToken),
				default
				);
		public static BaseListSyntax? BaseList(TypeSyntax[]? baseTypes)
		{
			if (baseTypes is null || baseTypes.Length == 0)
				return null;

			return SyntaxFactory.BaseList(SeparatedList<BaseTypeSyntax>(Array.ConvertAll(baseTypes, r => SyntaxFactory.SimpleBaseType(r))));
		}
		public static BaseListSyntax? BaseList(ITypeSymbol[]? baseTypes)
		{
			if (baseTypes is null || baseTypes.Length == 0)
				return null;

			return SyntaxFactory.BaseList(SeparatedList<BaseTypeSyntax>(Array.ConvertAll(baseTypes, r => SyntaxFactory.SimpleBaseType(TypeRef(r)))));
		}

		public static SyntaxKind DeclarationKind(this ITypeSymbol type)
			=> type.TypeKind.DeclarationKind();
		public static SyntaxKind DeclarationKind(this TypeKind typeKind)
			=> typeKind switch
			{
				TypeKind.Struct => SyntaxKind.StructDeclaration,
				TypeKind.Class => SyntaxKind.ClassDeclaration,
				TypeKind.Interface => SyntaxKind.InterfaceDeclaration,
				TypeKind.Delegate => SyntaxKind.DelegateDeclaration,
				TypeKind.Enum => SyntaxKind.EnumDeclaration,
			};

		public static TypeDeclarationSyntax DeclareType(
			SyntaxKind kind,
			string name,
			ITypeSymbol[]? baseTypes,
			params MemberDeclarationSyntax[] members)
			=> DeclareType(kind, name, baseTypes, members);
		public static TypeDeclarationSyntax DeclareType(
			SyntaxKind kind,
			string name,
			TypeSyntax[]? baseTypes,
			TypeParameterListSyntax? typeParams,
			params MemberDeclarationSyntax[] members)
			=> SyntaxFactory.TypeDeclaration(
				kind,
				default,
				SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PartialKeyword)),
				SyntaxFactory.Token(kind switch
				{
					SyntaxKind.StructDeclaration => SyntaxKind.StructKeyword,
					SyntaxKind.ClassDeclaration => SyntaxKind.ClassKeyword,
					SyntaxKind.InterfaceDeclaration => SyntaxKind.InterfaceKeyword,
				}),
				SyntaxFactory.Identifier(name),
				typeParams,
				//(baseTypes == null) ? null : SyntaxFactory.TypeParameterList(SeparatedList(typeParams)),
				BaseList(baseTypes),
				default,
				SyntaxFactory.Token(SyntaxKind.OpenBraceToken),
				List(members),
				SyntaxFactory.Token(SyntaxKind.CloseBraceToken),
				default
				);
		#endregion

		public static UsingDirectiveSyntax[] Using(string[] ns)
			=> Array.ConvertAll(ns, r => SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(r)));
		public static UsingDirectiveSyntax Using(string ns)
			=> SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(ns));

		#region Types
		public static TypeSyntax AsTypeRef(this ITypeSymbol type)
			=> TypeRef(type);
		public static TypeSyntax TypeRef(ITypeSymbol type)
		{
			return type.SpecialType switch
			{
				SpecialType.System_Object
				or SpecialType.System_Void
				or SpecialType.System_Boolean
				or SpecialType.System_Char
				or SpecialType.System_Byte
				or SpecialType.System_SByte
				or SpecialType.System_Int16
				or SpecialType.System_UInt16
				or SpecialType.System_Int32
				or SpecialType.System_UInt32
				or SpecialType.System_Int64
				or SpecialType.System_UInt64
				or SpecialType.System_Single
				or SpecialType.System_Double
				or SpecialType.System_Decimal
				=> SyntaxFactory.PredefinedType(SyntaxFactory.Token(type.SpecialType switch
				{
					SpecialType.System_Object => SyntaxKind.ObjectKeyword,
					SpecialType.System_Void => SyntaxKind.VoidKeyword,
					SpecialType.System_Boolean => SyntaxKind.BoolKeyword,
					SpecialType.System_Char => SyntaxKind.CharKeyword,
					SpecialType.System_Byte => SyntaxKind.ByteKeyword,
					SpecialType.System_SByte => SyntaxKind.SByteKeyword,
					SpecialType.System_Int16 => SyntaxKind.ShortKeyword,
					SpecialType.System_UInt16 => SyntaxKind.UShortKeyword,
					SpecialType.System_Int32 => SyntaxKind.IntKeyword,
					SpecialType.System_UInt32 => SyntaxKind.UIntKeyword,
					SpecialType.System_Int64 => SyntaxKind.LongKeyword,
					SpecialType.System_UInt64 => SyntaxKind.ULongKeyword,
					SpecialType.System_Single => SyntaxKind.FloatKeyword,
					SpecialType.System_Double => SyntaxKind.DoubleKeyword,
					SpecialType.System_Decimal => SyntaxKind.DecimalKeyword,
				})),
				_ => SyntaxFactory.ParseTypeName(type.FullName())
			};
		}
		public static TypeSyntax TypeRef(Type type)
		{
			var tc = Type.GetTypeCode(type);
			if (type.IsEnum)
				return SyntaxFactory.ParseTypeName(type.FullName);
			else if (type.IsGenericTypeDefinition)
			{
				var argCount = type.GetGenericArguments().Length;
				TypeSyntax[] args = new TypeSyntax[argCount];
				for (int i = 0; i < args.Length; i++)
				{
					args[i] = SyntaxFactory.OmittedTypeArgument();
				}
				SyntaxFactory.GenericName(
					SyntaxFactory.Identifier(type.FullName),
					SyntaxFactory.TypeArgumentList(
						SeparatedList(args)
						));
			}
			else if (type.IsGenericType)
			{
				var genericArgs = type.GetGenericArguments();
				TypeSyntax[] args = new TypeSyntax[genericArgs.Length];
				for (int i = 0; i < args.Length; i++)
				{
					args[i] = TypeRef(genericArgs[i]);
				}
				SyntaxFactory.GenericName(
					SyntaxFactory.Identifier(type.FullName),
					SyntaxFactory.TypeArgumentList(
						SeparatedList(args)
						));
			}

			return tc switch
			{
				TypeCode.Boolean
				or TypeCode.Byte
				or TypeCode.Char
				or TypeCode.Decimal
				or TypeCode.Double
				or TypeCode.Int16
				or TypeCode.Int32
				or TypeCode.Int64
				or TypeCode.SByte
				or TypeCode.Single
				or TypeCode.String
				or TypeCode.UInt16
				or TypeCode.UInt32
				or TypeCode.UInt64 => SyntaxFactory.PredefinedType(SyntaxFactory.Token(tc switch
				{
					TypeCode.Boolean => SyntaxKind.BoolKeyword,
					TypeCode.Byte => SyntaxKind.ByteKeyword,
					TypeCode.Char => SyntaxKind.CharKeyword,
					TypeCode.Decimal => SyntaxKind.DecimalKeyword,
					TypeCode.Double => SyntaxKind.DoubleKeyword,
					TypeCode.Int16 => SyntaxKind.ShortKeyword,
					TypeCode.Int32 => SyntaxKind.IntKeyword,
					TypeCode.Int64 => SyntaxKind.LongKeyword,
					TypeCode.SByte => SyntaxKind.SByteKeyword,
					TypeCode.Single => SyntaxKind.FloatKeyword,
					TypeCode.String => SyntaxKind.StringKeyword,
					TypeCode.UInt16 => SyntaxKind.UShortKeyword,
					TypeCode.UInt32 => SyntaxKind.UIntKeyword,
					TypeCode.UInt64 => SyntaxKind.ULongKeyword,
				})),
				_ => type switch
				{
					var t when t == typeof(void) => SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
					_ => SyntaxFactory.ParseTypeName(type.FullName)
				}
			};
		}
		#endregion
		#region Literal expressions
		public static LiteralExpressionSyntax Null => SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression);
		public static LiteralExpressionSyntax BooleanLiteral(bool value)
			=> SyntaxFactory.LiteralExpression(value ? SyntaxKind.TrueLiteralExpression : SyntaxKind.TrueLiteralExpression);
		public static ExpressionSyntax Primitive(object? value)
		{
			ExpressionSyntax expr = value switch
			{
				null => Null,
				char n => SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression, SyntaxFactory.Literal(n)),
				byte n => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(n)),
				sbyte n => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(n)),
				short n => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(n)),
				ushort n => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(n)),
				int n => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(n)),
				uint n => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(n)),
				string n => SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(n)),
				bool n => BooleanLiteral(n),
				Enum n => TypeRef(value.GetType()).FieldOf(Enum.GetName(value.GetType(), value))
			};
			return expr;
		}

		public static ThisExpressionSyntax This
			=> SyntaxFactory.ThisExpression();
		public static BaseExpressionSyntax Base
			=> SyntaxFactory.BaseExpression();
		#endregion
		#region Expression and operators
		public static ExpressionSyntax VarRef(string name) => SyntaxFactory.IdentifierName(name);
		public static ExpressionSyntax ArgRef(string name) => SyntaxFactory.IdentifierName(name);
		#region Binary
		public static ExpressionSyntax EqualTo(this ExpressionSyntax expr, ExpressionSyntax other)
			=> SyntaxFactory.BinaryExpression(SyntaxKind.EqualsExpression, expr, other);
		public static ExpressionSyntax BooleanAnd(this ExpressionSyntax expr, ExpressionSyntax other)
			=> SyntaxFactory.BinaryExpression(SyntaxKind.LogicalAndExpression, expr, other);
		#endregion

		[Obsolete("Use FieldOF, PropertyOf, or MethodOf instead.", true)]
		public static MemberAccessExpressionSyntax Member(this ExpressionSyntax instance, string memberName)
			=> SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, instance, SyntaxFactory.IdentifierName(memberName));
		public static MemberAccessExpressionSyntax FieldOf(this ExpressionSyntax instance, string memberName)
			=> SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, instance, SyntaxFactory.IdentifierName(memberName));
		public static MemberAccessExpressionSyntax PropertyOf(this ExpressionSyntax instance, string memberName)
			=> SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, instance, SyntaxFactory.IdentifierName(memberName));
		public static MemberAccessExpressionSyntax MethodOf(this ExpressionSyntax instance, string memberName)
			=> SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, instance, SyntaxFactory.IdentifierName(memberName));
		public static MemberAccessExpressionSyntax MethodOf(this ExpressionSyntax instance, string memberName, params TypeSyntax[] genericArgs)
			=> SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, instance, SyntaxFactory.GenericName(SyntaxFactory.Identifier(memberName), SyntaxFactory.TypeArgumentList(
				SeparatedList(genericArgs)
				)));
		public static MemberAccessExpressionSyntax MethodOf(this ExpressionSyntax instance, string memberName, params ITypeSymbol[] genericArgs)
			=> SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, instance, SyntaxFactory.GenericName(SyntaxFactory.Identifier(memberName), SyntaxFactory.TypeArgumentList(
				SeparatedList(Array.ConvertAll(genericArgs, r => TypeRef(r)))
				)));

		public static ParenthesizedExpressionSyntax Parenthesize(this ExpressionSyntax expression)
			=> SyntaxFactory.ParenthesizedExpression(expression);

		public static SwitchExpressionSyntax Switch(this ExpressionSyntax expression, params SwitchExpressionArmSyntax[] arms)
			=> SyntaxFactory.SwitchExpression(
				expression,
				SeparatedList(arms)
				);

		public static SwitchExpressionArmSyntax SwitchArm(this PatternSyntax pattern, ExpressionSyntax result)
			=> SyntaxFactory.SwitchExpressionArm(pattern, result);
		public static SwitchExpressionArmSyntax SwitchArm(this ExpressionSyntax pattern, ExpressionSyntax result)
			=> SyntaxFactory.SwitchExpressionArm(SyntaxFactory.ConstantPattern(pattern), result);

		public static ExpressionSyntax EnumField<TEnum>(this TEnum value)
			where TEnum : struct, Enum
			=> TypeRef(typeof(TEnum)).FieldOf(Enum.GetName(typeof(TEnum), value));

		public static CastExpressionSyntax Cast(this ExpressionSyntax expression, TypeSyntax toType)
			=> SyntaxFactory.CastExpression(toType, expression);
		public static CastExpressionSyntax Cast(this ExpressionSyntax expression, ITypeSymbol toType)
			=> SyntaxFactory.CastExpression(TypeRef(toType), expression);

		public static IsPatternExpressionSyntax IsNull(this ExpressionSyntax expression)
		{
			return SyntaxFactory.IsPatternExpression(
				expression,
				SyntaxFactory.ConstantPattern(SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)));
		}

		public static IsPatternExpressionSyntax IsNotNull(this ExpressionSyntax expression)
		{
			return SyntaxFactory.IsPatternExpression(
				expression,
				SyntaxFactory.UnaryPattern(
					SyntaxFactory.Token(SyntaxKind.NotKeyword),
					SyntaxFactory.ConstantPattern(SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression))
					));
		}

		public static InvocationExpressionSyntax Call(
			this ExpressionSyntax method,
			params ExpressionSyntax[] arguments
			)
			=> SyntaxFactory.InvocationExpression(method, SyntaxFactory.ArgumentList(
				SeparatedList(Array.ConvertAll(arguments, r => SyntaxFactory.Argument(r)))
				));

		public static AwaitExpressionSyntax Await(this ExpressionSyntax expr)
			=> SyntaxFactory.AwaitExpression(expr);
		#endregion

		public static ThrowExpressionSyntax Throw(this ExpressionSyntax exception)
			=> SyntaxFactory.ThrowExpression(exception);

		public static ObjectCreationExpressionSyntax New(this TypeSyntax type, params ExpressionSyntax[] arguments)
			=> SyntaxFactory.ObjectCreationExpression(
				type,
				SyntaxFactory.ArgumentList(SeparatedList(Array.ConvertAll(arguments, r => SyntaxFactory.Argument(r)))),
				null
				);

		public static ArrayCreationExpressionSyntax NewArray(this TypeSyntax elementType, ExpressionSyntax size)
			=> SyntaxFactory.ArrayCreationExpression(
				SyntaxFactory.ArrayType(elementType, List(SyntaxFactory.ArrayRankSpecifier(SeparatedList(size))))
				);


		public static BinaryExpressionSyntax LessThan(this ExpressionSyntax expr, ExpressionSyntax other)
			=> SyntaxFactory.BinaryExpression(SyntaxKind.LessThanExpression, expr, other);

		public static ExpressionSyntax Increment(this ExpressionSyntax expr)
			=> SyntaxFactory.PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, expr);

		public static ExpressionSyntax Indexer(this ExpressionSyntax expr,
			ExpressionSyntax index)
			=> SyntaxFactory.ElementAccessExpression(expr, SyntaxFactory.BracketedArgumentList(SeparatedList(SyntaxFactory.Argument(index))));

		#region Statements

		public static ExpressionStatementSyntax Do(this ExpressionSyntax expr)
			=> SyntaxFactory.ExpressionStatement(expr);
		public static TList Do<TList>(this TList statements, ExpressionSyntax expr)
			where TList : IList<StatementSyntax>
		{
			statements.Add(expr.Do());
			return statements;
		}

		public static ReturnStatementSyntax Return(this ExpressionSyntax expr)
			=> SyntaxFactory.ReturnStatement(expr);

		public static TList IfThen<TList>(this TList statements,
			ExpressionSyntax condition,
			StatementSyntax trueStatement
			)
			where TList : IList<StatementSyntax>
		{
			statements.Add(SyntaxFactory.IfStatement(condition, trueStatement));
			return statements;
		}

		public static TList IfThen<TList>(this TList statements,
			ExpressionSyntax condition,
			StatementSyntax trueStatement,
			StatementSyntax falseStatement
			)
			where TList : IList<StatementSyntax>
		{
			statements.Add(SyntaxFactory.IfStatement(condition, trueStatement, SyntaxFactory.ElseClause(falseStatement)));
			return statements;
		}

		public static StatementSyntax Assign(ExpressionSyntax left, ExpressionSyntax right)
			=> SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, left, right).Do();

		public static TList Assign<TList>(this TList statements, ExpressionSyntax left, ExpressionSyntax right)
			where TList : IList<StatementSyntax>
		{
			statements.Do(SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, left, right));
			return statements;
		}

		public static TList Switch<TList>(this TList statements,
			ExpressionSyntax test,
			params SwitchSectionSyntax[] sections
			)
			where TList : IList<StatementSyntax>
		{
			statements.Add(SyntaxFactory.SwitchStatement(test,
				List(sections)
				));
			return statements;
		}

		public static BreakStatementSyntax Break => SyntaxFactory.BreakStatement();
		public static SwitchSectionSyntax DefaultCase(StatementSyntax statement)
			=> SyntaxFactory.SwitchSection(
				List<SwitchLabelSyntax>(SyntaxFactory.DefaultSwitchLabel()),
				List(statement, Break)
				);
		public static SwitchSectionSyntax Case(this ExpressionSyntax test, StatementSyntax statement)
			=> SyntaxFactory.SwitchSection(
				List<SwitchLabelSyntax>(SyntaxFactory.CaseSwitchLabel(test)),
				List(statement, Break)
				);

		public static BlockSyntax Block(params StatementSyntax[] statements)
			=> SyntaxFactory.Block(List(statements));

		public static BlockSyntax AsBlock<TList>(this TList statements)
			where TList : IList<StatementSyntax>
			=> SyntaxFactory.Block(List(statements.ToArray()));

		public static TypeSyntax Void() => TypeRef(typeof(void));
		public static TypeSyntax Var() => SyntaxFactory.IdentifierName(SyntaxFactory.Identifier("var"));
		public static VariableDeclarationSyntax DeclareVariable(
			string variableName,
			ExpressionSyntax? value)
			=> SyntaxFactory.VariableDeclaration(
				Var(),
				SeparatedList(
					SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(variableName), null, SyntaxFactory.EqualsValueClause(value))
				)
				);
		public static LocalDeclarationStatementSyntax AsLocal(this VariableDeclarationSyntax decl)
			=> SyntaxFactory.LocalDeclarationStatement(decl);
		public static VariableDeclarationSyntax DeclareVariable(
			TypeSyntax type,
			string variableName,
			ExpressionSyntax? value)
			=> SyntaxFactory.VariableDeclaration(
				type,
				SeparatedList(
					SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(variableName), null, SyntaxFactory.EqualsValueClause(value))
				)
				);
		public static VariableDeclarationSyntax DeclareVariable(
			TypeSyntax type,
			string variableName
			)
			=> SyntaxFactory.VariableDeclaration(
				type,
				SeparatedList(
					SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(variableName), null, null)
				));
		public static VariableDeclarationSyntax DeclareVariable(
			string variableName
			)
			=> SyntaxFactory.VariableDeclaration(
				Var(),
				SeparatedList(
					SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(variableName), null, null)
				));

		public static TList DeclareVariable<TList>(this TList statements,
			string variableName,
			ExpressionSyntax value)
			where TList : IList<StatementSyntax>
		{
			statements.Add(SyntaxFactory.LocalDeclarationStatement(DeclareVariable(variableName, value)));
			return statements;
		}

		public static TList DeclareVariable<TList>(this TList statements,
			TypeSyntax type,
			string variableName
			)
			where TList : IList<StatementSyntax>
		{
			statements.Add(SyntaxFactory.LocalDeclarationStatement(DeclareVariable(type, variableName)));
			return statements;
		}

		public static ForStatementSyntax For(string rangeVar, ExpressionSyntax limit, StatementSyntax body)
			=> SyntaxFactory.ForStatement(
				DeclareVariable(rangeVar, Primitive(0)),
				default,
				SyntaxFactory.IdentifierName(rangeVar).LessThan(limit),
				SeparatedList(SyntaxFactory.IdentifierName(rangeVar).Increment()),
				body
				);
		#endregion
	}
}
