using System.Runtime.InteropServices;
using System;
using System.Text;

namespace Titanis.Winterop
{
	/// <summary>
	/// Exposes functionality for retrieving the short name of an object.
	/// </summary>
	/// <seealso cref="ShortNameHelper"/>
	public interface IHaveShortName
	{
		/// <summary>
		/// Gets a reference to the first byte of the short name.
		/// </summary>
		/// <returns></returns>
		ref byte GetShortNameRef();
		/// <summary>
		/// Gets the length of the short name, in bytes.
		/// </summary>
		int ShortNameLength { get; }
		/// <summary>
		/// Gets the encoding of the short name.
		/// </summary>
		Encoding ShortNameEncoding { get; }
	}

	/// <summary>
	/// Implements helper methods for <see cref="IHaveShortName"/>.
	/// </summary>
	/// <remarks>
	/// <see cref="IHaveShortName"/> is intended for value types that cannot use polymorphism.
	/// This class provides the implementation that would normally be provided by a base class.
	/// <c>ref readonly</c> with a generic type cannot be used as an extension method.
	/// </remarks>
	public static class ShortNameHelper
	{
		/// <summary>
		/// Gets the short name from a structure.
		/// </summary>
		/// <typeparam name="TStruc">Structure implementing <see cref="IHaveShortName"/></typeparam>
		/// <param name="struc">Structure containing name</param>
		/// <returns>The short name</returns>
		public static string? GetShortName<TStruc>(ref readonly TStruc struc)
			where TStruc : struct, IHaveShortName
		{
			if (struc.ShortNameLength > 0)
			{
				ReadOnlySpan<byte> shortNameBytes = MemoryMarshal.CreateReadOnlySpan(ref struc.GetShortNameRef(), struc.ShortNameLength);
				return struc.ShortNameEncoding.GetString(shortNameBytes);
			}
			else
				return null;
		}
	}
}
