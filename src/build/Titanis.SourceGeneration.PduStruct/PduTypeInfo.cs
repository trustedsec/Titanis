#if DEBUG
//#define DEBUG_LAUNCH
#endif

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Titanis.CodeGen;
using Titanis.PduStruct;

namespace Titanis.SourceGen
{
	record struct PduTypeContext(CancellationToken cancellationToken)
	{

	}
	class PduTypeInfo
	{

		public PduTypeInfo(INamedTypeSymbol typeSymbol, TypeDeclarationSyntax declaration, SemanticModel model)
		{
			this.TypeSymbol = typeSymbol;
			this.Members = typeSymbol.GetMembers();
			this.Declaration = declaration;
			this.model = model;

			this.ByteOrder = SyntaxHelpers.GetByteOrder(TypeSymbol);

			this.Parameters = GetPduParameters(typeSymbol);
		}

		public sealed override string ToString() => this.TypeSymbol.Name;

		private ImmutableArray<PduFieldInfo>? _fields;
		internal readonly SemanticModel model;

		public INamedTypeSymbol TypeSymbol { get; }
		public ImmutableArray<ISymbol> Members { get; }
		public TypeDeclarationSyntax Declaration { get; }
		public ImmutableArray<PduParamInfo> Parameters { get; }

		public PduByteOrder? ByteOrder { get; }

		public ImmutableArray<PduFieldInfo> GetFields(in PduTypeContext ctx)
		{
			if (!this._fields.HasValue)
			{
				var fields = ImmutableArray.CreateBuilder<PduFieldInfo>(this.Members.Length);
				foreach (var member in this.Members)
				{
					ctx.cancellationToken.ThrowIfCancellationRequested();

					if (
						member.IsStatic
						|| member.IsDefined(typeof(PduIgnoreAttribute))
						|| member.IsDefined(typeof(PduParameterAttribute))
						)
						continue;

					var attrPduField = member.GetAttribute(typeof(PduFieldAttribute));

					ITypeSymbol fieldType;
					SyntaxToken declarator;
					switch (member.Kind)
					{
						case SymbolKind.Field:
							{
								var field = (IFieldSymbol)member;
								if (field.AssociatedSymbol != null)
									// This is a backing field
									continue;

								declarator = ((VariableDeclaratorSyntax)field.DeclaringSyntaxReferences[0].GetSyntax(ctx.cancellationToken)).Identifier;
								fieldType = field.Type;
							}
							break;

						case SymbolKind.Property:
							{
								if (attrPduField == null)
									continue;

								var prop = (IPropertySymbol)member;
								declarator = ((PropertyDeclarationSyntax)prop.DeclaringSyntaxReferences[0].GetSyntax(ctx.cancellationToken)).Identifier;
								fieldType = prop.Type;
							}
							break;

						default:
							continue;
					}

					fields.Add(new PduFieldInfo(member, declarator, fieldType, attrPduField));
				}
				this._fields = fields.ToImmutable();
			}
			return this._fields.Value;
		}

		internal static ImmutableArray<PduParamInfo> GetPduParameters(ITypeSymbol typeSymbol)
		{
			var parameters = ImmutableArray.CreateBuilder<PduParamInfo>();
			GetPduParametersInto(typeSymbol, parameters, true);
			return parameters.ToImmutable();
		}

		private static void GetPduParametersInto(ITypeSymbol typesym, IList<PduParamInfo> parameters, bool local)
		{
			if (typesym.TypeKind is TypeKind.Class)
			{
				if (typesym.BaseType != null)
					GetPduParametersInto(typesym.BaseType, parameters, false);
			}

			var members = typesym.GetMembers();
			foreach (var member in members)
			{
				if (member.IsDefined(typeof(PduParameterAttribute)))
				{
					var type = member.DataType();
					parameters.Add(new PduParamInfo(member, local, type));
				}
			}
		}
	}
}
