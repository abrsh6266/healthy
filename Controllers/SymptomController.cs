using Microsoft.AspNetCore.Mvc;
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;
namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
//    [Authorize]
    public class SymptomController : Controller
    {
        private readonly SymptomService _symptomService;

        public SymptomController(SymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        [Authorize(Roles = "USER")]
        [HttpGet]
        public async Task<List<Symptom>> Get()
        {
            return await _symptomService.GetAllAsync();
        }

        [Authorize(Roles = "USER")]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var symptoms = await _symptomService.GetByNameAsync(name);

            if (symptoms == null || symptoms.Count == 0)
            {
                return NotFound();
            }

            return Ok(symptoms);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Symptom symptom)
        {
            await _symptomService.CreateAsync(symptom);
            return CreatedAtAction(nameof(Get), new { id = symptom.Id }, symptom);
        }
    }
}
