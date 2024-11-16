using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Friends
{   
    [Key]
    public Guid Id { get; set; }
    public Guid UserId1 { get; set; }
    public Guid UserId2 { get; set; }
    public bool Accepted { get; set; } = false;
}