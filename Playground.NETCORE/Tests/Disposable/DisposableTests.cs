using System;

namespace Playground.NETCORE.Tests.Disposable
{
    public class DisposableTests : ITestCase
    {

        public bool Enabled { get; } = true;
        public string Name { get; } = "Disposable Tests";

        public void Run()
        {
            var derived = new DerivedDisposable();
            Console.WriteLine("Disposing Derived");
            Console.WriteLine();
            derived.Dispose();
            Console.WriteLine();
            Console.WriteLine("Disposing by the use of Interface");
            Console.WriteLine();
            var derived2 = new DerivedDisposable();
            ((IDisposable)derived2).Dispose();
            Console.WriteLine();
            Console.WriteLine("Disposing via base class");
            Console.WriteLine();
            var derived3 = new DerivedDisposable();
            ((Disposable)derived3).Dispose();

            Console.WriteLine("Testing Lock Derived Disposable");
            Console.WriteLine();
            //var lockedState = new LockDerivedDisposable();
            //Task.Run(() =>
            //{
            //    var timeOfDay = DateTime.Now.TimeOfDay.Add(TimeSpan.FromSeconds(10));
            //    while (timeOfDay > DateTime.Now.TimeOfDay)
            //    {
            //        lockedState.CheckBaseDisposed();
            //    }
            //});
            //Task.Run(async () =>
            //{
            //    await Task.Delay(200);

            //    lockedState.Dispose();

            //});

            Console.WriteLine();
        }
    }

    public class Disposable : IDisposable
    {
        private bool _disposed;

        public void CheckBaseDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DerivedDisposable));

            System.Console.WriteLine("CheckDisposed");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _disposed = true;
                System.Console.WriteLine("Base Disposable");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class DerivedDisposable : Disposable
    {
        private bool _disposed;

        public void CheckDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DerivedDisposable));

            System.Console.WriteLine("CheckDisposed");
        }
        protected override void Dispose(bool disposing)
        {

            if (disposing && !_disposed)
            {
                _disposed = true;
                System.Console.WriteLine("Derived Disposable");
            }
            base.Dispose(disposing);
        }
    }

    public class LockDerivedDisposable : Disposable
    {
        private bool _disposed;

        public void CheckLockedState()
        {
            System.Console.WriteLine("Before _disposed check CheckLockedState");
            if (_disposed) throw new ObjectDisposedException(nameof(DerivedDisposable));

            System.Console.WriteLine("CheckLockedState");
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                lock (this)
                {
                    System.Threading.Thread.Sleep(1000);
                    _disposed = true;
                    System.Threading.Thread.Sleep(1000);
                    System.Console.WriteLine("LockDerivedDisposable Disposable");
                }
            }
            base.Dispose(disposing);
        }
    }
}