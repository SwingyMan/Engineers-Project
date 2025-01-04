using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class LeaveGroupCommandHandler : IRequestHandler<LeaveGroupCommand>
{
    private readonly SocialPlatformDbContext _dbContext;

    public LeaveGroupCommandHandler(SocialPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task Handle(LeaveGroupCommand request, CancellationToken cancellationToken)
    {
        var user = _dbContext.GroupUsers.Where(x => x.GroupId == request.GroupId && x.UserId == request.UserId && x.IsOwner == false);
        _dbContext.GroupUsers.RemoveRange(user);
        _dbContext.SaveChanges();
        return Task.CompletedTask;
    }
}