namespace Playground.Synchronization
{
	public class ContextAccessor: IContextAccessor
	{
		private readonly IObjectContext _context;

		public ContextAccessor(IObjectContext context)
		{
			_context = context;
		}
		public IObjectContext GetCurrent()
		{
			return _context;
		}
	}
}