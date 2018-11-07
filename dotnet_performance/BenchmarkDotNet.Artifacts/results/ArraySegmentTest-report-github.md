``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i5-8350U CPU 1.70GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.1.403
  [Host]     : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT


```
|       Method | Capacity |        Mean |      Error |     StdDev |
|------------- |--------- |------------:|-----------:|-----------:|
| **ArraySegment** |      **100** |    **423.3 ns** |   **1.750 ns** |   **1.551 ns** |
| **ArraySegment** |      **512** |  **2,004.9 ns** |  **26.887 ns** |  **20.992 ns** |
| **ArraySegment** |     **2048** |  **7,865.7 ns** |  **79.855 ns** |  **74.696 ns** |
| **ArraySegment** |     **4096** | **15,753.0 ns** | **309.993 ns** | **274.800 ns** |
