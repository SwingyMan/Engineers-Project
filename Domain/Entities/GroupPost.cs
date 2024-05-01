using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class GroupPost
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int PostId { get; set; }

    [ForeignKey("GroupId")]
    public Group Group { get; set; }
    [ForeignKey("PostId")]
    public Post Post { get; set; }
}
