namespace Generics;

internal class Constraints
{
	static void Main(string[] args)
	{

	}

	public class DataStore1<T> where T : class { } //T muss ein Referenztyp sein (string, Objekte, ...)

	public class DataStore2<T> where T : struct { } //T muss ein Wertetyp sein (int, bool, ...)

	public class DataStore3<T> where T : Constraints { } //T muss von Constraints erben oder selbst die Klasse sein
														 
	public class DataStore4<T> where T : new() { } //T muss einen Standardkonstruktor haben

	public class DataStore5<T> where T : Enum { } //T muss ein Enum sein (aber kein Enumwert)

	public class DataStore6<T> where T : Delegate { } //T muss ein Delegate sein

	public class DataStore7<T> where T : unmanaged { }
	//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/unmanaged-types

	public class DataStore8<T> where T : class, Enum, new() { } //Mehrere Constraints auf ein Generic

	public class DataStore9<T1, T2> //Mehrere Constraints auf mehrere Generics
		where T1 : class
		where T2 : unmanaged
	{
	
	}

	public void Test<MyType>() where MyType : struct //Constraints bei Methode hinzufügen
	{

	}
}
