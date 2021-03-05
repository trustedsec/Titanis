using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Titanis.Winterop.Test;

[TestClass]
public sealed class ErrorCodeTests
{
	[TestMethod]
	public void Win32ErrorTest()
	{
		var ex = Win32ErrorCode.ERROR_INVALID_OPERATION.GetException();
		Assert.IsInstanceOfType<Win32Exception>(ex);
	}
	[TestMethod]
	public void Win32Error_NonUintTest()
	{
		var ex = ((Win32ErrorCode)(-1)).GetException();
		Assert.IsInstanceOfType<Win32Exception>(ex);
	}
	[TestMethod]
	public void HresultTest()
	{
		var ex = Hresult.E_INVALIDARG.GetException();
		Assert.IsInstanceOfType<COMException>(ex);
	}
	[TestMethod]
	public void NtstatusTest()
	{
		var ex = Ntstatus.STATUS_INVALID_PARAMETER.GetException();
		Assert.IsInstanceOfType<NtstatusException>(ex);
	}
}
