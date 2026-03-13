using System;
using System.Runtime.Serialization;
using Titanis.Dynamic;

namespace Titanis.Mocks
{
	[Serializable]
	internal class MissingExpectationException : Exception
	{
		public MissingExpectationException()
		{
		}

		public MissingExpectationException(MethodCallMessage methodCall)
			: base(BuildMessage(methodCall))
		{
			this.MethodCall = methodCall;
		}

		private static string BuildMessage(MethodCallMessage methodCall)
		{
			return $"Method called without an expectation: {methodCall.Method.DeclaringType.Name}.{methodCall.Method.Name}({string.Join(", ", methodCall.GetArguments())})";
		}

		public MissingExpectationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected MissingExpectationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public MethodCallMessage MethodCall { get; }
	}
}