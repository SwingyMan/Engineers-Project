using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ChatUser
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }//
    public Guid ChatId { get; set; }
    public Guid MessageId { get; set; }


    public User User { get; set; }//
    public Chat Chat { get; set; }
    public Message Message { get; set; }
}
