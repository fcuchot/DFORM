using System;
using System.Collections.Generic;
using System.Text;

namespace IntechCode.Tests.Animals
{
    abstract class Animal
    {
        public string Name { get; set; }

        public abstract string Shout();

    }
}
