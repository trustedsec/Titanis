# Source Generation

Much of the codebase uses a source generator to generate code for reading and writing data structures from/to a data source.

Source generation is supported by these components:
* `Titanis.SourceGen` - Implements the source generator
* `Titanis.PduStruct` - Shared project that defines attributes relevant to source generation
* `Titanis.IO` - Implements `IByteSource` and `ByteWriter`, used by the generated code

A PDU structure implements `IPduStruct` with methods to read data from a `IByteSource` and write data to a `ByteWriter`.  A `IByteSource` object contains methods to read primitives such as `Int32` from a data source, whereas `ByteWriter` contains methods to write primitives.  Although writing an implementation using this approach is straightforward, it is tedious and error-prone.

Enter the PDU source generator.  The source generator generates the implementation of `IPduStruct` and takes advantage of the following observations:

1. The calls to `ReadXxx` and `WriteXxx` correspond to members of the class or struct.
1. Those calls are generally symmetric.  That is, a call to `ReadXxx` generally has a call to `WriteXxx`.
1. There are certain patterns that are fairly prevalent across different protocols, such as counted strings.

Consult the PduStructSample project for examples on how to use the source generator and related attributes.

# Enabling the Source Generator
To enable the source generator for a project, add a reference to `Titanis.SourceGen`:

```XML
<ProjectReference
    Include=
    "$(SolutionDir)\src\build\Titanis.SourceGen\Titanis.SourceGen.csproj"
    ReferenceOutputAssembly="false"
    OutputItemType="Analyzer"
    />
```

The important parts are the `ReferenceOutputAssembly` and `OutputItemType` attributes.  These cause MSBuild and Visual Studio to invoke the source generator during the build process.


# PduStruct

To trigger the source generation, mark a class or struct with `[PduStruct]`:

```C#
[PduStruct]
partial struct PduHeader {
    internal ushort size;
    private ushort reserved;
}

[PduStruct]
partial struct PduClass {
    ...
}
```

The declaration must include `partial` so that the generated source code can add the generated code to the definition.

A class marked with `[PduStruct]` may inherit another class marked with `[PduStruct]`.  In this case, the generated code first calls the methods generated on the base class to read or write members.

The source generator supports fields of the primitive numeric types with no additional information.  A field may also reference an embedded struct that implements `IPduStruct` directly or is marked with `[PduStruct]`.  Strings, arrays, and nullable types are supported but require additional information.

# PduByteOrder
When serializing integral types spanning multiple bytes, the source generator must know the byte order.  Provide this with `[PduByteOrder]`, which you may apply at the assembly level, to specific types, or to individual fields.

```C#
[assembly: PduByteOrder(PduByteOrder.LittleEndian)]

[PduStruct]
[PduByteOrder(PduByteOrder.BigEndian)]
partial struct MyPdu {
    internal int intBE;

    [PduByteOrder(PduByteOrder.LittleEndian)]
    internal int intLE;
}
```

# PduField and PduIgnore
When a class or struct is marked with `[PduStruct]`, all fields are automatically included by default, regardless of the access modifier.  Properties are not.  To ignore a field, mark the field with `[PduIgnore]`.  To include a property, mark it with `[PduField]`.

```C#
[PduStruct]
partial struct PduHeader {
    internal int includedField;

    [PduIgnore]
    internal int ignoredField;

    internal int IgnoredProperty { get; set; }

    [PduField]
    internal int IncludedProperty { get; set; }
}
```

# PduAlignment
The source generator does not enforce any alignment requirements in the generated code.  To enforce alignment for an individual field, use `[PduAlignment]`.

```C#
```

# PduString
The source generator requires additional information to serialize a string, such as the encoding and the length in bytes.  Provide these values by marking the string field with `[PduString]`, specifying the character set as either `CharSet.Ansi` or `CharSet.Unicode`.  The second argument is the name of a member evaluated at runtime to return the number of bytes to read.  The member may be a field, property, or method that accepts no arguments.  The member may either be an instance member or static member.

```C#
[PduStruct]
partial struct CountedString
{
    internal ushort byteCount;
    // Could also be:
    internal ushort ByteCountProperty => this.byteCount;
    internal ushort GetByteCount() => this.byteCount;
    internal static ushort GetStaticByteCount() => 42;

    [PduString(CharSet.Ansi, nameof(byteCount))]
    internal string str;
}
```

Note that the member is only consulted when the struct is read, not written.  It's up to the application to ensure the field is set before writing the struct.

# PduArraySize
Similar to strings, the source generator needs additional information for array fields.  Provide the number of elements with the `[PduArraySize]` attribute.  This works similarly to `[PduString]`, but specifies the number of elements rather than the number of bytes.

```C#
[PduStruct]
partial struct CountedArray
{
    internal ushort elementCount;

    [PduArraySize(nameof(elementCount))]
    internal int[] elements;
}
```

Similar to how `[PduString]` works, the provided size is only consulted when the struct is read, not written.  It's up to the application to ensure the field is set before writing the struct.

# PduConditional
Sometimes a PDU may have a bitfield or flag indicating whether certain fields are present.

```C#
[PduStruct]
partial struct ConditionalField
{
    internal int includedFields;

    internal bool IncludeField1 => 0 != (this.includedFields & 1);
    [PduConditional(nameof(IncludeField1))]
    internal int? field1;

    internal bool IncludeField2 => 0 != (this.includedFields & 2);
    [PduConditional(nameof(IncludeField2))]
    internal int field2;

    internal bool IncludeField3 => 0 != (this.includedFields & 4);
    [PduConditional(nameof(IncludeField3))]
    [PduString(CharSet.Ansi, nameof(byteCount))]
    internal string? field3;
}
```

`[PduConditional]` is required on `Nullable<T>` fields as well as fields with a reference type annotated as nullable.  It may also be applied to non-nullable fields.  In this case, the generated code does not set the field, and it will retain its previous value.

The generated code checks the condition both when reading and writing.  It does not check whether the member itself has a value.  This can result in `NullReferenceException` if the condition evaluates to `true` but the field isn't actually set.  It's up to your application to ensure the value and condition are consistent.

# PduPosition
To capture the position within a stream, declare a field (or property) of type `long` and mark it with `[PduPosition]`.  The generated code won't ready anything from the stream, but will set the field to the current position within the stream when reading or writing.

```C#
[PduStruct]
partial struct PduWithPosition
{
    ...

    [PduPosition]
    internal long positionWithinStream;

    ...
}
```

# Custom ReadMethod and WriteMethod
What if none of the above features satisfy the requirements for your particular use case?  You have a couple options:

1. Declare a struct, implement `IPduStruct` yourself, and embed the struct in the PDU struct.
1. Define methods to read and write your custom data.

If the custom pattern occurs in multiple places within the protocol implementation, prefer Option 1 above so that your implementation is reusable.  If this pattern is specific to one field, use Option 2.

For Option 2, implement a methods to read and write the custom field and specify these methods in `[PduField]` with the `ReadMethod` and `WriteMethod` named arguments.  These methods may be either static or instance methods.

```C#
[PduStruct]
partial struct PduWithCustomField
{
    [PduField(ReadMethod = nameof(ReadCustom), WriteMethod = nameof(WriteCustom))]
    internal string customField;
    private string ReadCustom(IByteSource source, PduByteOrder byteOrder)
    {
        Console.WriteLine("ReadCustom called");
        return "";
    }
    private void WriteCustom(ByteWriter writer, string value, PduByteOrder byteOrder)
    {
        Console.WriteLine("WriteCustom called");
    }
}
```

Note that if you specify custom methods, the source generator still generates code for `[PduAlignment]` and `[PduConditional]` before calling your custom methods.

For arrays, the custom methods must deal with the array as a whole.  If you want to use the built-in functionality to read an array but with a custom element type, declare a custom type for the array element and implement `IPduStruct` on this type.

# Callbacks
