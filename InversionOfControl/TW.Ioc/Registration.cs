﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Ioc
{
    internal struct Registration
    {
        public Type Type;
        public Object Instance;
        public LifeStyleType LifeStyleType;
    }
}
