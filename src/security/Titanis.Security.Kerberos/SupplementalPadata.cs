using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos
{
	public enum SupplementalPadataType
	{
		AsrepKey = 1,
		TicketKey = 2,
		TicketComment=3,
	}

	internal class SupplementalPadata
	{
		public const uint Signature = (uint)'T' | ((uint)'S' << 8) | ((uint)'P' << 16) | ((uint)'A' << 24);
	}
}
