using Domain.Entities;
using MediatR;
using Newtonsoft.Json;

namespace Application.Queries;

public class PostTitleQuery : IRequest<List<Post>>
{
    public string Title { get; set; }
    [JsonIgnore]
    public Guid UserId { get; set; }

    public PostTitleQuery(string title, Guid userId)
    {
        Title = title;
        UserId = userId;
    }
}