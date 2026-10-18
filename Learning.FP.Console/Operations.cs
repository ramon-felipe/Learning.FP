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

    public static Either<string, decimal> ApplyDiscountEither(decimal price)
    {
        return price > 0
            ? Either<string, decimal>.Right(price * 0.9m)
            : Either<string, decimal>.Left("Price must be positive");
    }
}