using System.ComponentModel;
using System.Text;
using Titanis.Cli;
using Titanis.Msrpc.Mseven6;

namespace Even6;

public abstract class QueryCommandBase : RpcCommand<EventLog6Client>
{
	[Parameter(After = nameof(ServerName))]
	[Mandatory]
	[Description("Channel path")]
	public string? Channel { get; set; }


	[Parameter]
	[Description("Query events after (UTC)")]
	public DateTime? After { get; set; }

	[Parameter]
	[Description("Query events before (UTC)")]
	public DateTime? Before { get; set; }

	[Parameter]
	[Description("XPath query")]
	public string? QueryXPath { get; set; }

	[Parameter]
	[Description("Query events by provider (source)")]
	public string[]? ByProvider { get; set; }

	[Parameter]
	[Description("Query events by level")]
	public EventLevel[]? ByLevel { get; set; }

	[Parameter]
	[Description("Query events by matching keywords")]
	public StandardEventKeywords[]? ByKeyword { get; set; }

	[Parameter]
	[Description("Query event IDs")]
	public NumberOrRange[]? ByEventId { get; set; }

	[Parameter]
	[Description("Query event data")]
	public NameValuePair[]? ByEventData { get; set; }



	[Parameter]
	[Description("Page size")]
	[DefaultValue("1024")]
	public int PageSize { get; set; }


	protected override void ValidateParameters(ParameterValidationContext context)
	{
		base.ValidateParameters(context);

		int queryCount = 0;
		if (this.QueryXPath != null)
			queryCount++;
		if (
			(this.ByProvider != null)
			|| (this.Before.HasValue)
			|| (this.After.HasValue)
			|| (this.ByLevel != null)
			|| (this.ByKeyword != null)
			|| (this.ByEventId != null)
			|| (this.ByEventData != null)
			)
			queryCount++;

		if (queryCount > 1)
			context.LogError("-QueryXPath cannot be used with the other query options.");
	}

	protected string BuildQueryXPath()
	{
		string query;

		if (this.QueryXPath != null)
			query = this.QueryXPath;
		else if (
			(this.ByProvider != null)
			|| (this.Before.HasValue)
			|| (this.After.HasValue)
			|| (this.ByLevel != null)
			|| (this.ByKeyword != null)
			|| (this.ByEventId != null)
			|| (this.ByEventData != null)
			)
		{
			StringBuilder sb = new StringBuilder("*[System[");

			bool hasCond = false;
			if (this.After != null)
			{
				if (hasCond)
					sb.Append(" and ");

				sb.Append($"(TimeCreated[@SystemTime >= '{this.After.Value:O}'])");
				hasCond = true;
			}
			if (this.Before != null)
			{
				if (hasCond)
					sb.Append(" and ");

				sb.Append($"(TimeCreated[@SystemTime <= '{this.Before.Value:O}'])");
				hasCond = true;
			}
			if (this.ByProvider != null)
			{
				if (hasCond)
					sb.Append(" and ");

				sb.Append('(');
				for (int i = 0; i < this.ByProvider.Length; i++)
				{
					if (i > 0)
						sb.Append(" or ");

					var provName = this.ByProvider[i];
					provName = EscapeXPathString(provName);
					sb.Append($"Provider='{provName}'");
				}
				sb.Append(')');
				hasCond = true;
			}
			if (this.ByLevel != null)
			{
				if (hasCond)
					sb.Append(" and ");

				sb.Append('(');
				for (int i = 0; i < this.ByLevel.Length; i++)
				{
					if (i > 0)
						sb.Append(" or ");

					EventLevel level = this.ByLevel[i];
					sb.Append($"Level={(int)level}");
					if (level == EventLevel.Information)
					{
						// Event Viewer also includes 0
						sb.Append($" or Level=0");
					}
				}
				sb.Append(')');
				hasCond = true;
			}
			if (this.ByKeyword != null)
			{
				if (hasCond)
					sb.Append(" and ");

				ulong flagsValue = 0;
				foreach (var keyword in this.ByKeyword)
				{
					flagsValue |= (ulong)keyword;
				}
				sb.Append($"band(Keywords,{flagsValue})");

				hasCond = true;
			}
			if (this.ByEventId != null)
			{
				if (hasCond)
					sb.Append(" and ");

				sb.Append('(');
				for (int i = 0; i < this.ByEventId.Length; i++)
				{
					if (i > 0)
						sb.Append(" or ");

					var idSpec = this.ByEventId[i];
					if (idSpec.MinValue == idSpec.MaxValue)
						sb.Append($"EventID={idSpec.MinValue}");
					else
						// EVEN6 doesn't expect an escaped path
						sb.Append($"(EventID >= {idSpec.MinValue} and EventID <= {idSpec.MaxValue})");
				}
				sb.Append(')');

				hasCond = true;
			}

			sb.Append(']');

			if (this.ByEventData != null)
			{
				if (hasCond)
					sb.Append(" and ");

				sb.Append("EventData[");
				hasCond = false;

				var groups = this.ByEventData.GroupBy(r => r.Name, StringComparer.OrdinalIgnoreCase);

				foreach (var group in groups)
				{

					if (hasCond)
						sb.Append(" and ");


					sb.Append('(');
					bool subseq = false;
					foreach (var entry in group)
					{
						if (subseq)
							sb.Append(" or ");
						else
							subseq = true;

						sb.Append($"Data[@Name='{group.Key}']=");
						var value = entry.Value;
						if (int.TryParse(entry.Value, out var num))
						{
							sb.Append(value);
						}
						else
						{
							value = EscapeXPathString(value);
							sb.Append($"'{value}'");
						}
					}
					sb.Append(')');
					hasCond = true;
				}

				sb.Append(']');
			}

			sb.Append(']');
			query = sb.ToString();
		}
		else
			query = "*";

		return query;
	}

	private static string EscapeXPathString(string provName)
	{
		return provName.Replace("'", "''");
	}
}
