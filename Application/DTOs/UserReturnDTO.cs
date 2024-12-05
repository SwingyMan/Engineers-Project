namespace Application.DTOs;

public class UserReturnDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string AvatarFileName { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}