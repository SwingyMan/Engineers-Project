using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ChatUser
{
    public int Id { get; set; }
    public int FirstUserId { get; set; }
    public int SecondUserId { get; set; }

    [ForeignKey("FirstUserId")]
    public User FirstUser { get; set; }
    [ForeignKey("SecondUserId")]
    public User SecondUser { get; set; }
}
