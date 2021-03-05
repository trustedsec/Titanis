using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Titanis
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
	public static class HashCode
	{
		public const int InitialValue = -1185072511;
		public const int Coeff = -1521134295;
		public static int CombineValues(int x, int y)
		{
			return x * Coeff + y;
		}

		public static int Combine<T1, T2>(T1 x, T2 y)
		{
			int hashCode = InitialValue;
			hashCode = CombineValues(hashCode, EqualityComparer<T1>.Default.GetHashCode(x));
			hashCode = CombineValues(hashCode, EqualityComparer<T2>.Default.GetHashCode(y));
			return hashCode;
		}

		public static int Combine<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
		{
			int hashCode = InitialValue;
			hashCode = CombineValues(hashCode, EqualityComparer<T1>.Default.GetHashCode(a1));
			hashCode = CombineValues(hashCode, EqualityComparer<T2>.Default.GetHashCode(a2));
			hashCode = CombineValues(hashCode, EqualityComparer<T3>.Default.GetHashCode(a3));
			return hashCode;
		}

		public static int Combine<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
		{
			int hashCode = InitialValue;
			hashCode = CombineValues(hashCode, EqualityComparer<T1>.Default.GetHashCode(a1));
			hashCode = CombineValues(hashCode, EqualityComparer<T2>.Default.GetHashCode(a2));
			hashCode = CombineValues(hashCode, EqualityComparer<T3>.Default.GetHashCode(a3));
			hashCode = CombineValues(hashCode, EqualityComparer<T4>.Default.GetHashCode(a4));
			return hashCode;
		}

		public static int Combine<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
		{
			int hashCode = InitialValue;
			hashCode = CombineValues(hashCode, EqualityComparer<T1>.Default.GetHashCode(a1));
			hashCode = CombineValues(hashCode, EqualityComparer<T2>.Default.GetHashCode(a2));
			hashCode = CombineValues(hashCode, EqualityComparer<T3>.Default.GetHashCode(a3));
			hashCode = CombineValues(hashCode, EqualityComparer<T4>.Default.GetHashCode(a4));
			hashCode = CombineValues(hashCode, EqualityComparer<T5>.Default.GetHashCode(a5));
			return hashCode;
		}

		public static int Combine<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
		{
			int hashCode = InitialValue;
			hashCode = CombineValues(hashCode, EqualityComparer<T1>.Default.GetHashCode(a1));
			hashCode = CombineValues(hashCode, EqualityComparer<T2>.Default.GetHashCode(a2));
			hashCode = CombineValues(hashCode, EqualityComparer<T3>.Default.GetHashCode(a3));
			hashCode = CombineValues(hashCode, EqualityComparer<T4>.Default.GetHashCode(a4));
			hashCode = CombineValues(hashCode, EqualityComparer<T5>.Default.GetHashCode(a5));
			hashCode = CombineValues(hashCode, EqualityComparer<T6>.Default.GetHashCode(a6));
			return hashCode;
		}

		public static int Combine<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
		{
			int hashCode = InitialValue;
			hashCode = CombineValues(hashCode, EqualityComparer<T1>.Default.GetHashCode(a1));
			hashCode = CombineValues(hashCode, EqualityComparer<T2>.Default.GetHashCode(a2));
			hashCode = CombineValues(hashCode, EqualityComparer<T3>.Default.GetHashCode(a3));
			hashCode = CombineValues(hashCode, EqualityComparer<T4>.Default.GetHashCode(a4));
			hashCode = CombineValues(hashCode, EqualityComparer<T5>.Default.GetHashCode(a5));
			hashCode = CombineValues(hashCode, EqualityComparer<T6>.Default.GetHashCode(a6));
			hashCode = CombineValues(hashCode, EqualityComparer<T7>.Default.GetHashCode(a7));
			return hashCode;
		}

		public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
		{
			int hashCode = InitialValue;
			hashCode = CombineValues(hashCode, EqualityComparer<T1>.Default.GetHashCode(a1));
			hashCode = CombineValues(hashCode, EqualityComparer<T2>.Default.GetHashCode(a2));
			hashCode = CombineValues(hashCode, EqualityComparer<T3>.Default.GetHashCode(a3));
			hashCode = CombineValues(hashCode, EqualityComparer<T4>.Default.GetHashCode(a4));
			hashCode = CombineValues(hashCode, EqualityComparer<T5>.Default.GetHashCode(a5));
			hashCode = CombineValues(hashCode, EqualityComparer<T6>.Default.GetHashCode(a6));
			hashCode = CombineValues(hashCode, EqualityComparer<T7>.Default.GetHashCode(a7));
			hashCode = CombineValues(hashCode, EqualityComparer<T8>.Default.GetHashCode(a8));
			return hashCode;
		}

		public static int Combine<T>(T[] values)
		{
			if (values.IsNullOrEmpty())
				return 0;
			else
			{
				int hashCode = InitialValue;
				for (int i = 0; i < values.Length; i++)
				{
					hashCode = CombineValues(hashCode, EqualityComparer<T>.Default.GetHashCode(values[i]));
				}
				return hashCode;
			}
		}
	}
}
