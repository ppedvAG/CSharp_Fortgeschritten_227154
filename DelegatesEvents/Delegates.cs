namespace DelegatesEvents;

internal class Delegates
{
	public delegate void Vorstellungen(string name); //Definition von Delegate, speichert Referenzen auf Methoden, können zur Laufzeit hinzugefügt und weggenommen werden

	static void Main(string[] args)
	{
		Vorstellungen v = new Vorstellungen(VorstellungDE); //Erstellung von Delegate + Initialmethoed
		v("Max"); //Delegate ausführen

		v += VorstellungEN; //Methode anhängen
		v += VorstellungEN;
		v += VorstellungEN; //Selbe Methode kann mehrmals angehängt werden
		v("Max");

		v -= VorstellungDE;
		v -= VorstellungDE;
		v -= VorstellungDE; //Methode die nicht dran ist, gibt keinen Fehler wenn sie abgenommen wird
		v("Max");

		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN; //Delegate ist null wenn die letzte Methode abgenommen wird
		//v("Max");

		if (v is not null)
			v("Max");

		v?.Invoke("Max"); //Schneller Null-Check (Null propagation): wenn v nicht null ist, führe den Code danach aus

		List<int> ints = null;
		ints?.Add(11); //Wenn ints nicht null ist, führe den Code danach aus

		foreach (Delegate dg in v.GetInvocationList()) //Delegate anschauen
		{
			Console.WriteLine(dg.Method.Name);
		}
	}

	static void VorstellungDE(string name)
	{
		Console.WriteLine($"Hallo mein Name ist {name}");
	}

	static void VorstellungEN(string name) 
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}