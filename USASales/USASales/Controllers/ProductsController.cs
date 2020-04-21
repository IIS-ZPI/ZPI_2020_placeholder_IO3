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

        public ProductsController(IProductsRepository productsRepository, ITaxesRepository taxesRepository)
        {
            _productsRepository = productsRepository;
            _taxesRepository = taxesRepository;
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
            var product = await _productsRepository.Get(1);
            var tax = await _taxesRepository.Get(2);

            return Json(ProductsService.CalculatePrice(product, tax));
        }
    }
}