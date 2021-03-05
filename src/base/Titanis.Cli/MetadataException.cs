using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Thrown when a <see cref="Command"/> class has metadata problems.
	/// </summary>
	[Serializable]
	public class MetadataException : Exception
	{
		/// <summary>
		/// Gets the name of the member with a metadata problem.
		/// </summary>
		public string? PropertyName { get; }

		/// <summary>
		/// Initializes a new <see cref="MetadataException"/>.
		/// </summary>
		/// <param name="message">Message that describes the error</param>
		/// <param name="propertyName">Name of property with metadata problem</param>
		public MetadataException(string message, string propertyName, Exception? innerException = null)
			: base(message, innerException)
		{
			this.PropertyName = propertyName;
		}

		/// <summary>
		/// Initializes a new <see cref="MetadataException"/> with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information</param>
		protected MetadataException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			this.PropertyName = info.GetString(nameof(PropertyName));
		}

		/// <inheritdoc/>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(PropertyName), this.PropertyName);
			base.GetObjectData(info, context);
		}
	}
}
