using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Winterop
{
	/// <summary>
	/// Provides extensions for status values.
	/// </summary>
	public static class NtstatusExtensions
	{
		/// <summary>
		/// Checks a <see cref="Win32ErrorCode"/> and throws the appropriate exception.
		/// </summary>
		/// <param name="errorCode">Error code</param>
		/// <returns>The original value of <paramref name="errorCode"/></returns>
		/// <exception cref="Win32Exception">Exception indicated by <paramref name="errorCode"/></exception>
		public static Win32ErrorCode CheckAndThrow(this Win32ErrorCode errorCode)
		{
			if (errorCode != 0)
				throw errorCode.GetException();

			return errorCode;
		}

		/// <summary>
		/// Creates an <see cref="Exception"/> corresponding to the specified <see cref="Win32ErrorCode"/>.
		/// </summary>
		/// <param name="errorCode">Win32 error code</param>
		/// <returns>An <see cref="Exception"/> object corresponding to <paramref name="errorCode"/></returns>
		public static Exception GetException(this Win32ErrorCode errorCode)
		{
			string message = GetMessageForCode(errorCode, (uint)errorCode, Win32MessageTable.ResourceManager);
			switch (errorCode)
			{
				default:
					return new Win32Exception((int)errorCode, message);
			}
		}

		public static string GetErrorMessage(this Win32ErrorCode errorCode)
			=> GetMessageForCode(errorCode, (uint)errorCode, Win32MessageTable.ResourceManager);

		/// <summary>
		/// Gets the message for an error code from a resource table.
		/// </summary>
		/// <typeparam name="TEnum">Type of error code</typeparam>
		/// <param name="errorCode">Error code</param>
		/// <param name="uval">Error code as a uint</param>
		/// <param name="resources">Resource manager</param>
		/// <returns>A string suitable to use for <see cref="Exception.Message"/></returns>
		/// <remarks>
		/// Why pass the error code as both <typeparamref name="TEnum"/> and <see cref="uint"/>?
		/// <paramref name="uval"/> is formatted to become part of the string.
		/// <typeparamref name="TEnum"/> may or not be based on <see langword="uint"/>, so casting it
		/// may fail.  Although <see cref="Enum"/> implements <see cref="IConvertible"/>, if it is a
		/// negative value, the conversion to <see langword="uint"/> fails.  Requiring both parameters
		/// causes the value to be converted at the call site, and this method doesn't have to come
		/// up with a fancy way of doing the conversion.
		/// </remarks>
		private static string GetMessageForCode<TEnum>(TEnum errorCode, uint uval, ResourceManager resources)
			where TEnum : struct, Enum
		{
			string message;
			if (Enum.IsDefined(typeof(TEnum), errorCode))
			{
				var key = errorCode.ToString();
				message = resources.GetString(key);
				if (message == null)
					message = $"No message found for {typeof(TEnum).Name}.{errorCode} (code=0x{uval:X8})";
			}
			else
			{
				message = $"No message found for {typeof(TEnum).Name} error code=0x{uval:X8}";
			}

			return message;
		}

		/// <summary>
		/// Checks an NTSTATUS value and throws if it indicates an error.
		/// </summary>
		/// <param name="status">NTSTATUS value</param>
		/// <exception cref="NtstatusException"><paramref name="status"/> indicates an error</exception>
		/// <returns>The original value of <paramref name="status"/></returns>
		public static Ntstatus CheckAndThrow(this Ntstatus status)
		{
			if ((int)status < 0)
				throw status.GetException();

			return status;
		}

		public static string GetErrorMessage(this Ntstatus ntstatus)
			=> GetMessageForCode(ntstatus, (uint)ntstatus, NtstatusMessageTable.ResourceManager);

		/// <summary>
		/// Creates an <see cref="Exception"/> corresponding to the specified <see cref="Ntstatus"/>.
		/// </summary>
		/// <param name="status">NTSTATUS</param>
		/// <returns>An <see cref="Exception"/> object corresponding to <paramref name="status"/></returns>
		public static NtstatusException GetException(this Ntstatus status)
		{
			return new NtstatusException(status, GetMessageForCode(status, (uint)status, NtstatusMessageTable.ResourceManager));
		}

		public static string GetErrorMessage(this Hresult hres)
			=> GetMessageForCode(hres, (uint)hres, HresultMessageTable.ResourceManager);

		/// <summary>
		/// Checks an HRESULT value and throws if it indicates an error.
		/// </summary>
		/// <param name="hr">HRESULT value</param>
		/// <returns>The original value of <paramref name="hr"/></returns>
		public static Hresult CheckAndThrow(this Hresult hr)
		{
			if ((int)hr < 0)
				throw hr.GetException();

			return hr;
		}

		/// <summary>
		/// Creates an <see cref="Exception"/> corresponding to the specified <see cref="Hresult"/>.
		/// </summary>
		/// <param name="hr">HRESULT</param>
		/// <returns>An <see cref="Exception"/> object corresponding to <paramref name="hr"/></returns>
		public static Exception GetException(this Hresult hr)
		{
			return new COMException(GetMessageForCode(hr, (uint)hr, HresultMessageTable.ResourceManager), (int)hr);
		}
	}
}
