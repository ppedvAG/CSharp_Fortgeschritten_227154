namespace Generics;

internal class Generics
{
	static void Main(string[] args)
	{
		List<string> list = new List<string>(); //Generic: T wird nach unten übernommen (hier T = string)
		list.Add("123");

		List<int> ints = new(); //T wird durch int ersetzt
		ints.Add(123); //Add(T) -> Add(int)

		Dictionary<string, int> dict = new Dictionary<string, int>(); //Klasse mit 2 Generics: TKey -> string, TValue -> int
		dict.Add("123", 123); //Add(TKey, TValue) -> Add(string, int)
	}
}

public class DataStore<T> :
	IProgress<T>, //T bei Vererbung weitergeben
	IEquatable<int> //T statt fixer Typ
{
	public T[] data { get; set; } //T bei einem Feld/Property

	public List<T> Data => data.ToList(); //Generic nach unten weitergeben

	public void Add(int index, T item) //T bei Parameter
	{
		data[index] = item;
	}

	public T Get(int index) //T als Rückgabewert
	{
		if (index < 0 || index >= data.Length)
			return default(T); //default(T): Standardwert von T (int: 0, string: null, bool: false, ...)
		return data[index];
	}

	public void Report(T value) //T kommt von Interface
	{

	}

	public bool Equals(int other) //Hier kommt der fixe Typ von oben
	{
		return true;
	}

	public void PrintType<MyType>()
	{
		Console.WriteLine(default(MyType)); //default(T): Standardwert von T (int: 0, string: null, bool: false, ...)
		Console.WriteLine(typeof(MyType)); //Typ von dem Generic
		Console.WriteLine(nameof(MyType)); //Typ als string (z.B.: "int")
	}
}

public class DataStore2<T> : DataStore<T>
{

}