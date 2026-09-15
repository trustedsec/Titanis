using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mseven6.BinXml
{
	internal struct FrugalList<T> : IList<T>
	{
		public FrugalList(int capacity)
		{
			if (capacity > 3)
				this._list = new List<T>(capacity);
		}

		private T _item0;
		private T _item1;
		private T _item2;
		private List<T>? _list;

		public T this[int index]
		{
			get
			{
				return ((uint)index < this.Count)
					? (
						(this._list == null) ? index switch
						{
							0 => this._item0,
							1 => this._item1,
							2 => this._item2,
						}
						: this._list[index])
					: throw new IndexOutOfRangeException();
			}

			set
			{
				var res = ((uint)index < this.Count)
					? (
						(this._list == null) ? index switch
						{
							0 => (this._item0 = value),
							1 => (this._item1 = value),
							2 => (this._item2 = value),
						}
						: (this._list[index] = value))
					: throw new IndexOutOfRangeException();
			}
		}

		public int Count { get; private set; }

		public bool IsReadOnly => false;

		public void Add(T item)
		{
			if (this._list is null)
			{
				switch (this.Count)
				{
					case 0: this._item0 = item; break;
					case 1: this._item1 = item; break;
					case 2: this._item2 = item; break;
					default:
						this._list = [this._item0, this._item1, this._item2, item];
						break;
				}

			}
			else
			{
				this._list.Add(item);
			}
			this.Count++;
		}

		public void Clear()
		{
			this.Count = 0;
			this._item0 = this._item1 = this._item2 = default;
			this._list = null;
		}

		public bool Contains(T item)
		{
			if (this._list is null)
			{
				var comparer = EqualityComparer<T>.Default;
				return
					comparer.Equals(item, this._item0)
					|| comparer.Equals(item, this._item1)
					|| comparer.Equals(item, this._item2)
					;
			}
			else
			{
				return this._list.Contains(item);
			}
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (this._list is null)
			{
				if (this.Count > 0)
				{
					array[arrayIndex++] = this._item0;
					if (this.Count > 1)
					{
						array[arrayIndex++] = this._item1;
						if (this.Count > 2)
						{
							array[arrayIndex++] = this._item2;
						}
					}
				}
			}
			else
			{
				this._list.CopyTo(array, arrayIndex);
			}
		}

		public int IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		struct Enumerator : IEnumerator<T>
		{
			public Enumerator(FrugalList<T> list)
			{
				this.index = -1;
				this.list = list;
			}

			private readonly FrugalList<T> list;
			private int index;

			public T Current => this.list[this.index];
			object IEnumerator.Current => this.Current;

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				this.index++;
				return (this.index < this.list.Count);
			}

			public void Reset()
			{
				this.index = 0;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			if (this._list is null)
			{
				return new Enumerator(this);
			}
			else
			{
				return this._list.GetEnumerator();
			}
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
