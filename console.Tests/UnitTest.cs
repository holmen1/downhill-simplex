using NUnit.Framework;
using DownhillSimplex.FSharp;

namespace DownhillSimplex.Tests
{
    [TestFixture]
    public class DownhillSimplex_IsConverging
    {
        private MinimizeBanana _MB;
        private OptimalDiscountFactors _ODF;

        [SetUp]
        public void SetUp()
        {
            _ODF = new OptimalDiscountFactors(0.188, 0.215, 0.255, 0.318, 0.375, 0.445, 0.51, 0.57, 0.625, 0.677, 0.773, 0.873, 0.958);
            _MB = new MinimizeBanana(2.0, 3.0);
        }


        [Test]
        public void IsWithinTol()
        {
            var dfs = _ODF.fit(_ODF.initGuess);
            double tol = 1.0E-6;
            double objValue = _ODF.obj(dfs);
            Assert.LessOrEqual(objValue, tol);
        }

        [Test]
        public void IsMinimized_ReturnTrue()
        {
            var banana = _MB.fit(_MB.initGuess);
            double tol = 1.0E-6;
            double objValue = _MB.obj(banana);
            Assert.LessOrEqual(objValue, tol);
        }
    }
}