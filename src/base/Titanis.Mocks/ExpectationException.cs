using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Titanis.Mocks
{
	[Serializable]
	internal class ExpectationException : Exception
	{
		private Expectation[] expectations;

		public ExpectationException()
		{
		}

		public ExpectationException(Expectation[] expectations)
			: base(BuildMessage(expectations))
		{
			this.expectations = expectations;
		}

		private static string BuildMessage(Expectation[] expectations)
		{
			StringBuilder sb = new StringBuilder($"One or more expectations were not met:")
				.AppendLine();
			foreach (var expect in expectations)
			{
				sb.AppendLine(expect.ToString());
			}
			return sb.ToString();
		}

		public ExpectationException(string message) : base(message)
		{
		}

		public ExpectationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ExpectationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}