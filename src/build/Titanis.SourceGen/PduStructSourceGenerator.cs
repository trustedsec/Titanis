#if DEBUG
//#define DEBUG_LAUNCH
#endif

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Titanis.CodeGen;
using Titanis.PduStruct;

namespace Titanis.SourceGen
{
	[Generator]
	public class PduStructSourceGenerator : IIncrementalGenerator
	{
		private const string PduStructAttrname = nameof(PduStructAttribute);
		private const string PduStructSizeName = "PduStructSize";
		private const string IPduStructName = "Titanis.IO.IPduStruct";
		private const string ByteSourceName = "Titanis.IO.IByteSource";
		private const string ByteWriterName = "Titanis.IO.ByteWriter";
		private const string ReadFromName = "ReadFrom";
		private const string ReaderParamName = "reader";
		private const string WriteToName = "WriteTo";
		private const string WriterParamName = "writer";
		private const string ReadPduStructName = "ReadPduStruct";
		private const string WritePduStructName = "WritePduStruct";
		private const string ByteOrderParamName = "byteOrder";
		private const string LimitVarName = "_PduStruct_count";
		private const string ElementVarName = "_PduStruct_elem";
		private const string LoopVarName = "i";
		private const string ArrayVarName = "_PduStruct_array";

		private const string OnBeforeReadPdu = nameof(OnBeforeReadPdu);
		private const string OnAfterReadPdu = nameof(OnAfterReadPdu);
		private const string OnBeforeWritePdu = nameof(OnBeforeWritePdu);
		private const string OnAfterWritePdu = nameof(OnAfterWritePdu);
		private const string SourceParamName = "source";
		private const string LE_Suffix = "_LE";
		private const string BE_Suffix = "_BE";
		private const string Titanis_IO = "Titanis.IO";
		private const string PositionName = "Position";
		private const string WriteBytesName = "WriteBytes";
		private const string ReadBytesName = "ReadBytes";

		public PduStructSourceGenerator()
		{
		}

		public void Initialize(IncrementalGeneratorInitializationContext context)
		{
#if DEBUG_LAUNCH
			try
			{
				this.InitializeInternal(context);
			}
			catch (OperationCanceledException ex)
			{
				throw;
			}
			catch (Exception ex)
			{
				Debugger.Launch();
				Debug.WriteLine(ex);
				throw;
			}
#else
			this.InitializeInternal(context);
#endif
		}
		private void InitializeInternal(IncrementalGeneratorInitializationContext context)
		{
			var pduTypes = context.SyntaxProvider
				.CreateSyntaxProvider(PduStructFilterPredicateWrapper, GetPduStructOrNullWrapper)
				.Where(t => t is not null)
				.Collect();

			context.RegisterSourceOutput(pduTypes, GeneratePduStructCodeWrapper);
		}


		private bool PduStructFilterPredicateWrapper(SyntaxNode node, CancellationToken token)
		{
			try
			{
				return PduStructFilterPredicate(node, token);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
				Debugger.Launch();
				Debug.WriteLine(ex);
				return false;
			}
		}
		private bool PduStructFilterPredicate(SyntaxNode node, CancellationToken token)
		{
			if (node is AttributeSyntax attr)
			{
				var simpleName = attr.Name.GetSimpleName();

				bool matches = (simpleName is not null) && (
					simpleName.Equals(PduStructAttrname, StringComparison.Ordinal)
					|| simpleName.Equals("PduStruct", StringComparison.Ordinal)
					);
				return matches;
			}

			return false;
		}

		#region Filtering
		private bool HasPduStructAttribute(ITypeSymbol? type)
		{
			return (type is not null) && (type.SpecialType is SpecialType.None) && (type.IsDefined(typeof(PduStructAttribute)));
		}
		private bool IsPduStruct(ITypeSymbol? type)
		{
			if (HasPduStructAttribute(type))
				return true;
			else
			{
				bool f = (type.AllInterfaces.Any(r => r.FullName() == IPduStructName));
				if (f)
					return true;

				if (type is ITypeParameterSymbol parm)
					f = parm.ConstraintTypes.Any(r => r.FullName() == IPduStructName);

				return f;
			}
		}

		struct PduParamInfo
		{
			internal ISymbol field;
			internal bool local;
			internal ITypeSymbol? type;
		}
		class PduTypeInfo
		{
			internal readonly INamedTypeSymbol typeSymbol;
			internal readonly ImmutableArray<ISymbol> members;
			internal readonly TypeDeclarationSyntax decl;
			internal readonly SemanticModel model;

			internal PduParamInfo[]? parameters;

			public PduTypeInfo(INamedTypeSymbol typeSymbol, ImmutableArray<ISymbol> members, TypeDeclarationSyntax decl, SemanticModel model)
			{
				this.typeSymbol = typeSymbol;
				this.members = members;
				this.decl = decl;
				this.model = model;
			}
		}

		private PduTypeInfo? GetPduStructOrNullWrapper(GeneratorSyntaxContext context, CancellationToken token)
		{
			try
			{
				return this.GetPduStructOrNull(context, token);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
				Debugger.Launch();
				Debug.WriteLine(ex);
				throw;
			}
		}
		private PduTypeInfo? GetPduStructOrNull(GeneratorSyntaxContext context, CancellationToken token)
		{
			var attr = (AttributeSyntax)context.Node;

			// > AttributeList > <target>
			var typeDecl = attr.Parent?.Parent as TypeDeclarationSyntax;
			if (typeDecl is null)
				return null;

			var type = context.SemanticModel.GetDeclaredSymbol(typeDecl, token);
			if (type is null || !(type.TypeKind is TypeKind.Struct or TypeKind.Class))
				return null;

			if (HasPduStructAttribute(type))
			{
				return new PduTypeInfo(
					type,
					type.GetMembers(),
					typeDecl,
					context.SemanticModel
				);
			}

			return null;
		}
		#endregion

		#region Generation
		private void GeneratePduStructCodeWrapper(
			SourceProductionContext context,
			ImmutableArray<PduTypeInfo> pduTypes
			)
		{
			try
			{
				GeneratePduStructCode(context, pduTypes);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
				Debugger.Launch();
				Debug.WriteLine(ex);
				throw;
			}
		}
		private void GeneratePduStructCode(
			SourceProductionContext context,
			ImmutableArray<PduTypeInfo> pduTypes
			)
		{
			if (pduTypes.IsDefaultOrEmpty)
				return;

			foreach (var pduType in pduTypes)
			{
				context.CancellationToken.ThrowIfCancellationRequested();

				var code = GeneratePduStruct(pduType, context);
				if (code != null)
				{
					var fullTypeName = pduType.typeSymbol.FullName();
					context.CancellationToken.ThrowIfCancellationRequested();
					context.AddSource($"{fullTypeName}.g.cs", code);
				}
			}
		}
		#endregion

		private int GetSizeOf(ITypeSymbol type)
		{
			// Try predefined types
			{
				var size = type.SpecialType switch
				{
					SpecialType.System_Byte or SpecialType.System_SByte => 1,
					SpecialType.System_Int16 or SpecialType.System_UInt16 => 2,
					SpecialType.System_Int32 or SpecialType.System_UInt32 or SpecialType.System_Single => 4,
					SpecialType.System_UInt64 or SpecialType.System_UInt64 or SpecialType.System_Double => 8,
					SpecialType.System_Decimal => 16,
					_ => 0
				};
				if (size != 0)
					return size;
			}

			if (type.TypeKind is TypeKind.Struct or TypeKind.Class)
			{
				int size = 0;

				if (type.TypeKind is TypeKind.Class)
				{
					var baseType = type.BaseType;
					if (baseType is not null && baseType.SpecialType is not SpecialType.System_Object)
						size = GetSizeOf(baseType);
				}

				foreach (var member in type.GetMembers())
				{
					if (member is IFieldSymbol field)
					{
						if (!field.IsStatic)
						{
							var fieldSize = GetSizeOf(field);
							if (fieldSize < 0)
							{
								size = fieldSize;
								break;
							}
							else
								size += fieldSize;
						}
					}
				}
				return size;
			}
			else if (type.TypeKind == TypeKind.Enum)
			{
				var namedType = ((INamedTypeSymbol)type);
				var size = GetSizeOf(namedType.EnumUnderlyingType);
				return size;
			}
			else
				return -1;
		}
		private int GetSizeOf(IFieldSymbol field)
		{
			int fieldSize = GetSizeOf(field.Type);
			return fieldSize;
		}

		private string? GeneratePduStruct(PduTypeInfo pduType, SourceProductionContext context)
		{
			var structType = pduType.typeSymbol;

			// Check that the declaration is marked `partial`
			if (!pduType.decl.IsPartial())
			{
				context.ReportDiagnostic(PduDiagnostics.NoPartialError_1.Create(
					pduType.decl.Keyword.GetLocation(),
					pduType.typeSymbol.Name
					));
				return null;
			}

			var typeSym = pduType.typeSymbol;
			pduType.parameters = GetPduParameters(typeSym);

			// Determine base types
			TypeSyntax[]? baseTypes = null;
			if ((structType.TypeKind is not TypeKind.Class)
				|| !IsPduStruct(structType.BaseType)
				|| (pduType.parameters.Length > 0)
				)
			{
				var pduInterfaceTypeRef = SyntaxFactory.ParseTypeName(IPduStructName);
				if (pduType.parameters.Length > 0)
				{
					pduInterfaceTypeRef = SyntaxFactory.GenericName(SyntaxFactory.Identifier(IPduStructName), SyntaxFactory.TypeArgumentList(
						Code.SeparatedList(Array.ConvertAll(pduType.parameters, r =>
						{
							return r.type.AsTypeRef();
						}))));
				}
				baseTypes = new TypeSyntax[] { pduInterfaceTypeRef };
			}

			int size = GetSizeOf(structType);

			// Generate ReadFrom and WriteTo methods
			var readerTypeRef = pduType.model.Compilation.GetTypeByMetadataName(ByteSourceName);
			var byteOrderTypeRef = pduType.model.Compilation.GetTypeByMetadataName(typeof(PduByteOrder).FullName);
			var writerTypeRef = pduType.model.Compilation.GetTypeByMetadataName(ByteWriterName);

			List<StatementSyntax> readStatements = new List<StatementSyntax>();
			List<StatementSyntax> writeStatements = new List<StatementSyntax>();
			List<StatementSyntax> readStatements_LE = new List<StatementSyntax>();
			List<StatementSyntax> readStatements_BE = new List<StatementSyntax>();
			List<StatementSyntax> writeStatements_LE = new List<StatementSyntax>();
			List<StatementSyntax> writeStatements_BE = new List<StatementSyntax>();

			InheritModifier inherit = InheritModifier.Instance;
			if (typeSym.TypeKind is TypeKind.Class)
			{
				List<ExpressionSyntax> readBaseArgList = new List<ExpressionSyntax>() { readerArg, byteOrderArg };
				List<ExpressionSyntax> writeBaseArgList = new List<ExpressionSyntax>() { writerArg, byteOrderArg };
				foreach (var baseParam in pduType.parameters)
				{
					if (baseParam.local)
					{
						readStatements.Assign(Code.This.FieldOf(baseParam.field.Name), Code.VarRef(baseParam.field.Name));
						writeStatements.Assign(Code.This.FieldOf(baseParam.field.Name), Code.VarRef(baseParam.field.Name));
					}
					else
					{
						readBaseArgList.Add(Code.VarRef(baseParam.field.Name));
						writeBaseArgList.Add(Code.VarRef(baseParam.field.Name));
					}
				}
				var readBaseArgs = readBaseArgList.ToArray();
				var writeBaseArgs = writeBaseArgList.ToArray();

				inherit = this.DetermineInherit(typeSym);
				if (inherit != InheritModifier.Virtual)
				{
					// base.ReadFrom(...)
					readStatements.Do(Code.Base.MethodOf(ReadFromName).Call(readBaseArgs));
					// base.WriteTo(...)
					writeStatements.Do(Code.Base.MethodOf(WriteToName).Call(writeBaseArgs));
				}

				if (pduType.parameters.Length > 0)
					inherit = InheritModifier.Virtual;
			}

			// this.OnBeforeReadPdu()
			readStatements.Do(Code.This.MethodOf(OnBeforeReadPdu).Call(readerArg));
			// this.OnBeforeWritePdu()
			writeStatements.Do(Code.This.MethodOf(OnBeforeWritePdu).Call(writerArg));

			var typeByteOrder = SyntaxHelpers.GetByteOrder(typeSym);

			bool isByteOrderInvariant = true;
			foreach (var member in pduType.members)
			{
				context.CancellationToken.ThrowIfCancellationRequested();

				if (
					member.IsStatic
					|| member.IsDefined(typeof(PduIgnoreAttribute))
					|| member.IsDefined(typeof(PduParameterAttribute))
					)
					continue;

				this.GenerateFieldReadWrite(
					pduType,
					context,
					readStatements_LE,
					readStatements_BE,
					writeStatements_LE,
					writeStatements_BE,
					member,
					ref isByteOrderInvariant);
			}

			List<MemberDeclarationSyntax> members = new List<MemberDeclarationSyntax>(8);

			// OnBeforeRead
			members.Add(Code.DeclarePartialMethod(OnBeforeReadPdu,
				Code.DeclareParameter(WriterParamName, Code.TypeRef(readerTypeRef))
			));
			// OnAfterRead
			members.Add(Code.DeclarePartialMethod(OnAfterReadPdu,
				Code.DeclareParameter(WriterParamName, Code.TypeRef(readerTypeRef))
			));
			// OnBeforeWrite
			members.Add(Code.DeclarePartialMethod(OnBeforeWritePdu,
				Code.DeclareParameter(WriterParamName, Code.TypeRef(writerTypeRef))
			));
			// OnAfterWrite
			members.Add(Code.DeclarePartialMethod(OnAfterWritePdu,
				Code.DeclareParameter(WriterParamName, Code.TypeRef(writerTypeRef))
			));

			// void ReadFrom(IByteSource source, ...)
			members.Add(Code.DeclareMethod(ReadFromName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
				BuildParamsList(pduType, Code.DeclareParameter(ReaderParamName, Code.TypeRef(readerTypeRef))),
				(typeByteOrder.HasValue || isByteOrderInvariant) ? Code.This.MethodOf(ReadFromName).Call(BuildArgList(pduType, readerArg, (typeByteOrder ?? PduByteOrder.Inherit).EnumField()))
				: Code.TypeRef(typeof(NotSupportedException)).New(Code.Primitive("The struct PDU does not have a defined order and must be provided.")).Throw()
				));
			// void WriteTo(ByteWriter writer, ...)
			members.Add(Code.DeclareMethod(WriteToName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
				BuildParamsList(pduType, Code.DeclareParameter(WriterParamName, writerTypeRef.AsTypeRef())),
				(typeByteOrder.HasValue || isByteOrderInvariant) ? Code.This.MethodOf(WriteToName).Call(BuildArgList(pduType, writerArg, (typeByteOrder ?? PduByteOrder.Inherit).EnumField()))
				: Code.TypeRef(typeof(NotSupportedException)).New(Code.Primitive("The struct PDU does not have a defined order and must be provided.")).Throw()
				));

			if (isByteOrderInvariant)
			{
				readStatements.AddRange(readStatements_LE);
				writeStatements.AddRange(writeStatements_LE);

				// this.OnAfterReadPdu()
				readStatements.Do(Code.This.MethodOf(OnAfterReadPdu).Call(readerArg));

				// void ReadFrom(IByteSource source, ByteOrder byteOrder, ...)
				members.Add(Code.DeclareMethod(ReadFromName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
					BuildParamsList(pduType,
						Code.DeclareParameter(ReaderParamName, readerTypeRef.AsTypeRef()),
						Code.DeclareParameter(ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
					),
					readStatements.AsBlock()));


				// this.OnAfterWritePdu()
				writeStatements.Do(Code.This.MethodOf(OnAfterWritePdu).Call(writerArg));
				// void WriteTo(ByteWriter writer, ByteOrder byteOrder, ...)
				members.Add(Code.DeclareMethod(WriteToName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
					BuildParamsList(pduType,
						Code.DeclareParameter(WriterParamName, writerTypeRef.AsTypeRef()),
						Code.DeclareParameter(ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
					), writeStatements.AsBlock()));
			}
			else
			{
				// void ReadFrom_LE(IByteSource source, ByteOrder byteOrder)
				members.Add(Code.DeclareMethod(ReadFromName + LE_Suffix, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
					new ParameterSyntax[] {
						Code.DeclareParameter(ReaderParamName, readerTypeRef.AsTypeRef()),
						Code.DeclareParameter(ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
					},
					readStatements_LE.AsBlock()));
				// void ReadFrom_BE(IByteSource source, ByteOrder byteOrder)
				members.Add(Code.DeclareMethod(ReadFromName + BE_Suffix, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
					new ParameterSyntax[] {
						Code.DeclareParameter(ReaderParamName, readerTypeRef.AsTypeRef()),
						Code.DeclareParameter(ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
					}, readStatements_BE.AsBlock()));
				// void ReadFrom(IByteSource source, ByteOrder byteOrder)
				readStatements.Switch(byteOrderArg,
					Code.EnumField(PduByteOrder.LittleEndian).Case(Code.This.MethodOf(ReadFromName + LE_Suffix).Call(readerArg, byteOrderArg).Do()),
					Code.EnumField(PduByteOrder.BigEndian).Case(Code.This.MethodOf(ReadFromName + BE_Suffix).Call(readerArg, byteOrderArg).Do()),
					Code.DefaultCase(Code.TypeRef(typeof(ArgumentOutOfRangeException)).New(Code.Primitive(ByteOrderParamName)).Throw().Do())
					);
				// this.OnAfterReadPdu()
				readStatements.Do(Code.This.MethodOf(OnAfterReadPdu).Call(readerArg));
				members.Add(Code.DeclareMethod(ReadFromName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
					BuildParamsList(pduType,
						Code.DeclareParameter(ReaderParamName, readerTypeRef.AsTypeRef()),
						Code.DeclareParameter(ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
					),
					readStatements.AsBlock()));

				// void WriteTo_LE(ByteWriter writer, ByteOrder byteOrder)
				members.Add(Code.DeclareMethod(WriteToName + LE_Suffix, Code.TypeRef(typeof(void)), Accessibility.Public, inherit, new ParameterSyntax[] {
					Code.DeclareParameter(WriterParamName, writerTypeRef.AsTypeRef()),
					Code.DeclareParameter(ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
					}, writeStatements_LE.AsBlock()));
				// void WriteTo_BE(ByteWriter writer, ByteOrder byteOrder)
				members.Add(Code.DeclareMethod(WriteToName + BE_Suffix, Code.TypeRef(typeof(void)), Accessibility.Public, inherit, new ParameterSyntax[] {
					Code.DeclareParameter(WriterParamName, writerTypeRef.AsTypeRef()),
					Code.DeclareParameter(ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
					}, writeStatements_BE.AsBlock()));

				// void WriteTo(ByteWriter writer, ByteOrder byteOrder)
				writeStatements.Switch(byteOrderArg,
					Code.EnumField(PduByteOrder.LittleEndian).Case(Code.This.MethodOf(WriteToName + LE_Suffix).Call(writerArg, byteOrderArg).Do()),
					Code.EnumField(PduByteOrder.BigEndian).Case(Code.This.MethodOf(WriteToName + BE_Suffix).Call(writerArg, byteOrderArg).Do())
					);
				// this.OnAfterWritePdu()
				writeStatements.Do(Code.This.MethodOf(OnAfterWritePdu).Call(writerArg));
				members.Add(Code.DeclareMethod(WriteToName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
					BuildParamsList(pduType,
						Code.DeclareParameter(WriterParamName, writerTypeRef.AsTypeRef()),
						Code.DeclareParameter(ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
					),
					writeStatements.AsBlock()));
			}


			if (size >= 0)
				members.Add(Code.DeclareProperty(PduStructSizeName, Code.TypeRef(typeof(int)), Accessibility.Public, InheritModifier.Static, Code.Primitive(size)));

			var genType = Code.DeclareType(
				structType.DeclarationKind(),
				structType.Name,
				baseTypes,
				pduType.decl.TypeParameterList,
				members.ToArray()
				);

			MemberDeclarationSyntax? topNode;
			var ns = structType.ContainingNamespace.FullName();
			if (!string.IsNullOrEmpty(ns))
			{
				topNode = Code.DeclareNamespace(ns, genType);
			}
			else
			{
				topNode = genType;
			}

			var comp = SyntaxFactory.CompilationUnit(
				default,
				SyntaxFactory.List<UsingDirectiveSyntax>(new UsingDirectiveSyntax[] { Code.Using(Titanis_IO) }),
				default,
				SyntaxFactory.List<MemberDeclarationSyntax>(new MemberDeclarationSyntax[] { topNode })
				);

			comp = comp.NormalizeWhitespace();
			string source = comp.ToFullString();
			return source;
		}

		private static ParameterSyntax[] BuildParamsList(
			PduTypeInfo pduType,
			params ParameterSyntax[] prefixParams
			)
		{
			if (pduType.parameters.Length > 0)
			{
				List<ParameterSyntax> parmsList = new List<ParameterSyntax>(prefixParams);

				foreach (var param in pduType.parameters)
				{
					parmsList.Add(Code.DeclareParameter(param.field.Name, param.type));
				}

				var parms = parmsList.ToArray();
				return parms;
			}
			else
				return prefixParams;
		}

		private static ExpressionSyntax[] BuildArgList(
			PduTypeInfo pduType,
			params ExpressionSyntax[] prefixArgs
			)
		{
			List<ExpressionSyntax> parmsList = new List<ExpressionSyntax>(prefixArgs);
			foreach (var parm in pduType.parameters)
			{
				parmsList.Add(SyntaxFactory.IdentifierName(parm.field.Name));
			}
			var parms = parmsList.ToArray();
			return parms;
		}

		private static PduParamInfo[] GetPduParameters(ITypeSymbol typeSym)
		{
			List<PduParamInfo> parameters = new List<PduParamInfo>();
			GetPduParametersInto(typeSym, parameters, true);
			return parameters.ToArray();
		}
		private static void GetPduParametersInto(ITypeSymbol typesym, List<PduParamInfo> parameters, bool local)
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
					parameters.Add(new PduParamInfo { field = member, local = local, type = type });
				}
			}
		}

		private static ThisExpressionSyntax _code_this = Code.This;
		private static ExpressionSyntax readerArg = Code.VarRef(ReaderParamName);
		private static ExpressionSyntax writerArg = Code.VarRef(WriterParamName);
		private static ExpressionSyntax byteOrderArg = Code.VarRef(ByteOrderParamName);

		/// <summary>
		/// Generates the statements to read and write a field.
		/// </summary>
		/// <param name="pduType">PDU type</param>
		/// <param name="context"><see cref="SourceProductionContext"/></param>
		/// <param name="readStatements_LE">List to add read statements to</param>
		/// <param name="readStatements_BE">List to add read statements to</param>
		/// <param name="writeStatements_LE">List to add write statements to</param>
		/// <param name="writeStatements_BE">List to add write statements to</param>
		/// <param name="member">Member to read and write</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		private bool GenerateFieldReadWrite(
			PduTypeInfo pduType,
			SourceProductionContext context,
			List<StatementSyntax> readStatements_LE,
			List<StatementSyntax> readStatements_BE,
			List<StatementSyntax> writeStatements_LE,
			List<StatementSyntax> writeStatements_BE,
			ISymbol member,
			ref bool byteOrderInvariant)
		{
			var structType = pduType.typeSymbol;
			var attrPduField = member.TryGetAttribute(typeof(PduFieldAttribute));
			var attrConditional = member.TryGetAttribute(typeof(PduConditionalAttribute));

			ITypeSymbol fieldType;
			SyntaxToken declarator;
			switch (member.Kind)
			{
				case SymbolKind.Field:
					{
						var field = (IFieldSymbol)member;
						if (field.AssociatedSymbol != null)
							// This is a backing field
							return false;

						declarator = ((VariableDeclaratorSyntax)field.DeclaringSyntaxReferences[0].GetSyntax(context.CancellationToken)).Identifier;
						fieldType = field.Type;
					}
					break;
				case SymbolKind.Property:
					{
						var prop = (IPropertySymbol)member;
						declarator = ((PropertyDeclarationSyntax)prop.DeclaringSyntaxReferences[0].GetSyntax(context.CancellationToken)).Identifier;
						if (prop.SetMethod == null)
						{
							if (attrPduField != null)
							{
								context.ReportDiagnostic(Diagnostic.Create(
									PduDiagnostics.MissingSetterError_Type_Member,
									declarator.GetLocation(),
									pduType.typeSymbol.FullName(), prop.Name
									));
							}
							else
								return false;
						}
						fieldType = prop.Type;
					}
					break;
				default:
					return false;
			}

			ExpressionSyntax fieldRef = _code_this.FieldOf(member.Name);


			var attrAlign = member.TryGetAttribute(typeof(PduAlignmentAttribute));
			if (attrAlign != null)
			{
				if (attrAlign.TryGetArgument<int>(0, out int alignment))
				{
					writeStatements_LE.Do(writerArg.MethodOf("Align").Call(Code.Primitive(alignment)));
					writeStatements_BE.Do(writerArg.MethodOf("Align").Call(Code.Primitive(alignment)));
					readStatements_LE.Do(readerArg.MethodOf("Align").Call(Code.Primitive(alignment)));
					readStatements_BE.Do(readerArg.MethodOf("Align").Call(Code.Primitive(alignment)));
				}
			}

			// Determine how to read/write the field
			var customReadMethod = attrPduField.TryGetArgument(nameof(PduFieldAttribute.ReadMethod), pduType.typeSymbol, member, context);
			var customWriteMethod = attrPduField.TryGetArgument(nameof(PduFieldAttribute.WriteMethod), pduType.typeSymbol, member, context);

			if (
				(customReadMethod != null)
				|| (customWriteMethod != null)
				)
			{
				// The field has custom ReadMethod and WriteMethod specified

				//if (customReadMethod == null)
				//{
				//	context.ReportDiagnostic(Diagnostic.Create(
				//		PduDiagnostics.MissingReadWriteMethod_3,
				//		(readNameNode ?? attrPduField.ApplicationSyntaxReference.GetSyntax(context.CancellationToken)).GetLocation(),
				//		member.Name, structType.Name, customReadMethod
				//		));
				//}
				//if (customWriteMethod == null)
				//{
				//	context.ReportDiagnostic(Diagnostic.Create(
				//		PduDiagnostics.MissingReadWriteMethod_3,
				//		(writeNameNode ?? attrPduField.ApplicationSyntaxReference.GetSyntax(context.CancellationToken)).GetLocation(),
				//		member.Name, structType.Name, customWriteMethod
				//		));
				//}

				// TODO: Provide a way to indicate byte order invariance
				// For now, this breaks the contract
				byteOrderInvariant = false;

				// TODO: Verify signature of read/write methods

				if (customReadMethod != null)
				{
					ExpressionSyntax fieldReadExpr = Code.VarRef(customReadMethod.Name).Call(readerArg, byteOrderArg);

					readStatements_LE.Assign(fieldRef, fieldReadExpr);
					readStatements_BE.Assign(fieldRef, fieldReadExpr);
				}
				if (customWriteMethod != null)
				{
					writeStatements_LE.Do(Code.VarRef(customWriteMethod.Name).Call(writerArg, fieldRef, byteOrderArg));
					writeStatements_BE.Do(Code.VarRef(customWriteMethod.Name).Call(writerArg, fieldRef, byteOrderArg));
				}
			}
			else
			{
				// Save original field ref (used for arrays)
				var origFieldRef = fieldRef;
				var fieldReadRef = fieldRef;
				var fieldWriteRef = fieldRef;


				// Check nullable
				bool isNullableRef = fieldType.IsReferenceType && (fieldType.NullableAnnotation == NullableAnnotation.Annotated);
				bool isNullableValue = !fieldType.IsReferenceType && (fieldType is INamedTypeSymbol { ConstructedFrom: { SpecialType: SpecialType.System_Nullable_T } });

				var condReadBlock_LE = readStatements_LE;
				var condReadBlock_BE = readStatements_BE;
				var condWriteBlock_LE = writeStatements_LE;
				var condWriteBlock_BE = writeStatements_BE;

				if (attrConditional is not null)
				{
					readStatements_LE = new List<StatementSyntax>();
					readStatements_BE = new List<StatementSyntax>();
					writeStatements_LE = new List<StatementSyntax>();
					writeStatements_BE = new List<StatementSyntax>();
				}

				if ((isNullableRef || isNullableValue))
				{
					if (attrConditional is null)
					{
						context.ReportDiagnostic(Diagnostic.Create(
							PduDiagnostics.PduConditionalMissing_2,
							declarator.GetLocation(),
							pduType.typeSymbol.FullName(), member.Name
							));
					}
					else
					{
						if (isNullableValue)
						{
							var named = (INamedTypeSymbol)fieldType;
							fieldType = named.TypeArguments[0];

							fieldWriteRef = fieldWriteRef.PropertyOf(nameof(Nullable<int>.Value));
						}
					}
				}

				if (fieldType.SpecialType == SpecialType.System_String)
				{
					var stringAttr = member.TryGetAttribute(typeof(PduStringAttribute));
					//context.ReportDiagnostic(PduStringOnNonString_2.Create(stringAttr.ApplicationSyntaxReference.GetLocation(), member.Name, pduType.typeSymbol.Name));
					if (stringAttr != null)
					{
						stringAttr.TryGetArgument(0, out int charset);
						var stringLengthMember = stringAttr.TryGetArgument(1, pduType.typeSymbol, member, context);

						ExpressionSyntax? encodingRef;
						switch ((CharSet)charset)
						{
							case CharSet.Ansi:
								encodingRef = Code.TypeRef(typeof(Encoding)).PropertyOf(nameof(Encoding.ASCII));
								break;
							case CharSet.Unicode:
								encodingRef = Code.TypeRef(typeof(Encoding)).PropertyOf(nameof(Encoding.Unicode));
								break;
							default:
								encodingRef = null;
								context.ReportDiagnostic(PduDiagnostics.PduStringBadEncoding_0.Create((stringAttr.ArgSyntaxOrAttribute(0).GetLocation())));
								break;
						}
						ExpressionSyntax? lengthRef = null;
						if (stringLengthMember is not null)
						{
							lengthRef = Code.This.PropertyOf(stringLengthMember.Name);
							if (stringLengthMember.Kind == SymbolKind.Method)
								lengthRef = lengthRef.Call();
						}

						if (encodingRef is not null && lengthRef is not null)
						{
							readStatements_LE.Assign(fieldReadRef, readerArg.MethodOf("ReadString").Call(lengthRef, encodingRef));
							readStatements_BE.Assign(fieldReadRef, readerArg.MethodOf("ReadString").Call(lengthRef, encodingRef));

							writeStatements_LE.Do(writerArg.MethodOf("WriteString").Call(fieldWriteRef, encodingRef));
							writeStatements_BE.Do(writerArg.MethodOf("WriteString").Call(fieldWriteRef, encodingRef));
						}
					}
					else
					{
						context.ReportDiagnostic(PduDiagnostics.PduStringMissingOnString_Type_Member.Create(declarator.GetLocation(), pduType.typeSymbol.FullName(), member.Name));
					}
				}
				else if (member.IsDefined(typeof(PduPositionAttribute)))
				{
					// Check the type
					if (fieldType.SpecialType != SpecialType.System_Int64)
					{
						context.ReportDiagnostic(PduDiagnostics.PduPositionNotLong_Type_Field.Create(member.DeclaringSyntaxReferences[0].GetLocation(), pduType.typeSymbol.FullName(), member.Name));
					}

					readStatements_LE.Assign(fieldReadRef, readerArg.FieldOf(PositionName));
					readStatements_BE.Assign(fieldReadRef, readerArg.FieldOf(PositionName));
					writeStatements_LE.Assign(fieldWriteRef, writerArg.FieldOf(PositionName));
					writeStatements_BE.Assign(fieldWriteRef, writerArg.FieldOf(PositionName));
				}
				else
				{

					var arrayReadBlock_LE = readStatements_LE;
					var arrayReadBlock_BE = readStatements_BE;
					var arrayWriteBlock_LE = writeStatements_LE;
					var arrayWriteBlock_BE = writeStatements_BE;

					// Check for array
					var elementType = fieldType;
					bool isByteArray = false;
					ExpressionSyntax? arraySizeExpr = null;
					bool encodeAsArray = false;
					if (elementType.TypeKind == TypeKind.Array)
					{
						// Get array size
						var attrArraySize = member.TryGetAttribute(typeof(PduArraySizeAttribute));
						if (attrArraySize == null)
						{
							context.ReportDiagnostic(Diagnostic.Create(
								PduDiagnostics.MissingCountAttribute_Type_Member,
								declarator.GetLocation(),
								pduType.typeSymbol.FullName(), member.Name
								));
						}
						else
						{
							var countValue = attrArraySize.ConstructorArg(0);

							if (countValue is int n && n >= 0)
							{
								arraySizeExpr = Code.Primitive(countValue);
							}
							else if (countValue is string str)
							{
								var countSym = attrArraySize.TryGetArgument(0, pduType.typeSymbol, member, context);
								if (countSym != null)
								{
									if (countSym.Kind is SymbolKind.Method)
									{
										arraySizeExpr = Code.VarRef(str).Call();
									}
									else if (countSym.Kind is SymbolKind.Field or SymbolKind.Property)
									{
										arraySizeExpr = Code.VarRef(str);
									}
								}
							}

							if (arraySizeExpr == null)
							{
								context.ReportDiagnostic(Diagnostic.Create(
									PduDiagnostics.BadCountAttribute_Type_Member,
									attrArraySize.ApplicationSyntaxReference.GetLocation(),
									pduType.typeSymbol.FullName(), member.Name
									));
							}
						}

						if (((IArrayTypeSymbol)elementType).ElementType.SpecialType == SpecialType.System_Byte)
							isByteArray = true;
						else
						{
							encodeAsArray = true;

							fieldReadRef = fieldWriteRef = Code.VarRef(ElementVarName);

							readStatements_LE = new List<StatementSyntax>();
							readStatements_BE = new List<StatementSyntax>();
							writeStatements_LE = new List<StatementSyntax>();
							writeStatements_BE = new List<StatementSyntax>();

							elementType = ((IArrayTypeSymbol)elementType).ElementType;

							readStatements_LE.DeclareVariable(Code.TypeRef(elementType), ElementVarName);
							readStatements_BE.DeclareVariable(Code.TypeRef(elementType), ElementVarName);

							// array[i] = elem
							writeStatements_LE.DeclareVariable(ElementVarName, Code.VarRef(ArrayVarName).Indexer(Code.VarRef(LoopVarName)));
							writeStatements_BE.DeclareVariable(ElementVarName, Code.VarRef(ArrayVarName).Indexer(Code.VarRef(LoopVarName)));
						}
					}

					// Handle enums
					var fieldSerType = elementType;
					if (fieldSerType.TypeKind == TypeKind.Enum)
					{
						fieldSerType = ((INamedTypeSymbol)fieldSerType).EnumUnderlyingType;
					}

					(var readMethodName_LE, var writeMethodName_LE, var readMethodName_BE, var writeMethodName_BE) = fieldSerType.SpecialType switch
					{
						SpecialType.System_Byte => ("ReadByte", "WriteByte", "ReadByte", "WriteByte"),
						SpecialType.System_SByte => ("ReadSByte", "WriteSByte", "ReadSByte", "WriteSByte"),
						SpecialType.System_Int16 => ("ReadInt16LE", "WriteInt16LE", "ReadInt16BE", "WriteInt16BE"),
						SpecialType.System_UInt16 => ("ReadUInt16LE", "WriteUInt16LE", "ReadUInt16BE", "WriteUInt16BE"),
						SpecialType.System_Int32 => ("ReadInt32LE", "WriteInt32LE", "ReadInt32BE", "WriteInt32BE"),
						SpecialType.System_UInt32 => ("ReadUInt32LE", "WriteUInt32LE", "ReadUInt32BE", "WriteUInt32BE"),
						SpecialType.System_Int64 => ("ReadInt64LE", "WriteInt64LE", "ReadInt64BE", "WriteInt64BE"),
						SpecialType.System_UInt64 => ("ReadUInt64LE", "WriteUInt64LE", "ReadUInt64BE", "WriteUInt64BE"),
						_ => fieldSerType.FullName() switch
						{
							"System.Guid" => ("ReadGuid", "WriteGuid", "ReadGuid", "WriteGuid"),
							_ => default
						}
					};

					// Check byte order
					//if (!memberByteOrder.HasValue)
					//{
					//	context.ReportDiagnostic(Diagnostic.Create(
					//		MissingByteOrderError,
					//		Location.Create(declarator.SyntaxTree, declarator.Span),
					//		pduType.typeSymbol.Name
					//		));
					//	memberByteOrder = PduByteOrder.LittleEndian;
					//}

					var memberByteOrder = SyntaxHelpers.GetDeclaredByteOrder(member);

					if (readMethodName_LE != readMethodName_BE
						|| writeMethodName_LE != writeMethodName_BE
						)
						byteOrderInvariant = false;

					if (readMethodName_LE != null)
					{
						if (
							!memberByteOrder.HasValue
							&& (readMethodName_LE == readMethodName_BE)
							&& (writeMethodName_LE == writeMethodName_BE)
							)
							memberByteOrder = PduByteOrder.LittleEndian;

						ExpressionSyntax fieldWriteExpr = fieldWriteRef;
						if (memberByteOrder.HasValue)
						{
							// The member defines the byte order.  Always use it.

							(var readMethodName, var writeMethodName) = memberByteOrder switch
							{
								PduByteOrder.LittleEndian => (readMethodName_LE, writeMethodName_LE),
								PduByteOrder.BigEndian => (readMethodName_BE, writeMethodName_BE),
								_ => throw new NotImplementedException()
							};

							ExpressionSyntax fieldReadExpr = readerArg.MethodOf(readMethodName).Call();

							if (elementType.TypeKind == TypeKind.Enum)
							{
								fieldReadExpr = fieldReadExpr.Cast(elementType);
								fieldWriteExpr = fieldWriteExpr.Cast(fieldSerType);
							}

							// Read
							readStatements_LE.Assign(fieldReadRef, fieldReadExpr);
							readStatements_BE.Assign(fieldReadRef, fieldReadExpr);
							// Write
							writeStatements_LE.Do(writerArg.MethodOf(writeMethodName).Call(fieldWriteExpr));
							writeStatements_BE.Do(writerArg.MethodOf(writeMethodName).Call(fieldWriteExpr));
						}
						else
						{
							ExpressionSyntax fieldReadExpr_LE = readerArg.MethodOf(readMethodName_LE).Call();
							ExpressionSyntax fieldReadExpr_BE = readerArg.MethodOf(readMethodName_BE).Call();

							if (elementType.TypeKind == TypeKind.Enum)
							{
								fieldReadExpr_LE = fieldReadExpr_LE.Parenthesize().Cast(elementType);
								fieldReadExpr_BE = fieldReadExpr_BE.Parenthesize().Cast(elementType);
								fieldWriteExpr = fieldWriteExpr.Cast(fieldSerType);
							}

							// Read
							readStatements_LE.Assign(fieldWriteRef, fieldReadExpr_LE);
							readStatements_BE.Assign(fieldWriteRef, fieldReadExpr_BE);

							// Write
							writeStatements_LE.Do(writerArg.MethodOf(writeMethodName_LE).Call(fieldWriteExpr));
							writeStatements_BE.Do(writerArg.MethodOf(writeMethodName_BE).Call(fieldWriteExpr));
						}
					}
					else if (isByteArray)
					{
						if (arraySizeExpr != null)
						{
							ExpressionSyntax fieldReadExpr = readerArg.MethodOf(ReadBytesName).Call(arraySizeExpr);
							ExpressionSyntax fieldWriteExpr = fieldWriteRef;

							// Read
							readStatements_LE.Assign(fieldWriteRef, fieldReadExpr);
							readStatements_BE.Assign(fieldWriteRef, fieldReadExpr);

							// Write
							writeStatements_LE.Do(writerArg.MethodOf(WriteBytesName).Call(fieldWriteExpr));
							writeStatements_BE.Do(writerArg.MethodOf(WriteBytesName).Call(fieldWriteExpr));
						}
						else
						{
							// If missing, an errer was already reported above
						}
					}
					else if (IsPduStruct(elementType))
					{
						var fieldParameters = GetPduParameters(elementType);

						// Check for an embedded PDU struct
						ExpressionSyntax byteOrderValue =
							memberByteOrder.HasValue ? Code.EnumField(memberByteOrder.Value)
							: byteOrderArg;

						var attrArgs = member.TryGetAttribute(typeof(PduArgumentsAttribute));
						ISymbol?[] args;
						if (attrArgs != null && attrArgs.ConstructorArguments.Length == 1)
						{
							var argNames = attrArgs.ConstructorArguments[0].Values;
							args = new ISymbol[argNames.Length];
							for (int i = 0; i < attrArgs.ConstructorArguments.Length; i++)
							{
								var argName = argNames[i];
								var argSym = pduType.members.FirstOrDefault(r => r.Name == (string?)argName.Value);
								if (argSym is null)
								{
									context.ReportDiagnostic(Diagnostics.UndefinedMemberRef_Type_Member_AttrType_AttributeArg_Member.Create(
										attrArgs.ApplicationSyntaxReference.GetLocation(),
										pduType.typeSymbol.Name, member.Name, nameof(PduArgumentsAttribute), "[0]", argName
										));
								}
								args[i] = argSym;
							}
						}
						else
						{
							args = Array.Empty<ISymbol>();
						}

						// Check argument count
						if (fieldParameters.Length != args.Length)
						{
							context.ReportDiagnostic(PduDiagnostics.PduArgCountMismatch_Type_Member_NestedType.Create(
								member.DeclaringSyntaxReferences[0].GetLocation(),
								pduType.typeSymbol.Name, member.Name, fieldType.Name
								));
							return false;
						}

						// Check argument types
						List<ExpressionSyntax> readArgs = new List<ExpressionSyntax>(1 + args.Length)
						{
							byteOrderValue
						};
						List<ExpressionSyntax> writeArgs = new List<ExpressionSyntax>(2 + args.Length)
						{
							fieldWriteRef, byteOrderValue
						};

						List<ITypeSymbol> readMethodGenericArgs = new List<ITypeSymbol>(1 + args.Length)
						{
							elementType
						};

						{
							bool argMismatch = false;
							for (int i = 0; i < args.Length; i++)
							{
								var argSym = args[i];
								var param = fieldParameters[i];

								if (!SymbolEqualityComparer.Default.Equals(argSym.DataType(), param.type))
								{
									argMismatch = true;
									context.ReportDiagnostic(PduDiagnostics.PduArgTypeMismatch_Type_Member_ArgIndex_ArgMember_ParamType.Create(
										attrArgs.ApplicationSyntaxReference.GetLocation(),
										pduType.typeSymbol.Name,
										member.Name,
										i,
										argSym.DataType().Name,
										param.type.Name
										));
								}

								ExpressionSyntax argExpr = Code.VarRef(argSym.Name);
								if (argSym is IMethodSymbol)
									argExpr = argExpr.Call();
								readArgs.Add(argExpr);
								writeArgs.Add(argExpr);

								readMethodGenericArgs.Add(param.type);
							}

							if (argMismatch)
								return false;
						}

						ExpressionSyntax fieldReadExpr = readerArg.MethodOf(ReadPduStructName, readMethodGenericArgs.ToArray()).Call(readArgs.ToArray());
						ExpressionSyntax fieldWriteExpr = fieldWriteRef;

						readStatements_LE.Assign(fieldReadRef, fieldReadExpr);
						readStatements_BE.Assign(fieldReadRef, fieldReadExpr);
						writeStatements_LE.Do(writerArg.MethodOf(WritePduStructName).Call(writeArgs.ToArray()));
						writeStatements_BE.Do(writerArg.MethodOf(WritePduStructName).Call(writeArgs.ToArray()));
					}
					else
					{
						context.ReportDiagnostic(Diagnostic.Create(
							PduDiagnostics.CantSerializeError_Type_Member_FieldType,
							Location.Create(declarator.SyntaxTree, declarator.Span),
							pduType.typeSymbol.FullName(), member.Name, fieldType.FullName()
							));
					}


					// Check for array
					if (encodeAsArray)
					{
						// Create the array loop
						if (arraySizeExpr != null)
						{
							// Read block
							void GenerateReadBlock(
								List<StatementSyntax> readStatements,
								List<StatementSyntax> topReadBlock
								)
							{
								List<StatementSyntax> arrayBlock = new List<StatementSyntax>();
								arrayBlock.DeclareVariable(LimitVarName, arraySizeExpr);

								// var array = new TElement[count];
								arrayBlock.DeclareVariable(
									ArrayVarName,
									Code.TypeRef(elementType).NewArray(Code.VarRef(LimitVarName))
									);
								// prop = array;
								arrayBlock.Assign(origFieldRef, Code.VarRef(ArrayVarName));

								// array[i] = elem
								readStatements.Assign(
									Code.VarRef(ArrayVarName).Indexer(Code.VarRef(LoopVarName)),
									Code.VarRef(ElementVarName)
									);

								arrayBlock.Add(Code.For(LoopVarName, Code.VarRef(LimitVarName), readStatements.AsBlock()));

								topReadBlock.Add(arrayBlock.AsBlock());
							}
							GenerateReadBlock(readStatements_LE, arrayReadBlock_LE);
							GenerateReadBlock(readStatements_BE, arrayReadBlock_BE);

							// Write block
							void GenerateWriteBlock(
								List<StatementSyntax> writeStatements,
								List<StatementSyntax> topWriteBlock
								)
							{
								List<StatementSyntax> arrayBlock = new List<StatementSyntax>();

								// var array = new TElement[count];
								arrayBlock.DeclareVariable(
									ArrayVarName,
									origFieldRef
									);
								// prop = array;
								arrayBlock.Assign(origFieldRef, Code.VarRef(ArrayVarName));

								arrayBlock.IfThen(
									Code.VarRef(ArrayVarName).IsNotNull(),
									Code.For(
										LoopVarName,
										Code.VarRef(ArrayVarName).PropertyOf(nameof(Array.Length)),
										writeStatements.AsBlock())
									);

								topWriteBlock.Add(arrayBlock.AsBlock());
							}
							GenerateWriteBlock(writeStatements_LE, arrayWriteBlock_LE);
							GenerateWriteBlock(writeStatements_BE, arrayWriteBlock_BE);
						}
					}
				}

				if (attrConditional is not null)
				{
					var condSymbol = attrConditional.TryGetArgument(0, pduType.typeSymbol, member, context);
					if (condSymbol == null)
						return false;

					// Read condition
					{
						ExpressionSyntax condReadRef = Code.VarRef(condSymbol.Name);
						if (condSymbol.Kind == SymbolKind.Method)
							condReadRef = condReadRef.Call();
						condReadBlock_LE.IfThen(condReadRef, SyntaxFactory.Block(readStatements_LE));
						condReadBlock_BE.IfThen(condReadRef, SyntaxFactory.Block(readStatements_BE));
					}

					// Write condition
					{
						ExpressionSyntax condWriteRef = Code.VarRef(condSymbol.Name);
						//ExpressionSyntax condWriteRef =
						//	(isNullableRef) ? fieldRef.IsNotNull()
						//	: (isNullableValue) ? fieldRef.Member(nameof(Nullable<int>.HasValue))
						//	: fieldRef;

						condWriteBlock_LE.IfThen(condWriteRef, SyntaxFactory.Block(writeStatements_LE));
						condWriteBlock_BE.IfThen(condWriteRef, SyntaxFactory.Block(writeStatements_BE));
					}
				}
			}

			return true;
		}

		private InheritModifier DetermineInherit(ITypeSymbol typeSym)
		{
			InheritModifier inherit;
			var baseType = typeSym.BaseType;
			if (IsPduStruct(baseType))
				inherit = typeSym.IsSealed ? InheritModifier.SealedOverride : InheritModifier.Override;
			else
				inherit = InheritModifier.Virtual;
			return inherit;
		}
	}
}
