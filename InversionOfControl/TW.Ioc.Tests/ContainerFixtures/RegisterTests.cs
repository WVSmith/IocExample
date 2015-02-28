using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TW.Ioc.Tests.ContainerFixtures
{
    [TestFixture]
    public class RegisterTests
    {
        [Test]
        public void CanRegisterString()
        {
            IContainer container = new Container();
            container.Register<IContainer, Container>();

            Assert.IsTrue(container.IsRegistered(typeof(IContainer)));
        }

        [Test]
        public void CanSetLifeStyle()
        {
            IContainer container = new Container();
            container.Register<IContainer, Container>(LifeStyleType.Singleton);

            Assert.AreEqual(LifeStyleType.Singleton, container.GetLifeStyleType(typeof(IContainer)));
        }

        [Test]
        [ExpectedException(ExpectedMessage = "The type 'TW.Ioc.IContainer' is not registered.\r\nParameter name: type")]
        public void UnregisteredRequestForLifeStyleThrowsException()
        {
            IContainer container = new Container();
            Assert.AreEqual(LifeStyleType.Singleton, container.GetLifeStyleType(typeof(IContainer)));
        }
    }
}
