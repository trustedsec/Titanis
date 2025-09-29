using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	public enum Smb2LogMessageId
	{
		Other = 0,
		Connecting,
		Connected,
		ShareConnected,
		SessionAuthenticated,
		DfsReferralConnectFailed,
		DfsReferralFollowed,
		DfsReferralReceived,
		DfsReferralDetail,
	}

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

		public static readonly string SourceName = typeof(Smb2Client).FullName!;


		private static readonly LogMessageType Connecting = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)Smb2LogMessageId.Connecting, @"Connected to \\{0} at {1}
	Client GUID  : {2}
	Capabilities : {3}
	Security mode: {4}",
			"serverName",
			"serverEP",
			"clientGuid",
			"capabilities",
			"securityMode");
		void ISmb2TraceCallback.OnConnecting(EndPoint serverEP, string serverName, Smb2ConnectionOptions options)
		{
			this.Log.WriteMessage(Connecting.Create(
				serverName,
				serverEP,
				options.ClientGuid,
				options.Capabilities,
				options.SecurityMode));

			this._chainedCallback?.OnConnecting(serverEP, serverName, options);
		}

		private static readonly LogMessageType Connected = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)Smb2LogMessageId.Connected, @"Connected to \\{0} at {1}
	Dialect         : {2}
	Server GUID     : {3}
	Server time UTC : {4}
	Capabilities    : {5}
	Cipher          : {6}
	Signing alg     : {7}
	Signing required: {8}",
			"serverName",
			"serverEP",
			"dialect",
			"serverGuid",
			"serverConnectTimeUtc",
			"capabilities",
			"cipherId",
			"signingAlgorithm",
			"requiresSigning");
		void ISmb2TraceCallback.OnConnected(EndPoint serverEP, Smb2Connection connection)
		{
			this.Log.WriteMessage(Connected.Create(
				connection.ServerName,
				serverEP,
				connection.Dialect,
				connection.ServerGuid,
				connection.ServerConnectTimeUtc,
				connection.Capabilities,
				connection.CipherId,
				connection.SigningAlgorithm,
				connection.ServerSecurityMode));

			this._chainedCallback?.OnConnected(serverEP, connection);
		}

		private static readonly LogMessageType SessionAuthenticated = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)Smb2LogMessageId.SessionAuthenticated, @"Authenticated to \\{0}
	Session ID: 0x{1:X16}
	Session key: {2}",
			"serverName",
			"sessionId",
			"sessionKey"
			);
		void ISmb2TraceCallback.OnSessionAuthenticated(Smb2Session session)
		{
			this.Log.WriteMessage(SessionAuthenticated.Create(
				session.Connection.ServerName,
				session.SessionId,
				session.GetSessionKey()?.ToHexString()
				));

			this._chainedCallback?.OnSessionAuthenticated(session);
		}


		private static readonly LogMessageType DfsReferralConnectFailed = new LogMessageType(LogMessageSeverity.Error, SourceName, (int)Smb2LogMessageId.DfsReferralConnectFailed, @"Failed to follow DFS referral for {0}.
	Original path: {0}
	Referred path: {1}
	Error code: {2} (0x{2:X8})
	Error message: {3}
	Details:

{4}", "originalPath", "referredPath", "errorCode", "errorMessage", "exception");
		void ISmb2TraceCallback.OnDfsReferralConnectFailed(UncPath uncPath, DfsReferral referral, DfsReferralEntry entry, UncPath referredPath, Exception ex)
		{
			// TODO: Log DFS
			this.Log.WriteMessage(DfsReferralConnectFailed.Create(uncPath, referredPath, ex.HResult, ex.Message, ex));

			this._chainedCallback?.OnDfsReferralConnectFailed(uncPath, referral, entry, referredPath, ex);
		}

		private static readonly LogMessageType DfsReferralFollowed = new LogMessageType(LogMessageSeverity.Verbose, SourceName, (int)Smb2LogMessageId.DfsReferralFollowed, @"Followed DFS referral for {0}.
	Original path: {0}
	Referred path: {1}", "originalPath", "referredPath");
		void ISmb2TraceCallback.OnDfsReferralFollowed(UncPath originalPath, Smb2TreeConnect referredShare, UncPath referredPath)
		{
			this.Log.WriteMessage(DfsReferralFollowed.Create(originalPath, referredPath));

			this._chainedCallback?.OnDfsReferralFollowed(originalPath, referredShare, referredPath);
		}

		private static readonly LogMessageType DfsReferralReceived = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)Smb2LogMessageId.DfsReferralReceived, @"Server sent DFS referral for {0}.  Details follow.", "originalPath");
		private static readonly LogMessageType DfsReferralDetail = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)Smb2LogMessageId.DfsReferralDetail, @"\tTarget={0}
	Server type: {1}
	Path: {2}
	Alt. path: {3}
	TTL: {4}
	Site service GUID: {5}", "target", "serverType", "path", "altPath", "ttl", "siteServiceGuid");
		void ISmb2TraceCallback.OnDfsReferralReceived(UncPath uncPath, DfsReferral referral)
		{
			// TODO: Log DFS
			this.Log.WriteMessage(DfsReferralReceived.Create(uncPath));
			foreach (var entry in referral.Entries)
			{
				this.Log.WriteMessage(DfsReferralDetail.Create(entry.ServerType, entry.DfsPath, entry.DfsAltPath, entry.Ttl, entry.SiteServiceGuid));
			}

			this._chainedCallback?.OnDfsReferralReceived(uncPath, referral);
		}

		private static readonly LogMessageType ShareConnected = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)Smb2LogMessageId.ShareConnected, @"Connect to share \\{0}\{1}
	Share capabilities: {2}
	Share flags : {3}",
			"serverName",
			"shareName",
			"capabilities",
			"shareFlags");
		void ISmb2TraceCallback.OnShareConnected(UncPath uncPath, Smb2TreeConnect share)
		{
			this.Log.WriteMessage(ShareConnected.Create(
				share.Session.Connection.ServerName,
				share.ShareName,
				share.Capabilities,
				share.ShareFlags));

			this._chainedCallback?.OnShareConnected(uncPath, share);
		}
	}
}
