using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop;

namespace Titanis.Msrpc.Msdcom
{
	/// <summary>
	/// Throw when an error is returned from an OLE Automation call.
	/// </summary>
	[Serializable]
	public class AutomationException : Exception, IHaveErrorCode
	{
		public AutomationException(
			Hresult hr,
			int appSpecificErrorCode,
			string? source,
			string? description,
			string? helpFile,
			uint helpContextId
			)
			: base(BuildMessage(appSpecificErrorCode, hr, description, source))
		{
			this.HResult = (int)hr;
			this.AppSpecificErrorCode = appSpecificErrorCode;
			this.ErrorSource = source;
			this.Description = description;
			this.HelpFile = helpFile;
			this.HelpContextId = helpContextId;
		}

		private static string? BuildMessage(int appSpecificErrorCode, Hresult hr, string? description, string? source)
			=> $"The automation call failed: {(appSpecificErrorCode == 0 ? "0x" + ((uint)hr).ToString("X8") : appSpecificErrorCode)}: {description ?? "<no description provided>"}";

		/// <summary>
		/// Initializes a new <see cref="AutomationException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected AutomationException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			this.HResult = info.GetInt32(nameof(HResult));
			this.AppSpecificErrorCode = info.GetInt32(nameof(AppSpecificErrorCode));
			this.ErrorSource = info.GetString(nameof(ErrorSource));
			this.Description = info.GetString(nameof(Description));
			this.HelpFile = info.GetString(nameof(HelpFile));
			this.HelpContextId = info.GetUInt32(nameof(HelpContextId));
		}

		/// <summary>
		/// Gets the application-specific error code (if <see cref="Hresult"/> is <c>0</c>).
		/// </summary>
		public int AppSpecificErrorCode { get; }
		/// <summary>
		/// Gets the programmatic source of the error.
		/// </summary>
		public string? ErrorSource { get; }
		/// <summary>
		/// Gets a textual description of the error.
		/// </summary>
		public string? Description { get; }
		/// <summary>
		/// Gets the help file that describes the error.
		/// </summary>
		public string? HelpFile { get; }
		/// <summary>
		/// Gets the help context ID.
		/// </summary>
		public uint HelpContextId { get; }

		/// <inheritdoc/>
		int IHaveErrorCode.ErrorCode => this.AppSpecificErrorCode;

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(HResult), (int)this.HResult);
			info.AddValue(nameof(AppSpecificErrorCode), this.AppSpecificErrorCode);
			info.AddValue(nameof(ErrorSource), this.ErrorSource);
			info.AddValue(nameof(Description), this.Description);
			info.AddValue(nameof(HelpFile), this.HelpFile);
			info.AddValue(nameof(HelpContextId), this.HelpContextId);
			base.GetObjectData(info, context);
		}
	}
}
