using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class FriendQuery : IRequest<FriendsDTO>
{
    public Guid UserId { get; set; }

    public FriendQuery(Guid userId)
    {
        UserId = userId;
    }
}