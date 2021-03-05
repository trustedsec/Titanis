namespace Titanis.Msrpc.Msscmr
{
	public class ScmLockStatus
	{
		public bool IsLocked { get; set; }
		public int LockDuration { get; set; }
		public string LockOwner { get; set; }
	}
}