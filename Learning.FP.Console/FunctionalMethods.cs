namespace Learning.FP;

public static class FunctionalMethods
{
    public static Maybe<B> Map<A, B>(
        Maybe<A> maybe,
        Func<A, B> function)
        {
            return new Maybe<B>(function(maybe.Value));
        }
}

