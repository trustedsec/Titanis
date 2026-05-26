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
	public partial class PduStructSourceGenerator : IIncrementalGenerator
	{
		private const string PduStructAttrname = nameof(PduStructAttribute);

		private static ThisExpressionSyntax _code_this = Code.This;
		internal readonly static ExpressionSyntax readerArg = Code.VarRef(PduStructNames.ReaderParamName);
		internal readonly static ExpressionSyntax writerArg = Code.VarRef(PduStructNames.WriterParamName);
		private static ExpressionSyntax byteOrderArg = Code.VarRef(PduStructNames.ByteOrderParamName);


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
				.ForAttributeWithMetadataName("Titanis.PduStructAttribute", PduStructFilterPredicate, GetPduStructOrNullWrapper)
				.Where(t => t is not null)
				.Collect();

			context.RegisterSourceOutput(pduTypes, GeneratePduStructCodeWrapper);
		}


		private bool PduStructFilterPredicate(SyntaxNode node, CancellationToken token)
		{
			return true;
		}

		#region Filtering


		private PduTypeInfo? GetPduStructOrNullWrapper(GeneratorAttributeSyntaxContext context, CancellationToken token)
		{
			return this.GetPduStructOrNull(context, token);
		}
		private PduTypeInfo? GetPduStructOrNull(GeneratorAttributeSyntaxContext context, CancellationToken token)
		{
			var attrData = context.Attributes.First();
			var attr = (AttributeSyntax)attrData.ApplicationSyntaxReference.GetSyntax(token);

			// > AttributeList > <target>
			var typeDecl = context.TargetNode as TypeDeclarationSyntax;
			if (typeDecl is null)
				return null;

			var type = context.SemanticModel.GetDeclaredSymbol(typeDecl, token);
			if (type is null || !(type.TypeKind is TypeKind.Struct or TypeKind.Class))
				return null;

			if (type.HasPduStructAttribute())
			{
				return new PduTypeInfo(
					type,
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
				context.AddSource($"PduStructError.txt", SourceText.From(ex.ToString()));
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
					var fullTypeName = pduType.TypeSymbol.FullName();
					context.CancellationToken.ThrowIfCancellationRequested();
					context.AddSource($"{fullTypeName}.g.cs", code);
				}
			}
		}
		#endregion

		private const string StartOffsetVariable = "__offStart";

		private string? GeneratePduStruct(PduTypeInfo pduType, SourceProductionContext context)
		{
			var structType = pduType.TypeSymbol;
			var model = pduType.model;

			// Check that the declaration is marked `partial`
			if (!pduType.Declaration.IsPartial())
			{
				context.ReportDiagnostic(PduDiagnostics.NoPartialError_1.Create(
					pduType.Declaration.Keyword.GetLocation(),
					pduType.TypeSymbol.Name
					));
				return null;
			}


			// Determine base types
			TypeSyntax[]? implementedBaseTypes = null;
			if (
				// Exclude structs, since structs can't have a base requiring parameters
				(structType.TypeKind is not TypeKind.Class)
				// Base type isn't a PduStruct and therefore doesn't have parameters
				|| !structType.BaseType.IsPduStruct()
				// TODO: Shouldn't this be the PduTypeInfo for the base?
				// Exclude types without parameters
				|| (pduType.Parameters.Length > 0)
				)
			{
				var pduInterfaceTypeRef = SyntaxFactory.ParseTypeName(PduStructNames.IPduStructName);
				if (pduType.Parameters.Length > 0)
				{
					pduInterfaceTypeRef = SyntaxFactory.GenericName(SyntaxFactory.Identifier(PduStructNames.IPduStructName), SyntaxFactory.TypeArgumentList(
						Code.SeparatedList(pduType.Parameters.ConvertAll(r =>
						{
							return r.FieldType.AsTypeRef();
						}))));
				}
				implementedBaseTypes = [pduInterfaceTypeRef];
			}

			// Generate ReadFrom and WriteTo methods
			TypeSyntax readerTypeRef = SyntaxFactory.IdentifierName("TSource");
			//var readerTypeRef = model.Compilation.GetTypeByMetadataName(PduStructNames.ByteSourceName);
			var byteOrderTypeRef = model.Compilation.GetTypeByMetadataName(typeof(PduByteOrder).FullName);
			var writerTypeRef = model.Compilation.GetTypeByMetadataName(PduStructNames.ByteWriterName);

			List<StatementSyntax> readStatements = new List<StatementSyntax>();
			List<StatementSyntax> writeStatements = new List<StatementSyntax>();
			// TODO: Mixed byte order
			//List<StatementSyntax> readStatements_LE = new List<StatementSyntax>();
			//List<StatementSyntax> readStatements_BE = new List<StatementSyntax>();
			//List<StatementSyntax> writeStatements_LE = new List<StatementSyntax>();
			//List<StatementSyntax> writeStatements_BE = new List<StatementSyntax>();

			InheritModifier inherit = InheritModifier.Instance;
			bool isChained = false;
			if (structType.TypeKind is TypeKind.Class)
			{
				// TODO: Mixed byte order
				//List<ExpressionSyntax> readBaseArgList = new List<ExpressionSyntax>() { readerArg, byteOrderArg };
				//List<ExpressionSyntax> writeBaseArgList = new List<ExpressionSyntax>() { writerArg, byteOrderArg };
				List<ExpressionSyntax> readBaseArgList = new List<ExpressionSyntax>() { readerArg };
				List<ExpressionSyntax> writeBaseArgList = new List<ExpressionSyntax>() { writerArg };
				foreach (var baseParam in pduType.Parameters)
				{
					if (baseParam.IsLocal)
					{
						readStatements.Assign(Code.This.FieldOf(baseParam.Member.Name), Code.VarRef(baseParam.Member.Name));
						writeStatements.Assign(Code.This.FieldOf(baseParam.Member.Name), Code.VarRef(baseParam.Member.Name));
					}
					else
					{
						readBaseArgList.Add(Code.VarRef(baseParam.Member.Name));
						writeBaseArgList.Add(Code.VarRef(baseParam.Member.Name));
					}
				}
				var readBaseArgs = readBaseArgList.ToArray();
				var writeBaseArgs = writeBaseArgList.ToArray();

				inherit = structType.DetermineInherit();
				if (inherit != InheritModifier.Virtual)
				{
					isChained = true;
					// base.ReadFrom(...)
					readStatements.Do(Code.Base.MethodOf(PduStructNames.ReadFromName).Call(readBaseArgs));
					// base.WriteTo(...)
					writeStatements.Do(Code.Base.MethodOf(PduStructNames.WriteToName).Call(writeBaseArgs));
				}

				if (pduType.Parameters.Length > 0)
					inherit = InheritModifier.Virtual;
			}

			// Alignment
			if (pduType.Alignment != null)
			{
				var align = pduType.Alignment.Value;
				writeStatements.Do(writerArg.MethodOf(PduStructNames.Align).Call(Code.Primitive(align)));
				readStatements.Do(readerArg.MethodOf(PduStructNames.Align).Call(Code.Primitive(align)));
			}

			// this.OnBeforeReadPdu()
			readStatements.Do(Code.This.MethodOf(PduStructNames.OnBeforeReadPdu).Call(readerArg));
			// this.OnBeforeWritePdu()
			writeStatements.Do(Code.This.MethodOf(PduStructNames.OnBeforeWritePdu).Call(writerArg));

			readStatements.DeclareVariable(StartOffsetVariable, readerArg.MethodOf("Position"));

			bool isByteOrderInvariant = true;
			var fields = pduType.GetFields(new PduTypeContext(context.CancellationToken));
			foreach (var field in fields)
			{
				this.GenerateFieldReadWrite(
					pduType,
					context,
					readStatements,
					writeStatements,
					field,
					pduType.ByteOrder == PduByteOrder.BigEndian,
					out var isFieldByteOrderVariant);
				if (isFieldByteOrderVariant)
					isByteOrderInvariant = false;
			}

			List<MemberDeclarationSyntax> members = new List<MemberDeclarationSyntax>(8);

			// OnBeforeRead
			members.Add(Code.DeclarePartialMethod(PduStructNames.OnBeforeReadPdu, ["TSource"], [Code.Constraint("TSource", [Code.ClassConstraint(), Code.TypeConstraint("IByteSource")])],
				Code.DeclareParameter(PduStructNames.WriterParamName, readerTypeRef)
			));
			// OnAfterRead
			members.Add(Code.DeclarePartialMethod(PduStructNames.OnAfterReadPdu, ["TSource"], [Code.Constraint("TSource", [Code.ClassConstraint(), Code.TypeConstraint("IByteSource")])],
				Code.DeclareParameter(PduStructNames.WriterParamName, readerTypeRef, [SyntaxFactory.Token(SyntaxKind.RefKeyword)])
			));
			// OnBeforeWrite
			members.Add(Code.DeclarePartialMethod(PduStructNames.OnBeforeWritePdu,
				Code.DeclareParameter(PduStructNames.WriterParamName, Code.TypeRef(writerTypeRef))
			));
			// OnAfterWrite
			members.Add(Code.DeclarePartialMethod(PduStructNames.OnAfterWritePdu,
				Code.DeclareParameter(PduStructNames.WriterParamName, Code.TypeRef(writerTypeRef))
			));

			// TODO: For mixed byte order
			//// void ReadFrom(IByteSource source, ...)
			//members.Add(Code.DeclareMethod(PduStructNames.ReadFromName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
			//	BuildParamsList(pduType, Code.DeclareParameter(PduStructNames.ReaderParamName, Code.TypeRef(readerTypeRef))),
			//	Code.This.MethodOf(PduStructNames.ReadFromName).Call(BuildArgList(pduType, readerArg, (typeByteOrder ?? PduByteOrder.Inherit).EnumField()))
			//	));
			//// void WriteTo(ByteWriter writer, ...)
			//members.Add(Code.DeclareMethod(PduStructNames.WriteToName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
			//	BuildParamsList(pduType, Code.DeclareParameter(PduStructNames.WriterParamName, writerTypeRef.AsTypeRef())),
			//	(typeByteOrder.HasValue || isByteOrderInvariant) ? Code.This.MethodOf(PduStructNames.WriteToName).Call(BuildArgList(pduType, writerArg, (typeByteOrder ?? PduByteOrder.Inherit).EnumField()))
			//	: Code.TypeRef(typeof(NotSupportedException)).New(Code.Primitive("The struct PDU does not have a defined order and must be provided.")).Throw()
			//	));

			if (true || isByteOrderInvariant)
			{
				// this.OnAfterReadPdu()
				readStatements.Do(Code.This.MethodOf(PduStructNames.OnAfterReadPdu).Call(readerArg));

				// void ReadFrom(IByteSource source, ByteOrder byteOrder, ...)
				members.Add(Code.DeclareMethod(PduStructNames.ReadFromName, new string[] { "TSource" }, isChained ? default : [Code.Constraint("TSource", [Code.ClassConstraint(), Code.TypeConstraint("IByteSource")])], Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
					BuildParamsList(pduType,
						Code.DeclareParameter(PduStructNames.ReaderParamName, readerTypeRef, [SyntaxFactory.Token(SyntaxKind.RefKeyword)])
					//Code.DeclareParameter(PduStructNames.ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
					),
					readStatements.AsBlock()));


				// this.OnAfterWritePdu()
				writeStatements.Do(Code.This.MethodOf(PduStructNames.OnAfterWritePdu).Call(writerArg));
				// void WriteTo(ByteWriter writer, ByteOrder byteOrder, ...)
				members.Add(Code.DeclareMethod(PduStructNames.WriteToName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
					BuildParamsList(pduType,
						Code.DeclareParameter(PduStructNames.WriterParamName, writerTypeRef.AsTypeRef())
					//Code.DeclareParameter(PduStructNames.ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
					), writeStatements.AsBlock()));
			}
			else
			{
				// TODO: Mixed byte order
				//// void ReadFrom_LE(IByteSource source, ByteOrder byteOrder)
				//members.Add(Code.DeclareMethod(PduStructNames.ReadFromName + PduStructNames.LE_Suffix, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
				//	new ParameterSyntax[] {
				//		Code.DeclareParameter(PduStructNames.ReaderParamName, readerTypeRef.AsTypeRef()),
				//		Code.DeclareParameter(PduStructNames.ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
				//	},
				//	readStatements_LE.AsBlock()));
				//// void ReadFrom_BE(IByteSource source, ByteOrder byteOrder)
				//members.Add(Code.DeclareMethod(PduStructNames.ReadFromName + PduStructNames.BE_Suffix, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
				//	new ParameterSyntax[] {
				//		Code.DeclareParameter(PduStructNames.ReaderParamName, readerTypeRef.AsTypeRef()),
				//		Code.DeclareParameter(PduStructNames.ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
				//	}, readStatements_BE.AsBlock()));
				//// void ReadFrom(IByteSource source, ByteOrder byteOrder)
				//readStatements.Switch(byteOrderArg,
				//	Code.EnumField(PduByteOrder.LittleEndian).Case(Code.This.MethodOf(PduStructNames.ReadFromName + PduStructNames.LE_Suffix).Call(readerArg, byteOrderArg).Do(), Code.Break),
				//	Code.EnumField(PduByteOrder.BigEndian).Case(Code.This.MethodOf(PduStructNames.ReadFromName + PduStructNames.BE_Suffix).Call(readerArg, byteOrderArg).Do(), Code.Break),
				//	Code.DefaultCase(Code.TypeRef(typeof(ArgumentOutOfRangeException)).New(Code.Primitive(PduStructNames.ByteOrderParamName)).Throw().Do())
				//	);
				//// this.OnAfterReadPdu()
				//readStatements.Do(Code.This.MethodOf(PduStructNames.OnAfterReadPdu).Call(readerArg));
				//members.Add(Code.DeclareMethod(PduStructNames.ReadFromName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
				//	BuildParamsList(pduType,
				//		Code.DeclareParameter(PduStructNames.ReaderParamName, readerTypeRef.AsTypeRef()),
				//		Code.DeclareParameter(PduStructNames.ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
				//	),
				//	readStatements.AsBlock()));

				//// void WriteTo_LE(ByteWriter writer, ByteOrder byteOrder)
				//members.Add(Code.DeclareMethod(PduStructNames.WriteToName + PduStructNames.LE_Suffix, Code.TypeRef(typeof(void)), Accessibility.Public, inherit, new ParameterSyntax[] {
				//	Code.DeclareParameter(PduStructNames.WriterParamName, writerTypeRef.AsTypeRef()),
				//	Code.DeclareParameter(PduStructNames.ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
				//	}, writeStatements_LE.AsBlock()));
				//// void WriteTo_BE(ByteWriter writer, ByteOrder byteOrder)
				//members.Add(Code.DeclareMethod(PduStructNames.WriteToName + PduStructNames.BE_Suffix, Code.TypeRef(typeof(void)), Accessibility.Public, inherit, new ParameterSyntax[] {
				//	Code.DeclareParameter(PduStructNames.WriterParamName, writerTypeRef.AsTypeRef()),
				//	Code.DeclareParameter(PduStructNames.ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
				//	}, writeStatements_BE.AsBlock()));

				//// void WriteTo(ByteWriter writer, ByteOrder byteOrder)
				//writeStatements.Switch(byteOrderArg,
				//	Code.EnumField(PduByteOrder.LittleEndian).Case(Code.This.MethodOf(PduStructNames.WriteToName + PduStructNames.LE_Suffix).Call(writerArg, byteOrderArg).Do(), Code.Break),
				//	Code.EnumField(PduByteOrder.BigEndian).Case(Code.This.MethodOf(PduStructNames.WriteToName + PduStructNames.BE_Suffix).Call(writerArg, byteOrderArg).Do(), Code.Break)
				//	);
				//// this.OnAfterWritePdu()
				//writeStatements.Do(Code.This.MethodOf(PduStructNames.OnAfterWritePdu).Call(writerArg));
				//members.Add(Code.DeclareMethod(PduStructNames.WriteToName, Code.TypeRef(typeof(void)), Accessibility.Public, inherit,
				//	BuildParamsList(pduType,
				//		Code.DeclareParameter(PduStructNames.WriterParamName, writerTypeRef.AsTypeRef()),
				//		Code.DeclareParameter(PduStructNames.ByteOrderParamName, byteOrderTypeRef.AsTypeRef())
				//	),
				//	writeStatements.AsBlock()));
			}


			if (pduType.Size >= 0)
				members.Add(Code.DeclareProperty(PduStructNames.PduStructSizeName, Code.TypeRef(typeof(int)), Accessibility.Public, InheritModifier.Static, Code.Primitive(pduType.Size)));

			var genType = Code.DeclareType(
				structType.DeclarationKind(),
				structType.Name,
				implementedBaseTypes,
				pduType.Declaration.TypeParameterList,
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
				SyntaxFactory.List<UsingDirectiveSyntax>(new UsingDirectiveSyntax[] { Code.Using(PduStructNames.Titanis_IO) }),
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
			if (pduType.Parameters.Length > 0)
			{
				List<ParameterSyntax> parmsList = new List<ParameterSyntax>(prefixParams);

				foreach (var param in pduType.Parameters)
				{
					parmsList.Add(Code.DeclareParameter(param.Member.Name, Code.TypeRef(param.FieldType)));
				}

				var parms = parmsList.ToArray();
				return parms;
			}
			else
				return prefixParams;
		}

		// Used for mixed byte order; don't remove
		private static ExpressionSyntax[] BuildArgList(
			PduTypeInfo pduType,
			params ExpressionSyntax[] prefixArgs
			)
		{
			List<ExpressionSyntax> parmsList = new List<ExpressionSyntax>(prefixArgs);
			foreach (var parm in pduType.Parameters)
			{
				parmsList.Add(SyntaxFactory.IdentifierName(parm.Member.Name));
			}
			var parms = parmsList.ToArray();
			return parms;
		}

		/// <summary>
		/// Generates the statements to read and write a field.
		/// </summary>
		/// <param name="pduType">PDU type</param>
		/// <param name="context"><see cref="SourceProductionContext"/></param>
		/// <param name="readStatements">List to add read statements to</param>
		/// <param name="writeStatements">List to add write statements to</param>
		/// <param name="field">Field to read and write</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		private bool GenerateFieldReadWrite(
			PduTypeInfo pduType,
			SourceProductionContext context,
			List<StatementSyntax> readStatements,
			List<StatementSyntax> writeStatements,
			PduFieldInfo field,
			bool isBigEndian,
			out bool byteOrderVariant
			)
		{
			byteOrderVariant = false;

			switch (field.Member.Kind)
			{
				case SymbolKind.Property:
					{
						var prop = (IPropertySymbol)field.Member;
						if (prop.SetMethod == null)
						{
							if (field.Attribute != null)
							{
								context.ReportDiagnostic(Diagnostic.Create(
									PduDiagnostics.MissingSetterError_Type_Member,
									field.Declarator.GetLocation(),
									pduType.TypeSymbol.FullName(), prop.Name
									));
							}
						}
					}
					break;
				case SymbolKind.Field:
					break;
				default:
					return false;
			}

			ITypeSymbol fieldType = field.FieldType;
			SyntaxToken declarator = field.Declarator;

			ExpressionSyntax fieldRef = _code_this.FieldOf(field.Name);
			var member = field.Member;

			if (field.Alignment != null)
			{
				writeStatements.Do(writerArg.MethodOf(PduStructNames.Align).Call(Code.Primitive(field.Alignment.Value)));
				readStatements.Do(readerArg.MethodOf(PduStructNames.Align).Call(Code.Primitive(field.Alignment.Value)));
			}

			if (field.OffsetAttribute != null)
			{
				var offsetSymbol = field.OffsetAttribute.GetArgument<string>(0)?.TryResolveMemberName(member, context);
				readStatements.Assign(readerArg.PropertyOf("Position"), Code.VarRef(StartOffsetVariable).Add(Code.VarRef(offsetSymbol.Name)));
			}


			// Determine how to read/write the field
			var customReadMethod = field.CustomReadMethod.TryResolveMemberName(member, context);
			var customWriteMethod = field.CustomWriteMethod.TryResolveMemberName(member, context);

			if (
				(customReadMethod != null)
				|| (customWriteMethod != null)
				)
			{
				// The field has custom ReadMethod and WriteMethod specified

				// TODO: Provide a way to indicate byte order invariance
				// For now, this breaks the contract
				byteOrderVariant = true;

				// TODO: Verify signature of read/write methods

				if (customReadMethod != null)
					readStatements.Assign(fieldRef, Code.VarRef(customReadMethod.Name).Call(readerArg));
				if (customWriteMethod != null)
					writeStatements.Do(Code.VarRef(customWriteMethod.Name).Call(writerArg, fieldRef));
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

				var condReadBlock = readStatements;
				var condWriteBlock = writeStatements;

				if (field.Condition is not null || field.Case != null)
				{
					readStatements = new List<StatementSyntax>();
					writeStatements = new List<StatementSyntax>();
				}

				// Nullable conditional
				if ((isNullableRef || isNullableValue))
				{
					if (field.Condition is null)
					{
						context.ReportDiagnostic(Diagnostic.Create(
							PduDiagnostics.PduConditionalMissing_2,
							declarator.GetLocation(),
							pduType.TypeSymbol.FullName(), field.Name
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

				// String
				if (fieldType.SpecialType == SpecialType.System_String)
				{
					var stringAttr = field.StringLengthAttribute;
					//context.ReportDiagnostic(PduStringOnNonString_2.Create(stringAttr.ApplicationSyntaxReference.GetLocation(), member.Name, pduType.typeSymbol.Name));
					ExpressionSyntax? lengthRef = null;
					ExpressionSyntax? encodingRef = null;
					if (stringAttr != null)
					{
						var charset = field.StringCharSet;

						switch (charset?.Value ?? CharSet.None)
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


						{
							var stringLengthArg = field.StringLength;
							if (stringLengthArg?.Value is int n)
								lengthRef = Code.Primitive(n);
							else
							{
								var stringLengthMember = stringLengthArg.TryResolveMemberName(member, context);

								if (stringLengthMember is not null)
								{
									lengthRef = Code.This.PropertyOf(stringLengthMember.Name);
									if (stringLengthMember.Kind == SymbolKind.Method)
										lengthRef = lengthRef.Call();
								}
							}
						}
					}

					if (lengthRef is not null && encodingRef is not null)
					{
						readStatements.Assign(fieldReadRef, readerArg.MethodOf(PduStructNames.ReadString).Call(lengthRef, encodingRef));

						writeStatements.Do(writerArg.MethodOf("WriteString").Call(fieldWriteRef, encodingRef));
					}
					else
					{
						// TODO: More accurate/precise error message
						context.ReportDiagnostic(PduDiagnostics.PduStringMissingOnString_Type_Member.Create(declarator.GetLocation(), pduType.TypeSymbol.FullName(), field.Name));
					}
				}
				else if (field.IsPosition)
				{
					// Check the type
					if (fieldType.SpecialType != SpecialType.System_Int64)
					{
						context.ReportDiagnostic(PduDiagnostics.PduPositionNotLong_Type_Field.Create(field.Declarator.GetLocation(), pduType.TypeSymbol.FullName(), field.Name));
					}

					readStatements.Assign(fieldReadRef, readerArg.FieldOf(PduStructNames.PositionName));
					writeStatements.Assign(fieldWriteRef, writerArg.FieldOf(PduStructNames.PositionName));
				}
				else
				{
					var arrayReadBlock = readStatements;
					var arrayWriteBlock = writeStatements;

					// Check for array
					var elementType = fieldType;
					bool isByteArray = false;
					ExpressionSyntax? arraySizeExpr = null;
					bool encodeAsArray = false;
					if ((elementType.TypeKind == TypeKind.Array) || (field.ListAttribute != null))
					{
						if (elementType.TypeKind == TypeKind.Array)
						{
							// Get array size
							var attrArraySize = field.ArraySizeAttribute;
							if (attrArraySize == null)
							{
								context.ReportDiagnostic(Diagnostic.Create(
									PduDiagnostics.MissingCountAttribute_Type_Member,
									declarator.GetLocation(),
									pduType.TypeSymbol.FullName(), field.Name
									));
							}
							else
							{
								var countValue = field.ArrayElementCount;
								if (countValue?.Value is int n && n >= 0)
								{
									arraySizeExpr = Code.Primitive(n);
								}
								else if (countValue?.Value is string str)
								{
									var countSym = countValue.TryResolveMemberName(member, context);
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
										pduType.TypeSymbol.FullName(), field.Name
										));
								}
							}

							if (((IArrayTypeSymbol)elementType).ElementType.SpecialType == SpecialType.System_Byte)
								isByteArray = true;
							else
							{
								encodeAsArray = true;
								elementType = ((IArrayTypeSymbol)elementType).ElementType;
							}
						}
						else if (field.ListAttribute != null)
						{
							var predicateSym = field.ListAttribute.GetArgument<string>(nameof(PduListAttribute.PredicateMember))?.TryResolveMemberName(member, context);
							var sizeSym = field.ListAttribute.GetArgument<string>(nameof(PduListAttribute.SizeMember))?.TryResolveMemberName(member, context);

							string startPosVarName = $"__offListStart_{field.Name}";
							string endPosVarName = $"__offListEnd_{field.Name}";
							MemberAccessExpressionSyntax readerPos = readerArg.PropertyOf("Position");
							if (predicateSym != null)
								readStatements.DeclareVariable(startPosVarName, readerPos);
							if (sizeSym != null)
								readStatements.DeclareVariable(endPosVarName, readerPos.Add(Code.VarRef(sizeSym.Name)));

							var listPosExpr = readerPos.Subtract(Code.VarRef(startPosVarName));

							ExpressionSyntax? predicateExpr;
							if (predicateSym != null)
							{
								predicateExpr = Code.VarRef(predicateSym.Name).Call(readerArg, listPosExpr);
							}
							else if (sizeSym != null)
							{
								predicateExpr = listPosExpr.LessThan(Code.VarRef(endPosVarName));
							}

							elementType = ((IArrayTypeSymbol)elementType).ElementType;

							encodeAsArray = true;
						}

						if (encodeAsArray)
						{
							fieldReadRef = fieldWriteRef = Code.VarRef(PduStructNames.ElementVarName);

							readStatements = new List<StatementSyntax>();
							writeStatements = new List<StatementSyntax>();

							readStatements.DeclareVariable(Code.TypeRef(elementType), PduStructNames.ElementVarName);

							// array[i] = elem
							writeStatements.DeclareVariable(PduStructNames.ElementVarName, Code.VarRef(PduStructNames.ArrayVarName).Indexer(Code.VarRef(PduStructNames.LoopVarName)));
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

					var memberByteOrder = field.DeclaredByteOrder;

					if (readMethodName_LE != readMethodName_BE
						|| writeMethodName_LE != writeMethodName_BE
						)
						byteOrderVariant = true;

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
							readStatements.Assign(fieldReadRef, fieldReadExpr);
							// Write
							writeStatements.Do(writerArg.MethodOf(writeMethodName).Call(fieldWriteExpr));
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
							readStatements.Assign(fieldWriteRef, isBigEndian ? fieldReadExpr_BE : fieldReadExpr_LE);

							// Write
							writeStatements.Do(writerArg.MethodOf(isBigEndian ? writeMethodName_BE : writeMethodName_LE).Call(fieldWriteExpr));
						}
					}
					else if (isByteArray)
					{
						if (arraySizeExpr != null)
						{
							ExpressionSyntax fieldReadExpr = readerArg.MethodOf(PduStructNames.ReadBytesName).Call(arraySizeExpr);
							ExpressionSyntax fieldWriteExpr = fieldWriteRef;

							// Read
							readStatements.Assign(fieldWriteRef, fieldReadExpr);

							// Write
							writeStatements.Do(writerArg.MethodOf(PduStructNames.WriteBytesName).Call(fieldWriteExpr));
						}
						else
						{
							// If missing, an errer was already reported above
						}
					}
					else if (elementType.IsPduStruct())
					{
						var fieldParameters = PduTypeInfo.GetPduParameters(elementType, out _);

						// Check for an embedded PDU struct
						ExpressionSyntax byteOrderValue =
							memberByteOrder.HasValue ? Code.EnumField(memberByteOrder.Value)
							: byteOrderArg;

						var attrArgs = field.ArgumentsAttribute;
						ISymbol?[] args;
						if (attrArgs != null && attrArgs.ConstructorArguments.Length == 1)
						{
							var argNames = attrArgs.ConstructorArguments[0].Values;
							args = new ISymbol[argNames.Length];
							for (int i = 0; i < attrArgs.ConstructorArguments.Length; i++)
							{
								var argName = argNames[i];
								var argSym = pduType.Members.FirstOrDefault(r => r.Name == (string?)argName.Value);
								if (argSym is null)
								{
									context.ReportDiagnostic(Diagnostics.UndefinedMemberRef_Type_Member_AttrType_AttributeArg_Member.Create(
										attrArgs.ApplicationSyntaxReference.GetLocation(),
										pduType.TypeSymbol.Name, field.Name, nameof(PduArgumentsAttribute), "[0]", argName
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
								field.Declarator.GetLocation(),
								pduType.TypeSymbol.Name, field.Name, fieldType.Name
								));
							return false;
						}

						// Check argument types
						List<ExpressionSyntax> readArgs = new List<ExpressionSyntax>(1 + args.Length);
						List<ExpressionSyntax> writeArgs = new List<ExpressionSyntax>(2 + args.Length)
						{
							fieldWriteRef
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

								if (!SymbolEqualityComparer.Default.Equals(argSym.DataType(), param.FieldType))
								{
									argMismatch = true;
									context.ReportDiagnostic(PduDiagnostics.PduArgTypeMismatch_Type_Member_ArgIndex_ArgMember_ParamType.Create(
										attrArgs.ApplicationSyntaxReference.GetLocation(),
										pduType.TypeSymbol.Name,
										field.Name,
										i,
										argSym.DataType().Name,
										param.FieldType.Name
										));
								}

								ExpressionSyntax argExpr = Code.VarRef(argSym.Name);
								if (argSym is IMethodSymbol)
									argExpr = argExpr.Call();
								readArgs.Add(argExpr);
								writeArgs.Add(argExpr);

								readMethodGenericArgs.Add(param.FieldType);
							}

							if (argMismatch)
								return false;
						}

						ExpressionSyntax fieldReadExpr = readerArg.MethodOf(PduStructNames.ReadPduStructName, readMethodGenericArgs.ToArray()).Call(readArgs.ToArray());
						ExpressionSyntax fieldWriteExpr = fieldWriteRef;

						readStatements.Assign(fieldReadRef, fieldReadExpr);
						writeStatements.Do(writerArg.MethodOf(PduStructNames.WritePduStructName).Call(writeArgs.ToArray()));
					}
					else
					{
						context.ReportDiagnostic(Diagnostic.Create(
							PduDiagnostics.CantSerializeError_Type_Member_FieldType,
							Location.Create(declarator.SyntaxTree, declarator.Span),
							pduType.TypeSymbol.FullName(), field.Name, fieldType.FullName()
							));
					}


					// Check for array
					if (encodeAsArray)
					{
						// Create the array loop
						if (arraySizeExpr != null)
						{
							// Read block
							GenerateReadBlock(readStatements, arrayReadBlock, elementType, arraySizeExpr, origFieldRef);

							// Write block
							void GenerateWriteBlock(
								List<StatementSyntax> writeStatements,
								List<StatementSyntax> topWriteBlock
								)
							{
								List<StatementSyntax> arrayBlock = new List<StatementSyntax>();

								// var array = new TElement[count];
								arrayBlock.DeclareVariable(
									PduStructNames.ArrayVarName,
									origFieldRef
									);
								// prop = array;
								arrayBlock.Assign(origFieldRef, Code.VarRef(PduStructNames.ArrayVarName));

								arrayBlock.IfThen(
									Code.VarRef(PduStructNames.ArrayVarName).IsNotNull(),
									Code.For(
										PduStructNames.LoopVarName,
										Code.VarRef(PduStructNames.ArrayVarName).PropertyOf(nameof(Array.Length)),
										writeStatements.AsBlock())
									);

								topWriteBlock.Add(arrayBlock.AsBlock());
							}
							GenerateWriteBlock(writeStatements, arrayWriteBlock);
						}
					}
				}

				if (field.Condition is not null || field.Case is not null)
				{
					ExpressionSyntax? condExpr = null;
					if (field.Condition != null)
					{
						var condSymbol = field.Condition.TryResolveMemberName(member, context);
						if (condSymbol == null)
							return false;

						condExpr = Code.VarRef(condSymbol.Name);
					}

					if (field.Case != null)
					{
						var caseExpr = Code.VarRef("object").MethodOf("Equals").Call(Code.VarRef(pduType.SwitchMember?.Name ?? "missingSwitch"), field.Case.ArgumentSyntax.Expression);
						condExpr =
							(condExpr is null) ? caseExpr
							: condExpr.BooleanAnd(caseExpr);
					}

					// Read condition
					{
						condReadBlock.IfThen(condExpr, SyntaxFactory.Block(readStatements));
					}

					// Write condition
					{
						ExpressionSyntax condWriteRef = condExpr;
						//ExpressionSyntax condWriteRef =
						//	(isNullableRef) ? fieldRef.IsNotNull()
						//	: (isNullableValue) ? fieldRef.Member(nameof(Nullable<int>.HasValue))
						//	: fieldRef;

						condWriteBlock.IfThen(condWriteRef, SyntaxFactory.Block(writeStatements));
					}
				}
			}

			return true;
		}

		private static void GenerateReadBlock(
			List<StatementSyntax> readStatements,
			List<StatementSyntax> topReadBlock,
			ITypeSymbol elementType,
			ExpressionSyntax arraySizeExpr,
			ExpressionSyntax origFieldRef
			)
		{
			List<StatementSyntax> arrayBlock = new List<StatementSyntax>();
			arrayBlock.DeclareVariable(PduStructNames.LimitVarName, arraySizeExpr);

			// var array = new TElement[count];
			arrayBlock.DeclareVariable(
				PduStructNames.ArrayVarName,
				Code.TypeRef(elementType).NewArray(Code.VarRef(PduStructNames.LimitVarName))
				);
			// prop = array;
			arrayBlock.Assign(origFieldRef, Code.VarRef(PduStructNames.ArrayVarName));

			// array[i] = elem
			readStatements.Assign(
				Code.VarRef(PduStructNames.ArrayVarName).Indexer(Code.VarRef(PduStructNames.LoopVarName)),
				Code.VarRef(PduStructNames.ElementVarName)
				);

			arrayBlock.Add(Code.For(PduStructNames.LoopVarName, Code.VarRef(PduStructNames.LimitVarName), readStatements.AsBlock()));

			topReadBlock.Add(arrayBlock.AsBlock());
		}
	}
}
