using ms_lsar;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Mslsar
{
	public class AuditLogInfo
	{
		private POLICY_AUDIT_LOG_INFO _struc;

		internal AuditLogInfo(POLICY_AUDIT_LOG_INFO struc)
		{
			this._struc = struc;
		}

		public int PercentFull => (int)this._struc.AuditLogPercentFull;
		public uint MaxLogSize => this._struc.MaximumLogSize;
	}
}