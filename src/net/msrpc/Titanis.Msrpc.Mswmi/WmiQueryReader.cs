using ms_wmi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mswmi
{
	/// <summary>
	/// Reads the results of a WMI query.
	/// </summary>
	/// <seealso cref="WmiScope.ExecuteWqlQueryAsync(string, int, CancellationToken)"/>
	/// <remarks>
	/// The results are read a page at a time.  A larger page size reduces the number of network calls,
	/// but increases the size of the response.
	/// </remarks>
	public partial class WmiQueryReader
	{
		internal WmiQueryReader(
			WmiScope scope,
			WmiClient wmi,
			IEnumWbemClassObject enumObj,
			int pageSize
			)
		{
			this._scope = scope;
			this._wmi = wmi;
			this._enumObj = enumObj;

			this._results = new WmiObject?[pageSize];
		}

		private readonly WmiScope _scope;
		private WmiClient _wmi;
		private IEnumWbemClassObject _enumObj;

		/// <summary>
		/// Number of results on current page
		/// </summary>
		private int _resultCount;
		/// <summary>
		/// Current page of results
		/// </summary>
		private WmiObject?[] _results;
		/// <summary>
		/// Index of next result
		/// </summary>
		private int _readIndex;

		private WmiObject? _current;
		/// <summary>
		/// Gets the current object.
		/// </summary>
		/// <remarks>
		/// Call <see cref="ReadAsync(CancellationToken)"/> to fetch the next object.
		/// Retrieving this property before the first call to <see cref="ReadAsync(CancellationToken)"/>
		/// returns <see langword="null"/> but does not cause an error.
		/// </remarks>
		public WmiObject? Current => this._current;

		/// <summary>
		/// Reads the next object from the query.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns><see langword="true"/> if </returns>
		public ValueTask<bool> ReadAsync(CancellationToken cancellationToken)
			=> this.ReadAsync(TimeSpan.FromSeconds(1), cancellationToken);
		/// <summary>
		/// Reads the next object from the query.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <param name="pollingInterval">Amount of time to wait between calls</param>
		/// <returns><see langword="true"/> if </returns>
		public ValueTask<bool> ReadAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			if (this._resultCount < 0)
				// Current page is empty
				return ValueTask.FromResult(false);

			if (this._readIndex < this._resultCount)
			{
				// Next result is contained in current page
				this._current = this._results[this._readIndex];
				this._readIndex++;
				return ValueTask.FromResult(true);
			}
			else
			{
				// Fetch the next page
				return this.FetchNextPage(pollingInterval, cancellationToken);
			}
		}

		private async ValueTask<bool> FetchNextPage(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			DceRpc.RpcPointer<uint> puReturned = new DceRpc.RpcPointer<uint>();
			DceRpc.RpcPointer<ArraySegment<DceRpc.TypedObjref<IWbemClassObject>>> apObjects = new DceRpc.RpcPointer<ArraySegment<DceRpc.TypedObjref<IWbemClassObject>>>();
			WBEMSTATUS res;
			do
			{
				res = (WBEMSTATUS)await this._enumObj.Next(
					0,
					(uint)this._results.Length,
					apObjects,
					puReturned,
					cancellationToken
					).ConfigureAwait(false);
				if (res == WBEMSTATUS.WBEM_S_TIMEDOUT)
					await Task.Delay(pollingInterval, cancellationToken).ConfigureAwait(false);
				else
					break;
			} while (true);

			if (res < 0)
			{
				// TODO: Check for error and return to caller
				this._resultCount = -1;
				this._current = null;
				return false;
			}
			else
			{
				var resultCount = (int)puReturned.value;
				if (resultCount == 0)
				{
					this._resultCount = -1;
					this._current = null;
					return false;
				}
				else
				{
					Debug.Assert(resultCount <= this._results.Length);
					for (int i = 0; i < resultCount; i++)
					{
						var objref = apObjects.value[i];
						try
						{
							var wmiobj = (WmiObject)await objref.Unwrap(this._wmi.dcomClient, cancellationToken).ConfigureAwait(false);
							wmiobj.Scope = this._scope;
							this._results[i] = wmiobj;
						}
						catch (Exception ex)
						{
							// TODO: Log exception
							this._results[i] = null;
						}
					}

					this._resultCount = resultCount;
					this._current = this._results[0];
					this._readIndex = 1;
					return true;
				}
			}
		}
	}
}
