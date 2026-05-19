#if DEBUG
//#define DEBUG_LAUNCH
#endif

using Microsoft.CodeAnalysis;

namespace Titanis.SourceGen
{
	class PduParamInfo
	{
		public PduParamInfo() { }
		public PduParamInfo(
			ISymbol member,
			bool isLocal,
			ITypeSymbol fieldType
			)
		{
			this.Member = member;
			this.IsLocal = isLocal;
			this.FieldType = fieldType;
		}

		public ISymbol Member { get; }
		public bool IsLocal { get; }
		public ITypeSymbol? FieldType { get; }
	}
}
