using System.Runtime.InteropServices;
using Titanis.Winterop.Registry;
using Wmi.Registry;

namespace Wmi.Test
{
	[TestClass]
	public sealed class ExportHexTest
	{
		private RegistryEntry CreateRegistryEntry(string valueName, RegistryValueKind regType, object data)
		{
			var regData = regType switch
			{
				RegistryValueKind.REG_SZ => RegistryData.CreateString((string)data),
				RegistryValueKind.REG_EXPAND_SZ => RegistryData.CreateExpandableString((string)data),
				RegistryValueKind.REG_BINARY => RegistryData.CreateBinary((byte[])data),
				RegistryValueKind.REG_DWORD => RegistryData.CreateDword((uint)data),
				RegistryValueKind.REG_MULTI_SZ => RegistryData.CreateRegMultiString((string[])data),
				RegistryValueKind.REG_QWORD => RegistryData.CreateDword((ulong)data),
				_ => throw new NotSupportedException($"Registry type {regType} is not supported in this test."),
			};
			return new RegistryEntry(PredefinedKey.HKEY_LOCAL_MACHINE, "unusedPath", valueName, regData);
		}

		[TestMethod]
		[DataRow(RegistryValueKind.REG_SZ, "val1", "fun", "\"val1\"=\"fun\"\r\n")]
		[DataRow(RegistryValueKind.REG_BINARY, "binval", new byte[] {
  0x01,0x02,0x03,0x04,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x00,0x10,0x10,0x10,0x10,0x10,0x10,0x10,
  0x10,0x10,0x10,0x10,0x10,0x10,0x10,0x10,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
  0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
  0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
  0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
  0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
		}, @"""binval""=hex:01,02,03,04,01,01,01,01,01,01,01,01,01,01,00,10,10,10,10,10,10,10,\
  10,10,10,10,10,10,10,10,01,01,01,01,01,01,01,01,01,00,00,00,00,00,00,00,00,\
  00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,\
  00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,\
  00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,00,\
  00,00,00,00,00,00,00,00,00,00,00,00,00,00,00
")]
		[DataRow(RegistryValueKind.REG_DWORD, "dword", 0x00005a2fU, "\"dword\"=dword:00005a2f\r\n")]
		[DataRow(RegistryValueKind.REG_QWORD, "qword", 0x00000000005d2f3a4f2dUL, "\"qword\"=hex(b):2d,4f,3a,2f,5d,00,00,00\r\n")]
		[DataRow(RegistryValueKind.REG_MULTI_SZ, "multistr", new string[] { "this", "is", "a", "multi", "str" }, @"""multistr""=hex(7):74,00,68,00,69,00,73,00,00,00,69,00,73,00,00,00,61,00,00,00,\
  6d,00,75,00,6c,00,74,00,69,00,00,00,73,00,74,00,72,00,00,00,00,00
")]
		[DataRow(RegistryValueKind.REG_EXPAND_SZ, "expand", "%systemroot%\\fun\\time", @"""expand""=hex(2):25,00,73,00,79,00,73,00,74,00,65,00,6d,00,72,00,6f,00,6f,00,74,\
  00,25,00,5c,00,66,00,75,00,6e,00,5c,00,74,00,69,00,6d,00,65,00,00,00
")]
		[DataRow(RegistryValueKind.REG_BINARY, "thisisabinaryvaluewithquitealongnamehowaboutthat", new byte[] {
					0x43,0x30,0x30,0x30,0x30,0x30,0x30,0x30,
  0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,
  0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,
  0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,
  0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,
  0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,
  0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30,0x30
				}, @"""thisisabinaryvaluewithquitealongnamehowaboutthat""=hex:43,30,30,30,30,30,30,30,\
  30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,\
  30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,\
  30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,\
  30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,\
  30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,\
  30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30
")]
		[DataRow(RegistryValueKind.REG_SZ, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "happy day nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooowwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww", "\"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\"=\"happy day nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooowwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww\"\r\n")]
		[DataRow(RegistryValueKind.REG_SZ, "so\\me\"key", "", "\"so\\\\me\\\"key\"=\"\"\r\n")]
		[DataRow(RegistryValueKind.REG_QWORD, "really", 0x0000000000000014UL, "\"really\"=hex(b):14,00,00,00,00,00,00,00\r\n")]
		[DataRow(RegistryValueKind.REG_SZ, "empty", "", "\"empty\"=\"\"\r\n")]
		//		[DataRow(RegistryType.REG_BINARY, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb", new byte[0], "\"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb\"=hex:\r\n")]
		//		[DataRow(RegistryType.REG_BINARY, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", new byte[] { 0xab, 0x32, 0x12 }, "\"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\"=hex:ab,\\\r\n  32,12\r\n")]
		[DataRow(RegistryValueKind.REG_EXPAND_SZ, "temp", "%temp%thing", @"""temp""=hex(2):25,00,74,00,65,00,6d,00,70,00,25,00,74,00,68,00,69,00,6e,00,67,\
  00,00,00
")]
		[DataRow(RegistryValueKind.REG_EXPAND_SZ, "onedrive", "%onedrive%", @"""onedrive""=hex(2):25,00,6f,00,6e,00,65,00,64,00,72,00,69,00,76,00,65,00,25,00,\
  00,00
")]
		[DataRow(RegistryValueKind.REG_EXPAND_SZ, "com", "%comspec%", @"""com""=hex(2):25,00,63,00,6f,00,6d,00,73,00,70,00,65,00,63,00,25,00,00,00
")]
		[DataRow(RegistryValueKind.REG_EXPAND_SZ, "emptryexpand", "", "\"emptryexpand\"=hex(2):00,00\r\n")]
		public void TestHexExport(RegistryValueKind regType, string valueName, object data, string expected)
		{
			var entry = CreateRegistryEntry(valueName, regType, data);
			var exported = entry.GetExportString();
			Assert.AreEqual(expected, exported);
		}
	}
}
