using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FractionFormatter.Test
{
    [TestClass]
    public class FractionalizeTests
    {
        [TestMethod]
        public void ExactMatch()
        {
            var testValue = Fractionalize.ToFraction(.5d);
            
            Assert.AreEqual("\u00bd", testValue);
        }

        [TestMethod]
        public void ExactMatchExtensionMethod()
        {
            var n = .5d;
            var testValue = n.ToFraction();

            Assert.AreEqual("\u00bd", testValue);
        }

        [TestMethod]
        public void ExactMatchFloat()
        {
            var testValue = Fractionalize.ToFraction(.5f);
            
            Assert.AreEqual("\u00bd", testValue);
        }

        [TestMethod]
        public void ExactMatchFloatExtensionMethod()
        {
            var n = .5f;
            var testValue = n.ToFraction();

            Assert.AreEqual("\u00bd", testValue);
        }

        [TestMethod]
        public void ExactMatchString()
        {
            var testValue = Fractionalize.ToFraction("0.5");
            
            Assert.AreEqual("\u00bd", testValue);
        }

        [TestMethod]
        public void ExactMatchStringExtensionMethod()
        {
            var n = "0.5";
            var testValue = n.ToFraction();

            Assert.AreEqual("\u00bd", testValue);
        }

        [TestMethod]
        public void ExactMatchDecimal()
        {
            var testValue = Fractionalize.ToFraction(.5m);

            Assert.AreEqual("\u00bd", testValue);
        }

        [TestMethod]
        public void ExactMatchDecimalExtensionMethod()
        {
            var n = .5m;
            var testValue = n.ToFraction();

            Assert.AreEqual("\u00bd", testValue);
        }

        [TestMethod]
        public void ExplicitNumber()
        {
            var testValue = Fractionalize.ToFraction(1, 1, 2);
            
            Assert.AreEqual("1 \u00bd", testValue);
        }

        [TestMethod]
        public void ExactMatchWithNegativeNumbers()
        {
            var testValue = Fractionalize.ToFraction(-.5d);
            
            Assert.AreEqual("-\u00bd", testValue);
        }

        [TestMethod]
        public void CloseEnoughWithApproximation()
        {
            var opts = new FractionalizeOptions {ShowApproximationSymbol = true};
            var testValue = Fractionalize.ToFraction(.5001d, opts);
            
            Assert.AreEqual("\u2248\u00bd", testValue);
        }

        [TestMethod]
        public void NoExactMatch()
        {
            var opts = new FractionalizeOptions {ExactMatch = true};
            var testValue = Fractionalize.ToFraction(.5001d, opts);

            Assert.AreEqual("0.5001", testValue);
        }

        [TestMethod]
        public void ThereIsNotAUnicodeEquivalent()
        {
            var testValue = Fractionalize.ToFraction(.03);

            Assert.AreEqual("1/26", testValue);
        }

        [TestMethod]
        public void ThereIsNotAUnicodeEquivalentWithApproximation()
        {
            var opts = new FractionalizeOptions {ShowApproximationSymbol = true};
            var testValue = Fractionalize.ToFraction(.03, opts);

            Assert.AreEqual("\u22481/26", testValue);
        }

        [TestMethod]
        public void ThereIsNotAUnicodeEquivalentWithNumbersGreaterThanOne()
        {
            var testValue = Fractionalize.ToFraction(1.1);

            Assert.AreEqual("1 1/10", testValue);
        }

        [TestMethod]
        public void WholeNumbersAreCool()
        {
            var testValue = Fractionalize.ToFraction(1d);

            Assert.AreEqual("1", testValue);
        }

        [TestMethod]
        public void NegativeWholeNumbersAreCool()
        {
            var testValue = Fractionalize.ToFraction(-1d);

            Assert.AreEqual("-1", testValue);
        }

        [TestMethod]
        public void WholeNumbersAreFractionalized()
        {
            var testValue = Fractionalize.ToFraction(1.25);

            Assert.AreEqual("1 \u00bc", testValue);
        }

        [TestMethod]
        public void NegativeWholeNumbersAreFractionalized()
        {
            var testValue = Fractionalize.ToFraction(-1.25);

            Assert.AreEqual("-1 \u00bc", testValue);
        }

        [TestMethod]
        public void NegativeRealNumbersAreFractionalized()
        {
            var testValue = Fractionalize.ToFraction(-1.06);

            Assert.AreEqual("-1 1/15", testValue);
        }

        [TestMethod]
        public void ZeroWorks()
        {
            var testValue = Fractionalize.ToFraction(0d);
            
            Assert.AreEqual("0", testValue);
        }

        [TestMethod]
        public void NegativeZeroWorks()
        {
            var testValue = Fractionalize.ToFraction(-0d);

            Assert.AreEqual("-0", testValue);
        }

        [TestMethod]
        public void InfinityWorksOk()
        {
            var testValue = Fractionalize.ToFraction(double.PositiveInfinity);

            Assert.AreEqual("Infinity", testValue);
        }

        [TestMethod]
        public void NegativeInfinityWorksOk()
        {
            var testValue = Fractionalize.ToFraction(double.NegativeInfinity);

            Assert.AreEqual("-Infinity", testValue);
        }

        [TestMethod]
        public void WholeNumbersAreFractionalizedNoSpace()
        {
            var opts = new FractionalizeOptions {SpaceBetweenIntegerAndFraction = false};
            var testValue = Fractionalize.ToFraction(1.25, opts);

            Assert.AreEqual("1\u00bc", testValue);
        }

        [TestMethod]
        public void NanDoesntBreakThings()
        {
            var testValue = Fractionalize.ToFraction(double.NaN);

            Assert.AreEqual("NaN", testValue);
        }

        [TestMethod]
        public void SetMaxDenominator()
        {
            var opts = new FractionalizeOptions {MaxDenominator = 4};
            var testValue = Fractionalize.ToFraction(.375, opts);

            Assert.AreEqual("0.375", testValue);
        }

        [TestMethod]
        public void SetTolerance()
        {
            var opts = new FractionalizeOptions {MaxDenominator = 4, Tolerance = 0.125};
            var testValue = Fractionalize.ToFraction(.37, opts);

            Assert.AreEqual("\u2153", testValue);
        }

        [TestMethod]
        public void CultureIsUsed()
        {
            var opts = new FractionalizeOptions()
            {
                Culture = new CultureInfo("es")
            };
            var testValue = Fractionalize.ToFraction("1.000,25", opts);

            Assert.AreEqual("1000 \u00bc", testValue);
        }
    }
}
