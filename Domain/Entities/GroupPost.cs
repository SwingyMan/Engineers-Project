using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class GroupPost
{
    [Key]
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public Guid PostId { get; set; }

    public Group Group { get; set; }
    public Post Post { get; set; }
}
