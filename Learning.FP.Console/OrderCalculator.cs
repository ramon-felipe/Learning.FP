namespace Learning.FP;

public static class OrderCalculator
{
    public static decimal CalculateTotal(IEnumerable<Product> products)
    {
        return products
            .Where(ProductMethods.IsPaidProduct)
            .Select(ProductMethods.GetPrice)
            .Aggregate(seed: 0m, Add);
    }

    private static decimal Add(decimal total, decimal price)
    {
        return total + price;
    }
}