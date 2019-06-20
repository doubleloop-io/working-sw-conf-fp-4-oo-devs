using System;

namespace Fp4OoDevelopers.Functional
{
    public abstract class Option<T>
    {
        public static Option<T> Pure(T value) =>
            value == null ? (Option<T>)new None<T>() : new Some<T>(value);

        public abstract T GetOrElse(T @default);

        public abstract Option<TOut> Map<TOut>(Func<T, TOut> func);

        public static implicit operator Option<T>(T value) => Pure(value);
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
        public None()
        {
        }

        public override T GetOrElse(T @default) => @default;

        public override Option<TOut> Map<TOut>(Func<T, TOut> func)
        {
            return new None<TOut>();
        }

        public override bool Equals(object obj) =>
            obj is None<T>;

        public override int GetHashCode() => 0;
    }
}
