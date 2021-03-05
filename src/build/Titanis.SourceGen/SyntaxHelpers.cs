using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Threading;
using Titanis.CodeGen;

namespace Titanis.SourceGen
{
	static class SyntaxHelpers
	{

		public static PduByteOrder? GetDeclaredByteOrder(ISymbol sym)
		{
			var orderAtr = sym.TryGetAttribute(typeof(PduByteOrderAttribute));
			var value = orderAtr?.ConstructorArg(0) as int?;
			return (value.HasValue ? (PduByteOrder)value.Value : null);
		}

		public static PduByteOrder? GetByteOrder(ISymbol symbol)
		{
			var order = GetDeclaredByteOrder(symbol);
			if (!order.HasValue)
			{
				if (symbol.ContainingType is not null)
					return GetByteOrder(symbol.ContainingType);
				else
					return GetByteOrder(symbol.ContainingAssembly);
			}
			return order;
		}
		public static PduByteOrder? GetByteOrder(ITypeSymbol type)
		{
			var order = GetDeclaredByteOrder(type);
			return order ?? ((type.ContainingType != null) ? GetByteOrder(type.ContainingType) : GetDeclaredByteOrder(type.ContainingAssembly));
		}
	}
}
