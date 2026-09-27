namespace Learning.FP.Tests;

public class Tests
{
    [Test]
    public void CalculateTotal_ShouldReturnSumOfProductPrices()
    {
        // Arrange
        var products = new List<Product>
        {
            new("Apple", 1.20m),
            new("Bread", 2.50m),
            new("Milk", 1.80m)
        };

        // Act
        var totalPrice = OrderCalculator.CalculateTotal(products);

        // Assert
        Assert.That(totalPrice, Is.EqualTo(5.50m));
    }

    [Test]
    public void CalculateTotal_ShouldReturnZero_WhenNoProducts()
    {
        // Arrange
        var products = new List<Product>();

        // Act
        var totalPrice = OrderCalculator.CalculateTotal(products);

        // Assert
        Assert.That(totalPrice, Is.EqualTo(0m));
    }
}
