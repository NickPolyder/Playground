namespace Playground.NETCORE
{
    public interface ITestCase
    {
        string Name { get; }
        void Run();
    }
}
