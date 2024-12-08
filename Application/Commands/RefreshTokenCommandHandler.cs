using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand,string>
{
    private readonly SocialPlatformDbContext _context;

    public RefreshTokenCommandHandler(SocialPlatformDbContext context)
    {
        _context = context;
    }
    public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = _context.RefreshTokens.Single(x => x.Token == request.RefreshToken && x.ExpiryDate > DateTime.Now);
        if (token is null)
        {
            return null;
        }
        else
        {
            var user =await _context.Users.Include(x=>x.Role).SingleAsync(u => u.Id == token.UserId);
            var jwtToken = user.CreateToken(user.Username, user.Email, user.Id, user.Role.Name);
            return jwtToken.Token;
        }
    }
}