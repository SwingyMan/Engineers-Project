namespace Application.DTOs;

public class PostsTagDTO
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid TagId { get; set; }
}
