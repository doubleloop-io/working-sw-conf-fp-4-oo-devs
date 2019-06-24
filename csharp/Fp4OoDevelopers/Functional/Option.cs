﻿namespace Fp4OoDevelopers.Functional
{
    public abstract class Option<T>
    {
        public static Option<T> Pure(T value) =>
            value == null ? (Option<T>)new None<T>() : new Some<T>(value);
    }

    public class Some<T> : Option<T> 
    {
        private readonly T value;

        public Some(T value)
        {
            this.value = value;
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

        public override bool Equals(object obj) =>
            obj is None<T>;

        public override int GetHashCode() => 0;
    }
}