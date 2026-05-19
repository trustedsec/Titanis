This source generator generates the source code for reading and writing PDU structures.

To declare a type as a PDU structure, mark it with [PduStruct].

* The type must be declared with the `partial` keyword so that the source generator can add declarations to it.
* The base class must either be marked with `[PduStruct]` or implement `IPduStruct`.

# PDU Parameters
Some PDUs may require parameters to control serialization.  Declare the parameter as a field marked with `[PduParameter]`

# Fields
All fields declared within a type marked with `[PduStruct]` are considered to be fields within the serialized PDU, unless marked with `[PduIgnore]`.  Specifically, the access modifier doesn't matter.  Properties are not included by default and must be marked with `[PduField]` to be serialized.

## Special Fields
Mark a field with `[PduPosition]` to record the serialization offset.  The field must be declared as `long`.  Position fields are ignored when writing the PDU.