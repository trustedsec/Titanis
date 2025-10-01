using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.IO;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.Msrpc.Msdcom
{
	using ms_dcom;
	using ms_oaut;
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection.Metadata;
	using Titanis.Winterop;

	/// <summary>
	/// Receives notifications from <see cref="DcomClient"/>.
	/// </summary>
	[Callback]
	public interface IDcomCallback
	{
		void OnDcomConnected(ObjectExporterServerInfo info);
		void OnActivatingObject(Guid clsid, Guid iid);
		void OnActivatedObject(Guid clsid, Guid iid, ActivationResult result);
		void OnActivationFailed(Guid clsid, Guid iid, Exception ex);
		void OnConnectingToExporter(ulong oxid, StringBinding binding);
	}
	/// <seealso cref="ConnectTo(string, RpcClient, CancellationToken, IDcomCallback?)"/>
	public class DcomClient : IObjrefMarshal
	{
		static DcomClient()
		{
			Objref.RegisterUnmarshaler(DcomIds.CLSID_ActivationPropertiesIn, new ActUnmarshaler());
			Objref.RegisterUnmarshaler(DcomIds.CLSID_ActivationPropertiesOut, new ActPropertiesOutUnmarshaler());
			Objref.RegisterUnmarshaler(DcomIds.CLSID_ContextMarshaler, new ClientContextUnmarshaler());
			Objref.RegisterUnmarshaler(DcomIds.CLSID_ErrorObject, new DcomErrorUnmarshaler());

		}
		private DcomClient(
			RpcClient rpcClient,
			IDcomCallback? callback = null
			)
		{
			this._rpcClient = rpcClient;
			this._callback = callback;
		}

		private readonly RpcClient _rpcClient;
		private readonly IDcomCallback? _callback;

		/// <summary>
		/// Gets the host name to prefer when connecting to object exporter.
		/// </summary>
		/// <remarks>
		/// When the client activates an object, the server provides
		/// a list of endpoints.  Usually this list contains an entry
		/// for every host name and IP address of the server.
		/// When <see cref="PreferredHostName"/> is set, this implementation
		/// will first try entries with a host name matching the value of
		/// <see cref="PreferredHostName"/> using a case-insensitive
		/// comparison.  Note that IP address are represented as strings.
		/// <para>
		/// This property is initialized to the host name provided to
		/// <see cref="ConnectTo(string, RpcClient, CancellationToken, IDcomCallback?)"/>.
		/// </para>
		/// </remarks>
		public string? PreferredHostName { get; set; }

		private bool IsPreferredHost(string? hostName)
		{
			if (string.IsNullOrEmpty(hostName) || string.IsNullOrEmpty(this.PreferredHostName))
				return false;

			return this.PreferredHostName.Equals(hostName, StringComparison.OrdinalIgnoreCase);
		}

		internal COMVERSION NegotiatedVersion { get; private set; }
		private ObjectExporterServerInfo? _serverInfo;

		// Object exporter
		private ObjectExporterClient _oxClient;

		// IActivation
		private ActivationClient _activator;

		// IRemoteSCMActivator
		private ScmActivatorClient? _scmActivator;

		internal async Task<ObjectExporterServerInfo> GetServerInfo(
			string host,
			RpcAuthLevel authLevel,
			CancellationToken cancellationToken
			)
		{
			if (string.IsNullOrEmpty(host)) throw new ArgumentException($"'{nameof(host)}' cannot be null or empty.", nameof(host));

			using (var exporter = await _rpcClient.ConnectTcp<ObjectExporterClient>(new DnsEndPoint(host, 135), null, authLevel, cancellationToken).ConfigureAwait(false))
			{
				var info = await exporter.GetServerInfo(cancellationToken).ConfigureAwait(false);
				return info;
			}
		}
		/// <summary>
		/// Connects to the DCOM service on a network host.
		/// </summary>
		/// <param name="host">Name or address of host</param>
		/// <param name="rpcClient"><see cref="RpcClient"/></param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <param name="callback"><see cref="IDcomCallback"/> object that receives notifications.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"><paramref name="host"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="NotSupportedException">The DCOM server returned a version not supported by this implementation.</exception>
		public static async Task<DcomClient> ConnectTo(
			string host,
			RpcClient rpcClient,
			CancellationToken cancellationToken,
			IDcomCallback? callback = null
			)
		{
			if (string.IsNullOrEmpty(host)) throw new ArgumentException($"'{nameof(host)}' cannot be null or empty.", nameof(host));
			ArgumentNullException.ThrowIfNull(rpcClient);

			DcomClient dcom = new DcomClient(rpcClient, callback);
			dcom.PreferredHostName = host;

			// Get server info
			ObjectExporterClient exporter = new ObjectExporterClient();
			dcom._oxClient = exporter;

			// Connect to exporter service
			await rpcClient.ConnectTcp(exporter, new DnsEndPoint(host, 135), null, cancellationToken).ConfigureAwait(false);
			var info = await exporter.GetServerInfo(cancellationToken).ConfigureAwait(false);
			dcom._serverInfo = info;
			callback?.OnDcomConnected(info);

			var rpcChannel = exporter.Proxy.Channel;

			dcom.NegotiatedVersion = info.Version;
			if (info.Version.MajorVersion != 5)
				throw new NotSupportedException($"The remote server version ({info.Version.MajorVersion}.{info.Version.MinorVersion}) is not supported.");

			// IActivation
			var actClient = new ActivationClient();
			dcom._activator = actClient;
			await actClient.BindToAsync(rpcChannel, false, exporter.Proxy.BoundAuthContext?.AuthContext, exporter.Proxy.BoundAuthContext?.AuthLevel ?? RpcAuthLevel.None, cancellationToken).ConfigureAwait(false);

			// SCMActivator
			if (info.Version.MinorVersion >= 6)
			{
				var scmClient = new ScmActivatorClient(dcom);
				dcom._scmActivator = scmClient;
				await scmClient.BindToAsync(rpcChannel, false, exporter.Proxy.BoundAuthContext?.AuthContext, exporter.Proxy.BoundAuthContext?.AuthLevel ?? RpcAuthLevel.None, cancellationToken).ConfigureAwait(false);
			}

			return dcom;
		}

		#region Error stuff
		private static AsyncLocal<object?> _lastError = new AsyncLocal<object?>();
		internal static object? GetLastError() => _lastError.Value;
		internal static void ClearLastError() => _lastError.Value = null;
		internal static void SetLastError(object errorInfo)
		{
			_lastError.Value = errorInfo;
		}

		public static Hresult CheckHresultAndThrow(Hresult hr)
		{
			if ((int)hr < 0)
				throw GetExceptionFor(hr);
			return hr;
		}

		public static Exception GetExceptionFor(Hresult hr)
		{
			return hr.GetException();
		}
		#endregion

		/// <summary>
		/// Activates an object.
		/// </summary>
		/// <param name="clsid">Class ID of object</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>An <see cref="OleAutomationObject"/> representing the activated object</returns>
		public async Task<OleAutomationObject> Activate(
			Guid clsid,
			CancellationToken cancellationToken
			)
		{
			var obj = await this.Activate<IDispatch>(clsid, cancellationToken).ConfigureAwait(false);
			return new OleAutomationObject(obj, this);
		}
		/// <summary>
		/// Activates an object.
		/// </summary>
		/// <param name="clsid">Class ID of object</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <typeparam name="TInterface">Interface to query on activated object</typeparam>
		/// <returns>An <see cref="OleAutomationObject"/> representing the activated object</returns>
		public async Task<TInterface> Activate<TInterface>(
			Guid clsid,
			CancellationToken cancellationToken
			)
			where TInterface : class, IRpcObject
		{
			var iid = typeof(TInterface).GUID;
			if (iid == Guid.Empty)
				throw new ArgumentException("The interface type does not has a GUID associated with it.", nameof(TInterface));

			//var iid = RpcObjectProxy.GetProxyIid<TProxy>();

			ushort[] protseqs = new ushort[]
			{
				// DOD TCP
				7
			};

			ActivationResult result;
			this._callback?.OnActivatingObject(clsid, iid);
			try
			{
				if (this._scmActivator != null)
				{
					result = await this._scmActivator.CreateInstance(
						clsid,
						false,
						this.NegotiatedVersion,
						new Guid[] { iid, iid, iid, iid },
						protseqs,
						cancellationToken).ConfigureAwait(false);
				}
				else
				{
					result = await this._activator.CreateInstance(
						clsid,
						false,
						this.NegotiatedVersion,
						new Guid[] { iid },
						protseqs,
						cancellationToken).ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
				this._callback?.OnActivationFailed(clsid, iid, ex);
				throw;
			}

			this._callback?.OnActivatedObject(clsid, iid, result);

			var exporter = await GetExporterRecord(result, cancellationToken).ConfigureAwait(false);

			var proxy = await Unmarshal<TInterface>(
				iid,
				exporter,
				result.QueryInterfaceResults[0].Objref,
				cancellationToken).ConfigureAwait(false);
			return proxy;
		}

		#region Exporters
		private SemaphoreSlim _exporterLock = new SemaphoreSlim(1, 1);
		private Dictionary<ulong, ObjectExporterRecord> _exporters = new Dictionary<ulong, ObjectExporterRecord>();
		/// <summary>
		/// Gets the exporter for an activation.
		/// </summary>
		/// <param name="result">Activation result</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>An <see cref="ObjectExporterRecord"/> representing the exporter connection</returns>
		private async Task<ObjectExporterRecord> GetExporterRecord(ActivationResult result, CancellationToken cancellationToken)
		{
			var oxid = result.Oxid;
			if (this._exporters.TryGetValue(oxid, out var exporter))
				return exporter;

			await this._exporterLock.WaitAsync(cancellationToken).ConfigureAwait(false);
			try
			{
				if (this._exporters.TryGetValue(oxid, out exporter))
					return exporter;

				exporter = await this.ConnectExporter(result, cancellationToken).ConfigureAwait(false);
				return exporter;
			}
			finally
			{
				this._exporterLock.Release();
			}
		}

		private async Task<ObjectExporterRecord> ConnectExporter(
			ActivationResult result,
			CancellationToken cancellationToken
			)
		{
			bool connected = false;
			Exception? connectException = null;

			DualStringArray bindings = result.OxidBinding;
			var remUnk = new IRemUnknown2ClientProxy();
			remUnk.SetObjectInfo(
				result.IpidRemUnknown,
				result.ComVersion,
				this
				);

			RpcAuthLevel authLevel = (RpcAuthLevel)Math.Max(
				(int)result.AuthLevelHint,
				Math.Max(
					(int)RpcAuthLevel.PacketIntegrity,
					(int)this._rpcClient.DefaultAuthLevel
				));

			ServicePrincipalName? exporterSpn = null;
			bool isUntrustedSpn = false;
			foreach (var secbinding in bindings.SecurityBindings)
			{
				if (secbinding.AuthenticationService is RpcAuthType.Spnego or RpcAuthType.Kerberos)
				{
					isUntrustedSpn = true;
					// When the initial connection to the resolver used NTLM, the binding may be <domain>\<user>, even for Kerberos/Spnego
					if (ServicePrincipalName.TryParse(secbinding.PrincipalName, out var spn))
					{
						exporterSpn = spn;
						break;
					}
				}
			}

			StringBinding[]? stringBindings = result.OxidBinding.StringBindings;
			if (stringBindings != null)
			{
				if (!string.IsNullOrEmpty(this.PreferredHostName))
				{
					Array.Sort(stringBindings,
						(x, y) =>
							(IsPreferredHost(x.HostName) ? 0 : 1)
							- (IsPreferredHost(y.HostName) ? 0 : 1)
					);
				}

				foreach (var binding in stringBindings)
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (!binding.IsValid)
						continue;

					var ep = new DnsEndPoint(binding.HostName, binding.Port);
					try
					{
						this._callback?.OnConnectingToExporter(result.Oxid, binding);

						// TODO: This loses the "untrusted" bit
						await this._rpcClient.ConnectTcp(remUnk, ep, exporterSpn, authLevel, cancellationToken).ConfigureAwait(false);
						connected = true;
						break;
					}
					catch (Exception ex)
					{
						// TODO: Log exception
						connectException = ex;
					}
				}
			}

			if (!connected)
				throw new Exception("DCOM failed to connect to the object exporter binding.");

			var exporter = new ObjectExporterRecord
			{
				oxid = result.Oxid,
				channel = remUnk.Channel,
				bindings = bindings,
				ipidRemUnknown = result.IpidRemUnknown,
				authHint = result.AuthLevelHint,
				comVersion = result.ComVersion,
				remunk = remUnk,
				authLevel = authLevel,
			};

			this._exporters.Add(result.Oxid, exporter);

			return exporter;
		}

		class ObjectExporterRecord
		{
			internal ulong oxid;
			internal RpcClientChannel channel;
			internal DualStringArray bindings;
			internal Guid ipidRemUnknown;
			internal RpcAuthLevel authHint;
			internal COMVERSION comVersion;
			internal IRemUnknown2ClientProxy remunk;
			internal RpcAuthLevel authLevel;
		}
		#endregion

		public Task<TInterface> QueryInterface<TInterface>(
			IRpcObject proxy,
			CancellationToken cancellationToken
			)
			where TInterface : class, IRpcObject
		{
			if (proxy is null) throw new ArgumentNullException(nameof(proxy));
			if (proxy is not RpcObjectProxy objProxy)
				throw new ArgumentException("The RPC proxy is not supported by this DCOM client.", nameof(proxy));

			return this.QueryInterfaceImpl<TInterface>(objProxy, cancellationToken);
		}
		public async Task<TInterface> QueryInterfaceImpl<TInterface>(
			RpcObjectProxy proxy,
			CancellationToken cancellationToken
			)
			where TInterface : class, IRpcObject
		{
			if (proxy.Dcom != this)
				throw new ArgumentException("The object proxy belongs to another DcomClient.", nameof(proxy));

			var ipid = proxy.Ipid;
			var ipidEntry = this.TryGetIpid(ipid);
			if (ipidEntry == null)
				throw new ArgumentException("Invalid IPID", nameof(proxy));

			var oidEntry = ipidEntry.objEntry;

			// Check whether the proxy is already available
			var iid = typeof(TInterface).GUID;
			var iidEntry = oidEntry.TryGetByIid(iid);
			if (iidEntry != null)
			{
				var targetProxy = iidEntry.TryGetProxy();
				if (targetProxy != null)
					return (TInterface)targetProxy;
			}

			if (this._exporters.TryGetValue(ipidEntry.oxid, out var exporter))
			{
				RpcPointer<RpcPointer<REMQIRESULT[]>> ppQIResults = new RpcPointer<RpcPointer<REMQIRESULT[]>>();
				var res = (Win32ErrorCode)await exporter.remunk.RemQueryInterface(
					new RpcPointer<Guid>(proxy.Ipid),
					1,
					1,
					new Guid[] { iid },
					ppQIResults,
					cancellationToken
					).ConfigureAwait(false);
				res.CheckAndThrow();

				var qires0 = ppQIResults.value.value[0];
				DcomClient.CheckHresultAndThrow((Hresult)qires0.hResult);

				var objref = new Objref_Standard(iid, qires0.std, null);
				var typedProxy = await this.Unmarshal<TInterface>(iid, exporter, objref, cancellationToken).ConfigureAwait(false);
				return typedProxy;
			}
			else
			{
				// TODO: If the objref was obtained through other means, the exporter is not guaranteed to be there
				throw new NotImplementedException();
			}
		}

		public async Task<T> Unwrap<T>(byte[] marshalData, CancellationToken cancellationToken)
			where T : class, IRpcObject
		{
			var objref = Objref.Decode(marshalData);

			var iid = typeof(T).GUID;
			return await this.Unmarshal<T>(iid, null, objref, cancellationToken).ConfigureAwait(false);
		}

		private async Task<TInterface> Unmarshal<TInterface>(
			Guid iid,
			ObjectExporterRecord? exporter,
			Objref objref,
			CancellationToken cancellationToken
			)
			where TInterface : class, IRpcObject
		{
			if (objref is Objref_Custom custom)
			{
				return (TInterface)custom.GetObject();
			}
			else
			{
				Debug.Assert(objref.Iid != Guid.Empty);

				var ipidEntry = this.GetOrCreateIpidEntry(objref);

				if (ipidEntry.iid == iid)
				{
					{
						var existingProxy = ipidEntry.TryGetProxy();
						if (existingProxy != null)
							// Great, it already exists and is ready
							return (TInterface)existingProxy;
					}

					// It doesn't exist, so go about creating one
					var proxyType = RpcObjectProxy.GetProxyTypeFor<TInterface>();
					var typedProxy = (IRpcObjectProxy)Activator.CreateInstance(proxyType);
					var objProxy = typedProxy as RpcObjectProxy;
					if (objProxy is null)
						throw new NotSupportedException($"The proxy type '{typeof(TInterface).FullName}' is not supported by this DCOM client because it does not inherit RpcObjectProxy.");
					if (ipidEntry.BeginProxyInit(typedProxy))
					{
						if (exporter == null)
							exporter = this._exporters.TryGetValue(objref.Oxid);

						if (exporter == null)
							throw new NotImplementedException();

						// This thread won the race, so continue initialization
						objProxy.SetObjectInfo(
							objref.Ipid,
							exporter.comVersion,
							this
							);

						try
						{
							await objProxy.BindToAsync(
								exporter.channel,
								false,
								exporter.remunk.BoundAuthContext?.AuthContext,
								exporter.authLevel,
								//(RpcAuthLevel)result.AuthLevelHint,
								null,
								cancellationToken).ConfigureAwait(false);
							ipidEntry.CompleteInit(null);
							return (TInterface)typedProxy;
						}
						catch (Exception ex)
						{
							ipidEntry.CompleteInit(ex);
							throw;
						}
					}
					else
					{
						// Another thread won the race, so wait for it to finish
						return (TInterface)await ipidEntry.WaitProxy().ConfigureAwait(false);
					}
				}
				else
				{
					throw new NotSupportedException("The IID does not match.");
				}

				// TODO: If the IID doesn't match what the caller requested, use QueryInterface to cast the pointer
				throw new NotSupportedException();

				if (objref.Iid != iid)
				{
					// TODO: Call RemQueryInterface
				}
			}
		}

		#region Concurrency stuff
		enum EntryState
		{
			New = 0,
			Initializing = 1,
			Ready = 2,
		}
		interface IConcurrentInitHandler<TKey, TEntry>
			where TEntry : class
		{
			TEntry Create(TKey key);
			ref int SelectState(TEntry entry);
			void Initialize(TEntry entry, TKey key);
		}

		private delegate ref TField FieldSelector<TEntry, TField>(TEntry obj) where TEntry : class;
		private static TEntry GetOrCreateConcurrentEntry<TKey, TEntry, THandler>(
			ConcurrentDictionary<TKey, TEntry> entries,
			TKey key,
			THandler initHandler
			)
			where TEntry : class
			where THandler : IConcurrentInitHandler<TKey, TEntry>
		{
			var entry = entries.GetOrAdd(key, key => initHandler.Create(key));

			ref var stateField = ref initHandler.SelectState(entry);
			if (EntryState.New == (EntryState)Interlocked.CompareExchange(ref stateField, (int)EntryState.Initializing, (int)EntryState.New))
			{
				initHandler.Initialize(entry, key);
				stateField = (int)EntryState.Ready;
			}
			else
			{
				while ((EntryState)stateField != EntryState.Ready)
					;
			}

			return entry;
		}
		#endregion

		#region IPID table
		class IpidEntry
		{
			public IpidEntry(Guid ipid)
			{
				this.ipid = ipid;
				this.proxySource = new TaskCompletionSource<IRpcObjectProxy>();
				this.proxyTask = this.proxySource.Task;
			}

			internal volatile int entryState;

			internal readonly Guid ipid;
			internal Guid iid;
			internal ulong oid;
			internal ulong oxid;
			internal int publicRefCount;
			internal int privateRefCount;

			internal ObjectExporterRecord exporter;
			internal ObjectEntry objEntry;

			private TaskCompletionSource<IRpcObjectProxy> proxySource;
			private Task<IRpcObjectProxy> proxyTask;
			private volatile IRpcObjectProxy? proxy;
			private volatile int proxyInitState;

			internal IRpcObjectProxy? TryGetProxy() => ((EntryState)this.proxyInitState == EntryState.Ready) ? this.proxy : null;
			internal Task<IRpcObjectProxy> WaitProxy() => this.proxyTask;

			internal bool BeginProxyInit(IRpcObjectProxy proxy)
			{
				if (EntryState.New == (EntryState)Interlocked.CompareExchange(ref this.proxyInitState, (int)EntryState.Initializing, (int)EntryState.New))
				{
					this.proxy = proxy;
					return true;
				}
				else
					return false;
			}
			internal void CompleteInit(Exception? ex)
			{
				Debug.Assert(this.proxy != null);

				if (ex != null)
				{
					this.proxySource.SetException(ex);
				}
				else
				{
					this.proxySource.SetResult(this.proxy);
				}
				this.proxyInitState = (int)EntryState.Ready;
			}
		}

		private ConcurrentDictionary<Guid, IpidEntry> _ipidTable = new ConcurrentDictionary<Guid, IpidEntry>();
		struct IpidEntryInitHandler : IConcurrentInitHandler<Guid, IpidEntry>
		{
			private readonly Objref objref;
			private readonly DcomClient dcom;

			internal IpidEntryInitHandler(Objref objref, DcomClient dcom)
			{
				this.objref = objref;
				this.dcom = dcom;
			}

			public IpidEntry Create(Guid ipid)
			{
				return new IpidEntry(ipid)
				{
					iid = objref.Iid,
					oid = objref.Oid,
					oxid = objref.Oxid,
					publicRefCount = objref.PublicRefCount,
					objEntry = this.dcom.GetObjectEntry(objref.Oid),
				};
			}

			public void Initialize(IpidEntry entry, Guid key)
			{
				entry.objEntry.Add(entry);
			}

			public ref int SelectState(IpidEntry entry) => ref entry.entryState;
		}
		private IpidEntry GetOrCreateIpidEntry(Objref objref)
			=> GetOrCreateConcurrentEntry(this._ipidTable, objref.Ipid, new IpidEntryInitHandler(objref, this));
		private IpidEntry? TryGetIpid(Guid ipid)
		{
			if (this._ipidTable.TryGetValue(ipid, out var entry))
			{
				while ((EntryState)entry.entryState != EntryState.Ready)
					;
			}

			return entry;
		}
		#endregion

		#region OID table
		class ObjectEntry
		{
			internal ulong oid;

			private readonly ConcurrentDictionary<Guid, IpidEntry> ipidEntries = new ConcurrentDictionary<Guid, IpidEntry>();
			private readonly ConcurrentDictionary<Guid, IpidEntry> ipidEntriesByIid = new ConcurrentDictionary<Guid, IpidEntry>();

			public ObjectEntry(ulong oid)
			{
				this.oid = oid;
			}

			internal IpidEntry Add(IpidEntry entry)
			{
				Debug.Assert(entry != null);
				lock (this.ipidEntries)
				{
					var entry2 = this.ipidEntries.GetOrAdd(entry.ipid, entry);
					if (entry2 == entry)
					{
						this.ipidEntriesByIid.TryAdd(entry.iid, entry);
					}

					return entry2;
				}
			}

			internal IpidEntry? TryGetByIid(Guid iid)
			{
				this.ipidEntriesByIid.TryGetValue(iid, out var entry);
				return entry;
			}
		}
		private ConcurrentDictionary<ulong, ObjectEntry> _oidTable = new ConcurrentDictionary<ulong, ObjectEntry>();
		private ObjectEntry GetObjectEntry(ulong oid)
		{
			return this._oidTable.GetOrAdd(oid, new ObjectEntry(oid));
		}

		byte[] IObjrefMarshal.DecodeObjref<T>(IRpcDecoder decoder)
			=> DecodeObjref<T>(decoder);
		public static byte[] DecodeObjref<T>(IRpcDecoder decoder)
			where T : class, IRpcObject
		{
			if (decoder is null) throw new ArgumentNullException(nameof(decoder));

			var mptr = decoder.ReadConformantStruct<MInterfacePointer>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral(ref mptr);
			return mptr.abData;
		}

		void IObjrefMarshal.EncodeObjref<T>(IRpcEncoder encoder, TypedObjref<T>? objref)
		{
			if (encoder is null) throw new ArgumentNullException(nameof(encoder));

			// [MS-DCOM] § 2.2.16 - PMInterfacePoniter
			if (objref != null)
			{
				var mptr = objref.AsMInterfacePtr();
				encoder.WriteConformantStruct(mptr, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(mptr);
			}
		}

		public TypedObjref<T> Wrap<T>(T obj)
			where T : class, IRpcObject
		{
			if (obj is ICustomDcomMarshal marshal)
			{
				var objref = marshal.CreateObjref();
				var bytes = objref.ToByteArray();
				var tref = new TypedObjref<T>(bytes);

#if DEBUG
				var unwrapped = tref.Unwrap(this, CancellationToken.None).Result;
#endif

				return tref;
			}
			else
				throw new NotImplementedException();
		}
		#endregion

		/// <summary>
		/// Converts a string to its WMI RPC representation.
		/// </summary>
		/// <param name="str">String to convert</param>
		/// <returns>The RPC representation of <paramref name="str"/></returns>
		[return: NotNullIfNotNull(nameof(str))]
		public static RpcPointer<FLAGGED_WORD_BLOB>? MakeString(string? str)
		{
			if (str == null)
				return null;

			var cBytes = Encoding.Unicode.GetByteCount(str);
			Debug.Assert(0 == (cBytes % 2));
			//cBytes += 2;    // Null terminator
			ushort[] chars = new ushort[cBytes / 2];

			Encoding.Unicode.GetBytes(str, MemoryMarshal.Cast<ushort, byte>(chars));
			RpcPointer<FLAGGED_WORD_BLOB> blob = new RpcPointer<FLAGGED_WORD_BLOB>(new FLAGGED_WORD_BLOB
			{
				cBytes = (uint)cBytes,
				clSize = (uint)str.Length,
				asData = chars
			});

			return blob;
		}

	}

	static class DcomExtensions
	{
		internal static MInterfacePointer AsMInterfacePtr<T>(this TypedObjref<T> objref)
			where T : class, IRpcObject
		{
			var marshalData = objref.TryGetMarshalData();
			return new MInterfacePointer
			{
				ulCntData = (uint)marshalData.Length,
				abData = marshalData,
			};
		}
		internal static string? DecodeString(this RpcPointer<FLAGGED_WORD_BLOB>? bstrVal)
		{
			if (bstrVal == null || (int)bstrVal.value.clSize == -1)
				return null;

			StringBuilder sb = new StringBuilder((int)bstrVal.value.clSize);
			foreach (var w in bstrVal.value.asData)
			{
				sb.Append((char)w);
			}

			return sb.ToString();
		}
	}
}
