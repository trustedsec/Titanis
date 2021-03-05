using ms_dtyp;
using ms_samr;
using System.Text;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mssamr
{
	public class SamSid
	{
		private RPC_SID sid;

		public SamSid(RPC_SID sid)
		{
			this.sid = sid;
		}

		public SecurityIdentifier ToSid()
			=> this.sid.ToSid();

		public long IdentifierAuthority
		{
			get
			{
				long n = 0;
				foreach (var b in this.sid.IdentifierAuthority.Value)
				{
					n <<= 8;
					n |= b;
				}
				return n;
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder("S-");
			sb.Append(this.sid.Revision)
				.Append('-')
				.Append(this.IdentifierAuthority);
			foreach (var subauth in this.sid.SubAuthority)
			{
				sb.Append('-').Append(subauth);
			}

			return sb.ToString();
		}
	}
}