using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PostsTag
{
    [Key]
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid TagId { get; set; }

    public Post Post { get; set; }
    public Tag Tag { get; set; }
}
