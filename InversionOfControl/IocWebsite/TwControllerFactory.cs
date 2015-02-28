using IocWebsite.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using TW.Ioc;

namespace IocWebsite
{
    public class TwControllerFactory : DefaultControllerFactory
    {
        private ConcurrentDictionary<string, Type> _controllerReference;
        IContainer _container;

        public TwControllerFactory()
        {
            _controllerReference = new ConcurrentDictionary<string, Type>();
            _container = new Container();
            _container.Register<IContainer, Container>(LifeStyleType.Singleton);
        }
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            if(_controllerReference.ContainsKey(controllerName))
            {
                return (IController)_container.Resolve(_controllerReference[controllerName]);
            }
            else
                return new HomeController();
        }

        public void RegisterController(string name, Type type)
        {
            _container.Register(type, type);
            _controllerReference.TryAdd(name, type);
        }
    }
}