Console.WriteLine("Learning.FP");

var apple = new Product("Apple", 1.20m);
var bread = new Product("Bread", 2.50m);
var milk = new Product("Milk", 1.8m);

var products = new List<Product> { apple, bread, milk };

var totalPrice = OrderCalculator.CalculateTotal(products);

Console.WriteLine($"Total Price: {totalPrice:F2}");
