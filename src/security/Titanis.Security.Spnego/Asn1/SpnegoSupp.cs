using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Security.Spnego.Asn1.SPNEGOASNOneSpec
{
    public class MechTypeList : IAsn1DerEncodableTlv
    {
        public Titanis.Asn1.Asn1Oid[] mechTypes;

        public virtual Asn1Tag Tag
        {
            get
            {
                return (new Asn1Tag(48));
            }
        }

        public virtual void DecodeTlv(Asn1DerDecoder decoder)
        {
            var end_struc_0 = decoder.DecodeTlvStart((new Asn1Tag(48)));
            this.DecodeValue(decoder);
            decoder.CloseTlv(end_struc_0);
        }

        public virtual void DecodeValue(Asn1DerDecoder decoder)
        {
            List<Titanis.Asn1.Asn1Oid> list_mechTypes_3 = new List<Titanis.Asn1.Asn1Oid>();
            for (
            ; (decoder.IsEndOfTuple == false);
            )
            {
                Titanis.Asn1.Asn1Oid item_mechTypes_3;
                var end_mechTypes_4 = decoder.DecodeTlvStart((new Asn1Tag(6)));
                item_mechTypes_3 = decoder.DecodeOid();
                decoder.CloseTlv(end_mechTypes_4);
                list_mechTypes_3.Add(item_mechTypes_3);
            }
        }

        public virtual bool TryDecodeTlv(Asn1DerDecoder decoder)
        {
            if (decoder.CheckTag((new Asn1Tag(48))))
            {
                this.DecodeTlv(decoder);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void EncodeValue(Asn1DerEncoder encoder)
        {
            //var end_mechTypes_2 = encoder.Position;
            if ((this.mechTypes != null))
            {
                for (int index_mechTypes_3 = (this.mechTypes.Length - 1); (index_mechTypes_3 >= 0); index_mechTypes_3 = (index_mechTypes_3 - 1))
                {
                    Titanis.Asn1.Asn1Oid item_mechTypes_3 = this.mechTypes[index_mechTypes_3];
                    var end_mechTypes_4 = encoder.Position;
                    encoder.EncodeOid(item_mechTypes_3);
                    encoder.EncodeCloseTlvHeader((new Asn1Tag(6)), end_mechTypes_4);
                }
            }
            //encoder.EncodeCloseTlvHeader((new Asn1Tag(48)), end_mechTypes_2);
        }
    }
}
