using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using Titanis.CodeGen;

namespace Titanis.SourceGen
{
	static class SymbolHelpers
	{
		internal static bool HasPduStructAttribute(this ITypeSymbol? type)
		{
			return (type is not null) && (type.SpecialType is SpecialType.None) && (type.IsDefined(typeof(PduStructAttribute)));
		}

		internal static InheritModifier DetermineInherit(this ITypeSymbol typeSym)
		{
			InheritModifier inherit;
			var baseType = typeSym.BaseType;
			if (IsPduStruct(baseType))
				inherit = typeSym.IsSealed ? InheritModifier.SealedOverride : InheritModifier.Override;
			else
				inherit = InheritModifier.Virtual;
			return inherit;
		}

		internal static bool IsPduStruct(this ITypeSymbol? type)
		{
			if (type.HasPduStructAttribute())
				return true;
			else
			{
				bool f = (type.AllInterfaces.Any(r => r.FullName() == PduStructNames.IPduStructName));
				if (f)
					return true;

				if (type is ITypeParameterSymbol parm)
					f = parm.ConstraintTypes.Any(r => r.FullName() == PduStructNames.IPduStructName);

				return f;
			}
		}
	}
}
