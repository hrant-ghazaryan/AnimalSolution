using AuthDTOs;
using AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestDto request)
    {
        var isRegistered = await authenticationService
            .RegisterAsync(request);

        if (!isRegistered)
        {
            return Conflict(new
            {
                message = "User with this email or username already exists."
            });
        }

        return StatusCode(
            StatusCodes.Status201Created,
            new
            {
                message = "User registered successfully."
            });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
        [FromBody] LoginRequestDto request)
    {
        var result = await authenticationService
            .LoginAsync(request);

        if (result is null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        return Ok(result);
    }
}