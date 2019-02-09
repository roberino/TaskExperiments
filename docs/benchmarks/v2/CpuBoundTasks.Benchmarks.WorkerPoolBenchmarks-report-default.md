
BenchmarkDotNet=v0.11.3, OS=Windows 10.0.16299.726 (1709/FallCreatorsUpdate/Redstone3)
Intel Core i7-8550U CPU 1.80GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
Frequency=1945313 Hz, Resolution=514.0561 ns, Timer=TSC
.NET Core SDK=2.2.103
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

           Method | Iterations | ParallelOperations | ExecutionStrategy |      Mean |     Error |    StdDev | Rank |
----------------- |----------- |------------------- |------------------ |----------:|----------:|----------:|-----:|
 **EnqueueWorkAsync** |         **50** |                  **4** |         **Dedicated** |  **5.097 us** | **0.1011 us** | **0.2218 us** |    **9** |
 **EnqueueWorkAsync** |         **50** |                  **4** |  **GlobalThreadPool** |  **3.736 us** | **0.0724 us** | **0.0775 us** |    **6** |
 **EnqueueWorkAsync** |         **50** |                  **4** |            **Inline** |  **1.103 us** | **0.0209 us** | **0.0215 us** |    **1** |
 **EnqueueWorkAsync** |         **50** |                  **8** |         **Dedicated** | **10.070 us** | **0.1944 us** | **0.2080 us** |   **14** |
 **EnqueueWorkAsync** |         **50** |                  **8** |  **GlobalThreadPool** |  **6.133 us** | **0.1194 us** | **0.1278 us** |   **11** |
 **EnqueueWorkAsync** |         **50** |                  **8** |            **Inline** |  **1.900 us** | **0.0371 us** | **0.0507 us** |    **3** |
 **EnqueueWorkAsync** |        **100** |                  **4** |         **Dedicated** |  **5.154 us** | **0.0989 us** | **0.1286 us** |    **9** |
 **EnqueueWorkAsync** |        **100** |                  **4** |  **GlobalThreadPool** |  **4.127 us** | **0.0822 us** | **0.1231 us** |    **7** |
 **EnqueueWorkAsync** |        **100** |                  **4** |            **Inline** |  **1.407 us** | **0.0271 us** | **0.0266 us** |    **2** |
 **EnqueueWorkAsync** |        **100** |                  **8** |         **Dedicated** |  **8.926 us** | **0.1776 us** | **0.1661 us** |   **13** |
 **EnqueueWorkAsync** |        **100** |                  **8** |  **GlobalThreadPool** |  **6.083 us** | **0.1157 us** | **0.1286 us** |   **11** |
 **EnqueueWorkAsync** |        **100** |                  **8** |            **Inline** |  **2.291 us** | **0.0452 us** | **0.0502 us** |    **4** |
 **EnqueueWorkAsync** |        **500** |                  **4** |         **Dedicated** |  **5.209 us** | **0.0958 us** | **0.0941 us** |    **9** |
 **EnqueueWorkAsync** |        **500** |                  **4** |  **GlobalThreadPool** |  **4.523 us** | **0.0347 us** | **0.0308 us** |    **8** |
 **EnqueueWorkAsync** |        **500** |                  **4** |            **Inline** |  **2.966 us** | **0.0239 us** | **0.0200 us** |    **5** |
 **EnqueueWorkAsync** |        **500** |                  **8** |         **Dedicated** |  **8.954 us** | **0.1725 us** | **0.2474 us** |   **13** |
 **EnqueueWorkAsync** |        **500** |                  **8** |  **GlobalThreadPool** |  **6.610 us** | **0.1294 us** | **0.2267 us** |   **12** |
 **EnqueueWorkAsync** |        **500** |                  **8** |            **Inline** |  **5.438 us** | **0.0742 us** | **0.0658 us** |   **10** |
