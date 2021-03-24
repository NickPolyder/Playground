namespace Playground.Synchronization
{
	public interface IContextAccessor
	{
		IObjectContext GetCurrent();
	}
}