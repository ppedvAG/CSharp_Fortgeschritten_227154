namespace Multitasking;

internal class _06_WhenAll
{
	static void Main(string[] args)
	{
		List<Task<int>> tasks = new();

		Task.WhenAll(tasks) //WhenAll: Warte auf die Ergebnisse von mehreren Tasks
			.ContinueWith(t => Console.WriteLine(t.Result.Sum()));
	}
}
