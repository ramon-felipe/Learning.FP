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

    public static Either<PriceValidationError, decimal> ApplyDiscountEither(decimal price)
    {
        return price > 100m
            ? Either<PriceValidationError, decimal>.Right(price * 0.9m)
            : Either<PriceValidationError, decimal>.Left(PriceValidationError.DiscountNotApplicable);
    }

    public static Either<PriceValidationError, decimal> ValidatePrice(decimal price) =>
        price > 0
            ? Either<PriceValidationError, decimal>.Right(price)
            : Either<PriceValidationError, decimal>.Left(PriceValidationError.InvalidPrice);
}