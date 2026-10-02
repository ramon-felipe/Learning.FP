namespace Learning.FP;

/// <summary>
/// Non-generic entrypoint for <see cref="Maybe{T}" /> members
/// </summary>
public readonly struct Maybe
{
    public static Maybe None => new();

    /// <summary>
    /// Creates a new <see cref="Maybe{T}" /> from the provided <paramref name="value"/>
    /// </summary>
    public static Maybe<T> From<T>(T? value) => Maybe<T>.From(value);
}

public interface IMaybe<out T>
{
    T Value { get; }
    bool HasValue { get; }
    bool HasNoValue { get; }
}

public readonly struct Maybe<T> : IMaybe<T>
{
    private readonly T? _value;
    private readonly bool _isValueSet;
    public bool HasValue => _isValueSet;
    public bool HasNoValue => !HasValue;

    private Maybe(T? value)
    {
        if (value == null)
        {
            _isValueSet = false;
            _value = default;
            return;
        }

        _isValueSet = true;
        _value = value;
    }

    public static Maybe<T> From(T? value)
    {
        return new Maybe<T>(value);
    }


    public static Maybe<T> None => new();
    public T Value => GetValueOrThrow();


    /// <summary>
    /// Returns the inner value if there's one, otherwise throws an InvalidOperationException with <paramref name="errorMessage"/>
    /// </summary>
    /// <exception cref="InvalidOperationException">Maybe has no value.</exception>
    public T GetValueOrThrow(string? errorMessage = null)
    {
        if (HasNoValue)
            throw new InvalidOperationException(errorMessage);

        return _value;
    }

    public T? GetValueOrDefault()
    {
        if (HasNoValue)
            return default;

        return _value;
    }

}

