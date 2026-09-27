namespace Learning.FP;

public static class ProductMethods
{
    // pure function
    public static decimal GetPrice(Product product)
    {
        return product.Price;
    }

    // impure function
    public static decimal GetPriceWithLogging(Product product)
    {
        Console.WriteLine($"Getting price for {product.Name}");
        return product.Price;
    }

}