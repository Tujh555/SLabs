using System.Globalization;

namespace SLabs.Lab2;

public class ComplexNumber
{
    private const double E = Math.E;
    private readonly double _degree;
    private readonly double _module;

    public ComplexNumber(double module, double degree)
    {
        _degree = degree;
        _module = module;
    }

    public override string ToString()
    {
        if (_degree == 0)
        {
            return _module.ToString(CultureInfo.CurrentUICulture);
        }
        
        var degreePrefix = _degree < 0 ? "-" : "";
        return $"{_module}^{degreePrefix}i{_degree}";
    }

    private static double GetModule(double real, double imaginary) => Math.Sqrt(real * real + imaginary * imaginary);

    private static double GetDegree(double real, double imaginary)
    {
        if (real > 0)
            return Math.Atan(imaginary / real);
        
        if (imaginary > 0) 
            return Math.Atan(imaginary / real) + Math.PI;
        
        return Math.Atan(imaginary / real) - Math.PI;
    }
    
    public static ComplexNumber operator +(ComplexNumber first, ComplexNumber second)
    {
        var a = first._module * Math.Cos(first._degree) + second._module * Math.Cos(second._degree);
        var b = first._module * Math.Sin(first._degree) + second._module * Math.Sin(second._degree);

        var degree = GetDegree(a, b);
        var module = GetModule(a, b);

        return new ComplexNumber(module, degree);
    }
    
    public static ComplexNumber operator -(ComplexNumber first, ComplexNumber second)
    {
        var a = first._module * Math.Cos(first._degree) - second._module * Math.Cos(second._degree);
        var b = first._module * Math.Sin(first._degree) - second._module * Math.Sin(second._degree);

        var degree = GetDegree(a, b);
        var module = GetModule(a, b);

        return new ComplexNumber(module, degree);
    }

    public static ComplexNumber operator *(ComplexNumber first, ComplexNumber second) => new(
        first._module * first._module,
        first._degree + second._degree);
}