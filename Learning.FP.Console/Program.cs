using Learning.FP;

var forThreshold = CostIsGreaterThan();
var expensiveProduct = forThreshold(5m);
var result = expensiveProduct(new Product("Apple", 1.20m));

var addTax23 = TaxCalculator.CreateTaxCalculator(1.23m);
var addTax33 = TaxCalculator.CreateTaxCalculator(1.33m);
var priceWith23 = addTax23(100m);
var priceWith33 = addTax33(100m);

var x = addTax23(10m);

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


static Func<decimal, bool> IsGreaterThan(decimal threshold)
{
    return value => value > threshold;
}

static Func<decimal, Func<Product, bool>> CostIsGreaterThan()
    => threshold =>
        product => product.Price > threshold;