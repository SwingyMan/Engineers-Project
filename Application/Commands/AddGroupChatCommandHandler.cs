using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class AddGroupChatCommandHandler : IRequestHandler<AddGroupChatCommand, Chat>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IMapper _mapper;

    public AddGroupChatCommandHandler(SocialPlatformDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Chat> Handle(AddGroupChatCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
