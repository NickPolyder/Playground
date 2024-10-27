using System;
using System.Reflection;

namespace Playground.NETCORE.Tests.DispatchProxy
{
    public class ExceptionDispatch<TClass> : System.Reflection.DispatchProxy where TClass : class
    {
        private TClass _instance;

        protected TClass Instance
        {
            get => _instance;

            set => _instance = value;
        }

        public ExceptionDispatch()
        { }

        public ExceptionDispatch(TClass instance)
        {
            _instance = instance;
        }

        /// <inheritdoc />
        protected override object Invoke(System.Reflection.MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine($"Just got in {nameof(Invoke)} and just before run the method {targetMethod.Name}");
            try
            {
                return targetMethod.Invoke(Instance, args);

            }
            catch (TargetInvocationException ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public static TClass Create(TClass instance)
        {
            object proxy = Create<TClass, ExceptionDispatch<TClass>>();
            ((ExceptionDispatch<TClass>)proxy).Instance = instance;
            return (TClass)proxy;
        }
    }
}