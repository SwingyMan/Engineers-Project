using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class GroupChat
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<ChatMessage>? ChatMessages { get; set; }
}
