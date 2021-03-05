using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis
{
	/// <summary>
	/// Specifies the state of a <see cref="Runnable"/> object.
	/// </summary>
	public enum RunnableState
	{
		/// <summary>
		/// The object is stopped
		/// </summary>
		Stopped = 0,
		/// <summary>
		/// The object is transitioning from <see cref="Stopped"/> to <see cref="Running"/>
		/// </summary>
		Starting,
		/// <summary>
		/// The object is running
		/// </summary>
		Running,
		/// <summary>
		/// The object is transitioning from <see cref="Running"/> to <see cref="Stopped"/>
		/// </summary>
		Stopping
	}

	/// <summary>
	/// Implements an object that can be run, such as a service.
	/// </summary>
	/// <remarks>
	/// A 
	/// </remarks>
	public abstract partial class Runnable
	{
		private int _state;
		/// <summary>
		/// Gets the state of the runnable object.
		/// </summary>
		public RunnableState State => (RunnableState)this._state;
		/// <summary>
		/// Gets a value indicating whether the object is currently running.
		/// </summary>
		/// <seealso cref="State"/>
		public bool IsRunning => this.State == RunnableState.Running;

		/// <summary>
		/// <see cref="CancellationTokenSource"/> for signaling the runnable to stop
		/// </summary>
		private CancellationTokenSource? _stopTokenSource;
		/// <summary>
		/// <see cref="Task"/> representing the run state
		/// </summary>
		private Task? _runTask;
		/// <summary>
		/// <see cref="TaskCompletionSource{Boolean}"/> signaled when the <see cref="Runnable"/> has entered the <see cref="RunnableState.Running"/> state.
		/// </summary>
		private TaskCompletionSource<bool>? _startedSource;
		/// <summary>
		/// Gets a task <see cref="Task"/> that is completed when the <see cref="Runnable"/> has entered the <see cref="RunnableState.Running"/> state.
		/// </summary>
		/// <remarks>
		/// This property does not have a value until the first call to <see cref="Start"/>.
		/// <para>
		/// If <see cref="OnStarting"/> throws an exception, that exception is is returned by the task.
		/// </para>
		/// </remarks>
		public Task? StartedTask => this._startedSource?.Task;
		/// <summary>
		/// <see cref="TaskCompletionSource{Boolean}"/> signaled when the <see cref="Runnable"/> has entered the <see cref="RunnableState.Stopped"/> state.
		/// </summary>
		private TaskCompletionSource<bool>? _stoppedSource;
		/// <summary>
		/// Gets a task <see cref="Task"/> that is completed when the <see cref="Runnable"/> has entered the <see cref="RunnableState.Stopped"/> state.
		/// </summary>
		/// <remarks>
		/// This property does not have a value until the first call to <see cref="Start"/>.
		/// </remarks>
		public Task? StoppedTask => this._stoppedSource?.Task;
		/// <summary>
		/// Gets the <see cref="Exception"/> thrown while the <see cref="Runnable"/> was running, if any.
		/// </summary>
		public Exception? Exception { get; private set; }

		/// <summary>
		/// Starts the <see cref="Runnable"/>.
		/// </summary>
		/// <exception cref="InvalidOperationException">The <see cref="Runnable"/> was not in the <see cref="RunnableState.Stopped"/> state.</exception>
		/// <remarks>
		/// This method initializes internal state information before calling <see cref="OnStarting"/> to initialize the object
		/// and then <see cref="Run"/> to run the object.  The object remains in the <see cref="RunnableState.Running"/> state
		/// until either one of these methods throws an exception, or <see cref="Stop(TimeSpan)"/> is called.
		/// <para>
		/// This method can only be called while the <see cref="Runnable"/> is in the <see cref="RunnableState.Stopped"/> state.
		/// </para>
		/// </remarks>
		public void Start()
		{
			var oldState = (RunnableState)Interlocked.CompareExchange(ref this._state, (int)RunnableState.Starting, (int)RunnableState.Stopped);
			if (oldState != RunnableState.Stopped)
				throw new InvalidOperationException(Messages.ProtocolBase_NotStopped);

			this._startedSource = new TaskCompletionSource<bool>();
			this._stoppedSource = new TaskCompletionSource<bool>();
			this.Exception = null;
			var cancelSource = this._stopTokenSource = new CancellationTokenSource();

			this._runTask = Task.Factory.StartNew(() => this.RunWrapper(cancelSource.Token), TaskCreationOptions.LongRunning).Unwrap();
		}

		private async Task RunWrapper(CancellationToken cancellationToken)
		{
			try
			{
				try
				{
					await this.OnStarting(cancellationToken);
				}
				catch (Exception ex)
				{
					this._startedSource!.SetException(ex);
					throw;
				}
				this._state = (int)RunnableState.Running;
				this._startedSource!.SetResult(true);
				await this.Run(cancellationToken);
			}
			catch (Exception ex)
			{
				this.Exception = ex;
				// Transition to stopped state
				_ = this.Stop(TimeSpan.MaxValue);
			}
		}

		/// <summary>
		/// Called when the object transitions to the <see cref="RunnableState.Running"/> state.
		/// </summary>
		/// <param name="cancellationToken">Signals a request to stop the object</param>
		protected virtual Task OnStarting(CancellationToken cancellationToken)
			=> Task.CompletedTask;

		/// <summary>
		/// Performs the main running task of the object.
		/// </summary>
		/// <param name="cancellationToken">Signals a request to stop the object</param>
		/// <remarks>
		/// <paramref name="cancellationToken"/> is signaled by calling <see cref="Stop(TimeSpan)"/>.
		/// <para>
		/// If the implementation throws an exception, the object transitions to <see cref="RunnableState.Stopped"/>.
		/// IF the method exits normally, the object remains in <see cref="RunnableState.Running"/>.
		/// The implementation may stop itself by calling <see cref="Stop(TimeSpan)"/> with an infinite timeout and
		/// without awaiting.  Awaiting the call to <see cref="Stop(TimeSpan)"/> will result in a deadlock, since
		/// <see cref="Stop(TimeSpan)"/> awaits the <see cref="Run"/> method.
		/// </para>
		/// </remarks>
		protected virtual Task Run(CancellationToken cancellationToken)
			=> Task.CompletedTask;

		/// <summary>
		/// Called when the object transitions to the <see cref="RunnableState.Running"/> state.
		/// </summary>
		protected virtual Task OnStopping()
			=> Task.CompletedTask;

		/// <summary>
		/// Called when an object does not respond to a request to stop.
		/// </summary>
		/// <remarks>
		/// The default implementation calls <see cref="OnStopping"/>.
		/// </remarks>
		protected virtual Task OnAborting()
			=> this.OnStopping();

		/// <summary>
		/// Stops the <see cref="Runnable"/>.
		/// </summary>
		/// <param name="timeout">Time to wait for the object to stop</param>
		/// <remarks>
		/// If the object is <see cref="RunnableState.Starting"/> or <see cref="RunnableState.Running"/>,
		/// this method first signals the <see cref="CancellationToken"/> passed to <see cref="OnStarting(CancellationToken)"/>
		/// and <see cref="Run"/> and waits for <paramref name="timeout"/> for those methods to gracefully exit.
		/// If the method gracefully exits, <see cref="OnStopping"/> is called.  If the method does not gracefully exit
		/// before <paramref name="timeout"/> passes, <see cref="OnAborting"/> is called.
		/// </remarks>
		public async Task Stop(TimeSpan timeout)
		{
			var oldState = (RunnableState)Interlocked.Exchange(ref this._state, (int)RunnableState.Stopping);
			if (oldState == RunnableState.Starting || oldState == RunnableState.Running)
			{
				this._stopTokenSource!.Cancel();
				bool stopped;
				try
				{
					// Guarded by _state
					Debug.Assert(this._runTask != null);

					await this._runTask!.AwaitWithTimeout(timeout);
					stopped = true;
				}
				catch (Exception ex)
				{
					this.Exception = ex;
					stopped = false;
				}

				try
				{
					await (stopped ? this.OnStopping() : this.OnAborting());
				}
				catch (Exception ex)
				{
					this.Exception = ex;
				}

				this._stoppedSource!.SetResult(true);

				this._state = (int)RunnableState.Stopped;
			}
			else if (oldState == RunnableState.Stopping)
			{
				// Await previous call to Stop
				await this._stoppedSource!.Task;
			}
		}
	}
}
