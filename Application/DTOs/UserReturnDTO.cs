namespace Application.DTOs;

public class UserReturnDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string AvatarName { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}