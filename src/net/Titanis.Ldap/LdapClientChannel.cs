using Lightweight_Directory_Access_Protocol_V3;
using System.Collections.Concurrent;
using System.Net;
using System.Text;
using Titanis.Security;

namespace Titanis.Ldap
{
	internal class LdapClientChannel : LdapChannel
	{
		internal LdapClientChannel(Stream stream)
			: base(stream)
		{
		}

		private const string GssSpnegoMechName = "GSS-SPNEGO";
		private const int NtlmRpcAuthType = 0x0A;
		private AuthClientContext? _authContext;
		protected override AuthContext? AuthContext => this._authContext;

		internal async Task<LdapResponse> Bind(AuthClientContext authContext, CancellationToken cancellationToken)
		{
			var resp = await this.SendRequest(new LDAPMessage_ProtocolOp()
			{
				BindRequest = new BindRequest_Tagged0(3, Array.Empty<byte>(), new AuthenticationChoice()
				{
					Sasl = new SaslCredentials(Encoding.UTF8.GetBytes(GssSpnegoMechName), authContext.Initialize().ToArray())
				})
			}, cancellationToken).ConfigureAwait(false);
			var saslResult = resp.message.protocolOp.BindResponse.resultCode;

			while (saslResult == LDAPResult_ResultCode.SaslBindInProgress || (saslResult == LDAPResult_ResultCode.Success && !authContext.IsComplete))
			{
				var token = authContext.Initialize(resp.message.protocolOp.BindResponse.serverSaslCreds).ToArray();
				if (token.Length > 0)
				{
					resp = await this.SendRequest(new LDAPMessage_ProtocolOp()
					{
						BindRequest = new BindRequest_Tagged0(3, Array.Empty<byte>(), new AuthenticationChoice()
						{
							Sasl = new SaslCredentials(Encoding.UTF8.GetBytes(GssSpnegoMechName), token)
						})
					}, cancellationToken).ConfigureAwait(false);
				}

				saslResult = resp.message.protocolOp.BindResponse.resultCode;
			}

			if (saslResult != LDAPResult_ResultCode.Success)
				throw new LdapException((LdapResultCode)saslResult, Encoding.UTF8.GetString(resp.message.protocolOp.BindResponse.diagnosticMessage));
			// TODO: Why?  I didn't see anything in the spec, but the first message received in NTLM has seq# 1
			authContext = authContext.GetMechContext();
			if (authContext.RpcAuthType == NtlmRpcAuthType)
				authContext.IncrementRecvSeqNbr();
			this._authContext = authContext;

			return resp;
		}

		private static void EnsureResponseChoice(LDAPMessage response, LDAPMessage_ProtocolOp.ChoiceIndex expected)
		{
			if (response.protocolOp.SelectedChoice != expected)
				throw new ProtocolViolationException($"The LDAP server returned an unexpected response.  Expected {expected} but received {response.protocolOp.SelectedChoice}");
		}

		internal static void CheckAndThrow(LDAPResult resultMessage)
		{
			switch (resultMessage.resultCode)
			{
				case LDAPResult_ResultCode.Success:
					break;
				default:
					{
						throw new LdapException((LdapResultCode)resultMessage.resultCode, Encoding.UTF8.GetString(resultMessage.diagnosticMessage));
					}
			}
		}

		internal async Task<LdapResponse> Search(SearchRequest_Tagged3 request, ILdapChannelSearchCallback? searchCallback, Control[]? controls, CancellationToken cancellationToken)
		{
			var resp = await this.SendRequest(
				new LDAPMessage_ProtocolOp()
				{
					SearchRequest = request,
				}, cancellationToken, searchCallback: searchCallback, controls: controls).ConfigureAwait(false);
			EnsureResponseChoice(resp.message, LDAPMessage_ProtocolOp.ChoiceIndex.SearchResDone);

			CheckAndThrow(resp.message.protocolOp.SearchResDone);

			return resp;
		}




		private int _nextMessageID;
		internal uint GetNextMessageId() => (uint)Interlocked.Increment(ref this._nextMessageID);

		private ConcurrentDictionary<uint, LdapResponse> _outstandingMessages = new ConcurrentDictionary<uint, LdapResponse>();

		internal async Task<LdapResponse> SendRequest(LDAPMessage_ProtocolOp op, CancellationToken cancellationToken, ILdapChannelSearchCallback? searchCallback = null, Control[]? controls = null)
		{
			uint messageId = this.GetNextMessageId();

			var resp = new LdapResponse() { searchCallback = searchCallback };
			resp.cancelReg = cancellationToken.Register(() => resp.taskSource.TrySetCanceled(cancellationToken));

			this._outstandingMessages.TryAdd(messageId, resp);

			await SendMessage(op, controls, messageId, cancellationToken).ConfigureAwait(false);

			await resp.taskSource.Task.ConfigureAwait(false);
			resp.cancelReg.Unregister();

			this._outstandingMessages.TryRemove(messageId, out _);

			return resp;
		}

		protected override Task OnStopping()
		{
			while (this._outstandingMessages.Count > 0)
			{
				var entry = this._outstandingMessages.FirstOrDefault();
				entry.Value.taskSource.TrySetException(ChannelClosedException());
				this._outstandingMessages.TryRemove(entry);
			}
			return base.OnStopping();
		}

		protected override void HandleMessage(LDAPMessage message)
		{
			if (this._outstandingMessages.TryGetValue(message.messageID, out var resp))
			{
				switch (message.protocolOp.SelectedChoice)
				{
					case LDAPMessage_ProtocolOp.ChoiceIndex.SearchResEntry:
						resp.searchCallback?.OnEntry(message.protocolOp.SearchResEntry);
						break;
					case LDAPMessage_ProtocolOp.ChoiceIndex.SearchResRef:
						foreach (var entry in message.protocolOp.SearchResRef)
						{
							string text = Encoding.UTF8.GetString(entry);
							resp.searchCallback?.OnReference(text);
						}
						break;
					case LDAPMessage_ProtocolOp.ChoiceIndex.SearchResDone:
					default:
						resp.message = message;
						resp.taskSource.TrySetResult(0);
						break;
				}
			}
			else
			{
				// TODO: Report spurious reply
			}
		}
	}
}
