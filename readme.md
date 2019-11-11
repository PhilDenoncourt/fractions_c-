Fraction Formatter
==================

Fraction Formatter is a library that converts a real number into a 
fractional string.  Unicode representations of common fractions are
used if available.

Installation Instructions
-------------------------
The recommended way to get Fraction Formatter is to use Nuget

```
Install-Package FractionFormatter
``` 

Example Usage
-------------

###### Convert a real number to a fraction:

```csharp
using FractionFormatter;

...

string fractionalizedString = Fractionalize.ToFraction(.5d); 
```

fractionalizedString = "&#0189;"

###### As an extension method:

```csharp
using FractionFormatter;

...
float d = .5;
string fractionalizedString = d.ToFraction();
```

###### Use the approximation symbol when the match isn't exact:

```csharp
using FractionFormatter;

...

var opts = new FractionalizeOptions {ShowApproximationSymbol = true};
string testValue = Fractionalize.ToFraction(.5001d, opts);
```
fractionalizedString = "&#8776;&#0189;"

### Options
```csharp
public class FractionalizeOptions
{
    public int MaxDenominator { get; set; } = 64;
    public bool SpaceBetweenIntegerAndFraction {get;set;} = true;
    public double Tolerance { get; set; } = 0.01f;
    public bool ExactMatch { get; set; } = false;
    public bool ShowApproximationSymbol { get; set; } = false;

    public CultureInfo Culture = CultureInfo.InvariantCulture;
}
```

Supply a class to the ToFraction method to fine tune the process.

* maxDenominator - The highest number that would acceptable as a denominator in the fraction.  Defaults to 64.
* spaceBetweenIntegerAndFraction - Determines whether or not to place a space between the fraction and the integer portion of the number.  Defaults to true
* tolerance - if exactMatch is false, how close does the real number need to be to the fractional number to be used.  Default is 1/100 (.01)
* exactMatch - if true, the number will only be shown as a fraction if it exactly matches. 
* showApproximationSymbol - if true, and the fractional representation is not exact, show the ≈ symbol.
