using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Msdrsr
{
	// [MS-DRSR] § 4.1.1.1.25 DIRERR Codes
	public enum DirerrCode
	{
		AttributeError = 1,
		NameError = 2,
		ReferralError = 3,
		SecurityError = 4,
		ServiceError = 5,
		UpdateError = 6,
		SystemError = 7,
	}
}
