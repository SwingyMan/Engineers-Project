using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string IpOfRegistry { get; set; }

    public Role Role { get; set; }
    public ICollection<GroupUser> GroupUsers { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<Message> Messages { get; set; }
    public ICollection<ChatUser> ChatUsers { get; set; }


    public JwtToken CreateToken(string username, string email, Guid id, string role)
    {
        var claims = new[]
            {
                new Claim("username", username),
                new Claim("email", email),
                new Claim("id", id.ToString()),
                new Claim("role",role)

            };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisismySecretKey213213123213231123123123124325758346456436245621345124321414124214421421421"));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: "Test.com",
            audience: "Test.com",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signingCredentials);

        return new JwtToken(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
    }
}
