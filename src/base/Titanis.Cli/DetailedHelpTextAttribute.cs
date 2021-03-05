using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies the position of the documentation for a class relative to documentation on a base class.
	/// </summary>
	public enum DocumentationPlacement
	{
		/// <summary>
		/// Base class documentation is replaced.
		/// </summary>
		ReplacesBase = 0,
		/// <summary>
		/// Documentation for this class preceeds documentation for the base class.
		/// </summary>
		BeforeBase,
		/// <summary>
		/// Documentation for this class follows documentation for the base class.
		/// </summary>
		AfterBase,
	}

	/// <summary>
	/// Specifies detailed help for a command.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class DetailedHelpTextAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new <see cref="DetailedHelpTextAttribute"/>.
		/// </summary>
		/// <param name="text"></param>
		public DetailedHelpTextAttribute(string text)
		{
			this._text = text;
		}

		protected DetailedHelpTextAttribute() { }

		private string _text;

		/// <summary>
		/// Gets the detailed help text.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public virtual string GetText(CommandMetadataContext context) => this._text;

		/// <summary>
		/// Gets a <see cref="DocumentationPlacement"/> value specifying the placement of the documentation
		/// relative to the documentation for the base class.
		/// </summary>
		public DocumentationPlacement Placement { get; set; }
	}
}
