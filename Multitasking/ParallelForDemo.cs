using System.Diagnostics;

namespace Multitasking;

internal class ParallelForDemo
{
	static void Main(string[] args)
	{
		int[] durchgänge = { 1000, 10000, 50000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000 };
		double[] verhältnis = new double[durchgänge.Length];
		for (int i = 0; i < durchgänge.Length; i++)
		{
			int d = durchgänge[i];
			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(d);
			sw.Stop();
			Console.WriteLine($"For Durchgänge: {d}: {sw.ElapsedMilliseconds}");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(d);
			sw2.Stop();
			Console.WriteLine($"ParallelFor Durchgänge: {d}: {sw2.ElapsedMilliseconds}");

			//Console.WriteLine(((double) sw2.ElapsedMilliseconds / (sw.ElapsedMilliseconds == 0 ? 1 : sw.ElapsedMilliseconds) * 100) + "%");
		}

		/*
		    For Durchgänge: 1000: 0
			ParallelFor Durchgänge: 1000: 17
			For Durchgänge: 10000: 1
			ParallelFor Durchgänge: 10000: 0
			For Durchgänge: 50000: 7
			ParallelFor Durchgänge: 50000: 5
			For Durchgänge: 100000: 10
			ParallelFor Durchgänge: 100000: 3
			For Durchgänge: 250000: 29
			ParallelFor Durchgänge: 250000: 44
			For Durchgänge: 500000: 62
			ParallelFor Durchgänge: 500000: 20
			For Durchgänge: 1000000: 116
			ParallelFor Durchgänge: 1000000: 103
			For Durchgänge: 5000000: 512
			ParallelFor Durchgänge: 5000000: 197
			For Durchgänge: 10000000: 1318
			ParallelFor Durchgänge: 10000000: 227
			For Durchgänge: 100000000: 10086
			ParallelFor Durchgänge: 100000000: 2449
		 */
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		//int i = 0; i < iterations; i++
		Parallel.For(0, iterations, i => erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100));
	}
}
