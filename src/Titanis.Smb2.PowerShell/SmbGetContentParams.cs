using Microsoft.PowerShell.Commands;
using System.Management.Automation;

namespace Titanis.Smb2.PowerShell
{
	internal class SmbGetContentParams:FileSystemContentReaderDynamicParameters
	{
		const string TypeSetName = "Type";

		[Parameter(ParameterSetName = TypeSetName)]
		public SwitchParameter Raw { get; set; }
	}
}