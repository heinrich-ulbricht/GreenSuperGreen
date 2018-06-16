﻿using GreenSuperGreen.Timing;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

namespace GreenSuperGreen.UnifiedConcurrency
{
	internal static class TimerProcessorUC
	{
		public static int Period { get; } = 10;
		public static IRealTimeSource RealTimeSource { get; } = new RealTimeSource();
		public static ITimerProcessor TimerProcessor { get; } = new TimerProcessor(Period, RealTimeSource);
	}
}
