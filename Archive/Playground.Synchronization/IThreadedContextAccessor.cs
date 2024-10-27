using System;

namespace Playground.Synchronization
{
	public interface IThreadedContextAccessor: IContextAccessor,IDisposable
	{
		void Synchronize(IObjectContext context);
	}
}