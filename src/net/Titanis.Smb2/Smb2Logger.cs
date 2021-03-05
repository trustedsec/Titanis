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
	}
	public class Smb2Logger : ISmb2TraceCallback
	{
		public Smb2Logger(ILog log)
		{
			ArgumentNullException.ThrowIfNull(log);
			this.Log = log;
		}

		public ILog Log { get; }

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
		private static readonly LogMessageType SessionAuthenticated = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)Smb2LogMessageId.SessionAuthenticated, @"Authenticated to \\{0}
	Session ID: 0x{1:X16}
	Session key: {2}",
			"serverName",
			"sessionId",
			"sessionKey"
			);
		private static readonly LogMessageType ShareConnected = new LogMessageType(LogMessageSeverity.Diagnostic, SourceName, (int)Smb2LogMessageId.ShareConnected, @"Connect to share \\{0}\{1}
	Share capabilities: {2}
	Share flags : {3}",
			"serverName",
			"shareName",
			"capabilities",
			"shareFlags");

		void ISmb2TraceCallback.OnConnecting(EndPoint serverEP, string serverName, Smb2ConnectionOptions options)
		{
			this.Log.WriteMessage(Connecting.Create(
				serverName,
				serverEP,
				options.ClientGuid,
				options.Capabilities,
				options.SecurityMode));
		}

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
		}

		void ISmb2TraceCallback.OnSessionAuthenticated(Smb2Session session)
		{
			this.Log.WriteMessage(SessionAuthenticated.Create(
				session.Connection.ServerName,
				session.SessionId,
				session.GetSessionKey()?.ToHexString()
				));
		}

		void ISmb2TraceCallback.OnDfsReferralConnectFailed(UncPath uncPath, DfsReferral referral, DfsReferralEntry entry, Exception ex)
		{
			// TODO: Log DFS
		}

		void ISmb2TraceCallback.OnDfsReferralFollowed(UncPath originalPath, Smb2TreeConnect referredShare, UncPath referredPath)
		{
			this.Log.WriteVerbose($"Following DFS referral: {originalPath} => {referredPath}");
		}

		void ISmb2TraceCallback.OnDfsReferralReceived(UncPath uncPath, DfsReferral referral)
		{
			// TODO: Log DFS
		}

		void ISmb2TraceCallback.OnShareConnected(UncPath uncPath, Smb2TreeConnect share)
		{
			this.Log.WriteMessage(ShareConnected.Create(
				share.Session.Connection.ServerName,
				share.ShareName,
				share.Capabilities,
				share.ShareFlags));
		}
	}
}
