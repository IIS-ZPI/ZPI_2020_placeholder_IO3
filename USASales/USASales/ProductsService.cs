using System.Drawing.Printing;
using USASales.Models;

namespace USASales
{
    public static class ProductsService
    {
        public static DetailedPrice CalculatePrice(Product product, Tax tax, int amount)
        {
            var taxPercentage = tax.TaxPercentage;
            var netPrice = product.GrossPrice / (1 + taxPercentage * 0.01) * amount;

            if (tax.ThresholdUsd > 0 && netPrice <= tax.ThresholdUsd)
            {
                taxPercentage = 0;
                netPrice = product.GrossPrice * amount;
            }

            var margin = netPrice - product.WholesalePrice * amount;
            var taxValue = netPrice * taxPercentage * 0.01;

            return new DetailedPrice
            {
                WholesalePrice = product.WholesalePrice * amount,
                Margin = margin,
                TaxPercentage = taxPercentage,
                TaxValue = taxValue,
                GrossPrice = product.GrossPrice * amount,
                State = tax.State,
                Threshold = tax.ThresholdUsd
            };
        }
    }
}