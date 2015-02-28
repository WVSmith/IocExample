using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Ioc.Tests
{
    internal interface ITestInterface
    {
        IContainer Foo { get; set; }
    }
}
