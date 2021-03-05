using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Titanis.Smb2.Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestUncPath_ServerName()
		{
			string path = @"\\server";

			var actual = Smb2UncPathInfo.Parse(path);
			Assert.AreEqual(path, actual.OriginalString);
			Assert.AreEqual(path, actual.ToString());
			Assert.AreEqual("server", actual.ServerName);
			Assert.AreEqual(null, actual.ShareName);
			Assert.AreEqual(null, actual.Path);
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestUncPath_Invalid()
		{
			string path = @"server";

			var actual = Smb2UncPathInfo.Parse(path);
		}
		[TestMethod]
		public void TestUncPath_ServerShareName()
		{
			string path = @"\\server\share";

			var actual = Smb2UncPathInfo.Parse(path);
			Assert.AreEqual(path, actual.OriginalString);
			Assert.AreEqual(path, actual.ToString());
			Assert.AreEqual("server", actual.ServerName);
			Assert.AreEqual("share", actual.ShareName);
			Assert.AreEqual(null, actual.Path);
		}
		[TestMethod]
		public void TestUncPath_ServerShareNamePath()
		{
			string path = @"\\server\share\path\file";

			var actual = Smb2UncPathInfo.Parse(path);
			Assert.AreEqual(path, actual.OriginalString);
			Assert.AreEqual(path, actual.ToString());
			Assert.AreEqual("server", actual.ServerName);
			Assert.AreEqual("share", actual.ShareName);
			Assert.AreEqual(@"path\file", actual.Path);
		}
		[TestMethod]
		public void TestUncPath_ServerShareNamePath_AltSeparator()
		{
			string path = @"//server/share/path/file";

			var actual = Smb2UncPathInfo.Parse(path);
			Assert.AreEqual(path, actual.OriginalString);
			Assert.AreEqual(path, actual.ToString());
			Assert.AreEqual("server", actual.ServerName);
			Assert.AreEqual("share", actual.ShareName);
			Assert.AreEqual(@"path\file", actual.Path);
		}
		[TestMethod]
		public void TestUncPath_ServerShareNamePath_MixedSeparator()
		{
			string path = @"//server\share/path\file";

			var actual = Smb2UncPathInfo.Parse(path);
			Assert.AreEqual(path, actual.OriginalString);
			Assert.AreEqual(path, actual.ToString());
			Assert.AreEqual("server", actual.ServerName);
			Assert.AreEqual("share", actual.ShareName);
			Assert.AreEqual(@"path\file", actual.Path);
		}
	}
}
