using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Msrpc.Msdcom
{
	/// <summary>
	/// Provides functionality to unmarshal an object returned from a DCOM call.
	/// </summary>
	/// <remarks>
	/// Register an implementation of this interface with <see cref="Objref.RegisterUnmarshaler(Guid, IUnmarshaler)"/>.
	/// </remarks>
	public interface IUnmarshaler
	{
		/// <summary>
		/// Reads OBJREF data and constructs an object.
		/// </summary>
		/// <param name="reader">Reader containing object data</param>
		/// <returns>The unmarshaled <see cref="Objref"/></returns>
		Objref Unmarshal(ByteMemoryReader reader);
	}
}
