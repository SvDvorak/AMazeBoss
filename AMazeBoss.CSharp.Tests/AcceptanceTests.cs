namespace AMazeBoss.CSharp.Tests
{
    public abstract class AcceptanceTests<T> where T : AcceptanceTests<T>
    {
        public T Given => (T) this;
        public T When => (T) this;
        public T Then => (T) this;
    }
}