using AuthDTOs;

namespace AuthServices;

public interface IAuthenticationService
{
    Task RegisterAsync(RegisterRequestDto request);

    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
}