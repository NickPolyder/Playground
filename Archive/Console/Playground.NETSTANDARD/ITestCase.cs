namespace Playground.NETSTANDARD
{
    public interface ITestCase
    {
        bool Enabled { get; }
        string Name { get; }
        void Run();
    }
}
