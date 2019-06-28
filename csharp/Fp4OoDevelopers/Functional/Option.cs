using System;

namespace Fp4OoDevelopers.Functional
{
    public interface IOption
    {
    }

    public abstract class Option<T> : IOption
    {
        public static readonly Option<T> None = None<T>.Instance;

        public static Option<T> Pure(T value) =>
            value == null ? None<T>.Instance : new Some<T>(value);

        public abstract T GetOrElse(T @default);

        public abstract Option<TOut> Map<TOut>(Func<T, TOut> func);

        public Either<TLeft, T> ToEither<TLeft>(TLeft left) =>
            Map(right => Syntax.Right<TLeft, T>(right))
                .GetOrElse(Syntax.Left<TLeft, T>(left));

        public static implicit operator Option<T>(T value) => Pure(value);

        public static bool operator ==(Option<T> left, IOption right) => Equals(left, right);

        public static bool operator !=(Option<T> left, IOption right) => !(left == right);

        public static bool operator ==(IOption left, Option<T> right) => right == left;

        public static bool operator !=(IOption left, Option<T> right) => right != left;
    }

    public class Some<T> : Option<T> 
    {
        private readonly T value;

        public Some(T value)
        {
            this.value = value;
        }

        public override T GetOrElse(T @default) => value;

        public override Option<TOut> Map<TOut>(Func<T, TOut> func)
        {
            return Option<TOut>.Pure(func(value));
        }

        public override bool Equals(object obj) => 
            obj is Some<T> some && Equals(value, some.value);

        public override int GetHashCode() => 
            value.GetHashCode();
    }

    public class None<T> : Option<T>
    {
        public static readonly Option<T> Instance = new None<T>();

        private None()
        {
        }

        public override T GetOrElse(T @default) => @default;

        public override Option<TOut> Map<TOut>(Func<T, TOut> func)
        {
            return new None<TOut>();
        }

        public override bool Equals(object obj) => 
            obj != null && obj.GetType().IsGenericType 
                        && obj.GetType().GetGenericTypeDefinition() == typeof(None<>);

        public override int GetHashCode() => 0;
    }
}
