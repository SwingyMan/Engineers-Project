using System.Collections.Generic;

namespace Domain.Entities;

public class GroupChat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<ChatMessage>? ChatMessages { get; set; }
}
