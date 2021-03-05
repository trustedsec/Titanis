using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies the type of record that a command writes to output.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class OutputRecordTypeAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new <see cref="OutputRecordTypeAttribute"/>.
		/// </summary>
		/// <param name="recordType">Type of output record</param>
		public OutputRecordTypeAttribute(Type recordType)
		{
			this.RecordType = recordType;
		}

		/// <summary>
		/// Gets the type of output record.
		/// </summary>
		public Type RecordType { get; }

		public OutputStyle DefaultOutputStyle { get; set; } = OutputStyle.Table;
		public string[]? DefaultFields { get; set; }
	}
}
