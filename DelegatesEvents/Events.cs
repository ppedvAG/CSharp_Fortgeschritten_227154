namespace DelegatesEvents;

internal class Events
{
	static event EventHandler TestEvent; //event: Statischer Punkt an den Methoden angehängt werden können

	static event EventHandler<TestEventArgs> ArgsEvent;

	static event EventHandler<int> IntEvent;

	static void Main(string[] args)
	{
		TestEvent += Events_TestEvent; //Methode anhängen ohne new, Event kann nicht erstellt werden
		TestEvent(null, EventArgs.Empty); //Event ausführen
		TestEvent += (sender, args) => Console.WriteLine("Eine anonyme Methode"); //Anonyme Methode anhängen
		TestEvent(null, EventArgs.Empty); //Event ausführen

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent(null, new TestEventArgs() { Status = "Ein Event" });

		IntEvent += Events_IntEvent;
	}

	private static void Events_TestEvent(object sender, EventArgs e)
	{
		Console.WriteLine("TestEvent ausgeführt");
	}

	private static void Events_ArgsEvent(object sender, TestEventArgs e)
	{
		Console.WriteLine(e.Status);
	}

	private static void Events_IntEvent(object sender, int e)
	{
		Console.WriteLine(e);
	}
}

internal class TestEventArgs : EventArgs
{
	public string Status { get; set; }
}