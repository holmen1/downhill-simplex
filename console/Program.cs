﻿using System;
using DownhillSimplex.FSharp;

namespace console;

internal class Program
{
    private static void Main(string[] args)
    {
/*             // Fi par 2019-12-31
            int[] swapdur = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 15, 20, 30 };
            double[] par_debug = new double[] { 0.1, 0.188, 0.215, 0.255, 0.318, 0.375, 0.445, 0.51, 0.57, 0.625, 0.677, 0.773, 0.873, 0.958, 1.0 }; // Fi December

            OptimalDiscountFactors ODF = new OptimalDiscountFactors(0.188, 0.215, 0.255, 0.318, 0.375, 0.445, 0.51, 0.57, 0.625, 0.677, 0.773, 0.873, 0.958);

            // Fi rate 2019-12-31
            double [] FiFFFS = new double[] { 0, 0, 0, 0, 0, 0.000250125, 0.000952065, 0.001606129, 0.002212058, 0.002769566, 0.003298784, 0.004104149, 0.005022016,
                0.006039549, 0.007121224, 0.008254043, 0.009411152, 0.010607462, 0.011836308, 0.013092432, 0.014371629, 0.015670499, 0.016852732, 0.017933365,
                0.018924953, 0.019838068, 0.020681669, 0.021463404, 0.022189836, 0.022866634, 0.023498716, 0.024090373, 0.024645361, 0.025166988, 0.025658174,
                0.026121507, 0.026559292, 0.026973585, 0.027366227, 0.027738872, 0.02809301, 0.028429987, 0.028751019, 0.029057213, 0.029349575, 0.02962902,
                0.029896386, 0.03015244, 0.030397885, 0.030633367, 0.03085948, 0.031076772, 0.03128575, 0.031486883, 0.031680603, 0.031867313, 0.032047387,
                0.032221172, 0.032388993, 0.03255115, 0.032707927, 0.032859586, 0.033006374, 0.033148522, 0.033286246, 0.03341975, 0.033549226, 0.033674851,
                0.033796797, 0.033915221, 0.034030275, 0.034142101, 0.034250831, 0.034356594, 0.034459509, 0.034559689, 0.034657242, 0.03475227, 0.03484487,
                0.034935133, 0.035023148, 0.035108996, 0.035192758, 0.035274508, 0.035354317, 0.035432255, 0.035508385, 0.035582771, 0.035655472, 0.035726544,
                0.035796041, 0.035864015, 0.035930516, 0.035995591, 0.036059285, 0.036121642, 0.036182704, 0.03624251, 0.036301099, 0.036358508, 0.036414771 };


            Console.WriteLine("dur\tDF\tfi");
            for (int d = 1; d < 21; d++)
                Console.WriteLine("{0}\t{1}\t{2}", d, ODF.discountFactor(d), ODF.fiRate(d));

            Console.WriteLine("dur\tfi");
            for (int d = 1; d < 101; d++)
                Console.WriteLine("{0}\t{1}", d, ODF.fiRate(d)); */


        // test DF
        double p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p12, p15, p20;
        var DF = new double[]
        {
            1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
            0.998499434385086,
            0.997001130859396,
            0.991867414193933,
            0.986760118251288,
            0.981679134127974,
            0.97761011690404,
            0.973557963582377,
            0.96952259438059,
            0.965503955160068,
            0.961501988309603
        };
        DF[11] = Math.Exp((Math.Log(DF[10]) + Math.Log(DF[12])) / 2);
        DF[13] = Math.Exp(Math.Log(DF[12]) * 2 / 3 + Math.Log(DF[15]) / 3);
        DF[13] = Math.Exp(Math.Log(DF[12]) / 3 + Math.Log(DF[15]) * 2 / 3);
        DF[16] = Math.Exp(Math.Log(DF[15]) * 4 / 5 + Math.Log(DF[20]) / 5);
        DF[17] = Math.Exp(Math.Log(DF[15]) * 3 / 5 + Math.Log(DF[20]) * 2 / 5);
        DF[18] = Math.Exp(Math.Log(DF[15]) * 2 / 5 + Math.Log(DF[20]) * 3 / 5);
        DF[19] = Math.Exp(Math.Log(DF[15]) / 5 + Math.Log(DF[20]) * 4 / 5);

        p1 = (1 - DF[1]) / DF[1];
        p2 = (1 - DF[2]) / (DF[1] + DF[2]);
        p3 = (1 - DF[3]) / (DF[1] + DF[2] + DF[3]);
        p4 = (1 - DF[4]) / (DF[1] + DF[2] + DF[3] + DF[4]);
        p5 = (1 - DF[5]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5]);
        p6 = (1 - DF[6]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6]);
        p7 = (1 - DF[7]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7]);
        p8 = (1 - DF[8]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7] + DF[8]);
        p9 = (1 - DF[9]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7] + DF[8] + DF[9]);
        p10 = (1 - DF[10]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7] + DF[8] + DF[9] + DF[10]);
        p12 = (1 - DF[12]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7] + DF[8] + DF[9] + DF[10] + DF[11] +
                              DF[12]);
        p15 = (1 - DF[15]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7] + DF[8] + DF[9] + DF[10] + DF[11] +
                              DF[12] + DF[13] + DF[14] + DF[15]);
        p20 = (1 - DF[20]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7] + DF[8] + DF[9] + DF[10] + DF[11] +
                              DF[12] + DF[13] + DF[14] + DF[15] + DF[16] + DF[17] + DF[18] + DF[19] + DF[20]);

        var creditrisk = 0.0035;
        var ODF = new OptimalDiscountFactors(100 * (p1 + creditrisk), 100 * (p2 + creditrisk), 100 * (p3 + creditrisk),
            100 * (p4 + creditrisk), 100 * (p5 + creditrisk), 100 * (p6 + creditrisk), 100 * (p7 + creditrisk),
            100 * (p8 + creditrisk), 100 * (p9 + creditrisk), 100 * (p10 + creditrisk), 100 * (p12 + creditrisk),
            100 * (p15 + creditrisk), 100 * (p20 + creditrisk));
        var dfs = ODF.fit(ODF.initGuess);

        Console.WriteLine("dur\tDF\tDF'");
        for (var d = 1; d < 20; d++)
            Console.WriteLine("{0}\t{1}\t{2}", d, DF[d + 1], dfs[d]);
    }
}