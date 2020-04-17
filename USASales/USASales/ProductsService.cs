using System.Runtime.InteropServices;
using System.Threading.Tasks;
using USASales.Models;
using USASales.Repositories;

namespace USASales
{
    public class ProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ITaxesRepository _taxesRepository;
        public ProductsService(IProductsRepository productsRepository, ITaxesRepository taxesRepository)
        {
            _productsRepository = productsRepository;
            _taxesRepository = taxesRepository;
        }

        public async Task<DetailedPrice> CalculatePrice(long productId, string state, string category)
        {
            var tax = await _taxesRepository.Get(state, category);

            var product = await _productsRepository.Get(productId);

            var margin = CalculateMargin((double)product.PurchaseAmount, (double)product.GrossPrice, tax.TaxPercentage);

            var taxValue = ((double)product.PurchaseAmount + margin) * tax.TaxPercentage * 0.01;

            return new DetailedPrice
            {
                PurchaseAmount = (double) product.PurchaseAmount,
                Margin = margin,
                TaxPercentage = tax.TaxPercentage,
                TaxValue = taxValue,
                GrossPrice = (double) product.GrossPrice
            };
        }

        private static double CalculateMargin(double basicPrice, double grossPrice, double taxPercentage)
        {
            return (grossPrice - basicPrice * (1 + taxPercentage *0.01)) / (1 + taxPercentage*0.01);
        }
    }
}