using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Winterop.Security
{
	public enum SecurityObjectType
	{
		File = 0,
		Directory,
		RegistryKey,
		SamServer,
		SamDomain,
		SamGroup,
		SamAlias,
		SamUserAccount,
		DirectoryObject,
		Scm,
		Service,
	}
}
