using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Ioc.Tests.ContainerFixtures
{
    [TestFixture]
    public class ResolveTests
    {
        [Test]
        [ExpectedException(ExpectedMessage = "The type 'TW.Ioc.IContainer' is not registered.\r\nParameter name: type")]
        public void UnregisteredRequestForLifeStyleThrowsException()
        {
            IContainer container = new Container();
            IContainer result = container.Resolve<IContainer>();
        }

        [Test]
        public void CanResolveInstance()
        {
            IContainer container = new Container();
            container.Register<IContainer, Container>();

            IContainer result = container.Resolve<IContainer>();
            Assert.IsNotNull(result);
        }

        [Test]
        public void CanResolveParameterizedConstructor()
        {
            IContainer container = new Container();
            container.Register<ITestInterface, TestImplementation>();
            container.Register<IContainer, Container>();

            ITestInterface result = container.Resolve<ITestInterface>();
            Assert.IsNotNull(result);
        }
    }
}
