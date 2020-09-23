using NUnit.Framework;
using DownhillSimplex.FSharp;
using System;

namespace DownhillSimplex.Tests
{
    [TestFixture]
    public class DownhillSimplex_IsConverging
    {
        private MinimizeBanana MB;
        private OptimalDiscountFactors ODF;
        private double[] DF;
        double p1,p2,p3,p4,p5,p6,p7,p8,p9,p10,p12,p15,p20;

        [SetUp]
        public void SetUp()
        {
            DF = new double[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
                                0.998499434385086,
                                0.997001130859396,
                                0.991867414193933,
                                0.986760118251288,
                                0.981679134127974,
                                0.97761011690404,
                                0.973557963582377,
                                0.96952259438059,
                                0.965503955160068,
                                0.961501988309603 };
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
            p10= (1 - DF[10]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7] + DF[8] + DF[9] + DF[10]);
            p12= (1 - DF[12]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7] + DF[8] + DF[9] + DF[10] + DF[11] + DF[12]);
            p15= (1 - DF[15]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7] + DF[8] + DF[9] + DF[10] + DF[11] + DF[12] + DF[13] + DF[14] + DF[15]);
            p20= (1 - DF[20]) / (DF[1] + DF[2] + DF[3] + DF[4] + DF[5] + DF[6] + DF[7] + DF[8] + DF[9] + DF[10] + DF[11] + DF[12] + DF[13] + DF[14] + DF[15] + DF[16] + DF[17] + DF[18] + DF[19] + DF[20]);


            //ODF = new OptimalDiscountFactors(0.188, 0.215, 0.255, 0.318, 0.375, 0.445, 0.51, 0.57, 0.625, 0.677, 0.773, 0.873, 0.958);
            double creditrisk = 0.0035;
            ODF = new OptimalDiscountFactors(100*(p1 + creditrisk),100*(p2 + creditrisk),100*(p3 + creditrisk),100*(p4 + creditrisk),100*(p5 + creditrisk),100*(p6 + creditrisk),100*(p7 + creditrisk),100*(p8 + creditrisk),100*(p9 + creditrisk),100*(p10 + creditrisk),100*(p12 + creditrisk),100*(p15 + creditrisk),100*(p20 + creditrisk));
            MB = new MinimizeBanana(2.0, 3.0);
        }

        [Test]
        public void DiscountFactorsIsWithinTol()
        {
            bool within = true;
            var dfs = ODF.fit(ODF.initGuess);
            double tol = 1E-3;
            for (int d = 1; d < 20; d++)
            {
                 if (Math.Abs(DF[d +1] - dfs[d]) > tol)
                    within = false;
            }
            Assert.IsTrue(within);
        }

        [Test]
        public void IsWithinTol()
        {
            var dfs = ODF.fit(ODF.initGuess);
            double tol = 1.0E-6;
            double objValue = ODF.obj(dfs);
            Assert.LessOrEqual(objValue, tol);
        }

        [Test]
        public void IsMinimized_ReturnTrue()
        {
            var banana = MB.fit(MB.initGuess);
            double tol = 1.0E-6;
            double objValue = MB.obj(banana);
            Assert.LessOrEqual(objValue, tol);
        }
    }
}