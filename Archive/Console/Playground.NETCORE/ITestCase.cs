namespace Playground.NETCORE
{
    public interface ITestCase
    {
        bool Enabled { get; }
        string Name { get; }
        void Run();
    }
}
