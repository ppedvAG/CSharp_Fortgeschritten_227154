using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		Program p = new();
		Type pt = p.GetType(); //Typ holen mit GetType() über Objekt
		Type t = typeof(Program); //Typ holen durch typeof(<Klassenname>)

		object o = Activator.CreateInstance(pt); //Objekt über Typ erstellen

		//////////////////////////////////////////////

		pt.GetMethods(); //alle Methoden anschauen
		pt.GetMethod("Test").Invoke(o, null);
		pt.GetMethod("Test2").Invoke(o, new[] { "Zwei Test" });

		pt.GetField("Zahl").SetValue(o, 5);
		Console.WriteLine(pt.GetField("Zahl").GetValue(o));

		//////////////////////////////////////////////
		
		object o2 = Activator.CreateInstance("Reflection", "Reflection.Program"); //Objekt nur über strings erstellen

		Assembly assembly = Assembly.GetExecutingAssembly(); //Informationen über das derzeitige "Projekt" erhalten

		assembly.GetTypes(); //Alle Typen aus einem Assembly holen

		Type loadedType = assembly.GetType("Program"); //Typ über Name holen

		//////////////////////////////////////////////

		Assembly a = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_04_03\DelegatesEvents\bin\Debug\net7.0\DelegatesEvents.dll");

		Type compType = a.GetType("DelegatesEvents.Component"); //Komponente finden

		object comp = Activator.CreateInstance(compType);
		compType.GetEvent("ProcessCompleted").AddEventHandler(comp, () => Console.WriteLine("Reflection Prozess fertig"));
		compType.GetEvent("Progress").AddEventHandler(comp, (int i) => Console.WriteLine($"Reflection Fortschritt: {i}"));
		compType.GetMethod("StartProcess").Invoke(comp, null);
	}

	public int Zahl;

	public void Test() => Console.WriteLine("Ein Test");

	public void Test2(string text) => Console.WriteLine(text);
}