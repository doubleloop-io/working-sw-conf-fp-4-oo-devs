using Fp4OoDevelopers.Functional;
using Xunit;

namespace Fp4OoDevelopers.Tests.Functional
{
    public class NaturalTransformationTests
    {
        [Fact]
        public void SomeToEither()
        {
            var option = new Some<int>(42);

            Either<string, int> result = option.ToEither("error");

            Assert.Equal(new Right<string, int>(42), result);
        }

        [Fact]
        public void NoneToEither()
        {
            var option = None<int>.Instance;

            Either<string, int> result = option.ToEither("error");

            Assert.Equal(new Left<string, int>("error"), result);
        }
    }
}