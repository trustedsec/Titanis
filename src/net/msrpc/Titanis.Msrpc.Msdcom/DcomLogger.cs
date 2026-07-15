using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Msdcom
{
	[CallbackLogger]
	public class DcomLogger : IDcomCallback
	{
		public DcomLogger(ILog log, IDcomCallback? chainedCallback = null)
		{
			ArgumentNullException.ThrowIfNull(log);
			this._log = log;
			this._chainedCallback = chainedCallback;
		}

		private readonly ILog _log;
		private readonly IDcomCallback? _chainedCallback;

		void IDcomCallback.OnDcomConnected(ObjectExporterServerInfo info)
		{
			Guid correlationId = Guid.NewGuid();
			this._log.WriteDcomClientConnectedMessage(correlationId, info.Version.MajorVersion, info.Version.MinorVersion);
			PrintBindings(correlationId, info.Bindings);

			this._chainedCallback?.OnDcomConnected(info);
		}

		private void PrintBindings(Guid correlationId, DualStringArray bindings)
		{
			foreach (var binding in bindings.StringBindings)
			{
				this._log.WriteDcomClientBindingInfoMessage(correlationId, binding.TowerId, binding.HostName, binding.NetworkAddress, binding.Port);
			}
			foreach (var binding in bindings.SecurityBindings)
			{
				this._log.WriteDcomClientSecurityBindingInfoMessage(correlationId, binding.AuthenticationService, (int)binding.AuthenticationService, binding.PrincipalName);
			}
		}

		void IDcomCallback.OnActivatingObject(Guid correlationId, Guid clsid, Guid iid)
		{
			this._log.WriteDcomClientActivatingObjectMessage(correlationId, clsid, iid);

			this._chainedCallback?.OnActivatingObject(correlationId, clsid, iid);
		}

		void IDcomCallback.OnActivatedObject(Guid correlationId, Guid clsid, Guid iid, ActivationResult result)
		{
			this._log.WriteDcomClientActivatedObjectMessage(correlationId, clsid, iid, result.IpidRemUnknown, result.Oxid, result.AuthLevelHint, (int)result.AuthLevelHint);
			PrintBindings(correlationId, result.OxidBinding);

			this._chainedCallback?.OnActivatedObject(correlationId, clsid, iid, result);
		}

		void IDcomCallback.OnActivationFailed(Guid correlationId, Guid clsid, Guid iid, Exception ex)
		{
			this._log.WriteDcomClientActivationFailedMessage(correlationId, clsid, iid, (uint)ex.HResult, ex.Message);

			this._chainedCallback?.OnActivationFailed(correlationId, clsid, iid, ex);
		}

		void IDcomCallback.OnConnectingToExporter(Guid correlationId, ulong oxid, StringBinding binding)
		{
			this._log.WriteDcomClientConnectingToExporterMessage(correlationId, oxid, binding.HostName, binding.Port);

			this._chainedCallback?.OnConnectingToExporter(correlationId, oxid, binding);
		}

		void IDcomCallback.OnExporterConnectionFailed(Guid correlationId, ulong oxid, StringBinding binding, Exception ex)
		{
			this._log.WriteDcomClientConnectToExporterFailedMessage(correlationId, oxid, binding.HostName, binding.Port, (uint)ex.HResult, ex.Message, ex.ToString());

			this._chainedCallback?.OnExporterConnectionFailed(correlationId, oxid, binding, ex);
		}
	}
}
