using Microsoft.AspNetCore.Mvc;
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;
namespace Auth.Controllers
{
//    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SymptomController : Controller
    {
        private readonly SymptomService _symptomService;

        public SymptomController(SymptomService symptomService)
        {
            _symptomService = symptomService;
        }
        [HttpGet]
        public async Task<List<Symptom>> Get()
        {
            return await _symptomService.GetAllAsync();
        }
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
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Symptom symptom)
        {
            await _symptomService.CreateAsync(symptom);
            return CreatedAtAction(nameof(Get), new { id = symptom.Id }, symptom);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSymptom(string id)
        {
            var isRemoved = await _symptomService.RemoveSymptomAsync(id);

            if (isRemoved)
            {
                return Ok("Symptom removed successfully");
            }

            return NotFound("Symptom not found");
        }

    }
}
