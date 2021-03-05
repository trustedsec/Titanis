using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Titanis.IO.Test
{
	[TestClass]
	public class PathHelperTest
	{
		[TestMethod]
		public void TestParseSearchPattern_Null()
		{
			string path = null;

			bool actual = PathHelper.TryParseSearchPattern(path, out string dirName, out string? pattern);
			Assert.IsFalse(actual);
			Assert.AreEqual(dirName, path);
			Assert.IsNull(pattern);
		}
		[TestMethod]
		public void TestParseSearchPattern_Empty()
		{
			string path = string.Empty;

			bool actual = PathHelper.TryParseSearchPattern(path, out string dirName, out string? pattern);
			Assert.IsFalse(actual);
			Assert.AreEqual(dirName, path);
			Assert.IsNull(pattern);
		}

		[TestMethod]
		public void TestParseSearchPattern_Dir()
		{
			string path = "dir1";

			bool actual = PathHelper.TryParseSearchPattern(path, out string dirName, out string? pattern);
			Assert.IsFalse(actual);
			Assert.AreEqual(dirName, path);
			Assert.IsNull(pattern);
		}

		[TestMethod]
		public void TestParseSearchPattern_Subdir()
		{
			string path = @"dir1\subdir";

			bool actual = PathHelper.TryParseSearchPattern(path, out string dirName, out string? pattern);
			Assert.IsFalse(actual);
			Assert.AreEqual(path, dirName);
			Assert.IsNull(pattern);
		}

		[TestMethod]
		public void TestParseSearchPattern_Mask()
		{
			string path = @"dir1\mask*";

			bool actual = PathHelper.TryParseSearchPattern(path, out string dirName, out string? pattern);
			Assert.IsTrue(actual);
			Assert.AreEqual(@"dir1", dirName);
			Assert.AreEqual("mask*", pattern);
		}

		[TestMethod]
		public void TestParseSearchPattern_Subdir_Mask()
		{
			string path = @"dir1\subdir\mask*";

			bool actual = PathHelper.TryParseSearchPattern(path, out string dirName, out string? pattern);
			Assert.IsTrue(actual);
			Assert.AreEqual(@"dir1\subdir", dirName);
			Assert.AreEqual("mask*", pattern);
		}

		[TestMethod]
		public void TestParseSearchPattern_NoDir_Mask()
		{
			string path = @"mask*";

			bool actual = PathHelper.TryParseSearchPattern(path, out string dirName, out string? pattern);
			Assert.IsTrue(actual);
			Assert.AreEqual(string.Empty, dirName);
			Assert.AreEqual("mask*", pattern);
		}
	}
}
