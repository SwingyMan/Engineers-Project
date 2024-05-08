using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Identity;

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
    public ICollection<ChatUser> ChatUsers { get; set; }//
}
