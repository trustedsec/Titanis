using ms_oaut;
using ms_wmi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.Msrpc.Msdcom;

namespace Titanis.Msrpc.Mswmi
{
	/// <summary>
	/// Represents a connection to a WMI namespace.
	/// </summary>
	/// <seealso cref="WmiClient.OpenNamespace(string, string, CancellationToken)"/>
	public class WmiScope
	{
		internal WmiScope(WmiClient wmiClient, IWbemServices ns)
		{
			this._wmi = wmiClient;
			this._ns = ns;
		}

		private WmiClient _wmi;
		private IWbemServices _ns;

		private static readonly RpcPointer<FLAGGED_WORD_BLOB> WqlString = DcomClient.MakeString("WQL");

		/// <summary>
		/// Executes a WQL query.
		/// </summary>
		/// <param name="query">WQL query</param>
		/// <param name="pageSize">Page size</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="WmiQueryReader"/> that may be used to read the results</returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<WmiQueryReader> ExecuteWqlQueryAsync(
			string query,
			int pageSize,
			CancellationToken cancellationToken
			)
		{
			if (string.IsNullOrEmpty(query)) throw new ArgumentException($"'{nameof(query)}' cannot be null or empty.", nameof(query));

			RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<TypedObjref<IEnumWbemClassObject>>();
			var res = (WBEMSTATUS)await this._ns.ExecQuery(
				WqlString,
				DcomClient.MakeString(query),
				0,
				null,
				ppEnum,
				cancellationToken
				).ConfigureAwait(false);
			WmiClient.CheckHResult(res);

			return new WmiQueryReader(
				this,
				this._wmi,
				await ppEnum.value.Unwrap(this._wmi.dcomClient, cancellationToken).ConfigureAwait(false),
				pageSize
				);
		}
		/// <summary>
		/// Executes a WQL query and returns the first result.
		/// </summary>
		/// <param name="query">WQL query</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="WmiQueryReader"/> that may be used to read the results</returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<WmiObject?> ExecuteWqlQuerySingleAsync(
			string query,
			CancellationToken cancellationToken
			)
		{
			var reader = await ExecuteWqlQueryAsync(query, 1, cancellationToken).ConfigureAwait(false);
			if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
			{
				return reader.Current;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// Executes a WQL notification query.
		/// </summary>
		/// <param name="query">WQL query</param>
		/// <param name="pageSize">Page size</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="WmiQueryReader"/> that may be used to read the results</returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<WmiQueryReader> ExecuteWqlNotificationQueryAsync(
			string query,
			int pageSize,
			CancellationToken cancellationToken
			)
		{
			if (string.IsNullOrEmpty(query)) throw new ArgumentException($"'{nameof(query)}' cannot be null or empty.", nameof(query));

			RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<TypedObjref<IEnumWbemClassObject>>();
			var res = (WBEMSTATUS)await this._ns.ExecNotificationQuery(
				WqlString,
				DcomClient.MakeString(query),
				(int)(WBEM_GENERIC_FLAG_TYPE.WBEM_FLAG_FORWARD_ONLY | WBEM_GENERIC_FLAG_TYPE.WBEM_FLAG_RETURN_IMMEDIATELY),
				null,
				ppEnum,
				cancellationToken
				).ConfigureAwait(false);
			WmiClient.CheckHResult(res);

			return new WmiQueryReader(
				this,
				this._wmi,
				await ppEnum.value.Unwrap(this._wmi.dcomClient, cancellationToken).ConfigureAwait(false),
				pageSize
				);
		}
		/// <summary>
		/// Executes a WQL notification query and returns the first result.
		/// </summary>
		/// <param name="query">WQL query</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="WmiQueryReader"/> that may be used to read the results</returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<WmiObject?> ExecuteWqlNotificationQuerySingleAsync(
			string query,
			CancellationToken cancellationToken
			)
		{
			var reader = await ExecuteWqlNotificationQueryAsync(query, 1, cancellationToken).ConfigureAwait(false);
			if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
			{
				return reader.Current;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// Deletes a class.
		/// </summary>
		/// <param name="className">Name of class to delete</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns><see langword="true"/> if a class named <paramref name="className"/> was found and deleted; <see langword="false"/> if no class was found</returns>
		/// <exception cref="ArgumentException"><paramref name="className"/> is <see langword="null"/> or empty.</exception>
		public async Task<bool> DeleteClass(string className, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(className)) throw new ArgumentException($"'{nameof(className)}' cannot be null or empty.", nameof(className));

			var res = (WBEMSTATUS)await this._ns.DeleteClass(DcomClient.MakeString(className), 0, null, null, cancellationToken).ConfigureAwait(false);
			if (res is WBEMSTATUS.WBEM_S_NO_ERROR)
				return true;
			else if (res is WBEMSTATUS.WBEM_E_NOT_FOUND)
				return false;
			else
				throw WmiClient.GetExceptionFor(res);
		}
		/// <summary>
		/// Puts a class in the WMI repository.
		/// </summary>
		/// <param name="klass">Class to create</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <remarks>
		/// Use <see cref="WmiClassBuilder"/> to build a class.
		/// </remarks>
		public async Task PutClass(WmiClassObject klass, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(klass);

			var classRef = this._wmi.dcomClient.Wrap<IWbemClassObject>(klass);
			var result = (WBEMSTATUS)await this._ns.PutClass(
				classRef,
				0,
				null,
				null,
				cancellationToken).ConfigureAwait(false);

			WmiClient.CheckHResult(result);
		}
		/// <summary>
		/// Puts an instance in the WMI repository.
		/// </summary>
		/// <param name="className"></param>
		/// <param name="properties"></param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task PutInstance(
			string className,
			Dictionary<string, object> properties,
			CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(className)) throw new ArgumentException($"'{nameof(className)}' cannot be null or empty.", nameof(className));
			ArgumentNullException.ThrowIfNull(properties);

			var klass = (WmiClassObject)await this.GetObjectAsync(className, cancellationToken).ConfigureAwait(false);
			var inst = klass.Instantiate(properties);
			await this.PutInstance(inst, cancellationToken).ConfigureAwait(false);
		}
		/// <summary>
		/// Puts an instance in the WMI repository.
		/// </summary>
		/// <param name="obj">Object instance</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <seealso cref="WmiClassObject.Instantiate(Dictionary{string, object}?)"/>
		public async Task PutInstance(WmiInstanceObject obj, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(obj);

			var instanceRef = this._wmi.dcomClient.Wrap<IWbemClassObject>(obj);
			var result = (WBEMSTATUS)await this._ns.PutInstance(instanceRef, (int)WBEM_GENERIC_FLAG_TYPE.WBEM_FLAG_USE_AMENDED_QUALIFIERS, null, null, cancellationToken).ConfigureAwait(false);
			WmiClient.CheckHResult(result);
		}
		/// <summary>
		/// Deletes an instance.
		/// </summary>
		/// <param name="path">Path of object to delete</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns><see langword="true"/> if an object at <paramref name="path"/> was found and deleted; <see langword="false"/> if no object was found</returns>
		/// <exception cref="ArgumentException"><paramref name="path"/> is <see langword="null"/> or empty.</exception>
		public async Task<bool> DeleteInstance(string path, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(path)) throw new ArgumentException($"'{nameof(path)}' cannot be null or empty.", nameof(path));
			var res = (WBEMSTATUS)await this._ns.DeleteInstance(DcomClient.MakeString(path), 0, null, null, cancellationToken).ConfigureAwait(false);
			if (res is WBEMSTATUS.WBEM_S_NO_ERROR)
				return true;
			else if (res is WBEMSTATUS.WBEM_E_NOT_FOUND)
				return false;
			else
				throw WmiClient.GetExceptionFor(res);
		}

		private ConcurrentDictionary<string, WmiClassObject> _classCache = new ConcurrentDictionary<string, WmiClassObject>();
		internal WmiClassObject? TryGetCachedClass(string name)
		{
			this._classCache.TryGetValue(name, out var cachedClass);
			return cachedClass;
		}

		/// <summary>
		/// Gets an object from the WMI repository.
		/// </summary>
		/// <param name="objectPath">Path of object to get</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>An instance of <see cref="WmiObject"/> representing the object.</returns>
		/// <exception cref="ArgumentException"><paramref name="objectPath"/> is <see langword="null"/> or empty.</exception>
		/// <remarks>
		/// The return object may be a <see cref="WmiInstanceObject"/> or <see cref="WmiClassObject"/>.
		/// </remarks>
		public async Task<WmiObject?> GetObjectAsync(string objectPath, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(objectPath)) throw new ArgumentException($"'{nameof(objectPath)}' cannot be null or empty.", nameof(objectPath));

			RpcPointer<TypedObjref<IWbemClassObject>> ppObject = new RpcPointer<TypedObjref<IWbemClassObject>>();
			RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult = new RpcPointer<TypedObjref<IWbemCallResult>>();
			var result = (WBEMSTATUS)await this._ns.GetObject(
				DcomClient.MakeString(objectPath),
				(int)WBEM_GENERIC_FLAG_TYPE.WBEM_FLAG_RETURN_WBEM_COMPLETE
				| (int)WBEM_GENERIC_FLAG_TYPE.WBEM_FLAG_USE_AMENDED_QUALIFIERS,
				null,
				ppObject,
				ppCallResult,
				cancellationToken
				).ConfigureAwait(false);
			WmiClient.CheckHResult(result);

			if (ppObject.value != null)
			{
				var obj = (WmiObject)await ppObject.value.Unwrap(this._wmi.dcomClient, cancellationToken).ConfigureAwait(false);
				obj.Scope = this;

				if (obj is WmiClassObject cls)
				{
					this._classCache.TryAdd(cls.Name, cls);
				}
				return obj;
			}

			if (ppCallResult.value != null)
			{
				var callResult = await ppCallResult.value.Unwrap(this._wmi.dcomClient, cancellationToken).ConfigureAwait(false);
				RpcPointer<int> plStatus = new RpcPointer<int>();
				result = (WBEMSTATUS)await callResult.GetCallStatus(-1, plStatus, cancellationToken).ConfigureAwait(false);
				WmiClient.CheckHResult((WBEMSTATUS)plStatus.value);

				RpcPointer<TypedObjref<IWbemClassObject>> ppResultObject = new RpcPointer<TypedObjref<IWbemClassObject>>();
				var resObj = await callResult.GetResultObject(-1, ppResultObject, cancellationToken).ConfigureAwait(false);
				var obj = (WmiObject)await ppResultObject.value.Unwrap(this._wmi.dcomClient, cancellationToken).ConfigureAwait(false);
				obj.Scope = this;

				if (obj is WmiClassObject cls)
				{
					this._classCache.TryAdd(cls.Name, cls);
				}

				return obj;
			}
			else
				return null;
		}

		/// <summary>
		/// Executes a method on an object.
		/// </summary>
		/// <param name="objectPath">Path to object</param>
		/// <param name="methodName">Name of method to invoke</param>
		/// <param name="inputArgs">Input arguments</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>A <see cref="WmiObject"/> containing the results of the method execution</returns>
		/// <exception cref="ArgumentException"><paramref name="objectPath"/> is <see langword="null"/> or empty</exception>
		/// <exception cref="ArgumentException"><paramref name="methodName"/> is <see langword="null"/> or empty</exception>
		/// <exception cref="ArgumentNullException"><paramref name="inputArgs"/> is <see langword="null"/></exception>
		public async Task<WmiInstanceObject> ExecuteMethodAsync(
			string objectPath,
			string methodName,
			WmiObject? inputArgs,
			CancellationToken cancellationToken
			)
		{
			if (string.IsNullOrEmpty(objectPath)) throw new ArgumentException($"'{nameof(objectPath)}' cannot be null or empty.", nameof(objectPath));
			if (string.IsNullOrEmpty(methodName)) throw new ArgumentException($"'{nameof(methodName)}' cannot be null or empty.", nameof(methodName));

			RpcPointer<TypedObjref<IWbemClassObject>> ppOutParams = new RpcPointer<TypedObjref<IWbemClassObject>>();
			var result = (WBEMSTATUS)await this._ns.ExecMethod(
				DcomClient.MakeString(objectPath),
				DcomClient.MakeString(methodName),
				0,
				null,
				(inputArgs == null) ? null : this._wmi.dcomClient.Wrap<IWbemClassObject>(inputArgs),
				ppOutParams,
				null,
				cancellationToken
				).ConfigureAwait(false);

			WmiClient.CheckHResult(result);

			var outParams = (WmiInstanceObject)await ppOutParams.value.Unwrap(this._wmi.dcomClient, cancellationToken).ConfigureAwait(false);
			return outParams;
		}
	}
}
