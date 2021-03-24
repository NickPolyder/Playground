using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Synchronization
{
	public class ServiceA: IServiceA
	{
		private readonly IThreadedContextAccessor _threadedContextAccessor;
		private readonly IServiceB _serviceB;

		public ServiceA(IThreadedContextAccessor threadedContextAccessor, IServiceB serviceB)
		{
			_threadedContextAccessor = threadedContextAccessor;
			_serviceB = serviceB;
		}
		public async Task Call(string with)
		{
			var context = _threadedContextAccessor.GetCurrent();
			Console.WriteLine($"Service A. Context Thread: {context.CurrentThread}. {context.Data}");
			Console.WriteLine($"Service A. Current Thread: {Thread.CurrentThread.ManagedThreadId}.");
			Console.WriteLine($"Service A. User {context.User}");
			context.Data = with;
			var result = await Task.Run(async () =>
			{
				_threadedContextAccessor.Synchronize(context);
				return await _serviceB.GetFrom();
			});

			Console.WriteLine(result);
		}
	}

	public class ServiceB : IServiceB
	{
		private readonly IThreadedContextAccessor _threadedContextAccessor;

		public ServiceB(IThreadedContextAccessor threadedContextAccessor)
		{
			_threadedContextAccessor = threadedContextAccessor;
		}
		public Task<string> GetFrom()
		{
			var context = _threadedContextAccessor.GetCurrent();
			if (context == null)
			{
				Console.WriteLine($"Service B. Context Null");
				return Task.FromResult("");
			}
			Console.WriteLine($"Service B. Context Current Thread: {context.CurrentThread}. {context.Data}");
			Console.WriteLine($"Service B. Current Thread: {Thread.CurrentThread.ManagedThreadId}.");
			Console.WriteLine($"Service B. User {context.User}");
			context.CalledFromThreads.Add(Thread.CurrentThread.ManagedThreadId);
			context.CurrentThread = Thread.CurrentThread.ManagedThreadId;
			return Task.FromResult(context.Data);
		}
	}
}