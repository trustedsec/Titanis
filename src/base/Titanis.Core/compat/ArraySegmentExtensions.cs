using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Titanis
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
	public static class ArraySegmentExtensions
	{
		public static ref T Item<T>(this ArraySegment<T> segment, int index)
		{
			return ref segment.Array[segment.Offset + index];
		}
	}
}
