﻿using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Commands;

public class AddFriendsCommandHandler : IRequestHandler<AddFriendsCommand,Friends>
{
    private readonly SocialPlatformDbContext _context;

    public AddFriendsCommandHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<Friends> Handle(AddFriendsCommand request, CancellationToken cancellationToken)
    {
        var friends = new Friends();
        friends.UserId1 = request.UserId1;
        friends.UserId2 = request.UserId2;
        var check = _context.Friends.SingleOrDefault(x=>(x.UserId1==request.UserId1 && x.UserId2==request.UserId2)|| (x.UserId1==request.UserId2 && x.UserId2==request.UserId1));
        if (check != null)
        {
            return null;
        }
        await _context.Friends.AddAsync(friends,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return friends;
    }
}