namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine();
		int x = 5;

		string text = "wOrT";
		string t = char.ToUpper(text[0]) + text[1..].ToLower();
		Console.WriteLine(t);

		void Test()
		{
			Console.WriteLine(x);
		}

		//Verbatim-String: Escape Sequenzen werden ignoriert
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_04_03";

		//Interpolated-String: Code in Strings einbauen
		string interpolated = $$"""Der Text ist: {{t}}, die {Zahl} "mal" Zwei "ist": {{x * 2}}, der heutige Tag ist: """ +
			$"{DateTime.Now.DayOfWeek switch
			{
				DayOfWeek.Monday => 1,
				DayOfWeek.Tuesday => 2,
				DayOfWeek.Wednesday => 3,
				_ => 0
			}}";
	}

	public int Test()
	{
		return DayOfWeek.Monday switch //Strg + . -> Schnelloptionen
		{
			DayOfWeek.Monday => 1,
			DayOfWeek.Tuesday => 2,
			DayOfWeek.Wednesday => 3,
			_ => 0
		};
	}

	public int CalcGehalt()
	{
		Person p = new(33, "Max", "Eine Beschreibung", "");
		//p.Author = "";
		StreamWriter sw = new("Pfad");

		//var(alter, name, desc, author) = p;
		//Console.WriteLine(alter);

		if (p == null || p != null)
		{

		}

		if (p is null || p is not null)
		{

		}

		switch (DateTime.Now.DayOfWeek)
		{
			case >= DayOfWeek.Monday and <= DayOfWeek.Friday:
				Console.WriteLine("Wochentag");
				break;
			case DayOfWeek.Saturday or DayOfWeek.Sunday:
				Console.WriteLine("Wochenende");
				break;
			default:
				Console.WriteLine("Anderer Tag");
				break;
		}

		return p switch
		{
			var (alter, name, desc, author) when alter >= 20 && name == "Max" => 10000
		};


		//return p switch
		//{
		//	{ Alter: 1, Name: "Max" } => 1,
		//	{ Name: "Peter" } => 2,
		//	{ Author: "Ein Autor", Description: "Eine Beschreibung" } => 5
		//};

		//return p switch
		//{
		//	{ Name.Length: 5 } => 1,
		//	{ Name: { Length: 6 } } => 1
		//};
	}
}

//public class Person
//{
//	public int Alter { get; set; }

//	public string Name { get; set; }

//	public string Description { get; set; }

//	public string Author { get; init; }

//	public Person() 
//	{
//		Author = string.Empty;
//	}

//	public void Deconstruct(out int Alter, out string Name, out string Description, out string Author)
//	{
//		Alter = this.Alter;
//		Name = this.Name;
//		Description = this.Description;
//		Author = this.Author;
//	}
//}

public record Person(int Alter, string Name, string Description, string Author)
{
	public void Test()
	{
		//Record auch erweiterbar wie normale Klasse
	}
}

public interface IArbeit
{
	static int Wochenstunden = 40;

	string Job { get; set; }

	void Lohnauszahlung();

	public void Test()
	{
		//Bad Practice
	}
}