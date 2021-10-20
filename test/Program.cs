using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

using MiYALAB.CSharp.Mathematics;

namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var stopWatch = Stopwatch.StartNew();

            Parallel.For(0, 10000, i =>
            {
                Parallel.For(0, 5000, j => Method(i, j));
            });

            stopWatch.Stop();
            Console.WriteLine("{0}", stopWatch.ElapsedMilliseconds);

            stopWatch.Restart();

            Parallel.For(0, 10000, i =>
            {
                for (int j = 0; j < 5000; ++j)
                {
                    Method(i, j);
                }
            });

            stopWatch.Stop();
            Console.WriteLine("{0}", stopWatch.ElapsedMilliseconds);

            stopWatch.Restart();

            for(int i=0; i<10000; i++)
            {
                for (int j = 0; j < 5000; ++j)
                {
                    Method(i, j);
                }
            }

            stopWatch.Stop();
            Console.WriteLine("{0}", stopWatch.ElapsedMilliseconds);

        }
        private static void Method(int n1, int n2)
        {
            double d;

            d = Math.Sqrt(n1 * n2);
        }
    }
}