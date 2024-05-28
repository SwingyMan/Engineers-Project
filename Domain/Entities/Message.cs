using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Message
{
    [Key]
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}
