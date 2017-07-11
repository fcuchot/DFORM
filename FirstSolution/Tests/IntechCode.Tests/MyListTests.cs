using FluentAssertions;
using IntechCode.IntechCollection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntechCode.Tests
{
    [NUnit.Framework.TestFixture]
    public class MyListTests
    {
        [NUnit.Framework.Test]
        [Fact]
        public void MyList_supports_foreach()
        {
            var l = new MyList<int>();
            l.Add(3712);
            int nbTurn = 0;
            foreach( var item in l )
            {
                ++nbTurn;
                item.Should().Be(3712);
            }
            nbTurn.Should().Be(1);
        }

    }
}
