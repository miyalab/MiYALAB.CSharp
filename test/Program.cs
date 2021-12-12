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
            Matrix mat1, mat2;

            mat1 = Matrix.Double2DToMatrix(new double[,]{
                { 1,2,3},
                { 4,5,6},
                { 7,8,9}
            });

            mat2 = Matrix.Double2DToMatrix(new double[,]{
                { 1,2,3},
                { 4,5,6},
                { 7,8,9}
            });
            Console.WriteLine(Matrix.Equals(mat1, mat2));
        }
        private static void Method(int n1, int n2)
        {
            double d;

            d = Math.Sqrt(n1 * n2);
        }
    }
}