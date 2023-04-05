using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Progress.Value++;
		} //UI Thread wird blockiert
	}

	private void Button_Click1(object sender, RoutedEventArgs e)
	{
		Task.Run(() => //UI Updates sind nicht erlaubt von einem Side Thread/Task
		{
			Dispatcher.Invoke(() => Progress.Value = 0); //Dispatcher.Invoke um UI Updates auf dem Main Thread auszuführen
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Dispatcher.Invoke(() => Progress.Value++);
			}
		});
	}

	private async void Button_Click2(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		for (int i = 0; i < 100; i++)
		{
			await Task.Delay(25);
			Progress.Value++;
		}
	}

	private async void Button_Click3(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		Progress.Maximum = 3;
		using (HttpClient client = new HttpClient())
		{
			Task<HttpResponseMessage> response = client.GetAsync(@"http://www.gutenberg.org/files/54700/54700-0.txt");
			//UI Update
			TB.Text = "Text lädt...";
			Progress.Value++;
			HttpResponseMessage message = await response;
			//UI Update
			TB.Text = "Text wird angezeigt...";
			Progress.Value++;
			await Task.Delay(1000);
			string text = await message.Content.ReadAsStringAsync();
			TB.Text = text;
			Progress.Value++;
		}
	}

	private async void Button_Click4(object sender, RoutedEventArgs e)
	{
		List<int> ints = Enumerable.Range(0, 10_000_000).ToList();
		Stopwatch sw = Stopwatch.StartNew();
		await Parallel.ForEachAsync(ints, (item, ct) =>
		{
			Console.WriteLine(item * 10);
			return ValueTask.CompletedTask; //Leeren Task zurückgeben
		});
		sw.Stop();
		TB.Text = sw.ElapsedMilliseconds.ToString();
	}
}
