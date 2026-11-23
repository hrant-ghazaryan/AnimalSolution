using AnimalExceptions;
using AuthDTOs;
using AuthModels.Data;
using AuthModels.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthServices;

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthDbContext context;
    private readonly IPasswordHasher<ApplicationUser> passwordHasher;
    private readonly IConfiguration configuration;

    public AuthenticationService(
        AuthDbContext context,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IConfiguration configuration)
    {
        this.context = context;
        this.passwordHasher = passwordHasher;
        this.configuration = configuration;
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var userName = request.UserName.Trim();

        var alreadyExists = await context.Users.AnyAsync(user =>
            user.Email == email ||
            user.UserName == userName);

        if (alreadyExists)
            throw new UserExistsException();

        if (request.Password.Length > 8)
            throw new Exception("User`s password can`t be longer than 8");

        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email
        };

        user.PasswordHash = passwordHasher.HashPassword(
            user,
            request.Password);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await context.Users
            .SingleOrDefaultAsync(u => u.Email == email);

        if (user is null)
            throw new LoginFailedException();

        var passwordResult = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (passwordResult == PasswordVerificationResult.Failed)
            throw new LoginFailedException();

        return CreateToken(user);
    }

    private AuthResponseDto CreateToken(ApplicationUser user)
    {
        var jwtKey = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT key is not configured.");

        var issuer = configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("JWT issuer is not configured.");

        var audience = configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("JWT audience is not configured.");

        var expiresInMinutes = configuration.GetValue<int>(
            "Jwt:ExpiresInMinutes");

        var expiresAt = DateTime.UtcNow.AddMinutes(expiresInMinutes);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new AuthResponseDto
        {
            AccessToken = new JwtSecurityTokenHandler().
                WriteToken(token),

            ExpiresAt = expiresAt
        };
    }

    public async Task<IEnumerable<UserGetDto>> GetAllUsers()
    {
        var users = await context.Users.Select(u => new UserGetDto
            {
                UserName = u.UserName,
                Email = u.Email
            }).ToListAsync();
        return users;
    }
}