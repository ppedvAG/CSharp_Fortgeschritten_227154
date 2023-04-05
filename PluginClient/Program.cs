using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_04_03\PluginCalculator\bin\Debug\net7.0\PluginCalculator.dll";
		Assembly loaded = Assembly.LoadFrom(pfad);
		//Type t = loaded.GetType("PluginCalculator.CalculatorPlugin");
		Type t = loaded.GetTypes().First(e => e.GetInterface(typeof(IPlugin).Name) != null); //Plugin dynamisch aus der DLL finden
		IPlugin plugin = Activator.CreateInstance(t) as IPlugin;

		Console.WriteLine($"Name: {plugin.Name}");
		Console.WriteLine($"Desc: {plugin.Description}");
		Console.WriteLine($"Version: {plugin.Version}");
		Console.WriteLine($"Autor: {plugin.Author}");

		Console.WriteLine();

		List<MethodInfo> methods = t.GetMethods().Where(e => e.GetCustomAttribute(typeof(ReflectionVisible)) != null).ToList();
		Console.WriteLine("Wähle eine Methode aus:");
		for (int i = 0; i < methods.Count; i++)
		{
			Console.WriteLine($"{i}: {methods[i].Name}");
		}
		
		int auswahl = int.Parse(Console.ReadLine());

		List<object> inputValues = new();
		foreach (ParameterInfo par in methods[auswahl].GetParameters())
		{
			Console.WriteLine($"{par.Name} ({par.ParameterType}) eingeben: ");
			object input = Convert.ChangeType(Console.ReadLine(), par.ParameterType);
			inputValues.Add(input);
		}

		Console.WriteLine(methods[auswahl].Invoke(plugin, inputValues.ToArray()));
	}
}