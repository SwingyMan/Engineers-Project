using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class RemoveFromGroupCommandHandler : IRequestHandler<RemoveFromGroupCommand>
{
    private readonly SocialPlatformDbContext _context;

    public RemoveFromGroupCommandHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task Handle(RemoveFromGroupCommand request, CancellationToken cancellationToken)
    {
       var groupOwner= await _context.GroupUsers.SingleAsync(x => x.UserId == request.CallerId);
       if (groupOwner.IsOwner == false)
       {
           
       }
       var assignement = _context.GroupUsers.Single(x => x.GroupId == request.GroupId && x.UserId == request.UserId);
       _context.GroupUsers.Remove(assignement);
    }
}