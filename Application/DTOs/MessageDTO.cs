namespace Application.DTOs;

public class MessageDTO
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
}
