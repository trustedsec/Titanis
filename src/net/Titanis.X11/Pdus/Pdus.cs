using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.X11.Pdus
{
	[PduStruct]
	partial struct Window { internal uint value; }
	[PduStruct]
	partial struct Pixmap { internal uint value; }
	[PduStruct]
	partial struct Cursor { internal uint value; }
	[PduStruct]
	partial struct Font { internal uint value; }
	[PduStruct]
	partial struct GContext { internal uint value; }
	[PduStruct]
	partial struct Colormap { internal uint value; }
	// WINDOW or COLORMAP
	[PduStruct]
	partial struct Drawable { internal uint value; }
	// FONT or GCONTEXT
	[PduStruct]
	partial struct Fontable { internal uint value; }
	[PduStruct]
	partial struct Atom { internal uint value; }
	[PduStruct]
	partial struct VisualId { internal uint value; }
	[PduStruct]
	partial struct ListValue { internal uint value; }
	[PduStruct]
	partial struct Timestamp { internal uint value; }

	public enum BitGravity : byte
	{
		Forget,
		NorthWest,
		North,
		NorthEast,
		West,
		Center,
		East,
		SouthWest,
		South,
		SouthEast,
		Static,
	}

	public enum WinGravity : byte
	{
		Unmap,
		NorthWest,
		North,
		NorthEast,
		West,
		Center,
		East,
		SouthWest,
		South,
		SouthEast,
		Static,
	}

	[PduStruct]
	partial struct XBool
	{
		internal byte value;
		public static implicit operator bool(XBool n) => n.value != 0;
	}

	enum EventsMask : uint
	{
		KeyPress = 0x00000001,
		KeyRelease = 0x00000002,
		ButtonPress = 0x00000004,
		ButtonRelease = 0x00000008,
		EnterWindow = 0x00000010,
		LeaveWindow = 0x00000020,
		PointerMotion = 0x00000040,
		PointerMotionHint = 0x00000080,
		Button1Motion = 0x00000100,
		Button2Motion = 0x00000200,
		Button3Motion = 0x00000400,
		Button4Motion = 0x00000800,
		Button5Motion = 0x00001000,
		ButtonMotion = 0x00002000,
		KeymapState = 0x00004000,
		Exposure = 0x00008000,
		VisibilityChange = 0x00010000,
		StructureNotify = 0x00020000,
		ResizeRedirect = 0x00040000,
		SubstructureNotify = 0x00080000,
		SubstructureRedirect = 0x00100000,
		FocusChange = 0x00200000,
		PropertyChange = 0x00400000,
		ColormapChange = 0x00800000,
		OwnerGrabButton = 0x01000000,
		Unused = 0xFE000000,
	}

	enum KeySym : uint
	{

	}
	enum KeyCode : byte
	{

	}
	enum Button : byte
	{

	}

	public enum KeyButtonMask : ushort
	{
		Shift = 0x0001,
		Lock = 0x0002,
		Control = 0x0004,
		Mod1 = 0x0008,
		Mod2 = 0x0010,
		Mod3 = 0x0020,
		Mod4 = 0x0040,
		Mod5 = 0x0080,
		Button1 = 0x0100,
		Button2 = 0x0200,
		Button3 = 0x0400,
		Button4 = 0x0800,
		Button5 = 0x1000,
		Unused = 0xE000,
	}

	public enum KeyMask : ushort
	{
		Shift = 0x0001,
		Lock = 0x0002,
		Control = 0x0004,
		Mod1 = 0x0008,
		Mod2 = 0x0010,
		Mod3 = 0x0020,
		Mod4 = 0x0040,
		Mod5 = 0x0080,
		Unused = 0xFF00,
	}

	[PduStruct]
	partial struct Char2B
	{
		internal byte b1, b2;
	}

	[PduStruct]
	partial struct Point
	{
		short x, y;
	}

	[PduStruct]
	partial struct Rect
	{
		short x, y;
		ushort width, height;
	}

	[PduStruct]
	partial struct Arc
	{
		short x, y;
		ushort width, height;
		short angle1, angle2;
	}

	enum XByteOrder : byte
	{
		Big = (byte)'B',
		Little = (byte)'l'
	}

	enum HostFamily : byte
	{
		Internet = 0,
		Decnet = 1,
		Chaos = 2,
		ServerInterpreted = 5,
		Ipv6 = 6
	}

	struct XPad : IPduStruct
	{
		public void ReadFrom(IByteSource reader)
			=> reader.Align(4);
		public void ReadFrom(IByteSource reader, PduByteOrder byteOrder)
			=> reader.Align(4);
		public void WriteTo(ByteWriter writer)
			=> writer.Align(4);
		public void WriteTo(ByteWriter writer, PduByteOrder byteOrder)
			=> writer.Align(4);
	}

	[PduStruct]
	partial struct Host
	{
		internal HostFamily family;
		private byte unused;
		internal ushort cbAddress;
		[PduArraySize(nameof(cbAddress))]
		internal byte[] addressBytes;
		private XPad pad;
	}

	[PduStruct]
	partial struct XStr
	{
		internal byte length;
		[PduArraySize(nameof(length))]
		internal byte[] bytes;
	}

	[PduStruct]
	partial struct ProtocolVersion
	{
		internal ushort major;
		internal ushort minor;
	}

	[PduStruct]
	partial struct ConnectionSetup
	{
		internal XByteOrder order;
		private byte unused;
		internal ProtocolVersion version;
		internal ushort cbAuthProtocolName;
		internal ushort cbAuthProtocolData;
		private ushort unused2;

		[PduString(CharSet.Ansi, nameof(cbAuthProtocolName))]
		internal string authProtocolName;
		private XPad pad1;

		[PduArraySize(nameof(cbAuthProtocolData))]
		internal byte[] authProtocolData;
		private XPad pad2;
	}
}
