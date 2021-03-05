using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Titanis.Dynamic
{
	public class MethodCallMessage
	{
		public MethodCallMessage(RuntimeMethodHandle methodHandle, int count)
		{
			var method = (MethodInfo)MethodBase.GetMethodFromHandle(methodHandle);
			this.Method = method;
			this.args = new object[count];
			this._names = new string[count];
		}

		public MethodInfo Method { get; }
		private readonly string[] _names;

		internal readonly object[] args;
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

		public object GetParam(int index)
		{
			return this.args[index];
		}
	}
}
