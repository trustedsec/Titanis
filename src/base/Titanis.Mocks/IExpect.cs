using System;
using System.Threading.Tasks;

namespace Titanis.Mocks
{
	public interface IExpect
	{
		void Throw(Exception? ex);
		void CallBase();
	}

	public interface IExpect<TInstance> : IExpect
	{
	}

	public interface IExpect<TInstance, TReturn> : IExpect<TInstance>
	{
		void Return(TReturn value);
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
	}
}