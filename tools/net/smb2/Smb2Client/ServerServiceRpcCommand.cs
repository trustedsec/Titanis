using ms_srvs;
using System.ComponentModel;
using Titanis.Cli;
using Titanis.DceRpc.Client;
using Titanis.Msrpc.Mswkst;

namespace Titanis.Smb2.Cli
{
	internal abstract class ServerServiceRpcCommand : RpcCommand<ServerServiceClient>
	{
		protected sealed override Type InterfaceType => typeof(srvsvc);
		protected sealed override bool SupportsDynamicTcp => false;

		protected sealed override string? WellKnownPipeName => "srvsvc";

		[Parameter]
		[Description("Max size for response buffer")]
		public int BufferSize { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (this.BufferSize == 0)
				this.BufferSize = (int)ServerServiceClient.DefaultReturnBufferSize;
		}
	}
}
