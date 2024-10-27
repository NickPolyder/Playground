using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;

namespace Playground.Synchronization
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var builder = new ContainerBuilder();
			builder.Register(ctx => new ContextAccessor(new ObjectContext
				{
					CurrentThread = Thread.CurrentThread.ManagedThreadId,
					CalledFromThreads = new List<int>
					{
						Thread.CurrentThread.ManagedThreadId
					},
					Data = Guid.NewGuid().ToString(),
					User = Environment.UserName
				}))
				.Named<IContextAccessor>("default");

			builder.Register(ctx =>
			{
				var internalCtx = ctx.Resolve<IComponentContext>();
				return new Func<IContextAccessor>(() => internalCtx.ResolveNamed<IContextAccessor>("default"));
			}).As<Func<IContextAccessor>>();

			builder.RegisterType<ThreadedThreadedContextAccessor>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<ServiceA>()
				.AsImplementedInterfaces();

			builder.RegisterType<ServiceB>()
				.AsImplementedInterfaces();

			using var container = builder.Build();

			var serviceB = container.Resolve<IServiceB>();

			var res = await serviceB.GetFrom();
			Console.WriteLine($"Result: {res}");

			var serviceA = container.Resolve<IServiceA>();

			await serviceA.Call("Hello There");

			await Task.Run(async () =>
			{
				var serviceC = container.Resolve<IServiceB>();

				var res2 = await serviceB.GetFrom();
				Console.WriteLine($"Result: {res2}");
			});

			serviceA = container.Resolve<IServiceA>();

			await serviceA.Call("Hi");
			await serviceA.Call("Ho");

			Console.WriteLine("Close");
			Console.ReadKey();
			var threaded = container.Resolve<IThreadedContextAccessor>();
			
			threaded.Dispose();
			Console.WriteLine("Done");
			Console.ReadKey();

		}
	}
}
