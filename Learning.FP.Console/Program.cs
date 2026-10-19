using Learning.FP;
using LanguageExt;

Either<string, decimal> successfulPrice =
    Either<string, decimal>.Right(90m);

Either<string, decimal> failedPrice =
    Either<string, decimal>.Left("Price must be positive");

var successfulWithTax = successfulPrice.Map(OrderCalculator.AddTax);
var failedWithTax = failedPrice.Map(OrderCalculator.AddTax);
var failedWithContext = failedWithTax.MapLeft(
    error => $"Validation error: {error}");

var finalPrice = successfulPrice.Bind(Operations.ApplyDiscountEither).Map(OrderCalculator.AddTax);
var finalFailedPrice = failedPrice.Bind(Operations.ApplyDiscountEither).Map(OrderCalculator.AddTax);

// -------------

var optSome = Option<decimal>.Some(10m);
var optNone = Option<decimal>.None;

var optSomeWithTax = optSome.Map(OrderCalculator.AddTax);
var optNoneWithTax = optNone.Map(OrderCalculator.AddTax);

var optSomeWithDiscount = optSome.Bind(Operations.ApplyDiscount).Map(OrderCalculator.AddTax);
var optNoneWithDiscount = optNone.Bind(Operations.ApplyDiscount).Map(OrderCalculator.AddTax);

// --------
decimal? nullDecimal = null;
var test = Maybe<decimal>.None;

var price = Maybe.From(100m);

var discount = FunctionalMethods.Bind(price, OrderCalculator.ApplyDiscount);
var discountAddedTax = FunctionalMethods.Map(discount, OrderCalculator.AddTax);

// ----

var resultMap = FunctionalMethods.Map(test, OrderCalculator.AddTax);
var resultMapBind = FunctionalMethods.Bind(resultMap, ProductMethods.ApplyDiscount);
var maybeDiscount = FunctionalMethods.Map(price, ProductMethods.ApplyDiscount);
var maybeDiscount2 = FunctionalMethods.Bind(price, ProductMethods.ApplyDiscount);

// ---------------

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


static Maybe<decimal> ApplyDiscount(decimal price)
{
    if (price < 10m)
        return Maybe<decimal>.From(price);

    return Maybe<decimal>.From(price * 0.9m);
}