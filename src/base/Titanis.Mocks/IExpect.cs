using System;
using System.Threading.Tasks;

namespace Titanis.Mocks
{
	public interface IExpect
	{
		void Throw(Exception? ex);
		void CallBase();

		IExpect Do(Action<object[]> callback);
		IExpect Do<TArg>(Action<TArg> callback);
	}

	public interface IExpect<TInstance> : IExpect
	{
		new IExpect<TInstance> Do(Action<object[]> callback);
		new IExpect<TInstance> Do<TArg>(Action<TArg> callback);
	}

	public interface IExpect<TInstance, TReturn> : IExpect<TInstance>
	{
		void Return(TReturn value);
		void Return(Func<object[], TReturn> valueFunc);

		new IExpect<TInstance, TReturn> Do(Action<object[]> callback);
		new IExpect<TInstance, TReturn> Do<TArg>(Action<TArg> callback);
	}

	public static class ExpectExtensions
	{
		public static void Return<TInstance, TReturn, TArg0>(this IExpect<TInstance, TReturn> expect, Func<TArg0, TReturn> valueFunc)
		{
			expect.Return((object[] args) => valueFunc((TArg0)args[0]));
		}
	}

	public interface IExpectAsync
	{
		void ThrowAsync(Exception ex);
	}

	public interface IExpectAsync<TInstance> : IExpect<TInstance, Task>, IExpectAsync
	{
	}

	public interface IExpectAsync<TInstance, TReturn> : IExpect<Task<TReturn>>
	{
		void ThrowAsync(Exception ex);
		void ReturnAsync(TReturn value);
		void ReturnAsync(Func<object[], Task<TReturn>> valueFunc);
	}
}