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
	/// Thrown a member name cannot be resolved on an OLE automation object.
	/// </summary>
	[Serializable]
	public class UnknownMemberException : Exception
	{
		/// <summary>
		/// Initializes a new <see cref="UnknownMemberException"/>.
		/// </summary>
		/// <param name="memberName"></param>
		public UnknownMemberException(string memberName) : base($"The automation object does not contain a member named '{memberName}'")
		{
			this.HResult = unchecked((int)Hresult.DISP_E_MEMBERNOTFOUND);
			this.MemberName = memberName;
		}

		/// <summary>
		/// Initializes a new <see cref="UnknownMemberException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected UnknownMemberException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			this.MemberName = info.GetString(nameof(MemberName));
		}

		/// <summary>
		/// Gets the name of the unknown member.
		/// </summary>
		public string MemberName { get; }

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(MemberName), this.MemberName);
			base.GetObjectData(info, context);
		}
	}
}
