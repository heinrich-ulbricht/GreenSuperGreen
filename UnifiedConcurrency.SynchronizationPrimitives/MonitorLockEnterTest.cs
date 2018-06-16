﻿using System;
using System.Threading.Tasks;
using GreenSuperGreen.UnifiedConcurrency;
using NUnit.Framework;

// ReSharper disable RedundantExtendsListEntry

namespace UnifiedConcurrency.SynchronizationPrimitives
{
	public sealed class MonitorLockEnter : ATestingJob, ITestingJob
	{
		public MonitorLockEnter(int count) : base(count) { }

		#pragma warning disable 618
			private ILockUC Lock { get; } = new MonitorLockUC();
		#pragma warning restore 618

		protected override bool ExclusiveAccess()
		{
			using (EntryBlockUC entry = Lock.Enter())
			{
				if (!entry.HasEntry) throw new Exception("hmmmmmmmmm");
				return ProcessExclusively();
			}
		}
	}

	[TestFixture]
	public partial class UnifiedConcurrency
	{
		/// <summary> About 30 seconds </summary>
		[Test]
		public async Task MonitorLockEnterTest()
		{
			using (ITestingJob job = new LockEnter(1000000))
			{
				await job.Execute(Environment.ProcessorCount);
			}
		}
	}
}
