using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.Cli.Registry;
using Titanis.Winterop.Registry;


namespace Wmi.Test
{
	[TestClass]
	public sealed class ExportHexTest
	{
		private RegistryEntry CreateRegistryEntry(string valueName, RegistryValueType regType, object data)
		{
			var regData = regType switch
			{
				RegistryValueType.String => RegistryData.CreateString((string)data),
				RegistryValueType.ExpandString => RegistryData.CreateExpandableString((string)data),
				RegistryValueType.Binary => RegistryData.CreateBinary((byte[])data),
				RegistryValueType.DwordLE => RegistryData.CreateDword((uint)data),
				RegistryValueType.MultiString => RegistryData.CreateRegMultiString((string[])data),
				RegistryValueType.Qword => RegistryData.CreateQword((ulong)data),
				_ => throw new NotSupportedException($"Registry type {regType} is not supported in this test."),
			};
			return new RegistryEntry(PredefinedKey.LocalMachine, "unusedPath", valueName, regData);
		}

		[TestMethod]
		[DataRow(RegistryValueType.String, "val1", "fun", "\"val1\"=\"fun\"\r\n")]
		[DataRow(RegistryValueType.Binary, "binval", new byte[] {
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
		[DataRow(RegistryValueType.Qword, "dword", 0x00005a2fU, "\"dword\"=dword:00005a2f\r\n")]
		[DataRow(RegistryValueType.Qword, "qword", 0x00000000005d2f3a4f2dUL, "\"qword\"=hex(b):2d,4f,3a,2f,5d,00,00,00\r\n")]
		[DataRow(RegistryValueType.MultiString, "multistr", new string[] { "this", "is", "a", "multi", "str" }, @"""multistr""=hex(7):74,00,68,00,69,00,73,00,00,00,69,00,73,00,00,00,61,00,00,00,\
  6d,00,75,00,6c,00,74,00,69,00,00,00,73,00,74,00,72,00,00,00,00,00
")]
		[DataRow(RegistryValueType.ExpandString, "expand", "%systemroot%\\fun\\time", @"""expand""=hex(2):25,00,73,00,79,00,73,00,74,00,65,00,6d,00,72,00,6f,00,6f,00,74,\
  00,25,00,5c,00,66,00,75,00,6e,00,5c,00,74,00,69,00,6d,00,65,00,00,00
")]
		[DataRow(RegistryValueType.Binary, "thisisabinaryvaluewithquitealongnamehowaboutthat", new byte[] {
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
		[DataRow(RegistryValueType.String, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "happy day nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooowwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww", "\"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\"=\"happy day nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooowwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww\"\r\n")]
		[DataRow(RegistryValueType.String, "so\\me\"key", "", "\"so\\\\me\\\"key\"=\"\"\r\n")]
		[DataRow(RegistryValueType.Qword, "really", 0x0000000000000014UL, "\"really\"=hex(b):14,00,00,00,00,00,00,00\r\n")]
		[DataRow(RegistryValueType.String, "empty", "", "\"empty\"=\"\"\r\n")]
		//		[DataRow(RegistryType.REG_BINARY, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb", new byte[0], "\"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb\"=hex:\r\n")]
		//		[DataRow(RegistryType.REG_BINARY, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", new byte[] { 0xab, 0x32, 0x12 }, "\"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\"=hex:ab,\\\r\n  32,12\r\n")]
		[DataRow(RegistryValueType.ExpandString, "temp", "%temp%thing", @"""temp""=hex(2):25,00,74,00,65,00,6d,00,70,00,25,00,74,00,68,00,69,00,6e,00,67,\
  00,00,00
")]
		[DataRow(RegistryValueType.ExpandString, "onedrive", "%onedrive%", @"""onedrive""=hex(2):25,00,6f,00,6e,00,65,00,64,00,72,00,69,00,76,00,65,00,25,00,\
  00,00
")]
		[DataRow(RegistryValueType.ExpandString, "com", "%comspec%", @"""com""=hex(2):25,00,63,00,6f,00,6d,00,73,00,70,00,65,00,63,00,25,00,00,00
")]
		[DataRow(RegistryValueType.ExpandString, "emptryexpand", "", "\"emptryexpand\"=hex(2):00,00\r\n")]
		public void TestHexExport(RegistryValueType regType, string valueName, object data, string expected)
		{
			var entry = CreateRegistryEntry(valueName, regType, data);
			using (MemoryStream ms = new MemoryStream())
			{
				using (TextWriter writer = new StreamWriter(ms))
				{
					entry?.Data.ExportTo(writer, 0);
				}
				var output = ms.ToArray();
				var exported = Encoding.Unicode.GetString(output);
				Assert.AreEqual(expected, exported);
			}
		}
	}
}
