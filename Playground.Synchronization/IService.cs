using System.Threading.Tasks;

namespace Playground.Synchronization
{
	public interface IServiceA
	{
		Task Call(string with);
	}
	
	public interface IServiceB
	{
		Task<string> GetFrom();
	}
}