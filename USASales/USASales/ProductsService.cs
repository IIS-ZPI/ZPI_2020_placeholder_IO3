using USASales.Models;

namespace USASales
{
    public static class ProductsService
    {
        public static DetailedPrice CalculatePrice(Product product, Tax tax)
        {
            var taxPercentage = tax.TaxPercentage;
            var netPrice = product.GrossPrice / (1 + taxPercentage * 0.01);

            if (tax.ThresholdUsd > 0 && netPrice <= tax.ThresholdUsd)
            {
                taxPercentage = 0;
                netPrice = product.GrossPrice;
            }

            var margin = netPrice - product.WholesalePrice;
            var taxValue = netPrice * taxPercentage * 0.01;

            return new DetailedPrice
            {
                WholesalePrice = product.WholesalePrice,
                Margin = margin,
                TaxPercentage = taxPercentage,
                TaxValue = taxValue,
                GrossPrice = product.GrossPrice,
                State = tax.State,
                Threshold = tax.ThresholdUsd
            };
        }
    }
}