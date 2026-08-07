using Microsoft.Data.Sqlite;

namespace Titanis.Info
{

	record struct InfoCol(string Name, SqliteType ColType);

	record class RowData(object[] FieldValues)
	{
		public long InsertedId { get; internal set; }
		public Exception? Error { get; internal set; }

		internal List<RowBuffer>? childRows;
		public void AddChildRows(RowBuffer rows)
		{
			if (rows is null) throw new ArgumentNullException(nameof(rows));
			(this.childRows ??= new List<RowBuffer>()).Add(rows);
		}
	}
	class RowBuffer
	{
		public RowBuffer(string table, InfoCol[] columns, int parentIndex = -1)
		{
			Table = table;
			Columns = columns;
			ParentIndex = parentIndex;
		}

		public string Table { get; }
		public InfoCol[] Columns { get; }
		public int ParentIndex { get; }

		internal List<RowData> rows = new List<RowData>();

		public RowData AppendRow(object[] values)
		{
			if (values is null) throw new ArgumentNullException(nameof(values));
			if (values.Length != this.Columns.Length)
				throw new ArgumentException($"The number of values ({values.Length}) does not match the number of columns ({this.Columns.Length}).");

			RowData row = new(values);
			this.rows.Add(row);
			return row;
		}
	}
}
