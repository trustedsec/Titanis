using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Smb2
{
	public class Smb2SessionOptions(bool mustEncryptData)
	{
		public bool MustEncryptData => mustEncryptData;
	}
}
