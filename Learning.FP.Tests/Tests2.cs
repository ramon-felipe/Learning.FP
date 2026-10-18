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

    private static bool IsEven(int number) => number % 2 == 0;
}
