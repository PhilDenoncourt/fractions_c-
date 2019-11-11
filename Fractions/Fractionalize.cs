using System;
using System.Collections.Generic;
using System.Globalization;

namespace FractionFormatter
{
    public class Fractionalize
    {
        private static readonly Dictionary<long, Dictionary<long, char>> UnicodeMap = new Dictionary<long, Dictionary<long, char>>
        {
            {0, new Dictionary<long, char>() },
            {1, new Dictionary<long, char>() },
            {2, new Dictionary<long, char>
            {
                    {1, '\u00bd' }
                } },
            {3, new Dictionary<long, char>
            {
                    { 1, '\u2153'},
                    { 2, '\u2154'}
                } },
            {4, new Dictionary<long, char>
            {
                    {1,'\u00bc' },
                    {3,'\u00be' }
                } },
            {5, new Dictionary<long, char>
            {
                    {1,'\u2155' },
                    {2,'\u2156' },
                    {3,'\u2157' },
                    {4,'\u2158' }
                } },
            {6, new Dictionary<long, char>
            {
                    {1,'\u2159' },
                    {5,'\u215a' }
                } },
            {7, new Dictionary<long, char>
            {
                    {1, '\u2150' }
                } },
            {8, new Dictionary<long, char>
            {
                    {1,'\u215b' },
                    {3,'\u215c' },
                    {5,'\u215d' },
                    {7,'\u215e' }
                } },
            {9, new Dictionary<long, char>
            {
                {1, '\u2151' }
            } }
        };

        private static readonly List<List<double>> DenominatorMap = new List<List<double>>();

        private static void CreateDenominatorMap(int maxDenominator)
        {
            lock (DenominatorMap)
            {
                while (DenominatorMap.Count < maxDenominator)
                {
                    var currentDenominator = DenominatorMap.Count + 1;
                    var lst = new List<double>();
                    for (var i = 1; i <= currentDenominator; ++i)
                    {
                        lst.Add(i / Convert.ToDouble(currentDenominator));
                    }
                    DenominatorMap.Add(lst);
                }
            }
        }
        private static string FormatResult(long integerPortion, long numerator, long denominator, FractionalizeOptions options, bool isNegative, bool isApproximate=false)
        {
            if (UnicodeMap.ContainsKey(denominator) && UnicodeMap[denominator].ContainsKey(numerator))
            {
                return String.Join("", isApproximate && options.ShowApproximationSymbol ? "\u2248" : "", isNegative ? "-":"", integerPortion != 0 ? integerPortion.ToString() : "", integerPortion != 0 && options.SpaceBetweenIntegerAndFraction ? " " : "", UnicodeMap[denominator][numerator].ToString());
            }

            return String.Join("", isApproximate && options.ShowApproximationSymbol ? "\u2248" : "", isNegative ? "-":"", integerPortion != 0 ? integerPortion.ToString() : "", integerPortion != 0 && options.SpaceBetweenIntegerAndFraction ? " " : "", numerator.ToString(), "/", denominator.ToString());
        }

        public static string ToFraction(long integer, long numerator, long denominator, FractionalizeOptions options = null)
        {
            if (options == null)
            {
                options = new FractionalizeOptions();
            }

            return FormatResult(Math.Abs(integer), numerator, denominator, options, integer < 0);
        }
        public static string ToFraction(string number, FractionalizeOptions options = null)
        {
            return ToFraction(Convert.ToDouble(number, options?.Culture), options);
        }
        public static string ToFraction(float number, FractionalizeOptions options = null)
        {
            return ToFraction(Convert.ToDouble(number), options);
        }

        public static string ToFraction(decimal number, FractionalizeOptions options = null)
        {
            return ToFraction(Convert.ToDouble(number), options);
        }

        public static string ToFraction(double number, FractionalizeOptions options = null)
        {
            if (options == null)
            {
                options = new FractionalizeOptions();
            }

            lock (DenominatorMap)
            {
                if (DenominatorMap.Count < options.MaxDenominator)
                {
                    CreateDenominatorMap(options.MaxDenominator);
                }
            }

            if (number > long.MinValue && number < long.MaxValue)
            {
                var isNegative = number < 0;
                var decimalPortion = Math.Abs(number) - Math.Truncate(Math.Abs(number));
                var integerPortion = Convert.ToInt64(Math.Floor(Math.Abs(number)));
                for (var i = 0; i < options.MaxDenominator; ++i)
                {
                    lock (DenominatorMap)
                    {
                        for (var j = 0; j < DenominatorMap[i].Count; ++j)
                        {
                            // ReSharper disable once CompareOfFloatsByEqualityOperator
                            if (decimalPortion == DenominatorMap[i][j])
                            {
                                return FormatResult(integerPortion, j + 1, i + 1, options, isNegative);
                            }
                        }
                    }
                }

                if (!options.ExactMatch)
                {
                    for (var i = 0; i < options.MaxDenominator; ++i)
                    {
                        lock (DenominatorMap)
                        {
                            for (var j = 0; j < DenominatorMap[i].Count; ++j)
                            {
                                if (decimalPortion + options.Tolerance > DenominatorMap[i][j] &&
                                    decimalPortion - options.Tolerance < DenominatorMap[i][j])
                                {
                                    return FormatResult(integerPortion, j + 1, i + 1, options, isNegative, true);
                                }
                            }
                        }
                    }

                }
            }

            return number.ToString(options.Culture);
        }
    }
}
