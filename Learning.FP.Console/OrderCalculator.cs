public static class OrderCalculator
{
    public static decimal CalculateTotal(IEnumerable<Product> products)
    {
        // return products.Sum(p => p.Price);
        return products.Select(product => product.Price).Aggregate(seed: 0m, (total, price) => total + price);
    }
}