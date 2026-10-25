namespace Learning.FP;

using LanguageExt;
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

    public static Either<string, decimal> CalculateOrderTotal(decimal price, int quantity)
    {
        var priceValidation = Operations.ValidatePrice(price);
        var quantityValidation = Operations.ValidateQuantity(quantity);

        if (priceValidation.IsRight && quantityValidation.IsRight)
            return Either<string, decimal>.Right(price * quantity);

        var priceRes = priceValidation.Match(
            Right: _ => "",
            Left: error => error.Message);

        var quantityRes = quantityValidation.Match(
            Right: _ => "",
            Left: error => error.Message);

        var errors = new[] { priceRes, quantityRes }
            .Where(error => !string.IsNullOrEmpty(error));

        return Either<string, decimal>.Left(string.Join("; ", errors));
    }

    public static Either<string, decimal> CalculateOrderTotalToValidation(decimal price, int quantity)
    {
        var priceValidation = Operations.ValidatePrice(price).ToValidation();
        var quantityValidation = Operations.ValidateQuantity(quantity).ToValidation();

        return (priceValidation, quantityValidation)
            .Apply((validPrice, validQuantity) => validPrice * validQuantity)
            .Match(
                Succ: Either<string, decimal>.Right,
                Fail: errors => Either<string, decimal>.Left(string.Join("; ", errors.Select(e => e.Message)))
            );
    }

    public static Either<string, decimal> CalculateOrderTotalValidation(decimal price, int quantity)
    {
        return (ValidatePrice(price), ValidateQuantity(quantity))
            .Apply((validPrice, validQuantity) => validPrice * validQuantity)
            .Match(
                Succ: Either<string, decimal>.Right,
                Fail: errors => Either<string, decimal>.Left(string.Join("; ", errors.Select(e => e)))
            );
    }

    public static Validation<string, decimal> ValidatePrice(decimal price)
    {
        return price > 0
            ? Validation<string, decimal>.Success(price)
            : Validation<string, decimal>.Fail(Seq.create("Price must be positive"));   
    }

    public static Validation<string, int> ValidateQuantity(int quantity)
    {
        return quantity > 0
            ? Validation<string, int>.Success(quantity)
            : Validation<string, int>.Fail(Seq.create("Quantity must be positive"));
    }

    private static decimal Add(decimal total, decimal price)
    {
        return total + price;
    }

    public static decimal AddTax(decimal price)
    {
        return price * 1.23m;
    }

    public static Maybe<decimal> ApplyDiscount(decimal price)
    {
        return Maybe.From(price * 0.9m);
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
