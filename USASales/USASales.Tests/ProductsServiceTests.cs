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
                WholesalePrice = 0.25m,
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
                WholesalePrice = 0.25m,
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
    }
}
