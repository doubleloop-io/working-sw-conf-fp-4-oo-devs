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
    }
}