using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class GroupUser
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GroupId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
    [ForeignKey("GroupId")]
    public Group Group { get; set; }
}
