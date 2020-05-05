using USASales.Models;

namespace USASales
{
    public static class ProductsService
    {
        public static DetailedPrice CalculatePrice(Product product, Tax tax)
        {
            var margin = CalculateMargin(product.WholesalePrice, product.GrossPrice, tax.TaxPercentage);
            var taxValue = (product.WholesalePrice + margin) * tax.TaxPercentage * 0.01;

            return new DetailedPrice
            {
                WholesalePrice = product.WholesalePrice,
                Margin = margin,
                TaxPercentage = tax.TaxPercentage,
                TaxValue = taxValue,
                GrossPrice = product.GrossPrice,
                State = tax.State,
                Threshold = tax.ThresholdUsd
            };
        }

        private static double CalculateMargin(double basicPrice, double grossPrice, double taxPercentage)
        {
            return (grossPrice - basicPrice * (1 + taxPercentage * 0.01)) / (1 + taxPercentage * 0.01);
        }
    }
}