namespace Learning.FP;

public abstract record PriceValidationError(string Code, string Message);
public sealed record InvalidPriceError(string Message) : PriceValidationError("INVALID_PRICE", Message);
public sealed record DiscountNotApplicableError(string Message) : PriceValidationError("DISCOUNT_NOT_APPLICABLE", Message);