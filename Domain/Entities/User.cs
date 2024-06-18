using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Domain.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    [JsonIgnore]
    public Guid RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string IpOfRegistry { get; set; }
    public string RoleName => Role.Name;
    [JsonIgnore]
    public Role Role { get; set; }
    
    public ICollection<GroupUser> GroupUsers { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<Message> Messages { get; set; }
    public ICollection<ChatUser> ChatUsers { get; set; }//
}
