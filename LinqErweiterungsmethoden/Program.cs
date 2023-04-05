using System.Diagnostics;
using System.Text;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Einfaches Linq
		List<int> ints = Enumerable.Range(1, 20).ToList();

		Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());
		Console.WriteLine(ints.Sum());

		Console.WriteLine(ints.First()); //Gibt das erste Element zurück, Exception wenn kein Element existiert oder wenn die Bedingung kein Element findet
		Console.WriteLine(ints.FirstOrDefault()); //Gibt das erste Element zurück, null wenn kein Element gefunden

		Console.WriteLine(ints.Last());
		Console.WriteLine(ints.LastOrDefault());

		Console.WriteLine(ints.First(e => e % 5 == 0)); //Die erste Zahl die durch 5 teilbar ist
		Console.WriteLine(ints.Last(e => e % 5 == 0)); //Finde die letzte Zahl in der Liste die durch 5 teilbar ist
		//Console.WriteLine(ints.First(e => e % 50 == 0)); //Exception
		Console.WriteLine(ints.FirstOrDefault(e => e % 50 == 0));
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Vergleich Linq Schreibweisen
		//Alle BMWs finden
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);

		//Standard-Linq: SQL-ähnliche Schreibweise (alt)
		List<Fahrzeug> bmwsAlt = (from f in fahrzeuge
								  where f.Marke == FahrzeugMarke.BMW
								  select f).ToList();

		//Methodenkette
		List<Fahrzeug> bmwsNeu = fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).ToList();
		#endregion

		#region Linq mit Lambda
		//Alle Fahrzeuge mit MaxV >= 200
		fahrzeuge.Where(e => e.MaxV >= 200);

		//Alle VWs mit MaxV >= 200
		fahrzeuge.Where(e => e.MaxV >= 200 && e.Marke == FahrzeugMarke.VW);

		//Autos nach Marke
		fahrzeuge.OrderBy(e => e.Marke); //Originale Liste wir NICHT verändert
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV);
		fahrzeuge.OrderByDescending(e => e.Marke).ThenByDescending(e => e.MaxV);

		//Select: Liste in eine neue Form umwandeln
		fahrzeuge.Select(e => e.Marke); //Liste von Marken aus der Fahrzeugliste entnehmen
		fahrzeuge.Select(e => e.Marke).Distinct(); //Marken eindeutig machen
		fahrzeuge.Select(e => e.MaxV); //Liste von allen Geschwindigkeiten (int)

		//Praktisches Beispiel
		//Ohne Select
		string[] pfade = Directory.GetFiles(@"C:\Windows\System32");
		List<string> p = new();
		foreach (string s in pfade)
			p.Add(Path.GetFileNameWithoutExtension(s));

		//Mit Select
		Directory.GetFiles(@"C:\Windows\System32").Select(e => Path.GetFileNameWithoutExtension(e));

		//Fahren alle unsere Fahrzeuge mindestens 200km/h?
		fahrzeuge.All(e => e.MaxV >= 200);

		//Fährt mindestens eines unserer Fahrzeuge 200km/h+?
		fahrzeuge.Any(e => e.MaxV >= 200);

		//Enhält die Liste Elemente?
		fahrzeuge.Any(); //fahrzeuge.Count > 0

		//Wieviele Audis haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.Audi);

		//Wie schnell sind unsere Autos im Durchschnitt?
		fahrzeuge.Select(e => e.MaxV).Average();
		fahrzeuge.Average(e => e.MaxV); //Vereinfachung von oben

		fahrzeuge.Sum(e => e.MaxV);

		fahrzeuge.Min(e => e.MaxV); //Die kleinste Geschwindigkeit
		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der kleinsten Geschwindigkeit

		fahrzeuge.Max(e => e.MaxV); //Die größte Geschwindigkeit
		fahrzeuge.MaxBy(e => e.MaxV); //Das Fahrzeug mit der größten Geschwindigkeit

		//Linq Abfragen vereinfachen
		fahrzeuge.Where(e => e.MaxV >= 200).Count();
		fahrzeuge.Count(e => e.MaxV >= 200);
		fahrzeuge.Where(e => e.MaxV >= 200).First();
		fahrzeuge.First(e => e.MaxV >= 200);

		fahrzeuge.Select(e => e.MaxV).Sum();
		fahrzeuge.Sum(e => e.MaxV);
		fahrzeuge.Select(e => e.MaxV).Average();
		fahrzeuge.Average(e => e.MaxV);

		//Fahrzeuge in X große Teile aufteilen
		fahrzeuge.Chunk(5);

		//Überspringe die ersten 2 Elemente, nimm die nächsten 5 Elemente
		fahrzeuge.Skip(2).Take(5);

		//Nimm die schnellsten 5 Fahrzeuge
		fahrzeuge
			.OrderByDescending(e => e.MaxV)
			.Take(5); //beliebig viele Umbrüche möglich

		//IDs hinzufügen
		fahrzeuge.Zip(Enumerable.Range(0, fahrzeuge.Count)); //falsche Reihenfolge
		IEnumerable<(int First, Fahrzeug Second)> x = Enumerable.Range(0, fahrzeuge.Count).Zip(fahrzeuge);

		Dictionary<int, Fahrzeug> dict = x.ToDictionary(e => e.First, e => e.Second);

		//Nach Marke gruppieren
		fahrzeuge.GroupBy(e => e.Marke);

		Dictionary<FahrzeugMarke, List<Fahrzeug>> grouped = fahrzeuge.GroupBy(e => e.Marke).ToDictionary(e => e.Key, e => e.ToList()); //ToList auf ein IGrouping gibt die Liste der Fahrzeuge
		Console.WriteLine(grouped[FahrzeugMarke.Audi]);

		//Aggregate
		//Schönen Output erzeugen
		Console.WriteLine(fahrzeuge.Aggregate("", (agg, fzg) => agg + $"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxV}km/h fahren.\n"));

		//Ohne Linq (23 Zeilen)
		HttpClient client = new();
		string str = Task.Run(() => client.GetStringAsync("http://www.gutenberg.org/files/54700/54700-0.txt")).Result;
		string[] words = str.Split(new char[] { ' ', '\n', ',', '.', ';', ':', '-', '_', '/' }, StringSplitOptions.RemoveEmptyEntries);

		Dictionary<string, int> anzahlen = new();
		foreach (string wort in words)
		{
			if (wort.Length > 6)
			{
				if (!anzahlen.ContainsKey(wort.ToLower()))
					anzahlen.Add(wort.ToLower(), 0);
				anzahlen[wort.ToLower()]++;
			}
		}

		IEnumerable<KeyValuePair<string, int>> top10 = anzahlen.OrderByDescending(e => e.Value).Take(10);

		StringBuilder sb = new StringBuilder();
		sb.AppendLine("Die häufigsten Wörter sind:");
		foreach (var v in top10)
		{
			sb.AppendLine("  " + v);
		}
		Console.WriteLine(sb.ToString());

		//Mit Linq
		IEnumerable<KeyValuePair<string, int>> a = words
			.Where(e => e.Length > 6)
			.GroupBy(e => e.ToLower())
			.ToDictionary(e => e.Key, e => e.Count())
			.OrderByDescending(e => e.Value)
			.Take(10);

		foreach (KeyValuePair<string, int> kv in a)
			Console.WriteLine(kv);

		//Mit Aggregate
		Console.WriteLine
		(
			words
				.Where(e => e.Length > 6)
				.GroupBy(e => e.ToLower())
				.ToDictionary(e => e.Key, e => e.Count())
				.OrderByDescending(e => e.Value)
				.Take(10)
				.Aggregate(string.Empty, (agg, e) => $"{agg}{e} \n")
		);
		#endregion

		#region Erweiterungsmethoden
		int i = 385792;
		i.Quersumme();
		Console.WriteLine(3489725.Quersumme());

		fahrzeuge.Shuffle();
		dict.Shuffle();
		new int[] { 1, 2, 3 }.Shuffle();
		#endregion
	}
}

[DebuggerDisplay("Marke: {Marke}, Geschwindigkeit: {MaxV} - {typeof(Fahrzeug).FullName}")]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }