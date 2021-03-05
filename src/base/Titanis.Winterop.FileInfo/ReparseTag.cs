using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Winterop
{
	// [MS-FSCC] § 2.1.2.1
	public enum ReparseTag : uint
	{
		SymbolicLink = 0xA000000C,
		MountPoint = 0xA0000003,
		HierarchicalStorageManager = 0xC0000004,
		DriveExtender = 0x80000005,
		Hsm2 = 0x80000006,
		SingleInstanceStorage = 0x80000007,
		WimMount = 0x80000008,
		ClusteredSharedVolume = 0x80000009,
		Dfs = 0x8000000A,
		FilterManager = 0x8000000B,
		IisCache = 0xA0000010,
		Dfsr = 0x80000012,
		Deduplication = 0x80000013,
		AppxStream = 0xC0000014,
		Nfs = 0x80000014,
		FilePlaceholder = 0x80000015,
		DynamicFileFilter = 0x80000016,
		WindowsOverlayFilter = 0x80000017,
		WindowsContainerIsolationFilter = 0x80000018,
		WindowsContainerIsolationFilter_1 = 0x90001018,
		NpfsGlobal = 0xA0000019,
		Cloud = 0x9000001A,
		AppExecLink = 0x8000001B,
		ProjectedFS = 0x9000001C,
		LinuxSymlink = 0xA000001D,
		AfsStorageSync = 0x8000001E,
		WciTombstone = 0xA000001F,
		WciUnhandled = 0x80000020,
		OneDrive = 0x80000021,
		ProjFSTombstone = 0xA0000022,
		UnixSocket = 0x80000023,
		UnixFifo = 0x80000024,
		UnixCharacterSpecialFile = 0x80000025,
		UnixBlock = 0x80000026,
		WciLink = 0xA0000027,
		WciLink_1 = 0xA0001027,

	}
}
