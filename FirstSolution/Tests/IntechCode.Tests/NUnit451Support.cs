using System;
using System.Collections.Generic;
using System.Text;

namespace NUnit.Framework
{

#if NETCOREAPP1_0

    class TestAttribute : Attribute
    {
    }

    class TestFixtureAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    class TestCaseAttribute : Attribute
    {
        public TestCaseAttribute(params object[] parameters)
        {
        }
    }
#endif

}

