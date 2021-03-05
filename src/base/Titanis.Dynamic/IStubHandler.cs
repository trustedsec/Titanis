namespace Titanis.Dynamic
{
	public interface IStubHandler
	{
		public void HandleCall(MethodCallMessage message);
	}
}