using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USASales.Models;
using USASales.Repositories;

namespace USASales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ITaxesRepository _taxesRepository;
        private readonly IStatesRepository _statesRepository;

        public ProductsController(IProductsRepository productsRepository, ITaxesRepository taxesRepository, IStatesRepository statesRepository)
        {
            _productsRepository = productsRepository;
            _taxesRepository = taxesRepository;
            _statesRepository = statesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await _productsRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            await _productsRepository.Add(product);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var product = await _productsRepository.Get(id);
            var prices = new List<DetailedPrice>();

            var states = await _statesRepository.GetAll();
            foreach (var state in states)
            {
                var tax = await _taxesRepository.Get(state.Name, product.Category);
                prices.Add(ProductsService.CalculatePrice(product, tax));
            }

            var detailedProduct = new DetailedProduct
            {
                Product = product,
                PriceInStates = prices
            };

            return Json(detailedProduct);
        }

        [HttpGet("{productId}/{state}")]
        public async Task<IActionResult> GetPrice(long productId, string state)
        {
            var product = await _productsRepository.Get(productId);
            var tax = await _taxesRepository.Get(state, product.Category);

            return Json(ProductsService.CalculatePrice(product, tax));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            await _productsRepository.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                await _productsRepository.Update(product);
            }

            catch (Exception)
            {
                throw new ArgumentException();
            }

            return Ok();
        }
    }
}