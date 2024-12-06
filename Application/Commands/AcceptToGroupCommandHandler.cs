using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class AcceptToGroupCommandHandler : IRequestHandler<AcceptToGroupCommand,GroupUser>
{
    private readonly SocialPlatformDbContext _context;

    public AcceptToGroupCommandHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<GroupUser> Handle(AcceptToGroupCommand request, CancellationToken cancellationToken)
    {
        var caller =_context.GroupUsers.Single(x=>x.UserId == request.CallerId&&x.GroupId == request.GroupId && x.IsOwner==true);
        if (caller == null)
            return null;
      var groupUser =await  _context.GroupUsers.SingleAsync(x => x.UserId == request.UserId && x.GroupId == request.GroupId);
      groupUser.IsAccepted = true;
      await _context.SaveChangesAsync();
      return groupUser;
    }
}