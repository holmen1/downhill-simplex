using NUnit.Framework;
using DownhillSimplex.FSharp;

namespace DownhillSimplex.Tests
{
    [TestFixture]
    public class DownhillSimplex_IsConverging
    {
        private MinimizeBanana _MB;

        [SetUp]
        public void SetUp()
        {
            _MB = new MinimizeBanana(2.0, 3.0);
        }

        [Test]
        public void IsMinimized_ReturnTrue()
        {
            var banana = _MB.fit;
            bool converged = banana.Item4;
            Assert.IsTrue(converged, "Yess!");
        }
    }
}