using System.Collections;

namespace Sonstiges;

internal class Program
{
	static void Main(string[] args)
	{
		Wagon w1 = new();
		Wagon w2 = new();
		Console.WriteLine(w1 == w2);

		Zug z = new();
		z += w1;
		z += w2;
		z++;
		z++;
		z++;
		z++;
		z++;

		foreach (Wagon w in z)
		{
			Console.WriteLine(w.GetHashCode());
		}

		Console.WriteLine(z[2]);
		//z[2] = new Wagon();
		Console.WriteLine(z["Rot"]);
		Console.WriteLine(z[30, "Rot"]);

		var x = z.Wagons.Select(e => new { e.Farbe, HC = e.GetHashCode() }).ToList();
		Console.WriteLine(x[0].HC);

		System.Timers.Timer timer = new();
		timer.Interval = 1000;
		timer.Elapsed += (sender, e) => Console.WriteLine("1s vergangen");
		timer.Start(); //Timer arbeitet mit Tasks -> Main Thread blockieren

		Console.ReadKey();
	}
}

public class Zug : IEnumerable
{
	public List<Wagon> Wagons = new();

	public IEnumerator GetEnumerator()
	{
		return Wagons.GetEnumerator();
	}

	public static Zug operator +(Zug z, Wagon w)
	{
		z.Wagons.Add(w);
		return z;
	}

	public static Zug operator ++(Zug z)
	{
		z.Wagons.Add(new Wagon());
		return z;
	}

	public Wagon this[int index]
	{
		get => Wagons[index];
	}

	public Wagon this[string farbe]
	{
		get => Wagons.First(e => e.Farbe == farbe);
	}

	public Wagon this[int anzSitze, string farbe]
	{
		get => Wagons.First(e => e.AnzSitze == anzSitze && e.Farbe == farbe);
	}
}

public class Wagon
{
	public int AnzSitze { get; set; }

	public string Farbe { get; set; }

	public static bool operator ==(Wagon a, Wagon b)
	{
		return a.AnzSitze == b.AnzSitze && a.Farbe == b.Farbe;
	}

	public static bool operator !=(Wagon a, Wagon b)
	{
		return !(a == b);
	}
}