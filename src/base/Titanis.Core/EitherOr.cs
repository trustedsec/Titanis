using System;
using System.Diagnostics.CodeAnalysis;

namespace Titanis
{
	public delegate void ActionRef<T>(ref T value);
	public delegate TResult FuncRef<T, TResult>(ref T value);

	/// <summary>
	/// Describes a value that may be one of two different types.
	/// </summary>
	/// <typeparam name="T1">First type option</typeparam>
	/// <typeparam name="T2">Second type option</typeparam>
	/// <remarks>
	/// The actual value is not exposed directly.  Use
	/// <see cref="Apply(Action{T1}, Action{T2})"/> (or one of its overloads)
	/// to access the value.
	/// </remarks>
	public struct EitherOr<T1, T2>
	{
		[AllowNull]
		internal T1 alt1;
		[AllowNull]
		internal T2 alt2;

		internal int alt;

		public EitherOr(T1 value)
		{
			this.alt1 = value;
			this.alt2 = default(T2);
			this.alt = 1;
		}
		public EitherOr(T2 value)
		{
			this.alt1 = default(T1);
			this.alt2 = value;
			this.alt = 2;
		}

		public static implicit operator EitherOr<T1, T2>(T1 value)
			=> new EitherOr<T1, T2>(value);
		public static implicit operator EitherOr<T1, T2>(T2 value)
			=> new EitherOr<T1, T2>(value);

		/// <summary>
		/// Gets a value indicating whether a value is present.
		/// </summary>
		public bool HasValue => this.alt != 0;

		public void Apply(
			Action<T1> action1,
			Action<T2> action2
			)
		{
			switch (this.alt)
			{
				case 1:
					ArgValidation.IsNonNull(action1)(this.alt1);
					break;
				case 2:
					ArgValidation.IsNonNull(action2)(this.alt2);
					break;
			}
		}

		public void Apply(
			ActionRef<T1> action1,
			ActionRef<T2> action2
			)
		{
			switch (this.alt)
			{
				case 1:
					ArgValidation.IsNonNull(action1)(ref this.alt1);
					break;
				case 2:
					ArgValidation.IsNonNull(action2)(ref this.alt2);
					break;
			}
		}


		public TResult? Apply<TResult>(
			Func<T1, TResult> action1,
			Func<T2, TResult> action2
			) => this.alt switch
			{
				1 => ArgValidation.IsNonNull(action1)(this.alt1),
				2 => ArgValidation.IsNonNull(action2)(this.alt2),
				_ => default(TResult),// Should never happen
			};

		public TResult? Apply<TResult>(
			FuncRef<T1, TResult> action1,
			FuncRef<T2, TResult> action2
			)
		{
			switch (this.alt)
			{
				case 1:
					return ArgValidation.IsNonNull(action1)(ref this.alt1);
				case 2:
					return ArgValidation.IsNonNull(action2)(ref this.alt2);
				default:
					// Should never happen
					return default(TResult);
			}
		}
	}

	/// <summary>
	/// Describes a value that may be one of two different types.
	/// </summary>
	/// <typeparam name="T1">First type option</typeparam>
	/// <typeparam name="T2">Second type option</typeparam>
	/// <typeparam name="T3">Third type option</typeparam>
	/// <remarks>
	/// The actual value is not exposed directly.  Use
	/// <see cref="Apply(Action{T1}, Action{T2}, Action{T3})"/> (or one of its overloads)
	/// to access the value.
	/// </remarks>
	public struct EitherOr<T1, T2, T3>
	{
		[AllowNull]
		internal T1 alt1;
		[AllowNull]
		internal T2 alt2;
		[AllowNull]
		internal T3 alt3;
		internal int alt;

		public EitherOr(T1 value)
		{
			this.alt1 = value;
			this.alt2 = default(T2);
			this.alt3 = default(T3);
			this.alt = 1;
		}
		public EitherOr(T2 value)
		{
			this.alt1 = default(T1);
			this.alt2 = value;
			this.alt3 = default(T3);
			this.alt = 2;
		}
		public EitherOr(T3 value)
		{
			this.alt1 = default(T1);
			this.alt2 = default(T2);
			this.alt3 = value;
			this.alt = 3;
		}

		public static implicit operator EitherOr<T1, T2, T3>(T1 value)
			=> new EitherOr<T1, T2, T3>(value);
		public static implicit operator EitherOr<T1, T2, T3>(T2 value)
			=> new EitherOr<T1, T2, T3>(value);
		public static implicit operator EitherOr<T1, T2, T3>(T3 value)
			=> new EitherOr<T1, T2, T3>(value);

		/// <summary>
		/// Gets a value indicating whether a value is present.
		/// </summary>
		public bool HasValue => this.alt != 0;

		public void Apply(
			Action<T1> action1,
			Action<T2> action2,
			Action<T3> action3
			)
		{
			switch (this.alt)
			{
				case 1:
					action1(this.alt1);
					break;
				case 2:
					action2(this.alt2);
					break;
				case 3:
					action3(this.alt3);
					break;
			}
		}

		public void Apply(
			ActionRef<T1> action1,
			ActionRef<T2> action2,
			ActionRef<T3> action3
			)
		{
			switch (this.alt)
			{
				case 1:
					action1(ref this.alt1);
					break;
				case 2:
					action2(ref this.alt2);
					break;
				case 3:
					action3(ref this.alt3);
					break;
			}
		}



		public TResult? Apply<TResult>(
			Func<T1, TResult?> action1,
			Func<T2, TResult?> action2,
			Func<T3, TResult?> action3
			) => this.alt switch
			{
				1 => action1(this.alt1),
				2 => action2(this.alt2),
				3 => action3(this.alt3),
				_ => default(TResult),// Should never happen
			};

		public TResult? Apply<TResult>(
			FuncRef<T1, TResult?> action1,
			FuncRef<T2, TResult?> action2,
			FuncRef<T3, TResult?> action3
			) => this.alt switch
			{
				1 => action1(ref this.alt1),
				2 => action2(ref this.alt2),
				3 => action3(ref this.alt3),
				_ => default(TResult),// Should never happen
			};
	}
}
