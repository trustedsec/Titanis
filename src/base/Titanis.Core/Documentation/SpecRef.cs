using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Documentation
{
	/// <summary>
	/// Specifies a reference to a specification
	/// </summary>
	/// <remarks>
	/// When referencing a specification, try to be as specific as possible.
	/// If multiple versions of a specification exist, specify the version
	/// pertaining to the implementation.  Consider breaking a long block
	/// of code covering multiple sections of a specification into separate
	/// methods that may each be marked with a specific section.
	/// <para>
	/// In addition to marking individual members with a specification
	/// reference, consider marking the assembly with a list of all specification
	/// implemented within the assembly.  This could provide a central location
	/// to specify unchanging information about a specification, such as URL,
	/// rather that duplicating this for each specific reference.
	/// </para>
	/// The attribute may be applied to any type of target.
	/// </remarks>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	public class SpecRefAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new <see cref="SpecRefAttribute"/>
		/// </summary>
		/// <param name="specName">Name of specification</param>
		public SpecRefAttribute(string specName)
		{
			this.SpecName = specName;
		}

		/// <summary>
		/// Gets the name of the specification.
		/// </summary>
		public string SpecName { get; }
		/// <summary>
		/// Gets or sets the title of the specification.
		/// </summary>
		public string? Title { get; set; }
		/// <summary>
		/// Version of the specification implemnted.
		/// </summary>
		public string? Version { get; set; }
		/// <summary>
		/// Gets or sets the type of specification.
		/// </summary>
		/// <seealso cref="SpecTypes"/>
		public string? SpecType { get; set; }
		/// <summary>
		/// Gets or sets the URL where the specification is located.
		/// </summary>
		public string? Url { get; set; }
		/// <summary>
		/// Gets or sets the section of the specification pertaining to the implementation.
		/// </summary>
		public string? Section { get; set; }
	}
}
