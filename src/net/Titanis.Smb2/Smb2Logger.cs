using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop;

namespace Titanis.Smb2
{
	[CallbackLogger]
	public class Smb2Logger : ISmb2TraceCallback
	{
		public Smb2Logger(ILog log, ISmb2TraceCallback? chainedCallback = null)
		{
			ArgumentNullException.ThrowIfNull(log);
			this.Log = log;
			this._chainedCallback = chainedCallback;
		}

		public ILog Log { get; }
		private readonly ISmb2TraceCallback? _chainedCallback;

		void ISmb2TraceCallback.OnConnecting(EndPoint serverEP, string serverName, Smb2ConnectionOptions options)
		{
			this.Log.WriteSmb2ClientConnectingMessage(
				serverName,
				serverEP,
				options.ClientGuid,
				options.Capabilities,
				options.SecurityMode);

			this._chainedCallback?.OnConnecting(serverEP, serverName, options);
		}

		void ISmb2TraceCallback.OnConnected(EndPoint serverEP, string serverName, Guid clientGuid, Smb2Connection connection)
		{
			this.Log.WriteSmb2ClientConnectedMessage(
				connection.ServerName,
				serverEP,
				clientGuid,
				connection.ServerGuid,
				connection.Dialect,
				connection.ServerConnectTimeUtc,
				connection.Capabilities,
				connection.ServerSecurityMode,
				connection.SigningAlgorithm,
				connection.CipherId
				);

			this._chainedCallback?.OnConnected(serverEP, serverName, clientGuid, connection);
		}

		void ISmb2TraceCallback.OnSessionAuthenticated(Smb2Session session)
		{
			this.Log.WriteSmb2ClientSessionAuthenticatedMessage(session.Connection.ServerName, session.SessionId, session.GetSessionKey()?.ToHexString());

			this._chainedCallback?.OnSessionAuthenticated(session);
		}

		void ISmb2TraceCallback.OnDfsReferralConnectFailed(UncPath uncPath, DfsReferral referral, DfsReferralEntry entry, UncPath referredPath, Exception ex)
		{
			this.Log.WriteSmb2ClientDfsReferralConnectFailedMessage(uncPath.ToString(), referredPath.ToString(), (Hresult)ex.HResult, ex.Message, ex.ToString());

			this._chainedCallback?.OnDfsReferralConnectFailed(uncPath, referral, entry, referredPath, ex);
		}

		void ISmb2TraceCallback.OnDfsReferralFollowed(UncPath originalPath, Smb2TreeConnect referredShare, UncPath referredPath)
		{
			this.Log.WriteSmb2ClientDfsReferralFollowedMessage(originalPath.ToString(), referredPath.ToString());

			this._chainedCallback?.OnDfsReferralFollowed(originalPath, referredShare, referredPath);
		}

		void ISmb2TraceCallback.OnDfsReferralReceived(UncPath uncPath, DfsReferral referral)
		{
			Guid correlationId = Guid.NewGuid();
			this.Log.WriteSmb2ClientDfsReferralReceivedMessage(correlationId, uncPath.ToString());

			foreach (var entry in referral.Entries)
			{
				this.Log.WriteSmb2ClientDfsReferralDetailMessage(correlationId, entry.DfsTarget.ToString(), entry.ServerType, entry.DfsPath?.ToString(), entry.DfsAltPath?.ToString(), entry.Ttl, entry.SiteServiceGuid);
			}

			this._chainedCallback?.OnDfsReferralReceived(uncPath, referral);
		}

		void ISmb2TraceCallback.OnShareConnected(UncPath uncPath, Smb2TreeConnect share)
		{
			this.Log.WriteSmb2ClientShareConnectedMessage(
				share.Session.Connection.ServerName,
				share.ShareName,
				share.Capabilities,
				share.ShareFlags);

			this._chainedCallback?.OnShareConnected(uncPath, share);
		}
	}
}
