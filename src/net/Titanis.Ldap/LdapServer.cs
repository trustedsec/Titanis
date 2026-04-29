using Lightweight_Directory_Access_Protocol_V3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Security;

namespace Titanis.Ldap
{
	public class LdapServer : Runnable
	{
		public LdapServer(ISaslAuthProvider? authProvider)
		{
			this.authProvider = authProvider;
		}

		internal readonly ISaslAuthProvider? authProvider;

		protected override async Task Run(CancellationToken cancellationToken)
		{
			Socket serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
			serverSocket.Bind(new IPEndPoint(IPAddress.Any, 389));
			serverSocket.Listen();

			while (!cancellationToken.IsCancellationRequested)
			{
				var clientSocket = await serverSocket.AcceptAsync(cancellationToken).ConfigureAwait(false);
				HandleClient(clientSocket, cancellationToken);
			}
		}

		private void HandleClient(Socket clientSocket, CancellationToken cancellationToken)
		{
			var stream = new NetworkStream(clientSocket, true);
			var channel = new LdapServerChannel(stream, this);
			channel.Start();
		}
	}

	class LdapServerChannel : LdapChannel
	{
		internal LdapServerChannel(Stream stream, LdapServer server)
			: base(stream)
		{
			this._server = server;
		}

		private readonly LdapServer _server;

		protected override async Task HandleMessage(LDAPMessage message)
		{
			CancellationTokenSource cancelSource = new CancellationTokenSource();

			try
			{
				switch (message.protocolOp.SelectedChoice)
				{
					case LDAPMessage_ProtocolOp.ChoiceIndex.SearchRequest:
						await this.HandleSearchRequest(message, cancelSource.Token).ConfigureAwait(false);
						break;
					case LDAPMessage_ProtocolOp.ChoiceIndex.BindRequest:
						await HandleBindRequest(message, cancelSource.Token).ConfigureAwait(false);
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				// Send error
			}
		}

		private AuthServerContext? _authContext;
		protected override AuthContext? AuthContext => this._authContext;

		private async Task HandleBindRequest(LDAPMessage request, CancellationToken cancellationToken)
		{
			if (request.protocolOp.BindRequest.version != 3)
				throw new NotImplementedException();

			var authReq = request.protocolOp.BindRequest.authentication;

			if (authReq.SelectedChoice is not AuthenticationChoice.ChoiceIndex.Sasl)
				throw new NotImplementedException();

			var sasl = authReq.Sasl;
			var mech = Encoding.UTF8.GetString(sasl.mechanism);

			if (mech != "GSS-SPNEGO")
				throw new NotImplementedException();

			var authContext = this._authContext;
			if (authContext == null)
			{
				var authProvider = this._server.authProvider;
				if (authProvider == null)
					throw new NotImplementedException();

				authContext = authProvider.TryGetAuthContext(mech);
				this._authContext = authContext;
			}

			if (authContext.IsComplete)
				throw new NotImplementedException();

			var responseToken = authContext.Accept(sasl.credentials).ToArray();

			await this.SendMessage(new LDAPMessage_ProtocolOp()
			{
				BindResponse = new BindResponse_Tagged1(authContext.IsComplete ? LDAPResult_ResultCode.Success : LDAPResult_ResultCode.SaslBindInProgress, Array.Empty<byte>(), Array.Empty<byte>(), serverSaslCreds: responseToken)
			}, null, request.messageID, cancellationToken).ConfigureAwait(false);
		}

		private async Task HandleSearchRequest(LDAPMessage request, CancellationToken cancellationToken)
		{
			var searchReq = request.protocolOp.SearchRequest;
			if (searchReq.baseObject.IsNullOrEmpty())
			{
				// This is a rootDSE request

				SearchResultEntry_Tagged4 searchres = GetRootDse();

				await SendMessage(new LDAPMessage_ProtocolOp()
				{
					SearchResEntry = searchres
				}, null, request.messageID, cancellationToken).ConfigureAwait(false);

				await SendMessage(new LDAPMessage_ProtocolOp()
				{
					SearchResDone = new LDAPResult(LDAPResult_ResultCode.Success, Array.Empty<byte>(), Array.Empty<byte>())
				}, null, request.messageID, cancellationToken).ConfigureAwait(false);
			}

			await Task.Yield();
		}

		private SearchResultEntry_Tagged4 CreateSearchResult(LdapEntry entry)
		{
			List<PartialAttribute> partialAttrs = new List<PartialAttribute>(entry.Attributes.Length);

			foreach (var attrEntry in entry.Attributes)
			{
				var encodedValues = Array.ConvertAll(attrEntry.Values, r => attrEntry.AttributeType.Syntax.Encode(r));

				PartialAttribute partialAttr = new PartialAttribute(attrEntry.AttributeType.EncodedName, encodedValues);
				partialAttrs.Add(partialAttr);
			}

			return new SearchResultEntry_Tagged4(Encoding.UTF8.GetBytes(entry.EntryName?.Text ?? string.Empty), partialAttrs.ToArray());
		}

		private SearchResultEntry_Tagged4 GetRootDse()
		{
			Dictionary<string, object> attrValues = new Dictionary<string, object>()
				{
					{ "supportedCapabilities", new string[] {
						"1.2.840.113556.1.4.800",
						"1.2.840.113556.1.4.1670",
						"1.2.840.113556.1.4.1791",
						"1.2.840.113556.1.4.1935",
						"1.2.840.113556.1.4.2080",
						"1.2.840.113556.1.4.2237",
						} },
					{ "serverName", @"CN=ALLENTOWN,CN=Kier\, PE,DC=lumon,DC=ind" },
					{ "ldapServiceName", "lumon.ind:allentown$@LUMON.IND" },
					{ "dnsHostName", "LUMON-DC1.lumon.ind" },
					{ "supportedSASLMechanisms", new string[] {
						"GSSAPI",
						"GSS-SPNEGO",
						"EXTERNAL",
						"DIGEST-MD5"
						} },
					{ "supportedLDAPPolicies", new string[] {
						"MaxPoolThreads",
						"MaxPercentDirSyncRequests",
						"MaxDatagramRecv",
						"MaxReceiveBuffer",
						"InitRecvTimeout",
						"MaxConnections",
						"MaxConnIdleTime",
						"MaxPageSize",
						"MaxBatchReturnMessages",
						"MaxQueryDuration",
						"MaxDirSyncDuration",
						"MaxTempTableSize",
						"MaxResultSetSize",
						"MinResultSets",
						"MaxResultSetsPerConn",
						"MaxNotificationPerConn",
						"MaxValRange",
						"MaxValRangeTransitive",
						"ThreadMemoryLimit",
						"SystemMemoryLimitPercent"
						} },
					{ "supportedLDAPVersion", new string[] { "3", "2" } },
					{ "supportedControl", new string[] {
						"1.2.840.113556.1.4.319",
						"1.2.840.113556.1.4.801",
						"1.2.840.113556.1.4.473",
						"1.2.840.113556.1.4.528",
						"1.2.840.113556.1.4.417",
						"1.2.840.113556.1.4.619",
						"1.2.840.113556.1.4.841",
						"1.2.840.113556.1.4.529",
						"1.2.840.113556.1.4.805",
						"1.2.840.113556.1.4.521",
						"1.2.840.113556.1.4.970",
						"1.2.840.113556.1.4.1338",
						"1.2.840.113556.1.4.474",
						"1.2.840.113556.1.4.1339",
						"1.2.840.113556.1.4.1340",
						"1.2.840.113556.1.4.1413",
						"2.16.840.1.113730.3.4.9",
						"2.16.840.1.113730.3.4.1",
						"1.2.840.113556.1.4.1504",
						"1.2.840.113556.1.4.1852",
						"1.2.840.113556.1.4.802",
						"1.2.840.113556.1.4.1907",
						"1.2.840.113556.1.4.1948",
						"1.2.840.113556.1.4.1974",
						"1.2.840.113556.1.4.1341",
						"1.2.840.113556.1.4.2026",
						"1.2.840.113556.1.4.2064",
						"1.2.840.113556.1.4.2065",
						"1.2.840.113556.1.4.2066",
						"1.2.840.113556.1.4.2090",
						"1.2.840.113556.1.4.2205",
						"1.2.840.113556.1.4.2204",
						"1.2.840.113556.1.4.2206",
						"1.2.840.113556.1.4.2211",
						"1.2.840.113556.1.4.2239",
						"1.2.840.113556.1.4.2255",
						"1.2.840.113556.1.4.2256",
						"1.2.840.113556.1.4.2309",
						"1.2.840.113556.1.4.2330",
						"1.2.840.113556.1.4.2354",
						} },
					{ "rootDomainNamingContext", "DC=lumon,DC=ind" },
					{ "configurationNamingContext", "CN=Configuration,DC=lumon,DC=ind" },
					{ "schemaNamingContext", "CN=Schema,CN=Configuration,DC=lumon,DC=ind" },
					{ "defaultNamingContext", "DC=lumon,DC=ind" },
					{ "namingContexts", new string[] {
						"DC=lumon,DC=ind",
						"CN=Configuration,DC=lumon,DC=ind",
						"CN=Schema,CN=Configuration,DC=lumon,DC=ind",
						"DC=DomainDnsZones,DC=lumon,DC=ind",
						"DC=ForestDnsZones,DC=lumon,DC=ind",
						} },
					{ "dsServiceName", "CN=NTDS Settings,CN=ALLENTOWN,DC=lumon,DC=ind" },
					{ "subschemaSubentry", "CN=Aggregate,CN=Schema,CN=Configuration,DC=lumon,DC=ind" }
				};

			LdapEntry entry = new LdapEntry(null, attrValues);
			var searchres = CreateSearchResult(entry);
			return searchres;
		}
	}
}
