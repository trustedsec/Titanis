namespace Titanis
{
	/// <summary>
	/// Specifies the output format of a log.
	/// </summary>
	public enum LogFormat
	{
		/// <summary>
		/// Messages are written as human-readable text.
		/// </summary>
		Text,
		/// <summary>
		/// Messages are written as human-readable text and prefixed with an ISO-formatted UTC timestamp.
		/// </summary>
		TextWithTimestamp,
		/// <summary>
		/// Messages are written as JSON objects.
		/// </summary>
		Json,
	}
}
