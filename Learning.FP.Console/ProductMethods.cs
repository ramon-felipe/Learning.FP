namespace Learning.FP;

public static class ProductMethods
{
    // pure function
    public static decimal GetPrice(Product product)
    {
        return product.Price;
    }

    public static bool IsPaidProduct(Product product)
    {
        return product.Price > 0;
    }

    public static Func<Product, bool> CostIsGreaterThan(decimal threshold)
    {
        return product => product.Price > threshold;        
    }

    // impure function
    public static decimal GetPriceWithLogging(Product product)
    {
        Console.WriteLine($"Getting price for {product.Name}");
        return product.Price;
    }

    public static Maybe<decimal> ApplyDiscount(decimal price)
    {
        if (price < 10m)
            return Maybe.From(price);

        return Maybe.From(price * 0.9m);
    }

}