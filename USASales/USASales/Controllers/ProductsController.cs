﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using USASales.Models;
using USASales.Repositories;

namespace USASales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ProductsService _productsService;
        public ProductsController(IProductsRepository productsRepository, ProductsService productsService)
        {
            _productsRepository = productsRepository;
            _productsService = productsService;
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
            return Json(await _productsRepository.Get(id));
        }

        //just for test
        [HttpGet("test")]//TODO: REMOVE
        public async Task<IActionResult> Test()
        {
           return Json(await _productsService.CalculatePrice(5, "Alabama", "Groceries"));
        }
    }
}