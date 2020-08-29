using System;
using DownhillSimplex;

namespace console2
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = NM.fit(3.0, 5.3);
            Console.WriteLine(res);
            // ((1.0000000000061395, 1.0000000000172617), 2.52040479898386E-21, 130, True)
        }
    }
}
