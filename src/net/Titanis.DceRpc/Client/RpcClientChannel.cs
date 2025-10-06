using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Communication;
using Titanis.DceRpc.WireProtocol;
using Titanis.IO;
using Titanis.Security;
using Titanis.Winterop;

namespace Titanis.DceRpc.Client
{
	/// <summary>
	/// Implements the client side of an RPC channel.
	/// </summary>
	/// <seealso cref="RpcClient.BindTo(Stream)"/>
	public partial class RpcClientChannel : RpcChannel
	{
		internal RpcClientChannel(RpcClient client, RpcTransport transport, IRpcCallback? callback)
			: base(transport, client.DefaultCallTimeout, callback)
		{
			this._client = client;
		}

		private RpcClient _client;
		public RpcClient Client => this._client;

		private RpcAssocGroup? _assocGroup;
		internal RpcAssocGroup? AssocGroup => this._assocGroup;

		private int _lastCallId = 0;
		private int GetNextCallId() => Interlocked.Increment(ref this._lastCallId);

		private Dictionary<int, BindRequest> _pendingBinds = new Dictionary<int, BindRequest>();

		// Start at 0 to mimic Windows RPC client
		private int _lastAuthContextId = -1;
		private uint GetNextAuthContextId() => (uint)Interlocked.Increment(ref this._lastAuthContextId);

		[Flags]
		enum BindTimeFeatures : ulong
		{
			None = 0,

			SecurityContextMultiplexingSupported = 1,
			KeepConnectionOnOrphanSupported = 2,
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		struct BindTimeFeatureGuid
		{
			internal const ulong PrefixValue = 0x454098126cb71c2c;
			internal ulong prefix;
			internal BindTimeFeatures features;

			internal BindTimeFeatureGuid(BindTimeFeatures features)
			{
				this.prefix = PrefixValue;
				this.features = features;
			}
		}
		static unsafe Guid MakeBindTimeFeatureGuid(BindTimeFeatures features)
		{
			// 6cb71c2c-9812-4540-0300-000000000000
			// 0000   2c 1c b7 6c 12 98 40 45 03 00 00 00 00 00 00 00   ,..l..@E........
			BindTimeFeatureGuid guid = new BindTimeFeatureGuid(features);
			return *(Guid*)&guid;
		}

		#region ContextId
		private int _nextContextId = -1;
		private ushort GetNextContextId()
		{
			return (ushort)Interlocked.Increment(ref _nextContextId);
		}
		#endregion

		internal sealed override void OnTransportAborted(Exception? exception)
		{
			lock (this._pendingBinds)
			{
				foreach (var req in this._pendingBinds.Values)
				{
					req._taskSource.TrySetException(new RpcFaultException("The RPC call has been aborted due to a failure of the underlying transport."));
				}
			}
			lock (this._pendingRequests)
			{
				foreach (var req in this._pendingRequests.Values)
				{
					req._taskSource.TrySetException(new RpcFaultException("The RPC call has been aborted due to a failure of the underlying transport."));
				}
			}
		}

		private Dictionary<AuthClientContext, BindAuthContext> _authContextByOriginal = new Dictionary<AuthClientContext, BindAuthContext>();

		internal async Task<RpcBindContext> Bind(
			Guid interfaceUuid,
			RpcVersion version,
			AuthClientContext? authContext,
			RpcAuthLevel authLevel,
			RpcAssocGroup? assocGroup,
			CancellationToken cancellationToken
			)
		{

			SyntaxId[] TransferSyntaxes = new SyntaxId[]
			{
				RpcEncoding.MsrpcSyntaxId
			};
			SyntaxId[] TransferSyntaxes64 = new SyntaxId[]
			{
				RpcEncoding.MsrpcNdr64SyntaxId
			};

			BindAuthContext? bindAuthContext = null;
			bool existingAuthContext = false;
			if (authContext != null)
			{
				existingAuthContext = this._authContextByOriginal.TryGetValue(authContext, out bindAuthContext);
				if (!existingAuthContext)
				{
					authContext.Initialize();
					bindAuthContext = new BindAuthContext(
						authContext,
						this.GetNextAuthContextId(),
						authLevel
						);
					this.RegisterAuthContext(bindAuthContext.ContextId, bindAuthContext);

					lock (this._authContextByOriginal)
					{
						this._authContextByOriginal.Add(authContext, bindAuthContext);
					}
				}
			}

			this._assocGroup = assocGroup;
			var assocGroupId = (this._assocGroup?.GroupId ?? 0U);

			var contextId0 = this.GetNextContextId();

			ByteWriter writer = RpcPduWriter.Create();
			var bindPdu = new BindPdu(
				new PresContext()
				{
					p_cont_id = contextId0,
					abstract_syntax = new SyntaxId(interfaceUuid, version),
					transferSyntaxes = TransferSyntaxes
				},
				// HACK: Windows sends these as separate transfer syntaxes
				//new PresContext(new ContextElement
				//{
				//	p_cont_id = this.GetNextContextId(),
				//	abstract_syntax = new SyntaxId(interfaceUuid, version)
				//})
				//{
				//	transferSyntaxes = TransferSyntaxes64
				//},
				new PresContext()
				{
					p_cont_id = this.GetNextContextId(),
					abstract_syntax = new SyntaxId(interfaceUuid, version),
					transferSyntaxes = new SyntaxId[] {
						new SyntaxId(
							MakeBindTimeFeatureGuid(BindTimeFeatures.KeepConnectionOnOrphanSupported | BindTimeFeatures.SecurityContextMultiplexingSupported),
							new RpcVersion(1,0)
							)
					}
				})
			{
				max_xmit_frag = (ushort)this.MaxXmitFrag,
				max_recv_frag = (ushort)this.MaxRecvFrag,
				assoc_group_id = assocGroupId
			};

			bindPdu.WriteTo(writer, PduByteOrder.LittleEndian);
			int authLength;
			if (!existingAuthContext && bindAuthContext != null)
				authLength = WriteBindAuthToken(bindAuthContext, writer);
			else
				authLength = 0;

			int callId = this.GetNextCallId();
			BindRequest bindreq = new BindRequest(callId, bindPdu)
			{
				authContext = bindAuthContext
			};
			lock (this._pendingBinds)
				this._pendingBinds.Add(callId, bindreq);

			PfcFlags flags = (authContext != null) ? PfcFlags.SupportHeaderSigning : PfcFlags.None;
			await this.SendPduAsync(
				(contextId0 != 0) ? PduType.AlterContext : PduType.Bind,
				flags,
				callId,
				writer,
				authLength,
				cancellationToken).ConfigureAwait(false);
			var bindContext = await bindreq._taskSource.Task.ConfigureAwait(false);
			// TODO: Track bound contexts
			return bindContext;
		}

		private static int WriteBindAuthToken(BindAuthContext bindAuthContext, ByteWriter writer)
		{
			int authLength;
			int pad = writer.Align(AuthVerifierHeader.Alignment);
			AuthVerifierHeader hdr = new AuthVerifierHeader
			{
				auth_type = (RpcAuthType)bindAuthContext.AuthContext.RpcAuthType,
				auth_level = bindAuthContext.AuthLevel,
				auth_pad_length = (byte)pad,
				auth_context_id = bindAuthContext.ContextId
			};
			writer.WritePduStruct(hdr);
			var authToken = bindAuthContext.AuthContext.Token;
			writer.WriteBytes(authToken);
			authLength = authToken.Length;
			return authLength;
		}

		private readonly Dictionary<int, PendingRequest> _pendingRequests = new Dictionary<int, PendingRequest>();
		class PendingRequest
		{
			internal readonly int callId;
			internal readonly RpcEncoding encoding;
			internal readonly TaskCompletionSource<RpcDecoder> _taskSource = new TaskCompletionSource<RpcDecoder>();
			internal readonly RpcCallContext callContext;
			internal readonly RpcBindContext bindContext;

			internal CancellationTokenRegistration cancelCallback;
			internal CancellationTokenSource? callCancel;

			internal PendingRequest(
				int callId,
				RpcEncoding encoding,
				RpcCallContext callContext,
				RpcBindContext bindContext)
			{
				this.callId = callId;
				this.encoding = encoding;
				this.callContext = callContext;
				this.bindContext = bindContext;
			}
		}

		internal async Task<RpcDecoder> SendRequestAsync(
			RpcRequestBuilder stubData,
			RpcBindContext context,
			bool includePContext,
			CancellationToken cancellationToken
			)
		{
			if (includePContext)
			{
				var writer = stubData.StubData.GetWriter();
				writer.WriteVerifyTrailerSig();
				writer.WriteVerifyTrailerPContext(new VerifyTrailerPContext
				{
					command = VerifyTrailerCommand.PContext | VerifyTrailerCommand.End,
					length = VerifyTrailerPContext.DataSize,
					interfaceId = context.interfaceId,
					transferSyntaxId = context.transferSyntaxId
				});
			}

			var buffer = stubData.Complete(context);

			var callId = this.GetNextCallId();
			PendingRequest pendingRequest = new PendingRequest(
				callId,
				context.encoding,
				stubData.callContext,
				context);
			lock (this._pendingBinds)
				this._pendingRequests.Add(callId, pendingRequest);

			int authLength = (context.authContext != null)
				? context.authContext.MessageAuthTokenSize
				: 0;
			PfcFlags flags = (stubData.HasObjectId)
				? PfcFlags.ObjectUuid
				: PfcFlags.None
				;

			pendingRequest.callCancel = new CancellationTokenSource(this.CallTimeout);
			pendingRequest.callCancel.Token.Register(() => this.CancelRequest(pendingRequest, cancellationToken).Wait());
			pendingRequest.cancelCallback = cancellationToken.Register(() => pendingRequest.callCancel.Cancel());

			await this.SendPduAsync(
				PduType.Request,
				flags,
				callId,
				buffer,
				authLength,
				pendingRequest.callCancel.Token,
				context.authContext).ConfigureAwait(false);

			return await pendingRequest._taskSource.Task.ConfigureAwait(false);
		}

		private async Task CancelRequest(
			PendingRequest pendingRequest,
			CancellationToken cancellationToken
			)
		{
			bool isTimeout = pendingRequest.callCancel.IsCancellationRequested;

			if (!pendingRequest._taskSource.Task.IsCompleted)
			{
				var writer = RpcPduWriter.Create();
				int authLength = (pendingRequest.bindContext.authContext != null)
					? pendingRequest.bindContext.authContext.MessageAuthTokenSize
					: 0;
				await this.SendPduAsync(
					PduType.CoCancel, PfcFlags.None,
					pendingRequest.callId,
					writer,
					authLength,
					CancellationToken.None,
					pendingRequest.bindContext.authContext
					).ConfigureAwait(false);

				if (isTimeout)
					pendingRequest._taskSource.TrySetException(new TimeoutException("The RPC call has timed out."));
				else
					pendingRequest._taskSource.TrySetCanceled(cancellationToken);
			}
		}

		public sealed override Task DispatchPduAsync(
			PduHeader header,
			Memory<byte> message,
			CancellationToken cancellationToken)
		{
			try
			{
				switch (header.ptype)
				{
					case PduType.Request:
					case PduType.Ping:
					case PduType.Working:
					case PduType.NoCall:
					case PduType.Reject:
					case PduType.Ack:
					case PduType.CLCancel:
					case PduType.Fack:
					case PduType.CancelAck:
					case PduType.Bind:
					case PduType.Orphaned:
					case PduType.CoCancel:
					case PduType.Shutdown:
					case PduType.AlterContext:
						// TODO: Alert on bad PDU
						break;
					case PduType.Response:
						ProcessResponsePdu(header, message);
						break;
					case PduType.Fault:
						ProcessFaultPdu(header, message);
						break;
					case PduType.BindAck:
						HandleBindAck(header, message, cancellationToken);
						break;
					case PduType.BindNak:
						HandleBindNak(header, message);
						break;
					case PduType.AlterContextResp:
						HandleBindAck(header, message, cancellationToken);
						//HandleAlterContextResp(header, message, cancelToken);
						break;
					default:
						throw new NotImplementedException();
				}

				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				return Task.FromException(ex);
			}
		}

		private void HandleBindNak(PduHeader header, ReadOnlyMemory<byte> message)
		{
			BindRequest bindreq = this._pendingBinds.TryGetValue(header.callId);
			if (bindreq != null)
			{
				ByteMemoryReader reader = new ByteMemoryReader(message);
				BindNakPdu bindnak = reader.ReadBindNak();
				RpcBindException exception = new(bindnak.header.provider_reject_reason, header.callId);

				this.FailBind(bindreq, exception);
			}
			else
			{
				// TODO: Log unsolicited bind_ack
			}
		}

		private void FailBind(
			BindRequest bindreq,
			Exception exception)
		{
			lock (this._pendingBinds)
				this._pendingBinds.Remove(bindreq.callId);

			bindreq._taskSource.SetException(exception);
		}

		private Task ProcessResponsePdu(PduHeader header, Memory<byte> message)
		{
			try
			{
				var request = this._pendingRequests.TryGetValue(header.callId);
				if (request != null)
				{
					request.cancelCallback.Unregister();

					lock (this._pendingRequests)
						this._pendingRequests.Remove(header.callId);

					ByteMemoryReader reader = new ByteMemoryReader(message);
					ResponsePdu resp = reader.ReadResponsePdu();

					var respReader = request.encoding.CreateDecoder(new ByteMemoryReader(resp.StubData), request.callContext);
					request._taskSource.SetResult(respReader);
				}
				else
				{
					// TODO: Log unsolicited response
				}

				return Task.CompletedTask;
			}
			catch (Exception ex)
			{
				return Task.FromException(ex);
			}
		}

		private void ProcessFaultPdu(PduHeader header, ReadOnlyMemory<byte> message)
		{
			ByteMemoryReader reader = new ByteMemoryReader(message);
			var fault = reader.ReadFaultPdu();

			Exception ex;
			if (RpcFaultException.IsRpcFaultCode((int)fault.hdr.status))
				ex = new RpcFaultException(fault.hdr.status);
			else
				ex = ((Win32ErrorCode)fault.hdr.status).GetException();

			var request = this._pendingRequests.TryGetValue(header.callId);
			if (request == null)
			{
				BindRequest bindreq = this._pendingBinds.TryGetValue(header.callId);
				if (bindreq != null)
				{
					this.FailBind(bindreq, ex);
				}
				else
					// TODO: Log unsolicited fault
					return;
			}
			else
			{
				lock (this._pendingRequests)
					this._pendingRequests.Remove(header.callId);

				//var respReader = request.encoding.CreateDecoder(resp.StubData, request.callContext);
				request._taskSource.TrySetException(ex);
			}
		}

		private void HandleBindAck(
			PduHeader header,
			ReadOnlyMemory<byte> message,
			CancellationToken cancellationToken)
		{
			BindRequest bindreq = this._pendingBinds.TryGetValue(header.callId);
			if (bindreq == null)
				// TODO: Log unsolicited bind_ack
				return;

			lock (this._pendingBinds)
				this._pendingBinds.Remove(header.callId);

			BindPdu bind = bindreq.bindPdu;

			ByteMemoryReader reader = new ByteMemoryReader(message);
			BindAckPdu bindack = reader.ReadBindAck();

#if DEBUG
			string secaddr = Encoding.UTF8.GetString(bindack.secondaryAddress.spec);
#endif
			AuthVerifier? auth;
			if (header.authLength > 0)
			{
				int pad = reader.Align(4);
				auth = reader.ReadAuthVerifier(header.authLength);
			}
			else
				auth = null;

			// TODO: Verify auth is valid

			// TODO: Sanity check on transmission parameters

			this.UpdateMaxParams(
				bindack.header.max_xmit_frag,
				bindack.header.max_recv_frag
				);

			RpcAssocGroup assocGroup = new RpcAssocGroup(bindack.header.assoc_group_id);
			//RpcBindContext bindContext = null;

			var contextResults = bindack.contextResults;
			for (int i = 0; i < contextResults.Length; i++)
			{
				var contextElem = contextResults[i];
				if (contextElem.result == ContextDefResult.Acceptance)
				{
					RpcEncoding encoding = RpcEncoding.GetEncoding(contextElem.transfer_syntax);

					var bindContext = new RpcBindContext(
						null,
						assocGroup,
						encoding,
						ref bind.contextList.contexts[i],
						contextElem.transfer_syntax,
						bindreq.authContext);

					if (auth != null)
					{
						AuthClientContext? authContext = bindreq.authContext?.AuthContext;
						Debug.Assert(authContext != null);
						authContext.Initialize(auth.token);
						bindreq.bindContext = bindContext;

						bool hasToken = authContext.Token.Length > 0;
						if (hasToken)
						{
							bool preferAuth3 = (authContext.Legs == 3) && this.Client.PreferAuth3;
							if (authContext.IsComplete && preferAuth3)
								_ = this.SendAuth3(bindreq, cancellationToken);
							else
							{
								_ = this.SendAlterContext(bindreq, cancellationToken);
								return;
							}
						}
					}

					bindreq._taskSource.SetResult(bindContext);
					return;
				}
			}

			bindreq._taskSource.SetException(new NotSupportedException(Messages.RpcClient_InterfaceNotSupported));
		}

		private void HandleAlterContextResp(
			PduHeader header,
			ReadOnlyMemory<byte> message,
			CancellationToken cancellationToken)
		{
			BindRequest bindreq = this._pendingBinds.TryGetValue(header.callId);
			if (bindreq == null)
				// TODO: Log unsolicited bind_ack
				return;

			lock (this._pendingBinds)
				this._pendingBinds.Remove(header.callId);

			ByteMemoryReader reader = new ByteMemoryReader(message);
			BindAckPdu bindack = reader.ReadBindAck();

			AuthVerifier? auth;
			if (header.authLength > 0)
			{
#if DEBUG
				int pad =
#endif
					reader.Align(4);
				auth = reader.ReadAuthVerifier(header.authLength);
			}
			else
				auth = null;

			// TODO: Verify auth is valid

			// TODO: Sanity check on transmission parameters

			this.UpdateMaxParams(
				bindack.header.max_xmit_frag,
				bindack.header.max_recv_frag
				);

			if (auth != null)
			{
				AuthClientContext? authContext = bindreq.authContext?.AuthContext;
				Debug.Assert(authContext != null);
				authContext.Initialize(auth.token);
				//if (!authContext.IsComplete)
				{
					_ = this.SendAlterContext(bindreq, cancellationToken);
					return;
				}
			}

			bindreq._taskSource.SetResult(bindreq.bindContext);
			return;
		}

		private async Task SendAlterContext(
			BindRequest bindreq,
			CancellationToken cancellationToken
			)
		{
			var bindContext = bindreq.bindContext;

			ByteWriter writer = RpcPduWriter.Create();
			var bindPdu = new BindPdu(
				new PresContext[]
				{
					new PresContext()
					{
						p_cont_id = (ushort)bindContext.contextId,
						abstract_syntax = bindreq.bindContext.interfaceId,
						transferSyntaxes = new SyntaxId[] {
							new SyntaxId(bindContext.encoding.InterfaceUuid, bindContext.encoding.InterfaceVersion)
						}
					}
				}
			)
			{
				max_xmit_frag = (ushort)this.MaxXmitFrag,
				max_recv_frag = (ushort)this.MaxRecvFrag,
				assoc_group_id = bindContext.assocGroup.GroupId
			};

			bindPdu.WriteTo(writer, PduByteOrder.LittleEndian);
			lock (this._pendingBinds)
				this._pendingBinds.Add(bindreq.callId, bindreq);

			int authLength;
			if (bindContext.authContext != null)
				authLength = WriteBindAuthToken(bindContext.authContext, writer);
			else
				authLength = 0;

			await this.SendPduAsync(
				PduType.AlterContext,
				PfcFlags.SupportHeaderSigning,
				bindreq.callId,
				writer,
				authLength,
				cancellationToken).ConfigureAwait(false);
		}

		private async Task SendAuth3(
			BindRequest bindreq,
			CancellationToken cancellationToken
			)
		{
			var bindContext = bindreq.bindContext;

			ByteWriter writer = RpcPduWriter.Create();
			// TODO: Send random pad
			writer.WriteAuth3(0);
			// UNDONE: There is no response, and this entry will never be cleared
			//lock (this._pendingBindRequests)
			//	this._pendingBindRequests.Add((uint)bindreq.callId, bindreq);

			int authLength;
			if (bindContext.authContext != null)
				authLength = WriteBindAuthToken(bindContext.authContext, writer);
			else
				authLength = 0;

			await this.SendPduAsync(
				PduType.Auth3,
				PfcFlags.SupportHeaderSigning,
				bindreq.callId,
				writer,
				authLength,
				cancellationToken).ConfigureAwait(false);
		}
	}

	class BindRequest
	{
		internal int callId;
		internal TaskCompletionSource<RpcBindContext> _taskSource = new TaskCompletionSource<RpcBindContext>();
		internal BindPdu bindPdu;
		internal BindAuthContext? authContext;
		internal RpcBindContext? bindContext;

		public BindRequest(int callId, BindPdu bindPdu)
		{
			this.callId = callId;
			this.bindPdu = bindPdu;
		}
	}

	/// <summary>
	/// Describes the authentication context of a bound RPC proxy.
	/// </summary>
	public class BindAuthContext
	{
		internal BindAuthContext(
			AuthClientContext authContext,
			uint contextId,
			RpcAuthLevel authLevel
			)
		{
			this.AuthContext = authContext;
			this.ContextId = contextId;
			this.AuthLevel = authLevel;
		}

		/// <summary>
		/// Gets the ID of the bound authentication context.
		/// </summary>
		public uint ContextId { get; }
		public AuthClientContext AuthContext { get; }
		public RpcAuthLevel AuthLevel { get; }

		internal int MessageAuthTokenSize
			=>
				(this.AuthLevel == RpcAuthLevel.PacketIntegrity) ? this.AuthContext.SignTokenSize
				: (this.AuthLevel == RpcAuthLevel.PacketPrivacy) ? (this.AuthContext.SealHeaderSize + this.AuthContext.SealTrailerSize)
				: 0;
	}
}