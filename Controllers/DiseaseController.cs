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
        [HttpGet]
        public async Task<List<Disease>> Get()
        {
            return await _diseaseService.GetAsync();
        }
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
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Disease disease)
        {
            await _diseaseService.CreateAsync(disease);
            return CreatedAtAction(nameof(Get), new { id = disease.Id }, disease);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDisease(string id)
        {
            var isRemoved = await _diseaseService.RemoveDiseaseAsync(id);

            if (isRemoved)
            {
                return Ok("Disease removed successfully");
            }

            return NotFound("Disease not found");
        }

    }
}