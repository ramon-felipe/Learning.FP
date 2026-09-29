namespace Learning.FP;
using static Learning.FP.ProductMethods;
public static class OrderCalculator
{
    public static decimal CalculateTotal(IEnumerable<Product> products)
    {
        var getPriceWithTaxFunc = Compose<Product, decimal, decimal>(GetPrice, AddTax);
        var costMoreThan5 = CostIsGreaterThan(5m);

        return products
            .Where(IsPaidProduct)
            .Where(costMoreThan5)
            .Select(getPriceWithTaxFunc)
            .Aggregate(seed: 0m, Add);
    }

    private static decimal Add(decimal total, decimal price)
    {
        return total + price;
    }

    private static decimal AddTax(decimal price)
    {
        return price * 1.23m;
    }

    private static decimal GetPriceWithTax(Product product)
    {
        var addTaxFunc = TaxCalculator.CreateTaxCalculator(1.23m);
        return addTaxFunc(GetPrice(product));
    }

    public static Func<A, C> Compose<A,B,C>(
        Func<A, B> first,
        Func<B, C> second)
    {
        return product => second(first(product));
    }

}

public static class TaxCalculator
{
    public static Func<decimal, decimal> CreateTaxCalculator(decimal taxRate)
    {
        return price => price * taxRate;
    }
}