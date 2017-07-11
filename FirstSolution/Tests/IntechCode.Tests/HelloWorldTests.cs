using FluentAssertions;
using NUnit.Framework;
using Xunit;

namespace IntechCode.Tests
{
    [TestFixture]
    public class HelloWorldTests
    {
        [Test]
        [Fact]
        public void HelloWorld_says_hello_in_hexadecimal()
        {
            HelloWorld.SayHelloWorld(10).Should().Be("Count is A");
            (4 + 5).Should().Be(9);
        }
    }
}
