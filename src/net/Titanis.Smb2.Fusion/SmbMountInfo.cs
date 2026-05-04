using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Linterop.Fuse;

namespace Titanis.Smb2.Fusion
{
	internal class SmbMountInfo
	{
		internal Smb2Client smbClient;
		internal uint uid;
		internal uint gid;
		internal Smb2FileCreateOptions createOptions;

		internal PosixFileMode defaultDirAccess = PosixFileMode.DefaultDirAccess;
		internal PosixFileMode defaultFileAccess = PosixFileMode.DefaultDirAccess;
		internal Smb2FileCreateOptions extraCreateOptions;
	}
}
