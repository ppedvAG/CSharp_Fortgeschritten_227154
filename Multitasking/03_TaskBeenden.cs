namespace Multitasking;

internal class _03_TaskBeenden
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new(); //Sender, erstellt beliebig viele Tokens
		CancellationToken ct = cts.Token; //Empfänger, vom Sender angesteuert

		Task t = new Task(Run, ct); //Hier Token übergeben
		t.Start();

		Thread.Sleep(1000);

		cts.Cancel(); //Der Source sagen das alle Tokens von der Source gecancelled werden sollen

		Console.ReadKey();
	}

	static void Run(object o) //Object o muss gegeben sein für CancellationToken
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				ct.ThrowIfCancellationRequested(); //Task wirft Exception ist aber nicht sichtbar
				Console.WriteLine(i);
				Thread.Sleep(100);
			}
		}
	}
}
