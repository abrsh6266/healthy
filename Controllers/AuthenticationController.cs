using System.Net;
using Auth.Dtos;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/authenticate")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRoleService _roleService;

    public AuthenticationController(IAuthenticationService authenticationService, IRoleService roleService)
    {
        _authenticationService = authenticationService;
        _roleService = roleService;
    }

    [HttpPost]
    [Route("roles/add")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        return await _roleService.CreateRoleAsync(request);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authenticationService.RegisterAsync(request);
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(LoginResponse))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authenticationService.LoginAsync(request);
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }
}
