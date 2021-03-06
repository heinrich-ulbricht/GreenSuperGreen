﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

// ReSharper disable CheckNamespace

namespace GreenSuperGreen.Benchmarking
{
	public abstract class BenchmarkWorker : BenchmarkConfiguration, IBenchmarkWorker
	{
		private Stopwatch StopWatchTimer { get; set; } = new Stopwatch();

		public long ElapsedMilliseconds { get; private set; }
		public int Iterations { get; private set; }
		public string Pair { get; }
		public string ResourceName { get; }
		public double ThroughputPerMillisecond => ElapsedMilliseconds / (double)(Iterations <= 0 ? 1 : Iterations);

		protected BenchmarkWorker(IBenchmarkConfiguration benchmarkConfiguration, string resourceName, string pair)
		:	base(benchmarkConfiguration)
		{
			ResourceName = resourceName ?? string.Empty;
			Pair = pair ?? string.Empty;
		}

		protected void WastingTime()
		{
			// ReSharper disable once EmptyForStatement
			// ReSharper disable once SuggestVarOrType_BuiltInTypes
			for (long t = 0; t < Spins; ++t) { }
		}

		protected virtual async Task BenchmarkingTarget() { await Task.CompletedTask; throw new Exception($"virtual {nameof(BenchmarkingTarget)}() must be overridden!)"); }

		public Task RunProcessing() => Task.Run(Processing);

		protected async Task Processing()
		{
			StopWatchTimer.Start();
			TimeSpan? next = null;
			TimeSpan now;
			do
			{
				await BenchmarkingTarget();
				Iterations++;

				now = StopWatchTimer.Elapsed;
				next = now > next ? now + PerfCollector.TryRead(): next ?? (now + PerfCollector.TryRead());
			} while (now < TimeSpan);

			StopWatchTimer.Stop();
			ElapsedMilliseconds = StopWatchTimer.ElapsedMilliseconds;
			StopWatchTimer = null;
		}
	}
}