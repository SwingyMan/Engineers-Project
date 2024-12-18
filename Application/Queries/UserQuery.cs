using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class UserQuery : IRequest<List<User>>
{
    public string Username { get; set; }

    public UserQuery(string username)
    {
        Username = username;
    }
}