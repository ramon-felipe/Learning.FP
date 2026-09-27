namespace Learning.FP;

public static class OrderCalculator
{
    public static decimal CalculateTotal(IEnumerable<Product> products)
    {
        return products
            .Select(product => product.Price)
            .Where(price => price > 0)
            .Aggregate(seed: 0m, (total, price) => total + price);
    }
}