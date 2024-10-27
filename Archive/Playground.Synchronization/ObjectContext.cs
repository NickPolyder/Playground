using System.Collections.Generic;

namespace Playground.Synchronization
{

	public interface IObjectContext
	{
		List<int> CalledFromThreads { get; set; } 

		int CurrentThread { get; set; }

		string Data { get; set; }

		string User { get; set; }
	}
	public class ObjectContext: IObjectContext
	{
		public List<int> CalledFromThreads { get; set; } = new List<int>();

		public int CurrentThread { get; set; }

		public string Data { get; set; }

		public string User { get; set; }
	}
}