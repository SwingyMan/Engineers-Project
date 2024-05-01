using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public DateTime CreatedAt { get; set; }
    public string IpOfRegistry { get; set; }
    public IEnumerable<GroupUser>? GroupUsers { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<ChatUser> ChatUsersFirst { get; set; }
    public ICollection<ChatUser> ChatUsersSecond { get; set; }
}
