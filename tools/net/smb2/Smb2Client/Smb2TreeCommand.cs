using System.ComponentModel;
using System.Threading;
using Titanis.Cli;

namespace Titanis.Smb2.Cli
{
	/// <summary>
	/// Base class from SMB2 tree commands
	/// </summary>
	/// <remarks>
	/// A tree command operates on a share.  <see cref="ValidateParameters"/>
	/// is extended to ensure that <see cref="Smb2CommandBase.UncPath"/>
	/// specifies a share name.
	/// </remarks>
	abstract class Smb2TreeCommand : Smb2CommandBase
	{
		[Parameter]
		[Category(ParameterCategories.Connection)]
		[Description("Encrypts PDUs for the target share")]
		public SwitchParam EncryptShare { get; set; }

		protected override void ValidateParameters(ParameterValidationContext context)
		{
			base.ValidateParameters(context);

			if (string.IsNullOrEmpty(this.ShareName))
				context.LogError(nameof(ShareName), "The UNC path must include a share name.");
		}
	}
}
