using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Ioc.Tests
{
    internal class TestImplementation : ITestInterface
    {
        public TestImplementation(IContainer foo)
        {
            Foo = foo;
        }
        public IContainer Foo
        {
            get;
            set;
        }
    }
}
