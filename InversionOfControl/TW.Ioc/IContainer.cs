using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Ioc
{
    public interface IContainer
    {
        void Register<Interface, Implementation>(LifeStyleType lifeStyle = LifeStyleType.Transient);
        bool IsRegistered(Type type);
        LifeStyleType GetLifeStyleType(Type type);
    }
}
