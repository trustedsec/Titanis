using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis
{
	/// <summary>
	/// Provides extension methods for <see cref="Task"/> bojects.
	/// </summary>
	public static class TaskExtensions
	{
		/// <summary>
		/// Awaits a task for a specified timeout.
		/// </summary>
		/// <param name="task"><see cref="Task"/> to await</param>
		/// <param name="timeout">Amount of time to wait</param>
		/// <exception cref="TimeoutException"><paramref name="task"/> did not complete within <paramref name="task"/>.</exception>
		public static async Task AwaitWithTimeout(this Task task, TimeSpan timeout)
		{
			bool completed = await Task.WhenAny(task, Task.Delay(timeout)) == task;
			if (!completed)
				throw new TimeoutException();
		}
		/// <summary>
		/// Awaits a task for a specified timeout.
		/// </summary>
		/// <param name="task"><see cref="Task"/> to await</param>
		/// <param name="timeout">Amount of time to wait</param>
		/// <exception cref="TimeoutException"><paramref name="task"/> did not complete within <paramref name="task"/>.</exception>
		/// <returns>The value returned by <paramref name="task"/></returns>
		public static async Task<T> AwaitWithTimeout<T>(this Task<T> task, TimeSpan timeout)
		{
			bool completed = await Task.WhenAny(task, Task.Delay(timeout)) == task;
			if (!completed)
				throw new TimeoutException();

			return task.Result;
		}
	}
}
