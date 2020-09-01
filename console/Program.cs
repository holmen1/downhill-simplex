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
            Console.WriteLine("MinimizeBanana");
            Console.WriteLine(banana);
            // (Vertex [1.000014292; 1.000027606], 2.9984686488385386E-10, 65, True)
            MinimizeHimmelblau MH1 = new MinimizeHimmelblau(2.0, 3.0);
            MinimizeHimmelblau MH2 = new MinimizeHimmelblau(-3.0, 3.0);
            MinimizeHimmelblau MH3 = new MinimizeHimmelblau(-4.0, -3.0);
            MinimizeHimmelblau MH4 = new MinimizeHimmelblau(4.0, -1.0);
            var himmel1 = MH1.fit;
            var himmel2 = MH2.fit;
            var himmel3 = MH3.fit;
            var himmel4 = MH4.fit;
            Console.WriteLine("MinimizeHimmelblau");
            Console.WriteLine(himmel1);
            Console.WriteLine(himmel2);
            Console.WriteLine(himmel3);
            Console.WriteLine(himmel4);
            // (Vertex [3.000001421; 2.0000037], 4.126256827866179E-10, 46, True)
            // (Vertex [-2.805121661; 3.131318866], 2.005864380382668E-09, 40, True)
            // (Vertex [-3.77931128; -3.283189075], 3.913165166932208E-10, 46, True)
            // (Vertex [3.584429363; -1.848122617], 3.067787952574689E-10, 48, True)
        }
    }
}
