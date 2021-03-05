
namespace Titanis.DceRpc
{
	/// <summary>
	/// Represents an object reference.
	/// </summary>
	/// <typeparam name="T">Interface type</typeparam>
	public class TypedObjref<T> where T : class, IRpcObject
	{
		public TypedObjref() { }
		public TypedObjref(byte[] marshalData)
		{
			if (marshalData is null) throw new ArgumentNullException(nameof(marshalData));
			this._marshalData = marshalData;
		}

		private byte[]? _marshalData;
		private volatile int _state;
		private Task<T>? _target;

		internal void SetMarshalData(byte[] data)
		{
			if (data is null) throw new ArgumentNullException(nameof(data));
			if (this._marshalData != null)
				throw new InvalidOperationException("Marshal data has already been set on this object.");

			this._marshalData = data;
		}

		public byte[]? TryGetMarshalData() => this._marshalData;
		public Task<T> Unwrap(IObjrefMarshal dcomClient, CancellationToken cancellationToken)
		{
			if (this._state == 2)
				return this._target!;

			if (this._marshalData == null)
				throw new InvalidOperationException("The reference is not initialized.");

			if (0 == Interlocked.CompareExchange(ref this._state, 1, 0))
			{
				this._target = dcomClient.Unwrap<T>(this._marshalData, cancellationToken);
				this._state = 2;
			}
			else
			{
				while (this._state != 2)
					;
			}

			return this._target!;
		}
	}
}