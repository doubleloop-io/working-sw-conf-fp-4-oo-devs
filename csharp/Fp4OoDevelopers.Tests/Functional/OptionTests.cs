using Fp4OoDevelopers.Functional;
using Xunit;

namespace Fp4OoDevelopers.Tests.Functional
{
    public class OptionTests
    {
        [Fact]
        public void PureOfSomething()
        {
            Option<string> result = Option<string>.Pure("test");

            Assert.Equal(new Some<string>("test"), result);
        }

        [Fact]
        public void PureOfNull()
        {
            Option<string> result = Option<string>.Pure(null);

            Assert.Equal(new None<string>(), result);
        }
    }
}