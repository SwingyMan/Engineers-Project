using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class Post
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; }
    public string Availability { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; }
    public IEnumerable<GroupPost>? GroupPosts { get; set; }
    //public BoardPost BoardPost { get; set; }
    public ICollection<PostsTag> PostsTags { get; set; }
}
