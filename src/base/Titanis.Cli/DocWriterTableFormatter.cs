using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Titanis.Cli
{
	public abstract class TextTableFormatterBase
	{
		public abstract void RenderRow(TextTableRow? row);
	}
	internal class DocWriterTableFormatter : TextTableFormatterBase
	{
		internal DocWriterTableFormatter(
			string? columnSep,
			string? leftMargin,
			string? rightMargin
			)
		{
			this.columnSep = columnSep ?? string.Empty;
			this.leftMargin = leftMargin ?? string.Empty;
			this.rightMargin = rightMargin ?? string.Empty;
		}

		record struct CellInfo(TextTableCell? Source, int Width);
		record struct Row(CellInfo[] cells);

		private int _maxColumns;

		private List<Row> _rows = new List<Row>();
		private readonly string columnSep;
		private readonly string leftMargin;
		private readonly string rightMargin;
		private readonly MeasureTarget measure = new MeasureTarget();

		public sealed override void RenderRow(TextTableRow? row)
		{
			if (row is not null)
			{
				this._maxColumns = Math.Max(this._maxColumns, row.Cells.Count);

				var cells = new CellInfo[row.Cells.Count];
				for (int i = 0; i < row.Cells.Count; i++)
				{
					var cell = row.Cells[i];
					if (cell != null)
					{
						cell.FormattedText.PrintTo(measure);
						cells[i] = new CellInfo(cell, measure.Size);
						this.measure.Reset();
					}
				}

				this._rows.Add(new Row(cells));
			}
			else
			{
				this._rows.Add(new Row([]));
			}
		}


		internal void Complete(IDocWriter writer)
		{
			int[] columnWidths = new int[this._maxColumns];
			foreach (var row in this._rows)
			{
				for (int i = 0; i < columnWidths.Length; i++)
				{
					var cell = (i < row.cells.Length) ? row.cells[i] : default;
					columnWidths[i] = Math.Max(columnWidths[i], cell.Width);
				}
			}

			int columnCount = this._maxColumns;
			int rowWidth = columnWidths.Sum();
			foreach (var row in this._rows)
			{
				writer.WriteText(this.leftMargin);
				for (int i = 0; i < row.cells.Length; i++)
				{
					CellInfo cellInfo = row.cells[i];
					var colWidth = columnWidths[i];
					if (i > 0 && colWidth > 0)
						writer.WriteText(this.columnSep);

					TextTableCell? cell = cellInfo.Source;
					if (cell != null)
					{
						bool shouldPadRight = (i + 1 < columnCount) || !string.IsNullOrEmpty(rightMargin) || cell.Padding != ' ';

						if (cellInfo.Width == colWidth)
							writer.WriteText(cell.FormattedText);
						else if (cellInfo.Width == 0)
						{
							if (shouldPadRight)
								writer.WriteText(new string(cell.Padding, colWidth));
						}
						else
						{
							switch (cell.Alignment)
							{
								case DisplayAlignment.Center:
									writer.WriteText(new string(cell.Padding, (colWidth - cellInfo.Width) / 2));
									writer.WriteText(cell.FormattedText);
									if (shouldPadRight)
										writer.WriteText(new string(cell.Padding, (colWidth - cellInfo.Width + 1) / 2));
									break;
								case DisplayAlignment.Right:
									writer.WriteText(new string(cell.Padding, colWidth - cellInfo.Width));
									writer.WriteText(cell.FormattedText);
									break;
								case DisplayAlignment.Left:
								default:
									writer.WriteText(cell.FormattedText);
									if (shouldPadRight)
										writer.WriteText(new string(cell.Padding, colWidth - cellInfo.Width));
									break;
							}
						}
					}
					else
					{
						writer.WriteText(new string(' ', colWidth));
					}
				}

				writer.WriteText(this.rightMargin);
				writer.WriteLine();
			}
		}
	}
}
