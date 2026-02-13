using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Asn1.Serialization
{
	public class Asn1UnexpectedTagException : InvalidOperationException
	{
		public Asn1UnexpectedTagException(Asn1DerDecoder decoder, Asn1Tag expectedTag, Asn1Tag actualTag, string? message)
			: base(message ?? BuildMessage(expectedTag, actualTag))
		{
			this.Decoder = decoder;
			this.ExpectedTag = expectedTag;
			this.ActualTag = actualTag;
		}

		public Asn1DerDecoder Decoder { get; }
		public Asn1Tag ExpectedTag { get; }
		public Asn1Tag ActualTag { get; }

		private static string? BuildMessage(Asn1Tag expectedTag, Asn1Tag actualTag)
		{
			return string.Format(Messages.Asn1_UnexpectedTag, expectedTag.TagNumber, actualTag);
		}
	}
}
