namespace Learning.FP;

public sealed class Maybe<T>
{
    public T Value { get; }

    public Maybe(T value)
    {
        Value = value;
    }
}

