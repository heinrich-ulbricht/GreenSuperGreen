﻿using System.Threading;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace GreenSuperGreen.UnifiedConcurrency
{
	/// <summary>
	/// <para/> <see cref="SemaphoreLockUC"/> is based on .Net <see cref="System.Threading.Semaphore"/>.
	/// <para/> Does not support recursive call and does not protect against recursive call!
	/// <para/> Enter and Exit can be done on different threads, but same thread should be preffered...
	/// </summary>
	internal class SemaphoreLockUC : ILockUC
	{
		private Semaphore Semaphore { get; } = new Semaphore(0, 1);

		private EntryCompletionUC EntryCompletion { get; }

		public SyncPrimitiveCapabilityUC Capability { get; } = 0
		| SyncPrimitiveCapabilityUC.Enter
		| SyncPrimitiveCapabilityUC.TryEnter
		| SyncPrimitiveCapabilityUC.TryEnterWithTimeout
		| SyncPrimitiveCapabilityUC.NonCancellable
		| SyncPrimitiveCapabilityUC.NonRecursive
		| SyncPrimitiveCapabilityUC.NonThreadAffine
		;

		public SemaphoreLockUC()
		{
			EntryCompletion = new EntryCompletionUC(Exit);
		}

		private void Exit() => Semaphore.Release();

		public EntryBlockUC Enter()
		{
			Semaphore.WaitOne();
			return new EntryBlockUC(EntryTypeUC.Exclusive, EntryCompletion);
		}

		public EntryBlockUC TryEnter()
		{
			return Semaphore.WaitOne(0)
			? new EntryBlockUC(EntryTypeUC.Exclusive, EntryCompletion)
			: EntryBlockUC.RefusedEntry
			;
		}

		public EntryBlockUC TryEnter(int milliseconds)
		{
			return Semaphore.WaitOne(milliseconds)
			? new EntryBlockUC(EntryTypeUC.Exclusive, EntryCompletion)
			: EntryBlockUC.RefusedEntry
			;
		}
	}
}