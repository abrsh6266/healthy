using Microsoft.AspNetCore.Mvc;
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
//    [Authorize]
    public class DiseaseController : ControllerBase
    {
        private readonly DiseaseService _diseaseService;

        public DiseaseController(DiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        [Authorize(Roles = "USER")]
        [HttpGet]
        public async Task<List<Disease>> Get()
        {
            return await _diseaseService.GetAsync();
        }

        [Authorize(Roles = "USER")]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var diseases = await _diseaseService.GetByNameAsync(name);

            if (diseases == null || diseases.Count == 0)
            {
                return NotFound();
            }

            return Ok(diseases);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Disease disease)
        {
            await _diseaseService.CreateAsync(disease);
            return CreatedAtAction(nameof(Get), new { id = disease.Id }, disease);
        }
    }
}