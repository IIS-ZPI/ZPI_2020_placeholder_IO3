using USASales.Models;

namespace USASales
{
    public static class ProductsService
    {
        public static DetailedPrice CalculatePrice(Product product, Tax tax)
        {
            var margin = CalculateMargin((double)product.PurchaseAmount, (double)product.GrossPrice, tax.TaxPercentage);
            var taxValue = ((double)product.PurchaseAmount + margin) * tax.TaxPercentage * 0.01;

            return new DetailedPrice
            {
                PurchaseAmount = (double)product.PurchaseAmount,
                Margin = margin,
                TaxPercentage = tax.TaxPercentage,
                TaxValue = taxValue,
                GrossPrice = (double)product.GrossPrice
            };
        }

        private static double CalculateMargin(double basicPrice, double grossPrice, double taxPercentage)
        {
            return (grossPrice - basicPrice * (1 + taxPercentage * 0.01)) / (1 + taxPercentage * 0.01);
        }
    }
}