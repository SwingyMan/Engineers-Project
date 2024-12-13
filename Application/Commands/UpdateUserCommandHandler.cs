

using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly SocialPlatformDbContext _context;

    public UpdateUserCommandHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }


    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _context.Users.Single(x => x.Id == request.UserId);
        if (request.Email != string.Empty)
        {
            user.Email = request.Email;
        }

        if (request.Password != string.Empty)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        }

        if (request.UserName != string.Empty)
        {
            user.Username = request.UserName;
        }
        await _context.SaveChangesAsync(cancellationToken);
        return user;    
    }
}