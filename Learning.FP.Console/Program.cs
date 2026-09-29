using Learning.FP;

var products = GetProducts();

var total1 = OrderCalculator.CalculateTotal(products);
var total2 = OrderCalculator.CalculateTotal(products);
var total3 = OrderCalculator.CalculateTotal(products);

IEnumerable<Product> GetProducts()
{
    Console.WriteLine("Loading products...");
    yield return new Product("Apple", 1.20m);
    yield return new Product("Bread", 2.50m);
    yield return new Product("Bread", 2.50m);
}

Console.WriteLine($"Total 1: {total1}");
Console.WriteLine($"Total 2: {total2}");
Console.WriteLine($"Total 3: {total3}");

static decimal GetPriceWithTax(Product product)
{
    // manual composition
    return AddTax(GetPrice(product));
}

// pure function
// deterministic: given the same input, it will always return the same output
// no side effects: it does not modify any external state or have any observable effects outside of returning a value
static decimal GetPrice(Product product)
{
    return product.Price;
}

static decimal AddTax(decimal price)
{
    return price * 1.23m;
}