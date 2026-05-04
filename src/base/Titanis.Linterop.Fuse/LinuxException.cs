using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Titanis.Winterop;

namespace Titanis.Linterop.Fuse
{
	public class LinuxException : Exception
	{
		private static string GetMessage(LinuxErrorCode errorCode)
		{
			return errorCode.ToString();
		}

		public LinuxException(LinuxErrorCode errorCode, string? message = null)
			: base(message ?? GetMessage(errorCode))
		{
			ErrorCode = errorCode;
		}

		public LinuxErrorCode ErrorCode { get; }

		public static LinuxErrorCode FromNtstatus(Ntstatus ntstatus)
		{
			return ntstatus switch
			{
				Ntstatus.STATUS_NO_SUCH_FILE => LinuxErrorCode.ENOENT,
				Ntstatus.STATUS_ACCESS_DENIED => LinuxErrorCode.EACCES,
				Ntstatus.STATUS_INVALID_PARAMETER => LinuxErrorCode.EINVAL,
				Ntstatus.STATUS_OBJECT_NAME_NOT_FOUND => LinuxErrorCode.ENOENT,
				Ntstatus.STATUS_OBJECT_PATH_NOT_FOUND => LinuxErrorCode.ENOENT,
				Ntstatus.STATUS_NOT_IMPLEMENTED => LinuxErrorCode.ENOSYS,
				Ntstatus.STATUS_NOT_SUPPORTED => LinuxErrorCode.ENOSYS,
				Ntstatus.STATUS_TIMEOUT => LinuxErrorCode.ETIME,
				_ => LinuxErrorCode.EIO
			};
		}

		public static LinuxErrorCode FromHresult(Hresult hresult)
		{
			return hresult switch
			{
				_ => LinuxErrorCode.EIO
			};
		}

		public static LinuxErrorCode GetErrorCodeForException(Exception ex)
		{
			while (ex is AggregateException agg)
				ex = agg.InnerException;

			if (ex is LinuxException linex)
				return linex.ErrorCode;
			else if (ex is NtstatusException ntex)
				return FromNtstatus(ntex.StatusCode);
			else if (ex is ArgumentException)
				return LinuxErrorCode.EINVAL;
			else if (ex is FileNotFoundException)
				return LinuxErrorCode.ENOENT;
			else if (ex is KeyNotFoundException)
				return LinuxErrorCode.ENOENT;
			else if (ex is SecurityException)
				return LinuxErrorCode.EPERM;
			else if (ex is NotImplementedException)
				return LinuxErrorCode.ENOSYS;
			else if (ex is TimeoutException)
				return LinuxErrorCode.ETIME;
			else
				return FromHresult((Hresult)ex.HResult);

			//else if (ex is NotSupportedException)
			//	return LinuxErrorCode.ENOSPC;
		}
	}
}
