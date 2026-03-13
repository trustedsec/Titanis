using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Titanis.Dynamic
{
	/// <summary>
	/// Represents a method call.
	/// </summary>
	public class MethodCallMessage
	{
		// NOTE: This must be public so that it can be accessed by the mock implementations
		public MethodCallMessage(RuntimeMethodHandle methodHandle, int count)
		{
			var method = (MethodInfo)MethodBase.GetMethodFromHandle(methodHandle);
			this.Method = method;
			this.args = new object[count];
			this._names = new string[count];
		}
		/// <summary>
		/// Gets the method called.
		/// </summary>
		public MethodInfo Method { get; }
		private readonly string[] _names;

		internal readonly object[] args;
		/// <summary>
		/// Gets an argument.
		/// </summary>
		/// <param name="index">Argument index</param>
		/// <returns>The value of argument at position <paramref name="index"/></returns>
		public object GetArgument(int index)
			=> this.args[index];

		public int ArgCount => (this.args == null) ? 0 : this.args.Length;

		public bool callBase;
		public object? returnValue;

		public void SetParam(string name, int index, object value)
		{
			this.args[index] = value;
			this._names[index] = name;
		}

		public object[] GetArguments()
		{
			return this.args;
		}
	}
}
