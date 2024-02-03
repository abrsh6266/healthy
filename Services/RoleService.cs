using Auth.Dtos;
using Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateRoleAsync(CreateRoleRequest request)
        {
            try
            {
                var appRole = new ApplicationRole { Name = request.Role };
                var createRole = await _roleManager.CreateAsync(appRole);

                return createRole.Succeeded
                    ? new OkObjectResult(new { message = "Role created successfully" })
                    : new BadRequestObjectResult(new { message = $"Failed to create role: {createRole?.Errors?.FirstOrDefault()?.Description}" });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}