using KerberosV5Spec2;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Asn1.Serialization;
using Titanis.IO;
using Titanis.Net;

namespace Titanis.Security.Kerberos
{

	record struct ChangepwRequest(
		KerberosCredential Credential,
		TicketInfo Ticket)
	{

	}

	internal interface IKerberosTransport
	{
		Task<KDC_REP_CHOICE> TransceiveKdcAsync(
			string realm,
			EndPoint kdcEP,
			KDC_REQ_CHOICE kdcreq,
			CancellationToken cancellationToken);

		Task SendChangepwRequest(KerberosClient client, EndPoint kdcEP, byte[] privData, ChangepwVersion version, ChangepwRequest request, CancellationToken cancellationToken);
	}

	internal class KerberosSocketTransport : IKerberosTransport
	{
		private readonly ISocketService _socketService;

		public KerberosSocketTransport(ISocketService socketService)
		{
			ArgumentNullException.ThrowIfNull(socketService);
			this._socketService = socketService;
		}

		/// <summary>
		/// Builds a PDU from a <see cref="KDC_REQ_CHOICE"/>.
		/// </summary>
		/// <param name="obj">Protocol object</param>
		/// <returns>A buffer containing the PDU suitable for transmission within the application protocol</returns>
		private static Memory<byte> BuildPdu(KDC_REQ_CHOICE obj)
		{
			Asn1DerEncoder encoder = Asn1DerEncoding.CreateDerEncoder();
			obj.EncodeTlv(encoder);
			var writer = encoder.GetWriter();
			int cbPdu = writer.Position;
			writer.WriteInt32BE(cbPdu);
			Memory<byte> pduBytes = writer.GetData();
			return pduBytes;
		}

		public async Task<KDC_REP_CHOICE> TransceiveKdcAsync(
			string realm,
			EndPoint kdcEP,
			KDC_REQ_CHOICE kdcreq,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(kdcEP);
			ArgumentNullException.ThrowIfNull(kdcreq);

			Debug.Assert(!string.IsNullOrEmpty(realm));

			Memory<byte> requestBytes = BuildPdu(kdcreq);

			using (var s = await _socketService!.ConnectTcp(kdcEP, cancellationToken).ConfigureAwait(false))
			{
				var stream = s.GetStream(false);
				await stream.WriteAsync(requestBytes, cancellationToken).ConfigureAwait(false);

				byte[]? buf = null;
				const int BufferSize = 64 * 1024;
				try
				{
					buf = ArrayPool<byte>.Shared.Rent(BufferSize);

					int cbTotalRecv = await stream.ReadAtLeastAsync(buf, 0, buf.Length, 4, cancellationToken).ConfigureAwait(false);

					int cbPdu = BinaryPrimitives.ReadInt32BigEndian(buf.SliceReadOnly(0, 4));
					if (cbPdu < 4)
						throw new ProtocolViolationException("The KDC returned an empty response.  This may indicate that it could not parse the request.");

					await stream.ReadAllAsync(buf, cbTotalRecv, (cbPdu + 4 - cbTotalRecv), cancellationToken).ConfigureAwait(false);

					s.Shutdown(SocketShutdown.Both);


					var rep = ParseReplyPdu(buf);
					return rep;
				}
				finally
				{
					if (buf != null)
						ArrayPool<byte>.Shared.Return(buf);
				}
			}
		}

		internal static KDC_REP_CHOICE ParseReplyPdu(ReadOnlyMemory<byte> pduBytes)
		{
			return Asn1DerDecoder.DecodeTlv<KDC_REP_CHOICE>(pduBytes.Slice(4));
		}

		public async Task SendChangepwRequest(
			KerberosClient client,
			EndPoint kdcEP,
			byte[] privData,
			ChangepwVersion version,
			ChangepwRequest request,
			CancellationToken cancellationToken)
		{
			var authContext = new MskileClientContext(request.Credential, client, KerberosClient.ChangePwSpn, request.Ticket, null);
			authContext.RequiredCapabilities |= SecurityCapabilities.DceStyle;
			var apreqBytes = authContext.Initialize().ToArray();

			var privBytes = authContext.EncodeKrbPriv(privData);

			var msg = new ChangepwMessage
			{
				MessageLength = checked((ushort)(6 + apreqBytes.Length + privBytes.Length)),
				ProtocolVersionNumber = version,
				ApreqLength = checked((ushort)apreqBytes.Length),
				Apreqdata = apreqBytes,
				PrivMessage = privBytes.ToArray()
			};
			var writer = new ByteWriter(4 + msg.MessageLength);
			writer.WriteInt32BE(msg.MessageLength);
			writer.WritePduStruct(msg);

			var bytes = writer.GetData();

			var socketService = this._socketService;
			var socket = await socketService.ConnectTcp(kdcEP, cancellationToken).ConfigureAwait(false);

			byte[] replyBuf;
			int cbRecv;
			await using (socket)
			{
				await socket.SendAsync(bytes, SocketFlags.None, cancellationToken).ConfigureAwait(false);

				replyBuf = new byte[32 * 1024];

				cbRecv = await socket.ReceiveAtLeastAsync(
					replyBuf,
					4 + 6,
					cancellationToken).ConfigureAwait(false);
				var cbMessage = BinaryPrimitives.ReadInt32BigEndian(replyBuf) + 4;
				if (cbRecv < cbMessage)
					cbRecv += await socket.ReceiveAtLeastAsync(replyBuf.AsMemory(cbRecv), cbMessage - cbRecv, cancellationToken).ConfigureAwait(false);
			}

			if (version == ChangepwVersion.Win2kResetPasswordVersionNumber
				&& replyBuf[4] == 0x7E
				)
			{
				var cbReply = BinaryPrimitives.ReadInt32BigEndian(replyBuf.AsSpan(0, 4));
				KRB_ERROR err = Asn1DerDecoder.DecodeTlv<KRB_ERROR>(replyBuf.AsMemory(4, cbReply));
				if (err.Value.error_code != 0)
					throw err.Value.GetException();

				return;
			}
			else
			{
				var reader = new ByteMemoryReader(replyBuf.AsMemory(4, cbRecv - 4));

				var reply = reader.ReadPduStruct<ChangepwMessage>();
				authContext.Initialize(reply.Apreqdata);
				var privReply = Asn1DerDecoder.DecodeTlv<KRB_PRIV>(reply.PrivMessage);

				var privEncpart = authContext.InitiatorSubkey.DecryptTlv<EncKrbPrivPart>(KeyUsage.Priv, privReply.Value.enc_part).Value;
				if (privEncpart.user_data.Length >= 2)
				{
					var status = (ChangepwStatus)BinaryPrimitives.ReadUInt16BigEndian(privEncpart.user_data);
					if (status != ChangepwStatus.Success)
					{
						string? errorMessage = null;
						if (privEncpart.user_data.Length > 2)
							errorMessage = Encoding.UTF8.GetString(privEncpart.user_data.Slice(2));

						throw new KrbChangePasswordException(status, errorMessage);
					}
				}
			}
		}

	}
}
