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
}