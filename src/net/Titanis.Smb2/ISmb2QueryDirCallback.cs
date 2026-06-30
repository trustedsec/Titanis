namespace Titanis.Smb2
{
	public interface ISmb2QueryDirCallback<TArg>
	{
		bool OnDirEntry(Smb2DirEntry entry, TArg arg);
	}
}