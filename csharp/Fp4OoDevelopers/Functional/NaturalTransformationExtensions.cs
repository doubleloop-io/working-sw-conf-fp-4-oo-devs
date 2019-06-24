namespace Fp4OoDevelopers.Functional
{
    public static class NaturalTransformationExtensions
    {
        public static Either<TLeft, TRight> ToEither<TLeft, TRight>(this Option<TRight> option, TLeft left) =>
            option
                .Map(right => Syntax.Right<TLeft, TRight>(right))
                .GetOrElse(Syntax.Left<TLeft, TRight>(left));
    }
}