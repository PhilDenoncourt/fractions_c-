using System.Globalization;

namespace FractionFormatter
{
    public class FractionalizeOptions
    {
        public int MaxDenominator { get; set; } = 64;
        public bool SpaceBetweenIntegerAndFraction {get;set;} = true;
        public double Tolerance { get; set; } = 0.01f;
        public bool ExactMatch { get; set; } = false;
        public bool ShowApproximationSymbol { get; set; } = false;

        public CultureInfo Culture = CultureInfo.InvariantCulture;
    }
}
