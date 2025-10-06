using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;

namespace Titanis.Cli
{
	public interface ITextTableRenderCallback
	{
		void RenderCellText(TextTableCell cell, StringBuilder sb);
	}

	/// <summary>
	/// Represents a table that is rendered to text.
	/// </summary>
	/// <remarks>
	/// Use <see cref="ToString()"/> or one its overloads to render the table as text.
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

		/// <summary>
		/// Renders the table as a string.
		/// </summary>
		public sealed override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			this.ToString(sb);
			return sb.ToString();
		}
		public string ToString(StringBuilder sb, ITextTableRenderCallback? callback = null)
		{
			string columnSeparator = this.ColumnSeparator ?? string.Empty;
			string leftMargin = this.LeftMargin ?? string.Empty;
			string rightMargin = this.RightMargin ?? string.Empty;

			// Count columns
			var columnCount = 0;
			foreach (var row in this.Rows)
			{
				if (row != null)
					columnCount = Math.Max(columnCount, row.Cells.Count);
			}

			StringBuilder tempSB = new StringBuilder();

			// Measure columns
			var columnWidths = new int[columnCount];
			foreach (var row in this.Rows)
			{
				if (row is null)
					continue;

				for (int i = 0; i < row.Cells.Count; i++)
				{
					var cell = row.Cells[i];
					if (cell != null)
					{
						if (callback != null)
						{
							callback.RenderCellText(cell, tempSB);
							cell.RenderedText = tempSB.ToString();
							tempSB.Clear();
						}
						else
						{
							cell.RenderedText = cell.Text ?? string.Empty;
						}

						columnWidths[i] = Math.Max(columnWidths[i], cell.RenderedText?.Length ?? 0);
					}
				}
			}

			// Print rows
			int rowWidth = columnWidths.Sum();
			//StringBuilder sb = new StringBuilder(this.Rows.Count * (rowWidth + leftMargin.Length + rightMargin.Length + 2));
			foreach (var row in this.Rows)
			{
				sb.Append(leftMargin);

				if (row is null)
					continue;

				for (int i = 0; i < row.Cells.Count; i++)
				{
					var colWidth = columnWidths[i];
					if (i > 0 && colWidth > 0)
						sb.Append(columnSeparator);

					var cell = row.Cells[i];
					if (cell != null)
					{
						bool shouldPadRight = (i + 1 < columnCount) || !string.IsNullOrEmpty(rightMargin) || cell.Padding != ' ';

						var cellText = cell.RenderedText!;
						if (cellText.Length == colWidth)
							sb.Append(cellText);
						else if (cellText.Length == 0)
						{
							if (shouldPadRight)
								sb.Append(new string(cell.Padding, colWidth));
						}
						else
						{
							switch (cell.Alignment)
							{
								case DisplayAlignment.Center:
									sb.Append(new string(cell.Padding, (colWidth - cellText.Length) / 2));
									sb.Append(cellText);
									if (shouldPadRight)
										sb.Append(new string(cell.Padding, (colWidth - cellText.Length + 1) / 2));
									break;
								case DisplayAlignment.Right:
									sb.Append(new string(cell.Padding, colWidth - cellText.Length));
									sb.Append(cellText);
									break;
								case DisplayAlignment.Left:
								default:
									sb.Append(cellText);
									if (shouldPadRight)
										sb.Append(new string(cell.Padding, colWidth - cellText.Length));
									break;
							}
						}
					}
					else
					{
						sb.Append(new string(' ', colWidth));
					}
				}

				sb.Append(rightMargin);
				sb.AppendLine();
			}

			return sb.ToString();
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
			this.Text = string.Empty;
		}
		public TextTableCell(string? text)
		{
			this.Text = text ?? string.Empty;
		}
		public TextTableCell(string? text, DisplayAlignment alignment = DisplayAlignment.Left)
		{
			this.Text = text ?? string.Empty;
			this.Alignment = alignment;
		}
		/// <summary>
		/// Gets or sets the text in the cell.
		/// </summary>
		public string? Text { get; set; }
		/// <summary>
		/// Gets or sets the alignment of the text within the cell.
		/// </summary>
		public DisplayAlignment Alignment { get; set; } = DisplayAlignment.Left;
		/// <summary>
		/// Gets the text to pad the cell with if the text is shorter than the column containing it.
		/// </summary>
		public char Padding { get; set; } = ' ';

		public TextStyleOptions StyleOptions { get; set; }

		internal string? RenderedText { get; set; }
	}
}
