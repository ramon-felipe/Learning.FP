namespace Learning.FP;
using static Learning.FP.ProductMethods;
public static class OrderCalculator
{
    public static decimal CalculateTotal(IEnumerable<Product> products)
    {
        var minimumPricePredicate = CostIsGreaterThan(5m);

        return products
            .Where(IsPaidProduct)
            .Where(minimumPricePredicate)
            .Select(GetPrice)
            .Aggregate(seed: 0m, Add);
    }

    private static decimal Add(decimal total, decimal price)
    {
        return total + price;
    }
}