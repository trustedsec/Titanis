using System;
using System.ComponentModel;

namespace Titanis.Cli
{
	internal class FieldListProvider : ValueListProvider
	{
		public override Array? GetValueListFor(ParameterMetadata parameter, object? command, CommandMetadataContext context)
		{
			var recordType = parameter.DeclaringCommand.OutputRecordType;
			if (recordType is not null)
			{
				if (context.Resolver.ReflectType(typeof(ICustomTypeDescriptor)).IsAssignableFrom(recordType))
				{
					// This type has dynamic fields
				}
				else
				{
					var outputFields = OutputField.GetFieldsFor(recordType, context);
					var fieldNames = Array.ConvertAll(outputFields, r => r.Name);
					return fieldNames;
				}
			}

			return null;
		}
	}
}