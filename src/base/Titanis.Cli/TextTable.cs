using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Represents a table that is rendered to text.
	/// </summary>
	/// <remarks>
	/// Use <see cref="BuildString()"/> or one its overloads to render the table as text.
	/// </remarks>
	public class TextTable
	{
		public TextTable()
		{
			this.Rows = new List<TextTableRow>();
		}
		public TextTable(params TextTableRow[] rows)
		{
			this.Rows = new List<TextTableRow>(rows?.Length ?? 2);
		}

		public List<TextTableRow> Rows { get; }
		public TextTableRow AddRow()
		{
			TextTableRow tr = new TextTableRow();
			this.Rows.Add(tr);
			return tr;
		}
		public TextTableRow AddRow(params string?[] cells)
		{
			TextTableRow tr = new TextTableRow(Array.ConvertAll(cells, r => new TextTableCell(r)));
			this.Rows.Add(tr);
			return tr;
		}
		public TextTableRow AddRow(params FormattedText?[] cells)
		{
			TextTableRow tr = new TextTableRow(Array.ConvertAll(cells, r => new TextTableCell(r)));
			this.Rows.Add(tr);
			return tr;
		}
		public TextTableRow AddRow(params TextTableCell?[] cells)
		{
			TextTableRow tr = new TextTableRow(cells);
			this.Rows.Add(tr);
			return tr;
		}

		/// <summary>
		/// Gets or sets the text to print on the left end of each row.
		/// </summary>
		public string? LeftMargin { get; set; }
		/// <summary>
		/// Gets or sets the text to print on the right end of each row.
		/// </summary>
		public string? RightMargin { get; set; }
		/// <summary>
		/// Gets or sets the text to print between cells
		/// </summary>
		public string? ColumnSeparator { get; set; } = "  ";

		public sealed override string ToString() => this.BuildString();
		/// <summary>
		/// Renders the table as a string.
		/// </summary>
		public string BuildString()
		{
			StringBuilder sb = new StringBuilder();
			//this.BuildString(sb);
			return sb.ToString();
		}
		public void Render(TextTableFormatterBase formatter)
		{
			if (formatter is null) throw new ArgumentNullException(nameof(formatter));
			foreach (var row in this.Rows)
			{
				formatter.RenderRow(row);
			}
		}

		public static TextTable BuildTable<TRecord>(OutputField<TRecord>[] fields, IEnumerable<TRecord> items)
		{
			TextTable tbl = new TextTable();
			{
				var trHeader = tbl.AddRow();
				var trLine = tbl.AddRow();
				foreach (var field in fields)
				{
					trHeader.AddCell(field.Caption);
					trLine.AddCell(new TextTableCell() { Padding = '-' });
				}
			}

			foreach (var item in items)
			{
				var tr = tbl.AddRow();
				foreach (var field in fields)
				{
					var value = field.GetValue(item);
					var formatted =
						(!string.IsNullOrEmpty(field.FormatString) && (value is IFormattable f))
							? f.ToString(field.FormatString, null)
						: value?.ToString();
					tr.AddCell(formatted, field.Alignment);
				}
			}

			return tbl;
		}
	}
	/// <summary>
	/// Represents a row within a <see cref="TextTable"/>.
	/// </summary>
	public class TextTableRow
	{
		public TextTableRow(params TextTableCell?[] cells)
		{
			this.Cells = !cells.IsNullOrEmpty() ? new List<TextTableCell?>(cells) : new List<TextTableCell?>();
		}

		public List<TextTableCell?> Cells { get; }

		public TextTableCell AddCell()
			=> this.AddCell((string?)null);
		public void AddCell(TextTableCell cell)
			=> this.Cells.Add(cell ?? throw new ArgumentNullException(nameof(cell)));
		public TextTableCell AddCell(string? text, DisplayAlignment alignment = DisplayAlignment.Left)
		{
			var cell = new TextTableCell(text, alignment);
			this.Cells.Add(cell);
			return cell;
		}
		public TextTableCell AddCell(FormattedText text, DisplayAlignment alignment = DisplayAlignment.Left)
		{
			var cell = new TextTableCell(text, alignment);
			this.Cells.Add(cell);
			return cell;
		}
	}

	[Flags]
	public enum TextStyleOptions
	{
		None = 0,
		Bold = 1,
		Italic = 2,

		BoldItalic = Bold | Italic,
	}

	/// <summary>
	/// Represents a cell within a <see cref="TextTable"/>.
	/// </summary>
	public class TextTableCell
	{
		public TextTableCell()
		{
			this.IsEmpty = true;
			this.FormattedText = FormattedTextFactory.Empty;
		}
		public TextTableCell(string? text, DisplayAlignment alignment = DisplayAlignment.Left)
		{
			this.IsEmpty=string.IsNullOrEmpty(text);
			this.FormattedText = new FormattedText(text);
			this.Alignment = alignment;
		}
		public TextTableCell(FormattedText text, DisplayAlignment alignment = DisplayAlignment.Left)
		{
			if (text is null) throw new ArgumentNullException(nameof(text));
			this.FormattedText = text;
			this.Alignment = alignment;
		}
		public bool IsEmpty { get; }
		/// <summary>
		/// Gets or sets the formatted text in the cell.
		/// </summary>
		public FormattedText FormattedText { get; }
		/// <summary>
		/// Gets or sets the alignment of the text within the cell.
		/// </summary>
		public DisplayAlignment Alignment { get; set; } = DisplayAlignment.Left;
		/// <summary>
		/// Gets the text to pad the cell with if the text is shorter than the column containing it.
		/// </summary>
		public char Padding { get; set; } = ' ';
	}
}
