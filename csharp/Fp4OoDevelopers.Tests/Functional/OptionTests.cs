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

            Assert.Equal(Option<string>.None, result);
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
            Option<string> option = None<string>.Instance;

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
            Option<string> option = Option<string>.None;

            Option<int> result = option.Map(str => str.Length);

            Assert.Equal(Option<int>.None, result);
        }

        [Fact]
        public void AllNoneAreEqual()
        {
            Assert.Equal(Option<string>.None, (IOption)Option<object>.None);
            Assert.Equal(Option<string>.None, (IOption)Option<int>.None);
        }
    }
}
