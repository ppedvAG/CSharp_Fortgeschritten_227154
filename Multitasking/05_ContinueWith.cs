namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		Task<double> t = Task.Run(() =>
		{
			Thread.Sleep(1000);
			//throw new Exception();
			return Math.Pow(4, 30);
		});
		t.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result)); //Führt diesen Task immer aus (außer bei Exception)
		//Tasks verketten, Code wird der ausgeführt wenn der vorherige Task fertig ist
		//Verhindert Blockieren des Main Threads
		//Ergebnis des vorherigen Tasks kann verwendet werden
		t.ContinueWith(vorherigerTask => Console.WriteLine("Exception"), TaskContinuationOptions.OnlyOnFaulted); //Führt diesen Task nur bei einer Exception aus
		t.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result * 2)); //Hier werden jetzt 2 Tasks ausgeführt

		for (int i = 0; i < 20; i++)
		{
			Thread.Sleep(100);
			Console.WriteLine($"Main Thread: {i}");
		}

		Console.ReadKey();
	}
}
