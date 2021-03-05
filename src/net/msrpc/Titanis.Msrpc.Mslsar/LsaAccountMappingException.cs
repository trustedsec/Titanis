using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop;

namespace Titanis.Msrpc.Mslsar
{
	/// <summary>
	/// Thrown when the LSA cannot map between an account name and SID.
	/// </summary>
	/// <remarks>
	/// The <see cref="Mappings"/> property returns an array of mappings with <see langword="null"/> entries for failed mappings.
	/// </remarks>
	public class LsaAccountMappingException : Exception
	{
		public LsaAccountMappingException(LsaAccountMapping?[] mappings)
		{
			this.HResult = (int)Ntstatus.STATUS_SOME_NOT_MAPPED;
			ArgumentNullException.ThrowIfNull(mappings);
			Mappings = mappings;
		}

		/// <summary>
		/// Gets the successful mappings.
		/// </summary>
		/// <remarks>
		/// The array contains <see langword="null"/> entries for failed mappings.
		/// </remarks>
		public LsaAccountMapping?[] Mappings { get; }
	}
}
