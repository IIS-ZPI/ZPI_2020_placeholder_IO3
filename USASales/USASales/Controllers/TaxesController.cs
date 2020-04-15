using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using USASales.Repositories;

namespace USASales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxesController : Controller
    {
        private readonly ITaxesRepository _taxesRepository;

        public TaxesController(ITaxesRepository taxesRepository)
        {
            _taxesRepository = taxesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await _taxesRepository.GetAll());
        }

        [HttpGet("{state}")]
        public async Task<IActionResult> GetMany(string state)
        {
            return Json(await _taxesRepository.GetMany(state));
        }

        [HttpGet("{state}/{category}")]
        public async Task<IActionResult> Get(string state, string category)
        {
            return Json(await _taxesRepository.Get(state, category));
        }
    }
}