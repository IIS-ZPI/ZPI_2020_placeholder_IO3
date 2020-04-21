using Microsoft.VisualStudio.TestTools.UnitTesting;
using USASales.Models;

namespace USASales.Tests
{
    [TestClass]
    public class ProductsServiceTests
    {
        [TestMethod]
        public void ShouldReturnValidTaxValue()
        {
            var product = new Product
            {
                PurchaseAmount = 90,
                GrossPrice = 100
            };

            var tax = new Tax
            {
                TaxPercentage = 10
            };

            var result = ProductsService.CalculatePrice(product, tax);

            Assert.AreEqual(9.1, result.TaxValue, 0.1);
        }
    }
}
