using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using Microsoft.IO;

namespace dotnet_performance
{
    [SimpleJob(invocationCount: 500)]
    public class ForgetToCloseStream
    {
        private RecyclableMemoryStreamManager streamManager;

        [Params(100, 4096)]
        public int Capacity { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            this.streamManager = new RecyclableMemoryStreamManager();
        }

        [Benchmark]
        [MemoryDiagnoser]
        public void Close()
        {
            for (int i = 0; i < 200; i++)
            {
                using(var str = this.streamManager.GetStream(null, this.Capacity))
                {

                }                
            }
        }  

        [Benchmark]
        [MemoryDiagnoser]
        public void Forget()
        {
            for (int i = 0; i < 200; i++)
            {
                var str = this.streamManager.GetStream(null, this.Capacity);
            }
        }
    }        
}