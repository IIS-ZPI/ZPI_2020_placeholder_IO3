using Microsoft.VisualStudio.TestTools.UnitTesting;
using USASales.Models;

namespace USASales.Tests
{
    [TestClass]
    public class ProductsServiceTests
    {
        [TestMethod]
        public void ShouldCalculateNetPriceAndMargin()
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

            var result = ProductsService.CalculatePrice(apple, tax);

            Assert.AreEqual(0.9615, result.NetPrice, 0.00005);
            Assert.AreEqual(0.7115, result.Margin, 0.00005);
        }

        [TestMethod]
        public void ShouldCalculateNetPriceAndMarginWhenTaxFree()
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

            var result = ProductsService.CalculatePrice(apple, tax);

            Assert.AreEqual(1.0, result.NetPrice, 0.05);
            Assert.AreEqual(0.75, result.Margin, 0.005);
        }

        [TestMethod]
        public void ShouldCalculateNetPriceAndMarginWhenAboveThreshold()
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

            var result = ProductsService.CalculatePrice(shirt, tax);

            Assert.AreEqual(144.23, result.NetPrice, 0.05);
            Assert.AreEqual(19.23, result.Margin, 0.005);
            Assert.AreEqual(150, shirt.GrossPrice, 0.05);
        }

        [TestMethod]
        public void ShouldCalculateNetPriceAndMarginWhenBelowThreshold()
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

            var result = ProductsService.CalculatePrice(shirt, tax);

            Assert.AreEqual(100, result.NetPrice, 0.05);
            Assert.AreEqual(10, result.Margin, 0.005);
            Assert.AreEqual(100, shirt.GrossPrice, 0.05);
        }
    }
}
