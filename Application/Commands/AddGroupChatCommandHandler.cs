using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class AddGroupChatCommandHandler : IRequestHandler<AddGroupChatCommand, Chat?>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly IChatRepository _chatRepository;

    public AddGroupChatCommandHandler(SocialPlatformDbContext context, IMapper mapper, IUserRepository userRepository, IMediator mediator, IChatRepository chatRepository)
    {
        _context = context;
        _mapper = mapper;
        _userRepository = userRepository;
        _mediator = mediator;
        _chatRepository = chatRepository;
    }

    public async Task<Chat?> Handle(AddGroupChatCommand request, CancellationToken cancellationToken)
    {
        List<User> users = new List<User>();

        foreach (var userId in request.usersGuids)
        {
            User user = await _mediator.Send(new GenericGetByIdQuery<User>(userId));
            users.Add(user);
        }

        Chat chat = new Chat()
        {
            Users = users,
            IsGroupChat = true,
            Messages = new List<Message>(),
            Name = request.Name
        };

        return await _chatRepository.AddChatAsync(chat);
    }
}
