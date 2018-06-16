1) Unified Concurrency
The main mission is to unify usage of heterogeneous synchronization primitives with interface/pattern based Object Oriented approach. Switching between multiple synchronization primitives with one code line change! Agile Development ready solution to synchronization. Easy upgrade from general threading to async/await thanks to the pattern based design! Reduced code complexity. Easy correctness testing. Simplified performance optimization. Currently implemented synchronization primitives cover:
  
  - wrapper for the .Net SpinLock,
  - TicketSpinLock,
  - Lock replacement of C# lock (Monitor),
  - wrapper around C# lock (monitor) benchmarking only,
  - async/await AsyncLock,
  - async/await AsyncSpinLock,
  - async/await AsyncTicketSpinLock,
  
  
  Articles:
  
  https://www.codeproject.com/Articles/1236238/Unified-Concurrency-I-Introduction
  https://www.codeproject.com/Articles/1237518/Unified-Concurrency-II-benchmarking-methodologies
  https://www.codeproject.com/Articles/1242156/Unified-Concurrency-III-cross-benchmarking
  
  Nuget:
  
  https://www.nuget.org/packages/GreenSuperGreen/


2) AsyncTesting: SequencerUC
A strongly typed Sequencer is a powerful threading or async/await based unit testing tool to simplify and test correctness in asynchronous and/or parallel code. The Sequencer allows to simplify control/detection of one or many testing thread flows and allows to deterministically setup testing scenario from unit test method and optionally inject data into the tested code to ensure required state.


3) Awaitable Concurrent Priority Queue
  
  Articles:
  
  https://www.codeproject.com/Articles/1222893/Optionally-Awaitable-Concurrent-Priority-Queue
