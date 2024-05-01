using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Group
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageLink { get; set; }
    public DateTime CreatedAt { get; set; }

    public IEnumerable<GroupUser>? GroupUsers { get; set; }
    public ICollection<GroupPost> GroupPosts { get; set; }
}
