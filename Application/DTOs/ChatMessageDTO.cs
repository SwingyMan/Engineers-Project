namespace Application.DTOs;

public class ChatMessageDTO
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid MessageId { get; set; }
}