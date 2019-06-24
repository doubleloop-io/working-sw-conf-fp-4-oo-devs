using Fp4OoDevelopers.Functional;
using Xunit;

namespace Fp4OoDevelopers.Tests.Functional
{
    public class EitherTests
    {
        [Fact]
        public void Equality()
        {
            Either<string, int> right = new Right<string, int>(42);
            Either<string, int> left = new Left<string, int>("test");

            Assert.NotEqual(right, left);
            Assert.Equal(right, new Right<string, int>(42));
            Assert.Equal(left, new Left<string, int>("test"));
        }

        [Fact]
        public void MapOfLeft()
        {
            Either<string, int> either = new Left<string, int>("test");

            Either<string, string> result = either.Map(x => x.ToString());

            Assert.Equal(new Left<string, string>("test"), result);
        }

        [Fact]
        public void MapOfRight()
        {
            Either<string, int> either = new Right<string, int>(42);

            Either<string, string> result = either.Map(x => x.ToString());

            Assert.Equal(new Right<string, string>("42"), result);
        }
    }
}