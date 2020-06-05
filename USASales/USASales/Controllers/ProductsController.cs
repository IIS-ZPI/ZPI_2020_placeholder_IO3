using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using USASales.Models;
using USASales.Models.Dto;
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
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepository productsRepository, ITaxesRepository taxesRepository, IStatesRepository statesRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _taxesRepository = taxesRepository;
            _statesRepository = statesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await _productsRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

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
                prices.Add(ProductsService.CalculatePrice(product, tax, 1));
            }

            var detailedProduct = new DetailedProduct
            {
                Product = product,
                PriceInStates = prices
            };

            return Json(detailedProduct);
        }

        [HttpGet("{id}/{amount}")]
        public async Task<IActionResult> Get(long id, int amount)
        {
            if (amount < 1) return BadRequest();

            var product = await _productsRepository.Get(id);
            var prices = new List<DetailedPrice>();

            var states = await _statesRepository.GetAll();
            foreach (var state in states)
            {
                var tax = await _taxesRepository.Get(state.Name, product.Category);
                prices.Add(ProductsService.CalculatePrice(product, tax, amount));
            }

            var detailedProduct = new DetailedProduct
            {
                Product = product,
                PriceInStates = prices
            };

            return Json(detailedProduct);
        }

        [HttpGet("{productId}/{state}/{amount}")]
        public async Task<IActionResult> GetPrice(long productId, string state, int amount)
        {
            if (amount < 1) return BadRequest();

            var product = await _productsRepository.Get(productId);
            var tax = await _taxesRepository.Get(state, product.Category);

            return Json(ProductsService.CalculatePrice(product, tax, amount));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            await _productsRepository.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _productsRepository.Update(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}