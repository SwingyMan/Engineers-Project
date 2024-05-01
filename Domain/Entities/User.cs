using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public IEnumerable<GroupUser>? GroupUsers { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<ChatUser> ChatUsersFirst { get; set; }
    public ICollection<ChatUser> ChatUsersSecond { get; set; }
}
