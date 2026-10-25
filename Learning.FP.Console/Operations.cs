namespace Learning.FP;

using LanguageExt;

public static class Operations
{
    public static Option<decimal> ApplyDiscount(decimal price)
    {
        return price > 0
            ? Option<decimal>.Some(price * 0.9m)
            : Option<decimal>.None;
    }

    public static Either<PriceValidationError, decimal> CalculateFinalPrice(decimal price) =>
        ValidatePrice(price)
            .Bind(ApplyDiscountEither)
            .Map(OrderCalculator.AddTax);

    public static Either<PriceValidationError, decimal> ValidatePrice(decimal price) =>
        price > 0
            ? Either<PriceValidationError, decimal>.Right(price)
            : Either<PriceValidationError, decimal>.Left(new InvalidPriceError("Price must be positive"));

    public static Either<PriceValidationError, decimal> ValidateQuantity(decimal quantity) =>
        quantity > 0
            ? Either<PriceValidationError, decimal>.Right(quantity)
            : Either<PriceValidationError, decimal>.Left(new InvalidPriceError("Quantity must be positive"));

    public static Either<PriceValidationError, decimal> ApplyDiscountEither(decimal price)
    {
        return price > 100m
            ? Either<PriceValidationError, decimal>.Right(price * 0.9m)
            : Either<PriceValidationError, decimal>.Left(new DiscountNotApplicableError("Discount not applicable for this price"));
    }

    public static Either<PriceValidationError, decimal> ApplyOptionalDiscount(decimal price) =>
        ValidatePrice(price)
            .Bind(p => 
                ApplyDiscountEither(p)
                .Match(
                    Right: discountedPrice => Either<PriceValidationError, decimal>.Right(discountedPrice),
                    Left: _ => Either<PriceValidationError, decimal>.Right(p) // If discount not applicable, return original price
            ));

    public static Either<string, decimal> CalculateFinalPriceWithErrorMessage(decimal price) =>
        CalculateFinalPrice(price)
            .MapLeft(error => $"{error.Code}: {error.Message}");
}