using System;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Server;
using Titanis.DceRpc.WireProtocol;

namespace Titanis.DceRpc.Communication
{
	internal class RpcBindContext
	{
		internal readonly RpcServiceStub? binding;
		internal readonly RpcAssocGroup assocGroup;
		internal readonly RpcEncoding encoding;
		internal readonly int contextId;
		internal readonly BindAuthContext? authContext;

		internal readonly SyntaxId interfaceId;
		internal readonly SyntaxId transferSyntaxId;

		internal RpcBindContext(
			RpcServiceStub? binding,
			RpcAssocGroup assocGroup,
			RpcEncoding encoding,
			ref PresContext presContext,
			SyntaxId transferSyntaxId,
			BindAuthContext? authContext
			)
		{
			this.binding = binding;
			this.assocGroup = assocGroup;
			this.encoding = encoding;
			this.authContext = authContext;
			this.contextId = presContext.p_cont_id;
			this.interfaceId = presContext.abstract_syntax;
			this.transferSyntaxId = transferSyntaxId;
		}
	}
}