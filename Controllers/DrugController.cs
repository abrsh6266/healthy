using Microsoft.AspNetCore.Mvc;
using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DrugController : Controller
    {
        private readonly DrugService _drugService;

        public DrugController(DrugService drugService)
        {
            _drugService = drugService;
        }

        [HttpGet]
        public async Task<List<Drug>> Get()
        {
            return await _drugService.GetAllAsync();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var drugs = await _drugService.GetByNameAsync(name);

            if (drugs == null || drugs.Count == 0)
            {
                return NotFound();
            }

            return Ok(drugs);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Drug drug)
        {
            await _drugService.CreateAsync(drug);
            return CreatedAtAction(nameof(Get), new { id = drug.Id }, drug);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDrug(string id)
        {
            var isRemoved = await _drugService.RemoveDrugAsync(id);

            if (isRemoved)
            {
                return Ok("Drug removed successfully");
            }

            return NotFound("Drug not found");
        }

    }
}