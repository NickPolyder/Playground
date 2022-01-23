using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.NETCORE.Tests.ProducerConsumer
{
    public class TaskConsumer
    {
        private readonly BlockingCollection<ConsumeItem> _workerQueue;

        private readonly EventWaitHandle _waitHandle;

        public long WaitingForExecution => _workerQueue.Count;

        private readonly CancellationTokenSource _cancellationSource;

        public bool IsStarted { get; private set; }

        public bool IsCompleted => _workerQueue.IsCompleted;

        public TaskConsumer(CancellationToken cts = default(CancellationToken))
        {
            _workerQueue = new BlockingCollection<ConsumeItem>();
            _waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            _cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(cts);

            _cancellationSource.Token.Register(() => { _waitHandle.Set(); });
        }

        public Task QueueItem(Action item)
        {
            ConsumeItem consumeItem = new ConsumeItem(item);

            TryAddConsumeItemOnQueue(consumeItem);

            return consumeItem.Task;
        }

        public Task QueueItem(Func<Task> item)
        {
            ConsumeItem consumeItem = new ConsumeItem(item);

            TryAddConsumeItemOnQueue(consumeItem);

            return consumeItem.Task;
        }

        public Task<object> QueueItem<TReturn>(Func<Task<TReturn>> item)
        {
            ConsumeItem consumeItem = new ConsumeItemWithReturn<TReturn>(item);

            TryAddConsumeItemOnQueue(consumeItem);

            return consumeItem.Task;
        }

        public void Start()
        {
            if (IsStarted) return;
            new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None).StartNew(async () =>
            {
                IsStarted = true;
                Logger.Logger.Instance.Debug($"Im in: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                while (CanWorkerQueueContinue())
                {
                    while (_workerQueue.TryTake(out ConsumeItem item, TimeSpan.FromMilliseconds(100)))
                    {
                        await item.Execute();
                    }
                }

            }, _cancellationSource.Token);
        }

        private bool CanWorkerQueueContinue()
        {
            return !_workerQueue.IsCompleted && !_cancellationSource.Token.IsCancellationRequested && _waitHandle.WaitOne();
        }

        public void Complete()
        {
            _workerQueue.CompleteAdding();
            _cancellationSource.Cancel();
        }


        private void TryAddConsumeItemOnQueue(ConsumeItem consumeItem)
        {
            try
            {
                if (_workerQueue.TryAdd(consumeItem))
                {
                    _waitHandle.Set();
                }
                else
                {
                    consumeItem.Source.TrySetResult(false);
                }
            }
            catch (Exception ex)
            {
                consumeItem.Source.TrySetException(ex);
            }
        }


        class ConsumeItem
        {
            protected internal TaskCompletionSource<object> Source { get; protected set; } = new TaskCompletionSource<object>();

            internal Task<object> Task => Source.Task;

            protected Func<Task> Executable;

            protected ConsumeItem() { }

            public ConsumeItem(Action actionToExecute)
            {
                Executable = () =>
                {
                    WrapActionWithCatch(actionToExecute);

                    return System.Threading.Tasks.Task.CompletedTask;
                };
            }

            public ConsumeItem(Func<Task> functionToExecute)
            {
                Source = new TaskCompletionSource<object>();
                Executable = () => WrapFunctionWithCatch(functionToExecute);
            }

            public Task Execute()
            {
                return Executable();
            }

            private void WrapActionWithCatch(Action action)
            {
                try
                {
                    action?.Invoke();
                    Source.TrySetResult(true);
                }
                catch (Exception ex)
                {
                    Source.TrySetException(ex);
                }
            }

            private async Task WrapFunctionWithCatch(Func<Task> function)
            {
                try
                {
                    if (function != null)
                    {
                        await function();
                        Source.TrySetResult(true);
                    }
                    else
                    {
                        Source.TrySetException(new ArgumentException("Function is null"));
                    }
                }
                catch (Exception ex)
                {
                    Source.TrySetException(ex);
                }
            }

        }

        class ConsumeItemWithReturn<TReturn> : ConsumeItem
        {
            public ConsumeItemWithReturn(Func<Task<TReturn>> functionToExecute)
            {
                Source = new TaskCompletionSource<object>();
                Executable = () => WrapFunctionWithCatch(functionToExecute);
            }

            private async Task WrapFunctionWithCatch(Func<Task<TReturn>> function)
            {
                try
                {
                    if (function != null)
                    {
                        var result = await function();
                        Source.TrySetResult(result);
                    }
                    else
                    {
                        Source.TrySetException(new ArgumentException("Function is null"));
                    }
                }
                catch (Exception ex)
                {
                    Source.TrySetException(ex);
                }
            }
        }

    }
}