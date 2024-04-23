using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
    public ICollection<GroupPost> GroupPosts { get; set; }
    public ICollection<BoardPost> BoardPosts { get; set; }
    public ICollection<PostsTag> PostsTags { get; set; }
}
