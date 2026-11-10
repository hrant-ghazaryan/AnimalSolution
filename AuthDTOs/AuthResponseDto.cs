namespace AuthDTOs;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }
}
