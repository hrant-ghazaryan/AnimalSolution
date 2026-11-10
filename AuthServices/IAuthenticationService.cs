using AuthDTOs;

namespace AuthServices;

public interface IAuthenticationService
{
    Task<bool> RegisterAsync(RegisterRequestDto request);

    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
}