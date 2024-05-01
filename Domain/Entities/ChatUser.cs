using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ChatUser
{
    [Key]
    public int Id { get; set; }
    public int FirstUserId { get; set; }
    public int SecondUserId { get; set; }

    public User FirstUser { get; set; }
    public User SecondUser { get; set; }
}
