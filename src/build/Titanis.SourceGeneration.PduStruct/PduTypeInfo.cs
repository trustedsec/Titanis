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
			this.Size = PduFieldInfo.GetSizeOf(typeSymbol);
		}

		public sealed override string ToString() => this.TypeSymbol.Name;

		private ImmutableArray<PduFieldInfo>? _fields;
		internal readonly SemanticModel model;

		public INamedTypeSymbol TypeSymbol { get; }
		public ImmutableArray<ISymbol> Members { get; }
		public TypeDeclarationSyntax Declaration { get; }
		public ImmutableArray<PduParamInfo> Parameters { get; }
		public int Size { get; }
		public PduByteOrder? ByteOrder { get; }

		public ImmutableArray<PduFieldInfo> GetFields(in PduTypeContext ctx)
		{
			if (!this._fields.HasValue)
			{
				var fields = ImmutableArray.CreateBuilder<PduFieldInfo>(this.Members.Length);
				HashSet<string> includedProps = new HashSet<string>();

				foreach (var member_ in this.Members)
				{
					var member = member_;
					ctx.cancellationToken.ThrowIfCancellationRequested();

					if (
						member.IsStatic
						|| member.IsDefined(typeof(PduIgnoreAttribute))
						|| member.IsDefined(typeof(PduParameterAttribute))
						)
						continue;

					var attrPduField = member.GetAttribute(typeof(PduFieldAttribute));

					ITypeSymbol? fieldType = null;
					SyntaxToken declarator = default;
					bool isBackingField = false;
					if (member.Kind == SymbolKind.Field)
					{
						var field = (IFieldSymbol)member;
						if (field.AssociatedSymbol != null)
						{
							// This is a backing field, use the property
							member = field.AssociatedSymbol;
							isBackingField = true;
						}
						else
						{
							declarator = ((VariableDeclaratorSyntax)member.DeclaringSyntaxReferences[0].GetSyntax(ctx.cancellationToken)).Identifier;
							fieldType = field.Type;
						}
					}
					if (member.Kind == SymbolKind.Property)
					{
						if (attrPduField != null || isBackingField)
						{
							var prop = (IPropertySymbol)member;
							if (!includedProps.Add(prop.Name))
								continue;

							declarator = ((PropertyDeclarationSyntax)prop.DeclaringSyntaxReferences[0].GetSyntax(ctx.cancellationToken)).Identifier;
							fieldType = prop.Type;
						}
						else
							continue;
					}

					if (fieldType is null)
						continue;

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
