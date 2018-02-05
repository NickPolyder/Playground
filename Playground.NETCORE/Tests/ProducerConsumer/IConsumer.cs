using System;
using System.Collections.Generic;
using System.Threading;

namespace Playground.NETCORE.Tests.ProducerConsumer
{
    public interface IConsumer
    {
        IEnumerable<Action> Queue { get; }

        void Start(CancellationToken cts);

        void Complete();

        bool AddItem(Action item);
    }
}