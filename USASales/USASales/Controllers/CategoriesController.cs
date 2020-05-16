using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USASales.Repositories;

namespace USASales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await _categoriesRepository.GetCategoriesNames());
        }
    }
}