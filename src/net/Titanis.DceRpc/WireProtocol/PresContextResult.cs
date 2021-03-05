using System.Runtime.InteropServices;

namespace Titanis.DceRpc.WireProtocol
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct PresContextResult
	{
		public unsafe static int StructSize => sizeof(PresContextResult);

		internal ContextDefResult result;
		internal ProviderReason reason;
		internal SyntaxId transfer_syntax;

		public PresContextResult(
			ContextDefResult result,
			ProviderReason reason
			)
		{
			this.result = result;
			this.reason = reason;
			this.transfer_syntax = new SyntaxId();
		}
		public PresContextResult(
			SyntaxId transferSyntax
			)
		{
			this.result = ContextDefResult.Acceptance;
			this.reason = 0;
			this.transfer_syntax = transferSyntax;
		}
	}
}
