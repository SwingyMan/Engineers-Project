
namespace Domain.Entities;

public class JwtToken
{
    public JwtToken(string token)
    {
        Token = token;
    }
    public string Token { get; set; }

}