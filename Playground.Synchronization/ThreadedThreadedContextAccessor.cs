using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Synchronization
{
	public class ThreadedThreadedContextAccessor : IThreadedContextAccessor
	{
		private const int CleanSweepEvery = 1;
		private readonly Func<IContextAccessor> _contextAccessor;
		private ConcurrentDictionary<Thread, IObjectContext> _mapper;
		private Thread _backgroundThread;
		private CancellationTokenSource _cancellationToken;
	
		public ThreadedThreadedContextAccessor(Func<IContextAccessor> contextAccessor)
		{
			_contextAccessor = contextAccessor;
			_mapper = new ConcurrentDictionary<Thread, IObjectContext>();
			_cancellationToken = new CancellationTokenSource();
			_backgroundThread = new Thread(Check) { IsBackground = true };
			
			_backgroundThread.Start( new Tuple<ConcurrentDictionary<Thread, IObjectContext>, CancellationToken>(_mapper, _cancellationToken.Token));
		}

		public IObjectContext GetCurrent()
		{
			if (_mapper.TryGetValue(Thread.CurrentThread, out var context))
			{
				return context;
			}

			var newContext = _contextAccessor.Invoke().GetCurrent();
			Synchronize(newContext);
			return newContext;
		}

		public void Synchronize(IObjectContext context)
		{
			_mapper.AddOrUpdate(Thread.CurrentThread, context, (thread, objectContext) => context);
		}
		
		private void Check(object context)
		{
			var tuple = (Tuple<ConcurrentDictionary<Thread, IObjectContext>, CancellationToken>)context;
			var mapper = tuple.Item1;
			var ctx = tuple.Item2;

			while (!ctx.IsCancellationRequested)
			{
				foreach (var key in mapper.Keys)
				{
					if (!key.IsAlive)
					{
						mapper.TryRemove(key, out var _);
					}
				}

				Task.Delay(TimeSpan.FromMinutes(CleanSweepEvery), ctx).ContinueWith(item => {/* Eat Cancelled Exception */}).GetAwaiter().GetResult();
			}
			Console.WriteLine("Thread Dying");
		}

		public void Dispose()
		{
			_cancellationToken.Cancel();
			_cancellationToken.Dispose();
			_cancellationToken = null;
			_backgroundThread = null;
			_mapper = null;
		}

	}
}