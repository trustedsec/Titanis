using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Msdcom
{
	public enum DcomEventId
	{
		Other = 0,
		Connected = 1,
		BindingInfo = 2,
		SecurityBindingInfo = 3,
		ActivatingObject = 4,
		ActivatedObject = 5,
		ActivationFailed = 6,
		ConnectingToExporter = 7,
	}

	public class DcomLogger : IDcomCallback
	{
		public DcomLogger(ILog log)
		{
			ArgumentNullException.ThrowIfNull(log);
			this._log = log;
		}

		private readonly ILog _log;

		private static readonly string DcomSourceName = typeof(DcomClient).FullName!;


		private static readonly LogMessageType DcomConnected = new LogMessageType(LogMessageSeverity.Diagnostic, DcomSourceName, (int)DcomEventId.Connected, "Connected to DCOM with COM version {0}.{1}.", "versionMajor", "versionMinor");
		private static readonly LogMessageType BindingInfo = new LogMessageType(LogMessageSeverity.Diagnostic, DcomSourceName, (int)DcomEventId.BindingInfo, "  Binding: TowerId={0}, HostName='{1}', NetworkAddress='{2}', Port={3}", "towerId", "hostName", "networkAddress", "port");
		private static readonly LogMessageType SecurityBindingInfo = new LogMessageType(LogMessageSeverity.Diagnostic, DcomSourceName, (int)DcomEventId.SecurityBindingInfo, "  Security binding: AuthService={0} ({1}), Principal={2}", "authService", "authServiceValue", "principal");
		private static readonly LogMessageType ActivatingObject = new LogMessageType(LogMessageSeverity.Diagnostic, DcomSourceName, (int)DcomEventId.ActivatingObject, "Activating object with CLSID {{{0}}} requesting IID {{{1}}}", "clsid", "iid");
		private static readonly LogMessageType ActivatedObject = new LogMessageType(LogMessageSeverity.Diagnostic, DcomSourceName, (int)DcomEventId.ActivatedObject, "Activated object with CLSID {{{0}}} requesting IID {{{1}}}: IPID={{{3}}}, OXID={3:X8}, auth level hint={4} ({5})", "clsid", "iid", "ipid", "oxid", "authLevelHint", "authLevelHintValue");
		private static readonly LogMessageType ActivationFailed = new LogMessageType(LogMessageSeverity.Error, DcomSourceName, (int)DcomEventId.ActivationFailed, "Activation failed for CLSID {{{0}}} requesting IID {{{1}}} with error code {2} (0x{2:X8}): {3}", "clsid", "iid", "hres", "message");
		private static readonly LogMessageType ConnectingToExporter = new LogMessageType(LogMessageSeverity.Diagnostic, DcomSourceName, (int)DcomEventId.ConnectingToExporter, "Connecting to exporter OXID={0:X8} on {1}:{2}", "oxid", "address", "port");

		private void Write(LogMessageType messageType, params object[] parameters)
		{
			this._log.WriteMessage(messageType.Create(parameters));
		}

		void IDcomCallback.OnDcomConnected(ObjectExporterServerInfo info)
		{
			this.Write(DcomConnected, info.Version.MajorVersion, info.Version.MinorVersion);
			PrintBindings(info.Bindings);
		}

		private void PrintBindings(DualStringArray bindings)
		{
			foreach (var binding in bindings.StringBindings)
			{
				this.Write(BindingInfo, binding.TowerId, binding.HostName, binding.NetworkAddress, binding.Port);
			}
			foreach (var binding in bindings.SecurityBindings)
			{
				this.Write(SecurityBindingInfo, binding.AuthenticationService, (int)binding.AuthenticationService, binding.PrincipalName);
			}
		}

		void IDcomCallback.OnActivating(Guid clsid, Guid iid)
		{
			this.Write(ActivatingObject, clsid, iid);
		}

		void IDcomCallback.OnActivatedObject(Guid clsid, Guid iid, ActivationResult result)
		{
			this.Write(ActivatedObject, clsid, iid, result.IpidRemUnknown, result.Oxid, result.AuthLevelHint, (int)result.AuthLevelHint);
			PrintBindings(result.OxidBinding);
		}

		void IDcomCallback.OnActivationFailed(Guid clsid, Guid iid, Exception ex)
		{
			this.Write(ActivationFailed, clsid, iid, ex.HResult, ex.Message);
		}

		void IDcomCallback.OnConnectingToExporter(ulong oxid, StringBinding binding)
		{
			this.Write(ConnectingToExporter, oxid, binding.HostName, binding.Port);
		}
	}
}
