namespace Learning.FP;

public static class FunctionalMethods
{
    public static Maybe<B> Map<A, B>(
        Maybe<A> maybe,
        Func<A, B> function) => maybe.HasNoValue ? Maybe<B>.None : Maybe<B>.From(function(maybe.Value));

    public static Maybe<B> Bind<A, B>(
        Maybe<A> maybe,
        Func<A, Maybe<B>> function) => maybe.HasValue ? function(maybe.Value) : Maybe<B>.None;
}

