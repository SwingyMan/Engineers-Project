using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class AddGroupCommandHandler : IRequestHandler<AddGroupCommand,Group>
{
    private readonly IGenericRepository<Group> _groupRepository;
    private readonly SocialPlatformDbContext _dbContext;
    private readonly IMapper _mapper;
    public AddGroupCommandHandler(IGenericRepository<Group> groupRepository, SocialPlatformDbContext dbContext, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Group> Handle(AddGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Group>(request.GroupDto);
        entity.CreatedAt = DateTime.Now;
        entity.ImageLink = "default.jpg";
        var group = await _groupRepository.Add(entity);
        var userGroup = new GroupUser(request.CallerId,group.Id);
        userGroup.IsAccepted = true;
        userGroup.IsOwner = true;
        _dbContext.GroupUsers.Add(userGroup);
        _dbContext.SaveChanges();
        group.GroupUsers = null;
        return group;
    }
}