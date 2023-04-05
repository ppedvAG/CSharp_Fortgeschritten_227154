using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Geschirr();
		//Kaffee();
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//sw.Restart();
		//Task t = Task.Run(() => 
		//{
		//	Toast();
		//	Geschirr();
		//	Kaffee();
		//});
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //0s, Main Thread läuft weiter

		//sw.Restart();
		//Task t = Task.Run(Toast);
		//Task t2 = Task.Run(Geschirr);
		//Task t3 = Task.Run(Kaffee);
		//t.ContinueWith(v =>
		//{
		//	sw.Stop();
		//	Console.WriteLine(sw.ElapsedMilliseconds);
		//});
		//Task.WaitAll(t, t2, t3);

		//sw.Restart();
		//await ToastAsync(); //async Methoden werden immer als Tasks gestartet -> Main Thread läuft weiter
		//await GeschirrAsync(); //void Methoden können nicht awaited werden -> Rückgabewert muss Task sein
		//await KaffeeAsync(); //await: Warte hier bis der gegebene Task fertig ist
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//sw.Restart();
		//Task t1 = ToastAsync();
		//Task t2 = GeschirrAsync();
		//await t2; //Warte bis die Tasse hergerrichtet ist, starte danach den Kaffee
		//Task t3 = KaffeeAsync();
		//await t3;
		//await t1;
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//sw.Restart();
		//Task<Toast> t1 = ToastObjectAsync();
		//Task<Tasse> t2 = GeschirrObjectAsync();
		//Tasse tasse = await t2; //Warte bis die Tasse hergerrichtet ist und gib das fertige Objekt aus
		//Task<Kaffee> t3 = KaffeeObjectAsync(tasse); //Fertige Tasse aus t2 hier übergeben
		//Kaffee kaffee = await t3;
		//Toast toast = await t1;
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		sw.Restart();
		Task<Toast> t1 = ToastObjectAsync();
		Task<Tasse> t2 = GeschirrObjectAsync();
		Task<Kaffee> t3 = KaffeeObjectAsync(await t2);
		await Task.WhenAll(t1, t2, t3); //WhenAll: awaite mehrere Tasks
		sw.Stop();
		Console.WriteLine(sw.ElapsedMilliseconds); //4s
	}

	static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	static void Geschirr()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Geschirr hergerrichtet");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}

	static async Task ToastAsync() //Task als Rückgabewert braucht kein return
	{
		await Task.Delay(4000); //= Thread.Sleep
		Console.WriteLine("Toast fertig");
	}

	static async Task GeschirrAsync() //Task als Rückgabewert braucht kein return
	{
		await Task.Delay(1500); //= Thread.Sleep
		Console.WriteLine("Geschirr hergerrichtet");
	}

	static async Task KaffeeAsync() //Task als Rückgabewert braucht kein return
	{
		await Task.Delay(1500); //= Thread.Sleep
		Console.WriteLine("Kaffee fertig");
	}

	static async Task<Toast> ToastObjectAsync() //Rückgabewert über Generic im Task vorgeben
	{
		await Task.Delay(4000); //= Thread.Sleep
		Console.WriteLine("Toast fertig");
		return new Toast(); //Wenn der Task fertig ist, kommt ein Toast als Ergebnis heraus
	}

	static async Task<Tasse> GeschirrObjectAsync()
	{
		await Task.Delay(1500); //= Thread.Sleep
		Console.WriteLine("Geschirr hergerrichtet");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500); //= Thread.Sleep
		Console.WriteLine("Kaffee fertig");
		return new Kaffee();
	}
}

public class Toast { }

public class Tasse { }

public class Kaffee { }