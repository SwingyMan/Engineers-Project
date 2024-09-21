using Application.Queries;
using AutoMapper;
using BCrypt.Net;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
        var emailExists = await _mediator.Send(new EmailQuery(request.UserRegisterDto.Email));

        //mappedEntity.IpOfRegistry = request.HttpContext.Connection.RemoteIpAddress;

        if (!emailExists)
        {
            mappedEntity.IpOfRegistry = request.IpAddress.ToString();

            mappedEntity.Password = BCrypt.Net.BCrypt.HashPassword(request.UserRegisterDto.Password);

            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "USER", cancellationToken);

            if (userRole == null)
            {
                userRole = new Role { Name = "USER" };
                await _context.Roles.AddAsync(userRole, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            mappedEntity.RoleId = userRole.Id;
            mappedEntity.Role = userRole;

            var entity = await _context.AddAsync(mappedEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Entity.CreateToken(entity.Entity.Username, entity.Entity.Email, entity.Entity.Id,
                entity.Entity.Role.Name);
        }

        return null;
    }
}