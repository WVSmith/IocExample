using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Ioc
{
    public class Container : IContainer
    {
        private ConcurrentDictionary<Type, Registration> _registrations;

        public Container()
        {
            _registrations = new ConcurrentDictionary<Type, Registration>();
        }
        public bool IsRegistered(Type type)
        {
            return _registrations.ContainsKey(type);
        }
        public void Register<Interface, Implementation>(LifeStyleType lifeStyle = LifeStyleType.Transient)
        {
            Register(typeof(Interface), typeof(Implementation), lifeStyle);
        }

        public void Register(Type type, Type implementation, LifeStyleType lifeStyle = LifeStyleType.Transient)
        {
            if (!_registrations.ContainsKey(type))
            {
                Registration registration = new Registration
                {
                    Type = implementation,
                    LifeStyleType = lifeStyle,
                };
                _registrations.TryAdd(type, registration);
            }
        }

        public LifeStyleType GetLifeStyleType(Type type)
        {
            if (!IsRegistered(type))
                throw new ArgumentOutOfRangeException("type", string.Format("The type '{0}' is not registered.", type.ToString()));

            return _registrations[type].LifeStyleType;
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            if (!IsRegistered(type))
                throw new ArgumentOutOfRangeException("type", string.Format("The type '{0}' is not registered.", type.ToString()));

            object result;
            Registration registration = _registrations[type];
            if (registration.LifeStyleType == LifeStyleType.Singleton)
                result = registration.Instance ?? (registration.Instance = CreateInstance(registration.Type));
            else
                result = CreateInstance(registration.Type);
            return result;
        }

        private object CreateInstance(Type type)
        {
            var parameters = new List<object>();

            bool canResolve = false;
            foreach (var constructor in type.GetConstructors())
            {
                parameters.Clear();
                canResolve = true;
                foreach (var param in constructor.GetParameters())
                {
                    Type paramType = param.ParameterType;
                    if (IsRegistered(paramType))
                        parameters.Add(Resolve(paramType));
                    else
                    {
                        canResolve = false;
                        break;
                    }
                }
                if (canResolve)
                    break;
            }

            if (canResolve && parameters.Count > 0)
                return Activator.CreateInstance(type, parameters.ToArray());
            else
                return Activator.CreateInstance(type);
        }


        
    }
}
