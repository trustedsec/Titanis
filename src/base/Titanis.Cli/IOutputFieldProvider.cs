using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Titanis.Cli
{
	public interface IOutputFieldProvider
	{
		OutputField[] GetFieldsForType(Type recordType);
		OutputField[] GetFieldsForRecord(object record);
		bool IncludesField(string fieldName);
	}

	public class OutputFieldProvider : IOutputFieldProvider
	{
		public OutputFieldProvider(CommandMetadataContext mdContext)
		{
			this.mdContext = mdContext;
		}

		private readonly CommandMetadataContext mdContext;

		public OutputField[] GetFieldsForRecord(object record)
		{
			return OutputField.GetFieldsFor(record, null);
		}

		public OutputField[] GetFieldsForType(Type recordType)
		{
			return OutputField.GetFieldsFor(recordType, this.mdContext, null);
		}

		public bool IncludesField(string fieldName) => true;
	}
}
