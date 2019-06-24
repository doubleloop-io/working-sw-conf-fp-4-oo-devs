using System;
using System.Collections.Generic;

namespace Fp4OoDevelopers.Functional
{
    public abstract class Either<TLeft, TRight>
    {
        public abstract Either<TLeft, TOut> Map<TOut>(Func<TRight, TOut> func) where TOut : class;

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

        public override Either<TLeft, TOut> Map<TOut>(Func<TRight, TOut> func)
        {
            return value;
        }

        public override bool Equals(object obj) =>
            obj is Left<TLeft, TRight> left && Equals(left.value, value);

        public override int GetHashCode() =>
            value.GetHashCode();
    }

    public class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        private readonly TRight value;

        public Right(TRight value)
        {
            this.value = value;
        }

        public override Either<TLeft, TOut> Map<TOut>(Func<TRight, TOut> func)
        {
            return func(value);
        }

        public override bool Equals(object obj) =>
            obj is Right<TLeft, TRight> right && Equals(right.value, value);

        public override int GetHashCode() =>
            value.GetHashCode();
    }
}
