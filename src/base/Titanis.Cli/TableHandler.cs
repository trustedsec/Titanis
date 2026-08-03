using System;
using System.Collections;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	internal class TableHandler
	{
		private readonly OutputField[] fields;
		private readonly bool includeHeaders;
		private readonly OutputStyle _outputStyle;
		private readonly TextTable _tbl;

		public TableHandler(OutputField[] fields, bool includeHeaders, OutputStyle outputStyle)
		{
			this.fields = fields;
			this.includeHeaders = includeHeaders;
			this._outputStyle = outputStyle;
			this._tbl = BuildResultTable(fields, this.includeHeaders);
		}

		private static TextTable BuildResultTable(OutputField[]? fields, bool includeHeaders)
		{
			TextTable tbl = new TextTable();
			if (includeHeaders)
			{
				var trHeader = tbl.AddRow();
				var trLine = tbl.AddRow();
				foreach (var field in fields!)
				{
					trHeader.AddCell(field.Caption);
					trLine.AddCell(new TextTableCell() { Padding = '-' });
				}
			}

			return tbl;
		}

		internal void AddRow(object? record, string? prefix = null, string? subPrefix = null, bool repeatHeader = true)
		{
			var tbl = this._tbl;
			if (record is not null)
			{
				int maxArrayLength = 1;
				for (int arrayIndex = 0; arrayIndex < maxArrayLength; arrayIndex++)
				{
					var tr = tbl.AddRow();
					for (int fieldIndex = 0; fieldIndex < fields!.Length; fieldIndex++)
					{
						OutputField? field = fields![fieldIndex];
						var value = field.GetValue(record);
						string? formatted;

						if ((value is IList arr))
						{
							maxArrayLength = Math.Max(maxArrayLength, arr.Count);
							if (arrayIndex < arr.Count)
							{
								value = arr[arrayIndex];
								formatted = field.FormatValue(value, this._outputStyle);
							}
							else
								formatted = null;
						}
						else if (arrayIndex == 0 || (repeatHeader && (fieldIndex == 0)))
						{
							// Print the field if it is either row0 or col0
							formatted = field.FormatValue(value, this._outputStyle);
						}
						else
							formatted = null;

						if (fieldIndex == 0)
							formatted = ((arrayIndex == 0) ? prefix : subPrefix) + formatted;

						tr.AddCell(formatted, field.Alignment);
					}
				}
			}
			else
			{
				var tr = tbl.AddRow();
			}
		}

		public TextTable BuildTable() => this._tbl;
	}
}
