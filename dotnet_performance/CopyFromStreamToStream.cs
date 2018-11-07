using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using Microsoft.IO;

namespace dotnet_performance
{
    [SimpleJob(invocationCount: 500)]
    public class CopyFromStreamToStream
    {
        private RecyclableMemoryStreamManager streamManager;

        [Params(100, 512, 2048, 4096)]
        public int Capacity { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            this.streamManager = new RecyclableMemoryStreamManager();
        }

        [Benchmark]
        [MemoryDiagnoser]
        public void ArraySegment()
        {
            var ms = new MemoryStream(this.Capacity);

            for (int i = 0; i < this.Capacity; i++)
            {
                ms.WriteByte(100);
            }

            var asm = new ArraySegment<byte>(ms.GetBuffer(), 0, this.Capacity);

            var ms2 = new MemoryStream(asm.Array, 0, asm.Array.Length);
        }  

        [Benchmark]
        [MemoryDiagnoser]
        public void CopyToStream()
        {
            var ms = new MemoryStream(this.Capacity);

            for (int i = 0; i < this.Capacity; i++)
            {
                ms.WriteByte(100);
            }

            var ms2 = new MemoryStream(this.Capacity);

            ms.CopyTo(ms2);
        }

        [Benchmark]
        [MemoryDiagnoser]
        public void CopyToStreamFromManager()
        {
            var ms = this.streamManager.GetStream(null, this.Capacity);

            for (int i = 0; i < this.Capacity; i++)
            {
                ms.WriteByte(100);
            }

            var ms2 = this.streamManager.GetStream(null, this.Capacity);

            ms.CopyTo(ms2);
        }

        [Benchmark]
        [MemoryDiagnoser]
        public void CopyToStreamFromManagerInsideUsing()
        {
            using(var ms = this.streamManager.GetStream(null, this.Capacity))
            {
                for (int i = 0; i < this.Capacity; i++)
                {
                    ms.WriteByte(100);
                }

                using(var ms2 = this.streamManager.GetStream(null, this.Capacity))
                {
                    ms.CopyTo(ms2);
                }
            }
        }
    }
}