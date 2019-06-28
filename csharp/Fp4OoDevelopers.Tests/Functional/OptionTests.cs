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

        [Fact]
        public void GetOrElseOfSomething()
        {
            Option<string> option = new Some<string>("test");

            string result = option.GetOrElse("missing value");

            Assert.Equal("test", result);
        }

        [Fact]
        public void GetOrElseOfNone()
        {
            Option<string> option = new None<string>();

            string result = option.GetOrElse("missing value");

            Assert.Equal("missing value", result);
        }
    }
}
