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
	/// Thrown when an OLE automation call fails due to a bad argument.
	/// </summary>
	[Serializable]
	public class DispatchArgumentException : Exception
	{
		/// <summary>
		/// Initializes a new <see cref="DispatchArgumentException"/>.
		/// </summary>
		/// <param name="badArgumentIndex">Index of argument that caused the error</param>
		/// <param name="message">Message describing the error</param>
		public DispatchArgumentException(int badArgumentIndex, string message) : base(message)
		{
			this.HResult = unchecked((int)Hresult.E_INVALIDARG);
			this.BadArgumentIndex = badArgumentIndex;
		}

		/// <summary>
		/// Initializes a new <see cref="DispatchArgumentException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected DispatchArgumentException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			this.BadArgumentIndex = info.GetInt32(nameof(BadArgumentIndex));
		}

		/// <summary>
		/// Gets the index of the argument that caused the error.
		/// </summary>
		public int BadArgumentIndex { get; }

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(BadArgumentIndex), this.BadArgumentIndex);
			base.GetObjectData(info, context);
		}
	}
}
