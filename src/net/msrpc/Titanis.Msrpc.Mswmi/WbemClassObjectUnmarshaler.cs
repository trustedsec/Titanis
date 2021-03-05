using ms_wmi;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Transactions;
using Titanis.IO;
using Titanis.Msrpc.Msdcom;

namespace Titanis.Msrpc.Mswmi
{
	/// <summary>
	/// Implements functionality to unmarshal WMI objects.
	/// </summary>
	/// <remarks>
	/// <see cref="WmiClient"/> registers this class with <see cref="DcomClient"/> so
	/// that WMI objects are automatically unmarshaled.
	/// </remarks>
	public class WbemClassObjectUnmarshaler : IUnmarshaler
	{
		public static readonly Guid Clsid = new Guid("4590F812-1D3A-11D0-891F-00AA004B2E24");

		/// <inheritdoc/>
		public Objref Unmarshal(ByteMemoryReader reader)
		{
			WmiObject obj = reader.ReadEncodingUnit();
			return new WmiObjref(obj);
		}
	}

	// [MS-WMIO] § 2.2.5 - ObjectFlags
	public enum WmiObjectFlags : byte
	{
		None = 0,
		CimClass = 1,
		CimInstance = 2,
		HasDecoration = 4,
		Prototype = 0x10,
		MissingKeys = 0x40,

		ObjectTypeMask = CimClass | CimInstance
	}

	/// <summary>
	/// Describes the WMI decoration.
	/// </summary>
	// [MS-WMIO] § 2.2.7 - Decoration
	public class WmiDecoration
	{
		/// <summary>
		/// Initializes a new <see cref="WmiDecoration"/>.
		/// </summary>
		/// <param name="serverName">Server name</param>
		/// <param name="ns">Namespace</param>
		public WmiDecoration(string serverName, string ns)
		{
			this.ServerName = serverName;
			this.NamespaceName = ns;
		}
		/// <summary>
		/// Gets the server name.
		/// </summary>
		public string ServerName { get; }
		/// <summary>
		/// Gets the namespace.
		/// </summary>
		public string NamespaceName { get; }
	}
}
