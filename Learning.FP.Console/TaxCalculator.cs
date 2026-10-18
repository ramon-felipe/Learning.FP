namespace Learning.FP;

public static class TaxCalculator
{
    public static Func<decimal, decimal> CreateTaxCalculator(decimal taxRate)
    {
        return price => price * taxRate;
    }
}
