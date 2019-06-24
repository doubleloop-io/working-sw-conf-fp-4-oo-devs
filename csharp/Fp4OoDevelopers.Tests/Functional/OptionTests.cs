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

        [Fact]
        public void MapOfSomething()
        {
            Option<string> option = new Some<string>("test");

            Option<int> result = option.Map(str => str.Length);

            Assert.Equal(new Some<int>(4), result);
        }

        [Fact]
        public void MapOfNone()
        {
            Option<string> option = new None<string>();

            Option<int> result = option.Map(str => str.Length);

            Assert.Equal(new None<int>(), result);
        }
    }
}
