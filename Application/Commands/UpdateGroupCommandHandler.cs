using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Group>
{
    private readonly IGroupRepository _repository;
    private readonly SocialPlatformDbContext _dbContext;
    public UpdateGroupCommandHandler(IGroupRepository repository, SocialPlatformDbContext dbContext)
    {
        _repository = repository;
        _dbContext = dbContext;
    }
    public async Task<Group> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var user = _dbContext.GroupUsers.Single(x =>
            x.UserId == request.UserId && x.GroupId == request.Groupid && x.IsOwner == true);
        if (user == null)
            return null;
        var group = new Group();
        group.Description = request.GroupDescription;
        group.Name = request.GroupName;
        return await _repository.Update(request.Groupid,group);
    }
}
