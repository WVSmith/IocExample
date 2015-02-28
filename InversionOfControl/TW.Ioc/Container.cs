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
            if (!_registrations.ContainsKey(typeof(Interface)))
            {
                Registration registration = new Registration
                {
                    Type = typeof(Interface),
                    LifeStyleType = lifeStyle,
                };
                _registrations.TryAdd(typeof(Interface), registration);
            }
        }

        public LifeStyleType GetLifeStyleType(Type type)
        {
            if (!_registrations.ContainsKey(type))
                throw new ArgumentOutOfRangeException("type", string.Format("The type '{0}' is not registered.", type.ToString()));

            return _registrations[type].LifeStyleType;
        }
    }
}
