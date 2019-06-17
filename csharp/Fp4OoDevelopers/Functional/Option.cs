namespace Fp4OoDevelopers.Functional
{
    public abstract class Option<T>
    {
    }

    public class Some<T> : Option<T> 
    {
        private readonly T value;

        public Some(T value)
        {
            this.value = value;
        }
    }

    public class None<T> : Option<T>
    {
        public None()
        {
        }
    }
}