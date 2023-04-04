using Microsoft.VisualBasic.FileIO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Test Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//NewtonsoftJson();

		//SystemTextJson();

		//XML();

		//Binary();

		//CSV();
	}

	static void NewtonsoftJson()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Test Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//JsonSerializerSettings settings = new(); //Einstellungen müssen beim (De-)Serialisieren übergeben werden
		//settings.Formatting = Formatting.Indented; //Json schön schreiben
		//settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbungshierarchien speichern
		//settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //Kreisbezüge verhindern

		//string json = JsonConvert.SerializeObject(fahrzeuge, settings);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson, settings);

		////////////////////////////////////////////////////////////////

		//JToken token = JToken.Parse(readJson);
		//foreach (JToken jt in token)
		//{
		//	Console.WriteLine(jt["MaxV"].Value<int>()); //Mit [] auf ein Feld zugreifen, mit Value<T> in einen Typen konvertieren

		//	Fahrzeug f = JsonConvert.DeserializeObject<Fahrzeug>(jt.ToString());
		//}
	}

	static void SystemTextJson()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Test Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		JsonSerializerOptions options = new();
		options.WriteIndented = true; //Schön schreiben
		options.ReferenceHandler = ReferenceHandler.IgnoreCycles; //Kreisbezüge ignorieren

		string json = JsonSerializer.Serialize(fahrzeuge, options);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		Fahrzeug[] readFzg = JsonSerializer.Deserialize<Fahrzeug[]>(readJson, options);

		///////////////////////////////////////////////////////

		JsonDocument doc = JsonDocument.Parse(readJson);
		foreach (JsonElement je in doc.RootElement.EnumerateArray())
		{
			Console.WriteLine(je.GetProperty("MaxV").GetInt32());

			Fahrzeug f = je.Deserialize<Fahrzeug>();
			Console.WriteLine(f.MaxV);
		}
	}

	static void XML()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Test Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		XmlSerializer xml = new(fahrzeuge.GetType());
		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		{
			xml.Serialize(fs, fahrzeuge);
		}

		using (FileStream readStream = new FileStream(filePath, FileMode.Open))
		{
			List<Fahrzeug> readFzg = xml.Deserialize(readStream) as List<Fahrzeug>;
		}

		//////////////////////////////////////////////////

		XmlDocument doc = new XmlDocument();
		doc.LoadXml(File.ReadAllText(filePath));

		foreach (XmlNode node in doc.ChildNodes[1]) //Header überspringen
		{
			Console.WriteLine(node.ChildNodes[1].InnerText);

			Console.WriteLine(node.Attributes[0].InnerText);
		}
	}

	static void Binary()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Test Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		BinaryFormatter formatter = new();
		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		{
			formatter.Serialize(fs, fahrzeuge);
		}

		using (FileStream readStream = new FileStream(filePath, FileMode.Open))
		{
			List<Fahrzeug> readFzg = formatter.Deserialize(readStream) as List<Fahrzeug>;
		}
	}

	static void CSV()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Test Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		TextFieldParser tfp = new TextFieldParser(filePath);
		tfp.SetDelimiters(",");

		//string file = tfp.ReadToEnd();
		//string[] lines = file.Split("\n");
		//foreach (string line in lines)
		//{
		//	//Etwas machen mit jeder CSV Zeile
		//}

		string[] headers = tfp.ReadFields();
		while (!tfp.EndOfData)
		{
			string[] derzeitigeZeile = tfp.ReadFields();
			//Zeile zu Objekt konvertieren
		}


		string fzg = "";
		foreach (Fahrzeug f in fahrzeuge)
		{
			fzg += string.Join(';', f.GetType().GetProperties().Select(e => e.GetValue(f).ToString()));
			fzg += Environment.NewLine;
		}
	}
}

//Vererbung mit System.Text.Json
[JsonDerivedType(typeof(Fahrzeug), "F")] //Oberklasse selbst muss auch angegeben werden
//[JsonDerivedType(typeof(PKW), "P")]
[Serializable] //Für Binary
public class Fahrzeug
{
	//Newtonsoft.Json Attribute
	//[JsonIgnore]
	//[JsonProperty("Identifier")]
	public int ID { get; set; }

	//System.Text.Json Attribute
	//[JsonIgnore]
	//[JsonPropertyName("Maximalgeschwindigkeit")]
	public int MaxV { get; set; }

	//[XmlIgnore]
	//[XmlElement("M")]
	//[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	//[field: NonSerialized] //BinaryIgnore
	public int AnzReifen { get; set; }

	public Fahrzeug(int ID, int MaxV, FahrzeugMarke Marke)
	{
		this.ID = ID;
		this.MaxV = MaxV;
		this.Marke = Marke;
	}

	public Fahrzeug()
	{
		//Für XML
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }