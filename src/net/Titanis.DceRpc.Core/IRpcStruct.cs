using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Provides functionality to encode and decode a structure.
	/// </summary>
	public interface IRpcStruct
	{
		/// <summary>
		/// Encodes the struct.
		/// </summary>
		/// <param name="encoder">Encoder to receive struct</param>
		void Encode(IRpcEncoder encoder);
		void EncodeDeferrals(IRpcEncoder encoder);

		void Decode(IRpcDecoder decoder);
		void DecodeDeferrals(IRpcDecoder decoder);
	}
}
