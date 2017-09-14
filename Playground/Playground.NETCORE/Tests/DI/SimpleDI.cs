using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Playground.NETCORE.Tests.DI
{
    public class SimpleDI : IServiceInjector
    {
        private List<ServiceItem> ServiceItems = new List<ServiceItem>();
        public void AddSingleton<TMap, TType>()
        {
            ServiceItems.Add(new ServiceItem(Scope.Singleton, typeof(TMap), typeof(TType), this));
        }

        public void AddSingleton<TMap, TType>(TType instance)
        {
            ServiceItems.Add(new ServiceItem(Scope.Singleton, typeof(TMap), typeof(TType), this, instance));
        }

        public void AddSingleton<TMap, TType>(Func<IServiceInjector, object> factory)
        {
            ServiceItems.Add(new ServiceItem(Scope.Singleton, typeof(TMap), typeof(TType), this, factory));
        }

        public void AddScoped<TMap, TType>()
        {
            ServiceItems.Add(new ServiceItem(Scope.Scoped, typeof(TMap), typeof(TType), this));
        }

        public void AddScoped<TMap, TType>(Func<IServiceInjector, object> factory)
        {
            ServiceItems.Add(new ServiceItem(Scope.Scoped, typeof(TMap), typeof(TType), this, factory));
        }

        public void AddPerUse<TMap, TType>()
        {
            ServiceItems.Add(new ServiceItem(Scope.PerUse, typeof(TMap), typeof(TType), this));
        }

        public void AddPerUse<TMap, TType>(Func<IServiceInjector, object> factory)
        {
            ServiceItems.Add(new ServiceItem(Scope.PerUse, typeof(TMap), typeof(TType), this, factory));
        }

        public object GetService(Type serviceType)
        {
            var index = ServiceItems.FindIndex(tt => tt.MappingType == serviceType || tt.Type == serviceType);
            if (index >= 0)
            {
                return ServiceItems[index].Value;
            }
            throw new ArgumentException($"Could not create {serviceType.FullName}");
        }

        public TType GetService<TType>()
        {
            try
            {
                return (TType)GetService(typeof(TType));
            }
            catch (InvalidCastException)
            {
                return default(TType);
            }
        }
    }

    public enum Scope { Singleton, Scoped, PerUse }

    public sealed class ServiceItem
    {
        public Scope Scope { get; }
        public Type MappingType { get; }
        public Type Type { get; }
        private object _value;
        public object Value
        {
            get
            {
                if (Scope.Singleton == this.Scope)
                {
                    if (_value != null)
                    {
                        return _value;
                    }
                    _value = _factory?.Invoke(_injector);
                    return _value;
                }
                return _factory?.Invoke(_injector);
            }
        }

        private Func<IServiceInjector, object> _factory;
        private ConstructorInfo _bestMatchConstructor;

        readonly IServiceInjector _injector;
        public ServiceItem(Scope scope, Type mappingType, Type type, IServiceInjector injector) :
            this(scope, mappingType, type, injector, default(object))
        { }

        public ServiceItem(Scope scope, Type mappingType, Type type, IServiceInjector injector, object instance = null)
        {
            Scope = scope;
            MappingType = mappingType;
            Type = type;
            _injector = injector;
            _value = instance;
            _factory = _FindService;
        }

        public ServiceItem(Scope scope, Type mappingType, Type type, IServiceInjector injector, Func<IServiceInjector, object> factory = null)
        {
            Scope = scope;
            MappingType = mappingType;
            Type = type;
            _injector = injector;
            _factory = factory;
        }


        private object _findBestMatch()
        {
            var possibleMatches = (from constructor in Type.GetConstructors()
                                   where constructor.IsConstructor
                                   let parameters = constructor.GetParameters()
                                   orderby parameters.Length descending
                                   select new { parameters, constructor });
            foreach (var item in possibleMatches)
            {
                try
                {
                    var objs = _generateParameters(item.parameters);
                    _bestMatchConstructor = item.constructor;
                    return _bestMatchConstructor.Invoke(objs);
                }
                catch
                {
                    //intented
                }

            }
            return null;
        }

        private object[] _generateParameters(ParameterInfo[] parameters)
        {
            var objParams = new object[parameters.Length];
            var index = 0;
            foreach (var parameter in parameters)
            {
                var liveobj = _injector.GetService(parameter.ParameterType);
                if (liveobj == null)
                {
                    throw new ArgumentNullException(nameof(liveobj));
                }
                objParams[index++] = _injector.GetService(parameter.ParameterType);
            }
            return objParams;
        }

        private object _FindService(IServiceInjector injector)
        {
            return _bestMatchConstructor == null
                ? _findBestMatch()
                : _bestMatchConstructor.Invoke(_generateParameters(_bestMatchConstructor.GetParameters()));
        }
    }
}
