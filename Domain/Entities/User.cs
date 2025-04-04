﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class User
{
    [Key] public Guid Id { get; set; }

    public string Username { get; set; }
    public string Email { get; set; }
    [JsonIgnore] public string Password { get; set; }
    public Guid RoleId { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public string IpOfRegistry { get; set; }
    public string AvatarFileName { get; set; } = null;
    [JsonIgnore]
    public Guid? ActivationToken { get; set; }
    [JsonIgnore]
    public bool IsActivated { get; set; } = false;

    [JsonIgnore] 
    public Role Role { get; set; }
    [JsonIgnore] 
    public ICollection<Friends> FriendsInitiated { get; set; }
    [JsonIgnore] 
    public ICollection<Friends> FriendsSent { get; set; }
    [JsonIgnore] 
    public ICollection<GroupUser> GroupUsers { get; set; }

    [JsonIgnore] 
    public ICollection<Post> Posts { get; set; }

    [JsonIgnore] 
    public ICollection<Message> Messages { get; set; }


    [JsonIgnore]
    public ICollection<Chat> Chats { get; set; }

    [JsonIgnore]
    public RefreshToken RefreshToken { get; set; }

    public JwtToken CreateToken(string username, string email, Guid id, string role)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
        var claims = new[]
        {
            new Claim("username", username),
            new Claim("email", email),
            new Claim("id", id.ToString()),
            new Claim("role", role)
        };
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
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