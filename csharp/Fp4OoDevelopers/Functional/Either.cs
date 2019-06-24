namespace Fp4OoDevelopers.Functional
{
    public abstract class Either<TLeft, TRight>
    {
    }

    public class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        private readonly TLeft value;

        public Left(TLeft value)
        {
            this.value = value;
        }
    }

    public class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        private readonly TRight value;

        public Right(TRight value)
        {
            this.value = value;
        }
    }
}