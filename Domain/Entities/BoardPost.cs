using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class BoardPost
{
    public int Id { get; set; }
    public int PostsId { get; set; }
    public string Availability { get; set; }

    [ForeignKey("PostsId")]
    public Post Post { get; set; }
}
