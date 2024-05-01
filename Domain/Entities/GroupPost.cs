using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class GroupPost
{
    [Key]
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int PostId { get; set; }

    public Group Group { get; set; }
    public Post Post { get; set; }
}
