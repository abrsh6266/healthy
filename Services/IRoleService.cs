using Auth.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Services
{
    public interface IRoleService
    {
        Task<IActionResult> CreateRoleAsync(CreateRoleRequest request);
    }
}