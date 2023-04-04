using System.Collections.Concurrent;

namespace Multitasking;

internal class ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> list = new(); //Thread-sichere Liste
		list.Add(1);

		ConcurrentDictionary<string, int> dict = new(); //Thread-sicheres Dictionary
		dict.TryAdd("a", 1); //Äquivalent zu Add im normalen Dictionary
	}
}
