
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, JwtToken>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RegisterCommandHandler(SocialPlatformDbContext context, IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _context = context;
        _mediator = mediator;
    }
    public async Task<JwtToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var mappedEntity = _mapper.Map<User>(request.UserRegisterDto);
        var test = await _mediator.Send(new EmailQuery(request.UserRegisterDto.Email));
        if (test == false)
        {
            var entity = await _context.AddAsync(mappedEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Entity.CreateToken(entity.Entity.Username, entity.Entity.Email, entity.Entity.Id, entity.Entity.Role.Name);
        }
        else
        {
            return null;
        }
    }
}
