using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USASales.Repositories;

namespace USASales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : Controller
    {
        private readonly IStatesRepository _statesRepository;

        public StatesController(IStatesRepository statesRepository)
        {
            _statesRepository = statesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await _statesRepository.GetAll());
        }
    }
}