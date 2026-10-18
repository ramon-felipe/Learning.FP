namespace Learning.FP.Tests;

public class MapBindTests
{
    [Test]
    public void Map_ShouldTransformValue_WhenMaybeHasValue()
    {
        var maybe = Maybe.From(100m);

        var result = FunctionalMethods.Map(
            maybe,
            price => price * 1.23m);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.EqualTo(123m));
        }
    }

    [Test]
    public void Map_ShouldReturnNone_WhenMaybeHasNoValue()
    {
        var maybe = Maybe<decimal>.None;

        var result = FunctionalMethods.Map(
            maybe,
            price => price * 1.23m);

        Assert.That(result.HasNoValue, Is.True);
    }

    [Test]
    public void Bind_ShouldReturnFunctionResult_WhenMaybeHasValue()
    {
        var maybe = Maybe.From(100m);

        var result = FunctionalMethods.Bind(
            maybe,
            ProductMethods.ApplyDiscount);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value, Is.EqualTo(90m));
        }
    }

    [Test]
    public void Bind_ShouldReturnNone_WhenMaybeHasNoValue()
    {
        var maybe = Maybe<decimal>.None;

        var result = FunctionalMethods.Bind(
            maybe,
            ProductMethods.ApplyDiscount);

        Assert.That(result.HasNoValue, Is.True);
    }
}
