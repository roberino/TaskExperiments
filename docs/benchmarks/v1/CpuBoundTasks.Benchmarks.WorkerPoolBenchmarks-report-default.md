
BenchmarkDotNet=v0.11.3, OS=Windows 10.0.16299.726 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
Frequency=1945313 Hz, Resolution=514.0561 ns, Timer=TSC
.NET Core SDK=2.2.103
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

           Method | Iterations | ParallelOperations | ExecutionStrategy |      Mean |     Error |    StdDev |    Median | Rank |
----------------- |----------- |------------------- |------------------ |----------:|----------:|----------:|----------:|-----:|
 **EnqueueWorkAsync** |         **50** |                  **4** |         **Dedicated** | **19.007 us** | **0.3695 us** | **0.5058 us** | **18.995 us** |   **12** |
 **EnqueueWorkAsync** |         **50** |                  **4** |  **GlobalThreadPool** |  **6.812 us** | **0.1318 us** | **0.1619 us** |  **6.861 us** |    **7** |
 **EnqueueWorkAsync** |         **50** |                  **4** |            **Inline** |  **1.336 us** | **0.0291 us** | **0.0853 us** |  **1.343 us** |    **2** |
 **EnqueueWorkAsync** |         **50** |                  **8** |         **Dedicated** | **35.483 us** | **0.7386 us** | **1.6367 us** | **35.461 us** |   **14** |
 **EnqueueWorkAsync** |         **50** |                  **8** |  **GlobalThreadPool** | **12.131 us** | **0.2398 us** | **0.3515 us** | **12.178 us** |   **10** |
 **EnqueueWorkAsync** |         **50** |                  **8** |            **Inline** |  **2.134 us** | **0.0538 us** | **0.1587 us** |  **2.163 us** |    **4** |
 **EnqueueWorkAsync** |        **100** |                  **4** |         **Dedicated** | **18.819 us** | **0.3710 us** | **0.6095 us** | **18.984 us** |   **12** |
 **EnqueueWorkAsync** |        **100** |                  **4** |  **GlobalThreadPool** |  **6.651 us** | **0.1299 us** | **0.1904 us** |  **6.588 us** |    **7** |
 **EnqueueWorkAsync** |        **100** |                  **4** |            **Inline** |  **1.128 us** | **0.0639 us** | **0.1885 us** |  **1.133 us** |    **1** |
 **EnqueueWorkAsync** |        **100** |                  **8** |         **Dedicated** | **32.605 us** | **0.6489 us** | **1.4646 us** | **32.624 us** |   **13** |
 **EnqueueWorkAsync** |        **100** |                  **8** |  **GlobalThreadPool** | **11.427 us** | **0.2259 us** | **0.4074 us** | **11.400 us** |    **9** |
 **EnqueueWorkAsync** |        **100** |                  **8** |            **Inline** |  **1.888 us** | **0.0918 us** | **0.2707 us** |  **1.792 us** |    **3** |
 **EnqueueWorkAsync** |        **500** |                  **4** |         **Dedicated** | **18.280 us** | **0.3640 us** | **0.6470 us** | **18.325 us** |   **11** |
 **EnqueueWorkAsync** |        **500** |                  **4** |  **GlobalThreadPool** |  **7.006 us** | **0.1396 us** | **0.2851 us** |  **7.046 us** |    **8** |
 **EnqueueWorkAsync** |        **500** |                  **4** |            **Inline** |  **2.217 us** | **0.0599 us** | **0.1727 us** |  **2.266 us** |    **5** |
 **EnqueueWorkAsync** |        **500** |                  **8** |         **Dedicated** | **32.166 us** | **0.6379 us** | **1.2441 us** | **32.120 us** |   **13** |
 **EnqueueWorkAsync** |        **500** |                  **8** |  **GlobalThreadPool** | **11.520 us** | **0.2264 us** | **0.3720 us** | **11.489 us** |    **9** |
 **EnqueueWorkAsync** |        **500** |                  **8** |            **Inline** |  **4.293 us** | **0.1077 us** | **0.3001 us** |  **4.318 us** |    **6** |
