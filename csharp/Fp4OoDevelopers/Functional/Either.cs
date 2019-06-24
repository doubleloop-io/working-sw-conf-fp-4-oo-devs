namespace Fp4OoDevelopers.Functional
{
    public abstract class Either<TLeft, TRight>
    {
        public static implicit operator Either<TLeft, TRight>(TLeft value) => new Left<TLeft, TRight>(value);

        public static implicit operator Either<TLeft, TRight>(TRight value) => new Right<TLeft, TRight>(value);
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