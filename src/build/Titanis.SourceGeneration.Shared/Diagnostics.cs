using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.CodeGen
{
	public static class Diagnostics
	{
		const string GenericCategory = "Titanis.CodeGen";

		public static DiagnosticDescriptor MemberRefNotNameof_Type_Member_AttrType_AttributeArg_Member = new DiagnosticDescriptor(
			"TI1006",
			"Attribute references member without nameof(...)",
			"'{0}.{1}': {2}.{3} references member '{4}' wihout using nameof(...)",
			GenericCategory,
			DiagnosticSeverity.Warning,
			true,
			"<description>"
			);
		public static DiagnosticDescriptor UndefinedMemberRef_Type_Member_AttrType_AttributeArg_Member = new DiagnosticDescriptor(
			"TI1005",
			"Reference to undefined member",
			"'{0}.{1}': {2}.{3} references member '{4}' which does not exist",
			GenericCategory,
			DiagnosticSeverity.Error,
			true,
			"<description>"
			);
	}
}
