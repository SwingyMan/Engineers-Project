using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class RequestToGroupCommandHandler : IRequestHandler<RequestToGroupCommand, GroupUser>
{
    private readonly SocialPlatformDbContext _context;

    public RequestToGroupCommandHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<GroupUser> Handle(RequestToGroupCommand request, CancellationToken cancellationToken)
    {
        var groupUser = new GroupUser(request.UserId, request.GroupId);
        var check = _context.GroupUsers.Single(x=>x.GroupId == request.GroupId && x.UserId == request.UserId);
        if (check != null)
        {
            return null;
        }
        _context.GroupUsers.Add(groupUser);
        await _context.SaveChangesAsync(cancellationToken);
        return groupUser;
    }
}