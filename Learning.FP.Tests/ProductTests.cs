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
    public void CalculateTotal_Should_NotSum_FreeProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new("Apple", 1.20m),
            new("Bread", 2.50m),
            new("Milk", 1.80m),
            new("Free Milk", 0m)
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

    [Test]
    public void CalculateTotal_ShouldEnumerateProducts()
    {
        var enumerationCount = 0;

        IEnumerable<Product> GetProducts()
        {
            enumerationCount++;

            yield return new Product("Apple", 1.20m);
            yield return new Product("Bread", 2.50m);
        }

        var products = GetProducts();

        Assert.That(enumerationCount, Is.EqualTo(0));

        var total = OrderCalculator.CalculateTotal(products);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(total, Is.EqualTo(3.70m));
            Assert.That(enumerationCount, Is.EqualTo(1));
        }
    }

    [Test]
    public void CalculateTotal_ShouldEnumerateEachProduct()
    {
        var enumerationCount = 0;

        IEnumerable<Product> GetProducts()
        {
            yield return new Product("Apple", 1.20m);
            enumerationCount++;

            yield return new Product("Bread", 2.50m);
            enumerationCount++;
        }

        var products = GetProducts();
        Assert.That(enumerationCount, Is.EqualTo(0));

        var total = OrderCalculator.CalculateTotal(products);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(total, Is.EqualTo(3.70m));
            Assert.That(enumerationCount, Is.EqualTo(2));
        }
    }

    [Test]
    public void PureFunction()
    {
        // Arrange
        var products = new List<Product>
        {
            new("Apple", 1.20m),
            new("Bread", 2.50m),
            new("Milk", 1.80m)
        };

        // Act
        var totalPrice = products.Select(ProductMethods.GetPrice).Sum();

        // Assert
        Assert.That(totalPrice, Is.EqualTo(5.50m));
    }

    [Test]
    public void ImpureFunction()
    {
        // Arrange
        var products = new List<Product>
        {
            new("Apple", 1.20m),
            new("Bread", 2.50m),
            new("Milk", 1.80m)
        };

        // Act
        var prices = products.Select(ProductMethods.GetPriceWithLogging);
        var totalPrice = prices.Sum();

        // Assert
        Assert.That(totalPrice, Is.EqualTo(5.50m));
    }
}
