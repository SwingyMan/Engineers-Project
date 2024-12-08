﻿using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AddFriendsCommand : IRequest<Friends>
{
    public Guid UserId1 { get; set; }
    public Guid UserId2 { get; set; }

    public AddFriendsCommand(Guid userId1, Guid userId2)
    {
        UserId1 = userId1;
        UserId2 = userId2;
    }
}