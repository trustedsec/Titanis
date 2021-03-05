using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.SourceGen
{
	static class PduDiagnostics
	{
		const string PduStructCategory = "Titanis.PduStruct";
		internal static DiagnosticDescriptor NoPartialError_1 = new DiagnosticDescriptor(
			"TI1001",
			"PduStruct type must be partial",
			"The type '{0}' is marked with [PduStruct] but it is not declared partial.",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"<description>"
			);
		//internal static DiagnosticDescriptor MissingByteOrderError_2 = new DiagnosticDescriptor(
		//	"TI1002",
		//	"<title>",
		//	"Field '{0}' of type '{1}' is missing a byte order attribute.",
		//	PduStructCategory,
		//	DiagnosticSeverity.Error,
		//	true,
		//	"<description>"
		//	);
		internal static DiagnosticDescriptor MissingSetterError_Type_Member = new DiagnosticDescriptor(
			"TI1003",
			"Property must define a setter",
			"'{0}.{1}': Property does not define a setter.  Either define a setter or mark it with [PduIgnoreAttribute].",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"A PduField property in a PDU structure must have a setter.  If the property is not ot be read, mark it with [PduIgnore]."
			);
		internal static DiagnosticDescriptor CantSerializeError_Type_Member_FieldType = new DiagnosticDescriptor(
			"TI1004",
			"Field type cannot be encoded",
			"'{0}.{1}': Field type '{2}' cannot be encoded",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"The field type must be an encodable built-in type or a type that implements IPduStruct.  To resolve this error, implement IPduStruct on the type, or provide custom encode/decode methods with PduFieldAttribute."
			);
		internal static DiagnosticDescriptor MissingCountAttribute_Type_Member = new DiagnosticDescriptor(
			"TI1007",
			"Missing array size",
			"'{0}.{1}': Member is an array but is missing [PduArraySize]",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"<description>"
			);
		internal static DiagnosticDescriptor BadCountAttribute_Type_Member = new DiagnosticDescriptor(
			"TI10071",
			"Bad array size",
			"'{0}.{1}': Count must be specified either as a positive integer or the name of a member to retrieve the length at runtime",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"<description>"
			);
		internal static DiagnosticDescriptor PduStringOnNonString_Type_Field = new DiagnosticDescriptor(
			"TI1008",
			"<title>",
			"'{0}.{1}': Non-string member is marked with [PduString]",
			PduStructCategory,
			DiagnosticSeverity.Warning,
			true,
			"<description>"
			);
		internal static DiagnosticDescriptor PduStringMissingOnString_Type_Member = new DiagnosticDescriptor(
			"TI1009",
			"<title>",
			"'{0}.{1}': String field is not marked with [PduString]",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"<description>"
			);
		internal static DiagnosticDescriptor PduStringBadEncoding_0 = new DiagnosticDescriptor(
			"TI1010",
			"<title>",
			"[PduString] has an invalid encoding.",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"<description>"
			);
		internal static DiagnosticDescriptor PduPositionNotLong_Type_Field = new DiagnosticDescriptor(
			"TI1011",
			"<title>",
			"'{0}.{1}': Member marked with [PduPosition] but does not have the type Int64",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"<description>"
			);
		internal static DiagnosticDescriptor PduConditionalMissing_2 = new DiagnosticDescriptor(
			"TI1012",
			"<title>",
			"'{0}.{1}': Nullable member is not marked with [PduConditional]",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"<description>"
			);
		internal static DiagnosticDescriptor PduArgCountMismatch_Type_Member_NestedType = new DiagnosticDescriptor(
			"TI1012",
			"<title>",
			"'{0}.{1}': Wrong number of arguments provided for type {2}",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"<description>"
			);
		internal static DiagnosticDescriptor PduArgTypeMismatch_Type_Member_ArgIndex_ArgMember_ParamType = new DiagnosticDescriptor(
			"TI1012",
			"<title>",
			"'{0}.{1}': The type argument #{2} {3} does not match the parameter type {4}",
			PduStructCategory,
			DiagnosticSeverity.Error,
			true,
			"<description>"
			);
	}
}
