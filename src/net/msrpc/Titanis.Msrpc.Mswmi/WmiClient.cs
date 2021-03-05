using ms_oaut;
using ms_wmi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.Asn1.Metadata;
using Titanis.DceRpc;
using Titanis.Msrpc.Msdcom;
using Titanis.Security;
using Titanis.Winterop;

namespace Titanis.Msrpc.Mswmi
{
	/// <summary>
	/// Represents a connection to the WMI service.
	/// </summary>
	/// <remarks>
	/// Use <see cref="ConnectTo(string, int, DcomClient, CancellationToken)"/>
	/// to establish a connection to a WMI service.  Once connected, call
	/// <see cref="OpenNamespace(string, string, CancellationToken)"/> to connect
	/// to a namespace.
	/// <para>
	/// This class registers one or more DCOM handlers in its static constructor.
	/// This is invoked automatically if you use this class.  If you are not
	/// directly using this class (e.g. for testing or offline serialization),
	/// call <see cref="Initialize"/> to ensure these handlers are registered.
	/// </para>
	/// </remarks>
	public class WmiClient
	{
		static WmiClient()
		{
			Objref.RegisterUnmarshaler(WbemClassObjectUnmarshaler.Clsid, new WbemClassObjectUnmarshaler());
		}

		private WmiClient(DcomClient dcomClient, IWbemLevel1Login login)
		{
			this.dcomClient = dcomClient;
			this._login = login;
		}

		/// <summary>
		/// Ensures WMI client infrastructure components are registered.
		/// </summary>
		public static void Initialize()
		{

		}

		private static readonly Guid clsidLogin = new Guid("8BC3F05E-D86B-11d0-A075-00C04FB68820");
		internal readonly DcomClient dcomClient;
		private IWbemLevel1Login _login;

		/// <summary>
		/// Creates an exception for a <see cref="WBEMSTATUS"/> value.
		/// </summary>
		/// <param name="statusCode">Status code</param>
		/// <returns>An <see cref="Exception"/> representing <paramref name="statusCode"/></returns>
		public static Exception GetExceptionFor(WBEMSTATUS statusCode)
		{
			return new WmiException(statusCode);
		}

		/// <summary>
		/// Connects to a WMI service.
		/// </summary>
		/// <param name="clientName">Name of client machine</param>
		/// <param name="clientProcessId">Client process ID</param>
		/// <param name="dcomClient">DCOM client</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="clientName"/> is <see langword="null"/> or empty.</exception>
		/// <remarks>
		/// <paramref name="clientName"/> and <paramref name="clientProcessId"/>
		/// are provided to the WMI service as-is and are not checked or used by this implementation.
		/// </remarks>
		public static async Task<WmiClient> ConnectTo(
			string clientName,
			int clientProcessId,
			DcomClient dcomClient,
			CancellationToken cancellationToken)
		{
			var id = await dcomClient.Activate<IWbemLoginClientID>(clsidLogin, cancellationToken).ConfigureAwait(false);

			await id.SetClientInfo(clientName, clientProcessId, 0, cancellationToken).ConfigureAwait(false);

			var login = await dcomClient.QueryInterface<IWbemLevel1Login>(id, cancellationToken).ConfigureAwait(false);
			Titanis.DceRpc.RpcPointer<uint> pLocale = new Titanis.DceRpc.RpcPointer<uint>();
			var hr = (WBEMSTATUS)await login.EstablishPosition(null, 0, pLocale, cancellationToken).ConfigureAwait(false);
			CheckHResult(hr);

			return new WmiClient(dcomClient, login);
		}

		/// <summary>
		/// Name of the <c>root\cimv2</c> namespace.
		/// </summary>
		public const string RootCimV2Namespace = @"root\cimv2";

		/// <summary>
		/// Connects to a WMI namespace
		/// </summary>
		/// <param name="path">Namespace path</param>
		/// <param name="locale">Locale</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="WmiScope"/> representing the namespace</returns>
		/// <exception cref="ArgumentException"><paramref name="path"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="ArgumentException"><paramref name="locale"/> is <see langword="null"/> or empty.</exception>
		/// <example>
		/// <code
		/// >wmiConnection.OpenNamespace("root\cimv2", "en-US", cancellationToken);
		/// </code>
		/// </example>
		public async Task<WmiScope> OpenNamespace(
			string path,
			string locale,
			CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));
			if (string.IsNullOrEmpty(locale)) throw new ArgumentException($"'{nameof(locale)}' cannot be null or empty.", nameof(locale));

			RpcPointer<TypedObjref<IWbemServices>> ppNamespace = new RpcPointer<TypedObjref<IWbemServices>>();
			var hr = (WBEMSTATUS)await this._login.NTLMLogin(path, locale, 0, null, ppNamespace, cancellationToken).ConfigureAwait(false);
			CheckHResult(hr);

			var ns = await ppNamespace.value.Unwrap(this.dcomClient, cancellationToken).ConfigureAwait(false);
			return new WmiScope(this, ns);
		}

		#region Backup and Restore
		public async Task Backup(string backupFileName, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(backupFileName);
			var backupObj = await dcomClient.Activate<IWbemBackupRestore>(clsidBackupRestore, cancellationToken).ConfigureAwait(false);
			var hres = (Hresult)await backupObj.Backup(backupFileName, 0, cancellationToken).ConfigureAwait(false);
			hres.CheckAndThrow();
		}

		private static readonly Guid clsidBackupRestore = new Guid("c49e32c6-bc8b-11d2-85d4-00105a1f8304");
		public async Task Restore(string backupFileName, WmiRestoreOptions options, CancellationToken cancellationToken)
		{
			ArgumentException.ThrowIfNullOrEmpty(backupFileName);
			var backupObj = await dcomClient.Activate<IWbemBackupRestore>(clsidBackupRestore, cancellationToken).ConfigureAwait(false);
			var hres = (Hresult)await backupObj.Restore(backupFileName, (int)options, cancellationToken).ConfigureAwait(false);
			hres.CheckAndThrow();
		}
		#endregion

		internal static void CheckHResult(WBEMSTATUS res)
		{
			if ((int)res < 0)
				throw WmiClient.GetExceptionFor(res);
		}
	}

	[Flags]
	public enum WmiRestoreOptions : int
	{
		None = 0,

		ForceShutdown = 1,
	}
}
