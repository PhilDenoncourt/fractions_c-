using System;

namespace FractionFormatter
{
    public static class ExtensionMethods
    {
        public static string ToFraction(this double number, FractionalizeOptions options = null)
        {
            return Fractionalize.ToFraction(number, options);
        }
        public static string ToFraction(this string number, FractionalizeOptions options = null)
        {
            return Fractionalize.ToFraction(Convert.ToDouble(number), options);
        }
        public static string ToFraction(this float number, FractionalizeOptions options = null)
        {
            return Fractionalize.ToFraction(Convert.ToDouble(number), options);
        }
        public static string ToFraction(this decimal number, FractionalizeOptions options = null)
        {
            return Fractionalize.ToFraction(Convert.ToDouble(number), options);
        }
    }
}
