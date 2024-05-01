using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class GroupUser
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GroupId { get; set; }

    public User User { get; set; }
    public Group Group { get; set; }
}
