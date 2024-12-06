using Domain.Entities;

namespace Application.DTOs;

public class PostDTO
{
    public string Title { get; set; }
    public string Body { get; set; }
    public Availability Availability { get; set; }
    public Guid GroupId { get; set; }
    
}
