using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.DceRpc
{
	/// <summary>
	/// Exposes functionality to encode and decode a conformant struct.
	/// </summary>
	public interface IRpcConformantStruct : IRpcStruct
	{
		/// <summary>
		/// Encodes the conformant struct header.
		/// </summary>
		/// <param name="encoder"><see cref="IRpcEncoder"/> to contain data</param>
		void EncodeHeader(IRpcEncoder encoder);
		/// <summary>
		/// Encodes the conformant array field and the end of the struct.
		/// </summary>
		/// <param name="encoder"><see cref="IRpcEncoder"/> to contain data</param>
		void EncodeConformantArrayField(IRpcEncoder encoder);
		/// <summary>
		/// Decodes the conformant struct header.
		/// </summary>
		/// <param name="decoder"><see cref="IRpcDecoder"/> containing data</param>
		void DecodeHeader(IRpcDecoder decoder);
		/// <summary>
		/// Decodes the conformant array field and the end of the struct.
		/// </summary>
		/// <param name="decoder"><see cref="IRpcDecoder"/> containing data</param>
		void DecodeConformantArrayField(IRpcDecoder decoder);
	}
}
