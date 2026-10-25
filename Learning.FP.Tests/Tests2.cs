using LanguageExt.UnitTesting;

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
            Assert.That(actual, Is.EqualTo(new InvalidPriceError("Price must be positive")));
        }
    }

    [Test]
    public void ApplyDiscount_ShouldReturnRight_WhenPriceIsHigherthan100()
    {
        var result = Operations.ApplyDiscountEither(101m);

        var actual = result.Match(
            Right: price => price,
            Left: _ => -1m);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsRight, Is.True);
            Assert.That(actual, Is.EqualTo(101m * 0.9m));
        }
    }

    [TestCase(99)]
    [TestCase(100)]
    public void ApplyDiscount_ShouldReturnLeft_WhenPriceIsNotHigherthan100(decimal price)
    {
        var result = Operations.ApplyDiscountEither(price);

        var actual = result.Match(
            Right: _ => (PriceValidationError?)null,
            Left: error => error);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsLeft, Is.True);
            Assert.That(actual, Is.EqualTo(new DiscountNotApplicableError("Discount not applicable for this price")));
        }
    }

    [Test]
    public void CalculateFinalPrice_ShouldReturnFinalPrice_WhenPriceIsEligible()
    {
        var result = Operations.CalculateFinalPrice(150m);

        var actual = result.Match(
            Right: price => price,
            Left: _ => -1m);

        Assert.That(result.IsRight, Is.True);
        Assert.That(actual, Is.EqualTo(166.05m));
    }

    [Test]
    public void CalculateFinalPrice_ShouldReturnInvalidPrice_WhenPriceIsZero()
    {
        var result = Operations.CalculateFinalPrice(0m);

        var actual = result.Match(
            Right: _ => (PriceValidationError?)null,
            Left: error => error);

        Assert.That(result.IsLeft, Is.True);
        Assert.That(actual, Is.EqualTo(new InvalidPriceError("Price must be positive")));
    }

    [Test]
    public void CalculateFinalPrice_ShouldReturnDiscountNotApplicable_WhenPriceIsTooLow()
    {
        var result = Operations.CalculateFinalPrice(50m);

        var actual = result.Match(
            Right: _ => (PriceValidationError?)null,
            Left: error => error);

        Assert.That(result.IsLeft, Is.True);
        Assert.That(actual, Is.EqualTo(new DiscountNotApplicableError("Discount not applicable for this price")));
    }

    [TestCase(101)]
    [TestCase(150)]
    public void ApplyOptionalDiscount_ShouldReturnDiscountedPrice_WhenPriceIsEligible(decimal price)
    {
        // Act
        var result = Operations.ApplyOptionalDiscount(price);

        // Arrange
        result.ShouldBeRight(_ => Assert.That(_, Is.EqualTo(price * 0.9m)));
    }

    [TestCase(99)]
    [TestCase(1)]
    public void ApplyOptionalDiscount_ShouldReturnOriginalPrice_WhenPriceIsNotEligible(decimal price)
    {
        // Act
        var result = Operations.ApplyOptionalDiscount(price);

        // Arrange
        result.ShouldBeRight(_ => Assert.That(_, Is.EqualTo(price)));
    }

    [TestCase(-1)]
    [TestCase(0)]
    public void ApplyOptionalDiscount_ShouldReturnError_WhenPriceIsNotValid(decimal price)
    {
        // Arrange
        var expectedError = new InvalidPriceError("Price must be positive");

        // Act
        var result = Operations.ApplyOptionalDiscount(price);

        // Arrange
        result.ShouldBeLeft(error => Assert.That(error, Is.EqualTo(expectedError)));
    }

    [TestCase(101)]
    [TestCase(150)]
    public void CalculateFinalPriceWithErrorMessage_ShouldReturnDiscountedPrice_WhenPriceIsEligible(decimal price)
    {
        // Act
        var result = Operations.CalculateFinalPriceWithErrorMessage(price);

        // Arrange
        result.ShouldBeRight(_ => Assert.That(_, Is.EqualTo(price * 0.9m * 1.23m)));
    }

    [TestCase(99)]
    [TestCase(1)]
    public void CalculateFinalPriceWithErrorMessage_ShouldReturnDiscountNotApplicable_WhenPriceIsNotValid(decimal price)
    {
        // Arrange
        var expectedError = "DISCOUNT_NOT_APPLICABLE: Discount not applicable for this price";

        // Act
        var result = Operations.CalculateFinalPriceWithErrorMessage(price);

        // Arrange
        result.ShouldBeLeft(error => Assert.That(error, Is.EqualTo(expectedError)));
    }

    [TestCase(-1)]
    [TestCase(0)]
    public void CalculateFinalPriceWithErrorMessage_ShouldReturnINVALID_PRICE_WhenPriceIsNotValid(decimal price)
    {
        // Arrange
        var expectedError = "INVALID_PRICE: Price must be positive";

        // Act
        var result = Operations.CalculateFinalPriceWithErrorMessage(price);

        // Arrange
        result.ShouldBeLeft(error => Assert.That(error, Is.EqualTo(expectedError)));
    }
}
