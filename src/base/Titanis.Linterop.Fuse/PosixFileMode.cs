namespace Titanis.Linterop.Fuse;

[Flags]
public enum PosixFileMode : uint
{
	OwnerRead = 4 << 6,
	OwnerWrite = 2 << 6,
	OwnerExecute = 1 << 6,
	OwnerReadExecute = OwnerRead | OwnerExecute,
	OwnerReadWrite = OwnerRead | OwnerWrite,
	OwnerAll = 7 << 6,

	GroupRead = 4 << 3,
	GroupWrite = 2 << 3,
	GroupExecute = 1 << 3,
	GroupReadExecute = GroupRead | GroupExecute,
	GroupReadWrite = GroupRead | GroupWrite,
	GroupAll = 7 << 3,

	OtherRead = 4,
	OtherWrite = 2,
	OtherExecute = 1,
	OtherReadExecute = OtherRead | OtherExecute,
	OtherReadWrite = OtherRead | OtherWrite,
	OtherAll = 7,

	ModeReadAll = OwnerRead | GroupRead | OtherRead,
	ModeReadExecuteAll = OwnerReadExecute | GroupReadExecute | OtherReadExecute,
	ModeWriteAll = OwnerReadWrite | GroupReadWrite | OtherReadWrite,
	Mode755 = OwnerAll | GroupReadExecute | OtherReadExecute,
	Mode777 = OwnerAll | GroupAll | OtherAll,

	DefaultDirAccess = Mode755,

	Setuid = (4 << 9),
	Setgid = (2 << 9),
	Svtx = (1 << 9),
}
