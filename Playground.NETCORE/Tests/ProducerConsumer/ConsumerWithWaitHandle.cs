using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.NETCORE.Tests.ProducerConsumer
{
    public class ConsumerWithWaitHandle : IConsumer
    {
        private readonly BlockingCollection<Action> _workerQueue;

        private EventWaitHandle waitHandle;

        public IEnumerable<Action> Queue => _workerQueue.GetConsumingEnumerable();

        public ConsumerWithWaitHandle()
        {
            _workerQueue = new BlockingCollection<Action>();
            waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        }

        public bool AddItem(Action item)
        {
            try
            {
                if (_workerQueue.TryAdd(item))
                {
                    waitHandle.Set();
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
                while (!_workerQueue.IsCompleted && waitHandle.WaitOne())
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