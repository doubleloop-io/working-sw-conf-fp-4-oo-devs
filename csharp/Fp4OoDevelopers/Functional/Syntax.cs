namespace Fp4OoDevelopers.Functional
{
    public static class Syntax
    {
        public static readonly Unit Unit = Unit.Instance;

        public static readonly IOption None = None<object>.Instance;

        public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft value) 
            => value;

        public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight value) 
            => value;
    }
}
