using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class BoardPost
{
    [Key]
    public int Id { get; set; }
    public int PostsId { get; set; }
    public string Availability { get; set; }

    public Post Post { get; set; }
}
