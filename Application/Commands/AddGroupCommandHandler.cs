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
        var group = await _groupRepository.Add(entity);
        var userGroup = new GroupUser(group.Id,request.CallerId);
        userGroup.IsAccepted = true;
        userGroup.IsOwner = true;
        await _dbContext.GroupUsers.AddAsync(userGroup);
        await _dbContext.SaveChangesAsync();
        return group;
    }
}