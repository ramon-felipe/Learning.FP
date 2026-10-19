using LanguageExt.UnsafeValueAccess;

namespace Learning.FP.Tests;

public sealed class Tests2
{
    [Test]
    public void MyTest1()
    {
        // Arrange
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        var min = 3;

        // Act
        var evenNumbers = numbers
            .Where(_ => _ % 2 == 0)
            .Where(_ => _ > min);

    }

    [Test]
    public void ValidatePrice_ShouldReturnRight_WhenPriceIsPositive()
    {
        var result = Operations.ValidatePrice(100m);

        var actual = result.Match(
            Right: price => price,
            Left: _ => -1m);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsRight, Is.True);
            Assert.That(actual, Is.EqualTo(100m));
        }
    }

    [TestCase(0)]
    [TestCase(-10)]
    public void ValidatePrice_ShouldReturnLeft_WhenPriceIsNotPositive(
        decimal price)
    {
        var result = Operations.ValidatePrice(price);

        var actual = result.Match(
            Right: _ => (PriceValidationError?)null,
            Left: error => error);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsLeft, Is.True);
            Assert.That(actual, Is.EqualTo(PriceValidationError.InvalidPrice));
        }
    }

    private static bool IsEven(int number) => number % 2 == 0;
}
