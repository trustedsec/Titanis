using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Titanis.IO
{
	/// <summary>
	/// Exposes the position within an underlying <see cref="Stream"/>.
	/// </summary>
	public interface IHaveStreamPosition
	{
		/// <summary>
		/// Gets the position of the underlying <see cref="Stream"/>.
		/// </summary>
		/// <remarks>
		/// In cases where the object using the stream supports buffering,
		/// the value may not match the value of <see cref="Stream.Position"/>.
		/// </remarks>
		long StreamPosition { get; }
	}
}
