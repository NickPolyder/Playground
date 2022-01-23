namespace Playground.NETFRAMEWORK.Models
{
    public class ClassWithPrivateConstructor
    {
        public int Number { get; }

        private ClassWithPrivateConstructor()
        {
            Number = -99;
        }

        public ClassWithPrivateConstructor(int i)
        {
            Number = i;
        }

        public override string ToString()
        {
            return GetType().Name + $" Property {nameof(Number)}: {Number}";
        }
    }
}