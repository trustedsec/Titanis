using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.CodeGen;
using Titanis.PduStruct;

namespace Titanis.SourceGen
{
	public enum SpecialPduFieldKind
	{
		None = 0,
		Position,
	}

	internal class PduFieldInfo
	{
		internal PduFieldInfo(
			ISymbol member,
			SyntaxToken declarator,
			ITypeSymbol fieldType,
			AttributeData fieldAttr
			)
		{
			this.Member = member;
			this.Declarator = declarator;
			this.FieldType = fieldType;
			this.Attribute = fieldAttr;

			var attrConditional = member.GetAttribute(typeof(PduConditionalAttribute));
			this.Condition = attrConditional.GetArgument<string>(0);

			this.Alignment = member.GetAttribute(typeof(PduAlignmentAttribute))?.GetArgument<int>(0);
			this.DeclaredByteOrder = SyntaxHelpers.GetDeclaredByteOrder(member);

			this.CustomReadMethod = fieldAttr.GetArgument<string>(nameof(PduFieldAttribute.ReadMethod));
			this.CustomWriteMethod = fieldAttr.GetArgument<string>(nameof(PduFieldAttribute.WriteMethod));

			if (member.IsDefined(typeof(PduPositionAttribute)))
			{
				this.SpecialKind = SpecialPduFieldKind.Position;
			}

			// Offset
			this.OffsetAttribute = member.GetAttribute(typeof(PduOffsetAttribute));

			// List
			this.ListAttribute = member.GetAttribute(typeof(PduListAttribute));

			// Union stuff
			var caseAttr = member.GetAttribute(typeof(PduCaseAttribute));
			this.Case = caseAttr?.GetArgument<object>(0);

			this.StringLengthAttribute = member.GetAttribute(typeof(PduStringAttribute));
			this.StringLength = this.StringLengthAttribute?.GetArgument<object>(1);
			this.StringCharSet = this.StringLengthAttribute?.GetArgument<CharSet>(0);

			this.ArraySizeAttribute = member.GetAttribute(typeof(PduArraySizeAttribute));
			this.ArrayElementCount = this.ArraySizeAttribute?.GetArgument<object>(0);

			this.ArgumentsAttribute = member.GetAttribute(typeof(PduArgumentsAttribute));
		}

		public sealed override string ToString() => this.Name;

		public ISymbol Member { get; }
		public string Name => this.Member.Name;
		public SyntaxToken Declarator { get; }
		public ITypeSymbol FieldType { get; }
		public AttributeData? Attribute { get; }
		public SpecialPduFieldKind SpecialKind { get; }
		public bool IsPosition => this.SpecialKind == SpecialPduFieldKind.Position;

		public AttrArg<string>? Condition { get; }
		public AttrArg<int>? Alignment { get; }
		public PduByteOrder? DeclaredByteOrder { get; }
		public AttributeData? OffsetAttribute { get; set; }
		public AttributeData? ListAttribute { get; set; }
		public AttrArg<string>? CustomReadMethod { get; }
		public AttrArg<string>? CustomWriteMethod { get; }
		public AttrArg<object>? Case { get; }
		public AttributeData? StringLengthAttribute { get; }
		public AttrArg<object>? StringLength { get; }
		public AttrArg<CharSet>? StringCharSet { get; }
		public AttributeData? ArraySizeAttribute { get; }
		public AttrArg<object>? ArrayElementCount { get; }
		public AttributeData? ArgumentsAttribute { get; }

		internal static int GetSizeOf(ITypeSymbol type)
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
		internal static int GetSizeOf(IFieldSymbol field)
		{
			int fieldSize = GetSizeOf(field.Type);
			return fieldSize;
		}
	}
}
