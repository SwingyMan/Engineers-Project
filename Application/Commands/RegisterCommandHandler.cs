using Application.Queries;
using AutoMapper;
using BCrypt.Net;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, JwtToken>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IEmailSender _emailSender;

    public RegisterCommandHandler(SocialPlatformDbContext context, IMapper mapper, IMediator mediator, IEmailSender emailSender)
    {
        _mapper = mapper;
        _context = context;
        _mediator = mediator;
        _emailSender = emailSender;
    }

    public async Task<JwtToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var mappedEntity = _mapper.Map<User>(request.UserRegisterDto);
        var emailExists = await _mediator.Send(new EmailQuery(request.UserRegisterDto.Email));


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

            // Account Activation stuff
            mappedEntity.ActivationToken = Guid.NewGuid();
            mappedEntity.IsActivated = false;

            var entity = await _context.AddAsync(mappedEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await SendActivationEmail(mappedEntity);

            return entity.Entity.CreateToken(entity.Entity.Username, entity.Entity.Email, entity.Entity.Id,
                entity.Entity.Role.Name);
        }

        return null;
    }

    private async Task SendActivationEmail(User user)
    {
        var activationLink = $"https://localhost:7290/api/v1/User/ActivateAccount?token={user.ActivationToken}";
        var emailBody = $"<html><body>" +
                        $"<p>Hello {user.Username},</p>" +
                        $"<p>Please activate your account by clicking the link below:</p>" +
                        $"<p><a href=\"{activationLink}\">Activate Account</a></p>" +
                        $"</body></html>";

        await _emailSender.SendEmailAsync(user.Email, "Account Activation", emailBody);
    }
}