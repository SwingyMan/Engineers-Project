using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Entities;

public class User
{
    [Key] public Guid Id { get; set; }

    public string Username { get; set; }
    public string Email { get; set; }
    [JsonIgnore] public string Password { get; set; }
    public Guid RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string IpOfRegistry { get; set; }

    [JsonIgnore] public Role Role { get; set; }

    [JsonIgnore] public ICollection<GroupUser> GroupUsers { get; set; }

    [JsonIgnore] public ICollection<Post> Posts { get; set; }

    [JsonIgnore] public ICollection<Message> Messages { get; set; }

    [JsonIgnore] public ICollection<ChatUser> ChatUsers { get; set; }




    public JwtToken CreateToken(string username, string email, Guid id, string role)
    {
        var claims = new[]
        {
            new Claim("username", username),
            new Claim("email", email),
            new Claim("id", id.ToString()),
            new Claim("role", role)
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            "ThisismySecretKey213213123213231123123123124325758346456436245621345124321414124214421421421"));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            "Test.com",
            "Test.com",
            claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signingCredentials);

        return new JwtToken(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
    }
}