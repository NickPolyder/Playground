using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.NETCORE.Tests.ProducerConsumer
{
    public class Consumer : IConsumer
    {
        private readonly BlockingCollection<Action> _workerQueue;

        public IEnumerable<Action> Queue => _workerQueue.GetConsumingEnumerable();

        public Consumer()
        {
            _workerQueue = new BlockingCollection<Action>();
        }

        public bool AddItem(Action item)
        {
            try
            {
                if (_workerQueue.TryAdd(item))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public void Complete()
        {
            _workerQueue.CompleteAdding();

        }

        public void Start(CancellationToken cts)
        {
            new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None).StartNew(() =>
            {
                while (!_workerQueue.IsCompleted)
                {
                    Console.WriteLine("Waiting");
                    if (_workerQueue.TryTake(out Action item, TimeSpan.FromMilliseconds(100)))
                    {

                        item.Invoke();
                    }
                }

            }, cts);
        }
    }
}