using Lightweight_Directory_Access_Protocol_V3;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Titanis.Asn1.Serialization;
using Titanis.Certificates;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.Ldap
{
	// [RFC 4511] § 4.5.1.2
	/// <summary>
	/// Specifies the scope of the search.
	/// </summary>
	public enum LdapSearchScope
	{
		/// <summary>
		/// Only the base entry itself
		/// </summary>
		BaseObject = 0,
		Base = BaseObject,
		/// <summary>
		/// The immediate subordinates of the search base entry
		/// </summary>
		SingleLevel = 1,
		/// <summary>
		/// All subordinates of the search base entry
		/// </summary>
		WholeSubtree = 2,
		Subtree = WholeSubtree
	}

	/// <summary>
	/// Implements the client side of LDAP.
	/// </summary>
	public partial class LdapClient
	{
		private LdapClient(LdapClientChannel channel)
		{
			this._channel = channel;
		}

		private readonly LdapClientChannel _channel;

		/// <summary>
		/// Port for normal LDAP
		/// </summary>
		public const int LdapPort = 389;
		/// <summary>
		/// Port for LDAP over SSL
		/// </summary>
		public const int LdapsPort = 636;
		/// <summary>
		/// Port for the Global Catalog service
		/// </summary>
		public const int GcLdapPort = 3268;
		/// <summary>
		/// Port for the Global Catalog service over SSL
		/// </summary>
		public const int GcLdapsPort = 3269;

		/// <summary>
		/// Gets the name of the schema container.
		/// </summary>
		public LdapDistinguishedName? SchemaRoot { get; private set; }
		/// <summary>
		/// Gets the name of the domain root container.
		/// </summary>
		public LdapDistinguishedName? DomainRoot { get; private set; }
		/// <summary>
		/// Gets the name of the configuration container.
		/// </summary>
		public LdapDistinguishedName? ConfigurationRoot { get; private set; }
		/// <summary>
		/// Gets the name of the forest root container.
		/// </summary>
		public LdapDistinguishedName? ForestRoot { get; private set; }
		/// <summary>
		/// Connects to an LDAP server.
		/// </summary>
		/// <param name="ep">Server endpoint</param>
		/// <param name="socketService">Socket service</param>
		/// <param name="credentials">Client credentials</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>The connected <see cref="LdapClient"/>.</returns>
		public static async Task<LdapClient> Connect(EndPoint ep, SslClientAuthenticationOptions? sslOptions, ISocketService socketService, IClientCredentialService? credentials, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(ep);
			ArgumentNullException.ThrowIfNull(socketService);

			var socket = await socketService.ConnectTcp(ep, cancellationToken).ConfigureAwait(false);
			var stream = socket.GetStream(true);
			bool useSsl = (sslOptions != null);
			bool sslAuth = false;
			ChannelBinding? channelBinding = null;
			if (useSsl)
			{
				Debug.Assert(sslOptions != null);
				var secureStream = new SslStream(stream, false);
				secureStream.AuthenticateAsClient(sslOptions);
				sslAuth = secureStream.IsMutuallyAuthenticated;
				var cert2 = secureStream.RemoteCertificate as X509Certificate2;
				if (cert2 != null)
					channelBinding = new TlsServerEndPointChannelBinding(cert2);

				stream = secureStream;
			}

			var channel = new LdapClientChannel(stream);
			await channel.Start().ConfigureAwait(false);
			var ldap = new LdapClient(channel);

			var rootDse = (await ldap.Search(new LdapQuery(null, LdapSearchScope.BaseObject, null, [
				LdapAttributeTypes.SubSchemaSubEntry,
				LdapAttributeTypes.dsServiceName,
				LdapAttributeTypes.namingContexts,
				LdapAttributeTypes.defaultNamingContext,
				LdapAttributeTypes.schemaNamingContext,
				LdapAttributeTypes.configurationNamingContext,
				LdapAttributeTypes.rootDomainNamingContext,
				LdapAttributeTypes.supportedControl,
				LdapAttributeTypes.supportedLDAPVersion,
				LdapAttributeTypes.supportedLDAPPolicies,
				LdapAttributeTypes.supportedSASLMechanisms,
				LdapAttributeTypes.DNSHostName,
				LdapAttributeTypes.ldapServiceName,
				LdapAttributeTypes.ServerName,
				LdapAttributeTypes.supportedCapabilities
				]), cancellationToken).ConfigureAwait(false)).Entries.FirstOrDefault();
			if (rootDse == null)
				throw new ProtocolViolationException("The LDAP server did not return a root DSE.");

			var dnsName = rootDse[LdapAttributeTypes.DNSHostName]?.Value as string;

			if (!sslAuth)
			{
				if (credentials is null)
					throw new ArgumentNullException(nameof(credentials));

				ServicePrincipalName? spn = null;
				{
					var serviceName = rootDse[LdapAttributeTypes.ldapServiceName]?.Value as string;
					if (!string.IsNullOrEmpty(serviceName))
					{
						var m = rgxLdapServiceName.Match(serviceName);
						if (m.Success)
						{
							spn = new ServicePrincipalName(PrincipalNameType.ServiceInstance, "LDAP", [dnsName, m.Groups["domain"].Value]);
						}
					}
				}


				var flags = (sslOptions != null)
					? SecurityCapabilities.None
					: SecurityCapabilities.Integrity | SecurityCapabilities.SequenceDetection | SecurityCapabilities.ReplayDetection | SecurityCapabilities.Confidentiality;
				var authContext = credentials.GetAuthContextForService(spn, flags, AuthOptions.PreferSpnego);
				if (authContext != null)
				{
					if (authContext is null)
						throw new ArgumentException($"The credential service did not provide an authentication context for {spn}.", nameof(credentials));

					authContext.ChannelBinding = channelBinding;
					if (spn is null)
					{
						spn = new ServicePrincipalName(PrincipalNameType.ServiceInstance, "LDAP", dnsName);
					}
					var resp2 = await channel.Bind(authContext, cancellationToken).ConfigureAwait(false);
				}
			}

			ldap.SchemaRoot = rootDse[LdapAttributeTypes.schemaNamingContext]?.Value as LdapDistinguishedName;
			ldap.DomainRoot = rootDse[LdapAttributeTypes.defaultNamingContext]?.Value as LdapDistinguishedName;
			ldap.ForestRoot = rootDse[LdapAttributeTypes.rootDomainNamingContext]?.Value as LdapDistinguishedName;
			ldap.ConfigurationRoot = rootDse[LdapAttributeTypes.configurationNamingContext]?.Value as LdapDistinguishedName;

			return ldap;
		}

		private static readonly Regex rgxLdapServiceName = LdapServiceRegex();
		[GeneratedRegex(@"^(?<forest>[^:]*):(?<computer>[^@]*)@(?<domain>.*)$")]
		private static partial Regex LdapServiceRegex();

		// [RFC 4532] § 2
		private const string WhoamiOid = "1.3.6.1.4.1.4203.1.11.3";
		// [RFC 4532] § 2.1
		private static readonly byte[] WhoamiBytes = new byte[]
		{
			0x31, 0x2e, 0x33, 0x2e, 0x36, 0x2e, 0x31,
			0x2e, 0x34, 0x2e, 0x31, 0x2e, 0x34, 0x32, 0x30,  0x33, 0x2e, 0x31, 0x2e, 0x31, 0x31, 0x2e, 0x33,
		};
		public async Task<SaslIdentity> Whoami(CancellationToken cancellationToken)
		{
			var resp = await _channel.SendRequest(new LDAPMessage_ProtocolOp()
			{
				ExtendedReq = new ExtendedRequest_Tagged23(WhoamiBytes)
			}, cancellationToken).ConfigureAwait(false);

			var ext = resp.message.protocolOp.ExtendedResp;
			if (ext.resultCode != LDAPResult_ResultCode.Success)
				throw new LdapException((LdapResultCode)ext.resultCode, Encoding.UTF8.GetString(ext.diagnosticMessage));
			// [RFC 4513] § 5.2.1.8.  SASL Authorization Identities
			var authzId = Encoding.UTF8.GetString(ext.responseValue);
			return new SaslIdentity(authzId);
		}

		#region Searching
		class SearchResultBuilder : ILdapChannelSearchCallback
		{
			internal SearchResultBuilder(IList<AttributeSpec>? allAttrs, LdapQueryOptions options)
			{
				this.allAttrs = allAttrs;
			}

			internal ILdapClientSearchCallback? callback;

			internal int entryCount;
			internal readonly List<LdapEntry> entries = new List<LdapEntry>();
			internal readonly List<string> referrals = new List<string>();

			/// <remarks>
			/// If set, forces entries for all attributes
			/// </remarks>
			internal readonly IList<AttributeSpec>? allAttrs;

			public void OnEntry(SearchResultEntry_Tagged4 result)
			{
				try
				{
					var nameBytes = result.objectName;
					var name = (nameBytes != null) ? new LdapDistinguishedName(Encoding.UTF8.GetString(nameBytes)) : null;

					HashSet<string> attrNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
					PartialAttribute[] attrStrucs = result.attributes;
					List<LdapAttribute> attrs = new List<LdapAttribute>(attrStrucs.Length);
					for (int iAttr = 0; iAttr < attrStrucs.Length; iAttr++)
					{
						PartialAttribute? attrStruc = attrStrucs[iAttr];
						var attrDesc = new LdapAttributeDescription(attrStruc.type);
						// TODO: If this is a custom attribute, check the directory schema
						var attrType = LdapAttributeTypes.TryGetByNameOrOid(attrDesc.TypeName);
						if (attrType == null)
						{
							attrType = new AttributeTypeDescription(AttributeTypeDescriptionFlags.None, string.Empty, [attrDesc.TypeName], syntax: AdSyntaxes.StringOctet);
						}
						// Decode values
						object[] attrValues = new object[attrStruc.vals.Length];
						for (int iValue = 0; iValue < attrStruc.vals.Length; iValue++)
						{
							byte[]? valueBytes = attrStruc.vals[iValue];
							object value;
							if (attrType.Syntax != null)
							{
								try
								{
									value = attrType.Syntax.Decode(valueBytes);
								}
								catch (Exception ex)
								{
									// TODO: Somehow represent undecoded value.  For now, set as Blob
									value = new BinaryString(valueBytes, ex);
								}
							}
							else
							{
								value = new BinaryString(valueBytes);
							}

							attrValues[iValue] = value;
						}

						var attr = new LdapAttribute(attrDesc, attrType, attrValues);
						attrs.Add(attr);
						attrNames.Add(attr.TypeName);
					}

					if (this.allAttrs != null)
					{
						foreach (var attrName in this.allAttrs)
						{
							var attrDesc = new LdapAttributeDescription(attrName.rep);
							var attrType = LdapAttributeTypes.TryGetByNameOrOid(attrDesc.TypeName);
							if (attrNames.Add(attrDesc.TypeName))
								attrs.Add(new LdapAttribute(attrDesc, attrType, Array.Empty<object>()));
						}
					}

					LdapEntry entry = new LdapEntry(name, attrs.ToArray());
					this.entryCount++;
					if (this.callback != null)
						this.callback.OnEntry(entry);
					else
						entries.Add(entry);

					attrs.Clear();
				}
				catch
				{
					// TODO: Report decoding error
				}
			}

			public void OnReference(string reference)
			{
				if (this.callback != null)
					this.callback.OnReference(reference);
				else
					this.referrals.Add(reference);
			}
		}

		private LdapDistinguishedName? ResolveSearchBase(LdapDistinguishedName? dn)
		{
			if (dn is null)
				return null;

			if (object.ReferenceEquals(dn, LdapDistinguishedNameConverter.DomainRoot))
				return this.DomainRoot;
			else if (object.ReferenceEquals(dn, LdapDistinguishedNameConverter.ForestRoot))
				return this.ForestRoot;
			else if (object.ReferenceEquals(dn, LdapDistinguishedNameConverter.ConfigRoot))
				return this.ConfigurationRoot;
			else if (object.ReferenceEquals(dn, LdapDistinguishedNameConverter.SchemaRoot))
				return this.SchemaRoot;
			else if (object.ReferenceEquals(dn, LdapDistinguishedNameConverter.RootDse))
				return null;
			else if (dn.Rdns.Count == 0)
				return null;
			else
				return dn;
		}

		/// <summary>
		/// Executes a search request.
		/// </summary>
		/// <param name="query"><see cref="LdapQuery"/></param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <param name="searchCallback"><see cref="ILdapClientSearchCallback"/> to receive results as they arrive</param>
		/// <returns>A <see cref="LdapSearchResult"/> describing the result</returns>
		/// <remarks>
		/// If the caller provides <paramref name="searchCallback"/> then the returned <see cref="LdapSearchResult"/> does not include the entries nor referrals returned by the search.
		/// </remarks>
		public async Task<LdapSearchResult> Search(LdapQuery query, CancellationToken cancellationToken, ILdapClientSearchCallback? searchCallback = null)
		{
			ArgumentNullException.ThrowIfNull(query);

			var searchBase = this.ResolveSearchBase(query.SearchBase);
			var filter = query.Filter ?? FilterFactory.Any();
			var attributes = (query.Attributes is null) ? [] : Array.ConvertAll(query.Attributes, r => r.rep);

			var allAttrs = (query.IncludeMissingAttributes ? query.Attributes : null);

			List<Control> controls = new List<Control>();
			if (searchBase != null)
			{
				if (query.PageSize.HasValue)
				{
					byte[] pagingControl = Asn1DerEncoder.EncodeTlv(new PagingSearchControlValue(query.PageSize.Value, query.PagingBookmark ?? Array.Empty<byte>())).ToArray();
					controls.Add(new Control(Encoding.UTF8.GetBytes(AdExtensions.PagingControlOid), false, pagingControl));
				}
				if (query.WatchForChanges)
				{
					controls.Add(new Control(Encoding.UTF8.GetBytes(AdExtensions.NotificationOid), true, null));
				}
				if (query.IncludeDeleted)
					controls.Add(new Control(Encoding.UTF8.GetBytes(AdExtensions.ShowDeletedOid), false, null));
				if (query.IncludeRecycled)
					controls.Add(new Control(Encoding.UTF8.GetBytes(AdExtensions.ShowRecycledOid), false, null));
				if (query.IncludeDeletedLinks)
					controls.Add(new Control(Encoding.UTF8.GetBytes(AdExtensions.ShowDeactivatedLinkOid), false, null));
				if (query.DirSyncCookie != null)
				{
					// [MS-ADTS] § 3.1.1.3.4.1.3 LDAP_SERVER_DIRSYNC_OID
					// NOTE: While [MS-ADTS] specifies NULL for Cookie, this results in an LDAP error; using empty array instead
					byte[] dirsync = Asn1DerEncoder.EncodeTlv(new DirSyncRequestValue(DirSyncFlags.IncrementalValues, cookie: query.DirSyncCookie)).ToArray();
					controls.Add(new Control(Encoding.UTF8.GetBytes(AdExtensions.DirSyncOid), true, dirsync));
				}
			}

			SearchRequest_Tagged3 asn1Search = new(
				(searchBase != null) ? Encoding.UTF8.GetBytes(searchBase.Text) : Array.Empty<byte>(),
				(SearchRequest_Tagged3_Scope)query.Scope,
				SearchRequest_Tagged3_DerefAliases.NeverDerefAliases,
				0,
				120,
				false,
				filter.struc,
				attributes
				);
			var callback = new SearchResultBuilder(allAttrs, query.Options) { callback = searchCallback };
			var resp = await this._channel.Search(asn1Search, callback, controls.ToArray(), cancellationToken).ConfigureAwait(false);

			byte[]? bookmark = null;
			byte[]? dirsyncCookie = null;
			if (!resp.message.controls.IsNullOrEmpty())
			{
				foreach (var control in resp.message.controls)
				{
					var oid = Encoding.UTF8.GetString(control.controlType);
					if (oid == AdExtensions.PagingControlOid)
					{
						var pagingValue = Asn1DerDecoder.DecodeTlv<PagingSearchControlValue>(control.controlValue);
						bookmark = pagingValue.cookie;
					}
					else if (oid == AdExtensions.DirSyncOid)
					{
						var dirsyncValue = Asn1DerDecoder.DecodeTlv<DirSyncRequestValue>(control.controlValue);
						dirsyncCookie = dirsyncValue.cookie;
					}
				}
			}

			return new LdapSearchResult(callback.entryCount, callback.entries.ToArray(), callback.referrals.ToArray()) { Bookmark = bookmark, DirsyncCookie = dirsyncCookie };
		}

		/// <summary>
		/// Searches for entries with a specified name
		/// </summary>
		/// <param name="searchRoot">Search base entry</param>
		/// <param name="scope"><see cref="LdapSearchScope"/> indicating scope</param>
		/// <param name="name">Name of object to search for</param>
		/// <param name="attributes">Attributes to return</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="LdapSearchResult"/> containing search results.</returns>
		/// <remarks>
		/// This method constructs an <see cref="LdapFilter"/> to search the ANR attribute which generally includes all name-like attributes.  See [MS-ADTS] for details on how this attribute works.
		/// </remarks>
		public Task<LdapSearchResult> SimpleSearch(LdapDistinguishedName? searchRoot, LdapSearchScope scope, string name, AttributeSpec[]? attributes, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(name);
			searchRoot ??= this.DomainRoot;

			LdapFilter? filter;
			if (name == "*")
			{
				filter = null;
			}
			else if (name.StartsWith('*') && name.EndsWith('*'))
			{
				filter = FilterFactory.ContainsSubstring(LdapAttributeTypes.ANR, name[1..^1]);
			}
			else if (name.StartsWith('*'))
			{
				filter = FilterFactory.ContainsSubstring(LdapAttributeTypes.ANR, name[1..]);
			}
			else if (name.EndsWith('*'))
			{
				filter = FilterFactory.ContainsSubstring(LdapAttributeTypes.ANR, name[0..^1]);
			}
			else
			{
				filter = FilterFactory.Matches(LdapAttributeTypes.ANR, name);
			}

			return this.Search(new LdapQuery(searchRoot, scope, filter, attributes), cancellationToken);
		}

		/// <summary>
		/// Searches for entries with a specified name
		/// </summary>
		/// <param name="name">Name of object to search for</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="LdapSearchResult"/> containing search results.</returns>
		/// <remarks>
		/// This method constructs an <see cref="LdapFilter"/> to search the ANR attribute which generally includes all name-like attributes.  See [MS-ADTS] for details on how this attribute works.
		/// </remarks>
		public Task<LdapSearchResult> SimpleSearch(string name, CancellationToken cancellationToken) => this.SimpleSearch(name, null, cancellationToken);
		/// <summary>
		/// Searches for entries with a specified name
		/// </summary>
		/// <param name="name">Name of object to search for</param>
		/// <param name="attributes">Attributes to return</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="LdapSearchResult"/> containing search results.</returns>
		/// <remarks>
		/// This method constructs an <see cref="LdapFilter"/> to search the ANR attribute which generally includes all name-like attributes.  See [MS-ADTS] for details on how this attribute works.
		/// </remarks>
		public Task<LdapSearchResult> SimpleSearch(string name, AttributeSpec[]? attributes, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(name);
			return this.SimpleSearch(this.DomainRoot, LdapSearchScope.WholeSubtree, name, null, cancellationToken);
		}
		#endregion

		#region Schema
		private Dictionary<string, LdapAttributeSchema> _attrsByName = new Dictionary<string, LdapAttributeSchema>(StringComparer.OrdinalIgnoreCase);

		private LdapAttributeSchema CreateAttributeSchema(LdapEntry entry)
		{
			var ldapName = (string)entry[LdapAttributeTypes.LDAPDisplayName].Value;
			var attrSyntaxId = (string)entry[LdapAttributeTypes.AttributeSyntax].Value;
			var omSyntax = (int)entry[LdapAttributeTypes.OMSyntax].Value;
			var omObjectClass = (string)entry[LdapAttributeTypes.OmObjectClass]?.Value;

			var syntax = AdSyntaxes.TryGetSyntax(attrSyntaxId, omSyntax, omObjectClass);

			var attr = new LdapAttributeSchema(ldapName, syntax);
			return attr;
		}

		public async Task<AttributeTypeDescription?> GetAttribute(string ldapName, CancellationToken cancellationToken)
		{
			await Task.Yield();

			//if (this._attrsByName.TryGetValue(ldapName, out var attr))
			//	return attr;
			AttributeTypeDescription attr;
			if ((attr = LdapAttributeTypes.TryGetByNameOrOid(ldapName)) != null)
				return attr;

			return null;
			//var result = await Search(new LdapQuery(
			//	SchemaRoot,
			//	LdapSearchScope.SingleLevel,
			//	FilterFactory.Matches(LdapAttributeTypes.LDAPDisplayName, ldapName), [SpecialAttributes.LdapDisplayName, SpecialAttributes.AttributeSyntax, SpecialAttributes.OmSyntax, SpecialAttributes.OmObjectClass]),
			//	cancellationToken).ConfigureAwait(false);
			//if (result.Entries.Length == 0)
			//	return null;

			//attr = CreateAttributeSchema(result.Entries[0]);
			//lock (this._attrsByName)
			//{
			//	if (this._attrsByName.TryGetValue(ldapName, out var existing))
			//		attr = existing;
			//	else
			//		this._attrsByName.Add(ldapName, existing);
			//}

			//return attr;
		}


		private Dictionary<string, LdapClassSchema> _classesByName = new Dictionary<string, LdapClassSchema>(StringComparer.OrdinalIgnoreCase);
		#endregion

		public async Task Add(
			LdapDistinguishedName dn,
			Dictionary<string, object> attributes,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(dn);
			ArgumentNullException.ThrowIfNull(attributes);

			List<PartialAttribute> attrs = new List<PartialAttribute>(attributes.Count);
			foreach (var attrEntry in attributes)
			{
				if (attrEntry.Value is null)
					throw new ArgumentException($"The attribute list contains an attribute '{attrEntry.Key}' with no value.");

				var attrSchema = await GetAttribute(attrEntry.Key, cancellationToken).ConfigureAwait(false);
				if (attrSchema is null)
					throw new ArgumentException($"The attributes list contains attribute '{attrEntry.Key}' that cannot be found.", nameof(attributes));

				if (attrEntry.Value is object[] multiValues)
					;
				else
					multiValues = [attrEntry.Value];

				var encodedValues = new byte[multiValues.Length][];
				for (int iValue = 0; iValue < encodedValues.Length; iValue++)
				{
					var multiValue = multiValues[iValue];

					try
					{
						var encoded = (multiValue is byte[] bytes) ? bytes : attrSchema.Syntax.ParseOrEncode(multiValue);
						encodedValues[iValue] = encoded;
					}
					catch (Exception ex)
					{
						throw new ArgumentException($"Error while encoding attribute '{attrEntry.Key}', value '{multiValue}': {ex.Message}", nameof(attributes), ex);
					}
				}
				attrs.Add(new PartialAttribute(attrSchema.EncodedName, encodedValues));
			}

			var resp = await _channel.SendRequest(new LDAPMessage_ProtocolOp()
			{
				AddRequest = new AddRequest_Tagged8(Encoding.UTF8.GetBytes(dn.Text), attrs.ToArray())
			}, cancellationToken).ConfigureAwait(false);
			LdapClientChannel.CheckAndThrow(resp.message.protocolOp.AddResponse);
		}

		public async Task Modify(
			LdapModifyRequest request,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);

			List<ModifyRequest_Tagged6_Changes_Element> attrs = new List<ModifyRequest_Tagged6_Changes_Element>(request._changes.Count);
			foreach (var change in request._changes)
			{
				if (change.Values is null)
					throw new ArgumentException($"The attribute list contains an attribute '{change.Name}' with no value.");

				if (change.Values is byte[][] byteses)
				{
					attrs.Add(new ModifyRequest_Tagged6_Changes_Element((ModifyRequest_Tagged6_Changes_Element_Operation)change.ChangeType, new PartialAttribute(Encoding.UTF8.GetBytes(change.Name), byteses)));
				}
				else if (change.Values is [byte[] bytes])
				{
					attrs.Add(new ModifyRequest_Tagged6_Changes_Element((ModifyRequest_Tagged6_Changes_Element_Operation)change.ChangeType, new PartialAttribute(Encoding.UTF8.GetBytes(change.Name), [bytes])));
				}
				else
				{
					var attrSchema = await GetAttribute(change.Name, cancellationToken).ConfigureAwait(false);
					if (attrSchema is null)
						throw new ArgumentException($"The attributes list contains attribute '{change.Name}' that cannot be found.", nameof(request));

					try
					{
						var values = change.Values;
						byte[][] encoded = Array.ConvertAll(values, attrSchema.Syntax.ParseOrEncode);
						attrs.Add(new ModifyRequest_Tagged6_Changes_Element((ModifyRequest_Tagged6_Changes_Element_Operation)change.ChangeType, new PartialAttribute(attrSchema.EncodedName, encoded)));
					}
					catch (Exception ex)
					{
						throw new ArgumentException($"Error while encoding attribute '{change.Name}': {ex.Message}", nameof(request), ex);
					}
				}
			}

			var dn = request.DistinguishedName;
			var resp = await _channel.SendRequest(new LDAPMessage_ProtocolOp()
			{
				ModifyRequest = new ModifyRequest_Tagged6(Encoding.UTF8.GetBytes(dn.Text), attrs.ToArray())
			}, cancellationToken).ConfigureAwait(false);
			LdapClientChannel.CheckAndThrow(resp.message.protocolOp.ModifyResponse);
		}

		public async Task Move(LdapDistinguishedName oldName, LdapDistinguishedName newName)
		{
			await Task.Yield();
			throw new NotImplementedException();
		}
	}

	internal interface ILdapChannelSearchCallback
	{
		void OnEntry(SearchResultEntry_Tagged4 result);
		void OnReference(string reference);
	}
}
