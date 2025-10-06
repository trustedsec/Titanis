using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Communication;
using Titanis.DceRpc.WireProtocol;
using Titanis.IO;

namespace Titanis.DceRpc.Server
{
	public sealed class RpcServerChannel : Communication.RpcChannel
	{
		internal RpcServerChannel(RpcServer server, RpcTransport transport, IRpcCallback? callback)
			: base(transport, Timeout.InfiniteTimeSpan, callback)
		{
			this.Server = server;
		}

		public RpcServer Server { get; private set; }

		internal sealed override void OnTransportAborted(Exception? exception)
		{
			// TODO: Notify outstanding calls
		}

		private Dictionary<ushort, RpcBindContext> _bindContexts = new Dictionary<ushort, RpcBindContext>();

		private RpcBindContext TryGetBindContext(ushort contextId)
		{
			return this._bindContexts.TryGetValue(contextId);
		}

		private protected sealed override async Task HandleBind(
			PduHeader header,
			Memory<byte> message,
			BindPdu bind,
			AuthVerifier? authVerifier,
			CancellationToken cancellationToken)
		{
			if (
				(bind.max_recv_frag < MustRecvFragSize_CO)
				)
			{
				// TODO: Determine actual error value
				await this.SendBindNak(header.callId, BindRejectReason.Unspecified, cancellationToken).ConfigureAwait(false);
			}

			this.UpdateMaxParams(
				bind.max_xmit_frag,
				bind.max_recv_frag
				);

			RpcAssocGroup assocGroup = this.Server.GetOrCreateAssocGroup(bind.assoc_group_id);
			RpcBindContext bindContext = null;

			var contextElems = bind.contextList.contexts;
			var contextResults = new PresContextResult[contextElems.Length];
			for (int i = 0; i < contextElems.Length; i++)
			{
				var contextElem = contextElems[i];
				if (bindContext != null)
				{
					contextResults[i] = new PresContextResult
					{
						result = ContextDefResult.ProviderRejection,
						reason = ProviderReason.LocalLimitExceeded
					};
				}
				else
				{
					var binding = this.Server.TryGetService(contextElem.abstract_syntax);
					if (binding == null)
					{
						contextResults[i] = new PresContextResult(ContextDefResult.ProviderRejection, ProviderReason.AbstractSyntaxNotSupported);
					}
					else
					{
						RpcEncoding encoding = null;
						SyntaxId selectedXferSyntax = new SyntaxId();
						foreach (var xferSyntax in contextElem.transferSyntaxes)
						{
							encoding = this.Server.TryGetEncoding(xferSyntax);
							if (encoding != null)
							{
								selectedXferSyntax = xferSyntax;
								break;
							}
						}

						if (encoding == null)
						{
							contextResults[i] = new PresContextResult(ContextDefResult.ProviderRejection, ProviderReason.ProposedTransferSyntaxesNotSupported);
						}
						else
						{
							bindContext = new RpcBindContext(
								binding,
								assocGroup,
								encoding,
								ref contextElem,
								new SyntaxId(),
								null
								);
							lock (this._bindContexts)
								this._bindContexts.Add(contextElem.p_cont_id, bindContext);

							contextResults[i] = new PresContextResult(selectedXferSyntax);
						}
					}
				}
			}

			ByteWriter writer = RpcPduWriter.Create();
			BindAckPdu bindack = new BindAckPdu(new BindAckPduHeader()
			{
				max_xmit_frag = (ushort)this.MaxXmitFrag,
				max_recv_frag = (ushort)this.MaxRecvFrag,
				assoc_group_id = assocGroup.GroupId
			}, new PortAny())
			{
				contextResults = contextResults
			};
			writer.WriteBindAck(bindack);
			// TODO: Add support for auth tokens
			await this.SendPduAsync(
				PduType.BindAck,
				PfcFlags.SupportHeaderSigning,
				header.callId,
				writer,
				0,
				cancellationToken).ConfigureAwait(false);
		}

		private ConcurrentDictionary<int, CancellationTokenSource> _ongoingRequests = new ConcurrentDictionary<int, CancellationTokenSource>();

		private protected async Task HandleRequestAsync(
			PduHeader header,
			ReadOnlyMemory<byte> message,
			RequestPdu request,
			ByteMemoryReader reader,
			CancellationToken cancellationToken
			)
		{

			// TODO: Validate that call_id is unique, other validation to be done here

			// TODO: This really should be resolved by assoc_group as well
			RpcBindContext bindContext = this.TryGetBindContext(request.header.p_cont_id);
			if (bindContext != null)
			{
				try
				{
					CancellationTokenSource cancelSource = new CancellationTokenSource();
					this._ongoingRequests.TryAdd(header.callId, cancelSource);
					await bindContext.binding.DispatchAsync(
						request,
						bindContext,
						cancelSource.Token
						).ConfigureAwait(false);
				}
				finally
				{
					this._ongoingRequests.Remove(header.callId, out _);
				}
			}
			else
			{
				// TODO: What is the appropriate error?
			}
		}
	}
}
