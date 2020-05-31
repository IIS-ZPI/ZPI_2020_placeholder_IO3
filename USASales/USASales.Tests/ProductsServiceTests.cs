using Microsoft.VisualStudio.TestTools.UnitTesting;
using USASales.Models;

namespace USASales.Tests
{
    [TestClass]
    public class ProductsServiceTests
    {
        [TestMethod]
        public void Should_Calculate_Net_Price_And_Margin()
        {
            var apple = new Product
            {
                Name = "Apple",
                Category = "Groceries",
                WholesalePrice = 0.25,
                GrossPrice = 1
            };

            var tax = new Tax
            {
                State = "Alabama",
                Category = "Groceries",
                TaxPercentage = 4
            };

            var result = ProductsService.CalculatePrice(apple, tax, 1);

            Assert.AreEqual(0.9615, result.NetPrice, 0.00005);
            Assert.AreEqual(0.7115, result.Margin, 0.00005);
        }

        [TestMethod]
        public void Should_Calculate_Net_Price_And_Margin_When_Tax_Free()
        {
            var apple = new Product
            {
                Name = "Apple",
                Category = "Groceries",
                WholesalePrice = 0.25,
                GrossPrice = 1
            };

            var tax = new Tax
            {
                State = "Alabama",
                Category = "Groceries",
                TaxPercentage = 0
            };

            var result = ProductsService.CalculatePrice(apple, tax, 1);

            Assert.AreEqual(1.0, result.NetPrice, 0.05);
            Assert.AreEqual(0.75, result.Margin, 0.005);
        }

        [TestMethod]
        public void Should_Calculate_Net_Price_And_Margin_When_Above_Threshold()
        {
            var shirt = new Product
            {
                Name = "Shirt",
                Category = "Clothing",
                WholesalePrice = 125,
                GrossPrice = 150
            };

            var tax = new Tax
            {
                State = "New York",
                Category = "Clothing",
                TaxPercentage = 4,
                ThresholdUsd = 110
            };

            var result = ProductsService.CalculatePrice(shirt, tax, 1);

            Assert.AreEqual(144.23, result.NetPrice, 0.05);
            Assert.AreEqual(19.23, result.Margin, 0.005);
            Assert.AreEqual(150, result.GrossPrice, 0.05);
        }

        [TestMethod]
        public void Should_Calculate_Net_Price_And_Margin_When_Below_Threshold()
        {
            var shirt = new Product
            {
                Name = "Shirt",
                Category = "Clothing",
                WholesalePrice = 90,
                GrossPrice = 100
            };

            var tax = new Tax
            {
                State = "New York",
                Category = "Clothing",
                TaxPercentage = 4,
                ThresholdUsd = 110
            };

            var result = ProductsService.CalculatePrice(shirt, tax, 1);

            Assert.AreEqual(100, result.NetPrice, 0.05);
            Assert.AreEqual(10, result.Margin, 0.005);
            Assert.AreEqual(100, result.GrossPrice, 0.05);
        }

        [TestMethod]
        public void Should_Calculate_Net_Price_And_Margin_When_Below_Threshold_If_Quantity_Is_Two()
        {
            var shirt = new Product
            {
                Name = "Shirt",
                Category = "Clothing",
                WholesalePrice = 10,
                GrossPrice = 20
            };

            var tax = new Tax
            {
                State = "New York",
                Category = "Clothing",
                TaxPercentage = 4,
                ThresholdUsd = 110
            };

            var result = ProductsService.CalculatePrice(shirt, tax, 2);

            Assert.AreEqual(40, result.NetPrice, 0.05);
            Assert.AreEqual(20, result.Margin, 0.005);
            Assert.AreEqual(40, result.GrossPrice, 0.05);
        }

        [TestMethod]
        public void Should_Calculate_Net_Price_And_Margin_When_Above_Threshold_If_Quantity_Is_Two()
        {
            var shirt = new Product
            {
                Name = "Shirt",
                Category = "Clothing",
                WholesalePrice = 55,
                GrossPrice = 60
            };

            var tax = new Tax
            {
                State = "New York",
                Category = "Clothing",
                TaxPercentage = 4,
                ThresholdUsd = 110
            };

            var result = ProductsService.CalculatePrice(shirt, tax, 2);

            Assert.AreEqual(115.38, result.NetPrice, 0.05);
            Assert.AreEqual(5.38, result.Margin, 0.005);
            Assert.AreEqual(120, result.GrossPrice, 0.05);
        }
    }
}
