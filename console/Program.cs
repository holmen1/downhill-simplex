using System;
using DownhillSimplex.FSharp;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            MinimizeBanana MB = new MinimizeBanana(2.0, 3.0);
            var banana = MB.fit;
            Console.WriteLine(banana);
            // (Vertex [1.000014292; 1.000027606], 2.9984686488385386E-10, 65, True)
            MinimizeHimmelblau MH = new MinimizeHimmelblau(2.0, 3.0);
            var himmel = MH.fit;
            Console.WriteLine(himmel);
            // (Vertex [3.000001421; 2.0000037], 4.126256827866179E-10, 46, True)
        }
    }
}
